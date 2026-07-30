using System;

namespace System.Xml
{
	/// <summary>Provides data for the <see cref="E:System.Xml.XmlDocument.NodeChanged" />, <see cref="E:System.Xml.XmlDocument.NodeChanging" />, <see cref="E:System.Xml.XmlDocument.NodeInserted" />, <see cref="E:System.Xml.XmlDocument.NodeInserting" />, <see cref="E:System.Xml.XmlDocument.NodeRemoved" /> and <see cref="E:System.Xml.XmlDocument.NodeRemoving" /> events.</summary>
	// Token: 0x02000233 RID: 563
	public class XmlNodeChangedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlNodeChangedEventArgs" /> class.</summary>
		/// <param name="node">The <see cref="T:System.Xml.XmlNode" /> that generated the event.</param>
		/// <param name="oldParent">The old parent <see cref="T:System.Xml.XmlNode" /> of the <see cref="T:System.Xml.XmlNode" /> that generated the event.</param>
		/// <param name="newParent">The new parent <see cref="T:System.Xml.XmlNode" /> of the <see cref="T:System.Xml.XmlNode" /> that generated the event.</param>
		/// <param name="oldValue">The old value of the <see cref="T:System.Xml.XmlNode" /> that generated the event.</param>
		/// <param name="newValue">The new value of the <see cref="T:System.Xml.XmlNode" /> that generated the event.</param>
		/// <param name="action">The <see cref="T:System.Xml.XmlNodeChangedAction" />.</param>
		// Token: 0x0600159C RID: 5532 RVA: 0x00079A0A File Offset: 0x00077C0A
		public XmlNodeChangedEventArgs(XmlNode node, XmlNode oldParent, XmlNode newParent, string oldValue, string newValue, XmlNodeChangedAction action)
		{
			this.node = node;
			this.oldParent = oldParent;
			this.newParent = newParent;
			this.action = action;
			this.oldValue = oldValue;
			this.newValue = newValue;
		}

		/// <summary>Gets a value indicating what type of node change event is occurring.</summary>
		/// <returns>An XmlNodeChangedAction value describing the node change event.XmlNodeChangedAction Value Description Insert A node has been or will be inserted. Remove A node has been or will be removed. Change A node has been or will be changed. NoteThe Action value does not differentiate between when the event occurred (before or after). You can create separate event handlers to handle both instances.</returns>
		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x0600159D RID: 5533 RVA: 0x00079A3F File Offset: 0x00077C3F
		public XmlNodeChangedAction Action
		{
			get
			{
				return this.action;
			}
		}

		/// <summary>Gets the <see cref="T:System.Xml.XmlNode" /> that is being added, removed or changed.</summary>
		/// <returns>The XmlNode that is being added, removed or changed; this property never returns null.</returns>
		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x0600159E RID: 5534 RVA: 0x00079A47 File Offset: 0x00077C47
		public XmlNode Node
		{
			get
			{
				return this.node;
			}
		}

		/// <summary>Gets the value of the <see cref="P:System.Xml.XmlNode.ParentNode" /> before the operation began.</summary>
		/// <returns>The value of the ParentNode before the operation began. This property returns null if the node did not have a parent.NoteFor attribute nodes this property returns the <see cref="P:System.Xml.XmlAttribute.OwnerElement" />.</returns>
		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x0600159F RID: 5535 RVA: 0x00079A4F File Offset: 0x00077C4F
		public XmlNode OldParent
		{
			get
			{
				return this.oldParent;
			}
		}

		/// <summary>Gets the value of the <see cref="P:System.Xml.XmlNode.ParentNode" /> after the operation completes.</summary>
		/// <returns>The value of the ParentNode after the operation completes. This property returns null if the node is being removed.NoteFor attribute nodes this property returns the <see cref="P:System.Xml.XmlAttribute.OwnerElement" />.</returns>
		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x060015A0 RID: 5536 RVA: 0x00079A57 File Offset: 0x00077C57
		public XmlNode NewParent
		{
			get
			{
				return this.newParent;
			}
		}

		/// <summary>Gets the original value of the node.</summary>
		/// <returns>The original value of the node. This property returns null if the node is neither an attribute nor a text node, or if the node is being inserted.If called in a <see cref="E:System.Xml.XmlDocument.NodeChanging" /> event, OldValue returns the current value of the node that will be replaced if the change is successful. If called in a <see cref="E:System.Xml.XmlDocument.NodeChanged" /> event, OldValue returns the value of node prior to the change.</returns>
		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x060015A1 RID: 5537 RVA: 0x00079A5F File Offset: 0x00077C5F
		public string OldValue
		{
			get
			{
				return this.oldValue;
			}
		}

		/// <summary>Gets the new value of the node.</summary>
		/// <returns>The new value of the node. This property returns null if the node is neither an attribute nor a text node, or if the node is being removed.If called in a <see cref="E:System.Xml.XmlDocument.NodeChanging" /> event, NewValue returns the value of the node if the change is successful. If called in a <see cref="E:System.Xml.XmlDocument.NodeChanged" /> event, NewValue returns the current value of the node.</returns>
		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x060015A2 RID: 5538 RVA: 0x00079A67 File Offset: 0x00077C67
		public string NewValue
		{
			get
			{
				return this.newValue;
			}
		}

		// Token: 0x04000DFE RID: 3582
		private XmlNodeChangedAction action;

		// Token: 0x04000DFF RID: 3583
		private XmlNode node;

		// Token: 0x04000E00 RID: 3584
		private XmlNode oldParent;

		// Token: 0x04000E01 RID: 3585
		private XmlNode newParent;

		// Token: 0x04000E02 RID: 3586
		private string oldValue;

		// Token: 0x04000E03 RID: 3587
		private string newValue;
	}
}
