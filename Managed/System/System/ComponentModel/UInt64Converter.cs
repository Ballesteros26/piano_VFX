using System;
using System.Globalization;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Provides a type converter to convert 64-bit unsigned integer objects to and from other representations.</summary>
	// Token: 0x020002F3 RID: 755
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class UInt64Converter : BaseNumberConverter
	{
		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x0600184E RID: 6222 RVA: 0x00060305 File Offset: 0x0005E505
		internal override Type TargetType
		{
			get
			{
				return typeof(ulong);
			}
		}

		// Token: 0x0600184F RID: 6223 RVA: 0x00060311 File Offset: 0x0005E511
		internal override object FromString(string value, int radix)
		{
			return Convert.ToUInt64(value, radix);
		}

		// Token: 0x06001850 RID: 6224 RVA: 0x0006031F File Offset: 0x0005E51F
		internal override object FromString(string value, NumberFormatInfo formatInfo)
		{
			return ulong.Parse(value, NumberStyles.Integer, formatInfo);
		}

		// Token: 0x06001851 RID: 6225 RVA: 0x0006032E File Offset: 0x0005E52E
		internal override object FromString(string value, CultureInfo culture)
		{
			return ulong.Parse(value, culture);
		}

		// Token: 0x06001852 RID: 6226 RVA: 0x0006033C File Offset: 0x0005E53C
		internal override string ToString(object value, NumberFormatInfo formatInfo)
		{
			return ((ulong)value).ToString("G", formatInfo);
		}
	}
}
