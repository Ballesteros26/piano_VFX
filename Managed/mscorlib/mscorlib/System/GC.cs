using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Security;

namespace System
{
	/// <summary>Controls the system garbage collector, a service that automatically reclaims unused memory.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000167 RID: 359
	public static class GC
	{
		// Token: 0x06000F3E RID: 3902
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetCollectionCount(int generation);

		// Token: 0x06000F3F RID: 3903
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetMaxGeneration();

		// Token: 0x06000F40 RID: 3904
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalCollect(int generation);

		// Token: 0x06000F41 RID: 3905
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RecordPressure(long bytesAllocated);

		// Token: 0x06000F42 RID: 3906
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void register_ephemeron_array(Ephemeron[] array);

		// Token: 0x06000F43 RID: 3907
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern object get_ephemeron_tombstone();

		/// <summary>Informs the runtime of a large allocation of unmanaged memory that should be taken into account when scheduling garbage collection.</summary>
		/// <param name="bytesAllocated">The incremental amount of unmanaged memory that has been allocated. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="bytesAllocated" /> is less than or equal to 0.-or-On a 32-bit computer, <paramref name="bytesAllocated" /> is larger than <see cref="F:System.Int32.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06000F44 RID: 3908 RVA: 0x0003EB48 File Offset: 0x0003CD48
		[SecurityCritical]
		public static void AddMemoryPressure(long bytesAllocated)
		{
			if (bytesAllocated <= 0L)
			{
				throw new ArgumentOutOfRangeException("bytesAllocated", Environment.GetResourceString("Positive number required."));
			}
			if (4 == IntPtr.Size && bytesAllocated > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("pressure", Environment.GetResourceString("Value must be non-negative and less than or equal to Int32.MaxValue."));
			}
			GC.RecordPressure(bytesAllocated);
		}

		/// <summary>Informs the runtime that unmanaged memory has been released and no longer needs to be taken into account when scheduling garbage collection.</summary>
		/// <param name="bytesAllocated">The amount of unmanaged memory that has been released. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="bytesAllocated" /> is less than or equal to 0. -or- On a 32-bit computer, <paramref name="bytesAllocated" /> is larger than <see cref="F:System.Int32.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06000F45 RID: 3909 RVA: 0x0003EB9C File Offset: 0x0003CD9C
		[SecurityCritical]
		public static void RemoveMemoryPressure(long bytesAllocated)
		{
			if (bytesAllocated <= 0L)
			{
				throw new ArgumentOutOfRangeException("bytesAllocated", Environment.GetResourceString("Positive number required."));
			}
			if (4 == IntPtr.Size && bytesAllocated > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("bytesAllocated", Environment.GetResourceString("Value must be non-negative and less than or equal to Int32.MaxValue."));
			}
			GC.RecordPressure(-bytesAllocated);
		}

		/// <summary>Returns the current generation number of the specified object.</summary>
		/// <returns>The current generation number of <paramref name="obj" />.</returns>
		/// <param name="obj">The object that generation information is retrieved for. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F46 RID: 3910
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetGeneration(object obj);

		/// <summary>Forces an immediate garbage collection from generation 0 through a specified generation.</summary>
		/// <param name="generation">The number of the oldest generation that garbage collection can be performed on. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="generation" /> is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F47 RID: 3911 RVA: 0x0003EBF0 File Offset: 0x0003CDF0
		public static void Collect(int generation)
		{
			GC.Collect(generation, GCCollectionMode.Default);
		}

		/// <summary>Forces an immediate garbage collection of all generations. </summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F48 RID: 3912 RVA: 0x0003EBF9 File Offset: 0x0003CDF9
		[SecuritySafeCritical]
		public static void Collect()
		{
			GC.InternalCollect(GC.MaxGeneration);
		}

		/// <summary>Forces a garbage collection from generation 0 through a specified generation, at a time specified by a <see cref="T:System.GCCollectionMode" /> value.</summary>
		/// <param name="generation">The number of the oldest generation that garbage collection can be performed on. </param>
		/// <param name="mode">One of the enumeration values that specifies the behavior for the garbage collection.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="generation" /> is not valid.-or-<paramref name="mode" /> is not one of the <see cref="T:System.GCCollectionMode" /> values.</exception>
		// Token: 0x06000F49 RID: 3913 RVA: 0x0003EC05 File Offset: 0x0003CE05
		[SecuritySafeCritical]
		public static void Collect(int generation, GCCollectionMode mode)
		{
			GC.Collect(generation, mode, true);
		}

