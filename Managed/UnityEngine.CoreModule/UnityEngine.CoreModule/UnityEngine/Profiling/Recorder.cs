using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Profiling
{
	// Token: 0x02000213 RID: 531
	[NativeHeader("Runtime/Profiler/Recorder.h")]
	[NativeHeader("Runtime/Profiler/ScriptBindings/Recorder.bindings.h")]
	[UsedByNativeCode]
	[StructLayout(0)]
	public sealed class Recorder
	{
		// Token: 0x060017AE RID: 6062 RVA: 0x000166AA File Offset: 0x000148AA
		internal Recorder()
		{
		}

		// Token: 0x060017AF RID: 6063 RVA: 0x0002646F File Offset: 0x0002466F
		internal Recorder(IntPtr ptr)
		{
			this.m_Ptr = ptr;
		}

		// Token: 0x060017B0 RID: 6064 RVA: 0x00026480 File Offset: 0x00024680
		protected override void Finalize()
		{
			try
			{
				bool flag = this.m_Ptr != IntPtr.Zero;
				if (flag)
				{
					Recorder.DisposeNative(this.m_Ptr);
				}
			}
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x060017B1 RID: 6065 RVA: 0x000264CC File Offset: 0x000246CC
		public static Recorder Get(string samplerName)
		{
			IntPtr @internal = Recorder.GetInternal(samplerName);
			bool flag = @internal == IntPtr.Zero;
			Recorder recorder;
			if (flag)
			{
				recorder = Recorder.s_InvalidRecorder;
			}
			else
			{
				recorder = new Recorder(@internal);
			}
			return recorder;
		}

		// Token: 0x060017B2 RID: 6066
		[NativeMethod(Name = "ProfilerBindings::GetRecorderInternal", IsFreeFunction = true)]
		[MethodImpl(4096)]
		private static extern IntPtr GetInternal(string samplerName);

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x060017B3 RID: 6067 RVA: 0x00026504 File Offset: 0x00024704
		public bool isValid
		{
			get
			{
				return this.m_Ptr != IntPtr.Zero;
			}
		}

		// Token: 0x060017B4 RID: 6068
		[NativeMethod(Name = "ProfilerBindings::DisposeNativeRecorder", IsFreeFunction = true, IsThreadSafe = true)]
		[MethodImpl(4096)]
		private static extern void DisposeNative(IntPtr ptr);

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x060017B5 RID: 6069 RVA: 0x00026528 File Offset: 0x00024728
		// (set) Token: 0x060017B6 RID: 6070 RVA: 0x0002654C File Offset: 0x0002474C
		public bool enabled
		{
			get
			{
				return this.isValid && this.IsEnabled();
			}
			set
			{
				bool isValid = this.isValid;
				if (isValid)
				{
					this.SetEnabled(value);
				}
			}
		}

		// Token: 0x060017B7 RID: 6071
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(4096)]
		private extern bool IsEnabled();

		// Token: 0x060017B8 RID: 6072
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(4096)]
		private extern void SetEnabled(bool enabled);

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x060017B9 RID: 6073 RVA: 0x0002656C File Offset: 0x0002476C
		public long elapsedNanoseconds
		{
			get
			{
				return this.isValid ? this.GetElapsedNanoseconds() : 0L;
			}
		}

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x060017BA RID: 6074 RVA: 0x00026590 File Offset: 0x00024790
		public long gpuElapsedNanoseconds
		{
			get
			{
				return this.isValid ? this.GetGpuElapsedNanoseconds() : 0L;
			}
		}

		// Token: 0x060017BB RID: 6075
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(4096)]
		private extern long GetElapsedNanoseconds();

		// Token: 0x060017BC RID: 6076
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(4096)]
		private extern long GetGpuElapsedNanoseconds();

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x060017BD RID: 6077 RVA: 0x000265B4 File Offset: 0x000247B4
		public int sampleBlockCount
		{
			get
			{
				return this.isValid ? this.GetSampleBlockCount() : 0;
			}
		}

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x060017BE RID: 6078 RVA: 0x000265D8 File Offset: 0x000247D8
		public int gpuSampleBlockCount
		{
			get
			{
				return this.isValid ? this.GetGpuSampleBlockCount() : 0;
			}
		}

		// Token: 0x060017BF RID: 6079
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(4096)]
		private extern int GetSampleBlockCount();

		// Token: 0x060017C0 RID: 6080
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(4096)]
		private extern int GetGpuSampleBlockCount();

		// Token: 0x060017C1 RID: 6081
		[ThreadSafe]
		[MethodImpl(4096)]
		public extern void FilterToCurrentThread();

		// Token: 0x060017C2 RID: 6082
		[ThreadSafe]
		[MethodImpl(4096)]
		public extern void CollectFromAllThreads();

		// Token: 0x0400074A RID: 1866
		internal IntPtr m_Ptr;

		// Token: 0x0400074B RID: 1867
		internal static Recorder s_InvalidRecorder = new Recorder();
	}
}
