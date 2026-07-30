using System;
using System.ComponentModel;
using System.Drawing;
using Mono.WebBrowser;
using Mono.WebBrowser.DOM;

namespace System.Windows.Forms
{
	/// <summary>Provides top-level programmatic access to an HTML document hosted by the <see cref="T:System.Windows.Forms.WebBrowser" /> control.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020001B6 RID: 438
	public sealed class HtmlDocument
	{
		// Token: 0x06001C3B RID: 7227 RVA: 0x0006C834 File Offset: 0x0006AA34
		internal HtmlDocument(WebBrowser owner, IWebBrowser webHost)
			: this(owner, webHost, webHost.Document)
		{
		}

		// Token: 0x06001C3C RID: 7228 RVA: 0x0006C844 File Offset: 0x0006AA44
		internal HtmlDocument(WebBrowser owner, IWebBrowser webHost, IDocument doc)
		{
			this.webHost = webHost;
			this.document = doc;
			this.owner = owner;
		}

		// Token: 0x06001C3D RID: 7229 RVA: 0x0006C864 File Offset: 0x0006AA64
		// Note: this type is marked as 'beforefieldinit'.
		static HtmlDocument()
		{
			HtmlDocument.ClickEvent = new object();
			HtmlDocument.ContextMenuShowingEvent = new object();
			HtmlDocument.FocusingEvent = new object();
			HtmlDocument.LosingFocusEvent = new object();
			HtmlDocument.MouseDownEvent = new object();
			HtmlDocument.MouseLeaveEvent = new object();
			HtmlDocument.MouseMoveEvent = new object();
			HtmlDocument.MouseOverEvent = new object();
			HtmlDocument.MouseUpEvent = new object();
			HtmlDocument.StopEvent = new object();
		}

		/// <summary>Occurs when the user clicks anywhere on the document.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140001C6 RID: 454
		// (add) Token: 0x06001C3E RID: 7230 RVA: 0x0006C8D8 File Offset: 0x0006AAD8
		// (remove) Token: 0x06001C3F RID: 7231 RVA: 0x0006C910 File Offset: 0x0006AB10
		public event HtmlElementEventHandler Click
		{
			add
			{
				this.Events.AddHandler(HtmlDocument.ClickEvent, value);
				this.document.Click += new NodeEventHandler(this.OnClick);
			}
			remove
			{
				this.Events.RemoveHandler(HtmlDocument.ClickEvent, value);
				this.document.Click -= new NodeEventHandler(this.OnClick);
			}
		}

		/// <summary>Occurs when the user requests to display the document's context menu. </summary>
		// Token: 0x140001C7 RID: 455
		// (add) Token: 0x06001C40 RID: 7232 RVA: 0x0006C948 File Offset: 0x0006AB48
		// (remove) Token: 0x06001C41 RID: 7233 RVA: 0x0006C984 File Offset: 0x0006AB84
		public event HtmlElementEventHandler ContextMenuShowing
		{
			add
			{
				this.Events.AddHandler(HtmlDocument.ContextMenuShowingEvent, value);
				this.owner.WebHost.ContextMenuShown += new ContextMenuEventHandler(this.OnContextMenuShowing);
			}
			remove
			{
				this.Events.RemoveHandler(HtmlDocument.ContextMenuShowingEvent, value);
				this.owner.WebHost.ContextMenuShown -= new ContextMenuEventHandler(this.OnContextMenuShowing);
			}
		}

		/// <summary>Occurs before focus is given to the document.</summary>
		// Token: 0x140001C8 RID: 456
		// (add) Token: 0x06001C42 RID: 7234 RVA: 0x0006C9C0 File Offset: 0x0006ABC0
		// (remove) Token: 0x06001C43 RID: 7235 RVA: 0x0006C9F8 File Offset: 0x0006ABF8
		public event HtmlElementEventHandler Focusing
		{
			add
			{
				this.Events.AddHandler(HtmlDocument.FocusingEvent, value);
				this.document.OnFocus += new NodeEventHandler(this.OnFocusing);
			}
			remove
			{
				this.Events.RemoveHandler(HtmlDocument.FocusingEvent, value);
				this.document.OnFocus -= new NodeEventHandler(this.OnFocusing);
			}
		}

