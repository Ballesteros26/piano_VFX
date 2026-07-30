using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Globalization;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml;

namespace System.Web.Configuration.nBrowser
{
	// Token: 0x020005FD RID: 1533
	internal class Node
	{
		// Token: 0x17001514 RID: 5396
		// (get) Token: 0x06004271 RID: 17009 RVA: 0x000AE159 File Offset: 0x000AC359
		// (set) Token: 0x06004272 RID: 17010 RVA: 0x000AE161 File Offset: 0x000AC361
		public NodeType NameType
		{
			get
			{
				return this.pName;
			}
			set
			{
				this.pName = value;
			}
		}

		// Token: 0x17001515 RID: 5397
		// (get) Token: 0x06004273 RID: 17011 RVA: 0x000AE16A File Offset: 0x000AC36A
		// (set) Token: 0x06004274 RID: 17012 RVA: 0x000AE172 File Offset: 0x000AC372
		public string Id
		{
			get
			{
				return this.pId;
			}
			set
			{
				this.pId = value;
			}
		}

		// Token: 0x17001516 RID: 5398
		// (get) Token: 0x06004275 RID: 17013 RVA: 0x000AE17B File Offset: 0x000AC37B
		// (set) Token: 0x06004276 RID: 17014 RVA: 0x000AE183 File Offset: 0x000AC383
		public string ParentId
		{
			get
			{
				return this.pParentID;
			}
			set
			{
				this.pParentID = value;
			}
		}

		// Token: 0x17001517 RID: 5399
		// (get) Token: 0x06004277 RID: 17015 RVA: 0x000AE18C File Offset: 0x000AC38C
		// (set) Token: 0x06004278 RID: 17016 RVA: 0x000AE194 File Offset: 0x000AC394
		public string RefId
		{
			get
			{
				return this.pRefID;
			}
			set
			{
				this.pRefID = value;
			}
		}

		// Token: 0x17001518 RID: 5400
		// (get) Token: 0x06004279 RID: 17017 RVA: 0x000AE19D File Offset: 0x000AC39D
		// (set) Token: 0x0600427A RID: 17018 RVA: 0x000AE1A5 File Offset: 0x000AC3A5
		public string MarkupTextWriterType
		{
			get
			{
				return this.pMarkupTextWriterType;
			}
			set
			{
				this.pMarkupTextWriterType = value;
			}
		}

		// Token: 0x17001519 RID: 5401
		// (get) Token: 0x0600427B RID: 17019 RVA: 0x000AE1AE File Offset: 0x000AC3AE
		// (set) Token: 0x0600427C RID: 17020 RVA: 0x000AE1B6 File Offset: 0x000AC3B6
		public string FileName
		{
			get
			{
				return this.pFileName;
			}
			set
			{
				this.pFileName = value;
			}
		}

		// Token: 0x0600427D RID: 17021 RVA: 0x000AE1C0 File Offset: 0x000AC3C0
		public Node(XmlNode xmlNode)
		{
			this.xmlNode = xmlNode;
			this.ResetChildern();
			this.Reset();
		}

		// Token: 0x0600427E RID: 17022 RVA: 0x000AE228 File Offset: 0x000AC428
		internal Node()
		{
			this.ResetChildern();
			this.Identification = new Identification[1];
			this.Identification[0] = new Identification(true, "header", "User-Agent", ".");
			this.Id = "[Base Node]";
			this.NameType = NodeType.Browser;
		}

