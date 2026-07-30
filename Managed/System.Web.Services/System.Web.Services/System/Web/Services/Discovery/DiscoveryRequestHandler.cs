using System;
using System.ComponentModel;
using System.IO;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Web.Services.Diagnostics;
using System.Web.Services.Protocols;
using System.Xml;

namespace System.Web.Services.Discovery
{
	/// <summary>An ASP.NET HTTP handler that processes a request for a Web services discovery document.</summary>
	// Token: 0x020000AD RID: 173
	public sealed class DiscoveryRequestHandler : IHttpHandler
	{
		/// <summary>Gets a value of true, indicates whether the instance of <see cref="T:System.Web.Services.Discovery.DiscoveryRequestHandler" /> (or a derived class) is reusable. </summary>
		/// <returns>This property always returns true.</returns>
		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000489 RID: 1161 RVA: 0x00002B54 File Offset: 0x00000D54
		public bool IsReusable
		{
			get
			{
				return true;
			}
		}

		/// <summary>Handles an HTTP request for a discovery document, which is serialized to the HTTP response.</summary>
		/// <param name="context">The <see cref="P:System.Web.HttpContext.Request" /> and <see cref="P:System.Web.HttpContext.Response" /> properties of the <see cref="T:System.Web.HttpContext" /> class are used for input and output, respectively.</param>
		// Token: 0x0600048A RID: 1162 RVA: 0x00015450 File Offset: 0x00013650
		public void ProcessRequest(HttpContext context)
		{
			TraceMethod traceMethod = (Tracing.On ? new TraceMethod(this, "ProcessRequest", Array.Empty<object>()) : null);
			if (Tracing.On)
			{
				Tracing.Enter("IHttpHandler.ProcessRequest", traceMethod, Tracing.Details(context.Request));
			}
			new PermissionSet(PermissionState.Unrestricted).Demand();
			string physicalPath = context.Request.PhysicalPath;
			bool traceVerbose = CompModSwitches.DynamicDiscoverySearcher.TraceVerbose;
			if (File.Exists(physicalPath))
			{
				DynamicDiscoveryDocument dynamicDiscoveryDocument = null;
				FileStream fileStream = null;
				try
				{
					fileStream = new FileStream(physicalPath, FileMode.Open, FileAccess.Read);
					if (new XmlTextReader(fileStream)
					{
						WhitespaceHandling = WhitespaceHandling.Significant,
						XmlResolver = null,
						DtdProcessing = DtdProcessing.Prohibit
					}.IsStartElement("dynamicDiscovery", "urn:schemas-dynamicdiscovery:disco.2000-03-17"))
					{
						fileStream.Position = 0L;
						dynamicDiscoveryDocument = DynamicDiscoveryDocument.Load(fileStream);
					}
				}
				finally
				{
					if (fileStream != null)
					{
						fileStream.Close();
					}
				}
				if (dynamicDiscoveryDocument != null)
				{
					string[] array = new string[dynamicDiscoveryDocument.ExcludePaths.Length];
					string directoryName = Path.GetDirectoryName(physicalPath);
					string text = Path.GetFileName(physicalPath);
					for (int i = 0; i < array.Length; i++)
					{
						array[i] = dynamicDiscoveryDocument.ExcludePaths[i].Path;
					}
					Uri url = context.Request.Url;
					string text2 = RuntimeUtils.EscapeUri(url);
					string dirPartOfPath = DiscoveryRequestHandler.GetDirPartOfPath(text2);
					DynamicDiscoSearcher dynamicDiscoSearcher;
					if (DiscoveryRequestHandler.GetDirPartOfPath(url.LocalPath).Length == 0 || CompModSwitches.DynamicDiscoveryVirtualSearch.Enabled)
					{
						text = DiscoveryRequestHandler.GetFilePartOfPath(text2);
						dynamicDiscoSearcher = new DynamicVirtualDiscoSearcher(directoryName, array, dirPartOfPath);
					}
					else
					{
						dynamicDiscoSearcher = new DynamicPhysicalDiscoSearcher(directoryName, array, dirPartOfPath);
					}
					bool traceVerbose2 = CompModSwitches.DynamicDiscoverySearcher.TraceVerbose;
					dynamicDiscoSearcher.Search(text);
					DiscoveryDocument discoveryDocument = dynamicDiscoSearcher.DiscoveryDocument;
					MemoryStream memoryStream = new MemoryStream(1024);
					StreamWriter streamWriter = new StreamWriter(memoryStream, new UTF8Encoding(false));
					discoveryDocument.Write(streamWriter);
					memoryStream.Position = 0L;
					byte[] array2 = new byte[(int)memoryStream.Length];
					int num = memoryStream.Read(array2, 0, array2.Length);
					context.Response.ContentType = ContentType.Compose("text/xml", Encoding.UTF8);
					context.Response.OutputStream.Write(array2, 0, num);
				}
				else
				{
					context.Response.ContentType = "text/xml";
					context.Response.WriteFile(physicalPath);
				}
				if (Tracing.On)
				{
					Tracing.Exit("IHttpHandler.ProcessRequest", traceMethod);
				}
				return;
			}
			if (Tracing.On)
			{
				Tracing.Exit("IHttpHandler.ProcessRequest", traceMethod);
			}
			throw new HttpException(404, Res.GetString("WebPathNotFound", new object[] { context.Request.Path }));
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x000156CC File Offset: 0x000138CC
		private static string GetDirPartOfPath(string str)
		{
			int num = str.LastIndexOf('/');
			if (num <= 0)
			{
				return "";
			}
			return str.Substring(0, num);
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x000156F4 File Offset: 0x000138F4
		private static string GetFilePartOfPath(string str)
		{
			int num = str.LastIndexOf('/');
			if (num < 0)
			{
				return str;
			}
			if (num == str.Length - 1)
			{
				return "";
			}
			return str.Substring(num + 1);
		}
	}
}
