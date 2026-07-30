using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using Mono.WebBrowser;
using Mono.WebBrowser.DOM;

namespace System.Windows.Forms
{
	/// <summary>Represents an HTML element inside of a Web page. </summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020001B7 RID: 439
	public sealed class HtmlElement
	{
		// Token: 0x06001C8E RID: 7310 RVA: 0x0006D670 File Offset: 0x0006B870
		internal HtmlElement(WebBrowser owner, IWebBrowser webHost, IElement element)
		{
			this.webHost = webHost;
			this.element = element;
			this.owner = owner;
		}

		// Token: 0x06001C8F RID: 7311 RVA: 0x0006D690 File Offset: 0x0006B890
		// Note: this type is marked as 'beforefieldinit'.
		static HtmlElement()
		{
			HtmlElement.ClickEvent = new object();
			HtmlElement.DoubleClickEvent = new object();
			HtmlElement.MouseDownEvent = new object();
			HtmlElement.MouseUpEvent = new object();
			HtmlElement.MouseMoveEvent = new object();
			HtmlElement.MouseOverEvent = new object();
			HtmlElement.MouseEnterEvent = new object();
			HtmlElement.MouseLeaveEvent = new object();
			HtmlElement.KeyDownEvent = new object();
			HtmlElement.KeyPressEvent = new object();
			HtmlElement.KeyUpEvent = new object();
			HtmlElement.DragEvent = new object();
			HtmlElement.DragEndEvent = new object();
			HtmlElement.DragLeaveEvent = new object();
			HtmlElement.DragOverEvent = new object();
			HtmlElement.FocusingEvent = new object();
			HtmlElement.GotFocusEvent = new object();
			HtmlElement.LosingFocusEvent = new object();
			HtmlElement.LostFocusEvent = new object();
		}

		/// <summary>Occurs when the user clicks on the element with the left mouse button. </summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140001D0 RID: 464
		// (add) Token: 0x06001C90 RID: 7312 RVA: 0x0006D75C File Offset: 0x0006B95C
		// (remove) Token: 0x06001C91 RID: 7313 RVA: 0x0006D794 File Offset: 0x0006B994
		public event HtmlElementEventHandler Click
		{
			add
			{
				this.Events.AddHandler(HtmlElement.ClickEvent, value);
				this.element.Click += new NodeEventHandler(this.OnClick);
			}
			remove
			{
				this.Events.RemoveHandler(HtmlElement.ClickEvent, value);
				this.element.Click -= new NodeEventHandler(this.OnClick);
			}
		}

		/// <summary>Occurs when the user clicks the left mouse button over an element twice, in rapid succession.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140001D1 RID: 465
		// (add) Token: 0x06001C92 RID: 7314 RVA: 0x0006D7CC File Offset: 0x0006B9CC
		// (remove) Token: 0x06001C93 RID: 7315 RVA: 0x0006D804 File Offset: 0x0006BA04
		public event HtmlElementEventHandler DoubleClick
		{
			add
			{
				this.Events.AddHandler(HtmlElement.DoubleClickEvent, value);
				this.element.DoubleClick += new NodeEventHandler(this.OnDoubleClick);
			}
			remove
			{
				this.Events.RemoveHandler(HtmlElement.DoubleClickEvent, value);
				this.element.DoubleClick -= new NodeEventHandler(this.OnDoubleClick);
			}
		}

		/// <summary>Occurs when the user presses a mouse button.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140001D2 RID: 466
		// (add) Token: 0x06001C94 RID: 7316 RVA: 0x0006D83C File Offset: 0x0006BA3C
		// (remove) Token: 0x06001C95 RID: 7317 RVA: 0x0006D874 File Offset: 0x0006BA74
		public event HtmlElementEventHandler MouseDown
		{
			add
			{
				this.Events.AddHandler(HtmlElement.MouseDownEvent, value);
				this.element.MouseDown += new NodeEventHandler(this.OnMouseDown);
			}
			remove
			{
				this.Events.RemoveHandler(HtmlElement.MouseDownEvent, value);
				this.element.MouseDown -= new NodeEventHandler(this.OnMouseDown);
			}
		}

		/// <summary>Occurs when the user releases a mouse button.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140001D3 RID: 467
		// (add) Token: 0x06001C96 RID: 7318 RVA: 0x0006D8AC File Offset: 0x0006BAAC
		// (remove) Token: 0x06001C97 RID: 7319 RVA: 0x0006D8E4 File Offset: 0x0006BAE4
		public event HtmlElementEventHandler MouseUp
		{
			add
			{
				this.Events.AddHandler(HtmlElement.MouseUpEvent, value);
				this.element.MouseUp += new NodeEventHandler(this.OnMouseUp);
			}
			remove
			{
				this.Events.RemoveHandler(HtmlElement.MouseUpEvent, value);
				this.element.MouseUp -= new NodeEventHandler(this.OnMouseUp);
			}
		}

		/// <summary>Occurs when the user moves the mouse cursor across the element.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140001D4 RID: 468
		// (add) Token: 0x06001C98 RID: 7320 RVA: 0x0006D91C File Offset: 0x0006BB1C
		// (remove) Token: 0x06001C99 RID: 7321 RVA: 0x0006D954 File Offset: 0x0006BB54
		public event HtmlElementEventHandler MouseMove
		{
			add
			{
				this.Events.AddHandler(HtmlElement.MouseMoveEvent, value);
				this.element.MouseMove += new NodeEventHandler(this.OnMouseMove);
			}
			remove
			{
				this.Events.RemoveHandler(HtmlElement.MouseMoveEvent, value);
				this.element.MouseMove -= new NodeEventHandler(this.OnMouseMove);
			}
		}

		/// <summary>Occurs when the mouse cursor enters the bounds of the element.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140001D5 RID: 469
		// (add) Token: 0x06001C9A RID: 7322 RVA: 0x0006D98C File Offset: 0x0006BB8C
		// (remove) Token: 0x06001C9B RID: 7323 RVA: 0x0006D9C4 File Offset: 0x0006BBC4
		public event HtmlElementEventHandler MouseOver
		{
			add
			{
				this.Events.AddHandler(HtmlElement.MouseOverEvent, value);
				this.element.MouseOver += new NodeEventHandler(this.OnMouseOver);
			}
			remove
			{
				this.Events.RemoveHandler(HtmlElement.MouseOverEvent, value);
				this.element.MouseOver -= new NodeEventHandler(this.OnMouseOver);
			}
		}

