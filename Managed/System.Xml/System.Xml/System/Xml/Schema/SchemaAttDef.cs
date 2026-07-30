using System;

namespace System.Xml.Schema
{
	// Token: 0x0200040E RID: 1038
	internal sealed class SchemaAttDef : SchemaDeclBase, IDtdDefaultAttributeInfo, IDtdAttributeInfo
	{
		// Token: 0x06002843 RID: 10307 RVA: 0x000F1460 File Offset: 0x000EF660
		public SchemaAttDef(XmlQualifiedName name, string prefix)
			: base(name, prefix)
		{
		}

		// Token: 0x06002844 RID: 10308 RVA: 0x000F146A File Offset: 0x000EF66A
		public SchemaAttDef(XmlQualifiedName name)
			: base(name, null)
		{
		}

		// Token: 0x06002845 RID: 10309 RVA: 0x000F1474 File Offset: 0x000EF674
		private SchemaAttDef()
		{
		}

		// Token: 0x17000863 RID: 2147
		// (get) Token: 0x06002846 RID: 10310 RVA: 0x000F147C File Offset: 0x000EF67C
		string IDtdAttributeInfo.Prefix
		{
			get
			{
				return base.Prefix;
			}
		}

		// Token: 0x17000864 RID: 2148
		// (get) Token: 0x06002847 RID: 10311 RVA: 0x000F1484 File Offset: 0x000EF684
		string IDtdAttributeInfo.LocalName
		{
			get
			{
				return base.Name.Name;
			}
		}

		// Token: 0x17000865 RID: 2149
		// (get) Token: 0x06002848 RID: 10312 RVA: 0x000F1491 File Offset: 0x000EF691
		int IDtdAttributeInfo.LineNumber
		{
			get
			{
				return this.LineNumber;
			}
		}

		// Token: 0x17000866 RID: 2150
		// (get) Token: 0x06002849 RID: 10313 RVA: 0x000F1499 File Offset: 0x000EF699
		int IDtdAttributeInfo.LinePosition
		{
			get
			{
				return this.LinePosition;
			}
		}

		// Token: 0x17000867 RID: 2151
		// (get) Token: 0x0600284A RID: 10314 RVA: 0x000F14A1 File Offset: 0x000EF6A1
		bool IDtdAttributeInfo.IsNonCDataType
		{
			get
			{
				return this.TokenizedType > XmlTokenizedType.CDATA;
			}
		}

		// Token: 0x17000868 RID: 2152
		// (get) Token: 0x0600284B RID: 10315 RVA: 0x000F14AC File Offset: 0x000EF6AC
		bool IDtdAttributeInfo.IsDeclaredInExternal
		{
			get
			{
				return base.IsDeclaredInExternal;
			}
		}

		// Token: 0x17000869 RID: 2153
		// (get) Token: 0x0600284C RID: 10316 RVA: 0x000F14B4 File Offset: 0x000EF6B4
		bool IDtdAttributeInfo.IsXmlAttribute
		{
			get
			{
				return this.Reserved > SchemaAttDef.Reserve.None;
			}
		}

		// Token: 0x1700086A RID: 2154
		// (get) Token: 0x0600284D RID: 10317 RVA: 0x000F14BF File Offset: 0x000EF6BF
		string IDtdDefaultAttributeInfo.DefaultValueExpanded
		{
			get
			{
				return this.DefaultValueExpanded;
			}
		}

		// Token: 0x1700086B RID: 2155
		// (get) Token: 0x0600284E RID: 10318 RVA: 0x000F14C7 File Offset: 0x000EF6C7
		object IDtdDefaultAttributeInfo.DefaultValueTyped
		{
			get
			{
				return base.DefaultValueTyped;
			}
		}

		// Token: 0x1700086C RID: 2156
		// (get) Token: 0x0600284F RID: 10319 RVA: 0x000F14CF File Offset: 0x000EF6CF
		int IDtdDefaultAttributeInfo.ValueLineNumber
		{
			get
			{
				return this.ValueLineNumber;
			}
		}

		// Token: 0x1700086D RID: 2157
		// (get) Token: 0x06002850 RID: 10320 RVA: 0x000F14D7 File Offset: 0x000EF6D7
		int IDtdDefaultAttributeInfo.ValueLinePosition
		{
			get
			{
				return this.ValueLinePosition;
			}
		}

		// Token: 0x1700086E RID: 2158
		// (get) Token: 0x06002851 RID: 10321 RVA: 0x000F14DF File Offset: 0x000EF6DF
		// (set) Token: 0x06002852 RID: 10322 RVA: 0x000F14E7 File Offset: 0x000EF6E7
		internal int LinePosition
		{
			get
			{
				return this.linePos;
			}
			set
			{
				this.linePos = value;
			}
		}

		// Token: 0x1700086F RID: 2159
		// (get) Token: 0x06002853 RID: 10323 RVA: 0x000F14F0 File Offset: 0x000EF6F0
		// (set) Token: 0x06002854 RID: 10324 RVA: 0x000F14F8 File Offset: 0x000EF6F8
		internal int LineNumber
		{
			get
			{
				return this.lineNum;
			}
			set
			{
				this.lineNum = value;
			}
		}

		// Token: 0x17000870 RID: 2160
		// (get) Token: 0x06002855 RID: 10325 RVA: 0x000F1501 File Offset: 0x000EF701
		// (set) Token: 0x06002856 RID: 10326 RVA: 0x000F1509 File Offset: 0x000EF709
		internal int ValueLinePosition
		{
			get
			{
				return this.valueLinePos;
			}
			set
			{
				this.valueLinePos = value;
			}
		}

