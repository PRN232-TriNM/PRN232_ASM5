using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EVCS.Common.Shared.trinm
{
    public static class Utilities
    {
        private static string LoggerFilePath = Directory.GetCurrentDirectory() + @"\DataLog.txt";

        public static string ConvertObjectToJSONString<T>(T entity)
        {
            string jsonString = JsonSerializer.Serialize(entity, new JsonSerializerOptions { WriteIndented = false });

            return jsonString;
        }

        public static void WriteLoggerFile(string content)
        {
            try
            {
                var path = Directory.GetCurrentDirectory();

                using (var file = File.Open(LoggerFilePath, FileMode.Append, FileAccess.Write))
                using (var writer = new StreamWriter(file))
                {
                    writer.WriteLineAsync(content);
                    writer.FlushAsync();
                }
            }
            catch (Exception ex)
            {
                // handle exception here
            }
        }
    }
}
