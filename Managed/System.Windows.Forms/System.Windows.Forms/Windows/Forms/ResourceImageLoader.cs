using System;
using System.Drawing;
using System.IO;
using System.Reflection;

namespace System.Windows.Forms
{
	// Token: 0x02000263 RID: 611
	internal class ResourceImageLoader
	{
		// Token: 0x060027CC RID: 10188 RVA: 0x00098C18 File Offset: 0x00096E18
		internal static Bitmap Get(string name)
		{
			Stream manifestResourceStream = ResourceImageLoader.assembly.GetManifestResourceStream(name);
			if (manifestResourceStream == null)
			{
				Console.WriteLine("Failed to read {0}", name);
				return null;
			}
			return new Bitmap(manifestResourceStream);
		}

		// Token: 0x060027CD RID: 10189 RVA: 0x00098C4C File Offset: 0x00096E4C
		internal static Icon GetIcon(string name)
		{
			Stream manifestResourceStream = ResourceImageLoader.assembly.GetManifestResourceStream(name);
			if (manifestResourceStream == null)
			{
				Console.WriteLine("Failed to read {0}", name);
				return null;
			}
			return new Icon(manifestResourceStream);
		}

		// Token: 0x040013EB RID: 5099
		private static Assembly assembly = typeof(ResourceImageLoader).Assembly;
	}
}
