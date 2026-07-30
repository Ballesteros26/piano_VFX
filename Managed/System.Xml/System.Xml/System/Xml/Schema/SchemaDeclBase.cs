using System;
using System.Collections.Generic;

namespace System.Xml.Schema
{
	// Token: 0x02000414 RID: 1044
	internal abstract class SchemaDeclBase
	{
		// Token: 0x060028D2 RID: 10450 RVA: 0x000F8AB0 File Offset: 0x000F6CB0
		protected SchemaDeclBase(XmlQualifiedName name, string prefix)
		{
			this.name = name;
			this.prefix = prefix;
			this.maxLength = -1L;
			this.minLength = -1L;
		}

		// Token: 0x060028D3 RID: 10451 RVA: 0x000F8AE1 File Offset: 0x000F6CE1
		protected SchemaDeclBase()
		{
		}

		// Token: 0x17000879 RID: 2169
		// (get) Token: 0x060028D4 RID: 10452 RVA: 0x000F8AF4 File Offset: 0x000F6CF4
		// (set) Token: 0x060028D5 RID: 10453 RVA: 0x000F8AFC File Offset: 0x000F6CFC
		internal XmlQualifiedName Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x1700087A RID: 2170
		// (get) Token: 0x060028D6 RID: 10454 RVA: 0x000F8B05 File Offset: 0x000F6D05
		// (set) Token: 0x060028D7 RID: 10455 RVA: 0x000F8B1B File Offset: 0x000F6D1B
		internal string Prefix
		{
			get
			{
				if (this.prefix != null)
				{
					return this.prefix;
				}
				return string.Empty;
			}
			set
			{
				this.prefix = value;
			}
		}

		// Token: 0x1700087B RID: 2171
		// (get) Token: 0x060028D8 RID: 10456 RVA: 0x000F8B24 File Offset: 0x000F6D24
		// (set) Token: 0x060028D9 RID: 10457 RVA: 0x000F8B2C File Offset: 0x000F6D2C
		internal bool IsDeclaredInExternal
		{
			get
			{
				return this.isDeclaredInExternal;
			}
			set
			{
				this.isDeclaredInExternal = value;
			}
		}

		// Token: 0x1700087C RID: 2172
		// (get) Token: 0x060028DA RID: 10458 RVA: 0x000F8B35 File Offset: 0x000F6D35
		// (set) Token: 0x060028DB RID: 10459 RVA: 0x000F8B3D File Offset: 0x000F6D3D
		internal SchemaDeclBase.Use Presence
		{
			get
			{
				return this.presence;
			}
			set
			{
				this.presence = value;
			}
		}

		// Token: 0x1700087D RID: 2173
		// (get) Token: 0x060028DC RID: 10460 RVA: 0x000F8B46 File Offset: 0x000F6D46
		// (set) Token: 0x060028DD RID: 10461 RVA: 0x000F8B4E File Offset: 0x000F6D4E
		internal long MaxLength
		{
			get
			{
				return this.maxLength;
			}
			set
			{
				this.maxLength = value;
			}
		}

		// Token: 0x1700087E RID: 2174
		// (get) Token: 0x060028DE RID: 10462 RVA: 0x000F8B57 File Offset: 0x000F6D57
		// (set) Token: 0x060028DF RID: 10463 RVA: 0x000F8B5F File Offset: 0x000F6D5F
		internal long MinLength
		{
			get
			{
				return this.minLength;
			}
			set
			{
				this.minLength = value;
			}
		}

		// Token: 0x1700087F RID: 2175
		// (get) Token: 0x060028E0 RID: 10464 RVA: 0x000F8B68 File Offset: 0x000F6D68
		// (set) Token: 0x060028E1 RID: 10465 RVA: 0x000F8B70 File Offset: 0x000F6D70
		internal XmlSchemaType SchemaType
		{
			get
			{
				return this.schemaType;
			}
			set
			{
				this.schemaType = value;
			}
		}

		// Token: 0x17000880 RID: 2176
		// (get) Token: 0x060028E2 RID: 10466 RVA: 0x000F8B79 File Offset: 0x000F6D79
		// (set) Token: 0x060028E3 RID: 10467 RVA: 0x000F8B81 File Offset: 0x000F6D81
		internal XmlSchemaDatatype Datatype
		{
			get
			{
				return this.datatype;
			}
			set
			{
				this.datatype = value;
			}
		}

