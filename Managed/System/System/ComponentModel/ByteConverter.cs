using System;
using System.Globalization;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Provides a type converter to convert 8-bit unsigned integer objects to and from various other representations.</summary>
	// Token: 0x0200023A RID: 570
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class ByteConverter : BaseNumberConverter
	{
		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06001286 RID: 4742 RVA: 0x0004DE75 File Offset: 0x0004C075
		internal override Type TargetType
		{
			get
			{
				return typeof(byte);
			}
		}

		// Token: 0x06001287 RID: 4743 RVA: 0x0004DE81 File Offset: 0x0004C081
		internal override object FromString(string value, int radix)
		{
			return Convert.ToByte(value, radix);
		}

		// Token: 0x06001288 RID: 4744 RVA: 0x0004DE8F File Offset: 0x0004C08F
		internal override object FromString(string value, NumberFormatInfo formatInfo)
		{
			return byte.Parse(value, NumberStyles.Integer, formatInfo);
		}

		// Token: 0x06001289 RID: 4745 RVA: 0x0004DE9E File Offset: 0x0004C09E
		internal override object FromString(string value, CultureInfo culture)
		{
			return byte.Parse(value, culture);
		}

		// Token: 0x0600128A RID: 4746 RVA: 0x0004DEAC File Offset: 0x0004C0AC
		internal override string ToString(object value, NumberFormatInfo formatInfo)
		{
			return ((byte)value).ToString("G", formatInfo);
		}
	}
}