		// Token: 0x0600427F RID: 17023 RVA: 0x000AE2C0 File Offset: 0x000AC4C0
		private void ProcessIdentification(XmlNode node)
		{
			this.Identification = new Identification[node.ChildNodes.Count];
			int num = -1;
			for (int i = 0; i <= node.ChildNodes.Count - 1; i++)
			{
				XmlNodeType nodeType = node.ChildNodes[i].NodeType;
				if (nodeType != XmlNodeType.Text && nodeType != XmlNodeType.Comment)
				{
					string text = string.Empty;
					string text2 = string.Empty;
					if (string.Compare(node.ChildNodes[i].Name, "userAgent", true, CultureInfo.CurrentCulture) == 0)
					{
						text = "header";
						text2 = "User-Agent";
					}
					else if (string.Compare(node.ChildNodes[i].Name, "header", true, CultureInfo.CurrentCulture) == 0)
					{
						text = node.ChildNodes[i].Name;
						text2 = node.ChildNodes[i].Attributes["name"].Value;
					}
					else
					{
						if (string.Compare(node.ChildNodes[i].Name, "capability", true, CultureInfo.CurrentCulture) != 0)
						{
							throw new Exception("Invalid Node found in Identification");
						}
						text = node.ChildNodes[i].Name;
						text2 = node.ChildNodes[i].Attributes["name"].Value;
					}
					if (node.ChildNodes[i].Attributes["match"] != null)
					{
						num++;
						this.Identification[num] = new Identification(true, text, text2, node.ChildNodes[i].Attributes["match"].Value);
					}
					else if (node.ChildNodes[i].Attributes["nonMatch"] != null)
					{
						num++;
						this.Identification[num] = new Identification(false, text, text2, node.ChildNodes[i].Attributes["nonMatch"].Value);
					}
				}
			}
		}

		// Token: 0x06004280 RID: 17024 RVA: 0x000AE4C8 File Offset: 0x000AC6C8
		private void ProcessCapture(XmlNode node)
		{
			this.Capture = new Identification[node.ChildNodes.Count];
			int num = -1;
			for (int i = 0; i <= node.ChildNodes.Count - 1; i++)
			{
				XmlNodeType nodeType = node.ChildNodes[i].NodeType;
				if (nodeType != XmlNodeType.Text && nodeType != XmlNodeType.Comment)
				{
					string text = string.Empty;
					string text2 = string.Empty;
					string text3 = string.Empty;
					if (node.ChildNodes[i].Name == "userAgent")
					{
						text2 = "header";
						text3 = "User-Agent";
					}
					else
					{
						text2 = node.ChildNodes[i].Name;
						text3 = node.ChildNodes[i].Attributes["name"].Value;
					}
					text = node.ChildNodes[i].Attributes["match"].Value;
					num++;
					this.Capture[num] = new Identification(true, text2, text3, text);
				}
			}
		}

		// Token: 0x06004281 RID: 17025 RVA: 0x000AE5D8 File Offset: 0x000AC7D8
		private void ProcessCapabilities(XmlNode node)
		{
			this.Capabilities = new NameValueCollection(node.ChildNodes.Count, StringComparer.OrdinalIgnoreCase);
			for (int i = 0; i <= node.ChildNodes.Count - 1; i++)
			{
				if (node.ChildNodes[i].NodeType != XmlNodeType.Comment)
				{
					string text = string.Empty;
					string text2 = string.Empty;
					for (int j = 0; j <= node.ChildNodes[i].Attributes.Count - 1; j++)
					{
						string name = node.ChildNodes[i].Attributes[j].Name;
						if (!(name == "name"))
						{
							if (name == "value")
							{
								text2 = node.ChildNodes[i].Attributes[j].Value;
							}
						}
						else
						{
							text = node.ChildNodes[i].Attributes[j].Value;
						}
					}
					if (text.Length > 0)
					{
						this.Capabilities[text] = text2;
					}
				}
			}
		}

		// Token: 0x06004282 RID: 17026 RVA: 0x000AE6F8 File Offset: 0x000AC8F8
		private void ProcessControlAdapters(XmlNode node)
		{
			this.Adapter = new NameValueCollection();
			for (int i = 0; i <= node.Attributes.Count - 1; i++)
			{
				string name = node.Attributes[i].Name;
				if (name == "markupTextWriterType")
				{
					this.MarkupTextWriterType = node.Attributes[i].Value;
				}
			}
			for (int j = 0; j <= node.ChildNodes.Count - 1; j++)
			{
				if (node.ChildNodes[j].NodeType != XmlNodeType.Comment && node.ChildNodes[j].NodeType != XmlNodeType.Text)
				{
					XmlNode xmlNode = node.ChildNodes[j];
					string text = string.Empty;
					string text2 = string.Empty;
					for (int k = 0; k <= xmlNode.Attributes.Count - 1; k++)
					{
						if (string.Compare(xmlNode.Attributes[k].Name, "controlType", true, CultureInfo.CurrentCulture) == 0)
						{
							text = xmlNode.Attributes[k].Value;
						}
						else if (string.Compare(xmlNode.Attributes[k].Name, "adapterType", true, CultureInfo.CurrentCulture) == 0)
						{
							text2 = xmlNode.Attributes[k].Value;
						}
					}
					if (text.Length > 0 && text2.Length > 0)
					{
						this.Adapter[text] = text2;
					}
				}
			}
			this.AdapterControlTypes = null;
			this.AdapterTypes = null;
		}

