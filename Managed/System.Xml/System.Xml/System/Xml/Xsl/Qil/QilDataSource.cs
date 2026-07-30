using System;

namespace System.Xml.Xsl.Qil
{
	// Token: 0x0200062D RID: 1581
	internal class QilDataSource : QilBinary
	{
		// Token: 0x06003DEA RID: 15850 RVA: 0x00155FEF File Offset: 0x001541EF
		public QilDataSource(QilNodeType nodeType, QilNode name, QilNode baseUri)
			: base(nodeType, name, baseUri)
		{
		}

		// Token: 0x17000C72 RID: 3186
		// (get) Token: 0x06003DEB RID: 15851 RVA: 0x00155FFA File Offset: 0x001541FA
		// (set) Token: 0x06003DEC RID: 15852 RVA: 0x00156002 File Offset: 0x00154202
		public QilNode Name
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

		// Token: 0x17000C73 RID: 3187
		// (get) Token: 0x06003DED RID: 15853 RVA: 0x00156147 File Offset: 0x00154347
		// (set) Token: 0x06003DEE RID: 15854 RVA: 0x00156018 File Offset: 0x00154218
		public QilNode BaseUri
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