		// Token: 0x17000871 RID: 2161
		// (get) Token: 0x06002857 RID: 10327 RVA: 0x000F1512 File Offset: 0x000EF712
		// (set) Token: 0x06002858 RID: 10328 RVA: 0x000F151A File Offset: 0x000EF71A
		internal int ValueLineNumber
		{
			get
			{
				return this.valueLineNum;
			}
			set
			{
				this.valueLineNum = value;
			}
		}

		// Token: 0x17000872 RID: 2162
		// (get) Token: 0x06002859 RID: 10329 RVA: 0x000F1523 File Offset: 0x000EF723
		// (set) Token: 0x0600285A RID: 10330 RVA: 0x000F1539 File Offset: 0x000EF739
		internal string DefaultValueExpanded
		{
			get
			{
				if (this.defExpanded == null)
				{
					return string.Empty;
				}
				return this.defExpanded;
			}
			set
			{
				this.defExpanded = value;
			}
		}

		// Token: 0x17000873 RID: 2163
		// (get) Token: 0x0600285B RID: 10331 RVA: 0x000F1542 File Offset: 0x000EF742
		// (set) Token: 0x0600285C RID: 10332 RVA: 0x000F154F File Offset: 0x000EF74F
		internal XmlTokenizedType TokenizedType
		{
			get
			{
				return base.Datatype.TokenizedType;
			}
			set
			{
				base.Datatype = XmlSchemaDatatype.FromXmlTokenizedType(value);
			}
		}

		// Token: 0x17000874 RID: 2164
		// (get) Token: 0x0600285D RID: 10333 RVA: 0x000F155D File Offset: 0x000EF75D
		// (set) Token: 0x0600285E RID: 10334 RVA: 0x000F1565 File Offset: 0x000EF765
		internal SchemaAttDef.Reserve Reserved
		{
			get
			{
				return this.reserved;
			}
			set
			{
				this.reserved = value;
			}
		}

		// Token: 0x17000875 RID: 2165
		// (get) Token: 0x0600285F RID: 10335 RVA: 0x000F156E File Offset: 0x000EF76E
		internal bool DefaultValueChecked
		{
			get
			{
				return this.defaultValueChecked;
			}
		}

		// Token: 0x17000876 RID: 2166
		// (get) Token: 0x06002860 RID: 10336 RVA: 0x000F1576 File Offset: 0x000EF776
		// (set) Token: 0x06002861 RID: 10337 RVA: 0x000F157E File Offset: 0x000EF77E
		internal bool HasEntityRef
		{
			get
			{
				return this.hasEntityRef;
			}
			set
			{
				this.hasEntityRef = value;
			}
		}

		// Token: 0x17000877 RID: 2167
		// (get) Token: 0x06002862 RID: 10338 RVA: 0x000F1587 File Offset: 0x000EF787
		// (set) Token: 0x06002863 RID: 10339 RVA: 0x000F158F File Offset: 0x000EF78F
		internal XmlSchemaAttribute SchemaAttribute
		{
			get
			{
				return this.schemaAttribute;
			}
			set
			{
				this.schemaAttribute = value;
			}
		}

		// Token: 0x06002864 RID: 10340 RVA: 0x000F1598 File Offset: 0x000EF798
		internal void CheckXmlSpace(IValidationEventHandling validationEventHandling)
		{
			if (this.datatype.TokenizedType == XmlTokenizedType.ENUMERATION && this.values != null && this.values.Count <= 2)
			{
				string text = this.values[0].ToString();
				if (this.values.Count == 2)
				{
					string text2 = this.values[1].ToString();
					if ((text == "default" || text2 == "default") && (text == "preserve" || text2 == "preserve"))
					{
						return;
					}
				}
				else if (text == "default" || text == "preserve")
				{
					return;
				}
			}
			validationEventHandling.SendEvent(new XmlSchemaException("Invalid xml:space syntax.", string.Empty), XmlSeverityType.Error);
		}

		// Token: 0x06002865 RID: 10341 RVA: 0x000F166B File Offset: 0x000EF86B
		internal SchemaAttDef Clone()
		{
			return (SchemaAttDef)base.MemberwiseClone();
		}

		// Token: 0x04001ACA RID: 6858
		private string defExpanded;

		// Token: 0x04001ACB RID: 6859
		private int lineNum;

		// Token: 0x04001ACC RID: 6860
		private int linePos;

		// Token: 0x04001ACD RID: 6861
		private int valueLineNum;

		// Token: 0x04001ACE RID: 6862
		private int valueLinePos;

		// Token: 0x04001ACF RID: 6863
		private SchemaAttDef.Reserve reserved;

		// Token: 0x04001AD0 RID: 6864
		private bool defaultValueChecked;

		// Token: 0x04001AD1 RID: 6865
		private bool hasEntityRef;

		// Token: 0x04001AD2 RID: 6866
		private XmlSchemaAttribute schemaAttribute;

		// Token: 0x04001AD3 RID: 6867
		public static readonly SchemaAttDef Empty = new SchemaAttDef();

		// Token: 0x0200040F RID: 1039
		internal enum Reserve
		{
			// Token: 0x04001AD5 RID: 6869
			None,
			// Token: 0x04001AD6 RID: 6870
			XmlSpace,
			// Token: 0x04001AD7 RID: 6871
			XmlLang
		}
	}
}
