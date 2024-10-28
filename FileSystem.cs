using System.Text.Json;
using System.IO;

namespace Stopwatch
{
    internal static class FileSystem
    {
        public class UserData
        {
            // Timer data
            //public byte timerHours = 0;
            //public byte timerMinutes = 0;
            //public byte timerSeconds = 0;

            // Stopwatch data
            public int currentSec { get; set; } = 0;
            public int currentMin { get; set; } = 0;
            public int currentHour { get; set; } = 0;

            // Style data
            public byte opacity { get; set; } = 255;
            public float glowOpacity { get; set; } = 1;
            public System.Windows.Media.Color textColor { get; set; } = System.Windows.Media.Color.FromRgb(255, 255, 255);
            public System.Windows.Media.Color bgColor { get; set; } = System.Windows.Media.Color.FromRgb(111, 111, 111);
            public System.Windows.Media.Color glowColor { get; set; } = System.Windows.Media.Color.FromRgb(255, 255, 255);

            public int textSize = 56;
            public string textFamily = "Segoe UI";
            public int borderRadius = 0;
        }

        public static UserData styledata = new UserData();

        public static void WriteJSON()
        {
            string filename = "mystyle.json";
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(styledata, options);
            File.WriteAllText(filename, json);
        }

        public static void ReadJSON()
        {
            string filename = "mystyle.json";
            string json;
            try
            {
                json = File.ReadAllText(filename);
            }
            catch
            {
                json = null;
            }
      
            UserData newdata = new UserData();
            if (json != null)
            {
                try
                {
                    newdata = JsonSerializer.Deserialize<UserData>(json);
                }
                catch
                {
                    newdata = new UserData();
                }
            }
            styledata = newdata;
        }
    }
}
