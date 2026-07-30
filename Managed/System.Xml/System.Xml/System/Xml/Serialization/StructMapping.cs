using System;

namespace System.Xml.Serialization
{
	// Token: 0x020002ED RID: 749
	internal class StructMapping : TypeMapping, INameScope
	{
		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x06001BF2 RID: 7154 RVA: 0x0009A074 File Offset: 0x00098274
		// (set) Token: 0x06001BF3 RID: 7155 RVA: 0x0009A07C File Offset: 0x0009827C
		internal StructMapping BaseMapping
		{
			get
			{
				return this.baseMapping;
			}
			set
			{
				this.baseMapping = value;
				if (!base.IsAnonymousType && this.baseMapping != null)
				{
					this.nextDerivedMapping = this.baseMapping.derivedMappings;
					this.baseMapping.derivedMappings = this;
				}
				if (value.isSequence && !this.isSequence)
				{
					this.isSequence = true;
					if (this.baseMapping.IsSequence)
					{
						for (StructMapping structMapping = this.derivedMappings; structMapping != null; structMapping = structMapping.NextDerivedMapping)
						{
							structMapping.SetSequence();
						}
					}
				}
			}
		}

		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x06001BF4 RID: 7156 RVA: 0x0009A0FA File Offset: 0x000982FA
		internal StructMapping DerivedMappings
		{
			get
			{
				return this.derivedMappings;
			}
		}

		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x06001BF5 RID: 7157 RVA: 0x0009A102 File Offset: 0x00098302
		internal bool IsFullyInitialized
		{
			get
			{
				return this.baseMapping != null && this.Members != null;
			}
		}

		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x06001BF6 RID: 7158 RVA: 0x0009A117 File Offset: 0x00098317
		internal NameTable LocalElements
		{
			get
			{
				if (this.elements == null)
				{
					this.elements = new NameTable();
				}
				return this.elements;
			}
		}

		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x06001BF7 RID: 7159 RVA: 0x0009A132 File Offset: 0x00098332
		internal NameTable LocalAttributes
		{
			get
			{
				if (this.attributes == null)
				{
					this.attributes = new NameTable();
				}
				return this.attributes;
			}
		}

