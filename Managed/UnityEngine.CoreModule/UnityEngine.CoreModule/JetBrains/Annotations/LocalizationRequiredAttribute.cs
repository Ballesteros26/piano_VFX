using System;

namespace JetBrains.Annotations
{
	// Token: 0x02000081 RID: 129
	[AttributeUsage(32767, AllowMultiple = false, Inherited = true)]
	public sealed class LocalizationRequiredAttribute : Attribute
	{
		// Token: 0x060001A7 RID: 423 RVA: 0x00004034 File Offset: 0x00002234
		public LocalizationRequiredAttribute()
			: this(true)
		{
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x0000403F File Offset: 0x0000223F
		public LocalizationRequiredAttribute(bool required)
		{
			this.Required = required;
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x00004051 File Offset: 0x00002251
		// (set) Token: 0x060001AA RID: 426 RVA: 0x00004059 File Offset: 0x00002259
		public bool Required { get; private set; }
	}
}
