using System;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000737 RID: 1847
	internal sealed class TypeInformation
	{
		// Token: 0x17000CCD RID: 3277
		// (get) Token: 0x06004C79 RID: 19577 RVA: 0x00111652 File Offset: 0x0010F852
		internal string FullTypeName
		{
			get
			{
				return this.fullTypeName;
			}
		}

		// Token: 0x17000CCE RID: 3278
		// (get) Token: 0x06004C7A RID: 19578 RVA: 0x0011165A File Offset: 0x0010F85A
		internal string AssemblyString
		{
			get
			{
				return this.assemblyString;
			}
		}

		// Token: 0x17000CCF RID: 3279
		// (get) Token: 0x06004C7B RID: 19579 RVA: 0x00111662 File Offset: 0x0010F862
		internal bool HasTypeForwardedFrom
		{
			get
			{
				return this.hasTypeForwardedFrom;
			}
		}

		// Token: 0x06004C7C RID: 19580 RVA: 0x0011166A File Offset: 0x0010F86A
		internal TypeInformation(string fullTypeName, string assemblyString, bool hasTypeForwardedFrom)
		{
			this.fullTypeName = fullTypeName;
			this.assemblyString = assemblyString;
			this.hasTypeForwardedFrom = hasTypeForwardedFrom;
		}

		// Token: 0x040028DA RID: 10458
		private string fullTypeName;

		// Token: 0x040028DB RID: 10459
		private string assemblyString;

		// Token: 0x040028DC RID: 10460
		private bool hasTypeForwardedFrom;
	}
}
