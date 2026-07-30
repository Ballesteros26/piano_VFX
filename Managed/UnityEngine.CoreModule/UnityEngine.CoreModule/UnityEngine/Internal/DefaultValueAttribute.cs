using System;

namespace UnityEngine.Internal
{
	// Token: 0x0200030A RID: 778
	[AttributeUsage(18432)]
	[Serializable]
	public class DefaultValueAttribute : Attribute
	{
		// Token: 0x06001AA1 RID: 6817 RVA: 0x0002B9DC File Offset: 0x00029BDC
		public DefaultValueAttribute(string value)
		{
			this.DefaultValue = value;
		}

		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x06001AA2 RID: 6818 RVA: 0x0002B9F0 File Offset: 0x00029BF0
		public object Value
		{
			get
			{
				return this.DefaultValue;
			}
		}

		// Token: 0x06001AA3 RID: 6819 RVA: 0x0002BA08 File Offset: 0x00029C08
		public override bool Equals(object obj)
		{
			DefaultValueAttribute defaultValueAttribute = obj as DefaultValueAttribute;
			bool flag = defaultValueAttribute == null;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool flag3 = this.DefaultValue == null;
				if (flag3)
				{
					flag2 = defaultValueAttribute.Value == null;
				}
				else
				{
					flag2 = this.DefaultValue.Equals(defaultValueAttribute.Value);
				}
			}
			return flag2;
		}

		// Token: 0x06001AA4 RID: 6820 RVA: 0x0002BA58 File Offset: 0x00029C58
		public override int GetHashCode()
		{
			bool flag = this.DefaultValue == null;
			int num;
			if (flag)
			{
				num = base.GetHashCode();
			}
			else
			{
				num = this.DefaultValue.GetHashCode();
			}
			return num;
		}

		// Token: 0x04000835 RID: 2101
		private object DefaultValue;
	}
}
