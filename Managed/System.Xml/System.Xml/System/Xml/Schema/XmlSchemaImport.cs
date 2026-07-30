using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the import element from XML Schema as specified by the World Wide Web Consortium (W3C). This class is used to import schema components from other schemas.</summary>
	// Token: 0x02000469 RID: 1129
	public class XmlSchemaImport : XmlSchemaExternal
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaImport" /> class.</summary>
		// Token: 0x06002C68 RID: 11368 RVA: 0x001070F3 File Offset: 0x001052F3
		public XmlSchemaImport()
		{
			base.Compositor = Compositor.Import;
		}

		/// <summary>Gets or sets the target namespace for the imported schema as a Uniform Resource Identifier (URI) reference.</summary>
		/// <returns>The target namespace for the imported schema as a URI reference.Optional.</returns>
		// Token: 0x170009A0 RID: 2464
		// (get) Token: 0x06002C69 RID: 11369 RVA: 0x00107102 File Offset: 0x00105302
		// (set) Token: 0x06002C6A RID: 11370 RVA: 0x0010710A File Offset: 0x0010530A
		[XmlAttribute("namespace", DataType = "anyURI")]
		public string Namespace
		{
			get
			{
				return this.ns;
			}
			set
			{
				this.ns = value;
			}
		}

		/// <summary>Gets or sets the annotation property.</summary>
		/// <returns>The annotation.</returns>
		// Token: 0x170009A1 RID: 2465
		// (get) Token: 0x06002C6B RID: 11371 RVA: 0x00107113 File Offset: 0x00105313
		// (set) Token: 0x06002C6C RID: 11372 RVA: 0x0010711B File Offset: 0x0010531B
		[XmlElement("annotation", typeof(XmlSchemaAnnotation))]
		public XmlSchemaAnnotation Annotation
		{
			get
			{
				return this.annotation;
			}
			set
			{
				this.annotation = value;
			}
		}

		// Token: 0x06002C6D RID: 11373 RVA: 0x0010711B File Offset: 0x0010531B
		internal override void AddAnnotation(XmlSchemaAnnotation annotation)
		{
			this.annotation = annotation;
		}

		// Token: 0x04001DC9 RID: 7625
		private string ns;

		// Token: 0x04001DCA RID: 7626
		private XmlSchemaAnnotation annotation;
	}
}
