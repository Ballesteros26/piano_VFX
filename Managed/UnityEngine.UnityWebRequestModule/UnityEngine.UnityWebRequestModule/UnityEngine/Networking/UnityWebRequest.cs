using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine.Bindings;
using UnityEngineInternal;

namespace UnityEngine.Networking
{
	// Token: 0x02000009 RID: 9
	[NativeHeader("Modules/UnityWebRequest/Public/UnityWebRequest.h")]
	[StructLayout(0)]
	public class UnityWebRequest : IDisposable
	{
		// Token: 0x0600004A RID: 74
		[NativeMethod(IsThreadSafe = true)]
		[NativeConditional("ENABLE_UNITYWEBREQUEST")]
		[MethodImpl(4096)]
		private static extern string GetWebErrorString(UnityWebRequest.UnityWebRequestError err);

		// Token: 0x0600004B RID: 75
		[VisibleToOtherModules]
		[MethodImpl(4096)]
		internal static extern string GetHTTPStatusString(long responseCode);

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600004C RID: 76 RVA: 0x0000351A File Offset: 0x0000171A
		// (set) Token: 0x0600004D RID: 77 RVA: 0x00003522 File Offset: 0x00001722
		public bool disposeCertificateHandlerOnDispose { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600004E RID: 78 RVA: 0x0000352B File Offset: 0x0000172B
		// (set) Token: 0x0600004F RID: 79 RVA: 0x00003533 File Offset: 0x00001733
		public bool disposeDownloadHandlerOnDispose { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000050 RID: 80 RVA: 0x0000353C File Offset: 0x0000173C
		// (set) Token: 0x06000051 RID: 81 RVA: 0x00003544 File Offset: 0x00001744
		public bool disposeUploadHandlerOnDispose { get; set; }

		// Token: 0x06000052 RID: 82 RVA: 0x0000354D File Offset: 0x0000174D
		public static void ClearCookieCache()
		{
			UnityWebRequest.ClearCookieCache(null, null);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00003558 File Offset: 0x00001758
		public static void ClearCookieCache(Uri uri)
		{
			bool flag = uri == null;
			if (flag)
			{
				UnityWebRequest.ClearCookieCache(null, null);
			}
			else
			{
				string host = uri.Host;
				string text = uri.AbsolutePath;
				bool flag2 = text == "/";
				if (flag2)
				{
					text = null;
				}
				UnityWebRequest.ClearCookieCache(host, text);
			}
		}

		// Token: 0x06000054 RID: 84
		[MethodImpl(4096)]
		private static extern void ClearCookieCache(string domain, string path);

		// Token: 0x06000055 RID: 85
		[MethodImpl(4096)]
		internal static extern IntPtr Create();

		// Token: 0x06000056 RID: 86
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(4096)]
		private extern void Release();

		// Token: 0x06000057 RID: 87 RVA: 0x000035A4 File Offset: 0x000017A4
		internal void InternalDestroy()
		{
			bool flag = this.m_Ptr != IntPtr.Zero;
			if (flag)
			{
				this.Abort();
				this.Release();
				this.m_Ptr = IntPtr.Zero;
			}
		}

		// Token: 0x06000058 RID: 88 RVA: 0x000035E1 File Offset: 0x000017E1
		private void InternalSetDefaults()
		{
			this.disposeDownloadHandlerOnDispose = true;
			this.disposeUploadHandlerOnDispose = true;
			this.disposeCertificateHandlerOnDispose = true;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x000035FC File Offset: 0x000017FC
		public UnityWebRequest()
		{
			this.m_Ptr = UnityWebRequest.Create();
			this.InternalSetDefaults();
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00003618 File Offset: 0x00001818
		public UnityWebRequest(string url)
		{
			this.m_Ptr = UnityWebRequest.Create();
			this.InternalSetDefaults();
			this.url = url;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x0000363C File Offset: 0x0000183C
		public UnityWebRequest(Uri uri)
		{
			this.m_Ptr = UnityWebRequest.Create();
			this.InternalSetDefaults();
			this.uri = uri;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00003660 File Offset: 0x00001860
		public UnityWebRequest(string url, string method)
		{
			this.m_Ptr = UnityWebRequest.Create();
			this.InternalSetDefaults();
			this.url = url;
			this.method = method;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x0000368C File Offset: 0x0000188C
		public UnityWebRequest(Uri uri, string method)
		{
			this.m_Ptr = UnityWebRequest.Create();
			this.InternalSetDefaults();
			this.uri = uri;
			this.method = method;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x000036B8 File Offset: 0x000018B8
		public UnityWebRequest(string url, string method, DownloadHandler downloadHandler, UploadHandler uploadHandler)
		{
			this.m_Ptr = UnityWebRequest.Create();
			this.InternalSetDefaults();
			this.url = url;
			this.method = method;
			this.downloadHandler = downloadHandler;
			this.uploadHandler = uploadHandler;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000036F5 File Offset: 0x000018F5
		public UnityWebRequest(Uri uri, string method, DownloadHandler downloadHandler, UploadHandler uploadHandler)
		{
			this.m_Ptr = UnityWebRequest.Create();
			this.InternalSetDefaults();
			this.uri = uri;
			this.method = method;
			this.downloadHandler = downloadHandler;
			this.uploadHandler = uploadHandler;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003734 File Offset: 0x00001934
		~UnityWebRequest()
		{
			this.DisposeHandlers();
			this.InternalDestroy();
		}

		// Token: 0x06000061 RID: 97 RVA: 0x0000376C File Offset: 0x0000196C
		public void Dispose()
		{
			this.DisposeHandlers();
			this.InternalDestroy();
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003784 File Offset: 0x00001984
		private void DisposeHandlers()
		{
			bool disposeDownloadHandlerOnDispose = this.disposeDownloadHandlerOnDispose;
			if (disposeDownloadHandlerOnDispose)
			{
				DownloadHandler downloadHandler = this.downloadHandler;
				bool flag = downloadHandler != null;
				if (flag)
				{
					downloadHandler.Dispose();
				}
			}
			bool disposeUploadHandlerOnDispose = this.disposeUploadHandlerOnDispose;
			if (disposeUploadHandlerOnDispose)
			{
				UploadHandler uploadHandler = this.uploadHandler;
				bool flag2 = uploadHandler != null;
				if (flag2)
				{
					uploadHandler.Dispose();
				}
			}
			bool disposeCertificateHandlerOnDispose = this.disposeCertificateHandlerOnDispose;
			if (disposeCertificateHandlerOnDispose)
			{
				CertificateHandler certificateHandler = this.certificateHandler;
				bool flag3 = certificateHandler != null;
				if (flag3)
				{
					certificateHandler.Dispose();
				}
			}
		}

		// Token: 0x06000063 RID: 99
		[NativeThrows]
		[MethodImpl(4096)]
		internal extern UnityWebRequestAsyncOperation BeginWebRequest();

		// Token: 0x06000064 RID: 100 RVA: 0x0000380C File Offset: 0x00001A0C
		[Obsolete("Use SendWebRequest.  It returns a UnityWebRequestAsyncOperation which contains a reference to the WebRequest object.", false)]
		public AsyncOperation Send()
		{
			return this.SendWebRequest();
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003824 File Offset: 0x00001A24
		public UnityWebRequestAsyncOperation SendWebRequest()
		{
			UnityWebRequestAsyncOperation unityWebRequestAsyncOperation = this.BeginWebRequest();
			bool flag = unityWebRequestAsyncOperation != null;
			if (flag)
			{
				unityWebRequestAsyncOperation.webRequest = this;
			}
			return unityWebRequestAsyncOperation;
		}

		// Token: 0x06000066 RID: 102
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public extern void Abort();

		// Token: 0x06000067 RID: 103
		[MethodImpl(4096)]
		private extern UnityWebRequest.UnityWebRequestError SetMethod(UnityWebRequest.UnityWebRequestMethod methodType);

		// Token: 0x06000068 RID: 104 RVA: 0x00003850 File Offset: 0x00001A50
		internal void InternalSetMethod(UnityWebRequest.UnityWebRequestMethod methodType)
		{
			bool flag = !this.isModifiable;
			if (flag)
			{
				throw new InvalidOperationException("UnityWebRequest has already been sent and its request method can no longer be altered");
			}
			UnityWebRequest.UnityWebRequestError unityWebRequestError = this.SetMethod(methodType);
			bool flag2 = unityWebRequestError > UnityWebRequest.UnityWebRequestError.OK;
			if (flag2)
			{
				throw new InvalidOperationException(UnityWebRequest.GetWebErrorString(unityWebRequestError));
			}
		}

		// Token: 0x06000069 RID: 105
		[MethodImpl(4096)]
		private extern UnityWebRequest.UnityWebRequestError SetCustomMethod(string customMethodName);

		// Token: 0x0600006A RID: 106 RVA: 0x00003894 File Offset: 0x00001A94
		internal void InternalSetCustomMethod(string customMethodName)
		{
			bool flag = !this.isModifiable;
			if (flag)
			{
				throw new InvalidOperationException("UnityWebRequest has already been sent and its request method can no longer be altered");
			}
			UnityWebRequest.UnityWebRequestError unityWebRequestError = this.SetCustomMethod(customMethodName);
			bool flag2 = unityWebRequestError > UnityWebRequest.UnityWebRequestError.OK;
			if (flag2)
			{
				throw new InvalidOperationException(UnityWebRequest.GetWebErrorString(unityWebRequestError));
			}
		}

		// Token: 0x0600006B RID: 107
		[MethodImpl(4096)]
		internal extern UnityWebRequest.UnityWebRequestMethod GetMethod();

		// Token: 0x0600006C RID: 108
		[MethodImpl(4096)]
		internal extern string GetCustomMethod();

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600006D RID: 109 RVA: 0x000038D8 File Offset: 0x00001AD8
		// (set) Token: 0x0600006E RID: 110 RVA: 0x00003934 File Offset: 0x00001B34
		public string method
		{
			get
			{
				string text;
				switch (this.GetMethod())
				{
				case UnityWebRequest.UnityWebRequestMethod.Get:
					text = "GET";
					break;
				case UnityWebRequest.UnityWebRequestMethod.Post:
					text = "POST";
					break;
				case UnityWebRequest.UnityWebRequestMethod.Put:
					text = "PUT";
					break;
				case UnityWebRequest.UnityWebRequestMethod.Head:
					text = "HEAD";
					break;
				default:
					text = this.GetCustomMethod();
					break;
				}
				return text;
			}
			set
			{
				bool flag = string.IsNullOrEmpty(value);
				if (flag)
				{
					throw new ArgumentException("Cannot set a UnityWebRequest's method to an empty or null string");
				}
				string text = value.ToUpper();
				if (!(text == "GET"))
				{
					if (!(text == "POST"))
					{
						if (!(text == "PUT"))
						{
							if (!(text == "HEAD"))
							{
								this.InternalSetCustomMethod(value.ToUpper());
							}
							else
							{
								this.InternalSetMethod(UnityWebRequest.UnityWebRequestMethod.Head);
							}
						}
						else
						{
							this.InternalSetMethod(UnityWebRequest.UnityWebRequestMethod.Put);
						}
					}
					else
					{
						this.InternalSetMethod(UnityWebRequest.UnityWebRequestMethod.Post);
					}
				}
				else
				{
					this.InternalSetMethod(UnityWebRequest.UnityWebRequestMethod.Get);
				}
			}
		}

		// Token: 0x0600006F RID: 111
		[MethodImpl(4096)]
		private extern UnityWebRequest.UnityWebRequestError GetError();

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000070 RID: 112 RVA: 0x000039CC File Offset: 0x00001BCC
		public string error
		{
			get
			{
				UnityWebRequest.Result result = this.result;
				string text;
				if (result > UnityWebRequest.Result.Success)
				{
					if (result != UnityWebRequest.Result.ProtocolError)
					{
						text = UnityWebRequest.GetWebErrorString(this.GetError());
					}
					else
					{
						text = string.Format("HTTP/1.1 {0} {1}", this.responseCode, UnityWebRequest.GetHTTPStatusString(this.responseCode));
					}
				}
				else
				{
					text = null;
				}
				return text;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000071 RID: 113
		// (set) Token: 0x06000072 RID: 114
		private extern bool use100Continue
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000073 RID: 115 RVA: 0x00003A24 File Offset: 0x00001C24
		// (set) Token: 0x06000074 RID: 116 RVA: 0x00003A3C File Offset: 0x00001C3C
		public bool useHttpContinue
		{
			get
			{
				return this.use100Continue;
			}
			set
			{
				bool flag = !this.isModifiable;
				if (flag)
				{
					throw new InvalidOperationException("UnityWebRequest has already been sent and its 100-Continue setting cannot be altered");
				}
				this.use100Continue = value;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000075 RID: 117 RVA: 0x00003A6C File Offset: 0x00001C6C
		// (set) Token: 0x06000076 RID: 118 RVA: 0x00003A84 File Offset: 0x00001C84
		public string url
		{
			get
			{
				return this.GetUrl();
			}
			set
			{
				string text = "http://localhost/";
				this.InternalSetUrl(WebRequestUtils.MakeInitialUrl(value, text));
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00003AA8 File Offset: 0x00001CA8
		// (set) Token: 0x06000078 RID: 120 RVA: 0x00003AC8 File Offset: 0x00001CC8
		public Uri uri
		{
			get
			{
				return new Uri(this.GetUrl());
			}
			set
			{
				bool flag = !value.IsAbsoluteUri;
				if (flag)
				{
					throw new ArgumentException("URI must be absolute");
				}
				this.InternalSetUrl(WebRequestUtils.MakeUriString(value, value.OriginalString, false));
				this.m_Uri = value;
			}
		}

		// Token: 0x06000079 RID: 121
		[MethodImpl(4096)]
		private extern string GetUrl();

		// Token: 0x0600007A RID: 122
		[MethodImpl(4096)]
		private extern UnityWebRequest.UnityWebRequestError SetUrl(string url);

		// Token: 0x0600007B RID: 123 RVA: 0x00003B0C File Offset: 0x00001D0C
		private void InternalSetUrl(string url)
		{
			bool flag = !this.isModifiable;
			if (flag)
			{
				throw new InvalidOperationException("UnityWebRequest has already been sent and its URL cannot be altered");
			}
			UnityWebRequest.UnityWebRequestError unityWebRequestError = this.SetUrl(url);
			bool flag2 = unityWebRequestError > UnityWebRequest.UnityWebRequestError.OK;
			if (flag2)
			{
				throw new InvalidOperationException(UnityWebRequest.GetWebErrorString(unityWebRequestError));
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600007C RID: 124
		public extern long responseCode
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x0600007D RID: 125
		[MethodImpl(4096)]
		private extern float GetUploadProgress();

		// Token: 0x0600007E RID: 126
		[MethodImpl(4096)]
		private extern bool IsExecuting();

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00003B50 File Offset: 0x00001D50
		public float uploadProgress
		{
			get
			{
				bool flag = !this.IsExecuting() && !this.isDone;
				float num;
				if (flag)
				{
					num = -1f;
				}
				else
				{
					num = this.GetUploadProgress();
				}
				return num;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000080 RID: 128
		public extern bool isModifiable
		{
			[NativeMethod("IsModifiable")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000081 RID: 129 RVA: 0x00003B88 File Offset: 0x00001D88
		public bool isDone
		{
			get
			{
				return this.result > UnityWebRequest.Result.InProgress;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00003BA4 File Offset: 0x00001DA4
		public bool isNetworkError
		{
			get
			{
				return this.result == UnityWebRequest.Result.ConnectionError;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00003BC0 File Offset: 0x00001DC0
		public bool isHttpError
		{
			get
			{
				return this.result == UnityWebRequest.Result.ProtocolError;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000084 RID: 132
		public extern UnityWebRequest.Result result
		{
			[NativeMethod("GetResult")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06000085 RID: 133
		[MethodImpl(4096)]
		private extern float GetDownloadProgress();

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000086 RID: 134 RVA: 0x00003BDC File Offset: 0x00001DDC
		public float downloadProgress
		{
			get
			{
				bool flag = !this.IsExecuting() && !this.isDone;
				float num;
				if (flag)
				{
					num = -1f;
				}
				else
				{
					num = this.GetDownloadProgress();
				}
				return num;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000087 RID: 135
		public extern ulong uploadedBytes
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000088 RID: 136
		public extern ulong downloadedBytes
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06000089 RID: 137
		[MethodImpl(4096)]
		private extern int GetRedirectLimit();

		// Token: 0x0600008A RID: 138
		[NativeThrows]
		[MethodImpl(4096)]
		private extern void SetRedirectLimitFromScripting(int limit);

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600008B RID: 139 RVA: 0x00003C14 File Offset: 0x00001E14
		// (set) Token: 0x0600008C RID: 140 RVA: 0x00003C2C File Offset: 0x00001E2C
		public int redirectLimit
		{
			get
			{
				return this.GetRedirectLimit();
			}
			set
			{
				this.SetRedirectLimitFromScripting(value);
			}
		}

		// Token: 0x0600008D RID: 141
		[MethodImpl(4096)]
		private extern bool GetChunked();

		// Token: 0x0600008E RID: 142
		[MethodImpl(4096)]
		private extern UnityWebRequest.UnityWebRequestError SetChunked(bool chunked);

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00003C38 File Offset: 0x00001E38
		// (set) Token: 0x06000090 RID: 144 RVA: 0x00003C50 File Offset: 0x00001E50
		[Obsolete("HTTP/2 and many HTTP/1.1 servers don't support this; we recommend leaving it set to false (default).", false)]
		public bool chunkedTransfer
		{
			get
			{
				return this.GetChunked();
			}
			set
			{
				bool flag = !this.isModifiable;
				if (flag)
				{
					throw new InvalidOperationException("UnityWebRequest has already been sent and its chunked transfer encoding setting cannot be altered");
				}
				UnityWebRequest.UnityWebRequestError unityWebRequestError = this.SetChunked(value);
				bool flag2 = unityWebRequestError > UnityWebRequest.UnityWebRequestError.OK;
				if (flag2)
				{
					throw new InvalidOperationException(UnityWebRequest.GetWebErrorString(unityWebRequestError));
				}
			}
		}

		// Token: 0x06000091 RID: 145
		[MethodImpl(4096)]
		public extern string GetRequestHeader(string name);

		// Token: 0x06000092 RID: 146
		[NativeMethod("SetRequestHeader")]
		[MethodImpl(4096)]
		internal extern UnityWebRequest.UnityWebRequestError InternalSetRequestHeader(string name, string value);

		// Token: 0x06000093 RID: 147 RVA: 0x00003C94 File Offset: 0x00001E94
		public void SetRequestHeader(string name, string value)
		{
			bool flag = string.IsNullOrEmpty(name);
			if (flag)
			{
				throw new ArgumentException("Cannot set a Request Header with a null or empty name");
			}
			bool flag2 = value == null;
			if (flag2)
			{
				throw new ArgumentException("Cannot set a Request header with a null");
			}
			bool flag3 = !this.isModifiable;
			if (flag3)
			{
				throw new InvalidOperationException("UnityWebRequest has already been sent and its request headers cannot be altered");
			}
			UnityWebRequest.UnityWebRequestError unityWebRequestError = this.InternalSetRequestHeader(name, value);
			bool flag4 = unityWebRequestError > UnityWebRequest.UnityWebRequestError.OK;
			if (flag4)
			{
				throw new InvalidOperationException(UnityWebRequest.GetWebErrorString(unityWebRequestError));
			}
		}

		// Token: 0x06000094 RID: 148
		[MethodImpl(4096)]
		public extern string GetResponseHeader(string name);

		// Token: 0x06000095 RID: 149
		[MethodImpl(4096)]
		internal extern string[] GetResponseHeaderKeys();

		// Token: 0x06000096 RID: 150 RVA: 0x00003D04 File Offset: 0x00001F04
		public Dictionary<string, string> GetResponseHeaders()
		{
			string[] responseHeaderKeys = this.GetResponseHeaderKeys();
			bool flag = responseHeaderKeys == null || responseHeaderKeys.Length == 0;
			Dictionary<string, string> dictionary;
			if (flag)
			{
				dictionary = null;
			}
			else
			{
				Dictionary<string, string> dictionary2 = new Dictionary<string, string>(responseHeaderKeys.Length, StringComparer.OrdinalIgnoreCase);
				for (int i = 0; i < responseHeaderKeys.Length; i++)
				{
					string responseHeader = this.GetResponseHeader(responseHeaderKeys[i]);
					dictionary2.Add(responseHeaderKeys[i], responseHeader);
				}
				dictionary = dictionary2;
			}
			return dictionary;
		}

		// Token: 0x06000097 RID: 151
		[MethodImpl(4096)]
		private extern UnityWebRequest.UnityWebRequestError SetUploadHandler(UploadHandler uh);

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000098 RID: 152 RVA: 0x00003D74 File Offset: 0x00001F74
		// (set) Token: 0x06000099 RID: 153 RVA: 0x00003D8C File Offset: 0x00001F8C
		public UploadHandler uploadHandler
		{
			get
			{
				return this.m_UploadHandler;
			}
			set
			{
				bool flag = !this.isModifiable;
				if (flag)
				{
					throw new InvalidOperationException("UnityWebRequest has already been sent; cannot modify the upload handler");
				}
				UnityWebRequest.UnityWebRequestError unityWebRequestError = this.SetUploadHandler(value);
				bool flag2 = unityWebRequestError > UnityWebRequest.UnityWebRequestError.OK;
				if (flag2)
				{
					throw new InvalidOperationException(UnityWebRequest.GetWebErrorString(unityWebRequestError));
				}
				this.m_UploadHandler = value;
			}
		}

		// Token: 0x0600009A RID: 154
		[MethodImpl(4096)]
		private extern UnityWebRequest.UnityWebRequestError SetDownloadHandler(DownloadHandler dh);

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600009B RID: 155 RVA: 0x00003DD8 File Offset: 0x00001FD8
		// (set) Token: 0x0600009C RID: 156 RVA: 0x00003DF0 File Offset: 0x00001FF0
		public DownloadHandler downloadHandler
		{
			get
			{
				return this.m_DownloadHandler;
			}
			set
			{
				bool flag = !this.isModifiable;
				if (flag)
				{
					throw new InvalidOperationException("UnityWebRequest has already been sent; cannot modify the download handler");
				}
				UnityWebRequest.UnityWebRequestError unityWebRequestError = this.SetDownloadHandler(value);
				bool flag2 = unityWebRequestError > UnityWebRequest.UnityWebRequestError.OK;
				if (flag2)
				{
					throw new InvalidOperationException(UnityWebRequest.GetWebErrorString(unityWebRequestError));
				}
				this.m_DownloadHandler = value;
			}
		}

		// Token: 0x0600009D RID: 157
		[MethodImpl(4096)]
		private extern UnityWebRequest.UnityWebRequestError SetCertificateHandler(CertificateHandler ch);

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600009E RID: 158 RVA: 0x00003E3C File Offset: 0x0000203C
		// (set) Token: 0x0600009F RID: 159 RVA: 0x00003E54 File Offset: 0x00002054
		public CertificateHandler certificateHandler
		{
			get
			{
				return this.m_CertificateHandler;
			}
			set
			{
				bool flag = !this.isModifiable;
				if (flag)
				{
					throw new InvalidOperationException("UnityWebRequest has already been sent; cannot modify the certificate handler");
				}
				UnityWebRequest.UnityWebRequestError unityWebRequestError = this.SetCertificateHandler(value);
				bool flag2 = unityWebRequestError > UnityWebRequest.UnityWebRequestError.OK;
				if (flag2)
				{
					throw new InvalidOperationException(UnityWebRequest.GetWebErrorString(unityWebRequestError));
				}
				this.m_CertificateHandler = value;
			}
		}

		// Token: 0x060000A0 RID: 160
		[MethodImpl(4096)]
		private extern int GetTimeoutMsec();

		// Token: 0x060000A1 RID: 161
		[MethodImpl(4096)]
		private extern UnityWebRequest.UnityWebRequestError SetTimeoutMsec(int timeout);

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x00003EA0 File Offset: 0x000020A0
		// (set) Token: 0x060000A3 RID: 163 RVA: 0x00003EC0 File Offset: 0x000020C0
		public int timeout
		{
			get
			{
				return this.GetTimeoutMsec() / 1000;
			}
			set
			{
				bool flag = !this.isModifiable;
				if (flag)
				{
					throw new InvalidOperationException("UnityWebRequest has already been sent; cannot modify the timeout");
				}
				value = Math.Max(value, 0);
				UnityWebRequest.UnityWebRequestError unityWebRequestError = this.SetTimeoutMsec(value * 1000);
				bool flag2 = unityWebRequestError > UnityWebRequest.UnityWebRequestError.OK;
				if (flag2)
				{
					throw new InvalidOperationException(UnityWebRequest.GetWebErrorString(unityWebRequestError));
				}
			}
		}

		// Token: 0x060000A4 RID: 164
		[MethodImpl(4096)]
		private extern bool GetSuppressErrorsToConsole();

		// Token: 0x060000A5 RID: 165
		[MethodImpl(4096)]
		private extern UnityWebRequest.UnityWebRequestError SetSuppressErrorsToConsole(bool suppress);

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x00003F14 File Offset: 0x00002114
		// (set) Token: 0x060000A7 RID: 167 RVA: 0x00003F2C File Offset: 0x0000212C
		internal bool suppressErrorsToConsole
		{
			get
			{
				return this.GetSuppressErrorsToConsole();
			}
			set
			{
				bool flag = !this.isModifiable;
				if (flag)
				{
					throw new InvalidOperationException("UnityWebRequest has already been sent; cannot modify the timeout");
				}
				UnityWebRequest.UnityWebRequestError unityWebRequestError = this.SetSuppressErrorsToConsole(value);
				bool flag2 = unityWebRequestError > UnityWebRequest.UnityWebRequestError.OK;
				if (flag2)
				{
					throw new InvalidOperationException(UnityWebRequest.GetWebErrorString(unityWebRequestError));
				}
			}
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003F70 File Offset: 0x00002170
		public static UnityWebRequest Get(string uri)
		{
			return new UnityWebRequest(uri, "GET", new DownloadHandlerBuffer(), null);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00003F98 File Offset: 0x00002198
		public static UnityWebRequest Get(Uri uri)
		{
			return new UnityWebRequest(uri, "GET", new DownloadHandlerBuffer(), null);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00003FC0 File Offset: 0x000021C0
		public static UnityWebRequest Delete(string uri)
		{
			return new UnityWebRequest(uri, "DELETE");
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00003FE0 File Offset: 0x000021E0
		public static UnityWebRequest Delete(Uri uri)
		{
			return new UnityWebRequest(uri, "DELETE");
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00004000 File Offset: 0x00002200
		public static UnityWebRequest Head(string uri)
		{
			return new UnityWebRequest(uri, "HEAD");
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00004020 File Offset: 0x00002220
		public static UnityWebRequest Head(Uri uri)
		{
			return new UnityWebRequest(uri, "HEAD");
		}

		// Token: 0x060000AE RID: 174 RVA: 0x0000403F File Offset: 0x0000223F
		[Obsolete("UnityWebRequest.GetTexture is obsolete. Use UnityWebRequestTexture.GetTexture instead (UnityUpgradable) -> [UnityEngine] UnityWebRequestTexture.GetTexture(*)", true)]
		[EditorBrowsable(1)]
		public static UnityWebRequest GetTexture(string uri)
		{
			throw new NotSupportedException("UnityWebRequest.GetTexture is obsolete. Use UnityWebRequestTexture.GetTexture instead.");
		}

		// Token: 0x060000AF RID: 175 RVA: 0x0000403F File Offset: 0x0000223F
		[EditorBrowsable(1)]
		[Obsolete("UnityWebRequest.GetTexture is obsolete. Use UnityWebRequestTexture.GetTexture instead (UnityUpgradable) -> [UnityEngine] UnityWebRequestTexture.GetTexture(*)", true)]
		public static UnityWebRequest GetTexture(string uri, bool nonReadable)
		{
			throw new NotSupportedException("UnityWebRequest.GetTexture is obsolete. Use UnityWebRequestTexture.GetTexture instead.");
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x0000404C File Offset: 0x0000224C
		[EditorBrowsable(1)]
		[Obsolete("UnityWebRequest.GetAudioClip is obsolete. Use UnityWebRequestMultimedia.GetAudioClip instead (UnityUpgradable) -> [UnityEngine] UnityWebRequestMultimedia.GetAudioClip(*)", true)]
		public static UnityWebRequest GetAudioClip(string uri, AudioType audioType)
		{
			return null;
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00004060 File Offset: 0x00002260
		[EditorBrowsable(1)]
		[Obsolete("UnityWebRequest.GetAssetBundle is obsolete. Use UnityWebRequestAssetBundle.GetAssetBundle instead (UnityUpgradable) -> [UnityEngine] UnityWebRequestAssetBundle.GetAssetBundle(*)", true)]
		public static UnityWebRequest GetAssetBundle(string uri)
		{
			return null;
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00004074 File Offset: 0x00002274
		[EditorBrowsable(1)]
		[Obsolete("UnityWebRequest.GetAssetBundle is obsolete. Use UnityWebRequestAssetBundle.GetAssetBundle instead (UnityUpgradable) -> [UnityEngine] UnityWebRequestAssetBundle.GetAssetBundle(*)", true)]
		public static UnityWebRequest GetAssetBundle(string uri, uint crc)
		{
			return null;
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00004088 File Offset: 0x00002288
		[EditorBrowsable(1)]
		[Obsolete("UnityWebRequest.GetAssetBundle is obsolete. Use UnityWebRequestAssetBundle.GetAssetBundle instead (UnityUpgradable) -> [UnityEngine] UnityWebRequestAssetBundle.GetAssetBundle(*)", true)]
		public static UnityWebRequest GetAssetBundle(string uri, uint version, uint crc)
		{
			return null;
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x0000409C File Offset: 0x0000229C
		[EditorBrowsable(1)]
		[Obsolete("UnityWebRequest.GetAssetBundle is obsolete. Use UnityWebRequestAssetBundle.GetAssetBundle instead (UnityUpgradable) -> [UnityEngine] UnityWebRequestAssetBundle.GetAssetBundle(*)", true)]
		public static UnityWebRequest GetAssetBundle(string uri, Hash128 hash, uint crc)
		{
			return null;
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x000040B0 File Offset: 0x000022B0
		[EditorBrowsable(1)]
		[Obsolete("UnityWebRequest.GetAssetBundle is obsolete. Use UnityWebRequestAssetBundle.GetAssetBundle instead (UnityUpgradable) -> [UnityEngine] UnityWebRequestAssetBundle.GetAssetBundle(*)", true)]
		public static UnityWebRequest GetAssetBundle(string uri, CachedAssetBundle cachedAssetBundle, uint crc)
		{
			return null;
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x000040C4 File Offset: 0x000022C4
		public static UnityWebRequest Put(string uri, byte[] bodyData)
		{
			return new UnityWebRequest(uri, "PUT", new DownloadHandlerBuffer(), new UploadHandlerRaw(bodyData));
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x000040F0 File Offset: 0x000022F0
		public static UnityWebRequest Put(Uri uri, byte[] bodyData)
		{
			return new UnityWebRequest(uri, "PUT", new DownloadHandlerBuffer(), new UploadHandlerRaw(bodyData));
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x0000411C File Offset: 0x0000231C
		public static UnityWebRequest Put(string uri, string bodyData)
		{
			return new UnityWebRequest(uri, "PUT", new DownloadHandlerBuffer(), new UploadHandlerRaw(Encoding.UTF8.GetBytes(bodyData)));
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00004150 File Offset: 0x00002350
		public static UnityWebRequest Put(Uri uri, string bodyData)
		{
			return new UnityWebRequest(uri, "PUT", new DownloadHandlerBuffer(), new UploadHandlerRaw(Encoding.UTF8.GetBytes(bodyData)));
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00004184 File Offset: 0x00002384
		public static UnityWebRequest Post(string uri, string postData)
		{
			UnityWebRequest unityWebRequest = new UnityWebRequest(uri, "POST");
			UnityWebRequest.SetupPost(unityWebRequest, postData);
			return unityWebRequest;
		}

		// Token: 0x060000BB RID: 187 RVA: 0x000041AC File Offset: 0x000023AC
		public static UnityWebRequest Post(Uri uri, string postData)
		{
			UnityWebRequest unityWebRequest = new UnityWebRequest(uri, "POST");
			UnityWebRequest.SetupPost(unityWebRequest, postData);
			return unityWebRequest;
		}

		// Token: 0x060000BC RID: 188 RVA: 0x000041D4 File Offset: 0x000023D4
		private static void SetupPost(UnityWebRequest request, string postData)
		{
			byte[] array = null;
			bool flag = !string.IsNullOrEmpty(postData);
			if (flag)
			{
				string text = WWWTranscoder.DataEncode(postData, Encoding.UTF8);
				array = Encoding.UTF8.GetBytes(text);
			}
			request.uploadHandler = new UploadHandlerRaw(array);
			request.uploadHandler.contentType = "application/x-www-form-urlencoded";
			request.downloadHandler = new DownloadHandlerBuffer();
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00004238 File Offset: 0x00002438
		public static UnityWebRequest Post(string uri, WWWForm formData)
		{
			UnityWebRequest unityWebRequest = new UnityWebRequest(uri, "POST");
			UnityWebRequest.SetupPost(unityWebRequest, formData);
			return unityWebRequest;
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00004260 File Offset: 0x00002460
		public static UnityWebRequest Post(Uri uri, WWWForm formData)
		{
			UnityWebRequest unityWebRequest = new UnityWebRequest(uri, "POST");
			UnityWebRequest.SetupPost(unityWebRequest, formData);
			return unityWebRequest;
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00004288 File Offset: 0x00002488
		private static void SetupPost(UnityWebRequest request, WWWForm formData)
		{
			byte[] array = null;
			bool flag = formData != null;
			if (flag)
			{
				array = formData.data;
				bool flag2 = array.Length == 0;
				if (flag2)
				{
					array = null;
				}
			}
			request.uploadHandler = new UploadHandlerRaw(array);
			request.downloadHandler = new DownloadHandlerBuffer();
			bool flag3 = formData != null;
			if (flag3)
			{
				Dictionary<string, string> headers = formData.headers;
				foreach (KeyValuePair<string, string> keyValuePair in headers)
				{
					request.SetRequestHeader(keyValuePair.Key, keyValuePair.Value);
				}
			}
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00004334 File Offset: 0x00002534
		public static UnityWebRequest Post(string uri, List<IMultipartFormSection> multipartFormSections)
		{
			byte[] array = UnityWebRequest.GenerateBoundary();
			return UnityWebRequest.Post(uri, multipartFormSections, array);
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00004354 File Offset: 0x00002554
		public static UnityWebRequest Post(Uri uri, List<IMultipartFormSection> multipartFormSections)
		{
			byte[] array = UnityWebRequest.GenerateBoundary();
			return UnityWebRequest.Post(uri, multipartFormSections, array);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00004374 File Offset: 0x00002574
		public static UnityWebRequest Post(string uri, List<IMultipartFormSection> multipartFormSections, byte[] boundary)
		{
			UnityWebRequest unityWebRequest = new UnityWebRequest(uri, "POST");
			UnityWebRequest.SetupPost(unityWebRequest, multipartFormSections, boundary);
			return unityWebRequest;
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x0000439C File Offset: 0x0000259C
		public static UnityWebRequest Post(Uri uri, List<IMultipartFormSection> multipartFormSections, byte[] boundary)
		{
			UnityWebRequest unityWebRequest = new UnityWebRequest(uri, "POST");
			UnityWebRequest.SetupPost(unityWebRequest, multipartFormSections, boundary);
			return unityWebRequest;
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x000043C4 File Offset: 0x000025C4
		private static void SetupPost(UnityWebRequest request, List<IMultipartFormSection> multipartFormSections, byte[] boundary)
		{
			byte[] array = null;
			bool flag = multipartFormSections != null && multipartFormSections.Count != 0;
			if (flag)
			{
				array = UnityWebRequest.SerializeFormSections(multipartFormSections, boundary);
			}
			request.uploadHandler = new UploadHandlerRaw(array)
			{
				contentType = "multipart/form-data; boundary=" + Encoding.UTF8.GetString(boundary, 0, boundary.Length)
			};
			request.downloadHandler = new DownloadHandlerBuffer();
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x0000442C File Offset: 0x0000262C
		public static UnityWebRequest Post(string uri, Dictionary<string, string> formFields)
		{
			UnityWebRequest unityWebRequest = new UnityWebRequest(uri, "POST");
			UnityWebRequest.SetupPost(unityWebRequest, formFields);
			return unityWebRequest;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00004454 File Offset: 0x00002654
		public static UnityWebRequest Post(Uri uri, Dictionary<string, string> formFields)
		{
			UnityWebRequest unityWebRequest = new UnityWebRequest(uri, "POST");
			UnityWebRequest.SetupPost(unityWebRequest, formFields);
			return unityWebRequest;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x0000447C File Offset: 0x0000267C
		private static void SetupPost(UnityWebRequest request, Dictionary<string, string> formFields)
		{
			byte[] array = null;
			bool flag = formFields != null && formFields.Count != 0;
			if (flag)
			{
				array = UnityWebRequest.SerializeSimpleForm(formFields);
			}
			request.uploadHandler = new UploadHandlerRaw(array)
			{
				contentType = "application/x-www-form-urlencoded"
			};
			request.downloadHandler = new DownloadHandlerBuffer();
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x000044D0 File Offset: 0x000026D0
		public static string EscapeURL(string s)
		{
			return UnityWebRequest.EscapeURL(s, Encoding.UTF8);
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x000044F0 File Offset: 0x000026F0
		public static string EscapeURL(string s, Encoding e)
		{
			bool flag = s == null;
			string text;
			if (flag)
			{
				text = null;
			}
			else
			{
				bool flag2 = s == "";
				if (flag2)
				{
					text = "";
				}
				else
				{
					bool flag3 = e == null;
					if (flag3)
					{
						text = null;
					}
					else
					{
						byte[] bytes = e.GetBytes(s);
						byte[] array = WWWTranscoder.URLEncode(bytes);
						text = e.GetString(array);
					}
				}
			}
			return text;
		}

		// Token: 0x060000CA RID: 202 RVA: 0x0000454C File Offset: 0x0000274C
		public static string UnEscapeURL(string s)
		{
			return UnityWebRequest.UnEscapeURL(s, Encoding.UTF8);
		}

		// Token: 0x060000CB RID: 203 RVA: 0x0000456C File Offset: 0x0000276C
		public static string UnEscapeURL(string s, Encoding e)
		{
			bool flag = s == null;
			string text;
			if (flag)
			{
				text = null;
			}
			else
			{
				bool flag2 = s.IndexOf('%') == -1 && s.IndexOf('+') == -1;
				if (flag2)
				{
					text = s;
				}
				else
				{
					byte[] bytes = e.GetBytes(s);
					byte[] array = WWWTranscoder.URLDecode(bytes);
					text = e.GetString(array);
				}
			}
			return text;
		}

		// Token: 0x060000CC RID: 204 RVA: 0x000045C4 File Offset: 0x000027C4
		public static byte[] SerializeFormSections(List<IMultipartFormSection> multipartFormSections, byte[] boundary)
		{
			bool flag = multipartFormSections == null || multipartFormSections.Count == 0;
			byte[] array;
			if (flag)
			{
				array = null;
			}
			else
			{
				byte[] bytes = Encoding.UTF8.GetBytes("\r\n");
				byte[] bytes2 = WWWForm.DefaultEncoding.GetBytes("--");
				int num = 0;
				foreach (IMultipartFormSection multipartFormSection in multipartFormSections)
				{
					num += 64 + multipartFormSection.sectionData.Length;
				}
				List<byte> list = new List<byte>(num);
				foreach (IMultipartFormSection multipartFormSection2 in multipartFormSections)
				{
					string text = "form-data";
					string sectionName = multipartFormSection2.sectionName;
					string fileName = multipartFormSection2.fileName;
					string text2 = "Content-Disposition: " + text;
					bool flag2 = !string.IsNullOrEmpty(sectionName);
					if (flag2)
					{
						text2 = text2 + "; name=\"" + sectionName + "\"";
					}
					bool flag3 = !string.IsNullOrEmpty(fileName);
					if (flag3)
					{
						text2 = text2 + "; filename=\"" + fileName + "\"";
					}
					text2 += "\r\n";
					string contentType = multipartFormSection2.contentType;
					bool flag4 = !string.IsNullOrEmpty(contentType);
					if (flag4)
					{
						text2 = text2 + "Content-Type: " + contentType + "\r\n";
					}
					list.AddRange(bytes);
					list.AddRange(bytes2);
					list.AddRange(boundary);
					list.AddRange(bytes);
					list.AddRange(Encoding.UTF8.GetBytes(text2));
					list.AddRange(bytes);
					list.AddRange(multipartFormSection2.sectionData);
				}
				list.AddRange(bytes);
				list.AddRange(bytes2);
				list.AddRange(boundary);
				list.AddRange(bytes2);
				list.AddRange(bytes);
				array = list.ToArray();
			}
			return array;
		}

		// Token: 0x060000CD RID: 205 RVA: 0x000047F4 File Offset: 0x000029F4
		public static byte[] GenerateBoundary()
		{
			byte[] array = new byte[40];
			for (int i = 0; i < 40; i++)
			{
				int num = Random.Range(48, 110);
				bool flag = num > 57;
				if (flag)
				{
					num += 7;
				}
				bool flag2 = num > 90;
				if (flag2)
				{
					num += 6;
				}
				array[i] = (byte)num;
			}
			return array;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00004854 File Offset: 0x00002A54
		public static byte[] SerializeSimpleForm(Dictionary<string, string> formFields)
		{
			string text = "";
			foreach (KeyValuePair<string, string> keyValuePair in formFields)
			{
				bool flag = text.Length > 0;
				if (flag)
				{
					text += "&";
				}
				text = text + WWWTranscoder.DataEncode(keyValuePair.Key) + "=" + WWWTranscoder.DataEncode(keyValuePair.Value);
			}
			return Encoding.UTF8.GetBytes(text);
		}

		// Token: 0x04000019 RID: 25
		[NonSerialized]
		internal IntPtr m_Ptr;

		// Token: 0x0400001A RID: 26
		[NonSerialized]
		internal DownloadHandler m_DownloadHandler;

		// Token: 0x0400001B RID: 27
		[NonSerialized]
		internal UploadHandler m_UploadHandler;

		// Token: 0x0400001C RID: 28
		[NonSerialized]
		internal CertificateHandler m_CertificateHandler;

		// Token: 0x0400001D RID: 29
		[NonSerialized]
		internal Uri m_Uri;

		// Token: 0x0400001E RID: 30
		public const string kHttpVerbGET = "GET";

		// Token: 0x0400001F RID: 31
		public const string kHttpVerbHEAD = "HEAD";

		// Token: 0x04000020 RID: 32
		public const string kHttpVerbPOST = "POST";

		// Token: 0x04000021 RID: 33
		public const string kHttpVerbPUT = "PUT";

		// Token: 0x04000022 RID: 34
		public const string kHttpVerbCREATE = "CREATE";

		// Token: 0x04000023 RID: 35
		public const string kHttpVerbDELETE = "DELETE";

		// Token: 0x0200000A RID: 10
		internal enum UnityWebRequestMethod
		{
			// Token: 0x04000028 RID: 40
			Get,
			// Token: 0x04000029 RID: 41
			Post,
			// Token: 0x0400002A RID: 42
			Put,
			// Token: 0x0400002B RID: 43
			Head,
			// Token: 0x0400002C RID: 44
			Custom
		}

		// Token: 0x0200000B RID: 11
		internal enum UnityWebRequestError
		{
			// Token: 0x0400002E RID: 46
			OK,
			// Token: 0x0400002F RID: 47
			Unknown,
			// Token: 0x04000030 RID: 48
			SDKError,
			// Token: 0x04000031 RID: 49
			UnsupportedProtocol,
			// Token: 0x04000032 RID: 50
			MalformattedUrl,
			// Token: 0x04000033 RID: 51
			CannotResolveProxy,
			// Token: 0x04000034 RID: 52
			CannotResolveHost,
			// Token: 0x04000035 RID: 53
			CannotConnectToHost,
			// Token: 0x04000036 RID: 54
			AccessDenied,
			// Token: 0x04000037 RID: 55
			GenericHttpError,
			// Token: 0x04000038 RID: 56
			WriteError,
			// Token: 0x04000039 RID: 57
			ReadError,
			// Token: 0x0400003A RID: 58
			OutOfMemory,
			// Token: 0x0400003B RID: 59
			Timeout,
			// Token: 0x0400003C RID: 60
			HTTPPostError,
			// Token: 0x0400003D RID: 61
			SSLCannotConnect,
			// Token: 0x0400003E RID: 62
			Aborted,
			// Token: 0x0400003F RID: 63
			TooManyRedirects,
			// Token: 0x04000040 RID: 64
			ReceivedNoData,
			// Token: 0x04000041 RID: 65
			SSLNotSupported,
			// Token: 0x04000042 RID: 66
			FailedToSendData,
			// Token: 0x04000043 RID: 67
			FailedToReceiveData,
			// Token: 0x04000044 RID: 68
			SSLCertificateError,
			// Token: 0x04000045 RID: 69
			SSLCipherNotAvailable,
			// Token: 0x04000046 RID: 70
			SSLCACertError,
			// Token: 0x04000047 RID: 71
			UnrecognizedContentEncoding,
			// Token: 0x04000048 RID: 72
			LoginFailed,
			// Token: 0x04000049 RID: 73
			SSLShutdownFailed,
			// Token: 0x0400004A RID: 74
			NoInternetConnection
		}

		// Token: 0x0200000C RID: 12
		public enum Result
		{
			// Token: 0x0400004C RID: 76
			InProgress,
			// Token: 0x0400004D RID: 77
			Success,
			// Token: 0x0400004E RID: 78
			ConnectionError,
			// Token: 0x0400004F RID: 79
			ProtocolError,
			// Token: 0x04000050 RID: 80
			DataProcessingError
		}
	}
}
