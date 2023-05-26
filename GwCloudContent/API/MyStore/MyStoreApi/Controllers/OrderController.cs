using Microsoft.AspNetCore.Mvc;
using MyStoreApi.Contracts.OrderDto;
using MyStoreApi.Contracts.OrderProductDto;
using MyStoreApi.UseCases;
using MyStoreApi.Application.UseCases.OrderUseCases.CreateOrderUseCase;
using MyStoreApi.Application.UseCases.OrderUseCases.GetOrderByIdUseCase;
using MyStoreApi.Application.UseCases.OrderUseCases.UpsertProductInOrderUseCase;
using MyStoreApi.Application.UseCases.OrderUseCases.RemoveProductFromOrderUseCase;

namespace MyStoreApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly ICreateOrderUseCase _createOrder;
        private readonly IGetOrderByIdUseCase _getOrderById;
        private readonly IUpsertProductInOrderUseCase _upSertProduct;
        private readonly IRemoveProductFromOrderUseCase _removeProductFromOrder;
        private readonly ServiceResponse<OrderResponse> _serviceResponse;

        public OrderController(ServiceResponse<OrderResponse> serviceResponse,
            ICreateOrderUseCase createOrder,
            IGetOrderByIdUseCase getOrderById,
            IUpsertProductInOrderUseCase upSertProduct,
            IRemoveProductFromOrderUseCase removeProductFromOrder)
        {
            _serviceResponse = serviceResponse;
            _createOrder = createOrder;
            _getOrderById = getOrderById;
            _upSertProduct = upSertProduct;
            _removeProductFromOrder = removeProductFromOrder;
        }

        [HttpPost("order")]
        public ActionResult<ServiceResponse<OrderResponse>> GetOrderById([FromBody] OrderByIdRequest request)
        {
            var response = _getOrderById.GetOrderById(request);

            if (!response.Success) return NoContent();

            return Ok(response.Data);
        }

        [HttpPost("new")]
        public ActionResult<ServiceResponse<OrderResponse>> CreateOrder(OrderRequest request)
        {
            return Ok(_createOrder.CreateOrder(request));
        }

        [HttpPost("products")]
        public ActionResult<ServiceResponse<OrderResponse>> UpSertProduct(OrderProductRequest request)
        {
            var response = _upSertProduct.UpSertProduct(request);

            if (!response.Success) return NotFound();

            return Ok(response.Data);
        }

        [HttpDelete("products/remove")]
        public ActionResult<ServiceResponse<OrderResponse>> RemoveProduct(OrderProductRequest request)
        {
            return Ok(_removeProductFromOrder.RemoveProduct(request));
        }
    }
}