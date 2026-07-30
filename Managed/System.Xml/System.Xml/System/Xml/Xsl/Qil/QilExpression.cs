using System;
using System.Collections.Generic;
using System.Xml.Xsl.Runtime;

namespace System.Xml.Xsl.Qil
{
	// Token: 0x0200062E RID: 1582
	internal class QilExpression : QilNode
	{
		// Token: 0x06003DEF RID: 15855 RVA: 0x0015614F File Offset: 0x0015434F
		public QilExpression(QilNodeType nodeType, QilNode root)
			: this(nodeType, root, new QilFactory())
		{
		}

		// Token: 0x06003DF0 RID: 15856 RVA: 0x00156160 File Offset: 0x00154360
		public QilExpression(QilNodeType nodeType, QilNode root, QilFactory factory)
			: base(nodeType)
		{
			this.factory = factory;
			this.isDebug = factory.False();
			this.defWSet = factory.LiteralObject(new XmlWriterSettings
			{
				ConformanceLevel = ConformanceLevel.Auto
			});
			this.wsRules = factory.LiteralObject(new List<WhitespaceRule>());
			this.gloVars = factory.GlobalVariableList();
			this.gloParams = factory.GlobalParameterList();
			this.earlBnd = factory.LiteralObject(new List<EarlyBoundInfo>());
			this.funList = factory.FunctionList();
			this.rootNod = root;
		}

		// Token: 0x17000C74 RID: 3188
		// (get) Token: 0x06003DF1 RID: 15857 RVA: 0x00072E3D File Offset: 0x0007103D
		public override int Count
		{
			get
			{
				return 8;
			}
		}

		// Token: 0x17000C75 RID: 3189
		public override QilNode this[int index]
		{
			get
			{
				switch (index)
				{
				case 0:
					return this.isDebug;
				case 1:
					return this.defWSet;
				case 2:
					return this.wsRules;
				case 3:
					return this.gloParams;
				case 4:
					return this.gloVars;
				case 5:
					return this.earlBnd;
				case 6:
					return this.funList;
				case 7:
					return this.rootNod;
				default:
					throw new IndexOutOfRangeException();
				}
			}
			set
			{
				switch (index)
				{
				case 0:
					this.isDebug = value;
					return;
				case 1:
					this.defWSet = value;
					return;
				case 2:
					this.wsRules = value;
					return;
				case 3:
					this.gloParams = value;
					return;
				case 4:
					this.gloVars = value;
					return;
				case 5:
					this.earlBnd = value;
					return;
				case 6:
					this.funList = value;
					return;
				case 7:
					this.rootNod = value;
					return;
				default:
					throw new IndexOutOfRangeException();
				}
			}
		}

		// Token: 0x17000C76 RID: 3190
		// (get) Token: 0x06003DF4 RID: 15860 RVA: 0x001562DE File Offset: 0x001544DE
		// (set) Token: 0x06003DF5 RID: 15861 RVA: 0x001562E6 File Offset: 0x001544E6
		public QilFactory Factory
		{
			get
			{
				return this.factory;
			}
			set
			{
				this.factory = value;
			}
		}

		// Token: 0x17000C77 RID: 3191
		// (get) Token: 0x06003DF6 RID: 15862 RVA: 0x001562EF File Offset: 0x001544EF
		// (set) Token: 0x06003DF7 RID: 15863 RVA: 0x00156300 File Offset: 0x00154500
		public bool IsDebug
		{
			get
			{
				return this.isDebug.NodeType == QilNodeType.True;
			}
			set
			{
				this.isDebug = (value ? this.factory.True() : this.factory.False());
			}
		}

