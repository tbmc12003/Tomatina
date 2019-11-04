using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;

namespace Core.Core
{
    public static class CoreExtensions
    {
        private static Random rng = new Random();

        public static void Shuffle<T>(this List<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
        public static string GetAssemblyLocation(this Assembly assembly)
        {
            return Directory.GetParent(assembly.Location).FullName;
        }

        
        public static T Deserialize<T>(this string json) where T : class
        {
            try
            {
               return JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static string Serialize<T>(this T tObject) where T : class
        {
            try
            {
                return JsonConvert.SerializeObject(tObject);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static string ReadFromFile(this string filePath)
        {
            return File.ReadAllText(filePath);
        }
    }
}
