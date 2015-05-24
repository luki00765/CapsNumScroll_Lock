using Hardcodet.Wpf.TaskbarNotification;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace CapsNumScroll_Lock
{
	/// <summary>
	/// Logika interakcji dla klasy MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private KeyboardListener listener;
		
		public MainWindow()
		{
			InitializeComponent();
			//this.WindowState = System.Windows.WindowState.Minimized;

			/*string title = "CapsNumScroll_Lock";
			string text = "Program Runs";
			//show balloon with custom icon
			taskBarIcon.ShowBalloonTip(title, text, BalloonIcon.Info);
			Thread.Sleep(3000);
			//hide balloon
			taskBarIcon.HideBalloonTip();*/
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			listener = new KeyboardListener();
			listener.HookKeyboard();
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			listener.UnHookKeyboard();
			Application.Current.Shutdown();
		}

	}
}
