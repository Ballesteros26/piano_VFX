using System;
using System.Collections.Generic;

namespace System.Web
{
	// Token: 0x0200006A RID: 106
	internal sealed class DynamicModuleManager
	{
		// Token: 0x06000443 RID: 1091 RVA: 0x00008B84 File Offset: 0x00006D84
		public void Add(Type moduleType)
		{
			if (moduleType == null)
			{
				throw new ArgumentException("moduleType");
			}
			if (!typeof(IHttpModule).IsAssignableFrom(moduleType))
			{
				throw new ArgumentException("Given object does not implement IHttpModule.", "moduleType");
			}
			object obj = this.mutex;
			lock (obj)
			{
				if (this.entriesAreReadOnly)
				{
					throw new InvalidOperationException("A module was to be added to the dynamic module list, but the list was already initialized. The dynamic module list can only be initialized once.");
				}
				this.entries.Add(new DynamicModuleInfo(moduleType, string.Format("__Module__{0}_{1}", moduleType.AssemblyQualifiedName, Guid.NewGuid())));
			}
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x00008C34 File Offset: 0x00006E34
		public ICollection<DynamicModuleInfo> LockAndGetModules()
		{
			object obj = this.mutex;
			ICollection<DynamicModuleInfo> collection;
			lock (obj)
			{
				this.entriesAreReadOnly = true;
				collection = this.entries;
			}
			return collection;
		}

		// Token: 0x04000E5D RID: 3677
		private const string moduleNameFormat = "__Module__{0}_{1}";

		// Token: 0x04000E5E RID: 3678
		private readonly List<DynamicModuleInfo> entries = new List<DynamicModuleInfo>();

		// Token: 0x04000E5F RID: 3679
		private bool entriesAreReadOnly;

		// Token: 0x04000E60 RID: 3680
		private readonly object mutex = new object();
	}
}
