using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyStoreApi.Contracts.OrderDto;
using MyStoreApi.Contracts.OrderProductDto;
using MyStoreApi.Domain.Models;
using MyStoreApi.Application.Interfaces;
using MyStoreApi.UseCases;

namespace MyStoreApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly ServiceResponse<OrderResponse> _serviceResponse;

        public OrderController(IOrderService orderService,
            ServiceResponse<OrderResponse> serviceResponse)
        {
            _orderService = orderService;
            _serviceResponse = serviceResponse;
        }

        [HttpPost("order")]
        public ActionResult<OrderResponse> GetOrderById([FromBody] OrderByIdRequest request)
        {
            var response = _orderService.GetOrderById(request);

            if (!response.Success) return NoContent();

            return Ok(response.Data);
        }

        [HttpPost("new")]
        public ActionResult<OrderResponse> CreateOrder(OrderRequest request)
        {
            return Ok(_orderService.CreateOrder(request));
        }

        [HttpPost("products")]
        public ActionResult<ServiceResponse<OrderResponse>> UpSertProduct(OrderProductRequest request)
        {
            var response = _orderService.UpSertProduct(request);

            if (!response.Success) return NotFound();

            return Ok(response.Data);
        }

        [HttpDelete("products/remove")]
        public ActionResult<ServiceResponse<OrderResponse>> RemoveProduct(OrderProductRequest request)
        {
            return Ok(_orderService.RemoveProduct(request));
        }

        // private CreatedAtActionResult CreatedAsGetOrder(Order order)
        // {
        //     return CreatedAtAction(
        //         actionName: nameof(GetOrderById),
        //         routeValues: new {id = order.Id},
        //         value: Order
        //     );
        // }
    }
}