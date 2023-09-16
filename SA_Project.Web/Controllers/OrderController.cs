using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SA_Project.Web.Models;
using SA_Project.Web.Service;

namespace SA_Project.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrderController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            List<OrderDto> _list = new();

            var response = await _orderService.GetAll<APIResponse>();
            if(response != null && response.IsSuccess)
            {
                _list = JsonConvert.DeserializeObject<List<OrderDto>>(Convert.ToString(response.Result)!)!;
            }

            return View(_list);
        }
    }
}
