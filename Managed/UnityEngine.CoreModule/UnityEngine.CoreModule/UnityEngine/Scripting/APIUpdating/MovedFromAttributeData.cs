using System;

namespace UnityEngine.Scripting.APIUpdating
{
	// Token: 0x0200026D RID: 621
	internal struct MovedFromAttributeData
	{
		// Token: 0x060019EE RID: 6638 RVA: 0x0002A678 File Offset: 0x00028878
		public void Set(bool autoUpdateAPI, string sourceNamespace = null, string sourceAssembly = null, string sourceClassName = null)
		{
			this.className = sourceClassName;
			this.classHasChanged = this.className != null;
			this.nameSpace = sourceNamespace;
			this.nameSpaceHasChanged = this.nameSpace != null;
			this.assembly = sourceAssembly;
			this.assemblyHasChanged = this.assembly != null;
			this.autoUdpateAPI = autoUpdateAPI;
		}

		// Token: 0x040007F4 RID: 2036
		public string className;

		// Token: 0x040007F5 RID: 2037
		public string nameSpace;

		// Token: 0x040007F6 RID: 2038
		public string assembly;

		// Token: 0x040007F7 RID: 2039
		public bool classHasChanged;

		// Token: 0x040007F8 RID: 2040
		public bool nameSpaceHasChanged;

		// Token: 0x040007F9 RID: 2041
		public bool assemblyHasChanged;

		// Token: 0x040007FA RID: 2042
		public bool autoUdpateAPI;
	}
}
