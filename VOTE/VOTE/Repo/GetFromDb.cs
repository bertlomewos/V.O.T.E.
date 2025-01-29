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
       public static int theuserId;
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
                        theuserId = int.Parse(userID);
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
                                id: int.Parse(reader["PartyID"].ToString()),
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

        public void UpdateParty(string partyName, string partyAcronym, DateTime? foundedDate,
             string headquartersLocation, string partyLeader, string partyInfo,
             string membershipCriteria, int membershipSize, string electionParticipation,
             string fundingSources, byte[] legalCertification)
        {

         
            string query = @"UPDATE parties 
                     SET PartyName = @partyName, PartyAcronym = @partyAcronym, FoundedDate = @foundedDate, 
                         HeadquartersLocation = @headquartersLocation, PartyLeader = @partyLeader, 
                         PartyInfo = @partyInfo, MembershipCriteria = @membershipCriteria, 
                         MembershipSize = @membershipSize, ElectionParticipation = @electionParticipation, 
                         FundingSources = @fundingSources, LegalCertification = @legalCertification, 
                         UserId = @userId 
                     WHERE UserID = @UserID";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(Dbconn.connectionString))
                {
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        // Add parameters
                        command.Parameters.AddWithValue("@UserID", theuserId);
                        command.Parameters.AddWithValue("@partyName", partyName);
                        command.Parameters.AddWithValue("@partyAcronym", partyAcronym);
                        command.Parameters.AddWithValue("@foundedDate", foundedDate.HasValue ? foundedDate.Value : (object)DBNull.Value);
                        command.Parameters.AddWithValue("@headquartersLocation", headquartersLocation);
                        command.Parameters.AddWithValue("@partyLeader", partyLeader);
                        command.Parameters.AddWithValue("@partyInfo", partyInfo);
                        command.Parameters.AddWithValue("@membershipCriteria", membershipCriteria);
                        command.Parameters.AddWithValue("@membershipSize", membershipSize);
                        command.Parameters.AddWithValue("@electionParticipation", electionParticipation);
                        command.Parameters.AddWithValue("@fundingSources", fundingSources);
                        command.Parameters.AddWithValue("@legalCertification", legalCertification ?? (object)DBNull.Value);

                        // Open connection
                        connection.Open();

                        // Execute the query
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Party record updated successfully.");
                        }
                        else
                        {
                            Console.WriteLine("No rows were updated. Check PartyId and query conditions.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating party: " + ex.Message);
                throw;
            }
        }
    }
}
