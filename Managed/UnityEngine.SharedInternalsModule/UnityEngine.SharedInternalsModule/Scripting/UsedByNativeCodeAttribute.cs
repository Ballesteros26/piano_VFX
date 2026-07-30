using System;
using UnityEngine.Bindings;

namespace UnityEngine.Scripting
{
	// Token: 0x02000029 RID: 41
	[VisibleToOtherModules]
	[AttributeUsage(1532, Inherited = false)]
	internal class UsedByNativeCodeAttribute : Attribute
	{
		// Token: 0x06000084 RID: 132 RVA: 0x00002078 File Offset: 0x00000278
		public UsedByNativeCodeAttribute()
		{
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00002640 File Offset: 0x00000840
		public UsedByNativeCodeAttribute(string name)
		{
			this.Name = name;
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000086 RID: 134 RVA: 0x00002652 File Offset: 0x00000852
		// (set) Token: 0x06000087 RID: 135 RVA: 0x0000265A File Offset: 0x0000085A
		public string Name { get; set; }
	}
}
