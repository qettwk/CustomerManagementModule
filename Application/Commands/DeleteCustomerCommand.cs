using Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class DeleteCustomerCommand : IRequest // response?
    {
        public Guid Id { get; set; }
    }

    public class DeleteCustomerHandler : IRequestHandler<DeleteCustomerCommand> // что возвращать
    {
        private readonly CustomerDbContext _customerDbContext;
        public DeleteCustomerHandler(CustomerDbContext customerDbContext)
        {
            _customerDbContext = customerDbContext;
        }
        public async Task Handle(DeleteCustomerCommand command, CancellationToken cancellationToken)
        {
            var customer = await _customerDbContext.Customers.FindAsync(command.Id);
            if (customer != null)
            {
                _customerDbContext.Remove(customer);
                await _customerDbContext.SaveChangesAsync();
            }
        }
    }

}
