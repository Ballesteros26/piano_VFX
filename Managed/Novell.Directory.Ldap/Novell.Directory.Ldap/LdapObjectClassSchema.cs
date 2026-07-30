using System;
using System.Collections;
using System.IO;
using System.Text;
using Novell.Directory.Ldap.Utilclass;

namespace Novell.Directory.Ldap
{
	// Token: 0x0200002C RID: 44
	public class LdapObjectClassSchema : LdapSchemaElement
	{
		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x00008F9D File Offset: 0x0000719D
		public virtual string[] Superiors
		{
			get
			{
				return this.superiors;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x00008FA5 File Offset: 0x000071A5
		public virtual string[] RequiredAttributes
		{
			get
			{
				return this.required;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x00008FAD File Offset: 0x000071AD
		public virtual string[] OptionalAttributes
		{
			get
			{
				return this.optional;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x00008FB5 File Offset: 0x000071B5
		public virtual int Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00008FC0 File Offset: 0x000071C0
		public LdapObjectClassSchema(string[] names, string oid, string[] superiors, string description, string[] required, string[] optional, int type, bool obsolete)
			: base(LdapSchema.schemaTypeNames[1])
		{
			this.names = new string[names.Length];
			names.CopyTo(this.names, 0);
			this.oid = oid;
			this.description = description;
			this.type = type;
			this.obsolete = obsolete;
			if (superiors != null)
			{
				this.superiors = new string[superiors.Length];
				superiors.CopyTo(this.superiors, 0);
			}
			if (required != null)
			{
				this.required = new string[required.Length];
				required.CopyTo(this.required, 0);
			}
			if (optional != null)
			{
				this.optional = new string[optional.Length];
				optional.CopyTo(this.optional, 0);
			}
			base.Value = this.formatString();
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00009088 File Offset: 0x00007288
		public LdapObjectClassSchema(string raw)
			: base(LdapSchema.schemaTypeNames[1])
		{
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
				this.obsolete = schemaParser.Obsolete;
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
				if (schemaParser.Superiors != null)
				{
					this.superiors = new string[schemaParser.Superiors.Length];
					schemaParser.Superiors.CopyTo(this.superiors, 0);
				}
				this.type = schemaParser.Type;
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

		// Token: 0x060001C7 RID: 455 RVA: 0x00009200 File Offset: 0x00007400
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
						stringBuilder.Append(" $ ");
					}
					stringBuilder.Append(array[j]);
				}
				if (array.Length > 1)
				{
					stringBuilder.Append(" )");
				}
			}
			if (this.Type != -1)
			{
				if (this.Type == 0)
				{
					stringBuilder.Append(" ABSTRACT");
				}
				else if (this.Type == 2)
				{
					stringBuilder.Append(" AUXILIARY");
				}
				else if (this.Type == 1)
				{
					stringBuilder.Append(" STRUCTURAL");
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
						for (int m = 0; m < qualifier.Length; m++)
						{
							if (m > 0)
							{
								stringBuilder.Append(" ");
							}
							stringBuilder.Append("'" + qualifier[m] + "'");
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

		// Token: 0x04000127 RID: 295
		internal string[] superiors;

		// Token: 0x04000128 RID: 296
		internal string[] required;

		// Token: 0x04000129 RID: 297
		internal string[] optional;

		// Token: 0x0400012A RID: 298
		internal int type = -1;

		// Token: 0x0400012B RID: 299
		public const int ABSTRACT = 0;

		// Token: 0x0400012C RID: 300
		public const int STRUCTURAL = 1;

		// Token: 0x0400012D RID: 301
		public const int AUXILIARY = 2;
	}
}
