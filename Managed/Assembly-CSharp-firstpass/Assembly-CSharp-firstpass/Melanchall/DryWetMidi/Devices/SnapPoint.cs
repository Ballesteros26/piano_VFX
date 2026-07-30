using System;

namespace Melanchall.DryWetMidi.Devices
{
	// Token: 0x0200010A RID: 266
	public class SnapPoint
	{
		// Token: 0x06000714 RID: 1812 RVA: 0x0001C3DD File Offset: 0x0001A5DD
		internal SnapPoint(TimeSpan time)
		{
			this.Time = time;
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000715 RID: 1813 RVA: 0x0001C3F3 File Offset: 0x0001A5F3
		// (set) Token: 0x06000716 RID: 1814 RVA: 0x0001C3FB File Offset: 0x0001A5FB
		public bool IsEnabled { get; set; } = true;

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000717 RID: 1815 RVA: 0x0001C404 File Offset: 0x0001A604
		public TimeSpan Time { get; }

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000718 RID: 1816 RVA: 0x0001C40C File Offset: 0x0001A60C
		// (set) Token: 0x06000719 RID: 1817 RVA: 0x0001C414 File Offset: 0x0001A614
		public SnapPointsGroup SnapPointsGroup { get; internal set; }
	}
}
