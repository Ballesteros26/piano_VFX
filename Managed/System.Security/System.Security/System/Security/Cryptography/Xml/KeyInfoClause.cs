using System;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	/// <summary>Represents the abstract base class from which all implementations of <see cref="T:System.Security.Cryptography.Xml.KeyInfo" /> subelements inherit.</summary>
	// Token: 0x02000063 RID: 99
	public abstract class KeyInfoClause
	{
		/// <summary>When overridden in a derived class, returns an XML representation of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoClause" />.</summary>
		/// <returns>An XML representation of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoClause" />.</returns>
		// Token: 0x06000287 RID: 647
		public abstract XmlElement GetXml();

		// Token: 0x06000288 RID: 648 RVA: 0x00009BE8 File Offset: 0x00007DE8
		internal virtual XmlElement GetXml(XmlDocument xmlDocument)
		{
			XmlElement xml = this.GetXml();
			return (XmlElement)xmlDocument.ImportNode(xml, true);
		}

		/// <summary>When overridden in a derived class, parses the input <see cref="T:System.Xml.XmlElement" /> and configures the internal state of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoClause" /> to match.</summary>
		/// <param name="element">The <see cref="T:System.Xml.XmlElement" /> that specifies the state of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoClause" />. </param>
		// Token: 0x06000289 RID: 649
		public abstract void LoadXml(XmlElement element);
	}
}
