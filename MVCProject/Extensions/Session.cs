using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace MVCProject.Extensions
{
    public static class Session
    {
        public static void SetObject<T>(this ISession session, string key, T value)
            where T : class
        {
            if (value == null)
                return;

            string jsonData = JsonSerializer.Serialize(value);
            session.SetString(key, jsonData);
        }

        public static T GetObject<T>(this ISession session, string key)
            where T : class
        {
            string jsonData = session.GetString(key);

            if (string.IsNullOrEmpty(jsonData))
                return null;

            return JsonSerializer.Deserialize<T>(jsonData);
        }
    }
}
