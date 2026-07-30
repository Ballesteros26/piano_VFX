using System;
using System.Diagnostics;
using System.Xml.Xsl.Qil;
using System.Xml.Xsl.XPath;

namespace System.Xml.Xsl.Xslt
{
	// Token: 0x02000575 RID: 1397
	internal struct SingletonFocus : IFocus
	{
		// Token: 0x06003789 RID: 14217 RVA: 0x00135397 File Offset: 0x00133597
		public SingletonFocus(XPathQilFactory f)
		{
			this.f = f;
			this.focusType = SingletonFocusType.None;
			this.current = null;
		}

		// Token: 0x0600378A RID: 14218 RVA: 0x001353AE File Offset: 0x001335AE
		public void SetFocus(SingletonFocusType focusType)
		{
			this.focusType = focusType;
		}

		// Token: 0x0600378B RID: 14219 RVA: 0x001353B7 File Offset: 0x001335B7
		public void SetFocus(QilIterator current)
		{
			if (current != null)
			{
				this.focusType = SingletonFocusType.Iterator;
				this.current = current;
				return;
			}
			this.focusType = SingletonFocusType.None;
			this.current = null;
		}

		// Token: 0x0600378C RID: 14220 RVA: 0x00002F50 File Offset: 0x00001150
		[Conditional("DEBUG")]
		private void CheckFocus()
		{
		}

		// Token: 0x0600378D RID: 14221 RVA: 0x001353DC File Offset: 0x001335DC
		public QilNode GetCurrent()
		{
			SingletonFocusType singletonFocusType = this.focusType;
			if (singletonFocusType == SingletonFocusType.InitialDocumentNode)
			{
				return this.f.Root(this.f.XmlContext());
			}
			if (singletonFocusType != SingletonFocusType.InitialContextNode)
			{
				return this.current;
			}
			return this.f.XmlContext();
		}

		// Token: 0x0600378E RID: 14222 RVA: 0x00135423 File Offset: 0x00133623
		public QilNode GetPosition()
		{
			return this.f.Double(1.0);
		}

		// Token: 0x0600378F RID: 14223 RVA: 0x00135423 File Offset: 0x00133623
		public QilNode GetLast()
		{
			return this.f.Double(1.0);
		}

		// Token: 0x040023A9 RID: 9129
		private XPathQilFactory f;

		// Token: 0x040023AA RID: 9130
		private SingletonFocusType focusType;

		// Token: 0x040023AB RID: 9131
		private QilIterator current;
	}
}
