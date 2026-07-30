using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the include element from XML Schema as specified by the World Wide Web Consortium (W3C). This class is used to include declarations and definitions from an external schema. The included declarations and definitions are then available for processing in the containing schema.</summary>
	// Token: 0x0200046A RID: 1130
	public class XmlSchemaInclude : XmlSchemaExternal
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaInclude" /> class.</summary>
		// Token: 0x06002C6E RID: 11374 RVA: 0x00107124 File Offset: 0x00105324
		public XmlSchemaInclude()
		{
			base.Compositor = Compositor.Include;
		}

		/// <summary>Gets or sets the annotation property.</summary>
		/// <returns>The annotation.</returns>
		// Token: 0x170009A2 RID: 2466
		// (get) Token: 0x06002C6F RID: 11375 RVA: 0x00107133 File Offset: 0x00105333
		// (set) Token: 0x06002C70 RID: 11376 RVA: 0x0010713B File Offset: 0x0010533B
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

		// Token: 0x06002C71 RID: 11377 RVA: 0x0010713B File Offset: 0x0010533B
		internal override void AddAnnotation(XmlSchemaAnnotation annotation)
		{
			this.annotation = annotation;
		}

		// Token: 0x04001DCB RID: 7627
		private XmlSchemaAnnotation annotation;
	}
}
