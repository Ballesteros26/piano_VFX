using System;

namespace System.Xml.Xsl.Qil
{
	// Token: 0x0200062B RID: 1579
	internal class QilChoice : QilBinary
	{
		// Token: 0x06003DDC RID: 15836 RVA: 0x00155FEF File Offset: 0x001541EF
		public QilChoice(QilNodeType nodeType, QilNode expression, QilNode branches)
			: base(nodeType, expression, branches)
		{
		}

		// Token: 0x17000C70 RID: 3184
		// (get) Token: 0x06003DDD RID: 15837 RVA: 0x00155FFA File Offset: 0x001541FA
		// (set) Token: 0x06003DDE RID: 15838 RVA: 0x00156002 File Offset: 0x00154202
		public QilNode Expression
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

		// Token: 0x17000C71 RID: 3185
		// (get) Token: 0x06003DDF RID: 15839 RVA: 0x0015600B File Offset: 0x0015420B
		// (set) Token: 0x06003DE0 RID: 15840 RVA: 0x00156018 File Offset: 0x00154218
		public QilList Branches
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