		// Token: 0x06004283 RID: 17027 RVA: 0x000AE888 File Offset: 0x000ACA88
		private void ProcessSampleHeaders(XmlNode node)
		{
			this.sampleHeaders = new NameValueCollection(node.ChildNodes.Count);
			for (int i = 0; i <= node.ChildNodes.Count - 1; i++)
			{
				if (node.ChildNodes[i].NodeType != XmlNodeType.Comment)
				{
					string text = string.Empty;
					string text2 = string.Empty;
					for (int j = 0; j <= node.ChildNodes[i].Attributes.Count - 1; j++)
					{
						string name = node.ChildNodes[i].Attributes[j].Name;
						if (!(name == "name"))
						{
							if (name == "value")
							{
								text2 = node.ChildNodes[i].Attributes[j].Value;
							}
						}
						else
						{
							text = node.ChildNodes[i].Attributes[j].Value;
						}
					}
					if (text.Length > 0)
					{
						this.sampleHeaders[text] = text2;
					}
				}
			}
		}

		// Token: 0x06004284 RID: 17028 RVA: 0x000AE9A0 File Offset: 0x000ACBA0
		internal void ResetChildern()
		{
			this.Children = new SortedList<string, Node>();
			this.DefaultChildren = new SortedList<string, Node>();
			this.ChildrenKeys = new List<string>();
			this.DefaultChildrenKeys = new List<string>();
		}

		// Token: 0x1700151A RID: 5402
		// (get) Token: 0x06004285 RID: 17029 RVA: 0x000AE9CE File Offset: 0x000ACBCE
		public bool HasChildren
		{
			get
			{
				return this.Children.Count > -1;
			}
		}

		// Token: 0x06004286 RID: 17030 RVA: 0x000AE9E4 File Offset: 0x000ACBE4
		public void Reset()
		{
			this.Capture = null;
			this.Capabilities = null;
			this.Adapter = null;
			this.AdapterControlTypes = null;
			this.AdapterTypes = null;
			if (string.Compare(this.xmlNode.Name, "browser", true, CultureInfo.CurrentCulture) == 0)
			{
				this.NameType = NodeType.Browser;
			}
			else if (string.Compare(this.xmlNode.Name, "defaultBrowser", true, CultureInfo.CurrentCulture) == 0)
			{
				this.NameType = NodeType.DefaultBrowser;
			}
			else if (string.Compare(this.xmlNode.Name, "gateway", true, CultureInfo.CurrentCulture) == 0)
			{
				this.NameType = NodeType.Gateway;
			}
			for (int i = 0; i <= this.xmlNode.Attributes.Count - 1; i++)
			{
				if (string.Compare(this.xmlNode.Attributes[i].Name, "id", true, CultureInfo.CurrentCulture) == 0)
				{
					this.Id = this.xmlNode.Attributes[i].Value.ToLower(CultureInfo.CurrentCulture);
				}
				else if (string.Compare(this.xmlNode.Attributes[i].Name, "parentID", true, CultureInfo.CurrentCulture) == 0)
				{
					this.ParentId = this.xmlNode.Attributes[i].Value.ToLower(CultureInfo.CurrentCulture);
				}
				else if (string.Compare(this.xmlNode.Attributes[i].Name, "refID", true, CultureInfo.CurrentCulture) == 0)
				{
					this.RefId = this.xmlNode.Attributes[i].Value.ToLower(CultureInfo.CurrentCulture);
				}
			}
			for (int j = 0; j <= this.xmlNode.ChildNodes.Count - 1; j++)
			{
				if (string.Compare(this.xmlNode.ChildNodes[j].Name, "identification", true, CultureInfo.CurrentCulture) == 0)
				{
					this.ProcessIdentification(this.xmlNode.ChildNodes[j]);
				}
				else if (string.Compare(this.xmlNode.ChildNodes[j].Name, "capture", true, CultureInfo.CurrentCulture) == 0)
				{
					this.ProcessCapture(this.xmlNode.ChildNodes[j]);
				}
				else if (string.Compare(this.xmlNode.ChildNodes[j].Name, "capabilities", true, CultureInfo.CurrentCulture) == 0)
				{
					this.ProcessCapabilities(this.xmlNode.ChildNodes[j]);
				}
				else if (string.Compare(this.xmlNode.ChildNodes[j].Name, "controlAdapters", true, CultureInfo.CurrentCulture) == 0)
				{
					this.ProcessControlAdapters(this.xmlNode.ChildNodes[j]);
				}
				else if (string.Compare(this.xmlNode.ChildNodes[j].Name, "sampleHeaders", true, CultureInfo.CurrentCulture) == 0)
				{
					this.ProcessSampleHeaders(this.xmlNode.ChildNodes[j]);
				}
				if (this.Id == "default" && (this.Identification == null || this.Identification.Length == 0))
				{
					this.Identification = new Identification[1];
					this.Identification[0] = new Identification(true, "header", "User-Agent", ".");
				}
			}
		}

