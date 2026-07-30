using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Windows.Forms
{
	// Token: 0x02000191 RID: 401
	internal class SpecialFolderEnumConverter : TypeConverter
	{
		// Token: 0x06001990 RID: 6544 RVA: 0x0006230C File Offset: 0x0006050C
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value == null || !(value is string))
			{
				return base.ConvertFrom(context, culture, value);
			}
			return Enum.Parse(typeof(Environment.SpecialFolder), (string)value, true);
		}

		// Token: 0x06001991 RID: 6545 RVA: 0x00062340 File Offset: 0x00060540
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (value == null || !(value is Environment.SpecialFolder) || destinationType != typeof(string))
			{
				return base.ConvertTo(context, culture, value, destinationType);
			}
			return ((int)value).ToString();
		}
	}
}
