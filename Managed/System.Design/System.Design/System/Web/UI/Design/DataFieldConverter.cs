using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Web.UI.Design
{
	/// <summary>Provides a type converter that can retrieve a list of data fields from the current component's selected data source.</summary>
	// Token: 0x02000064 RID: 100
	public class DataFieldConverter : TypeConverter
	{
		/// <summary>Gets a value indicating whether the converter can convert an object of the specified source type to the native type of the converter.</summary>
		/// <returns>true if the converter can perform the conversion; otherwise, false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> object that can be used to gain additional context information. </param>
		/// <param name="sourceType">A <see cref="T:System.Type" /> that represents the type you wish to convert from. </param>
		// Token: 0x06000324 RID: 804 RVA: 0x00008E9F File Offset: 0x0000709F
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string);
		}

		/// <summary>Converts the specified object to the native type of the converter.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the specified object after conversion.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> object that can be used to gain additional context information. </param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> that can be used to support localization features. </param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert. </param>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed. </exception>
		// Token: 0x06000325 RID: 805 RVA: 0x00008EB1 File Offset: 0x000070B1
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value == null)
			{
				return string.Empty;
			}
			if (value.GetType() == typeof(string))
			{
				return (string)value;
			}
			throw base.GetConvertFromException(value);
		}

		/// <summary>Gets the data fields present within the selected data source if information about them is available.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection" /> listing the standard accessible data sources.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> object indicating the component or control to get values for. </param>
		// Token: 0x06000326 RID: 806 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets a value indicating whether the collection of standard values returned from <see cref="M:System.ComponentModel.TypeConverter.GetStandardValues" /> is a list of all possible values.</summary>
		/// <returns>true if the <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection" /> returned from <see cref="M:System.ComponentModel.TypeConverter.GetStandardValues" /> is an exclusive list of all possible values that are valid; false if other values are possible.As implemented in this class, this method always returns false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> object that can be used to gain additional context information. </param>
		// Token: 0x06000327 RID: 807 RVA: 0x0000241E File Offset: 0x0000061E
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return false;
		}

		/// <summary>Gets a value indicating whether the converter supports a standard set of values that can be picked from a list.</summary>
		/// <returns>true if <see cref="M:System.ComponentModel.TypeConverter.GetStandardValues" /> can be called to find a common set of values the object supports; otherwise, false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to gain additional context information. </param>
		// Token: 0x06000328 RID: 808 RVA: 0x00008EE1 File Offset: 0x000070E1
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return context.Instance is IComponent;
		}
	}
}
