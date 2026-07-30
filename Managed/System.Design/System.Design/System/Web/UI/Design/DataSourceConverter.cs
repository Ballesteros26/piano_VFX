using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Web.UI.Design
{
	/// <summary>Provides a type converter that can retrieve a list of data sources accessible to the current component.</summary>
	// Token: 0x0200006A RID: 106
	public class DataSourceConverter : TypeConverter
	{
		/// <summary>Gets a value indicating whether the converter can convert an object of the specified source type to the native type of the converter.</summary>
		/// <returns>true if the converter can perform the conversion; otherwise, false.As implemented in this class, this method always returns false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to gain additional context information. </param>
		/// <param name="sourceType">A <see cref="T:System.Type" /> that represents the type you want to convert from. </param>
		// Token: 0x06000343 RID: 835 RVA: 0x00008E9F File Offset: 0x0000709F
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string);
		}

		/// <summary>Converts the specified object to the native type of the converter.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the specified object after conversion.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to gain additional context information. </param>
		/// <param name="culture">The <see cref="T:System.Globalization.CultureInfo" /> that can be used to support localization features. </param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert. </param>
		/// <exception cref="T:System.NotSupportedException">The conversion could not be performed. </exception>
		// Token: 0x06000344 RID: 836 RVA: 0x00008EB1 File Offset: 0x000070B1
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

		/// <summary>Gets the standard data sources accessible to the control.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection" /> listing the standard accessible data sources.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> indicating the component or control to get values for. </param>
		// Token: 0x06000345 RID: 837 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets a value indicating whether the collection of standard values returned from <see cref="M:System.ComponentModel.TypeConverter.GetStandardValues" /> is a list of all possible values.</summary>
		/// <returns>true if the <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection" /> returned from <see cref="M:System.ComponentModel.TypeConverter.GetStandardValues" /> is an exclusive list of all possible values; false if other values are possible.As implemented in this class, this method always returns false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to gain additional context information. </param>
		// Token: 0x06000346 RID: 838 RVA: 0x0000241E File Offset: 0x0000061E
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return false;
		}

		/// <summary>Gets a value indicating whether the converter supports a standard set of values that can be picked from a list.</summary>
		/// <returns>true if <see cref="M:System.ComponentModel.TypeConverter.GetStandardValues" /> should be called to find a common set of values the object supports; otherwise, false. This implementation always returns true.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to gain additional context information. </param>
		// Token: 0x06000347 RID: 839 RVA: 0x00008EE1 File Offset: 0x000070E1
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return context.Instance is IComponent;
		}

		/// <summary>Indicates whether the specified component is a valid data source for this converter.</summary>
		/// <returns>true if <paramref name="component" /> implements <see cref="T:System.Collections.IEnumerable" /> or <see cref="T:System.ComponentModel.IListSource" />; otherwise, false.</returns>
		/// <param name="component">The component to check as a valid data source.</param>
		// Token: 0x06000348 RID: 840 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		protected virtual bool IsValidDataSource(IComponent component)
		{
			throw new NotImplementedException();
		}
	}
}
