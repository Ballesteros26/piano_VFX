using System;

namespace System.Xml
{
	// Token: 0x0200009B RID: 155
	internal interface IDtdEntityInfo
	{
		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000535 RID: 1333
		string Name { get; }

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000536 RID: 1334
		bool IsExternal { get; }

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000537 RID: 1335
		bool IsDeclaredInExternal { get; }

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000538 RID: 1336
		bool IsUnparsedEntity { get; }

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000539 RID: 1337
		bool IsParameterEntity { get; }

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x0600053A RID: 1338
		string BaseUriString { get; }

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x0600053B RID: 1339
		string DeclaredUriString { get; }

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x0600053C RID: 1340
		string SystemId { get; }

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x0600053D RID: 1341
		string PublicId { get; }

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x0600053E RID: 1342
		string Text { get; }

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x0600053F RID: 1343
		int LineNumber { get; }

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000540 RID: 1344
		int LinePosition { get; }
	}
}
