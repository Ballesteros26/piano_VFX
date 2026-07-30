using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Net
{
	// Token: 0x0200003E RID: 62
	internal static class DebugThreadTracking
	{
		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000233 RID: 563 RVA: 0x0000D2D8 File Offset: 0x0000B4D8
		private static Stack<ThreadKinds> ThreadKindStack
		{
			get
			{
				Stack<ThreadKinds> stack;
				if ((stack = DebugThreadTracking.t_threadKindStack) == null)
				{
					stack = (DebugThreadTracking.t_threadKindStack = new Stack<ThreadKinds>());
				}
				return stack;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000234 RID: 564 RVA: 0x0000D2EE File Offset: 0x0000B4EE
		internal static ThreadKinds CurrentThreadKind
		{
			get
			{
				if (DebugThreadTracking.ThreadKindStack.Count <= 0)
				{
					return ThreadKinds.Other;
				}
				return DebugThreadTracking.ThreadKindStack.Peek();
			}
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000D310 File Offset: 0x0000B510
		internal static IDisposable SetThreadKind(ThreadKinds kind)
		{
			if ((kind & ThreadKinds.SourceMask) != ThreadKinds.Unknown)
			{
				throw new InternalException();
			}
			if (Environment.HasShutdownStarted)
			{
				return null;
			}
			ThreadKinds currentThreadKind = DebugThreadTracking.CurrentThreadKind;
			ThreadKinds threadKinds = currentThreadKind & ThreadKinds.SourceMask;
			if ((currentThreadKind & ThreadKinds.User) != ThreadKinds.Unknown && (kind & ThreadKinds.System) != ThreadKinds.Unknown && NetEventSource.IsEnabled)
			{
				NetEventSource.Error(null, "Thread changed from User to System; user's thread shouldn't be hijacked.", "SetThreadKind");
			}
			if ((currentThreadKind & ThreadKinds.Async) != ThreadKinds.Unknown && (kind & ThreadKinds.Sync) != ThreadKinds.Unknown)
			{
				if (NetEventSource.IsEnabled)
				{
					NetEventSource.Error(null, "Thread changed from Async to Sync, may block an Async thread.", "SetThreadKind");
				}
			}
			else if ((currentThreadKind & (ThreadKinds.CompletionPort | ThreadKinds.Other)) == ThreadKinds.Unknown && (kind & ThreadKinds.Sync) != ThreadKinds.Unknown && NetEventSource.IsEnabled)
			{
				NetEventSource.Error(null, "Thread from a limited resource changed to Sync, may deadlock or bottleneck.", "SetThreadKind");
			}
			DebugThreadTracking.ThreadKindStack.Push(((((kind & ThreadKinds.OwnerMask) == ThreadKinds.Unknown) ? currentThreadKind : kind) & ThreadKinds.OwnerMask) | ((((kind & ThreadKinds.SyncMask) == ThreadKinds.Unknown) ? currentThreadKind : kind) & ThreadKinds.SyncMask) | (kind & ~(ThreadKinds.User | ThreadKinds.System | ThreadKinds.Sync | ThreadKinds.Async)) | threadKinds);
			if (DebugThreadTracking.CurrentThreadKind != currentThreadKind && NetEventSource.IsEnabled)
			{
				NetEventSource.Info(null, FormattableStringFactory.Create("Thread becomes:({0})", new object[] { DebugThreadTracking.CurrentThreadKind }), "SetThreadKind");
			}
			return new DebugThreadTracking.ThreadKindFrame();
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000D414 File Offset: 0x0000B614
		internal static void SetThreadSource(ThreadKinds source)
		{
			if ((source & ThreadKinds.SourceMask) != source || source == ThreadKinds.Unknown)
			{
				throw new ArgumentException("Must specify the thread source.", "source");
			}
			if (DebugThreadTracking.ThreadKindStack.Count == 0)
			{
				DebugThreadTracking.ThreadKindStack.Push(source);
				return;
			}
			if (DebugThreadTracking.ThreadKindStack.Count > 1)
			{
				if (NetEventSource.IsEnabled)
				{
					NetEventSource.Error(null, "SetThreadSource must be called at the base of the stack, or the stack has been corrupted.", "SetThreadSource");
				}
				while (DebugThreadTracking.ThreadKindStack.Count > 1)
				{
					DebugThreadTracking.ThreadKindStack.Pop();
				}
			}
			if (DebugThreadTracking.ThreadKindStack.Peek() != source)
			{
				if (NetEventSource.IsEnabled)
				{
					NetEventSource.Error(null, "The stack has been corrupted.", "SetThreadSource");
				}
				ThreadKinds threadKinds = DebugThreadTracking.ThreadKindStack.Pop() & ThreadKinds.SourceMask;
				if (threadKinds != source && threadKinds != ThreadKinds.Other && NetEventSource.IsEnabled)
				{
					NetEventSource.Fail(null, FormattableStringFactory.Create("Thread source changed.|Was:({0}) Now:({1})", new object[] { threadKinds, source }), "SetThreadSource");
				}
				DebugThreadTracking.ThreadKindStack.Push(source);
			}
		}

		// Token: 0x04000466 RID: 1126
		[ThreadStatic]
		private static Stack<ThreadKinds> t_threadKindStack;

		// Token: 0x0200003F RID: 63
		private class ThreadKindFrame : IDisposable
		{
			// Token: 0x06000237 RID: 567 RVA: 0x0000D514 File Offset: 0x0000B714
			internal ThreadKindFrame()
			{
				this._frameNumber = DebugThreadTracking.ThreadKindStack.Count;
			}

			// Token: 0x06000238 RID: 568 RVA: 0x0000D52C File Offset: 0x0000B72C
			void IDisposable.Dispose()
			{
				if (Environment.HasShutdownStarted)
				{
					return;
				}
				if (this._frameNumber != DebugThreadTracking.ThreadKindStack.Count)
				{
					throw new InternalException();
				}
				ThreadKinds threadKinds = DebugThreadTracking.ThreadKindStack.Pop();
				if (DebugThreadTracking.CurrentThreadKind != threadKinds && NetEventSource.IsEnabled && NetEventSource.IsEnabled)
				{
					NetEventSource.Info(this, FormattableStringFactory.Create("Thread reverts:({0})", new object[] { DebugThreadTracking.CurrentThreadKind }), "Dispose");
				}
			}

			// Token: 0x04000467 RID: 1127
			private readonly int _frameNumber;
		}
	}
}
