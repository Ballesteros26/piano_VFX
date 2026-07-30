using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;

namespace System.Web.Configuration.nBrowser
{
	// Token: 0x020005F9 RID: 1529
	internal class Build : CapabilitiesBuild
	{
		// Token: 0x06004254 RID: 16980 RVA: 0x000AD68E File Offset: 0x000AB88E
		public Build()
		{
			this.Browserfiles = new Dictionary<string, File>();
			this.nbrowserfiles = new List<File>();
			this.DefaultKeys = new Dictionary<string, string>();
			this.BrowserKeys = new Dictionary<string, string>();
		}

		// Token: 0x06004255 RID: 16981 RVA: 0x000AD6D0 File Offset: 0x000AB8D0
		public void AddBrowserDirectory(string path)
		{
			if (Directory.Exists(path))
			{
				FileInfo[] files = new DirectoryInfo(path).GetFiles("*.browser");
				for (int i = 0; i <= files.Length - 1; i++)
				{
					this.AddBrowserFile(files[i].FullName);
				}
				return;
			}
			if (File.Exists(path))
			{
				this.AddBrowserFile(path);
			}
		}

		// Token: 0x06004256 RID: 16982 RVA: 0x000AD724 File Offset: 0x000AB924
		public void AddBrowserFile(string fileName)
		{
			if (!this.Browserfiles.ContainsKey(fileName))
			{
				File file = new File(fileName);
				this.AddBrowserFile(file);
			}
		}

		// Token: 0x06004257 RID: 16983 RVA: 0x000AD750 File Offset: 0x000AB950
		private void AddBrowserFile(File file)
		{
			if (!this.Browserfiles.ContainsKey(file.FileName))
			{
				this.Browserfiles.Add(file.FileName, file);
				this.nbrowserfiles.Add(file);
				string[] array = file.Keys;
				for (int i = 0; i <= array.Length - 1; i++)
				{
					if (this.BrowserKeys.ContainsKey(array[i]))
					{
						throw new Exception(string.Concat(new string[]
						{
							"Duplicate Key \"",
							array[i],
							"\" found in ",
							file.FileName,
							" and in file ",
							this.BrowserKeys[array[i]]
						}));
					}
					this.BrowserKeys.Add(array[i], file.FileName);
				}
				array = file.DefaultKeys;
				for (int j = 0; j <= array.Length - 1; j++)
				{
					if (this.DefaultKeys.ContainsKey(array[j]))
					{
						throw new Exception(string.Concat(new string[]
						{
							"Duplicate Key \"",
							array[j],
							"\" found in ",
							file.FileName,
							" and in file ",
							this.DefaultKeys[array[j]]
						}));
					}
					this.DefaultKeys.Add(array[j], file.FileName);
				}
			}
		}

		// Token: 0x06004258 RID: 16984 RVA: 0x000AD8A0 File Offset: 0x000ABAA0
		public void AddBrowserFile(XmlDocument browser, string fileName)
		{
			if (!this.Browserfiles.ContainsKey(fileName))
			{
				File file = new File(browser, fileName);
				this.AddBrowserFile(file);
			}
		}

		// Token: 0x06004259 RID: 16985 RVA: 0x000AD8CC File Offset: 0x000ABACC
		public Node Browser()
		{
			if (this.browser == null)
			{
				object obj = this.browserSyncRoot;
				lock (obj)
				{
					if (this.browser == null)
					{
						this.browser = this.InitializeTree();
					}
				}
			}
			return this.browser;
		}

