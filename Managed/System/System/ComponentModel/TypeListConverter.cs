using System;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Provides a type converter that can be used to populate a list box with available types.</summary>
	// Token: 0x020002F0 RID: 752
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public abstract class TypeListConverter : TypeConverter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.TypeListConverter" /> class using the type array as the available types.</summary>
		/// <param name="types">The array of type <see cref="T:System.Type" /> to use as the available types. </param>
		// Token: 0x0600183A RID: 6202 RVA: 0x000600E4 File Offset: 0x0005E2E4
		protected TypeListConverter(Type[] types)
		{
			this.types = types;
		}

		/// <summary>Gets a value indicating whether this converter can convert the specified <see cref="T:System.Type" /> of the source object using the given context.</summary>
		/// <returns>true if this converter can perform the conversion; otherwise, false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="sourceType">The <see cref="T:System.Type" /> of the source object.</param>
		// Token: 0x0600183B RID: 6203 RVA: 0x0004DD24 File Offset: 0x0004BF24
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		/// <summary>Gets a value indicating whether this converter can convert an object to the given destination type using the context.</summary>
		/// <returns>true if this converter can perform the conversion; otherwise, false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="destinationType">A <see cref="T:System.Type" /> that represents the type you wish to convert to. </param>
		// Token: 0x0600183C RID: 6204 RVA: 0x0004F265 File Offset: 0x0004D465
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(InstanceDescriptor) || base.CanConvertTo(context, destinationType);
		}

		/// <summary>Converts the specified object to the native type of the converter.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> that specifies the culture used to represent the font. </param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert. </param>
		// Token: 0x0600183D RID: 6205 RVA: 0x000600F4 File Offset: 0x0005E2F4
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is string)
			{
				foreach (Type type in this.types)
				{
					if (value.Equals(type.FullName))
					{
						return type;
					}
				}
			}
			return base.ConvertFrom(context, culture, value);
		}

		/// <summary>Converts the given value object to the specified destination type.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted <paramref name="value" />.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="culture">An optional <see cref="T:System.Globalization.CultureInfo" />. If not supplied, the current culture is assumed. </param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert. </param>
		/// <param name="destinationType">The <see cref="T:System.Type" /> to convert the value to. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="destinationType" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed. </exception>
		// Token: 0x0600183E RID: 6206 RVA: 0x0006013C File Offset: 0x0005E33C
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (!(destinationType == typeof(string)))
			{
				if (destinationType == typeof(InstanceDescriptor) && value is Type)
				{
					MethodInfo method = typeof(Type).GetMethod("GetType", new Type[] { typeof(string) });
					if (method != null)
					{
						return new InstanceDescriptor(method, new object[] { ((Type)value).AssemblyQualifiedName });
					}
				}
				return base.ConvertTo(context, culture, value, destinationType);
			}
			if (value == null)
			{
				return global::SR.GetString("(none)");
			}
			return ((Type)value).FullName;
		}

		/// <summary>Gets a collection of standard values for the data type this validator is designed for.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection" /> that holds a standard set of valid values, or null if the data type does not support a standard set of values.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		// Token: 0x0600183F RID: 6207 RVA: 0x00060200 File Offset: 0x0005E400
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			if (this.values == null)
			{
				object[] array;
				if (this.types != null)
				{
					array = new object[this.types.Length];
					Array.Copy(this.types, array, this.types.Length);
				}
				else
				{
					array = null;
				}
				this.values = new TypeConverter.StandardValuesCollection(array);
			}
			return this.values;
		}

		/// <summary>Gets a value indicating whether the list of standard values returned from the <see cref="M:System.ComponentModel.TypeListConverter.GetStandardValues(System.ComponentModel.ITypeDescriptorContext)" /> method is an exclusive list.</summary>
		/// <returns>true because the <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection" /> returned from <see cref="M:System.ComponentModel.TypeListConverter.GetStandardValues(System.ComponentModel.ITypeDescriptorContext)" /> is an exhaustive list of possible values. This method never returns false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		// Token: 0x06001840 RID: 6208 RVA: 0x000027E2 File Offset: 0x000009E2
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return true;
		}

		/// <summary>Gets a value indicating whether this object supports a standard set of values that can be picked from a list using the specified context.</summary>
		/// <returns>true because <see cref="M:System.ComponentModel.TypeListConverter.GetStandardValues(System.ComponentModel.ITypeDescriptorContext)" /> should be called to find a common set of values the object supports. This method never returns false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		// Token: 0x06001841 RID: 6209 RVA: 0x000027E2 File Offset: 0x000009E2
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x0400141F RID: 5151
		private Type[] types;

		// Token: 0x04001420 RID: 5152
		private TypeConverter.StandardValuesCollection values;
	}
}
