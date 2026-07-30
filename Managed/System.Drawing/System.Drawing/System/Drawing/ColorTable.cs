using System;
using System.Collections.Generic;
using System.Reflection;

namespace System.Drawing
{
	// Token: 0x0200000E RID: 14
	internal static class ColorTable
	{
		// Token: 0x0600001F RID: 31 RVA: 0x00002434 File Offset: 0x00000634
		private static Dictionary<string, Color> GetColors()
		{
			Dictionary<string, Color> dictionary = new Dictionary<string, Color>(StringComparer.OrdinalIgnoreCase);
			ColorTable.FillConstants(dictionary, typeof(Color));
			return dictionary;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002450 File Offset: 0x00000650
		internal static Dictionary<string, Color> Colors
		{
			get
			{
				return ColorTable.s_colorConstants.Value;
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x0000245C File Offset: 0x0000065C
		private static void FillConstants(Dictionary<string, Color> colors, Type enumType)
		{
			foreach (PropertyInfo propertyInfo in enumType.GetProperties())
			{
				if (propertyInfo.PropertyType == typeof(Color))
				{
					colors[propertyInfo.Name] = (Color)propertyInfo.GetValue(null, null);
				}
			}
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000024B2 File Offset: 0x000006B2
		internal static bool TryGetNamedColor(string name, out Color result)
		{
			return ColorTable.Colors.TryGetValue(name, out result);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000024C0 File Offset: 0x000006C0
		internal static bool IsKnownNamedColor(string name)
		{
			Color color;
			return ColorTable.Colors.TryGetValue(name, out color);
		}

		// Token: 0x04000091 RID: 145
		private static readonly Lazy<Dictionary<string, Color>> s_colorConstants = new Lazy<Dictionary<string, Color>>(new Func<Dictionary<string, Color>>(ColorTable.GetColors));
	}
}
