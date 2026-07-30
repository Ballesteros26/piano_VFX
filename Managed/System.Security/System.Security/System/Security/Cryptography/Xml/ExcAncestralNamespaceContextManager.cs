using System;
using System.Collections;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x0200005E RID: 94
	internal class ExcAncestralNamespaceContextManager : AncestralNamespaceContextManager
	{
		// Token: 0x0600026A RID: 618 RVA: 0x0000953D File Offset: 0x0000773D
		internal ExcAncestralNamespaceContextManager(string inclusiveNamespacesPrefixList)
		{
			this._inclusivePrefixSet = Utils.TokenizePrefixListString(inclusiveNamespacesPrefixList);
		}

		// Token: 0x0600026B RID: 619 RVA: 0x00009554 File Offset: 0x00007754
		private bool HasNonRedundantInclusivePrefix(XmlAttribute attr)
		{
			string namespacePrefix = Utils.GetNamespacePrefix(attr);
			int num;
			return this._inclusivePrefixSet.ContainsKey(namespacePrefix) && Utils.IsNonRedundantNamespaceDecl(attr, base.GetNearestRenderedNamespaceWithMatchingPrefix(namespacePrefix, out num));
		}

		// Token: 0x0600026C RID: 620 RVA: 0x00009588 File Offset: 0x00007788
		private void GatherNamespaceToRender(string nsPrefix, SortedList nsListToRender, Hashtable nsLocallyDeclared)
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
			XmlAttribute xmlAttribute = (XmlAttribute)nsLocallyDeclared[nsPrefix];
			int num;
			XmlAttribute nearestRenderedNamespaceWithMatchingPrefix = base.GetNearestRenderedNamespaceWithMatchingPrefix(nsPrefix, out num);
			if (xmlAttribute != null)
			{
				if (Utils.IsNonRedundantNamespaceDecl(xmlAttribute, nearestRenderedNamespaceWithMatchingPrefix))
				{
					nsLocallyDeclared.Remove(nsPrefix);
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
					nsListToRender.Add(nearestUnrenderedNamespaceWithMatchingPrefix, null);
				}
			}
		}

		// Token: 0x0600026D RID: 621 RVA: 0x00009640 File Offset: 0x00007840
		internal override void GetNamespacesToRender(XmlElement element, SortedList attrListToRender, SortedList nsListToRender, Hashtable nsLocallyDeclared)
		{
			this.GatherNamespaceToRender(element.Prefix, nsListToRender, nsLocallyDeclared);
			foreach (object obj in attrListToRender.GetKeyList())
			{
				string prefix = ((XmlAttribute)obj).Prefix;
				if (prefix.Length > 0)
				{
					this.GatherNamespaceToRender(prefix, nsListToRender, nsLocallyDeclared);
				}
			}
		}

		// Token: 0x0600026E RID: 622 RVA: 0x000096BC File Offset: 0x000078BC
		internal override void TrackNamespaceNode(XmlAttribute attr, SortedList nsListToRender, Hashtable nsLocallyDeclared)
		{
			if (!Utils.IsXmlPrefixDefinitionNode(attr))
			{
				if (this.HasNonRedundantInclusivePrefix(attr))
				{
					nsListToRender.Add(attr, null);
					return;
				}
				nsLocallyDeclared.Add(Utils.GetNamespacePrefix(attr), attr);
			}
		}

		// Token: 0x0600026F RID: 623 RVA: 0x000096E5 File Offset: 0x000078E5
		internal override void TrackXmlNamespaceNode(XmlAttribute attr, SortedList nsListToRender, SortedList attrListToRender, Hashtable nsLocallyDeclared)
		{
			attrListToRender.Add(attr, null);
		}

		// Token: 0x04000169 RID: 361
		private Hashtable _inclusivePrefixSet;
	}
}
