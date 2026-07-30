using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the World Wide Web Consortium (W3C) anyAttribute element.</summary>
	// Token: 0x02000439 RID: 1081
	public class XmlSchemaAnyAttribute : XmlSchemaAnnotated
	{
		/// <summary>Gets or sets the namespaces containing the attributes that can be used.</summary>
		/// <returns>Namespaces for attributes that are available for use. The default is ##any.Optional.</returns>
		// Token: 0x17000904 RID: 2308
		// (get) Token: 0x06002ADD RID: 10973 RVA: 0x00104BEB File Offset: 0x00102DEB
		// (set) Token: 0x06002ADE RID: 10974 RVA: 0x00104BF3 File Offset: 0x00102DF3
		[XmlAttribute("namespace")]
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

		/// <summary>Gets or sets information about how an application or XML processor should handle the validation of XML documents for the attributes specified by the anyAttribute element.</summary>
		/// <returns>One of the <see cref="T:System.Xml.Schema.XmlSchemaContentProcessing" /> values. If no processContents attribute is specified, the default is Strict.</returns>
		// Token: 0x17000905 RID: 2309
		// (get) Token: 0x06002ADF RID: 10975 RVA: 0x00104BFC File Offset: 0x00102DFC
		// (set) Token: 0x06002AE0 RID: 10976 RVA: 0x00104C04 File Offset: 0x00102E04
		[XmlAttribute("processContents")]
		[DefaultValue(XmlSchemaContentProcessing.None)]
		public XmlSchemaContentProcessing ProcessContents
		{
			get
			{
				return this.processContents;
			}
			set
			{
				this.processContents = value;
			}
		}

		// Token: 0x17000906 RID: 2310
		// (get) Token: 0x06002AE1 RID: 10977 RVA: 0x00104C0D File Offset: 0x00102E0D
		[XmlIgnore]
		internal NamespaceList NamespaceList
		{
			get
			{
				return this.namespaceList;
			}
		}

		// Token: 0x17000907 RID: 2311
		// (get) Token: 0x06002AE2 RID: 10978 RVA: 0x00104C15 File Offset: 0x00102E15
		[XmlIgnore]
		internal XmlSchemaContentProcessing ProcessContentsCorrect
		{
			get
			{
				if (this.processContents != XmlSchemaContentProcessing.None)
				{
					return this.processContents;
				}
				return XmlSchemaContentProcessing.Strict;
			}
		}

		// Token: 0x06002AE3 RID: 10979 RVA: 0x00104C27 File Offset: 0x00102E27
		internal void BuildNamespaceList(string targetNamespace)
		{
			if (this.ns != null)
			{
				this.namespaceList = new NamespaceList(this.ns, targetNamespace);
				return;
			}
			this.namespaceList = new NamespaceList();
		}

		// Token: 0x06002AE4 RID: 10980 RVA: 0x00104C4F File Offset: 0x00102E4F
		internal void BuildNamespaceListV1Compat(string targetNamespace)
		{
			if (this.ns != null)
			{
				this.namespaceList = new NamespaceListV1Compat(this.ns, targetNamespace);
				return;
			}
			this.namespaceList = new NamespaceList();
		}

		// Token: 0x06002AE5 RID: 10981 RVA: 0x00104C77 File Offset: 0x00102E77
		internal bool Allows(XmlQualifiedName qname)
		{
			return this.namespaceList.Allows(qname.Namespace);
		}

		// Token: 0x06002AE6 RID: 10982 RVA: 0x00104C8A File Offset: 0x00102E8A
		internal static bool IsSubset(XmlSchemaAnyAttribute sub, XmlSchemaAnyAttribute super)
		{
			return NamespaceList.IsSubset(sub.NamespaceList, super.NamespaceList);
		}

		// Token: 0x06002AE7 RID: 10983 RVA: 0x00104CA0 File Offset: 0x00102EA0
		internal static XmlSchemaAnyAttribute Intersection(XmlSchemaAnyAttribute o1, XmlSchemaAnyAttribute o2, bool v1Compat)
		{
			NamespaceList namespaceList = NamespaceList.Intersection(o1.NamespaceList, o2.NamespaceList, v1Compat);
			if (namespaceList != null)
			{
				return new XmlSchemaAnyAttribute
				{
					namespaceList = namespaceList,
					ProcessContents = o1.ProcessContents,
					Annotation = o1.Annotation
				};
			}
			return null;
		}

		// Token: 0x06002AE8 RID: 10984 RVA: 0x00104CEC File Offset: 0x00102EEC
		internal static XmlSchemaAnyAttribute Union(XmlSchemaAnyAttribute o1, XmlSchemaAnyAttribute o2, bool v1Compat)
		{
			NamespaceList namespaceList = NamespaceList.Union(o1.NamespaceList, o2.NamespaceList, v1Compat);
			if (namespaceList != null)
			{
				return new XmlSchemaAnyAttribute
				{
					namespaceList = namespaceList,
					processContents = o1.processContents,
					Annotation = o1.Annotation
				};
			}
			return null;
		}

		// Token: 0x04001D28 RID: 7464
		private string ns;

		// Token: 0x04001D29 RID: 7465
		private XmlSchemaContentProcessing processContents;

		// Token: 0x04001D2A RID: 7466
		private NamespaceList namespaceList;
	}
}
