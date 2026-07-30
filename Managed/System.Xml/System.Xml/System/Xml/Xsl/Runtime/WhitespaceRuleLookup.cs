using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Xsl.Qil;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x020005F4 RID: 1524
	internal class WhitespaceRuleLookup
	{
		// Token: 0x06003B61 RID: 15201 RVA: 0x0014E0B3 File Offset: 0x0014C2B3
		public WhitespaceRuleLookup()
		{
			this.qnames = new Hashtable();
			this.wildcards = new ArrayList();
		}

		// Token: 0x06003B62 RID: 15202 RVA: 0x0014E0D4 File Offset: 0x0014C2D4
		public WhitespaceRuleLookup(IList<WhitespaceRule> rules)
			: this()
		{
			for (int i = rules.Count - 1; i >= 0; i--)
			{
				WhitespaceRule whitespaceRule = rules[i];
				WhitespaceRuleLookup.InternalWhitespaceRule internalWhitespaceRule = new WhitespaceRuleLookup.InternalWhitespaceRule(whitespaceRule.LocalName, whitespaceRule.NamespaceName, whitespaceRule.PreserveSpace, -i);
				if (whitespaceRule.LocalName == null || whitespaceRule.NamespaceName == null)
				{
					this.wildcards.Add(internalWhitespaceRule);
				}
				else
				{
					this.qnames[internalWhitespaceRule] = internalWhitespaceRule;
				}
			}
			this.ruleTemp = new WhitespaceRuleLookup.InternalWhitespaceRule();
		}

		// Token: 0x06003B63 RID: 15203 RVA: 0x0014E154 File Offset: 0x0014C354
		public void Atomize(XmlNameTable nameTable)
		{
			if (nameTable != this.nameTable)
			{
				this.nameTable = nameTable;
				foreach (object obj in this.qnames.Values)
				{
					((WhitespaceRuleLookup.InternalWhitespaceRule)obj).Atomize(nameTable);
				}
				foreach (object obj2 in this.wildcards)
				{
					((WhitespaceRuleLookup.InternalWhitespaceRule)obj2).Atomize(nameTable);
				}
			}
		}

		// Token: 0x06003B64 RID: 15204 RVA: 0x0014E20C File Offset: 0x0014C40C
		public bool ShouldStripSpace(string localName, string namespaceName)
		{
			this.ruleTemp.Init(localName, namespaceName, false, 0);
			WhitespaceRuleLookup.InternalWhitespaceRule internalWhitespaceRule = this.qnames[this.ruleTemp] as WhitespaceRuleLookup.InternalWhitespaceRule;
			int count = this.wildcards.Count;
			while (count-- != 0)
			{
				WhitespaceRuleLookup.InternalWhitespaceRule internalWhitespaceRule2 = this.wildcards[count] as WhitespaceRuleLookup.InternalWhitespaceRule;
				if (internalWhitespaceRule != null)
				{
					if (internalWhitespaceRule.Priority > internalWhitespaceRule2.Priority)
					{
						return !internalWhitespaceRule.PreserveSpace;
					}
					if (internalWhitespaceRule.PreserveSpace == internalWhitespaceRule2.PreserveSpace)
					{
						continue;
					}
				}
				if ((internalWhitespaceRule2.LocalName == null || internalWhitespaceRule2.LocalName == localName) && (internalWhitespaceRule2.NamespaceName == null || internalWhitespaceRule2.NamespaceName == namespaceName))
				{
					return !internalWhitespaceRule2.PreserveSpace;
				}
			}
			return internalWhitespaceRule != null && !internalWhitespaceRule.PreserveSpace;
		}

		// Token: 0x04002726 RID: 10022
		private Hashtable qnames;

		// Token: 0x04002727 RID: 10023
		private ArrayList wildcards;

		// Token: 0x04002728 RID: 10024
		private WhitespaceRuleLookup.InternalWhitespaceRule ruleTemp;

		// Token: 0x04002729 RID: 10025
		private XmlNameTable nameTable;

		// Token: 0x020005F5 RID: 1525
		private class InternalWhitespaceRule : WhitespaceRule
		{
			// Token: 0x06003B65 RID: 15205 RVA: 0x0014E2C9 File Offset: 0x0014C4C9
			public InternalWhitespaceRule()
			{
			}

			// Token: 0x06003B66 RID: 15206 RVA: 0x0014E2D1 File Offset: 0x0014C4D1
			public InternalWhitespaceRule(string localName, string namespaceName, bool preserveSpace, int priority)
			{
				this.Init(localName, namespaceName, preserveSpace, priority);
			}

			// Token: 0x06003B67 RID: 15207 RVA: 0x0014E2E4 File Offset: 0x0014C4E4
			public void Init(string localName, string namespaceName, bool preserveSpace, int priority)
			{
				base.Init(localName, namespaceName, preserveSpace);
				this.priority = priority;
				if (localName != null && namespaceName != null)
				{
					this.hashCode = localName.GetHashCode();
				}
			}

			// Token: 0x06003B68 RID: 15208 RVA: 0x0014E309 File Offset: 0x0014C509
			public void Atomize(XmlNameTable nameTable)
			{
				if (base.LocalName != null)
				{
					base.LocalName = nameTable.Add(base.LocalName);
				}
				if (base.NamespaceName != null)
				{
					base.NamespaceName = nameTable.Add(base.NamespaceName);
				}
			}

			// Token: 0x17000C17 RID: 3095
			// (get) Token: 0x06003B69 RID: 15209 RVA: 0x0014E33F File Offset: 0x0014C53F
			public int Priority
			{
				get
				{
					return this.priority;
				}
			}

			// Token: 0x06003B6A RID: 15210 RVA: 0x0014E347 File Offset: 0x0014C547
			public override int GetHashCode()
			{
				return this.hashCode;
			}

			// Token: 0x06003B6B RID: 15211 RVA: 0x0014E350 File Offset: 0x0014C550
			public override bool Equals(object obj)
			{
				WhitespaceRuleLookup.InternalWhitespaceRule internalWhitespaceRule = obj as WhitespaceRuleLookup.InternalWhitespaceRule;
				return base.LocalName == internalWhitespaceRule.LocalName && base.NamespaceName == internalWhitespaceRule.NamespaceName;
			}

			// Token: 0x0400272A RID: 10026
			private int priority;

			// Token: 0x0400272B RID: 10027
			private int hashCode;
		}
	}
}
