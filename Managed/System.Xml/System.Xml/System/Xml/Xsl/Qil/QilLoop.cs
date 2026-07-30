using System;

namespace System.Xml.Xsl.Qil
{
	// Token: 0x02000637 RID: 1591
	internal class QilLoop : QilBinary
	{
		// Token: 0x06003EB6 RID: 16054 RVA: 0x00155FEF File Offset: 0x001541EF
		public QilLoop(QilNodeType nodeType, QilNode variable, QilNode body)
			: base(nodeType, variable, body)
		{
		}

		// Token: 0x17000C93 RID: 3219
		// (get) Token: 0x06003EB7 RID: 16055 RVA: 0x00157BC0 File Offset: 0x00155DC0
		// (set) Token: 0x06003EB8 RID: 16056 RVA: 0x00156002 File Offset: 0x00154202
		public QilIterator Variable
		{
			get
			{
				return (QilIterator)base.Left;
			}
			set
			{
				base.Left = value;
			}
		}

		// Token: 0x17000C94 RID: 3220
		// (get) Token: 0x06003EB9 RID: 16057 RVA: 0x00156147 File Offset: 0x00154347
		// (set) Token: 0x06003EBA RID: 16058 RVA: 0x00156018 File Offset: 0x00154218
		public QilNode Body
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
