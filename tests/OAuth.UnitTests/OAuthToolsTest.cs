using System;
using NUnit.Framework;

namespace OAuth.Tests
{
    [TestFixture]
    public class OAuthToolsTest
    {
        private readonly string _aStringToSign = "AStringToSign";
        private readonly string _ARsaPemPrivateKey = "ARsaPemPrivateKey";

#if NETFRAMEWORK
        [Test]
        public void ThrowsForRsaSha1SignatureWithoutSupportOfDotNetStandard20()
        {
            void GetRsaSha1Signature() => OAuthTools.GetSignature(OAuthSignatureMethod.RsaSha1, OAuthSignatureTreatment.Escaped, _aStringToSign, _ARsaPemPrivateKey);

            Assert.Throws<NotImplementedException>(GetRsaSha1Signature);
        }
#endif

        [Test]
        public void DoesNotThrowForHmacSha1WithoutSupportOfDotNetStandard20()
        {
            void GetHmacSha1Signature() => OAuthTools.GetSignature(OAuthSignatureMethod.HmacSha1,
                OAuthSignatureTreatment.Escaped, _aStringToSign, _ARsaPemPrivateKey);

            Assert.DoesNotThrow(GetHmacSha1Signature);
        }

#if NETCOREAPP
        [Test]
        public void SignsWithRsaSha1()
        {
            var aStringToSign = "AStringToSign";
            var rsaPemPrivateKey = @"-----BEGIN RSA PRIVATE KEY-----
MIICXQIBAAKBgQDAJjTl6pZ3/Z2uuJMHa4ZSUdNu20BrQBf0qSVaJBARdS1JAYk+
f2sLDABERsqJBvkkk3++28bW7RtzDuvB3MYIK5TZyKIvcE+Tgev3tjmD1b7XY1qo
8o0ledbDB/S3k2FfyA9oIfjpHYY1g/S+fD9abe69RTh1ds60XHOwxUgI4QIDAQAB
AoGAcZmlCJEqqIIeqV+iPW7KmPybfhzN9xqLjzA5TxOnFEssnM71rydxx7QurC8W
KvEedwtlKReSdRr1cY7Ov2yg/slUEq4fNhjSEkzmdxj9E+2dre235r45yyCU5cod
QkbluvDOJAgfLq+ZBPZLvwKhEZN/iVfi/3jfnbuzyyWPiukCQQDfFhoHCQciQZbl
kNpqs4GFhU1TvxkdKPKkyX0xeh+JPSWo+0cw5yxF2IlifIElxfWGGGupqZCn+nxm
MLUSLIZfAkEA3H+fAo9xicTP3F+9TAZCunT0IsheTj7b/E+vbSQam9eLs+ddzgsg
DNCe1Bhw1NC2cnXxCqX0+EZix9s0uTU4vwJAcZ1O+iBF6tNep2Hjaw4qu7aNEEa1
4pz1HqmjQeyBXSKwKGR4+FXzvUqvhWIFYBh2l6meQ1UhX/t5GY5a2XulnwJBAJ/E
/XO+endoG3FEEgbHNoyid8/IPcUWeRH+r0827OzlJv4pdGf62bNPaval6wPZY4nW
edzMWY+YeLT12eMldEMCQQDP7T/nbOomylhOHlCVgojO/BGkbwdhfwOj4EpE8XDU
D3yw8+j8kSgGSLuydIGPxHq0JYqTVdkIbA+agBZOiNRQ
-----END RSA PRIVATE KEY-----
";
            var rsaSha1Signature = OAuthTools.GetSignature(OAuthSignatureMethod.RsaSha1,
                OAuthSignatureTreatment.Escaped, aStringToSign, rsaPemPrivateKey);

            Assert.AreEqual(
                "bh2Ljy82v5FSD0PQaKDPDwTHolA6JrBfQPciDLTlR0nNodgFja%2Fw7UmLJuxuARNerX7gpKpFxboprGAOaCWZp0D5NiB4%2FrejvyM3u9iLkh9NPhtU0jihny0MYiWlxT6Tg4yiHr%2FQ5d6a1DEZvg8L6m9A6ckb0%2Bn69vkrnDd1zoE%3D",
                rsaSha1Signature);
        }
#endif
    }
}