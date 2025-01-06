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
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Window
    {
        GetFromDb gb;

        public MainPage()
        {
            InitializeComponent();

            var getFromDb = new GetFromDb();
            gb = new GetFromDb();
            gb.GetPartiesForMainPage();
            AssigneUserControl();

        }
        private void AssigneUserControl()
        {
            List<Party> pL = gb.parties;
            PartiesContainer.Children.Clear();
            // Create a UserControl1 instance and set its data
            foreach (Party p in pL)
            {
                var partyControl = new UserControl1
                {

                    PartyNameLabel = { Content = p.PartyName },
                    PartyAcronymLabel = { Content = p.PartyAcronym },
                    //FoundedDateLabel = { Content = reader["FoundedDate"].ToString() },
                    HeadquartersLocationLabel = { Content = p.HeadquartersLocation },
                    PartyLeaderLabel = { Content = p.PartyLeader },
                    MembershipCriteriaLabel = { Content = p.MembershipCriteria },
                    PartyInfoLabel = { Text = p.PartyInfo },
                    MembershipSizeLabel = { Content = p.MembershipSize },
                    ElectionParticipationLabel = { Content = p.ElectionParticipation },
                    FundingSourcesLabel = { Content = p.FundingSources },
                    //LegalCertificationLabel = { Content = reader["LegalCertification"].ToString() }

                };
                partyControl.Margin = new Thickness(10);
                PartiesContainer.Children.Add(partyControl);
            }




        }

        private void PartyButton_Click(object sender, RoutedEventArgs e)
        {
            PartiesScrollViewer.Visibility = Visibility.Collapsed;
            EventsScrollViewer.Visibility = Visibility.Collapsed;

            PartiesScrollViewer.Visibility = Visibility.Visible;

        }

        private void EventButton_Click(object sender, RoutedEventArgs e)
        {
            PartiesScrollViewer.Visibility = Visibility.Collapsed;
            EventsScrollViewer.Visibility = Visibility.Collapsed;


            EventsScrollViewer.Visibility = Visibility.Visible;

        }
    }
    }
