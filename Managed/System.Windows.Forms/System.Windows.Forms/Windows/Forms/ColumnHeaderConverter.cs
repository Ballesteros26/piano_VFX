using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;

namespace System.Windows.Forms
{
	/// <summary>Provides a type converter to convert <see cref="T:System.Windows.Forms.ColumnHeader" /> objects from one type to another.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200008B RID: 139
	public class ColumnHeaderConverter : ExpandableObjectConverter
	{
		/// <summary>Converts the specified object to the specified type, using the specified context and culture information.</summary>
		/// <returns>The <see cref="T:System.Object" /> that is the result of the conversion.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> that represents information about a culture, such as language and calendar system. Can be null.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert.</param>
		/// <param name="destinationType">The <see cref="T:System.Type" /> to convert to.</param>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed<paramref name="." /></exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600065C RID: 1628 RVA: 0x0001D618 File Offset: 0x0001B818
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(InstanceDescriptor) && value is ColumnHeader)
			{
				ColumnHeader columnHeader = (ColumnHeader)value;
				if (columnHeader.ImageIndex != -1)
				{
					Type[] array = new Type[] { typeof(int) };
					ConstructorInfo constructorInfo = typeof(ColumnHeader).GetConstructor(array);
					if (constructorInfo != null)
					{
						object[] array2 = new object[] { columnHeader.ImageIndex };
						return new InstanceDescriptor(constructorInfo, array2, false);
					}
				}
				else if (string.IsNullOrEmpty(columnHeader.ImageKey))
				{
					Type[] array = new Type[] { typeof(string) };
					ConstructorInfo constructorInfo = typeof(ColumnHeader).GetConstructor(array);
					if (constructorInfo != null)
					{
						object[] array3 = new object[] { columnHeader.ImageKey };
						return new InstanceDescriptor(constructorInfo, array3, false);
					}
				}
				else
				{
					Type[] array = Type.EmptyTypes;
					ConstructorInfo constructorInfo = typeof(ColumnHeader).GetConstructor(array);
					if (constructorInfo != null)
					{
						object[] array4 = new object[0];
						return new InstanceDescriptor(constructorInfo, array4, false);
					}
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		/// <summary>Returns a value indicating whether the <see cref="T:System.Windows.Forms.ColumnHeaderConverter" /> can convert a <see cref="T:System.Windows.Forms.ColumnHeader" /> to the specified type, using the specified context.</summary>
		/// <returns>true if this converter can perform the conversion; otherwise, false.</returns>
		/// <param name="context">A <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="destinationType">A type representing the type to convert to.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600065D RID: 1629 RVA: 0x0001D738 File Offset: 0x0001B938
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(InstanceDescriptor) || base.CanConvertTo(context, destinationType);
		}
	}
}
