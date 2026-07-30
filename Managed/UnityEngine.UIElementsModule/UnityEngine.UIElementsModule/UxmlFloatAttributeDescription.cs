using System;
using System.Globalization;

namespace UnityEngine.UIElements
{
	// Token: 0x020001E8 RID: 488
	public class UxmlFloatAttributeDescription : TypedUxmlAttributeDescription<float>
	{
		// Token: 0x06000F10 RID: 3856 RVA: 0x00037DB0 File Offset: 0x00035FB0
		public UxmlFloatAttributeDescription()
		{
			base.type = "float";
			base.typeNamespace = "http://www.w3.org/2001/XMLSchema";
			base.defaultValue = 0f;
		}

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x06000F11 RID: 3857 RVA: 0x00037DE0 File Offset: 0x00035FE0
		public override string defaultValueAsString
		{
			get
			{
				return base.defaultValue.ToString(CultureInfo.InvariantCulture.NumberFormat);
			}
		}

		// Token: 0x06000F12 RID: 3858 RVA: 0x00037E0C File Offset: 0x0003600C
		public override float GetValueFromBag(IUxmlAttributes bag, CreationContext cc)
		{
			return base.GetValueFromBag<float>(bag, cc, (string s, float f) => UxmlFloatAttributeDescription.ConvertValueToFloat(s, f), base.defaultValue);
		}

		// Token: 0x06000F13 RID: 3859 RVA: 0x00037E4C File Offset: 0x0003604C
		public bool TryGetValueFromBag(IUxmlAttributes bag, CreationContext cc, ref float value)
		{
			return base.TryGetValueFromBag<float>(bag, cc, (string s, float f) => UxmlFloatAttributeDescription.ConvertValueToFloat(s, f), base.defaultValue, ref value);
		}

		// Token: 0x06000F14 RID: 3860 RVA: 0x00037E8C File Offset: 0x0003608C
		private static float ConvertValueToFloat(string v, float defaultValue)
		{
			float num;
			bool flag = v == null || !float.TryParse(v, ref num);
			float num2;
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
