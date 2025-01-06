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
using System.Windows.Navigation;
using System.Windows.Shapes;
using VOTE.Model;

namespace VOTE
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        private VoteManage voteManage;
        private int PartyID;
        public UserControl1()
        {
            InitializeComponent();
            voteManage = new VoteManage();         
         
        }
        

        private void UpdateVoteCount()
        {
            int newVoteCount = voteManage.GetVoteCount(PartyID);
            VoteCountLabel.Content = newVoteCount.ToString();
        }
        public void InitializeControl(int partyID, string partyName)
        {
            PartyID = partyID;
            PartyNameLabel.Content = partyName;

            // Fetch the initial vote count
            UpdateVoteCount();
        }
        private void MoreButton_Click(object sender, RoutedEventArgs e)
        {
            if (AdditionalInfoPanel.Visibility == Visibility.Collapsed)
            {
                AdditionalInfoPanel.Visibility = Visibility.Visible;
                ((Label)sender).Content = "Less...";
            }
            else
            {
                AdditionalInfoPanel.Visibility = Visibility.Collapsed;
                ((Label)sender).Content = "More...";
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
          //  Console.WriteLine("Button clicked!");

            voteManage.IncrementVoteCount(PartyID);

            UpdateVoteCount();
        }
    }
}
