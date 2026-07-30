using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x020003A6 RID: 934
	internal class ContentValidator
	{
		// Token: 0x0600255D RID: 9565 RVA: 0x000E0B41 File Offset: 0x000DED41
		public ContentValidator(XmlSchemaContentType contentType)
		{
			this.contentType = contentType;
			this.isEmptiable = true;
		}

		// Token: 0x0600255E RID: 9566 RVA: 0x000E0B57 File Offset: 0x000DED57
		protected ContentValidator(XmlSchemaContentType contentType, bool isOpen, bool isEmptiable)
		{
			this.contentType = contentType;
			this.isOpen = isOpen;
			this.isEmptiable = isEmptiable;
		}

		// Token: 0x1700077E RID: 1918
		// (get) Token: 0x0600255F RID: 9567 RVA: 0x000E0B74 File Offset: 0x000DED74
		public XmlSchemaContentType ContentType
		{
			get
			{
				return this.contentType;
			}
		}

		// Token: 0x1700077F RID: 1919
		// (get) Token: 0x06002560 RID: 9568 RVA: 0x000E0B7C File Offset: 0x000DED7C
		public bool PreserveWhitespace
		{
			get
			{
				return this.contentType == XmlSchemaContentType.TextOnly || this.contentType == XmlSchemaContentType.Mixed;
			}
		}

		// Token: 0x17000780 RID: 1920
		// (get) Token: 0x06002561 RID: 9569 RVA: 0x000E0B91 File Offset: 0x000DED91
		public virtual bool IsEmptiable
		{
			get
			{
				return this.isEmptiable;
			}
		}

		// Token: 0x17000781 RID: 1921
		// (get) Token: 0x06002562 RID: 9570 RVA: 0x000E0B99 File Offset: 0x000DED99
		// (set) Token: 0x06002563 RID: 9571 RVA: 0x000E0BB4 File Offset: 0x000DEDB4
		public bool IsOpen
		{
			get
			{
				return this.contentType != XmlSchemaContentType.TextOnly && this.contentType != XmlSchemaContentType.Empty && this.isOpen;
			}
			set
			{
				this.isOpen = value;
			}
		}

		// Token: 0x06002564 RID: 9572 RVA: 0x00002F50 File Offset: 0x00001150
		public virtual void InitValidation(ValidationState context)
		{
		}

		// Token: 0x06002565 RID: 9573 RVA: 0x000E0BBD File Offset: 0x000DEDBD
		public virtual object ValidateElement(XmlQualifiedName name, ValidationState context, out int errorCode)
		{
			if (this.contentType == XmlSchemaContentType.TextOnly || this.contentType == XmlSchemaContentType.Empty)
			{
				context.NeedValidateChildren = false;
			}
			errorCode = -1;
			return null;
		}

		// Token: 0x06002566 RID: 9574 RVA: 0x00003242 File Offset: 0x00001442
		public virtual bool CompleteValidation(ValidationState context)
		{
			return true;
		}

		// Token: 0x06002567 RID: 9575 RVA: 0x0000365F File Offset: 0x0000185F
		public virtual ArrayList ExpectedElements(ValidationState context, bool isRequiredOnly)
		{
			return null;
		}

		// Token: 0x06002568 RID: 9576 RVA: 0x0000365F File Offset: 0x0000185F
		public virtual ArrayList ExpectedParticles(ValidationState context, bool isRequiredOnly, XmlSchemaSet schemaSet)
		{
			return null;
		}

		// Token: 0x06002569 RID: 9577 RVA: 0x000E0BDB File Offset: 0x000DEDDB
		public static void AddParticleToExpected(XmlSchemaParticle p, XmlSchemaSet schemaSet, ArrayList particles)
		{
			ContentValidator.AddParticleToExpected(p, schemaSet, particles, false);
		}

		// Token: 0x0600256A RID: 9578 RVA: 0x000E0BE8 File Offset: 0x000DEDE8
		public static void AddParticleToExpected(XmlSchemaParticle p, XmlSchemaSet schemaSet, ArrayList particles, bool global)
		{
			if (!particles.Contains(p))
			{
				particles.Add(p);
			}
			XmlSchemaElement xmlSchemaElement = p as XmlSchemaElement;
			if (xmlSchemaElement != null && (global || !xmlSchemaElement.RefName.IsEmpty))
			{
				XmlSchemaSubstitutionGroup xmlSchemaSubstitutionGroup = (XmlSchemaSubstitutionGroup)schemaSet.SubstitutionGroups[xmlSchemaElement.QualifiedName];
				if (xmlSchemaSubstitutionGroup != null)
				{
					for (int i = 0; i < xmlSchemaSubstitutionGroup.Members.Count; i++)
					{
						XmlSchemaElement xmlSchemaElement2 = (XmlSchemaElement)xmlSchemaSubstitutionGroup.Members[i];
						if (!xmlSchemaElement.QualifiedName.Equals(xmlSchemaElement2.QualifiedName) && !particles.Contains(xmlSchemaElement2))
						{
							particles.Add(xmlSchemaElement2);
						}
					}
				}
			}
		}

		// Token: 0x04001938 RID: 6456
		private XmlSchemaContentType contentType;

		// Token: 0x04001939 RID: 6457
		private bool isOpen;

		// Token: 0x0400193A RID: 6458
		private bool isEmptiable;

		// Token: 0x0400193B RID: 6459
		public static readonly ContentValidator Empty = new ContentValidator(XmlSchemaContentType.Empty);

		// Token: 0x0400193C RID: 6460
		public static readonly ContentValidator TextOnly = new ContentValidator(XmlSchemaContentType.TextOnly, false, false);

		// Token: 0x0400193D RID: 6461
		public static readonly ContentValidator Mixed = new ContentValidator(XmlSchemaContentType.Mixed);

		// Token: 0x0400193E RID: 6462
		public static readonly ContentValidator Any = new ContentValidator(XmlSchemaContentType.Mixed, true, true);
	}
}
