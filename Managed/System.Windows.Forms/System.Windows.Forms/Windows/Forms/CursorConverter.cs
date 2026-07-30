using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;

namespace System.Windows.Forms
{
	/// <summary>Provides a type converter to convert <see cref="T:System.Windows.Forms.Cursor" /> objects to and from various other representations. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000B9 RID: 185
	public class CursorConverter : TypeConverter
	{
		/// <summary>Determines if this converter can convert an object in the given source type to the native type of the converter.</summary>
		/// <returns>true if this object can perform the conversion.</returns>
		/// <param name="context">A formatter context. This object can be used to extract additional information about the environment this converter is being invoked from. This may be null, so you should always check. Also, properties on the context object may also return null. </param>
		/// <param name="sourceType">The type you wish to convert from. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B67 RID: 2919 RVA: 0x0002EC9C File Offset: 0x0002CE9C
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(byte[]) || base.CanConvertFrom(context, sourceType);
		}

		/// <summary>Gets a value indicating whether this converter can convert an object to the given destination type using the context.</summary>
		/// <returns>true if this converter can perform the conversion; otherwise, false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="destinationType">A <see cref="T:System.Type" /> that represents the type you wish to convert to. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B68 RID: 2920 RVA: 0x0002ECB8 File Offset: 0x0002CEB8
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(byte[]) || destinationType == typeof(InstanceDescriptor) || base.CanConvertTo(context, destinationType);
		}

		/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="culture">The <see cref="T:System.Globalization.CultureInfo" /> to use as the current culture. </param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B69 RID: 2921 RVA: 0x0002ECF0 File Offset: 0x0002CEF0
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			byte[] array = value as byte[];
			if (array == null)
			{
				return base.ConvertFrom(context, culture, value);
			}
			object obj;
			using (MemoryStream memoryStream = new MemoryStream(array))
			{
				obj = new Cursor(memoryStream);
			}
			return obj;
		}

		/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" />. If null is passed, the current culture is assumed. </param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert. </param>
		/// <param name="destinationType">The <see cref="T:System.Type" /> to convert the <paramref name="value" /> parameter to. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B6A RID: 2922 RVA: 0x0002ED58 File Offset: 0x0002CF58
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (value == null && destinationType == typeof(string))
			{
				return "(none)";
			}
			if (!(value is Cursor))
			{
				throw new ArgumentException("object must be of class Cursor", "value");
			}
			if (destinationType != typeof(byte[]))
			{
				if (destinationType == typeof(InstanceDescriptor))
				{
					PropertyInfo[] properties = typeof(Cursors).GetProperties();
					foreach (PropertyInfo propertyInfo in properties)
					{
						if (propertyInfo.GetValue(null, null) == value)
						{
							return new InstanceDescriptor(propertyInfo, null);
						}
					}
				}
				return base.ConvertTo(context, culture, value, destinationType);
			}
			if (value == null)
			{
				return new byte[0];
			}
			Cursor cursor = (Cursor)value;
			SerializationInfo serializationInfo = new SerializationInfo(typeof(Cursor), new FormatterConverter());
			cursor.GetObjectData(serializationInfo, new StreamingContext(16));
			return (byte[])serializationInfo.GetValue("CursorData", typeof(byte[]));
		}

		/// <summary>Retrieves a collection containing a set of standard values for the data type this validator is designed for. This will return null if the data type does not support a standard set of values.</summary>
		/// <returns>A collection containing a standard set of valid values, or null. The default implementation always returns null.</returns>
		/// <param name="context">A formatter context. This object can be used to extract additional information about the environment this converter is being invoked from. This may be null, so you should always check. Also, properties on the context object may also return null. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B6B RID: 2923 RVA: 0x0002EE78 File Offset: 0x0002D078
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			PropertyInfo[] properties = typeof(Cursors).GetProperties();
			ArrayList arrayList = new ArrayList();
			for (int i = 0; i < properties.Length; i++)
			{
				arrayList.Add(properties[i].GetValue(null, null));
			}
			return new TypeConverter.StandardValuesCollection(arrayList);
		}

		/// <summary>Determines if this object supports a standard set of values that can be picked from a list.</summary>
		/// <returns>Returns true if GetStandardValues should be called to find a common set of values the object supports.</returns>
		/// <param name="context">A type descriptor through which additional context may be provided. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B6C RID: 2924 RVA: 0x0002EEC8 File Offset: 0x0002D0C8
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}
	}
}
