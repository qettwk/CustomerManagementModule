using Domain;
using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class ChangeCustomerOrderIDsCommand : IRequest<Unit>
    {
        public Guid CustomerID { get; set; }
        public Guid OrderID { get; set; }
    }

    public class ChangeCustomerOrderIDsHandler : IRequestHandler<ChangeCustomerOrderIDsCommand, Unit>
    {
        private readonly CustomerDbContext _customerDbContext;
        public ChangeCustomerOrderIDsHandler(CustomerDbContext customerDbContext)
        {
            _customerDbContext = customerDbContext;
        }

        public async Task<Unit> Handle(ChangeCustomerOrderIDsCommand command, CancellationToken cancellationToken)
        {
            var customer = await _customerDbContext.Customers
                        .FirstOrDefaultAsync(c => c.Id == command.CustomerID);
            
            customer.OrderIDs.Add(command.OrderID);

            await _customerDbContext.SaveChangesAsync();
            return Unit.Value;
        }
    }
}
