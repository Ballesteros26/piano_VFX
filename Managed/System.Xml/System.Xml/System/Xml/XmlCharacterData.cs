using System;
using System.Text;
using System.Xml.XPath;

namespace System.Xml
{
	/// <summary>Provides text manipulation methods that are used by several classes.</summary>
	// Token: 0x02000218 RID: 536
	public abstract class XmlCharacterData : XmlLinkedNode
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlCharacterData" /> class.</summary>
		/// <param name="data">String that contains character data to be added to document.</param>
		/// <param name="doc">
		///   <see cref="T:System.Xml.XmlDocument" /> to contain character data.</param>
		// Token: 0x0600138B RID: 5003 RVA: 0x0007295C File Offset: 0x00070B5C
		protected internal XmlCharacterData(string data, XmlDocument doc)
			: base(doc)
		{
			this.data = data;
		}

		/// <summary>Gets or sets the value of the node.</summary>
		/// <returns>The value of the node.</returns>
		/// <exception cref="T:System.ArgumentException">Node is read-only. </exception>
		// Token: 0x17000373 RID: 883
		// (get) Token: 0x0600138C RID: 5004 RVA: 0x0007296C File Offset: 0x00070B6C
		// (set) Token: 0x0600138D RID: 5005 RVA: 0x00072974 File Offset: 0x00070B74
		public override string Value
		{
			get
			{
				return this.Data;
			}
			set
			{
				this.Data = value;
			}
		}

		/// <summary>Gets or sets the concatenated values of the node and all the children of the node.</summary>
		/// <returns>The concatenated values of the node and all the children of the node.</returns>
		// Token: 0x17000374 RID: 884
		// (get) Token: 0x0600138E RID: 5006 RVA: 0x0007297D File Offset: 0x00070B7D
		// (set) Token: 0x0600138F RID: 5007 RVA: 0x00072985 File Offset: 0x00070B85
		public override string InnerText
		{
			get
			{
				return this.Value;
			}
			set
			{
				this.Value = value;
			}
		}

