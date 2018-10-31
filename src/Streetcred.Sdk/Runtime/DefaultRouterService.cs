using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Hyperledger.Indy.WalletApi;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Streetcred.Sdk.Contracts;
using Streetcred.Sdk.Model;
using Streetcred.Sdk.Model.Records;
using Streetcred.Sdk.Model.Routes;
using Streetcred.Sdk.Utils;

namespace Streetcred.Sdk.Runtime
{
    /// <inheritdoc />
    public class RouterService : IRouterService
    {
        private readonly IWalletRecordService _walletRecordService;
        private readonly IMessageSerializer _messageSerializer;
        private readonly ILogger<RouterService> _logger;
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Streetcred.Sdk.Runtime.RouterService"/> class.
        /// </summary>
        public RouterService(IWalletRecordService walletRecordService, IMessageSerializer messageSerializer, ILogger<RouterService> logger)
        {
            _walletRecordService = walletRecordService;
            _messageSerializer = messageSerializer;
            _logger = logger;
            _httpClient = new HttpClient();
        }

        /// <inheritdoc />
        public async Task SendMessageAsync(IEnvelopeMessage envelope, AgentEndpoint endpoint)
        {
            _logger.LogInformation(LoggingEvents.SendMessage, "Envelope {0}, Endpoint {1}", envelope.Type, endpoint.Uri);

            var encrypted = await _messageSerializer.PackAsync(envelope, endpoint.Verkey);

            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(endpoint.Uri),
                Method = HttpMethod.Post,
                Content = new ByteArrayContent(encrypted)
            };
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }

        public async Task ProcessRoute(Wallet wallet, Route route)
        {
            var record = new RouteRecord
            {
                To = route.To,
                Endpoint = route.Endpoint
            };

            switch (route.Type)
            {
                case MessageTypes.AddRoute:
                    record.Tags.Add(TagConstants.Did,record.To);
                    await _walletRecordService.AddAsync(wallet, record);
                    break;
                case MessageTypes.DeleteRoute:
                    await _walletRecordService.DeleteAsync<RouteRecord>(wallet, record.To);
                    break;
                case MessageTypes.UpdateRoute:
                    record.Tags.Add(TagConstants.Did, record.To);
                    await _walletRecordService.UpdateAsync(wallet, record);
                    break;
            }
        }
    }
}
