using System;
using System.Globalization;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Provides a type converter to convert 64-bit signed integer objects to and from various other representations.</summary>
	// Token: 0x02000296 RID: 662
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class Int64Converter : BaseNumberConverter
	{
		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x060014A9 RID: 5289 RVA: 0x00052F11 File Offset: 0x00051111
		internal override Type TargetType
		{
			get
			{
				return typeof(long);
			}
		}

		// Token: 0x060014AA RID: 5290 RVA: 0x00052F1D File Offset: 0x0005111D
		internal override object FromString(string value, int radix)
		{
			return Convert.ToInt64(value, radix);
		}

		// Token: 0x060014AB RID: 5291 RVA: 0x00052F2B File Offset: 0x0005112B
		internal override object FromString(string value, NumberFormatInfo formatInfo)
		{
			return long.Parse(value, NumberStyles.Integer, formatInfo);
		}

		// Token: 0x060014AC RID: 5292 RVA: 0x00052F3A File Offset: 0x0005113A
		internal override object FromString(string value, CultureInfo culture)
		{
			return long.Parse(value, culture);
		}

		// Token: 0x060014AD RID: 5293 RVA: 0x00052F48 File Offset: 0x00051148
		internal override string ToString(object value, NumberFormatInfo formatInfo)
		{
			return ((long)value).ToString("G", formatInfo);
		}
	}
}
