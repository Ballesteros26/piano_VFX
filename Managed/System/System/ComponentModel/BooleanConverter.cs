using System;
using System.Globalization;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Provides a type converter to convert <see cref="T:System.Boolean" /> objects to and from various other representations.</summary>
	// Token: 0x02000238 RID: 568
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class BooleanConverter : TypeConverter
	{
		/// <summary>Gets a value indicating whether this converter can convert an object in the given source type to a Boolean object using the specified context.</summary>
		/// <returns>true if this object can perform the conversion; otherwise, false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="sourceType">A <see cref="T:System.Type" /> that represents the type you wish to convert from. </param>
		// Token: 0x0600127A RID: 4730 RVA: 0x0004DD24 File Offset: 0x0004BF24
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		/// <summary>Converts the given value object to a Boolean object.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted <paramref name="value" />.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> that specifies the culture to which to convert.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not a valid value for the target type. </exception>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed. </exception>
		// Token: 0x0600127B RID: 4731 RVA: 0x0004DD44 File Offset: 0x0004BF44
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is string)
			{
				string text = ((string)value).Trim();
				try
				{
					return bool.Parse(text);
				}
				catch (FormatException ex)
				{
					throw new FormatException(global::SR.GetString("{0} is not a valid value for {1}.", new object[]
					{
						(string)value,
						"Boolean"
					}), ex);
				}
			}
			return base.ConvertFrom(context, culture, value);
		}

		/// <summary>Gets a collection of standard values for the Boolean data type.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection" /> that holds a standard set of valid values.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		// Token: 0x0600127C RID: 4732 RVA: 0x0004DDB8 File Offset: 0x0004BFB8
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			if (BooleanConverter.values == null)
			{
				BooleanConverter.values = new TypeConverter.StandardValuesCollection(new object[] { true, false });
			}
			return BooleanConverter.values;
		}

		/// <summary>Gets a value indicating whether the list of standard values returned from the <see cref="M:System.ComponentModel.BooleanConverter.GetStandardValues(System.ComponentModel.ITypeDescriptorContext)" /> method is an exclusive list.</summary>
		/// <returns>true because the <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection" /> returned from <see cref="M:System.ComponentModel.BooleanConverter.GetStandardValues(System.ComponentModel.ITypeDescriptorContext)" /> is an exhaustive list of possible values. This method never returns false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		// Token: 0x0600127D RID: 4733 RVA: 0x000027E2 File Offset: 0x000009E2
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return true;
		}

		/// <summary>Gets a value indicating whether this object supports a standard set of values that can be picked from a list.</summary>
		/// <returns>true because <see cref="M:System.ComponentModel.BooleanConverter.GetStandardValues(System.ComponentModel.ITypeDescriptorContext)" /> can be called to find a common set of values the object supports. This method never returns false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		// Token: 0x0600127E RID: 4734 RVA: 0x000027E2 File Offset: 0x000009E2
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x04001262 RID: 4706
		private static volatile TypeConverter.StandardValuesCollection values;
	}
}
