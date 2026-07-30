using System;

namespace UnityEngine.Scripting.APIUpdating
{
	// Token: 0x0200026E RID: 622
	[AttributeUsage(5148)]
	public class MovedFromAttribute : Attribute
	{
		// Token: 0x060019EF RID: 6639 RVA: 0x0002A6D0 File Offset: 0x000288D0
		public MovedFromAttribute(bool autoUpdateAPI, string sourceNamespace = null, string sourceAssembly = null, string sourceClassName = null)
		{
			this.data.Set(autoUpdateAPI, sourceNamespace, sourceAssembly, sourceClassName);
		}

		// Token: 0x060019F0 RID: 6640 RVA: 0x0002A6EB File Offset: 0x000288EB
		public MovedFromAttribute(string sourceNamespace)
		{
			this.data.Set(true, sourceNamespace, null, null);
		}

		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x060019F1 RID: 6641 RVA: 0x0002A708 File Offset: 0x00028908
		internal bool AffectsAPIUpdater
		{
			get
			{
				return !this.data.classHasChanged && !this.data.assemblyHasChanged;
			}
		}

		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x060019F2 RID: 6642 RVA: 0x0002A738 File Offset: 0x00028938
		public bool IsInDifferentAssembly
		{
			get
			{
				return this.data.assemblyHasChanged;
			}
		}

		// Token: 0x040007FB RID: 2043
		internal MovedFromAttributeData data;
	}
}
