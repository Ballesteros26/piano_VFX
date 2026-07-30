using System;
using System.Collections.Generic;
using System.Xml.Xsl.Qil;

namespace System.Xml.Xsl.Xslt
{
	// Token: 0x0200057F RID: 1407
	internal class PatternBag
	{
		// Token: 0x060037B9 RID: 14265 RVA: 0x00136532 File Offset: 0x00134732
		public void Clear()
		{
			this.FixedNamePatterns.Clear();
			this.FixedNamePatternsNames.Clear();
			this.NonFixedNamePatterns.Clear();
		}

		// Token: 0x060037BA RID: 14266 RVA: 0x00136558 File Offset: 0x00134758
		public void Add(Pattern pattern)
		{
			QilName qname = pattern.Match.QName;
			List<Pattern> list;
			if (qname == null)
			{
				list = this.NonFixedNamePatterns;
			}
			else if (!this.FixedNamePatterns.TryGetValue(qname, out list))
			{
				this.FixedNamePatternsNames.Add(qname);
				list = (this.FixedNamePatterns[qname] = new List<Pattern>());
			}
			list.Add(pattern);
		}

		// Token: 0x0400243D RID: 9277
		public Dictionary<QilName, List<Pattern>> FixedNamePatterns = new Dictionary<QilName, List<Pattern>>();

		// Token: 0x0400243E RID: 9278
		public List<QilName> FixedNamePatternsNames = new List<QilName>();

		// Token: 0x0400243F RID: 9279
		public List<Pattern> NonFixedNamePatterns = new List<Pattern>();
	}
}
