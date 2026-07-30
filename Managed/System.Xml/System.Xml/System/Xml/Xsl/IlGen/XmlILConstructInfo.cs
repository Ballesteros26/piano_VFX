using System;
using System.Collections;
using System.Xml.Xsl.Qil;

namespace System.Xml.Xsl.IlGen
{
	// Token: 0x02000668 RID: 1640
	internal class XmlILConstructInfo : IQilAnnotation
	{
		// Token: 0x060041F1 RID: 16881 RVA: 0x001600B0 File Offset: 0x0015E2B0
		public static XmlILConstructInfo Read(QilNode nd)
		{
			XmlILAnnotation xmlILAnnotation = nd.Annotation as XmlILAnnotation;
			XmlILConstructInfo xmlILConstructInfo = ((xmlILAnnotation != null) ? xmlILAnnotation.ConstructInfo : null);
			if (xmlILConstructInfo == null)
			{
				if (XmlILConstructInfo.Default == null)
				{
					xmlILConstructInfo = new XmlILConstructInfo(QilNodeType.Unknown);
					xmlILConstructInfo.isReadOnly = true;
					XmlILConstructInfo.Default = xmlILConstructInfo;
				}
				else
				{
					xmlILConstructInfo = XmlILConstructInfo.Default;
				}
			}
			return xmlILConstructInfo;
		}

		// Token: 0x060041F2 RID: 16882 RVA: 0x00160104 File Offset: 0x0015E304
		public static XmlILConstructInfo Write(QilNode nd)
		{
			XmlILAnnotation xmlILAnnotation = XmlILAnnotation.Write(nd);
			XmlILConstructInfo xmlILConstructInfo = xmlILAnnotation.ConstructInfo;
			if (xmlILConstructInfo == null || xmlILConstructInfo.isReadOnly)
			{
				xmlILConstructInfo = new XmlILConstructInfo(nd.NodeType);
				xmlILAnnotation.ConstructInfo = xmlILConstructInfo;
			}
			return xmlILConstructInfo;
		}

		// Token: 0x060041F3 RID: 16883 RVA: 0x00160140 File Offset: 0x0015E340
		private XmlILConstructInfo(QilNodeType nodeType)
		{
			this.nodeType = nodeType;
			this.xstatesInitial = (this.xstatesFinal = PossibleXmlStates.Any);
			this.xstatesBeginLoop = (this.xstatesEndLoop = PossibleXmlStates.None);
			this.isNmspInScope = false;
			this.mightHaveNmsp = true;
			this.mightHaveAttrs = true;
			this.mightHaveDupAttrs = true;
			this.mightHaveNmspAfterAttrs = true;
			this.constrMeth = XmlILConstructMethod.Iterator;
			this.parentInfo = null;
		}

		// Token: 0x17000CDE RID: 3294
		// (get) Token: 0x060041F4 RID: 16884 RVA: 0x001601AB File Offset: 0x0015E3AB
		// (set) Token: 0x060041F5 RID: 16885 RVA: 0x001601B3 File Offset: 0x0015E3B3
		public PossibleXmlStates InitialStates
		{
			get
			{
				return this.xstatesInitial;
			}
			set
			{
				this.xstatesInitial = value;
			}
		}

		// Token: 0x17000CDF RID: 3295
		// (get) Token: 0x060041F6 RID: 16886 RVA: 0x001601BC File Offset: 0x0015E3BC
		// (set) Token: 0x060041F7 RID: 16887 RVA: 0x001601C4 File Offset: 0x0015E3C4
		public PossibleXmlStates FinalStates
		{
			get
			{
				return this.xstatesFinal;
			}
			set
			{
				this.xstatesFinal = value;
			}
		}

		// Token: 0x17000CE0 RID: 3296
		// (set) Token: 0x060041F8 RID: 16888 RVA: 0x001601CD File Offset: 0x0015E3CD
		public PossibleXmlStates BeginLoopStates
		{
			set
			{
				this.xstatesBeginLoop = value;
			}
		}

		// Token: 0x17000CE1 RID: 3297
		// (set) Token: 0x060041F9 RID: 16889 RVA: 0x001601D6 File Offset: 0x0015E3D6
		public PossibleXmlStates EndLoopStates
		{
			set
			{
				this.xstatesEndLoop = value;
			}
		}

		// Token: 0x17000CE2 RID: 3298
		// (get) Token: 0x060041FA RID: 16890 RVA: 0x001601DF File Offset: 0x0015E3DF
		// (set) Token: 0x060041FB RID: 16891 RVA: 0x001601E7 File Offset: 0x0015E3E7
		public XmlILConstructMethod ConstructMethod
		{
			get
			{
				return this.constrMeth;
			}
			set
			{
				this.constrMeth = value;
			}
		}