		// Token: 0x06004287 RID: 17031 RVA: 0x000AED54 File Offset: 0x000ACF54
		public void AddChild(Node child)
		{
			if (child == null)
			{
				return;
			}
			if (child.NameType == NodeType.Browser || child.NameType == NodeType.Gateway)
			{
				this.Children.Add(child.Id, child);
				this.ChildrenKeys.Add(child.Id);
				return;
			}
			if (child.NameType == NodeType.DefaultBrowser)
			{
				this.DefaultChildren.Add(child.Id, child);
				this.DefaultChildrenKeys.Add(child.Id);
			}
		}

		// Token: 0x06004288 RID: 17032 RVA: 0x000AEDC8 File Offset: 0x000ACFC8
		public void RemoveChild(Node child)
		{
			if (child == null)
			{
				return;
			}
			if (child.NameType == NodeType.Browser || child.NameType == NodeType.Gateway)
			{
				this.Children.Remove(child.Id);
				this.ChildrenKeys.Remove(child.Id);
				return;
			}
			if (child.NameType == NodeType.DefaultBrowser)
			{
				this.DefaultChildren.Remove(child.Id);
				this.DefaultChildrenKeys.Remove(child.Id);
			}
		}

		// Token: 0x06004289 RID: 17033 RVA: 0x000AEE40 File Offset: 0x000AD040
		private Type FindType(string typeName)
		{
			foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
			{
				string text = typeName + "," + assembly.FullName;
				Type type = Type.GetType(text);
				if (type != null)
				{
					return type;
				}
				type = Type.GetType(text, false, true);
				if (type != null)
				{
					return type;
				}
			}
			throw new TypeLoadException(typeName);
		}

		// Token: 0x0600428A RID: 17034 RVA: 0x000AEEB0 File Offset: 0x000AD0B0
		internal bool Process(NameValueCollection header, Result result, List<Match> matchList)
		{
			int count = matchList.Count;
			bool flag = this.ProcessSubtree(header, result, matchList);
			if (matchList.Count > count)
			{
				matchList.RemoveRange(count, matchList.Count - count);
			}
			return flag;
		}

