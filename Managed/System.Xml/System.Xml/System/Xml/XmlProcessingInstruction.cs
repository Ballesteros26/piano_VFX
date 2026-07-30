using System;
using System.Xml.XPath;

namespace System.Xml
{
	/// <summary>Represents a processing instruction, which XML defines to keep processor-specific information in the text of the document.</summary>
	// Token: 0x0200023A RID: 570
	public class XmlProcessingInstruction : XmlLinkedNode
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlProcessingInstruction" /> class.</summary>
		/// <param name="target">The target of the processing instruction; see the <see cref="P:System.Xml.XmlProcessingInstruction.Target" /> property.</param>
		/// <param name="data">The content of the instruction; see the <see cref="P:System.Xml.XmlProcessingInstruction.Data" /> property.</param>
		/// <param name="doc">The parent XML document.</param>
		// Token: 0x0600162A RID: 5674 RVA: 0x0007BC46 File Offset: 0x00079E46
		protected internal XmlProcessingInstruction(string target, string data, XmlDocument doc)
			: base(doc)
		{
			this.target = target;
			this.data = data;
		}

		/// <summary>Gets the qualified name of the node.</summary>
		/// <returns>For processing instruction nodes, this property returns the target of the processing instruction.</returns>
		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x0600162B RID: 5675 RVA: 0x0007BC5D File Offset: 0x00079E5D
		public override string Name
		{
			get
			{
				if (this.target != null)
				{
					return this.target;
				}
				return string.Empty;
			}
		}

		/// <summary>Gets the local name of the node.</summary>
		/// <returns>For processing instruction nodes, this property returns the target of the processing instruction.</returns>
		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x0600162C RID: 5676 RVA: 0x000730B3 File Offset: 0x000712B3
		public override string LocalName
		{
			get
			{
				return this.Name;
			}
		}

		/// <summary>Gets or sets the value of the node.</summary>
		/// <returns>The entire content of the processing instruction, excluding the target.</returns>
		/// <exception cref="T:System.ArgumentException">Node is read-only. </exception>
		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x0600162D RID: 5677 RVA: 0x0007BC73 File Offset: 0x00079E73
		// (set) Token: 0x0600162E RID: 5678 RVA: 0x0007BC7B File Offset: 0x00079E7B
		public override string Value
		{
			get
			{
				return this.data;
			}
			set
			{
				this.Data = value;
			}
		}

		/// <summary>Gets the target of the processing instruction.</summary>
		/// <returns>The target of the processing instruction.</returns>
		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x0600162F RID: 5679 RVA: 0x0007BC84 File Offset: 0x00079E84
		public string Target
		{
			get
			{
				return this.target;
			}
		}

		/// <summary>Gets or sets the content of the processing instruction, excluding the target.</summary>
		/// <returns>The content of the processing instruction, excluding the target.</returns>
		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x06001630 RID: 5680 RVA: 0x0007BC73 File Offset: 0x00079E73
		// (set) Token: 0x06001631 RID: 5681 RVA: 0x0007BC8C File Offset: 0x00079E8C
		public string Data
		{
			get
			{
				return this.data;
			}
			set
			{
				XmlNode parentNode = this.ParentNode;
				XmlNodeChangedEventArgs eventArgs = this.GetEventArgs(this, parentNode, parentNode, this.data, value, XmlNodeChangedAction.Change);
				if (eventArgs != null)
				{
					this.BeforeEvent(eventArgs);
				}
				this.data = value;
				if (eventArgs != null)
				{
					this.AfterEvent(eventArgs);
				}
			}
		}

		/// <summary>Gets or sets the concatenated values of the node and all its children.</summary>
		/// <returns>The concatenated values of the node and all its children.</returns>
		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x06001632 RID: 5682 RVA: 0x0007BC73 File Offset: 0x00079E73
		// (set) Token: 0x06001633 RID: 5683 RVA: 0x0007BC7B File Offset: 0x00079E7B
		public override string InnerText
		{
			get
			{
				return this.data;
			}
			set
			{
				this.Data = value;
			}
		}

		/// <summary>Gets the type of the current node.</summary>
		/// <returns>For XmlProcessingInstruction nodes, this value is XmlNodeType.ProcessingInstruction.</returns>
		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x06001634 RID: 5684 RVA: 0x00006D07 File Offset: 0x00004F07
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.ProcessingInstruction;
			}
		}

		/// <summary>Creates a duplicate of this node.</summary>
		/// <returns>The duplicate node.</returns>
		/// <param name="deep">true to recursively clone the subtree under the specified node; false to clone only the node itself. </param>
		// Token: 0x06001635 RID: 5685 RVA: 0x0007BCCD File Offset: 0x00079ECD
		public override XmlNode CloneNode(bool deep)
		{
			return this.OwnerDocument.CreateProcessingInstruction(this.target, this.data);
		}

		/// <summary>Saves the node to the specified <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="w">The XmlWriter to which you want to save. </param>
		// Token: 0x06001636 RID: 5686 RVA: 0x0007BCE6 File Offset: 0x00079EE6
		public override void WriteTo(XmlWriter w)
		{
			w.WriteProcessingInstruction(this.target, this.data);
		}

		/// <summary>Saves all the children of the node to the specified <see cref="T:System.Xml.XmlWriter" />. Because ProcessingInstruction nodes do not have children, this method has no effect.</summary>
		/// <param name="w">The XmlWriter to which you want to save. </param>
		// Token: 0x06001637 RID: 5687 RVA: 0x00002F50 File Offset: 0x00001150
		public override void WriteContentTo(XmlWriter w)
		{
		}

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x06001638 RID: 5688 RVA: 0x000730B3 File Offset: 0x000712B3
		internal override string XPLocalName
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x06001639 RID: 5689 RVA: 0x00006D07 File Offset: 0x00004F07
		internal override XPathNodeType XPNodeType
		{
			get
			{
				return XPathNodeType.ProcessingInstruction;
			}
		}

		// Token: 0x04000E28 RID: 3624
		private string target;

		// Token: 0x04000E29 RID: 3625
		private string data;
	}
}