		/// <summary>Contains the data of the node.</summary>
		/// <returns>The data of the node.</returns>
		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06001390 RID: 5008 RVA: 0x0007298E File Offset: 0x00070B8E
		// (set) Token: 0x06001391 RID: 5009 RVA: 0x000729A4 File Offset: 0x00070BA4
		public virtual string Data
		{
			get
			{
				if (this.data != null)
				{
					return this.data;
				}
				return string.Empty;
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

		/// <summary>Gets the length of the data, in characters.</summary>
		/// <returns>The length, in characters, of the string in the <see cref="P:System.Xml.XmlCharacterData.Data" /> property. The length may be zero; that is, CharacterData nodes can be empty.</returns>
		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06001392 RID: 5010 RVA: 0x000729E5 File Offset: 0x00070BE5
		public virtual int Length
		{
			get
			{
				if (this.data != null)
				{
					return this.data.Length;
				}
				return 0;
			}
		}

		/// <summary>Retrieves a substring of the full string from the specified range.</summary>
		/// <returns>The substring corresponding to the specified range.</returns>
		/// <param name="offset">The position within the string to start retrieving. An offset of zero indicates the starting point is at the start of the data. </param>
		/// <param name="count">The number of characters to retrieve. </param>
		// Token: 0x06001393 RID: 5011 RVA: 0x000729FC File Offset: 0x00070BFC
		public virtual string Substring(int offset, int count)
		{
			int num = ((this.data != null) ? this.data.Length : 0);
			if (num > 0)
			{
				if (num < offset + count)
				{
					count = num - offset;
				}
				return this.data.Substring(offset, count);
			}
			return string.Empty;
		}

		/// <summary>Appends the specified string to the end of the character data of the node.</summary>
		/// <param name="strData">The string to insert into the existing string. </param>
		// Token: 0x06001394 RID: 5012 RVA: 0x00072A44 File Offset: 0x00070C44
		public virtual void AppendData(string strData)
		{
			XmlNode parentNode = this.ParentNode;
			int num = ((this.data != null) ? this.data.Length : 0);
			if (strData != null)
			{
				num += strData.Length;
			}
			string text = new StringBuilder(num).Append(this.data).Append(strData).ToString();
			XmlNodeChangedEventArgs eventArgs = this.GetEventArgs(this, parentNode, parentNode, this.data, text, XmlNodeChangedAction.Change);
			if (eventArgs != null)
			{
				this.BeforeEvent(eventArgs);
			}
			this.data = text;
			if (eventArgs != null)
			{
				this.AfterEvent(eventArgs);
			}
		}

		/// <summary>Inserts the specified string at the specified character offset.</summary>
		/// <param name="offset">The position within the string to insert the supplied string data. </param>
		/// <param name="strData">The string data that is to be inserted into the existing string. </param>
		// Token: 0x06001395 RID: 5013 RVA: 0x00072AC8 File Offset: 0x00070CC8
		public virtual void InsertData(int offset, string strData)
		{
			XmlNode parentNode = this.ParentNode;
			int num = ((this.data != null) ? this.data.Length : 0);
			if (strData != null)
			{
				num += strData.Length;
			}
			string text = new StringBuilder(num).Append(this.data).Insert(offset, strData).ToString();
			XmlNodeChangedEventArgs eventArgs = this.GetEventArgs(this, parentNode, parentNode, this.data, text, XmlNodeChangedAction.Change);
			if (eventArgs != null)
			{
				this.BeforeEvent(eventArgs);
			}
			this.data = text;
			if (eventArgs != null)
			{
				this.AfterEvent(eventArgs);
			}
		}

		/// <summary>Removes a range of characters from the node.</summary>
		/// <param name="offset">The position within the string to start deleting. </param>
		/// <param name="count">The number of characters to delete. </param>
		// Token: 0x06001396 RID: 5014 RVA: 0x00072B4C File Offset: 0x00070D4C
		public virtual void DeleteData(int offset, int count)
		{
			int num = ((this.data != null) ? this.data.Length : 0);
			if (num > 0 && num < offset + count)
			{
				count = Math.Max(num - offset, 0);
			}
			string text = new StringBuilder(this.data).Remove(offset, count).ToString();
			XmlNode parentNode = this.ParentNode;
			XmlNodeChangedEventArgs eventArgs = this.GetEventArgs(this, parentNode, parentNode, this.data, text, XmlNodeChangedAction.Change);
			if (eventArgs != null)
			{
				this.BeforeEvent(eventArgs);
			}
			this.data = text;
			if (eventArgs != null)
			{
				this.AfterEvent(eventArgs);
			}
		}

		/// <summary>Replaces the specified number of characters starting at the specified offset with the specified string.</summary>
		/// <param name="offset">The position within the string to start replacing. </param>
		/// <param name="count">The number of characters to replace. </param>
		/// <param name="strData">The new data that replaces the old string data. </param>
		// Token: 0x06001397 RID: 5015 RVA: 0x00072BD4 File Offset: 0x00070DD4
		public virtual void ReplaceData(int offset, int count, string strData)
		{
			int num = ((this.data != null) ? this.data.Length : 0);
			if (num > 0 && num < offset + count)
			{
				count = Math.Max(num - offset, 0);
			}
			string text = new StringBuilder(this.data).Remove(offset, count).Insert(offset, strData).ToString();
			XmlNode parentNode = this.ParentNode;
			XmlNodeChangedEventArgs eventArgs = this.GetEventArgs(this, parentNode, parentNode, this.data, text, XmlNodeChangedAction.Change);
			if (eventArgs != null)
			{
				this.BeforeEvent(eventArgs);
			}
			this.data = text;
			if (eventArgs != null)
			{
				this.AfterEvent(eventArgs);
			}
		}

		// Token: 0x06001398 RID: 5016 RVA: 0x00072C60 File Offset: 0x00070E60
		internal bool CheckOnData(string data)
		{
			return XmlCharType.Instance.IsOnlyWhitespace(data);
		}

		// Token: 0x06001399 RID: 5017 RVA: 0x00072C7C File Offset: 0x00070E7C
		internal bool DecideXPNodeTypeForTextNodes(XmlNode node, ref XPathNodeType xnt)
		{
			while (node != null)
			{
				XmlNodeType nodeType = node.NodeType;
				if (nodeType <= XmlNodeType.EntityReference)
				{
					if (nodeType - XmlNodeType.Text <= 1)
					{
						xnt = XPathNodeType.Text;
						return false;
					}
					if (nodeType != XmlNodeType.EntityReference)
					{
						return false;
					}
					if (!this.DecideXPNodeTypeForTextNodes(node.FirstChild, ref xnt))
					{
						return false;
					}
				}
				else if (nodeType != XmlNodeType.Whitespace)
				{
					if (nodeType != XmlNodeType.SignificantWhitespace)
					{
						return false;
					}
					xnt = XPathNodeType.SignificantWhitespace;
				}
				node = node.NextSibling;
			}
			return true;
		}

		// Token: 0x04000D81 RID: 3457
		private string data;
	}
}
