using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using Mono.WebBrowser;

namespace Mono.Mozilla.DOM
{
	// Token: 0x02000133 RID: 307
	internal class ContentListener : nsIURIContentListener
	{
		// Token: 0x060008FC RID: 2300 RVA: 0x00005E13 File Offset: 0x00004013
		public ContentListener(WebBrowser instance)
		{
			this.owner = instance;
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060008FD RID: 2301 RVA: 0x00005E22 File Offset: 0x00004022
		public EventHandlerList Events
		{
			get
			{
				if (this.events == null)
				{
					this.events = new EventHandlerList();
				}
				return this.events;
			}
		}

		// Token: 0x060008FE RID: 2302 RVA: 0x00005E40 File Offset: 0x00004040
		public void AddHandler(NavigationRequestedEventHandler value)
		{
			if (this.Events[WebBrowser.NavigationRequestedEvent] == null && this.owner.Navigation != null)
			{
				((nsIWebBrowser)this.owner.navigation.navigation).setParentURIContentListener(this);
			}
			this.Events.AddHandler(WebBrowser.NavigationRequestedEvent, value);
		}

		// Token: 0x060008FF RID: 2303 RVA: 0x00005E99 File Offset: 0x00004099
		public void RemoveHandler(NavigationRequestedEventHandler value)
		{
			this.Events.RemoveHandler(WebBrowser.NavigationRequestedEvent, value);
		}

		// Token: 0x06000900 RID: 2304 RVA: 0x00005EAC File Offset: 0x000040AC
		bool nsIURIContentListener.onStartURIOpen(nsIURI aURI)
		{
			NavigationRequestedEventHandler navigationRequestedEventHandler = (NavigationRequestedEventHandler)this.Events[WebBrowser.NavigationRequestedEvent];
			if (navigationRequestedEventHandler != null)
			{
				AsciiString asciiString = new AsciiString("");
				aURI.getSpec(asciiString.Handle);
				NavigationRequestedEventArgs navigationRequestedEventArgs = new NavigationRequestedEventArgs(asciiString.ToString());
				navigationRequestedEventHandler(this, navigationRequestedEventArgs);
				return navigationRequestedEventArgs.Cancel;
			}
			return true;
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x00005F06 File Offset: 0x00004106
		bool nsIURIContentListener.doContent(string aContentType, bool aIsContentPreferred, nsIRequest aRequest, out nsIStreamListener aContentHandler)
		{
			aContentHandler = null;
			return true;
		}

		// Token: 0x06000902 RID: 2306 RVA: 0x00005F0D File Offset: 0x0000410D
		bool nsIURIContentListener.isPreferred(string aContentType, ref string aDesiredContentType)
		{
			return true;
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x00005F10 File Offset: 0x00004110
		bool nsIURIContentListener.canHandleContent(string aContentType, bool aIsContentPreferred, ref string aDesiredContentType)
		{
			return true;
		}

		// Token: 0x06000904 RID: 2308 RVA: 0x00005F13 File Offset: 0x00004113
		[return: MarshalAs(UnmanagedType.Interface)]
		IntPtr nsIURIContentListener.getLoadCookie()
		{
			return IntPtr.Zero;
		}

		// Token: 0x06000905 RID: 2309 RVA: 0x00005F1A File Offset: 0x0000411A
		void nsIURIContentListener.setLoadCookie([MarshalAs(UnmanagedType.Interface)] IntPtr value)
		{
		}

		// Token: 0x06000906 RID: 2310 RVA: 0x00005F1C File Offset: 0x0000411C
		nsIURIContentListener nsIURIContentListener.getParentContentListener()
		{
			return null;
		}

		// Token: 0x06000907 RID: 2311 RVA: 0x00005F1F File Offset: 0x0000411F
		void nsIURIContentListener.setParentContentListener(nsIURIContentListener value)
		{
		}

		// Token: 0x04000110 RID: 272
		private WebBrowser owner;

		// Token: 0x04000111 RID: 273
		private EventHandlerList events;
	}
}
