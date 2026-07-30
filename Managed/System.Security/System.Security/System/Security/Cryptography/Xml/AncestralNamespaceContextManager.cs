using System;
using System.Collections;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x0200003C RID: 60
	internal abstract class AncestralNamespaceContextManager
	{
		// Token: 0x06000141 RID: 321 RVA: 0x00004BCE File Offset: 0x00002DCE
		internal NamespaceFrame GetScopeAt(int i)
		{
			return (NamespaceFrame)this._ancestorStack[i];
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00004BE1 File Offset: 0x00002DE1
		internal NamespaceFrame GetCurrentScope()
		{
			return this.GetScopeAt(this._ancestorStack.Count - 1);
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00004BF8 File Offset: 0x00002DF8
		protected XmlAttribute GetNearestRenderedNamespaceWithMatchingPrefix(string nsPrefix, out int depth)
		{
			depth = -1;
			for (int i = this._ancestorStack.Count - 1; i >= 0; i--)
			{
				XmlAttribute rendered;
				if ((rendered = this.GetScopeAt(i).GetRendered(nsPrefix)) != null)
				{
					depth = i;
					return rendered;
				}
			}
			return null;
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00004C3C File Offset: 0x00002E3C
		protected XmlAttribute GetNearestUnrenderedNamespaceWithMatchingPrefix(string nsPrefix, out int depth)
		{
			depth = -1;
			for (int i = this._ancestorStack.Count - 1; i >= 0; i--)
			{
				XmlAttribute unrendered;
				if ((unrendered = this.GetScopeAt(i).GetUnrendered(nsPrefix)) != null)
				{
					depth = i;
					return unrendered;
				}
			}
			return null;
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00004C7D File Offset: 0x00002E7D
		internal void EnterElementContext()
		{
			this._ancestorStack.Add(new NamespaceFrame());
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00004C90 File Offset: 0x00002E90
		internal void ExitElementContext()
		{
			this._ancestorStack.RemoveAt(this._ancestorStack.Count - 1);
		}

		// Token: 0x06000147 RID: 327
		internal abstract void TrackNamespaceNode(XmlAttribute attr, SortedList nsListToRender, Hashtable nsLocallyDeclared);

		// Token: 0x06000148 RID: 328
		internal abstract void TrackXmlNamespaceNode(XmlAttribute attr, SortedList nsListToRender, SortedList attrListToRender, Hashtable nsLocallyDeclared);

		// Token: 0x06000149 RID: 329
		internal abstract void GetNamespacesToRender(XmlElement element, SortedList attrListToRender, SortedList nsListToRender, Hashtable nsLocallyDeclared);

		// Token: 0x0600014A RID: 330 RVA: 0x00004CAC File Offset: 0x00002EAC
		internal void LoadUnrenderedNamespaces(Hashtable nsLocallyDeclared)
		{
			object[] array = new object[nsLocallyDeclared.Count];
			nsLocallyDeclared.Values.CopyTo(array, 0);
			foreach (object obj in array)
			{
				this.AddUnrendered((XmlAttribute)obj);
			}
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00004CF4 File Offset: 0x00002EF4
		internal void LoadRenderedNamespaces(SortedList nsRenderedList)
		{
			foreach (object obj in nsRenderedList.GetKeyList())
			{
				this.AddRendered((XmlAttribute)obj);
			}
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00004D50 File Offset: 0x00002F50
		internal void AddRendered(XmlAttribute attr)
		{
			this.GetCurrentScope().AddRendered(attr);
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00004D5E File Offset: 0x00002F5E
		internal void AddUnrendered(XmlAttribute attr)
		{
			this.GetCurrentScope().AddUnrendered(attr);
		}

		// Token: 0x04000109 RID: 265
		internal ArrayList _ancestorStack = new ArrayList();
	}
}
