using System;

namespace System.Linq.Expressions
{
	// Token: 0x020002B6 RID: 694
	internal sealed class SymbolDocumentWithGuids : SymbolDocumentInfo
	{
		// Token: 0x060014AE RID: 5294 RVA: 0x0003D8C4 File Offset: 0x0003BAC4
		internal SymbolDocumentWithGuids(string fileName, ref Guid language)
			: base(fileName)
		{
			this.Language = language;
			this.DocumentType = SymbolDocumentInfo.DocumentType_Text;
		}

		// Token: 0x060014AF RID: 5295 RVA: 0x0003D8E4 File Offset: 0x0003BAE4
		internal SymbolDocumentWithGuids(string fileName, ref Guid language, ref Guid vendor)
			: base(fileName)
		{
			this.Language = language;
			this.LanguageVendor = vendor;
			this.DocumentType = SymbolDocumentInfo.DocumentType_Text;
		}

		// Token: 0x060014B0 RID: 5296 RVA: 0x0003D910 File Offset: 0x0003BB10
		internal SymbolDocumentWithGuids(string fileName, ref Guid language, ref Guid vendor, ref Guid documentType)
			: base(fileName)
		{
			this.Language = language;
			this.LanguageVendor = vendor;
			this.DocumentType = documentType;
		}

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x060014B1 RID: 5297 RVA: 0x0003D93E File Offset: 0x0003BB3E
		public override Guid Language { get; }

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x060014B2 RID: 5298 RVA: 0x0003D946 File Offset: 0x0003BB46
		public override Guid LanguageVendor { get; }

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x060014B3 RID: 5299 RVA: 0x0003D94E File Offset: 0x0003BB4E
		public override Guid DocumentType { get; }
	}
}
