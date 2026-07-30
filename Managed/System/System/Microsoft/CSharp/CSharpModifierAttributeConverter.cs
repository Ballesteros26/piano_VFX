using System;
using System.ComponentModel;
using System.Globalization;

namespace Microsoft.CSharp
{
	// Token: 0x020000EB RID: 235
	internal abstract class CSharpModifierAttributeConverter : TypeConverter
	{
		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000654 RID: 1620
		protected abstract object[] Values { get; }

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000655 RID: 1621
		protected abstract string[] Names { get; }

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000656 RID: 1622
		protected abstract object DefaultValue { get; }

		// Token: 0x06000657 RID: 1623 RVA: 0x00013983 File Offset: 0x00011B83
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x00019B98 File Offset: 0x00017D98
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			string text = value as string;
			if (text != null)
			{
				string[] names = this.Names;
				for (int i = 0; i < names.Length; i++)
				{
					if (names[i].Equals(text))
					{
						return this.Values[i];
					}
				}
			}
			return this.DefaultValue;
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x00019BE0 File Offset: 0x00017DE0
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

		// Token: 0x0600065A RID: 1626 RVA: 0x000027E2 File Offset: 0x000009E2
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x000027E2 File Offset: 0x000009E2
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x00019C4F File Offset: 0x00017E4F
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			return new TypeConverter.StandardValuesCollection(this.Values);
		}
	}
}
