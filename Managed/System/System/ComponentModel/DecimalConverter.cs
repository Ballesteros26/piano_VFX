using System;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Provides a type converter to convert <see cref="T:System.Decimal" /> objects to and from various other representations.</summary>
	// Token: 0x02000257 RID: 599
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class DecimalConverter : BaseNumberConverter
	{
		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06001335 RID: 4917 RVA: 0x00004240 File Offset: 0x00002440
		internal override bool AllowHex
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06001336 RID: 4918 RVA: 0x00051138 File Offset: 0x0004F338
		internal override Type TargetType
		{
			get
			{
				return typeof(decimal);
			}
		}

		/// <summary>Gets a value indicating whether this converter can convert an object to the given destination type using the context.</summary>
		/// <returns>true if this converter can perform the conversion; otherwise, false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="destinationType">A <see cref="T:System.Type" /> that represents the type you wish to convert to. </param>
		// Token: 0x06001337 RID: 4919 RVA: 0x00051144 File Offset: 0x0004F344
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(InstanceDescriptor) || base.CanConvertTo(context, destinationType);
		}

		/// <summary>Converts the given value object to a <see cref="T:System.Decimal" /> using the arguments.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="culture">An optional <see cref="T:System.Globalization.CultureInfo" />. If not supplied, the current culture is assumed. </param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert. </param>
		/// <param name="destinationType">The <see cref="T:System.Type" /> to convert the value to. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="destinationType" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed. </exception>
		// Token: 0x06001338 RID: 4920 RVA: 0x00051164 File Offset: 0x0004F364
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (!(destinationType == typeof(InstanceDescriptor)) || !(value is decimal))
			{
				return base.ConvertTo(context, culture, value, destinationType);
			}
			object[] array = new object[] { decimal.GetBits((decimal)value) };
			MemberInfo constructor = typeof(decimal).GetConstructor(new Type[] { typeof(int[]) });
			if (constructor != null)
			{
				return new InstanceDescriptor(constructor, array);
			}
			return null;
		}

		// Token: 0x06001339 RID: 4921 RVA: 0x000511F7 File Offset: 0x0004F3F7
		internal override object FromString(string value, int radix)
		{
			return Convert.ToDecimal(value, CultureInfo.CurrentCulture);
		}

		// Token: 0x0600133A RID: 4922 RVA: 0x00051209 File Offset: 0x0004F409
		internal override object FromString(string value, NumberFormatInfo formatInfo)
		{
			return decimal.Parse(value, NumberStyles.Float, formatInfo);
		}

		// Token: 0x0600133B RID: 4923 RVA: 0x0005121C File Offset: 0x0004F41C
		internal override object FromString(string value, CultureInfo culture)
		{
			return decimal.Parse(value, culture);
		}

		// Token: 0x0600133C RID: 4924 RVA: 0x0005122C File Offset: 0x0004F42C
		internal override string ToString(object value, NumberFormatInfo formatInfo)
		{
			return ((decimal)value).ToString("G", formatInfo);
		}
	}
}
