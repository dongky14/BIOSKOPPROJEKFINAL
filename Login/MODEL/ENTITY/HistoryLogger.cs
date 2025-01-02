using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Login.MODEL.ENTITY
{

    public static class HistoryLogger
    {
        private static string historyFilePath = "login_history.txt";

        public static void AddToHistory(string username, string password, string name, string phone, string dob)
        {
            using (StreamWriter writer = new StreamWriter(historyFilePath, true))
            {
                writer.WriteLine($"{username}:{password}:{name}:{phone}:{dob}:{DateTime.Now}");
            }
        }

        public static List<string[]> LoadHistory()
        {
            var history = new List<string[]>();
            if (File.Exists(historyFilePath))
            {
                foreach (var line in File.ReadAllLines(historyFilePath))
                {
                    history.Add(line.Split(':'));
                }
            }
            return history;
        }
        public static void SaveHistory(List<string[]> history)
        {
            using (StreamWriter writer = new StreamWriter(historyFilePath, false)) // Overwrite file
            {
                foreach (var entry in history)
                {
                    writer.WriteLine(string.Join(":", entry));
                }
            }
        }
      


    }



}