		// Token: 0x0600428B RID: 17035 RVA: 0x000AEEE8 File Offset: 0x000AD0E8
		private bool ProcessSubtree(NameValueCollection header, Result result, List<Match> matchList)
		{
			result.AddCapabilities("", header["User-Agent"]);
			if (this.RefId.Length == 0 && this.NameType != NodeType.DefaultBrowser && !this.BrowserIdentification(header, result, matchList))
			{
				return false;
			}
			result.AddMatchingBrowserId(this.Id);
			result.AddTrack(string.Concat(new object[] { "[", this.NameType, "]\t", this.Id }));
			if (this.Adapter != null)
			{
				this.LookupAdapterTypes();
				for (int i = 0; i <= this.Adapter.Count - 1; i++)
				{
					result.AddAdapter(this.AdapterControlTypes[i], this.AdapterTypes[i]);
				}
			}
			if (this.MarkupTextWriterType != null && this.MarkupTextWriterType.Length > 0)
			{
				result.MarkupTextWriter = Type.GetType(this.MarkupTextWriterType);
				if (result.MarkupTextWriter == null)
				{
					result.MarkupTextWriter = Type.GetType(this.MarkupTextWriterType, true, true);
				}
			}
			if (this.Capture != null)
			{
				for (int j = 0; j <= this.Capture.Length - 1; j++)
				{
					if (this.Capture[j] != null)
					{
						Match match = null;
						if (this.Capture[j].Group == "header")
						{
							match = this.Capture[j].GetMatch(header[this.Capture[j].Name]);
						}
						else if (this.Capture[j].Group == "capability")
						{
							match = this.Capture[j].GetMatch(result[this.Capture[j].Name]);
						}
						if (this.Capture[j].IsMatchSuccessful(match) && match.Groups.Count > 0)
						{
							matchList.Add(match);
						}
					}
				}
			}
			if (this.Capabilities != null)
			{
				for (int k = 0; k <= this.Capabilities.Count - 1; k++)
				{
					string text = this.Capabilities[k];
					int num = matchList.Count - 1;
					while (num >= 0 && text != null && text.Length > 0 && text.IndexOf('$') > -1)
					{
						if (matchList[num].Groups.Count != 0 && matchList[num].Success)
						{
							text = matchList[num].Result(text);
						}
						num--;
					}
					if (text.IndexOf('$') > -1 || text.IndexOf('%') > -1)
					{
						text = result.Replace(text);
					}
					result.AddCapabilities(this.Capabilities.Keys[k], text);
				}
			}
			for (int l = 0; l <= this.DefaultChildren.Count - 1; l++)
			{
				string text2 = this.DefaultChildrenKeys[l];
				Node node = this.DefaultChildren[text2];
				if (node.NameType == NodeType.DefaultBrowser)
				{
					node.Process(header, result, matchList);
				}
			}
			for (int m = 0; m <= this.Children.Count - 1; m++)
			{
				string text3 = this.ChildrenKeys[m];
				Node node2 = this.Children[text3];
				if (node2.NameType == NodeType.Gateway)
				{
					node2.Process(header, result, matchList);
				}
			}
			for (int n = 0; n <= this.Children.Count - 1; n++)
			{
				string text4 = this.ChildrenKeys[n];
				Node node3 = this.Children[text4];
				if (node3.NameType == NodeType.Browser && node3.Process(header, result, matchList))
				{
					break;
				}
			}
			return true;
		}

		// Token: 0x0600428C RID: 17036 RVA: 0x000AF28C File Offset: 0x000AD48C
		private bool BrowserIdentification(NameValueCollection header, CapabilitiesResult result, List<Match> matchList)
		{
			if (this.Id.Length > 0 && this.RefId.Length > 0)
			{
				throw new Exception("Id and refID Attributes givin when there should only be one set not both");
			}
			if (this.Identification == null || this.Identification.Length == 0)
			{
				throw new Exception(string.Format("Missing Identification Section where one is required (Id={0}, RefID={1})", this.Id, this.RefId));
			}
			if (header == null)
			{
				throw new Exception("Null Value where NameValueCollection expected ");
			}
			if (result == null)
			{
				throw new Exception("Null Value where Result expected ");
			}
			for (int i = 0; i <= this.Identification.Length - 1; i++)
			{
				if (this.Identification[i] != null)
				{
					string text = string.Empty;
					if (string.Compare(this.Identification[i].Group, "header", true, CultureInfo.CurrentCulture) == 0)
					{
						text = header[this.Identification[i].Name];
					}
					else if (string.Compare(this.Identification[i].Group, "capability", true, CultureInfo.CurrentCulture) == 0)
					{
						text = result[this.Identification[i].Name];
					}
					if (text == null)
					{
						text = string.Empty;
					}
					Match match = this.Identification[i].GetMatch(text);
					if (!this.Identification[i].IsMatchSuccessful(match))
					{
						return false;
					}
					if (match.Groups.Count > 0)
					{
						matchList.Add(match);
					}
				}
			}
			return true;
		}

