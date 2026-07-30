using System;

namespace Melanchall.DryWetMidi.Devices
{
	// Token: 0x0200010B RID: 267
	public sealed class SnapPoint<TData> : SnapPoint
	{
		// Token: 0x0600071A RID: 1818 RVA: 0x0001C41D File Offset: 0x0001A61D
		internal SnapPoint(TimeSpan time, TData data)
			: base(time)
		{
			this.Data = data;
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x0600071B RID: 1819 RVA: 0x0001C42D File Offset: 0x0001A62D
		public TData Data { get; }
	}
}
