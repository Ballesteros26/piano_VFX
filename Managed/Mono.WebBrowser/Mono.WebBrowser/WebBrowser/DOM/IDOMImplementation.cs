using System;

namespace Mono.WebBrowser.DOM
{
	// Token: 0x02000026 RID: 38
	public interface IDOMImplementation
	{
		// Token: 0x060000B7 RID: 183
		bool HasFeature(string feature, string version);

		// Token: 0x060000B8 RID: 184
		IDocumentType CreateDocumentType(string qualifiedName, string publicId, string systemId);

		// Token: 0x060000B9 RID: 185
		IDocument CreateDocument(string namespaceURI, string qualifiedName, IDocumentType doctype);
	}
}
