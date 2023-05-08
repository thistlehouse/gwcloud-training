using Microsoft.AspNetCore.Mvc;
using MyStore.Contracts.OrderDto;
using MyStore.Contracts.OrderProductDto;
using MyStore.Models;
using MyStore.Services;

namespace MyStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("order")]
        public ActionResult<OrderResponse> GetOrderById([FromBody] OrderByIdRequest request)
        {
            Order order = _orderService.GetOrderById(request.Id);
            
            return MapOrderResponse(order);
        }

        [HttpPost("new")]
        public ActionResult<OrderResponse> CreateOrder(OrderRequest request)
        {
            Order order = MapOrderRequest(request);
            Order orderResponse = _orderService.CreateOrder(order);
            
            return CreatedAsGetOrder(orderResponse);
        }

        [HttpPost("products")]
        public OrderResponse UpSertProduct(OrderProductRequest request)
        {
            OrderProduct orderProduct = MapOrderProductRequest(request);
            Order order = _orderService.UpSertProduct(orderProduct);

            return MapOrderResponse(order);
        }

        [HttpDelete("products/remove")]
        public OrderResponse RemoveProduct(OrderProductRequest request)
        {
            OrderProduct orderProduct = MapOrderProductRequest(request);
            Order order = _orderService.RemoveProduct(orderProduct);

            return MapOrderResponse(order);
        }

        private CreatedAtActionResult CreatedAsGetOrder(Order order)
        {
            return CreatedAtAction(
                actionName: nameof(GetOrderById),
                routeValues: new {id = order.Id},
                value: MapOrderResponse(order)
            );
        }

        private static OrderResponse MapOrderResponse(Order order)
        {
            return new OrderResponse(
                order.CustomerId,                
                order.OrderProducts,
                order.TotalToPay
            );
        }

        private static Order MapOrderRequest(OrderRequest request)
        {
            return new Order(
                request.Id,
                request.CustomerId,
                request.OrderProducts,
                request.TotalToPay
            );
        }
        
        private static OrderProduct MapOrderProductRequest(OrderProductRequest request)
        {
            return new OrderProduct(
                request.OrderId,
                request.ProductId,
                request.Quantity,
                request.Total
            );
        }
    }
}