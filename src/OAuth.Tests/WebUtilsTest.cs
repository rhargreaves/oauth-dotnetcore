using System;
using System.Linq;
using NUnit.Framework;

namespace OAuth.Tests
{
    [TestFixture]
    public class WebUtilsTest
    {
        [TestCase("https://www.google.com", 0)]
        [TestCase("https://www.google.com/", 0)]
        [TestCase("https://www.google.com/?a=b", 1)]
        [TestCase("https://www.google.com/?a=b&c=d", 2)]
        [TestCase("https://www.google.com/?a=b&c=d&e=f", 3)]
        [TestCase("https://www.google.com/?a=b&c=d&e=f&g=", 4)]
        [TestCase("https://www.url.com/search?jql=assignee=User&user_id=UserId", 2)]
        public void ParsesQueryString(string url, int expectedQueryParametersCount)
        {
            Assert.AreEqual(expectedQueryParametersCount, WebUtils.ParseQueryString(new Uri(url)).Count());
        }

        [TestCase("https://www.google.com/?")]
        [TestCase("https://www.google.com/?a=b&c=d&e=f&")]
        public void ParseQueryStringThrowsForInvalidQueryString(string invalidUrl)
        {
            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            Assert.Throws<ArgumentException>(() => WebUtils.ParseQueryString(new Uri(invalidUrl)).ToList());
        }

        [TestCase(null)]
        public void ParseQueryStringThrowsArgumentNullException(string invalidUrl)
        {
            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            Assert.Throws<ArgumentNullException>(() => WebUtils.ParseQueryString(new Uri(invalidUrl)).ToList());
        }
    }
}