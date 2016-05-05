using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Collections.ObjectModel;
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
using System.ComponentModel;
using Microsoft.Win32;

namespace PartyOrganizer
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<Guest> guestsList;
        private int ID_EVENT;
        System.Windows.Threading.DispatcherTimer dispatcherTimer;

        public MainWindow(int ID_EVENT)
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

            guestsList = DataBase.GetGuestsByEventID(ID_EVENT);
            dgGuestList.ItemsSource = guestsList;    

            this.ID_EVENT = ID_EVENT;
            
            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(StatsRefreshTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }

        private void btnExportToXML_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog xmlSavePath = new SaveFileDialog() { Filter = "XML Files (*.xml)|*.xml", FilterIndex = 0 };
            xmlSavePath.ShowDialog();

            XmlParser.SaveXML(xmlSavePath.FileName, guestsList);
        }

        private void btnAddNewGuest_Click(object sender, RoutedEventArgs e)
        {
            if (tbName.Text != "" && tbSurname.Text != "" && cbSex.Text != "")
            {
                IEnumerable<Guest> tmp = guestsList
                    .Where(m => m.Name == tbName.Text)
                    .Where(m => m.Surname == tbSurname.Text)
                    .Where(m => m.Sex == cbSex.Text);

                if(tmp.Count() > 0)
                {
                    MessageBox.Show("Guest exist in the database");
                } else
                {
                    Guest newGuest = DataBase.AddGuest(this.ID_EVENT, tbName.Text, tbSurname.Text, cbSex.Text, tbEmail.Text);
                    guestsList.Add(newGuest);

                }

            } else
            {
                MessageBox.Show("You have to provide Name, Surname and Sex");
            }
        }
                
        private void btnSaveChanges_Click(object sender, RoutedEventArgs e)
        {
            DataBase.UpdateChanges(guestsList);
        }

        private void StatsRefreshTimer_Tick(object sender, EventArgs e)
        {
            Stats statisticsBox = Statistics.RefreshStats(guestsList);
            lbGuestCount.Content = statisticsBox.guestCount;
            lbMaleCount.Content = statisticsBox.maleCount;
            lbFemaleCount.Content = statisticsBox.femaleCount;
            lbConfirmedCount.Content = statisticsBox.guestConfirmed;
            lbMaleConfirmed.Content = statisticsBox.maleConfirmed;
            lbFemaleConfirmed.Content = statisticsBox.femaleConfirmed;
        }

        private void dgGuestList_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            Guest guest = (Guest)dgGuestList.SelectedItem;
            guestsList.Where(m => m.ID_GUEST == guest.ID_GUEST).Single().UpdateChanges = true;
        }
    }
}
