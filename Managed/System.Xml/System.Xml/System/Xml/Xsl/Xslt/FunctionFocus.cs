using System;
using System.Collections.Generic;
using System.Xml.Xsl.Qil;
using System.Xml.Xsl.XPath;

namespace System.Xml.Xsl.Xslt
{
	// Token: 0x02000576 RID: 1398
	internal struct FunctionFocus : IFocus
	{
		// Token: 0x06003790 RID: 14224 RVA: 0x0013543C File Offset: 0x0013363C
		public void StartFocus(IList<QilNode> args, XslFlags flags)
		{
			int num = 0;
			if ((flags & XslFlags.Current) != XslFlags.None)
			{
				this.current = (QilParameter)args[num++];
			}
			if ((flags & XslFlags.Position) != XslFlags.None)
			{
				this.position = (QilParameter)args[num++];
			}
			if ((flags & XslFlags.Last) != XslFlags.None)
			{
				this.last = (QilParameter)args[num++];
			}
			this.isSet = true;
		}

		// Token: 0x06003791 RID: 14225 RVA: 0x001354B0 File Offset: 0x001336B0
		public void StopFocus()
		{
			this.isSet = false;
			this.current = (this.position = (this.last = null));
		}

		// Token: 0x17000BB6 RID: 2998
		// (get) Token: 0x06003792 RID: 14226 RVA: 0x001354DD File Offset: 0x001336DD
		public bool IsFocusSet
		{
			get
			{
				return this.isSet;
			}
		}

		// Token: 0x06003793 RID: 14227 RVA: 0x001354E5 File Offset: 0x001336E5
		public QilNode GetCurrent()
		{
			return this.current;
		}

		// Token: 0x06003794 RID: 14228 RVA: 0x001354ED File Offset: 0x001336ED
		public QilNode GetPosition()
		{
			return this.position;
		}

		// Token: 0x06003795 RID: 14229 RVA: 0x001354F5 File Offset: 0x001336F5
		public QilNode GetLast()
		{
			return this.last;
		}

		// Token: 0x040023AC RID: 9132
		private bool isSet;

		// Token: 0x040023AD RID: 9133
		private QilParameter current;

		// Token: 0x040023AE RID: 9134
		private QilParameter position;

		// Token: 0x040023AF RID: 9135
		private QilParameter last;
	}
}
