﻿using System;
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

namespace Hotel_Aqua_System
{
    /// <summary>
    /// Interaction logic for EditOrderData.xaml
    /// </summary>
    public partial class EditOrderData : Window
    {
        // Selected order's number.
        int s;

        public EditOrderData(int i)
        {
            InitializeComponent();

            // Selected customer's number assignment.
            s = i;

            // Nie działa.
            // ComboBox1 item selection.
            for (int j = 0; j < cbox1.Items.Count; j++)
            {
                if (cbox1.Items[j].ToString().Contains("ID: 0"))
                {
                    cbox1.SelectedItem = cbox1.Items[j];
                    break;
                }
            }
           
            dp1.SelectedDate = Order.getChosenOrder(i).startDatePub;
            dp2.SelectedDate = Order.getChosenOrder(i).endDatePub;

        }

        private void btn2_Click(object sender, RoutedEventArgs e)
        {
            AddClient newWin = new AddClient();
            newWin.Show();
        }

        private void cbox2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Reset of previously chosen settings.
            tbox3.Text = "";
            cbox4.IsChecked = false;
            tbox4.Text = "";
            cbox3.IsChecked = false;
            tbox7.Text = "";

            // Room choice.
            if (cbox2.SelectedIndex >= 0)
            {
                tbox3.Text = Room.getChosenRoom(cbox2.SelectedIndex).basicNightPricePub.ToString();
                tbox4.Text = (Room.getChosenRoom(cbox2.SelectedIndex).basicNightPricePub * (dp2.SelectedDate.Value - dp1.SelectedDate.Value).TotalDays).ToString();

            }
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            cbox1.Items.Clear();

            for (int i = 0; i < Client.getClientAmount(); i++)
            {
                cbox1.Items.Add(Client.getChosenClient(i).surnamePub + "  " + Client.getChosenClient(i).firstNamePub + "  ( ID: " + Client.getChosenClient(i).idPub + " )");
            }
        }

        private void dp1_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            // Reset of previously chosen settings.
            cbox2.Items.Clear();
            tbox3.Text = "";
            cbox4.IsChecked = false;
            tbox4.Text = "";
            cbox3.IsChecked = false;
            tbox7.Text = "";

            // If date of check-out has been selected.
            if (dp2.SelectedDate != null)
            {
                List<int> notAvailableRooms = new List<int>();

                notAvailableRooms = Order.notAvailableRooms(dp1.SelectedDate.Value, dp2.SelectedDate.Value);

                for (int i = 0; i < Room.getRoomAmount(); i++)
                {
                    if (notAvailableRooms.Contains(Room.getChosenRoom(i).roomNumberPub))
                        continue;
                    else
                        cbox2.Items.Add(Room.getChosenRoom(i).roomNumberPub);
                }
            }
        }

        private void dp2_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            // Reset of previously chosen settings.
            cbox2.Items.Clear();
            tbox3.Text = "";
            cbox4.IsChecked = false;
            tbox4.Text = "";
            cbox3.IsChecked = false;
            tbox7.Text = "";

            // If date of check-in has been selected.
            if (dp1.SelectedDate != null)
            {
                List<int> notAvailableRooms = new List<int>();

                notAvailableRooms = Order.notAvailableRooms(dp1.SelectedDate.Value, dp2.SelectedDate.Value);

                for (int i = 0; i < Room.getRoomAmount(); i++)
                {
                    if (notAvailableRooms.Contains(Room.getChosenRoom(i).roomNumberPub))
                        continue;
                    else
                        cbox2.Items.Add(Room.getChosenRoom(i).roomNumberPub);
                }
            }
        }

        private void cbox4_Checked(object sender, RoutedEventArgs e)
        {
            tbox6.IsEnabled = true;
        }

        private void cbox4_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        private void cbox4_Unchecked(object sender, RoutedEventArgs e)
        {
            tbox6.IsEnabled = false;
            tbox6.Text = "0%";
        }

        private void tbox6_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!(tbox6.Text.EndsWith("%")))
            {
                tbox6.Text += "%";
            }

            // Discount implementation.
            double basicPrice = Convert.ToDouble(tbox4.Text);
            double discount = Int32.Parse(tbox6.Text.Remove(tbox6.Text.Length - 1)) * 0.01;
            double newPrice = basicPrice - basicPrice * discount;
            tbox4.Text = newPrice.ToString();
        }

        private void cbox3_Checked(object sender, RoutedEventArgs e)
        {
            tbox3.IsEnabled = false;
            tbox6.IsEnabled = false;
            tbox4.IsEnabled = false;
            tbox5.IsEnabled = true;
        }

        private void cbox3_Unchecked(object sender, RoutedEventArgs e)
        {
            tbox3.IsEnabled = true;
            tbox6.IsEnabled = true;
            tbox4.IsEnabled = true;
            tbox5.IsEnabled = false;
            tbox5.Text = "";
        }
    }
}
