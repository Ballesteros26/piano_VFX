using System;
using System.Collections;
using System.IO;
using System.Text;
using Novell.Directory.Ldap.Utilclass;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000039 RID: 57
	public class LdapSyntaxSchema : LdapSchemaElement
	{
		// Token: 0x06000241 RID: 577 RVA: 0x0000AD05 File Offset: 0x00008F05
		public LdapSyntaxSchema(string oid, string description)
			: base(LdapSchema.schemaTypeNames[2])
		{
			this.oid = oid;
			this.description = description;
			base.Value = this.formatString();
		}

		// Token: 0x06000242 RID: 578 RVA: 0x0000AD30 File Offset: 0x00008F30
		public LdapSyntaxSchema(string raw)
			: base(LdapSchema.schemaTypeNames[2])
		{
			try
			{
				SchemaParser schemaParser = new SchemaParser(raw);
				if (schemaParser.ID != null)
				{
					this.oid = schemaParser.ID;
				}
				if (schemaParser.Description != null)
				{
					this.description = schemaParser.Description;
				}
				IEnumerator qualifiers = schemaParser.Qualifiers;
				while (qualifiers.MoveNext())
				{
					object obj = qualifiers.Current;
					AttributeQualifier attributeQualifier = (AttributeQualifier)obj;
					this.setQualifier(attributeQualifier.Name, attributeQualifier.Values);
				}
				base.Value = this.formatString();
			}
			catch (IOException ex)
			{
				throw new SystemException(ex.ToString());
			}
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0000ADD4 File Offset: 0x00008FD4
		protected internal override string formatString()
		{
			StringBuilder stringBuilder = new StringBuilder("( ");
			string text;
			if ((text = this.ID) != null)
			{
				stringBuilder.Append(text);
			}
			if ((text = this.Description) != null)
			{
				stringBuilder.Append(" DESC ");
				stringBuilder.Append("'" + text + "'");
			}
			IEnumerator qualifierNames;
			if ((qualifierNames = this.QualifierNames) != null)
			{
				while (qualifierNames.MoveNext())
				{
					object obj = qualifierNames.Current;
					string text2 = (string)obj;
					stringBuilder.Append(" " + text2 + " ");
					string[] qualifier;
					if ((qualifier = this.getQualifier(text2)) != null && qualifier.Length > 1)
					{
						stringBuilder.Append("( ");
						for (int i = 0; i < qualifier.Length; i++)
						{
							if (i > 0)
							{
								stringBuilder.Append(" ");
							}
							stringBuilder.Append("'" + qualifier[i] + "'");
						}
						if (qualifier.Length > 1)
						{
							stringBuilder.Append(" )");
						}
					}
				}
			}
			stringBuilder.Append(" )");
			return stringBuilder.ToString();
		}
	}
}
