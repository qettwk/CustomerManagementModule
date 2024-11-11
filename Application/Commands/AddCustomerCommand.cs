using Application.Commands.ViewModels;
using AutoMapper;
using Domain;
using Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class AddCustomerCommand : IRequest<Guid>
    {
        public CustomerAddEditDTO customerAddEditDTO { get; set; }
    }

    public class AddCustomerHandler : IRequestHandler<AddCustomerCommand, Guid>
    {
        private readonly CustomerDbContext _customerDbContext;
        private readonly IMapper _mapper;
        public AddCustomerHandler(CustomerDbContext customerDbContext, IMapper mapper) 
        {
            _customerDbContext = customerDbContext;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(AddCustomerCommand command, CancellationToken cancellationToken)
        {
            Customer customer = _mapper.Map<Customer>(command.customerAddEditDTO);
            customer.Id = new Guid();
            
            await _customerDbContext.Customers.AddAsync(customer);
            await _customerDbContext.SaveChangesAsync();

            return customer.Id;
        }
    }
}
