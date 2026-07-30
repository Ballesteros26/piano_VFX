using System;
using System.Collections;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x0200003E RID: 62
	internal class C14NAncestralNamespaceContextManager : AncestralNamespaceContextManager
	{
		// Token: 0x06000151 RID: 337 RVA: 0x00004DCF File Offset: 0x00002FCF
		internal C14NAncestralNamespaceContextManager()
		{
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00004DD8 File Offset: 0x00002FD8
		private void GetNamespaceToRender(string nsPrefix, SortedList attrListToRender, SortedList nsListToRender, Hashtable nsLocallyDeclared)
		{
			using (IEnumerator enumerator = nsListToRender.GetKeyList().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (Utils.HasNamespacePrefix((XmlAttribute)enumerator.Current, nsPrefix))
					{
						return;
					}
				}
			}
			using (IEnumerator enumerator = attrListToRender.GetKeyList().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (((XmlAttribute)enumerator.Current).LocalName.Equals(nsPrefix))
					{
						return;
					}
				}
			}
			XmlAttribute xmlAttribute = (XmlAttribute)nsLocallyDeclared[nsPrefix];
			int num;
			XmlAttribute nearestRenderedNamespaceWithMatchingPrefix = base.GetNearestRenderedNamespaceWithMatchingPrefix(nsPrefix, out num);
			if (xmlAttribute != null)
			{
				if (Utils.IsNonRedundantNamespaceDecl(xmlAttribute, nearestRenderedNamespaceWithMatchingPrefix))
				{
					nsLocallyDeclared.Remove(nsPrefix);
					if (Utils.IsXmlNamespaceNode(xmlAttribute))
					{
						attrListToRender.Add(xmlAttribute, null);
						return;
					}
					nsListToRender.Add(xmlAttribute, null);
					return;
				}
			}
			else
			{
				int num2;
				XmlAttribute nearestUnrenderedNamespaceWithMatchingPrefix = base.GetNearestUnrenderedNamespaceWithMatchingPrefix(nsPrefix, out num2);
				if (nearestUnrenderedNamespaceWithMatchingPrefix != null && num2 > num && Utils.IsNonRedundantNamespaceDecl(nearestUnrenderedNamespaceWithMatchingPrefix, nearestRenderedNamespaceWithMatchingPrefix))
				{
					if (Utils.IsXmlNamespaceNode(nearestUnrenderedNamespaceWithMatchingPrefix))
					{
						attrListToRender.Add(nearestUnrenderedNamespaceWithMatchingPrefix, null);
						return;
					}
					nsListToRender.Add(nearestUnrenderedNamespaceWithMatchingPrefix, null);
				}
			}
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00004F10 File Offset: 0x00003110
		internal override void GetNamespacesToRender(XmlElement element, SortedList attrListToRender, SortedList nsListToRender, Hashtable nsLocallyDeclared)
		{
			object[] array = new object[nsLocallyDeclared.Count];
			nsLocallyDeclared.Values.CopyTo(array, 0);
			foreach (XmlAttribute xmlAttribute in array)
			{
				XmlAttribute xmlAttribute;
				int num;
				XmlAttribute nearestRenderedNamespaceWithMatchingPrefix = base.GetNearestRenderedNamespaceWithMatchingPrefix(Utils.GetNamespacePrefix(xmlAttribute), out num);
				if (Utils.IsNonRedundantNamespaceDecl(xmlAttribute, nearestRenderedNamespaceWithMatchingPrefix))
				{
					nsLocallyDeclared.Remove(Utils.GetNamespacePrefix(xmlAttribute));
					if (Utils.IsXmlNamespaceNode(xmlAttribute))
					{
						attrListToRender.Add(xmlAttribute, null);
					}
					else
					{
						nsListToRender.Add(xmlAttribute, null);
					}
				}
			}
			for (int j = this._ancestorStack.Count - 1; j >= 0; j--)
			{
				foreach (object obj in base.GetScopeAt(j).GetUnrendered().Values)
				{
					XmlAttribute xmlAttribute = (XmlAttribute)obj;
					if (xmlAttribute != null)
					{
						this.GetNamespaceToRender(Utils.GetNamespacePrefix(xmlAttribute), attrListToRender, nsListToRender, nsLocallyDeclared);
					}
				}
			}
		}

		// Token: 0x06000154 RID: 340 RVA: 0x0000501C File Offset: 0x0000321C
		internal override void TrackNamespaceNode(XmlAttribute attr, SortedList nsListToRender, Hashtable nsLocallyDeclared)
		{
			nsLocallyDeclared.Add(Utils.GetNamespacePrefix(attr), attr);
		}

		// Token: 0x06000155 RID: 341 RVA: 0x0000502B File Offset: 0x0000322B
		internal override void TrackXmlNamespaceNode(XmlAttribute attr, SortedList nsListToRender, SortedList attrListToRender, Hashtable nsLocallyDeclared)
		{
			nsLocallyDeclared.Add(Utils.GetNamespacePrefix(attr), attr);
		}
	}
}
