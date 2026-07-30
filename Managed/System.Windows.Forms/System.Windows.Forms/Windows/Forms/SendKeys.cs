using System;
using System.Collections;
using System.Text;

namespace System.Windows.Forms
{
	/// <summary>Provides methods for sending keystrokes to an application.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020002DA RID: 730
	public class SendKeys
	{
		// Token: 0x0600300B RID: 12299 RVA: 0x000B9C10 File Offset: 0x000B7E10
		private SendKeys()
		{
		}

		// Token: 0x0600300C RID: 12300 RVA: 0x000B9C18 File Offset: 0x000B7E18
		static SendKeys()
		{
			SendKeys.keywords.Add("BACKSPACE", 8);
			SendKeys.keywords.Add("BS", 8);
			SendKeys.keywords.Add("BKSP", 8);
			SendKeys.keywords.Add("BREAK", 3);
			SendKeys.keywords.Add("CAPSLOCK", 20);
			SendKeys.keywords.Add("DELETE", 46);
			SendKeys.keywords.Add("DEL", 46);
			SendKeys.keywords.Add("DOWN", 40);
			SendKeys.keywords.Add("END", 35);
			SendKeys.keywords.Add("ENTER", 13);
			SendKeys.keywords.Add("~", 13);
			SendKeys.keywords.Add("ESC", 27);
			SendKeys.keywords.Add("HELP", 47);
			SendKeys.keywords.Add("HOME", 36);
			SendKeys.keywords.Add("INSERT", 45);
			SendKeys.keywords.Add("INS", 45);
			SendKeys.keywords.Add("LEFT", 37);
			SendKeys.keywords.Add("NUMLOCK", 144);
			SendKeys.keywords.Add("PGDN", 34);
			SendKeys.keywords.Add("PGUP", 33);
			SendKeys.keywords.Add("PRTSC", 44);
			SendKeys.keywords.Add("RIGHT", 39);
			SendKeys.keywords.Add("SCROLLLOCK", 145);
			SendKeys.keywords.Add("TAB", 9);
			SendKeys.keywords.Add("UP", 38);
			SendKeys.keywords.Add("F1", 112);
			SendKeys.keywords.Add("F2", 113);
			SendKeys.keywords.Add("F3", 114);
			SendKeys.keywords.Add("F4", 115);
			SendKeys.keywords.Add("F5", 116);
			SendKeys.keywords.Add("F6", 117);
			SendKeys.keywords.Add("F7", 118);
			SendKeys.keywords.Add("F8", 119);
			SendKeys.keywords.Add("F9", 120);
			SendKeys.keywords.Add("F10", 121);
			SendKeys.keywords.Add("F11", 122);
			SendKeys.keywords.Add("F12", 123);
			SendKeys.keywords.Add("F13", 124);
			SendKeys.keywords.Add("F14", 125);
			SendKeys.keywords.Add("F15", 126);
			SendKeys.keywords.Add("F16", 127);
			SendKeys.keywords.Add("ADD", 107);
			SendKeys.keywords.Add("SUBTRACT", 109);
			SendKeys.keywords.Add("MULTIPLY", 106);
			SendKeys.keywords.Add("DIVIDE", 111);
			SendKeys.keywords.Add("+", 16);
			SendKeys.keywords.Add("^", 17);
			SendKeys.keywords.Add("%", 18);
		}

		// Token: 0x0600300D RID: 12301 RVA: 0x000BA068 File Offset: 0x000B8268
		private static void AddVKey(int vk, bool down)
		{
			MSG msg = default(MSG);
			msg.message = ((!down) ? Msg.WM_KEYUP : Msg.WM_KEYDOWN);
			msg.wParam = new IntPtr(vk);
			msg.lParam = IntPtr.Zero;
			SendKeys.keys.Enqueue(msg);
		}

		// Token: 0x0600300E RID: 12302 RVA: 0x000BA0C4 File Offset: 0x000B82C4
		private static void AddVKey(int vk, int repeat_count)
		{
			for (int i = 0; i < repeat_count; i++)
			{
				MSG msg = default(MSG);
				msg.message = Msg.WM_KEYDOWN;
				msg.wParam = new IntPtr(vk);
				msg.lParam = (IntPtr)1;
				SendKeys.keys.Enqueue(msg);
				msg = default(MSG);
				msg.message = Msg.WM_KEYUP;
				msg.wParam = new IntPtr(vk);
				msg.lParam = IntPtr.Zero;
				SendKeys.keys.Enqueue(msg);
			}
		}

