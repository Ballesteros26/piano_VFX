using System;
using System.Security.Permissions;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the root class for the Xml schema object model hierarchy and serves as a base class for classes such as the <see cref="T:System.Xml.Schema.XmlSchema" /> class.</summary>
	// Token: 0x0200046D RID: 1133
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public abstract class XmlSchemaObject
	{
		/// <summary>Gets or sets the line number in the file to which the schema element refers.</summary>
		/// <returns>The line number.</returns>
		// Token: 0x170009B3 RID: 2483
		// (get) Token: 0x06002C93 RID: 11411 RVA: 0x00107322 File Offset: 0x00105522
		// (set) Token: 0x06002C94 RID: 11412 RVA: 0x0010732A File Offset: 0x0010552A
		[XmlIgnore]
		public int LineNumber
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

		/// <summary>Gets or sets the line position in the file to which the schema element refers.</summary>
		/// <returns>The line position.</returns>
		// Token: 0x170009B4 RID: 2484
		// (get) Token: 0x06002C95 RID: 11413 RVA: 0x00107333 File Offset: 0x00105533
		// (set) Token: 0x06002C96 RID: 11414 RVA: 0x0010733B File Offset: 0x0010553B
		[XmlIgnore]
		public int LinePosition
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

		/// <summary>Gets or sets the source location for the file that loaded the schema.</summary>
		/// <returns>The source location (URI) for the file.</returns>
		// Token: 0x170009B5 RID: 2485
		// (get) Token: 0x06002C97 RID: 11415 RVA: 0x00107344 File Offset: 0x00105544
		// (set) Token: 0x06002C98 RID: 11416 RVA: 0x0010734C File Offset: 0x0010554C
		[XmlIgnore]
		public string SourceUri
		{
			get
			{
				return this.sourceUri;
			}
			set
			{
				this.sourceUri = value;
			}
		}

		/// <summary>Gets or sets the parent of this <see cref="T:System.Xml.Schema.XmlSchemaObject" />.</summary>
		/// <returns>The parent <see cref="T:System.Xml.Schema.XmlSchemaObject" /> of this <see cref="T:System.Xml.Schema.XmlSchemaObject" />.</returns>
		// Token: 0x170009B6 RID: 2486
		// (get) Token: 0x06002C99 RID: 11417 RVA: 0x00107355 File Offset: 0x00105555
		// (set) Token: 0x06002C9A RID: 11418 RVA: 0x0010735D File Offset: 0x0010555D
		[XmlIgnore]
		public XmlSchemaObject Parent
		{
			get
			{
				return this.parent;
			}
			set
			{
				this.parent = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Xml.Serialization.XmlSerializerNamespaces" /> to use with this schema object.</summary>
		/// <returns>The <see cref="T:System.Xml.Serialization.XmlSerializerNamespaces" /> property for the schema object.</returns>
		// Token: 0x170009B7 RID: 2487
		// (get) Token: 0x06002C9B RID: 11419 RVA: 0x00107366 File Offset: 0x00105566
		// (set) Token: 0x06002C9C RID: 11420 RVA: 0x00107381 File Offset: 0x00105581
		[XmlNamespaceDeclarations]
		public XmlSerializerNamespaces Namespaces
		{
			get
			{
				if (this.namespaces == null)
				{
					this.namespaces = new XmlSerializerNamespaces();
				}
				return this.namespaces;
			}
			set
			{
				this.namespaces = value;
			}
		}

		// Token: 0x06002C9D RID: 11421 RVA: 0x00002F50 File Offset: 0x00001150
		internal virtual void OnAdd(XmlSchemaObjectCollection container, object item)
		{
		}

		// Token: 0x06002C9E RID: 11422 RVA: 0x00002F50 File Offset: 0x00001150
		internal virtual void OnRemove(XmlSchemaObjectCollection container, object item)
		{
		}

		// Token: 0x06002C9F RID: 11423 RVA: 0x00002F50 File Offset: 0x00001150
		internal virtual void OnClear(XmlSchemaObjectCollection container)
		{
		}

		// Token: 0x170009B8 RID: 2488
		// (get) Token: 0x06002CA0 RID: 11424 RVA: 0x0000365F File Offset: 0x0000185F
		// (set) Token: 0x06002CA1 RID: 11425 RVA: 0x00002F50 File Offset: 0x00001150
		[XmlIgnore]
		internal virtual string IdAttribute
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x06002CA2 RID: 11426 RVA: 0x00002F50 File Offset: 0x00001150
		internal virtual void SetUnhandledAttributes(XmlAttribute[] moreAttributes)
		{
		}

		// Token: 0x06002CA3 RID: 11427 RVA: 0x00002F50 File Offset: 0x00001150
		internal virtual void AddAnnotation(XmlSchemaAnnotation annotation)
		{
		}

		// Token: 0x170009B9 RID: 2489
		// (get) Token: 0x06002CA4 RID: 11428 RVA: 0x0000365F File Offset: 0x0000185F
		// (set) Token: 0x06002CA5 RID: 11429 RVA: 0x00002F50 File Offset: 0x00001150
		[XmlIgnore]
		internal virtual string NameAttribute
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x170009BA RID: 2490
		// (get) Token: 0x06002CA6 RID: 11430 RVA: 0x0010738A File Offset: 0x0010558A
		// (set) Token: 0x06002CA7 RID: 11431 RVA: 0x00107392 File Offset: 0x00105592
		[XmlIgnore]
		internal bool IsProcessing
		{
			get
			{
				return this.isProcessing;
			}
			set
			{
				this.isProcessing = value;
			}
		}

		// Token: 0x06002CA8 RID: 11432 RVA: 0x0010739B File Offset: 0x0010559B
		internal virtual XmlSchemaObject Clone()
		{
			return (XmlSchemaObject)base.MemberwiseClone();
		}

		// Token: 0x04001DD8 RID: 7640
		private int lineNum;

		// Token: 0x04001DD9 RID: 7641
		private int linePos;

		// Token: 0x04001DDA RID: 7642
		private string sourceUri;

		// Token: 0x04001DDB RID: 7643
		private XmlSerializerNamespaces namespaces;

		// Token: 0x04001DDC RID: 7644
		private XmlSchemaObject parent;

		// Token: 0x04001DDD RID: 7645
		private bool isProcessing;
	}
}
