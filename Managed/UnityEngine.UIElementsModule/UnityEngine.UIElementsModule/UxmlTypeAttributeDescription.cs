using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001F4 RID: 500
	public class UxmlTypeAttributeDescription<TBase> : TypedUxmlAttributeDescription<Type>
	{
		// Token: 0x06000F46 RID: 3910 RVA: 0x0003846D File Offset: 0x0003666D
		public UxmlTypeAttributeDescription()
		{
			base.type = "string";
			base.typeNamespace = "http://www.w3.org/2001/XMLSchema";
			base.defaultValue = null;
		}

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x06000F47 RID: 3911 RVA: 0x00038498 File Offset: 0x00036698
		public override string defaultValueAsString
		{
			get
			{
				return (base.defaultValue == null) ? "null" : base.defaultValue.FullName;
			}
		}

		// Token: 0x06000F48 RID: 3912 RVA: 0x000384C4 File Offset: 0x000366C4
		public override Type GetValueFromBag(IUxmlAttributes bag, CreationContext cc)
		{
			return base.GetValueFromBag<Type>(bag, cc, (string s, Type type1) => this.ConvertValueToType(s, type1), base.defaultValue);
		}

		// Token: 0x06000F49 RID: 3913 RVA: 0x000384F0 File Offset: 0x000366F0
		public bool TryGetValueFromBag(IUxmlAttributes bag, CreationContext cc, ref Type value)
		{
			return base.TryGetValueFromBag<Type>(bag, cc, (string s, Type type1) => this.ConvertValueToType(s, type1), base.defaultValue, ref value);
		}

		// Token: 0x06000F4A RID: 3914 RVA: 0x00038520 File Offset: 0x00036720
		private Type ConvertValueToType(string v, Type defaultValue)
		{
			bool flag = string.IsNullOrEmpty(v);
			Type type;
			if (flag)
			{
				type = defaultValue;
			}
			else
			{
				try
				{
					Type type2 = Type.GetType(v, true);
					bool flag2 = !typeof(TBase).IsAssignableFrom(type2);
					if (!flag2)
					{
						return type2;
					}
					Debug.LogError(string.Concat(new string[]
					{
						"Type: Invalid type \"",
						v,
						"\". Type must derive from ",
						typeof(TBase).FullName,
						"."
					}));
				}
				catch (Exception ex)
				{
					Debug.LogException(ex);
				}
				type = defaultValue;
			}
			return type;
		}
	}
}
