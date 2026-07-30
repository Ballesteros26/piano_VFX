using System;
using System.Diagnostics.Tracing;
using System.Threading;

namespace System.Data
{
	// Token: 0x02000054 RID: 84
	[EventSource(Name = "System.Data.DataCommonEventSource")]
	internal class DataCommonEventSource : EventSource
	{
		// Token: 0x060002A2 RID: 674 RVA: 0x0000F12A File Offset: 0x0000D32A
		[Event(1, Level = EventLevel.Informational)]
		internal void Trace(string message)
		{
			base.WriteEvent(1, message);
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0000F134 File Offset: 0x0000D334
		[NonEvent]
		internal void Trace<T0>(string format, T0 arg0)
		{
			if (!DataCommonEventSource.Log.IsEnabled())
			{
				return;
			}
			this.Trace(string.Format(format, arg0));
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0000F155 File Offset: 0x0000D355
		[NonEvent]
		internal void Trace<T0, T1>(string format, T0 arg0, T1 arg1)
		{
			if (!DataCommonEventSource.Log.IsEnabled())
			{
				return;
			}
			this.Trace(string.Format(format, arg0, arg1));
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0000F17C File Offset: 0x0000D37C
		[NonEvent]
		internal void Trace<T0, T1, T2>(string format, T0 arg0, T1 arg1, T2 arg2)
		{
			if (!DataCommonEventSource.Log.IsEnabled())
			{
				return;
			}
			this.Trace(string.Format(format, arg0, arg1, arg2));
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x0000F1AC File Offset: 0x0000D3AC
		[NonEvent]
		internal void Trace<T0, T1, T2, T3>(string format, T0 arg0, T1 arg1, T2 arg2, T3 arg3)
		{
			if (!DataCommonEventSource.Log.IsEnabled())
			{
				return;
			}
			this.Trace(string.Format(format, new object[] { arg0, arg1, arg2, arg3 }));
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x0000F200 File Offset: 0x0000D400
		[NonEvent]
		internal void Trace<T0, T1, T2, T3, T4>(string format, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
		{
			if (!DataCommonEventSource.Log.IsEnabled())
			{
				return;
			}
			this.Trace(string.Format(format, new object[] { arg0, arg1, arg2, arg3, arg4 }));
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0000F25C File Offset: 0x0000D45C
		[NonEvent]
		internal void Trace<T0, T1, T2, T3, T4, T5, T6>(string format, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
		{
			if (!DataCommonEventSource.Log.IsEnabled())
			{
				return;
			}
			this.Trace(string.Format(format, new object[] { arg0, arg1, arg2, arg3, arg4, arg5, arg6 }));
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0000F2CC File Offset: 0x0000D4CC
		[Event(2, Level = EventLevel.Verbose)]
		internal long EnterScope(string message)
		{
			long num = 0L;
			if (DataCommonEventSource.Log.IsEnabled())
			{
				num = Interlocked.Increment(ref DataCommonEventSource.s_nextScopeId);
				base.WriteEvent(2, num, message);
			}
			return num;
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0000F2FD File Offset: 0x0000D4FD
		[NonEvent]
		internal long EnterScope<T1>(string format, T1 arg1)
		{
			if (!DataCommonEventSource.Log.IsEnabled())
			{
				return 0L;
			}
			return this.EnterScope(string.Format(format, arg1));
		}

		// Token: 0x060002AB RID: 683 RVA: 0x0000F320 File Offset: 0x0000D520
		[NonEvent]
		internal long EnterScope<T1, T2>(string format, T1 arg1, T2 arg2)
		{
			if (!DataCommonEventSource.Log.IsEnabled())
			{
				return 0L;
			}
			return this.EnterScope(string.Format(format, arg1, arg2));
		}

		// Token: 0x060002AC RID: 684 RVA: 0x0000F349 File Offset: 0x0000D549
		[NonEvent]
		internal long EnterScope<T1, T2, T3>(string format, T1 arg1, T2 arg2, T3 arg3)
		{
			if (!DataCommonEventSource.Log.IsEnabled())
			{
				return 0L;
			}
			return this.EnterScope(string.Format(format, arg1, arg2, arg3));
		}

		// Token: 0x060002AD RID: 685 RVA: 0x0000F37C File Offset: 0x0000D57C
		[NonEvent]
		internal long EnterScope<T1, T2, T3, T4>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
		{
			if (!DataCommonEventSource.Log.IsEnabled())
			{
				return 0L;
			}
			return this.EnterScope(string.Format(format, new object[] { arg1, arg2, arg3, arg4 }));
		}

		// Token: 0x060002AE RID: 686 RVA: 0x0000F3D0 File Offset: 0x0000D5D0
		[Event(3, Level = EventLevel.Verbose)]
		internal void ExitScope(long scopeId)
		{
			base.WriteEvent(3, scopeId);
		}

		// Token: 0x040004F5 RID: 1269
		internal static readonly DataCommonEventSource Log = new DataCommonEventSource();

		// Token: 0x040004F6 RID: 1270
		private static long s_nextScopeId = 0L;

		// Token: 0x040004F7 RID: 1271
		private const int TraceEventId = 1;

		// Token: 0x040004F8 RID: 1272
		private const int EnterScopeId = 2;

		// Token: 0x040004F9 RID: 1273
		private const int ExitScopeId = 3;
	}
}
