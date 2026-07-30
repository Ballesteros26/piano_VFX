using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Windows.Forms
{
	/// <summary>Converts <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> objects to and from other data types.  </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000F3 RID: 243
	public class DataGridViewCellStyleConverter : TypeConverter
	{
		/// <returns>true if this converter can perform the conversion; otherwise, false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="destinationType">A <see cref="T:System.Type" /> that represents the type you want to convert to. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060012A8 RID: 4776 RVA: 0x00048EA4 File Offset: 0x000470A4
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			throw new NotImplementedException();
		}

		/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" />. If null is passed, the current culture is assumed. </param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert. </param>
		/// <param name="destinationType">The <see cref="T:System.Type" /> to convert the <paramref name="value" /> parameter to. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060012A9 RID: 4777 RVA: 0x00048EAC File Offset: 0x000470AC
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			throw new NotImplementedException();
		}
	}
}
