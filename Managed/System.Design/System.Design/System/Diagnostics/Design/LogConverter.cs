using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Diagnostics.Design
{
	/// <summary>Provides the type converter for the <see cref="P:System.Diagnostics.EventLog.Log" /> property.</summary>
	// Token: 0x020000E4 RID: 228
	public class LogConverter : TypeConverter
	{
		/// <summary>Indicates whether this converter can convert an object of the given type to the type of this converter, using the specified context. </summary>
		/// <returns>true if the conversion can be performed; otherwise, false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="sourceType">A T:System.Type  that represents the type you want to convert from.</param>
		// Token: 0x0600069B RID: 1691 RVA: 0x0000A4F4 File Offset: 0x000086F4
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		/// <summary>Converts the given object to a string, using the specified context and culture information.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
		/// <param name="context">An T:System.ComponentModel.ITypeDescriptorContext  that provides a format context.</param>
		/// <param name="culture">The T:System.Globalization.CultureInfo  to use as the current culture.</param>
		/// <param name="value">The T:System.Object  to convert</param>
		// Token: 0x0600069C RID: 1692 RVA: 0x0000A512 File Offset: 0x00008712
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is string)
			{
				return ((string)value).Trim();
			}
			return base.ConvertFrom(context, culture, value);
		}

		/// <summary>Gets a collection of standard values for the data type this validator is designed for.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection" /> that holds a standard set of valid values, or null if the data type does not support a standard set of values.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		// Token: 0x0600069D RID: 1693 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			throw new NotImplementedException();
		}

		/// <summary>Indicates whether this object supports a standard set of values that can be picked from a list using the specified context.</summary>
		/// <returns>true because <see cref="M:System.ComponentModel.TypeConverter.GetStandardValues" /> should be called to find a common set of values the object supports. This method never returns false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		// Token: 0x0600069E RID: 1694 RVA: 0x000023D8 File Offset: 0x000005D8
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}
	}
}
