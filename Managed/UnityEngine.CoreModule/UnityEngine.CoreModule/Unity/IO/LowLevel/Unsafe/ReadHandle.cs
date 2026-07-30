using System;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Bindings;

namespace Unity.IO.LowLevel.Unsafe
{
	// Token: 0x0200004F RID: 79
	public struct ReadHandle : IDisposable
	{
		// Token: 0x060000C4 RID: 196 RVA: 0x00002C08 File Offset: 0x00000E08
		public bool IsValid()
		{
			return ReadHandle.IsReadHandleValid(this);
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00002C28 File Offset: 0x00000E28
		public void Dispose()
		{
			bool flag = !ReadHandle.IsReadHandleValid(this);
			if (flag)
			{
				throw new InvalidOperationException("ReadHandle.Dispose cannot be called twice on the same ReadHandle");
			}
			bool flag2 = this.Status == ReadStatus.InProgress;
			if (flag2)
			{
				throw new InvalidOperationException("ReadHandle.Dispose cannot be called until the read operation completes");
			}
			ReadHandle.ReleaseReadHandle(this);
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x00002C78 File Offset: 0x00000E78
		public JobHandle JobHandle
		{
			get
			{
				bool flag = !ReadHandle.IsReadHandleValid(this);
				if (flag)
				{
					throw new InvalidOperationException("ReadHandle.JobHandle cannot be called after the ReadHandle has been disposed");
				}
				return ReadHandle.GetJobHandle(this);
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x00002CB4 File Offset: 0x00000EB4
		public ReadStatus Status
		{
			get
			{
				bool flag = !ReadHandle.IsReadHandleValid(this);
				if (flag)
				{
					throw new InvalidOperationException("ReadHandle.Status cannot be called after the ReadHandle has been disposed");
				}
				return ReadHandle.GetReadStatus(this);
			}
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00002CEE File Offset: 0x00000EEE
		[ThreadAndSerializationSafe]
		[FreeFunction("AsyncReadManagerManaged::GetReadStatus", IsThreadSafe = true)]
		private static ReadStatus GetReadStatus(ReadHandle handle)
		{
			return ReadHandle.GetReadStatus_Injected(ref handle);
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00002CF7 File Offset: 0x00000EF7
		[FreeFunction("AsyncReadManagerManaged::ReleaseReadHandle", IsThreadSafe = true)]
		[ThreadAndSerializationSafe]
		private static void ReleaseReadHandle(ReadHandle handle)
		{
			ReadHandle.ReleaseReadHandle_Injected(ref handle);
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00002D00 File Offset: 0x00000F00
		[FreeFunction("AsyncReadManagerManaged::IsReadHandleValid", IsThreadSafe = true)]
		[ThreadAndSerializationSafe]
		private static bool IsReadHandleValid(ReadHandle handle)
		{
			return ReadHandle.IsReadHandleValid_Injected(ref handle);
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00002D0C File Offset: 0x00000F0C
		[FreeFunction("AsyncReadManagerManaged::GetJobHandle", IsThreadSafe = true)]
		[ThreadAndSerializationSafe]
		private static JobHandle GetJobHandle(ReadHandle handle)
		{
			JobHandle jobHandle;
			ReadHandle.GetJobHandle_Injected(ref handle, out jobHandle);
			return jobHandle;
		}

		// Token: 0x060000CC RID: 204
		[MethodImpl(4096)]
		private static extern ReadStatus GetReadStatus_Injected(ref ReadHandle handle);

		// Token: 0x060000CD RID: 205
		[MethodImpl(4096)]
		private static extern void ReleaseReadHandle_Injected(ref ReadHandle handle);

		// Token: 0x060000CE RID: 206
		[MethodImpl(4096)]
		private static extern bool IsReadHandleValid_Injected(ref ReadHandle handle);

		// Token: 0x060000CF RID: 207
		[MethodImpl(4096)]
		private static extern void GetJobHandle_Injected(ref ReadHandle handle, out JobHandle ret);

		// Token: 0x04000101 RID: 257
		[NativeDisableUnsafePtrRestriction]
		internal IntPtr ptr;

		// Token: 0x04000102 RID: 258
		internal int version;
	}
}