		/// <summary>Occurs while focus is leaving a control.</summary>
		// Token: 0x140001C9 RID: 457
		// (add) Token: 0x06001C44 RID: 7236 RVA: 0x0006CA30 File Offset: 0x0006AC30
		// (remove) Token: 0x06001C45 RID: 7237 RVA: 0x0006CA68 File Offset: 0x0006AC68
		public event HtmlElementEventHandler LosingFocus
		{
			add
			{
				this.Events.AddHandler(HtmlDocument.LosingFocusEvent, value);
				this.document.OnBlur += new NodeEventHandler(this.OnLosingFocus);
			}
			remove
			{
				this.Events.RemoveHandler(HtmlDocument.LosingFocusEvent, value);
				this.document.OnBlur -= new NodeEventHandler(this.OnLosingFocus);
			}
		}

		/// <summary>Occurs when the user clicks the left mouse button.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140001CA RID: 458
		// (add) Token: 0x06001C46 RID: 7238 RVA: 0x0006CAA0 File Offset: 0x0006ACA0
		// (remove) Token: 0x06001C47 RID: 7239 RVA: 0x0006CAD8 File Offset: 0x0006ACD8
		public event HtmlElementEventHandler MouseDown
		{
			add
			{
				this.Events.AddHandler(HtmlDocument.MouseDownEvent, value);
				this.document.MouseDown += new NodeEventHandler(this.OnMouseDown);
			}
			remove
			{
				this.Events.RemoveHandler(HtmlDocument.MouseDownEvent, value);
				this.document.MouseDown -= new NodeEventHandler(this.OnMouseDown);
			}
		}

		/// <summary>Occurs when the mouse is no longer hovering over the document. </summary>
		// Token: 0x140001CB RID: 459
		// (add) Token: 0x06001C48 RID: 7240 RVA: 0x0006CB10 File Offset: 0x0006AD10
		// (remove) Token: 0x06001C49 RID: 7241 RVA: 0x0006CB48 File Offset: 0x0006AD48
		public event HtmlElementEventHandler MouseLeave
		{
			add
			{
				this.Events.AddHandler(HtmlDocument.MouseLeaveEvent, value);
				this.document.MouseLeave += new NodeEventHandler(this.OnMouseLeave);
			}
			remove
			{
				this.Events.RemoveHandler(HtmlDocument.MouseLeaveEvent, value);
				this.document.MouseLeave -= new NodeEventHandler(this.OnMouseLeave);
			}
		}

		/// <summary>Occurs when the mouse is moved over the document.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140001CC RID: 460
		// (add) Token: 0x06001C4A RID: 7242 RVA: 0x0006CB80 File Offset: 0x0006AD80
		// (remove) Token: 0x06001C4B RID: 7243 RVA: 0x0006CBB8 File Offset: 0x0006ADB8
		public event HtmlElementEventHandler MouseMove
		{
			add
			{
				this.Events.AddHandler(HtmlDocument.MouseMoveEvent, value);
				this.document.MouseMove += new NodeEventHandler(this.OnMouseMove);
			}
			remove
			{
				this.Events.RemoveHandler(HtmlDocument.MouseMoveEvent, value);
				this.document.MouseMove -= new NodeEventHandler(this.OnMouseMove);
			}
		}

		/// <summary>Occurs when the mouse is moved over the document. </summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140001CD RID: 461
		// (add) Token: 0x06001C4C RID: 7244 RVA: 0x0006CBF0 File Offset: 0x0006ADF0
		// (remove) Token: 0x06001C4D RID: 7245 RVA: 0x0006CC28 File Offset: 0x0006AE28
		public event HtmlElementEventHandler MouseOver
		{
			add
			{
				this.Events.AddHandler(HtmlDocument.MouseOverEvent, value);
				this.document.MouseOver += new NodeEventHandler(this.OnMouseOver);
			}
			remove
			{
				this.Events.RemoveHandler(HtmlDocument.MouseOverEvent, value);
				this.document.MouseOver -= new NodeEventHandler(this.OnMouseOver);
			}
		}

		/// <summary>Occurs when the user releases the left mouse button.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140001CE RID: 462
		// (add) Token: 0x06001C4E RID: 7246 RVA: 0x0006CC60 File Offset: 0x0006AE60
		// (remove) Token: 0x06001C4F RID: 7247 RVA: 0x0006CC98 File Offset: 0x0006AE98
		public event HtmlElementEventHandler MouseUp
		{
			add
			{
				this.Events.AddHandler(HtmlDocument.MouseUpEvent, value);
				this.document.MouseUp += new NodeEventHandler(this.OnMouseUp);
			}
			remove
			{
				this.Events.RemoveHandler(HtmlDocument.MouseUpEvent, value);
				this.document.MouseUp -= new NodeEventHandler(this.OnMouseUp);
			}
		}

