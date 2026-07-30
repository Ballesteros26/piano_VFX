using System;
using System.Web.Services.Description;
using System.Xml;
using System.Xml.Serialization;

namespace System.Web.Services.Protocols
{
	// Token: 0x02000074 RID: 116
	internal class SoapReflectedMethod
	{
		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060002FE RID: 766 RVA: 0x0000D2E1 File Offset: 0x0000B4E1
		internal bool IsClaimsConformance
		{
			get
			{
				return this.binding != null && this.binding.ConformsTo == WsiProfiles.BasicProfile1_1;
			}
		}

		// Token: 0x040002B2 RID: 690
		internal LogicalMethodInfo methodInfo;

		// Token: 0x040002B3 RID: 691
		internal string action;

		// Token: 0x040002B4 RID: 692
		internal string name;

		// Token: 0x040002B5 RID: 693
		internal XmlMembersMapping requestMappings;

		// Token: 0x040002B6 RID: 694
		internal XmlMembersMapping responseMappings;

		// Token: 0x040002B7 RID: 695
		internal XmlMembersMapping inHeaderMappings;

		// Token: 0x040002B8 RID: 696
		internal XmlMembersMapping outHeaderMappings;

		// Token: 0x040002B9 RID: 697
		internal SoapReflectedHeader[] headers;

		// Token: 0x040002BA RID: 698
		internal SoapReflectedExtension[] extensions;

		// Token: 0x040002BB RID: 699
		internal bool oneWay;

		// Token: 0x040002BC RID: 700
		internal bool rpc;

		// Token: 0x040002BD RID: 701
		internal SoapBindingUse use;

		// Token: 0x040002BE RID: 702
		internal SoapParameterStyle paramStyle;

		// Token: 0x040002BF RID: 703
		internal WebServiceBindingAttribute binding;

		// Token: 0x040002C0 RID: 704
		internal XmlQualifiedName requestElementName;

		// Token: 0x040002C1 RID: 705
		internal XmlQualifiedName portType;
	}
}
