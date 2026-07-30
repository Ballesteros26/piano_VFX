using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Web.UI.WebControls
{
	// Token: 0x020003AE RID: 942
	internal class HorizontalAlignConverter : EnumConverter
	{
		// Token: 0x06002679 RID: 9849 RVA: 0x00064C2C File Offset: 0x00062E2C
		public HorizontalAlignConverter()
			: base(typeof(HorizontalAlign))
		{
		}

		// Token: 0x0600267A RID: 9850 RVA: 0x00064C3E File Offset: 0x00062E3E
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x0600267B RID: 9851 RVA: 0x00064C48 File Offset: 0x00062E48
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			return base.ConvertFrom(context, culture, value);
		}

		// Token: 0x0600267C RID: 9852 RVA: 0x00064C53 File Offset: 0x00062E53
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return base.CanConvertTo(context, destinationType);
		}

		// Token: 0x0600267D RID: 9853 RVA: 0x00064C5D File Offset: 0x00062E5D
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
