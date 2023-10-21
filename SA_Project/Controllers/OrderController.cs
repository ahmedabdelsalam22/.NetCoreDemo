using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SA_Project.Models;
using SA_Project.Models.Order;
using SA_Project_API.Services.Repository.IRepository;
using SA_Project_API.Utilities;
using System.Net;

namespace SA_Project.Controllers
{
    [Route("api/")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrderController(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        [Authorize(Roles =$"{SD.ADMIN}")]
        [HttpGet("orders")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<OrderDto>>> GetAllOrders()
        {
            try
            {
                List<Order> orders = await _orderRepository.GetAll();
                if (orders == null) 
                {
                    return NotFound();
                }

                List<OrderDto> orderDtos = _mapper.Map<List<OrderDto>>(orders);

                return Ok(orderDtos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpGet("order/{orderId}")]
        [Authorize(Roles = $"{SD.ADMIN}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<OrderDto>> GetOrderById(int? orderId)
        {
            try
            {
                if (orderId == 0 || orderId == null) 
                {
                    return BadRequest();
                }
                Order order = await _orderRepository.GetById(tracked: false, filter: x => x.Id == orderId);
                if (order == null)
                {
                    return NotFound();
                }

                OrderDto orderDto = _mapper.Map<OrderDto>(order);

                return Ok(orderDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [Authorize]
        [HttpPost("order/create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreateOrder([FromBody] OrderDto orderDto)
        {
            try 
            {
                if (orderDto == null)
                {
                    return NotFound();
                }

                Order order = _mapper.Map<Order>(orderDto);

                await _orderRepository.Create(order);
                await _orderRepository.Save();

                return Ok(order);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpDelete("order/delete/{orderId}")]
        [Authorize(Roles = $"{SD.ADMIN}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeleteOrder(int? orderId) 
        {
            try 
            {
                if (orderId == 0 || orderId == null)
                {
                    return BadRequest();
                }
                Order order = await _orderRepository.GetById(tracked: false, filter: x => x.Id == orderId);
                if (order == null)
                {
                    return NotFound();
                }
                _orderRepository.Delete(order);
                await _orderRepository.Save();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
    }
}
