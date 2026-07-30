using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Security;
using System.Security.Util;
using System.Threading;

namespace System
{
	// Token: 0x020001BF RID: 447
	internal sealed class SharedStatics
	{
		// Token: 0x060012FE RID: 4862 RVA: 0x00002111 File Offset: 0x00000311
		private SharedStatics()
		{
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x060012FF RID: 4863 RVA: 0x0004D738 File Offset: 0x0004B938
		public static string Remoting_Identity_IDGuid
		{
			[SecuritySafeCritical]
			get
			{
				if (SharedStatics._sharedStatics._Remoting_Identity_IDGuid == null)
				{
					bool flag = false;
					RuntimeHelpers.PrepareConstrainedRegions();
					try
					{
						Monitor.Enter(SharedStatics._sharedStatics, ref flag);
						if (SharedStatics._sharedStatics._Remoting_Identity_IDGuid == null)
						{
							SharedStatics._sharedStatics._Remoting_Identity_IDGuid = Guid.NewGuid().ToString().Replace('-', '_');
						}
					}
					finally
					{
						if (flag)
						{
							Monitor.Exit(SharedStatics._sharedStatics);
						}
					}
				}
				return SharedStatics._sharedStatics._Remoting_Identity_IDGuid;
			}
		}

		// Token: 0x06001300 RID: 4864 RVA: 0x0004D7C8 File Offset: 0x0004B9C8
		[SecuritySafeCritical]
		public static Tokenizer.StringMaker GetSharedStringMaker()
		{
			Tokenizer.StringMaker stringMaker = null;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				Monitor.Enter(SharedStatics._sharedStatics, ref flag);
				if (SharedStatics._sharedStatics._maker != null)
				{
					stringMaker = SharedStatics._sharedStatics._maker;
					SharedStatics._sharedStatics._maker = null;
				}
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(SharedStatics._sharedStatics);
				}
			}
			if (stringMaker == null)
			{
				stringMaker = new Tokenizer.StringMaker();
			}
			return stringMaker;
		}

		// Token: 0x06001301 RID: 4865 RVA: 0x0004D838 File Offset: 0x0004BA38
		[SecuritySafeCritical]
		public static void ReleaseSharedStringMaker(ref Tokenizer.StringMaker maker)
		{
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				Monitor.Enter(SharedStatics._sharedStatics, ref flag);
				SharedStatics._sharedStatics._maker = maker;
				maker = null;
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(SharedStatics._sharedStatics);
				}
			}
		}

		// Token: 0x06001302 RID: 4866 RVA: 0x0004D888 File Offset: 0x0004BA88
		internal static int Remoting_Identity_GetNextSeqNum()
		{
			return Interlocked.Increment(ref SharedStatics._sharedStatics._Remoting_Identity_IDSeqNum);
		}

		// Token: 0x06001303 RID: 4867 RVA: 0x0004D899 File Offset: 0x0004BA99
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal static long AddMemoryFailPointReservation(long size)
		{
			return Interlocked.Add(ref SharedStatics._sharedStatics._memFailPointReservedMemory, size);
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06001304 RID: 4868 RVA: 0x0004D8AB File Offset: 0x0004BAAB
		internal static ulong MemoryFailPointReservedMemory
		{
			get
			{
				return (ulong)Volatile.Read(ref SharedStatics._sharedStatics._memFailPointReservedMemory);
			}
		}

		// Token: 0x04000ACF RID: 2767
		private static readonly SharedStatics _sharedStatics = new SharedStatics();

		// Token: 0x04000AD0 RID: 2768
		private volatile string _Remoting_Identity_IDGuid;

		// Token: 0x04000AD1 RID: 2769
		private Tokenizer.StringMaker _maker;

		// Token: 0x04000AD2 RID: 2770
		private int _Remoting_Identity_IDSeqNum;

		// Token: 0x04000AD3 RID: 2771
		private long _memFailPointReservedMemory;
	}
}
