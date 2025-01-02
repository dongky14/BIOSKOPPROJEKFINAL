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

            try
            {
                using (StreamReader reader = new StreamReader("login_history.txt"))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        history.Add(line.Split(','));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load history: {ex.Message}");
            }

            return history;
        }

        public static void SaveHistory(List<string[]> history)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter("login_history.txt"))
                {
                    foreach (var entry in history)
                    {
                        writer.WriteLine(string.Join(",", entry));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to save history: {ex.Message}");
            }
        }

    }



}
