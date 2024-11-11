using Domain;
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
    public class GetCustomerQuery : IRequest<Customer>
    {
        public Guid Id { get; set; }
    }

    public class GetCustomerHandler : IRequestHandler<GetCustomerQuery, Customer>
    {
        
        private readonly CustomerDbContext _customerDbContext;

        public GetCustomerHandler(CustomerDbContext customerDbContext)
        {
            _customerDbContext = customerDbContext;
        }

        public async Task<Customer> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
        {
            var customer = await _customerDbContext.Customers.Where(c => c.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
            return customer;
        }
    }
}
    
        



