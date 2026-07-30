using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001F0 RID: 496
	public class UxmlBoolAttributeDescription : TypedUxmlAttributeDescription<bool>
	{
		// Token: 0x06000F34 RID: 3892 RVA: 0x0003821D File Offset: 0x0003641D
		public UxmlBoolAttributeDescription()
		{
			base.type = "boolean";
			base.typeNamespace = "http://www.w3.org/2001/XMLSchema";
			base.defaultValue = false;
		}

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x06000F35 RID: 3893 RVA: 0x00038248 File Offset: 0x00036448
		public override string defaultValueAsString
		{
			get
			{
				return base.defaultValue.ToString().ToLower();
			}
		}

		// Token: 0x06000F36 RID: 3894 RVA: 0x00038270 File Offset: 0x00036470
		public override bool GetValueFromBag(IUxmlAttributes bag, CreationContext cc)
		{
			return base.GetValueFromBag<bool>(bag, cc, (string s, bool b) => UxmlBoolAttributeDescription.ConvertValueToBool(s, b), base.defaultValue);
		}

		// Token: 0x06000F37 RID: 3895 RVA: 0x000382B0 File Offset: 0x000364B0
		public bool TryGetValueFromBag(IUxmlAttributes bag, CreationContext cc, ref bool value)
		{
			return base.TryGetValueFromBag<bool>(bag, cc, (string s, bool b) => UxmlBoolAttributeDescription.ConvertValueToBool(s, b), base.defaultValue, ref value);
		}

		// Token: 0x06000F38 RID: 3896 RVA: 0x000382F0 File Offset: 0x000364F0
		private static bool ConvertValueToBool(string v, bool defaultValue)
		{
			bool flag2;
			bool flag = v == null || !bool.TryParse(v, ref flag2);
			bool flag3;
			if (flag)
			{
				flag3 = defaultValue;
			}
			else
			{
				flag3 = flag2;
			}
			return flag3;
		}
	}
}
