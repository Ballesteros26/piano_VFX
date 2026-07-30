using System;
using Unity;

namespace System.Xml.Serialization
{
	/// <summary>Provides data for the <see cref="E:System.Xml.Serialization.XmlSerializer.UnknownNode" /> event.</summary>
	// Token: 0x02000371 RID: 881
	public class XmlNodeEventArgs : EventArgs
	{
		// Token: 0x060023FE RID: 9214 RVA: 0x000DCBCA File Offset: 0x000DADCA
		internal XmlNodeEventArgs(XmlNode xmlNode, int lineNumber, int linePosition, object o)
		{
			this.o = o;
			this.xmlNode = xmlNode;
			this.lineNumber = lineNumber;
			this.linePosition = linePosition;
		}

		/// <summary>Gets the object being deserialized.</summary>
		/// <returns>The <see cref="T:System.Object" /> being deserialized.</returns>
		// Token: 0x17000710 RID: 1808
		// (get) Token: 0x060023FF RID: 9215 RVA: 0x000DCBEF File Offset: 0x000DADEF
		public object ObjectBeingDeserialized
		{
			get
			{
				return this.o;
			}
		}

		/// <summary>Gets the type of the XML node being deserialized.</summary>
		/// <returns>The <see cref="T:System.Xml.XmlNodeType" /> that represents the XML node being deserialized.</returns>
		// Token: 0x17000711 RID: 1809
		// (get) Token: 0x06002400 RID: 9216 RVA: 0x000DCBF7 File Offset: 0x000DADF7
		public XmlNodeType NodeType
		{
			get
			{
				return this.xmlNode.NodeType;
			}
		}

		/// <summary>Gets the name of the XML node being deserialized.</summary>
		/// <returns>The name of the node being deserialized.</returns>
		// Token: 0x17000712 RID: 1810
		// (get) Token: 0x06002401 RID: 9217 RVA: 0x000DCC04 File Offset: 0x000DAE04
		public string Name
		{
			get
			{
				return this.xmlNode.Name;
			}
		}

		/// <summary>Gets the XML local name of the XML node being deserialized.</summary>
		/// <returns>The XML local name of the node being deserialized.</returns>
		// Token: 0x17000713 RID: 1811
		// (get) Token: 0x06002402 RID: 9218 RVA: 0x000DCC11 File Offset: 0x000DAE11
		public string LocalName
		{
			get
			{
				return this.xmlNode.LocalName;
			}
		}

		/// <summary>Gets the namespace URI that is associated with the XML node being deserialized.</summary>
		/// <returns>The namespace URI that is associated with the XML node being deserialized.</returns>
		// Token: 0x17000714 RID: 1812
		// (get) Token: 0x06002403 RID: 9219 RVA: 0x000DCC1E File Offset: 0x000DAE1E
		public string NamespaceURI
		{
			get
			{
				return this.xmlNode.NamespaceURI;
			}
		}

		/// <summary>Gets the text of the XML node being deserialized.</summary>
		/// <returns>The text of the XML node being deserialized.</returns>
		// Token: 0x17000715 RID: 1813
		// (get) Token: 0x06002404 RID: 9220 RVA: 0x000DCC2B File Offset: 0x000DAE2B
		public string Text
		{
			get
			{
				return this.xmlNode.Value;
			}
		}

		/// <summary>Gets the line number of the unknown XML node.</summary>
		/// <returns>The line number of the unknown XML node.</returns>
		// Token: 0x17000716 RID: 1814
		// (get) Token: 0x06002405 RID: 9221 RVA: 0x000DCC38 File Offset: 0x000DAE38
		public int LineNumber
		{
			get
			{
				return this.lineNumber;
			}
		}

		/// <summary>Gets the position in the line of the unknown XML node.</summary>
		/// <returns>The position number of the unknown XML node.</returns>
		// Token: 0x17000717 RID: 1815
		// (get) Token: 0x06002406 RID: 9222 RVA: 0x000DCC40 File Offset: 0x000DAE40
		public int LinePosition
		{
			get
			{
				return this.linePosition;
			}
		}

		// Token: 0x06002407 RID: 9223 RVA: 0x000728B0 File Offset: 0x00070AB0
		internal XmlNodeEventArgs()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001881 RID: 6273
		private object o;

		// Token: 0x04001882 RID: 6274
		private XmlNode xmlNode;

		// Token: 0x04001883 RID: 6275
		private int lineNumber;

		// Token: 0x04001884 RID: 6276
		private int linePosition;
	}
}
