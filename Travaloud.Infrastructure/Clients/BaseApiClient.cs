using RestSharp;
using Travaloud.Core.Interfaces.Services;
using Microsoft.AspNetCore.Http;

namespace Travaloud.Infrastructure.Clients
{
    public class BaseApiClient : RestClient
    {
        private readonly IErrorLoggerService _errorLoggerService;
        private readonly Uri _baseUri;

        public BaseApiClient(IErrorLoggerService errorLoggerService, IHttpContextAccessor httpContextAccessor, string baseUrl = null)
        {
            var current = httpContextAccessor.HttpContext;
            var uri = new Uri(baseUrl ?? $"{current.Request.Scheme}://{current.Request.Host}{current.Request.PathBase}/api/");

            _baseUri = uri;
            _errorLoggerService = errorLoggerService;

            Options.BaseUrl = uri;
        }

        private void TimeoutCheck(RestRequest request, RestResponse response)
        {
            if (response.StatusCode == 0)
            {
                LogError(_baseUri, request, response);
            }
        }

        public async Task<RestResponse<T>> ExecuteAsync<T>(RestRequest request)
        {
            var response = await this.ExecuteAsync<T>(request);
            TimeoutCheck(request, response);
            return response;
        }

        public async Task<RestResponse<T>> PostAsync<T>(RestRequest request) where T : new()
        {
            var response = await this.ExecutePostAsync<T>(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                if (response.Content != null)
                    response.Data = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(response.Content);
            }
            else
            {
                LogError(_baseUri, request, response);
            }

            return response;
        }

        public async Task<RestResponse<T>> GetAsync<T>(RestRequest request) where T : new()
        {
            var response = await this.ExecuteGetAsync<T>(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                if (response.Content != null)
                    response.Data = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(response.Content);
            }
            else
            {
                LogError(_baseUri, request, response);
            }
            return response;
        }

        private void LogError(Uri baseUrl, RestRequest request, RestResponse response)
        {
            //Get the values of the parameters passed to the API
            var parameters = string.Join(", ", request.Parameters.Select(x => x.Name?.ToString() + "=" + (x.Value ?? "NULL")).ToArray());

            //Set up the information message with the URL, 
            //the status code, and the parameters.
            var info = "Request to " + baseUrl.AbsoluteUri
                                     + request.Resource + " failed with status code "
                                     + response.StatusCode + ", parameters: "
                                     + parameters + ", and content: " + response.Content;

            //Acquire the actual exception
            var ex = response.ErrorException ?? new Exception(info);

            //Log the exception and info message
            _errorLoggerService.LogError(ex);
        }
    }
}
