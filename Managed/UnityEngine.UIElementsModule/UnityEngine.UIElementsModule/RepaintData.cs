using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200003A RID: 58
	internal class RepaintData
	{
		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000129 RID: 297 RVA: 0x00005FB8 File Offset: 0x000041B8
		// (set) Token: 0x0600012A RID: 298 RVA: 0x00005FC0 File Offset: 0x000041C0
		public Matrix4x4 currentOffset { get; set; } = Matrix4x4.identity;

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600012B RID: 299 RVA: 0x00005FC9 File Offset: 0x000041C9
		// (set) Token: 0x0600012C RID: 300 RVA: 0x00005FD1 File Offset: 0x000041D1
		public Vector2 mousePosition { get; set; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600012D RID: 301 RVA: 0x00005FDA File Offset: 0x000041DA
		// (set) Token: 0x0600012E RID: 302 RVA: 0x00005FE2 File Offset: 0x000041E2
		public Rect currentWorldClip { get; set; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600012F RID: 303 RVA: 0x00005FEB File Offset: 0x000041EB
		// (set) Token: 0x06000130 RID: 304 RVA: 0x00005FF3 File Offset: 0x000041F3
		public Event repaintEvent { get; set; }
	}
}
