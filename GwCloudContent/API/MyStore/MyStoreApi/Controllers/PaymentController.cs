using Microsoft.AspNetCore.Mvc;
using MyStoreApi.Application.UseCases.PaymentUseCases.ProcessOrderPayment;
using MyStoreApi.Contracts.PaymentDto;
using MyStoreApi.UseCases;

namespace MyStoreApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPayOrderUseCase _payOrderUseCase;

        public PaymentController(IPayOrderUseCase payOrderUseCase)
        {
            _payOrderUseCase = payOrderUseCase;
        }

        [HttpPost]
        public ActionResult<ServiceResponse<PaymentResponse>> ProcessPayment([FromBody] PaymentRequest request)
        {
            var response = _payOrderUseCase.PayOrder(request);

            return Ok(response);
        }
    }
}