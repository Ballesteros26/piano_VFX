using System;

namespace System.Xml.Xsl.Qil
{
	// Token: 0x02000642 RID: 1602
	internal class QilSortKey : QilBinary
	{
		// Token: 0x06003FEE RID: 16366 RVA: 0x00155FEF File Offset: 0x001541EF
		public QilSortKey(QilNodeType nodeType, QilNode key, QilNode collation)
			: base(nodeType, key, collation)
		{
		}

		// Token: 0x17000CAC RID: 3244
		// (get) Token: 0x06003FEF RID: 16367 RVA: 0x00155FFA File Offset: 0x001541FA
		// (set) Token: 0x06003FF0 RID: 16368 RVA: 0x00156002 File Offset: 0x00154202
		public QilNode Key
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

		// Token: 0x17000CAD RID: 3245
		// (get) Token: 0x06003FF1 RID: 16369 RVA: 0x00156147 File Offset: 0x00154347
		// (set) Token: 0x06003FF2 RID: 16370 RVA: 0x00156018 File Offset: 0x00154218
		public QilNode Collation
		{
			get
			{
				return base.Right;
			}
			set
			{
				base.Right = value;
			}
		}
	}
}
