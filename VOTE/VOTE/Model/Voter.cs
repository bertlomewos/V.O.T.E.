using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VOTE.Model
{
    internal class Voter : User
    {
        public string ID { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string Location { get; set; }

        Dbconn dbconn = new Dbconn();
        public Voter(string email, string password, string role, string NID, string fName, string lName, string location) : base(email, password, role)
        {
            ID = NID;
            FName = fName;
            LName = lName;
            Location = location;
            assignVoter();
        }

        public void assignVoter()
        {
            dbconn.InsertINtoVoters(FName, LName, Location, ID);
            MessageBox.Show("Voter Registered Successfully");
        }
    }
}
