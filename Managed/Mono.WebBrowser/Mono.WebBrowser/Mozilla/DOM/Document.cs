using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;
using Mono.WebBrowser.DOM;

namespace Mono.Mozilla.DOM
{
	// Token: 0x02000136 RID: 310
	internal class Document : Node, IDocument, INode
	{
		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000913 RID: 2323 RVA: 0x00006263 File Offset: 0x00004463
		// (set) Token: 0x06000914 RID: 2324 RVA: 0x00006270 File Offset: 0x00004470
		internal new nsIDOMDocument node
		{
			get
			{
				return base.node as nsIDOMDocument;
			}
			set
			{
				base.node = value;
			}
		}

		// Token: 0x06000915 RID: 2325 RVA: 0x00006279 File Offset: 0x00004479
		public Document(WebBrowser control, nsIDOMHTMLDocument document)
			: base(control, document)
		{
			if (control.platform != control.enginePlatform)
			{
				this.node = nsDOMHTMLDocument.GetProxy(control, document);
				return;
			}
			this.node = document;
		}

		// Token: 0x06000916 RID: 2326 RVA: 0x000062A6 File Offset: 0x000044A6
		public Document(WebBrowser control, nsIDOMDocument document)
			: base(control, document)
		{
			if (control.platform != control.enginePlatform)
			{
				this.node = nsDOMDocument.GetProxy(control, document);
				return;
			}
			this.node = document;
		}

