﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                    string query = "UPDATE parties SET CountVote = CountVote + 1 WHERE PartyID = @PartyID";

                    using (MySqlCommand command = new MySqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@PartyID", partyID);
                        command.ExecuteNonQuery();
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
