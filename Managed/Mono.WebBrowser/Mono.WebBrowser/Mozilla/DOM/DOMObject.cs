using System;
using System.Collections;
using System.ComponentModel;
using System.Runtime.InteropServices;
using Mono.WebBrowser.DOM;

namespace Mono.Mozilla.DOM
{
	// Token: 0x02000135 RID: 309
	internal class DOMObject : IDisposable
	{
		// Token: 0x0600090D RID: 2317 RVA: 0x00006098 File Offset: 0x00004298
		internal DOMObject(WebBrowser control)
		{
			this.control = control;
			IntPtr intPtr = Base.StringInit();
			this.storage = new HandleRef(this, intPtr);
			this.resources = new Hashtable();
			this.event_handlers = null;
		}

		// Token: 0x0600090E RID: 2318 RVA: 0x000060D8 File Offset: 0x000042D8
		~DOMObject()
		{
			this.Dispose(false);
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x00006108 File Offset: 0x00004308
		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					Base.StringFinish(this.storage);
				}
				this.disposed = true;
			}
		}

		// Token: 0x06000910 RID: 2320 RVA: 0x00006127 File Offset: 0x00004327
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000911 RID: 2321 RVA: 0x00006136 File Offset: 0x00004336
		protected EventHandlerList Events
		{
			get
			{
				if (this.event_handlers == null)
				{
					this.event_handlers = new EventHandlerList();
				}
				return this.event_handlers;
			}
		}

		// Token: 0x06000912 RID: 2322 RVA: 0x00006154 File Offset: 0x00004354
		internal INode GetTypedNode(nsIDOMNode obj)
		{
			if (obj == null)
			{
				return null;
			}
			obj.getLocalName(this.storage);
			ushort num;
			obj.getNodeType(out num);
			switch (num)
			{
			case 1:
				if (obj is nsIDOMHTMLBodyElement)
				{
					return new HTMLElement(this.control, obj as nsIDOMHTMLBodyElement);
				}
				if (obj is nsIDOMHTMLStyleElement)
				{
					return new HTMLElement(this.control, obj as nsIDOMHTMLStyleElement);
				}
				if (obj is nsIDOMHTMLElement)
				{
					return new HTMLElement(this.control, obj as nsIDOMHTMLElement);
				}
				return new Element(this.control, obj as nsIDOMElement);
			case 2:
				return new Attribute(this.control, obj as nsIDOMAttr);
			case 9:
				if (obj is nsIDOMHTMLDocument)
				{
					return new Document(this.control, obj as nsIDOMHTMLDocument);
				}
				return new Document(this.control, obj as nsIDOMDocument);
			}
			return new Node(this.control, obj);
		}

		// Token: 0x04000114 RID: 276
		private EventHandlerList event_handlers;

		// Token: 0x04000115 RID: 277
		protected WebBrowser control;

		// Token: 0x04000116 RID: 278
		internal HandleRef storage;

		// Token: 0x04000117 RID: 279
		protected bool disposed;

		// Token: 0x04000118 RID: 280
		protected Hashtable resources;
	}
}
