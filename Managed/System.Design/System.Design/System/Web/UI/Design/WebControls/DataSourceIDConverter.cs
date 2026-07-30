using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Web.UI.Design.WebControls
{
	/// <summary>Creates a user-selectable list of data source names. </summary>
	// Token: 0x020000D0 RID: 208
	public class DataSourceIDConverter : TypeConverter
	{
		/// <summary>Gets a value indicating whether this converter can convert an object in the specified source type to the native type of the converter. </summary>
		/// <returns>true if <paramref name="sourceType" /> is a string; otherwise, false.</returns>
		/// <param name="context">An object implementing the <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides information about a context to a type converter so that the type converter can perform a conversion.</param>
		/// <param name="sourceType">The <see cref="T:System.Type" /> of the object for which conversion is being requested.</param>
		// Token: 0x06000613 RID: 1555 RVA: 0x00008E9F File Offset: 0x0000709F
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string);
		}

		/// <summary>Converts the specified object to the native type of the converter. </summary>
		/// <returns>The <paramref name="value" /> parameter is returned as a string. </returns>
		/// <param name="context">An object implementing the <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides information about a context to a type converter so that the type converter can perform a conversion.</param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> object that specifies the culture of the <paramref name="value" /> parameter.</param>
		/// <param name="value">The object to convert.</param>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="value" /> is other than a string or null. </exception>
		// Token: 0x06000614 RID: 1556 RVA: 0x00009800 File Offset: 0x00007A00
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value == null)
			{
				return string.Empty;
			}
			if (value is string)
			{
				return (string)value;
			}
			throw base.GetConvertFromException(value);
		}

		/// <summary>Returns a list of the available data source names.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection" /> containing the names of the controls that implement the <see cref="T:System.Web.UI.IDataSource" /> interface and are available for use in the given context.</returns>
		/// <param name="context">An object implementing the <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides information about a context to a type converter so that the type converter can perform a conversion.</param>
		// Token: 0x06000615 RID: 1557 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets a value indicating whether the returned data source names are an exclusive list of possible values.</summary>
		/// <returns>Always false.</returns>
		/// <param name="context">An object implementing the <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides information about a context to a type converter so that the type converter can perform a conversion.</param>
		// Token: 0x06000616 RID: 1558 RVA: 0x0000241E File Offset: 0x0000061E
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return false;
		}

		/// <summary>Gets a value indicating whether this object returns a standard set of data source names that can be picked from a list.</summary>
		/// <returns>Always true.</returns>
		/// <param name="context">An object implementing the <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides information about a context to a type converter so that the type converter can perform a conversion.</param>
		// Token: 0x06000617 RID: 1559 RVA: 0x000023D8 File Offset: 0x000005D8
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		/// <summary>Gets a value that indicates whether the specified component is a valid data source.</summary>
		/// <returns>true if <paramref name="component" /> is a valid data source; otherwise, false.</returns>
		/// <param name="component">An object that implements the <see cref="T:System.ComponentModel.IComponent" /> interface.</param>
		// Token: 0x06000618 RID: 1560 RVA: 0x00009821 File Offset: 0x00007A21
		protected virtual bool IsValidDataSource(IComponent component)
		{
			return component != null && component is IDataSource;
		}
	}
}