		/// <summary>Occurs when the user first moves the mouse cursor over the current element. </summary>
		// Token: 0x140001D6 RID: 470
		// (add) Token: 0x06001C9C RID: 7324 RVA: 0x0006D9FC File Offset: 0x0006BBFC
		// (remove) Token: 0x06001C9D RID: 7325 RVA: 0x0006DA34 File Offset: 0x0006BC34
		public event HtmlElementEventHandler MouseEnter
		{
			add
			{
				this.Events.AddHandler(HtmlElement.MouseEnterEvent, value);
				this.element.MouseEnter += new NodeEventHandler(this.OnMouseEnter);
			}
			remove
			{
				this.Events.RemoveHandler(HtmlElement.MouseEnterEvent, value);
				this.element.MouseEnter -= new NodeEventHandler(this.OnMouseEnter);
			}
		}

		/// <summary>Occurs when the user moves the mouse cursor off of the current element. </summary>
		// Token: 0x140001D7 RID: 471
		// (add) Token: 0x06001C9E RID: 7326 RVA: 0x0006DA6C File Offset: 0x0006BC6C
		// (remove) Token: 0x06001C9F RID: 7327 RVA: 0x0006DAA4 File Offset: 0x0006BCA4
		public event HtmlElementEventHandler MouseLeave
		{
			add
			{
				this.Events.AddHandler(HtmlElement.MouseLeaveEvent, value);
				this.element.MouseLeave += new NodeEventHandler(this.OnMouseLeave);
			}
			remove
			{
				this.Events.RemoveHandler(HtmlElement.MouseLeaveEvent, value);
				this.element.MouseLeave -= new NodeEventHandler(this.OnMouseLeave);
			}
		}

		/// <summary>Occurs when the user presses a key on the keyboard.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140001D8 RID: 472
		// (add) Token: 0x06001CA0 RID: 7328 RVA: 0x0006DADC File Offset: 0x0006BCDC
		// (remove) Token: 0x06001CA1 RID: 7329 RVA: 0x0006DB14 File Offset: 0x0006BD14
		public event HtmlElementEventHandler KeyDown
		{
			add
			{
				this.Events.AddHandler(HtmlElement.KeyDownEvent, value);
				this.element.KeyDown += new NodeEventHandler(this.OnKeyDown);
			}
			remove
			{
				this.Events.RemoveHandler(HtmlElement.KeyDownEvent, value);
				this.element.KeyDown -= new NodeEventHandler(this.OnKeyDown);
			}
		}

		/// <summary>Occurs when the user presses and releases a key on the keyboard.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140001D9 RID: 473
		// (add) Token: 0x06001CA2 RID: 7330 RVA: 0x0006DB4C File Offset: 0x0006BD4C
		// (remove) Token: 0x06001CA3 RID: 7331 RVA: 0x0006DB84 File Offset: 0x0006BD84
		public event HtmlElementEventHandler KeyPress
		{
			add
			{
				this.Events.AddHandler(HtmlElement.KeyPressEvent, value);
				this.element.KeyPress += new NodeEventHandler(this.OnKeyPress);
			}
			remove
			{
				this.Events.RemoveHandler(HtmlElement.KeyPressEvent, value);
				this.element.KeyPress -= new NodeEventHandler(this.OnKeyPress);
			}
		}

		/// <summary>Occurs when the user releases a key on the keyboard.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140001DA RID: 474
		// (add) Token: 0x06001CA4 RID: 7332 RVA: 0x0006DBBC File Offset: 0x0006BDBC
		// (remove) Token: 0x06001CA5 RID: 7333 RVA: 0x0006DBF4 File Offset: 0x0006BDF4
		public event HtmlElementEventHandler KeyUp
		{
			add
			{
				this.Events.AddHandler(HtmlElement.KeyUpEvent, value);
				this.element.KeyUp += new NodeEventHandler(this.OnKeyUp);
			}
			remove
			{
				this.Events.RemoveHandler(HtmlElement.KeyUpEvent, value);
				this.element.KeyUp -= new NodeEventHandler(this.OnKeyUp);
			}
		}

		/// <summary>Occurs when the user drags text to various locations. </summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140001DB RID: 475
		// (add) Token: 0x06001CA6 RID: 7334 RVA: 0x0006DC2C File Offset: 0x0006BE2C
		// (remove) Token: 0x06001CA7 RID: 7335 RVA: 0x0006DC40 File Offset: 0x0006BE40
		public event HtmlElementEventHandler Drag
		{
			add
			{
				this.Events.AddHandler(HtmlElement.DragEvent, value);
			}
			remove
			{
				this.Events.RemoveHandler(HtmlElement.DragEvent, value);
			}
		}

		/// <summary>Occurs when a user finishes a drag operation.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140001DC RID: 476
		// (add) Token: 0x06001CA8 RID: 7336 RVA: 0x0006DC54 File Offset: 0x0006BE54
		// (remove) Token: 0x06001CA9 RID: 7337 RVA: 0x0006DC68 File Offset: 0x0006BE68
		public event HtmlElementEventHandler DragEnd
		{
			add
			{
				this.Events.AddHandler(HtmlElement.DragEndEvent, value);
			}
			remove
			{
				this.Events.RemoveHandler(HtmlElement.DragEndEvent, value);
			}
		}

		/// <summary>Occurs when the user is no longer dragging an item over this element. </summary>
		// Token: 0x140001DD RID: 477
		// (add) Token: 0x06001CAA RID: 7338 RVA: 0x0006DC7C File Offset: 0x0006BE7C
		// (remove) Token: 0x06001CAB RID: 7339 RVA: 0x0006DC90 File Offset: 0x0006BE90
		public event HtmlElementEventHandler DragLeave
		{
			add
			{
				this.Events.AddHandler(HtmlElement.DragLeaveEvent, value);
			}
			remove
			{
				this.Events.RemoveHandler(HtmlElement.DragLeaveEvent, value);
			}
		}

