using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VOTE.Model
{
    class Vote
    {
        public int VoteCount { get; set; }
        public int PartyID { get; set; }
    
    public Vote(int partyID, int voteCount)
        {
            PartyID = partyID;
            VoteCount = voteCount;
        }
    }
}
