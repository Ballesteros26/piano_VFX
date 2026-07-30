using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Diagnostics.Design
{
	// Token: 0x020000E8 RID: 232
	internal class StringValueConverter : TypeConverter
	{
		// Token: 0x060006A6 RID: 1702 RVA: 0x0000A4F4 File Offset: 0x000086F4
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x0000A534 File Offset: 0x00008734
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			string text = value as string;
			if (text == null)
			{
				return base.ConvertFrom(context, culture, value);
			}
			text = text.Trim();
			if (text.Length == 0)
			{
				return null;
			}
			return text;
		}
	}
}
