using System;

namespace System.Web.Services.Configuration
{
	// Token: 0x02000147 RID: 327
	internal class TypeAndName
	{
		// Token: 0x060009F9 RID: 2553 RVA: 0x00043D8D File Offset: 0x00041F8D
		public TypeAndName(string name)
		{
			this.type = Type.GetType(name, true, true);
			this.name = name;
		}

		// Token: 0x060009FA RID: 2554 RVA: 0x00043DAA File Offset: 0x00041FAA
		public TypeAndName(Type type)
		{
			this.type = type;
		}

		// Token: 0x060009FB RID: 2555 RVA: 0x00043DB9 File Offset: 0x00041FB9
		public override int GetHashCode()
		{
			return this.type.GetHashCode();
		}

		// Token: 0x060009FC RID: 2556 RVA: 0x00043DC6 File Offset: 0x00041FC6
		public override bool Equals(object comparand)
		{
			return this.type.Equals(((TypeAndName)comparand).type);
		}

		// Token: 0x040005B1 RID: 1457
		public readonly Type type;

		// Token: 0x040005B2 RID: 1458
		public readonly string name;
	}
}
