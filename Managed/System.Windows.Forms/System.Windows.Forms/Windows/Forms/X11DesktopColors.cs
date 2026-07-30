using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	// Token: 0x020003C6 RID: 966
	internal class X11DesktopColors
	{
		// Token: 0x06004586 RID: 17798 RVA: 0x0010F100 File Offset: 0x0010D300
		static X11DesktopColors()
		{
			X11DesktopColors.FindDesktopEnvironment();
			X11DesktopColors.Desktop desktop = X11DesktopColors.desktop;
			if (desktop != X11DesktopColors.Desktop.Gtk)
			{
				if (desktop == X11DesktopColors.Desktop.KDE)
				{
					if (!X11DesktopColors.ReadKDEColorsheme())
					{
						Console.Error.WriteLine("KDE colorscheme read failure, using built-in colorscheme");
					}
				}
			}
			else
			{
				try
				{
					X11DesktopColors.GtkInit();
					IntPtr intPtr = X11DesktopColors.gtk_invisible_new();
					X11DesktopColors.gtk_widget_ensure_style(intPtr);
					IntPtr intPtr2 = X11DesktopColors.gtk_widget_get_style(intPtr);
					X11DesktopColors.GtkStyleStruct gtkStyleStruct = (X11DesktopColors.GtkStyleStruct)Marshal.PtrToStructure(intPtr2, typeof(X11DesktopColors.GtkStyleStruct));
					ThemeEngine.Current.ColorControl = X11DesktopColors.ColorFromGdkColor(gtkStyleStruct.bg[0]);
					ThemeEngine.Current.ColorControlText = X11DesktopColors.ColorFromGdkColor(gtkStyleStruct.fg[0]);
					ThemeEngine.Current.ColorControlDark = X11DesktopColors.ColorFromGdkColor(gtkStyleStruct.dark[0]);
					ThemeEngine.Current.ColorControlLight = X11DesktopColors.ColorFromGdkColor(gtkStyleStruct.light[0]);
					ThemeEngine.Current.ColorControlLightLight = ControlPaint.Light(X11DesktopColors.ColorFromGdkColor(gtkStyleStruct.light[0]));
					ThemeEngine.Current.ColorControlDarkDark = ControlPaint.Dark(X11DesktopColors.ColorFromGdkColor(gtkStyleStruct.dark[0]));
					intPtr = X11DesktopColors.gtk_menu_new();
					X11DesktopColors.gtk_widget_ensure_style(intPtr);
					intPtr2 = X11DesktopColors.gtk_widget_get_style(intPtr);
					gtkStyleStruct = (X11DesktopColors.GtkStyleStruct)Marshal.PtrToStructure(intPtr2, typeof(X11DesktopColors.GtkStyleStruct));
					ThemeEngine.Current.ColorMenu = X11DesktopColors.ColorFromGdkColor(gtkStyleStruct.bg[0]);
					ThemeEngine.Current.ColorMenuText = X11DesktopColors.ColorFromGdkColor(gtkStyleStruct.text[0]);
				}
				catch (DllNotFoundException)
				{
					Console.Error.WriteLine("Gtk not found (missing LD_LIBRARY_PATH to libgtk-x11-2.0.so.0?), using built-in colorscheme");
				}
				catch
				{
					Console.Error.WriteLine("Gtk colorscheme read failure, using built-in colorscheme");
				}
			}
		}

		// Token: 0x06004587 RID: 17799 RVA: 0x0010F320 File Offset: 0x0010D520
		private static void GtkInit()
		{
			X11DesktopColors.gtk_init_check(IntPtr.Zero, IntPtr.Zero);
		}

		// Token: 0x06004588 RID: 17800 RVA: 0x0010F334 File Offset: 0x0010D534
		private static void FindDesktopEnvironment()
		{
			X11DesktopColors.desktop = X11DesktopColors.Desktop.Gtk;
			string text = Environment.GetEnvironmentVariable("DESKTOP_SESSION");
			if (text != null)
			{
				text = text.ToUpper();
				if (text == "DEFAULT")
				{
					string environmentVariable = Environment.GetEnvironmentVariable("KDE_FULL_SESSION");
					if (environmentVariable != null)
					{
						X11DesktopColors.desktop = X11DesktopColors.Desktop.KDE;
					}
				}
				else if (text.StartsWith("KDE"))
				{
					X11DesktopColors.desktop = X11DesktopColors.Desktop.KDE;
				}
			}
		}

		// Token: 0x06004589 RID: 17801 RVA: 0x0010F3A4 File Offset: 0x0010D5A4
		internal static void Initialize()
		{
		}

		// Token: 0x0600458A RID: 17802 RVA: 0x0010F3A8 File Offset: 0x0010D5A8
		private static Color ColorFromGdkColor(X11DesktopColors.GdkColorStruct gtkcolor)
		{
			return Color.FromArgb(255, (gtkcolor.red >> 8) & 255, (gtkcolor.green >> 8) & 255, (gtkcolor.blue >> 8) & 255);
		}

		// Token: 0x0600458B RID: 17803 RVA: 0x0010F3E4 File Offset: 0x0010D5E4
		private static bool ReadKDEColorsheme()
		{
			string text = Environment.GetFolderPath(5) + "/.kde/share/config/kdeglobals";
			if (!File.Exists(text))
			{
				return false;
			}
			StreamReader streamReader = new StreamReader(text);
			for (string text2 = streamReader.ReadLine(); text2 != null; text2 = streamReader.ReadLine())
			{
				text2 = text2.Trim();
				if (text2.StartsWith("background="))
				{
					Color color = X11DesktopColors.GetColorFromKDEString(text2);
					if (color != Color.Empty)
					{
						ThemeEngine.Current.ColorControl = color;
						ThemeEngine.Current.ColorMenu = color;
					}
				}
				else if (text2.StartsWith("foreground="))
				{
					Color color = X11DesktopColors.GetColorFromKDEString(text2);
					if (color != Color.Empty)
					{
						ThemeEngine.Current.ColorControlText = color;
						ThemeEngine.Current.ColorMenuText = color;
					}
				}
				else if (text2.StartsWith("selectBackground"))
				{
					Color color = X11DesktopColors.GetColorFromKDEString(text2);
					if (color != Color.Empty)
					{
						ThemeEngine.Current.ColorHighlight = color;
					}
				}
				else if (text2.StartsWith("selectForeground"))
				{
					Color color = X11DesktopColors.GetColorFromKDEString(text2);
					if (color != Color.Empty)
					{
						ThemeEngine.Current.ColorHighlightText = color;
					}
				}
			}
			streamReader.Close();
			return true;
		}

		// Token: 0x0600458C RID: 17804 RVA: 0x0010F52C File Offset: 0x0010D72C
		private static Color GetColorFromKDEString(string line)
		{
			string[] array = line.Split(new char[] { '=' });
			if (array.Length > 0)
			{
				line = array[1];
				array = line.Split(new char[] { ',' });
				if (array.Length == 3)
				{
					int num = Convert.ToInt32(array[0]);
					int num2 = Convert.ToInt32(array[1]);
					int num3 = Convert.ToInt32(array[2]);
					return Color.FromArgb(num, num2, num3);
				}
			}
			return Color.Empty;
		}

		// Token: 0x0600458D RID: 17805
		[DllImport("libgtk-x11-2.0.so.0")]
		private static extern bool gtk_init_check(IntPtr argc, IntPtr argv);

		// Token: 0x0600458E RID: 17806
		[DllImport("libgdk-x11-2.0.so.0")]
		internal static extern IntPtr gdk_display_manager_get();

		// Token: 0x0600458F RID: 17807
		[DllImport("libgdk-x11-2.0.so.0")]
		internal static extern IntPtr gdk_display_manager_get_default_display(IntPtr display_manager);

		// Token: 0x06004590 RID: 17808
		[DllImport("libgtk-x11-2.0.so.0")]
		private static extern IntPtr gtk_invisible_new();

		// Token: 0x06004591 RID: 17809
		[DllImport("libgtk-x11-2.0.so.0")]
		private static extern IntPtr gtk_menu_new();

		// Token: 0x06004592 RID: 17810
		[DllImport("libgtk-x11-2.0.so.0")]
		private static extern void gtk_widget_ensure_style(IntPtr raw);

		// Token: 0x06004593 RID: 17811
		[DllImport("libgtk-x11-2.0.so.0")]
		private static extern IntPtr gtk_widget_get_style(IntPtr raw);

		// Token: 0x04001D55 RID: 7509
		private const string libgdk = "libgdk-x11-2.0.so.0";

		// Token: 0x04001D56 RID: 7510
		private const string libgtk = "libgtk-x11-2.0.so.0";

		// Token: 0x04001D57 RID: 7511
		private static X11DesktopColors.Desktop desktop;

		// Token: 0x020003C7 RID: 967
		internal struct GdkColorStruct
		{
			// Token: 0x04001D58 RID: 7512
			internal int pixel;

			// Token: 0x04001D59 RID: 7513
			internal short red;

			// Token: 0x04001D5A RID: 7514
			internal short green;

			// Token: 0x04001D5B RID: 7515
			internal short blue;
		}

		// Token: 0x020003C8 RID: 968
		internal struct GObjectStruct
		{
			// Token: 0x04001D5C RID: 7516
			public IntPtr Instance;

			// Token: 0x04001D5D RID: 7517
			public IntPtr ref_count;

			// Token: 0x04001D5E RID: 7518
			public IntPtr data;
		}

		// Token: 0x020003C9 RID: 969
		internal struct GtkStyleStruct
		{
			// Token: 0x04001D5F RID: 7519
			internal X11DesktopColors.GObjectStruct obj;

			// Token: 0x04001D60 RID: 7520
			[MarshalAs(30, SizeConst = 5)]
			internal X11DesktopColors.GdkColorStruct[] fg;

			// Token: 0x04001D61 RID: 7521
			[MarshalAs(30, SizeConst = 5)]
			internal X11DesktopColors.GdkColorStruct[] bg;

			// Token: 0x04001D62 RID: 7522
			[MarshalAs(30, SizeConst = 5)]
			internal X11DesktopColors.GdkColorStruct[] light;

			// Token: 0x04001D63 RID: 7523
			[MarshalAs(30, SizeConst = 5)]
			internal X11DesktopColors.GdkColorStruct[] dark;

			// Token: 0x04001D64 RID: 7524
			[MarshalAs(30, SizeConst = 5)]
			internal X11DesktopColors.GdkColorStruct[] mid;

			// Token: 0x04001D65 RID: 7525
			[MarshalAs(30, SizeConst = 5)]
			internal X11DesktopColors.GdkColorStruct[] text;

			// Token: 0x04001D66 RID: 7526
			[MarshalAs(30, SizeConst = 5)]
			internal X11DesktopColors.GdkColorStruct[] baseclr;

			// Token: 0x04001D67 RID: 7527
			[MarshalAs(30, SizeConst = 5)]
			internal X11DesktopColors.GdkColorStruct[] text_aa;

			// Token: 0x04001D68 RID: 7528
			internal X11DesktopColors.GdkColorStruct black;

			// Token: 0x04001D69 RID: 7529
			internal X11DesktopColors.GdkColorStruct white;
		}

		// Token: 0x020003CA RID: 970
		private enum Desktop
		{
			// Token: 0x04001D6B RID: 7531
			Gtk,
			// Token: 0x04001D6C RID: 7532
			KDE,
			// Token: 0x04001D6D RID: 7533
			Unknown
		}
	}
}
