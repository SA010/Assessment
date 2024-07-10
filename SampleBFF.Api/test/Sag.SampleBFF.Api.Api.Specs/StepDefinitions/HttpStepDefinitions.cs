using FluentAssertions;
using Sag.Framework;
using Sag.Framework.ExceptionHandlers;
using Sag.SampleBFF.Api.Api.Specs.Util;
using System.Net;
using System.Text.RegularExpressions;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Sag.SampleBFF.Api.Api.Specs.StepDefinitions
{
    public partial class BaseStepDefinitions
    {
        protected static HttpClient Client => GlobalBindings.Client;
        protected HttpResponseMessage? _response;

        [GeneratedRegex(@"^\d{3}\.\d{1,4}(\.\d{1,4})?$")]
        private static partial Regex StatusCodeRegex();

        [Then(@"I expect the following validation errors")]
        public async Task VerifyValidationErrors(Table table)
        {
            _response.Should().NotBeNull();
            var error = await _response!.GetContentAsync<ApiError>();
            error.Should().NotBeNull();
            var errors = error!.ErrorDetails.SelectMany(e => e.Errors.Select(d => (e.PropertyName, d.ErrorCode, d.ErrorMessage)));
            var expectedErrors = table.CreateSet<(string PropertyName, string ErrorCode, string ErrorMessage)>();

            errors.Should().BeEquivalentTo(expectedErrors);
        }

        /// <summary>
        /// This method can be used to check both http status codes as specific api error codes
        /// </summary>
        /// <param name="expectedStatus">The expected status code</param>
        /// <exception cref="ArgumentNullException">If the <paramref name="expectedStatus"/> is null</exception>
        /// <exception cref="ArgumentException">If the <paramref name="expectedStatus"/> is invalid</exception>
        [Then(@"The response status should be '([^']*)'")]
        public async Task ThenResponseStatusCodeShouldBe(string expectedStatus)
        {
            if (expectedStatus == null)
            {
                throw new ArgumentNullException(nameof(expectedStatus));
            }

            string? expectedErrorCode;
            bool checkSubCode;

            //If we just enter a status code, we map it to the 'default' error code, and check if there is a message at all
            if (Enum.TryParse(expectedStatus, true, out HttpStatusCode expectedStatusCode))
            {
                expectedErrorCode = expectedStatusCode switch
                {
                    HttpStatusCode.BadRequest => SagErrorCode.HttpStatus400.Default,
                    HttpStatusCode.NotFound => SagErrorCode.HttpStatus404.Default,
                    HttpStatusCode.Conflict => SagErrorCode.HttpStatus409.Default,
                    _ => null
                };
                checkSubCode = false;
            }
            //If we give a specific status code it should match exactly
            else if (StatusCodeRegex().IsMatch(expectedStatus))
            {
                expectedStatusCode = Enum.Parse<HttpStatusCode>(expectedStatus.Split(".").First());
                expectedErrorCode = expectedStatus;
                checkSubCode = true;
            }
            else
            {
                throw new ArgumentException($"Invalid status {expectedStatus}");
            }

            await ValidateAsync(expectedStatusCode, expectedErrorCode, checkSubCode);
        }

        private async Task ValidateAsync(HttpStatusCode expectedStatusCode, string? expectedErrorCode, bool checkSubCode)
        {
            _response.Should().NotBeNull();
            _response!.StatusCode.Should().Be(expectedStatusCode);
            _response.ReasonPhrase.Should().Be(GetReasonPhrase(expectedStatusCode));

            if (expectedErrorCode != null)
            {
                if (_response.Content.Headers.ContentLength > 0)
                {
                    var error = await _response.GetContentAsync<ApiError>();
                    error.Should().NotBeNull();
                    error!.ErrorCode.Should().Be(expectedErrorCode);
                }
                else if (checkSubCode)
                {
                    throw new Exception($"Expected error code {expectedErrorCode}, but found NULL");
                }
            }
        }

        private static string GetReasonPhrase(HttpStatusCode statusCode)
        {
            return statusCode switch
            {
                HttpStatusCode.BadRequest => "Bad Request",
                HttpStatusCode.NoContent => "No Content",
                HttpStatusCode.NotFound => "Not Found",
                HttpStatusCode.Redirect => "Found",
                HttpStatusCode.InternalServerError => "Internal Server Error",
                _ => statusCode.ToString()
            };
        }
    }
}
