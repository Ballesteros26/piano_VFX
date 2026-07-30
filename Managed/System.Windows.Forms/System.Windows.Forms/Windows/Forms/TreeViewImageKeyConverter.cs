using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Windows.Forms
{
	/// <summary>Provides a type converter to convert data for an image key to and from another data type.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200039A RID: 922
	public class TreeViewImageKeyConverter : ImageKeyConverter
	{
		/// <summary>Converts the specified object to an object of the specified type using the specified culture information and context.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted <paramref name="value" />.</returns>
		/// <param name="context">A <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context, which can be used to extract additional information about the environment this type converter is being invoked from. This parameter or properties of this parameter can be null.</param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> that provides locale information.</param>
		/// <param name="value">The object to convert, typically an image key.</param>
		/// <param name="destinationType">The type to convert the object to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="destinationType" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">The specified <paramref name="value" /> cannot be converted to the specified <paramref name="destinationType" />.</exception>
		// Token: 0x0600437F RID: 17279 RVA: 0x0010ACCC File Offset: 0x00108ECC
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
