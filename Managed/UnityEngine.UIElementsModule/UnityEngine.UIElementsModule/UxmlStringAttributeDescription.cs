using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001E6 RID: 486
	public class UxmlStringAttributeDescription : TypedUxmlAttributeDescription<string>
	{
		// Token: 0x06000F08 RID: 3848 RVA: 0x00037CDB File Offset: 0x00035EDB
		public UxmlStringAttributeDescription()
		{
			base.type = "string";
			base.typeNamespace = "http://www.w3.org/2001/XMLSchema";
			base.defaultValue = "";
		}

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x06000F09 RID: 3849 RVA: 0x00037D0C File Offset: 0x00035F0C
		public override string defaultValueAsString
		{
			get
			{
				return base.defaultValue;
			}
		}

		// Token: 0x06000F0A RID: 3850 RVA: 0x00037D24 File Offset: 0x00035F24
		public override string GetValueFromBag(IUxmlAttributes bag, CreationContext cc)
		{
			return base.GetValueFromBag<string>(bag, cc, (string s, string t) => s, base.defaultValue);
		}

		// Token: 0x06000F0B RID: 3851 RVA: 0x00037D64 File Offset: 0x00035F64
		public bool TryGetValueFromBag(IUxmlAttributes bag, CreationContext cc, ref string value)
		{
			return base.TryGetValueFromBag<string>(bag, cc, (string s, string t) => s, base.defaultValue, ref value);
		}
	}
}
