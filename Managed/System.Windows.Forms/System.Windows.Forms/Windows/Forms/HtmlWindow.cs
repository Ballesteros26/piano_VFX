using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms.WebBrowserDialogs;
using Mono.WebBrowser;
using Mono.WebBrowser.DOM;

namespace System.Windows.Forms
{
	/// <summary>Represents the logical window that contains one or more instances of <see cref="T:System.Windows.Forms.HtmlDocument" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001BD RID: 445
	public sealed class HtmlWindow
	{
		// Token: 0x06001D29 RID: 7465 RVA: 0x0006EDB0 File Offset: 0x0006CFB0
		internal HtmlWindow(WebBrowser owner, IWebBrowser webHost, IWindow iWindow)
		{
			this.window = iWindow;
			this.webHost = webHost;
			this.owner = owner;
			this.window.Load += new EventHandler(this.OnLoad);
			this.window.Unload += new EventHandler(this.OnUnload);
		}

		// Token: 0x06001D2A RID: 7466 RVA: 0x0006EE08 File Offset: 0x0006D008
		// Note: this type is marked as 'beforefieldinit'.
		static HtmlWindow()
		{
			HtmlWindow.ErrorEvent = new object();
			HtmlWindow.GotFocusEvent = new object();
			HtmlWindow.LostFocusEvent = new object();
			HtmlWindow.LoadEvent = new object();
			HtmlWindow.UnloadEvent = new object();
			HtmlWindow.ScrollEvent = new object();
			HtmlWindow.ResizeEvent = new object();
		}

		/// <summary>Occurs when script running inside of the window encounters a run-time error.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140001E3 RID: 483
		// (add) Token: 0x06001D2B RID: 7467 RVA: 0x0006EE5C File Offset: 0x0006D05C
		// (remove) Token: 0x06001D2C RID: 7468 RVA: 0x0006EE94 File Offset: 0x0006D094
		public event HtmlElementErrorEventHandler Error
		{
			add
			{
				this.Events.AddHandler(HtmlWindow.ErrorEvent, value);
				this.window.Error += new EventHandler(this.OnError);
			}
			remove
			{
				this.Events.RemoveHandler(HtmlWindow.ErrorEvent, value);
				this.window.Error -= new EventHandler(this.OnError);
			}
		}

		/// <summary>Occurs when the current window obtains user input focus.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140001E4 RID: 484
		// (add) Token: 0x06001D2D RID: 7469 RVA: 0x0006EECC File Offset: 0x0006D0CC
		// (remove) Token: 0x06001D2E RID: 7470 RVA: 0x0006EF04 File Offset: 0x0006D104
		public event HtmlElementEventHandler GotFocus
		{
			add
			{
				this.Events.AddHandler(HtmlWindow.GotFocusEvent, value);
				this.window.OnFocus += new EventHandler(this.OnGotFocus);
			}
			remove
			{
				this.Events.RemoveHandler(HtmlWindow.GotFocusEvent, value);
				this.window.OnFocus -= new EventHandler(this.OnGotFocus);
			}
		}

		/// <summary>Occurs when user input focus has left the window.</summary>
		// Token: 0x140001E5 RID: 485
		// (add) Token: 0x06001D2F RID: 7471 RVA: 0x0006EF3C File Offset: 0x0006D13C
		// (remove) Token: 0x06001D30 RID: 7472 RVA: 0x0006EF74 File Offset: 0x0006D174
		public event HtmlElementEventHandler LostFocus
		{
			add
			{
				this.Events.AddHandler(HtmlWindow.LostFocusEvent, value);
				this.window.OnBlur += new EventHandler(this.OnLostFocus);
			}
			remove
			{
				this.Events.RemoveHandler(HtmlWindow.LostFocusEvent, value);
				this.window.OnBlur -= new EventHandler(this.OnLostFocus);
			}
		}

		/// <summary>Occurs when the window's document and all of its elements have finished initializing.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140001E6 RID: 486
		// (add) Token: 0x06001D31 RID: 7473 RVA: 0x0006EFAC File Offset: 0x0006D1AC
		// (remove) Token: 0x06001D32 RID: 7474 RVA: 0x0006EFE4 File Offset: 0x0006D1E4
		public event HtmlElementEventHandler Load
		{
			add
			{
				this.Events.AddHandler(HtmlWindow.LoadEvent, value);
				this.window.Load += new EventHandler(this.OnLoad);
			}
			remove
			{
				this.Events.RemoveHandler(HtmlWindow.LoadEvent, value);
				this.window.Load -= new EventHandler(this.OnLoad);
			}
		}

