using System;
using System.Text;
using System.Threading.Tasks;

namespace System.Xml
{
	// Token: 0x0200009D RID: 157
	internal interface IDtdParserAdapter
	{
		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000545 RID: 1349
		XmlNameTable NameTable { get; }

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000546 RID: 1350
		IXmlNamespaceResolver NamespaceResolver { get; }

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000547 RID: 1351
		Uri BaseUri { get; }

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000548 RID: 1352
		char[] ParsingBuffer { get; }

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000549 RID: 1353
		int ParsingBufferLength { get; }

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x0600054A RID: 1354
		// (set) Token: 0x0600054B RID: 1355
		int CurrentPosition { get; set; }

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x0600054C RID: 1356
		int LineNo { get; }

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x0600054D RID: 1357
		int LineStartPosition { get; }

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x0600054E RID: 1358
		bool IsEof { get; }

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x0600054F RID: 1359
		int EntityStackLength { get; }

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000550 RID: 1360
		bool IsEntityEolNormalized { get; }

		// Token: 0x06000551 RID: 1361
		int ReadData();

		// Token: 0x06000552 RID: 1362
		void OnNewLine(int pos);

		// Token: 0x06000553 RID: 1363
		int ParseNumericCharRef(StringBuilder internalSubsetBuilder);

		// Token: 0x06000554 RID: 1364
		int ParseNamedCharRef(bool expand, StringBuilder internalSubsetBuilder);

		// Token: 0x06000555 RID: 1365
		void ParsePI(StringBuilder sb);

		// Token: 0x06000556 RID: 1366
		void ParseComment(StringBuilder sb);

		// Token: 0x06000557 RID: 1367
		bool PushEntity(IDtdEntityInfo entity, out int entityId);

		// Token: 0x06000558 RID: 1368
		bool PopEntity(out IDtdEntityInfo oldEntity, out int newEntityId);

		// Token: 0x06000559 RID: 1369
		bool PushExternalSubset(string systemId, string publicId);

		// Token: 0x0600055A RID: 1370
		void PushInternalDtd(string baseUri, string internalDtd);

		// Token: 0x0600055B RID: 1371
		void OnSystemId(string systemId, LineInfo keywordLineInfo, LineInfo systemLiteralLineInfo);

		// Token: 0x0600055C RID: 1372
		void OnPublicId(string publicId, LineInfo keywordLineInfo, LineInfo publicLiteralLineInfo);

		// Token: 0x0600055D RID: 1373
		void Throw(Exception e);

		// Token: 0x0600055E RID: 1374
		Task<int> ReadDataAsync();

		// Token: 0x0600055F RID: 1375
		Task<int> ParseNumericCharRefAsync(StringBuilder internalSubsetBuilder);

		// Token: 0x06000560 RID: 1376
		Task<int> ParseNamedCharRefAsync(bool expand, StringBuilder internalSubsetBuilder);

		// Token: 0x06000561 RID: 1377
		Task ParsePIAsync(StringBuilder sb);

		// Token: 0x06000562 RID: 1378
		Task ParseCommentAsync(StringBuilder sb);

		// Token: 0x06000563 RID: 1379
		Task<Tuple<int, bool>> PushEntityAsync(IDtdEntityInfo entity);

		// Token: 0x06000564 RID: 1380
		Task<bool> PushExternalSubsetAsync(string systemId, string publicId);
	}
}
