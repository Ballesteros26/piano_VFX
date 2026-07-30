using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine.Networking
{
	// Token: 0x02000013 RID: 19
	[NativeHeader("Modules/UnityWebRequest/Public/UploadHandler/UploadHandlerRaw.h")]
	[StructLayout(0)]
	public sealed class UploadHandlerRaw : UploadHandler
	{
		// Token: 0x0600010E RID: 270
		[MethodImpl(4096)]
		private static extern IntPtr Create(UploadHandlerRaw self, byte[] data);

		// Token: 0x0600010F RID: 271 RVA: 0x00004EC0 File Offset: 0x000030C0
		public UploadHandlerRaw(byte[] data)
		{
			bool flag = data != null && data.Length == 0;
			if (flag)
			{
				throw new ArgumentException("Cannot create a data handler without payload data");
			}
			this.m_Ptr = UploadHandlerRaw.Create(this, data);
		}

		// Token: 0x06000110 RID: 272
		[MethodImpl(4096)]
		private extern byte[] InternalGetData();

		// Token: 0x06000111 RID: 273 RVA: 0x00004EFC File Offset: 0x000030FC
		internal override byte[] GetData()
		{
			return this.InternalGetData();
		}
	}
}
