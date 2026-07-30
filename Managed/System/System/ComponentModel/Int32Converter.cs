using System;
using System.Globalization;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Provides a type converter to convert 32-bit signed integer objects to and from other representations.</summary>
	// Token: 0x02000295 RID: 661
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class Int32Converter : BaseNumberConverter
	{
		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x060014A3 RID: 5283 RVA: 0x00052EB9 File Offset: 0x000510B9
		internal override Type TargetType
		{
			get
			{
				return typeof(int);
			}
		}

		// Token: 0x060014A4 RID: 5284 RVA: 0x00052EC5 File Offset: 0x000510C5
		internal override object FromString(string value, int radix)
		{
			return Convert.ToInt32(value, radix);
		}

		// Token: 0x060014A5 RID: 5285 RVA: 0x00052ED3 File Offset: 0x000510D3
		internal override object FromString(string value, NumberFormatInfo formatInfo)
		{
			return int.Parse(value, NumberStyles.Integer, formatInfo);
		}

		// Token: 0x060014A6 RID: 5286 RVA: 0x00052EE2 File Offset: 0x000510E2
		internal override object FromString(string value, CultureInfo culture)
		{
			return int.Parse(value, culture);
		}

		// Token: 0x060014A7 RID: 5287 RVA: 0x00052EF0 File Offset: 0x000510F0
		internal override string ToString(object value, NumberFormatInfo formatInfo)
		{
			return ((int)value).ToString("G", formatInfo);
		}
	}
}
