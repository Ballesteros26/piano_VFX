using System;

namespace System.Xml
{
	// Token: 0x0200009F RID: 159
	internal interface IDtdParserAdapterV1 : IDtdParserAdapterWithValidation, IDtdParserAdapter
	{
		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000567 RID: 1383
		bool V1CompatibilityMode { get; }

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000568 RID: 1384
		bool Normalization { get; }

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000569 RID: 1385
		bool Namespaces { get; }
	}
}
