using System;
using System.ComponentModel;
using Mono.WebBrowser;
using Mono.WebBrowser.DOM;

namespace Mono.Mozilla.DOM
{
	// Token: 0x0200013F RID: 319
	internal class Node : DOMObject, INode
	{
		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060009C0 RID: 2496 RVA: 0x00008666 File Offset: 0x00006866
		// (set) Token: 0x060009C1 RID: 2497 RVA: 0x00008670 File Offset: 0x00006870
		internal nsIDOMNode node
		{
			get
			{
				return this._node;
			}
			set
			{
				this.hashcode = value.GetHashCode();
				this.nodeNoProxy = this._node;
				if (!(value is nsIDOMHTMLDocument) && this.control.platform != this.control.enginePlatform)
				{
					this._node = nsDOMNode.GetProxy(this.control, value);
					return;
				}
				this._node = value;
			}
		}

		// Token: 0x060009C2 RID: 2498 RVA: 0x000086CF File Offset: 0x000068CF
		public Node(WebBrowser control, nsIDOMNode domNode)
			: base(control)
		{
			this.control = control;
			this.node = domNode;
		}

		// Token: 0x060009C3 RID: 2499 RVA: 0x000086E6 File Offset: 0x000068E6
		protected override void Dispose(bool disposing)
		{
			if (!this.disposed && disposing)
			{
				this.resources.Clear();
				this.node = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060009C4 RID: 2500 RVA: 0x0000870C File Offset: 0x0000690C
		internal virtual nsIDOMNode XPComObject
		{
			get
			{
				return this.node;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060009C5 RID: 2501 RVA: 0x00008714 File Offset: 0x00006914
		public virtual IAttributeCollection Attributes
		{
			get
			{
				if (!this.resources.Contains("Attributes"))
				{
					nsIDOMNamedNodeMap nsIDOMNamedNodeMap;
					this.node.getAttributes(out nsIDOMNamedNodeMap);
					if (nsIDOMNamedNodeMap == null)
					{
						return new AttributeCollection(this.control);
					}
					this.resources.Add("Attributes", new AttributeCollection(this.control, nsIDOMNamedNodeMap));
				}
				return this.resources["Attributes"] as IAttributeCollection;
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060009C6 RID: 2502 RVA: 0x00008784 File Offset: 0x00006984
		public virtual INodeList ChildNodes
		{
			get
			{
				if (!this.resources.Contains("ChildNodes"))
				{
					nsIDOMNodeList nsIDOMNodeList;
					this.node.getChildNodes(out nsIDOMNodeList);
					this.resources.Add("ChildNodes", new NodeList(this.control, nsIDOMNodeList));
				}
				return this.resources["ChildNodes"] as INodeList;
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060009C7 RID: 2503 RVA: 0x000087E4 File Offset: 0x000069E4
		public virtual INode FirstChild
		{
			get
			{
				if (!this.resources.Contains("FirstChild"))
				{
					nsIDOMNode nsIDOMNode;
					this.node.getFirstChild(out nsIDOMNode);
					this.resources.Add("FirstChild", base.GetTypedNode(nsIDOMNode));
				}
				return this.resources["FirstChild"] as INode;
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060009C8 RID: 2504 RVA: 0x00008840 File Offset: 0x00006A40
		public virtual INode LastChild
		{
			get
			{
				if (!this.resources.Contains("LastChild"))
				{
					nsIDOMNode nsIDOMNode;
					this.node.getLastChild(out nsIDOMNode);
					this.resources.Add("LastChild", base.GetTypedNode(nsIDOMNode));
				}
				return this.resources["LastChild"] as INode;
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060009C9 RID: 2505 RVA: 0x0000889C File Offset: 0x00006A9C
		public virtual INode Parent
		{
			get
			{
				if (!this.resources.Contains("Parent"))
				{
					nsIDOMNode nsIDOMNode;
					this.node.getParentNode(out nsIDOMNode);
					this.resources.Add("Parent", base.GetTypedNode(nsIDOMNode));
				}
				return this.resources["Parent"] as INode;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060009CA RID: 2506 RVA: 0x000088F8 File Offset: 0x00006AF8
		public virtual INode Previous
		{
			get
			{
				if (!this.resources.Contains("Previous"))
				{
					nsIDOMNode nsIDOMNode;
					this.node.getPreviousSibling(out nsIDOMNode);
					this.resources.Add("Previous", base.GetTypedNode(nsIDOMNode));
				}
				return this.resources["Previous"] as INode;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060009CB RID: 2507 RVA: 0x00008954 File Offset: 0x00006B54
		public virtual INode Next
		{
			get
			{
				if (!this.resources.Contains("Next"))
				{
					nsIDOMNode nsIDOMNode;
					this.node.getNextSibling(out nsIDOMNode);
					this.resources.Add("Next", base.GetTypedNode(nsIDOMNode));
				}
				return this.resources["Next"] as INode;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060009CC RID: 2508 RVA: 0x000089AD File Offset: 0x00006BAD
		public virtual string LocalName
		{
			get
			{
				this.node.getLocalName(this.storage);
				return Base.StringGet(this.storage);
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060009CD RID: 2509 RVA: 0x000089CC File Offset: 0x00006BCC
		public IDocument Owner
		{
			get
			{
				nsIDOMDocument nsIDOMDocument;
				this.node.getOwnerDocument(out nsIDOMDocument);
				if (!this.control.documents.ContainsKey(nsIDOMDocument.GetHashCode()))
				{
					this.control.documents.Add(nsIDOMDocument.GetHashCode(), new Document(this.control, nsIDOMDocument as nsIDOMHTMLDocument));
				}
				return this.control.documents[nsIDOMDocument.GetHashCode()] as IDocument;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060009CE RID: 2510 RVA: 0x00008A50 File Offset: 0x00006C50
		// (set) Token: 0x060009CF RID: 2511 RVA: 0x00008AD4 File Offset: 0x00006CD4
		public string Style
		{
			get
			{
				nsIDOMDocument nsIDOMDocument;
				this.node.getOwnerDocument(out nsIDOMDocument);
				nsIDOMAbstractView nsIDOMAbstractView;
				((nsIDOMDocumentView)nsIDOMDocument).getDefaultView(out nsIDOMAbstractView);
				nsIDOMViewCSS nsIDOMViewCSS = (nsIDOMViewCSS)nsIDOMAbstractView;
				Base.StringSet(this.storage, string.Empty);
				AsciiString asciiString = new AsciiString(string.Empty);
				nsIDOMCSSStyleDeclaration nsIDOMCSSStyleDeclaration;
				nsIDOMViewCSS.getComputedStyle(this.node as nsIDOMElement, asciiString.Handle, out nsIDOMCSSStyleDeclaration);
				if (nsIDOMCSSStyleDeclaration == null)
				{
					return "";
				}
				nsIDOMCSSStyleDeclaration.getCssText(this.storage);
				return Base.StringGet(this.storage);
			}
			set
			{
				nsIDOMDocument nsIDOMDocument;
				this.node.getOwnerDocument(out nsIDOMDocument);
				nsIDOMAbstractView nsIDOMAbstractView;
				((nsIDOMDocumentView)nsIDOMDocument).getDefaultView(out nsIDOMAbstractView);
				nsIDOMViewCSS nsIDOMViewCSS = (nsIDOMViewCSS)nsIDOMAbstractView;
				Base.StringSet(this.storage, string.Empty);
				nsIDOMCSSStyleDeclaration nsIDOMCSSStyleDeclaration;
				nsIDOMViewCSS.getComputedStyle(this.node as nsIDOMElement, this.storage, out nsIDOMCSSStyleDeclaration);
				Base.StringSet(this.storage, value);
				nsIDOMCSSStyleDeclaration.setCssText(this.storage);
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060009D0 RID: 2512 RVA: 0x00008B48 File Offset: 0x00006D48
		public virtual NodeType Type
		{
			get
			{
				ushort num;
				this.node.getNodeType(out num);
				return (NodeType)Enum.ToObject(typeof(NodeType), num);
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060009D1 RID: 2513 RVA: 0x00008B78 File Offset: 0x00006D78
		// (set) Token: 0x060009D2 RID: 2514 RVA: 0x00008B97 File Offset: 0x00006D97
		public virtual string Value
		{
			get
			{
				this.node.getNodeValue(this.storage);
				return Base.StringGet(this.storage);
			}
			set
			{
				Base.StringSet(this.storage, value);
				this.node.setNodeValue(this.storage);
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060009D3 RID: 2515 RVA: 0x00008BB8 File Offset: 0x00006DB8
		public virtual IntPtr AccessibleObject
		{
			get
			{
				nsIAccessible nsIAccessible = null;
				try
				{
					nsIAccessibilityService accessibilityService = this.control.AccessibilityService;
					nsIDOMDocument nsIDOMDocument;
					this.node.getOwnerDocument(out nsIDOMDocument);
					accessibilityService.getAccessibleFor(nsIDOMDocument, out nsIAccessible);
				}
				catch (Mono.WebBrowser.Exception ex)
				{
					Console.Error.WriteLine(ex.Message);
					goto IL_0063;
				}
				catch (global::System.Exception ex2)
				{
					Console.Error.WriteLine(ex2.Message);
					goto IL_0063;
				}
				if (nsIAccessible != null)
				{
					IntPtr zero = IntPtr.Zero;
					if (nsIAccessible.getNativeInterface(out zero) == 0)
					{
						return zero;
					}
				}
				IL_0063:
				Console.Error.WriteLine("Accessibility not available");
				return IntPtr.Zero;
			}
		}

		// Token: 0x060009D4 RID: 2516 RVA: 0x00008C58 File Offset: 0x00006E58
		public virtual void FireEvent(string eventName)
		{
			nsIDOMDocument nsIDOMDocument;
			this.node.getOwnerDocument(out nsIDOMDocument);
			nsIDOMDocumentEvent nsIDOMDocumentEvent = (nsIDOMDocumentEvent)nsIDOMDocument;
			nsIDOMAbstractView nsIDOMAbstractView;
			((nsIDOMDocumentView)nsIDOMDocument).getDefaultView(out nsIDOMAbstractView);
			nsIDOMEventTarget nsIDOMEventTarget = (nsIDOMEventTarget)this.node;
			bool flag = false;
			uint num = <PrivateImplementationDetails>.ComputeStringHash(eventName);
			if (num <= 1943722471U)
			{
				if (num <= 728389842U)
				{
					if (num <= 563185489U)
					{
						if (num != 297952813U)
						{
							if (num != 337658899U)
							{
								if (num != 563185489U)
								{
									goto IL_0528;
								}
								if (!(eventName == "error"))
								{
									goto IL_0528;
								}
								goto IL_0528;
							}
							else
							{
								if (!(eventName == "focus"))
								{
									goto IL_0528;
								}
								goto IL_0528;
							}
						}
						else
						{
							if (!(eventName == "select"))
							{
								goto IL_0528;
							}
							goto IL_0528;
						}
					}
					else if (num != 597938448U)
					{
						if (num != 679260337U)
						{
							if (num != 728389842U)
							{
								goto IL_0528;
							}
							if (!(eventName == "unload"))
							{
								goto IL_0528;
							}
							goto IL_0528;
						}
						else
						{
							if (!(eventName == "keyup"))
							{
								goto IL_0528;
							}
							goto IL_0477;
						}
					}
					else if (!(eventName == "mousedown"))
					{
						goto IL_0528;
					}
				}
				else if (num <= 1628281175U)
				{
					if (num != 1311441080U)
					{
						if (num != 1551804527U)
						{
							if (num != 1628281175U)
							{
								goto IL_0528;
							}
							if (!(eventName == "DOMFocusOut"))
							{
								goto IL_0528;
							}
							goto IL_04D2;
						}
						else if (!(eventName == "click"))
						{
							goto IL_0528;
						}
					}
					else
					{
						if (!(eventName == "keydown"))
						{
							goto IL_0528;
						}
						goto IL_0477;
					}
				}
				else if (num <= 1811665304U)
				{
					if (num != 1695364032U)
					{
						if (num != 1811665304U)
						{
							goto IL_0528;
						}
						if (!(eventName == "blur"))
						{
							goto IL_0528;
						}
						goto IL_0528;
					}
					else
					{
						if (!(eventName == "reset"))
						{
							goto IL_0528;
						}
						goto IL_0528;
					}
				}
				else if (num != 1922892221U)
				{
					if (num != 1943722471U)
					{
						goto IL_0528;
					}
					if (!(eventName == "dblclick"))
					{
						goto IL_0528;
					}
				}
				else
				{
					if (!(eventName == "change"))
					{
						goto IL_0528;
					}
					goto IL_0528;
				}
			}
			else if (num <= 3642186483U)
			{
				if (num <= 2958779366U)
				{
					if (num != 2035911400U)
					{
						if (num != 2771110649U)
						{
							if (num != 2958779366U)
							{
								goto IL_0528;
							}
							if (!(eventName == "DOMActivate"))
							{
								goto IL_0528;
							}
							goto IL_04D2;
						}
						else
						{
							if (!(eventName == "abort"))
							{
								goto IL_0528;
							}
							goto IL_0528;
						}
					}
					else if (!(eventName == "mouseout"))
					{
						goto IL_0528;
					}
				}
				else if (num != 3447633555U)
				{
					if (num != 3611423958U)
					{
						if (num != 3642186483U)
						{
							goto IL_0528;
						}
						if (!(eventName == "keypress"))
						{
							goto IL_0528;
						}
						goto IL_0477;
					}
					else if (!(eventName == "mouseover"))
					{
						goto IL_0528;
					}
				}
				else if (!(eventName == "contextmenu"))
				{
					goto IL_0528;
				}
			}
			else if (num <= 3923426769U)
			{
				if (num != 3859241449U)
				{
					if (num != 3909063190U)
					{
						if (num != 3923426769U)
						{
							goto IL_0528;
						}
						if (!(eventName == "mousemove"))
						{
							goto IL_0528;
						}
					}
					else
					{
						if (!(eventName == "DOMFocusIn"))
						{
							goto IL_0528;
						}
						goto IL_04D2;
					}
				}
				else
				{
					if (!(eventName == "load"))
					{
						goto IL_0528;
					}
					goto IL_0528;
				}
			}
			else if (num <= 4118085681U)
			{
				if (num != 4035692073U)
				{
					if (num != 4118085681U)
					{
						goto IL_0528;
					}
					if (!(eventName == "beforeunload"))
					{
						goto IL_0528;
					}
					goto IL_0528;
				}
				else if (!(eventName == "mouseup"))
				{
					goto IL_0528;
				}
			}
			else if (num != 4159373861U)
			{
				if (num != 4191711099U)
				{
					goto IL_0528;
				}
				if (!(eventName == "input"))
				{
					goto IL_0528;
				}
				goto IL_04D2;
			}
			else
			{
				if (!(eventName == "submit"))
				{
					goto IL_0528;
				}
				goto IL_0528;
			}
			string text = "mouseevents";
			Base.StringSet(this.storage, text);
			nsIDOMEvent nsIDOMEvent;
			nsIDOMDocumentEvent.createEvent(this.storage, out nsIDOMEvent);
			nsIDOMMouseEvent nsIDOMMouseEvent = nsIDOMEvent as nsIDOMMouseEvent;
			Base.StringSet(this.storage, eventName);
			nsIDOMMouseEvent.initMouseEvent(this.storage, true, true, nsIDOMAbstractView, 1, 0, 0, 0, 0, false, false, false, false, 0, nsIDOMEventTarget);
			nsIDOMEventTarget.dispatchEvent(nsIDOMMouseEvent, out flag);
			return;
			IL_0477:
			text = "keyevents";
			Base.StringSet(this.storage, text);
			nsIDOMEvent nsIDOMEvent2;
			nsIDOMDocumentEvent.createEvent(this.storage, out nsIDOMEvent2);
			Base.StringSet(this.storage, eventName);
			nsIDOMKeyEvent nsIDOMKeyEvent = nsIDOMEvent2 as nsIDOMKeyEvent;
			nsIDOMKeyEvent.initKeyEvent(this.storage, true, true, nsIDOMAbstractView, false, false, false, false, 0U, 0U);
			nsIDOMEventTarget.dispatchEvent(nsIDOMKeyEvent, out flag);
			return;
			IL_04D2:
			text = "uievents";
			Base.StringSet(this.storage, text);
			nsIDOMEvent nsIDOMEvent3;
			nsIDOMDocumentEvent.createEvent(this.storage, out nsIDOMEvent3);
			Base.StringSet(this.storage, eventName);
			nsIDOMUIEvent nsIDOMUIEvent = nsIDOMEvent3 as nsIDOMUIEvent;
			nsIDOMUIEvent.initUIEvent(this.storage, true, true, nsIDOMAbstractView, 1);
			nsIDOMEventTarget.dispatchEvent(nsIDOMUIEvent, out flag);
			return;
			IL_0528:
			text = "events";
			Base.StringSet(this.storage, text);
			nsIDOMEvent nsIDOMEvent4;
			nsIDOMDocumentEvent.createEvent(this.storage, out nsIDOMEvent4);
			Base.StringSet(this.storage, eventName);
			nsIDOMEvent4.initEvent(this.storage, true, true);
			nsIDOMEventTarget.dispatchEvent(nsIDOMEvent4, out flag);
		}

		// Token: 0x060009D5 RID: 2517 RVA: 0x000091D8 File Offset: 0x000073D8
		public virtual INode InsertBefore(INode child, INode refChild)
		{
			nsIDOMNode nsIDOMNode;
			this.node.insertBefore(((Node)child).node, ((Node)refChild).node, out nsIDOMNode);
			return child;
		}

		// Token: 0x060009D6 RID: 2518 RVA: 0x0000920C File Offset: 0x0000740C
		public virtual INode ReplaceChild(INode child, INode oldChild)
		{
			nsIDOMNode nsIDOMNode;
			this.node.replaceChild(((Node)child).node, ((Node)oldChild).node, out nsIDOMNode);
			return oldChild;
		}

		// Token: 0x060009D7 RID: 2519 RVA: 0x00009240 File Offset: 0x00007440
		public virtual INode RemoveChild(INode child)
		{
			nsIDOMNode nsIDOMNode;
			this.node.removeChild(((Node)child).node, out nsIDOMNode);
			return child;
		}

		// Token: 0x060009D8 RID: 2520 RVA: 0x00009268 File Offset: 0x00007468
		public virtual INode AppendChild(INode child)
		{
			nsIDOMNode nsIDOMNode;
			int num = this.node.appendChild(((Node)child).node, out nsIDOMNode);
			Console.Error.WriteLine(num);
			return child;
		}

		// Token: 0x060009D9 RID: 2521 RVA: 0x0000929A File Offset: 0x0000749A
		public override bool Equals(object obj)
		{
			return this == obj as Node;
		}

		// Token: 0x060009DA RID: 2522 RVA: 0x000092A8 File Offset: 0x000074A8
		public static bool operator ==(Node left, Node right)
		{
			return left == right || (left != null && right != null && left.hashcode == right.hashcode);
		}

		// Token: 0x060009DB RID: 2523 RVA: 0x000092C6 File Offset: 0x000074C6
		public static bool operator !=(Node left, Node right)
		{
			return !(left == right);
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x000092D2 File Offset: 0x000074D2
		public override int GetHashCode()
		{
			return this.hashcode;
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060009DD RID: 2525 RVA: 0x000092DA File Offset: 0x000074DA
		private EventListener EventListener
		{
			get
			{
				if (this.eventListener == null)
				{
					this.eventListener = new EventListener(this.node as nsIDOMEventTarget, this);
				}
				return this.eventListener;
			}
		}

		// Token: 0x060009DE RID: 2526 RVA: 0x00009301 File Offset: 0x00007501
		public void AttachEventHandler(string eventName, EventHandler handler)
		{
			this.EventListener.AddHandler(handler, eventName);
		}

		// Token: 0x060009DF RID: 2527 RVA: 0x00009310 File Offset: 0x00007510
		public void DetachEventHandler(string eventName, EventHandler handler)
		{
			this.EventListener.RemoveHandler(handler, eventName);
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060009E0 RID: 2528 RVA: 0x0000931F File Offset: 0x0000751F
		public new EventHandlerList Events
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

		// Token: 0x060009E1 RID: 2529 RVA: 0x0000933C File Offset: 0x0000753C
		public void AttachEventHandler(string eventName, Delegate handler)
		{
			string text = string.Intern(this.node.GetHashCode() + ":" + eventName);
			this.Events.AddHandler(text, handler);
		}

		// Token: 0x060009E2 RID: 2530 RVA: 0x00009378 File Offset: 0x00007578
		public void DetachEventHandler(string eventName, Delegate handler)
		{
			string text = string.Intern(this.node.GetHashCode() + ":" + eventName);
			this.Events.RemoveHandler(text, handler);
		}

		// Token: 0x14000044 RID: 68
		// (add) Token: 0x060009E3 RID: 2531 RVA: 0x000093B3 File Offset: 0x000075B3
		// (remove) Token: 0x060009E4 RID: 2532 RVA: 0x000093C6 File Offset: 0x000075C6
		public event NodeEventHandler Click
		{
			add
			{
				this.EventListener.AddHandler(value, "click");
			}
			remove
			{
				this.EventListener.RemoveHandler(value, "click");
			}
		}

		// Token: 0x14000045 RID: 69
		// (add) Token: 0x060009E5 RID: 2533 RVA: 0x000093D9 File Offset: 0x000075D9
		// (remove) Token: 0x060009E6 RID: 2534 RVA: 0x000093EC File Offset: 0x000075EC
		public event NodeEventHandler DoubleClick
		{
			add
			{
				this.EventListener.AddHandler(value, "dblclick");
			}
			remove
			{
				this.EventListener.RemoveHandler(value, "dblclick");
			}
		}

		// Token: 0x14000046 RID: 70
		// (add) Token: 0x060009E7 RID: 2535 RVA: 0x000093FF File Offset: 0x000075FF
		// (remove) Token: 0x060009E8 RID: 2536 RVA: 0x00009412 File Offset: 0x00007612
		public event NodeEventHandler KeyDown
		{
			add
			{
				this.EventListener.AddHandler(value, "keydown");
			}
			remove
			{
				this.EventListener.RemoveHandler(value, "keydown");
			}
		}

		// Token: 0x14000047 RID: 71
		// (add) Token: 0x060009E9 RID: 2537 RVA: 0x00009425 File Offset: 0x00007625
		// (remove) Token: 0x060009EA RID: 2538 RVA: 0x00009438 File Offset: 0x00007638
		public event NodeEventHandler KeyPress
		{
			add
			{
				this.EventListener.AddHandler(value, "keypress");
			}
			remove
			{
				this.EventListener.RemoveHandler(value, "keypress");
			}
		}

		// Token: 0x14000048 RID: 72
		// (add) Token: 0x060009EB RID: 2539 RVA: 0x0000944B File Offset: 0x0000764B
		// (remove) Token: 0x060009EC RID: 2540 RVA: 0x0000945E File Offset: 0x0000765E
		public event NodeEventHandler KeyUp
		{
			add
			{
				this.EventListener.AddHandler(value, "keyup");
			}
			remove
			{
				this.EventListener.RemoveHandler(value, "keyup");
			}
		}

		// Token: 0x14000049 RID: 73
		// (add) Token: 0x060009ED RID: 2541 RVA: 0x00009471 File Offset: 0x00007671
		// (remove) Token: 0x060009EE RID: 2542 RVA: 0x00009484 File Offset: 0x00007684
		public event NodeEventHandler MouseDown
		{
			add
			{
				this.EventListener.AddHandler(value, "mousedown");
			}
			remove
			{
				this.EventListener.RemoveHandler(value, "mousedown");
			}
		}

		// Token: 0x1400004A RID: 74
		// (add) Token: 0x060009EF RID: 2543 RVA: 0x00009497 File Offset: 0x00007697
		// (remove) Token: 0x060009F0 RID: 2544 RVA: 0x000094AA File Offset: 0x000076AA
		public event NodeEventHandler MouseEnter
		{
			add
			{
				this.EventListener.AddHandler(value, "mouseenter");
			}
			remove
			{
				this.EventListener.RemoveHandler(value, "mouseenter");
			}
		}

		// Token: 0x1400004B RID: 75
		// (add) Token: 0x060009F1 RID: 2545 RVA: 0x000094BD File Offset: 0x000076BD
		// (remove) Token: 0x060009F2 RID: 2546 RVA: 0x000094D0 File Offset: 0x000076D0
		public event NodeEventHandler MouseLeave
		{
			add
			{
				this.EventListener.AddHandler(value, "mouseout");
			}
			remove
			{
				this.EventListener.RemoveHandler(value, "mouseout");
			}
		}

		// Token: 0x1400004C RID: 76
		// (add) Token: 0x060009F3 RID: 2547 RVA: 0x000094E3 File Offset: 0x000076E3
		// (remove) Token: 0x060009F4 RID: 2548 RVA: 0x000094F6 File Offset: 0x000076F6
		public event NodeEventHandler MouseMove
		{
			add
			{
				this.EventListener.AddHandler(value, "mousemove");
			}
			remove
			{
				this.EventListener.RemoveHandler(value, "mousemove");
			}
		}

		// Token: 0x1400004D RID: 77
		// (add) Token: 0x060009F5 RID: 2549 RVA: 0x00009509 File Offset: 0x00007709
		// (remove) Token: 0x060009F6 RID: 2550 RVA: 0x0000951C File Offset: 0x0000771C
		public event NodeEventHandler MouseOver
		{
			add
			{
				this.EventListener.AddHandler(value, "mouseover");
			}
			remove
			{
				this.EventListener.RemoveHandler(value, "mouseover");
			}
		}

		// Token: 0x1400004E RID: 78
		// (add) Token: 0x060009F7 RID: 2551 RVA: 0x0000952F File Offset: 0x0000772F
		// (remove) Token: 0x060009F8 RID: 2552 RVA: 0x00009542 File Offset: 0x00007742
		public event NodeEventHandler MouseUp
		{
			add
			{
				this.EventListener.AddHandler(value, "mouseup");
			}
			remove
			{
				this.EventListener.RemoveHandler(value, "mouseup");
			}
		}

		// Token: 0x1400004F RID: 79
		// (add) Token: 0x060009F9 RID: 2553 RVA: 0x00009555 File Offset: 0x00007755
		// (remove) Token: 0x060009FA RID: 2554 RVA: 0x00009568 File Offset: 0x00007768
		public event NodeEventHandler OnFocus
		{
			add
			{
				this.EventListener.AddHandler(value, "focus");
			}
			remove
			{
				this.EventListener.RemoveHandler(value, "focus");
			}
		}

		// Token: 0x14000050 RID: 80
		// (add) Token: 0x060009FB RID: 2555 RVA: 0x0000957B File Offset: 0x0000777B
		// (remove) Token: 0x060009FC RID: 2556 RVA: 0x0000958E File Offset: 0x0000778E
		public event NodeEventHandler OnBlur
		{
			add
			{
				this.EventListener.AddHandler(value, "blur");
			}
			remove
			{
				this.EventListener.RemoveHandler(value, "blur");
			}
		}

		// Token: 0x04000124 RID: 292
		internal nsIDOMNode nodeNoProxy;

		// Token: 0x04000125 RID: 293
		private nsIDOMNode _node;

		// Token: 0x04000126 RID: 294
		protected int hashcode;

		// Token: 0x04000127 RID: 295
		private EventListener eventListener;

		// Token: 0x04000128 RID: 296
		private new WebBrowser control;

		// Token: 0x04000129 RID: 297
		private EventHandlerList events;
	}
}
