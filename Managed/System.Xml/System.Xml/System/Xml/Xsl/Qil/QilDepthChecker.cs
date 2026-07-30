using System;
using System.Collections.Generic;
using System.Xml.XmlConfiguration;

namespace System.Xml.Xsl.Qil
{
	// Token: 0x0200064A RID: 1610
	internal class QilDepthChecker
	{
		// Token: 0x060040FF RID: 16639 RVA: 0x0015A572 File Offset: 0x00158772
		public static void Check(QilNode input)
		{
			if (XsltConfigSection.LimitXPathComplexity)
			{
				new QilDepthChecker().Check(input, 0);
			}
		}

		// Token: 0x06004100 RID: 16640 RVA: 0x0015A588 File Offset: 0x00158788
		private void Check(QilNode input, int depth)
		{
			if (depth > 800)
			{
				throw XsltException.Create("The stylesheet is too complex.", Array.Empty<string>());
			}
			if (input is QilReference)
			{
				if (this.visitedRef.ContainsKey(input))
				{
					return;
				}
				this.visitedRef[input] = true;
			}
			int num = depth + 1;
			for (int i = 0; i < input.Count; i++)
			{
				QilNode qilNode = input[i];
				if (qilNode != null)
				{
					this.Check(qilNode, num);
				}
			}
		}

		// Token: 0x040028CE RID: 10446
		private const int MAX_QIL_DEPTH = 800;

		// Token: 0x040028CF RID: 10447
		private Dictionary<QilNode, bool> visitedRef = new Dictionary<QilNode, bool>();
	}
}
