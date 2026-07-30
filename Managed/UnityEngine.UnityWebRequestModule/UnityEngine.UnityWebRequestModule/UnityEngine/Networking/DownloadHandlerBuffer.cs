using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine.Networking
{
	// Token: 0x0200000F RID: 15
	[NativeHeader("Modules/UnityWebRequest/Public/DownloadHandler/DownloadHandlerBuffer.h")]
	[StructLayout(0)]
	public sealed class DownloadHandlerBuffer : DownloadHandler
	{
		// Token: 0x060000EB RID: 235
		[MethodImpl(4096)]
		private static extern IntPtr Create(DownloadHandlerBuffer obj);

		// Token: 0x060000EC RID: 236 RVA: 0x00004C5D File Offset: 0x00002E5D
		private void InternalCreateBuffer()
		{
			this.m_Ptr = DownloadHandlerBuffer.Create(this);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00004C6C File Offset: 0x00002E6C
		public DownloadHandlerBuffer()
		{
			this.InternalCreateBuffer();
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00004C80 File Offset: 0x00002E80
		protected override byte[] GetData()
		{
			return this.InternalGetData();
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00004C98 File Offset: 0x00002E98
		private byte[] InternalGetData()
		{
			return DownloadHandler.InternalGetByteArray(this);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00004CB0 File Offset: 0x00002EB0
		public static string GetContent(UnityWebRequest www)
		{
			return DownloadHandler.GetCheckedDownloader<DownloadHandlerBuffer>(www).text;
		}
	}
}
