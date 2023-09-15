using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SA_Project.Models;
using SA_Project.Models.Order;
using SA_Project.Repository.IRepository;
using System.Net;

namespace SA_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly APIResponse _apiResponse;

        public OrderController(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _apiResponse = new APIResponse();
        }

        [HttpGet("getAllOrders")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> GetAllOrders()
        {
            try
            {
                List<Order> orders = await _orderRepository.GetAll();
                if (orders == null) 
                {
                    return NotFound();
                }

                List<OrderDto> orderDtos = _mapper.Map<List<OrderDto>>(orders);

                _apiResponse.statusCode = HttpStatusCode.OK;
                _apiResponse.IsSuccess = true;
                _apiResponse.Result = orderDtos;
                return _apiResponse;
            }
            catch (Exception ex)
            {
                _apiResponse.statusCode = HttpStatusCode.BadRequest;
                _apiResponse.IsSuccess = false;
                _apiResponse.ErrorMessage = ex.Message;
                return _apiResponse;
            }
        }

        [HttpGet("order{orderId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> GetOrderById(int orderId)
        {
            try
            {
                Order order = await _orderRepository.GetById(tracked: false, filter: x => x.Id == orderId);
                if (order == null)
                {
                    return NotFound();
                }

                OrderDto orderDto = _mapper.Map<OrderDto>(order);

                _apiResponse.statusCode = HttpStatusCode.OK;
                _apiResponse.IsSuccess = true;
                _apiResponse.Result = orderDto;
                return _apiResponse;
            }
            catch (Exception ex)
            {
                _apiResponse.statusCode = HttpStatusCode.BadRequest;
                _apiResponse.IsSuccess = false;
                _apiResponse.ErrorMessage = ex.Message;
                return _apiResponse;
            }
        }

        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> CreateOrder([FromBody] OrderDto orderDto)
        {
            try 
            {
                if (orderDto == null)
                {
                    return BadRequest();
                }

                Order order = _mapper.Map<Order>(orderDto);

                await _orderRepository.Create(order);

                _apiResponse.statusCode = HttpStatusCode.OK;
                _apiResponse.IsSuccess = true;
                return _apiResponse;
            }
            catch (Exception ex)
            {
                _apiResponse.statusCode = HttpStatusCode.BadRequest;
                _apiResponse.IsSuccess = false;
                _apiResponse.ErrorMessage = ex.Message;
                return _apiResponse;
            }
        }
    }
}
