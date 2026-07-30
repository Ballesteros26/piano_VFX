using System;
using System.Globalization;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Provides a type converter to convert 32-bit unsigned integer objects to and from various other representations.</summary>
	// Token: 0x020002F2 RID: 754
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class UInt32Converter : BaseNumberConverter
	{
		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x06001848 RID: 6216 RVA: 0x000602AD File Offset: 0x0005E4AD
		internal override Type TargetType
		{
			get
			{
				return typeof(uint);
			}
		}

		// Token: 0x06001849 RID: 6217 RVA: 0x000602B9 File Offset: 0x0005E4B9
		internal override object FromString(string value, int radix)
		{
			return Convert.ToUInt32(value, radix);
		}

		// Token: 0x0600184A RID: 6218 RVA: 0x000602C7 File Offset: 0x0005E4C7
		internal override object FromString(string value, NumberFormatInfo formatInfo)
		{
			return uint.Parse(value, NumberStyles.Integer, formatInfo);
		}

		// Token: 0x0600184B RID: 6219 RVA: 0x000602D6 File Offset: 0x0005E4D6
		internal override object FromString(string value, CultureInfo culture)
		{
			return uint.Parse(value, culture);
		}

		// Token: 0x0600184C RID: 6220 RVA: 0x000602E4 File Offset: 0x0005E4E4
		internal override string ToString(object value, NumberFormatInfo formatInfo)
		{
			return ((uint)value).ToString("G", formatInfo);
		}
	}
}
