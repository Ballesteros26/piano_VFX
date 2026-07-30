using System;
using System.Xml.Xsl.Qil;

namespace System.Xml.Xsl.Xslt
{
	// Token: 0x02000583 RID: 1411
	internal class ReferenceReplacer : QilReplaceVisitor
	{
		// Token: 0x060037D1 RID: 14289 RVA: 0x00136F4D File Offset: 0x0013514D
		public ReferenceReplacer(QilFactory f)
			: base(f)
		{
		}

		// Token: 0x060037D2 RID: 14290 RVA: 0x00136F56 File Offset: 0x00135156
		public QilNode Replace(QilNode expr, QilReference lookFor, QilReference replaceBy)
		{
			QilDepthChecker.Check(expr);
			this.lookFor = lookFor;
			this.replaceBy = replaceBy;
			return this.VisitAssumeReference(expr);
		}

		// Token: 0x060037D3 RID: 14291 RVA: 0x00136F73 File Offset: 0x00135173
		protected override QilNode VisitReference(QilNode n)
		{
			if (n != this.lookFor)
			{
				return n;
			}
			return this.replaceBy;
		}

		// Token: 0x04002453 RID: 9299
		private QilReference lookFor;

		// Token: 0x04002454 RID: 9300
		private QilReference replaceBy;
	}
}
