using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;

namespace System.Windows.Forms
{
	/// <summary>Provides a type converter to convert <see cref="T:System.Windows.Forms.Padding" /> values to and from various other representations.</summary>
	// Token: 0x02000282 RID: 642
	public class PaddingConverter : TypeConverter
	{
		/// <summary>Returns whether this converter can convert an object of one type to the type of this converter.</summary>
		/// <returns>true if this object can perform the conversion; otherwise, false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="sourceType">A <see cref="T:System.Type" /> that represents the type you wish to convert from.</param>
		// Token: 0x060029BA RID: 10682 RVA: 0x000A0BA8 File Offset: 0x0009EDA8
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string);
		}

		/// <param name="context"></param>
		/// <param name="destinationType"></param>
		// Token: 0x060029BB RID: 10683 RVA: 0x000A0BC0 File Offset: 0x0009EDC0
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(string) || destinationType == typeof(InstanceDescriptor);
		}

		/// <param name="context"></param>
		/// <param name="culture"></param>
		/// <param name="value"></param>
		// Token: 0x060029BC RID: 10684 RVA: 0x000A0BE8 File Offset: 0x0009EDE8
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value == null || !(value is string))
			{
				return base.ConvertFrom(context, culture, value);
			}
			if (culture == null)
			{
				culture = CultureInfo.CurrentCulture;
			}
			string[] array = ((string)value).Split(culture.TextInfo.ListSeparator.ToCharArray());
			return new Padding(int.Parse(array[0].Trim()), int.Parse(array[1].Trim()), int.Parse(array[2].Trim()), int.Parse(array[3].Trim()));
		}

		/// <param name="context"></param>
		/// <param name="culture"></param>
		/// <param name="value"></param>
		/// <param name="destinationType"></param>
		// Token: 0x060029BD RID: 10685 RVA: 0x000A0C78 File Offset: 0x0009EE78
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (value is Padding)
			{
				Padding padding = (Padding)value;
				if (destinationType == typeof(string))
				{
					if (culture == null)
					{
						culture = CultureInfo.CurrentCulture;
					}
					return string.Format("{0}{4} {1}{4} {2}{4} {3}", new object[]
					{
						padding.Left,
						padding.Top,
						padding.Right,
						padding.Bottom,
						culture.TextInfo.ListSeparator
					});
				}
				if (destinationType == typeof(InstanceDescriptor))
				{
					Type[] array;
					object[] array2;
					if (padding.All != -1)
					{
						array = new Type[] { typeof(int) };
						array2 = new object[] { padding.All };
					}
					else
					{
						array = new Type[]
						{
							typeof(int),
							typeof(int),
							typeof(int),
							typeof(int)
						};
						array2 = new object[] { padding.Left, padding.Top, padding.Right, padding.Bottom };
					}
					ConstructorInfo constructor = typeof(Padding).GetConstructor(array);
					return new InstanceDescriptor(constructor, array2);
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		/// <param name="context"></param>
		/// <param name="propertyValues"></param>
		// Token: 0x060029BE RID: 10686 RVA: 0x000A0E00 File Offset: 0x0009F000
		public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
		{
			if (propertyValues == null)
			{
				throw new ArgumentNullException("propertyValues");
			}
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			if (((Padding)context.PropertyDescriptor.GetValue(context.Instance)).All == (int)propertyValues["All"])
			{
				return new Padding((int)propertyValues["Left"], (int)propertyValues["Top"], (int)propertyValues["Right"], (int)propertyValues["Bottom"]);
			}
			return new Padding((int)propertyValues["All"]);
		}

		/// <param name="context"></param>
		// Token: 0x060029BF RID: 10687 RVA: 0x000A0EC8 File Offset: 0x0009F0C8
		public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		/// <param name="context"></param>
		/// <param name="value"></param>
		/// <param name="attributes"></param>
		// Token: 0x060029C0 RID: 10688 RVA: 0x000A0ECC File Offset: 0x0009F0CC
		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			return TypeDescriptor.GetProperties(typeof(Padding), attributes);
		}

		/// <param name="context"></param>
		// Token: 0x060029C1 RID: 10689 RVA: 0x000A0EE0 File Offset: 0x0009F0E0
		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return true;
		}
	}
}
