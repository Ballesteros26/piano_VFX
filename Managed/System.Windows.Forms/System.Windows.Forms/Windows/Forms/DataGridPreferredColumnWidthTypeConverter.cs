using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Windows.Forms
{
	/// <summary>Converts the value of an object to a different data type.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000CB RID: 203
	public class DataGridPreferredColumnWidthTypeConverter : TypeConverter
	{
		/// <summary>Gets a value that specifies whether the converter can convert an object in the given source type to the native type of the converter.</summary>
		/// <returns>true, if this converter can perform the conversion; otherwise, false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="sourceType">A <see cref="T:System.Type" /> that represents the type you want to convert from. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000D70 RID: 3440 RVA: 0x0003679C File Offset: 0x0003499C
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || sourceType == typeof(int) || base.CanConvertFrom(context, sourceType);
		}

		/// <summary>Converts the given object to the native type of the converter.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that a provides format context. </param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> to use for the current culture. </param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000D71 RID: 3441 RVA: 0x000367D4 File Offset: 0x000349D4
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (!(value is string))
			{
				return base.ConvertFrom(context, culture, value);
			}
			if (((string)value).Equals("AutoColumnResize (-1)"))
			{
				return -1;
			}
			return int.Parse((string)value);
		}

		/// <summary>Converts the given value to the native type of the converter.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides format context. </param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> to use for the current culture. </param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert. </param>
		/// <param name="destinationType">The <see cref="T:System.Type" /> of the conversion. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000D72 RID: 3442 RVA: 0x00036824 File Offset: 0x00034A24
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(string) && (int)value == -1)
			{
				return "AutoColumnResize (-1)";
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
