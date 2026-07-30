using System;
using System.IO;
using System.Text;
using System.Web.Services.Description;
using System.Xml.Schema;

namespace System.Web.Services.Protocols
{
	// Token: 0x02000029 RID: 41
	internal sealed class DiscoveryServerProtocol : ServerProtocol
	{
		// Token: 0x060000E1 RID: 225 RVA: 0x000042CC File Offset: 0x000024CC
		internal override bool Initialize()
		{
			if ((this.serverType = (DiscoveryServerType)base.GetFromCache(typeof(DiscoveryServerProtocol), base.Type)) == null && (this.serverType = (DiscoveryServerType)base.GetFromCache(typeof(DiscoveryServerProtocol), base.Type, true)) == null)
			{
				object internalSyncObject = ServerProtocol.InternalSyncObject;
				lock (internalSyncObject)
				{
					if ((this.serverType = (DiscoveryServerType)base.GetFromCache(typeof(DiscoveryServerProtocol), base.Type)) == null && (this.serverType = (DiscoveryServerType)base.GetFromCache(typeof(DiscoveryServerProtocol), base.Type, true)) == null)
					{
						bool flag2 = base.IsCacheUnderPressure(typeof(DiscoveryServerProtocol), base.Type);
						string text = RuntimeUtils.EscapeUri(base.Request.Url);
						this.serverType = new DiscoveryServerType(base.Type, text, flag2);
						base.AddToCache(typeof(DiscoveryServerProtocol), base.Type, this.serverType, flag2);
					}
				}
			}
			return true;
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x00004404 File Offset: 0x00002604
		internal override ServerType ServerType
		{
			get
			{
				return this.serverType;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x00002B51 File Offset: 0x00000D51
		internal override bool IsOneWay
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x0000440C File Offset: 0x0000260C
		internal override LogicalMethodInfo MethodInfo
		{
			get
			{
				return this.serverType.MethodInfo;
			}
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00004419 File Offset: 0x00002619
		internal override object[] ReadParameters()
		{
			return new object[0];
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00004424 File Offset: 0x00002624
		internal override void WriteReturns(object[] returnValues, Stream outputStream)
		{
			string text = base.Request.QueryString["schema"];
			Encoding encoding = new UTF8Encoding(false);
			if (text != null)
			{
				XmlSchema schema = this.serverType.GetSchema(text);
				if (schema == null)
				{
					throw new InvalidOperationException(Res.GetString("WebSchemaNotFound"));
				}
				base.Response.ContentType = ContentType.Compose("text/xml", encoding);
				schema.Write(new StreamWriter(outputStream, encoding));
				return;
			}
			else
			{
				text = base.Request.QueryString["wsdl"];
				if (text != null)
				{
					ServiceDescription serviceDescription = this.serverType.GetServiceDescription(text);
					if (serviceDescription == null)
					{
						throw new InvalidOperationException(Res.GetString("ServiceDescriptionWasNotFound0"));
					}
					base.Response.ContentType = ContentType.Compose("text/xml", encoding);
					if (this.serverType.UriFixups == null)
					{
						serviceDescription.Write(new StreamWriter(outputStream, encoding));
						return;
					}
					object obj = this.syncRoot;
					lock (obj)
					{
						this.RunUriFixups();
						serviceDescription.Write(new StreamWriter(outputStream, encoding));
					}
					return;
				}
				else
				{
					string text2 = base.Request.QueryString[null];
					if (text2 != null && string.Compare(text2, "wsdl", StringComparison.OrdinalIgnoreCase) == 0)
					{
						base.Response.ContentType = ContentType.Compose("text/xml", encoding);
						if (this.serverType.UriFixups == null)
						{
							this.serverType.Description.Write(new StreamWriter(outputStream, encoding));
							return;
						}
						object obj = this.syncRoot;
						lock (obj)
						{
							this.RunUriFixups();
							this.serverType.Description.Write(new StreamWriter(outputStream, encoding));
						}
						return;
					}
					else
					{
						if (text2 == null || string.Compare(text2, "disco", StringComparison.OrdinalIgnoreCase) != 0)
						{
							throw new InvalidOperationException(Res.GetString("internalError0"));
						}
						base.Response.ContentType = ContentType.Compose("text/xml", encoding);
						if (this.serverType.UriFixups == null)
						{
							this.serverType.Disco.Write(new StreamWriter(outputStream, encoding));
							return;
						}
						object obj = this.syncRoot;
						lock (obj)
						{
							this.RunUriFixups();
							this.serverType.Disco.Write(new StreamWriter(outputStream, encoding));
						}
						return;
					}
				}
			}
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x000046A0 File Offset: 0x000028A0
		internal override bool WriteException(Exception e, Stream outputStream)
		{
			base.Response.Clear();
			base.Response.ClearHeaders();
			base.Response.ContentType = ContentType.Compose("text/plain", Encoding.UTF8);
			base.Response.StatusCode = 500;
			base.Response.StatusDescription = HttpWorkerRequest.GetStatusDescription(base.Response.StatusCode);
			StreamWriter streamWriter = new StreamWriter(outputStream, new UTF8Encoding(false));
			streamWriter.WriteLine(base.GenerateFaultString(e, true));
			streamWriter.Flush();
			return true;
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x0000210D File Offset: 0x0000030D
		internal void Discover()
		{
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00004728 File Offset: 0x00002928
		private void RunUriFixups()
		{
			foreach (Action<Uri> action in this.serverType.UriFixups)
			{
				action(base.Context.Request.Url);
			}
		}

		// Token: 0x040001DA RID: 474
		private DiscoveryServerType serverType;

		// Token: 0x040001DB RID: 475
		private object syncRoot = new object();
	}
}
