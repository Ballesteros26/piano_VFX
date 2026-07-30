using System;
using System.Collections.Generic;
using System.Xml.Xsl.Qil;
using System.Xml.Xsl.Runtime;

namespace System.Xml.Xsl.IlGen
{
	// Token: 0x02000663 RID: 1635
	internal class StaticDataManager
	{
		// Token: 0x060041D1 RID: 16849 RVA: 0x0015FC2B File Offset: 0x0015DE2B
		public int DeclareName(string name)
		{
			if (this.uniqueNames == null)
			{
				this.uniqueNames = new UniqueList<string>();
			}
			return this.uniqueNames.Add(name);
		}

		// Token: 0x17000CD0 RID: 3280
		// (get) Token: 0x060041D2 RID: 16850 RVA: 0x0015FC4C File Offset: 0x0015DE4C
		public string[] Names
		{
			get
			{
				if (this.uniqueNames == null)
				{
					return null;
				}
				return this.uniqueNames.ToArray();
			}
		}

		// Token: 0x060041D3 RID: 16851 RVA: 0x0015FC63 File Offset: 0x0015DE63
		public int DeclareNameFilter(string locName, string nsUri)
		{
			if (this.uniqueFilters == null)
			{
				this.uniqueFilters = new UniqueList<Int32Pair>();
			}
			return this.uniqueFilters.Add(new Int32Pair(this.DeclareName(locName), this.DeclareName(nsUri)));
		}

		// Token: 0x17000CD1 RID: 3281
		// (get) Token: 0x060041D4 RID: 16852 RVA: 0x0015FC96 File Offset: 0x0015DE96
		public Int32Pair[] NameFilters
		{
			get
			{
				if (this.uniqueFilters == null)
				{
					return null;
				}
				return this.uniqueFilters.ToArray();
			}
		}

		// Token: 0x060041D5 RID: 16853 RVA: 0x0015FCB0 File Offset: 0x0015DEB0
		public int DeclarePrefixMappings(IList<QilNode> list)
		{
			StringPair[] array = new StringPair[list.Count];
			for (int i = 0; i < list.Count; i++)
			{
				QilBinary qilBinary = (QilBinary)list[i];
				array[i] = new StringPair((QilLiteral)qilBinary.Left, (QilLiteral)qilBinary.Right);
			}
			if (this.prefixMappingsList == null)
			{
				this.prefixMappingsList = new List<StringPair[]>();
			}
			this.prefixMappingsList.Add(array);
			return this.prefixMappingsList.Count - 1;
		}

		// Token: 0x17000CD2 RID: 3282
		// (get) Token: 0x060041D6 RID: 16854 RVA: 0x0015FD3F File Offset: 0x0015DF3F
		public StringPair[][] PrefixMappingsList
		{
			get
			{
				if (this.prefixMappingsList == null)
				{
					return null;
				}
				return this.prefixMappingsList.ToArray();
			}
		}

		// Token: 0x060041D7 RID: 16855 RVA: 0x0015FD56 File Offset: 0x0015DF56
		public int DeclareGlobalValue(string name)
		{
			if (this.globalNames == null)
			{
				this.globalNames = new List<string>();
			}
			int count = this.globalNames.Count;
			this.globalNames.Add(name);
			return count;
		}

		// Token: 0x17000CD3 RID: 3283
		// (get) Token: 0x060041D8 RID: 16856 RVA: 0x0015FD82 File Offset: 0x0015DF82
		public string[] GlobalNames
		{
			get
			{
				if (this.globalNames == null)
				{
					return null;
				}
				return this.globalNames.ToArray();
			}
		}

		// Token: 0x060041D9 RID: 16857 RVA: 0x0015FD99 File Offset: 0x0015DF99
		public int DeclareEarlyBound(string namespaceUri, Type ebType)
		{
			if (this.earlyInfo == null)
			{
				this.earlyInfo = new UniqueList<EarlyBoundInfo>();
			}
			return this.earlyInfo.Add(new EarlyBoundInfo(namespaceUri, ebType));
		}

		// Token: 0x17000CD4 RID: 3284
		// (get) Token: 0x060041DA RID: 16858 RVA: 0x0015FDC0 File Offset: 0x0015DFC0
		public EarlyBoundInfo[] EarlyBound
		{
			get
			{
				if (this.earlyInfo != null)
				{
					return this.earlyInfo.ToArray();
				}
				return null;
			}
		}

		// Token: 0x060041DB RID: 16859 RVA: 0x0015FDD7 File Offset: 0x0015DFD7
		public int DeclareXmlType(XmlQueryType type)
		{
			if (this.uniqueXmlTypes == null)
			{
				this.uniqueXmlTypes = new UniqueList<XmlQueryType>();
			}
			return this.uniqueXmlTypes.Add(type);
		}

		// Token: 0x17000CD5 RID: 3285
		// (get) Token: 0x060041DC RID: 16860 RVA: 0x0015FDF8 File Offset: 0x0015DFF8
		public XmlQueryType[] XmlTypes
		{
			get
			{
				if (this.uniqueXmlTypes == null)
				{
					return null;
				}
				return this.uniqueXmlTypes.ToArray();
			}
		}

		// Token: 0x060041DD RID: 16861 RVA: 0x0015FE0F File Offset: 0x0015E00F
		public int DeclareCollation(string collation)
		{
			if (this.uniqueCollations == null)
			{
				this.uniqueCollations = new UniqueList<XmlCollation>();
			}
			return this.uniqueCollations.Add(XmlCollation.Create(collation));
		}

		// Token: 0x17000CD6 RID: 3286
		// (get) Token: 0x060041DE RID: 16862 RVA: 0x0015FE35 File Offset: 0x0015E035
		public XmlCollation[] Collations
		{
			get
			{
				if (this.uniqueCollations == null)
				{
					return null;
				}
				return this.uniqueCollations.ToArray();
			}
		}

		// Token: 0x04002A38 RID: 10808
		private UniqueList<string> uniqueNames;

		// Token: 0x04002A39 RID: 10809
		private UniqueList<Int32Pair> uniqueFilters;

		// Token: 0x04002A3A RID: 10810
		private List<StringPair[]> prefixMappingsList;

		// Token: 0x04002A3B RID: 10811
		private List<string> globalNames;

		// Token: 0x04002A3C RID: 10812
		private UniqueList<EarlyBoundInfo> earlyInfo;

		// Token: 0x04002A3D RID: 10813
		private UniqueList<XmlQueryType> uniqueXmlTypes;

		// Token: 0x04002A3E RID: 10814
		private UniqueList<XmlCollation> uniqueCollations;
	}
}