		/// <summary>Occurs when the current page is unloading, and a new page is about to be displayed. </summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140001E7 RID: 487
		// (add) Token: 0x06001D33 RID: 7475 RVA: 0x0006F01C File Offset: 0x0006D21C
		// (remove) Token: 0x06001D34 RID: 7476 RVA: 0x0006F054 File Offset: 0x0006D254
		public event HtmlElementEventHandler Unload
		{
			add
			{
				this.Events.AddHandler(HtmlWindow.UnloadEvent, value);
				this.window.Unload += new EventHandler(this.OnUnload);
			}
			remove
			{
				this.Events.RemoveHandler(HtmlWindow.UnloadEvent, value);
				this.window.Unload -= new EventHandler(this.OnUnload);
			}
		}

		/// <summary>Occurs when the user scrolls through the window to view off-screen text. </summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140001E8 RID: 488
		// (add) Token: 0x06001D35 RID: 7477 RVA: 0x0006F08C File Offset: 0x0006D28C
		// (remove) Token: 0x06001D36 RID: 7478 RVA: 0x0006F0C4 File Offset: 0x0006D2C4
		public event HtmlElementEventHandler Scroll
		{
			add
			{
				this.Events.AddHandler(HtmlWindow.ScrollEvent, value);
				this.window.Scroll += new EventHandler(this.OnScroll);
			}
			remove
			{
				this.Events.RemoveHandler(HtmlWindow.ScrollEvent, value);
				this.window.Scroll -= new EventHandler(this.OnScroll);
			}
		}

		/// <summary>Occurs when the user uses the mouse to change the dimensions of the window.</summary>
		// Token: 0x140001E9 RID: 489
		// (add) Token: 0x06001D37 RID: 7479 RVA: 0x0006F0FC File Offset: 0x0006D2FC
		// (remove) Token: 0x06001D38 RID: 7480 RVA: 0x0006F110 File Offset: 0x0006D310
		public event HtmlElementEventHandler Resize
		{
			add
			{
				this.Events.AddHandler(HtmlWindow.ResizeEvent, value);
			}
			remove
			{
				this.Events.RemoveHandler(HtmlWindow.ResizeEvent, value);
			}
		}

		// Token: 0x17000713 RID: 1811
		// (get) Token: 0x06001D39 RID: 7481 RVA: 0x0006F124 File Offset: 0x0006D324
		private EventHandlerList Events
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

		/// <summary>Gets the HTML document contained within the window.</summary>
		/// <returns>A valid instance of <see cref="T:System.Windows.Forms.HtmlDocument" />, if a document is loaded. If this window contains a FRAMESET, or no document is currently loaded, it will return null.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000714 RID: 1812
		// (get) Token: 0x06001D3A RID: 7482 RVA: 0x0006F144 File Offset: 0x0006D344
		public HtmlDocument Document
		{
			get
			{
				return new HtmlDocument(this.owner, this.webHost, this.window.Document);
			}
		}

		/// <summary>Gets the unmanaged interface wrapped by this class. </summary>
		/// <returns>An object that can be cast to an IHTMLWindow2, IHTMLWindow3, or IHTMLWindow4 pointer.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000715 RID: 1813
		// (get) Token: 0x06001D3B RID: 7483 RVA: 0x0006F164 File Offset: 0x0006D364
		public object DomWindow
		{
			get
			{
				throw new NotSupportedException("Retrieving a reference to an mshtml interface is not supported. Sorry.");
			}
		}

		/// <summary>Gets a reference to each of the FRAME elements defined within the Web page.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.HtmlWindowCollection" /> of a document's FRAME and IFRAME objects.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000716 RID: 1814
		// (get) Token: 0x06001D3C RID: 7484 RVA: 0x0006F170 File Offset: 0x0006D370
		public HtmlWindowCollection Frames
		{
			get
			{
				return new HtmlWindowCollection(this.owner, this.webHost, this.window.Frames);
			}
		}