		// Token: 0x17000CE3 RID: 3299
		// (get) Token: 0x060041FC RID: 16892 RVA: 0x001601F0 File Offset: 0x0015E3F0
		// (set) Token: 0x060041FD RID: 16893 RVA: 0x00160208 File Offset: 0x0015E408
		public bool PushToWriterFirst
		{
			get
			{
				return this.constrMeth == XmlILConstructMethod.Writer || this.constrMeth == XmlILConstructMethod.WriterThenIterator;
			}
			set
			{
				XmlILConstructMethod xmlILConstructMethod = this.constrMeth;
				if (xmlILConstructMethod == XmlILConstructMethod.Iterator)
				{
					this.constrMeth = XmlILConstructMethod.WriterThenIterator;
					return;
				}
				if (xmlILConstructMethod != XmlILConstructMethod.IteratorThenWriter)
				{
					return;
				}
				this.constrMeth = XmlILConstructMethod.Writer;
			}
		}

		// Token: 0x17000CE4 RID: 3300
		// (get) Token: 0x060041FE RID: 16894 RVA: 0x00160233 File Offset: 0x0015E433
		// (set) Token: 0x060041FF RID: 16895 RVA: 0x0016024C File Offset: 0x0015E44C
		public bool PushToWriterLast
		{
			get
			{
				return this.constrMeth == XmlILConstructMethod.Writer || this.constrMeth == XmlILConstructMethod.IteratorThenWriter;
			}
			set
			{
				XmlILConstructMethod xmlILConstructMethod = this.constrMeth;
				if (xmlILConstructMethod == XmlILConstructMethod.Iterator)
				{
					this.constrMeth = XmlILConstructMethod.IteratorThenWriter;
					return;
				}
				if (xmlILConstructMethod != XmlILConstructMethod.WriterThenIterator)
				{
					return;
				}
				this.constrMeth = XmlILConstructMethod.Writer;
			}
		}

		// Token: 0x17000CE5 RID: 3301
		// (get) Token: 0x06004200 RID: 16896 RVA: 0x00160277 File Offset: 0x0015E477
		// (set) Token: 0x06004201 RID: 16897 RVA: 0x00160290 File Offset: 0x0015E490
		public bool PullFromIteratorFirst
		{
			get
			{
				return this.constrMeth == XmlILConstructMethod.IteratorThenWriter || this.constrMeth == XmlILConstructMethod.Iterator;
			}
			set
			{
				XmlILConstructMethod xmlILConstructMethod = this.constrMeth;
				if (xmlILConstructMethod == XmlILConstructMethod.Writer)
				{
					this.constrMeth = XmlILConstructMethod.IteratorThenWriter;
					return;
				}
				if (xmlILConstructMethod != XmlILConstructMethod.WriterThenIterator)
				{
					return;
				}
				this.constrMeth = XmlILConstructMethod.Iterator;
			}
		}

		// Token: 0x17000CE6 RID: 3302
		// (set) Token: 0x06004202 RID: 16898 RVA: 0x001602BC File Offset: 0x0015E4BC
		public XmlILConstructInfo ParentInfo
		{
			set
			{
				this.parentInfo = value;
			}
		}

		// Token: 0x17000CE7 RID: 3303
		// (get) Token: 0x06004203 RID: 16899 RVA: 0x001602C5 File Offset: 0x0015E4C5
		public XmlILConstructInfo ParentElementInfo
		{
			get
			{
				if (this.parentInfo != null && this.parentInfo.nodeType == QilNodeType.ElementCtor)
				{
					return this.parentInfo;
				}
				return null;
			}
		}

		// Token: 0x17000CE8 RID: 3304
		// (get) Token: 0x06004204 RID: 16900 RVA: 0x001602E6 File Offset: 0x0015E4E6
		// (set) Token: 0x06004205 RID: 16901 RVA: 0x001602EE File Offset: 0x0015E4EE
		public bool IsNamespaceInScope
		{
			get
			{
				return this.isNmspInScope;
			}
			set
			{
				this.isNmspInScope = value;
			}
		}

		// Token: 0x17000CE9 RID: 3305
		// (get) Token: 0x06004206 RID: 16902 RVA: 0x001602F7 File Offset: 0x0015E4F7
		// (set) Token: 0x06004207 RID: 16903 RVA: 0x001602FF File Offset: 0x0015E4FF
		public bool MightHaveNamespaces
		{
			get
			{
				return this.mightHaveNmsp;
			}
			set
			{
				this.mightHaveNmsp = value;
			}
		}

