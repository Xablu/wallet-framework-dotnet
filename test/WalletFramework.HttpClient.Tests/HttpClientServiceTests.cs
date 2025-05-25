using Xunit;
using Moq;
using Moq.Protected;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;

namespace WalletFramework.HttpClient.Tests
{
    public class HttpClientServiceTests
    {
        [Fact]
        public async Task GetAsync_ValidUri_CallsHttpClientSendAsyncWithGetMethod()
        {
            // Arrange
            var testUri = "http://example.com/api/resource";
            var expectedContent = "{\"data\": \"expected_data\"}";
            var expectedResponse = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
            {
                Content = new StringContent(expectedContent)
            };

            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req =>
                        req.Method == HttpMethod.Get && req.RequestUri == new Uri(testUri)),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(expectedResponse);

            var httpClient = new System.Net.Http.HttpClient(mockHttpMessageHandler.Object);
            var httpClientService = new HttpClientService(httpClient);

            // Act
            var response = await httpClientService.GetAsync(testUri);

            // Assert
            mockHttpMessageHandler.Protected().Verify(
                "SendAsync",
                Times.Once(),
                ItExpr.Is<HttpRequestMessage>(req =>
                    req.Method == HttpMethod.Get && req.RequestUri == new Uri(testUri)),
                ItExpr.IsAny<CancellationToken>()
            );
            Assert.Equal(expectedResponse, response);
            var actualContent = await response.Content.ReadAsStringAsync();
            Assert.Equal(expectedContent, actualContent);
        }

        [Fact]
        public async Task PostAsync_ValidUriAndContent_CallsHttpClientSendAsyncWithPostMethodAndContent()
        {
            // Arrange
            var testUri = "http://example.com/api/resource";
            var testContent = "test content";
            var expectedResponse = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            HttpRequestMessage capturedRequest = null;

            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(expectedResponse)
                .Callback<HttpRequestMessage, CancellationToken>((req, ct) => capturedRequest = req);

            var httpClient = new System.Net.Http.HttpClient(mockHttpMessageHandler.Object);
            var httpClientService = new HttpClientService(httpClient);

            // Act
            var response = await httpClientService.PostAsync(testUri, new StringContent(testContent));

            // Assert
            mockHttpMessageHandler.Protected().Verify(
                "SendAsync",
                Times.Once(),
                ItExpr.Is<HttpRequestMessage>(req =>
                    req.Method == HttpMethod.Post &&
                    req.RequestUri == new Uri(testUri)),
                ItExpr.IsAny<CancellationToken>()
            );
            Assert.NotNull(capturedRequest.Content);
            var actualContent = await capturedRequest.Content.ReadAsStringAsync();
            Assert.Equal(testContent, actualContent);
        }

        [Fact]
        public async Task SetDefaultRequestHeader_AddsHeaderToRequests()
        {
            // Arrange
            var testUri = "http://example.com/api/resource";
            var headerName = "X-Test-Header";
            var headerValue = "test-value";
            var expectedResponse = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            HttpRequestMessage capturedRequest = null;

            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(expectedResponse)
                .Callback<HttpRequestMessage, CancellationToken>((req, ct) => capturedRequest = req);

            var httpClient = new System.Net.Http.HttpClient(mockHttpMessageHandler.Object);
            var httpClientService = new HttpClientService(httpClient);

            // Act
            httpClientService.SetDefaultRequestHeader(headerName, headerValue);
            await httpClientService.GetAsync(testUri);

            // Assert
            Assert.NotNull(capturedRequest);
            Assert.True(capturedRequest.Headers.Contains(headerName));
            Assert.Equal(headerValue, capturedRequest.Headers.GetValues(headerName).Single());
        }

        [Fact]
        public async Task GetAsync_TransientError_RetriesRequest()
        {
            // Arrange
            var testUri = "http://example.com/api/resource";
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            var retryCount = 0;

            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req => req.RequestUri == new Uri(testUri)),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(() =>
                {
                    retryCount++;
                    if (retryCount <= 2) // Simulate transient failure for the first 2 attempts
                    {
                        return new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);
                    }
                    return new HttpResponseMessage(System.Net.HttpStatusCode.OK); // Success on the 3rd attempt
                });

            var httpClient = new System.Net.Http.HttpClient(mockHttpMessageHandler.Object);
            // Note: HttpClientService needs to be modified to include retry logic.
            // This test assumes retry logic is implemented within HttpClientService or via a policy.
            // For now, we'll test the interaction with the mock handler.
            var httpClientService = new HttpClientService(httpClient);

            // Act
            var response = await httpClientService.GetAsync(testUri);

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(3, retryCount); // Verify that 3 attempts were made
        }
        [Fact]
        public async Task GetAsync_InvalidUri_ThrowsHttpRequestException()
        {
            // Arrange
            var testUri = "http://invalid-url-that-does-not-exist.com"; // Use a URL that will definitely fail
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req => req.RequestUri == new Uri(testUri)),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ThrowsAsync(new HttpRequestException("Simulated network error or invalid URL")); // Simulate failure

            var httpClient = new System.Net.Http.HttpClient(mockHttpMessageHandler.Object);
            var httpClientService = new HttpClientService(httpClient);

            // Act & Assert
            await Assert.ThrowsAsync<HttpRequestException>(() => httpClientService.GetAsync(testUri));

            mockHttpMessageHandler.Protected().Verify(
                "SendAsync",
                Times.Once(),
                ItExpr.Is<HttpRequestMessage>(req => req.RequestUri == new Uri(testUri)),
                ItExpr.IsAny<CancellationToken>()
            );
        }
    }
}