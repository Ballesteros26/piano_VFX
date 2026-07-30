using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Web.UI.Design
{
	/// <summary>Provides a type converter for <see cref="T:System.Web.UI.DataBindingCollection" /> objects.</summary>
	// Token: 0x0200005F RID: 95
	[Obsolete("This class is not supposed to be in use anymore as DesignerActionList is supposed to be used for editing DataBinding")]
	public class DataBindingCollectionConverter : TypeConverter
	{
		/// <summary>Converts a data binding collection to the specified type.</summary>
		/// <returns>The object produced by the type conversion. If the <paramref name="destinationType" /> parameter is of type <see cref="T:System.String" />, this method returns an empty string ("").</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that indicates the component or control to which the data binding collection belongs. </param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> that can be used to provide additional culture information. </param>
		/// <param name="value">The object to convert. </param>
		/// <param name="destinationType">The type to convert to. </param>
		// Token: 0x06000315 RID: 789 RVA: 0x0000252E File Offset: 0x0000072E
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(string))
			{
				return string.Empty;
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
