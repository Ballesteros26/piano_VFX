using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine.Networking
{
	// Token: 0x02000014 RID: 20
	[NativeHeader("Modules/UnityWebRequest/Public/UploadHandler/UploadHandlerFile.h")]
	[StructLayout(0)]
	public sealed class UploadHandlerFile : UploadHandler
	{
		// Token: 0x06000112 RID: 274
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern IntPtr Create(UploadHandlerFile self, string filePath);

		// Token: 0x06000113 RID: 275 RVA: 0x00004F14 File Offset: 0x00003114
		public UploadHandlerFile(string filePath)
		{
			this.m_Ptr = UploadHandlerFile.Create(this, filePath);
		}
	}
}
