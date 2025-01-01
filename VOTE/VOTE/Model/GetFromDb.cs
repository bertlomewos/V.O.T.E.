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
        // FIX the way this is accessing stuff
        public (bool exists, string role) GetfromUsers(string userID, string password)
        {
            string query = "SELECT Password, Role FROM users WHERE UserID = @UserID";

            using (MySqlConnection connection = new MySqlConnection(Dbconn.connectionString))
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserID", userID);
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader(); //[Barte] data provids fast forward-only dara froma data source

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

       public List<string> partyList = new List<string>();

        public void GetParties(string  UID)
        {

            string query = "SELECT PartyName, PartyAcronym, FoundedDate, HeadquartersLocation, PartyLeader, MembershipCriteria, PartyInfo, MembershipSize, ElectionParticipation, FundingSources, LegalCertification, UserID FROM parties WHERE UserID = @UserID";

            using (MySqlConnection connection = new MySqlConnection(Dbconn.connectionString))
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserID", UID);
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();

                // Read the data
                while (reader.Read())
                {
                    string partyName = reader["PartyName"].ToString();
                    string partyAcronym = reader["PartyAcronym"].ToString();
                    string headquartersLocation = reader["HeadquartersLocation"].ToString();
                    string partyLeader = reader["PartyLeader"].ToString();
                    string membershipCriteria = reader["MembershipCriteria"].ToString();
                    string partyInfo = reader["PartyInfo"].ToString();
                    int membershipSize = Convert.ToInt32(reader["MembershipSize"]);
                    string electionParticipation = reader["ElectionParticipation"].ToString();
                    string fundingSources = reader["FundingSources"].ToString();

                    partyList.AddRange(new List<string>
                                        {
                                            partyName,
                                            partyAcronym,
                                            headquartersLocation,
                                            partyLeader,
                                            membershipCriteria,
                                            partyInfo,
                                            electionParticipation,
                                            fundingSources
                                        });
                }
            }
        }

        public List<Party> partyListForMainPage = new List<Party>();
        public void GetPartiesForMainPage()
        {
            string query = "SELECT * FROM parties";

            using (MySqlConnection connection = new MySqlConnection(Dbconn.connectionString))
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();

                // Read the data
                while (reader.Read())
                {
                    Party party = new Party(
                        email: "", // Adjust as necessary
                        password: "", // Adjust as necessary
                        role: "", // Adjust as necessary
                        PartyName: reader["PartyName"].ToString(),
                        partyAcronym: reader["PartyAcronym"].ToString(),
                        foundedDate: "",
                        headquartersLocation: reader["HeadquartersLocation"].ToString(),
                        partyLeader: reader["PartyLeader"].ToString(),
                        membershipCriteria: reader["MembershipCriteria"].ToString(),
                        partyInfo: reader["PartyInfo"].ToString(),
                        membershipSize: Convert.ToInt32(reader["MembershipSize"]),
                        electionParticipation: reader["ElectionParticipation"].ToString(),
                        fundingSources: reader["FundingSources"].ToString(),
                        legalCertification: null // Handle legal certification as needed
                    );

                    partyListForMainPage.Add(party);
                }
            }
        }
    }
}
