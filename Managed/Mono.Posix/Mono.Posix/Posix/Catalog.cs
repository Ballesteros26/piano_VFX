using System;
using System.Runtime.InteropServices;

namespace Mono.Posix
{
	// Token: 0x02000092 RID: 146
	[Obsolete("Use Mono.Unix.Catalog")]
	public class Catalog
	{
		// Token: 0x060006F0 RID: 1776
		[DllImport("intl")]
		private static extern IntPtr bindtextdomain(IntPtr domainname, IntPtr dirname);

		// Token: 0x060006F1 RID: 1777
		[DllImport("intl")]
		private static extern IntPtr bind_textdomain_codeset(IntPtr domainname, IntPtr codeset);

		// Token: 0x060006F2 RID: 1778
		[DllImport("intl")]
		private static extern IntPtr textdomain(IntPtr domainname);

		// Token: 0x060006F3 RID: 1779 RVA: 0x0000FFCC File Offset: 0x0000E1CC
		public static void Init(string package, string localedir)
		{
			IntPtr intPtr = Marshal.StringToHGlobalAuto(package);
			IntPtr intPtr2 = Marshal.StringToHGlobalAuto(localedir);
			IntPtr intPtr3 = Marshal.StringToHGlobalAuto("UTF-8");
			Catalog.bindtextdomain(intPtr, intPtr2);
			Catalog.bind_textdomain_codeset(intPtr, intPtr3);
			Catalog.textdomain(intPtr);
			Marshal.FreeHGlobal(intPtr);
			Marshal.FreeHGlobal(intPtr2);
			Marshal.FreeHGlobal(intPtr3);
		}

		// Token: 0x060006F4 RID: 1780
		[DllImport("intl")]
		private static extern IntPtr gettext(IntPtr instring);

		// Token: 0x060006F5 RID: 1781 RVA: 0x0001001C File Offset: 0x0000E21C
		public static string GetString(string s)
		{
			IntPtr intPtr = Marshal.StringToHGlobalAuto(s);
			string text = Marshal.PtrToStringAuto(Catalog.gettext(intPtr));
			Marshal.FreeHGlobal(intPtr);
			return text;
		}

		// Token: 0x060006F6 RID: 1782
		[DllImport("intl")]
		private static extern IntPtr ngettext(IntPtr singular, IntPtr plural, int n);

		// Token: 0x060006F7 RID: 1783 RVA: 0x00010044 File Offset: 0x0000E244
		public static string GetPluralString(string s, string p, int n)
		{
			IntPtr intPtr = Marshal.StringToHGlobalAuto(s);
			IntPtr intPtr2 = Marshal.StringToHGlobalAuto(p);
			string text = Marshal.PtrToStringAnsi(Catalog.ngettext(intPtr, intPtr2, n));
			Marshal.FreeHGlobal(intPtr);
			Marshal.FreeHGlobal(intPtr2);
			return text;
		}
	}
}