		// Token: 0x17000571 RID: 1393
		object INameScope.this[string name, string ns]
		{
			get
			{
				object obj = this.LocalElements[name, ns];
				if (obj != null)
				{
					return obj;
				}
				if (this.baseMapping != null)
				{
					return ((INameScope)this.baseMapping)[name, ns];
				}
				return null;
			}
			set
			{
				this.LocalElements[name, ns] = value;
			}
		}

		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x06001BFA RID: 7162 RVA: 0x0009A197 File Offset: 0x00098397
		internal StructMapping NextDerivedMapping
		{
			get
			{
				return this.nextDerivedMapping;
			}
		}

		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x06001BFB RID: 7163 RVA: 0x0009A19F File Offset: 0x0009839F
		internal bool HasSimpleContent
		{
			get
			{
				return this.hasSimpleContent;
			}
		}

		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x06001BFC RID: 7164 RVA: 0x0009A1A8 File Offset: 0x000983A8
		internal bool HasXmlnsMember
		{
			get
			{
				for (StructMapping structMapping = this; structMapping != null; structMapping = structMapping.BaseMapping)
				{
					if (structMapping.XmlnsMember != null)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x06001BFD RID: 7165 RVA: 0x0009A1CE File Offset: 0x000983CE
		// (set) Token: 0x06001BFE RID: 7166 RVA: 0x0009A1D6 File Offset: 0x000983D6
		internal MemberMapping[] Members
		{
			get
			{
				return this.members;
			}
			set
			{
				this.members = value;
			}
		}

		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x06001BFF RID: 7167 RVA: 0x0009A1DF File Offset: 0x000983DF
		// (set) Token: 0x06001C00 RID: 7168 RVA: 0x0009A1E7 File Offset: 0x000983E7
		internal MemberMapping XmlnsMember
		{
			get
			{
				return this.xmlnsMember;
			}
			set
			{
				this.xmlnsMember = value;
			}
		}

		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x06001C01 RID: 7169 RVA: 0x0009A1F0 File Offset: 0x000983F0
		// (set) Token: 0x06001C02 RID: 7170 RVA: 0x0009A1F8 File Offset: 0x000983F8
		internal bool IsOpenModel
		{
			get
			{
				return this.openModel;
			}
			set
			{
				this.openModel = value;
			}
		}

		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x06001C03 RID: 7171 RVA: 0x0009A201 File Offset: 0x00098401
		// (set) Token: 0x06001C04 RID: 7172 RVA: 0x0009A21C File Offset: 0x0009841C
		internal CodeIdentifiers Scope
		{
			get
			{
				if (this.scope == null)
				{
					this.scope = new CodeIdentifiers();
				}
				return this.scope;
			}
			set
			{
				this.scope = value;
			}
		}

		// Token: 0x06001C05 RID: 7173 RVA: 0x0009A228 File Offset: 0x00098428
		internal MemberMapping FindDeclaringMapping(MemberMapping member, out StructMapping declaringMapping, string parent)
		{
			declaringMapping = null;
			if (this.BaseMapping != null)
			{
				MemberMapping memberMapping = this.BaseMapping.FindDeclaringMapping(member, out declaringMapping, parent);
				if (memberMapping != null)
				{
					return memberMapping;
				}
			}
			if (this.members == null)
			{
				return null;
			}
			int i = 0;
			while (i < this.members.Length)
			{
				if (this.members[i].Name == member.Name)
				{
					if (this.members[i].TypeDesc != member.TypeDesc)
					{
						throw new InvalidOperationException(Res.GetString("Member {0}.{1} of type {2} hides base class member {3}.{4} of type {5}. Use XmlElementAttribute or XmlAttributeAttribute to specify a new name.", new object[]
						{
							parent,
							member.Name,
							member.TypeDesc.FullName,
							base.TypeName,
							this.members[i].Name,
							this.members[i].TypeDesc.FullName
						}));
					}
					if (!this.members[i].Match(member))
					{
						throw new InvalidOperationException(Res.GetString("Member '{0}.{1}' hides inherited member '{2}.{3}', but has different custom attributes.", new object[]
						{
							parent,
							member.Name,
							base.TypeName,
							this.members[i].Name
						}));
					}
					declaringMapping = this;
					return this.members[i];
				}
				else
				{
					i++;
				}
			}
			return null;
		}

		// Token: 0x06001C06 RID: 7174 RVA: 0x0009A364 File Offset: 0x00098564
		internal bool Declares(MemberMapping member, string parent)
		{
			StructMapping structMapping;
			return this.FindDeclaringMapping(member, out structMapping, parent) != null;
		}

		// Token: 0x06001C07 RID: 7175 RVA: 0x0009A380 File Offset: 0x00098580
		internal void SetContentModel(TextAccessor text, bool hasElements)
		{
			if (this.BaseMapping == null || this.BaseMapping.TypeDesc.IsRoot)
			{
				this.hasSimpleContent = !hasElements && text != null && !text.Mapping.IsList;
			}
			else if (this.BaseMapping.HasSimpleContent)
			{
				if (text != null || hasElements)
				{
					throw new InvalidOperationException(Res.GetString("Cannot serialize object of type '{0}'. Base type '{1}' has simpleContent and can only be extended by adding XmlAttribute elements. Please consider changing XmlText member of the base class to string array.", new object[]
					{
						base.TypeDesc.FullName,
						this.BaseMapping.TypeDesc.FullName
					}));
				}
				this.hasSimpleContent = true;
			}
			else
			{
				this.hasSimpleContent = false;
			}
			if (!this.hasSimpleContent && text != null && !text.Mapping.TypeDesc.CanBeTextValue)
			{
				throw new InvalidOperationException(Res.GetString("Cannot serialize object of type '{0}'. Consider changing type of XmlText member '{0}.{1}' from {2} to string or string array.", new object[]
				{
					base.TypeDesc.FullName,
					text.Name,
					text.Mapping.TypeDesc.FullName
				}));
			}
		}

		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x06001C08 RID: 7176 RVA: 0x0009A47F File Offset: 0x0009867F
		internal bool HasElements
		{
			get
			{
				return this.elements != null && this.elements.Values.Count > 0;
			}
		}

		// Token: 0x06001C09 RID: 7177 RVA: 0x0009A4A0 File Offset: 0x000986A0
		internal bool HasExplicitSequence()
		{
			if (this.members != null)
			{
				for (int i = 0; i < this.members.Length; i++)
				{
					if (this.members[i].IsParticle && this.members[i].IsSequence)
					{
						return true;
					}
				}
			}
			return this.baseMapping != null && this.baseMapping.HasExplicitSequence();
		}

		// Token: 0x06001C0A RID: 7178 RVA: 0x0009A500 File Offset: 0x00098700
		internal void SetSequence()
		{
			if (base.TypeDesc.IsRoot)
			{
				return;
			}
			StructMapping structMapping = this;
			while (!structMapping.BaseMapping.IsSequence && structMapping.BaseMapping != null && !structMapping.BaseMapping.TypeDesc.IsRoot)
			{
				structMapping = structMapping.BaseMapping;
			}
			structMapping.IsSequence = true;
			for (StructMapping structMapping2 = structMapping.DerivedMappings; structMapping2 != null; structMapping2 = structMapping2.NextDerivedMapping)
			{
				structMapping2.SetSequence();
			}
		}

		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x06001C0B RID: 7179 RVA: 0x0009A56D File Offset: 0x0009876D
		// (set) Token: 0x06001C0C RID: 7180 RVA: 0x0009A587 File Offset: 0x00098787
		internal bool IsSequence
		{
			get
			{
				return this.isSequence && !base.TypeDesc.IsRoot;
			}
			set
			{
				this.isSequence = value;
			}
		}

		// Token: 0x0400161C RID: 5660
		private MemberMapping[] members;

		// Token: 0x0400161D RID: 5661
		private StructMapping baseMapping;

		// Token: 0x0400161E RID: 5662
		private StructMapping derivedMappings;

		// Token: 0x0400161F RID: 5663
		private StructMapping nextDerivedMapping;

		// Token: 0x04001620 RID: 5664
		private MemberMapping xmlnsMember;

		// Token: 0x04001621 RID: 5665
		private bool hasSimpleContent;

		// Token: 0x04001622 RID: 5666
		private bool openModel;

		// Token: 0x04001623 RID: 5667
		private bool isSequence;

		// Token: 0x04001624 RID: 5668
		private NameTable elements;

		// Token: 0x04001625 RID: 5669
		private NameTable attributes;

		// Token: 0x04001626 RID: 5670
		private CodeIdentifiers scope;
	}
}
