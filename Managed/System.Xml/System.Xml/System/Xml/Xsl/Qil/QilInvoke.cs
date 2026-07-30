using System;

namespace System.Xml.Xsl.Qil
{
	// Token: 0x02000631 RID: 1585
	internal class QilInvoke : QilBinary
	{
		// Token: 0x06003E8E RID: 16014 RVA: 0x00155FEF File Offset: 0x001541EF
		public QilInvoke(QilNodeType nodeType, QilNode function, QilNode arguments)
			: base(nodeType, function, arguments)
		{
		}

		// Token: 0x17000C85 RID: 3205
		// (get) Token: 0x06003E8F RID: 16015 RVA: 0x0015784E File Offset: 0x00155A4E
		// (set) Token: 0x06003E90 RID: 16016 RVA: 0x00156002 File Offset: 0x00154202
		public QilFunction Function
		{
			get
			{
				return (QilFunction)base.Left;
			}
			set
			{
				base.Left = value;
			}
		}

		// Token: 0x17000C86 RID: 3206
		// (get) Token: 0x06003E91 RID: 16017 RVA: 0x0015600B File Offset: 0x0015420B
		// (set) Token: 0x06003E92 RID: 16018 RVA: 0x00156018 File Offset: 0x00154218
		public QilList Arguments
		{
			get
			{
				return (QilList)base.Right;
			}
			set
			{
				base.Right = value;
			}
		}
	}
}
