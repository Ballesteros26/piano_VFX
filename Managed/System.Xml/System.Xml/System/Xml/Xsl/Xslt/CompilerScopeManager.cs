using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml.Xsl.Qil;

namespace System.Xml.Xsl.Xslt
{
	// Token: 0x0200056F RID: 1391
	internal sealed class CompilerScopeManager<V>
	{
		// Token: 0x06003760 RID: 14176 RVA: 0x00134B78 File Offset: 0x00132D78
		public CompilerScopeManager()
		{
			this.records[0].flags = CompilerScopeManager<V>.ScopeFlags.NsDecl;
			this.records[0].ncName = "xml";
			this.records[0].nsUri = "http://www.w3.org/XML/1998/namespace";
		}

		// Token: 0x06003761 RID: 14177 RVA: 0x00134BD8 File Offset: 0x00132DD8
		public CompilerScopeManager(KeywordsTable atoms)
		{
			this.records[0].flags = CompilerScopeManager<V>.ScopeFlags.NsDecl;
			this.records[0].ncName = atoms.Xml;
			this.records[0].nsUri = atoms.UriXml;
		}

		// Token: 0x06003762 RID: 14178 RVA: 0x00134C39 File Offset: 0x00132E39
		public void EnterScope()
		{
			this.lastScopes++;
		}

