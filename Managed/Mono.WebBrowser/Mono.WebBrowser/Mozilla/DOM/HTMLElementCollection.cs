using System;
using System.Collections;
using Mono.WebBrowser.DOM;

namespace Mono.Mozilla.DOM
{
	// Token: 0x0200013B RID: 315
	internal class HTMLElementCollection : NodeList, IElementCollection, INodeList, IList, ICollection, IEnumerable
	{
		// Token: 0x06000998 RID: 2456 RVA: 0x00007F01 File Offset: 0x00006101
		public HTMLElementCollection(WebBrowser control, nsIDOMNodeList nodeList)
			: base(control, nodeList)
		{
		}

		// Token: 0x06000999 RID: 2457 RVA: 0x00007F0B File Offset: 0x0000610B
		public HTMLElementCollection(WebBrowser control)
			: base(control)
		{
		}

		// Token: 0x0600099A RID: 2458 RVA: 0x00007F14 File Offset: 0x00006114
		internal override void Load()
		{
			base.Clear();
			uint num;
			this.unmanagedNodes.getLength(out num);
			Node[] array = new Node[num];
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				nsIDOMNode nsIDOMNode;
				this.unmanagedNodes.item((uint)num2, out nsIDOMNode);
				ushort num3;
				nsIDOMNode.getNodeType(out num3);
				if (num3 == 1)
				{
					Node[] array2 = array;
					int nodeCount = this.nodeCount;
					this.nodeCount = nodeCount + 1;
					array2[nodeCount] = new HTMLElement(this.control, (nsIDOMHTMLElement)nsIDOMNode);
				}
				num2++;
			}
			this.nodes = new Node[this.nodeCount];
			Array.Copy(array, this.nodes, this.nodeCount);
		}

		// Token: 0x170000E3 RID: 227
		public IElement this[int index]
		{
			get
			{
				if (index < 0 || index >= this.nodeCount)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				return this.nodes[index] as IElement;
			}
			set
			{
				if (index < 0 || index >= this.nodeCount)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				this.nodes[index] = value;
			}
		}
	}
}
