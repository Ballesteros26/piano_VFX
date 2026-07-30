using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the group element from XML Schema as specified by the World Wide Web Consortium (W3C). This class defines groups at the schema level that are referenced from the complex types. It groups a set of element declarations so that they can be incorporated as a group into complex type definitions.</summary>
	// Token: 0x02000461 RID: 1121
	public class XmlSchemaGroup : XmlSchemaAnnotated
	{
		/// <summary>Gets or sets the name of the schema group.</summary>
		/// <returns>The name of the schema group.</returns>
		// Token: 0x1700098D RID: 2445
		// (get) Token: 0x06002C39 RID: 11321 RVA: 0x00106EF7 File Offset: 0x001050F7
		// (set) Token: 0x06002C3A RID: 11322 RVA: 0x00106EFF File Offset: 0x001050FF
		[XmlAttribute("name")]
		public string Name
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

		/// <summary>Gets or sets one of the <see cref="T:System.Xml.Schema.XmlSchemaChoice" />, <see cref="T:System.Xml.Schema.XmlSchemaAll" />, or <see cref="T:System.Xml.Schema.XmlSchemaSequence" /> classes.</summary>
		/// <returns>One of the <see cref="T:System.Xml.Schema.XmlSchemaChoice" />, <see cref="T:System.Xml.Schema.XmlSchemaAll" />, or <see cref="T:System.Xml.Schema.XmlSchemaSequence" /> classes.</returns>
		// Token: 0x1700098E RID: 2446
		// (get) Token: 0x06002C3B RID: 11323 RVA: 0x00106F08 File Offset: 0x00105108
		// (set) Token: 0x06002C3C RID: 11324 RVA: 0x00106F10 File Offset: 0x00105110
		[XmlElement("sequence", typeof(XmlSchemaSequence))]
		[XmlElement("choice", typeof(XmlSchemaChoice))]
		[XmlElement("all", typeof(XmlSchemaAll))]
		public XmlSchemaGroupBase Particle
		{
			get
			{
				return this.particle;
			}
			set
			{
				this.particle = value;
			}
		}

		/// <summary>Gets the qualified name of the schema group.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlQualifiedName" /> object representing the qualified name of the schema group.</returns>
		// Token: 0x1700098F RID: 2447
		// (get) Token: 0x06002C3D RID: 11325 RVA: 0x00106F19 File Offset: 0x00105119
		[XmlIgnore]
		public XmlQualifiedName QualifiedName
		{
			get
			{
				return this.qname;
			}
		}

		// Token: 0x17000990 RID: 2448
		// (get) Token: 0x06002C3E RID: 11326 RVA: 0x00106F21 File Offset: 0x00105121
		// (set) Token: 0x06002C3F RID: 11327 RVA: 0x00106F29 File Offset: 0x00105129
		[XmlIgnore]
		internal XmlSchemaParticle CanonicalParticle
		{
			get
			{
				return this.canonicalParticle;
			}
			set
			{
				this.canonicalParticle = value;
			}
		}

		// Token: 0x17000991 RID: 2449
		// (get) Token: 0x06002C40 RID: 11328 RVA: 0x00106F32 File Offset: 0x00105132
		// (set) Token: 0x06002C41 RID: 11329 RVA: 0x00106F3A File Offset: 0x0010513A
		[XmlIgnore]
		internal XmlSchemaGroup Redefined
		{
			get
			{
				return this.redefined;
			}
			set
			{
				this.redefined = value;
			}
		}

		// Token: 0x17000992 RID: 2450
		// (get) Token: 0x06002C42 RID: 11330 RVA: 0x00106F43 File Offset: 0x00105143
		// (set) Token: 0x06002C43 RID: 11331 RVA: 0x00106F4B File Offset: 0x0010514B
		[XmlIgnore]
		internal int SelfReferenceCount
		{
			get
			{
				return this.selfReferenceCount;
			}
			set
			{
				this.selfReferenceCount = value;
			}
		}

		// Token: 0x17000993 RID: 2451
		// (get) Token: 0x06002C44 RID: 11332 RVA: 0x00106F54 File Offset: 0x00105154
		// (set) Token: 0x06002C45 RID: 11333 RVA: 0x00106F5C File Offset: 0x0010515C
		[XmlIgnore]
		internal override string NameAttribute
		{
			get
			{
				return this.Name;
			}
			set
			{
				this.Name = value;
			}
		}

		// Token: 0x06002C46 RID: 11334 RVA: 0x00106F65 File Offset: 0x00105165
		internal void SetQualifiedName(XmlQualifiedName value)
		{
			this.qname = value;
		}

		// Token: 0x06002C47 RID: 11335 RVA: 0x00106F6E File Offset: 0x0010516E
		internal override XmlSchemaObject Clone()
		{
			return this.Clone(null);
		}

		// Token: 0x06002C48 RID: 11336 RVA: 0x00106F78 File Offset: 0x00105178
		internal XmlSchemaObject Clone(XmlSchema parentSchema)
		{
			XmlSchemaGroup xmlSchemaGroup = (XmlSchemaGroup)base.MemberwiseClone();
			if (XmlSchemaComplexType.HasParticleRef(this.particle, parentSchema))
			{
				xmlSchemaGroup.particle = XmlSchemaComplexType.CloneParticle(this.particle, parentSchema) as XmlSchemaGroupBase;
			}
			xmlSchemaGroup.canonicalParticle = XmlSchemaParticle.Empty;
			return xmlSchemaGroup;
		}

		// Token: 0x04001DB9 RID: 7609
		private string name;

		// Token: 0x04001DBA RID: 7610
		private XmlSchemaGroupBase particle;

		// Token: 0x04001DBB RID: 7611
		private XmlSchemaParticle canonicalParticle;

		// Token: 0x04001DBC RID: 7612
		private XmlQualifiedName qname = XmlQualifiedName.Empty;

		// Token: 0x04001DBD RID: 7613
		private XmlSchemaGroup redefined;

		// Token: 0x04001DBE RID: 7614
		private int selfReferenceCount;
	}
}