		/// <summary>Gets an object containing the user's most recently visited URLs. </summary>
		/// <returns>The <see cref="T:System.Windows.Forms.HtmlHistory" />  for the current window.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000717 RID: 1815
		// (get) Token: 0x06001D3D RID: 7485 RVA: 0x0006F190 File Offset: 0x0006D390
		public HtmlHistory History
		{
			get
			{
				return new HtmlHistory(this.webHost, this.window.History);
			}
		}

		/// <summary>Gets a value indicating whether this window is open or closed.</summary>
		/// <returns>true if the window is still open on the screen; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000718 RID: 1816
		// (get) Token: 0x06001D3E RID: 7486 RVA: 0x0006F1A8 File Offset: 0x0006D3A8
		[MonoTODO("Windows are always open")]
		public bool IsClosed
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets or sets the name of the window. </summary>
		/// <returns>A <see cref="T:System.String" /> representing the name. </returns>
		// Token: 0x17000719 RID: 1817
		// (get) Token: 0x06001D3F RID: 7487 RVA: 0x0006F1AC File Offset: 0x0006D3AC
		// (set) Token: 0x06001D40 RID: 7488 RVA: 0x0006F1BC File Offset: 0x0006D3BC
		public string Name
		{
			get
			{
				return this.window.Name;
			}
			set
			{
				this.window.Name = value;
			}
		}

		/// <summary>Gets a reference to the window that opened the current window. </summary>
		/// <returns>The <see cref="T:System.Windows.Forms.HtmlWindow" /> that was created by a call to the <see cref="M:System.Windows.Forms.HtmlWindow.Open(System.String,System.String,System.String,System.Boolean)" /> or <see cref="M:System.Windows.Forms.HtmlWindow.OpenNew(System.String,System.String)" /> methods. If the window was not created using one of these methods, this property returns null.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700071A RID: 1818
		// (get) Token: 0x06001D41 RID: 7489 RVA: 0x0006F1CC File Offset: 0x0006D3CC
		[MonoTODO("Separate windows are not supported yet")]
		public HtmlWindow Opener
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets the window which resides above the current one in a page containing frames.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.HtmlWindow" /> that owns the current window. If the current window is not a FRAME, or is not embedded inside of a FRAME, it returns null.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700071B RID: 1819
		// (get) Token: 0x06001D42 RID: 7490 RVA: 0x0006F1D0 File Offset: 0x0006D3D0
		public HtmlWindow Parent
		{
			get
			{
				return new HtmlWindow(this.owner, this.webHost, this.window.Parent);
			}
		}

		/// <summary>Gets the position of the window's client area on the screen. </summary>
		/// <returns>A <see cref="T:System.Drawing.Point" /> describing the x -and y-coordinates of the top-left corner of the screen, in pixels. </returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700071C RID: 1820
		// (get) Token: 0x06001D43 RID: 7491 RVA: 0x0006F1F0 File Offset: 0x0006D3F0
		public Point Position
		{
			get
			{
				return this.owner.Location;
			}
		}

		/// <summary>Gets or sets the size of the current window.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> describing the size of the window in pixels. </returns>
		// Token: 0x1700071D RID: 1821
		// (get) Token: 0x06001D44 RID: 7492 RVA: 0x0006F200 File Offset: 0x0006D400
		// (set) Token: 0x06001D45 RID: 7493 RVA: 0x0006F210 File Offset: 0x0006D410
		public Size Size
		{
			get
			{
				return this.owner.Size;
			}
			set
			{
			}
		}

		/// <summary>Gets or sets the text displayed in the status bar of a window.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the current status text.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700071E RID: 1822
		// (get) Token: 0x06001D46 RID: 7494 RVA: 0x0006F214 File Offset: 0x0006D414
		// (set) Token: 0x06001D47 RID: 7495 RVA: 0x0006F224 File Offset: 0x0006D424
		public string StatusBarText
		{
			get
			{
				return this.window.StatusText;
			}
			set
			{
			}
		}

		/// <summary>Gets the frame element corresponding to this window.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.HtmlElement" /> corresponding to this window's FRAME element. If this window is not a frame, it returns null. </returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700071F RID: 1823
		// (get) Token: 0x06001D48 RID: 7496 RVA: 0x0006F228 File Offset: 0x0006D428
		public HtmlElement WindowFrameElement
		{
			get
			{
				return new HtmlElement(this.owner, this.webHost, this.window.Document.DocumentElement);
			}
		}

