using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VOTE.Model
{
    class GetFromDb
    {

        public (bool exists, string role) GetfromUsers(string userID, string password)
        {
            string query = "SELECT Password, Role FROM users WHERE UserID = @UserID";

            using (MySqlConnection connection = new MySqlConnection(Dbconn.connectionString))
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserID", userID);
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    string storedPassword = reader["Password"].ToString();
                    string role = reader["Role"].ToString();

                    if (password == storedPassword && role != null)
                    {
                        //MessageBox.Show("Login Successful");
                        return (true, role);
                        
                    }
                }
            }
            return (false, string.Empty);
        }



        public static string PName;
        public static string PAcronym;
        //public static string FoundedDate;
        public static string HeadquartersLocation;
        public static string PartyLeader;
        public static string MembershipCriteria;
        public static string PartyInfo;
        public static int MembershipSize;
        public static string ElectionParticipation;
        public static string FundingSources;
        //public static bool LegalCertification;

        public void GetParties(string UID)
        {
            string query = "SELECT PartyName, PartyAcronym, FoundedDate, HeadquartersLocation, PartyLeader, MembershipCriteria, PartyInfo, MembershipSize, ElectionParticipation, FundingSources, LegalCertification, UserID FROM parties WHERE UserID = @UserID";
            using (MySqlConnection connection = new MySqlConnection(Dbconn.connectionString))
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                MessageBox.Show(UID);
                command.Parameters.AddWithValue("@UserID", UID);
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();

                    // Read the data
                    while (reader.Read())
                    {
                        string partyName = reader["PartyName"].ToString();
                        string partyAcronym = reader["PartyAcronym"].ToString();
                        //DateTime foundedDate = Convert.ToDateTime(reader["FoundedDate"]);
                        string headquartersLocation = reader["HeadquartersLocation"].ToString();
                        string partyLeader = reader["PartyLeader"].ToString();
                        string membershipCriteria = reader["MembershipCriteria"].ToString();
                        string partyInfo = reader["PartyInfo"].ToString();
                        int membershipSize = Convert.ToInt32(reader["MembershipSize"]);
                        string electionParticipation = reader["ElectionParticipation"].ToString();
                        string fundingSources = reader["FundingSources"].ToString();
                        //bool legalCertification = Convert.ToBoolean(reader["LegalCertification"]);
                        string userID = reader["UserID"].ToString();

                        // Add the party data to the list
                        MessageBox.Show(partyName);
                        
                    PName = partyName;
                    PAcronym = partyAcronym;
                    //FoundedDate = foundedDate;
                    HeadquartersLocation = headquartersLocation;
                    PartyLeader = partyLeader;
                    MembershipCriteria = membershipCriteria;
                    PartyInfo = partyInfo;
                    MembershipSize = membershipSize;
                    ElectionParticipation = electionParticipation;
                    FundingSources = fundingSources;
                    //LegalCertification = legalCertification;


                }
            }
        }

    }
}
