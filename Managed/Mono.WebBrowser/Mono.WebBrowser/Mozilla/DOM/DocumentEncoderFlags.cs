using System;

namespace Mono.Mozilla.DOM
{
	// Token: 0x02000145 RID: 325
	internal enum DocumentEncoderFlags : uint
	{
		// Token: 0x0400013A RID: 314
		OutputSelectionOnly = 1U,
		// Token: 0x0400013B RID: 315
		OutputFormatted,
		// Token: 0x0400013C RID: 316
		OutputRaw = 4U,
		// Token: 0x0400013D RID: 317
		OutputBodyOnly = 8U,
		// Token: 0x0400013E RID: 318
		OutputPreformatted = 16U,
		// Token: 0x0400013F RID: 319
		OutputWrap = 32U,
		// Token: 0x04000140 RID: 320
		OutputFormatFlowed = 64U,
		// Token: 0x04000141 RID: 321
		OutputAbsoluteLinks = 128U,
		// Token: 0x04000142 RID: 322
		OutputEncodeW3CEntities = 256U,
		// Token: 0x04000143 RID: 323
		OutputCRLineBreak = 512U,
		// Token: 0x04000144 RID: 324
		OutputLFLineBreak = 1024U,
		// Token: 0x04000145 RID: 325
		OutputNoScriptContent = 2048U,
		// Token: 0x04000146 RID: 326
		OutputNoFramesContent = 4096U,
		// Token: 0x04000147 RID: 327
		OutputNoFormattingInPre = 8192U,
		// Token: 0x04000148 RID: 328
		OutputEncodeBasicEntities = 16384U,
		// Token: 0x04000149 RID: 329
		OutputEncodeLatin1Entities = 32768U,
		// Token: 0x0400014A RID: 330
		OutputEncodeHTMLEntities = 65536U,
		// Token: 0x0400014B RID: 331
		OutputPersistNBSP = 131072U
	}
}
