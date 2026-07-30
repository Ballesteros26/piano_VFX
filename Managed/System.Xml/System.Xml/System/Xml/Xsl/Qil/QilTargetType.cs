using System;

namespace System.Xml.Xsl.Qil
{
	// Token: 0x02000644 RID: 1604
	internal class QilTargetType : QilBinary
	{
		// Token: 0x06003FF8 RID: 16376 RVA: 0x00155FEF File Offset: 0x001541EF
		public QilTargetType(QilNodeType nodeType, QilNode expr, QilNode targetType)
			: base(nodeType, expr, targetType)
		{
		}

		// Token: 0x17000CB0 RID: 3248
		// (get) Token: 0x06003FF9 RID: 16377 RVA: 0x00155FFA File Offset: 0x001541FA
		// (set) Token: 0x06003FFA RID: 16378 RVA: 0x00156002 File Offset: 0x00154202
		public QilNode Source
		{
			get
			{
				return base.Left;
			}
			set
			{
				base.Left = value;
			}
		}

		// Token: 0x17000CB1 RID: 3249
		// (get) Token: 0x06003FFB RID: 16379 RVA: 0x00158DEF File Offset: 0x00156FEF
		// (set) Token: 0x06003FFC RID: 16380 RVA: 0x00158E06 File Offset: 0x00157006
		public XmlQueryType TargetType
		{
			get
			{
				return (XmlQueryType)((QilLiteral)base.Right).Value;
			}
			set
			{
				((QilLiteral)base.Right).Value = value;
			}
		}
	}
}