		/// <summary>Occurs when navigation to another Web page is halted.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140001CF RID: 463
		// (add) Token: 0x06001C50 RID: 7248 RVA: 0x0006CCD0 File Offset: 0x0006AED0
		// (remove) Token: 0x06001C51 RID: 7249 RVA: 0x0006CD08 File Offset: 0x0006AF08
		public event HtmlElementEventHandler Stop
		{
			add
			{
				this.Events.AddHandler(HtmlDocument.StopEvent, value);
				this.document.LoadStopped += new EventHandler(this.OnStop);
			}
			remove
			{
				this.Events.RemoveHandler(HtmlDocument.StopEvent, value);
				this.document.LoadStopped -= new EventHandler(this.OnStop);
			}
		}

		// Token: 0x170006CB RID: 1739
		// (get) Token: 0x06001C52 RID: 7250 RVA: 0x0006CD40 File Offset: 0x0006AF40
		internal EventHandlerList Events
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

		/// <summary>Adds an event handler for the named HTML DOM event.</summary>
		/// <param name="eventName">The name of the event you want to handle.</param>
		/// <param name="eventHandler">The managed code that handles the event. </param>
		// Token: 0x06001C53 RID: 7251 RVA: 0x0006CD60 File Offset: 0x0006AF60
		public void AttachEventHandler(string eventName, EventHandler eventHandler)
		{
			this.document.AttachEventHandler(eventName, eventHandler);
		}

		/// <summary>Creates a new HtmlElement of the specified HTML tag type. </summary>
		/// <returns>A new element of the specified tag type. </returns>
		/// <param name="elementTag">The name of the HTML element to create. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001C54 RID: 7252 RVA: 0x0006CD70 File Offset: 0x0006AF70
		public HtmlElement CreateElement(string elementTag)
		{
			IElement element = this.document.CreateElement(elementTag);
			return new HtmlElement(this.owner, this.webHost, element);
		}

		/// <summary>Removes an event handler from a named event on the HTML DOM. </summary>
		/// <param name="eventName">The name of the event you want to cease handling.</param>
		/// <param name="eventHandler">The managed code that handles the event.</param>
		// Token: 0x06001C55 RID: 7253 RVA: 0x0006CD9C File Offset: 0x0006AF9C
		public void DetachEventHandler(string eventName, EventHandler eventHandler)
		{
			this.document.DetachEventHandler(eventName, eventHandler);
		}

		/// <returns>true if the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.Object" /> to compare with the current <see cref="T:System.Object" />. </param>
		// Token: 0x06001C56 RID: 7254 RVA: 0x0006CDAC File Offset: 0x0006AFAC
		public override bool Equals(object obj)
		{
			return this == (HtmlDocument)obj;
		}

		/// <summary>Executes the specified command against the document. </summary>
		/// <param name="command">The name of the command to execute.</param>
		/// <param name="showUI">Whether or not to show command-specific dialog boxes or message boxes to the user. </param>
		/// <param name="value">The value to assign using the command. Not applicable for all commands.</param>
		// Token: 0x06001C57 RID: 7255 RVA: 0x0006CDBC File Offset: 0x0006AFBC
		public void ExecCommand(string command, bool showUI, object value)
		{
			throw new NotImplementedException("Not Supported");
		}

		/// <summary>Sets user input focus on the current document.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001C58 RID: 7256 RVA: 0x0006CDC8 File Offset: 0x0006AFC8
		[EditorBrowsable(2)]
		public void Focus()
		{
			this.webHost.FocusIn(0);
		}

		/// <summary>Retrieves a single <see cref="T:System.Windows.Forms.HtmlElement" /> using the element's ID attribute as a search key.</summary>
		/// <returns>Returns the first object with the same ID attribute as the specified value, or null if the <paramref name="id" /> cannot be found. </returns>
		/// <param name="id">The ID attribute of the element to retrieve.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001C59 RID: 7257 RVA: 0x0006CDD8 File Offset: 0x0006AFD8
		public HtmlElement GetElementById(string id)
		{
			IElement elementById = this.document.GetElementById(id);
			if (elementById != null)
			{
				return new HtmlElement(this.owner, this.webHost, elementById);
			}
			return null;
		}

		/// <summary>Retrieves the HTML element located at the specified client coordinates.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.HtmlElement" /> at the specified screen location in the document.</returns>
		/// <param name="point">The x,y position of the element on the screen, relative to the top-left corner of the document. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001C5A RID: 7258 RVA: 0x0006CE0C File Offset: 0x0006B00C
		public HtmlElement GetElementFromPoint(Point point)
		{
			IElement element = this.document.GetElement(point.X, point.Y);
			if (element != null)
			{
				return new HtmlElement(this.owner, this.webHost, element);
			}
			return null;
		}

