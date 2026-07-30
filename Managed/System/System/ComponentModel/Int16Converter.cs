using System;
using System.Globalization;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Provides a type converter to convert 16-bit signed integer objects to and from other representations.</summary>
	// Token: 0x02000294 RID: 660
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class Int16Converter : BaseNumberConverter
	{
		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x0600149D RID: 5277 RVA: 0x00052E60 File Offset: 0x00051060
		internal override Type TargetType
		{
			get
			{
				return typeof(short);
			}
		}

		// Token: 0x0600149E RID: 5278 RVA: 0x00052E6C File Offset: 0x0005106C
		internal override object FromString(string value, int radix)
		{
			return Convert.ToInt16(value, radix);
		}

		// Token: 0x0600149F RID: 5279 RVA: 0x00052E7A File Offset: 0x0005107A
		internal override object FromString(string value, CultureInfo culture)
		{
			return short.Parse(value, culture);
		}

		// Token: 0x060014A0 RID: 5280 RVA: 0x00052E88 File Offset: 0x00051088
		internal override object FromString(string value, NumberFormatInfo formatInfo)
		{
			return short.Parse(value, NumberStyles.Integer, formatInfo);
		}

		// Token: 0x060014A1 RID: 5281 RVA: 0x00052E98 File Offset: 0x00051098
		internal override string ToString(object value, NumberFormatInfo formatInfo)
		{
			return ((short)value).ToString("G", formatInfo);
		}
	}
}