		/// <summary>Forces a garbage collection from generation 0 through a specified generation, at a time specified by a <see cref="T:System.GCCollectionMode" /> value, with a value specifying whether the collection should be blocking.</summary>
		/// <param name="generation">The number of the oldest generation that garbage collection can be performed on.</param>
		/// <param name="mode">One of the enumeration values that specifies whether the garbage collection is forced or optimized.</param>
		/// <param name="blocking">true to perform a blocking garbage collection; false to perform a background garbage collection where possible.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="generation" /> is not valid.-or-<paramref name="mode" /> is not one of the <see cref="T:System.GCCollectionMode" /> values.</exception>
		// Token: 0x06000F4A RID: 3914 RVA: 0x0003EC0F File Offset: 0x0003CE0F
		[SecuritySafeCritical]
		public static void Collect(int generation, GCCollectionMode mode, bool blocking)
		{
			GC.Collect(generation, mode, blocking, false);
		}

		// Token: 0x06000F4B RID: 3915 RVA: 0x0003EC1C File Offset: 0x0003CE1C
		[SecuritySafeCritical]
		public static void Collect(int generation, GCCollectionMode mode, bool blocking, bool compacting)
		{
			if (generation < 0)
			{
				throw new ArgumentOutOfRangeException("generation", Environment.GetResourceString("Value must be positive."));
			}
			if (mode < GCCollectionMode.Default || mode > GCCollectionMode.Optimized)
			{
				throw new ArgumentOutOfRangeException(Environment.GetResourceString("Enum value was out of legal range."));
			}
			int num = 0;
			if (mode == GCCollectionMode.Optimized)
			{
				num |= 4;
			}
			if (compacting)
			{
				num |= 8;
			}
			if (blocking)
			{
				num |= 2;
			}
			else if (!compacting)
			{
				num |= 1;
			}
			GC.InternalCollect(generation);
		}

		/// <summary>Returns the number of times garbage collection has occurred for the specified generation of objects.</summary>
		/// <returns>The number of times garbage collection has occurred for the specified generation since the process was started.</returns>
		/// <param name="generation">The generation of objects for which the garbage collection count is to be determined. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="generation" /> is less than 0. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F4C RID: 3916 RVA: 0x0003EC81 File Offset: 0x0003CE81
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static int CollectionCount(int generation)
		{
			if (generation < 0)
			{
				throw new ArgumentOutOfRangeException("generation", Environment.GetResourceString("Value must be positive."));
			}
			return GC.GetCollectionCount(generation);
		}

		/// <summary>References the specified object, which makes it ineligible for garbage collection from the start of the current routine to the point where this method is called.</summary>
		/// <param name="obj">The object to reference. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F4D RID: 3917 RVA: 0x00002194 File Offset: 0x00000394
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void KeepAlive(object obj)
		{
		}

		/// <summary>Returns the current generation number of the target of a specified weak reference.</summary>
		/// <returns>The current generation number of the target of <paramref name="wo" />.</returns>
		/// <param name="wo">A <see cref="T:System.WeakReference" /> that refers to the target object whose generation number is to be determined. </param>
		/// <exception cref="T:System.ArgumentException">Garbage collection has already been performed on <paramref name="wo" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F4E RID: 3918 RVA: 0x0003ECA2 File Offset: 0x0003CEA2
		[SecuritySafeCritical]
		public static int GetGeneration(WeakReference wo)
		{
			object target = wo.Target;
			if (target == null)
			{
				throw new ArgumentException();
			}
			return GC.GetGeneration(target);
		}

		/// <summary>Gets the maximum number of generations that the system currently supports.</summary>
		/// <returns>A value that ranges from zero to the maximum number of supported generations.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000F4F RID: 3919 RVA: 0x0003ECB8 File Offset: 0x0003CEB8
		public static int MaxGeneration
		{
			[SecuritySafeCritical]
			get
			{
				return GC.GetMaxGeneration();
			}
		}

		/// <summary>Suspends the current thread until the thread that is processing the queue of finalizers has emptied that queue.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F50 RID: 3920
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void WaitForPendingFinalizers();

		// Token: 0x06000F51 RID: 3921
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _SuppressFinalize(object o);

		/// <summary>Requests that the system not call the finalizer for the specified object.</summary>
		/// <param name="obj">The object that a finalizer must not be called for. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="obj" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F52 RID: 3922 RVA: 0x0003ECBF File Offset: 0x0003CEBF
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SecuritySafeCritical]
		public static void SuppressFinalize(object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			GC._SuppressFinalize(obj);
		}

		// Token: 0x06000F53 RID: 3923
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _ReRegisterForFinalize(object o);

		/// <summary>Requests that the system call the finalizer for the specified object for which <see cref="M:System.GC.SuppressFinalize(System.Object)" /> has previously been called.</summary>
		/// <param name="obj">The object that a finalizer must be called for. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="obj" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F54 RID: 3924 RVA: 0x0003ECD5 File Offset: 0x0003CED5
		[SecuritySafeCritical]
		public static void ReRegisterForFinalize(object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			GC._ReRegisterForFinalize(obj);
		}

