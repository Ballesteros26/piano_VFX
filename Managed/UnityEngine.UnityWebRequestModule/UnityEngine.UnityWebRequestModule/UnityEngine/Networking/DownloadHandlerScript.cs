using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine.Networking
{
	// Token: 0x02000010 RID: 16
	[NativeHeader("Modules/UnityWebRequest/Public/DownloadHandler/DownloadHandlerScript.h")]
	[StructLayout(0)]
	public class DownloadHandlerScript : DownloadHandler
	{
		// Token: 0x060000F1 RID: 241
		[MethodImpl(4096)]
		private static extern IntPtr Create(DownloadHandlerScript obj);

		// Token: 0x060000F2 RID: 242
		[MethodImpl(4096)]
		private static extern IntPtr CreatePreallocated(DownloadHandlerScript obj, byte[] preallocatedBuffer);

		// Token: 0x060000F3 RID: 243 RVA: 0x00004CCD File Offset: 0x00002ECD
		private void InternalCreateScript()
		{
			this.m_Ptr = DownloadHandlerScript.Create(this);
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00004CDC File Offset: 0x00002EDC
		private void InternalCreateScript(byte[] preallocatedBuffer)
		{
			this.m_Ptr = DownloadHandlerScript.CreatePreallocated(this, preallocatedBuffer);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00004CEC File Offset: 0x00002EEC
		public DownloadHandlerScript()
		{
			this.InternalCreateScript();
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00004D00 File Offset: 0x00002F00
		public DownloadHandlerScript(byte[] preallocatedBuffer)
		{
			bool flag = preallocatedBuffer == null || preallocatedBuffer.Length < 1;
			if (flag)
			{
				throw new ArgumentException("Cannot create a preallocated-buffer DownloadHandlerScript backed by a null or zero-length array");
			}
			this.InternalCreateScript(preallocatedBuffer);
		}
	}
}
