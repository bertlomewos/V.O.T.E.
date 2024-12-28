using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static Google.Protobuf.Reflection.SourceCodeInfo.Types;

namespace VOTE.Model
{
    internal class Dbconn
    {
        public const string connectionString = "Server=localhost;Database=voting_app;Uid=root;Pwd=;";
        static int userId;

    }

}
