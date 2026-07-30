using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Web.Services.Configuration;

namespace System.Web.Services.Protocols
{
	// Token: 0x0200003D RID: 61
	internal abstract class HttpServerProtocol : ServerProtocol
	{
		// Token: 0x0600012E RID: 302 RVA: 0x00005A78 File Offset: 0x00003C78
		protected HttpServerProtocol(bool hasInputPayload)
		{
			this.hasInputPayload = hasInputPayload;
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00005A88 File Offset: 0x00003C88
		internal override bool Initialize()
		{
			string text = base.Request.PathInfo.Substring(1);
			if ((this.serverType = (HttpServerType)base.GetFromCache(typeof(HttpServerProtocol), base.Type)) == null && (this.serverType = (HttpServerType)base.GetFromCache(typeof(HttpServerProtocol), base.Type, true)) == null)
			{
				object internalSyncObject = ServerProtocol.InternalSyncObject;
				lock (internalSyncObject)
				{
					if ((this.serverType = (HttpServerType)base.GetFromCache(typeof(HttpServerProtocol), base.Type)) == null && (this.serverType = (HttpServerType)base.GetFromCache(typeof(HttpServerProtocol), base.Type, true)) == null)
					{
						bool flag2 = base.IsCacheUnderPressure(typeof(HttpServerProtocol), base.Type);
						this.serverType = new HttpServerType(base.Type);
						base.AddToCache(typeof(HttpServerProtocol), base.Type, this.serverType, flag2);
					}
				}
			}
			this.serverMethod = this.serverType.GetMethod(text);
			if (this.serverMethod == null)
			{
				this.serverMethod = this.serverType.GetMethodIgnoreCase(text);
				if (this.serverMethod != null)
				{
					throw new ArgumentException(Res.GetString("WebInvalidMethodNameCase", new object[]
					{
						text,
						this.serverMethod.name
					}), "methodName");
				}
				string @string = Encoding.UTF8.GetString(Encoding.Default.GetBytes(text));
				this.serverMethod = this.serverType.GetMethod(@string);
				if (this.serverMethod == null)
				{
					throw new InvalidOperationException(Res.GetString("WebInvalidMethodName", new object[] { text }));
				}
			}
			return true;
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000130 RID: 304 RVA: 0x00002B51 File Offset: 0x00000D51
		internal override bool IsOneWay
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000131 RID: 305 RVA: 0x00005C6C File Offset: 0x00003E6C
		internal override LogicalMethodInfo MethodInfo
		{
			get
			{
				return this.serverMethod.methodInfo;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000132 RID: 306 RVA: 0x00005C79 File Offset: 0x00003E79
		internal override ServerType ServerType
		{
			get
			{
				return this.serverType;
			}
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00005C84 File Offset: 0x00003E84
		internal override object[] ReadParameters()
		{
			if (this.serverMethod.readerTypes == null)
			{
				return new object[0];
			}
			int i = 0;
			while (i < this.serverMethod.readerTypes.Length)
			{
				if (!this.hasInputPayload)
				{
					if (!(this.serverMethod.readerTypes[i] != typeof(UrlParameterReader)))
					{
						goto IL_005E;
					}
				}
				else if (!(this.serverMethod.readerTypes[i] == typeof(UrlParameterReader)))
				{
					goto IL_005E;
				}
				IL_0093:
				i++;
				continue;
				IL_005E:
				object[] array = ((MimeParameterReader)MimeFormatter.CreateInstance(this.serverMethod.readerTypes[i], this.serverMethod.readerInitializers[i])).Read(base.Request);
				if (array != null)
				{
					return array;
				}
				goto IL_0093;
			}
			if (!this.hasInputPayload)
			{
				throw new InvalidOperationException(Res.GetString("WebInvalidRequestFormat"));
			}
			throw new InvalidOperationException(Res.GetString("WebInvalidRequestFormatDetails", new object[] { base.Request.ContentType }));
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00005D78 File Offset: 0x00003F78
		internal override void WriteReturns(object[] returnValues, Stream outputStream)
		{
			if (this.serverMethod.writerType == null)
			{
				return;
			}
			((MimeReturnWriter)MimeFormatter.CreateInstance(this.serverMethod.writerType, this.serverMethod.writerInitializer)).Write(base.Response, outputStream, returnValues[0]);
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00005DC8 File Offset: 0x00003FC8
		internal override bool WriteException(Exception e, Stream outputStream)
		{
			base.Response.Clear();
			base.Response.ClearHeaders();
			base.Response.ContentType = ContentType.Compose("text/plain", Encoding.UTF8);
			ServerProtocol.SetHttpResponseStatusCode(base.Response, 500);
			base.Response.StatusDescription = HttpWorkerRequest.GetStatusDescription(base.Response.StatusCode);
			StreamWriter streamWriter = new StreamWriter(outputStream, new UTF8Encoding(false));
			if (WebServicesSection.Current.Diagnostics.SuppressReturningExceptions)
			{
				streamWriter.WriteLine(Res.GetString("WebSuppressedExceptionMessage"));
			}
			else
			{
				streamWriter.WriteLine(base.GenerateFaultString(e, true));
			}
			streamWriter.Flush();
			return true;
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00005E78 File Offset: 0x00004078
		internal static bool AreUrlParametersSupported(LogicalMethodInfo methodInfo)
		{
			if (methodInfo.OutParameters.Length != 0)
			{
				return false;
			}
			ParameterInfo[] inParameters = methodInfo.InParameters;
			for (int i = 0; i < inParameters.Length; i++)
			{
				Type parameterType = inParameters[i].ParameterType;
				if (parameterType.IsArray)
				{
					if (!ScalarFormatter.IsTypeSupported(parameterType.GetElementType()))
					{
						return false;
					}
				}
				else if (!ScalarFormatter.IsTypeSupported(parameterType))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x040001FA RID: 506
		private HttpServerMethod serverMethod;

		// Token: 0x040001FB RID: 507
		private HttpServerType serverType;

		// Token: 0x040001FC RID: 508
		private bool hasInputPayload;
	}
}