		/// <summary>Retrieves the number of bytes currently thought to be allocated. A parameter indicates whether this method can wait a short interval before returning, to allow the system to collect garbage and finalize objects.</summary>
		/// <returns>A number that is the best available approximation of the number of bytes currently allocated in managed memory.</returns>
		/// <param name="forceFullCollection">true to indicate that this method can wait for garbage collection to occur before returning; otherwise, false.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F55 RID: 3925
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern long GetTotalMemory(bool forceFullCollection);

		// Token: 0x06000F56 RID: 3926 RVA: 0x0002126B File Offset: 0x0001F46B
		private static bool _RegisterForFullGCNotification(int maxGenerationPercentage, int largeObjectHeapPercentage)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000F57 RID: 3927 RVA: 0x0002126B File Offset: 0x0001F46B
		private static bool _CancelFullGCNotification()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000F58 RID: 3928 RVA: 0x0002126B File Offset: 0x0001F46B
		private static int _WaitForFullGCApproach(int millisecondsTimeout)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000F59 RID: 3929 RVA: 0x0002126B File Offset: 0x0001F46B
		private static int _WaitForFullGCComplete(int millisecondsTimeout)
		{
			throw new NotImplementedException();
		}

		/// <summary>Specifies that a garbage collection notification should be raised when conditions favor full garbage collection and when the collection has been completed.</summary>
		/// <param name="maxGenerationThreshold">A number between 1 and 99 that specifies when the notification should be raised based on the objects surviving in generation 2. </param>
		/// <param name="largeObjectHeapThreshold">A number between 1 and 99 that specifies when the notification should be raised based on objects allocated in the large object heap. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="maxGenerationThreshold " />or <paramref name="largeObjectHeapThreshold " />is not between 1 and 99.</exception>
		// Token: 0x06000F5A RID: 3930 RVA: 0x0003ECEC File Offset: 0x0003CEEC
		[SecurityCritical]
		public static void RegisterForFullGCNotification(int maxGenerationThreshold, int largeObjectHeapThreshold)
		{
			if (maxGenerationThreshold <= 0 || maxGenerationThreshold >= 100)
			{
				throw new ArgumentOutOfRangeException("maxGenerationThreshold", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument must be between {0} and {1}."), 1, 99));
			}
			if (largeObjectHeapThreshold <= 0 || largeObjectHeapThreshold >= 100)
			{
				throw new ArgumentOutOfRangeException("largeObjectHeapThreshold", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument must be between {0} and {1}."), 1, 99));
			}
			if (!GC._RegisterForFullGCNotification(maxGenerationThreshold, largeObjectHeapThreshold))
			{
				throw new InvalidOperationException(Environment.GetResourceString("This API is not available when the concurrent GC is enabled."));
			}
		}

		/// <summary>Cancels the registration of a garbage collection notification.</summary>
		/// <exception cref="T:System.InvalidOperationException">This member is not available when concurrent garbage collection is enabled. See the &lt;gcConcurrent&gt; runtime setting for information about how to disable concurrent garbage collection.</exception>
		// Token: 0x06000F5B RID: 3931 RVA: 0x0003ED7C File Offset: 0x0003CF7C
		[SecurityCritical]
		public static void CancelFullGCNotification()
		{
			if (!GC._CancelFullGCNotification())
			{
				throw new InvalidOperationException(Environment.GetResourceString("This API is not available when the concurrent GC is enabled."));
			}
		}

		/// <summary>Returns the status of a registered notification for determining whether a full, blocking garbage collection by the common langauge runtime is imminent.</summary>
		/// <returns>The status of the registered garbage collection notification.</returns>
		// Token: 0x06000F5C RID: 3932 RVA: 0x0003ED95 File Offset: 0x0003CF95
		[SecurityCritical]
		public static GCNotificationStatus WaitForFullGCApproach()
		{
			return (GCNotificationStatus)GC._WaitForFullGCApproach(-1);
		}

		/// <summary>Returns, in a specified time-out period, the status of a registered notification for determining whether a full, blocking garbage collection by the common language runtime is imminent.</summary>
		/// <returns>The status of the registered garbage collection notification.</returns>
		/// <param name="millisecondsTimeout">The length of time to wait before a notification status can be obtained. Specify -1 to wait indefinitely.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="millisecondsTimeout" /> must be either non-negative or less than or equal to <see cref="F:System.Int32.MaxValue" /> or -1.</exception>
		// Token: 0x06000F5D RID: 3933 RVA: 0x0003ED9D File Offset: 0x0003CF9D
		[SecurityCritical]
		public static GCNotificationStatus WaitForFullGCApproach(int millisecondsTimeout)
		{
			if (millisecondsTimeout < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeout", Environment.GetResourceString("Number must be either non-negative and less than or equal to Int32.MaxValue or -1."));
			}
			return (GCNotificationStatus)GC._WaitForFullGCApproach(millisecondsTimeout);
		}

		/// <summary>Returns the status of a registered notification for determining whether a full, blocking garbage collection by the common language runtime has completed.</summary>
		/// <returns>The status of the registered garbage collection notification.</returns>
		// Token: 0x06000F5E RID: 3934 RVA: 0x0003EDBE File Offset: 0x0003CFBE
		[SecurityCritical]
		public static GCNotificationStatus WaitForFullGCComplete()
		{
			return (GCNotificationStatus)GC._WaitForFullGCComplete(-1);
		}

		/// <summary>Returns, in a specified time-out period, the status of a registered notification for determining whether a full, blocking garbage collection by common language the runtime has completed.</summary>
		/// <returns>The status of the registered garbage collection notification.</returns>
		/// <param name="millisecondsTimeout">The length of time to wait before a notification status can be obtained. Specify -1 to wait indefinitely.</param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="millisecondsTimeout" /> must be either non-negative or less than or equal to <see cref="F:System.Int32.MaxValue" /> or -1.</exception>
		// Token: 0x06000F5F RID: 3935 RVA: 0x0003EDC6 File Offset: 0x0003CFC6
		[SecurityCritical]
		public static GCNotificationStatus WaitForFullGCComplete(int millisecondsTimeout)
		{
			if (millisecondsTimeout < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeout", Environment.GetResourceString("Number must be either non-negative and less than or equal to Int32.MaxValue or -1."));
			}
			return (GCNotificationStatus)GC._WaitForFullGCComplete(millisecondsTimeout);
		}

		// Token: 0x06000F60 RID: 3936 RVA: 0x0002126B File Offset: 0x0001F46B
		[SecurityCritical]
		private static bool StartNoGCRegionWorker(long totalSize, bool hasLohSize, long lohSize, bool disallowFullBlockingGC)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000F61 RID: 3937 RVA: 0x0003EDE7 File Offset: 0x0003CFE7
		[SecurityCritical]
		public static bool TryStartNoGCRegion(long totalSize)
		{
			return GC.StartNoGCRegionWorker(totalSize, false, 0L, false);
		}

		// Token: 0x06000F62 RID: 3938 RVA: 0x0003EDF3 File Offset: 0x0003CFF3
		[SecurityCritical]
		public static bool TryStartNoGCRegion(long totalSize, long lohSize)
		{
			return GC.StartNoGCRegionWorker(totalSize, true, lohSize, false);
		}

		// Token: 0x06000F63 RID: 3939 RVA: 0x0003EDFE File Offset: 0x0003CFFE
		[SecurityCritical]
		public static bool TryStartNoGCRegion(long totalSize, bool disallowFullBlockingGC)
		{
			return GC.StartNoGCRegionWorker(totalSize, false, 0L, disallowFullBlockingGC);
		}

		// Token: 0x06000F64 RID: 3940 RVA: 0x0003EE0A File Offset: 0x0003D00A
		[SecurityCritical]
		public static bool TryStartNoGCRegion(long totalSize, long lohSize, bool disallowFullBlockingGC)
		{
			return GC.StartNoGCRegionWorker(totalSize, true, lohSize, disallowFullBlockingGC);
		}

		// Token: 0x06000F65 RID: 3941 RVA: 0x0002126B File Offset: 0x0001F46B
		[SecurityCritical]
		private static GC.EndNoGCRegionStatus EndNoGCRegionWorker()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000F66 RID: 3942 RVA: 0x0003EE15 File Offset: 0x0003D015
		[SecurityCritical]
		public static void EndNoGCRegion()
		{
			GC.EndNoGCRegionWorker();
		}

		// Token: 0x04000926 RID: 2342
		internal static readonly object EPHEMERON_TOMBSTONE = GC.get_ephemeron_tombstone();

		// Token: 0x02000168 RID: 360
		private enum StartNoGCRegionStatus
		{
			// Token: 0x04000928 RID: 2344
			Succeeded,
			// Token: 0x04000929 RID: 2345
			NotEnoughMemory,
			// Token: 0x0400092A RID: 2346
			AmountTooLarge,
			// Token: 0x0400092B RID: 2347
			AlreadyInProgress
		}

		// Token: 0x02000169 RID: 361
		private enum EndNoGCRegionStatus
		{
			// Token: 0x0400092D RID: 2349
			Succeeded,
			// Token: 0x0400092E RID: 2350
			NotInProgress,
			// Token: 0x0400092F RID: 2351
			GCInduced,
			// Token: 0x04000930 RID: 2352
			AllocationExceeded
		}
	}
}
