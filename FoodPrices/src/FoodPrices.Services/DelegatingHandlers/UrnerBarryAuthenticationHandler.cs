using FoodPrices.Services.Options;
using Microsoft.Extensions.Options;

namespace FoodPrices.Services.DelegatingHandlers
{
    public class UrnerBarryAuthenticationHandler : DelegatingHandler
    {
        private UrnerBarryOptions options;

        public UrnerBarryAuthenticationHandler(IOptions<UrnerBarryOptions> options)
        {
            this.options = options.Value;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (!request.Headers.Contains("UBAPIKey"))
            {
                request.Headers.Add("UBAPIKey", this.options.ApiKey);
            }

            return base.SendAsync(request, cancellationToken);
        }
    }
}
