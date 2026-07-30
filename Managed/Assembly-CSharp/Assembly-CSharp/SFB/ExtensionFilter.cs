using System;

namespace SFB
{
	// Token: 0x0200002A RID: 42
	public struct ExtensionFilter
	{
		// Token: 0x0600017D RID: 381 RVA: 0x00012869 File Offset: 0x00010A69
		public ExtensionFilter(string filterName, params string[] filterExtensions)
		{
			this.Name = filterName;
			this.Extensions = filterExtensions;
		}

		// Token: 0x040003AB RID: 939
		public string Name;

		// Token: 0x040003AC RID: 940
		public string[] Extensions;
	}
}
