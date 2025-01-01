using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VOTE.Model
{

    internal class User
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        SendToDb sd = new SendToDb();
        public User(string email, string password, string role)
        {
            Email = email;
            Password = password;
            Role = role;
            //assign();

        }

        public virtual void assign()
        {
            sd.InsertINtoUsers(Email, Password, Role);
            MessageBox.Show("User Registered Successfully");
        }


    }
}