		/// <summary>Occurs when the user drags text over the element.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140001DE RID: 478
		// (add) Token: 0x06001CAC RID: 7340 RVA: 0x0006DCA4 File Offset: 0x0006BEA4
		// (remove) Token: 0x06001CAD RID: 7341 RVA: 0x0006DCB8 File Offset: 0x0006BEB8
		public event HtmlElementEventHandler DragOver
		{
			add
			{
				this.Events.AddHandler(HtmlElement.DragOverEvent, value);
			}
			remove
			{
				this.Events.RemoveHandler(HtmlElement.DragOverEvent, value);
			}
		}

		/// <summary>Occurs when the element first receives user input focus. </summary>
		// Token: 0x140001DF RID: 479
		// (add) Token: 0x06001CAE RID: 7342 RVA: 0x0006DCCC File Offset: 0x0006BECC
		// (remove) Token: 0x06001CAF RID: 7343 RVA: 0x0006DD04 File Offset: 0x0006BF04
		public event HtmlElementEventHandler Focusing
		{
			add
			{
				this.Events.AddHandler(HtmlElement.FocusingEvent, value);
				this.element.OnFocus += new NodeEventHandler(this.OnFocusing);
			}
			remove
			{
				this.Events.RemoveHandler(HtmlElement.FocusingEvent, value);
				this.element.OnFocus -= new NodeEventHandler(this.OnFocusing);
			}
		}

		/// <summary>Occurs when the element has received user input focus.</summary>
		// Token: 0x140001E0 RID: 480
		// (add) Token: 0x06001CB0 RID: 7344 RVA: 0x0006DD3C File Offset: 0x0006BF3C
		// (remove) Token: 0x06001CB1 RID: 7345 RVA: 0x0006DD74 File Offset: 0x0006BF74
		public event HtmlElementEventHandler GotFocus
		{
			add
			{
				this.Events.AddHandler(HtmlElement.GotFocusEvent, value);
				this.element.OnFocus += new NodeEventHandler(this.OnGotFocus);
			}
			remove
			{
				this.Events.RemoveHandler(HtmlElement.GotFocusEvent, value);
				this.element.OnFocus -= new NodeEventHandler(this.OnGotFocus);
			}
		}

		/// <summary>Occurs when the element is losing user input focus. </summary>
		// Token: 0x140001E1 RID: 481
		// (add) Token: 0x06001CB2 RID: 7346 RVA: 0x0006DDAC File Offset: 0x0006BFAC
		// (remove) Token: 0x06001CB3 RID: 7347 RVA: 0x0006DDE4 File Offset: 0x0006BFE4
		public event HtmlElementEventHandler LosingFocus
		{
			add
			{
				this.Events.AddHandler(HtmlElement.LosingFocusEvent, value);
				this.element.OnBlur += new NodeEventHandler(this.OnLosingFocus);
			}
			remove
			{
				this.Events.RemoveHandler(HtmlElement.LosingFocusEvent, value);
				this.element.OnBlur -= new NodeEventHandler(this.OnLosingFocus);
			}
		}

		/// <summary>Occurs when the element has lost user input focus. </summary>
		// Token: 0x140001E2 RID: 482
		// (add) Token: 0x06001CB4 RID: 7348 RVA: 0x0006DE1C File Offset: 0x0006C01C
		// (remove) Token: 0x06001CB5 RID: 7349 RVA: 0x0006DE54 File Offset: 0x0006C054
		public event HtmlElementEventHandler LostFocus
		{
			add
			{
				this.Events.AddHandler(HtmlElement.LostFocusEvent, value);
				this.element.OnBlur += new NodeEventHandler(this.OnLostFocus);
			}
			remove
			{
				this.Events.RemoveHandler(HtmlElement.LostFocusEvent, value);
				this.element.OnBlur -= new NodeEventHandler(this.OnLostFocus);
			}
		}

		// Token: 0x170006E2 RID: 1762
		// (get) Token: 0x06001CB6 RID: 7350 RVA: 0x0006DE8C File Offset: 0x0006C08C
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

		/// <summary>Gets an <see cref="T:System.Windows.Forms.HtmlElementCollection" /> of all elements underneath the current element. </summary>
		/// <returns>A collection of all elements that are direct or indirect children of the current element. If the current element is a TABLE, for example, <see cref="P:System.Windows.Forms.HtmlElement.All" /> will return every TH, TR, and TD element within the table, as well as any other elements, such as DIV and SPAN elements, contained within the cells. </returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006E3 RID: 1763
		// (get) Token: 0x06001CB7 RID: 7351 RVA: 0x0006DEAC File Offset: 0x0006C0AC
		public HtmlElementCollection All
		{
			get
			{
				return new HtmlElementCollection(this.owner, this.webHost, this.element.All);
			}
		}

		/// <summary>Gets a value indicating whether this element can have child elements.</summary>
		/// <returns>true if element can have child elements; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006E4 RID: 1764
		// (get) Token: 0x06001CB8 RID: 7352 RVA: 0x0006DECC File Offset: 0x0006C0CC
		public bool CanHaveChildren
		{
			get
			{
				string tagName = this.TagName;
				string text = tagName.ToLowerInvariant();
				if (text != null)
				{
					if (HtmlElement.<>f__switch$map7 == null)
					{
						Dictionary<string, int> dictionary = new Dictionary<string, int>(13);
						dictionary.Add("area", 0);
						dictionary.Add("base", 0);
						dictionary.Add("basefont", 0);
						dictionary.Add("br", 0);
						dictionary.Add("col", 0);
						dictionary.Add("frame", 0);
						dictionary.Add("hr", 0);
						dictionary.Add("img", 0);
						dictionary.Add("input", 0);
						dictionary.Add("isindex", 0);
						dictionary.Add("link", 0);
						dictionary.Add("meta", 0);
						dictionary.Add("param", 0);
						HtmlElement.<>f__switch$map7 = dictionary;
					}
					int num;
					if (HtmlElement.<>f__switch$map7.TryGetValue(text, ref num))
					{
						if (num == 0)
						{
							return false;
						}
					}
				}
				return true;
			}
		}

