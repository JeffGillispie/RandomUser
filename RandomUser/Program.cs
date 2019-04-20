using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RandomUser
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args == null || args.Length < 3)
            {
                Console.WriteLine("Expected Input Format: gender=male count=25 \"file=x:\\some folder\\some file.csv\"");
            }
            else
            {
                // parse params
                var paramMap = args.ToDictionary(
                    arg => arg.Split('=').First().ToLower(),
                    arg => arg.Split('=').Last().ToLower());

                var gender = GetGender(paramMap);
                int count = GetUserCount(paramMap);
                string file = GetFilePath(paramMap);

                // get users
                var manager = new UserManager();
                var users = manager.GetRandomUsers(count, gender);

                // write results
                WriteResults(users, file);
            }
        }

        static UserManager.Gender GetGender(Dictionary<string, string> map)
        {
            string value = map["gender"];

            if (value.Equals("male"))
                return UserManager.Gender.Male;
            else if (value.Equals("female"))
                return UserManager.Gender.Female;
            else
                throw new Exception($"Invalid gender selection: {value}");
        }

        static int GetUserCount(Dictionary<string, string> map)
        {
            string value = map["count"];
            return int.Parse(value);
        }

        static string GetFilePath(Dictionary<string, string> map)
        {
            string value = map["file"];
            return value;
        }

        static void WriteResults(IEnumerable<User> users, string filePath)
        {
            var lines = new List<string>();
            lines.Add(User.GetHeader());
            lines.AddRange(users.Select(user => user.ToString()));
            File.WriteAllLines(filePath, lines);
        }
    }
}