		/// <summary>Retrieve a collection of elements with the specified HTML tag.</summary>
		/// <returns>The collection of elements who tag name is equal to the <paramref name="tagName" /> argument.</returns>
		/// <param name="tagName">The name of the HTML tag for the <see cref="T:System.Windows.Forms.HtmlElement" /> objects you want to retrieve.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001C5B RID: 7259 RVA: 0x0006CE50 File Offset: 0x0006B050
		public HtmlElementCollection GetElementsByTagName(string tagName)
		{
			IElementCollection elementsByTagName = this.document.GetElementsByTagName(tagName);
			if (elementsByTagName != null)
			{
				return new HtmlElementCollection(this.owner, this.webHost, elementsByTagName);
			}
			return null;
		}

		/// <returns>A hash code for the current <see cref="T:System.Object" />.</returns>
		// Token: 0x06001C5C RID: 7260 RVA: 0x0006CE84 File Offset: 0x0006B084
		public override int GetHashCode()
		{
			if (this.document == null)
			{
				return 0;
			}
			return this.document.GetHashCode();
		}

		/// <summary>Executes an Active Scripting function defined in an HTML page.</summary>
		/// <returns>The object returned by the Active Scripting call. </returns>
		/// <param name="scriptName">The name of the script method to invoke.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001C5D RID: 7261 RVA: 0x0006CEA0 File Offset: 0x0006B0A0
		public object InvokeScript(string scriptName)
		{
			return this.document.InvokeScript("eval ('" + scriptName + "()');");
		}

		/// <summary>Executes an Active Scripting function defined in an HTML page.</summary>
		/// <returns>The object returned by the Active Scripting call. </returns>
		/// <param name="scriptName">The name of the script method to invoke.</param>
		/// <param name="args">The arguments to pass to the script method. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001C5E RID: 7262 RVA: 0x0006CEC0 File Offset: 0x0006B0C0
		public object InvokeScript(string scriptName, object[] args)
		{
			string[] array = new string[args.Length];
			for (int i = 0; i < args.Length; i++)
			{
				if (args[i] is string)
				{
					array[i] = "\"" + args[i].ToString() + "\"";
				}
				else
				{
					array[i] = args[i].ToString();
				}
			}
			return this.document.InvokeScript(string.Concat(new string[]
			{
				"eval ('",
				scriptName,
				"(",
				string.Join(",", array),
				")');"
			}));
		}

		/// <summary>Gets a new <see cref="T:System.Windows.Forms.HtmlDocument" /> to use with the <see cref="M:System.Windows.Forms.HtmlDocument.Write(System.String)" /> method.</summary>
		/// <returns>A new document for writing.</returns>
		/// <param name="replaceInHistory">Whether the new window's navigation should replace the previous element in the navigation history of the DOM. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001C5F RID: 7263 RVA: 0x0006CF64 File Offset: 0x0006B164
		public HtmlDocument OpenNew(bool replaceInHistory)
		{
			LoadFlags loadFlags = 0;
			if (replaceInHistory)
			{
				loadFlags |= 128;
			}
			this.webHost.Navigation.Go("about:blank", loadFlags);
			return this;
		}

		/// <summary>Writes a new HTML page.</summary>
		/// <param name="text">The HTML text to write into the document.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001C60 RID: 7264 RVA: 0x0006CF98 File Offset: 0x0006B198
		public void Write(string text)
		{
			this.document.Write(text);
		}

