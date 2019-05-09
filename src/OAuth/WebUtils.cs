using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;

namespace OAuth
{
    public static class WebUtils
    {
        public static IEnumerable<WebParameter> ParseQueryString(Uri uri)
        {
            if (uri is null) { throw new ArgumentNullException(nameof(uri)); }

            var isUriValidHttpScheme = false;
            if (Uri.TryCreate(uri.ToString(), UriKind.Absolute, out Uri validatedUri))
            {
                isUriValidHttpScheme = (validatedUri.Scheme == Uri.UriSchemeHttp || validatedUri.Scheme == Uri.UriSchemeHttps);
            }

            if(!isUriValidHttpScheme) { throw new UriFormatException( "Uri is not valid Url");}

            NameValueCollection parsedQuery = HttpUtility.ParseQueryString(uri.Query);

            if (uri.Query.Any() && parsedQuery.Count == 0)
            {
                throw new UriFormatException("Uri does not have valid query string");
            }

            var queryStringParameters =
                parsedQuery.AllKeys.SelectMany(parsedQuery.GetValues, (key, value) => new {key, value});
            foreach (var param in queryStringParameters)
            {
                yield return new WebParameter(param.key, param.value);
            }
        }
    }
}