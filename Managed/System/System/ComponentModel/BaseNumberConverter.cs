using System;
using System.Globalization;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Provides a base type converter for nonfloating-point numerical types.</summary>
	// Token: 0x020002F6 RID: 758
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public abstract class BaseNumberConverter : TypeConverter
	{
		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x06001878 RID: 6264 RVA: 0x000027E2 File Offset: 0x000009E2
		internal virtual bool AllowHex
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x06001879 RID: 6265
		internal abstract Type TargetType { get; }

		// Token: 0x0600187A RID: 6266
		internal abstract object FromString(string value, int radix);

		// Token: 0x0600187B RID: 6267
		internal abstract object FromString(string value, NumberFormatInfo formatInfo);

		// Token: 0x0600187C RID: 6268
		internal abstract object FromString(string value, CultureInfo culture);

		// Token: 0x0600187D RID: 6269 RVA: 0x00068883 File Offset: 0x00066A83
		internal virtual Exception FromStringError(string failedText, Exception innerException)
		{
			return new Exception(global::SR.GetString("{0} is not a valid value for {1}.", new object[]
			{
				failedText,
				this.TargetType.Name
			}), innerException);
		}

		// Token: 0x0600187E RID: 6270
		internal abstract string ToString(object value, NumberFormatInfo formatInfo);

		/// <summary>Determines if this converter can convert an object in the given source type to the native type of the converter.</summary>
		/// <returns>true if this converter can perform the operation; otherwise, false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="sourceType">A <see cref="T:System.Type" /> that represents the type from which you want to convert. </param>
		// Token: 0x0600187F RID: 6271 RVA: 0x0004DD24 File Offset: 0x0004BF24
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		/// <summary>Converts the given object to the converter's native type.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> that specifies the culture to represent the number. </param>
		/// <param name="value">The object to convert. </param>
		/// <exception cref="T:System.Exception">
		///   <paramref name="value" /> is not a valid value for the target type.</exception>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed. </exception>
		// Token: 0x06001880 RID: 6272 RVA: 0x000688B0 File Offset: 0x00066AB0
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is string)
			{
				string text = ((string)value).Trim();
				try
				{
					if (this.AllowHex && text[0] == '#')
					{
						return this.FromString(text.Substring(1), 16);
					}
					if ((this.AllowHex && text.StartsWith("0x")) || text.StartsWith("0X") || text.StartsWith("&h") || text.StartsWith("&H"))
					{
						return this.FromString(text.Substring(2), 16);
					}
					if (culture == null)
					{
						culture = CultureInfo.CurrentCulture;
					}
					NumberFormatInfo numberFormatInfo = (NumberFormatInfo)culture.GetFormat(typeof(NumberFormatInfo));
					return this.FromString(text, numberFormatInfo);
				}
				catch (Exception ex)
				{
					throw this.FromStringError(text, ex);
				}
			}
			return base.ConvertFrom(context, culture, value);
		}

		/// <summary>Converts the specified object to another type.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> that specifies the culture to represent the number. </param>
		/// <param name="value">The object to convert. </param>
		/// <param name="destinationType">The type to convert the object to. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="destinationType" /> is null.</exception>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed. </exception>
		// Token: 0x06001881 RID: 6273 RVA: 0x0006899C File Offset: 0x00066B9C
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == typeof(string) && value != null && this.TargetType.IsInstanceOfType(value))
			{
				if (culture == null)
				{
					culture = CultureInfo.CurrentCulture;
				}
				NumberFormatInfo numberFormatInfo = (NumberFormatInfo)culture.GetFormat(typeof(NumberFormatInfo));
				return this.ToString(value, numberFormatInfo);
			}
			if (destinationType.IsPrimitive)
			{
				return Convert.ChangeType(value, destinationType, culture);
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		/// <summary>Returns a value indicating whether this converter can convert an object to the given destination type using the context.</summary>
		/// <returns>true if this converter can perform the operation; otherwise, false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="t">A <see cref="T:System.Type" /> that represents the type to which you want to convert. </param>
		// Token: 0x06001882 RID: 6274 RVA: 0x00068A29 File Offset: 0x00066C29
		public override bool CanConvertTo(ITypeDescriptorContext context, Type t)
		{
			return base.CanConvertTo(context, t) || t.IsPrimitive;
		}
	}
}