		/// <summary>Gets an <see cref="T:System.Windows.Forms.HtmlElementCollection" /> of all children of the current element.</summary>
		/// <returns>A collection of all <see cref="T:System.Windows.Forms.HtmlElement" /> objects that have the current element as a parent.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006E5 RID: 1765
		// (get) Token: 0x06001CB9 RID: 7353 RVA: 0x0006DFC4 File Offset: 0x0006C1C4
		public HtmlElementCollection Children
		{
			get
			{
				return new HtmlElementCollection(this.owner, this.webHost, this.element.Children);
			}
		}

		/// <summary>Gets the bounds of the client area of the element in the HTML document.</summary>
		/// <returns>The client area occupied by the element, minus any area taken by borders and scroll bars. To obtain the position and dimensions of the element inclusive of its adornments, use <see cref="P:System.Windows.Forms.HtmlElement.OffsetRectangle" /> instead.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170006E6 RID: 1766
		// (get) Token: 0x06001CBA RID: 7354 RVA: 0x0006DFE4 File Offset: 0x0006C1E4
		public Rectangle ClientRectangle
		{
			get
			{
				return new Rectangle(0, 0, this.element.ClientWidth, this.element.ClientHeight);
			}
		}

		/// <summary>Gets the location of an element relative to its parent.</summary>
		/// <returns>The x- and y-coordinate positions of the element, and its width and its height, in relation to its parent. If an element's parent is relatively or absolutely positioned, <see cref="P:System.Windows.Forms.HtmlElement.OffsetRectangle" /> will return the offset of the parent element. If the element itself is relatively positioned with respect to its parent, <see cref="P:System.Windows.Forms.HtmlElement.OffsetRectangle" /> will return the offset from its parent.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170006E7 RID: 1767
		// (get) Token: 0x06001CBB RID: 7355 RVA: 0x0006E010 File Offset: 0x0006C210
		public Rectangle OffsetRectangle
		{
			get
			{
				return new Rectangle(this.element.OffsetLeft, this.element.OffsetTop, this.element.OffsetWidth, this.element.OffsetHeight);
			}
		}

		/// <summary>Gets the dimensions of an element's scrollable region.</summary>
		/// <returns>The size and coordinate location of the scrollable area of an element.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006E8 RID: 1768
		// (get) Token: 0x06001CBC RID: 7356 RVA: 0x0006E050 File Offset: 0x0006C250
		public Rectangle ScrollRectangle
		{
			get
			{
				return new Rectangle(this.element.ScrollLeft, this.element.ScrollTop, this.element.ScrollWidth, this.element.ScrollHeight);
			}
		}

		/// <summary>Gets or sets the distance between the edge of the element and the left edge of its content.</summary>
		/// <returns>The distance, in pixels, between the left edge of the element and the left edge of its content.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006E9 RID: 1769
		// (get) Token: 0x06001CBD RID: 7357 RVA: 0x0006E090 File Offset: 0x0006C290
		// (set) Token: 0x06001CBE RID: 7358 RVA: 0x0006E0A0 File Offset: 0x0006C2A0
		public int ScrollLeft
		{
			get
			{
				return this.element.ScrollLeft;
			}
			set
			{
				this.element.ScrollLeft = value;
			}
		}

		/// <summary>Gets or sets the distance between the edge of the element and the top edge of its content.</summary>
		/// <returns>The distance, in pixels, between the top edge of the element and the top edge of its content.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006EA RID: 1770
		// (get) Token: 0x06001CBF RID: 7359 RVA: 0x0006E0B0 File Offset: 0x0006C2B0
		// (set) Token: 0x06001CC0 RID: 7360 RVA: 0x0006E0C0 File Offset: 0x0006C2C0
		public int ScrollTop
		{
			get
			{
				return this.element.ScrollTop;
			}
			set
			{
				this.element.ScrollTop = value;
			}
		}