		// Token: 0x0600428D RID: 17037 RVA: 0x000AF3E0 File Offset: 0x000AD5E0
		private void LookupAdapterTypes()
		{
			if (this.Adapter == null || this.HaveAdapterTypes)
			{
				return;
			}
			object lookupAdapterTypesLock = this.LookupAdapterTypesLock;
			lock (lookupAdapterTypesLock)
			{
				if (!this.HaveAdapterTypes)
				{
					if (this.AdapterControlTypes == null)
					{
						this.AdapterControlTypes = new Type[this.Adapter.Count];
					}
					if (this.AdapterTypes == null)
					{
						this.AdapterTypes = new Type[this.Adapter.Count];
					}
					for (int i = 0; i <= this.Adapter.Count - 1; i++)
					{
						if (this.AdapterControlTypes[i] == null)
						{
							this.AdapterControlTypes[i] = this.FindType(this.Adapter.GetKey(i));
						}
						if (this.AdapterTypes[i] == null)
						{
							this.AdapterTypes[i] = this.FindType(this.Adapter[i]);
						}
					}
					this.HaveAdapterTypes = true;
				}
			}
		}

		// Token: 0x1700151B RID: 5403
		// (get) Token: 0x0600428E RID: 17038 RVA: 0x000AF4E8 File Offset: 0x000AD6E8
		public NameValueCollection SampleHeader
		{
			get
			{
				return this.sampleHeaders;
			}
		}

		// Token: 0x0600428F RID: 17039 RVA: 0x000AF4F0 File Offset: 0x000AD6F0
		public void Tree(XmlTextWriter xmlwriter, int position)
		{
			if (position == 0)
			{
				xmlwriter.WriteStartDocument();
				xmlwriter.WriteStartElement(this.NameType.ToString());
				xmlwriter.WriteRaw(Environment.NewLine);
			}
			string fileName = this.FileName;
			xmlwriter.WriteStartElement(this.NameType.ToString());
			xmlwriter.WriteAttributeString("FileName", fileName);
			xmlwriter.WriteAttributeString("ID", this.Id);
			xmlwriter.WriteRaw(Environment.NewLine);
			if (position != 2147483647)
			{
				position++;
			}
			for (int i = 0; i <= this.DefaultChildren.Count - 1; i++)
			{
				string text = this.DefaultChildrenKeys[i];
				Node node = this.DefaultChildren[text];
				if (node.NameType == NodeType.DefaultBrowser)
				{
					node.Tree(xmlwriter, position);
				}
			}
			for (int j = 0; j <= this.Children.Count - 1; j++)
			{
				string text2 = this.ChildrenKeys[j];
				Node node2 = this.Children[text2];
				if (node2.NameType == NodeType.Gateway)
				{
					node2.Tree(xmlwriter, position);
				}
			}
			for (int k = 0; k <= this.Children.Count - 1; k++)
			{
				string text3 = this.ChildrenKeys[k];
				Node node3 = this.Children[text3];
				if (node3.NameType == NodeType.Browser)
				{
					node3.Tree(xmlwriter, position);
				}
			}
			if (position != -2147483648)
			{
				position--;
			}
			xmlwriter.WriteEndElement();
			xmlwriter.WriteRaw(Environment.NewLine);
			if (position == 0)
			{
				xmlwriter.WriteEndDocument();
				xmlwriter.Flush();
			}
		}

