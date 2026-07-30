using System;
using System.Net;

namespace System.Xml
{
	// Token: 0x020002A3 RID: 675
	internal class XmlNullResolver : XmlResolver
	{
		// Token: 0x060018EC RID: 6380 RVA: 0x0008FD37 File Offset: 0x0008DF37
		private XmlNullResolver()
		{
		}

		// Token: 0x060018ED RID: 6381 RVA: 0x0008FD3F File Offset: 0x0008DF3F
		public override object GetEntity(Uri absoluteUri, string role, Type ofObjectToReturn)
		{
			throw new XmlException("Resolving of external URIs was prohibited.", string.Empty);
		}

		// Token: 0x170004AD RID: 1197
		// (set) Token: 0x060018EE RID: 6382 RVA: 0x00002F50 File Offset: 0x00001150
		public override ICredentials Credentials
		{
			set
			{
			}
		}

		// Token: 0x0400104D RID: 4173
		public static readonly XmlNullResolver Singleton = new XmlNullResolver();
	}
}
