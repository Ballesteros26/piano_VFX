using System;
using Mono.WebBrowser.DOM;

namespace Mono.Mozilla.DOM
{
	// Token: 0x02000141 RID: 321
	internal class Stylesheet : DOMObject, IStylesheet
	{
		// Token: 0x06000A1C RID: 2588 RVA: 0x000098FD File Offset: 0x00007AFD
		public Stylesheet(WebBrowser control, nsIDOMStyleSheet stylesheet)
			: base(control)
		{
			if (control.platform != control.enginePlatform)
			{
				this.unmanagedStyle = nsDOMStyleSheet.GetProxy(control, stylesheet);
			}
			else
			{
				this.unmanagedStyle = stylesheet;
			}
			this.hashcode = this.unmanagedStyle.GetHashCode();
		}

		// Token: 0x06000A1D RID: 2589 RVA: 0x0000993B File Offset: 0x00007B3B
		protected override void Dispose(bool disposing)
		{
			if (!this.disposed && disposing)
			{
				this.unmanagedStyle = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000A1E RID: 2590 RVA: 0x00009956 File Offset: 0x00007B56
		public string Type
		{
			get
			{
				this.unmanagedStyle.getType(this.storage);
				return Base.StringGet(this.storage);
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000A1F RID: 2591 RVA: 0x00009975 File Offset: 0x00007B75
		public string Href
		{
			get
			{
				this.unmanagedStyle.getHref(this.storage);
				return Base.StringGet(this.storage);
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000A20 RID: 2592 RVA: 0x00009994 File Offset: 0x00007B94
		// (set) Token: 0x06000A21 RID: 2593 RVA: 0x000099B0 File Offset: 0x00007BB0
		public bool Disabled
		{
			get
			{
				bool flag;
				this.unmanagedStyle.getDisabled(out flag);
				return flag;
			}
			set
			{
				this.unmanagedStyle.setDisabled(value);
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000A22 RID: 2594 RVA: 0x000099C0 File Offset: 0x00007BC0
		public INode OwnerNode
		{
			get
			{
				nsIDOMNode nsIDOMNode;
				this.unmanagedStyle.getOwnerNode(out nsIDOMNode);
				return base.GetTypedNode(nsIDOMNode);
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000A23 RID: 2595 RVA: 0x000099E4 File Offset: 0x00007BE4
		public IStylesheet ParentStyleSheet
		{
			get
			{
				nsIDOMStyleSheet nsIDOMStyleSheet;
				this.unmanagedStyle.getParentStyleSheet(out nsIDOMStyleSheet);
				return new Stylesheet(this.control, nsIDOMStyleSheet);
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000A24 RID: 2596 RVA: 0x00009A0B File Offset: 0x00007C0B
		public string Title
		{
			get
			{
				this.unmanagedStyle.getTitle(this.storage);
				return Base.StringGet(this.storage);
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000A25 RID: 2597 RVA: 0x00009A2A File Offset: 0x00007C2A
		public IMediaList Media
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06000A26 RID: 2598 RVA: 0x00009A2D File Offset: 0x00007C2D
		public override int GetHashCode()
		{
			return this.hashcode;
		}

		// Token: 0x0400012D RID: 301
		private nsIDOMStyleSheet unmanagedStyle;

		// Token: 0x0400012E RID: 302
		protected int hashcode;
	}
}