		/// <summary>Provides the <see cref="T:System.Windows.Forms.HtmlElement" /> which currently has user input focus. </summary>
		/// <returns>An <see cref="T:System.Windows.Forms.HtmlElement" />. </returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x06001C61 RID: 7265 RVA: 0x0006CFA8 File Offset: 0x0006B1A8
		public HtmlElement ActiveElement
		{
			get
			{
				IElement active = this.document.Active;
				if (active == null)
				{
					return null;
				}
				return new HtmlElement(this.owner, this.webHost, active);
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Drawing.Color" /> of a hyperlink when clicked by a user. </summary>
		/// <returns>The <see cref="T:System.Drawing.Color" /> for active links. </returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x06001C62 RID: 7266 RVA: 0x0006CFDC File Offset: 0x0006B1DC
		// (set) Token: 0x06001C63 RID: 7267 RVA: 0x0006CFF0 File Offset: 0x0006B1F0
		public Color ActiveLinkColor
		{
			get
			{
				return this.ParseColor(this.document.ActiveLinkColor);
			}
			set
			{
				this.document.ActiveLinkColor = value.ToArgb().ToString();
			}
		}

		/// <summary>Gets an instance of <see cref="T:System.Windows.Forms.HtmlElementCollection" />, which stores all <see cref="T:System.Windows.Forms.HtmlElement" /> objects for the document. </summary>
		/// <returns>The <see cref="T:System.Windows.Forms.HtmlElementCollection" /> of all elements in the document.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006CE RID: 1742
		// (get) Token: 0x06001C64 RID: 7268 RVA: 0x0006D018 File Offset: 0x0006B218
		public HtmlElementCollection All
		{
			get
			{
				return new HtmlElementCollection(this.owner, this.webHost, this.document.DocumentElement.All);
			}
		}

		/// <summary>Gets or sets the background color of the HTML document.</summary>
		/// <returns>The <see cref="T:System.Drawing.Color" /> of the document's background.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170006CF RID: 1743
		// (get) Token: 0x06001C65 RID: 7269 RVA: 0x0006D03C File Offset: 0x0006B23C
		// (set) Token: 0x06001C66 RID: 7270 RVA: 0x0006D050 File Offset: 0x0006B250
		public Color BackColor
		{
			get
			{
				return this.ParseColor(this.document.BackColor);
			}
			set
			{
				this.document.BackColor = value.ToArgb().ToString();
			}
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.HtmlElement" /> for the BODY tag. </summary>
		/// <returns>The <see cref="T:System.Windows.Forms.HtmlElement" /> object for the BODY tag.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006D0 RID: 1744
		// (get) Token: 0x06001C67 RID: 7271 RVA: 0x0006D078 File Offset: 0x0006B278
		public HtmlElement Body
		{
			get
			{
				return new HtmlElement(this.owner, this.webHost, this.document.Body);
			}
		}

		/// <summary>Gets or sets the HTTP cookies associated with this document.</summary>
		/// <returns>A <see cref="T:System.String" /> containing a list of cookies, with each cookie separated by a semicolon.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170006D1 RID: 1745
		// (get) Token: 0x06001C68 RID: 7272 RVA: 0x0006D098 File Offset: 0x0006B298
		// (set) Token: 0x06001C69 RID: 7273 RVA: 0x0006D0A8 File Offset: 0x0006B2A8
		public string Cookie
		{
			get
			{
				return this.document.Cookie;
			}
			set
			{
				this.document.Cookie = value;
			}
		}

		/// <summary>Gets the encoding used by default for the current document. </summary>
		/// <returns>The <see cref="T:System.String" /> representing the encoding that the browser uses when the page is first displayed.</returns>
		// Token: 0x170006D2 RID: 1746
		// (get) Token: 0x06001C6A RID: 7274 RVA: 0x0006D0B8 File Offset: 0x0006B2B8
		public string DefaultEncoding
		{
			get
			{
				return this.document.Charset;
			}
		}

		/// <summary>Gets or sets the string describing the domain of this document for security purposes.</summary>
		/// <returns>A valid domain. </returns>
		/// <exception cref="T:System.ArgumentException">The argument for the Domain property must be a valid domain name using Domain Name System (DNS) conventions.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006D3 RID: 1747
		// (get) Token: 0x06001C6B RID: 7275 RVA: 0x0006D0C8 File Offset: 0x0006B2C8
		// (set) Token: 0x06001C6C RID: 7276 RVA: 0x0006D0D8 File Offset: 0x0006B2D8
		public string Domain
		{
			get
			{
				return this.document.Domain;
			}
			set
			{
				throw new NotSupportedException("Setting the domain is not supported per the DOM Level 2 HTML specification. Sorry.");
			}
		}

		/// <summary>Gets the unmanaged interface pointer for this <see cref="T:System.Windows.Forms.HtmlDocument" />.</summary>
		/// <returns>An <see cref="T:System.Object" /> representing an IDispatch pointer to the unmanaged document. </returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006D4 RID: 1748
		// (get) Token: 0x06001C6D RID: 7277 RVA: 0x0006D0E4 File Offset: 0x0006B2E4
		public object DomDocument
		{
			get
			{
				throw new NotSupportedException("Retrieving a reference to an mshtml interface is not supported. Sorry.");
			}
		}

		/// <summary>Gets or sets the character encoding for this document.</summary>
		/// <returns>The <see cref="T:System.String" /> representing the current character encoding.</returns>
		// Token: 0x170006D5 RID: 1749
		// (get) Token: 0x06001C6E RID: 7278 RVA: 0x0006D0F0 File Offset: 0x0006B2F0
		// (set) Token: 0x06001C6F RID: 7279 RVA: 0x0006D100 File Offset: 0x0006B300
		public string Encoding
		{
			get
			{
				return this.document.Charset;
			}
			set
			{
				this.document.Charset = value;
			}
		}

		/// <summary>Gets a value indicating whether the document has user input focus. </summary>
		/// <returns>true if the document has focus; otherwise, false.</returns>
		// Token: 0x170006D6 RID: 1750
		// (get) Token: 0x06001C70 RID: 7280 RVA: 0x0006D110 File Offset: 0x0006B310
		public bool Focused
		{
			get
			{
				return this.webHost.Window.Document == this.document;
			}
		}

		/// <summary>Gets or sets the text color for the document.</summary>
		/// <returns>The color of the text in the document. </returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170006D7 RID: 1751
		// (get) Token: 0x06001C71 RID: 7281 RVA: 0x0006D12C File Offset: 0x0006B32C
		// (set) Token: 0x06001C72 RID: 7282 RVA: 0x0006D140 File Offset: 0x0006B340
		public Color ForeColor
		{
			get
			{
				return this.ParseColor(this.document.ForeColor);
			}
			set
			{
				this.document.ForeColor = value.ToArgb().ToString();
			}
		}

		/// <summary>Gets a collection of all of the &lt;FORM&gt; elements in the document. </summary>
		/// <returns>An <see cref="T:System.Windows.Forms.HtmlElementCollection" /> of the &lt;FORM&gt; elements within the document.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006D8 RID: 1752
		// (get) Token: 0x06001C73 RID: 7283 RVA: 0x0006D168 File Offset: 0x0006B368
		public HtmlElementCollection Forms
		{
			get
			{
				return new HtmlElementCollection(this.owner, this.webHost, this.document.Forms);
			}
		}

		/// <summary>Gets a collection of all image tags in the document. </summary>
		/// <returns>A collection of <see cref="T:System.Windows.Forms.HtmlElement" /> objects, one for each IMG tag in the document. Elements are returned from the collection in source order. </returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006D9 RID: 1753
		// (get) Token: 0x06001C74 RID: 7284 RVA: 0x0006D188 File Offset: 0x0006B388
		public HtmlElementCollection Images
		{
			get
			{
				return new HtmlElementCollection(this.owner, this.webHost, this.document.Images);
			}
		}

		/// <summary>Gets or sets the color of hyperlinks.</summary>
		/// <returns>The color for hyperlinks in the current document.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170006DA RID: 1754
		// (get) Token: 0x06001C75 RID: 7285 RVA: 0x0006D1A8 File Offset: 0x0006B3A8
		// (set) Token: 0x06001C76 RID: 7286 RVA: 0x0006D1BC File Offset: 0x0006B3BC
		public Color LinkColor
		{
			get
			{
				return this.ParseColor(this.document.LinkColor);
			}
			set
			{
				this.document.LinkColor = value.ToArgb().ToString();
			}
		}

		/// <summary>Gets a list of all the hyperlinks within this HTML document.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.HtmlElementCollection" /> of <see cref="T:System.Windows.Forms.HtmlElement" /> objects.</returns>
		// Token: 0x170006DB RID: 1755
		// (get) Token: 0x06001C77 RID: 7287 RVA: 0x0006D1E4 File Offset: 0x0006B3E4
		public HtmlElementCollection Links
		{
			get
			{
				return new HtmlElementCollection(this.owner, this.webHost, this.document.Links);
			}
		}

		/// <summary>Gets or sets the direction of text in the current document.</summary>
		/// <returns>true if text renders from right to left; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006DC RID: 1756
		// (get) Token: 0x06001C78 RID: 7288 RVA: 0x0006D204 File Offset: 0x0006B404
		// (set) Token: 0x06001C79 RID: 7289 RVA: 0x0006D240 File Offset: 0x0006B440
		public bool RightToLeft
		{
			get
			{
				IAttribute attribute = this.document.Attributes["dir"];
				return attribute != null && attribute.Value == "rtl";
			}
			set
			{
				IAttribute attribute = this.document.Attributes["dir"];
				if (attribute == null && value)
				{
					IAttribute attribute2 = this.document.CreateAttribute("dir");
					attribute2.Value = "rtl";
					this.document.AppendChild(attribute2);
				}
				else if (attribute != null && !value)
				{
					this.document.RemoveChild(attribute);
				}
			}
		}

		/// <summary>Gets or sets the text value of the &lt;TITLE&gt; tag in the current HTML document. </summary>
		/// <returns>The title of the current document.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006DD RID: 1757
		// (get) Token: 0x06001C7A RID: 7290 RVA: 0x0006D2B8 File Offset: 0x0006B4B8
		// (set) Token: 0x06001C7B RID: 7291 RVA: 0x0006D2D8 File Offset: 0x0006B4D8
		public string Title
		{
			get
			{
				if (this.document == null)
				{
					return string.Empty;
				}
				return this.document.Title;
			}
			set
			{
				this.document.Title = value;
			}
		}

		/// <summary>Gets the URL describing the location of this document. </summary>
		/// <returns>A <see cref="T:System.Uri" /> representing this document's URL. </returns>
		// Token: 0x170006DE RID: 1758
		// (get) Token: 0x06001C7C RID: 7292 RVA: 0x0006D2E8 File Offset: 0x0006B4E8
		public Uri Url
		{
			get
			{
				return new Uri(this.document.Url);
			}
		}

		/// <summary>Gets or sets the Color of links to HTML pages that the user has already visited. </summary>
		/// <returns>The color of visited links. </returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170006DF RID: 1759
		// (get) Token: 0x06001C7D RID: 7293 RVA: 0x0006D2FC File Offset: 0x0006B4FC
		// (set) Token: 0x06001C7E RID: 7294 RVA: 0x0006D310 File Offset: 0x0006B510
		public Color VisitedLinkColor
		{
			get
			{
				return this.ParseColor(this.document.VisitedLinkColor);
			}
			set
			{
				this.document.VisitedLinkColor = value.ToArgb().ToString();
			}
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.HtmlWindow" /> associated with this document.</summary>
		/// <returns>The window for this document. </returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006E0 RID: 1760
		// (get) Token: 0x06001C7F RID: 7295 RVA: 0x0006D338 File Offset: 0x0006B538
		public HtmlWindow Window
		{
			get
			{
				return new HtmlWindow(this.owner, this.webHost, this.webHost.Window);
			}
		}

		// Token: 0x06001C80 RID: 7296 RVA: 0x0006D358 File Offset: 0x0006B558
		private void OnClick(object sender, EventArgs e)
		{
			HtmlElementEventHandler htmlElementEventHandler = (HtmlElementEventHandler)this.Events[HtmlDocument.ClickEvent];
			if (htmlElementEventHandler != null)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs();
				htmlElementEventHandler(this, htmlElementEventArgs);
			}
		}

		// Token: 0x06001C81 RID: 7297 RVA: 0x0006D390 File Offset: 0x0006B590
		private void OnContextMenuShowing(object sender, ContextMenuEventArgs e)
		{
			HtmlElementEventHandler htmlElementEventHandler = (HtmlElementEventHandler)this.Events[HtmlDocument.ContextMenuShowingEvent];
			if (htmlElementEventHandler != null)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs();
				htmlElementEventHandler(this, htmlElementEventArgs);
				if (htmlElementEventArgs.ReturnValue)
				{
					this.owner.OnWebHostContextMenuShown(sender, e);
				}
			}
		}

		// Token: 0x06001C82 RID: 7298 RVA: 0x0006D3E0 File Offset: 0x0006B5E0
		private void OnFocusing(object sender, EventArgs e)
		{
			HtmlElementEventHandler htmlElementEventHandler = (HtmlElementEventHandler)this.Events[HtmlDocument.FocusingEvent];
			if (htmlElementEventHandler != null)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs();
				htmlElementEventHandler(this, htmlElementEventArgs);
			}
		}

		// Token: 0x06001C83 RID: 7299 RVA: 0x0006D418 File Offset: 0x0006B618
		private void OnLosingFocus(object sender, EventArgs e)
		{
			HtmlElementEventHandler htmlElementEventHandler = (HtmlElementEventHandler)this.Events[HtmlDocument.LosingFocusEvent];
			if (htmlElementEventHandler != null)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs();
				htmlElementEventHandler(this, htmlElementEventArgs);
			}
		}

