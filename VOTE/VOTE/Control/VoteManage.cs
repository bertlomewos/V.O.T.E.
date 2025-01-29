using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using VOTE.Model;

namespace VOTE.Model
{
    public class VoteManage
    {
        public void IncrementVoteCount(int partyID)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(Dbconn.connectionString))
                {
                    conn.Open();
                    string checkVoterQuery = "SELECT COUNT(*) FROM votes WHERE VoterID = @VoterID";
                    string query = "UPDATE parties SET CountVote = CountVote + 1 WHERE PartyID = @PartyID";
                    string queryVote = "INSERT INTO votes (VoterID ,PartyID) VALUES (@VoterID, @PartyID);";

                    // First, check if the email already exists
                    MySqlCommand checkoterCommand = new MySqlCommand(checkVoterQuery, conn);
                    checkoterCommand.Parameters.AddWithValue("@VoterID", GetFromDb.theuserId);

                    int VoterCount = Convert.ToInt32(checkoterCommand.ExecuteScalar());

                    // If email already exists, return a special value or throw an exception
                    if (VoterCount > 0)
                    {
                        MessageBox.Show("You have already voted");
                        return;
                    }

                    using (MySqlCommand command = new MySqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@PartyID", partyID);
                        command.ExecuteNonQuery();
                    }
                    using (MySqlCommand insertCommand = new MySqlCommand(queryVote, conn))
                    {
                        insertCommand.Parameters.AddWithValue("@PartyID", partyID);
                        insertCommand.Parameters.AddWithValue("@VoterID", GetFromDb.theuserId);
                        insertCommand.ExecuteNonQuery();
                    }
                }
                
            } 
            catch (Exception ex)
            {
                Console.WriteLine("Error incrementing vote count: " + ex.Message);

            }
        }
            

        public int GetVoteCount(int partyID)
        {
            int voteCount = 0;

            using (MySqlConnection conn = new MySqlConnection(Dbconn.connectionString))
            {
                conn.Open();
                string query = "SELECT CountVote FROM parties WHERE PartyID = @PartyID";

                using (MySqlCommand command = new MySqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@PartyID", partyID);
                    MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        voteCount = int.Parse(reader["CountVote"].ToString());

                    }


                }
            }

            return voteCount;
        }

    }
}
