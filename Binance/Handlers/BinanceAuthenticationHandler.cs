using Binance.Extensions;
using System;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Binance.Handlers {
    internal class BinanceAuthenticationHandler : DelegatingHandler {
        private readonly string _apiKey;
        private readonly string _secretKey;
        private readonly int _receiveWindow;

        public BinanceAuthenticationHandler(string apiKey, string secretKey, int receiveWindow) {
            _apiKey = apiKey;
            _secretKey = secretKey;
            _receiveWindow = receiveWindow;

            InnerHandler = new HttpClientHandler();
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) {
            var timestampedUri = request.RequestUri
                .AppendToQuery("recvWindow", _receiveWindow.ToString())
                .AppendToQuery("timestamp", DateTime.UtcNow.ToUnixTimestamp().ToString());

            var queryWithoutQuestion = timestampedUri.Query.TrimStart('?');
            var decodedQuery = WebUtility.UrlDecode(queryWithoutQuestion);
            var signature = CreateSignature(decodedQuery, _secretKey);

            var signedUri = timestampedUri.AppendToQuery("signature", signature);
            
            request.RequestUri = signedUri;
            request.Headers.Add("X-MBX-APIKEY", _apiKey);

            return base.SendAsync(request, cancellationToken);
        }
        
        private string CreateSignature(string input, string secret) {
            var secretBytes = Encoding.UTF8.GetBytes(secret);
            var inputBytes = Encoding.UTF8.GetBytes(input);

            var hmac = new HMACSHA256(secretBytes);
            var signatureBytes = hmac.ComputeHash(inputBytes);

            return BitConverter.ToString(signatureBytes).Replace("-", "");
        }
    }
}
