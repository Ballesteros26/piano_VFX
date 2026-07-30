using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Web.Services.Configuration
{
	// Token: 0x02000144 RID: 324
	internal class TypeTypeConverter : TypeAndNameConverter
	{
		// Token: 0x060009E0 RID: 2528 RVA: 0x00043B69 File Offset: 0x00041D69
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x060009E1 RID: 2529 RVA: 0x00043B73 File Offset: 0x00041D73
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is string)
			{
				return ((TypeAndName)base.ConvertFrom(context, culture, value)).type;
			}
			return base.ConvertFrom(context, culture, value);
		}

		// Token: 0x060009E2 RID: 2530 RVA: 0x00043B9C File Offset: 0x00041D9C
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(string))
			{
				TypeAndName typeAndName = new TypeAndName((Type)value);
				return base.ConvertTo(context, culture, typeAndName, destinationType);
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
