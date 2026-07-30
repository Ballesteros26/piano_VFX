using System;

namespace System.Xml
{
	/// <summary>Defines the context for a set of <see cref="T:System.Xml.XmlDocument" /> objects.</summary>
	// Token: 0x02000229 RID: 553
	public class XmlImplementation
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlImplementation" /> class.</summary>
		// Token: 0x060014E9 RID: 5353 RVA: 0x000762A3 File Offset: 0x000744A3
		public XmlImplementation()
			: this(new NameTable())
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlImplementation" /> class with the <see cref="T:System.Xml.XmlNameTable" /> specified.</summary>
		/// <param name="nt">An <see cref="T:System.Xml.XmlNameTable" /> object.</param>
		// Token: 0x060014EA RID: 5354 RVA: 0x000762B0 File Offset: 0x000744B0
		public XmlImplementation(XmlNameTable nt)
		{
			this.nameTable = nt;
		}

		/// <summary>Tests if the Document Object Model (DOM) implementation implements a specific feature.</summary>
		/// <returns>true if the feature is implemented in the specified version; otherwise, false.The following table shows the combinations that cause HasFeature to return true.strFeature strVersion XML 1.0 XML 2.0 </returns>
		/// <param name="strFeature">The package name of the feature to test. This name is not case-sensitive. </param>
		/// <param name="strVersion">This is the version number of the package name to test. If the version is not specified (null), supporting any version of the feature causes the method to return true. </param>
		// Token: 0x060014EB RID: 5355 RVA: 0x000762BF File Offset: 0x000744BF
		public bool HasFeature(string strFeature, string strVersion)
		{
			return string.Compare("XML", strFeature, StringComparison.OrdinalIgnoreCase) == 0 && (strVersion == null || strVersion == "1.0" || strVersion == "2.0");
		}

		/// <summary>Creates a new <see cref="T:System.Xml.XmlDocument" />.</summary>
		/// <returns>The new XmlDocument object.</returns>
		// Token: 0x060014EC RID: 5356 RVA: 0x000762EF File Offset: 0x000744EF
		public virtual XmlDocument CreateDocument()
		{
			return new XmlDocument(this);
		}

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x060014ED RID: 5357 RVA: 0x000762F7 File Offset: 0x000744F7
		internal XmlNameTable NameTable
		{
			get
			{
				return this.nameTable;
			}
		}

		// Token: 0x04000DE4 RID: 3556
		private XmlNameTable nameTable;
	}
}
