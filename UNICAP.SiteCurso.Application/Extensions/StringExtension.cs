using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;

namespace UNICAP.SiteCurso.Application.Extensions
{
    public static class StringExtension
    {
        public static string GetSimpleHash(this string value)
        {
            if (String.IsNullOrEmpty(value))
                return String.Empty;

            using (var sha = SHA256.Create())
            {
                byte[] textData = Encoding.UTF8.GetBytes(value);
                byte[] hash = sha.ComputeHash(textData);
                return BitConverter.ToString(hash).Replace("-", String.Empty);
            }
        }

        public static object DeserializeObject(this string value, Type type)
        {
            try
            {
                JsonSerializerSettings settings = new JsonSerializerSettings()
                {
                    TypeNameHandling = TypeNameHandling.Auto,
                };

                var obj = JsonConvert.DeserializeObject(value, type, settings);

                return obj;

            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
