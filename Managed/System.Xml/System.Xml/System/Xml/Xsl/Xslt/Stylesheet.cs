using System;
using System.Collections.Generic;
using System.Xml.Xsl.Qil;

namespace System.Xml.Xsl.Xslt
{
	// Token: 0x0200058C RID: 1420
	internal class Stylesheet : StylesheetLevel
	{
		// Token: 0x06003878 RID: 14456 RVA: 0x0013DCE4 File Offset: 0x0013BEE4
		public void AddTemplateMatch(Template template, QilLoop filter)
		{
			List<TemplateMatch> list;
			if (!this.TemplateMatches.TryGetValue(template.Mode, out list))
			{
				list = (this.TemplateMatches[template.Mode] = new List<TemplateMatch>());
			}
			list.Add(new TemplateMatch(template, filter));
		}

		// Token: 0x06003879 RID: 14457 RVA: 0x0013DD30 File Offset: 0x0013BF30
		public void SortTemplateMatches()
		{
			foreach (QilName qilName in this.TemplateMatches.Keys)
			{
				this.TemplateMatches[qilName].Sort(TemplateMatch.Comparer);
			}
		}

		// Token: 0x0600387A RID: 14458 RVA: 0x0013DD98 File Offset: 0x0013BF98
		public Stylesheet(Compiler compiler, int importPrecedence)
		{
			this.compiler = compiler;
			this.importPrecedence = importPrecedence;
			this.WhitespaceRules[0] = new List<WhitespaceRule>();
			this.WhitespaceRules[1] = new List<WhitespaceRule>();
			this.WhitespaceRules[2] = new List<WhitespaceRule>();
		}

		// Token: 0x17000BC3 RID: 3011
		// (get) Token: 0x0600387B RID: 14459 RVA: 0x0013DE23 File Offset: 0x0013C023
		public int ImportPrecedence
		{
			get
			{
				return this.importPrecedence;
			}
		}

		// Token: 0x0600387C RID: 14460 RVA: 0x0013DE2B File Offset: 0x0013C02B
		public void AddWhitespaceRule(int index, WhitespaceRule rule)
		{
			this.WhitespaceRules[index].Add(rule);
		}

		// Token: 0x0600387D RID: 14461 RVA: 0x0013DE3C File Offset: 0x0013C03C
		public bool AddVarPar(VarPar var)
		{
			using (List<XslNode>.Enumerator enumerator = this.GlobalVarPars.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.Name.Equals(var.Name))
					{
						return this.compiler.AllGlobalVarPars.ContainsKey(var.Name);
					}
				}
			}
			this.GlobalVarPars.Add(var);
			return true;
		}

		// Token: 0x0600387E RID: 14462 RVA: 0x0013DEC4 File Offset: 0x0013C0C4
		public bool AddTemplate(Template template)
		{
			template.ImportPrecedence = this.importPrecedence;
			int num = this.orderNumber;
			this.orderNumber = num + 1;
			template.OrderNumber = num;
			this.compiler.AllTemplates.Add(template);
			if (template.Name != null)
			{
				Template template2;
				if (!this.compiler.NamedTemplates.TryGetValue(template.Name, out template2))
				{
					this.compiler.NamedTemplates[template.Name] = template;
				}
				else if (template2.ImportPrecedence == template.ImportPrecedence)
				{
					return false;
				}
			}
			if (template.Match != null)
			{
				this.Templates.Add(template);
			}
			return true;
		}

		// Token: 0x040024A5 RID: 9381
		private Compiler compiler;

		// Token: 0x040024A6 RID: 9382
		public List<Uri> ImportHrefs = new List<Uri>();

		// Token: 0x040024A7 RID: 9383
		public List<XslNode> GlobalVarPars = new List<XslNode>();

		// Token: 0x040024A8 RID: 9384
		public Dictionary<QilName, AttributeSet> AttributeSets = new Dictionary<QilName, AttributeSet>();

		// Token: 0x040024A9 RID: 9385
		private int importPrecedence;

		// Token: 0x040024AA RID: 9386
		private int orderNumber;

		// Token: 0x040024AB RID: 9387
		public List<WhitespaceRule>[] WhitespaceRules = new List<WhitespaceRule>[3];

		// Token: 0x040024AC RID: 9388
		public List<Template> Templates = new List<Template>();

		// Token: 0x040024AD RID: 9389
		public Dictionary<QilName, List<TemplateMatch>> TemplateMatches = new Dictionary<QilName, List<TemplateMatch>>();
	}
}
