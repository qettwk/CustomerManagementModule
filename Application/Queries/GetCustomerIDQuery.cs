using Application.Queries.ViewModels;
using AutoMapper;
using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries
{
    public class GetCustomerIDQuery : IRequest<CheckedCustomerIDDTO>
    {
        public Guid ID { get; set; }
    }

    public class GetCustomerIDHandler : IRequestHandler<GetCustomerIDQuery, CheckedCustomerIDDTO>
    {
        private readonly CustomerDbContext _customerDbContext;
        public GetCustomerIDHandler(CustomerDbContext customerDbContext)
        {
            _customerDbContext = customerDbContext;
        }
        public async Task<CheckedCustomerIDDTO> Handle(GetCustomerIDQuery request, CancellationToken cancellationToken)
        {
            var checkedCustomerIDDTO = new CheckedCustomerIDDTO();
            
            checkedCustomerIDDTO.ID = await _customerDbContext.Customers.Select(c => c.Id).FirstOrDefaultAsync(ID => ID == request.ID);

            if (checkedCustomerIDDTO.ID == Guid.Empty)
            {
                checkedCustomerIDDTO.Excepted = true;
            }
            else
            {
                checkedCustomerIDDTO.Excepted = false;
            }

            return checkedCustomerIDDTO;
        }
    }
}
