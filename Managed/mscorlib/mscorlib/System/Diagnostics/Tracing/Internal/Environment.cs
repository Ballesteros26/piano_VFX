using System;
using System.Resources;
using Microsoft.Reflection;

namespace System.Diagnostics.Tracing.Internal
{
	// Token: 0x02000B1E RID: 2846
	internal static class Environment
	{
		// Token: 0x17001228 RID: 4648
		// (get) Token: 0x060065EF RID: 26095 RVA: 0x000C61FF File Offset: 0x000C43FF
		public static int TickCount
		{
			get
			{
				return Environment.TickCount;
			}
		}

		// Token: 0x060065F0 RID: 26096 RVA: 0x0014FCA8 File Offset: 0x0014DEA8
		public static string GetResourceString(string key, params object[] args)
		{
			string @string = Environment.rm.GetString(key);
			if (@string != null)
			{
				return string.Format(@string, args);
			}
			string text = string.Empty;
			foreach (object obj in args)
			{
				if (text != string.Empty)
				{
					text += ", ";
				}
				text += obj.ToString();
			}
			return key + " (" + text + ")";
		}

		// Token: 0x060065F1 RID: 26097 RVA: 0x0014FD1F File Offset: 0x0014DF1F
		public static string GetRuntimeResourceString(string key, params object[] args)
		{
			return Environment.GetResourceString(key, args);
		}

		// Token: 0x040032FE RID: 13054
		public static readonly string NewLine = Environment.NewLine;

		// Token: 0x040032FF RID: 13055
		private static ResourceManager rm = new ResourceManager("Microsoft.Diagnostics.Tracing.Messages", typeof(Environment).Assembly());
	}
}
