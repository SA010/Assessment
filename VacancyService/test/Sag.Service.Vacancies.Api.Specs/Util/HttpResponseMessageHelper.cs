using FluentAssertions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
//TODO: using Sag.Service.Identity.Client;
using Sag.Framework.ExceptionHandlers;
using Sag.Framework.Web.Json;
using System.Net;

namespace Sag.Service.Vacancies.Api.Specs.Util
{
    public static class HttpResponseMessageHelper
    {
        private static readonly JsonSerializerSettings SerializerSettings = new JsonSerializerSettings
        {
            DateFormatHandling = DateFormatHandling.IsoDateFormat,
            DateTimeZoneHandling = DateTimeZoneHandling.Utc,
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            Converters = new List<JsonConverter>
            {
                new StringEnumConverter(new SagEnumNamingStrategy())
            }
        };

        public static async Task<T?> GetContentAsync<T>(this HttpResponseMessage response, bool throwIfNull = true) where T : class
        {
            var data = await response.Content.ReadAsStringAsync();
            var obj = JsonConvert.DeserializeObject<T>(data, SerializerSettings);

            if (obj == null && throwIfNull)
            {
                throw new InvalidOperationException($"Expected response to be of type {typeof(T).Name}, retrieved NULL instead (string data: {data})");
            }

            return obj;
        }

        public static void ValidateStatusCode(this HttpResponseMessage response, HttpStatusCode expectedStatusCode)
        {
            response.StatusCode.Should().Be(expectedStatusCode);
        }

        public static async Task<T?> ValidateAndGetContentAsync<T>(this HttpResponseMessage response, HttpStatusCode expectedStatusCode = HttpStatusCode.OK) where T : class
        {
            response.ValidateStatusCode(expectedStatusCode);

            return await response.GetContentAsync<T>();
        }

        public static async Task ValidateNoContentAsync(this HttpResponseMessage response, HttpStatusCode expectedStatusCode = HttpStatusCode.NoContent)
        {
            response.ValidateStatusCode(expectedStatusCode);

            var data = await response.Content.ReadAsStringAsync();
            data.Should().BeNullOrEmpty("No response body was expected");
        }

        public static async Task ValidateRedirect(this HttpResponseMessage response, Uri redirectedTo, HttpStatusCode expectedStatusCode = HttpStatusCode.Redirect)
        {
            await response.ValidateNoContentAsync(expectedStatusCode);

            response.Headers
                .Should().ContainKey("Location").WhoseValue
                .Should().ContainSingle().Which
                .Should().Be(redirectedTo.ToString());
        }

        public static async Task ValidateResponseErrorAsync(this HttpResponseMessage response, string errorCode, string errorMessage)
        {
            var expectedStatusCode = Enum.Parse<HttpStatusCode>(errorCode.Split(".").First());
            response.ValidateStatusCode(expectedStatusCode);

            var error = await response.GetContentAsync<ApiError>();
            error.Should().NotBeNull();
            error!.ErrorCode.Should().Be(errorCode);
            error.ErrorMessage.Should().Be(errorMessage);
        }

        public static async Task ValidateResponseErrorAsync(this HttpResponseMessage response, HttpStatusCode expectedStatusCode)
        {
            response.ValidateStatusCode(expectedStatusCode);
            var data = await response.Content.ReadAsStringAsync();
            data.Should().BeNullOrEmpty("No response body was expected");
        }
    }
}