using System;

namespace Mono.WebBrowser.DOM
{
	// Token: 0x02000020 RID: 32
	public class NodeEventArgs : EventArgs
	{
		// Token: 0x060000A4 RID: 164 RVA: 0x000024C8 File Offset: 0x000006C8
		public NodeEventArgs(INode node)
		{
			this.node = node;
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x000024D7 File Offset: 0x000006D7
		public INode Node
		{
			get
			{
				return this.node;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x000024DF File Offset: 0x000006DF
		public IElement Element
		{
			get
			{
				if (this.node is IElement)
				{
					return (IElement)this.node;
				}
				return null;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x000024FB File Offset: 0x000006FB
		public IDocument Document
		{
			get
			{
				if (this.node is IDocument)
				{
					return (IDocument)this.node;
				}
				return null;
			}
		}

		// Token: 0x04000070 RID: 112
		private INode node;
	}
}