		// Token: 0x0600425A RID: 16986 RVA: 0x000AD928 File Offset: 0x000ABB28
		private Node InitializeTree()
		{
			Node node = new Node();
			SortedList<string, List<File>> sortedList = new SortedList<string, List<File>>();
			for (int i = 0; i <= this.Browserfiles.Count - 1; i++)
			{
				if (!sortedList.ContainsKey(this.nbrowserfiles[i].FileName))
				{
					List<File> list = new List<File>();
					sortedList.Add(this.nbrowserfiles[i].FileName, list);
				}
				sortedList[this.nbrowserfiles[i].FileName].Add(this.nbrowserfiles[i]);
			}
			File[] array = new File[this.Browserfiles.Count];
			int num = 0;
			for (int j = 0; j <= sortedList.Count - 1; j++)
			{
				List<File> list2 = sortedList[sortedList.Keys[j]];
				for (int k = 0; k <= list2.Count - 1; k++)
				{
					array[num] = list2[k];
					num++;
				}
			}
			for (int l = 0; l <= this.Browserfiles.Count - 1; l++)
			{
				for (int m = 0; m <= array[l].Keys.Length - 1; m++)
				{
					Node node2 = array[l].GetNode(array[l].Keys[m]);
					Node node3 = null;
					if (node2.ParentId.Length > 0)
					{
						node3 = this.GetNode(node2.ParentId);
						if (node3 == null)
						{
							throw new Exception(string.Format("Parent not found with id = {0}", node2.ParentId));
						}
					}
					if (node3 == null)
					{
						node3 = node;
					}
					node3.AddChild(node2);
				}
			}
			for (int n = 0; n <= this.Browserfiles.Count - 1; n++)
			{
				for (int num2 = 0; num2 <= array[n].DefaultKeys.Length - 1; num2++)
				{
					Node defaultNode = array[n].GetDefaultNode(array[n].DefaultKeys[num2]);
					Node node4 = this.GetNode(defaultNode.Id);
					if (node4 != defaultNode)
					{
						Node node5 = this.GetNode(node4.ParentId);
						if (node5 == null)
						{
							node5 = node;
						}
						node5.RemoveChild(node4);
						defaultNode.AddChild(node4);
						node5.AddChild(defaultNode);
					}
				}
			}
			for (int num3 = 0; num3 <= this.Browserfiles.Count - 1; num3++)
			{
				foreach (Node node6 in array[num3].RefNodes)
				{
					this.GetNode(node6.RefId).MergeFrom(node6);
				}
			}
			return node;
		}

		// Token: 0x0600425B RID: 16987 RVA: 0x000ADBD8 File Offset: 0x000ABDD8
		private Node GetNode(string Key)
		{
			if (Key == null || Key.Length == 0)
			{
				return null;
			}
			string text;
			if (!this.BrowserKeys.TryGetValue(Key, out text) && !this.DefaultKeys.TryGetValue(Key, out text))
			{
				return null;
			}
			if (text != null && text.Length > 0)
			{
				return this.Browserfiles[text].GetNode(Key);
			}
			return null;
		}

		// Token: 0x0600425C RID: 16988 RVA: 0x000ADC34 File Offset: 0x000ABE34
		public Node[] Nodes()
		{
			File[] array = new File[this.Browserfiles.Count];
			this.Browserfiles.Values.CopyTo(array, 0);
			int num = 0;
			for (int i = 0; i <= array.Length - 1; i++)
			{
				num += array[i].Nodes.Length;
			}
			Node[] array2 = new Node[num];
			num = 0;
			for (int j = 0; j <= array.Length - 1; j++)
			{
				for (int k = 0; k <= array[j].Nodes.Length - 1; k++)
				{
					array2[num] = array[j].Nodes[k];
					num++;
				}
			}
			return array2;
		}

		// Token: 0x0600425D RID: 16989 RVA: 0x000ADCD0 File Offset: 0x000ABED0
		public override CapabilitiesResult Process(NameValueCollection header, IDictionary initialCapabilities)
		{
			if (initialCapabilities == null)
			{
				initialCapabilities = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
			}
			Result result = new Result(initialCapabilities);
			this.Browser().Process(header, result, new List<Match>());
			return result;
		}

		// Token: 0x0600425E RID: 16990 RVA: 0x000ADD07 File Offset: 0x000ABF07
		protected override Collection<string> HeaderNames(Collection<string> list)
		{
			return this.Browser().HeaderNames(list);
		}

		// Token: 0x0400237B RID: 9083
		private Dictionary<string, File> Browserfiles;

		// Token: 0x0400237C RID: 9084
		private List<File> nbrowserfiles;

		// Token: 0x0400237D RID: 9085
		private Dictionary<string, string> DefaultKeys;

		// Token: 0x0400237E RID: 9086
		private Dictionary<string, string> BrowserKeys;

		// Token: 0x0400237F RID: 9087
		private object browserSyncRoot = new object();

		// Token: 0x04002380 RID: 9088
		private Node browser;
	}
}