		/// <summary>Gets the URL corresponding to the current item displayed in the window. </summary>
		/// <returns>A <see cref="T:System.Uri" /> describing the URL.</returns>
		// Token: 0x17000720 RID: 1824
		// (get) Token: 0x06001D49 RID: 7497 RVA: 0x0006F24C File Offset: 0x0006D44C
		public Uri Url
		{
			get
			{
				return this.Document.Url;
			}
		}

		/// <summary>Displays a message box. </summary>
		/// <param name="message">The <see cref="T:System.String" /> to display in the message box.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001D4A RID: 7498 RVA: 0x0006F25C File Offset: 0x0006D45C
		public void Alert(string message)
		{
			MessageBox.Show("Alert", message);
		}

		/// <summary>Displays a dialog box with a message and buttons to solicit a yes/no response.</summary>
		/// <returns>true if the user clicked Yes; false if the user clicked No or closed the dialog box.</returns>
		/// <param name="message">The text to display to the user.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001D4B RID: 7499 RVA: 0x0006F26C File Offset: 0x0006D46C
		public bool Confirm(string message)
		{
			DialogResult dialogResult = MessageBox.Show(message, "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
			return dialogResult == DialogResult.OK;
		}

		/// <summary>Shows a dialog box that displays a message and a text box to the user. </summary>
		/// <returns>A <see cref="T:System.String" /> representing the text entered by the user.</returns>
		/// <param name="message">The message to display to the user.</param>
		/// <param name="defaultInputValue">The default value displayed in the text box.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001D4C RID: 7500 RVA: 0x0006F28C File Offset: 0x0006D48C
		public string Prompt(string message, string defaultInputValue)
		{
			Prompt prompt = new Prompt("Prompt", message, defaultInputValue);
			prompt.Show();
			return prompt.Text;
		}

		/// <summary>Displays or downloads the new content located at the specified URL. </summary>
		/// <param name="urlString">The resource to display, described by a Uniform Resource Locator. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001D4D RID: 7501 RVA: 0x0006F2B4 File Offset: 0x0006D4B4
		public void Navigate(string urlString)
		{
			this.webHost.Navigation.Go(urlString);
		}

		/// <summary>Displays a new document in the current window. </summary>
		/// <param name="url">The location, specified as a <see cref="T:System.Uri" />, of the document or object to display in the current window.</param>
		// Token: 0x06001D4E RID: 7502 RVA: 0x0006F2C8 File Offset: 0x0006D4C8
		public void Navigate(Uri url)
		{
			this.webHost.Navigation.Go(url.ToString());
		}

		/// <summary>Moves the window to the specified coordinates. </summary>
		/// <param name="point">The x- and y-coordinates, relative to the top-left corner of the current window, toward which the page should scroll. </param>
		// Token: 0x06001D4F RID: 7503 RVA: 0x0006F2E0 File Offset: 0x0006D4E0
		public void ScrollTo(Point point)
		{
			this.ScrollTo(point.X, point.Y);
		}

		/// <summary>Scrolls the window to the designated position.</summary>
		/// <param name="x">The x-coordinate, relative to the top-left corner of the current window, toward which the page should scroll.</param>
		/// <param name="y">The y-coordinate, relative to the top-left corner of the current window, toward which the page should scroll.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001D50 RID: 7504 RVA: 0x0006F2F8 File Offset: 0x0006D4F8
		public void ScrollTo(int x, int y)
		{
			this.window.ScrollTo(x, y);
		}

		/// <summary>Displays a file in the named window.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.HtmlWindow" /> representing the new window, or the previously created window named by the <paramref name="target" /> parameter.</returns>
		/// <param name="url">The Uniform Resource Locator that describes the location of the file to load.</param>
		/// <param name="target">The name of the window in which to open the resource. This can be a developer-supplied name, or one of the following special values:_blank: Opens <paramref name="url" /> in a new window. Works the same as a call to <see cref="M:System.Windows.Forms.HtmlWindow.OpenNew(System.String,System.String)" />._media: Opens <paramref name="url" /> in the Media bar. _parent: Opens <paramref name="url" /> in the window that created the current window._search: Opens <paramref name="url" /> in the Search bar._self: Opens <paramref name="url" /> in the current window. _top: If called against a window belonging to a FRAME element, opens <paramref name="url" /> in the window hosting its FRAMESET. Otherwise, acts the same as _self.</param>
		/// <param name="windowOptions">A comma-delimited string consisting of zero or more of the following options in the form <paramref name="name=value" />. Except for the left, top, height, and width options, which take arbitrary integers, each option accepts yes or 1, and no or 0, as valid values.channelmode: Used with the deprecated channels technology of Internet Explorer 4.0. Default is no.directories: Whether the window should display directory navigation buttons. Default is yes. height: The height of the window's client area, in pixels. The minimum is 100; attempts to open a window smaller than this will cause the window to open according to The Internet Explorer defaults. left: The left (x-coordinate) position of the window, relative to the upper-left corner of the user's screen, in pixels. Must be a positive integer.location: Whether to display the Address bar, which enables users to navigate the window to a new URL. Default is yes. menubar: Whether to display menus on the new window. Default is yes.resizable: Whether the window can be resized by the user. Default is yes.scrollbars: Whether the window has horizontal and vertical scroll bars. Default is yes.status: Whether the window has a status bar at the bottom. Default is yes.titlebar: Whether the title of the current page is displayed. Setting this option to no has no effect within a managed application; the title bar will always appear.toolbar: Whether toolbar buttons such as Back, Forward, and Stop are visible. Default is yes.top: The top (y-coordinate) position of the window, relative to the upper-left corner of the user's screen, in pixels. Must be a positive integer.width: The width of the window's client area, in pixels. The minimum is 100; attempts to open a window smaller than this will cause the window to open according to The Internet Explorer defaults.</param>
		/// <param name="replaceEntry">Whether <paramref name="url" /> replaces the current window's URL in the navigation history. This will effect the operation of methods on the <see cref="T:System.Windows.Forms.HtmlHistory" /> class. </param>
		// Token: 0x06001D51 RID: 7505 RVA: 0x0006F308 File Offset: 0x0006D508
		[MonoTODO("Blank opens in current window at the moment. Missing media and search implementations. No options implemented")]
		public HtmlWindow Open(Uri url, string target, string windowOptions, bool replaceEntry)
		{
			return this.Open(url.ToString(), target, windowOptions, replaceEntry);
		}

		/// <summary>Displays a file in the named window.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.HtmlWindow" /> representing the new window, or the previously created window named by the <paramref name="target" /> parameter.</returns>
		/// <param name="urlString">The Uniform Resource Locator that describes the location of the file to load.</param>
		/// <param name="target">The name of the window in which to open the resource. This may be a developer-supplied name, or one of the following special values:_blank: Opens <paramref name="url" /> in a new window. Works the same as a call to <see cref="M:System.Windows.Forms.HtmlWindow.OpenNew(System.String,System.String)" />._media: Opens <paramref name="url" /> in the Media bar. _parent: Opens <paramref name="url" /> in the window that created the current window._search: Opens <paramref name="url" /> in the Search bar._self: Opens <paramref name="url" /> in the current window. _top: If called against a window belonging to a FRAME element, opens <paramref name="url" /> in the window hosting its FRAMESET. Otherwise, acts the same as _self.</param>
		/// <param name="windowOptions">A comma-delimited string consisting of zero or more of the following options in the form <paramref name="name=value" />. Except for the left, top, height, and width options, which take arbitrary integers, each option accepts yes or 1, and no or 0, as valid values.channelmode: Used with the deprecated channels technology of Internet Explorer 4.0. Default is no.directories: Whether the window should display directory navigation buttons. Default is yes. height: The height of the window's client area, in pixels. The minimum is 100; attempts to open a window smaller than this will cause the window to open according to the Internet Explorer defaults. left: The left (x-coordinate) position of the window, relative to the upper-left corner of the user's screen, in pixels. Must be a positive integer.location: Whether to display the Address bar, which enables users to navigate the window to a new URL. Default is yes. menubar: Whether to display menus on the new window. Default is yes.resizable: Whether the window can be resized by the user. Default is yes.scrollbars: Whether the window has horizontal and vertical scroll bars. Default is yes.status: Whether the window has a status bar at the bottom. Default is yes.titlebar: Whether the title of the current page is displayed. Setting this option to no has no effect within a managed application; the title bar will always appear.toolbar: Whether toolbar buttons such as Back, Forward, and Stop are visible. Default is yes.top: The top (y-coordinate) position of the window, relative to the upper-left corner of the user's screen, in pixels. Must be a positive integer.width: The width of the window's client area, in pixels. The minimum is 100; attempts to open a window smaller than this will cause the window to open according to the Internet Explorer defaults.</param>
		/// <param name="replaceEntry">Whether <paramref name="url" /> replaces the current window's URL in the navigation history. This will effect the operation of methods on the <see cref="T:System.Windows.Forms.HtmlHistory" /> class.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001D52 RID: 7506 RVA: 0x0006F31C File Offset: 0x0006D51C
		[MonoTODO("Blank opens in current window at the moment. Missing media and search implementations. No options implemented")]
		public HtmlWindow Open(string urlString, string target, string windowOptions, bool replaceEntry)
		{
			if (target != null)
			{
				if (HtmlWindow.<>f__switch$map8 == null)
				{
					Dictionary<string, int> dictionary = new Dictionary<string, int>(6);
					dictionary.Add("_blank", 0);
					dictionary.Add("_media", 1);
					dictionary.Add("_parent", 2);
					dictionary.Add("_search", 3);
					dictionary.Add("_self", 4);
					dictionary.Add("_top", 5);
					HtmlWindow.<>f__switch$map8 = dictionary;
				}
				int num;
				if (HtmlWindow.<>f__switch$map8.TryGetValue(target, ref num))
				{
					switch (num)
					{
					case 0:
						this.window.Open(urlString);
						break;
					case 2:
						this.window.Parent.Open(urlString);
						break;
					case 4:
						this.window.Open(urlString);
						break;
					case 5:
						this.window.Top.Open(urlString);
						break;
					}
				}
			}
			return this;
		}

		/// <summary>Displays a file in a new window.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.HtmlWindow" /> representing the new window. </returns>
		/// <param name="urlString">The Uniform Resource Locator that describes the location of the file to load.</param>
		/// <param name="windowOptions">A comma-delimited string consisting of zero or more of the following options in the form <paramref name="name=value" />. See <see cref="M:System.Windows.Forms.HtmlWindow.Open(System.String,System.String,System.String,System.Boolean)" /> for a full description of the valid options. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001D53 RID: 7507 RVA: 0x0006F420 File Offset: 0x0006D620
		[MonoTODO("Opens in current window at the moment.")]
		public HtmlWindow OpenNew(string urlString, string windowOptions)
		{
			return this.Open(urlString, "_blank", windowOptions, false);
		}

		/// <summary>Displays a file in a new window.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.HtmlWindow" /> representing the new window. </returns>
		/// <param name="url">The Uniform Resource Locator that describes the location of the file to load.</param>
		/// <param name="windowOptions">A comma-delimited string consisting of zero or more of the following options in the form <paramref name="name=value" />. See <see cref="M:System.Windows.Forms.HtmlWindow.Open(System.String,System.String,System.String,System.Boolean)" /> for a full description of the valid options. </param>
		// Token: 0x06001D54 RID: 7508 RVA: 0x0006F430 File Offset: 0x0006D630
		[MonoTODO("Opens in current window at the moment.")]
		public HtmlWindow OpenNew(Uri url, string windowOptions)
		{
			return this.OpenNew(url.ToString(), windowOptions);
		}

		/// <summary>Adds an event handler for the named HTML DOM event.</summary>
		/// <param name="eventName">The name of the event you want to handle.</param>
		/// <param name="eventHandler">A reference to the managed code that handles the event.</param>
		// Token: 0x06001D55 RID: 7509 RVA: 0x0006F440 File Offset: 0x0006D640
		public void AttachEventHandler(string eventName, EventHandler eventHandler)
		{
			this.window.AttachEventHandler(eventName, eventHandler);
		}

		/// <summary>Closes the window.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001D56 RID: 7510 RVA: 0x0006F450 File Offset: 0x0006D650
		public void Close()
		{
			throw new NotImplementedException();
		}

		/// <summary>Removes the named event handler.</summary>
		/// <param name="eventName">The name of the event you want to handle.</param>
		/// <param name="eventHandler">A reference to the managed code that handles the event.</param>
		// Token: 0x06001D57 RID: 7511 RVA: 0x0006F458 File Offset: 0x0006D658
		public void DetachEventHandler(string eventName, EventHandler eventHandler)
		{
			this.window.DetachEventHandler(eventName, eventHandler);
		}

		/// <summary>Puts the focus on the current window.</summary>
		// Token: 0x06001D58 RID: 7512 RVA: 0x0006F468 File Offset: 0x0006D668
		public void Focus()
		{
			this.window.Focus();
		}

		/// <summary>Moves the window to the specified coordinates on the screen. </summary>
		/// <param name="point">The x- and y-coordinates of the window's upper-left corner. </param>
		/// <exception cref="T:System.UnauthorizedAccessException">The code trying to execute this operation does not have permission to manipulate this window. See the Remarks section for details.</exception>
		// Token: 0x06001D59 RID: 7513 RVA: 0x0006F478 File Offset: 0x0006D678
		public void MoveTo(Point point)
		{
			throw new NotImplementedException();
		}

		/// <summary>Moves the window to the specified coordinates on the screen. </summary>
		/// <param name="x">The x-coordinate of the window's upper-left corner.</param>
		/// <param name="y">The y-coordinate of the window's upper-left corner.</param>
		/// <exception cref="T:System.UnauthorizedAccessException">The code trying to execute this operation does not have permission to manipulate this window. See the Remarks section for details.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001D5A RID: 7514 RVA: 0x0006F480 File Offset: 0x0006D680
		public void MoveTo(int x, int y)
		{
			throw new NotImplementedException();
		}

		/// <summary>Takes focus off of the current window. </summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001D5B RID: 7515 RVA: 0x0006F488 File Offset: 0x0006D688
		public void RemoveFocus()
		{
			this.webHost.FocusOut();
		}

		/// <summary>Changes the size of the window to the specified dimensions. </summary>
		/// <param name="size">A <see cref="T:System.Drawing.Size" /> describing the desired width and height of the window, in pixels. Must be 100 pixels or more in both dimensions. </param>
		/// <exception cref="T:System.UnauthorizedAccessException">The window you are trying to resize is in a different domain than its parent window. This restriction is part of cross-frame scripting security; for more information, see About Cross-Frame Scripting and Security.</exception>
		// Token: 0x06001D5C RID: 7516 RVA: 0x0006F498 File Offset: 0x0006D698
		public void ResizeTo(Size size)
		{
			throw new NotImplementedException();
		}

		/// <summary>Changes the size of the window to the specified dimensions. </summary>
		/// <param name="width">Describes the desired width of the window, in pixels. Must be 100 pixels or more.</param>
		/// <param name="height">Describes the desired height of the window, in pixels. Must be 100 pixels or more.</param>
		/// <exception cref="T:System.UnauthorizedAccessException">The window you are trying to resize is in a different domain than its parent window. This restriction is part of cross-frame scripting security; for more information, see About Cross-Frame Scripting and Security.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001D5D RID: 7517 RVA: 0x0006F4A0 File Offset: 0x0006D6A0
		public void ResizeTo(int width, int height)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001D5E RID: 7518 RVA: 0x0006F4A8 File Offset: 0x0006D6A8
		internal void OnError(object sender, EventArgs ev)
		{
			HtmlElementErrorEventHandler htmlElementErrorEventHandler = (HtmlElementErrorEventHandler)this.Events[HtmlWindow.ErrorEvent];
			if (htmlElementErrorEventHandler != null)
			{
				HtmlElementErrorEventArgs htmlElementErrorEventArgs = new HtmlElementErrorEventArgs(string.Empty, 0, null);
				htmlElementErrorEventHandler(this, htmlElementErrorEventArgs);
			}
		}

		// Token: 0x06001D5F RID: 7519 RVA: 0x0006F4E8 File Offset: 0x0006D6E8
		internal void OnGotFocus(object sender, EventArgs ev)
		{
			HtmlElementEventHandler htmlElementEventHandler = (HtmlElementEventHandler)this.Events[HtmlWindow.GotFocusEvent];
			if (htmlElementEventHandler != null)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs();
				htmlElementEventHandler(this, htmlElementEventArgs);
			}
		}

