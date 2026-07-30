using System;
using System.IO;
using System.Text;
using Novell.Directory.Ldap.Utilclass;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000025 RID: 37
	public class LdapMatchingRuleUseSchema : LdapSchemaElement
	{
		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600018F RID: 399 RVA: 0x00007FF9 File Offset: 0x000061F9
		public virtual string[] Attributes
		{
			get
			{
				return this.attributes;
			}
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00008004 File Offset: 0x00006204
		public LdapMatchingRuleUseSchema(string[] names, string oid, string description, bool obsolete, string[] attributes)
			: base(LdapSchema.schemaTypeNames[7])
		{
			this.names = new string[names.Length];
			names.CopyTo(this.names, 0);
			this.oid = oid;
			this.description = description;
			this.obsolete = obsolete;
			this.attributes = new string[attributes.Length];
			attributes.CopyTo(this.attributes, 0);
			base.Value = this.formatString();
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00008078 File Offset: 0x00006278
		public LdapMatchingRuleUseSchema(string raw)
			: base(LdapSchema.schemaTypeNames[7])
		{
			try
			{
				SchemaParser schemaParser = new SchemaParser(raw);
				this.names = new string[schemaParser.Names.Length];
				schemaParser.Names.CopyTo(this.names, 0);
				this.oid = schemaParser.ID;
				this.description = schemaParser.Description;
				this.obsolete = schemaParser.Obsolete;
				this.attributes = schemaParser.Applies;
				base.Value = this.formatString();
			}
			catch (IOException)
			{
			}
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00008110 File Offset: 0x00006310
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
			if ((names = this.Attributes) != null)
			{
				stringBuilder.Append(" APPLIES ");
				if (names.Length > 1)
				{
					stringBuilder.Append("( ");
				}
				for (int j = 0; j < names.Length; j++)
				{
					if (j > 0)
					{
						stringBuilder.Append(" $ ");
					}
					stringBuilder.Append(names[j]);
				}
				if (names.Length > 1)
				{
					stringBuilder.Append(" )");
				}
			}
			stringBuilder.Append(" )");
			return stringBuilder.ToString();
		}

		// Token: 0x04000101 RID: 257
		private string[] attributes;
	}
}
