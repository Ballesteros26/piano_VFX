using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine.Networking
{
	// Token: 0x02000011 RID: 17
	[NativeHeader("Modules/UnityWebRequest/Public/DownloadHandler/DownloadHandlerVFS.h")]
	[StructLayout(0)]
	public sealed class DownloadHandlerFile : DownloadHandler
	{
		// Token: 0x060000F7 RID: 247
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern IntPtr Create(DownloadHandlerFile obj, string path, bool append);

		// Token: 0x060000F8 RID: 248 RVA: 0x00004D3C File Offset: 0x00002F3C
		private void InternalCreateVFS(string path, bool append)
		{
			string directoryName = Path.GetDirectoryName(path);
			bool flag = !Directory.Exists(directoryName);
			if (flag)
			{
				Directory.CreateDirectory(directoryName);
			}
			this.m_Ptr = DownloadHandlerFile.Create(this, path, append);
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00004D73 File Offset: 0x00002F73
		public DownloadHandlerFile(string path)
		{
			this.InternalCreateVFS(path, false);
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00004D86 File Offset: 0x00002F86
		public DownloadHandlerFile(string path, bool append)
		{
			this.InternalCreateVFS(path, append);
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00004D99 File Offset: 0x00002F99
		protected override byte[] GetData()
		{
			throw new NotSupportedException("Raw data access is not supported");
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00004DA6 File Offset: 0x00002FA6
		protected override string GetText()
		{
			throw new NotSupportedException("String access is not supported");
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000FD RID: 253
		// (set) Token: 0x060000FE RID: 254
		public extern bool removeFileOnAbort
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}
	}
}
