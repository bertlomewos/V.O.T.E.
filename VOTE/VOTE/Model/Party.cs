﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VOTE.Model
{
    internal class Party : User
    {
        public int ID { get; set; }
        public string PartyName { get; set; }
        public string PartyAcronym { get; set; }
        public DateTime? FoundedDate { get; set; }
        public string HeadquartersLocation { get; set; }
        public string PartyLeader { get; set; }
        public string MembershipCriteria { get; set; }
        public string PartyInfo { get; set; }
        public int MembershipSize { get; set; }
        public int VoteCount { get; set; }
        public string ElectionParticipation { get; set; }
        public string FundingSources { get; set; }
        public byte[] LegalCertification { get; set; }

        SendToDb sd = new SendToDb();
       public Party(string email, string password, string role, string PartyName, string partyAcronym, DateTime? foundedDate,
       string headquartersLocation, string partyLeader, string membershipCriteria, string partyInfo, int membershipSize,
       string electionParticipation, string fundingSources, byte[] legalCertification)
       : base(email, password, role)
        {
            this.PartyName = PartyName;
            this.PartyAcronym = partyAcronym;
            this.FoundedDate = foundedDate;
            this.HeadquartersLocation = headquartersLocation;
            this.PartyLeader = partyLeader;
            this.MembershipCriteria = membershipCriteria;
            this.PartyInfo = partyInfo;
            this.MembershipSize = membershipSize;
            this.ElectionParticipation = electionParticipation;
            this.FundingSources = fundingSources;
            this.LegalCertification = legalCertification;
        }
        public Party(int id,string email, string password, string role, string PartyName, string partyAcronym, DateTime? foundedDate,
string headquartersLocation, string partyLeader, string membershipCriteria, string partyInfo, int membershipSize, int votecount,
string electionParticipation, string fundingSources)
: base(email, password, role)
        {
            this.ID = id;
            this.PartyName = PartyName;
            this.PartyAcronym = partyAcronym;
            this.FoundedDate = foundedDate;
            this.HeadquartersLocation = headquartersLocation;
            this.PartyLeader = partyLeader;
            this.MembershipCriteria = membershipCriteria;
            this.PartyInfo = partyInfo;
            this.MembershipSize = membershipSize;
            this.VoteCount = votecount;
            this.ElectionParticipation = electionParticipation;
            this.FundingSources = fundingSources;
        }


        public override void assign()
        {
            int userId = sd.InsertINtoUsers(Email, Password, Role);
            sd.InsertIntoParty(PartyName, PartyAcronym, FoundedDate, HeadquartersLocation, PartyLeader,
                    MembershipCriteria, PartyInfo, MembershipSize, ElectionParticipation, FundingSources, LegalCertification, userId);
        }
    }
}
