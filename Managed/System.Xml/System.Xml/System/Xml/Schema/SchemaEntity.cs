using System;

namespace System.Xml.Schema
{
	// Token: 0x02000417 RID: 1047
	internal sealed class SchemaEntity : IDtdEntityInfo
	{
		// Token: 0x06002916 RID: 10518 RVA: 0x000F8F90 File Offset: 0x000F7190
		internal SchemaEntity(XmlQualifiedName qname, bool isParameter)
		{
			this.qname = qname;
			this.isParameter = isParameter;
		}

		// Token: 0x17000896 RID: 2198
		// (get) Token: 0x06002917 RID: 10519 RVA: 0x000F8FB1 File Offset: 0x000F71B1
		string IDtdEntityInfo.Name
		{
			get
			{
				return this.Name.Name;
			}
		}

		// Token: 0x17000897 RID: 2199
		// (get) Token: 0x06002918 RID: 10520 RVA: 0x000F8FBE File Offset: 0x000F71BE
		bool IDtdEntityInfo.IsExternal
		{
			get
			{
				return this.IsExternal;
			}
		}

		// Token: 0x17000898 RID: 2200
		// (get) Token: 0x06002919 RID: 10521 RVA: 0x000F8FC6 File Offset: 0x000F71C6
		bool IDtdEntityInfo.IsDeclaredInExternal
		{
			get
			{
				return this.DeclaredInExternal;
			}
		}

		// Token: 0x17000899 RID: 2201
		// (get) Token: 0x0600291A RID: 10522 RVA: 0x000F8FCE File Offset: 0x000F71CE
		bool IDtdEntityInfo.IsUnparsedEntity
		{
			get
			{
				return !this.NData.IsEmpty;
			}
		}

		// Token: 0x1700089A RID: 2202
		// (get) Token: 0x0600291B RID: 10523 RVA: 0x000F8FDE File Offset: 0x000F71DE
		bool IDtdEntityInfo.IsParameterEntity
		{
			get
			{
				return this.isParameter;
			}
		}

		// Token: 0x1700089B RID: 2203
		// (get) Token: 0x0600291C RID: 10524 RVA: 0x000F8FE6 File Offset: 0x000F71E6
		string IDtdEntityInfo.BaseUriString
		{
			get
			{
				return this.BaseURI;
			}
		}

		// Token: 0x1700089C RID: 2204
		// (get) Token: 0x0600291D RID: 10525 RVA: 0x000F8FEE File Offset: 0x000F71EE
		string IDtdEntityInfo.DeclaredUriString
		{
			get
			{
				return this.DeclaredURI;
			}
		}

		// Token: 0x1700089D RID: 2205
		// (get) Token: 0x0600291E RID: 10526 RVA: 0x000F8FF6 File Offset: 0x000F71F6
		string IDtdEntityInfo.SystemId
		{
			get
			{
				return this.Url;
			}
		}

		// Token: 0x1700089E RID: 2206
		// (get) Token: 0x0600291F RID: 10527 RVA: 0x000F8FFE File Offset: 0x000F71FE
		string IDtdEntityInfo.PublicId
		{
			get
			{
				return this.Pubid;
			}
		}

		// Token: 0x1700089F RID: 2207
		// (get) Token: 0x06002920 RID: 10528 RVA: 0x000F9006 File Offset: 0x000F7206
		string IDtdEntityInfo.Text
		{
			get
			{
				return this.Text;
			}
		}

		// Token: 0x170008A0 RID: 2208
		// (get) Token: 0x06002921 RID: 10529 RVA: 0x000F900E File Offset: 0x000F720E
		int IDtdEntityInfo.LineNumber
		{
			get
			{
				return this.Line;
			}
		}

		// Token: 0x170008A1 RID: 2209
		// (get) Token: 0x06002922 RID: 10530 RVA: 0x000F9016 File Offset: 0x000F7216
		int IDtdEntityInfo.LinePosition
		{
			get
			{
				return this.Pos;
			}
		}

		// Token: 0x06002923 RID: 10531 RVA: 0x000F9020 File Offset: 0x000F7220
		internal static bool IsPredefinedEntity(string n)
		{
			return n == "lt" || n == "gt" || n == "amp" || n == "apos" || n == "quot";
		}

		// Token: 0x170008A2 RID: 2210
		// (get) Token: 0x06002924 RID: 10532 RVA: 0x000F906E File Offset: 0x000F726E
		internal XmlQualifiedName Name
		{
			get
			{
				return this.qname;
			}
		}

		// Token: 0x170008A3 RID: 2211
		// (get) Token: 0x06002925 RID: 10533 RVA: 0x000F9076 File Offset: 0x000F7276
		// (set) Token: 0x06002926 RID: 10534 RVA: 0x000F907E File Offset: 0x000F727E
		internal string Url
		{
			get
			{
				return this.url;
			}
			set
			{
				this.url = value;
				this.isExternal = true;
			}
		}

		// Token: 0x170008A4 RID: 2212
		// (get) Token: 0x06002927 RID: 10535 RVA: 0x000F908E File Offset: 0x000F728E
		// (set) Token: 0x06002928 RID: 10536 RVA: 0x000F9096 File Offset: 0x000F7296
		internal string Pubid
		{
			get
			{
				return this.pubid;
			}
			set
			{
				this.pubid = value;
			}
		}

