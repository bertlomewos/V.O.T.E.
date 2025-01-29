using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using VOTE.Model;


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
                        return (true, role);
                    }
                }
            }
            return (false, string.Empty);
        }

        public List<string> partyList = new List<string>();

        public void GetParties(string UID)
        {
            string query = "SELECT PartyName, PartyAcronym, FoundedDate, " +
                           "HeadquartersLocation, PartyLeader, MembershipCriteria, PartyInfo, " +
                           "MembershipSize, ElectionParticipation, FundingSources, LegalCertification, " +
                           "UserID FROM parties WHERE UserID = @UserID";

            using (MySqlConnection connection = new MySqlConnection(Dbconn.connectionString))
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserID", UID);
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();

                // Initialize partyList if not done yet
                if (partyList == null)
                    partyList = new List<string>();

                while (reader.Read())
                {
                    string partyName = reader["PartyName"].ToString();
                    string partyAcronym = reader["PartyAcronym"].ToString();
                    DateTime? foundedDate = reader["FoundedDate"] == DBNull.Value
                        ? (DateTime?)null
                        : Convert.ToDateTime(reader["FoundedDate"]);
                    string headquartersLocation = reader["HeadquartersLocation"].ToString();
                    string partyLeader = reader["PartyLeader"].ToString();
                    string membershipCriteria = reader["MembershipCriteria"].ToString();
                    string partyInfo = reader["PartyInfo"].ToString();
                    int membershipSize = Convert.ToInt32(reader["MembershipSize"]);
                    string electionParticipation = reader["ElectionParticipation"].ToString();
                    string fundingSources = reader["FundingSources"].ToString();
                    byte[] legalCertification = reader["LegalCertification"] as byte[];

                    // Add the strings to partyList
                    partyList.AddRange(new List<string>
                    {
                        partyName,
                        partyAcronym,
                        foundedDate.ToString(),
                        headquartersLocation,
                        partyLeader,
                        membershipCriteria,
                        partyInfo,
                        electionParticipation,
                        fundingSources,
                        membershipSize.ToString() 
                    });
                }
            }
        }


        public List<Party> parties = new List<Party>();
        public void GetPartiesForMainPage()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(Dbconn.connectionString))
                {
                    connection.Open();

                    string query = "SELECT * FROM parties";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            

                            while (reader.Read())
                            {
                                DateTime? foundedDate = reader["FoundedDate"] == DBNull.Value
                                ? (DateTime?)null
                                : Convert.ToDateTime(reader["FoundedDate"]);

                                var party = new Party(
                                email: "", 
                                password: "",      
                                role: "",                 
                                PartyName: reader["PartyName"].ToString(),
                                partyAcronym: reader["PartyAcronym"].ToString(),
                                foundedDate: foundedDate,
                                headquartersLocation: reader["HeadquartersLocation"].ToString(),
                                partyLeader: reader["PartyLeader"].ToString(),
                                membershipCriteria: reader["MembershipCriteria"].ToString(),
                                partyInfo: reader["PartyInfo"].ToString(),
                                membershipSize: int.Parse(reader["MembershipSize"].ToString()),
                                electionParticipation: reader["ElectionParticipation"].ToString(),
                                fundingSources: reader["FundingSources"].ToString(),
                                legalCertification: null // Replace with actual data if available
                            );
                                parties.Add(party);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data from the database: {ex.Message}", "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
