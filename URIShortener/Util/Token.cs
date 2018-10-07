using System.Security.Cryptography;
using System.Text;

namespace URIShortener.Util
{
    public class Token
    {
        public static string Generate(int length = 7)
        {
            var characterSet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var charecters = characterSet.ToCharArray();

            var data = new byte[1];
            data = new byte[length];

            var cryptoService = new RNGCryptoServiceProvider();
            cryptoService.GetNonZeroBytes(data);
            cryptoService.GetNonZeroBytes(data);

            var result = new StringBuilder(length);
            foreach (var byt in data)
            {
                result.Append(charecters[byt % (charecters.Length)]);
            }
            return result.ToString();
        }
    }
}
