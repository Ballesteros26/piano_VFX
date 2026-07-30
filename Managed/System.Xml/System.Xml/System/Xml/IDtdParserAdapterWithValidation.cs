using System;

namespace System.Xml
{
	// Token: 0x0200009E RID: 158
	internal interface IDtdParserAdapterWithValidation : IDtdParserAdapter
	{
		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000565 RID: 1381
		bool DtdValidation { get; }

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000566 RID: 1382
		IValidationEventHandling ValidationEventHandling { get; }
	}
}
