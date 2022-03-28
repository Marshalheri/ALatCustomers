
using AlatCustomer.Middleware.Client.Filters;
using AlatCustomer.Middleware.Core;
using AlatCustomer.Middleware.Core.DTOs.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AlatCustomer.Middleware.Client.Controllers
{
    [ApiController]
    [Route("api/v1/resources")]
    public class ResourcesController : RootController
    {
        private readonly IResourcesService _service;
        public ResourcesController(IResourcesService service)
        {
            _service = service;
        }

        /// <summary>
        /// Request to get list of states
        /// </summary>
        /// <returns></returns>
        [HttpGet("state")]
        [ProducesResponseType(typeof(GetStatesResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetStates()
        {
            var result = await _service.GetStatesAsync();
            if (!result.IsSuccessful)
            {
                return CreateResponse(result.Error, result.FaultType);
            }
            return Ok(result.GetPayload());
        }

        /// <summary>
        /// Request to get list of local governments by statecode
        /// </summary>
        /// <returns></returns>
        [HttpGet("lga/{stateCode}")]
        [ProducesResponseType(typeof(GetLocalGovernmentsResponse), StatusCodes.Status200OK)]

        public async Task<IActionResult> GetLocalGovernments([FromRoute] string stateCode)
        {
            var result = await _service.GetLocalGovernmentsAsync(stateCode);
            if (!result.IsSuccessful)
            {
                return CreateResponse(result.Error, result.FaultType);
            }
            return Ok(result.GetPayload());
        }

        /// <summary>
        /// Send otp to customer
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("otp/send")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [TypeFilter(typeof(ValidateRequestBodyFilter<SendOtpRequestDTO>))]
        public async Task<IActionResult> SendOtp([FromBody] SendOtpRequestDTO request)
        {
            var result = await _service.SendOtpAsync(request);
            if (!result.IsSuccessful)
            {
                return CreateResponse(result.Error, result.FaultType);
            }
            return Ok();
        }
    }
}
