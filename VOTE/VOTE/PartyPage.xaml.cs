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
        public PartyPage()
        {
            InitializeComponent(); 
            GetFromDb gd = new GetFromDb();

            PartyNameLabel.Content = GetFromDb.PName;
            PartyAcronymLabel.Content = GetFromDb.PAcronym;
            HeadquartersLocationLabel.Content = GetFromDb.HeadquartersLocation;
            PartyLeaderLabel.Content = GetFromDb.PartyLeader;
            MembershipCriteriaLabel.Content = GetFromDb.MembershipCriteria;
            PartyInfoLabel.Content = GetFromDb.PartyInfo;
            MembershipSizeLabel.Content = GetFromDb.MembershipSize;
            ElectionParticipationLabel.Content = GetFromDb.ElectionParticipation;
            FundingSourcesLabel.Content = GetFromDb.FundingSources;
        }

        private void Profile(object sender, RoutedEventArgs e)
        {

        }
    }
}
