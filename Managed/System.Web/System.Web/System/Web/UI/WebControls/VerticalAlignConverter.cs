using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200043B RID: 1083
	internal class VerticalAlignConverter : EnumConverter
	{
		// Token: 0x060031EF RID: 12783 RVA: 0x000859AB File Offset: 0x00083BAB
		public VerticalAlignConverter()
			: base(typeof(VerticalAlign))
		{
		}

		// Token: 0x060031F0 RID: 12784 RVA: 0x00064C3E File Offset: 0x00062E3E
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x060031F1 RID: 12785 RVA: 0x00064C48 File Offset: 0x00062E48
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			return base.ConvertFrom(context, culture, value);
		}

		// Token: 0x060031F2 RID: 12786 RVA: 0x00064C53 File Offset: 0x00062E53
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return base.CanConvertTo(context, destinationType);
		}

		// Token: 0x060031F3 RID: 12787 RVA: 0x00064C5D File Offset: 0x00062E5D
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
