using System;
using System.Collections;
using System.IO;
using System.Text;
using Novell.Directory.Ldap.Utilclass;

namespace Novell.Directory.Ldap
{
	// Token: 0x0200000D RID: 13
	public class LdapAttributeSchema : LdapSchemaElement
	{
		// Token: 0x06000083 RID: 131 RVA: 0x00004308 File Offset: 0x00002508
		private void InitBlock()
		{
			this.usage = 0;
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000084 RID: 132 RVA: 0x00004311 File Offset: 0x00002511
		public virtual string SyntaxString
		{
			get
			{
				return this.syntaxString;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000085 RID: 133 RVA: 0x00004319 File Offset: 0x00002519
		public virtual string Superior
		{
			get
			{
				return this.superior;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000086 RID: 134 RVA: 0x00004321 File Offset: 0x00002521
		public virtual bool SingleValued
		{
			get
			{
				return this.single;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00004329 File Offset: 0x00002529
		public virtual string EqualityMatchingRule
		{
			get
			{
				return this.equality;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00004331 File Offset: 0x00002531
		public virtual string OrderingMatchingRule
		{
			get
			{
				return this.ordering;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000089 RID: 137 RVA: 0x00004339 File Offset: 0x00002539
		public virtual string SubstringMatchingRule
		{
			get
			{
				return this.substring;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600008A RID: 138 RVA: 0x00004341 File Offset: 0x00002541
		public virtual bool Collective
		{
			get
			{
				return this.collective;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600008B RID: 139 RVA: 0x00004349 File Offset: 0x00002549
		public virtual bool UserModifiable
		{
			get
			{
				return this.userMod;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600008C RID: 140 RVA: 0x00004351 File Offset: 0x00002551
		public virtual int Usage
		{
			get
			{
				return this.usage;
			}
		}

		// Token: 0x0600008D RID: 141 RVA: 0x0000435C File Offset: 0x0000255C
		public LdapAttributeSchema(string[] names, string oid, string description, string syntaxString, bool single, string superior, bool obsolete, string equality, string ordering, string substring, bool collective, bool isUserModifiable, int usage)
			: base(LdapSchema.schemaTypeNames[0])
		{
			this.InitBlock();
			this.names = names;
			this.oid = oid;
			this.description = description;
			this.obsolete = obsolete;
			this.syntaxString = syntaxString;
			this.single = single;
			this.equality = equality;
			this.ordering = ordering;
			this.substring = substring;
			this.collective = collective;
			this.userMod = isUserModifiable;
			this.usage = usage;
			this.superior = superior;
			base.Value = this.formatString();
		}

		// Token: 0x0600008E RID: 142 RVA: 0x000043F4 File Offset: 0x000025F4
		public LdapAttributeSchema(string raw)
			: base(LdapSchema.schemaTypeNames[0])
		{
			this.InitBlock();
			try
			{
				SchemaParser schemaParser = new SchemaParser(raw);
				if (schemaParser.Names != null)
				{
					this.names = schemaParser.Names;
				}
				if (schemaParser.ID != null)
				{
					this.oid = schemaParser.ID;
				}
				if (schemaParser.Description != null)
				{
					this.description = schemaParser.Description;
				}
				if (schemaParser.Syntax != null)
				{
					this.syntaxString = schemaParser.Syntax;
				}
				if (schemaParser.Superior != null)
				{
					this.superior = schemaParser.Superior;
				}
				this.single = schemaParser.Single;
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
			catch (IOException ex)
			{
				throw new SystemException(ex.ToString());
			}
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000044F8 File Offset: 0x000026F8
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
			if ((text = this.Superior) != null)
			{
				stringBuilder.Append(" SUP ");
				stringBuilder.Append("'" + text + "'");
			}
			if ((text = this.EqualityMatchingRule) != null)
			{
				stringBuilder.Append(" EQUALITY ");
				stringBuilder.Append("'" + text + "'");
			}
			if ((text = this.OrderingMatchingRule) != null)
			{
				stringBuilder.Append(" ORDERING ");
				stringBuilder.Append("'" + text + "'");
			}
			if ((text = this.SubstringMatchingRule) != null)
			{
				stringBuilder.Append(" SUBSTR ");
				stringBuilder.Append("'" + text + "'");
			}
			if ((text = this.SyntaxString) != null)
			{
				stringBuilder.Append(" SYNTAX ");
				stringBuilder.Append(text);
			}
			if (this.SingleValued)
			{
				stringBuilder.Append(" SINGLE-VALUE");
			}
			if (this.Collective)
			{
				stringBuilder.Append(" COLLECTIVE");
			}
			if (!this.UserModifiable)
			{
				stringBuilder.Append(" NO-USER-MODIFICATION");
			}
			int num;
			if ((num = this.Usage) != 0)
			{
				switch (num)
				{
				case 1:
					stringBuilder.Append(" USAGE directoryOperation");
					break;
				case 2:
					stringBuilder.Append(" USAGE distributedOperation");
					break;
				case 3:
					stringBuilder.Append(" USAGE dSAOperation");
					break;
				}
			}
			IEnumerator qualifierNames = this.QualifierNames;
			while (qualifierNames.MoveNext())
			{
				object obj = qualifierNames.Current;
				text = (string)obj;
				if (text != null)
				{
					stringBuilder.Append(" " + text);
					array = this.getQualifier(text);
					if (array != null)
					{
						if (array.Length > 1)
						{
							stringBuilder.Append("(");
						}
						for (int j = 0; j < array.Length; j++)
						{
							stringBuilder.Append(" '" + array[j] + "'");
						}
						if (array.Length > 1)
						{
							stringBuilder.Append(" )");
						}
					}
				}
			}
			stringBuilder.Append(" )");
			return stringBuilder.ToString();
		}

		// Token: 0x04000060 RID: 96
		private string syntaxString;

		// Token: 0x04000061 RID: 97
		private bool single;

		// Token: 0x04000062 RID: 98
		private string superior;

		// Token: 0x04000063 RID: 99
		private string equality;

		// Token: 0x04000064 RID: 100
		private string ordering;

		// Token: 0x04000065 RID: 101
		private string substring;

		// Token: 0x04000066 RID: 102
		private bool collective;

		// Token: 0x04000067 RID: 103
		private bool userMod = true;

		// Token: 0x04000068 RID: 104
		private int usage;

		// Token: 0x04000069 RID: 105
		public const int USER_APPLICATIONS = 0;

		// Token: 0x0400006A RID: 106
		public const int DIRECTORY_OPERATION = 1;

		// Token: 0x0400006B RID: 107
		public const int DISTRIBUTED_OPERATION = 2;

		// Token: 0x0400006C RID: 108
		public const int DSA_OPERATION = 3;
	}
}
