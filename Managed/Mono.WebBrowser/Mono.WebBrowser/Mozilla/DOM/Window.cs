using System;
using Mono.WebBrowser.DOM;

namespace Mono.Mozilla.DOM
{
	// Token: 0x02000143 RID: 323
	internal class Window : DOMObject, IWindow
	{
		// Token: 0x06000A2F RID: 2607 RVA: 0x00009B4B File Offset: 0x00007D4B
		public Window(WebBrowser control, nsIDOMWindow domWindow)
			: base(control)
		{
			this.hashcode = domWindow.GetHashCode();
			this.window = domWindow;
		}

		// Token: 0x06000A30 RID: 2608 RVA: 0x00009B67 File Offset: 0x00007D67
		protected override void Dispose(bool disposing)
		{
			if (!this.disposed && disposing)
			{
				this.resources.Clear();
				this.window = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x00009B90 File Offset: 0x00007D90
		internal static bool FindDocument(ref nsIDOMWindow window, int docHashcode)
		{
			nsIDOMDocument nsIDOMDocument;
			window.getDocument(out nsIDOMDocument);
			if (nsIDOMDocument.GetHashCode() == docHashcode)
			{
				return true;
			}
			uint num = 1U;
			nsIDOMWindowCollection nsIDOMWindowCollection;
			window.getFrames(out nsIDOMWindowCollection);
			nsIDOMWindowCollection.getLength(out num);
			for (uint num2 = 0U; num2 < num; num2 += 1U)
			{
				nsIDOMWindowCollection.item(num2, out window);
				if (Window.FindDocument(ref window, docHashcode))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000A32 RID: 2610 RVA: 0x00009BE8 File Offset: 0x00007DE8
		public IDocument Document
		{
			get
			{
				nsIDOMDocument nsIDOMDocument;
				this.window.getDocument(out nsIDOMDocument);
				if (!this.control.documents.ContainsKey(nsIDOMDocument.GetHashCode()))
				{
					this.control.documents.Add(nsIDOMDocument.GetHashCode(), new Document(this.control, (nsIDOMHTMLDocument)nsIDOMDocument));
				}
				return this.control.documents[nsIDOMDocument.GetHashCode()] as IDocument;
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000A33 RID: 2611 RVA: 0x00009C6C File Offset: 0x00007E6C
		public IWindowCollection Frames
		{
			get
			{
				nsIDOMWindowCollection nsIDOMWindowCollection;
				this.window.getFrames(out nsIDOMWindowCollection);
				return new WindowCollection(this.control, nsIDOMWindowCollection);
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000A34 RID: 2612 RVA: 0x00009C93 File Offset: 0x00007E93
		// (set) Token: 0x06000A35 RID: 2613 RVA: 0x00009CB2 File Offset: 0x00007EB2
		public string Name
		{
			get
			{
				this.window.getName(this.storage);
				return Base.StringGet(this.storage);
			}
			set
			{
				Base.StringSet(this.storage, value);
				this.window.setName(this.storage);
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000A36 RID: 2614 RVA: 0x00009CD4 File Offset: 0x00007ED4
		public IWindow Parent
		{
			get
			{
				nsIDOMWindow nsIDOMWindow;
				this.window.getParent(out nsIDOMWindow);
				return new Window(this.control, nsIDOMWindow);
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000A37 RID: 2615 RVA: 0x00009CFC File Offset: 0x00007EFC
		public IWindow Top
		{
			get
			{
				nsIDOMWindow nsIDOMWindow;
				this.window.getTop(out nsIDOMWindow);
				return new Window(this.control, nsIDOMWindow);
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000A38 RID: 2616 RVA: 0x00009D23 File Offset: 0x00007F23
		public string StatusText
		{
			get
			{
				return this.control.StatusText;
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000A39 RID: 2617 RVA: 0x00009D30 File Offset: 0x00007F30
		public IHistory History
		{
			get
			{
				Navigation navigation = new Navigation(this.control, this.window as nsIWebNavigation);
				return new History(this.control, navigation);
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000A3A RID: 2618 RVA: 0x00009D60 File Offset: 0x00007F60
		private EventListener EventListener
		{
			get
			{
				if (this.eventListener == null)
				{
					this.eventListener = new EventListener(this.window as nsIDOMEventTarget, this);
				}
				return this.eventListener;
			}
		}

		// Token: 0x06000A3B RID: 2619 RVA: 0x00009D87 File Offset: 0x00007F87
		public void AttachEventHandler(string eventName, EventHandler handler)
		{
			this.EventListener.AddHandler(handler, eventName);
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x00009D96 File Offset: 0x00007F96
		public void DetachEventHandler(string eventName, EventHandler handler)
		{
			this.EventListener.RemoveHandler(handler, eventName);
		}

		// Token: 0x06000A3D RID: 2621 RVA: 0x00009DA5 File Offset: 0x00007FA5
		public void Focus()
		{
			((nsIWebBrowserFocus)this.window).setFocusedWindow(this.window);
		}

		// Token: 0x06000A3E RID: 2622 RVA: 0x00009DBE File Offset: 0x00007FBE
		public void Open(string url)
		{
			((nsIWebNavigation)this.window).loadURI(url, 0U, null, null, null);
		}

		// Token: 0x06000A3F RID: 2623 RVA: 0x00009DD6 File Offset: 0x00007FD6
		public void ScrollTo(int x, int y)
		{
			this.window.scrollTo(x, y);
		}

		// Token: 0x06000A40 RID: 2624 RVA: 0x00009DE6 File Offset: 0x00007FE6
		public override bool Equals(object obj)
		{
			return this == obj as Window;
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x00009DF4 File Offset: 0x00007FF4
		public static bool operator ==(Window left, Window right)
		{
			return left == right || (left != null && right != null && left.hashcode == right.hashcode);
		}

		// Token: 0x06000A42 RID: 2626 RVA: 0x00009E12 File Offset: 0x00008012
		public static bool operator !=(Window left, Window right)
		{
			return !(left == right);
		}

		// Token: 0x06000A43 RID: 2627 RVA: 0x00009E1E File Offset: 0x0000801E
		public override int GetHashCode()
		{
			return this.hashcode;
		}

		// Token: 0x14000051 RID: 81
		// (add) Token: 0x06000A44 RID: 2628 RVA: 0x00009E26 File Offset: 0x00008026
		// (remove) Token: 0x06000A45 RID: 2629 RVA: 0x00009E45 File Offset: 0x00008045
		public event EventHandler Load
		{
			add
			{
				base.Events.AddHandler(Window.LoadEvent, value);
				this.AttachEventHandler("load", value);
			}
			remove
			{
				base.Events.RemoveHandler(Window.LoadEvent, value);
				this.DetachEventHandler("load", value);
			}
		}

		// Token: 0x14000052 RID: 82
		// (add) Token: 0x06000A46 RID: 2630 RVA: 0x00009E64 File Offset: 0x00008064
		// (remove) Token: 0x06000A47 RID: 2631 RVA: 0x00009E83 File Offset: 0x00008083
		public event EventHandler Unload
		{
			add
			{
				base.Events.AddHandler(Window.UnloadEvent, value);
				this.AttachEventHandler("unload", value);
			}
			remove
			{
				base.Events.RemoveHandler(Window.UnloadEvent, value);
				this.DetachEventHandler("unload", value);
			}
		}

		// Token: 0x14000053 RID: 83
		// (add) Token: 0x06000A48 RID: 2632 RVA: 0x00009EA2 File Offset: 0x000080A2
		// (remove) Token: 0x06000A49 RID: 2633 RVA: 0x00009EB0 File Offset: 0x000080B0
		public event EventHandler OnFocus
		{
			add
			{
				this.AttachEventHandler("focus", value);
			}
			remove
			{
				this.DetachEventHandler("focus", value);
			}
		}

		// Token: 0x14000054 RID: 84
		// (add) Token: 0x06000A4A RID: 2634 RVA: 0x00009EBE File Offset: 0x000080BE
		// (remove) Token: 0x06000A4B RID: 2635 RVA: 0x00009ECC File Offset: 0x000080CC
		public event EventHandler OnBlur
		{
			add
			{
				this.AttachEventHandler("blur", value);
			}
			remove
			{
				this.DetachEventHandler("blur", value);
			}
		}

		// Token: 0x14000055 RID: 85
		// (add) Token: 0x06000A4C RID: 2636 RVA: 0x00009EDA File Offset: 0x000080DA
		// (remove) Token: 0x06000A4D RID: 2637 RVA: 0x00009EE8 File Offset: 0x000080E8
		public event EventHandler Error
		{
			add
			{
				this.AttachEventHandler("error", value);
			}
			remove
			{
				this.DetachEventHandler("error", value);
			}
		}

		// Token: 0x14000056 RID: 86
		// (add) Token: 0x06000A4E RID: 2638 RVA: 0x00009EF6 File Offset: 0x000080F6
		// (remove) Token: 0x06000A4F RID: 2639 RVA: 0x00009F04 File Offset: 0x00008104
		public event EventHandler Scroll
		{
			add
			{
				this.AttachEventHandler("scroll", value);
			}
			remove
			{
				this.DetachEventHandler("scroll", value);
			}
		}

		// Token: 0x06000A50 RID: 2640 RVA: 0x00009F14 File Offset: 0x00008114
		public void OnLoad()
		{
			EventHandler eventHandler = (EventHandler)base.Events[Window.LoadEvent];
			if (eventHandler != null)
			{
				EventArgs eventArgs = new EventArgs();
				eventHandler(this, eventArgs);
			}
		}

		// Token: 0x06000A51 RID: 2641 RVA: 0x00009F48 File Offset: 0x00008148
		public void OnUnload()
		{
			EventHandler eventHandler = (EventHandler)base.Events[Window.UnloadEvent];
			if (eventHandler != null)
			{
				EventArgs eventArgs = new EventArgs();
				eventHandler(this, eventArgs);
			}
		}

		// Token: 0x06000A52 RID: 2642 RVA: 0x00009F7C File Offset: 0x0000817C
		// Note: this type is marked as 'beforefieldinit'.
		static Window()
		{
			Window.LoadEvent = new object();
			Window.UnloadEvent = new object();
		}

		// Token: 0x04000131 RID: 305
		internal nsIDOMWindow window;

		// Token: 0x04000132 RID: 306
		private EventListener eventListener;

		// Token: 0x04000133 RID: 307
		private int hashcode;
	}
}
