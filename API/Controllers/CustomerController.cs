using Application.Commands;
using Application.Queries;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }



        [HttpGet("{customerId}")]
        public Task<Customer> GetCustomer(Guid customerId)
        {
            return _mediator.Send(new GetCustomerQuery { Id = customerId });
        }




        [HttpPost]
        public async Task<ActionResult> AddCustomer(AddCustomerCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> DeleteCustomer(Guid CustomerId)
        {
            var command = new DeleteCustomerCommand
            {
                Id = CustomerId
            };
            await _mediator.Send(command);

            return Ok();
        }





        [HttpGet("{Id}")]
        public async Task<ActionResult<Guid>> SendCustomerIDToOrder(Guid Id)
        {
            GetCustomerIDQuery query = new GetCustomerIDQuery
            {
                ID = Id
            };
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
