using System;
using UnityEngine.Bindings;

namespace UnityEngine.Scripting
{
	// Token: 0x0200002A RID: 42
	[AttributeUsage(1532, Inherited = false)]
	[VisibleToOtherModules]
	internal class RequiredByNativeCodeAttribute : Attribute
	{
		// Token: 0x06000088 RID: 136 RVA: 0x00002078 File Offset: 0x00000278
		public RequiredByNativeCodeAttribute()
		{
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00002663 File Offset: 0x00000863
		public RequiredByNativeCodeAttribute(string name)
		{
			this.Name = name;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00002675 File Offset: 0x00000875
		public RequiredByNativeCodeAttribute(bool optional)
		{
			this.Optional = optional;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00002687 File Offset: 0x00000887
		public RequiredByNativeCodeAttribute(string name, bool optional)
		{
			this.Name = name;
			this.Optional = optional;
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600008C RID: 140 RVA: 0x000026A1 File Offset: 0x000008A1
		// (set) Token: 0x0600008D RID: 141 RVA: 0x000026A9 File Offset: 0x000008A9
		public string Name { get; set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600008E RID: 142 RVA: 0x000026B2 File Offset: 0x000008B2
		// (set) Token: 0x0600008F RID: 143 RVA: 0x000026BA File Offset: 0x000008BA
		public bool Optional { get; set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000090 RID: 144 RVA: 0x000026C3 File Offset: 0x000008C3
		// (set) Token: 0x06000091 RID: 145 RVA: 0x000026CB File Offset: 0x000008CB
		public bool GenerateProxy { get; set; }
	}
}
