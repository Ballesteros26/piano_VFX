using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Scripting
{
	// Token: 0x0200026A RID: 618
	[NativeHeader("Runtime/Scripting/GarbageCollector.h")]
	public static class GarbageCollector
	{
		// Token: 0x14000020 RID: 32
		// (add) Token: 0x060019E3 RID: 6627 RVA: 0x0002A5BC File Offset: 0x000287BC
		// (remove) Token: 0x060019E4 RID: 6628 RVA: 0x0002A5F0 File Offset: 0x000287F0
		[field: DebuggerBrowsable(0)]
		public static event Action<GarbageCollector.Mode> GCModeChanged;

		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x060019E5 RID: 6629 RVA: 0x0002A624 File Offset: 0x00028824
		// (set) Token: 0x060019E6 RID: 6630 RVA: 0x0002A63C File Offset: 0x0002883C
		public static GarbageCollector.Mode GCMode
		{
			get
			{
				return GarbageCollector.GetMode();
			}
			set
			{
				bool flag = value == GarbageCollector.GetMode();
				if (!flag)
				{
					GarbageCollector.SetMode(value);
					bool flag2 = GarbageCollector.GCModeChanged != null;
					if (flag2)
					{
						GarbageCollector.GCModeChanged.Invoke(value);
					}
				}
			}
		}

		// Token: 0x060019E7 RID: 6631
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern void SetMode(GarbageCollector.Mode mode);

		// Token: 0x060019E8 RID: 6632
		[MethodImpl(4096)]
		private static extern GarbageCollector.Mode GetMode();

		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x060019E9 RID: 6633
		public static extern bool isIncremental
		{
			[NativeMethod("GetIncrementalEnabled")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x060019EA RID: 6634
		// (set) Token: 0x060019EB RID: 6635
		public static extern ulong incrementalTimeSliceNanoseconds
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x060019EC RID: 6636
		[NativeMethod("CollectIncrementalWrapper")]
		[NativeThrows]
		[MethodImpl(4096)]
		public static extern bool CollectIncremental(ulong nanoseconds);

		// Token: 0x0200026B RID: 619
		public enum Mode
		{
			// Token: 0x040007F2 RID: 2034
			Disabled,
			// Token: 0x040007F3 RID: 2035
			Enabled
		}
	}
}
