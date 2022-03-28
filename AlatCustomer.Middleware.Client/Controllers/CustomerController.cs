using AlatCustomer.Middleware.Client.Filters;
using AlatCustomer.Middleware.Core;
using AlatCustomer.Middleware.Core.DTOs.Customer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlatCustomer.Middleware.Client.Controllers
{
    [Route("api/v1/customer")]
    [ApiController]
    public class CustomerController : RootController
    {
        private readonly ICustomerService _service;
        public CustomerController(ICustomerService service)
        {
            _service = service;
        }
        /// <summary>
        /// onboard customer
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("onboard")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [TypeFilter(typeof(ValidateRequestBodyFilter<OnboardCustomerRequestDTO>))]
        public async Task<IActionResult> OnboardCustomer([FromBody] OnboardCustomerRequestDTO request)
        {
            var result = await _service.OnboardCustomerAsync(request);
            if (!result.IsSuccessful)
            {
                return CreateResponse(result.Error, result.FaultType);
            }
            return Ok();
        }
    }
}
