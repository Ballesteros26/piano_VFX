using System;
using System.Collections;
using System.Xml.XPath;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x02000547 RID: 1351
	internal class TemplateManager
	{
		// Token: 0x17000B8C RID: 2956
		// (get) Token: 0x060036A1 RID: 13985 RVA: 0x00131F85 File Offset: 0x00130185
		internal XmlQualifiedName Mode
		{
			get
			{
				return this.mode;
			}
		}

		// Token: 0x060036A2 RID: 13986 RVA: 0x00131F8D File Offset: 0x0013018D
		internal TemplateManager(Stylesheet stylesheet, XmlQualifiedName mode)
		{
			this.mode = mode;
			this.stylesheet = stylesheet;
		}

		// Token: 0x060036A3 RID: 13987 RVA: 0x00131FA3 File Offset: 0x001301A3
		internal void AddTemplate(TemplateAction template)
		{
			if (this.templates == null)
			{
				this.templates = new ArrayList();
			}
			this.templates.Add(template);
		}

		// Token: 0x060036A4 RID: 13988 RVA: 0x00131FC5 File Offset: 0x001301C5
		internal void ProcessTemplates()
		{
			if (this.templates != null)
			{
				this.templates.Sort(TemplateManager.s_TemplateComparer);
			}
		}

		// Token: 0x060036A5 RID: 13989 RVA: 0x00131FE0 File Offset: 0x001301E0
		internal TemplateAction FindTemplate(Processor processor, XPathNavigator navigator)
		{
			if (this.templates == null)
			{
				return null;
			}
			for (int i = this.templates.Count - 1; i >= 0; i--)
			{
				TemplateAction templateAction = (TemplateAction)this.templates[i];
				int matchKey = templateAction.MatchKey;
				if (matchKey != -1 && processor.Matches(navigator, matchKey))
				{
					return templateAction;
				}
			}
			return null;
		}

		// Token: 0x04002309 RID: 8969
		private XmlQualifiedName mode;

		// Token: 0x0400230A RID: 8970
		internal ArrayList templates;

		// Token: 0x0400230B RID: 8971
		private Stylesheet stylesheet;

		// Token: 0x0400230C RID: 8972
		private static TemplateManager.TemplateComparer s_TemplateComparer = new TemplateManager.TemplateComparer();

		// Token: 0x02000548 RID: 1352
		private class TemplateComparer : IComparer
		{
			// Token: 0x060036A7 RID: 13991 RVA: 0x00132048 File Offset: 0x00130248
			public int Compare(object x, object y)
			{
				TemplateAction templateAction = (TemplateAction)x;
				TemplateAction templateAction2 = (TemplateAction)y;
				if (templateAction.Priority == templateAction2.Priority)
				{
					return templateAction.TemplateId - templateAction2.TemplateId;
				}
				if (templateAction.Priority <= templateAction2.Priority)
				{
					return -1;
				}
				return 1;
			}
		}
	}
}
