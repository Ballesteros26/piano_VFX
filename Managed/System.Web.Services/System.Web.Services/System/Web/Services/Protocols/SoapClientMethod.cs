using System;
using System.Web.Services.Description;
using System.Xml.Serialization;

namespace System.Web.Services.Protocols
{
	// Token: 0x0200005C RID: 92
	internal class SoapClientMethod
	{
		// Token: 0x04000245 RID: 581
		internal XmlSerializer returnSerializer;

		// Token: 0x04000246 RID: 582
		internal XmlSerializer parameterSerializer;

		// Token: 0x04000247 RID: 583
		internal XmlSerializer inHeaderSerializer;

		// Token: 0x04000248 RID: 584
		internal XmlSerializer outHeaderSerializer;

		// Token: 0x04000249 RID: 585
		internal string action;

		// Token: 0x0400024A RID: 586
		internal LogicalMethodInfo methodInfo;

		// Token: 0x0400024B RID: 587
		internal SoapHeaderMapping[] inHeaderMappings;

		// Token: 0x0400024C RID: 588
		internal SoapHeaderMapping[] outHeaderMappings;

		// Token: 0x0400024D RID: 589
		internal SoapReflectedExtension[] extensions;

		// Token: 0x0400024E RID: 590
		internal object[] extensionInitializers;

		// Token: 0x0400024F RID: 591
		internal bool oneWay;

		// Token: 0x04000250 RID: 592
		internal bool rpc;

		// Token: 0x04000251 RID: 593
		internal SoapBindingUse use;

		// Token: 0x04000252 RID: 594
		internal SoapParameterStyle paramStyle;
	}
}
