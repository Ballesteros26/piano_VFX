using System;
using System.Globalization;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Provides a type converter to convert single-precision, floating point number objects to and from various other representations.</summary>
	// Token: 0x020002D4 RID: 724
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class SingleConverter : BaseNumberConverter
	{
		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x06001711 RID: 5905 RVA: 0x00004240 File Offset: 0x00002440
		internal override bool AllowHex
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x06001712 RID: 5906 RVA: 0x0005BCB9 File Offset: 0x00059EB9
		internal override Type TargetType
		{
			get
			{
				return typeof(float);
			}
		}

		// Token: 0x06001713 RID: 5907 RVA: 0x0005BCC5 File Offset: 0x00059EC5
		internal override object FromString(string value, int radix)
		{
			return Convert.ToSingle(value, CultureInfo.CurrentCulture);
		}

		// Token: 0x06001714 RID: 5908 RVA: 0x0005BCD7 File Offset: 0x00059ED7
		internal override object FromString(string value, NumberFormatInfo formatInfo)
		{
			return float.Parse(value, NumberStyles.Float, formatInfo);
		}

		// Token: 0x06001715 RID: 5909 RVA: 0x0005BCEA File Offset: 0x00059EEA
		internal override object FromString(string value, CultureInfo culture)
		{
			return float.Parse(value, culture);
		}

		// Token: 0x06001716 RID: 5910 RVA: 0x0005BCF8 File Offset: 0x00059EF8
		internal override string ToString(object value, NumberFormatInfo formatInfo)
		{
			return ((float)value).ToString("R", formatInfo);
		}
	}
}
