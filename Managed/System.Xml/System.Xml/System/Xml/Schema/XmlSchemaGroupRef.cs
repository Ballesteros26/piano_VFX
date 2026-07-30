using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the group element with ref attribute from the XML Schema as specified by the World Wide Web Consortium (W3C). This class is used within complex types that reference a group defined at the schema level.</summary>
	// Token: 0x02000463 RID: 1123
	public class XmlSchemaGroupRef : XmlSchemaParticle
	{
		/// <summary>Gets or sets the name of a group defined in this schema (or another schema indicated by the specified namespace).</summary>
		/// <returns>The name of a group defined in this schema.</returns>
		// Token: 0x17000995 RID: 2453
		// (get) Token: 0x06002C4D RID: 11341 RVA: 0x00106FD5 File Offset: 0x001051D5
		// (set) Token: 0x06002C4E RID: 11342 RVA: 0x00106FDD File Offset: 0x001051DD
		[XmlAttribute("ref")]
		public XmlQualifiedName RefName
		{
			get
			{
				return this.refName;
			}
			set
			{
				this.refName = ((value == null) ? XmlQualifiedName.Empty : value);
			}
		}

		/// <summary>Gets one of the <see cref="T:System.Xml.Schema.XmlSchemaChoice" />, <see cref="T:System.Xml.Schema.XmlSchemaAll" />, or <see cref="T:System.Xml.Schema.XmlSchemaSequence" /> classes, which holds the post-compilation value of the Particle property.</summary>
		/// <returns>The post-compilation value of the Particle property, which is one of the <see cref="T:System.Xml.Schema.XmlSchemaChoice" />, <see cref="T:System.Xml.Schema.XmlSchemaAll" />, or <see cref="T:System.Xml.Schema.XmlSchemaSequence" /> classes.</returns>
		// Token: 0x17000996 RID: 2454
		// (get) Token: 0x06002C4F RID: 11343 RVA: 0x00106FF6 File Offset: 0x001051F6
		[XmlIgnore]
		public XmlSchemaGroupBase Particle
		{
			get
			{
				return this.particle;
			}
		}

		// Token: 0x06002C50 RID: 11344 RVA: 0x00106FFE File Offset: 0x001051FE
		internal void SetParticle(XmlSchemaGroupBase value)
		{
			this.particle = value;
		}

		// Token: 0x17000997 RID: 2455
		// (get) Token: 0x06002C51 RID: 11345 RVA: 0x00107007 File Offset: 0x00105207
		// (set) Token: 0x06002C52 RID: 11346 RVA: 0x0010700F File Offset: 0x0010520F
		[XmlIgnore]
		internal XmlSchemaGroup Redefined
		{
			get
			{
				return this.refined;
			}
			set
			{
				this.refined = value;
			}
		}

		// Token: 0x04001DBF RID: 7615
		private XmlQualifiedName refName = XmlQualifiedName.Empty;

		// Token: 0x04001DC0 RID: 7616
		private XmlSchemaGroupBase particle;

		// Token: 0x04001DC1 RID: 7617
		private XmlSchemaGroup refined;
	}
}
