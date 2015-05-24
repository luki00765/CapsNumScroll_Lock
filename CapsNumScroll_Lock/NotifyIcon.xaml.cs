using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CapsNumScroll_Lock
{
	/// <summary>
	/// Logika interakcji dla klasy NotifyIcon.xaml
	/// </summary>
	///
	public partial class NotifyIcon : UserControl
	{
		public NotifyIcon()
		{
			InitializeComponent();
		}

		public void SetTextToDisplay(string text)
		{
			if(text.Contains("On"))
			{
				infoAboutOnOff.Foreground = Brushes.Green;
			}
			else
			{
				infoAboutOnOff.Foreground = Brushes.Red;
			}

			infoAboutOnOff.Text = text.ToString();
		}
	}
}
