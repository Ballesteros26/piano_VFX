using System;
using System.Globalization;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Provides a type converter to convert 16-bit unsigned integer objects to and from other representations.</summary>
	// Token: 0x020002F1 RID: 753
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class UInt16Converter : BaseNumberConverter
	{
		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x06001842 RID: 6210 RVA: 0x00060255 File Offset: 0x0005E455
		internal override Type TargetType
		{
			get
			{
				return typeof(ushort);
			}
		}

		// Token: 0x06001843 RID: 6211 RVA: 0x00060261 File Offset: 0x0005E461
		internal override object FromString(string value, int radix)
		{
			return Convert.ToUInt16(value, radix);
		}

		// Token: 0x06001844 RID: 6212 RVA: 0x0006026F File Offset: 0x0005E46F
		internal override object FromString(string value, NumberFormatInfo formatInfo)
		{
			return ushort.Parse(value, NumberStyles.Integer, formatInfo);
		}

		// Token: 0x06001845 RID: 6213 RVA: 0x0006027E File Offset: 0x0005E47E
		internal override object FromString(string value, CultureInfo culture)
		{
			return ushort.Parse(value, culture);
		}

		// Token: 0x06001846 RID: 6214 RVA: 0x0006028C File Offset: 0x0005E48C
		internal override string ToString(object value, NumberFormatInfo formatInfo)
		{
			return ((ushort)value).ToString("G", formatInfo);
		}
	}
}
