using System;
using System.Web.Services.Protocols;

namespace System.Web.Services.Description
{
	// Token: 0x020000C3 RID: 195
	internal class HttpGetProtocolReflector : HttpProtocolReflector
	{
		// Token: 0x17000150 RID: 336
		// (get) Token: 0x0600050E RID: 1294 RVA: 0x000173B5 File Offset: 0x000155B5
		public override string ProtocolName
		{
			get
			{
				return "HttpGet";
			}
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x00017424 File Offset: 0x00015624
		protected override void BeginClass()
		{
			if (base.IsEmptyBinding)
			{
				return;
			}
			HttpBinding httpBinding = new HttpBinding();
			httpBinding.Verb = "GET";
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

		// Token: 0x06000510 RID: 1296 RVA: 0x000174B8 File Offset: 0x000156B8
		protected override bool ReflectMethod()
		{
			if (!base.ReflectUrlParameters())
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
