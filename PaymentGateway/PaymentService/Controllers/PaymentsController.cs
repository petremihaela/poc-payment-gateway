using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PaymentService.Core.Constants;
using PaymentService.Core.Helpers;
using PaymentService.Core.ReponseModels;
using PaymentService.Core.RequestModels;
using PaymentService.Core.Services;
using System;
using System.Threading.Tasks;

namespace PaymentService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentManager _paymentManager;

        private readonly ILogger<PaymentsController> _logger;

        public PaymentsController(IPaymentManager paymentManager, ILogger<PaymentsController> logger)
        {
            _paymentManager = paymentManager;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Create([FromBody]PaymentProcessRequest payment)
        {
            //TODO validate request

            //TODO send request to bank

            //Mock back
            var paymentId = Guid.NewGuid();
            var status = PaymentStatus.Authorized;
            if (payment.Amount < 10)
            {
                status = PaymentStatus.Declined;
            }

            try
            {
                var paymentEntity = PaymentConverter.ConvertRequestToEntity(payment);

                paymentEntity.PaymentId = paymentId;
                paymentEntity.PaymentStatus = status;
                paymentEntity.CreatedDate = DateTime.UtcNow;

                _paymentManager.StorePaymentAsync(paymentEntity);
            }
            catch (Exception ex)
            {
                _logger.LogError($"BankResponse-PaymentId: {paymentId}, BankResponse-Status:{status}, PaymentRequest: [{payment.ToString()}], Error: {ex.ToString()}");
            }

            return Ok(new { paymentId, status });
        }

        [HttpGet("{paymentId}", Name = "RetrievePayment")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<PaymentDetailsResponse>> RetrievePaymentAsync(Guid paymentId)
        {
            var entity = await _paymentManager.GetPaymentAsync(paymentId);

            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity);
        }
    }
}