		/// <summary>Gets the element from which <see cref="P:System.Windows.Forms.HtmlElement.OffsetRectangle" /> is calculated.</summary>
		/// <returns>The element from which the offsets are calculated.If an element's parent or another element in the element's hierarchy uses relative or absolute positioning, OffsetParent will be the first relatively or absolutely positioned element in which the current element is nested. If none of the elements above the current element are absolutely or relatively positioned, OffsetParent will be the BODY tag of the document. </returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170006EB RID: 1771
		// (get) Token: 0x06001CC1 RID: 7361 RVA: 0x0006E0D0 File Offset: 0x0006C2D0
		public HtmlElement OffsetParent
		{
			get
			{
				return new HtmlElement(this.owner, this.webHost, this.element.OffsetParent);
			}
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.HtmlDocument" /> to which this element belongs.</summary>
		/// <returns>The parent document of this element.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006EC RID: 1772
		// (get) Token: 0x06001CC2 RID: 7362 RVA: 0x0006E0F0 File Offset: 0x0006C2F0
		public HtmlDocument Document
		{
			get
			{
				return new HtmlDocument(this.owner, this.webHost, this.element.Owner);
			}
		}

		/// <summary>Gets or sets whether the user can input data into this element.</summary>
		/// <returns>true if the element allows user input; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006ED RID: 1773
		// (get) Token: 0x06001CC3 RID: 7363 RVA: 0x0006E110 File Offset: 0x0006C310
		// (set) Token: 0x06001CC4 RID: 7364 RVA: 0x0006E120 File Offset: 0x0006C320
		public bool Enabled
		{
			get
			{
				return !this.element.Disabled;
			}
			set
			{
				this.element.Disabled = !value;
			}
		}

		/// <summary>Gets or sets the HTML markup underneath this element.</summary>
		/// <returns>The HTML markup that defines the child elements of the current element.</returns>
		/// <exception cref="T:System.NotSupportedException">Creating child elements on this element is not allowed. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006EE RID: 1774
		// (get) Token: 0x06001CC5 RID: 7365 RVA: 0x0006E134 File Offset: 0x0006C334
		// (set) Token: 0x06001CC6 RID: 7366 RVA: 0x0006E144 File Offset: 0x0006C344
		public string InnerHtml
		{
			get
			{
				return this.element.InnerHTML;
			}
			set
			{
				this.element.InnerHTML = value;
			}
		}

		/// <summary>Gets or sets the text assigned to the element.</summary>
		/// <returns>The element's text, absent any HTML markup. If the element contains child elements, only the text in those child elements will be preserved. </returns>
		/// <exception cref="T:System.NotSupportedException">The specified element cannot contain text (for example, an IMG element). </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006EF RID: 1775
		// (get) Token: 0x06001CC7 RID: 7367 RVA: 0x0006E154 File Offset: 0x0006C354
		// (set) Token: 0x06001CC8 RID: 7368 RVA: 0x0006E164 File Offset: 0x0006C364
		public string InnerText
		{
			get
			{
				return this.element.InnerText;
			}
			set
			{
				this.element.InnerText = value;
			}
		}

		/// <summary>Gets or sets a label by which to identify the element.</summary>
		/// <returns>The unique identifier for the element. </returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006F0 RID: 1776
		// (get) Token: 0x06001CC9 RID: 7369 RVA: 0x0006E174 File Offset: 0x0006C374
		// (set) Token: 0x06001CCA RID: 7370 RVA: 0x0006E184 File Offset: 0x0006C384
		public string Id
		{
			get
			{
				return this.GetAttribute("id");
			}
			set
			{
				this.SetAttribute("id", value);
			}
		}

		/// <summary>Gets or sets the name of the element. </summary>
		/// <returns>A <see cref="T:System.String" /> representing the element's name.</returns>
		// Token: 0x170006F1 RID: 1777
		// (get) Token: 0x06001CCB RID: 7371 RVA: 0x0006E194 File Offset: 0x0006C394
		// (set) Token: 0x06001CCC RID: 7372 RVA: 0x0006E1A4 File Offset: 0x0006C3A4
		public string Name
		{
			get
			{
				return this.GetAttribute("name");
			}
			set
			{
				this.SetAttribute("name", value);
			}
		}

		/// <summary>Gets the next element below this element in the document tree. </summary>
		/// <returns>An <see cref="T:System.Windows.Forms.HtmlElement" /> representing the first element contained underneath the current element, in source order.</returns>
		// Token: 0x170006F2 RID: 1778
		// (get) Token: 0x06001CCD RID: 7373 RVA: 0x0006E1B4 File Offset: 0x0006C3B4
		public HtmlElement FirstChild
		{
			get
			{
				return new HtmlElement(this.owner, this.webHost, (IElement)this.element.FirstChild);
			}
		}

		/// <summary>Gets the next element at the same level as this element in the document tree. </summary>
		/// <returns>An <see cref="T:System.Windows.Forms.HtmlElement" /> representing the element to the right of the current element. </returns>
		// Token: 0x170006F3 RID: 1779
		// (get) Token: 0x06001CCE RID: 7374 RVA: 0x0006E1D8 File Offset: 0x0006C3D8
		public HtmlElement NextSibling
		{
			get
			{
				return new HtmlElement(this.owner, this.webHost, (IElement)this.element.Next);
			}
		}

		/// <summary>Gets the current element's parent element.</summary>
		/// <returns>The element above the current element in the HTML document's hierarchy.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006F4 RID: 1780
		// (get) Token: 0x06001CCF RID: 7375 RVA: 0x0006E1FC File Offset: 0x0006C3FC
		public HtmlElement Parent
		{
			get
			{
				return new HtmlElement(this.owner, this.webHost, (IElement)this.element.Parent);
			}
		}

		/// <summary>Gets the name of the HTML tag.</summary>
		/// <returns>The name used to create this element using HTML markup.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006F5 RID: 1781
		// (get) Token: 0x06001CD0 RID: 7376 RVA: 0x0006E220 File Offset: 0x0006C420
		public string TagName
		{
			get
			{
				return this.element.TagName;
			}
		}

		/// <summary>Gets or sets the location of this element in the tab order.</summary>
		/// <returns>The numeric index of the element in the tab order.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006F6 RID: 1782
		// (get) Token: 0x06001CD1 RID: 7377 RVA: 0x0006E230 File Offset: 0x0006C430
		// (set) Token: 0x06001CD2 RID: 7378 RVA: 0x0006E240 File Offset: 0x0006C440
		public short TabIndex
		{
			get
			{
				return (short)this.element.TabIndex;
			}
			set
			{
				this.element.TabIndex = (int)value;
			}
		}

		/// <summary>Gets an unmanaged interface pointer for this element.</summary>
		/// <returns>The COM IUnknown pointer for the element, which you can cast to one of the HTML element interfaces, such as IHTMLElement.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006F7 RID: 1783
		// (get) Token: 0x06001CD3 RID: 7379 RVA: 0x0006E250 File Offset: 0x0006C450
		public object DomElement
		{
			get
			{
				throw new NotSupportedException("Retrieving a reference to an mshtml interface is not supported. Sorry.");
			}
		}

		/// <summary>Gets or sets the current element's HTML code. </summary>
		/// <returns>The HTML code for the current element and its children.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006F8 RID: 1784
		// (get) Token: 0x06001CD4 RID: 7380 RVA: 0x0006E25C File Offset: 0x0006C45C
		// (set) Token: 0x06001CD5 RID: 7381 RVA: 0x0006E26C File Offset: 0x0006C46C
		public string OuterHtml
		{
			get
			{
				return this.element.OuterHTML;
			}
			set
			{
				this.element.OuterHTML = value;
			}
		}

		/// <summary>Gets or sets the current element's text. </summary>
		/// <returns>The text inside the current element, and in the element's children. </returns>
		/// <exception cref="T:System.NotSupportedException">You cannot set text outside of this element.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006F9 RID: 1785
		// (get) Token: 0x06001CD6 RID: 7382 RVA: 0x0006E27C File Offset: 0x0006C47C
		// (set) Token: 0x06001CD7 RID: 7383 RVA: 0x0006E28C File Offset: 0x0006C48C
		public string OuterText
		{
			get
			{
				return this.element.OuterText;
			}
			set
			{
				this.element.OuterText = value;
			}
		}

		/// <summary>Gets or sets a comma-delimited list of styles for the current element. </summary>
		/// <returns>A string consisting of all of the element's styles</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006FA RID: 1786
		// (get) Token: 0x06001CD8 RID: 7384 RVA: 0x0006E29C File Offset: 0x0006C49C
		// (set) Token: 0x06001CD9 RID: 7385 RVA: 0x0006E2AC File Offset: 0x0006C4AC
		public string Style
		{
			get
			{
				return this.element.Style;
			}
			set
			{
				this.element.Style = value;
			}
		}

		/// <summary>Adds an element to another element's subtree.</summary>
		/// <returns>The element after it has been added to the tree. </returns>
		/// <param name="newElement">The <see cref="T:System.Windows.Forms.HtmlElement" /> to append to this location in the tree. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001CDA RID: 7386 RVA: 0x0006E2BC File Offset: 0x0006C4BC
		public HtmlElement AppendChild(HtmlElement newElement)
		{
			IElement element = this.element.AppendChild(newElement.element);
			newElement.element = element;
			return newElement;
		}

		/// <summary>Adds an event handler for a named event on the HTML Document Object Model (DOM).</summary>
		/// <param name="eventName">The name of the event you want to handle.</param>
		/// <param name="eventHandler">The managed code that handles the event.</param>
		// Token: 0x06001CDB RID: 7387 RVA: 0x0006E2E4 File Offset: 0x0006C4E4
		public void AttachEventHandler(string eventName, EventHandler eventHandler)
		{
			this.element.AttachEventHandler(eventName, eventHandler);
		}

		/// <summary>Removes an event handler from a named event on the HTML Document Object Model (DOM).</summary>
		/// <param name="eventName">The name of the event you want to handle.</param>
		/// <param name="eventHandler">The managed code that handles the event.</param>
		// Token: 0x06001CDC RID: 7388 RVA: 0x0006E2F4 File Offset: 0x0006C4F4
		public void DetachEventHandler(string eventName, EventHandler eventHandler)
		{
			this.element.DetachEventHandler(eventName, eventHandler);
		}

		/// <summary>Puts user input focus on the current element.</summary>
		// Token: 0x06001CDD RID: 7389 RVA: 0x0006E304 File Offset: 0x0006C504
		public void Focus()
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the value of the named attribute on the element.</summary>
		/// <returns>The value of this attribute on the element, as a <see cref="T:System.String" /> value. If the specified attribute does not exist on this element, returns an empty string.</returns>
		/// <param name="attributeName">The name of the attribute. This argument is case-insensitive.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001CDE RID: 7390 RVA: 0x0006E30C File Offset: 0x0006C50C
		public string GetAttribute(string attributeName)
		{
			return this.element.GetAttribute(attributeName);
		}

		/// <summary>Retrieves a collection of elements represented in HTML by the specified HTML tag.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.HtmlElementCollection" /> containing all elements whose HTML tag name is equal to <paramref name="tagName" />.</returns>
		/// <param name="tagName">The name of the tag whose <see cref="T:System.Windows.Forms.HtmlElement" /> objects you wish to retrieve.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001CDF RID: 7391 RVA: 0x0006E31C File Offset: 0x0006C51C
		public HtmlElementCollection GetElementsByTagName(string tagName)
		{
			IElementCollection elementsByTagName = this.element.GetElementsByTagName(tagName);
			return new HtmlElementCollection(this.owner, this.webHost, elementsByTagName);
		}

		/// <returns>A hash code for the current <see cref="T:System.Object" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001CE0 RID: 7392 RVA: 0x0006E348 File Offset: 0x0006C548
		public override int GetHashCode()
		{
			if (this.element == null)
			{
				return 0;
			}
			return this.element.GetHashCode();
		}

		// Token: 0x06001CE1 RID: 7393 RVA: 0x0006E364 File Offset: 0x0006C564
		internal bool HasAttribute(string name)
		{
			return this.element.HasAttribute(name);
		}

		/// <summary>Insert a new element into the Document Object Model (DOM).</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.HtmlElement" /> that was just inserted. If insertion failed, this will return null.</returns>
		/// <param name="orient">Where to insert this element in relation to the current element.</param>
		/// <param name="newElement">The new element to insert.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001CE2 RID: 7394 RVA: 0x0006E374 File Offset: 0x0006C574
		public HtmlElement InsertAdjacentElement(HtmlElementInsertionOrientation orient, HtmlElement newElement)
		{
			switch (orient)
			{
			case HtmlElementInsertionOrientation.BeforeBegin:
				this.element.Parent.InsertBefore(newElement.element, this.element);
				return newElement;
			case HtmlElementInsertionOrientation.AfterBegin:
				this.element.InsertBefore(newElement.element, this.element.FirstChild);
				return newElement;
			case HtmlElementInsertionOrientation.BeforeEnd:
				return this.AppendChild(newElement);
			case HtmlElementInsertionOrientation.AfterEnd:
				return this.AppendChild(newElement);
			default:
				return null;
			}
		}

		/// <summary>Executes an unexposed method on the underlying DOM element of this element.</summary>
		/// <returns>The element returned by this method, represented as an <see cref="T:System.Object" />. If this <see cref="T:System.Object" /> is another HTML element, and you have a reference to the unmanaged MSHTML library added to your project, you can cast it to its appropriate unmanaged interface.</returns>
		/// <param name="methodName">The name of the property or method to invoke. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001CE3 RID: 7395 RVA: 0x0006E3F0 File Offset: 0x0006C5F0
		public object InvokeMember(string methodName)
		{
			return this.element.Owner.InvokeScript("eval ('" + methodName + "()');");
		}

		/// <summary>Executes a function defined in the current HTML page by a scripting language.</summary>
		/// <returns>The element returned by the function, represented as an <see cref="T:System.Object" />. If this <see cref="T:System.Object" /> is another HTML element, and you have a reference to the unmanaged MSHTML library added to your project, you can cast it to its appropriate unmanaged interface.</returns>
		/// <param name="methodName">The name of the property or method to invoke.</param>
		/// <param name="parameter"></param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001CE4 RID: 7396 RVA: 0x0006E420 File Offset: 0x0006C620
		public object InvokeMember(string methodName, params object[] parameter)
		{
			string[] array = new string[parameter.Length];
			for (int i = 0; i < parameter.Length; i++)
			{
				array[i] = parameter.ToString();
			}
			return this.element.Owner.InvokeScript(string.Concat(new string[]
			{
				"eval ('",
				methodName,
				"(",
				string.Join(",", array),
				")');"
			}));
		}

		/// <summary>Causes the named event to call all registered event handlers. </summary>
		/// <param name="eventName">The name of the event to raise. </param>
		// Token: 0x06001CE5 RID: 7397 RVA: 0x0006E498 File Offset: 0x0006C698
		public void RaiseEvent(string eventName)
		{
			this.element.FireEvent(eventName);
		}

		/// <summary>Removes focus from the current element, if that element has focus. </summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001CE6 RID: 7398 RVA: 0x0006E4A8 File Offset: 0x0006C6A8
		public void RemoveFocus()
		{
			this.element.Blur();
		}

		/// <summary>Scrolls through the document containing this element until the top or bottom edge of this element is aligned with the document's window. </summary>
		/// <param name="alignWithTop">If true, the top of the object will be displayed at the top of the window. If false, the bottom of the object will be displayed at the bottom of the window.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001CE7 RID: 7399 RVA: 0x0006E4B8 File Offset: 0x0006C6B8
		public void ScrollIntoView(bool alignWithTop)
		{
			this.element.ScrollIntoView(alignWithTop);
		}

		/// <summary>Sets the value of the named attribute on the element.</summary>
		/// <param name="attributeName">The name of the attribute to set.</param>
		/// <param name="value">The new value of this attribute. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001CE8 RID: 7400 RVA: 0x0006E4C8 File Offset: 0x0006C6C8
		public void SetAttribute(string attributeName, string value)
		{
			this.element.SetAttribute(attributeName, value);
		}

		/// <summary>Tests if the supplied object is equal to the current element.</summary>
		/// <returns>true if <paramref name="obj" /> is an <see cref="T:System.Windows.Forms.HtmlElement" />; otherwise, false.</returns>
		/// <param name="obj">The object to test for equality.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001CE9 RID: 7401 RVA: 0x0006E4D8 File Offset: 0x0006C6D8
		public override bool Equals(object obj)
		{
			return this == (HtmlElement)obj;
		}

		// Token: 0x06001CEA RID: 7402 RVA: 0x0006E4E8 File Offset: 0x0006C6E8
		private void OnClick(object sender, EventArgs e)
		{
			HtmlElementEventHandler htmlElementEventHandler = (HtmlElementEventHandler)this.Events[HtmlElement.ClickEvent];
			if (htmlElementEventHandler != null)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs();
				htmlElementEventHandler(this, htmlElementEventArgs);
			}
		}

