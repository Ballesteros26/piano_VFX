using System;
using Mono.WebBrowser.DOM;

namespace Mono.Mozilla.DOM
{
	// Token: 0x02000137 RID: 311
	internal class DocumentType : Node, IDocumentType, INode
	{
		// Token: 0x06000945 RID: 2373 RVA: 0x00006EB7 File Offset: 0x000050B7
		public DocumentType(WebBrowser control, nsIDOMDocumentType doctype)
			: base(control, doctype)
		{
			if (control.platform != control.enginePlatform)
			{
				this.doctype = nsDOMDocumentType.GetProxy(control, doctype);
				return;
			}
			this.doctype = doctype;
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x00006EE4 File Offset: 0x000050E4
		protected override void Dispose(bool disposing)
		{
			if (!this.disposed && disposing)
			{
				this.resources.Clear();
				this.doctype = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000947 RID: 2375 RVA: 0x00006F0A File Offset: 0x0000510A
		internal nsIDOMDocumentType ComObject
		{
			get
			{
				return this.doctype;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000948 RID: 2376 RVA: 0x00006F12 File Offset: 0x00005112
		public string Name
		{
			get
			{
				this.doctype.getName(this.storage);
				return Base.StringGet(this.storage);
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000949 RID: 2377 RVA: 0x00006F34 File Offset: 0x00005134
		public INamedNodeMap Entities
		{
			get
			{
				nsIDOMNamedNodeMap nsIDOMNamedNodeMap;
				this.doctype.getEntities(out nsIDOMNamedNodeMap);
				return new NamedNodeMap(this.control, nsIDOMNamedNodeMap);
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x0600094A RID: 2378 RVA: 0x00006F5C File Offset: 0x0000515C
		public INamedNodeMap Notations
		{
			get
			{
				nsIDOMNamedNodeMap nsIDOMNamedNodeMap;
				this.doctype.getNotations(out nsIDOMNamedNodeMap);
				return new NamedNodeMap(this.control, nsIDOMNamedNodeMap);
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x0600094B RID: 2379 RVA: 0x00006F83 File Offset: 0x00005183
		public string PublicId
		{
			get
			{
				this.doctype.getPublicId(this.storage);
				return Base.StringGet(this.storage);
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x0600094C RID: 2380 RVA: 0x00006FA2 File Offset: 0x000051A2
		public string SystemId
		{
			get
			{
				this.doctype.getSystemId(this.storage);
				return Base.StringGet(this.storage);
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x0600094D RID: 2381 RVA: 0x00006FC1 File Offset: 0x000051C1
		public string InternalSubset
		{
			get
			{
				this.doctype.getInternalSubset(this.storage);
				return Base.StringGet(this.storage);
			}
		}

		// Token: 0x0600094E RID: 2382 RVA: 0x00006FE0 File Offset: 0x000051E0
		public override int GetHashCode()
		{
			return this.hashcode;
		}

		// Token: 0x0400011B RID: 283
		internal nsIDOMDocumentType doctype;
	}
}
