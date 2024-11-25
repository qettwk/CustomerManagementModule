using Application.Commands;
using Application.Commands.ViewModels;
using Application.Queries;
using Application.Queries.ViewModels;
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




        // если надо передать так же и OrderId, то как всё надо поменять?
        // надо ли создавать отдельно DTO под 2 Guid?
        // если передавать в одном DTO для автомобилей и клиентов, то клиентам
        // просто так будут присылаться ID автомобилей,
        // а автомобилям - ID клиентов

        // если надо будет создавать отдельно DTO под 2 Guid, то можно ли
        // как-то распределить DTO по папкам более удобно?

        // или через анонимный тип?
        // или к каждой команде, запросу ниже писать DTO?
        [HttpPost]
        public async Task<IActionResult> SendCustomerIDToOrder([FromBody] OrderAndCustomerIDsDTO orderAndCustomerIDsDTO)
        {
            GetCustomerIDQuery query = new GetCustomerIDQuery
            {
                orderAndCustomerIDsDTO = orderAndCustomerIDsDTO
            };

            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
