using System;
using System.Web.Services.Protocols;

namespace System.Web.Services.Description
{
	// Token: 0x020000C5 RID: 197
	internal class HttpPostProtocolImporter : HttpProtocolImporter
	{
		// Token: 0x06000514 RID: 1300 RVA: 0x00017524 File Offset: 0x00015724
		public HttpPostProtocolImporter()
			: base(true)
		{
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000515 RID: 1301 RVA: 0x0001752D File Offset: 0x0001572D
		public override string ProtocolName
		{
			get
			{
				return "HttpPost";
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000516 RID: 1302 RVA: 0x00017534 File Offset: 0x00015734
		internal override Type BaseClass
		{
			get
			{
				if (base.Style == ServiceDescriptionImportStyle.Client)
				{
					return typeof(HttpPostClientProtocol);
				}
				return typeof(WebService);
			}
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x00017554 File Offset: 0x00015754
		protected override bool IsBindingSupported()
		{
			HttpBinding httpBinding = (HttpBinding)base.Binding.Extensions.Find(typeof(HttpBinding));
			return httpBinding != null && !(httpBinding.Verb != "POST");
		}
	}
}
