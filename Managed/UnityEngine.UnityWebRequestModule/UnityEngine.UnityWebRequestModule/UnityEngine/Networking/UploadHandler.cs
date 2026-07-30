using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine.Networking
{
	// Token: 0x02000012 RID: 18
	[NativeHeader("Modules/UnityWebRequest/Public/UploadHandler/UploadHandler.h")]
	[StructLayout(0)]
	public class UploadHandler : IDisposable
	{
		// Token: 0x060000FF RID: 255
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(4096)]
		private extern void Release();

		// Token: 0x06000100 RID: 256 RVA: 0x000049A6 File Offset: 0x00002BA6
		internal UploadHandler()
		{
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00004DB4 File Offset: 0x00002FB4
		~UploadHandler()
		{
			this.Dispose();
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00004DE4 File Offset: 0x00002FE4
		public void Dispose()
		{
			bool flag = this.m_Ptr != IntPtr.Zero;
			if (flag)
			{
				this.Release();
				this.m_Ptr = IntPtr.Zero;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000103 RID: 259 RVA: 0x00004E1C File Offset: 0x0000301C
		public byte[] data
		{
			get
			{
				return this.GetData();
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000104 RID: 260 RVA: 0x00004E34 File Offset: 0x00003034
		// (set) Token: 0x06000105 RID: 261 RVA: 0x00004E4C File Offset: 0x0000304C
		public string contentType
		{
			get
			{
				return this.GetContentType();
			}
			set
			{
				this.SetContentType(value);
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000106 RID: 262 RVA: 0x00004E58 File Offset: 0x00003058
		public float progress
		{
			get
			{
				return this.GetProgress();
			}
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00004E70 File Offset: 0x00003070
		internal virtual byte[] GetData()
		{
			return null;
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00004E84 File Offset: 0x00003084
		internal virtual string GetContentType()
		{
			return this.InternalGetContentType();
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00004E9C File Offset: 0x0000309C
		internal virtual void SetContentType(string newContentType)
		{
			this.InternalSetContentType(newContentType);
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00004EA8 File Offset: 0x000030A8
		internal virtual float GetProgress()
		{
			return this.InternalGetProgress();
		}

		// Token: 0x0600010B RID: 267
		[NativeMethod("GetContentType")]
		[MethodImpl(4096)]
		private extern string InternalGetContentType();

		// Token: 0x0600010C RID: 268
		[NativeMethod("SetContentType")]
		[MethodImpl(4096)]
		private extern void InternalSetContentType(string newContentType);

		// Token: 0x0600010D RID: 269
		[NativeMethod("GetProgress")]
		[MethodImpl(4096)]
		private extern float InternalGetProgress();

		// Token: 0x04000053 RID: 83
		[NonSerialized]
		internal IntPtr m_Ptr;
	}
}
