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
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace kikelet_panzio
{
	public partial class Ugyfelek : Page
	{
		public Ugyfelek()
		{
			InitializeComponent();
		}

		private void regisztral(object sender, RoutedEventArgs e)
		{
			double kulonbseg = DateTime.Now.Subtract(szuldatum.SelectedDate ?? DateTime.Now).TotalDays;
			int evek = Convert.ToInt32(kulonbseg / 365.25d);
			if (ugyfelnev.Text == "" || szuldatum.SelectedDate.ToString() == "" || email.Text == "")
			{
				MessageBox.Show("Nincs minden adat megadva!");
			}
			else
			{
				if (evek < 18)
				{
					MessageBox.Show("Az ügyfél nincs minimum 18 éves!");
					return;
				}
				if (!MainWindow.megfeleloEmail(email.Text))
				{
					MessageBox.Show("Nem megfelelő e-mail!");
					return;
				}
                MainWindow.ugyfelek.Add(new Ugyfel(ugyfelnev.Text, MainWindow.Datumba(szuldatum.SelectedDate.ToString().Substring(0, 12)), email.Text, vip.IsChecked == true));
				MainWindow.KiirUgyfelek();
				MessageBox.Show("Sikeres hozzáadás!");
            }
        }
	}
}