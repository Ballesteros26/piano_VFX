using System;
using System.Runtime.InteropServices;
using System.Threading;
using Mono.Unix.Native;

namespace Mono.Unix
{
	// Token: 0x02000020 RID: 32
	public class UnixSignal : WaitHandle
	{
		// Token: 0x06000195 RID: 405 RVA: 0x000068B2 File Offset: 0x00004AB2
		static UnixSignal()
		{
			Stdlib.VersionCheck();
		}

		// Token: 0x06000196 RID: 406 RVA: 0x000068CC File Offset: 0x00004ACC
		public UnixSignal(Signum signum)
		{
			this.signum = NativeConvert.FromSignum(signum);
			this.signal_info = UnixSignal.install(this.signum);
			if (this.signal_info == IntPtr.Zero)
			{
				throw new ArgumentException("Unable to handle signal", "signum");
			}
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00006920 File Offset: 0x00004B20
		public UnixSignal(RealTimeSignum rtsig)
		{
			this.signum = NativeConvert.FromRealTimeSignum(rtsig);
			this.signal_info = UnixSignal.install(this.signum);
			Errno lastError = Stdlib.GetLastError();
			if (!(this.signal_info == IntPtr.Zero))
			{
				return;
			}
			if (lastError == Errno.EADDRINUSE)
			{
				throw new ArgumentException("Signal registered outside of Mono.Posix", "signum");
			}
			throw new ArgumentException("Unable to handle signal", "signum");
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000198 RID: 408 RVA: 0x0000698D File Offset: 0x00004B8D
		public Signum Signum
		{
			get
			{
				if (this.IsRealTimeSignal)
				{
					throw new InvalidOperationException("This signal is a RealTimeSignum");
				}
				return NativeConvert.ToSignum(this.signum);
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000199 RID: 409 RVA: 0x000069AD File Offset: 0x00004BAD
		public RealTimeSignum RealTimeSignum
		{
			get
			{
				if (!this.IsRealTimeSignal)
				{
					throw new InvalidOperationException("This signal is not a RealTimeSignum");
				}
				return NativeConvert.ToRealTimeSignum(this.signum - UnixSignal.GetSIGRTMIN());
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600019A RID: 410 RVA: 0x000069D4 File Offset: 0x00004BD4
		public bool IsRealTimeSignal
		{
			get
			{
				this.AssertValid();
				int sigrtmin = UnixSignal.GetSIGRTMIN();
				return sigrtmin != -1 && this.signum >= sigrtmin;
			}
		}

		// Token: 0x0600019B RID: 411
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Unix_UnixSignal_install", SetLastError = true)]
		private static extern IntPtr install(int signum);

		// Token: 0x0600019C RID: 412
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Unix_UnixSignal_uninstall")]
		private static extern int uninstall(IntPtr info);

		// Token: 0x0600019D RID: 413 RVA: 0x000069FF File Offset: 0x00004BFF
		private static int RuntimeShuttingDownCallback()
		{
			if (!Environment.HasShutdownStarted)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x0600019E RID: 414
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Unix_UnixSignal_WaitAny")]
		private static extern int WaitAny(IntPtr[] infos, int count, int timeout, UnixSignal.Mono_Posix_RuntimeIsShuttingDown shutting_down);

		// Token: 0x0600019F RID: 415
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_SIGRTMIN")]
		internal static extern int GetSIGRTMIN();

		// Token: 0x060001A0 RID: 416
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_SIGRTMAX")]
		internal static extern int GetSIGRTMAX();

		// Token: 0x060001A1 RID: 417 RVA: 0x00006A0B File Offset: 0x00004C0B
		private void AssertValid()
		{
			if (this.signal_info == IntPtr.Zero)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x00006A30 File Offset: 0x00004C30
		private unsafe UnixSignal.SignalInfo* Info
		{
			get
			{
				this.AssertValid();
				return (UnixSignal.SignalInfo*)(void*)this.signal_info;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x00006A43 File Offset: 0x00004C43
		public bool IsSet
		{
			get
			{
				return this.Count > 0;
			}
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00006A4E File Offset: 0x00004C4E
		public unsafe bool Reset()
		{
			return Interlocked.Exchange(ref this.Info->count, 0) != 0;
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x00006A64 File Offset: 0x00004C64
		// (set) Token: 0x060001A6 RID: 422 RVA: 0x00006A71 File Offset: 0x00004C71
		public unsafe int Count
		{
			get
			{
				return this.Info->count;
			}
			set
			{
				Interlocked.Exchange(ref this.Info->count, value);
			}
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00006A85 File Offset: 0x00004C85
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (this.signal_info == IntPtr.Zero)
			{
				return;
			}
			UnixSignal.uninstall(this.signal_info);
			this.signal_info = IntPtr.Zero;
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00006AB8 File Offset: 0x00004CB8
		public override bool WaitOne()
		{
			return this.WaitOne(-1, false);
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00006AC4 File Offset: 0x00004CC4
		public override bool WaitOne(TimeSpan timeout, bool exitContext)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout");
			}
			return this.WaitOne((int)num, exitContext);
		}

		// Token: 0x060001AA RID: 426 RVA: 0x00006AFC File Offset: 0x00004CFC
		public override bool WaitOne(int millisecondsTimeout, bool exitContext)
		{
			this.AssertValid();
			if (exitContext)
			{
				throw new InvalidOperationException("exitContext is not supported");
			}
			if (millisecondsTimeout == 0)
			{
				return this.IsSet;
			}
			return UnixSignal.WaitAny(new UnixSignal[] { this }, millisecondsTimeout) == 0;
		}

		// Token: 0x060001AB RID: 427 RVA: 0x00006B2F File Offset: 0x00004D2F
		public static int WaitAny(UnixSignal[] signals)
		{
			return UnixSignal.WaitAny(signals, -1);
		}

		// Token: 0x060001AC RID: 428 RVA: 0x00006B38 File Offset: 0x00004D38
		public static int WaitAny(UnixSignal[] signals, TimeSpan timeout)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout");
			}
			return UnixSignal.WaitAny(signals, (int)num);
		}

		// Token: 0x060001AD RID: 429 RVA: 0x00006B70 File Offset: 0x00004D70
		public static int WaitAny(UnixSignal[] signals, int millisecondsTimeout)
		{
			if (signals == null)
			{
				throw new ArgumentNullException("signals");
			}
			if (millisecondsTimeout < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeout");
			}
			IntPtr[] array = new IntPtr[signals.Length];
			for (int i = 0; i < signals.Length; i++)
			{
				array[i] = signals[i].signal_info;
				if (array[i] == IntPtr.Zero)
				{
					throw new InvalidOperationException("Disposed UnixSignal");
				}
			}
			return UnixSignal.WaitAny(array, array.Length, millisecondsTimeout, UnixSignal.ShuttingDown);
		}

		// Token: 0x04000084 RID: 132
		private int signum;

		// Token: 0x04000085 RID: 133
		private IntPtr signal_info;

		// Token: 0x04000086 RID: 134
		private static UnixSignal.Mono_Posix_RuntimeIsShuttingDown ShuttingDown = new UnixSignal.Mono_Posix_RuntimeIsShuttingDown(UnixSignal.RuntimeShuttingDownCallback);

		// Token: 0x020000A3 RID: 163
		// (Invoke) Token: 0x06000766 RID: 1894
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate int Mono_Posix_RuntimeIsShuttingDown();

		// Token: 0x020000A4 RID: 164
		[Map]
		private struct SignalInfo
		{
			// Token: 0x0400054D RID: 1357
			public int signum;

			// Token: 0x0400054E RID: 1358
			public int count;

			// Token: 0x0400054F RID: 1359
			public int read_fd;

			// Token: 0x04000550 RID: 1360
			public int write_fd;

			// Token: 0x04000551 RID: 1361
			public int pipecnt;

			// Token: 0x04000552 RID: 1362
			public int pipelock;

			// Token: 0x04000553 RID: 1363
			public int have_handler;

			// Token: 0x04000554 RID: 1364
			public IntPtr handler;
		}
	}
}
