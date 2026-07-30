using System;

namespace System.Xml.Xsl.Qil
{
	// Token: 0x02000633 RID: 1587
	internal class QilInvokeLateBound : QilBinary
	{
		// Token: 0x06003E9A RID: 16026 RVA: 0x00155FEF File Offset: 0x001541EF
		public QilInvokeLateBound(QilNodeType nodeType, QilNode name, QilNode arguments)
			: base(nodeType, name, arguments)
		{
		}

		// Token: 0x17000C8A RID: 3210
		// (get) Token: 0x06003E9B RID: 16027 RVA: 0x001578C6 File Offset: 0x00155AC6
		// (set) Token: 0x06003E9C RID: 16028 RVA: 0x00156002 File Offset: 0x00154202
		public QilName Name
		{
			get
			{
				return (QilName)base.Left;
			}
			set
			{
				base.Left = value;
			}
		}

		// Token: 0x17000C8B RID: 3211
		// (get) Token: 0x06003E9D RID: 16029 RVA: 0x0015600B File Offset: 0x0015420B
		// (set) Token: 0x06003E9E RID: 16030 RVA: 0x00156018 File Offset: 0x00154218
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
