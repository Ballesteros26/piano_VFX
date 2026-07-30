using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001F2 RID: 498
	public class UxmlColorAttributeDescription : TypedUxmlAttributeDescription<Color>
	{
		// Token: 0x06000F3D RID: 3901 RVA: 0x00038334 File Offset: 0x00036534
		public UxmlColorAttributeDescription()
		{
			base.type = "string";
			base.typeNamespace = "http://www.w3.org/2001/XMLSchema";
			base.defaultValue = new Color(0f, 0f, 0f, 1f);
		}

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x06000F3E RID: 3902 RVA: 0x00038384 File Offset: 0x00036584
		public override string defaultValueAsString
		{
			get
			{
				return base.defaultValue.ToString();
			}
		}

		// Token: 0x06000F3F RID: 3903 RVA: 0x000383AC File Offset: 0x000365AC
		public override Color GetValueFromBag(IUxmlAttributes bag, CreationContext cc)
		{
			return base.GetValueFromBag<Color>(bag, cc, (string s, Color color) => UxmlColorAttributeDescription.ConvertValueToColor(s, color), base.defaultValue);
		}

		// Token: 0x06000F40 RID: 3904 RVA: 0x000383EC File Offset: 0x000365EC
		public bool TryGetValueFromBag(IUxmlAttributes bag, CreationContext cc, ref Color value)
		{
			return base.TryGetValueFromBag<Color>(bag, cc, (string s, Color color) => UxmlColorAttributeDescription.ConvertValueToColor(s, color), base.defaultValue, ref value);
		}

		// Token: 0x06000F41 RID: 3905 RVA: 0x0003842C File Offset: 0x0003662C
		private static Color ConvertValueToColor(string v, Color defaultValue)
		{
			Color color;
			bool flag = v == null || !ColorUtility.TryParseHtmlString(v, out color);
			Color color2;
			if (flag)
			{
				color2 = defaultValue;
			}
			else
			{
				color2 = color;
			}
			return color2;
		}
	}
}
