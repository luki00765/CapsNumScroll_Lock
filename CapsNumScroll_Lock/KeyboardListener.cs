using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace CapsNumScroll_Lock
{
	class KeyboardListener
	{
		NotifyIcon balloon = new NotifyIcon();
		MainWindow main = new MainWindow();

		private const int WH_KEYBOARD_LL = 13;
		private const int WM_KEYDOWN = 0x0100;
		private static LowLevelKeyboardProc _proc = HookCallback;
		private static IntPtr _hookID = IntPtr.Zero;
		private const uint VK_NUMLOCK = 0x90;
		private const uint VK_CAPITAL = 0x14;
		private const uint VK_SCROLL = 0x91;

		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern IntPtr SetWindowsHookEx(int idHook,
			LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool UnhookWindowsHookEx(IntPtr hhk);

		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
			IntPtr wParam, IntPtr lParam);

		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern IntPtr GetModuleHandle(string lpModuleName);

		private static IntPtr SetHook(LowLevelKeyboardProc proc)
		{
			using (Process curProcess = Process.GetCurrentProcess())
			using (ProcessModule curModule = curProcess.MainModule)
			{
				return SetWindowsHookEx(WH_KEYBOARD_LL, proc,
					GetModuleHandle(curModule.ModuleName), 0);
			}
		}

		private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

		private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
		{
			string text = "";
			if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
			{
				KeyboardListener keyboardListener = new KeyboardListener();
				int vkCode = Marshal.ReadInt32(lParam);
				if (vkCode == VK_CAPITAL)
				{
					bool CapsLockOn = Keyboard.IsKeyToggled(Key.CapsLock);
					if (CapsLockOn == false)
					{
						text = "Caps Lock On";
						keyboardListener.SetTheTextToBallonAndShowBallon(text);
						//MessageBox.Show("Caps Lock On");
					}
					if (CapsLockOn == true)
					{
						text = "Caps Lock Off";
						keyboardListener.SetTheTextToBallonAndShowBallon(text);
						//MessageBox.Show("Caps Lock Off");
					}
				}

				if (vkCode == VK_NUMLOCK)
				{
					bool NumLockOn = Keyboard.IsKeyToggled(Key.NumLock);
					if (NumLockOn == false)
					{
						text = "Num Lock On";
						keyboardListener.SetTheTextToBallonAndShowBallon(text);
						//MessageBox.Show("Num Lock On");
					}
					if (NumLockOn == true)
					{
						text = "Num Lock Off";
						keyboardListener.SetTheTextToBallonAndShowBallon(text);
						//MessageBox.Show("Num Lock Off");
					}
				}

				if (vkCode == VK_SCROLL)
				{
					bool ScrollLockOn = Keyboard.IsKeyToggled(Key.Scroll);
					if (ScrollLockOn == false)
					{
						text = "Scroll Lock On";
						keyboardListener.SetTheTextToBallonAndShowBallon(text);
						//MessageBox.Show("Scroll Lock On");
					}
					if (ScrollLockOn == true)
					{
						text = "Scroll Lock Off";
						keyboardListener.SetTheTextToBallonAndShowBallon(text);
						//MessageBox.Show("Scroll Lock off");
					}
				}

			}
			return CallNextHookEx(_hookID, nCode, wParam, lParam);
		}

		private void SetTheTextToBallonAndShowBallon(string text)
		{
			balloon.SetTextToDisplay(text);
			main.taskBarIcon.ShowCustomBalloon(balloon, PopupAnimation.Slide, 2100);
			
			//Amain = null;
			GC.Collect();
			GC.WaitForPendingFinalizers();
		}

		public void UnHookKeyboard()
		{
			UnhookWindowsHookEx(_hookID);
		}

		public void HookKeyboard()
		{
			_hookID = SetHook(_proc);
		}

	}
}
