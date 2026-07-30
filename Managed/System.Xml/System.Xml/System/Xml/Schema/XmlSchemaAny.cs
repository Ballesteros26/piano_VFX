using System;
using System.ComponentModel;
using System.Text;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the World Wide Web Consortium (W3C) any element.</summary>
	// Token: 0x02000438 RID: 1080
	public class XmlSchemaAny : XmlSchemaParticle
	{
		/// <summary>Gets or sets the namespaces containing the elements that can be used.</summary>
		/// <returns>Namespaces for elements that are available for use. The default is ##any.Optional.</returns>
		// Token: 0x170008FE RID: 2302
		// (get) Token: 0x06002AD1 RID: 10961 RVA: 0x00104A4C File Offset: 0x00102C4C
		// (set) Token: 0x06002AD2 RID: 10962 RVA: 0x00104A54 File Offset: 0x00102C54
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

		/// <summary>Gets or sets information about how an application or XML processor should handle the validation of XML documents for the elements specified by the any element.</summary>
		/// <returns>One of the <see cref="T:System.Xml.Schema.XmlSchemaContentProcessing" /> values. If no processContents attribute is specified, the default is Strict.</returns>
		// Token: 0x170008FF RID: 2303
		// (get) Token: 0x06002AD3 RID: 10963 RVA: 0x00104A5D File Offset: 0x00102C5D
		// (set) Token: 0x06002AD4 RID: 10964 RVA: 0x00104A65 File Offset: 0x00102C65
		[DefaultValue(XmlSchemaContentProcessing.None)]
		[XmlAttribute("processContents")]
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

		// Token: 0x17000900 RID: 2304
		// (get) Token: 0x06002AD5 RID: 10965 RVA: 0x00104A6E File Offset: 0x00102C6E
		[XmlIgnore]
		internal NamespaceList NamespaceList
		{
			get
			{
				return this.namespaceList;
			}
		}

		// Token: 0x17000901 RID: 2305
		// (get) Token: 0x06002AD6 RID: 10966 RVA: 0x00104A76 File Offset: 0x00102C76
		[XmlIgnore]
		internal string ResolvedNamespace
		{
			get
			{
				if (this.ns == null || this.ns.Length == 0)
				{
					return "##any";
				}
				return this.ns;
			}
		}

		// Token: 0x17000902 RID: 2306
		// (get) Token: 0x06002AD7 RID: 10967 RVA: 0x00104A99 File Offset: 0x00102C99
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

		// Token: 0x17000903 RID: 2307
		// (get) Token: 0x06002AD8 RID: 10968 RVA: 0x00104AAC File Offset: 0x00102CAC
		internal override string NameString
		{
			get
			{
				switch (this.namespaceList.Type)
				{
				case NamespaceList.ListType.Any:
					return "##any:*";
				case NamespaceList.ListType.Other:
					return "##other:*";
				case NamespaceList.ListType.Set:
				{
					StringBuilder stringBuilder = new StringBuilder();
					int num = 1;
					foreach (object obj in this.namespaceList.Enumerate)
					{
						string text = (string)obj;
						stringBuilder.Append(text + ":*");
						if (num < this.namespaceList.Enumerate.Count)
						{
							stringBuilder.Append(" ");
						}
						num++;
					}
					return stringBuilder.ToString();
				}
				default:
					return string.Empty;
				}
			}
		}

		// Token: 0x06002AD9 RID: 10969 RVA: 0x00104B80 File Offset: 0x00102D80
		internal void BuildNamespaceList(string targetNamespace)
		{
			if (this.ns != null)
			{
				this.namespaceList = new NamespaceList(this.ns, targetNamespace);
				return;
			}
			this.namespaceList = new NamespaceList();
		}

		// Token: 0x06002ADA RID: 10970 RVA: 0x00104BA8 File Offset: 0x00102DA8
		internal void BuildNamespaceListV1Compat(string targetNamespace)
		{
			if (this.ns != null)
			{
				this.namespaceList = new NamespaceListV1Compat(this.ns, targetNamespace);
				return;
			}
			this.namespaceList = new NamespaceList();
		}

		// Token: 0x06002ADB RID: 10971 RVA: 0x00104BD0 File Offset: 0x00102DD0
		internal bool Allows(XmlQualifiedName qname)
		{
			return this.namespaceList.Allows(qname.Namespace);
		}

		// Token: 0x04001D25 RID: 7461
		private string ns;

		// Token: 0x04001D26 RID: 7462
		private XmlSchemaContentProcessing processContents;

		// Token: 0x04001D27 RID: 7463
		private NamespaceList namespaceList;
	}
}
