using System;
using System.Collections;
using System.IO;
using System.Text;
using Novell.Directory.Ldap.Utilclass;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000019 RID: 25
	public class LdapDITStructureRuleSchema : LdapSchemaElement
	{
		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000142 RID: 322 RVA: 0x00006FCC File Offset: 0x000051CC
		public virtual int RuleID
		{
			get
			{
				return this.ruleID;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000143 RID: 323 RVA: 0x00006FD4 File Offset: 0x000051D4
		public virtual string NameForm
		{
			get
			{
				return this.nameForm;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000144 RID: 324 RVA: 0x00006FDC File Offset: 0x000051DC
		public virtual string[] Superiors
		{
			get
			{
				return this.superiorIDs;
			}
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00006FE4 File Offset: 0x000051E4
		public LdapDITStructureRuleSchema(string[] names, int ruleID, string description, bool obsolete, string nameForm, string[] superiorIDs)
			: base(LdapSchema.schemaTypeNames[5])
		{
			this.names = new string[names.Length];
			names.CopyTo(this.names, 0);
			this.ruleID = ruleID;
			this.description = description;
			this.obsolete = obsolete;
			this.nameForm = nameForm;
			this.superiorIDs = superiorIDs;
			base.Value = this.formatString();
		}

		// Token: 0x06000146 RID: 326 RVA: 0x0000706C File Offset: 0x0000526C
		public LdapDITStructureRuleSchema(string raw)
			: base(LdapSchema.schemaTypeNames[5])
		{
			this.obsolete = false;
			try
			{
				SchemaParser schemaParser = new SchemaParser(raw);
				if (schemaParser.Names != null)
				{
					this.names = new string[schemaParser.Names.Length];
					schemaParser.Names.CopyTo(this.names, 0);
				}
				if (schemaParser.ID != null)
				{
					this.ruleID = int.Parse(schemaParser.ID);
				}
				if (schemaParser.Description != null)
				{
					this.description = schemaParser.Description;
				}
				if (schemaParser.Superiors != null)
				{
					this.superiorIDs = new string[schemaParser.Superiors.Length];
					schemaParser.Superiors.CopyTo(this.superiorIDs, 0);
				}
				if (schemaParser.NameForm != null)
				{
					this.nameForm = schemaParser.NameForm;
				}
				this.obsolete = schemaParser.Obsolete;
				IEnumerator qualifiers = schemaParser.Qualifiers;
				while (qualifiers.MoveNext())
				{
					object obj = qualifiers.Current;
					AttributeQualifier attributeQualifier = (AttributeQualifier)obj;
					this.setQualifier(attributeQualifier.Name, attributeQualifier.Values);
				}
				base.Value = this.formatString();
			}
			catch (IOException)
			{
			}
		}

		// Token: 0x06000147 RID: 327 RVA: 0x000071AC File Offset: 0x000053AC
		protected internal override string formatString()
		{
			StringBuilder stringBuilder = new StringBuilder("( ");
			string text = this.RuleID.ToString();
			stringBuilder.Append(text);
			string[] array = this.Names;
			if (array != null)
			{
				stringBuilder.Append(" NAME ");
				if (array.Length == 1)
				{
					stringBuilder.Append("'" + array[0] + "'");
				}
				else
				{
					stringBuilder.Append("( ");
					for (int i = 0; i < array.Length; i++)
					{
						stringBuilder.Append(" '" + array[i] + "'");
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
			if ((text = this.NameForm) != null)
			{
				stringBuilder.Append(" FORM ");
				stringBuilder.Append("'" + text + "'");
			}
			if ((array = this.Superiors) != null)
			{
				stringBuilder.Append(" SUP ");
				if (array.Length > 1)
				{
					stringBuilder.Append("( ");
				}
				for (int j = 0; j < array.Length; j++)
				{
					if (j > 0)
					{
						stringBuilder.Append(" ");
					}
					stringBuilder.Append(array[j]);
				}
				if (array.Length > 1)
				{
					stringBuilder.Append(" )");
				}
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
					if ((qualifier = this.getQualifier(text2)) != null)
					{
						if (qualifier.Length > 1)
						{
							stringBuilder.Append("( ");
						}
						for (int k = 0; k < qualifier.Length; k++)
						{
							if (k > 0)
							{
								stringBuilder.Append(" ");
							}
							stringBuilder.Append("'" + qualifier[k] + "'");
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

		// Token: 0x04000097 RID: 151
		private int ruleID;

		// Token: 0x04000098 RID: 152
		private string nameForm = "";

		// Token: 0x04000099 RID: 153
		private string[] superiorIDs = new string[] { "" };
	}
}
