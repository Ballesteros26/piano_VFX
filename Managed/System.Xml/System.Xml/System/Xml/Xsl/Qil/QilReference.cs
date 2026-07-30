using System;

namespace System.Xml.Xsl.Qil
{
	// Token: 0x0200063F RID: 1599
	internal class QilReference : QilNode
	{
		// Token: 0x06003FE2 RID: 16354 RVA: 0x00158A51 File Offset: 0x00156C51
		public QilReference(QilNodeType nodeType)
			: base(nodeType)
		{
		}

		// Token: 0x17000CAB RID: 3243
		// (get) Token: 0x06003FE3 RID: 16355 RVA: 0x00158A5A File Offset: 0x00156C5A
		// (set) Token: 0x06003FE4 RID: 16356 RVA: 0x00158A62 File Offset: 0x00156C62
		public string DebugName
		{
			get
			{
				return this.debugName;
			}
			set
			{
				if (value.Length > 1000)
				{
					value = value.Substring(0, 1000);
				}
				this.debugName = value;
			}
		}

		// Token: 0x040028C5 RID: 10437
		private const int MaxDebugNameLength = 1000;

		// Token: 0x040028C6 RID: 10438
		private string debugName;
	}
}