		// Token: 0x0600300F RID: 12303 RVA: 0x000BA160 File Offset: 0x000B8360
		private static void AddKey(char key, int repeat_count)
		{
			for (int i = 0; i < repeat_count; i++)
			{
				MSG msg = default(MSG);
				msg.message = Msg.WM_KEYDOWN;
				msg.wParam = new IntPtr((int)key);
				msg.lParam = IntPtr.Zero;
				SendKeys.keys.Enqueue(msg);
				msg = default(MSG);
				msg.message = Msg.WM_KEYUP;
				msg.wParam = new IntPtr((int)key);
				msg.lParam = IntPtr.Zero;
				SendKeys.keys.Enqueue(msg);
			}
		}

		// Token: 0x06003010 RID: 12304 RVA: 0x000BA1FC File Offset: 0x000B83FC
		private static void Parse(string key_string)
		{
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			bool flag5 = false;
			StringBuilder stringBuilder = new StringBuilder();
			StringBuilder stringBuilder2 = new StringBuilder();
			int length = key_string.Length;
			for (int i = 0; i < length; i++)
			{
				char c = key_string.get_Chars(i);
				switch (c)
				{
				case '%':
					SendKeys.AddVKey((int)SendKeys.keywords["%"], true);
					flag5 = true;
					break;
				default:
					switch (c)
					{
					case '{':
					{
						stringBuilder2.Remove(0, stringBuilder2.Length);
						stringBuilder.Remove(0, stringBuilder.Length);
						int num = i + 1;
						while (num < length && key_string.get_Chars(num) != '}')
						{
							if (char.IsWhiteSpace(key_string.get_Chars(num)))
							{
								if (flag2)
								{
									throw new ArgumentException("SendKeys string {0} is not valid.", key_string);
								}
								flag2 = true;
							}
							else if (flag2)
							{
								if (!char.IsDigit(key_string.get_Chars(num)))
								{
									throw new ArgumentException("SendKeys string {0} is not valid.", key_string);
								}
								stringBuilder.Append(key_string.get_Chars(num));
							}
							else
							{
								stringBuilder2.Append(key_string.get_Chars(num));
							}
							num++;
						}
						if (num == length || num == i + 1)
						{
							throw new ArgumentException("SendKeys string {0} is not valid.", key_string);
						}
						if (!SendKeys.keywords.Contains(stringBuilder2.ToString().ToUpper()))
						{
							throw new ArgumentException("SendKeys string {0} is not valid.", key_string);
						}
						bool flag6 = true;
						int num2 = 1;
						if (stringBuilder.Length > 0)
						{
							num2 = int.Parse(stringBuilder.ToString());
						}
						if (flag6)
						{
							SendKeys.AddVKey((int)SendKeys.keywords[stringBuilder2.ToString().ToUpper()], (stringBuilder.Length != 0) ? num2 : 1);
						}
						else if (char.IsUpper(char.Parse(stringBuilder2.ToString())))
						{
							if (!flag3)
							{
								SendKeys.AddVKey((int)SendKeys.keywords["+"], true);
							}
							SendKeys.AddKey(char.Parse(stringBuilder2.ToString()), 1);
							if (!flag3)
							{
								SendKeys.AddVKey((int)SendKeys.keywords["+"], false);
							}
						}
						else
						{
							SendKeys.AddKey(char.Parse(stringBuilder2.ToString().ToUpper()), (stringBuilder.Length != 0) ? num2 : 1);
						}
						i = num;
						flag2 = false;
						if (flag3)
						{
							SendKeys.AddVKey((int)SendKeys.keywords["+"], false);
						}
						if (flag4)
						{
							SendKeys.AddVKey((int)SendKeys.keywords["^"], false);
						}
						if (flag5)
						{
							SendKeys.AddVKey((int)SendKeys.keywords["%"], false);
						}
						flag4 = (flag3 = (flag5 = false));
						break;
					}
					default:
						if (c != '^')
						{
							if (char.IsUpper(key_string.get_Chars(i)))
							{
								if (!flag3)
								{
									SendKeys.AddVKey((int)SendKeys.keywords["+"], true);
								}
								SendKeys.AddKey(key_string.get_Chars(i), 1);
								if (!flag3)
								{
									SendKeys.AddVKey((int)SendKeys.keywords["+"], false);
								}
							}
							else
							{
								SendKeys.AddKey(char.Parse(key_string.get_Chars(i).ToString().ToUpper()), 1);
							}
							if (!flag)
							{
								if (flag3)
								{
									SendKeys.AddVKey((int)SendKeys.keywords["+"], false);
								}
								if (flag4)
								{
									SendKeys.AddVKey((int)SendKeys.keywords["^"], false);
								}
								if (flag5)
								{
									SendKeys.AddVKey((int)SendKeys.keywords["%"], false);
								}
								flag4 = (flag3 = (flag5 = (flag = false)));
							}
						}
						else
						{
							SendKeys.AddVKey((int)SendKeys.keywords["^"], true);
							flag4 = true;
						}
						break;
					case '~':
						SendKeys.AddVKey((int)SendKeys.keywords["ENTER"], 1);
						break;
					}
					break;
				case '(':
					flag = true;
					break;
				case ')':
					if (flag3)
					{
						SendKeys.AddVKey((int)SendKeys.keywords["+"], false);
					}
					if (flag4)
					{
						SendKeys.AddVKey((int)SendKeys.keywords["^"], false);
					}
					if (flag5)
					{
						SendKeys.AddVKey((int)SendKeys.keywords["%"], false);
					}
					flag4 = (flag3 = (flag5 = (flag = false)));
					break;
				case '+':
					SendKeys.AddVKey((int)SendKeys.keywords["+"], true);
					flag3 = true;
					break;
				}
			}
			if (flag)
			{
				throw new ArgumentException("SendKeys string {0} is not valid.", key_string);
			}
			if (flag3)
			{
				SendKeys.AddVKey((int)SendKeys.keywords["+"], false);
			}
			if (flag4)
			{
				SendKeys.AddVKey((int)SendKeys.keywords["^"], false);
			}
			if (flag5)
			{
				SendKeys.AddVKey((int)SendKeys.keywords["%"], false);
			}
		}