		// Token: 0x06004290 RID: 17040 RVA: 0x000AF690 File Offset: 0x000AD890
		public Collection<string> HeaderNames(Collection<string> list)
		{
			if (this.Identification != null)
			{
				for (int i = 0; i <= this.Identification.Length - 1; i++)
				{
					if (this.Identification[i] != null && this.Identification[i].Group == "header" && !list.Contains(this.Identification[i].Name))
					{
						list.Add(this.Identification[i].Name);
					}
				}
			}
			if (this.Capture != null)
			{
				for (int j = 0; j <= this.Capture.Length - 1; j++)
				{
					if (this.Capture[j] != null && this.Capture[j].Group == "header" && !list.Contains(this.Capture[j].Name))
					{
						list.Add(this.Capture[j].Name);
					}
				}
			}
			for (int k = 0; k <= this.DefaultChildren.Count - 1; k++)
			{
				string text = this.DefaultChildrenKeys[k];
				Node node = this.DefaultChildren[text];
				if (node.NameType == NodeType.DefaultBrowser)
				{
					list = node.HeaderNames(list);
				}
			}
			for (int l = 0; l <= this.Children.Count - 1; l++)
			{
				string text2 = this.ChildrenKeys[l];
				Node node2 = this.Children[text2];
				if (node2.NameType == NodeType.Gateway)
				{
					list = node2.HeaderNames(list);
				}
			}
			for (int m = 0; m <= this.Children.Count - 1; m++)
			{
				string text3 = this.ChildrenKeys[m];
				Node node3 = this.Children[text3];
				if (node3.NameType == NodeType.Browser)
				{
					list = node3.HeaderNames(list);
				}
			}
			return list;
		}

		// Token: 0x06004291 RID: 17041 RVA: 0x000AF854 File Offset: 0x000ADA54
		public void MergeFrom(Node n)
		{
			if (n.Capabilities != null)
			{
				if (this.Capabilities == null)
				{
					this.Capabilities = new NameValueCollection(n.Capabilities.Count, StringComparer.OrdinalIgnoreCase);
				}
				foreach (object obj in n.Capabilities)
				{
					string text = (string)obj;
					this.Capabilities[text] = n.Capabilities[text];
				}
			}
			int num = 0;
			if (this.Capture != null)
			{
				num += this.Capture.Length;
			}
			if (n.Capture != null)
			{
				num += n.Capture.Length;
			}
			Identification[] array = new Identification[num];
			if (this.Capture != null)
			{
				Array.Copy(this.Capture, 0, array, 0, this.Capture.Length);
			}
			if (n.Capture != null)
			{
				Array.Copy(n.Capture, 0, array, (this.Capture != null) ? this.Capture.Length : 0, n.Capture.Length);
			}
			this.Capture = array;
			if (n.MarkupTextWriterType != null && n.MarkupTextWriterType.Length > 0)
			{
				this.MarkupTextWriterType = n.MarkupTextWriterType;
			}
			if (n.Adapter != null)
			{
				if (this.Adapter == null)
				{
					this.Adapter = new NameValueCollection();
				}
				foreach (object obj2 in n.Adapter)
				{
					string text2 = (string)obj2;
					this.Adapter[text2] = n.Adapter[text2];
				}
			}
		}

		// Token: 0x0400238C RID: 9100
		private NodeType pName;

		// Token: 0x0400238D RID: 9101
		private string pId = string.Empty;

		// Token: 0x0400238E RID: 9102
		private string pParentID = string.Empty;

		// Token: 0x0400238F RID: 9103
		private string pRefID = string.Empty;

		// Token: 0x04002390 RID: 9104
		private string pMarkupTextWriterType = string.Empty;

		// Token: 0x04002391 RID: 9105
		private string pFileName = string.Empty;

		// Token: 0x04002392 RID: 9106
		private XmlNode xmlNode;

		// Token: 0x04002393 RID: 9107
		private Identification[] Identification;

		// Token: 0x04002394 RID: 9108
		private Identification[] Capture;

		// Token: 0x04002395 RID: 9109
		private NameValueCollection Capabilities;

		// Token: 0x04002396 RID: 9110
		private NameValueCollection Adapter;

		// Token: 0x04002397 RID: 9111
		private Type[] AdapterControlTypes;

		// Token: 0x04002398 RID: 9112
		private Type[] AdapterTypes;

		// Token: 0x04002399 RID: 9113
		private List<string> ChildrenKeys;

		// Token: 0x0400239A RID: 9114
		private List<string> DefaultChildrenKeys;

		// Token: 0x0400239B RID: 9115
		private SortedList<string, Node> Children;

		// Token: 0x0400239C RID: 9116
		private SortedList<string, Node> DefaultChildren;

		// Token: 0x0400239D RID: 9117
		private NameValueCollection sampleHeaders;

		// Token: 0x0400239E RID: 9118
		private bool HaveAdapterTypes;

		// Token: 0x0400239F RID: 9119
		private object LookupAdapterTypesLock = new object();
	}
}
