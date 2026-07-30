using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;

namespace System.Windows.Forms
{
	/// <summary>Provides a type converter to convert <see cref="T:System.Windows.Forms.Binding" /> objects to and from various other representations.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000211 RID: 529
	public class ListBindingConverter : TypeConverter
	{
		/// <returns>true if this converter can perform the conversion; otherwise, false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="destinationType">A <see cref="T:System.Type" /> that represents the type you want to convert to. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002094 RID: 8340 RVA: 0x00079A54 File Offset: 0x00077C54
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(string) || base.CanConvertTo(context, destinationType);
		}

		/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" />. If null is passed, the current culture is assumed. </param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert. </param>
		/// <param name="destinationType">The <see cref="T:System.Type" /> to convert the <paramref name="value" /> parameter to. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002095 RID: 8341 RVA: 0x00079A70 File Offset: 0x00077C70
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			return base.ConvertTo(context, culture, value, destinationType);
		}

		/// <returns>An <see cref="T:System.Object" /> representing the given <see cref="T:System.Collections.IDictionary" />, or null if the object cannot be created. This method always returns null.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="propertyValues">An <see cref="T:System.Collections.IDictionary" /> of new property values. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002096 RID: 8342 RVA: 0x00079A80 File Offset: 0x00077C80
		public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
		{
			return new Binding((string)propertyValues["PropertyName"], propertyValues["DataSource"], (string)propertyValues["DataMember"]);
		}

		/// <returns>true if changing a property on this object requires a call to <see cref="M:System.ComponentModel.TypeConverter.CreateInstance(System.Collections.IDictionary)" /> to create a new value; otherwise, false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002097 RID: 8343 RVA: 0x00079AC0 File Offset: 0x00077CC0
		public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
		{
			return true;
		}
	}
}
