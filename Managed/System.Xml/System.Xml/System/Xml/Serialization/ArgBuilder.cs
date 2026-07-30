using System;

namespace System.Xml.Serialization
{
	// Token: 0x020002CA RID: 714
	internal class ArgBuilder
	{
		// Token: 0x06001B08 RID: 6920 RVA: 0x00096835 File Offset: 0x00094A35
		internal ArgBuilder(string name, int index, Type argType)
		{
			this.Name = name;
			this.Index = index;
			this.ArgType = argType;
		}

		// Token: 0x040015B3 RID: 5555
		internal string Name;

		// Token: 0x040015B4 RID: 5556
		internal int Index;

		// Token: 0x040015B5 RID: 5557
		internal Type ArgType;
	}
}
