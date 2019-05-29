using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PaymentService.Core.Helpers;
using PaymentService.Core.Managers;
using PaymentService.Core.RequestModels;
using PaymentService.Core.ResponseModels;
using PaymentService.Managers.PaymentProcessor;
using System;
using System.Net;
using System.Threading.Tasks;

namespace PaymentService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentManager _paymentManager;

        private readonly IPaymentProcessor _paymentProcessor;

        private readonly ILogger<PaymentsController> _logger;

        public PaymentsController(IPaymentManager paymentManager, IPaymentProcessor paymentProcessor, ILogger<PaymentsController> logger)
        {
            _paymentManager = paymentManager;
            _paymentProcessor = paymentProcessor;
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(502)]
        public async Task<ActionResult<PaymentProcessResponse>> ProcessPaymentAsync([FromBody]PaymentProcessRequest payment)
        {
            try
            {
                _paymentManager.ValidatePaymentRequest(payment);
            }
            catch
            {
                return BadRequest();
            }

            var paymentResponse = await _paymentProcessor.ProcessPaymentAsync(payment);

            if (paymentResponse == null)
                return StatusCode((int)HttpStatusCode.BadGateway);

            var paymentId = paymentResponse.PaymentId;
            var status = paymentResponse.PaymentStatus;

            try
            {
                var paymentEntity = PaymentConverter.ConvertRequestToEntity(payment);

                paymentEntity.PaymentId = paymentId;
                paymentEntity.PaymentStatus = status;
                paymentEntity.CreatedDate = DateTime.UtcNow;

                await _paymentManager.StorePaymentAsync(paymentEntity);
            }
            catch (Exception ex)
            {
                _logger.LogError($"BankResponse-PaymentId: {paymentId}, BankResponse-Status:{status}, PaymentRequest: [{payment}], Error: {ex}");
            }

            return Ok(paymentResponse);
        }

        [HttpGet("{paymentId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<PaymentDetailsResponse>> RetrievePayment(Guid paymentId)
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