using System;
using System.Collections;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Provides automatic conversion between a nullable type and its underlying primitive type.</summary>
	// Token: 0x020002B9 RID: 697
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class NullableConverter : TypeConverter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.NullableConverter" /> class.</summary>
		/// <param name="type">The specified nullable type.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="type" /> is not a nullable type.</exception>
		// Token: 0x060015E5 RID: 5605 RVA: 0x00056740 File Offset: 0x00054940
		public NullableConverter(Type type)
		{
			this.nullableType = type;
			this.simpleType = Nullable.GetUnderlyingType(type);
			if (this.simpleType == null)
			{
				throw new ArgumentException(global::SR.GetString("The specified type is not a nullable type."), "type");
			}
			this.simpleTypeConverter = TypeDescriptor.GetConverter(this.simpleType);
		}

		/// <summary>Returns whether this converter can convert an object of the given type to the type of this converter, using the specified context.</summary>
		/// <returns>true if this converter can perform the conversion; otherwise, false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" />  that provides a format context.</param>
		/// <param name="sourceType">A <see cref="T:System.Type" /> that represents the type you want to convert from.</param>
		// Token: 0x060015E6 RID: 5606 RVA: 0x0005679A File Offset: 0x0005499A
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			if (sourceType == this.simpleType)
			{
				return true;
			}
			if (this.simpleTypeConverter != null)
			{
				return this.simpleTypeConverter.CanConvertFrom(context, sourceType);
			}
			return base.CanConvertFrom(context, sourceType);
		}

		/// <summary>Converts the given object to the type of this converter, using the specified context and culture information.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="culture">The <see cref="T:System.Globalization.CultureInfo" /> to use as the current culture.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert.</param>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed. </exception>
		// Token: 0x060015E7 RID: 5607 RVA: 0x000567CC File Offset: 0x000549CC
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value == null || value.GetType() == this.simpleType)
			{
				return value;
			}
			if (value is string && string.IsNullOrEmpty(value as string))
			{
				return null;
			}
			if (this.simpleTypeConverter != null)
			{
				return this.simpleTypeConverter.ConvertFrom(context, culture, value);
			}
			return base.ConvertFrom(context, culture, value);
		}

		/// <summary>Returns whether this converter can convert the object to the specified type, using the specified context.</summary>
		/// <returns>true if this converter can perform the conversion; otherwise, false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="destinationType">A <see cref="T:System.Type" /> that represents the type you want to convert to.</param>
		// Token: 0x060015E8 RID: 5608 RVA: 0x00056828 File Offset: 0x00054A28
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			if (destinationType == this.simpleType)
			{
				return true;
			}
			if (destinationType == typeof(InstanceDescriptor))
			{
				return true;
			}
			if (this.simpleTypeConverter != null)
			{
				return this.simpleTypeConverter.CanConvertTo(context, destinationType);
			}
			return base.CanConvertTo(context, destinationType);
		}

		/// <summary>Converts the given value object to the specified type, using the specified context and culture information.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="culture">The <see cref="T:System.Globalization.CultureInfo" /> to use as the current culture.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert.</param>
		/// <param name="destinationType">The <see cref="T:System.Type" /> to convert the value parameter to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="destinationType" /> is null.</exception>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed. </exception>
		// Token: 0x060015E9 RID: 5609 RVA: 0x00056878 File Offset: 0x00054A78
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == this.simpleType && this.nullableType.IsInstanceOfType(value))
			{
				return value;
			}
			if (destinationType == typeof(InstanceDescriptor))
			{
				return new InstanceDescriptor(this.nullableType.GetConstructor(new Type[] { this.simpleType }), new object[] { value }, true);
			}
			if (value == null)
			{
				if (destinationType == typeof(string))
				{
					return string.Empty;
				}
			}
			else if (this.simpleTypeConverter != null)
			{
				return this.simpleTypeConverter.ConvertTo(context, culture, value, destinationType);
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		/// <returns>An <see cref="T:System.Object" /> representing the given <see cref="T:System.Collections.IDictionary" />, or null if the object cannot be created. This method always returns null.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="propertyValues">An <see cref="T:System.Collections.IDictionary" /> of new property values. </param>
		// Token: 0x060015EA RID: 5610 RVA: 0x00056937 File Offset: 0x00054B37
		public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
		{
			if (this.simpleTypeConverter != null)
			{
				return this.simpleTypeConverter.CreateInstance(context, propertyValues);
			}
			return base.CreateInstance(context, propertyValues);
		}

		/// <returns>true if changing a property on this object requires a call to <see cref="M:System.ComponentModel.TypeConverter.CreateInstance(System.Collections.IDictionary)" /> to create a new value; otherwise, false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		// Token: 0x060015EB RID: 5611 RVA: 0x00056957 File Offset: 0x00054B57
		public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
		{
			if (this.simpleTypeConverter != null)
			{
				return this.simpleTypeConverter.GetCreateInstanceSupported(context);
			}
			return base.GetCreateInstanceSupported(context);
		}

		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> with the properties that are exposed for this data type, or null if there are no properties.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="value">An <see cref="T:System.Object" /> that specifies the type of array for which to get properties. </param>
		/// <param name="attributes">An array of type <see cref="T:System.Attribute" /> that is used as a filter. </param>
		// Token: 0x060015EC RID: 5612 RVA: 0x00056978 File Offset: 0x00054B78
		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			if (this.simpleTypeConverter != null)
			{
				return this.simpleTypeConverter.GetProperties(context, value, attributes);
			}
			return base.GetProperties(context, value, attributes);
		}

		/// <returns>true if <see cref="M:System.ComponentModel.TypeConverter.GetProperties(System.Object)" /> should be called to find the properties of this object; otherwise, false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		// Token: 0x060015ED RID: 5613 RVA: 0x000569A7 File Offset: 0x00054BA7
		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			if (this.simpleTypeConverter != null)
			{
				return this.simpleTypeConverter.GetPropertiesSupported(context);
			}
			return base.GetPropertiesSupported(context);
		}

		/// <returns>A <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection" /> that holds a standard set of valid values, or null if the data type does not support a standard set of values.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context that can be used to extract additional information about the environment from which this converter is invoked. This parameter or properties of this parameter can be null.</param>
		// Token: 0x060015EE RID: 5614 RVA: 0x000569C8 File Offset: 0x00054BC8
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			if (this.simpleTypeConverter != null)
			{
				TypeConverter.StandardValuesCollection standardValues = this.simpleTypeConverter.GetStandardValues(context);
				if (this.GetStandardValuesSupported(context) && standardValues != null)
				{
					object[] array = new object[standardValues.Count + 1];
					int num = 0;
					array[num++] = null;
					foreach (object obj in standardValues)
					{
						array[num++] = obj;
					}
					return new TypeConverter.StandardValuesCollection(array);
				}
			}
			return base.GetStandardValues(context);
		}

		/// <returns>true if the <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection" /> returned from <see cref="M:System.ComponentModel.TypeConverter.GetStandardValues" /> is an exhaustive list of possible values; false if other values are possible.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		// Token: 0x060015EF RID: 5615 RVA: 0x00056A64 File Offset: 0x00054C64
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			if (this.simpleTypeConverter != null)
			{
				return this.simpleTypeConverter.GetStandardValuesExclusive(context);
			}
			return base.GetStandardValuesExclusive(context);
		}

		/// <returns>true if <see cref="M:System.ComponentModel.TypeConverter.GetStandardValues" /> should be called to find a common set of values the object supports; otherwise, false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		// Token: 0x060015F0 RID: 5616 RVA: 0x00056A82 File Offset: 0x00054C82
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			if (this.simpleTypeConverter != null)
			{
				return this.simpleTypeConverter.GetStandardValuesSupported(context);
			}
			return base.GetStandardValuesSupported(context);
		}

		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="value">The <see cref="T:System.Object" /> to test for validity. </param>
		// Token: 0x060015F1 RID: 5617 RVA: 0x00056AA0 File Offset: 0x00054CA0
		public override bool IsValid(ITypeDescriptorContext context, object value)
		{
			if (this.simpleTypeConverter != null)
			{
				return value == null || this.simpleTypeConverter.IsValid(context, value);
			}
			return base.IsValid(context, value);
		}

		/// <summary>Gets the nullable type.</summary>
		/// <returns>A <see cref="T:System.Type" /> that represents the nullable type.</returns>
		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x060015F2 RID: 5618 RVA: 0x00056AD2 File Offset: 0x00054CD2
		public Type NullableType
		{
			get
			{
				return this.nullableType;
			}
		}

		/// <summary>Gets the underlying type.</summary>
		/// <returns>A <see cref="T:System.Type" /> that represents the underlying type.</returns>
		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x060015F3 RID: 5619 RVA: 0x00056ADA File Offset: 0x00054CDA
		public Type UnderlyingType
		{
			get
			{
				return this.simpleType;
			}
		}

		/// <summary>Gets the underlying type converter.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.TypeConverter" /> that represents the underlying type converter.</returns>
		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x060015F4 RID: 5620 RVA: 0x00056AE2 File Offset: 0x00054CE2
		public TypeConverter UnderlyingTypeConverter
		{
			get
			{
				return this.simpleTypeConverter;
			}
		}

		// Token: 0x0400137D RID: 4989
		private Type nullableType;

		// Token: 0x0400137E RID: 4990
		private Type simpleType;

		// Token: 0x0400137F RID: 4991
		private TypeConverter simpleTypeConverter;
	}
}
