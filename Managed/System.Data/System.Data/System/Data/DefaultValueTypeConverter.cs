using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Data
{
	// Token: 0x020000A3 RID: 163
	internal sealed class DefaultValueTypeConverter : StringConverter
	{
		// Token: 0x06000A08 RID: 2568 RVA: 0x0002D074 File Offset: 0x0002B274
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == typeof(string))
			{
				if (value == null)
				{
					return "<null>";
				}
				if (value == DBNull.Value)
				{
					return "<DBNull>";
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		// Token: 0x06000A09 RID: 2569 RVA: 0x0002D0CC File Offset: 0x0002B2CC
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value != null && value.GetType() == typeof(string))
			{
				string text = (string)value;
				if (string.Equals(text, "<null>", StringComparison.OrdinalIgnoreCase))
				{
					return null;
				}
				if (string.Equals(text, "<DBNull>", StringComparison.OrdinalIgnoreCase))
				{
					return DBNull.Value;
				}
			}
			return base.ConvertFrom(context, culture, value);
		}

		// Token: 0x040006A2 RID: 1698
		private const string NullString = "<null>";

		// Token: 0x040006A3 RID: 1699
		private const string DbNullString = "<DBNull>";
	}
}
