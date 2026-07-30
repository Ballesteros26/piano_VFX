using System;
using System.Reflection;

namespace System.Web.Util
{
	// Token: 0x0200010D RID: 269
	internal static class AssemblyUtil
	{
		// Token: 0x06000DC5 RID: 3525 RVA: 0x00025DC8 File Offset: 0x00023FC8
		public static string GetAssemblyFileVersion(Assembly assembly)
		{
			AssemblyFileVersionAttribute[] array = (AssemblyFileVersionAttribute[])assembly.GetCustomAttributes(typeof(AssemblyFileVersionAttribute), false);
			string text;
			if (array.Length != 0)
			{
				text = array[0].Version;
				if (string.IsNullOrEmpty(text))
				{
					text = "0.0.0.0";
				}
			}
			else
			{
				text = "0.0.0.0";
			}
			return text;
		}

		// Token: 0x0400118E RID: 4494
		private const string _emptyFileVersion = "0.0.0.0";
	}
}
