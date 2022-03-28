using Newtonsoft.Json;
namespace AlatCustomer.Middleware.Core.Services
{
    public static class Util
    {
        static JsonSerializerSettings settings = new JsonSerializerSettings { MissingMemberHandling = MissingMemberHandling.Ignore };
        public static string SerializeAsJson<T>(T item)
        {
            return JsonConvert.SerializeObject(item);
        }

        public static T DeserializeFromJson<T>(string input)
        {

            return JsonConvert.DeserializeObject<T>(input, settings);
        }
    }
}
