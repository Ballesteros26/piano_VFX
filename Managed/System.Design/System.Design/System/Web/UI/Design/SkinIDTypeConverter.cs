using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Web.UI.Design
{
	/// <summary>Provides a list of valid skin IDs for a control at design time, based on the currently applicable theme.</summary>
	// Token: 0x020000A0 RID: 160
	public class SkinIDTypeConverter : TypeConverter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.SkinIDTypeConverter" /> class.</summary>
		// Token: 0x060004B8 RID: 1208 RVA: 0x000092CE File Offset: 0x000074CE
		[MonoTODO]
		public SkinIDTypeConverter()
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns a value indicating whether this converter can convert a <see cref="P:System.Web.UI.Control.SkinID" /> object to a string using the provided format context and type.</summary>
		/// <returns>true, if the conversion can be made; otherwise, false.</returns>
		/// <param name="context">An T:System.ComponentModel.ITypeDescriptorContext that provides a format context for the control being designed.</param>
		/// <param name="sourceType">A <see cref="T:System.Type" />.</param>
		// Token: 0x060004B9 RID: 1209 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns a value indicating whether this converter can convert a <see cref="P:System.Web.UI.Control.SkinID" /> object to the specified type, using the specified context.</summary>
		/// <returns>true, if a conversion can be made; otherwise, false.</returns>
		/// <param name="context">An T:System.ComponentModel.ITypeDescriptorContext that provides a format context for the control being designed.</param>
		/// <param name="destType">A T:System.Type that represents the type to convert to.</param>
		// Token: 0x060004BA RID: 1210 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destType)
		{
			throw new NotImplementedException();
		}

		/// <summary>Converts the given string to a <see cref="P:System.Web.UI.Control.SkinID" /> object using the specified context and culture information.</summary>
		/// <returns>An object that can be cast as a <see cref="P:System.Web.UI.DataSourceControl.SkinID" /> object, if the conversion can be made; otherwise, null.</returns>
		/// <param name="context">An T:System.ComponentModel.ITypeDescriptorContext that provides a format context that represents the control being designed.</param>
		/// <param name="culture">A T:System.Globalization.CultureInfo. If null is passed, the current culture is assumed.</param>
		/// <param name="value">The string to convert.</param>
		// Token: 0x060004BB RID: 1211 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			throw new NotImplementedException();
		}

		/// <summary>Converts the given <see cref="P:System.Web.UI.Control.SkinID" /> object to a string using the specified context and culture information.</summary>
		/// <returns>An object that represents the converted value.</returns>
		/// <param name="context">An T:System.ComponentModel.ITypeDescriptorContext that provides a format context that represents the control being designed.</param>
		/// <param name="culture">A T:System.Globalization.CultureInfo. If null, the current culture is assumed. </param>
		/// <param name="value">The <see cref="P:System.Web.UI.Control.SkinID" /> object to convert.</param>
		/// <param name="destinationType">The T:System.Type to convert <paramref name="value" /> to (must be a <see cref="T:System.String" />). </param>
		// Token: 0x060004BC RID: 1212 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns a set of <see cref="P:System.Web.UI.Control.SkinID" /> objects that can be applied to the control that is represented by the given format context.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection" /> that holds a set of <see cref="P:System.Web.UI.Control.SkinID" /> objects; otherwise, null, if the control does not support skins.</returns>
		/// <param name="context">An T:System.ComponentModel.ITypeDescriptorContext that provides a format context that represents the control being designed. <paramref name="context" /> or properties of <paramref name="context" /> can be null.</param>
		// Token: 0x060004BD RID: 1213 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns a value indicating whether the control that is represented by the given context supports a set of <see cref="P:System.Web.UI.Control.SkinID" /> objects that can be picked from a list.</summary>
		/// <returns>true, if <see cref="Overload:System.Web.UI.Design.SkinIDTypeConverter.GetStandardValues" /> should be called to find a common set of values the object supports; otherwise, false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> for the control being designed.</param>
		// Token: 0x060004BE RID: 1214 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			throw new NotImplementedException();
		}
	}
}
