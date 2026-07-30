using System;
using System.ComponentModel;
using System.Globalization;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>Converts between a string containing a list of font names and an array of strings representing the individual names.</summary>
	// Token: 0x02000397 RID: 919
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class FontNamesConverter : TypeConverter
	{
		/// <summary>Determines whether this converter can convert an object of the specified data type to an array of strings containing individual font names.</summary>
		/// <returns>true if the type can be converted; otherwise, false.</returns>
		/// <param name="context">A <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> object that provides information about the context of a type converter. You can optionally pass in null for this parameter. </param>
		/// <param name="sourceType">A <see cref="T:System.Type" /> that represents the data type to convert from. </param>
		// Token: 0x0600242D RID: 9261 RVA: 0x00035B25 File Offset: 0x00033D25
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		/// <summary>Converts a string that represents a list of font names into an array of strings containing individual font names.</summary>
		/// <returns>A <see cref="T:System.Object" /> instance that represents the array of strings containing the individual font names.</returns>
		/// <param name="context">A <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> object that provides information about the context of a type converter. This parameter is not used in this method. It is reserved for future versions of this method. You can optionally pass in null for this parameter. </param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> object that represents information about a culture such as language, calendar system, and so on. This parameter is not used in this method. It is reserved for future versions of this method. You can optionally pass in null for this parameter. </param>
		/// <param name="value">A <see cref="T:System.Object" /> instance that represents the source string to convert from. </param>
		/// <exception cref="M:System.ComponentModel.TypeConverter.GetConvertFromException(System.Object)">
		///   <paramref name="value" /> is not of type <see cref="T:System.String" />.</exception>
		// Token: 0x0600242E RID: 9262 RVA: 0x0005DEA4 File Offset: 0x0005C0A4
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (!(value is string))
			{
				return base.ConvertFrom(context, culture, value);
			}
			string text = (string)value;
			if (text == string.Empty)
			{
				return new string[0];
			}
			string[] array = text.Split(new char[] { ',' });
			int num = array.Length;
			for (int i = 0; i < num; i++)
			{
				array[i] = array[i].Trim();
			}
			return array;
		}

		/// <summary>Creates a string that represents a list of font names from an array of strings containing individual font names.</summary>
		/// <returns>A <see cref="T:System.Object" /> instance that represents a string containing a list of font names.</returns>
		/// <param name="context">A <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> object that provides information about the context of a type converter. This parameter is not used in this method. It is reserved for future versions of this method. You can optionally pass in null for this parameter. </param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> object that represents information about a culture such as language, calendar system, and so on. This parameter is not used in this method. It is reserved for future versions of this method. You can optionally pass in null for this parameter. </param>
		/// <param name="value">An object that represents the source array of strings to convert from. </param>
		/// <param name="destinationType">A <see cref="T:System.Object" /> instance object that represents the data type to convert to. This parameter must be of type <see cref="T:System.String" />.</param>
		/// <exception cref="M:System.ComponentModel.TypeConverter.GetConvertToException(System.Object,System.Type)">
		///   <paramref name="destinationType" /> is not of type <see cref="T:System.String" />.</exception>
		// Token: 0x0600242F RID: 9263 RVA: 0x0005DF0C File Offset: 0x0005C10C
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(string) && value is string[])
			{
				return string.Join(",", (string[])value);
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
