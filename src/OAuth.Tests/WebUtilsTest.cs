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
    }
}