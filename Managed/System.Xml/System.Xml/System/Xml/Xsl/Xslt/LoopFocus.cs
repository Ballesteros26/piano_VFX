using System;
using System.Xml.Xsl.Qil;
using System.Xml.Xsl.XPath;

namespace System.Xml.Xsl.Xslt
{
	// Token: 0x02000577 RID: 1399
	internal struct LoopFocus : IFocus
	{
		// Token: 0x06003796 RID: 14230 RVA: 0x00135500 File Offset: 0x00133700
		public LoopFocus(XPathQilFactory f)
		{
			this.f = f;
			this.current = (this.cached = (this.last = null));
		}

		// Token: 0x06003797 RID: 14231 RVA: 0x00135530 File Offset: 0x00133730
		public void SetFocus(QilIterator current)
		{
			this.current = current;
			this.cached = (this.last = null);
		}

		// Token: 0x17000BB7 RID: 2999
		// (get) Token: 0x06003798 RID: 14232 RVA: 0x00135554 File Offset: 0x00133754
		public bool IsFocusSet
		{
			get
			{
				return this.current != null;
			}
		}

		// Token: 0x06003799 RID: 14233 RVA: 0x0013555F File Offset: 0x0013375F
		public QilNode GetCurrent()
		{
			return this.current;
		}

		// Token: 0x0600379A RID: 14234 RVA: 0x00135567 File Offset: 0x00133767
		public QilNode GetPosition()
		{
			return this.f.XsltConvert(this.f.PositionOf(this.current), XmlQueryTypeFactory.DoubleX);
		}

		// Token: 0x0600379B RID: 14235 RVA: 0x0013558A File Offset: 0x0013378A
		public QilNode GetLast()
		{
			if (this.last == null)
			{
				this.last = this.f.Let(this.f.Double(0.0));
			}
			return this.last;
		}

		// Token: 0x0600379C RID: 14236 RVA: 0x001355BF File Offset: 0x001337BF
		public void EnsureCache()
		{
			if (this.cached == null)
			{
				this.cached = this.f.Let(this.current.Binding);
				this.current.Binding = this.cached;
			}
		}

		// Token: 0x0600379D RID: 14237 RVA: 0x001355F6 File Offset: 0x001337F6
		public void Sort(QilNode sortKeys)
		{
			if (sortKeys != null)
			{
				this.EnsureCache();
				this.current = this.f.For(this.f.Sort(this.current, sortKeys));
			}
		}

		// Token: 0x0600379E RID: 14238 RVA: 0x00135624 File Offset: 0x00133824
		public QilLoop ConstructLoop(QilNode body)
		{
			if (this.last != null)
			{
				this.EnsureCache();
				this.last.Binding = this.f.XsltConvert(this.f.Length(this.cached), XmlQueryTypeFactory.DoubleX);
			}
			QilLoop qilLoop = this.f.BaseFactory.Loop(this.current, body);
			if (this.last != null)
			{
				qilLoop = this.f.BaseFactory.Loop(this.last, qilLoop);
			}
			if (this.cached != null)
			{
				qilLoop = this.f.BaseFactory.Loop(this.cached, qilLoop);
			}
			return qilLoop;
		}

		// Token: 0x040023B0 RID: 9136
		private XPathQilFactory f;

		// Token: 0x040023B1 RID: 9137
		private QilIterator current;

		// Token: 0x040023B2 RID: 9138
		private QilIterator cached;

		// Token: 0x040023B3 RID: 9139
		private QilIterator last;
	}
}
