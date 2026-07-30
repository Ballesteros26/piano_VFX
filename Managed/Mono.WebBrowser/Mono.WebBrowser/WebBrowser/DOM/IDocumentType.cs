using System;

namespace Mono.WebBrowser.DOM
{
	// Token: 0x02000028 RID: 40
	public interface IDocumentType : INode
	{
		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000E4 RID: 228
		string Name { get; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000E5 RID: 229
		INamedNodeMap Entities { get; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000E6 RID: 230
		INamedNodeMap Notations { get; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000E7 RID: 231
		string PublicId { get; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000E8 RID: 232
		string SystemId { get; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000E9 RID: 233
		string InternalSubset { get; }
	}
}
