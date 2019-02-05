using System;
using System.Collections.Generic;

namespace OAuth
{
    public static class WebUtils
    {
        public static IEnumerable<WebParameter> ParseQueryString(Uri uri)
        {
            if (string.IsNullOrEmpty(uri.Query) || uri.Query[0] != '?')
                yield break;
            var qsParams = uri.Query.Substring(1).Split('&');
            foreach (var param in qsParams)
            {
                var pair = param.Split('=');
                if (pair.Length != 2)
                    throw new ArgumentException("Uri does not have valid query string.");
                yield return new WebParameter(pair[0], pair[1]);
            }
        }
    }
}