		// Token: 0x17000C78 RID: 3192
		// (get) Token: 0x06003DF8 RID: 15864 RVA: 0x00156323 File Offset: 0x00154523
		// (set) Token: 0x06003DF9 RID: 15865 RVA: 0x0015633A File Offset: 0x0015453A
		public XmlWriterSettings DefaultWriterSettings
		{
			get
			{
				return (XmlWriterSettings)((QilLiteral)this.defWSet).Value;
			}
			set
			{
				value.ReadOnly = true;
				((QilLiteral)this.defWSet).Value = value;
			}
		}

		// Token: 0x17000C79 RID: 3193
		// (get) Token: 0x06003DFA RID: 15866 RVA: 0x00156354 File Offset: 0x00154554
		// (set) Token: 0x06003DFB RID: 15867 RVA: 0x0015636B File Offset: 0x0015456B
		public IList<WhitespaceRule> WhitespaceRules
		{
			get
			{
				return (IList<WhitespaceRule>)((QilLiteral)this.wsRules).Value;
			}
			set
			{
				((QilLiteral)this.wsRules).Value = value;
			}
		}

		// Token: 0x17000C7A RID: 3194
		// (get) Token: 0x06003DFC RID: 15868 RVA: 0x0015637E File Offset: 0x0015457E
		// (set) Token: 0x06003DFD RID: 15869 RVA: 0x0015638B File Offset: 0x0015458B
		public QilList GlobalParameterList
		{
			get
			{
				return (QilList)this.gloParams;
			}
			set
			{
				this.gloParams = value;
			}
		}

		// Token: 0x17000C7B RID: 3195
		// (get) Token: 0x06003DFE RID: 15870 RVA: 0x00156394 File Offset: 0x00154594
		// (set) Token: 0x06003DFF RID: 15871 RVA: 0x001563A1 File Offset: 0x001545A1
		public QilList GlobalVariableList
		{
			get
			{
				return (QilList)this.gloVars;
			}
			set
			{
				this.gloVars = value;
			}
		}

		// Token: 0x17000C7C RID: 3196
		// (get) Token: 0x06003E00 RID: 15872 RVA: 0x001563AA File Offset: 0x001545AA
		// (set) Token: 0x06003E01 RID: 15873 RVA: 0x001563C1 File Offset: 0x001545C1
		public IList<EarlyBoundInfo> EarlyBoundTypes
		{
			get
			{
				return (IList<EarlyBoundInfo>)((QilLiteral)this.earlBnd).Value;
			}
			set
			{
				((QilLiteral)this.earlBnd).Value = value;
			}
		}

		// Token: 0x17000C7D RID: 3197
		// (get) Token: 0x06003E02 RID: 15874 RVA: 0x001563D4 File Offset: 0x001545D4
		// (set) Token: 0x06003E03 RID: 15875 RVA: 0x001563E1 File Offset: 0x001545E1
		public QilList FunctionList
		{
			get
			{
				return (QilList)this.funList;
			}
			set
			{
				this.funList = value;
			}
		}

		// Token: 0x17000C7E RID: 3198
		// (get) Token: 0x06003E04 RID: 15876 RVA: 0x001563EA File Offset: 0x001545EA
		// (set) Token: 0x06003E05 RID: 15877 RVA: 0x001563F2 File Offset: 0x001545F2
		public QilNode Root
		{
			get
			{
				return this.rootNod;
			}
			set
			{
				this.rootNod = value;
			}
		}

		// Token: 0x04002839 RID: 10297
		private QilFactory factory;

		// Token: 0x0400283A RID: 10298
		private QilNode isDebug;

		// Token: 0x0400283B RID: 10299
		private QilNode defWSet;

		// Token: 0x0400283C RID: 10300
		private QilNode wsRules;

		// Token: 0x0400283D RID: 10301
		private QilNode gloVars;

		// Token: 0x0400283E RID: 10302
		private QilNode gloParams;

		// Token: 0x0400283F RID: 10303
		private QilNode earlBnd;

		// Token: 0x04002840 RID: 10304
		private QilNode funList;

		// Token: 0x04002841 RID: 10305
		private QilNode rootNod;
	}
}
