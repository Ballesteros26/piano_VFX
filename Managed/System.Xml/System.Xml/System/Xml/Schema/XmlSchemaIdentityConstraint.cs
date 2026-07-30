using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Class for the identity constraints: key, keyref, and unique elements.</summary>
	// Token: 0x02000464 RID: 1124
	public class XmlSchemaIdentityConstraint : XmlSchemaAnnotated
	{
		/// <summary>Gets or sets the name of the identity constraint.</summary>
		/// <returns>The name of the identity constraint.</returns>
		// Token: 0x17000998 RID: 2456
		// (get) Token: 0x06002C54 RID: 11348 RVA: 0x0010702B File Offset: 0x0010522B
		// (set) Token: 0x06002C55 RID: 11349 RVA: 0x00107033 File Offset: 0x00105233
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

		/// <summary>Gets or sets the XPath expression selector element.</summary>
		/// <returns>The XPath expression selector element.</returns>
		// Token: 0x17000999 RID: 2457
		// (get) Token: 0x06002C56 RID: 11350 RVA: 0x0010703C File Offset: 0x0010523C
		// (set) Token: 0x06002C57 RID: 11351 RVA: 0x00107044 File Offset: 0x00105244
		[XmlElement("selector", typeof(XmlSchemaXPath))]
		public XmlSchemaXPath Selector
		{
			get
			{
				return this.selector;
			}
			set
			{
				this.selector = value;
			}
		}

		/// <summary>Gets the collection of fields that apply as children for the XML Path Language (XPath) expression selector.</summary>
		/// <returns>The collection of fields.</returns>
		// Token: 0x1700099A RID: 2458
		// (get) Token: 0x06002C58 RID: 11352 RVA: 0x0010704D File Offset: 0x0010524D
		[XmlElement("field", typeof(XmlSchemaXPath))]
		public XmlSchemaObjectCollection Fields
		{
			get
			{
				return this.fields;
			}
		}

		/// <summary>Gets the qualified name of the identity constraint, which holds the post-compilation value of the QualifiedName property.</summary>
		/// <returns>The post-compilation value of the QualifiedName property.</returns>
		// Token: 0x1700099B RID: 2459
		// (get) Token: 0x06002C59 RID: 11353 RVA: 0x00107055 File Offset: 0x00105255
		[XmlIgnore]
		public XmlQualifiedName QualifiedName
		{
			get
			{
				return this.qualifiedName;
			}
		}

		// Token: 0x06002C5A RID: 11354 RVA: 0x0010705D File Offset: 0x0010525D
		internal void SetQualifiedName(XmlQualifiedName value)
		{
			this.qualifiedName = value;
		}

		// Token: 0x1700099C RID: 2460
		// (get) Token: 0x06002C5B RID: 11355 RVA: 0x00107066 File Offset: 0x00105266
		// (set) Token: 0x06002C5C RID: 11356 RVA: 0x0010706E File Offset: 0x0010526E
		[XmlIgnore]
		internal CompiledIdentityConstraint CompiledConstraint
		{
			get
			{
				return this.compiledConstraint;
			}
			set
			{
				this.compiledConstraint = value;
			}
		}

		// Token: 0x1700099D RID: 2461
		// (get) Token: 0x06002C5D RID: 11357 RVA: 0x00107077 File Offset: 0x00105277
		// (set) Token: 0x06002C5E RID: 11358 RVA: 0x0010707F File Offset: 0x0010527F
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

		// Token: 0x04001DC2 RID: 7618
		private string name;

		// Token: 0x04001DC3 RID: 7619
		private XmlSchemaXPath selector;

		// Token: 0x04001DC4 RID: 7620
		private XmlSchemaObjectCollection fields = new XmlSchemaObjectCollection();

		// Token: 0x04001DC5 RID: 7621
		private XmlQualifiedName qualifiedName = XmlQualifiedName.Empty;

		// Token: 0x04001DC6 RID: 7622
		private CompiledIdentityConstraint compiledConstraint;
	}
}
