using System;
using System.Collections;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Provides a unified way of converting types of values to other types, as well as for accessing standard values and subproperties.</summary>
	// Token: 0x020002DA RID: 730
	[ComVisible(true)]
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class TypeConverter
	{
		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x0600172C RID: 5932 RVA: 0x0005BFDB File Offset: 0x0005A1DB
		private static bool UseCompatibleTypeConversion
		{
			get
			{
				return TypeConverter.useCompatibleTypeConversion;
			}
		}

		/// <summary>Returns whether this converter can convert an object of the given type to the type of this converter.</summary>
		/// <returns>true if this converter can perform the conversion; otherwise, false.</returns>
		/// <param name="sourceType">A <see cref="T:System.Type" /> that represents the type you want to convert from. </param>
		// Token: 0x0600172D RID: 5933 RVA: 0x0005BFE4 File Offset: 0x0005A1E4
		public bool CanConvertFrom(Type sourceType)
		{
			return this.CanConvertFrom(null, sourceType);
		}

		/// <summary>Returns whether this converter can convert an object of the given type to the type of this converter, using the specified context.</summary>
		/// <returns>true if this converter can perform the conversion; otherwise, false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="sourceType">A <see cref="T:System.Type" /> that represents the type you want to convert from. </param>
		// Token: 0x0600172E RID: 5934 RVA: 0x0005BFEE File Offset: 0x0005A1EE
		public virtual bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(InstanceDescriptor);
		}

		/// <summary>Returns whether this converter can convert the object to the specified type.</summary>
		/// <returns>true if this converter can perform the conversion; otherwise, false.</returns>
		/// <param name="destinationType">A <see cref="T:System.Type" /> that represents the type you want to convert to. </param>
		// Token: 0x0600172F RID: 5935 RVA: 0x0005C005 File Offset: 0x0005A205
		public bool CanConvertTo(Type destinationType)
		{
			return this.CanConvertTo(null, destinationType);
		}

		/// <summary>Returns whether this converter can convert the object to the specified type, using the specified context.</summary>
		/// <returns>true if this converter can perform the conversion; otherwise, false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="destinationType">A <see cref="T:System.Type" /> that represents the type you want to convert to. </param>
		// Token: 0x06001730 RID: 5936 RVA: 0x0005C00F File Offset: 0x0005A20F
		public virtual bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(string);
		}

		/// <summary>Converts the given value to the type of this converter.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to convert. </param>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed. </exception>
		// Token: 0x06001731 RID: 5937 RVA: 0x0005C021 File Offset: 0x0005A221
		public object ConvertFrom(object value)
		{
			return this.ConvertFrom(null, CultureInfo.CurrentCulture, value);
		}

		/// <summary>Converts the given object to the type of this converter, using the specified context and culture information.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="culture">The <see cref="T:System.Globalization.CultureInfo" /> to use as the current culture. </param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert. </param>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed. </exception>
		// Token: 0x06001732 RID: 5938 RVA: 0x0005C030 File Offset: 0x0005A230
		public virtual object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			InstanceDescriptor instanceDescriptor = value as InstanceDescriptor;
			if (instanceDescriptor != null)
			{
				return instanceDescriptor.Invoke();
			}
			throw this.GetConvertFromException(value);
		}

		/// <summary>Converts the given string to the type of this converter, using the invariant culture.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted text.</returns>
		/// <param name="text">The <see cref="T:System.String" /> to convert. </param>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed. </exception>
		// Token: 0x06001733 RID: 5939 RVA: 0x0005C055 File Offset: 0x0005A255
		public object ConvertFromInvariantString(string text)
		{
			return this.ConvertFromString(null, CultureInfo.InvariantCulture, text);
		}

		/// <summary>Converts the given string to the type of this converter, using the invariant culture and the specified context.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted text.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="text">The <see cref="T:System.String" /> to convert. </param>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed. </exception>
		// Token: 0x06001734 RID: 5940 RVA: 0x0005C064 File Offset: 0x0005A264
		public object ConvertFromInvariantString(ITypeDescriptorContext context, string text)
		{
			return this.ConvertFromString(context, CultureInfo.InvariantCulture, text);
		}

		/// <summary>Converts the specified text to an object.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted text.</returns>
		/// <param name="text">The text representation of the object to convert. </param>
		/// <exception cref="T:System.NotSupportedException">The string cannot be converted into the appropriate object. </exception>
		// Token: 0x06001735 RID: 5941 RVA: 0x0005C073 File Offset: 0x0005A273
		public object ConvertFromString(string text)
		{
			return this.ConvertFrom(null, null, text);
		}

		/// <summary>Converts the given text to an object, using the specified context.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted text.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="text">The <see cref="T:System.String" /> to convert. </param>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed. </exception>
		// Token: 0x06001736 RID: 5942 RVA: 0x0005C07E File Offset: 0x0005A27E
		public object ConvertFromString(ITypeDescriptorContext context, string text)
		{
			return this.ConvertFrom(context, CultureInfo.CurrentCulture, text);
		}

		/// <summary>Converts the given text to an object, using the specified context and culture information.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted text.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" />. If null is passed, the current culture is assumed. </param>
		/// <param name="text">The <see cref="T:System.String" /> to convert. </param>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed. </exception>
		// Token: 0x06001737 RID: 5943 RVA: 0x0005C08D File Offset: 0x0005A28D
		public object ConvertFromString(ITypeDescriptorContext context, CultureInfo culture, string text)
		{
			return this.ConvertFrom(context, culture, text);
		}

		/// <summary>Converts the given value object to the specified type, using the arguments.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to convert. </param>
		/// <param name="destinationType">The <see cref="T:System.Type" /> to convert the <paramref name="value" /> parameter to. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="destinationType" /> parameter is null. </exception>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed. </exception>
		// Token: 0x06001738 RID: 5944 RVA: 0x0005C098 File Offset: 0x0005A298
		public object ConvertTo(object value, Type destinationType)
		{
			return this.ConvertTo(null, null, value, destinationType);
		}

		/// <summary>Converts the given value object to the specified type, using the specified context and culture information.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" />. If null is passed, the current culture is assumed. </param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert. </param>
		/// <param name="destinationType">The <see cref="T:System.Type" /> to convert the <paramref name="value" /> parameter to. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="destinationType" /> parameter is null. </exception>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed. </exception>
		// Token: 0x06001739 RID: 5945 RVA: 0x0005C0A4 File Offset: 0x0005A2A4
		public virtual object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (!(destinationType == typeof(string)))
			{
				throw this.GetConvertToException(value, destinationType);
			}
			if (value == null)
			{
				return string.Empty;
			}
			if (culture != null && culture != CultureInfo.CurrentCulture)
			{
				IFormattable formattable = value as IFormattable;
				if (formattable != null)
				{
					return formattable.ToString(null, culture);
				}
			}
			return value.ToString();
		}

		/// <summary>Converts the specified value to a culture-invariant string representation.</summary>
		/// <returns>A <see cref="T:System.String" /> that represents the converted value.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to convert. </param>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed. </exception>
		// Token: 0x0600173A RID: 5946 RVA: 0x0005C110 File Offset: 0x0005A310
		public string ConvertToInvariantString(object value)
		{
			return this.ConvertToString(null, CultureInfo.InvariantCulture, value);
		}

		/// <summary>Converts the specified value to a culture-invariant string representation, using the specified context.</summary>
		/// <returns>A <see cref="T:System.String" /> that represents the converted value.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert. </param>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed. </exception>
		// Token: 0x0600173B RID: 5947 RVA: 0x0005C11F File Offset: 0x0005A31F
		public string ConvertToInvariantString(ITypeDescriptorContext context, object value)
		{
			return this.ConvertToString(context, CultureInfo.InvariantCulture, value);
		}

		/// <summary>Converts the specified value to a string representation.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to convert. </param>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed. </exception>
		// Token: 0x0600173C RID: 5948 RVA: 0x0005C12E File Offset: 0x0005A32E
		public string ConvertToString(object value)
		{
			return (string)this.ConvertTo(null, CultureInfo.CurrentCulture, value, typeof(string));
		}

		/// <summary>Converts the given value to a string representation, using the given context.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert. </param>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed. </exception>
		// Token: 0x0600173D RID: 5949 RVA: 0x0005C14C File Offset: 0x0005A34C
		public string ConvertToString(ITypeDescriptorContext context, object value)
		{
			return (string)this.ConvertTo(context, CultureInfo.CurrentCulture, value, typeof(string));
		}

		/// <summary>Converts the given value to a string representation, using the specified context and culture information.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" />. If null is passed, the current culture is assumed. </param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert. </param>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed. </exception>
		// Token: 0x0600173E RID: 5950 RVA: 0x0005C16A File Offset: 0x0005A36A
		public string ConvertToString(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			return (string)this.ConvertTo(context, culture, value, typeof(string));
		}

		/// <summary>Re-creates an <see cref="T:System.Object" /> given a set of property values for the object.</summary>
		/// <returns>An <see cref="T:System.Object" /> representing the given <see cref="T:System.Collections.IDictionary" />, or null if the object cannot be created. This method always returns null.</returns>
		/// <param name="propertyValues">An <see cref="T:System.Collections.IDictionary" /> that represents a dictionary of new property values. </param>
		// Token: 0x0600173F RID: 5951 RVA: 0x0005C184 File Offset: 0x0005A384
		public object CreateInstance(IDictionary propertyValues)
		{
			return this.CreateInstance(null, propertyValues);
		}

		/// <summary>Creates an instance of the type that this <see cref="T:System.ComponentModel.TypeConverter" /> is associated with, using the specified context, given a set of property values for the object.</summary>
		/// <returns>An <see cref="T:System.Object" /> representing the given <see cref="T:System.Collections.IDictionary" />, or null if the object cannot be created. This method always returns null.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="propertyValues">An <see cref="T:System.Collections.IDictionary" /> of new property values. </param>
		// Token: 0x06001740 RID: 5952 RVA: 0x00009E57 File Offset: 0x00008057
		public virtual object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
		{
			return null;
		}

		/// <summary>Returns an exception to throw when a conversion cannot be performed.</summary>
		/// <returns>An <see cref="T:System.Exception" /> that represents the exception to throw when a conversion cannot be performed.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to convert, or null if the object is not available. </param>
		/// <exception cref="T:System.NotSupportedException">Automatically thrown by this method. </exception>
		// Token: 0x06001741 RID: 5953 RVA: 0x0005C190 File Offset: 0x0005A390
		protected Exception GetConvertFromException(object value)
		{
			string text;
			if (value == null)
			{
				text = global::SR.GetString("(null)");
			}
			else
			{
				text = value.GetType().FullName;
			}
			throw new NotSupportedException(global::SR.GetString("{0} cannot convert from {1}.", new object[]
			{
				base.GetType().Name,
				text
			}));
		}

		/// <summary>Returns an exception to throw when a conversion cannot be performed.</summary>
		/// <returns>An <see cref="T:System.Exception" /> that represents the exception to throw when a conversion cannot be performed.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to convert, or null if the object is not available. </param>
		/// <param name="destinationType">A <see cref="T:System.Type" /> that represents the type the conversion was trying to convert to. </param>
		/// <exception cref="T:System.NotSupportedException">Automatically thrown by this method. </exception>
		// Token: 0x06001742 RID: 5954 RVA: 0x0005C1E0 File Offset: 0x0005A3E0
		protected Exception GetConvertToException(object value, Type destinationType)
		{
			string text;
			if (value == null)
			{
				text = global::SR.GetString("(null)");
			}
			else
			{
				text = value.GetType().FullName;
			}
			throw new NotSupportedException(global::SR.GetString("'{0}' is unable to convert '{1}' to '{2}'.", new object[]
			{
				base.GetType().Name,
				text,
				destinationType.FullName
			}));
		}

		/// <summary>Returns whether changing a value on this object requires a call to the <see cref="M:System.ComponentModel.TypeConverter.CreateInstance(System.Collections.IDictionary)" /> method to create a new value.</summary>
		/// <returns>true if changing a property on this object requires a call to <see cref="M:System.ComponentModel.TypeConverter.CreateInstance(System.Collections.IDictionary)" /> to create a new value; otherwise, false.</returns>
		// Token: 0x06001743 RID: 5955 RVA: 0x0005C239 File Offset: 0x0005A439
		public bool GetCreateInstanceSupported()
		{
			return this.GetCreateInstanceSupported(null);
		}

		/// <summary>Returns whether changing a value on this object requires a call to <see cref="M:System.ComponentModel.TypeConverter.CreateInstance(System.Collections.IDictionary)" /> to create a new value, using the specified context.</summary>
		/// <returns>true if changing a property on this object requires a call to <see cref="M:System.ComponentModel.TypeConverter.CreateInstance(System.Collections.IDictionary)" /> to create a new value; otherwise, false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		// Token: 0x06001744 RID: 5956 RVA: 0x00004240 File Offset: 0x00002440
		public virtual bool GetCreateInstanceSupported(ITypeDescriptorContext context)
		{
			return false;
		}

		/// <summary>Returns a collection of properties for the type of array specified by the value parameter.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> with the properties that are exposed for this data type, or null if there are no properties.</returns>
		/// <param name="value">An <see cref="T:System.Object" /> that specifies the type of array for which to get properties. </param>
		// Token: 0x06001745 RID: 5957 RVA: 0x0005C242 File Offset: 0x0005A442
		public PropertyDescriptorCollection GetProperties(object value)
		{
			return this.GetProperties(null, value);
		}

		/// <summary>Returns a collection of properties for the type of array specified by the value parameter, using the specified context.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> with the properties that are exposed for this data type, or null if there are no properties.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="value">An <see cref="T:System.Object" /> that specifies the type of array for which to get properties. </param>
		// Token: 0x06001746 RID: 5958 RVA: 0x0005C24C File Offset: 0x0005A44C
		public PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value)
		{
			return this.GetProperties(context, value, new Attribute[] { BrowsableAttribute.Yes });
		}

		/// <summary>Returns a collection of properties for the type of array specified by the value parameter, using the specified context and attributes.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> with the properties that are exposed for this data type, or null if there are no properties.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="value">An <see cref="T:System.Object" /> that specifies the type of array for which to get properties. </param>
		/// <param name="attributes">An array of type <see cref="T:System.Attribute" /> that is used as a filter. </param>
		// Token: 0x06001747 RID: 5959 RVA: 0x00009E57 File Offset: 0x00008057
		public virtual PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			return null;
		}

		/// <summary>Returns whether this object supports properties.</summary>
		/// <returns>true if <see cref="M:System.ComponentModel.TypeConverter.GetProperties(System.Object)" /> should be called to find the properties of this object; otherwise, false.</returns>
		// Token: 0x06001748 RID: 5960 RVA: 0x0005C264 File Offset: 0x0005A464
		public bool GetPropertiesSupported()
		{
			return this.GetPropertiesSupported(null);
		}

		/// <summary>Returns whether this object supports properties, using the specified context.</summary>
		/// <returns>true if <see cref="M:System.ComponentModel.TypeConverter.GetProperties(System.Object)" /> should be called to find the properties of this object; otherwise, false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		// Token: 0x06001749 RID: 5961 RVA: 0x00004240 File Offset: 0x00002440
		public virtual bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return false;
		}

		/// <summary>Returns a collection of standard values from the default context for the data type this type converter is designed for.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection" /> containing a standard set of valid values, or null if the data type does not support a standard set of values.</returns>
		// Token: 0x0600174A RID: 5962 RVA: 0x0005C26D File Offset: 0x0005A46D
		public ICollection GetStandardValues()
		{
			return this.GetStandardValues(null);
		}

		/// <summary>Returns a collection of standard values for the data type this type converter is designed for when provided with a format context.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection" /> that holds a standard set of valid values, or null if the data type does not support a standard set of values.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context that can be used to extract additional information about the environment from which this converter is invoked. This parameter or properties of this parameter can be null. </param>
		// Token: 0x0600174B RID: 5963 RVA: 0x00009E57 File Offset: 0x00008057
		public virtual TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			return null;
		}

		/// <summary>Returns whether the collection of standard values returned from <see cref="M:System.ComponentModel.TypeConverter.GetStandardValues" /> is an exclusive list.</summary>
		/// <returns>true if the <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection" /> returned from <see cref="M:System.ComponentModel.TypeConverter.GetStandardValues" /> is an exhaustive list of possible values; false if other values are possible.</returns>
		// Token: 0x0600174C RID: 5964 RVA: 0x0005C276 File Offset: 0x0005A476
		public bool GetStandardValuesExclusive()
		{
			return this.GetStandardValuesExclusive(null);
		}

		/// <summary>Returns whether the collection of standard values returned from <see cref="M:System.ComponentModel.TypeConverter.GetStandardValues" /> is an exclusive list of possible values, using the specified context.</summary>
		/// <returns>true if the <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection" /> returned from <see cref="M:System.ComponentModel.TypeConverter.GetStandardValues" /> is an exhaustive list of possible values; false if other values are possible.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		// Token: 0x0600174D RID: 5965 RVA: 0x00004240 File Offset: 0x00002440
		public virtual bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return false;
		}

		/// <summary>Returns whether this object supports a standard set of values that can be picked from a list.</summary>
		/// <returns>true if <see cref="M:System.ComponentModel.TypeConverter.GetStandardValues" /> should be called to find a common set of values the object supports; otherwise, false.</returns>
		// Token: 0x0600174E RID: 5966 RVA: 0x0005C27F File Offset: 0x0005A47F
		public bool GetStandardValuesSupported()
		{
			return this.GetStandardValuesSupported(null);
		}

		/// <summary>Returns whether this object supports a standard set of values that can be picked from a list, using the specified context.</summary>
		/// <returns>true if <see cref="M:System.ComponentModel.TypeConverter.GetStandardValues" /> should be called to find a common set of values the object supports; otherwise, false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		// Token: 0x0600174F RID: 5967 RVA: 0x00004240 File Offset: 0x00002440
		public virtual bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return false;
		}

		/// <summary>Returns whether the given value object is valid for this type.</summary>
		/// <returns>true if the specified value is valid for this object; otherwise, false.</returns>
		/// <param name="value">The object to test for validity. </param>
		// Token: 0x06001750 RID: 5968 RVA: 0x0005C288 File Offset: 0x0005A488
		public bool IsValid(object value)
		{
			return this.IsValid(null, value);
		}

		/// <summary>Returns whether the given value object is valid for this type and for the specified context.</summary>
		/// <returns>true if the specified value is valid for this object; otherwise, false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="value">The <see cref="T:System.Object" /> to test for validity. </param>
		// Token: 0x06001751 RID: 5969 RVA: 0x0005C294 File Offset: 0x0005A494
		public virtual bool IsValid(ITypeDescriptorContext context, object value)
		{
			if (TypeConverter.UseCompatibleTypeConversion)
			{
				return true;
			}
			bool flag = true;
			try
			{
				if (value == null || this.CanConvertFrom(context, value.GetType()))
				{
					this.ConvertFrom(context, CultureInfo.InvariantCulture, value);
				}
				else
				{
					flag = false;
				}
			}
			catch
			{
				flag = false;
			}
			return flag;
		}

		/// <summary>Sorts a collection of properties.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that contains the sorted properties.</returns>
		/// <param name="props">A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that has the properties to sort. </param>
		/// <param name="names">An array of names in the order you want the properties to appear in the collection. </param>
		// Token: 0x06001752 RID: 5970 RVA: 0x0005C2E8 File Offset: 0x0005A4E8
		protected PropertyDescriptorCollection SortProperties(PropertyDescriptorCollection props, string[] names)
		{
			props.Sort(names);
			return props;
		}

		// Token: 0x040013F1 RID: 5105
		private const string s_UseCompatibleTypeConverterBehavior = "UseCompatibleTypeConverterBehavior";

		// Token: 0x040013F2 RID: 5106
		private static volatile bool useCompatibleTypeConversion;

		/// <summary>Represents an abstract class that provides properties for objects that do not have properties.</summary>
		// Token: 0x020002DB RID: 731
		protected abstract class SimplePropertyDescriptor : PropertyDescriptor
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.TypeConverter.SimplePropertyDescriptor" /> class.</summary>
			/// <param name="componentType">A <see cref="T:System.Type" /> that represents the type of component to which this property descriptor binds. </param>
			/// <param name="name">The name of the property. </param>
			/// <param name="propertyType">A <see cref="T:System.Type" /> that represents the data type for this property. </param>
			// Token: 0x06001755 RID: 5973 RVA: 0x0005C2F3 File Offset: 0x0005A4F3
			protected SimplePropertyDescriptor(Type componentType, string name, Type propertyType)
				: this(componentType, name, propertyType, new Attribute[0])
			{
			}

			/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.TypeConverter.SimplePropertyDescriptor" /> class.</summary>
			/// <param name="componentType">A <see cref="T:System.Type" /> that represents the type of component to which this property descriptor binds. </param>
			/// <param name="name">The name of the property. </param>
			/// <param name="propertyType">A <see cref="T:System.Type" /> that represents the data type for this property. </param>
			/// <param name="attributes">An <see cref="T:System.Attribute" /> array with the attributes to associate with the property. </param>
			// Token: 0x06001756 RID: 5974 RVA: 0x0005C304 File Offset: 0x0005A504
			protected SimplePropertyDescriptor(Type componentType, string name, Type propertyType, Attribute[] attributes)
				: base(name, attributes)
			{
				this.componentType = componentType;
				this.propertyType = propertyType;
			}

			/// <summary>Gets the type of component to which this property description binds.</summary>
			/// <returns>A <see cref="T:System.Type" /> that represents the type of component to which this property binds.</returns>
			// Token: 0x170004E1 RID: 1249
			// (get) Token: 0x06001757 RID: 5975 RVA: 0x0005C31D File Offset: 0x0005A51D
			public override Type ComponentType
			{
				get
				{
					return this.componentType;
				}
			}

			/// <summary>Gets a value indicating whether this property is read-only.</summary>
			/// <returns>true if the property is read-only; false if the property is read/write.</returns>
			// Token: 0x170004E2 RID: 1250
			// (get) Token: 0x06001758 RID: 5976 RVA: 0x0005C325 File Offset: 0x0005A525
			public override bool IsReadOnly
			{
				get
				{
					return this.Attributes.Contains(ReadOnlyAttribute.Yes);
				}
			}

			/// <summary>Gets the type of the property.</summary>
			/// <returns>A <see cref="T:System.Type" /> that represents the type of the property.</returns>
			// Token: 0x170004E3 RID: 1251
			// (get) Token: 0x06001759 RID: 5977 RVA: 0x0005C337 File Offset: 0x0005A537
			public override Type PropertyType
			{
				get
				{
					return this.propertyType;
				}
			}

			/// <summary>Returns whether resetting the component changes the value of the component.</summary>
			/// <returns>true if resetting the component changes the value of the component; otherwise, false.</returns>
			/// <param name="component">The component to test for reset capability. </param>
			// Token: 0x0600175A RID: 5978 RVA: 0x0005C340 File Offset: 0x0005A540
			public override bool CanResetValue(object component)
			{
				DefaultValueAttribute defaultValueAttribute = (DefaultValueAttribute)this.Attributes[typeof(DefaultValueAttribute)];
				return defaultValueAttribute != null && defaultValueAttribute.Value.Equals(this.GetValue(component));
			}

			/// <summary>Resets the value for this property of the component.</summary>
			/// <param name="component">The component with the property value to be reset. </param>
			// Token: 0x0600175B RID: 5979 RVA: 0x0005C380 File Offset: 0x0005A580
			public override void ResetValue(object component)
			{
				DefaultValueAttribute defaultValueAttribute = (DefaultValueAttribute)this.Attributes[typeof(DefaultValueAttribute)];
				if (defaultValueAttribute != null)
				{
					this.SetValue(component, defaultValueAttribute.Value);
				}
			}

			/// <summary>Returns whether the value of this property can persist.</summary>
			/// <returns>true if the value of the property can persist; otherwise, false.</returns>
			/// <param name="component">The component with the property that is to be examined for persistence. </param>
			// Token: 0x0600175C RID: 5980 RVA: 0x00004240 File Offset: 0x00002440
			public override bool ShouldSerializeValue(object component)
			{
				return false;
			}

			// Token: 0x040013F3 RID: 5107
			private Type componentType;

			// Token: 0x040013F4 RID: 5108
			private Type propertyType;
		}

		/// <summary>Represents a collection of values.</summary>
		// Token: 0x020002DC RID: 732
		public class StandardValuesCollection : ICollection, IEnumerable
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection" /> class.</summary>
			/// <param name="values">An <see cref="T:System.Collections.ICollection" /> that represents the objects to put into the collection. </param>
			// Token: 0x0600175D RID: 5981 RVA: 0x0005C3B8 File Offset: 0x0005A5B8
			public StandardValuesCollection(ICollection values)
			{
				if (values == null)
				{
					values = new object[0];
				}
				Array array = values as Array;
				if (array != null)
				{
					this.valueArray = array;
				}
				this.values = values;
			}

			/// <summary>Gets the number of objects in the collection.</summary>
			/// <returns>The number of objects in the collection.</returns>
			// Token: 0x170004E4 RID: 1252
			// (get) Token: 0x0600175E RID: 5982 RVA: 0x0005C3EE File Offset: 0x0005A5EE
			public int Count
			{
				get
				{
					if (this.valueArray != null)
					{
						return this.valueArray.Length;
					}
					return this.values.Count;
				}
			}

			/// <summary>Gets the object at the specified index number.</summary>
			/// <returns>The <see cref="T:System.Object" /> with the specified index.</returns>
			/// <param name="index">The zero-based index of the <see cref="T:System.Object" /> to get from the collection. </param>
			// Token: 0x170004E5 RID: 1253
			public object this[int index]
			{
				get
				{
					if (this.valueArray != null)
					{
						return this.valueArray.GetValue(index);
					}
					IList list = this.values as IList;
					if (list != null)
					{
						return list[index];
					}
					this.valueArray = new object[this.values.Count];
					this.values.CopyTo(this.valueArray, 0);
					return this.valueArray.GetValue(index);
				}
			}

			/// <summary>Copies the contents of this collection to an array.</summary>
			/// <param name="array">An <see cref="T:System.Array" /> that represents the array to copy to. </param>
			/// <param name="index">The index to start from. </param>
			// Token: 0x06001760 RID: 5984 RVA: 0x0005C47D File Offset: 0x0005A67D
			public void CopyTo(Array array, int index)
			{
				this.values.CopyTo(array, index);
			}

			/// <summary>Returns an enumerator for this collection.</summary>
			/// <returns>An enumerator of type <see cref="T:System.Collections.IEnumerator" />.</returns>
			// Token: 0x06001761 RID: 5985 RVA: 0x0005C48C File Offset: 0x0005A68C
			public IEnumerator GetEnumerator()
			{
				return this.values.GetEnumerator();
			}

			/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.Count" />.</summary>
			/// <returns>The number of elements contained in the <see cref="T:System.Collections.ICollection" />.</returns>
			// Token: 0x170004E6 RID: 1254
			// (get) Token: 0x06001762 RID: 5986 RVA: 0x0005C499 File Offset: 0x0005A699
			int ICollection.Count
			{
				get
				{
					return this.Count;
				}
			}

			/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.IsSynchronized" />.</summary>
			/// <returns>false in all cases.</returns>
			// Token: 0x170004E7 RID: 1255
			// (get) Token: 0x06001763 RID: 5987 RVA: 0x00004240 File Offset: 0x00002440
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.SyncRoot" />.</summary>
			/// <returns>null in all cases.</returns>
			// Token: 0x170004E8 RID: 1256
			// (get) Token: 0x06001764 RID: 5988 RVA: 0x00009E57 File Offset: 0x00008057
			object ICollection.SyncRoot
			{
				get
				{
					return null;
				}
			}

			/// <summary>Copies the contents of this collection to an array.</summary>
			/// <param name="array">The array to copy to. </param>
			/// <param name="index">The index in the array where copying should begin. </param>
			// Token: 0x06001765 RID: 5989 RVA: 0x0005C4A1 File Offset: 0x0005A6A1
			void ICollection.CopyTo(Array array, int index)
			{
				this.CopyTo(array, index);
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IEnumerable.GetEnumerator" />.</summary>
			/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
			// Token: 0x06001766 RID: 5990 RVA: 0x0005C4AB File Offset: 0x0005A6AB
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x040013F5 RID: 5109
			private ICollection values;

			// Token: 0x040013F6 RID: 5110
			private Array valueArray;
		}
	}
}
