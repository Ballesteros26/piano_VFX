using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Xml;

namespace System.Web.Configuration.nBrowser
{
	// Token: 0x020005FB RID: 1531
	internal class File
	{
		// Token: 0x1700150E RID: 5390
		// (get) Token: 0x06004263 RID: 16995 RVA: 0x000ADD30 File Offset: 0x000ABF30
		public string FileName
		{
			get
			{
				return this.pFileName;
			}
		}

		// Token: 0x06004264 RID: 16996 RVA: 0x000ADD38 File Offset: 0x000ABF38
		public File(string file)
		{
			this.pFileName = file;
			this.BrowserFile = new XmlDocument();
			this.BrowserFile.Load(file);
			this.Load(this.BrowserFile);
		}

		// Token: 0x06004265 RID: 16997 RVA: 0x000ADD75 File Offset: 0x000ABF75
		public File(XmlDocument BrowserFile, string filename)
		{
			this.pFileName = filename;
			this.Load(BrowserFile);
		}

		// Token: 0x06004266 RID: 16998 RVA: 0x000ADD98 File Offset: 0x000ABF98
		private void Load(XmlDocument BrowserFile)
		{
			this.Lookup = new ListDictionary();
			this.DefaultLookup = new ListDictionary();
			this.RefNodes = new List<Node>();
			this.Nodes = new Node[BrowserFile.DocumentElement.ChildNodes.Count];
			for (int i = 0; i <= BrowserFile.DocumentElement.ChildNodes.Count - 1; i++)
			{
				XmlNode xmlNode = BrowserFile.DocumentElement.ChildNodes[i];
				if (xmlNode.NodeType != XmlNodeType.Comment)
				{
					this.Nodes[i] = new Node(xmlNode);
					this.Nodes[i].FileName = this.FileName;
					if (this.Nodes[i].NameType != NodeType.DefaultBrowser)
					{
						if (this.Nodes[i].RefId.Length > 0)
						{
							this.RefNodes.Add(this.Nodes[i]);
						}
						else
						{
							if (this.Lookup.Contains(this.Nodes[i].Id))
							{
								throw new Exception("Duplicate ID found \"" + this.Nodes[i].Id + "\"");
							}
							this.Lookup.Add(this.Nodes[i].Id, i);
						}
					}
					else if (this.Nodes[i].RefId.Length > 0)
					{
						this.RefNodes.Add(this.Nodes[i]);
					}
					else
					{
						if (this.DefaultLookup.Contains(this.Nodes[i].Id))
						{
							throw new Exception("Duplicate ID found \"" + this.Nodes[i].Id + "\"");
						}
						this.DefaultLookup.Add(this.Nodes[i].Id, i);
					}
				}
			}
		}

		// Token: 0x1700150F RID: 5391
		// (get) Token: 0x06004267 RID: 16999 RVA: 0x000ADF68 File Offset: 0x000AC168
		public string[] Keys
		{
			get
			{
				string[] array = new string[this.Lookup.Keys.Count];
				int num = 0;
				for (int i = 0; i <= this.Nodes.Length - 1; i++)
				{
					if (this.Nodes[i] != null && this.Nodes[i].NameType != NodeType.DefaultBrowser && this.Nodes[i].RefId.Length == 0)
					{
						array[num] = this.Nodes[i].Id;
						num++;
					}
				}
				return array;
			}
		}

		// Token: 0x17001510 RID: 5392
		// (get) Token: 0x06004268 RID: 17000 RVA: 0x000ADFE8 File Offset: 0x000AC1E8
		public string[] DefaultKeys
		{
			get
			{
				string[] array = new string[this.DefaultLookup.Keys.Count];
				int num = 0;
				for (int i = 0; i <= this.Nodes.Length - 1; i++)
				{
					if (this.Nodes[i] != null && this.Nodes[i].NameType == NodeType.DefaultBrowser)
					{
						array[num] = this.Nodes[i].Id;
						num++;
					}
				}
				return array;
			}
		}

		// Token: 0x06004269 RID: 17001 RVA: 0x000AE054 File Offset: 0x000AC254
		internal Node GetNode(string Key)
		{
			object obj = this.Lookup[Key];
			if (obj == null)
			{
				return this.GetDefaultNode(Key);
			}
			return this.Nodes[(int)obj];
		}

		// Token: 0x0600426A RID: 17002 RVA: 0x000AE088 File Offset: 0x000AC288
		internal Node GetDefaultNode(string Key)
		{
			object obj = this.DefaultLookup[Key];
			if (obj == null)
			{
				return null;
			}
			return this.Nodes[(int)obj];
		}

		// Token: 0x04002381 RID: 9089
		private XmlDocument BrowserFile;

		// Token: 0x04002382 RID: 9090
		internal Node[] Nodes;

		// Token: 0x04002383 RID: 9091
		private ListDictionary Lookup;

		// Token: 0x04002384 RID: 9092
		private ListDictionary DefaultLookup;

		// Token: 0x04002385 RID: 9093
		internal List<Node> RefNodes;

		// Token: 0x04002386 RID: 9094
		private string pFileName = string.Empty;
	}
}
