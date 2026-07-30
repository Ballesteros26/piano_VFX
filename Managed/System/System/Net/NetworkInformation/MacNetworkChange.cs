using System;
using System.Runtime.InteropServices;
using Mono.Util;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000655 RID: 1621
	internal sealed class MacNetworkChange : INetworkChange, IDisposable
	{
		// Token: 0x06003393 RID: 13203
		[DllImport("/usr/lib/libSystem.dylib")]
		private static extern IntPtr dlopen(string path, int mode);

		// Token: 0x06003394 RID: 13204
		[DllImport("/usr/lib/libSystem.dylib")]
		private static extern IntPtr dlsym(IntPtr handle, string symbol);

		// Token: 0x06003395 RID: 13205
		[DllImport("/usr/lib/libSystem.dylib")]
		private static extern int dlclose(IntPtr handle);

		// Token: 0x06003396 RID: 13206
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		private static extern void CFRelease(IntPtr handle);

		// Token: 0x06003397 RID: 13207
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		private static extern IntPtr CFRunLoopGetMain();

		// Token: 0x06003398 RID: 13208
		[DllImport("/System/Library/Frameworks/SystemConfiguration.framework/SystemConfiguration")]
		private static extern IntPtr SCNetworkReachabilityCreateWithAddress(IntPtr allocator, ref MacNetworkChange.sockaddr_in sockaddr);

		// Token: 0x06003399 RID: 13209
		[DllImport("/System/Library/Frameworks/SystemConfiguration.framework/SystemConfiguration")]
		private static extern bool SCNetworkReachabilityGetFlags(IntPtr reachability, out MacNetworkChange.NetworkReachabilityFlags flags);

		// Token: 0x0600339A RID: 13210
		[DllImport("/System/Library/Frameworks/SystemConfiguration.framework/SystemConfiguration")]
		private static extern bool SCNetworkReachabilitySetCallback(IntPtr reachability, MacNetworkChange.SCNetworkReachabilityCallback callback, ref MacNetworkChange.SCNetworkReachabilityContext context);

		// Token: 0x0600339B RID: 13211
		[DllImport("/System/Library/Frameworks/SystemConfiguration.framework/SystemConfiguration")]
		private static extern bool SCNetworkReachabilityScheduleWithRunLoop(IntPtr reachability, IntPtr runLoop, IntPtr runLoopMode);

		// Token: 0x0600339C RID: 13212
		[DllImport("/System/Library/Frameworks/SystemConfiguration.framework/SystemConfiguration")]
		private static extern bool SCNetworkReachabilityUnscheduleFromRunLoop(IntPtr reachability, IntPtr runLoop, IntPtr runLoopMode);

		// Token: 0x1400005C RID: 92
		// (add) Token: 0x0600339D RID: 13213 RVA: 0x000C0B8C File Offset: 0x000BED8C
		// (remove) Token: 0x0600339E RID: 13214 RVA: 0x000C0BC4 File Offset: 0x000BEDC4
		private event NetworkAddressChangedEventHandler networkAddressChanged;

		// Token: 0x1400005D RID: 93
		// (add) Token: 0x0600339F RID: 13215 RVA: 0x000C0BFC File Offset: 0x000BEDFC
		// (remove) Token: 0x060033A0 RID: 13216 RVA: 0x000C0C34 File Offset: 0x000BEE34
		private event NetworkAvailabilityChangedEventHandler networkAvailabilityChanged;

		// Token: 0x1400005E RID: 94
		// (add) Token: 0x060033A1 RID: 13217 RVA: 0x000C0C69 File Offset: 0x000BEE69
		// (remove) Token: 0x060033A2 RID: 13218 RVA: 0x000C0C7E File Offset: 0x000BEE7E
		public event NetworkAddressChangedEventHandler NetworkAddressChanged
		{
			add
			{
				value(null, EventArgs.Empty);
				this.networkAddressChanged += value;
			}
			remove
			{
				this.networkAddressChanged -= value;
			}
		}

		// Token: 0x1400005F RID: 95
		// (add) Token: 0x060033A3 RID: 13219 RVA: 0x000C0C87 File Offset: 0x000BEE87
		// (remove) Token: 0x060033A4 RID: 13220 RVA: 0x000C0CA2 File Offset: 0x000BEEA2
		public event NetworkAvailabilityChangedEventHandler NetworkAvailabilityChanged
		{
			add
			{
				value(null, new NetworkAvailabilityEventArgs(this.IsAvailable));
				this.networkAvailabilityChanged += value;
			}
			remove
			{
				this.networkAvailabilityChanged -= value;
			}
		}

		// Token: 0x17000C25 RID: 3109
		// (get) Token: 0x060033A5 RID: 13221 RVA: 0x000C0CAB File Offset: 0x000BEEAB
		private bool IsAvailable
		{
			get
			{
				return (this.flags & MacNetworkChange.NetworkReachabilityFlags.Reachable) != MacNetworkChange.NetworkReachabilityFlags.None && (this.flags & MacNetworkChange.NetworkReachabilityFlags.ConnectionRequired) == MacNetworkChange.NetworkReachabilityFlags.None;
			}
		}

		// Token: 0x17000C26 RID: 3110
		// (get) Token: 0x060033A6 RID: 13222 RVA: 0x000C0CC4 File Offset: 0x000BEEC4
		public bool HasRegisteredEvents
		{
			get
			{
				return this.networkAddressChanged != null || this.networkAvailabilityChanged != null;
			}
		}

		// Token: 0x060033A7 RID: 13223 RVA: 0x000C0CDC File Offset: 0x000BEEDC
		public MacNetworkChange()
		{
			MacNetworkChange.sockaddr_in sockaddr_in = MacNetworkChange.sockaddr_in.Create();
			this.handle = MacNetworkChange.SCNetworkReachabilityCreateWithAddress(IntPtr.Zero, ref sockaddr_in);
			if (this.handle == IntPtr.Zero)
			{
				throw new Exception("SCNetworkReachabilityCreateWithAddress returned NULL");
			}
			this.callback = new MacNetworkChange.SCNetworkReachabilityCallback(MacNetworkChange.HandleCallback);
			MacNetworkChange.SCNetworkReachabilityContext scnetworkReachabilityContext = new MacNetworkChange.SCNetworkReachabilityContext
			{
				info = GCHandle.ToIntPtr(GCHandle.Alloc(this))
			};
			MacNetworkChange.SCNetworkReachabilitySetCallback(this.handle, this.callback, ref scnetworkReachabilityContext);
			this.scheduledWithRunLoop = this.LoadRunLoopMode() && MacNetworkChange.SCNetworkReachabilityScheduleWithRunLoop(this.handle, MacNetworkChange.CFRunLoopGetMain(), this.runLoopMode);
			MacNetworkChange.SCNetworkReachabilityGetFlags(this.handle, out this.flags);
		}

		// Token: 0x060033A8 RID: 13224 RVA: 0x000C0DA0 File Offset: 0x000BEFA0
		private bool LoadRunLoopMode()
		{
			IntPtr intPtr = MacNetworkChange.dlopen("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation", 0);
			if (intPtr == IntPtr.Zero)
			{
				return false;
			}
			try
			{
				this.runLoopMode = MacNetworkChange.dlsym(intPtr, "kCFRunLoopDefaultMode");
				if (this.runLoopMode != IntPtr.Zero)
				{
					this.runLoopMode = Marshal.ReadIntPtr(this.runLoopMode);
					return this.runLoopMode != IntPtr.Zero;
				}
			}
			finally
			{
				MacNetworkChange.dlclose(intPtr);
			}
			return false;
		}

		// Token: 0x060033A9 RID: 13225 RVA: 0x000C0E30 File Offset: 0x000BF030
		public void Dispose()
		{
			lock (this)
			{
				if (!(this.handle == IntPtr.Zero))
				{
					if (this.scheduledWithRunLoop)
					{
						MacNetworkChange.SCNetworkReachabilityUnscheduleFromRunLoop(this.handle, MacNetworkChange.CFRunLoopGetMain(), this.runLoopMode);
					}
					MacNetworkChange.CFRelease(this.handle);
					this.handle = IntPtr.Zero;
					this.callback = null;
					this.flags = MacNetworkChange.NetworkReachabilityFlags.None;
					this.scheduledWithRunLoop = false;
				}
			}
		}

		// Token: 0x060033AA RID: 13226 RVA: 0x000C0EC4 File Offset: 0x000BF0C4
		[MonoPInvokeCallback(typeof(MacNetworkChange.SCNetworkReachabilityCallback))]
		private static void HandleCallback(IntPtr reachability, MacNetworkChange.NetworkReachabilityFlags flags, IntPtr info)
		{
			if (info == IntPtr.Zero)
			{
				return;
			}
			MacNetworkChange macNetworkChange = GCHandle.FromIntPtr(info).Target as MacNetworkChange;
			if (macNetworkChange == null || macNetworkChange.flags == flags)
			{
				return;
			}
			macNetworkChange.flags = flags;
			NetworkAddressChangedEventHandler networkAddressChangedEventHandler = macNetworkChange.networkAddressChanged;
			if (networkAddressChangedEventHandler != null)
			{
				networkAddressChangedEventHandler(null, EventArgs.Empty);
			}
			NetworkAvailabilityChangedEventHandler networkAvailabilityChangedEventHandler = macNetworkChange.networkAvailabilityChanged;
			if (networkAvailabilityChangedEventHandler != null)
			{
				networkAvailabilityChangedEventHandler(null, new NetworkAvailabilityEventArgs(macNetworkChange.IsAvailable));
			}
		}

		// Token: 0x0400290F RID: 10511
		private const string DL_LIB = "/usr/lib/libSystem.dylib";

		// Token: 0x04002910 RID: 10512
		private const string CORE_SERVICES_LIB = "/System/Library/Frameworks/SystemConfiguration.framework/SystemConfiguration";

		// Token: 0x04002911 RID: 10513
		private const string CORE_FOUNDATION_LIB = "/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation";

		// Token: 0x04002912 RID: 10514
		private IntPtr handle;

		// Token: 0x04002913 RID: 10515
		private IntPtr runLoopMode;

		// Token: 0x04002914 RID: 10516
		private MacNetworkChange.SCNetworkReachabilityCallback callback;

		// Token: 0x04002915 RID: 10517
		private bool scheduledWithRunLoop;

		// Token: 0x04002916 RID: 10518
		private MacNetworkChange.NetworkReachabilityFlags flags;

		// Token: 0x02000656 RID: 1622
		// (Invoke) Token: 0x060033AC RID: 13228
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate void SCNetworkReachabilityCallback(IntPtr target, MacNetworkChange.NetworkReachabilityFlags flags, IntPtr info);

		// Token: 0x02000657 RID: 1623
		[StructLayout(LayoutKind.Explicit, Size = 28)]
		private struct sockaddr_in
		{
			// Token: 0x060033AF RID: 13231 RVA: 0x000C0F3C File Offset: 0x000BF13C
			public static MacNetworkChange.sockaddr_in Create()
			{
				return new MacNetworkChange.sockaddr_in
				{
					sin_len = 28,
					sin_family = 2
				};
			}

			// Token: 0x04002919 RID: 10521
			[FieldOffset(0)]
			public byte sin_len;

			// Token: 0x0400291A RID: 10522
			[FieldOffset(1)]
			public byte sin_family;
		}

		// Token: 0x02000658 RID: 1624
		private struct SCNetworkReachabilityContext
		{
			// Token: 0x0400291B RID: 10523
			public IntPtr version;

			// Token: 0x0400291C RID: 10524
			public IntPtr info;

			// Token: 0x0400291D RID: 10525
			public IntPtr retain;

			// Token: 0x0400291E RID: 10526
			public IntPtr release;

			// Token: 0x0400291F RID: 10527
			public IntPtr copyDescription;
		}

		// Token: 0x02000659 RID: 1625
		[Flags]
		private enum NetworkReachabilityFlags
		{
			// Token: 0x04002921 RID: 10529
			None = 0,
			// Token: 0x04002922 RID: 10530
			TransientConnection = 1,
			// Token: 0x04002923 RID: 10531
			Reachable = 2,
			// Token: 0x04002924 RID: 10532
			ConnectionRequired = 4,
			// Token: 0x04002925 RID: 10533
			ConnectionOnTraffic = 8,
			// Token: 0x04002926 RID: 10534
			InterventionRequired = 16,
			// Token: 0x04002927 RID: 10535
			ConnectionOnDemand = 32,
			// Token: 0x04002928 RID: 10536
			IsLocalAddress = 65536,
			// Token: 0x04002929 RID: 10537
			IsDirect = 131072,
			// Token: 0x0400292A RID: 10538
			IsWWAN = 262144,
			// Token: 0x0400292B RID: 10539
			ConnectionAutomatic = 8
		}
	}
}
