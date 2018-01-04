using System;
using System.Security.Cryptography;
using System.Text;

namespace OAuth
{

    public static class TextSigner
    {
        public static string SignWithRsaSha1(string text, string pemRsaPrivateKey)
        {
#if NETSTANDARD2_0
            using (var rsa = RSA.Create())
            using (var sha1 = SHA1.Create())
            {
                var rsaParameters = RsaPrivateKeyParser.ParsePem(pemRsaPrivateKey);
                rsa.ImportParameters(rsaParameters);
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(text));
                var signedHash = rsa.SignHash(hash, HashAlgorithmName.SHA1, RSASignaturePadding.Pkcs1);
                return Convert.ToBase64String(signedHash);
            }
#else
            throw new NotImplementedException("RSA-SHA1 is implemented only for dot net standard.");
#endif
        }
    }
}