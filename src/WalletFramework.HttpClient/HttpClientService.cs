using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace WalletFramework.HttpClient
{
    public class HttpClientService
    {
        private readonly System.Net.Http.HttpClient? _httpClient;

        public HttpClientService(System.Net.Http.HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public HttpClientService()
        {
            _httpClient = new System.Net.Http.HttpClient();
        }

        public async Task<HttpResponseMessage> GetAsync(string requestUri)
        {
            if (_httpClient == null)
            {
                throw new InvalidOperationException("HttpClientService is not initialized with an HttpClient.");
            }

            HttpResponseMessage response = null;
            int retryCount = 0;
            const int maxRetries = 3;

            while (retryCount < maxRetries)
            {
                response = await _httpClient.GetAsync(requestUri);

                if (response.IsSuccessStatusCode)
                {
                    return response;
                }

                // Simple retry logic for transient errors (e.g., 5xx status codes)
                if ((int)response.StatusCode >= 500 && (int)response.StatusCode <= 599)
                {
                    retryCount++;
                    if (retryCount < maxRetries)
                    {
                        // Optional: Add a delay before retrying
                        await Task.Delay(TimeSpan.FromSeconds(Math.Pow(2, retryCount))); // Exponential backoff
                    }
                }
                else
                {
                    // Not a transient error, don't retry
                    return response;
                }
            }

            // If all retries failed, return the last response
            return response;
        }

        public async Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent content)
        {
            if (_httpClient == null)
            {
                throw new InvalidOperationException("HttpClientService is not initialized with an HttpClient.");
            }
            return await _httpClient.PostAsync(requestUri, content);
        }
        public void SetDefaultRequestHeader(string name, string value)
        {
            if (_httpClient == null)
            {
                throw new InvalidOperationException("HttpClientService is not initialized with an HttpClient.");
            }
            _httpClient.DefaultRequestHeaders.Add(name, value);
        }

        // TODO: Implement other HTTP client functionalities (PUT, DELETE, etc.)
    }
}