using System;
using System.Globalization;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Provides a type converter to convert double-precision, floating point number objects to and from various other representations.</summary>
	// Token: 0x02000267 RID: 615
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class DoubleConverter : BaseNumberConverter
	{
		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x060013A7 RID: 5031 RVA: 0x00004240 File Offset: 0x00002440
		internal override bool AllowHex
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x060013A8 RID: 5032 RVA: 0x00051A6E File Offset: 0x0004FC6E
		internal override Type TargetType
		{
			get
			{
				return typeof(double);
			}
		}

		// Token: 0x060013A9 RID: 5033 RVA: 0x00051A7A File Offset: 0x0004FC7A
		internal override object FromString(string value, int radix)
		{
			return Convert.ToDouble(value, CultureInfo.CurrentCulture);
		}

		// Token: 0x060013AA RID: 5034 RVA: 0x00051A8C File Offset: 0x0004FC8C
		internal override object FromString(string value, NumberFormatInfo formatInfo)
		{
			return double.Parse(value, NumberStyles.Float, formatInfo);
		}

		// Token: 0x060013AB RID: 5035 RVA: 0x00051A9F File Offset: 0x0004FC9F
		internal override object FromString(string value, CultureInfo culture)
		{
			return double.Parse(value, culture);
		}

		// Token: 0x060013AC RID: 5036 RVA: 0x00051AB0 File Offset: 0x0004FCB0
		internal override string ToString(object value, NumberFormatInfo formatInfo)
		{
			return ((double)value).ToString("R", formatInfo);
		}
	}
}
