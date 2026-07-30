using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Web.UI
{
	// Token: 0x020001EB RID: 491
	internal class MinimizableAttributeTypeConverter : TypeConverter
	{
		// Token: 0x060013CC RID: 5068 RVA: 0x00035B25 File Offset: 0x00033D25
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x060013CD RID: 5069 RVA: 0x00035B44 File Offset: 0x00033D44
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (value is string && destinationType == typeof(bool))
			{
				return value != null;
			}
			if (value is bool && destinationType == typeof(string))
			{
				return ((bool)value).ToString(culture);
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		// Token: 0x060013CE RID: 5070 RVA: 0x00035BAC File Offset: 0x00033DAC
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			string text2;
			if (value != null)
			{
				Type type = value.GetType();
				if (type == typeof(string))
				{
					string text = value as string;
					if (string.IsNullOrEmpty(text) || string.Compare(text, "false", StringComparison.OrdinalIgnoreCase) == 0)
					{
						return false;
					}
					return true;
				}
				else
				{
					text2 = type.FullName;
				}
			}
			else
			{
				text2 = "null";
			}
			throw new NotSupportedException(string.Format("MinimizableAttributeTypeConverter cannot convert from {0}", text2));
		}
	}
}
