using System;
using System.Web.Services.Protocols;

namespace System.Web.Services.Description
{
	// Token: 0x020000C6 RID: 198
	internal class HttpPostProtocolReflector : HttpProtocolReflector
	{
		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06000518 RID: 1304 RVA: 0x0001752D File Offset: 0x0001572D
		public override string ProtocolName
		{
			get
			{
				return "HttpPost";
			}
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x0001759C File Offset: 0x0001579C
		protected override void BeginClass()
		{
			if (base.IsEmptyBinding)
			{
				return;
			}
			HttpBinding httpBinding = new HttpBinding();
			httpBinding.Verb = "POST";
			base.Binding.Extensions.Add(httpBinding);
			HttpAddressBinding httpAddressBinding = new HttpAddressBinding();
			httpAddressBinding.Location = base.ServiceUrl;
			if (base.UriFixups != null)
			{
				base.UriFixups.Add(delegate(Uri current)
				{
					httpAddressBinding.Location = DiscoveryServerType.CombineUris(current, httpAddressBinding.Location);
				});
			}
			base.Port.Extensions.Add(httpAddressBinding);
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x00017630 File Offset: 0x00015830
		protected override bool ReflectMethod()
		{
			if (!base.ReflectMimeParameters())
			{
				return false;
			}
			if (!base.ReflectMimeReturn())
			{
				return false;
			}
			HttpOperationBinding httpOperationBinding = new HttpOperationBinding();
			httpOperationBinding.Location = base.MethodUrl;
			base.OperationBinding.Extensions.Add(httpOperationBinding);
			return true;
		}
	}
}
