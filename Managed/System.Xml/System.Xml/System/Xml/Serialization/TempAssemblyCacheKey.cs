using System;

namespace System.Xml.Serialization
{
	// Token: 0x020002D8 RID: 728
	internal class TempAssemblyCacheKey
	{
		// Token: 0x06001B5F RID: 7007 RVA: 0x00098563 File Offset: 0x00096763
		internal TempAssemblyCacheKey(string ns, object type)
		{
			this.type = type;
			this.ns = ns;
		}

		// Token: 0x06001B60 RID: 7008 RVA: 0x0009857C File Offset: 0x0009677C
		public override bool Equals(object o)
		{
			TempAssemblyCacheKey tempAssemblyCacheKey = o as TempAssemblyCacheKey;
			return tempAssemblyCacheKey != null && tempAssemblyCacheKey.type == this.type && tempAssemblyCacheKey.ns == this.ns;
		}

		// Token: 0x06001B61 RID: 7009 RVA: 0x000985B6 File Offset: 0x000967B6
		public override int GetHashCode()
		{
			return ((this.ns != null) ? this.ns.GetHashCode() : 0) ^ ((this.type != null) ? this.type.GetHashCode() : 0);
		}

		// Token: 0x040015E3 RID: 5603
		private string ns;

		// Token: 0x040015E4 RID: 5604
		private object type;
	}
}
