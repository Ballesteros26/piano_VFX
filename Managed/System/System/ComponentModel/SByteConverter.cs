using System;
using System.Globalization;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Provides a type converter to convert 8-bit unsigned integer objects to and from a string.</summary>
	// Token: 0x020002D2 RID: 722
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class SByteConverter : BaseNumberConverter
	{
		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x06001706 RID: 5894 RVA: 0x0005BBFC File Offset: 0x00059DFC
		internal override Type TargetType
		{
			get
			{
				return typeof(sbyte);
			}
		}

		// Token: 0x06001707 RID: 5895 RVA: 0x0005BC08 File Offset: 0x00059E08
		internal override object FromString(string value, int radix)
		{
			return Convert.ToSByte(value, radix);
		}

		// Token: 0x06001708 RID: 5896 RVA: 0x0005BC16 File Offset: 0x00059E16
		internal override object FromString(string value, NumberFormatInfo formatInfo)
		{
			return sbyte.Parse(value, NumberStyles.Integer, formatInfo);
		}

		// Token: 0x06001709 RID: 5897 RVA: 0x0005BC25 File Offset: 0x00059E25
		internal override object FromString(string value, CultureInfo culture)
		{
			return sbyte.Parse(value, culture);
		}

		// Token: 0x0600170A RID: 5898 RVA: 0x0005BC34 File Offset: 0x00059E34
		internal override string ToString(object value, NumberFormatInfo formatInfo)
		{
			return ((sbyte)value).ToString("G", formatInfo);
		}
	}
}