		// Token: 0x06001D60 RID: 7520 RVA: 0x0006F520 File Offset: 0x0006D720
		internal void OnLostFocus(object sender, EventArgs ev)
		{
			HtmlElementEventHandler htmlElementEventHandler = (HtmlElementEventHandler)this.Events[HtmlWindow.LostFocusEvent];
			if (htmlElementEventHandler != null)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs();
				htmlElementEventHandler(this, htmlElementEventArgs);
			}
		}

		// Token: 0x06001D61 RID: 7521 RVA: 0x0006F558 File Offset: 0x0006D758
		internal void OnLoad(object sender, EventArgs ev)
		{
			HtmlElementEventHandler htmlElementEventHandler = (HtmlElementEventHandler)this.Events[HtmlWindow.LoadEvent];
			if (htmlElementEventHandler != null)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs();
				htmlElementEventHandler(this, htmlElementEventArgs);
			}
		}

		// Token: 0x06001D62 RID: 7522 RVA: 0x0006F590 File Offset: 0x0006D790
		internal void OnUnload(object sender, EventArgs ev)
		{
			HtmlElementEventHandler htmlElementEventHandler = (HtmlElementEventHandler)this.Events[HtmlWindow.UnloadEvent];
			if (htmlElementEventHandler != null)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs();
				htmlElementEventHandler(this, htmlElementEventArgs);
			}
		}

		// Token: 0x06001D63 RID: 7523 RVA: 0x0006F5C8 File Offset: 0x0006D7C8
		internal void OnScroll(object sender, EventArgs ev)
		{
			HtmlElementEventHandler htmlElementEventHandler = (HtmlElementEventHandler)this.Events[HtmlWindow.ScrollEvent];
			if (htmlElementEventHandler != null)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs();
				htmlElementEventHandler(this, htmlElementEventArgs);
			}
		}

		// Token: 0x06001D64 RID: 7524 RVA: 0x0006F600 File Offset: 0x0006D800
		internal void OnResize(object sender, EventArgs ev)
		{
			HtmlElementEventHandler htmlElementEventHandler = (HtmlElementEventHandler)this.Events[HtmlWindow.ResizeEvent];
			if (htmlElementEventHandler != null)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs();
				htmlElementEventHandler(this, htmlElementEventArgs);
			}
		}

		/// <returns>System.Int32</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001D65 RID: 7525 RVA: 0x0006F638 File Offset: 0x0006D838
		public override int GetHashCode()
		{
			if (this.window == null)
			{
				return 0;
			}
			return this.window.GetHashCode();
		}

		/// <summary>Tests the object for equality against the current object.</summary>
		/// <returns>true if the objects are equal; otherwise, false.</returns>
		/// <param name="obj">The object to test.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001D66 RID: 7526 RVA: 0x0006F654 File Offset: 0x0006D854
		public override bool Equals(object obj)
		{
			return this == (HtmlWindow)obj;
		}

		/// <summary>Tests the two <see cref="T:System.Windows.Forms.HtmlWindow" /> objects for equality.</summary>
		/// <returns>true if both parameters are null, or if both elements have the same underlying COM interface; otherwise, false.</returns>
		/// <param name="left">The first <see cref="T:System.Windows.Forms.HtmlWindow" /> object.</param>
		/// <param name="right">The second <see cref="T:System.Windows.Forms.HtmlWindow" /> object.</param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06001D67 RID: 7527 RVA: 0x0006F664 File Offset: 0x0006D864
		public static bool operator ==(HtmlWindow left, HtmlWindow right)
		{
			return left == right || (left != null && right != null && left.window.Equals(right.window));
		}

		/// <summary>Tests two HtmlWindow objects for inequality.</summary>
		/// <returns>true if one but not both of the objects is null, or the underlying COM pointers do not match; otherwise, false.</returns>
		/// <param name="left">The first <see cref="T:System.Windows.Forms.HtmlWindow" /> object.</param>
		/// <param name="right">The second <see cref="T:System.Windows.Forms.HtmlWindow" /> object.</param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06001D68 RID: 7528 RVA: 0x0006F69C File Offset: 0x0006D89C
		public static bool operator !=(HtmlWindow left, HtmlWindow right)
		{
			return !(left == right);
		}

		// Token: 0x04000F87 RID: 3975
		private EventHandlerList event_handlers;

		// Token: 0x04000F88 RID: 3976
		private IWindow window;

		// Token: 0x04000F89 RID: 3977
		private IWebBrowser webHost;

		// Token: 0x04000F8A RID: 3978
		private WebBrowser owner;
	}
}
