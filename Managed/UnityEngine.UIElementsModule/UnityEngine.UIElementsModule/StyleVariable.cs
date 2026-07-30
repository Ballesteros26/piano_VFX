using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001CF RID: 463
	internal struct StyleVariable
	{
		// Token: 0x06000E8D RID: 3725 RVA: 0x00036B08 File Offset: 0x00034D08
		public override int GetHashCode()
		{
			int num = this.name.GetHashCode();
			num = (num * 397) ^ this.sheet.GetHashCode();
			return (num * 397) ^ this.handles.GetHashCode();
		}

		// Token: 0x040005E0 RID: 1504
		public string name;

		// Token: 0x040005E1 RID: 1505
		public StyleSheet sheet;

		// Token: 0x040005E2 RID: 1506
		public StyleValueHandle[] handles;
	}
}
