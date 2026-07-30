using System;
using System.Collections.Generic;
using System.Globalization;

namespace UnityEngine.UIElements
{
	// Token: 0x020001F5 RID: 501
	public class UxmlEnumAttributeDescription<T> : TypedUxmlAttributeDescription<T> where T : struct, IConvertible
	{
		// Token: 0x06000F4D RID: 3917 RVA: 0x000385D4 File Offset: 0x000367D4
		public UxmlEnumAttributeDescription()
		{
			bool flag = !typeof(T).IsEnum;
			if (flag)
			{
				throw new ArgumentException("T must be an enumerated type");
			}
			base.type = "string";
			base.typeNamespace = "http://www.w3.org/2001/XMLSchema";
			base.defaultValue = new T();
			UxmlEnumeration uxmlEnumeration = new UxmlEnumeration();
			List<string> list = new List<string>();
			foreach (object obj in Enum.GetValues(typeof(T)))
			{
				T t = (T)((object)obj);
				list.Add(t.ToString(CultureInfo.InvariantCulture));
			}
			uxmlEnumeration.values = list;
			base.restriction = uxmlEnumeration;
		}

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x06000F4E RID: 3918 RVA: 0x000386BC File Offset: 0x000368BC
		public override string defaultValueAsString
		{
			get
			{
				T defaultValue = base.defaultValue;
				return defaultValue.ToString(CultureInfo.InvariantCulture.NumberFormat);
			}
		}

		// Token: 0x06000F4F RID: 3919 RVA: 0x000386EC File Offset: 0x000368EC
		public override T GetValueFromBag(IUxmlAttributes bag, CreationContext cc)
		{
			return base.GetValueFromBag<T>(bag, cc, (string s, T convertible) => UxmlEnumAttributeDescription<T>.ConvertValueToEnum<T>(s, convertible), base.defaultValue);
		}

		// Token: 0x06000F50 RID: 3920 RVA: 0x0003872C File Offset: 0x0003692C
		public bool TryGetValueFromBag(IUxmlAttributes bag, CreationContext cc, ref T value)
		{
			return base.TryGetValueFromBag<T>(bag, cc, (string s, T convertible) => UxmlEnumAttributeDescription<T>.ConvertValueToEnum<T>(s, convertible), base.defaultValue, ref value);
		}

		// Token: 0x06000F51 RID: 3921 RVA: 0x0003876C File Offset: 0x0003696C
		private static U ConvertValueToEnum<U>(string v, U defaultValue)
		{
			bool flag = v == null || !Enum.IsDefined(typeof(U), v);
			U u;
			if (flag)
			{
				u = defaultValue;
			}
			else
			{
				U u2 = (U)((object)Enum.Parse(typeof(U), v));
				u = u2;
			}
			return u;
		}
	}
}
