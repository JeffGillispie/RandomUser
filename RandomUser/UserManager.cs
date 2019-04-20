using System.Collections.Generic;

namespace RandomUser
{
    public class UserManager : UserAgent
    {
        public enum Gender
        {
            Male,
            Female
        }

        public User GetRandomUser()
        {
            var obj = this.GetRandomUserObject();            
            User user = new User(obj);
            return user;
        }

        public IEnumerable<User> GetRandomUsers(int count, Gender gender)
        {
            var results = this.GetRandomUserObjects(count, gender.ToString().ToLower());
            var users = User.ParseUsers(results);
            return users;
        }
    }
}
