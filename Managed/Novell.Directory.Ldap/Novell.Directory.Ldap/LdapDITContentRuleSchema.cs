using System;
using System.Collections;
using System.IO;
using System.Text;
using Novell.Directory.Ldap.Utilclass;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000018 RID: 24
	public class LdapDITContentRuleSchema : LdapSchemaElement
	{
		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600013B RID: 315 RVA: 0x000069A9 File Offset: 0x00004BA9
		public virtual string[] AuxiliaryClasses
		{
			get
			{
				return this.auxiliary;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600013C RID: 316 RVA: 0x000069B1 File Offset: 0x00004BB1
		public virtual string[] RequiredAttributes
		{
			get
			{
				return this.required;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600013D RID: 317 RVA: 0x000069B9 File Offset: 0x00004BB9
		public virtual string[] OptionalAttributes
		{
			get
			{
				return this.optional;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600013E RID: 318 RVA: 0x000069C1 File Offset: 0x00004BC1
		public virtual string[] PrecludedAttributes
		{
			get
			{
				return this.precluded;
			}
		}

		// Token: 0x0600013F RID: 319 RVA: 0x000069CC File Offset: 0x00004BCC
		public LdapDITContentRuleSchema(string[] names, string oid, string description, bool obsolete, string[] auxiliary, string[] required, string[] optional, string[] precluded)
			: base(LdapSchema.schemaTypeNames[4])
		{
			this.names = new string[names.Length];
			names.CopyTo(this.names, 0);
			this.oid = oid;
			this.description = description;
			this.obsolete = obsolete;
			this.auxiliary = auxiliary;
			this.required = required;
			this.optional = optional;
			this.precluded = precluded;
			base.Value = this.formatString();
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00006A94 File Offset: 0x00004C94
		public LdapDITContentRuleSchema(string raw)
			: base(LdapSchema.schemaTypeNames[4])
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
					this.oid = schemaParser.ID;
				}
				if (schemaParser.Description != null)
				{
					this.description = schemaParser.Description;
				}
				if (schemaParser.Auxiliary != null)
				{
					this.auxiliary = new string[schemaParser.Auxiliary.Length];
					schemaParser.Auxiliary.CopyTo(this.auxiliary, 0);
				}
				if (schemaParser.Required != null)
				{
					this.required = new string[schemaParser.Required.Length];
					schemaParser.Required.CopyTo(this.required, 0);
				}
				if (schemaParser.Optional != null)
				{
					this.optional = new string[schemaParser.Optional.Length];
					schemaParser.Optional.CopyTo(this.optional, 0);
				}
				if (schemaParser.Precluded != null)
				{
					this.precluded = new string[schemaParser.Precluded.Length];
					schemaParser.Precluded.CopyTo(this.precluded, 0);
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

		// Token: 0x06000141 RID: 321 RVA: 0x00006C80 File Offset: 0x00004E80
		protected internal override string formatString()
		{
			StringBuilder stringBuilder = new StringBuilder("( ");
			string text;
			if ((text = this.ID) != null)
			{
				stringBuilder.Append(text);
			}
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
			if ((array = this.AuxiliaryClasses) != null)
			{
				stringBuilder.Append(" AUX ");
				if (array.Length > 1)
				{
					stringBuilder.Append("( ");
				}
				for (int j = 0; j < array.Length; j++)
				{
					if (j > 0)
					{
						stringBuilder.Append(" $ ");
					}
					stringBuilder.Append(array[j]);
				}
				if (array.Length > 1)
				{
					stringBuilder.Append(" )");
				}
			}
			if ((array = this.RequiredAttributes) != null)
			{
				stringBuilder.Append(" MUST ");
				if (array.Length > 1)
				{
					stringBuilder.Append("( ");
				}
				for (int k = 0; k < array.Length; k++)
				{
					if (k > 0)
					{
						stringBuilder.Append(" $ ");
					}
					stringBuilder.Append(array[k]);
				}
				if (array.Length > 1)
				{
					stringBuilder.Append(" )");
				}
			}
			if ((array = this.OptionalAttributes) != null)
			{
				stringBuilder.Append(" MAY ");
				if (array.Length > 1)
				{
					stringBuilder.Append("( ");
				}
				for (int l = 0; l < array.Length; l++)
				{
					if (l > 0)
					{
						stringBuilder.Append(" $ ");
					}
					stringBuilder.Append(array[l]);
				}
				if (array.Length > 1)
				{
					stringBuilder.Append(" )");
				}
			}
			if ((array = this.PrecludedAttributes) != null)
			{
				stringBuilder.Append(" NOT ");
				if (array.Length > 1)
				{
					stringBuilder.Append("( ");
				}
				for (int m = 0; m < array.Length; m++)
				{
					if (m > 0)
					{
						stringBuilder.Append(" $ ");
					}
					stringBuilder.Append(array[m]);
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
						for (int n = 0; n < qualifier.Length; n++)
						{
							if (n > 0)
							{
								stringBuilder.Append(" ");
							}
							stringBuilder.Append("'" + qualifier[n] + "'");
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

		// Token: 0x04000093 RID: 147
		private string[] auxiliary = new string[] { "" };

		// Token: 0x04000094 RID: 148
		private string[] required = new string[] { "" };

		// Token: 0x04000095 RID: 149
		private string[] optional = new string[] { "" };

		// Token: 0x04000096 RID: 150
		private string[] precluded = new string[] { "" };
	}
}
