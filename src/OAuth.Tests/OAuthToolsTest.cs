using System;
using NUnit.Framework;

namespace OAuth.Tests
{
    [TestFixture]
    public class OAuthToolsTest
    {
        readonly string _aStringToSign = "AStringToSign";
        readonly string _ARsaPemPrivateKey = "ARsaPemPrivateKey";

        class GetSignatureTest : OAuthToolsTest
        {
            [Test]
            public void ThrowsForRsaSha1SignatureWithoutSupportOfDotNetStandard20()
            {
                void GetRsaSha1Signature() => OAuthTools.GetSignature(OAuthSignatureMethod.RsaSha1, OAuthSignatureTreatment.Escaped, _aStringToSign, _ARsaPemPrivateKey);

                Assert.Throws<NotImplementedException>(GetRsaSha1Signature);
            }

            [Test]
            public void DoesNotThrowForHmacSha1WithoutSupportOfDotNetStandard20()
            {
                void GetHmacSha1Signature() => OAuthTools.GetSignature(OAuthSignatureMethod.HmacSha1, OAuthSignatureTreatment.Escaped, _aStringToSign, _ARsaPemPrivateKey);

                Assert.DoesNotThrow(GetHmacSha1Signature);
            }
        }
    }
}