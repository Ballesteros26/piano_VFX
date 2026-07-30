using System;
using System.Globalization;

namespace UnityEngine.UIElements
{
	// Token: 0x020001EA RID: 490
	public class UxmlDoubleAttributeDescription : TypedUxmlAttributeDescription<double>
	{
		// Token: 0x06000F19 RID: 3865 RVA: 0x00037ECD File Offset: 0x000360CD
		public UxmlDoubleAttributeDescription()
		{
			base.type = "double";
			base.typeNamespace = "http://www.w3.org/2001/XMLSchema";
			base.defaultValue = 0.0;
		}

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x06000F1A RID: 3866 RVA: 0x00037F00 File Offset: 0x00036100
		public override string defaultValueAsString
		{
			get
			{
				return base.defaultValue.ToString(CultureInfo.InvariantCulture.NumberFormat);
			}
		}

		// Token: 0x06000F1B RID: 3867 RVA: 0x00037F2C File Offset: 0x0003612C
		public override double GetValueFromBag(IUxmlAttributes bag, CreationContext cc)
		{
			return base.GetValueFromBag<double>(bag, cc, (string s, double d) => UxmlDoubleAttributeDescription.ConvertValueToDouble(s, d), base.defaultValue);
		}

		// Token: 0x06000F1C RID: 3868 RVA: 0x00037F6C File Offset: 0x0003616C
		public bool TryGetValueFromBag(IUxmlAttributes bag, CreationContext cc, ref double value)
		{
			return base.TryGetValueFromBag<double>(bag, cc, (string s, double d) => UxmlDoubleAttributeDescription.ConvertValueToDouble(s, d), base.defaultValue, ref value);
		}

		// Token: 0x06000F1D RID: 3869 RVA: 0x00037FAC File Offset: 0x000361AC
		private static double ConvertValueToDouble(string v, double defaultValue)
		{
			double num;
			bool flag = v == null || !double.TryParse(v, ref num);
			double num2;
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
