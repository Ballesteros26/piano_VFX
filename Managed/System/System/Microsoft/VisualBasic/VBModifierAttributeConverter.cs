using System;
using System.ComponentModel;
using System.Globalization;

namespace Microsoft.VisualBasic
{
	// Token: 0x020000E5 RID: 229
	internal abstract class VBModifierAttributeConverter : TypeConverter
	{
		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000594 RID: 1428
		protected abstract object[] Values { get; }

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000595 RID: 1429
		protected abstract string[] Names { get; }

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000596 RID: 1430
		protected abstract object DefaultValue { get; }

		// Token: 0x06000597 RID: 1431 RVA: 0x00013983 File Offset: 0x00011B83
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x000139A4 File Offset: 0x00011BA4
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			string text = value as string;
			if (text != null)
			{
				string[] names = this.Names;
				for (int i = 0; i < names.Length; i++)
				{
					if (names[i].Equals(text, StringComparison.OrdinalIgnoreCase))
					{
						return this.Values[i];
					}
				}
			}
			return this.DefaultValue;
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x000139EC File Offset: 0x00011BEC
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == typeof(string))
			{
				object[] values = this.Values;
				for (int i = 0; i < values.Length; i++)
				{
					if (values[i].Equals(value))
					{
						return this.Names[i];
					}
				}
				return "(unknown)";
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x000027E2 File Offset: 0x000009E2
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x000027E2 File Offset: 0x000009E2
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x00013A5B File Offset: 0x00011C5B
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			return new TypeConverter.StandardValuesCollection(this.Values);
		}
	}
}
