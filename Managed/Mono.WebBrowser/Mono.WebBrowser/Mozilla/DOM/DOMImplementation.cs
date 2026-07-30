using System;
using Mono.WebBrowser.DOM;

namespace Mono.Mozilla.DOM
{
	// Token: 0x02000134 RID: 308
	internal class DOMImplementation : DOMObject, IDOMImplementation
	{
		// Token: 0x06000908 RID: 2312 RVA: 0x00005F21 File Offset: 0x00004121
		public DOMImplementation(WebBrowser control, nsIDOMDOMImplementation domImpl)
			: base(control)
		{
			if (control.platform != control.enginePlatform)
			{
				this.unmanagedDomImpl = nsDOMDOMImplementation.GetProxy(control, domImpl);
			}
			else
			{
				this.unmanagedDomImpl = domImpl;
			}
			this.hashcode = this.unmanagedDomImpl.GetHashCode();
		}

		// Token: 0x06000909 RID: 2313 RVA: 0x00005F5F File Offset: 0x0000415F
		protected override void Dispose(bool disposing)
		{
			if (!this.disposed && disposing)
			{
				this.unmanagedDomImpl = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600090A RID: 2314 RVA: 0x00005F7C File Offset: 0x0000417C
		public bool HasFeature(string feature, string version)
		{
			Base.StringSet(this.storage, feature);
			UniString uniString = new UniString(version);
			bool flag;
			this.unmanagedDomImpl.hasFeature(this.storage, uniString.Handle, out flag);
			return flag;
		}

		// Token: 0x0600090B RID: 2315 RVA: 0x00005FB8 File Offset: 0x000041B8
		public IDocumentType CreateDocumentType(string qualifiedName, string publicId, string systemId)
		{
			Base.StringSet(this.storage, qualifiedName);
			UniString uniString = new UniString(publicId);
			UniString uniString2 = new UniString(systemId);
			nsIDOMDocumentType nsIDOMDocumentType;
			this.unmanagedDomImpl.createDocumentType(this.storage, uniString.Handle, uniString2.Handle, out nsIDOMDocumentType);
			return new DocumentType(this.control, nsIDOMDocumentType);
		}

		// Token: 0x0600090C RID: 2316 RVA: 0x0000600C File Offset: 0x0000420C
		public IDocument CreateDocument(string namespaceURI, string qualifiedName, IDocumentType doctype)
		{
			Base.StringSet(this.storage, namespaceURI);
			UniString uniString = new UniString(qualifiedName);
			nsIDOMDocument nsIDOMDocument;
			this.unmanagedDomImpl.createDocument(this.storage, uniString.Handle, ((DocumentType)doctype).ComObject, out nsIDOMDocument);
			this.control.documents.Add(nsIDOMDocument.GetHashCode(), new Document(this.control, nsIDOMDocument));
			return this.control.documents[nsIDOMDocument.GetHashCode()] as IDocument;
		}

		// Token: 0x04000112 RID: 274
		private nsIDOMDOMImplementation unmanagedDomImpl;

		// Token: 0x04000113 RID: 275
		protected int hashcode;
	}
}
