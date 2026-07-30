using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Networking
{
	// Token: 0x0200000E RID: 14
	[NativeHeader("Modules/UnityWebRequest/Public/DownloadHandler/DownloadHandler.h")]
	[StructLayout(0)]
	public class DownloadHandler : IDisposable
	{
		// Token: 0x060000D6 RID: 214
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(4096)]
		private extern void Release();

		// Token: 0x060000D7 RID: 215 RVA: 0x000049A6 File Offset: 0x00002BA6
		[VisibleToOtherModules]
		internal DownloadHandler()
		{
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x000049B0 File Offset: 0x00002BB0
		~DownloadHandler()
		{
			this.Dispose();
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x000049E0 File Offset: 0x00002BE0
		public void Dispose()
		{
			bool flag = this.m_Ptr != IntPtr.Zero;
			if (flag)
			{
				this.Release();
				this.m_Ptr = IntPtr.Zero;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00004A18 File Offset: 0x00002C18
		public bool isDone
		{
			get
			{
				return this.IsDone();
			}
		}

		// Token: 0x060000DB RID: 219
		[MethodImpl(4096)]
		private extern bool IsDone();

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000DC RID: 220 RVA: 0x00004A30 File Offset: 0x00002C30
		public string error
		{
			get
			{
				return this.GetErrorMsg();
			}
		}

		// Token: 0x060000DD RID: 221
		[MethodImpl(4096)]
		private extern string GetErrorMsg();

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000DE RID: 222 RVA: 0x00004A48 File Offset: 0x00002C48
		public byte[] data
		{
			get
			{
				return this.GetData();
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000DF RID: 223 RVA: 0x00004A60 File Offset: 0x00002C60
		public string text
		{
			get
			{
				return this.GetText();
			}
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00004A78 File Offset: 0x00002C78
		protected virtual byte[] GetData()
		{
			return null;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00004A8C File Offset: 0x00002C8C
		protected virtual string GetText()
		{
			byte[] data = this.GetData();
			bool flag = data != null && data.Length != 0;
			string text;
			if (flag)
			{
				text = this.GetTextEncoder().GetString(data, 0, data.Length);
			}
			else
			{
				text = "";
			}
			return text;
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00004AD0 File Offset: 0x00002CD0
		private Encoding GetTextEncoder()
		{
			string contentType = this.GetContentType();
			bool flag = !string.IsNullOrEmpty(contentType);
			if (flag)
			{
				int num = contentType.IndexOf("charset", 5);
				bool flag2 = num > -1;
				if (flag2)
				{
					int num2 = contentType.IndexOf('=', num);
					bool flag3 = num2 > -1;
					if (flag3)
					{
						string text = contentType.Substring(num2 + 1).Trim().Trim(new char[] { '\'', '"' })
							.Trim();
						int num3 = text.IndexOf(';');
						bool flag4 = num3 > -1;
						if (flag4)
						{
							text = text.Substring(0, num3);
						}
						try
						{
							return Encoding.GetEncoding(text);
						}
						catch (ArgumentException ex)
						{
							Debug.LogWarning(string.Format("Unsupported encoding '{0}': {1}", text, ex.Message));
						}
					}
				}
			}
			return Encoding.UTF8;
		}

		// Token: 0x060000E3 RID: 227
		[MethodImpl(4096)]
		private extern string GetContentType();

		// Token: 0x060000E4 RID: 228 RVA: 0x00004BC0 File Offset: 0x00002DC0
		[UsedByNativeCode]
		protected virtual bool ReceiveData(byte[] data, int dataLength)
		{
			return true;
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00004BD3 File Offset: 0x00002DD3
		[UsedByNativeCode]
		protected virtual void ReceiveContentLengthHeader(ulong contentLength)
		{
			this.ReceiveContentLength((int)contentLength);
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00004BDF File Offset: 0x00002DDF
		[Obsolete("Use ReceiveContentLengthHeader")]
		protected virtual void ReceiveContentLength(int contentLength)
		{
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00004BDF File Offset: 0x00002DDF
		[UsedByNativeCode]
		protected virtual void CompleteContent()
		{
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00004BE4 File Offset: 0x00002DE4
		[UsedByNativeCode]
		protected virtual float GetProgress()
		{
			return 0f;
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00004BFC File Offset: 0x00002DFC
		protected static T GetCheckedDownloader<T>(UnityWebRequest www) where T : DownloadHandler
		{
			bool flag = www == null;
			if (flag)
			{
				throw new NullReferenceException("Cannot get content from a null UnityWebRequest object");
			}
			bool flag2 = !www.isDone;
			if (flag2)
			{
				throw new InvalidOperationException("Cannot get content from an unfinished UnityWebRequest object");
			}
			bool flag3 = www.result == UnityWebRequest.Result.ProtocolError;
			if (flag3)
			{
				throw new InvalidOperationException(www.error);
			}
			return (T)((object)www.downloadHandler);
		}

		// Token: 0x060000EA RID: 234
		[NativeThrows]
		[VisibleToOtherModules]
		[MethodImpl(4096)]
		internal static extern byte[] InternalGetByteArray(DownloadHandler dh);

		// Token: 0x04000052 RID: 82
		[VisibleToOtherModules]
		[NonSerialized]
		internal IntPtr m_Ptr;
	}
}
