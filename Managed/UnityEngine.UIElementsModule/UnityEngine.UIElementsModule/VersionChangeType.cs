using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000036 RID: 54
	[Flags]
	internal enum VersionChangeType
	{
		// Token: 0x04000088 RID: 136
		Bindings = 1,
		// Token: 0x04000089 RID: 137
		ViewData = 2,
		// Token: 0x0400008A RID: 138
		Hierarchy = 4,
		// Token: 0x0400008B RID: 139
		Layout = 8,
		// Token: 0x0400008C RID: 140
		StyleSheet = 16,
		// Token: 0x0400008D RID: 141
		Styles = 32,
		// Token: 0x0400008E RID: 142
		Overflow = 64,
		// Token: 0x0400008F RID: 143
		BorderRadius = 128,
		// Token: 0x04000090 RID: 144
		BorderWidth = 256,
		// Token: 0x04000091 RID: 145
		Transform = 512,
		// Token: 0x04000092 RID: 146
		Size = 1024,
		// Token: 0x04000093 RID: 147
		Repaint = 2048,
		// Token: 0x04000094 RID: 148
		Opacity = 4096
	}
}
