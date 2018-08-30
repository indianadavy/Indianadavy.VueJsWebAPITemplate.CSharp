using System;
namespace aspnetcore.Models
{
    public class UserItem
    {
        public UserItem()
        {  
            
        }
        public UserItem(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
