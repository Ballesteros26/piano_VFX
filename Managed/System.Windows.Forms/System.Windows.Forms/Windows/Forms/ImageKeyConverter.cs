using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Windows.Forms
{
	/// <summary>Provides a type converter to convert data for an image key to and from another data type.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001D6 RID: 470
	public class ImageKeyConverter : StringConverter
	{
		/// <summary>Gets or sets a value indicating whether null is valid in the <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection" /> collection.</summary>
		/// <returns>true in all cases, indicating null is valid in the standard values collection.</returns>
		// Token: 0x17000757 RID: 1879
		// (get) Token: 0x06001E11 RID: 7697 RVA: 0x00070F6C File Offset: 0x0006F16C
		protected virtual bool IncludeNoneAsStandardValue
		{
			get
			{
				return true;
			}
		}

		/// <summary>Returns whether this converter can convert an object of the given type to a string using the specified context.</summary>
		/// <returns>true to indicate the specified conversion can be performed; otherwise, false. </returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="sourceType">A <see cref="T:System.Type" /> that specifies the type you want to convert from.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001E12 RID: 7698 RVA: 0x00070F70 File Offset: 0x0006F170
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string);
		}

		/// <summary>Converts from the specified object to a string.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> to provide locale information. </param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert. </param>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001E13 RID: 7699 RVA: 0x00070F88 File Offset: 0x0006F188
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value != null && value is string)
			{
				return (string)value;
			}
			return base.ConvertFrom(context, culture, value);
		}

		/// <summary>Converts the given object to the specified type.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted <paramref name="value" />.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context, which can be used to extract additional information about the environment this type converter is being invoked from. This parameter or properties of this parameter can be null. </param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> that provides locale information. </param>
		/// <param name="value">The object to convert, typically an image key.</param>
		/// <param name="destinationType">The type to convert the object to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="destinationType" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">The specified <paramref name="value" /> could not be converted to the specified <paramref name="destinationType" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001E14 RID: 7700 RVA: 0x00070FAC File Offset: 0x0006F1AC
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (value == null)
			{
				return "(none)";
			}
			if (destinationType != typeof(string))
			{
				return base.ConvertTo(context, culture, value, destinationType);
			}
			if (value is string && (string)value == string.Empty)
			{
				return "(none)";
			}
			return value.ToString();
		}

		/// <summary>Returns a collection of standard image keys for the image list associated with the specified context. </summary>
		/// <returns>A <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection" /> that contains the standard set of image key values. </returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context, which can be used to extract additional information about the environment this type converter is being invoked from. This parameter or properties of this parameter can be null.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001E15 RID: 7701 RVA: 0x00071010 File Offset: 0x0006F210
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			string[] array = new string[] { string.Empty };
			return new TypeConverter.StandardValuesCollection(array);
		}

		/// <summary>Determines whether the list of standard values for the <see cref="T:System.Windows.Forms.ImageKeyConverter" /> is exclusive (that is, whether it allows values other than those returned by <see cref="Overload:System.Windows.Forms.ImageKeyConverter.GetStandardValues" />).</summary>
		/// <returns>true to indicate the list does not allow additional values; otherwise, false. Always returns true. </returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context, which can be used to extract additional information about the environment this type converter is being invoked from. This parameter or properties of this parameter can be null.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001E16 RID: 7702 RVA: 0x00071034 File Offset: 0x0006F234
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return true;
		}

		/// <summary>Determines whether this type converter supports a standard set of values that can be picked from a list.</summary>
		/// <returns>true to indicate a list of standard values is supported; otherwise, false. Always returns true.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context, which can be used to extract additional information about the environment this type converter is being invoked from. This parameter or properties of this parameter can be null.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001E17 RID: 7703 RVA: 0x00071038 File Offset: 0x0006F238
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}
	}
}