		// Token: 0x06000917 RID: 2327 RVA: 0x000062D3 File Offset: 0x000044D3
		protected override void Dispose(bool disposing)
		{
			if (!this.disposed && disposing)
			{
				this.resources.Clear();
				this.node = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000918 RID: 2328 RVA: 0x000062F9 File Offset: 0x000044F9
		internal new nsIDOMDocument XPComObject
		{
			get
			{
				return this.node;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000919 RID: 2329 RVA: 0x00006304 File Offset: 0x00004504
		public IElement Active
		{
			get
			{
				nsIWebBrowserFocus nsIWebBrowserFocus = (nsIWebBrowserFocus)this.control.navigation.navigation;
				if (nsIWebBrowserFocus == null)
				{
					return null;
				}
				nsIDOMElement nsIDOMElement;
				nsIWebBrowserFocus.getFocusedElement(out nsIDOMElement);
				return (IElement)base.GetTypedNode(nsIDOMElement);
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x0600091A RID: 2330 RVA: 0x00006344 File Offset: 0x00004544
		// (set) Token: 0x0600091B RID: 2331 RVA: 0x00006394 File Offset: 0x00004594
		public string ActiveLinkColor
		{
			get
			{
				if (!(this.node is nsIDOMHTMLDocument))
				{
					return string.Empty;
				}
				nsIDOMHTMLElement nsIDOMHTMLElement;
				((nsIDOMHTMLDocument)this.node).getBody(out nsIDOMHTMLElement);
				((nsIDOMHTMLBodyElement)nsIDOMHTMLElement).getALink(this.storage);
				return Base.StringGet(this.storage);
			}
			set
			{
				if (!(this.node is nsIDOMHTMLDocument))
				{
					return;
				}
				Base.StringSet(this.storage, value);
				nsIDOMHTMLElement nsIDOMHTMLElement;
				((nsIDOMHTMLDocument)this.node).getBody(out nsIDOMHTMLElement);
				((nsIDOMHTMLBodyElement)nsIDOMHTMLElement).setALink(this.storage);
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x0600091C RID: 2332 RVA: 0x000063E0 File Offset: 0x000045E0
		public IElementCollection Anchors
		{
			get
			{
				if (!(this.node is nsIDOMHTMLDocument))
				{
					return null;
				}
				nsIDOMHTMLCollection nsIDOMHTMLCollection;
				((nsIDOMHTMLDocument)this.node).getAnchors(out nsIDOMHTMLCollection);
				return new HTMLElementCollection(this.control, (nsIDOMNodeList)nsIDOMHTMLCollection);
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x0600091D RID: 2333 RVA: 0x00006420 File Offset: 0x00004620
		public IElementCollection Applets
		{
			get
			{
				if (!(this.node is nsIDOMHTMLDocument))
				{
					return null;
				}
				nsIDOMHTMLCollection nsIDOMHTMLCollection;
				((nsIDOMHTMLDocument)this.node).getApplets(out nsIDOMHTMLCollection);
				return new HTMLElementCollection(this.control, (nsIDOMNodeList)nsIDOMHTMLCollection);
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x0600091E RID: 2334 RVA: 0x00006460 File Offset: 0x00004660
		// (set) Token: 0x0600091F RID: 2335 RVA: 0x000064B0 File Offset: 0x000046B0
		public string Background
		{
			get
			{
				if (!(this.node is nsIDOMHTMLDocument))
				{
					return string.Empty;
				}
				nsIDOMHTMLElement nsIDOMHTMLElement;
				((nsIDOMHTMLDocument)this.node).getBody(out nsIDOMHTMLElement);
				((nsIDOMHTMLBodyElement)nsIDOMHTMLElement).getBackground(this.storage);
				return Base.StringGet(this.storage);
			}
			set
			{
				if (!(this.node is nsIDOMHTMLDocument))
				{
					return;
				}
				Base.StringSet(this.storage, value);
				nsIDOMHTMLElement nsIDOMHTMLElement;
				((nsIDOMHTMLDocument)this.node).getBody(out nsIDOMHTMLElement);
				((nsIDOMHTMLBodyElement)nsIDOMHTMLElement).setBackground(this.storage);
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000920 RID: 2336 RVA: 0x000064FC File Offset: 0x000046FC
		// (set) Token: 0x06000921 RID: 2337 RVA: 0x0000654C File Offset: 0x0000474C
		public string BackColor
		{
			get
			{
				if (!(this.node is nsIDOMHTMLDocument))
				{
					return string.Empty;
				}
				nsIDOMHTMLElement nsIDOMHTMLElement;
				((nsIDOMHTMLDocument)this.node).getBody(out nsIDOMHTMLElement);
				((nsIDOMHTMLBodyElement)nsIDOMHTMLElement).getBgColor(this.storage);
				return Base.StringGet(this.storage);
			}
			set
			{
				if (!(this.node is nsIDOMHTMLDocument))
				{
					return;
				}
				Base.StringSet(this.storage, value);
				nsIDOMHTMLElement nsIDOMHTMLElement;
				((nsIDOMHTMLDocument)this.node).getBody(out nsIDOMHTMLElement);
				((nsIDOMHTMLBodyElement)nsIDOMHTMLElement).setBgColor(this.storage);
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000922 RID: 2338 RVA: 0x00006598 File Offset: 0x00004798
		public IElement Body
		{
			get
			{
				if (!(this.node is nsIDOMHTMLDocument))
				{
					return null;
				}
				if (!this.resources.Contains("Body"))
				{
					nsIDOMHTMLElement nsIDOMHTMLElement;
					((nsIDOMHTMLDocument)this.node).getBody(out nsIDOMHTMLElement);
					this.resources.Add("Body", base.GetTypedNode(nsIDOMHTMLElement));
				}
				return this.resources["Body"] as IElement;
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000923 RID: 2339 RVA: 0x00006608 File Offset: 0x00004808
		// (set) Token: 0x06000924 RID: 2340 RVA: 0x00006670 File Offset: 0x00004870
		public string Charset
		{
			get
			{
				nsIDOMAbstractView nsIDOMAbstractView;
				((nsIDOMDocumentView)this.node).getDefaultView(out nsIDOMAbstractView);
				IntPtr intPtr;
				((nsIInterfaceRequestor)nsIDOMAbstractView).getInterface(typeof(nsIDocCharset).GUID, out intPtr);
				nsIDocCharset nsIDocCharset = (nsIDocCharset)Marshal.GetObjectForIUnknown(intPtr);
				IntPtr intPtr2 = Marshal.StringToHGlobalUni(new StringBuilder(30).ToString());
				nsIDocCharset.getCharset(ref intPtr2);
				return Marshal.PtrToStringAnsi(intPtr2);
			}
			set
			{
				nsIDOMAbstractView nsIDOMAbstractView;
				((nsIDOMDocumentView)this.node).getDefaultView(out nsIDOMAbstractView);
				IntPtr intPtr;
				((nsIInterfaceRequestor)nsIDOMAbstractView).getInterface(typeof(nsIDocCharset).GUID, out intPtr);
				((nsIDocCharset)Marshal.GetTypedObjectForIUnknown(intPtr, typeof(nsIDocCharset))).setCharset(value);
				this.control.navigation.Go(this.Url, LoadFlags.CharsetChange);
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000925 RID: 2341 RVA: 0x000066E4 File Offset: 0x000048E4
		// (set) Token: 0x06000926 RID: 2342 RVA: 0x0000671B File Offset: 0x0000491B
		public string Cookie
		{
			get
			{
				if (!(this.node is nsIDOMHTMLDocument))
				{
					return string.Empty;
				}
				((nsIDOMHTMLDocument)this.node).getCookie(this.storage);
				return Base.StringGet(this.storage);
			}
			set
			{
				if (!(this.node is nsIDOMHTMLDocument))
				{
					return;
				}
				Base.StringSet(this.storage, value);
				((nsIDOMHTMLDocument)this.node).setCookie(this.storage);
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000927 RID: 2343 RVA: 0x0000674E File Offset: 0x0000494E
		public string Domain
		{
			get
			{
				if (!(this.node is nsIDOMHTMLDocument))
				{
					return string.Empty;
				}
				((nsIDOMHTMLDocument)this.node).getDomain(this.storage);
				return Base.StringGet(this.storage);
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000928 RID: 2344 RVA: 0x00006788 File Offset: 0x00004988
		public IElement DocumentElement
		{
			get
			{
				if (!this.resources.Contains("DocumentElement"))
				{
					nsIDOMElement nsIDOMElement;
					this.node.getDocumentElement(out nsIDOMElement);
					this.resources.Add("DocumentElement", base.GetTypedNode(nsIDOMElement));
				}
				return this.resources["DocumentElement"] as IElement;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000929 RID: 2345 RVA: 0x000067E4 File Offset: 0x000049E4
		public IDocumentType DocType
		{
			get
			{
				nsIDOMDocumentType nsIDOMDocumentType;
				this.node.getDoctype(out nsIDOMDocumentType);
				return new DocumentType(this.control, nsIDOMDocumentType);
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x0600092A RID: 2346 RVA: 0x0000680C File Offset: 0x00004A0C
		// (set) Token: 0x0600092B RID: 2347 RVA: 0x0000685C File Offset: 0x00004A5C
		public string ForeColor
		{
			get
			{
				if (!(this.node is nsIDOMHTMLDocument))
				{
					return string.Empty;
				}
				nsIDOMHTMLElement nsIDOMHTMLElement;
				((nsIDOMHTMLDocument)this.node).getBody(out nsIDOMHTMLElement);
				((nsIDOMHTMLBodyElement)nsIDOMHTMLElement).getText(this.storage);
				return Base.StringGet(this.storage);
			}
			set
			{
				if (!(this.node is nsIDOMHTMLDocument))
				{
					return;
				}
				Base.StringSet(this.storage, value);
				nsIDOMHTMLElement nsIDOMHTMLElement;
				((nsIDOMHTMLDocument)this.node).getBody(out nsIDOMHTMLElement);
				((nsIDOMHTMLBodyElement)nsIDOMHTMLElement).setText(this.storage);
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x0600092C RID: 2348 RVA: 0x000068A8 File Offset: 0x00004AA8
		public IElementCollection Forms
		{
			get
			{
				if (!(this.node is nsIDOMHTMLDocument))
				{
					return null;
				}
				nsIDOMHTMLCollection nsIDOMHTMLCollection;
				((nsIDOMHTMLDocument)this.node).getForms(out nsIDOMHTMLCollection);
				return new HTMLElementCollection(this.control, (nsIDOMNodeList)nsIDOMHTMLCollection);
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600092D RID: 2349 RVA: 0x000068E8 File Offset: 0x00004AE8
		public IElementCollection Images
		{
			get
			{
				if (!(this.node is nsIDOMHTMLDocument))
				{
					return null;
				}
				nsIDOMHTMLCollection nsIDOMHTMLCollection;
				((nsIDOMHTMLDocument)this.node).getImages(out nsIDOMHTMLCollection);
				return new HTMLElementCollection(this.control, (nsIDOMNodeList)nsIDOMHTMLCollection);
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x0600092E RID: 2350 RVA: 0x00006928 File Offset: 0x00004B28
		public IDOMImplementation Implementation
		{
			get
			{
				nsIDOMDOMImplementation nsIDOMDOMImplementation;
				this.node.getImplementation(out nsIDOMDOMImplementation);
				return new DOMImplementation(this.control, nsIDOMDOMImplementation);
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x0600092F RID: 2351 RVA: 0x00006950 File Offset: 0x00004B50
		// (set) Token: 0x06000930 RID: 2352 RVA: 0x000069A0 File Offset: 0x00004BA0
		public string LinkColor
		{
			get
			{
				if (!(this.node is nsIDOMHTMLDocument))
				{
					return string.Empty;
				}
				nsIDOMHTMLElement nsIDOMHTMLElement;
				((nsIDOMHTMLDocument)this.node).getBody(out nsIDOMHTMLElement);
				((nsIDOMHTMLBodyElement)nsIDOMHTMLElement).getLink(this.storage);
				return Base.StringGet(this.storage);
			}
			set
			{
				if (!(this.node is nsIDOMHTMLDocument))
				{
					return;
				}
				Base.StringSet(this.storage, value);
				nsIDOMHTMLElement nsIDOMHTMLElement;
				((nsIDOMHTMLDocument)this.node).getBody(out nsIDOMHTMLElement);
				((nsIDOMHTMLBodyElement)nsIDOMHTMLElement).setLink(this.storage);
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000931 RID: 2353 RVA: 0x000069EC File Offset: 0x00004BEC
		public IElementCollection Links
		{
			get
			{
				if (!(this.node is nsIDOMHTMLDocument))
				{
					return null;
				}
				nsIDOMHTMLCollection nsIDOMHTMLCollection;
				((nsIDOMHTMLDocument)this.node).getLinks(out nsIDOMHTMLCollection);
				return new HTMLElementCollection(this.control, (nsIDOMNodeList)nsIDOMHTMLCollection);
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000932 RID: 2354 RVA: 0x00006A2C File Offset: 0x00004C2C
		public IStylesheetList Stylesheets
		{
			get
			{
				nsIDOMStyleSheetList nsIDOMStyleSheetList;
				((nsIDOMDocumentStyle)this.node).getStyleSheets(out nsIDOMStyleSheetList);
				return new StylesheetList(this.control, nsIDOMStyleSheetList);
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000933 RID: 2355 RVA: 0x00006A58 File Offset: 0x00004C58
		// (set) Token: 0x06000934 RID: 2356 RVA: 0x00006A8F File Offset: 0x00004C8F
		public string Title
		{
			get
			{
				if (!(this.node is nsIDOMHTMLDocument))
				{
					return string.Empty;
				}
				((nsIDOMHTMLDocument)this.node).getTitle(this.storage);
				return Base.StringGet(this.storage);
			}
			set
			{
				Base.StringSet(this.storage, value);
				((nsIDOMHTMLDocument)this.node).setTitle(this.storage);
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000935 RID: 2357 RVA: 0x00006AB4 File Offset: 0x00004CB4
		public string Url
		{
			get
			{
				if (!(this.node is nsIDOMHTMLDocument))
				{
					return string.Empty;
				}
				((nsIDOMHTMLDocument)this.node).getURL(this.storage);
				return Base.StringGet(this.storage);
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000936 RID: 2358 RVA: 0x00006AEC File Offset: 0x00004CEC
		// (set) Token: 0x06000937 RID: 2359 RVA: 0x00006B3C File Offset: 0x00004D3C
		public string VisitedLinkColor
		{
			get
			{
				if (!(this.node is nsIDOMHTMLDocument))
				{
					return string.Empty;
				}
				nsIDOMHTMLElement nsIDOMHTMLElement;
				((nsIDOMHTMLDocument)this.node).getBody(out nsIDOMHTMLElement);
				((nsIDOMHTMLBodyElement)nsIDOMHTMLElement).getVLink(this.storage);
				return Base.StringGet(this.storage);
			}
			set
			{
				if (!(this.node is nsIDOMHTMLDocument))
				{
					return;
				}
				Base.StringSet(this.storage, value);
				nsIDOMHTMLElement nsIDOMHTMLElement;
				((nsIDOMHTMLDocument)this.node).getBody(out nsIDOMHTMLElement);
				((nsIDOMHTMLBodyElement)nsIDOMHTMLElement).setVLink(this.storage);
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000938 RID: 2360 RVA: 0x00006B88 File Offset: 0x00004D88
		public IWindow Window
		{
			get
			{
				nsIDOMAbstractView nsIDOMAbstractView;
				((nsIDOMDocumentView)this.node).getDefaultView(out nsIDOMAbstractView);
				nsIInterfaceRequestor nsIInterfaceRequestor = (nsIInterfaceRequestor)nsIDOMAbstractView;
				if (nsIInterfaceRequestor == null)
				{
					return null;
				}
				IntPtr intPtr;
				nsIInterfaceRequestor.getInterface(typeof(nsIDOMWindow).GUID, out intPtr);
				nsIDOMWindow nsIDOMWindow = (nsIDOMWindow)Marshal.GetObjectForIUnknown(intPtr);
				return new Window(this.control, nsIDOMWindow);
			}
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x00006BE4 File Offset: 0x00004DE4
		public IAttribute CreateAttribute(string name)
		{
			Base.StringSet(this.storage, name);
			nsIDOMAttr nsIDOMAttr;
			this.node.createAttribute(this.storage, out nsIDOMAttr);
			return new Attribute(this.control, nsIDOMAttr);
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x00006C20 File Offset: 0x00004E20
		public IElement CreateElement(string tagName)
		{
			Base.StringSet(this.storage, tagName);
			nsIDOMElement nsIDOMElement;
			this.node.createElement(this.storage, out nsIDOMElement);
			if (this.node is nsIDOMHTMLDocument)
			{
				return new HTMLElement(this.control, (nsIDOMHTMLElement)nsIDOMElement);
			}
			return new Element(this.control, nsIDOMElement);
		}

		// Token: 0x0600093B RID: 2363 RVA: 0x00006C78 File Offset: 0x00004E78
		public IElement GetElementById(string id)
		{
			if (!this.resources.Contains("GetElementById" + id))
			{
				Base.StringSet(this.storage, id);
				nsIDOMElement nsIDOMElement;
				this.node.getElementById(this.storage, out nsIDOMElement);
				if (nsIDOMElement == null)
				{
					return null;
				}
				this.resources.Add("GetElementById" + id, base.GetTypedNode(nsIDOMElement));
			}
			return this.resources["GetElementById" + id] as IElement;
		}

		// Token: 0x0600093C RID: 2364 RVA: 0x00006CFC File Offset: 0x00004EFC
		public IElementCollection GetElementsByTagName(string name)
		{
			if (!this.resources.Contains("GetElementsByTagName" + name))
			{
				nsIDOMNodeList nsIDOMNodeList;
				this.node.getElementsByTagName(this.storage, out nsIDOMNodeList);
				if (nsIDOMNodeList == null)
				{
					return null;
				}
				this.resources.Add("GetElementsByTagName" + name, new HTMLElementCollection(this.control, nsIDOMNodeList));
			}
			return this.resources["GetElementsByTagName" + name] as IElementCollection;
		}

		// Token: 0x0600093D RID: 2365 RVA: 0x00006D78 File Offset: 0x00004F78
		public IElement GetElement(int x, int y)
		{
			nsIDOMNodeList nsIDOMNodeList;
			this.node.getChildNodes(out nsIDOMNodeList);
			NodeList nodeList = new HTMLElementCollection(this.control, nsIDOMNodeList);
			IElement element = null;
			foreach (object obj in nodeList)
			{
				Element element2 = (Element)obj;
				if (element2.Left <= x && element2.Top <= y && element2.Left + element2.Width >= x && element2.Top + element2.Height >= y)
				{
					element = element2;
					break;
				}
			}
			return element;
		}

		// Token: 0x0600093E RID: 2366 RVA: 0x00006E1C File Offset: 0x0000501C
		public void Write(string text)
		{
			if (!(this.node is nsIDOMHTMLDocument))
			{
				return;
			}
			Base.StringSet(this.storage, text);
			((nsIDOMHTMLDocument)this.node).write(this.storage);
		}

		// Token: 0x0600093F RID: 2367 RVA: 0x00006E4F File Offset: 0x0000504F
		public string InvokeScript(string script)
		{
			return Base.EvalScript(this.control, script);
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000940 RID: 2368 RVA: 0x00006E5D File Offset: 0x0000505D
		internal new EventHandlerList Events
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

		// Token: 0x14000043 RID: 67
		// (add) Token: 0x06000941 RID: 2369 RVA: 0x00006E78 File Offset: 0x00005078
		// (remove) Token: 0x06000942 RID: 2370 RVA: 0x00006E8B File Offset: 0x0000508B
		public event EventHandler LoadStopped
		{
			add
			{
				this.Events.AddHandler(Document.LoadStoppedEvent, value);
			}
			remove
			{
				this.Events.RemoveHandler(Document.LoadStoppedEvent, value);
			}
		}

		// Token: 0x06000943 RID: 2371 RVA: 0x00006E9E File Offset: 0x0000509E
		public override int GetHashCode()
		{
			return this.node.GetHashCode();
		}

		// Token: 0x06000944 RID: 2372 RVA: 0x00006EAB File Offset: 0x000050AB
		// Note: this type is marked as 'beforefieldinit'.
		static Document()
		{
			Document.LoadStoppedEvent = new object();
		}

		// Token: 0x04000119 RID: 281
		private EventHandlerList events;
	}
}
