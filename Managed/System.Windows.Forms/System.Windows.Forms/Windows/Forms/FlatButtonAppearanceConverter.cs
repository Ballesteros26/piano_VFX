using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Windows.Forms
{
	// Token: 0x02000189 RID: 393
	internal class FlatButtonAppearanceConverter : ExpandableObjectConverter
	{
		// Token: 0x06001954 RID: 6484 RVA: 0x00060850 File Offset: 0x0005EA50
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(string))
			{
				return string.Empty;
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		// Token: 0x06001955 RID: 6485 RVA: 0x00060880 File Offset: 0x0005EA80
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(string) || base.CanConvertTo(context, destinationType);
		}
	}
}
