//TODO: using Sag.Framework.Authentication.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace Sag.Service.Vacancies.Api.Specs.Util
{
    /* TODO:
    public class MockValidationKeyProvider : Mock<IValidationKeyProvider>
    {
        public static MockValidationKeyProvider Instance { get; } = CreateInstance();

        private readonly X509Certificate2 _certificate;

        private MockValidationKeyProvider()
        {
            _certificate = CreateSelfSignedCertificate();

            Setup(x => x.GetAsync()).ReturnsAsync(GetKeys);
        }

        private IEnumerable<JsonWebKey> GetKeys()
        {
            var key = new X509SecurityKey(_certificate);
            yield return Convert(key);
        }

        private static MockValidationKeyProvider CreateInstance()
        {
            return new MockValidationKeyProvider();
        }

        private static X509Certificate2 CreateSelfSignedCertificate()
        {
            using var rsa = RSA.Create();

            var request = new CertificateRequest("CN=unittest.sag.nl", rsa, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
            return request.CreateSelfSigned(DateTimeOffset.Now, DateTimeOffset.Now.AddYears(5));
        }

        public static SigningCredentials GetSigningCredentials()
        {
            var key = new X509SecurityKey(Instance._certificate);

            return new SigningCredentials(key, SecurityAlgorithms.RsaSha256);
        }

        private static JsonWebKey Convert(X509SecurityKey key)
        {
            var rsaJsonWebKey = new JsonWebKey
            {
                Kid = key.KeyId,
                KeyId = key.KeyId,
                Kty = JsonWebAlgorithmsKeyTypes.RSA,
                Use = JsonWebKeyUseNames.Sig,
                Alg = SecurityAlgorithms.RsaSha256
            };

            var rsa = (RSA)key.PublicKey;

            var parameters = rsa.ExportParameters(false);

            rsaJsonWebKey.E = Base64UrlEncoder.Encode(parameters.Exponent);
            rsaJsonWebKey.N = Base64UrlEncoder.Encode(parameters.Modulus);

            var certificate = key.Certificate;
            if (certificate is not null)
            {
                rsaJsonWebKey.X5t = Base64UrlEncoder.Encode(certificate.GetCertHash());
                rsaJsonWebKey.X5c.Add(System.Convert.ToBase64String(certificate.RawData));
            }

            return rsaJsonWebKey;
        }
    }
    */
}