using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Security.Permissions;

namespace System.Web.UI
{
	/// <summary>Provides a type converter to convert 32-bit signed integer objects to and from data source control cache duration representations.</summary>
	// Token: 0x020001C3 RID: 451
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class DataSourceCacheDurationConverter : Int32Converter
	{
		/// <summary>Determines whether the <see cref="T:System.Web.UI.DataSourceCacheDurationConverter" /> can convert an object in the given source type to an <see cref="T:System.Int32" /> object.</summary>
		/// <returns>true if this converter can perform the operation; otherwise, false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="sourceType">A <see cref="T:System.Type" /> that represents a type that the converter converts.</param>
		// Token: 0x0600124E RID: 4686 RVA: 0x0003297D File Offset: 0x00030B7D
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		/// <summary>Returns a value indicating whether the <see cref="T:System.Web.UI.DataSourceCacheDurationConverter" /> instance can convert an object to the given destination type.</summary>
		/// <returns>true if this converter can perform the operation; otherwise, false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="destinationType">A <see cref="T:System.Type" />  that represents the type to which you want to convert.</param>
		// Token: 0x0600124F RID: 4687 RVA: 0x0003299B File Offset: 0x00030B9B
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(string) || base.CanConvertTo(context, destinationType);
		}

		/// <summary>Converts the specified object to an <see cref="T:System.Int32" /> object.</summary>
		/// <returns>An object that represents the converted value. If null is passed in the value parameter, null is returned.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> that specifies the culture in which to represent the number.</param>
		/// <param name="value">The object to convert.</param>
		// Token: 0x06001250 RID: 4688 RVA: 0x000329BC File Offset: 0x00030BBC
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value == null)
			{
				return null;
			}
			string text = value as string;
			if (text != null && (text.Length == 0 || string.Compare("infinite", text, StringComparison.OrdinalIgnoreCase) == 0))
			{
				return 0;
			}
			return base.ConvertFrom(context, culture, value);
		}

		/// <summary>Converts the specified object to another type.</summary>
		/// <returns>An object that represents the converted value.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> that specifies the culture in which to represent the number.</param>
		/// <param name="value">The object to convert.</param>
		/// <param name="destinationType">The type to convert the object to.</param>
		// Token: 0x06001251 RID: 4689 RVA: 0x000329FE File Offset: 0x00030BFE
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(string))
			{
				if (value == null)
				{
					return string.Empty;
				}
				if (value is int && (int)value == 0)
				{
					return "Infinite";
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		/// <summary>Returns a collection of standard values for the data type the <see cref="T:System.Web.UI.DataSourceCacheDurationConverter" /> instance is designed for.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection" /> that contains 0 for cache duration, which represents "Infinite".</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" />  that provides a format context.</param>
		// Token: 0x06001252 RID: 4690 RVA: 0x00032A3D File Offset: 0x00030C3D
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			return new TypeConverter.StandardValuesCollection(DataSourceCacheDurationConverter.standardValues);
		}

		/// <summary>Specifies whether the collection of standard values returned from the <see cref="Overload:System.Web.UI.DataSourceCacheDurationConverter.GetStandardValues" /> method is an exclusive list, using the specified context.</summary>
		/// <returns>false in all cases.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		// Token: 0x06001253 RID: 4691 RVA: 0x00008A69 File Offset: 0x00006C69
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return false;
		}

		/// <summary>Specifies whether the <see cref="T:System.Web.UI.DataSourceCacheDurationConverter" /> object supports a standard set of values that can be picked from a list, using the specified context.</summary>
		/// <returns>true in all cases.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" />  that provides a format context.</param>
		// Token: 0x06001254 RID: 4692 RVA: 0x00008B66 File Offset: 0x00006D66
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x0400141E RID: 5150
		private static readonly List<int> standardValues = new List<int> { 0 };
	}
}
