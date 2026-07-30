using System;

namespace System.Xml.Xsl.Qil
{
	// Token: 0x02000634 RID: 1588
	internal class QilIterator : QilReference
	{
		// Token: 0x06003E9F RID: 16031 RVA: 0x001578D3 File Offset: 0x00155AD3
		public QilIterator(QilNodeType nodeType, QilNode binding)
			: base(nodeType)
		{
			this.Binding = binding;
		}

		// Token: 0x17000C8C RID: 3212
		// (get) Token: 0x06003EA0 RID: 16032 RVA: 0x00003242 File Offset: 0x00001442
		public override int Count
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000C8D RID: 3213
		public override QilNode this[int index]
		{
			get
			{
				if (index != 0)
				{
					throw new IndexOutOfRangeException();
				}
				return this.binding;
			}
			set
			{
				if (index != 0)
				{
					throw new IndexOutOfRangeException();
				}
				this.binding = value;
			}
		}

		// Token: 0x17000C8E RID: 3214
		// (get) Token: 0x06003EA3 RID: 16035 RVA: 0x00157906 File Offset: 0x00155B06
		// (set) Token: 0x06003EA4 RID: 16036 RVA: 0x0015790E File Offset: 0x00155B0E
		public QilNode Binding
		{
			get
			{
				return this.binding;
			}
			set
			{
				this.binding = value;
			}
		}

		// Token: 0x04002846 RID: 10310
		private QilNode binding;
	}
}
