using System;
using System.Collections;
using Mono.WebBrowser.DOM;

namespace Mono.Mozilla.DOM
{
	// Token: 0x02000140 RID: 320
	internal class NodeList : DOMObject, INodeList, IList, ICollection, IEnumerable
	{
		// Token: 0x060009FD RID: 2557 RVA: 0x000095A1 File Offset: 0x000077A1
		public NodeList(WebBrowser control, nsIDOMNodeList nodeList)
			: base(control)
		{
			if (control.platform != control.enginePlatform)
			{
				this.unmanagedNodes = nsDOMNodeList.GetProxy(control, nodeList);
				return;
			}
			this.unmanagedNodes = nodeList;
		}

		// Token: 0x060009FE RID: 2558 RVA: 0x000095CD File Offset: 0x000077CD
		public NodeList(WebBrowser control)
			: base(control)
		{
			this.nodes = new Node[0];
		}

		// Token: 0x060009FF RID: 2559 RVA: 0x000095E2 File Offset: 0x000077E2
		public NodeList(WebBrowser control, bool loaded)
			: base(control)
		{
		}

		// Token: 0x06000A00 RID: 2560 RVA: 0x000095EB File Offset: 0x000077EB
		protected override void Dispose(bool disposing)
		{
			if (!this.disposed && disposing)
			{
				this.Clear();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000A01 RID: 2561 RVA: 0x00009608 File Offset: 0x00007808
		protected void Clear()
		{
			if (this.nodes != null)
			{
				for (int i = 0; i < this.nodeCount; i++)
				{
					this.nodes[i] = null;
				}
				this.nodeCount = 0;
				this.unmanagedNodes = null;
				this.nodes = null;
			}
		}

		// Token: 0x06000A02 RID: 2562 RVA: 0x0000964C File Offset: 0x0000784C
		internal virtual void Load()
		{
			if (this.unmanagedNodes == null)
			{
				return;
			}
			this.Clear();
			uint num;
			this.unmanagedNodes.getLength(out num);
			this.nodeCount = (int)num;
			this.nodes = new Node[this.nodeCount];
			for (int i = 0; i < this.nodeCount; i++)
			{
				nsIDOMNode nsIDOMNode;
				this.unmanagedNodes.item((uint)i, out nsIDOMNode);
				ushort num2;
				nsIDOMNode.getNodeType(out num2);
				this.nodes[i] = base.GetTypedNode(nsIDOMNode);
			}
		}

		// Token: 0x06000A03 RID: 2563 RVA: 0x000096C6 File Offset: 0x000078C6
		public IEnumerator GetEnumerator()
		{
			return new NodeList.NodeListEnumerator(this);
		}

		// Token: 0x06000A04 RID: 2564 RVA: 0x000096CE File Offset: 0x000078CE
		public void CopyTo(Array dest, int index)
		{
			if (this.nodes != null)
			{
				Array.Copy(this.nodes, 0, dest, index, this.Count);
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000A05 RID: 2565 RVA: 0x000096EC File Offset: 0x000078EC
		public virtual int Count
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

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000A06 RID: 2566 RVA: 0x0000970A File Offset: 0x0000790A
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000A07 RID: 2567 RVA: 0x0000970D File Offset: 0x0000790D
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000A08 RID: 2568 RVA: 0x00009710 File Offset: 0x00007910
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000A09 RID: 2569 RVA: 0x00009713 File Offset: 0x00007913
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000A0A RID: 2570 RVA: 0x00009716 File Offset: 0x00007916
		void IList.RemoveAt(int index)
		{
			this.RemoveAt(index);
		}

		// Token: 0x06000A0B RID: 2571 RVA: 0x00009720 File Offset: 0x00007920
		public void RemoveAt(int index)
		{
			if (index > this.Count || index < 0)
			{
				return;
			}
			Array.Copy(this.nodes, index + 1, this.nodes, index, this.nodeCount - index - 1);
			this.nodeCount--;
			this.nodes[this.nodeCount] = null;
		}

		// Token: 0x06000A0C RID: 2572 RVA: 0x00009776 File Offset: 0x00007976
		public void Remove(INode node)
		{
			this.RemoveAt(this.IndexOf(node));
		}

		// Token: 0x06000A0D RID: 2573 RVA: 0x00009785 File Offset: 0x00007985
		void IList.Remove(object node)
		{
			this.Remove(node as INode);
		}

		// Token: 0x06000A0E RID: 2574 RVA: 0x00009794 File Offset: 0x00007994
		public void Insert(int index, INode value)
		{
			if (index > this.Count)
			{
				index = this.nodeCount;
			}
			INode[] array = new Node[this.nodeCount + 1];
			if (index > 0)
			{
				Array.Copy(this.nodes, 0, array, 0, index);
			}
			array[index] = value;
			if (index < this.nodeCount)
			{
				Array.Copy(this.nodes, index, array, index + 1, this.nodeCount - index);
			}
			this.nodes = array;
			this.nodeCount++;
		}

		// Token: 0x06000A0F RID: 2575 RVA: 0x0000980D File Offset: 0x00007A0D
		void IList.Insert(int index, object value)
		{
			this.Insert(index, value as INode);
		}

		// Token: 0x06000A10 RID: 2576 RVA: 0x0000981C File Offset: 0x00007A1C
		public int IndexOf(INode node)
		{
			return Array.IndexOf<INode>(this.nodes, node);
		}

		// Token: 0x06000A11 RID: 2577 RVA: 0x0000982A File Offset: 0x00007A2A
		int IList.IndexOf(object node)
		{
			return this.IndexOf(node as INode);
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x00009838 File Offset: 0x00007A38
		public bool Contains(INode node)
		{
			return this.IndexOf(node) != -1;
		}

		// Token: 0x06000A13 RID: 2579 RVA: 0x00009847 File Offset: 0x00007A47
		bool IList.Contains(object node)
		{
			return this.Contains(node as INode);
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x00009855 File Offset: 0x00007A55
		void IList.Clear()
		{
			this.Clear();
		}

		// Token: 0x06000A15 RID: 2581 RVA: 0x0000985D File Offset: 0x00007A5D
		public int Add(INode node)
		{
			this.Insert(this.Count + 1, node);
			return this.nodeCount - 1;
		}

		// Token: 0x06000A16 RID: 2582 RVA: 0x00009876 File Offset: 0x00007A76
		int IList.Add(object node)
		{
			return this.Add(node as INode);
		}

		// Token: 0x17000103 RID: 259
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				this[index] = value as INode;
			}
		}

		// Token: 0x17000104 RID: 260
		public INode this[int index]
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
				if (index < 0 || index >= this.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				this.nodes[index] = value;
			}
		}

		// Token: 0x06000A1B RID: 2587 RVA: 0x000098E1 File Offset: 0x00007AE1
		public override int GetHashCode()
		{
			if (this.unmanagedNodes != null)
			{
				return this.unmanagedNodes.GetHashCode();
			}
			return base.GetHashCode();
		}

		// Token: 0x0400012A RID: 298
		protected nsIDOMNodeList unmanagedNodes;

		// Token: 0x0400012B RID: 299
		protected INode[] nodes;

		// Token: 0x0400012C RID: 300
		protected int nodeCount;

		// Token: 0x0200014C RID: 332
		internal class NodeListEnumerator : IEnumerator
		{
			// Token: 0x06000A75 RID: 2677 RVA: 0x0000A344 File Offset: 0x00008544
			public NodeListEnumerator(NodeList collection)
			{
				this.collection = collection;
			}

			// Token: 0x1700011D RID: 285
			// (get) Token: 0x06000A76 RID: 2678 RVA: 0x0000A35A File Offset: 0x0000855A
			public object Current
			{
				get
				{
					if (this.index == -1)
					{
						return null;
					}
					return this.collection[this.index];
				}
			}

			// Token: 0x06000A77 RID: 2679 RVA: 0x0000A378 File Offset: 0x00008578
			public bool MoveNext()
			{
				if (this.index + 1 >= this.collection.Count)
				{
					return false;
				}
				this.index++;
				return true;
			}

			// Token: 0x06000A78 RID: 2680 RVA: 0x0000A3A0 File Offset: 0x000085A0
			public void Reset()
			{
				this.index = -1;
			}

			// Token: 0x0400016F RID: 367
			private NodeList collection;

			// Token: 0x04000170 RID: 368
			private int index = -1;
		}
	}
}