		// Token: 0x06003011 RID: 12305 RVA: 0x000BA780 File Offset: 0x000B8980
		private static void SendInput()
		{
			IntPtr intPtr = XplatUI.GetActive();
			if (intPtr != IntPtr.Zero)
			{
				Form form = (Form)Control.FromHandle(intPtr);
				if (form != null && form.ActiveControl != null)
				{
					intPtr = form.ActiveControl.Handle;
				}
				else if (form != null)
				{
					intPtr = form.Handle;
				}
			}
			XplatUI.SendInput(intPtr, SendKeys.keys);
			SendKeys.keys.Clear();
		}

		/// <summary>Processes all the Windows messages currently in the message queue.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003012 RID: 12306 RVA: 0x000BA7F4 File Offset: 0x000B89F4
		public static void Flush()
		{
			Application.DoEvents();
		}

		/// <summary>Sends keystrokes to the active application.</summary>
		/// <param name="keys">The string of keystrokes to send. </param>
		/// <exception cref="T:System.InvalidOperationException">There is not an active application to send keystrokes to. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="keys" /> does not represent valid keystrokes</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003013 RID: 12307 RVA: 0x000BA7FC File Offset: 0x000B89FC
		public static void Send(string keys)
		{
			SendKeys.Parse(keys);
			SendKeys.SendInput();
		}

		/// <summary>Sends the given keys to the active application, and then waits for the messages to be processed.</summary>
		/// <param name="keys">The string of keystrokes to send. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003014 RID: 12308 RVA: 0x000BA80C File Offset: 0x000B8A0C
		public static void SendWait(string keys)
		{
			object obj = SendKeys.lockobj;
			lock (obj)
			{
				SendKeys.Send(keys);
			}
			SendKeys.Flush();
		}

		// Token: 0x04001704 RID: 5892
		private static Queue keys = new Queue();

		// Token: 0x04001705 RID: 5893
		private static Hashtable keywords = new Hashtable();

		// Token: 0x04001706 RID: 5894
		private static object lockobj = new object();

		// Token: 0x020002DB RID: 731
		private struct Keyword
		{
			// Token: 0x06003015 RID: 12309 RVA: 0x000BA858 File Offset: 0x000B8A58
			public Keyword(string keyword, int vk)
			{
				this.keyword = keyword;
				this.vk = vk;
			}

			// Token: 0x04001707 RID: 5895
			internal string keyword;

			// Token: 0x04001708 RID: 5896
			internal int vk;
		}
	}
}
