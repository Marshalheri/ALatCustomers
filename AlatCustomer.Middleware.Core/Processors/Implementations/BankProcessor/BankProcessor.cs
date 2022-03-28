using AlatCustomer.Middleware.Core.DTOs;
using AlatCustomer.Middleware.Core.DTOs.Resources;
using AlatCustomer.Middleware.Core.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AlatCustomer.Middleware.Core.Processors.Implementations.BankProcessor
{
    public class BankProcessor : IBankProcessor
    {
        private readonly HttpClient _client;
        private readonly BankProcessorSettings _bankSettings;
        readonly ILogger _logger;
        internal const string _prefix = "ATTA";
        public BankProcessor(IOptions<BankProcessorSettings> bankProcessorSettings, IHttpClientFactory factory, ILogger<BankProcessor> logger)
        {
            _logger = logger;
            _bankSettings = bankProcessorSettings.Value;
            _client = factory.CreateClient("HttpMessageHandler");
            BuildFiClient();
        }
        private void BuildFiClient()
        {
            _client.BaseAddress = new Uri(_bankSettings.BaseUrl);
            _client.DefaultRequestHeaders.Add("Subscriptionkey", _bankSettings.Subscriptionkey);
        }
        protected async Task<T> GetMessage<T>(string path)
        {
            var rawResponse = await _client.GetAsync(path);
            var body = await rawResponse.Content.ReadAsStringAsync();
            _logger.LogInformation($"{typeof(T).Name} ALAT Response : {body}");
            if (!rawResponse.IsSuccessStatusCode)
            {
                throw new Exception("Service invocation failure");
            }
            return Util.DeserializeFromJson<T>(body);
        }

        public async Task<PayloadResponse<IEnumerable<Bank>>> GetAllBanksAsync()
        {
            PayloadResponse<IEnumerable<Bank>> response = new(false);
            var serviceResponse = await GetMessage<GetBanksResponse>(_bankSettings.GetBanksPath);
            if (!serviceResponse.IsSuccessful())
            {
                response.Error = new ErrorResponse
                {
                    ErrorCode = $"{_prefix}{01}",
                    Description = serviceResponse.ErrorMessage,
                };
                return response;
            }
            response.SetPayload(serviceResponse.Result.Select(x => new Bank
                {
                    Code = x.Code,
                    Name = x.Name
                }));
            response.IsSuccessful = true;
            return response;
        }
    }
}
