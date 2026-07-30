using System;
using System.Collections;
using System.Text;

namespace System.Xml.Schema
{
	// Token: 0x02000405 RID: 1029
	internal class NamespaceList
	{
		// Token: 0x060027EB RID: 10219 RVA: 0x000020FD File Offset: 0x000002FD
		public NamespaceList()
		{
		}

		// Token: 0x060027EC RID: 10220 RVA: 0x000EC6F4 File Offset: 0x000EA8F4
		public NamespaceList(string namespaces, string targetNamespace)
		{
			this.targetNamespace = targetNamespace;
			namespaces = namespaces.Trim();
			if (namespaces == "##any" || namespaces.Length == 0)
			{
				this.type = NamespaceList.ListType.Any;
				return;
			}
			if (namespaces == "##other")
			{
				this.type = NamespaceList.ListType.Other;
				return;
			}
			this.type = NamespaceList.ListType.Set;
			this.set = new Hashtable();
			string[] array = XmlConvert.SplitString(namespaces);
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] == "##local")
				{
					this.set[string.Empty] = string.Empty;
				}
				else if (array[i] == "##targetNamespace")
				{
					this.set[targetNamespace] = targetNamespace;
				}
				else
				{
					XmlConvert.ToUri(array[i]);
					this.set[array[i]] = array[i];
				}
			}
		}

		// Token: 0x060027ED RID: 10221 RVA: 0x000EC7D0 File Offset: 0x000EA9D0
		public NamespaceList Clone()
		{
			NamespaceList namespaceList = (NamespaceList)base.MemberwiseClone();
			if (this.type == NamespaceList.ListType.Set)
			{
				namespaceList.set = (Hashtable)this.set.Clone();
			}
			return namespaceList;
		}

		// Token: 0x17000858 RID: 2136
		// (get) Token: 0x060027EE RID: 10222 RVA: 0x000EC809 File Offset: 0x000EAA09
		public NamespaceList.ListType Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17000859 RID: 2137
		// (get) Token: 0x060027EF RID: 10223 RVA: 0x000EC811 File Offset: 0x000EAA11
		public string Excluded
		{
			get
			{
				return this.targetNamespace;
			}
		}

		// Token: 0x1700085A RID: 2138
		// (get) Token: 0x060027F0 RID: 10224 RVA: 0x000EC81C File Offset: 0x000EAA1C
		public ICollection Enumerate
		{
			get
			{
				NamespaceList.ListType listType = this.type;
				if (listType > NamespaceList.ListType.Other && listType == NamespaceList.ListType.Set)
				{
					return this.set.Keys;
				}
				throw new InvalidOperationException();
			}
		}

		// Token: 0x060027F1 RID: 10225 RVA: 0x000EC84C File Offset: 0x000EAA4C
		public virtual bool Allows(string ns)
		{
			switch (this.type)
			{
			case NamespaceList.ListType.Any:
				return true;
			case NamespaceList.ListType.Other:
				return ns != this.targetNamespace && ns.Length != 0;
			case NamespaceList.ListType.Set:
				return this.set[ns] != null;
			default:
				return false;
			}
		}

		// Token: 0x060027F2 RID: 10226 RVA: 0x000EC8A1 File Offset: 0x000EAAA1
		public bool Allows(XmlQualifiedName qname)
		{
			return this.Allows(qname.Namespace);
		}

		// Token: 0x060027F3 RID: 10227 RVA: 0x000EC8B0 File Offset: 0x000EAAB0
		public override string ToString()
		{
			switch (this.type)
			{
			case NamespaceList.ListType.Any:
				return "##any";
			case NamespaceList.ListType.Other:
				return "##other";
			case NamespaceList.ListType.Set:
			{
				StringBuilder stringBuilder = new StringBuilder();
				bool flag = true;
				foreach (object obj in this.set.Keys)
				{
					string text = (string)obj;
					if (flag)
					{
						flag = false;
					}
					else
					{
						stringBuilder.Append(" ");
					}
					if (text == this.targetNamespace)
					{
						stringBuilder.Append("##targetNamespace");
					}
					else if (text.Length == 0)
					{
						stringBuilder.Append("##local");
					}
					else
					{
						stringBuilder.Append(text);
					}
				}
				return stringBuilder.ToString();
			}
			default:
				return string.Empty;
			}
		}

		// Token: 0x060027F4 RID: 10228 RVA: 0x000EC99C File Offset: 0x000EAB9C
		public static bool IsSubset(NamespaceList sub, NamespaceList super)
		{
			if (super.type == NamespaceList.ListType.Any)
			{
				return true;
			}
			if (sub.type == NamespaceList.ListType.Other && super.type == NamespaceList.ListType.Other)
			{
				return super.targetNamespace == sub.targetNamespace;
			}
			if (sub.type != NamespaceList.ListType.Set)
			{
				return false;
			}
			if (super.type == NamespaceList.ListType.Other)
			{
				return !sub.set.Contains(super.targetNamespace);
			}
			foreach (object obj in sub.set.Keys)
			{
				string text = (string)obj;
				if (!super.set.Contains(text))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060027F5 RID: 10229 RVA: 0x000ECA60 File Offset: 0x000EAC60
		public static NamespaceList Union(NamespaceList o1, NamespaceList o2, bool v1Compat)
		{
			NamespaceList namespaceList = null;
			if (o1.type == NamespaceList.ListType.Any)
			{
				namespaceList = new NamespaceList();
			}
			else if (o2.type == NamespaceList.ListType.Any)
			{
				namespaceList = new NamespaceList();
			}
			else
			{
				if (o1.type == NamespaceList.ListType.Set && o2.type == NamespaceList.ListType.Set)
				{
					namespaceList = o1.Clone();
					using (IEnumerator enumerator = o2.set.Keys.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object obj = enumerator.Current;
							string text = (string)obj;
							namespaceList.set[text] = text;
						}
						return namespaceList;
					}
				}
				if (o1.type == NamespaceList.ListType.Other && o2.type == NamespaceList.ListType.Other)
				{
					if (o1.targetNamespace == o2.targetNamespace)
					{
						namespaceList = o1.Clone();
					}
					else
					{
						namespaceList = new NamespaceList("##other", string.Empty);
					}
				}
				else if (o1.type == NamespaceList.ListType.Set && o2.type == NamespaceList.ListType.Other)
				{
					if (v1Compat)
					{
						if (o1.set.Contains(o2.targetNamespace))
						{
							namespaceList = new NamespaceList();
						}
						else
						{
							namespaceList = o2.Clone();
						}
					}
					else if (o2.targetNamespace != string.Empty)
					{
						namespaceList = o1.CompareSetToOther(o2);
					}
					else if (o1.set.Contains(string.Empty))
					{
						namespaceList = new NamespaceList();
					}
					else
					{
						namespaceList = new NamespaceList("##other", string.Empty);
					}
				}
				else if (o2.type == NamespaceList.ListType.Set && o1.type == NamespaceList.ListType.Other)
				{
					if (v1Compat)
					{
						if (o2.set.Contains(o2.targetNamespace))
						{
							namespaceList = new NamespaceList();
						}
						else
						{
							namespaceList = o1.Clone();
						}
					}
					else if (o1.targetNamespace != string.Empty)
					{
						namespaceList = o2.CompareSetToOther(o1);
					}
					else if (o2.set.Contains(string.Empty))
					{
						namespaceList = new NamespaceList();
					}
					else
					{
						namespaceList = new NamespaceList("##other", string.Empty);
					}
				}
			}
			return namespaceList;
		}

		// Token: 0x060027F6 RID: 10230 RVA: 0x000ECC60 File Offset: 0x000EAE60
		private NamespaceList CompareSetToOther(NamespaceList other)
		{
			NamespaceList namespaceList;
			if (this.set.Contains(other.targetNamespace))
			{
				if (this.set.Contains(string.Empty))
				{
					namespaceList = new NamespaceList();
				}
				else
				{
					namespaceList = new NamespaceList("##other", string.Empty);
				}
			}
			else if (this.set.Contains(string.Empty))
			{
				namespaceList = null;
			}
			else
			{
				namespaceList = other.Clone();
			}
			return namespaceList;
		}

		// Token: 0x060027F7 RID: 10231 RVA: 0x000ECCCC File Offset: 0x000EAECC
		public static NamespaceList Intersection(NamespaceList o1, NamespaceList o2, bool v1Compat)
		{
			NamespaceList namespaceList = null;
			if (o1.type == NamespaceList.ListType.Any)
			{
				namespaceList = o2.Clone();
			}
			else if (o2.type == NamespaceList.ListType.Any)
			{
				namespaceList = o1.Clone();
			}
			else if (o1.type == NamespaceList.ListType.Set && o2.type == NamespaceList.ListType.Other)
			{
				namespaceList = o1.Clone();
				namespaceList.RemoveNamespace(o2.targetNamespace);
				if (!v1Compat)
				{
					namespaceList.RemoveNamespace(string.Empty);
				}
			}
			else if (o1.type == NamespaceList.ListType.Other && o2.type == NamespaceList.ListType.Set)
			{
				namespaceList = o2.Clone();
				namespaceList.RemoveNamespace(o1.targetNamespace);
				if (!v1Compat)
				{
					namespaceList.RemoveNamespace(string.Empty);
				}
			}
			else
			{
				if (o1.type == NamespaceList.ListType.Set && o2.type == NamespaceList.ListType.Set)
				{
					namespaceList = o1.Clone();
					namespaceList = new NamespaceList();
					namespaceList.type = NamespaceList.ListType.Set;
					namespaceList.set = new Hashtable();
					using (IEnumerator enumerator = o1.set.Keys.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object obj = enumerator.Current;
							string text = (string)obj;
							if (o2.set.Contains(text))
							{
								namespaceList.set.Add(text, text);
							}
						}
						return namespaceList;
					}
				}
				if (o1.type == NamespaceList.ListType.Other && o2.type == NamespaceList.ListType.Other)
				{
					if (o1.targetNamespace == o2.targetNamespace)
					{
						namespaceList = o1.Clone();
						return namespaceList;
					}
					if (!v1Compat)
					{
						if (o1.targetNamespace == string.Empty)
						{
							namespaceList = o2.Clone();
						}
						else if (o2.targetNamespace == string.Empty)
						{
							namespaceList = o1.Clone();
						}
					}
				}
			}
			return namespaceList;
		}

		// Token: 0x060027F8 RID: 10232 RVA: 0x000ECE78 File Offset: 0x000EB078
		private void RemoveNamespace(string tns)
		{
			if (this.set[tns] != null)
			{
				this.set.Remove(tns);
			}
		}

		// Token: 0x060027F9 RID: 10233 RVA: 0x000ECE94 File Offset: 0x000EB094
		public bool IsEmpty()
		{
			return this.type == NamespaceList.ListType.Set && (this.set == null || this.set.Count == 0);
		}

		// Token: 0x04001A81 RID: 6785
		private NamespaceList.ListType type;

		// Token: 0x04001A82 RID: 6786
		private Hashtable set;

		// Token: 0x04001A83 RID: 6787
		private string targetNamespace;

		// Token: 0x02000406 RID: 1030
		public enum ListType
		{
			// Token: 0x04001A85 RID: 6789
			Any,
			// Token: 0x04001A86 RID: 6790
			Other,
			// Token: 0x04001A87 RID: 6791
			Set
		}
	}
}
