using Application.Commands;
using Application.Commands.ViewModels;
using Application.Queries.ViewModels;
using AutoMapper;
using Domain;
using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries
{
    public class GetCustomerIDQuery : IRequest<CheckedCustomerIDDTO>
    {
        public OrderAndCustomerIDsDTO orderAndCustomerIDsDTO { get; set; } 
    }

    public class GetCustomerIDHandler : IRequestHandler<GetCustomerIDQuery, CheckedCustomerIDDTO>
    {
        private readonly CustomerDbContext _customerDbContext;
        private readonly IMediator _mediator;
        public GetCustomerIDHandler(CustomerDbContext customerDbContext, IMediator mediator)
        {
            _customerDbContext = customerDbContext;
            _mediator = mediator;
        }
        public async Task<CheckedCustomerIDDTO> Handle(GetCustomerIDQuery request, CancellationToken cancellationToken)
        {

            var checkedCustomerIDDTO = new CheckedCustomerIDDTO();
            checkedCustomerIDDTO.Excepted = false; // по умолчанию всё ок
            var customerID = await _customerDbContext
                .Customers.Select(customer => customer.Id)
                        .FirstOrDefaultAsync(CustomerID => CustomerID == request.orderAndCustomerIDsDTO.CustomerID);

            if (customerID == null)
            {
                checkedCustomerIDDTO.Excepted = true;   // если нет клиента с данным ID
            }
            else
            {
                var command = new ChangeCustomerOrderIDsCommand
                {
                    CustomerID = customerID,
                    OrderID = request.orderAndCustomerIDsDTO.OrderID
                };
                await _mediator.Send(command);
                var customerBithday = await _customerDbContext
                    .Customers.Where(customer => customer.Id == customerID)
                        .Select(Customer => Customer.DateOfBirth)
                            .SingleOrDefaultAsync();

                
                checkedCustomerIDDTO.dateOfBirth = customerBithday;
            }
            return checkedCustomerIDDTO;
        }
    }
}