		// Token: 0x06001C84 RID: 7300 RVA: 0x0006D450 File Offset: 0x0006B650
		private void OnMouseDown(object sender, EventArgs e)
		{
			HtmlElementEventHandler htmlElementEventHandler = (HtmlElementEventHandler)this.Events[HtmlDocument.MouseDownEvent];
			if (htmlElementEventHandler != null)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs();
				htmlElementEventHandler(this, htmlElementEventArgs);
			}
		}

		// Token: 0x06001C85 RID: 7301 RVA: 0x0006D488 File Offset: 0x0006B688
		private void OnMouseLeave(object sender, EventArgs e)
		{
			HtmlElementEventHandler htmlElementEventHandler = (HtmlElementEventHandler)this.Events[HtmlDocument.MouseLeaveEvent];
			if (htmlElementEventHandler != null)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs();
				htmlElementEventHandler(this, htmlElementEventArgs);
			}
		}

		// Token: 0x06001C86 RID: 7302 RVA: 0x0006D4C0 File Offset: 0x0006B6C0
		private void OnMouseMove(object sender, EventArgs e)
		{
			HtmlElementEventHandler htmlElementEventHandler = (HtmlElementEventHandler)this.Events[HtmlDocument.MouseMoveEvent];
			if (htmlElementEventHandler != null)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs();
				htmlElementEventHandler(this, htmlElementEventArgs);
			}
		}

		// Token: 0x06001C87 RID: 7303 RVA: 0x0006D4F8 File Offset: 0x0006B6F8
		private void OnMouseOver(object sender, EventArgs e)
		{
			HtmlElementEventHandler htmlElementEventHandler = (HtmlElementEventHandler)this.Events[HtmlDocument.MouseOverEvent];
			if (htmlElementEventHandler != null)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs();
				htmlElementEventHandler(this, htmlElementEventArgs);
			}
		}

		// Token: 0x06001C88 RID: 7304 RVA: 0x0006D530 File Offset: 0x0006B730
		private void OnMouseUp(object sender, EventArgs e)
		{
			HtmlElementEventHandler htmlElementEventHandler = (HtmlElementEventHandler)this.Events[HtmlDocument.MouseUpEvent];
			if (htmlElementEventHandler != null)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs();
				htmlElementEventHandler(this, htmlElementEventArgs);
			}
		}

		// Token: 0x06001C89 RID: 7305 RVA: 0x0006D568 File Offset: 0x0006B768
		private void OnStop(object sender, EventArgs e)
		{
			HtmlElementEventHandler htmlElementEventHandler = (HtmlElementEventHandler)this.Events[HtmlDocument.StopEvent];
			if (htmlElementEventHandler != null)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs();
				htmlElementEventHandler(this, htmlElementEventArgs);
			}
		}

		// Token: 0x06001C8A RID: 7306 RVA: 0x0006D5A0 File Offset: 0x0006B7A0
		private Color ParseColor(string color)
		{
			if (color.IndexOf("#") >= 0)
			{
				return Color.FromArgb(int.Parse(color.Substring(color.IndexOf("#") + 1), 515));
			}
			return Color.FromName(color);
		}

		// Token: 0x170006E1 RID: 1761
		// (get) Token: 0x06001C8B RID: 7307 RVA: 0x0006D5E8 File Offset: 0x0006B7E8
		internal string DocType
		{
			get
			{
				if (this.document == null)
				{
					return string.Empty;
				}
				if (this.document.DocType != null)
				{
					return this.document.DocType.Name;
				}
				return string.Empty;
			}
		}

		/// <summary>Returns a value that indicates whether the specified <see cref="T:System.Windows.Forms.HtmlDocument" /> instances represent the same value. </summary>
		/// <returns>true if the specified instances are equal; otherwise, false.</returns>
		/// <param name="left">The first instance to compare.</param>
		/// <param name="right">The second instance to compare.</param>
		// Token: 0x06001C8C RID: 7308 RVA: 0x0006D62C File Offset: 0x0006B82C
		public static bool operator ==(HtmlDocument left, HtmlDocument right)
		{
			return left == right || (left != null && right != null && left.document.Equals(right.document));
		}

		/// <summary>Returns a value that indicates whether the specified <see cref="T:System.Windows.Forms.HtmlDocument" /> instances do not represent the same value. </summary>
		/// <returns>true if the specified instances are not equal; otherwise, false.</returns>
		/// <param name="left">The first instance to compare.</param>
		/// <param name="right">The second instance to compare.</param>
		// Token: 0x06001C8D RID: 7309 RVA: 0x0006D664 File Offset: 0x0006B864
		public static bool operator !=(HtmlDocument left, HtmlDocument right)
		{
			return !(left == right);
		}

		// Token: 0x04000F45 RID: 3909
		private EventHandlerList events;

		// Token: 0x04000F46 RID: 3910
		private IWebBrowser webHost;

		// Token: 0x04000F47 RID: 3911
		private IDocument document;

		// Token: 0x04000F48 RID: 3912
		private WebBrowser owner;
	}
}
