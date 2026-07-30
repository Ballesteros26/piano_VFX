using System;
using System.Collections;
using Mono.WebBrowser.DOM;

namespace Mono.Mozilla.DOM
{
	// Token: 0x02000132 RID: 306
	internal class AttributeCollection : NodeList, IAttributeCollection, INodeList, IList, ICollection, IEnumerable
	{
		// Token: 0x060008F3 RID: 2291 RVA: 0x00005C7B File Offset: 0x00003E7B
		public AttributeCollection(WebBrowser control, nsIDOMNamedNodeMap nodeMap)
			: base(control, true)
		{
			if (control.platform != control.enginePlatform)
			{
				this.unmanagedNodes = nsDOMNamedNodeMap.GetProxy(control, nodeMap);
				return;
			}
			this.unmanagedNodes = nodeMap;
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x00005CA8 File Offset: 0x00003EA8
		public AttributeCollection(WebBrowser control)
			: base(control)
		{
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x00005CB4 File Offset: 0x00003EB4
		internal override void Load()
		{
			if (this.unmanagedNodes == null)
			{
				return;
			}
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

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060008F6 RID: 2294 RVA: 0x00005D27 File Offset: 0x00003F27
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

		// Token: 0x1700009C RID: 156
		public IAttribute this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				return this.nodes[index] as IAttribute;
			}
			set
			{
			}
		}

		// Token: 0x1700009D RID: 157
		public IAttribute this[string name]
		{
			get
			{
				for (int i = 0; i < this.nodes.Length; i++)
				{
					if (((IAttribute)this.nodes[i]).Name.Equals(name))
					{
						return this.nodes[i] as IAttribute;
					}
				}
				return null;
			}
		}

		// Token: 0x060008FA RID: 2298 RVA: 0x00005DBC File Offset: 0x00003FBC
		public bool Exists(string name)
		{
			if (this.unmanagedNodes == null)
			{
				return false;
			}
			Base.StringSet(this.storage, name);
			nsIDOMNode nsIDOMNode;
			this.unmanagedNodes.getNamedItem(this.storage, out nsIDOMNode);
			return nsIDOMNode != null;
		}

		// Token: 0x060008FB RID: 2299 RVA: 0x00005DF7 File Offset: 0x00003FF7
		public override int GetHashCode()
		{
			if (this.unmanagedNodes == null)
			{
				return base.GetHashCode();
			}
			return this.unmanagedNodes.GetHashCode();
		}

		// Token: 0x0400010F RID: 271
		protected new nsIDOMNamedNodeMap unmanagedNodes;
	}
}
