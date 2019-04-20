using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using NLog;

namespace RandomUser
{
    public class User
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public User(JObject obj)
        {
            this.Gender = (string)obj["gender"];
            this.Name = new UserName((JObject)obj["name"]);
            this.Location = new UserLocation((JObject)obj["location"]);
            this.Email = (string)obj["email"];
            this.Login = new UserLogin((JObject)obj["login"]);
            this.DOB = new UserEvent((JObject)obj["dob"]);
            this.Registered = new UserEvent((JObject)obj["registered"]);
            this.Phone = (string)obj["phone"];
            this.Cell = (string)obj["cell"];
            this.ID = new UserID((JObject)obj["id"]);
            this.Picture = new UserPicture((JObject)obj["picture"]);
            this.Nat = (string)obj["nat"];
        }

        public string Gender { get; set; }
        public UserName Name { get; set; }
        public UserLocation Location { get; set; }
        public string Email { get; set; }
        public UserLogin Login { get; set; }
        public UserEvent DOB { get; set; }
        public UserEvent Registered { get; set; }
        public string Phone { get; set; }
        public string Cell { get; set; }
        public UserID ID { get; set; }
        public UserPicture Picture { get; set; }
        public string Nat { get; set; }

        public static IEnumerable<User> ParseUsers(JArray array)
        {
            List<User> users = null;

            if (array != null && array.Any())
            {
                users = new List<User>();
                
                foreach (JObject item in array)
                {
                    User user = new User(item);
                    users.Add(user);
                }
            }
            else
            {
                logger.Warn("The array of users is empty.");
                logger.Warn(array.ToString());
            }

            return users;
        }

        public static string GetHeader()
        {
            var fields = new string[] {
                "Gender", "Title", "First Name", "Last Name", "Street", "City", "State", "Post Code", "Latitude", "Longitude",
                "Time Zone Offset", "Time Zone Description", "Email", "UUID", "User name", "Password", "Salt", "MD5", "SHA1", "SHA256",
                "DOB Date", "DOB Age", "Registered Date", "Registered Age", "Phone", "Cell", "ID Name", "ID Value", "Large Picture",
                "Medium Picture", "Thumbnail", "NAT"
            };

            return string.Join(",", fields.Select(x => $"\"{x}\""));
        }

        public override string ToString()
        {
            var props = new List<string>();
            props.Add(this.Gender);
            props.Add(this.Name.Title);
            props.Add(this.Name.First);
            props.Add(this.Name.Last);
            props.Add(this.Location.Street);
            props.Add(this.Location.City);
            props.Add(this.Location.State);
            props.Add(this.Location.PostCode);
            props.Add(this.Location.Latitude);
            props.Add(this.Location.Longitude);
            props.Add(this.Location.TimeZoneOffset);
            props.Add(this.Location.TimeZoneDescription);
            props.Add(this.Email);
            props.Add(this.Login.UUID);
            props.Add(this.Login.UserName);
            props.Add(this.Login.Password);
            props.Add(this.Login.Salt);
            props.Add(this.Login.MD5);
            props.Add(this.Login.SHA1);
            props.Add(this.Login.SHA256);
            props.Add(this.DOB.Date);
            props.Add(this.DOB.Age.ToString());
            props.Add(this.Registered.Date);
            props.Add(this.Registered.Age.ToString());
            props.Add(this.Phone);
            props.Add(this.Cell);
            props.Add(this.ID.Name);
            props.Add(this.ID.Value);
            props.Add(this.Picture.Large);
            props.Add(this.Picture.Medium);
            props.Add(this.Picture.Thumbnail);
            props.Add(this.Nat);
            return string.Join(",", props.Select(x => $"\"{x}\""));
        }

        public class UserName
        {
            public UserName(JObject obj)
            {
                this.Title = (string)obj["title"];
                this.First = (string)obj["first"];
                this.Last = (string)obj["last"];
            }

            public string Title { get; set; }
            public string First { get; set; }
            public string Last { get; set; }
        }

        public class UserLocation
        {
            public UserLocation(JObject obj)
            {
                this.Street = (string)obj["street"];
                this.City = (string)obj["city"];
                this.State = (string)obj["state"];
                this.PostCode = (string)obj["postcode"];
                this.Latitude = (string)obj["coordinates"]["latitude"];
                this.Longitude = (string)obj["coordinates"]["longitude"];
                this.TimeZoneOffset = (string)obj["timezone"]["offset"];
                this.TimeZoneDescription = (string)obj["timezone"]["description"];
            }

            public string Street { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string PostCode { get; set; }
            public string Latitude { get; set; }
            public string Longitude { get; set; }
            public string TimeZoneOffset { get; set; }
            public string TimeZoneDescription { get; set; }
        }

        public class UserLogin
        {
            public UserLogin(JObject obj)
            {
                this.UUID = (string)obj["uuid"];
                this.UserName = (string)obj["username"];
                this.Password = (string)obj["password"];
                this.Salt = (string)obj["salt"];
                this.MD5 = (string)obj["md5"];
                this.SHA1 = (string)obj["sha1"];
                this.SHA256 = (string)obj["sha256"];
            }

            public string UUID { get; set; }
            public string UserName { get; set; }
            public string Password { get; set; }
            public string Salt { get; set; }
            public string MD5 { get; set; }
            public string SHA1 { get; set; }
            public string SHA256 { get; set; }
        }

        public class UserEvent
        {
            public UserEvent(JObject obj)
            {
                this.Date = (string)obj["date"];
                this.Age = (int)obj["age"];
            }

            public string Date { get; set; }
            public int Age { get; set; }
        }

        public class UserID
        {
            public UserID(JObject obj)
            {
                this.Name = (string)obj["name"];
                this.Value = (string)obj["value"];
            }

            public string Name { get; set; }
            public string Value { get; set; }
        }

        public class UserPicture
        {
            public UserPicture(JObject obj)
            {
                this.Large = (string)obj["large"];
                this.Medium = (string)obj["medium"];
                this.Thumbnail = (string)obj["thumbnail"];
            }

            public string Large { get; set; }
            public string Medium { get; set; }
            public string Thumbnail { get; set; }
        }
    }
}
