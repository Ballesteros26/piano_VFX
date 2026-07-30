using System;
using System.Globalization;

namespace UnityEngine.UIElements
{
	// Token: 0x020001EC RID: 492
	public class UxmlIntAttributeDescription : TypedUxmlAttributeDescription<int>
	{
		// Token: 0x06000F22 RID: 3874 RVA: 0x00037FED File Offset: 0x000361ED
		public UxmlIntAttributeDescription()
		{
			base.type = "int";
			base.typeNamespace = "http://www.w3.org/2001/XMLSchema";
			base.defaultValue = 0;
		}

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x06000F23 RID: 3875 RVA: 0x00038018 File Offset: 0x00036218
		public override string defaultValueAsString
		{
			get
			{
				return base.defaultValue.ToString(CultureInfo.InvariantCulture.NumberFormat);
			}
		}

		// Token: 0x06000F24 RID: 3876 RVA: 0x00038044 File Offset: 0x00036244
		public override int GetValueFromBag(IUxmlAttributes bag, CreationContext cc)
		{
			return base.GetValueFromBag<int>(bag, cc, (string s, int i) => UxmlIntAttributeDescription.ConvertValueToInt(s, i), base.defaultValue);
		}

		// Token: 0x06000F25 RID: 3877 RVA: 0x00038084 File Offset: 0x00036284
		public bool TryGetValueFromBag(IUxmlAttributes bag, CreationContext cc, ref int value)
		{
			return base.TryGetValueFromBag<int>(bag, cc, (string s, int i) => UxmlIntAttributeDescription.ConvertValueToInt(s, i), base.defaultValue, ref value);
		}

		// Token: 0x06000F26 RID: 3878 RVA: 0x000380C4 File Offset: 0x000362C4
		private static int ConvertValueToInt(string v, int defaultValue)
		{
			int num;
			bool flag = v == null || !int.TryParse(v, ref num);
			int num2;
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
