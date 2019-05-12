using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace OAuth.Tests
{
    public class OAuthRequestTests 
    {
        [TestCase(false, null, OAuthRequestType.ProtectedResource)]
        [TestCase(true, "", OAuthRequestType.ProtectedResource)]
        [TestCase(true, "token", OAuthRequestType.ProtectedResource)]
        [TestCase(false, null, OAuthRequestType.RequestToken)]
        [TestCase(false, "", OAuthRequestType.RequestToken)]
        [TestCase(true, "token", OAuthRequestType.RequestToken)]
        public void GetAuthorizationHeaderContainsOAuthToken(bool expectedResult, string tokenValue, OAuthRequestType oAuthRequestType)
        {
            var oAuthRequest = new OAuthRequest
            {
                Method = "GET",
                Token = tokenValue,
                ConsumerSecret = "secret",
                ConsumerKey = "key",
                ClientUsername = "username",
                ClientPassword = "password",
                RequestUrl = "http://url",
                Type = oAuthRequestType
            };
            Assert.AreEqual(expectedResult,oAuthRequest.GetAuthorizationHeader().Contains("oauth_token"));
        }

        [TestCase("")]
        [TestCase(null)]
        public void GetAuthorizationHeaderThrowsExceptionWhenOAuthTokenIsNullOrEmptyString(string tokenValue)
        {
            var oAuthRequest = new OAuthRequest
            {
                Method = "GET",
                Token = tokenValue,
                ConsumerSecret = "secret",
                ConsumerKey = "key",
                ClientUsername = "username",
                ClientPassword = "password",
                RequestUrl = "http://url",
                Type = OAuthRequestType.AccessToken
            };
            Assert.Throws<ArgumentException>(() => oAuthRequest.GetAuthorizationHeader());
        }

    }
}
