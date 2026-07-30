using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Networking
{
	// Token: 0x0200000D RID: 13
	[NativeHeader("Modules/UnityWebRequest/Public/CertificateHandler/CertificateHandlerScript.h")]
	[StructLayout(0)]
	public class CertificateHandler : IDisposable
	{
		// Token: 0x060000CF RID: 207
		[MethodImpl(4096)]
		private static extern IntPtr Create(CertificateHandler obj);

		// Token: 0x060000D0 RID: 208
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(4096)]
		private extern void Release();

		// Token: 0x060000D1 RID: 209 RVA: 0x000048F8 File Offset: 0x00002AF8
		protected CertificateHandler()
		{
			this.m_Ptr = CertificateHandler.Create(this);
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00004910 File Offset: 0x00002B10
		~CertificateHandler()
		{
			this.Dispose();
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00004940 File Offset: 0x00002B40
		protected virtual bool ValidateCertificate(byte[] certificateData)
		{
			return false;
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00004954 File Offset: 0x00002B54
		[RequiredByNativeCode]
		internal bool ValidateCertificateNative(byte[] certificateData)
		{
			return this.ValidateCertificate(certificateData);
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00004970 File Offset: 0x00002B70
		public void Dispose()
		{
			bool flag = this.m_Ptr != IntPtr.Zero;
			if (flag)
			{
				this.Release();
				this.m_Ptr = IntPtr.Zero;
			}
		}

		// Token: 0x04000051 RID: 81
		[NonSerialized]
		internal IntPtr m_Ptr;
	}
}
