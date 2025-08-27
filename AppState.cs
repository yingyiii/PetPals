using System;
using System.Collections.Generic;
using Microsoft.Maui.Storage;



namespace PetPals_Pet_CareService_App
{
    public static class AppState
    {
        public static List<string> ServiceSelected { get; set; } = new List<string>();
        public static string PetSelected { get; set; }
        public static string Price { get; set; }
        public static DateTime Date { get; set; }
        public static TimeSpan Time { get; set; }
        public static string Location { get; set; }

        // User Details
        public static string CreatedEmail { get; set; }
        public static string CreatedPassword { get; set; }

        public static string FirstName { get; set; }
        public static string LastName { get; set; }
        public static DateTime DateOfBirth { get; set; }
        public static string Gender { get; set; }

        public static string Phone { get; set; }


        public static List<Tuple<string, DateTime>> Messages { get; } = new List<Tuple<string, DateTime>>();

        private const string UsersKey = "Users";

        public static Dictionary<string, string> userDatabase = LoadUserDatabase();

        private static Dictionary<string, string> LoadUserDatabase()
        {
            var usersJson = Preferences.Get(UsersKey, "{}");
            return System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(usersJson);
        }

        public static void SaveUserDatabase()
        {
            var usersJson = System.Text.Json.JsonSerializer.Serialize(userDatabase);
            Preferences.Set(UsersKey, usersJson);
        }
    }
}