		// Token: 0x17000CEA RID: 3306
		// (get) Token: 0x06004208 RID: 16904 RVA: 0x00160308 File Offset: 0x0015E508
		// (set) Token: 0x06004209 RID: 16905 RVA: 0x00160310 File Offset: 0x0015E510
		public bool MightHaveNamespacesAfterAttributes
		{
			get
			{
				return this.mightHaveNmspAfterAttrs;
			}
			set
			{
				this.mightHaveNmspAfterAttrs = value;
			}
		}

		// Token: 0x17000CEB RID: 3307
		// (get) Token: 0x0600420A RID: 16906 RVA: 0x00160319 File Offset: 0x0015E519
		// (set) Token: 0x0600420B RID: 16907 RVA: 0x00160321 File Offset: 0x0015E521
		public bool MightHaveAttributes
		{
			get
			{
				return this.mightHaveAttrs;
			}
			set
			{
				this.mightHaveAttrs = value;
			}
		}

		// Token: 0x17000CEC RID: 3308
		// (get) Token: 0x0600420C RID: 16908 RVA: 0x0016032A File Offset: 0x0015E52A
		// (set) Token: 0x0600420D RID: 16909 RVA: 0x00160332 File Offset: 0x0015E532
		public bool MightHaveDuplicateAttributes
		{
			get
			{
				return this.mightHaveDupAttrs;
			}
			set
			{
				this.mightHaveDupAttrs = value;
			}
		}

		// Token: 0x17000CED RID: 3309
		// (get) Token: 0x0600420E RID: 16910 RVA: 0x0016033B File Offset: 0x0015E53B
		public ArrayList CallersInfo
		{
			get
			{
				if (this.callersInfo == null)
				{
					this.callersInfo = new ArrayList();
				}
				return this.callersInfo;
			}
		}

		// Token: 0x17000CEE RID: 3310
		// (get) Token: 0x0600420F RID: 16911 RVA: 0x00160356 File Offset: 0x0015E556
		public virtual string Name
		{
			get
			{
				return "ConstructInfo";
			}
		}

		// Token: 0x06004210 RID: 16912 RVA: 0x00160360 File Offset: 0x0015E560
		public override string ToString()
		{
			string text = "";
			if (this.constrMeth != XmlILConstructMethod.Iterator)
			{
				text += this.constrMeth.ToString();
				text = text + ", " + this.xstatesInitial;
				if (this.xstatesBeginLoop != PossibleXmlStates.None)
				{
					text = string.Concat(new string[]
					{
						text,
						" => ",
						this.xstatesBeginLoop.ToString(),
						" => ",
						this.xstatesEndLoop.ToString()
					});
				}
				text = text + " => " + this.xstatesFinal;
				if (!this.MightHaveAttributes)
				{
					text += ", NoAttrs";
				}
				if (!this.MightHaveDuplicateAttributes)
				{
					text += ", NoDupAttrs";
				}
				if (!this.MightHaveNamespaces)
				{
					text += ", NoNmsp";
				}
				if (!this.MightHaveNamespacesAfterAttributes)
				{
					text += ", NoNmspAfterAttrs";
				}
			}
			return text;
		}

		// Token: 0x04002A53 RID: 10835
		private QilNodeType nodeType;

		// Token: 0x04002A54 RID: 10836
		private PossibleXmlStates xstatesInitial;

		// Token: 0x04002A55 RID: 10837
		private PossibleXmlStates xstatesFinal;

		// Token: 0x04002A56 RID: 10838
		private PossibleXmlStates xstatesBeginLoop;

		// Token: 0x04002A57 RID: 10839
		private PossibleXmlStates xstatesEndLoop;

		// Token: 0x04002A58 RID: 10840
		private bool isNmspInScope;

		// Token: 0x04002A59 RID: 10841
		private bool mightHaveNmsp;

		// Token: 0x04002A5A RID: 10842
		private bool mightHaveAttrs;

		// Token: 0x04002A5B RID: 10843
		private bool mightHaveDupAttrs;

		// Token: 0x04002A5C RID: 10844
		private bool mightHaveNmspAfterAttrs;

		// Token: 0x04002A5D RID: 10845
		private XmlILConstructMethod constrMeth;

		// Token: 0x04002A5E RID: 10846
		private XmlILConstructInfo parentInfo;

		// Token: 0x04002A5F RID: 10847
		private ArrayList callersInfo;

		// Token: 0x04002A60 RID: 10848
		private bool isReadOnly;

		// Token: 0x04002A61 RID: 10849
		private static volatile XmlILConstructInfo Default;
	}
}
