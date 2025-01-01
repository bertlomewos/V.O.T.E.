using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using VOTE.Model;

namespace VOTE
{
    /// <summary>
    /// Interaction logic for PartyPage.xaml
    /// </summary>
    public partial class PartyPage : Window
    {
        public static string UID;
        public PartyPage()
        {
            InitializeComponent(); 
            GetFromDb gd = new GetFromDb();

            gd.GetParties(UID);
            PartyNameLabel.Content = gd.partyList[0];
            PartyAcronymLabel.Content = gd.partyList[1];
            HeadquartersLocationLabel.Content = gd.partyList[2];
            PartyLeaderLabel.Content = gd.partyList[3];
            MembershipCriteriaLabel.Content = gd.partyList[4];
            PartyInfoLabel.Content = gd.partyList[5];
            ElectionParticipationLabel.Content = gd.partyList[6];
            FundingSourcesLabel.Content = gd.partyList[7];

        }

        private void Profile(object sender, RoutedEventArgs e)
        {

        }
    }
}