		// Token: 0x170008A5 RID: 2213
		// (get) Token: 0x06002929 RID: 10537 RVA: 0x000F909F File Offset: 0x000F729F
		// (set) Token: 0x0600292A RID: 10538 RVA: 0x000F90A7 File Offset: 0x000F72A7
		internal bool IsExternal
		{
			get
			{
				return this.isExternal;
			}
			set
			{
				this.isExternal = value;
			}
		}

		// Token: 0x170008A6 RID: 2214
		// (get) Token: 0x0600292B RID: 10539 RVA: 0x000F90B0 File Offset: 0x000F72B0
		// (set) Token: 0x0600292C RID: 10540 RVA: 0x000F90B8 File Offset: 0x000F72B8
		internal bool DeclaredInExternal
		{
			get
			{
				return this.isDeclaredInExternal;
			}
			set
			{
				this.isDeclaredInExternal = value;
			}
		}

		// Token: 0x170008A7 RID: 2215
		// (get) Token: 0x0600292D RID: 10541 RVA: 0x000F90C1 File Offset: 0x000F72C1
		// (set) Token: 0x0600292E RID: 10542 RVA: 0x000F90C9 File Offset: 0x000F72C9
		internal XmlQualifiedName NData
		{
			get
			{
				return this.ndata;
			}
			set
			{
				this.ndata = value;
			}
		}

		// Token: 0x170008A8 RID: 2216
		// (get) Token: 0x0600292F RID: 10543 RVA: 0x000F90D2 File Offset: 0x000F72D2
		// (set) Token: 0x06002930 RID: 10544 RVA: 0x000F90DA File Offset: 0x000F72DA
		internal string Text
		{
			get
			{
				return this.text;
			}
			set
			{
				this.text = value;
				this.isExternal = false;
			}
		}

		// Token: 0x170008A9 RID: 2217
		// (get) Token: 0x06002931 RID: 10545 RVA: 0x000F90EA File Offset: 0x000F72EA
		// (set) Token: 0x06002932 RID: 10546 RVA: 0x000F90F2 File Offset: 0x000F72F2
		internal int Line
		{
			get
			{
				return this.lineNumber;
			}
			set
			{
				this.lineNumber = value;
			}
		}

		// Token: 0x170008AA RID: 2218
		// (get) Token: 0x06002933 RID: 10547 RVA: 0x000F90FB File Offset: 0x000F72FB
		// (set) Token: 0x06002934 RID: 10548 RVA: 0x000F9103 File Offset: 0x000F7303
		internal int Pos
		{
			get
			{
				return this.linePosition;
			}
			set
			{
				this.linePosition = value;
			}
		}

		// Token: 0x170008AB RID: 2219
		// (get) Token: 0x06002935 RID: 10549 RVA: 0x000F910C File Offset: 0x000F730C
		// (set) Token: 0x06002936 RID: 10550 RVA: 0x000F9122 File Offset: 0x000F7322
		internal string BaseURI
		{
			get
			{
				if (this.baseURI != null)
				{
					return this.baseURI;
				}
				return string.Empty;
			}
			set
			{
				this.baseURI = value;
			}
		}

		// Token: 0x170008AC RID: 2220
		// (get) Token: 0x06002937 RID: 10551 RVA: 0x000F912B File Offset: 0x000F732B
		// (set) Token: 0x06002938 RID: 10552 RVA: 0x000F9133 File Offset: 0x000F7333
		internal bool ParsingInProgress
		{
			get
			{
				return this.parsingInProgress;
			}
			set
			{
				this.parsingInProgress = value;
			}
		}

		// Token: 0x170008AD RID: 2221
		// (get) Token: 0x06002939 RID: 10553 RVA: 0x000F913C File Offset: 0x000F733C
		// (set) Token: 0x0600293A RID: 10554 RVA: 0x000F9152 File Offset: 0x000F7352
		internal string DeclaredURI
		{
			get
			{
				if (this.declaredURI != null)
				{
					return this.declaredURI;
				}
				return string.Empty;
			}
			set
			{
				this.declaredURI = value;
			}
		}

		// Token: 0x04001B12 RID: 6930
		private XmlQualifiedName qname;

		// Token: 0x04001B13 RID: 6931
		private string url;

		// Token: 0x04001B14 RID: 6932
		private string pubid;

		// Token: 0x04001B15 RID: 6933
		private string text;

		// Token: 0x04001B16 RID: 6934
		private XmlQualifiedName ndata = XmlQualifiedName.Empty;

		// Token: 0x04001B17 RID: 6935
		private int lineNumber;

		// Token: 0x04001B18 RID: 6936
		private int linePosition;

		// Token: 0x04001B19 RID: 6937
		private bool isParameter;

		// Token: 0x04001B1A RID: 6938
		private bool isExternal;

		// Token: 0x04001B1B RID: 6939
		private bool parsingInProgress;

		// Token: 0x04001B1C RID: 6940
		private bool isDeclaredInExternal;

		// Token: 0x04001B1D RID: 6941
		private string baseURI;

		// Token: 0x04001B1E RID: 6942
		private string declaredURI;
	}
}
