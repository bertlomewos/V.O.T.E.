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
        public int PID;
        public UserControl1()
        {
            InitializeComponent();
            voteManage = new VoteManage();
         
        }
        

        private void UpdateVoteCount()
        {
            int newVoteCount = voteManage.GetVoteCount(PID);
            VoteCountLabel.Content = newVoteCount.ToString();
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
            var getFromDb = new GetFromDb();
            getFromDb.GetPartiesForMainPage();
            voteManage.IncrementVoteCount(PID);
            UpdateVoteCount();
        }
    }
}
