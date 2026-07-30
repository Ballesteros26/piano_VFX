using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Windows.Forms
{
	/// <summary>Provides a type converter to convert data for an image index to and from one data type to another for use by the <see cref="T:System.Windows.Forms.TreeView" /> control.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000399 RID: 921
	public class TreeViewImageIndexConverter : ImageIndexConverter
	{
		/// <summary>Gets a value indicating null is valid in the <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection" /> collection.</summary>
		/// <returns>true if null is valid in the standard values collection; otherwise, false.</returns>
		// Token: 0x1700117E RID: 4478
		// (get) Token: 0x0600437A RID: 17274 RVA: 0x0010AB98 File Offset: 0x00108D98
		protected override bool IncludeNoneAsStandardValue
		{
			get
			{
				return false;
			}
		}

		/// <param name="context"></param>
		/// <param name="culture"></param>
		/// <param name="value"></param>
		// Token: 0x0600437B RID: 17275 RVA: 0x0010AB9C File Offset: 0x00108D9C
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value == null || !(value is string))
			{
				return base.ConvertFrom(context, culture, value);
			}
			string text = (string)value;
			if (text.Equals("(default)", 3))
			{
				return -1;
			}
			if (text.Equals("(none)", 3))
			{
				return -2;
			}
			return int.Parse(text);
		}

		/// <param name="context"></param>
		/// <param name="culture"></param>
		/// <param name="value"></param>
		/// <param name="destinationType"></param>
		// Token: 0x0600437C RID: 17276 RVA: 0x0010AC08 File Offset: 0x00108E08
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType != typeof(string))
			{
				return base.ConvertTo(context, culture, value, destinationType);
			}
			if (value == null)
			{
				return string.Empty;
			}
			if (value is int && (int)value == -1)
			{
				return "(default)";
			}
			if (value is int && (int)value == -2)
			{
				return "(none)";
			}
			if (value is string && ((string)value).Length == 0)
			{
				return string.Empty;
			}
			return value.ToString();
		}

		/// <param name="context"></param>
		// Token: 0x0600437D RID: 17277 RVA: 0x0010ACA0 File Offset: 0x00108EA0
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			int[] array = new int[] { -1, -2 };
			return new TypeConverter.StandardValuesCollection(array);
		}
	}
}
