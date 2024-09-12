using System.Text;

namespace GeekShopping.ProductAPI.Utils
{
    public class ImageFunctions
    {
        public static string Base64ToString(string base64EncodedData)
        {
            byte[] base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            string decodedString = Encoding.UTF8.GetString(base64EncodedBytes);

            return decodedString;
        }
    }
}
