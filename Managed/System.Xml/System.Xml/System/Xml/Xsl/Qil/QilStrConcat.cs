using System;

namespace System.Xml.Xsl.Qil
{
	// Token: 0x02000643 RID: 1603
	internal class QilStrConcat : QilBinary
	{
		// Token: 0x06003FF3 RID: 16371 RVA: 0x00155FEF File Offset: 0x001541EF
		public QilStrConcat(QilNodeType nodeType, QilNode delimiter, QilNode values)
			: base(nodeType, delimiter, values)
		{
		}

		// Token: 0x17000CAE RID: 3246
		// (get) Token: 0x06003FF4 RID: 16372 RVA: 0x00155FFA File Offset: 0x001541FA
		// (set) Token: 0x06003FF5 RID: 16373 RVA: 0x00156002 File Offset: 0x00154202
		public QilNode Delimiter
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

		// Token: 0x17000CAF RID: 3247
		// (get) Token: 0x06003FF6 RID: 16374 RVA: 0x00156147 File Offset: 0x00154347
		// (set) Token: 0x06003FF7 RID: 16375 RVA: 0x00156018 File Offset: 0x00154218
		public QilNode Values
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
