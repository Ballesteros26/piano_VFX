using System;
using System.Globalization;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Provides a type converter to convert <see cref="T:System.Array" /> objects to and from various other representations.</summary>
	// Token: 0x02000227 RID: 551
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class ArrayConverter : CollectionConverter
	{
		/// <summary>Converts the given value object to the specified destination type.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="culture">The culture into which <paramref name="value" /> will be converted.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert. </param>
		/// <param name="destinationType">The <see cref="T:System.Type" /> to convert the value to. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="destinationType" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed. </exception>
		// Token: 0x060011D9 RID: 4569 RVA: 0x0004C994 File Offset: 0x0004AB94
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == typeof(string) && value is Array)
			{
				return global::SR.GetString("{0} Array", new object[] { value.GetType().Name });
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		/// <summary>Gets a collection of properties for the type of array specified by the value parameter.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> with the properties that are exposed for an array, or null if there are no properties.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="value">An <see cref="T:System.Object" /> that specifies the type of array to get the properties for. </param>
		/// <param name="attributes">An array of type <see cref="T:System.Attribute" /> that will be used as a filter. </param>
		// Token: 0x060011DA RID: 4570 RVA: 0x0004C9FC File Offset: 0x0004ABFC
		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			PropertyDescriptor[] array = null;
			if (value.GetType().IsArray)
			{
				int length = ((Array)value).GetLength(0);
				array = new PropertyDescriptor[length];
				Type type = value.GetType();
				Type elementType = type.GetElementType();
				for (int i = 0; i < length; i++)
				{
					array[i] = new ArrayConverter.ArrayPropertyDescriptor(type, elementType, i);
				}
			}
			return new PropertyDescriptorCollection(array);
		}

		/// <summary>Gets a value indicating whether this object supports properties.</summary>
		/// <returns>true because <see cref="M:System.ComponentModel.ArrayConverter.GetProperties(System.ComponentModel.ITypeDescriptorContext,System.Object,System.Attribute[])" /> should be called to find the properties of this object. This method never returns false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		// Token: 0x060011DB RID: 4571 RVA: 0x000027E2 File Offset: 0x000009E2
		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x02000228 RID: 552
		private class ArrayPropertyDescriptor : TypeConverter.SimplePropertyDescriptor
		{
			// Token: 0x060011DD RID: 4573 RVA: 0x0004CA65 File Offset: 0x0004AC65
			public ArrayPropertyDescriptor(Type arrayType, Type elementType, int index)
				: base(arrayType, "[" + index + "]", elementType, null)
			{
				this.index = index;
			}

			// Token: 0x060011DE RID: 4574 RVA: 0x0004CA8C File Offset: 0x0004AC8C
			public override object GetValue(object instance)
			{
				if (instance is Array)
				{
					Array array = (Array)instance;
					if (array.GetLength(0) > this.index)
					{
						return array.GetValue(this.index);
					}
				}
				return null;
			}

			// Token: 0x060011DF RID: 4575 RVA: 0x0004CAC8 File Offset: 0x0004ACC8
			public override void SetValue(object instance, object value)
			{
				if (instance is Array)
				{
					Array array = (Array)instance;
					if (array.GetLength(0) > this.index)
					{
						array.SetValue(value, this.index);
					}
					this.OnValueChanged(instance, EventArgs.Empty);
				}
			}

			// Token: 0x04001229 RID: 4649
			private int index;
		}
	}
}
