using System;

namespace System.Xml.Xsl.Xslt
{
	// Token: 0x02000581 RID: 1409
	internal class OutputScopeManager
	{
		// Token: 0x060037C8 RID: 14280 RVA: 0x00136CE8 File Offset: 0x00134EE8
		public OutputScopeManager()
		{
			this.Reset();
		}

		// Token: 0x060037C9 RID: 14281 RVA: 0x00136D03 File Offset: 0x00134F03
		public void Reset()
		{
			this.records[0].prefix = null;
			this.records[0].nsUri = null;
			this.PushScope();
		}

		// Token: 0x060037CA RID: 14282 RVA: 0x00136D2F File Offset: 0x00134F2F
		public void PushScope()
		{
			this.lastScopes++;
		}

		// Token: 0x060037CB RID: 14283 RVA: 0x00136D40 File Offset: 0x00134F40
		public void PopScope()
		{
			if (0 < this.lastScopes)
			{
				this.lastScopes--;
				return;
			}
			OutputScopeManager.ScopeReord[] array;
			int num;
			do
			{
				array = this.records;
				num = this.lastRecord - 1;
				this.lastRecord = num;
			}
			while (array[num].scopeCount == 0);
			this.lastScopes = this.records[this.lastRecord].scopeCount;
			this.lastScopes--;
		}

		// Token: 0x060037CC RID: 14284 RVA: 0x00136DB2 File Offset: 0x00134FB2
		public void AddNamespace(string prefix, string uri)
		{
			this.AddRecord(prefix, uri);
		}

		// Token: 0x060037CD RID: 14285 RVA: 0x00136DBC File Offset: 0x00134FBC
		private void AddRecord(string prefix, string uri)
		{
			this.records[this.lastRecord].scopeCount = this.lastScopes;
			this.lastRecord++;
			if (this.lastRecord == this.records.Length)
			{
				OutputScopeManager.ScopeReord[] array = new OutputScopeManager.ScopeReord[this.lastRecord * 2];
				Array.Copy(this.records, 0, array, 0, this.lastRecord);
				this.records = array;
			}
			this.lastScopes = 0;
			this.records[this.lastRecord].prefix = prefix;
			this.records[this.lastRecord].nsUri = uri;
		}

		// Token: 0x060037CE RID: 14286 RVA: 0x00136E61 File Offset: 0x00135061
		public void InvalidateAllPrefixes()
		{
			if (this.records[this.lastRecord].prefix == null)
			{
				return;
			}
			this.AddRecord(null, null);
		}

		// Token: 0x060037CF RID: 14287 RVA: 0x00136E84 File Offset: 0x00135084
		public void InvalidateNonDefaultPrefixes()
		{
			string text = this.LookupNamespace(string.Empty);
			if (text == null)
			{
				this.InvalidateAllPrefixes();
				return;
			}
			if (this.records[this.lastRecord].prefix.Length == 0 && this.records[this.lastRecord - 1].prefix == null)
			{
				return;
			}
			this.AddRecord(null, null);
			this.AddRecord(string.Empty, text);
		}

		// Token: 0x060037D0 RID: 14288 RVA: 0x00136EF4 File Offset: 0x001350F4
		public string LookupNamespace(string prefix)
		{
			int num = this.lastRecord;
			while (this.records[num].prefix != null)
			{
				if (this.records[num].prefix == prefix)
				{
					return this.records[num].nsUri;
				}
				num--;
			}
			return null;
		}

		// Token: 0x0400244D RID: 9293
		private OutputScopeManager.ScopeReord[] records = new OutputScopeManager.ScopeReord[32];

		// Token: 0x0400244E RID: 9294
		private int lastRecord;

		// Token: 0x0400244F RID: 9295
		private int lastScopes;

		// Token: 0x02000582 RID: 1410
		public struct ScopeReord
		{
			// Token: 0x04002450 RID: 9296
			public int scopeCount;

			// Token: 0x04002451 RID: 9297
			public string prefix;

			// Token: 0x04002452 RID: 9298
			public string nsUri;
		}
	}
}