		// Token: 0x060028E4 RID: 10468 RVA: 0x000F8B8A File Offset: 0x000F6D8A
		internal void AddValue(string value)
		{
			if (this.values == null)
			{
				this.values = new List<string>();
			}
			this.values.Add(value);
		}

		// Token: 0x17000881 RID: 2177
		// (get) Token: 0x060028E5 RID: 10469 RVA: 0x000F8BAB File Offset: 0x000F6DAB
		// (set) Token: 0x060028E6 RID: 10470 RVA: 0x000F8BB3 File Offset: 0x000F6DB3
		internal List<string> Values
		{
			get
			{
				return this.values;
			}
			set
			{
				this.values = value;
			}
		}

		// Token: 0x17000882 RID: 2178
		// (get) Token: 0x060028E7 RID: 10471 RVA: 0x000F8BBC File Offset: 0x000F6DBC
		// (set) Token: 0x060028E8 RID: 10472 RVA: 0x000F8BD2 File Offset: 0x000F6DD2
		internal string DefaultValueRaw
		{
			get
			{
				if (this.defaultValueRaw == null)
				{
					return string.Empty;
				}
				return this.defaultValueRaw;
			}
			set
			{
				this.defaultValueRaw = value;
			}
		}

		// Token: 0x17000883 RID: 2179
		// (get) Token: 0x060028E9 RID: 10473 RVA: 0x000F8BDB File Offset: 0x000F6DDB
		// (set) Token: 0x060028EA RID: 10474 RVA: 0x000F8BE3 File Offset: 0x000F6DE3
		internal object DefaultValueTyped
		{
			get
			{
				return this.defaultValueTyped;
			}
			set
			{
				this.defaultValueTyped = value;
			}
		}

		// Token: 0x060028EB RID: 10475 RVA: 0x000F8BEC File Offset: 0x000F6DEC
		internal bool CheckEnumeration(object pVal)
		{
			return (this.datatype.TokenizedType != XmlTokenizedType.NOTATION && this.datatype.TokenizedType != XmlTokenizedType.ENUMERATION) || this.values.Contains(pVal.ToString());
		}

		// Token: 0x060028EC RID: 10476 RVA: 0x000F8C1E File Offset: 0x000F6E1E
		internal bool CheckValue(object pVal)
		{
			return (this.presence != SchemaDeclBase.Use.Fixed && this.presence != SchemaDeclBase.Use.RequiredFixed) || (this.defaultValueTyped != null && this.datatype.IsEqual(pVal, this.defaultValueTyped));
		}

		// Token: 0x04001AF2 RID: 6898
		protected XmlQualifiedName name = XmlQualifiedName.Empty;

		// Token: 0x04001AF3 RID: 6899
		protected string prefix;

		// Token: 0x04001AF4 RID: 6900
		protected bool isDeclaredInExternal;

		// Token: 0x04001AF5 RID: 6901
		protected SchemaDeclBase.Use presence;

		// Token: 0x04001AF6 RID: 6902
		protected XmlSchemaType schemaType;

		// Token: 0x04001AF7 RID: 6903
		protected XmlSchemaDatatype datatype;

		// Token: 0x04001AF8 RID: 6904
		protected string defaultValueRaw;

		// Token: 0x04001AF9 RID: 6905
		protected object defaultValueTyped;

		// Token: 0x04001AFA RID: 6906
		protected long maxLength;

		// Token: 0x04001AFB RID: 6907
		protected long minLength;

		// Token: 0x04001AFC RID: 6908
		protected List<string> values;

		// Token: 0x02000415 RID: 1045
		internal enum Use
		{
			// Token: 0x04001AFE RID: 6910
			Default,
			// Token: 0x04001AFF RID: 6911
			Required,
			// Token: 0x04001B00 RID: 6912
			Implied,
			// Token: 0x04001B01 RID: 6913
			Fixed,
			// Token: 0x04001B02 RID: 6914
			RequiredFixed
		}
	}
}
