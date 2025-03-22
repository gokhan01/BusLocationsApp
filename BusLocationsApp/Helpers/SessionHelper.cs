using System.Text.Json;

namespace BusLocationsApp.Helpers
{
    public static class SessionHelper
    {
        public static void Set<T>(ISession session, string key, T value)
        {
            var jsonData = JsonSerializer.Serialize(value);
            session.SetString(key, jsonData);
        }

        public static T? Get<T>(ISession session, string key)
        {
            var jsonData = session.GetString(key);
            return jsonData == null ? default : JsonSerializer.Deserialize<T>(jsonData);
        }

        public static void Remove(ISession session, string key)
        {
            session.Remove(key);
        }
    }
}
