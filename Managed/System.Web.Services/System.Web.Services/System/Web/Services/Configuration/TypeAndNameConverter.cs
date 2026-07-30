using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Web.Services.Configuration
{
	// Token: 0x02000148 RID: 328
	internal class TypeAndNameConverter : TypeConverter
	{
		// Token: 0x060009FD RID: 2557 RVA: 0x00043DDE File Offset: 0x00041FDE
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x060009FE RID: 2558 RVA: 0x00043DFC File Offset: 0x00041FFC
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is string)
			{
				return new TypeAndName((string)value);
			}
			return base.ConvertFrom(context, culture, value);
		}

		// Token: 0x060009FF RID: 2559 RVA: 0x00043E1C File Offset: 0x0004201C
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (!(destinationType == typeof(string)))
			{
				return base.ConvertTo(context, culture, value, destinationType);
			}
			TypeAndName typeAndName = (TypeAndName)value;
			if (typeAndName.name != null)
			{
				return typeAndName.name;
			}
			return typeAndName.type.AssemblyQualifiedName;
		}
	}
}
