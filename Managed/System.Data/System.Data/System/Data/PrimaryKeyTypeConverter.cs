using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Data
{
	// Token: 0x020000DA RID: 218
	internal sealed class PrimaryKeyTypeConverter : ReferenceConverter
	{
		// Token: 0x06000BE0 RID: 3040 RVA: 0x0003633E File Offset: 0x0003453E
		public PrimaryKeyTypeConverter()
			: base(typeof(DataColumn[]))
		{
		}

		// Token: 0x06000BE1 RID: 3041 RVA: 0x000061D5 File Offset: 0x000043D5
		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return false;
		}

		// Token: 0x06000BE2 RID: 3042 RVA: 0x0000EE4C File Offset: 0x0000D04C
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(string) || base.CanConvertTo(context, destinationType);
		}

		// Token: 0x06000BE3 RID: 3043 RVA: 0x00036350 File Offset: 0x00034550
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (!(destinationType == typeof(string)))
			{
				return base.ConvertTo(context, culture, value, destinationType);
			}
			return Array.Empty<DataColumn>().GetType().Name;
		}
	}
}
