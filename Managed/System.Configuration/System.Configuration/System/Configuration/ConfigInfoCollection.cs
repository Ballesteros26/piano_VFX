using System;
using System.Collections;
using System.Collections.Specialized;

namespace System.Configuration
{
	// Token: 0x02000067 RID: 103
	internal class ConfigInfoCollection : NameObjectCollectionBase
	{
		// Token: 0x06000367 RID: 871 RVA: 0x00008A5E File Offset: 0x00006C5E
		public ConfigInfoCollection()
			: base(StringComparer.Ordinal)
		{
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000368 RID: 872 RVA: 0x00009E3C File Offset: 0x0000803C
		public ICollection AllKeys
		{
			get
			{
				return this.Keys;
			}
		}

		// Token: 0x170000FD RID: 253
		public ConfigInfo this[string name]
		{
			get
			{
				return (ConfigInfo)base.BaseGet(name);
			}
			set
			{
				base.BaseSet(name, value);
			}
		}

		// Token: 0x170000FE RID: 254
		public ConfigInfo this[int index]
		{
			get
			{
				return (ConfigInfo)base.BaseGet(index);
			}
			set
			{
				base.BaseSet(index, value);
			}
		}

		// Token: 0x0600036D RID: 877 RVA: 0x00009E74 File Offset: 0x00008074
		public void Add(string name, ConfigInfo config)
		{
			base.BaseAdd(name, config);
		}

		// Token: 0x0600036E RID: 878 RVA: 0x00009E7E File Offset: 0x0000807E
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x0600036F RID: 879 RVA: 0x00009E86 File Offset: 0x00008086
		public string GetKey(int index)
		{
			return base.BaseGetKey(index);
		}

		// Token: 0x06000370 RID: 880 RVA: 0x00009E8F File Offset: 0x0000808F
		public void Remove(string name)
		{
			base.BaseRemove(name);
		}

		// Token: 0x06000371 RID: 881 RVA: 0x00009E98 File Offset: 0x00008098
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}
	}
}
