using System;
using System.IO;
using System.Text;
using Novell.Directory.Ldap.Utilclass;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000024 RID: 36
	public class LdapMatchingRuleSchema : LdapSchemaElement
	{
		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600018A RID: 394 RVA: 0x00007DAC File Offset: 0x00005FAC
		public virtual string[] Attributes
		{
			get
			{
				return this.attributes;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600018B RID: 395 RVA: 0x00007DB4 File Offset: 0x00005FB4
		public virtual string SyntaxString
		{
			get
			{
				return this.syntaxString;
			}
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00007DBC File Offset: 0x00005FBC
		public LdapMatchingRuleSchema(string[] names, string oid, string description, string[] attributes, bool obsolete, string syntaxString)
			: base(LdapSchema.schemaTypeNames[6])
		{
			this.names = new string[names.Length];
			names.CopyTo(this.names, 0);
			this.oid = oid;
			this.description = description;
			this.obsolete = obsolete;
			this.attributes = new string[attributes.Length];
			attributes.CopyTo(this.attributes, 0);
			this.syntaxString = syntaxString;
			base.Value = this.formatString();
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00007E38 File Offset: 0x00006038
		public LdapMatchingRuleSchema(string rawMatchingRule, string rawMatchingRuleUse)
			: base(LdapSchema.schemaTypeNames[6])
		{
			try
			{
				SchemaParser schemaParser = new SchemaParser(rawMatchingRule);
				this.names = new string[schemaParser.Names.Length];
				schemaParser.Names.CopyTo(this.names, 0);
				this.oid = schemaParser.ID;
				this.description = schemaParser.Description;
				this.obsolete = schemaParser.Obsolete;
				this.syntaxString = schemaParser.Syntax;
				if (rawMatchingRuleUse != null)
				{
					SchemaParser schemaParser2 = new SchemaParser(rawMatchingRuleUse);
					this.attributes = schemaParser2.Applies;
				}
				base.Value = this.formatString();
			}
			catch (IOException)
			{
			}
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00007EE8 File Offset: 0x000060E8
		protected internal override string formatString()
		{
			StringBuilder stringBuilder = new StringBuilder("( ");
			string text;
			if ((text = this.ID) != null)
			{
				stringBuilder.Append(text);
			}
			string[] names = this.Names;
			if (names != null)
			{
				stringBuilder.Append(" NAME ");
				if (names.Length == 1)
				{
					stringBuilder.Append("'" + names[0] + "'");
				}
				else
				{
					stringBuilder.Append("( ");
					for (int i = 0; i < names.Length; i++)
					{
						stringBuilder.Append(" '" + names[i] + "'");
					}
					stringBuilder.Append(" )");
				}
			}
			if ((text = this.Description) != null)
			{
				stringBuilder.Append(" DESC ");
				stringBuilder.Append("'" + text + "'");
			}
			if (this.Obsolete)
			{
				stringBuilder.Append(" OBSOLETE");
			}
			if ((text = this.SyntaxString) != null)
			{
				stringBuilder.Append(" SYNTAX ");
				stringBuilder.Append(text);
			}
			stringBuilder.Append(" )");
			return stringBuilder.ToString();
		}

		// Token: 0x040000FF RID: 255
		private string syntaxString;

		// Token: 0x04000100 RID: 256
		private string[] attributes;
	}
}
