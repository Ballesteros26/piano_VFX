using System;

namespace System.Xml
{
	/// <summary>Gets the node immediately preceding or following this node.</summary>
	// Token: 0x0200022A RID: 554
	public abstract class XmlLinkedNode : XmlNode
	{
		// Token: 0x060014EE RID: 5358 RVA: 0x000762FF File Offset: 0x000744FF
		internal XmlLinkedNode()
		{
			this.next = null;
		}

		// Token: 0x060014EF RID: 5359 RVA: 0x0007630E File Offset: 0x0007450E
		internal XmlLinkedNode(XmlDocument doc)
			: base(doc)
		{
			this.next = null;
		}

		/// <summary>Gets the node immediately preceding this node.</summary>
		/// <returns>The preceding <see cref="T:System.Xml.XmlNode" /> or null if one does not exist.</returns>
		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x060014F0 RID: 5360 RVA: 0x00076320 File Offset: 0x00074520
		public override XmlNode PreviousSibling
		{
			get
			{
				XmlNode parentNode = this.ParentNode;
				if (parentNode != null)
				{
					XmlNode xmlNode;
					XmlNode nextSibling;
					for (xmlNode = parentNode.FirstChild; xmlNode != null; xmlNode = nextSibling)
					{
						nextSibling = xmlNode.NextSibling;
						if (nextSibling == this)
						{
							break;
						}
					}
					return xmlNode;
				}
				return null;
			}
		}

		/// <summary>Gets the node immediately following this node.</summary>
		/// <returns>The <see cref="T:System.Xml.XmlNode" /> immediately following this node or null if one does not exist.</returns>
		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x060014F1 RID: 5361 RVA: 0x00076354 File Offset: 0x00074554
		public override XmlNode NextSibling
		{
			get
			{
				XmlNode parentNode = this.ParentNode;
				if (parentNode != null && this.next != parentNode.FirstChild)
				{
					return this.next;
				}
				return null;
			}
		}

		// Token: 0x04000DE5 RID: 3557
		internal XmlLinkedNode next;
	}
}
