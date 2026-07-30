using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Windows.Forms
{
	/// <summary>Provides a type converter to convert data for an image index to and from a string.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001D5 RID: 469
	public class ImageIndexConverter : Int32Converter
	{
		/// <summary>Gets or sets a value indicating whether a none or null value is valid in the <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection" /> collection.</summary>
		/// <returns>true if a none or null value is valid in the standard values collection; otherwise, false.</returns>
		// Token: 0x17000756 RID: 1878
		// (get) Token: 0x06001E0A RID: 7690 RVA: 0x00070E90 File Offset: 0x0006F090
		protected virtual bool IncludeNoneAsStandardValue
		{
			get
			{
				return true;
			}
		}

		/// <summary>Converts the specified value object to a 32-bit signed integer object.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted <paramref name="value" />.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> to provide locale information. </param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert. </param>
		/// <exception cref="T:System.Exception">The conversion could not be performed. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001E0B RID: 7691 RVA: 0x00070E94 File Offset: 0x0006F094
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value == null || !(value is string))
			{
				return base.ConvertFrom(context, culture, value);
			}
			string text = (string)value;
			if (text == "(none)")
			{
				return -1;
			}
			return int.Parse(text);
		}

		/// <summary>Converts the specified object to the specified destination type.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted <paramref name="value" />.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context, which can be used to extract additional information about the environment this type converter is being invoked from. This parameter or properties of this parameter can be null. </param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> that provides locale information. </param>
		/// <param name="value">The object to convert, typically an index represented as an <see cref="T:System.Int32" />.</param>
		/// <param name="destinationType">The type to convert the object to, often a <see cref="T:System.String" />.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="destinationType" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">The specified <paramref name="value" /> could not be converted to the specified <paramref name="destinationType" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001E0C RID: 7692 RVA: 0x00070EE8 File Offset: 0x0006F0E8
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (value == null || destinationType != typeof(string))
			{
				return base.ConvertTo(context, culture, value, destinationType);
			}
			if (value is int && (int)value == -1)
			{
				return "(none)";
			}
			return value.ToString();
		}

		/// <summary>Returns a collection of standard index values for the image list associated with the specified format context.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection" /> that holds a standard set of valid index values. If no image list is found, this collection will contain a single object with a value of -1.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context, which can be used to extract additional information about the environment this type converter is being invoked from. This parameter or properties of this parameter can be null. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001E0D RID: 7693 RVA: 0x00070F3C File Offset: 0x0006F13C
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			int[] array = new int[] { -1 };
			return new TypeConverter.StandardValuesCollection(array);
		}

		/// <summary>Determines if the list of standard values returned from the <see cref="Overload:System.Windows.Forms.ImageIndexConverter.GetStandardValues" /> method is an exclusive list. </summary>
		/// <returns>true if the <see cref="Overload:System.Windows.Forms.ImageIndexConverter.GetStandardValues" /> method returns an exclusive list of valid values; otherwise, false. This implementation always returns false.</returns>
		/// <param name="context">A formatter context. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001E0E RID: 7694 RVA: 0x00070F5C File Offset: 0x0006F15C
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return false;
		}

		/// <summary>Determines if the type converter supports a standard set of values that can be picked from a list.</summary>
		/// <returns>true if the <see cref="Overload:System.Windows.Forms.ImageIndexConverter.GetStandardValues" /> method returns a standard set of values; otherwise, false. Always returns true.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context, which can be used to extract additional information about the environment this type converter is being invoked from. This parameter or properties of this parameter can be null. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001E0F RID: 7695 RVA: 0x00070F60 File Offset: 0x0006F160
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}
	}
}
