using System;

namespace UnityEngine.Bindings
{
	// Token: 0x02000022 RID: 34
	[VisibleToOtherModules]
	[AttributeUsage(204)]
	internal class StaticAccessorAttribute : Attribute, IBindingsAttribute
	{
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00002522 File Offset: 0x00000722
		// (set) Token: 0x06000066 RID: 102 RVA: 0x0000252A File Offset: 0x0000072A
		public string Name { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00002533 File Offset: 0x00000733
		// (set) Token: 0x06000068 RID: 104 RVA: 0x0000253B File Offset: 0x0000073B
		public StaticAccessorType Type { get; set; }

		// Token: 0x06000069 RID: 105 RVA: 0x00002078 File Offset: 0x00000278
		public StaticAccessorAttribute()
		{
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00002544 File Offset: 0x00000744
		[VisibleToOtherModules]
		internal StaticAccessorAttribute(string name)
		{
			this.Name = name;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00002556 File Offset: 0x00000756
		public StaticAccessorAttribute(StaticAccessorType type)
		{
			this.Type = type;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00002568 File Offset: 0x00000768
		public StaticAccessorAttribute(string name, StaticAccessorType type)
		{
			this.Name = name;
			this.Type = type;
		}
	}
}
