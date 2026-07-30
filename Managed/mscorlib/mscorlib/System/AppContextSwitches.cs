using System;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x020000FB RID: 251
	internal static class AppContextSwitches
	{
		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000961 RID: 2401 RVA: 0x00031136 File Offset: 0x0002F336
		public static bool NoAsyncCurrentCulture
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return AppContextSwitches.GetCachedSwitchValue("Switch.System.Globalization.NoAsyncCurrentCulture", ref AppContextSwitches._noAsyncCurrentCulture);
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000962 RID: 2402 RVA: 0x00031147 File Offset: 0x0002F347
		public static bool ThrowExceptionIfDisposedCancellationTokenSource
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return AppContextSwitches.GetCachedSwitchValue("Switch.System.Threading.ThrowExceptionIfDisposedCancellationTokenSource", ref AppContextSwitches._throwExceptionIfDisposedCancellationTokenSource);
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000963 RID: 2403 RVA: 0x00031158 File Offset: 0x0002F358
		public static bool PreserveEventListnerObjectIdentity
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return AppContextSwitches.GetCachedSwitchValue("Switch.System.Diagnostics.EventSource.PreserveEventListnerObjectIdentity", ref AppContextSwitches._preserveEventListnerObjectIdentity);
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000964 RID: 2404 RVA: 0x00031169 File Offset: 0x0002F369
		public static bool UseLegacyPathHandling
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return AppContextSwitches.GetCachedSwitchValue("Switch.System.IO.UseLegacyPathHandling", ref AppContextSwitches._useLegacyPathHandling);
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000965 RID: 2405 RVA: 0x0003117A File Offset: 0x0002F37A
		public static bool BlockLongPaths
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return AppContextSwitches.GetCachedSwitchValue("Switch.System.IO.BlockLongPaths", ref AppContextSwitches._blockLongPaths);
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000966 RID: 2406 RVA: 0x0003118B File Offset: 0x0002F38B
		public static bool SetActorAsReferenceWhenCopyingClaimsIdentity
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return AppContextSwitches.GetCachedSwitchValue("Switch.System.Security.ClaimsIdentity.SetActorAsReferenceWhenCopyingClaimsIdentity", ref AppContextSwitches._cloneActor);
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000967 RID: 2407 RVA: 0x0003119C File Offset: 0x0002F39C
		public static bool DoNotAddrOfCspParentWindowHandle
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return AppContextSwitches.GetCachedSwitchValue("Switch.System.Security.Cryptography.DoNotAddrOfCspParentWindowHandle", ref AppContextSwitches._doNotAddrOfCspParentWindowHandle);
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000968 RID: 2408 RVA: 0x000311AD File Offset: 0x0002F3AD
		// (set) Token: 0x06000969 RID: 2409 RVA: 0x000311B4 File Offset: 0x0002F3B4
		private static bool DisableCaching { get; set; }

		// Token: 0x0600096A RID: 2410 RVA: 0x000311BC File Offset: 0x0002F3BC
		static AppContextSwitches()
		{
			bool flag;
			if (AppContext.TryGetSwitch("TestSwitch.LocalAppContext.DisableCaching", out flag))
			{
				AppContextSwitches.DisableCaching = flag;
			}
		}

		// Token: 0x0600096B RID: 2411 RVA: 0x000311DD File Offset: 0x0002F3DD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static bool GetCachedSwitchValue(string switchName, ref int switchValue)
		{
			return switchValue >= 0 && (switchValue > 0 || AppContextSwitches.GetCachedSwitchValueInternal(switchName, ref switchValue));
		}

		// Token: 0x0600096C RID: 2412 RVA: 0x000311F4 File Offset: 0x0002F3F4
		private static bool GetCachedSwitchValueInternal(string switchName, ref int switchValue)
		{
			bool flag;
			AppContext.TryGetSwitch(switchName, out flag);
			if (AppContextSwitches.DisableCaching)
			{
				return flag;
			}
			switchValue = (flag ? 1 : (-1));
			return flag;
		}

		// Token: 0x04000706 RID: 1798
		private static int _noAsyncCurrentCulture;

		// Token: 0x04000707 RID: 1799
		private static int _throwExceptionIfDisposedCancellationTokenSource;

		// Token: 0x04000708 RID: 1800
		private static int _preserveEventListnerObjectIdentity;

		// Token: 0x04000709 RID: 1801
		private static int _useLegacyPathHandling;

		// Token: 0x0400070A RID: 1802
		private static int _blockLongPaths;

		// Token: 0x0400070B RID: 1803
		private static int _cloneActor;

		// Token: 0x0400070C RID: 1804
		private static int _doNotAddrOfCspParentWindowHandle;
	}
}