		// Token: 0x06001CEB RID: 7403 RVA: 0x0006E520 File Offset: 0x0006C720
		private void OnDoubleClick(object sender, EventArgs e)
		{
			HtmlElementEventHandler htmlElementEventHandler = (HtmlElementEventHandler)this.Events[HtmlElement.DoubleClickEvent];
			if (htmlElementEventHandler != null)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs();
				htmlElementEventHandler(this, htmlElementEventArgs);
			}
		}

		// Token: 0x06001CEC RID: 7404 RVA: 0x0006E558 File Offset: 0x0006C758
		private void OnMouseDown(object sender, EventArgs e)
		{
			HtmlElementEventHandler htmlElementEventHandler = (HtmlElementEventHandler)this.Events[HtmlElement.MouseDownEvent];
			if (htmlElementEventHandler != null)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs();
				htmlElementEventHandler(this, htmlElementEventArgs);
			}
		}

		// Token: 0x06001CED RID: 7405 RVA: 0x0006E590 File Offset: 0x0006C790
		private void OnMouseUp(object sender, EventArgs e)
		{
			HtmlElementEventHandler htmlElementEventHandler = (HtmlElementEventHandler)this.Events[HtmlElement.MouseUpEvent];
			if (htmlElementEventHandler != null)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs();
				htmlElementEventHandler(this, htmlElementEventArgs);
			}
		}

		// Token: 0x06001CEE RID: 7406 RVA: 0x0006E5C8 File Offset: 0x0006C7C8
		private void OnMouseMove(object sender, EventArgs e)
		{
			HtmlElementEventHandler htmlElementEventHandler = (HtmlElementEventHandler)this.Events[HtmlElement.MouseMoveEvent];
			if (htmlElementEventHandler != null)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs();
				htmlElementEventHandler(this, htmlElementEventArgs);
			}
		}

		// Token: 0x06001CEF RID: 7407 RVA: 0x0006E600 File Offset: 0x0006C800
		private void OnMouseOver(object sender, EventArgs e)
		{
			HtmlElementEventHandler htmlElementEventHandler = (HtmlElementEventHandler)this.Events[HtmlElement.MouseOverEvent];
			if (htmlElementEventHandler != null)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs();
				htmlElementEventHandler(this, htmlElementEventArgs);
			}
		}

		// Token: 0x06001CF0 RID: 7408 RVA: 0x0006E638 File Offset: 0x0006C838
		private void OnMouseEnter(object sender, EventArgs e)
		{
			HtmlElementEventHandler htmlElementEventHandler = (HtmlElementEventHandler)this.Events[HtmlElement.MouseEnterEvent];
			if (htmlElementEventHandler != null)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs();
				htmlElementEventHandler(this, htmlElementEventArgs);
			}
		}

		// Token: 0x06001CF1 RID: 7409 RVA: 0x0006E670 File Offset: 0x0006C870
		private void OnMouseLeave(object sender, EventArgs e)
		{
			HtmlElementEventHandler htmlElementEventHandler = (HtmlElementEventHandler)this.Events[HtmlElement.MouseLeaveEvent];
			if (htmlElementEventHandler != null)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs();
				htmlElementEventHandler(this, htmlElementEventArgs);
			}
		}

		// Token: 0x06001CF2 RID: 7410 RVA: 0x0006E6A8 File Offset: 0x0006C8A8
		private void OnKeyDown(object sender, EventArgs e)
		{
			HtmlElementEventHandler htmlElementEventHandler = (HtmlElementEventHandler)this.Events[HtmlElement.KeyDownEvent];
			if (htmlElementEventHandler != null)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs();
				htmlElementEventHandler(this, htmlElementEventArgs);
			}
		}

		// Token: 0x06001CF3 RID: 7411 RVA: 0x0006E6E0 File Offset: 0x0006C8E0
		private void OnKeyPress(object sender, EventArgs e)
		{
			HtmlElementEventHandler htmlElementEventHandler = (HtmlElementEventHandler)this.Events[HtmlElement.KeyPressEvent];
			if (htmlElementEventHandler != null)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs();
				htmlElementEventHandler(this, htmlElementEventArgs);
			}
		}

		// Token: 0x06001CF4 RID: 7412 RVA: 0x0006E718 File Offset: 0x0006C918
		private void OnKeyUp(object sender, EventArgs e)
		{
			HtmlElementEventHandler htmlElementEventHandler = (HtmlElementEventHandler)this.Events[HtmlElement.KeyUpEvent];
			if (htmlElementEventHandler != null)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs();
				htmlElementEventHandler(this, htmlElementEventArgs);
			}
		}

		// Token: 0x06001CF5 RID: 7413 RVA: 0x0006E750 File Offset: 0x0006C950
		private void OnDrag(object sender, EventArgs e)
		{
			HtmlElementEventHandler htmlElementEventHandler = (HtmlElementEventHandler)this.Events[HtmlElement.DragEvent];
			if (htmlElementEventHandler != null)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs();
				htmlElementEventHandler(this, htmlElementEventArgs);
			}
		}

		// Token: 0x06001CF6 RID: 7414 RVA: 0x0006E788 File Offset: 0x0006C988
		private void OnDragEnd(object sender, EventArgs e)
		{
			HtmlElementEventHandler htmlElementEventHandler = (HtmlElementEventHandler)this.Events[HtmlElement.DragEndEvent];
			if (htmlElementEventHandler != null)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs();
				htmlElementEventHandler(this, htmlElementEventArgs);
			}
		}

		// Token: 0x06001CF7 RID: 7415 RVA: 0x0006E7C0 File Offset: 0x0006C9C0
		private void OnDragLeave(object sender, EventArgs e)
		{
			HtmlElementEventHandler htmlElementEventHandler = (HtmlElementEventHandler)this.Events[HtmlElement.DragLeaveEvent];
			if (htmlElementEventHandler != null)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs();
				htmlElementEventHandler(this, htmlElementEventArgs);
			}
		}

		// Token: 0x06001CF8 RID: 7416 RVA: 0x0006E7F8 File Offset: 0x0006C9F8
		private void OnDragOver(object sender, EventArgs e)
		{
			HtmlElementEventHandler htmlElementEventHandler = (HtmlElementEventHandler)this.Events[HtmlElement.DragOverEvent];
			if (htmlElementEventHandler != null)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs();
				htmlElementEventHandler(this, htmlElementEventArgs);
			}
		}

		// Token: 0x06001CF9 RID: 7417 RVA: 0x0006E830 File Offset: 0x0006CA30
		private void OnFocusing(object sender, EventArgs e)
		{
			HtmlElementEventHandler htmlElementEventHandler = (HtmlElementEventHandler)this.Events[HtmlElement.FocusingEvent];
			if (htmlElementEventHandler != null)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs();
				htmlElementEventHandler(this, htmlElementEventArgs);
			}
		}

		// Token: 0x06001CFA RID: 7418 RVA: 0x0006E868 File Offset: 0x0006CA68
		private void OnGotFocus(object sender, EventArgs e)
		{
			HtmlElementEventHandler htmlElementEventHandler = (HtmlElementEventHandler)this.Events[HtmlElement.GotFocusEvent];
			if (htmlElementEventHandler != null)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs();
				htmlElementEventHandler(this, htmlElementEventArgs);
			}
		}

		// Token: 0x06001CFB RID: 7419 RVA: 0x0006E8A0 File Offset: 0x0006CAA0
		private void OnLosingFocus(object sender, EventArgs e)
		{
			HtmlElementEventHandler htmlElementEventHandler = (HtmlElementEventHandler)this.Events[HtmlElement.LosingFocusEvent];
			if (htmlElementEventHandler != null)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs();
				htmlElementEventHandler(this, htmlElementEventArgs);
			}
		}

		// Token: 0x06001CFC RID: 7420 RVA: 0x0006E8D8 File Offset: 0x0006CAD8
		private void OnLostFocus(object sender, EventArgs e)
		{
			HtmlElementEventHandler htmlElementEventHandler = (HtmlElementEventHandler)this.Events[HtmlElement.LostFocusEvent];
			if (htmlElementEventHandler != null)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs();
				htmlElementEventHandler(this, htmlElementEventArgs);
			}
		}

		/// <summary>Compares two elements for equality.</summary>
		/// <returns>true if both parameters are null, or if both elements have the same underlying COM interface; otherwise, false.</returns>
		/// <param name="left">The first <see cref="T:System.Windows.Forms.HtmlElement" />.</param>
		/// <param name="right">The second <see cref="T:System.Windows.Forms.HtmlElement" />.</param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06001CFD RID: 7421 RVA: 0x0006E910 File Offset: 0x0006CB10
		public static bool operator ==(HtmlElement left, HtmlElement right)
		{
			return left == right || (left != null && right != null && left.element.Equals(right.element));
		}

		/// <summary>Compares two <see cref="T:System.Windows.Forms.HtmlElement" /> objects for inequality.</summary>
		/// <returns>true is only one element is null, or the two objects are not equal; otherwise, false. </returns>
		/// <param name="left">The first <see cref="T:System.Windows.Forms.HtmlElement" />.</param>
		/// <param name="right">The second <see cref="T:System.Windows.Forms.HtmlElement" />.</param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06001CFE RID: 7422 RVA: 0x0006E948 File Offset: 0x0006CB48
		public static bool operator !=(HtmlElement left, HtmlElement right)
		{
			return !(left == right);
		}

		// Token: 0x04000F53 RID: 3923
		private EventHandlerList events;

		// Token: 0x04000F54 RID: 3924
		private IWebBrowser webHost;

		// Token: 0x04000F55 RID: 3925
		internal IElement element;

		// Token: 0x04000F56 RID: 3926
		private WebBrowser owner;
	}
}
