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




        // ���� ���� �������� ��� �� � OrderId, �� ��� �� ���� ��������?
        // ���� �� ��������� �������� DTO ��� 2 Guid?
        // ���� ���������� � ����� DTO ��� ����������� � ��������, �� ��������
        // ������ ��� ����� ����������� ID �����������,
        // � ����������� - ID ��������

        // ���� ���� ����� ��������� �������� DTO ��� 2 Guid, �� ����� ��
        // ���-�� ������������ DTO �� ������ ����� ������?

        // ��� ����� ��������� ���?
        // ��� � ������ �������, ������� ���� ������ DTO?
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
