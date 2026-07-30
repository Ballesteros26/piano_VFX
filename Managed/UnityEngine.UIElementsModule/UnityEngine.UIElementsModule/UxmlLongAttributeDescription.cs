using System;
using System.Globalization;

namespace UnityEngine.UIElements
{
	// Token: 0x020001EE RID: 494
	public class UxmlLongAttributeDescription : TypedUxmlAttributeDescription<long>
	{
		// Token: 0x06000F2B RID: 3883 RVA: 0x00038105 File Offset: 0x00036305
		public UxmlLongAttributeDescription()
		{
			base.type = "long";
			base.typeNamespace = "http://www.w3.org/2001/XMLSchema";
			base.defaultValue = 0L;
		}

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x06000F2C RID: 3884 RVA: 0x00038130 File Offset: 0x00036330
		public override string defaultValueAsString
		{
			get
			{
				return base.defaultValue.ToString(CultureInfo.InvariantCulture.NumberFormat);
			}
		}

		// Token: 0x06000F2D RID: 3885 RVA: 0x0003815C File Offset: 0x0003635C
		public override long GetValueFromBag(IUxmlAttributes bag, CreationContext cc)
		{
			return base.GetValueFromBag<long>(bag, cc, (string s, long l) => UxmlLongAttributeDescription.ConvertValueToLong(s, l), base.defaultValue);
		}

		// Token: 0x06000F2E RID: 3886 RVA: 0x0003819C File Offset: 0x0003639C
		public bool TryGetValueFromBag(IUxmlAttributes bag, CreationContext cc, ref long value)
		{
			return base.TryGetValueFromBag<long>(bag, cc, (string s, long l) => UxmlLongAttributeDescription.ConvertValueToLong(s, l), base.defaultValue, ref value);
		}

		// Token: 0x06000F2F RID: 3887 RVA: 0x000381DC File Offset: 0x000363DC
		private static long ConvertValueToLong(string v, long defaultValue)
		{
			long num;
			bool flag = v == null || !long.TryParse(v, ref num);
			long num2;
			if (flag)
			{
				num2 = defaultValue;
			}
			else
			{
				num2 = num;
			}
			return num2;
		}
	}
}
