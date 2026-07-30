using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Scripting;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Profiling
{
	// Token: 0x02000212 RID: 530
	[UsedByNativeCode]
	[NativeHeader("Runtime/Allocator/MemoryManager.h")]
	[MovedFrom("UnityEngine")]
	[NativeHeader("Runtime/ScriptingBackend/ScriptingApi.h")]
	[NativeHeader("Runtime/Profiler/Profiler.h")]
	[NativeHeader("Runtime/Profiler/ScriptBindings/Profiler.bindings.h")]
	[NativeHeader("Runtime/Utilities/MemoryUtilities.h")]
	public sealed class Profiler
	{
		// Token: 0x0600177B RID: 6011 RVA: 0x000166AA File Offset: 0x000148AA
		private Profiler()
		{
		}

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x0600177C RID: 6012
		public static extern bool supported
		{
			[NativeMethod(Name = "profiler_is_available", IsFreeFunction = true)]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x0600177D RID: 6013
		// (set) Token: 0x0600177E RID: 6014
		[StaticAccessor("ProfilerBindings", StaticAccessorType.DoubleColon)]
		public static extern string logFile
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x0600177F RID: 6015
		// (set) Token: 0x06001780 RID: 6016
		public static extern bool enableBinaryLog
		{
			[NativeMethod(Name = "ProfilerBindings::IsBinaryLogEnabled", IsFreeFunction = true)]
			[MethodImpl(4096)]
			get;
			[NativeMethod(Name = "ProfilerBindings::SetBinaryLogEnabled", IsFreeFunction = true)]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x06001781 RID: 6017
		// (set) Token: 0x06001782 RID: 6018
		public static extern int maxUsedMemory
		{
			[NativeMethod(Name = "ProfilerBindings::GetMaxUsedMemory", IsFreeFunction = true)]
			[MethodImpl(4096)]
			get;
			[NativeMethod(Name = "ProfilerBindings::SetMaxUsedMemory", IsFreeFunction = true)]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x06001783 RID: 6019
		// (set) Token: 0x06001784 RID: 6020
		public static extern bool enabled
		{
			[NativeConditional("ENABLE_PROFILER")]
			[NativeMethod(Name = "profiler_is_enabled", IsFreeFunction = true)]
			[MethodImpl(4096)]
			get;
			[NativeMethod(Name = "ProfilerBindings::SetProfilerEnabled", IsFreeFunction = true)]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x06001785 RID: 6021
		// (set) Token: 0x06001786 RID: 6022
		public static extern bool enableAllocationCallstacks
		{
			[NativeMethod(Name = "ProfilerBindings::IsAllocationCallstackCaptureEnabled", IsFreeFunction = true)]
			[MethodImpl(4096)]
			get;
			[NativeMethod(Name = "ProfilerBindings::SetAllocationCallstackCaptureEnabled", IsFreeFunction = true)]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x06001787 RID: 6023
		[Conditional("ENABLE_PROFILER")]
		[FreeFunction("profiler_set_area_enabled")]
		[MethodImpl(4096)]
		public static extern void SetAreaEnabled(ProfilerArea area, bool enabled);

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x06001788 RID: 6024 RVA: 0x00026190 File Offset: 0x00024390
		public static int areaCount
		{
			get
			{
				return Enum.GetNames(typeof(ProfilerArea)).Length;
			}
		}

		// Token: 0x06001789 RID: 6025
		[FreeFunction("profiler_is_area_enabled")]
		[NativeConditional("ENABLE_PROFILER")]
		[MethodImpl(4096)]
		public static extern bool GetAreaEnabled(ProfilerArea area);

		// Token: 0x0600178A RID: 6026 RVA: 0x000261B4 File Offset: 0x000243B4
		[Conditional("UNITY_EDITOR")]
		public static void AddFramesFromFile(string file)
		{
			bool flag = string.IsNullOrEmpty(file);
			if (flag)
			{
				Debug.LogError("AddFramesFromFile: Invalid or empty path");
			}
			else
			{
				Profiler.AddFramesFromFile_Internal(file, true);
			}
		}

		// Token: 0x0600178B RID: 6027
		[NativeHeader("Modules/ProfilerEditor/Public/ProfilerSession.h")]
		[NativeConditional("ENABLE_PROFILER && UNITY_EDITOR")]
		[StaticAccessor("profiling::GetProfilerSessionPtr()", StaticAccessorType.Arrow)]
		[NativeMethod(Name = "LoadFromFile")]
		[MethodImpl(4096)]
		private static extern void AddFramesFromFile_Internal(string file, bool keepExistingFrames);

		// Token: 0x0600178C RID: 6028 RVA: 0x000261E4 File Offset: 0x000243E4
		[Conditional("ENABLE_PROFILER")]
		public static void BeginThreadProfiling(string threadGroupName, string threadName)
		{
			bool flag = string.IsNullOrEmpty(threadGroupName);
			if (flag)
			{
				throw new ArgumentException("Argument should be a valid string", "threadGroupName");
			}
			bool flag2 = string.IsNullOrEmpty(threadName);
			if (flag2)
			{
				throw new ArgumentException("Argument should be a valid string", "threadName");
			}
			Profiler.BeginThreadProfilingInternal(threadGroupName, threadName);
		}

		// Token: 0x0600178D RID: 6029
		[NativeConditional("ENABLE_PROFILER")]
		[NativeMethod(Name = "ProfilerBindings::BeginThreadProfiling", IsFreeFunction = true, IsThreadSafe = true)]
		[MethodImpl(4096)]
		private static extern void BeginThreadProfilingInternal(string threadGroupName, string threadName);

		// Token: 0x0600178E RID: 6030
		[NativeMethod(Name = "ProfilerBindings::EndThreadProfiling", IsFreeFunction = true, IsThreadSafe = true)]
		[NativeConditional("ENABLE_PROFILER")]
		[MethodImpl(4096)]
		public static extern void EndThreadProfiling();

		// Token: 0x0600178F RID: 6031 RVA: 0x0002622E File Offset: 0x0002442E
		[Conditional("ENABLE_PROFILER")]
		[MethodImpl(256)]
		public static void BeginSample(string name)
		{
			Profiler.ValidateArguments(name);
			Profiler.BeginSampleImpl(name, null);
		}

		// Token: 0x06001790 RID: 6032 RVA: 0x00026240 File Offset: 0x00024440
		[Conditional("ENABLE_PROFILER")]
		[MethodImpl(256)]
		public static void BeginSample(string name, Object targetObject)
		{
			Profiler.ValidateArguments(name);
			Profiler.BeginSampleImpl(name, targetObject);
		}

		// Token: 0x06001791 RID: 6033 RVA: 0x00026254 File Offset: 0x00024454
		[MethodImpl(256)]
		private static void ValidateArguments(string name)
		{
			bool flag = string.IsNullOrEmpty(name);
			if (flag)
			{
				throw new ArgumentException("Argument should be a valid string.", "name");
			}
		}

		// Token: 0x06001792 RID: 6034
		[NativeMethod(Name = "ProfilerBindings::BeginSample", IsFreeFunction = true, IsThreadSafe = true)]
		[MethodImpl(4096)]
		private static extern void BeginSampleImpl(string name, Object targetObject);

		// Token: 0x06001793 RID: 6035
		[NativeMethod(Name = "ProfilerBindings::EndSample", IsFreeFunction = true, IsThreadSafe = true)]
		[Conditional("ENABLE_PROFILER")]
		[MethodImpl(4096)]
		public static extern void EndSample();

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x06001794 RID: 6036 RVA: 0x00026280 File Offset: 0x00024480
		// (set) Token: 0x06001795 RID: 6037 RVA: 0x00002EC3 File Offset: 0x000010C3
		[Obsolete("maxNumberOfSamplesPerFrame has been depricated. Use maxUsedMemory instead")]
		public static int maxNumberOfSamplesPerFrame
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x06001796 RID: 6038 RVA: 0x00026294 File Offset: 0x00024494
		[Obsolete("usedHeapSize has been deprecated since it is limited to 4GB. Please use usedHeapSizeLong instead.")]
		public static uint usedHeapSize
		{
			get
			{
				return (uint)Profiler.usedHeapSizeLong;
			}
		}

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x06001797 RID: 6039
		public static extern long usedHeapSizeLong
		{
			[NativeMethod(Name = "GetUsedHeapSize", IsFreeFunction = true)]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06001798 RID: 6040 RVA: 0x000262AC File Offset: 0x000244AC
		[Obsolete("GetRuntimeMemorySize has been deprecated since it is limited to 2GB. Please use GetRuntimeMemorySizeLong() instead.")]
		public static int GetRuntimeMemorySize(Object o)
		{
			return (int)Profiler.GetRuntimeMemorySizeLong(o);
		}

		// Token: 0x06001799 RID: 6041
		[NativeMethod(Name = "ProfilerBindings::GetRuntimeMemorySizeLong", IsFreeFunction = true)]
		[MethodImpl(4096)]
		public static extern long GetRuntimeMemorySizeLong(Object o);

		// Token: 0x0600179A RID: 6042 RVA: 0x000262C8 File Offset: 0x000244C8
		[Obsolete("GetMonoHeapSize has been deprecated since it is limited to 4GB. Please use GetMonoHeapSizeLong() instead.")]
		public static uint GetMonoHeapSize()
		{
			return (uint)Profiler.GetMonoHeapSizeLong();
		}

		// Token: 0x0600179B RID: 6043
		[NativeMethod(Name = "scripting_gc_get_heap_size", IsFreeFunction = true)]
		[MethodImpl(4096)]
		public static extern long GetMonoHeapSizeLong();

		// Token: 0x0600179C RID: 6044 RVA: 0x000262E0 File Offset: 0x000244E0
		[Obsolete("GetMonoUsedSize has been deprecated since it is limited to 4GB. Please use GetMonoUsedSizeLong() instead.")]
		public static uint GetMonoUsedSize()
		{
			return (uint)Profiler.GetMonoUsedSizeLong();
		}

		// Token: 0x0600179D RID: 6045
		[NativeMethod(Name = "scripting_gc_get_used_size", IsFreeFunction = true)]
		[MethodImpl(4096)]
		public static extern long GetMonoUsedSizeLong();

		// Token: 0x0600179E RID: 6046
		[StaticAccessor("GetMemoryManager()", StaticAccessorType.Dot)]
		[NativeConditional("ENABLE_MEMORY_MANAGER")]
		[MethodImpl(4096)]
		public static extern bool SetTempAllocatorRequestedSize(uint size);

		// Token: 0x0600179F RID: 6047
		[StaticAccessor("GetMemoryManager()", StaticAccessorType.Dot)]
		[NativeConditional("ENABLE_MEMORY_MANAGER")]
		[MethodImpl(4096)]
		public static extern uint GetTempAllocatorSize();

		// Token: 0x060017A0 RID: 6048 RVA: 0x000262F8 File Offset: 0x000244F8
		[Obsolete("GetTotalAllocatedMemory has been deprecated since it is limited to 4GB. Please use GetTotalAllocatedMemoryLong() instead.")]
		public static uint GetTotalAllocatedMemory()
		{
			return (uint)Profiler.GetTotalAllocatedMemoryLong();
		}

		// Token: 0x060017A1 RID: 6049
		[StaticAccessor("GetMemoryManager()", StaticAccessorType.Dot)]
		[NativeConditional("ENABLE_MEMORY_MANAGER")]
		[NativeMethod(Name = "GetTotalAllocatedMemory")]
		[MethodImpl(4096)]
		public static extern long GetTotalAllocatedMemoryLong();

		// Token: 0x060017A2 RID: 6050 RVA: 0x00026310 File Offset: 0x00024510
		[Obsolete("GetTotalUnusedReservedMemory has been deprecated since it is limited to 4GB. Please use GetTotalUnusedReservedMemoryLong() instead.")]
		public static uint GetTotalUnusedReservedMemory()
		{
			return (uint)Profiler.GetTotalUnusedReservedMemoryLong();
		}

		// Token: 0x060017A3 RID: 6051
		[NativeConditional("ENABLE_MEMORY_MANAGER")]
		[StaticAccessor("GetMemoryManager()", StaticAccessorType.Dot)]
		[NativeMethod(Name = "GetTotalUnusedReservedMemory")]
		[MethodImpl(4096)]
		public static extern long GetTotalUnusedReservedMemoryLong();

		// Token: 0x060017A4 RID: 6052 RVA: 0x00026328 File Offset: 0x00024528
		[Obsolete("GetTotalReservedMemory has been deprecated since it is limited to 4GB. Please use GetTotalReservedMemoryLong() instead.")]
		public static uint GetTotalReservedMemory()
		{
			return (uint)Profiler.GetTotalReservedMemoryLong();
		}

		// Token: 0x060017A5 RID: 6053
		[NativeMethod(Name = "GetTotalReservedMemory")]
		[NativeConditional("ENABLE_MEMORY_MANAGER")]
		[StaticAccessor("GetMemoryManager()", StaticAccessorType.Dot)]
		[MethodImpl(4096)]
		public static extern long GetTotalReservedMemoryLong();

		// Token: 0x060017A6 RID: 6054 RVA: 0x00026340 File Offset: 0x00024540
		[NativeConditional("ENABLE_MEMORY_MANAGER")]
		public static long GetTotalFragmentationInfo(NativeArray<int> stats)
		{
			return Profiler.InternalGetTotalFragmentationInfo((IntPtr)stats.GetUnsafePtr<int>(), stats.Length);
		}

		// Token: 0x060017A7 RID: 6055
		[NativeMethod(Name = "GetTotalFragmentationInfo")]
		[NativeConditional("ENABLE_MEMORY_MANAGER")]
		[StaticAccessor("GetMemoryManager()", StaticAccessorType.Dot)]
		[MethodImpl(4096)]
		private static extern long InternalGetTotalFragmentationInfo(IntPtr pStats, int count);

		// Token: 0x060017A8 RID: 6056
		[StaticAccessor("GetMemoryManager()", StaticAccessorType.Dot)]
		[NativeMethod(Name = "GetRegisteredGFXDriverMemory")]
		[NativeConditional("ENABLE_PROFILER")]
		[MethodImpl(4096)]
		public static extern long GetAllocatedMemoryForGraphicsDriver();

		// Token: 0x060017A9 RID: 6057 RVA: 0x0002636C File Offset: 0x0002456C
		[Conditional("ENABLE_PROFILER")]
		public static void EmitFrameMetaData(Guid id, int tag, Array data)
		{
			bool flag = data == null;
			if (flag)
			{
				throw new ArgumentNullException("data");
			}
			Type elementType = data.GetType().GetElementType();
			bool flag2 = !UnsafeUtility.IsBlittable(elementType);
			if (flag2)
			{
				throw new ArgumentException(string.Format("{0} type used in Profiler.ReportFrameStats must be blittable", elementType));
			}
			Profiler.Internal_EmitFrameMetaData_Array(id.ToByteArray(), tag, data, data.Length, UnsafeUtility.SizeOf(elementType));
		}

		// Token: 0x060017AA RID: 6058 RVA: 0x000263D4 File Offset: 0x000245D4
		[Conditional("ENABLE_PROFILER")]
		public static void EmitFrameMetaData<T>(Guid id, int tag, List<T> data) where T : struct
		{
			bool flag = data == null;
			if (flag)
			{
				throw new ArgumentNullException("data");
			}
			Type typeFromHandle = typeof(T);
			bool flag2 = !UnsafeUtility.IsBlittable(typeof(T));
			if (flag2)
			{
				throw new ArgumentException(string.Format("{0} type used in Profiler.ReportFrameStats must be blittable", typeFromHandle));
			}
			Profiler.Internal_EmitFrameMetaData_Array(id.ToByteArray(), tag, NoAllocHelpers.ExtractArrayFromList(data), data.Count, UnsafeUtility.SizeOf(typeFromHandle));
		}

		// Token: 0x060017AB RID: 6059 RVA: 0x00026447 File Offset: 0x00024647
		[Conditional("ENABLE_PROFILER")]
		public static void EmitFrameMetaData<T>(Guid id, int tag, NativeArray<T> data) where T : struct
		{
			Profiler.Internal_EmitFrameMetaData_Native(id.ToByteArray(), tag, (IntPtr)data.GetUnsafeReadOnlyPtr<T>(), data.Length, UnsafeUtility.SizeOf<T>());
		}

		// Token: 0x060017AC RID: 6060
		[NativeConditional("ENABLE_PROFILER")]
		[NativeMethod(Name = "ProfilerBindings::Internal_EmitFrameMetaData_Array", IsFreeFunction = true)]
		[ThreadSafe]
		[MethodImpl(4096)]
		private static extern void Internal_EmitFrameMetaData_Array(byte[] id, int tag, Array data, int count, int elementSize);

		// Token: 0x060017AD RID: 6061
		[NativeMethod(Name = "ProfilerBindings::Internal_EmitFrameMetaData_Native", IsFreeFunction = true)]
		[ThreadSafe]
		[NativeConditional("ENABLE_PROFILER")]
		[MethodImpl(4096)]
		private static extern void Internal_EmitFrameMetaData_Native(byte[] id, int tag, IntPtr data, int count, int elementSize);

		// Token: 0x04000749 RID: 1865
		internal const uint invalidProfilerArea = 4294967295U;
	}
}
