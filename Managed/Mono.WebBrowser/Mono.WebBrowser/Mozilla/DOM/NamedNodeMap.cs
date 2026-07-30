using System;
using System.Collections;
using Mono.WebBrowser.DOM;

namespace Mono.Mozilla.DOM
{
	// Token: 0x0200013D RID: 317
	internal class NamedNodeMap : NodeList, INamedNodeMap, IList, ICollection, IEnumerable
	{
		// Token: 0x060009A3 RID: 2467 RVA: 0x000080D1 File Offset: 0x000062D1
		public NamedNodeMap(WebBrowser control, nsIDOMNamedNodeMap nodeMap)
			: base(control, true)
		{
			if (control.platform != control.enginePlatform)
			{
				this.unmanagedNodes = nsDOMNamedNodeMap.GetProxy(control, nodeMap);
				return;
			}
			this.unmanagedNodes = nodeMap;
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x00008100 File Offset: 0x00006300
		internal override void Load()
		{
			base.Clear();
			uint num;
			this.unmanagedNodes.getLength(out num);
			this.nodeCount = (int)num;
			this.nodes = new Node[num];
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				nsIDOMNode nsIDOMNode;
				this.unmanagedNodes.item((uint)num2, out nsIDOMNode);
				this.nodes[num2] = new Attribute(this.control, nsIDOMNode as nsIDOMAttr);
				num2++;
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060009A5 RID: 2469 RVA: 0x0000816A File Offset: 0x0000636A
		public override int Count
		{
			get
			{
				if (this.unmanagedNodes != null && this.nodes == null)
				{
					this.Load();
				}
				return this.nodeCount;
			}
		}

		// Token: 0x170000E6 RID: 230
		public new INode this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				return this.nodes[index];
			}
			set
			{
			}
		}

		// Token: 0x170000E7 RID: 231
		public INode this[string name]
		{
			get
			{
				Base.StringSet(this.storage, name);
				nsIDOMNode nsIDOMNode;
				this.unmanagedNodes.getNamedItem(this.storage, out nsIDOMNode);
				for (int i = 0; i < this.Count; i++)
				{
					if (this.nodes[i].GetHashCode().Equals(nsIDOMNode.GetHashCode()))
					{
						return this.nodes[i];
					}
				}
				return null;
			}
			set
			{
			}
		}

		// Token: 0x060009AA RID: 2474 RVA: 0x00008214 File Offset: 0x00006414
		public INode RemoveNamedItem(string name)
		{
			Base.StringSet(this.storage, name);
			nsIDOMNode nsIDOMNode;
			this.unmanagedNodes.removeNamedItem(this.storage, out nsIDOMNode);
			for (int i = 0; i < this.Count; i++)
			{
				if (this.nodes[i].GetHashCode().Equals(nsIDOMNode.GetHashCode()))
				{
					INode node = this.nodes[i];
					base.Remove(this.nodes[i]);
					return node;
				}
			}
			return null;
		}

		// Token: 0x170000E8 RID: 232
		public INode this[string namespaceURI, string localName]
		{
			get
			{
				Base.StringSet(this.storage, namespaceURI);
				UniString uniString = new UniString(localName);
				nsIDOMNode nsIDOMNode;
				this.unmanagedNodes.getNamedItemNS(this.storage, uniString.Handle, out nsIDOMNode);
				for (int i = 0; i < this.Count; i++)
				{
					if (this.nodes[i].GetHashCode().Equals(nsIDOMNode.GetHashCode()))
					{
						return this.nodes[i];
					}
				}
				return null;
			}
			set
			{
			}
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x000082FC File Offset: 0x000064FC
		public INode RemoveNamedItemNS(string namespaceURI, string localName)
		{
			Base.StringSet(this.storage, namespaceURI);
			UniString uniString = new UniString(localName);
			nsIDOMNode nsIDOMNode;
			this.unmanagedNodes.removeNamedItemNS(this.storage, uniString.Handle, out nsIDOMNode);
			for (int i = 0; i < this.Count; i++)
			{
				if (this.nodes[i].GetHashCode().Equals(nsIDOMNode.GetHashCode()))
				{
					INode node = this.nodes[i];
					base.Remove(this.nodes[i]);
					return node;
				}
			}
			return null;
		}

		// Token: 0x060009AE RID: 2478 RVA: 0x0000837C File Offset: 0x0000657C
		public override int GetHashCode()
		{
			return this.unmanagedNodes.GetHashCode();
		}

		// Token: 0x04000122 RID: 290
		protected new nsIDOMNamedNodeMap unmanagedNodes;
	}
}
