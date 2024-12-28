using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VOTE.Model
{
    internal class Party : User
    {
        public string partyName {  get; set; }
        public string PartyAcronym { get; set; }
        public string FoundedDate { get; set; }
        public string HeadquartersLocation { get; set; }
        public string PartyLeader { get; set; }
        public string MembershipCriteria { get; set; }
        public string PartyInfo { get; set; }
        public int MembershipSize { get; set; }
        public string ElectionParticipation { get; set; }
        public string FundingSources { get; set; }
        public byte[] LegalCertification { get; set; }

        SendToDb sd = new SendToDb();
        public Party(string email, string password, string role, string partyName, string partyAcronym, string foundedDate,
    string headquartersLocation, string partyLeader, string membershipCriteria, string partyInfo, int membershipSize,
    string electionParticipation, string fundingSources, byte[] legalCertification)
    : base(email, password, role)
        {
            this.partyName = partyName;
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


            assignParty();
        }

        public void assignParty()
        {
            sd.InsertIntoParty(partyName, PartyAcronym, FoundedDate, HeadquartersLocation, PartyLeader, MembershipCriteria, PartyInfo, MembershipSize, ElectionParticipation, FundingSources, LegalCertification);
        }

    }
}
