using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Profiling
{
	// Token: 0x02000214 RID: 532
	[UsedByNativeCode]
	[NativeHeader("Runtime/Profiler/ScriptBindings/Sampler.bindings.h")]
	[NativeHeader("Runtime/Profiler/Marker.h")]
	public class Sampler
	{
		// Token: 0x060017C4 RID: 6084 RVA: 0x000166AA File Offset: 0x000148AA
		internal Sampler()
		{
		}

		// Token: 0x060017C5 RID: 6085 RVA: 0x00026607 File Offset: 0x00024807
		internal Sampler(IntPtr ptr)
		{
			this.m_Ptr = ptr;
		}

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x060017C6 RID: 6086 RVA: 0x00026618 File Offset: 0x00024818
		public bool isValid
		{
			get
			{
				return this.m_Ptr != IntPtr.Zero;
			}
		}

		// Token: 0x060017C7 RID: 6087 RVA: 0x0002663C File Offset: 0x0002483C
		public Recorder GetRecorder()
		{
			IntPtr recorderInternal = Sampler.GetRecorderInternal(this.m_Ptr);
			bool flag = recorderInternal == IntPtr.Zero;
			Recorder recorder;
			if (flag)
			{
				recorder = Recorder.s_InvalidRecorder;
			}
			else
			{
				recorder = new Recorder(recorderInternal);
			}
			return recorder;
		}

		// Token: 0x060017C8 RID: 6088 RVA: 0x00026678 File Offset: 0x00024878
		public static Sampler Get(string name)
		{
			IntPtr samplerInternal = Sampler.GetSamplerInternal(name);
			bool flag = samplerInternal == IntPtr.Zero;
			Sampler sampler;
			if (flag)
			{
				sampler = Sampler.s_InvalidSampler;
			}
			else
			{
				sampler = new Sampler(samplerInternal);
			}
			return sampler;
		}

		// Token: 0x060017C9 RID: 6089 RVA: 0x000266B0 File Offset: 0x000248B0
		public static int GetNames(List<string> names)
		{
			return Sampler.GetSamplerNamesInternal(names);
		}

		// Token: 0x060017CA RID: 6090
		[NativeConditional("ENABLE_PROFILER")]
		[NativeMethod(Name = "GetName", IsThreadSafe = true)]
		[MethodImpl(4096)]
		private extern string GetSamplerName();

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x060017CB RID: 6091 RVA: 0x000266C8 File Offset: 0x000248C8
		public string name
		{
			get
			{
				return this.isValid ? this.GetSamplerName() : null;
			}
		}

		// Token: 0x060017CC RID: 6092
		[NativeMethod(Name = "ProfilerBindings::GetRecorderInternal", IsFreeFunction = true)]
		[MethodImpl(4096)]
		private static extern IntPtr GetRecorderInternal(IntPtr ptr);

		// Token: 0x060017CD RID: 6093
		[NativeMethod(Name = "ProfilerBindings::GetSamplerInternal", IsFreeFunction = true)]
		[MethodImpl(4096)]
		private static extern IntPtr GetSamplerInternal([NotNull] string name);

		// Token: 0x060017CE RID: 6094
		[NativeMethod(Name = "ProfilerBindings::GetSamplerNamesInternal", IsFreeFunction = true)]
		[MethodImpl(4096)]
		private static extern int GetSamplerNamesInternal(List<string> namesScriptingPtr);

		// Token: 0x0400074C RID: 1868
		internal IntPtr m_Ptr;

		// Token: 0x0400074D RID: 1869
		internal static Sampler s_InvalidSampler = new Sampler();
	}
}
