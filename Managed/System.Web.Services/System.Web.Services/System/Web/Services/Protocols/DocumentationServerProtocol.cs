using System;
using System.IO;
using System.Security.Permissions;
using System.Threading;
using System.Web.Services.Configuration;
using System.Web.Services.Diagnostics;
using System.Web.UI;

namespace System.Web.Services.Protocols
{
	// Token: 0x0200002C RID: 44
	internal sealed class DocumentationServerProtocol : ServerProtocol
	{
		// Token: 0x060000F6 RID: 246 RVA: 0x000048C8 File Offset: 0x00002AC8
		internal override bool Initialize()
		{
			if ((this.serverType = (DocumentationServerType)base.GetFromCache(typeof(DocumentationServerProtocol), base.Type)) == null && (this.serverType = (DocumentationServerType)base.GetFromCache(typeof(DocumentationServerProtocol), base.Type, true)) == null)
			{
				object internalSyncObject = ServerProtocol.InternalSyncObject;
				lock (internalSyncObject)
				{
					if ((this.serverType = (DocumentationServerType)base.GetFromCache(typeof(DocumentationServerProtocol), base.Type)) == null && (this.serverType = (DocumentationServerType)base.GetFromCache(typeof(DocumentationServerProtocol), base.Type, true)) == null)
					{
						bool flag2 = base.IsCacheUnderPressure(typeof(DocumentationServerProtocol), base.Type);
						string text = RuntimeUtils.EscapeUri(base.Request.Url);
						this.serverType = new DocumentationServerType(base.Type, text, flag2);
						base.AddToCache(typeof(DocumentationServerProtocol), base.Type, this.serverType, flag2);
					}
				}
			}
			WebServicesSection webServicesSection = WebServicesSection.Current;
			if (webServicesSection.WsdlHelpGenerator.Href != null && webServicesSection.WsdlHelpGenerator.Href.Length > 0)
			{
				TraceMethod traceMethod = (Tracing.On ? new TraceMethod(this, "Initialize", Array.Empty<object>()) : null);
				if (Tracing.On)
				{
					Tracing.Enter("ASP.NET", traceMethod, new TraceMethod(typeof(PageParser), "GetCompiledPageInstance", new object[]
					{
						webServicesSection.WsdlHelpGenerator.HelpGeneratorVirtualPath,
						webServicesSection.WsdlHelpGenerator.HelpGeneratorPath,
						base.Context
					}));
				}
				this.handler = this.GetCompiledPageInstance(webServicesSection.WsdlHelpGenerator.HelpGeneratorVirtualPath, webServicesSection.WsdlHelpGenerator.HelpGeneratorPath, base.Context);
				if (Tracing.On)
				{
					Tracing.Exit("ASP.NET", traceMethod);
				}
			}
			return true;
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00004AD8 File Offset: 0x00002CD8
		[SecurityPermission(SecurityAction.Assert, Unrestricted = true)]
		[FileIOPermission(SecurityAction.Assert, Unrestricted = true)]
		private IHttpHandler GetCompiledPageInstance(string virtualPath, string inputFile, HttpContext context)
		{
			return PageParser.GetCompiledPageInstance(virtualPath, inputFile, context);
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x00004AE2 File Offset: 0x00002CE2
		internal override ServerType ServerType
		{
			get
			{
				return this.serverType;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x00002B51 File Offset: 0x00000D51
		internal override bool IsOneWay
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000FA RID: 250 RVA: 0x00004AEA File Offset: 0x00002CEA
		internal override LogicalMethodInfo MethodInfo
		{
			get
			{
				return this.serverType.MethodInfo;
			}
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00004419 File Offset: 0x00002619
		internal override object[] ReadParameters()
		{
			return new object[0];
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00004AF8 File Offset: 0x00002CF8
		internal override void WriteReturns(object[] returnValues, Stream outputStream)
		{
			try
			{
				if (this.handler != null)
				{
					base.Context.Items.Add("wsdls", this.serverType.ServiceDescriptions);
					base.Context.Items.Add("schemas", this.serverType.Schemas);
					if (base.Context.Request.Url.IsLoopback || base.Context.Request.IsLocal)
					{
						base.Context.Items.Add("wsdlsWithPost", this.serverType.ServiceDescriptionsWithPost);
						base.Context.Items.Add("schemasWithPost", this.serverType.SchemasWithPost);
					}
					base.Context.Items.Add("conformanceWarnings", WebServicesSection.Current.EnabledConformanceWarnings);
					base.Response.ContentType = "text/html";
					if (this.serverType.UriFixups == null)
					{
						this.handler.ProcessRequest(base.Context);
					}
					else
					{
						object obj = this.syncRoot;
						lock (obj)
						{
							this.RunUriFixups();
							this.handler.ProcessRequest(base.Context);
						}
					}
				}
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				throw new InvalidOperationException(Res.GetString("HelpGeneratorInternalError"), ex);
			}
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00002B51 File Offset: 0x00000D51
		internal override bool WriteException(Exception e, Stream outputStream)
		{
			return false;
		}

		// Token: 0x060000FE RID: 254 RVA: 0x0000210D File Offset: 0x0000030D
		internal void Documentation()
		{
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00004CA4 File Offset: 0x00002EA4
		private void RunUriFixups()
		{
			foreach (Action<Uri> action in this.serverType.UriFixups)
			{
				action(base.Context.Request.Url);
			}
		}

		// Token: 0x040001E2 RID: 482
		private DocumentationServerType serverType;

		// Token: 0x040001E3 RID: 483
		private IHttpHandler handler;

		// Token: 0x040001E4 RID: 484
		private object syncRoot = new object();

		// Token: 0x040001E5 RID: 485
		private const int MAX_PATH_SIZE = 1024;
	}
}