		// Token: 0x06003763 RID: 14179 RVA: 0x00134C4C File Offset: 0x00132E4C
		public void ExitScope()
		{
			if (0 < this.lastScopes)
			{
				this.lastScopes--;
				return;
			}
			CompilerScopeManager<V>.ScopeRecord[] array;
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

		// Token: 0x06003764 RID: 14180 RVA: 0x00134CBE File Offset: 0x00132EBE
		[Conditional("DEBUG")]
		public void CheckEmpty()
		{
			this.ExitScope();
		}

		// Token: 0x06003765 RID: 14181 RVA: 0x00134CC8 File Offset: 0x00132EC8
		public bool EnterScope(NsDecl nsDecl)
		{
			this.lastScopes++;
			bool flag = false;
			bool flag2 = false;
			while (nsDecl != null)
			{
				if (nsDecl.NsUri == null)
				{
					flag2 = true;
				}
				else if (nsDecl.Prefix == null)
				{
					this.AddExNamespace(nsDecl.NsUri);
				}
				else
				{
					flag = true;
					this.AddNsDeclaration(nsDecl.Prefix, nsDecl.NsUri);
				}
				nsDecl = nsDecl.Prev;
			}
			if (flag2)
			{
				this.AddExNamespace(null);
			}
			return flag;
		}

		// Token: 0x06003766 RID: 14182 RVA: 0x00134D38 File Offset: 0x00132F38
		private void AddRecord()
		{
			this.records[this.lastRecord].scopeCount = this.lastScopes;
			int num = this.lastRecord + 1;
			this.lastRecord = num;
			if (num == this.records.Length)
			{
				CompilerScopeManager<V>.ScopeRecord[] array = new CompilerScopeManager<V>.ScopeRecord[this.lastRecord * 2];
				Array.Copy(this.records, 0, array, 0, this.lastRecord);
				this.records = array;
			}
			this.lastScopes = 0;
		}

		// Token: 0x06003767 RID: 14183 RVA: 0x00134DAC File Offset: 0x00132FAC
		private void AddRecord(CompilerScopeManager<V>.ScopeFlags flag, string ncName, string uri, V value)
		{
			CompilerScopeManager<V>.ScopeFlags scopeFlags = this.records[this.lastRecord].flags;
			if (this.lastScopes != 0 || (scopeFlags & CompilerScopeManager<V>.ScopeFlags.ExclusiveFlags) != (CompilerScopeManager<V>.ScopeFlags)0)
			{
				this.AddRecord();
				scopeFlags &= CompilerScopeManager<V>.ScopeFlags.InheritedFlags;
			}
			this.records[this.lastRecord].flags = scopeFlags | flag;
			this.records[this.lastRecord].ncName = ncName;
			this.records[this.lastRecord].nsUri = uri;
			this.records[this.lastRecord].value = value;
		}

		// Token: 0x06003768 RID: 14184 RVA: 0x00134E50 File Offset: 0x00133050
		private void SetFlag(CompilerScopeManager<V>.ScopeFlags flag, bool value)
		{
			CompilerScopeManager<V>.ScopeFlags scopeFlags = this.records[this.lastRecord].flags;
			if ((scopeFlags & flag) > (CompilerScopeManager<V>.ScopeFlags)0 != value)
			{
				if (this.lastScopes != 0)
				{
					this.AddRecord();
					scopeFlags &= CompilerScopeManager<V>.ScopeFlags.InheritedFlags;
				}
				if (flag == CompilerScopeManager<V>.ScopeFlags.CanHaveApplyImports)
				{
					scopeFlags ^= flag;
				}
				else
				{
					scopeFlags &= (CompilerScopeManager<V>.ScopeFlags)(-4);
					if (value)
					{
						scopeFlags |= flag;
					}
				}
				this.records[this.lastRecord].flags = scopeFlags;
			}
		}

		// Token: 0x06003769 RID: 14185 RVA: 0x00134EBF File Offset: 0x001330BF
		public void AddVariable(QilName varName, V value)
		{
			this.AddRecord(CompilerScopeManager<V>.ScopeFlags.Variable, varName.LocalName, varName.NamespaceUri, value);
		}

		// Token: 0x0600376A RID: 14186 RVA: 0x00134ED8 File Offset: 0x001330D8
		private string LookupNamespace(string prefix, int from, int to)
		{
			int num = from;
			while (to <= num)
			{
				string text;
				string text2;
				if ((CompilerScopeManager<V>.GetName(ref this.records[num], out text, out text2) & CompilerScopeManager<V>.ScopeFlags.NsDecl) != (CompilerScopeManager<V>.ScopeFlags)0 && text == prefix)
				{
					return text2;
				}
				num--;
			}
			return null;
		}

		// Token: 0x0600376B RID: 14187 RVA: 0x00134F17 File Offset: 0x00133117
		public string LookupNamespace(string prefix)
		{
			return this.LookupNamespace(prefix, this.lastRecord, 0);
		}

		// Token: 0x0600376C RID: 14188 RVA: 0x00134F27 File Offset: 0x00133127
		private static CompilerScopeManager<V>.ScopeFlags GetName(ref CompilerScopeManager<V>.ScopeRecord re, out string prefix, out string nsUri)
		{
			prefix = re.ncName;
			nsUri = re.nsUri;
			return re.flags;
		}

		// Token: 0x0600376D RID: 14189 RVA: 0x00134F40 File Offset: 0x00133140
		public void AddNsDeclaration(string prefix, string nsUri)
		{
			this.AddRecord(CompilerScopeManager<V>.ScopeFlags.NsDecl, prefix, nsUri, default(V));
		}

		// Token: 0x0600376E RID: 14190 RVA: 0x00134F60 File Offset: 0x00133160
		public void AddExNamespace(string nsUri)
		{
			this.AddRecord(CompilerScopeManager<V>.ScopeFlags.NsExcl, null, nsUri, default(V));
		}

		// Token: 0x0600376F RID: 14191 RVA: 0x00134F80 File Offset: 0x00133180
		public bool IsExNamespace(string nsUri)
		{
			int num = 0;
			int num2 = this.lastRecord;
			while (0 <= num2)
			{
				string text;
				string text2;
				CompilerScopeManager<V>.ScopeFlags name = CompilerScopeManager<V>.GetName(ref this.records[num2], out text, out text2);
				if ((name & CompilerScopeManager<V>.ScopeFlags.NsExcl) != (CompilerScopeManager<V>.ScopeFlags)0)
				{
					if (text2 == nsUri)
					{
						return true;
					}
					if (text2 == null)
					{
						num = num2;
					}
				}
				else if (num != 0 && (name & CompilerScopeManager<V>.ScopeFlags.NsDecl) != (CompilerScopeManager<V>.ScopeFlags)0 && text2 == nsUri)
				{
					bool flag = false;
					for (int i = num2 + 1; i < num; i++)
					{
						string text3;
						string text4;
						CompilerScopeManager<V>.GetName(ref this.records[i], out text3, out text4);
						if ((name & CompilerScopeManager<V>.ScopeFlags.NsDecl) != (CompilerScopeManager<V>.ScopeFlags)0 && text3 == text)
						{
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						return true;
					}
				}
				num2--;
			}
			return false;
		}

		// Token: 0x06003770 RID: 14192 RVA: 0x00135034 File Offset: 0x00133234
		private int SearchVariable(string localName, string uri)
		{
			int num = this.lastRecord;
			while (0 <= num)
			{
				string text;
				string text2;
				if ((CompilerScopeManager<V>.GetName(ref this.records[num], out text, out text2) & CompilerScopeManager<V>.ScopeFlags.Variable) != (CompilerScopeManager<V>.ScopeFlags)0 && text == localName && text2 == uri)
				{
					return num;
				}
				num--;
			}
			return -1;
		}

		// Token: 0x06003771 RID: 14193 RVA: 0x00135084 File Offset: 0x00133284
		public V LookupVariable(string localName, string uri)
		{
			int num = this.SearchVariable(localName, uri);
			if (num >= 0)
			{
				return this.records[num].value;
			}
			return default(V);
		}

		// Token: 0x06003772 RID: 14194 RVA: 0x001350BC File Offset: 0x001332BC
		public bool IsLocalVariable(string localName, string uri)
		{
			int num = this.SearchVariable(localName, uri);
			while (0 <= --num)
			{
				if (this.records[num].scopeCount != 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x17000BAE RID: 2990
		// (get) Token: 0x06003773 RID: 14195 RVA: 0x001350F2 File Offset: 0x001332F2
		// (set) Token: 0x06003774 RID: 14196 RVA: 0x0013510F File Offset: 0x0013330F
		public bool ForwardCompatibility
		{
			get
			{
				return (this.records[this.lastRecord].flags & CompilerScopeManager<V>.ScopeFlags.ForwardCompatibility) > (CompilerScopeManager<V>.ScopeFlags)0;
			}
			set
			{
				this.SetFlag(CompilerScopeManager<V>.ScopeFlags.ForwardCompatibility, value);
			}
		}

		// Token: 0x17000BAF RID: 2991
		// (get) Token: 0x06003775 RID: 14197 RVA: 0x00135119 File Offset: 0x00133319
		// (set) Token: 0x06003776 RID: 14198 RVA: 0x00135136 File Offset: 0x00133336
		public bool BackwardCompatibility
		{
			get
			{
				return (this.records[this.lastRecord].flags & CompilerScopeManager<V>.ScopeFlags.BackwardCompatibility) > (CompilerScopeManager<V>.ScopeFlags)0;
			}
			set
			{
				this.SetFlag(CompilerScopeManager<V>.ScopeFlags.BackwardCompatibility, value);
			}
		}

		// Token: 0x17000BB0 RID: 2992
		// (get) Token: 0x06003777 RID: 14199 RVA: 0x00135140 File Offset: 0x00133340
		// (set) Token: 0x06003778 RID: 14200 RVA: 0x0013515D File Offset: 0x0013335D
		public bool CanHaveApplyImports
		{
			get
			{
				return (this.records[this.lastRecord].flags & CompilerScopeManager<V>.ScopeFlags.CanHaveApplyImports) > (CompilerScopeManager<V>.ScopeFlags)0;
			}
			set
			{
				this.SetFlag(CompilerScopeManager<V>.ScopeFlags.CanHaveApplyImports, value);
			}
		}

		// Token: 0x06003779 RID: 14201 RVA: 0x00135167 File Offset: 0x00133367
		internal IEnumerable<CompilerScopeManager<V>.ScopeRecord> GetActiveRecords()
		{
			int currentRecord = this.lastRecord + 1;
			for (;;)
			{
				int num = 0;
				int num2 = currentRecord - 1;
				currentRecord = num2;
				if (num >= num2)
				{
					break;
				}
				if (!this.records[currentRecord].IsNamespace || this.LookupNamespace(this.records[currentRecord].ncName, this.lastRecord, currentRecord + 1) == null)
				{
					yield return this.records[currentRecord];
				}
			}
			yield break;
		}

		// Token: 0x0600377A RID: 14202 RVA: 0x00135177 File Offset: 0x00133377
		public CompilerScopeManager<V>.NamespaceEnumerator GetEnumerator()
		{
			return new CompilerScopeManager<V>.NamespaceEnumerator(this);
		}

		// Token: 0x04002389 RID: 9097
		private const int LastPredefRecord = 0;

		// Token: 0x0400238A RID: 9098
		private CompilerScopeManager<V>.ScopeRecord[] records = new CompilerScopeManager<V>.ScopeRecord[32];

		// Token: 0x0400238B RID: 9099
		private int lastRecord;

		// Token: 0x0400238C RID: 9100
		private int lastScopes;

		// Token: 0x02000570 RID: 1392
		public enum ScopeFlags
		{
			// Token: 0x0400238E RID: 9102
			BackwardCompatibility = 1,
			// Token: 0x0400238F RID: 9103
			ForwardCompatibility,
			// Token: 0x04002390 RID: 9104
			CanHaveApplyImports = 4,
			// Token: 0x04002391 RID: 9105
			NsDecl = 16,
			// Token: 0x04002392 RID: 9106
			NsExcl = 32,
			// Token: 0x04002393 RID: 9107
			Variable = 64,
			// Token: 0x04002394 RID: 9108
			CompatibilityFlags = 3,
			// Token: 0x04002395 RID: 9109
			InheritedFlags = 7,
			// Token: 0x04002396 RID: 9110
			ExclusiveFlags = 112
		}

		// Token: 0x02000571 RID: 1393
		public struct ScopeRecord
		{
			// Token: 0x17000BB1 RID: 2993
			// (get) Token: 0x0600377B RID: 14203 RVA: 0x0013517F File Offset: 0x0013337F
			public bool IsVariable
			{
				get
				{
					return (this.flags & CompilerScopeManager<V>.ScopeFlags.Variable) > (CompilerScopeManager<V>.ScopeFlags)0;
				}
			}

			// Token: 0x17000BB2 RID: 2994
			// (get) Token: 0x0600377C RID: 14204 RVA: 0x0013518D File Offset: 0x0013338D
			public bool IsNamespace
			{
				get
				{
					return (this.flags & CompilerScopeManager<V>.ScopeFlags.NsDecl) > (CompilerScopeManager<V>.ScopeFlags)0;
				}
			}

			// Token: 0x04002397 RID: 9111
			public int scopeCount;

			// Token: 0x04002398 RID: 9112
			public CompilerScopeManager<V>.ScopeFlags flags;

			// Token: 0x04002399 RID: 9113
			public string ncName;

			// Token: 0x0400239A RID: 9114
			public string nsUri;

			// Token: 0x0400239B RID: 9115
			public V value;
		}

		// Token: 0x02000572 RID: 1394
		internal struct NamespaceEnumerator
		{
			// Token: 0x0600377D RID: 14205 RVA: 0x0013519B File Offset: 0x0013339B
			public NamespaceEnumerator(CompilerScopeManager<V> scope)
			{
				this.scope = scope;
				this.lastRecord = scope.lastRecord;
				this.currentRecord = this.lastRecord + 1;
			}

			// Token: 0x0600377E RID: 14206 RVA: 0x001351BE File Offset: 0x001333BE
			public void Reset()
			{
				this.currentRecord = this.lastRecord + 1;
			}

			// Token: 0x0600377F RID: 14207 RVA: 0x001351D0 File Offset: 0x001333D0
			public bool MoveNext()
			{
				do
				{
					int num = 0;
					int num2 = this.currentRecord - 1;
					this.currentRecord = num2;
					if (num >= num2)
					{
						return false;
					}
				}
				while (!this.scope.records[this.currentRecord].IsNamespace || this.scope.LookupNamespace(this.scope.records[this.currentRecord].ncName, this.lastRecord, this.currentRecord + 1) != null);
				return true;
			}

			// Token: 0x17000BB3 RID: 2995
			// (get) Token: 0x06003780 RID: 14208 RVA: 0x00135249 File Offset: 0x00133449
			public CompilerScopeManager<V>.ScopeRecord Current
			{
				get
				{
					return this.scope.records[this.currentRecord];
				}
			}

			// Token: 0x0400239C RID: 9116
			private CompilerScopeManager<V> scope;

			// Token: 0x0400239D RID: 9117
			private int lastRecord;

			// Token: 0x0400239E RID: 9118
			private int currentRecord;
		}
	}
}
