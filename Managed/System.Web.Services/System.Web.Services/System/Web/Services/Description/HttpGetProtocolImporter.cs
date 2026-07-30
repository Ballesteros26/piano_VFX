using System;
using System.Web.Services.Protocols;

namespace System.Web.Services.Description
{
	// Token: 0x020000C2 RID: 194
	internal class HttpGetProtocolImporter : HttpProtocolImporter
	{
		// Token: 0x0600050A RID: 1290 RVA: 0x000173AC File Offset: 0x000155AC
		public HttpGetProtocolImporter()
			: base(false)
		{
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x0600050B RID: 1291 RVA: 0x000173B5 File Offset: 0x000155B5
		public override string ProtocolName
		{
			get
			{
				return "HttpGet";
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x0600050C RID: 1292 RVA: 0x000173BC File Offset: 0x000155BC
		internal override Type BaseClass
		{
			get
			{
				if (base.Style == ServiceDescriptionImportStyle.Client)
				{
					return typeof(HttpGetClientProtocol);
				}
				return typeof(WebService);
			}
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x000173DC File Offset: 0x000155DC
		protected override bool IsBindingSupported()
		{
			HttpBinding httpBinding = (HttpBinding)base.Binding.Extensions.Find(typeof(HttpBinding));
			return httpBinding != null && !(httpBinding.Verb != "GET");
		}
	}
}
