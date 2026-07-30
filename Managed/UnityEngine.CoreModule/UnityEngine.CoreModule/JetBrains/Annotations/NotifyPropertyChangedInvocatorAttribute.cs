using System;

namespace JetBrains.Annotations
{
	// Token: 0x0200007F RID: 127
	[AttributeUsage(64, AllowMultiple = false, Inherited = true)]
	public sealed class NotifyPropertyChangedInvocatorAttribute : Attribute
	{
		// Token: 0x0600019D RID: 413 RVA: 0x00002059 File Offset: 0x00000259
		public NotifyPropertyChangedInvocatorAttribute()
		{
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00003FC9 File Offset: 0x000021C9
		public NotifyPropertyChangedInvocatorAttribute(string parameterName)
		{
			this.ParameterName = parameterName;
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600019F RID: 415 RVA: 0x00003FDB File Offset: 0x000021DB
		// (set) Token: 0x060001A0 RID: 416 RVA: 0x00003FE3 File Offset: 0x000021E3
		public string ParameterName { get; private set; }
	}
}
