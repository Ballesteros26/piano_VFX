using System;
using System.Reflection;
using System.Xml.Xsl.Qil;

namespace System.Xml.Xsl.IlGen
{
	// Token: 0x02000665 RID: 1637
	internal class XmlILAnnotation : ListBase<object>
	{
		// Token: 0x060041E2 RID: 16866 RVA: 0x0015FFD8 File Offset: 0x0015E1D8
		public static XmlILAnnotation Write(QilNode nd)
		{
			XmlILAnnotation xmlILAnnotation = nd.Annotation as XmlILAnnotation;
			if (xmlILAnnotation == null)
			{
				xmlILAnnotation = new XmlILAnnotation(nd.Annotation);
				nd.Annotation = xmlILAnnotation;
			}
			return xmlILAnnotation;
		}

		// Token: 0x060041E3 RID: 16867 RVA: 0x00160008 File Offset: 0x0015E208
		private XmlILAnnotation(object annPrev)
		{
			this.annPrev = annPrev;
		}

		// Token: 0x17000CD7 RID: 3287
		// (get) Token: 0x060041E4 RID: 16868 RVA: 0x00160017 File Offset: 0x0015E217
		// (set) Token: 0x060041E5 RID: 16869 RVA: 0x0016001F File Offset: 0x0015E21F
		public MethodInfo FunctionBinding
		{
			get
			{
				return this.funcMethod;
			}
			set
			{
				this.funcMethod = value;
			}
		}

		// Token: 0x17000CD8 RID: 3288
		// (get) Token: 0x060041E6 RID: 16870 RVA: 0x00160028 File Offset: 0x0015E228
		// (set) Token: 0x060041E7 RID: 16871 RVA: 0x00160030 File Offset: 0x0015E230
		public int ArgumentPosition
		{
			get
			{
				return this.argPos;
			}
			set
			{
				this.argPos = value;
			}
		}

		// Token: 0x17000CD9 RID: 3289
		// (get) Token: 0x060041E8 RID: 16872 RVA: 0x00160039 File Offset: 0x0015E239
		// (set) Token: 0x060041E9 RID: 16873 RVA: 0x00160041 File Offset: 0x0015E241
		public IteratorDescriptor CachedIteratorDescriptor
		{
			get
			{
				return this.iterInfo;
			}
			set
			{
				this.iterInfo = value;
			}
		}

		// Token: 0x17000CDA RID: 3290
		// (get) Token: 0x060041EA RID: 16874 RVA: 0x0016004A File Offset: 0x0015E24A
		// (set) Token: 0x060041EB RID: 16875 RVA: 0x00160052 File Offset: 0x0015E252
		public XmlILConstructInfo ConstructInfo
		{
			get
			{
				return this.constrInfo;
			}
			set
			{
				this.constrInfo = value;
			}
		}

		// Token: 0x17000CDB RID: 3291
		// (get) Token: 0x060041EC RID: 16876 RVA: 0x0016005B File Offset: 0x0015E25B
		// (set) Token: 0x060041ED RID: 16877 RVA: 0x00160063 File Offset: 0x0015E263
		public OptimizerPatterns Patterns
		{
			get
			{
				return this.optPatt;
			}
			set
			{
				this.optPatt = value;
			}
		}

		// Token: 0x17000CDC RID: 3292
		// (get) Token: 0x060041EE RID: 16878 RVA: 0x0016006C File Offset: 0x0015E26C
		public override int Count
		{
			get
			{
				if (this.annPrev == null)
				{
					return 2;
				}
				return 3;
			}
		}

		// Token: 0x17000CDD RID: 3293
		public override object this[int index]
		{
			get
			{
				if (this.annPrev != null)
				{
					if (index == 0)
					{
						return this.annPrev;
					}
					index--;
				}
				if (index == 0)
				{
					return this.constrInfo;
				}
				if (index != 1)
				{
					throw new IndexOutOfRangeException();
				}
				return this.optPatt;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x04002A3F RID: 10815
		private object annPrev;

		// Token: 0x04002A40 RID: 10816
		private MethodInfo funcMethod;

		// Token: 0x04002A41 RID: 10817
		private int argPos;

		// Token: 0x04002A42 RID: 10818
		private IteratorDescriptor iterInfo;

		// Token: 0x04002A43 RID: 10819
		private XmlILConstructInfo constrInfo;

		// Token: 0x04002A44 RID: 10820
		private OptimizerPatterns optPatt;
	}
}
