using System;
using System.Threading.Tasks;

namespace System.Xml
{
	// Token: 0x0200009C RID: 156
	internal interface IDtdParser
	{
		// Token: 0x06000541 RID: 1345
		IDtdInfo ParseInternalDtd(IDtdParserAdapter adapter, bool saveInternalSubset);

		// Token: 0x06000542 RID: 1346
		IDtdInfo ParseFreeFloatingDtd(string baseUri, string docTypeName, string publicId, string systemId, string internalSubset, IDtdParserAdapter adapter);

		// Token: 0x06000543 RID: 1347
		Task<IDtdInfo> ParseInternalDtdAsync(IDtdParserAdapter adapter, bool saveInternalSubset);

		// Token: 0x06000544 RID: 1348
		Task<IDtdInfo> ParseFreeFloatingDtdAsync(string baseUri, string docTypeName, string publicId, string systemId, string internalSubset, IDtdParserAdapter adapter);
	}
}
