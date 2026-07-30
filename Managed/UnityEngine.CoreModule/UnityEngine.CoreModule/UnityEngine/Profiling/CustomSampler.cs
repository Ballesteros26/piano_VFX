using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Profiling
{
	// Token: 0x02000215 RID: 533
	[NativeHeader("Runtime/Profiler/Marker.h")]
	[NativeHeader("Runtime/Profiler/ScriptBindings/Sampler.bindings.h")]
	[UsedByNativeCode]
	public sealed class CustomSampler : Sampler
	{
		// Token: 0x060017D0 RID: 6096 RVA: 0x000266F7 File Offset: 0x000248F7
		internal CustomSampler()
		{
		}

		// Token: 0x060017D1 RID: 6097 RVA: 0x00026701 File Offset: 0x00024901
		internal CustomSampler(IntPtr ptr)
		{
			this.m_Ptr = ptr;
		}

		// Token: 0x060017D2 RID: 6098 RVA: 0x00026714 File Offset: 0x00024914
		public static CustomSampler Create(string name, bool collectGpuData = false)
		{
			IntPtr intPtr = CustomSampler.CreateInternal(name, collectGpuData);
			bool flag = intPtr == IntPtr.Zero;
			CustomSampler customSampler;
			if (flag)
			{
				customSampler = CustomSampler.s_InvalidCustomSampler;
			}
			else
			{
				customSampler = new CustomSampler(intPtr);
			}
			return customSampler;
		}

		// Token: 0x060017D3 RID: 6099
		[NativeMethod(Name = "ProfilerBindings::CreateCustomSamplerInternal", IsFreeFunction = true, ThrowsException = true, IsThreadSafe = true)]
		[MethodImpl(4096)]
		private static extern IntPtr CreateInternal([NotNull] string name, bool collectGpuData);

		// Token: 0x060017D4 RID: 6100 RVA: 0x0002674B File Offset: 0x0002494B
		[Conditional("ENABLE_PROFILER")]
		public void Begin()
		{
			CustomSampler.Begin_Internal(this.m_Ptr);
		}

		// Token: 0x060017D5 RID: 6101 RVA: 0x0002675A File Offset: 0x0002495A
		[Conditional("ENABLE_PROFILER")]
		public void Begin(Object targetObject)
		{
			CustomSampler.BeginWithObject_Internal(this.m_Ptr, targetObject);
		}

		// Token: 0x060017D6 RID: 6102 RVA: 0x0002676A File Offset: 0x0002496A
		[Conditional("ENABLE_PROFILER")]
		public void End()
		{
			CustomSampler.End_Internal(this.m_Ptr);
		}

		// Token: 0x060017D7 RID: 6103
		[NativeMethod(Name = "ProfilerBindings::CustomSampler_Begin", IsFreeFunction = true, IsThreadSafe = true)]
		[MethodImpl(4096)]
		private static extern void Begin_Internal(IntPtr ptr);

		// Token: 0x060017D8 RID: 6104
		[NativeMethod(Name = "ProfilerBindings::CustomSampler_BeginWithObject", IsFreeFunction = true, IsThreadSafe = true)]
		[MethodImpl(4096)]
		private static extern void BeginWithObject_Internal(IntPtr ptr, Object targetObject);

		// Token: 0x060017D9 RID: 6105
		[NativeMethod(Name = "ProfilerBindings::CustomSampler_End", IsFreeFunction = true, IsThreadSafe = true)]
		[MethodImpl(4096)]
		private static extern void End_Internal(IntPtr ptr);

		// Token: 0x0400074E RID: 1870
		internal static CustomSampler s_InvalidCustomSampler = new CustomSampler();
	}
}
