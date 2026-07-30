using System;
using System.IO;
using Mono.WebBrowser.DOM;

namespace Mono.Mozilla.DOM
{
	// Token: 0x02000138 RID: 312
	internal class Element : Node, IElement, INode
	{
		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600094F RID: 2383 RVA: 0x00006FE8 File Offset: 0x000051E8
		// (set) Token: 0x06000950 RID: 2384 RVA: 0x00006FF5 File Offset: 0x000051F5
		internal new nsIDOMElement node
		{
			get
			{
				return base.node as nsIDOMElement;
			}
			set
			{
				base.node = value;
			}
		}

		// Token: 0x06000951 RID: 2385 RVA: 0x00006FFE File Offset: 0x000051FE
		public Element(WebBrowser control, nsIDOMElement domElement)
			: base(control, domElement)
		{
			this.node = domElement;
		}

		// Token: 0x06000952 RID: 2386 RVA: 0x0000700F File Offset: 0x0000520F
		protected override void Dispose(bool disposing)
		{
			if (!this.disposed && disposing)
			{
				this.node = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000953 RID: 2387 RVA: 0x0000702C File Offset: 0x0000522C
		public virtual IElement AppendChild(IElement child)
		{
			Element element = (Element)child;
			nsIDOMNode nsIDOMNode;
			this.node.appendChild(element.node, out nsIDOMNode);
			return new Element(this.control, nsIDOMNode as nsIDOMElement);
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000954 RID: 2388 RVA: 0x00007068 File Offset: 0x00005268
		// (set) Token: 0x06000955 RID: 2389 RVA: 0x000070BC File Offset: 0x000052BC
		public virtual string InnerText
		{
			get
			{
				nsIDOMRange nsIDOMRange;
				(((Document)this.control.Document).XPComObject as nsIDOMDocumentRange).createRange(out nsIDOMRange);
				nsIDOMRange.selectNodeContents(this.node);
				nsIDOMRange.toString(this.storage);
				return Base.StringGet(this.storage);
			}
			set
			{
				Base.StringSet(this.storage, value);
				this.node.setNodeValue(this.storage);
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000956 RID: 2390 RVA: 0x000070DC File Offset: 0x000052DC
		// (set) Token: 0x06000957 RID: 2391 RVA: 0x0000713C File Offset: 0x0000533C
		public virtual string OuterText
		{
			get
			{
				nsIDOMRange nsIDOMRange;
				(((Document)this.control.Document).XPComObject as nsIDOMDocumentRange).createRange(out nsIDOMRange);
				nsIDOMNode nsIDOMNode;
				this.node.getParentNode(out nsIDOMNode);
				nsIDOMRange.selectNodeContents(nsIDOMNode);
				nsIDOMRange.toString(this.storage);
				return Base.StringGet(this.storage);
			}
			set
			{
				Base.StringSet(this.storage, value);
				nsIDOMNode nsIDOMNode;
				this.node.getParentNode(out nsIDOMNode);
				nsIDOMNode.setNodeValue(this.storage);
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000958 RID: 2392 RVA: 0x00007170 File Offset: 0x00005370
		// (set) Token: 0x06000959 RID: 2393 RVA: 0x00007177 File Offset: 0x00005377
		public virtual string InnerHTML
		{
			get
			{
				return string.Empty;
			}
			set
			{
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x0600095A RID: 2394 RVA: 0x00007179 File Offset: 0x00005379
		// (set) Token: 0x0600095B RID: 2395 RVA: 0x00007180 File Offset: 0x00005380
		public virtual string OuterHTML
		{
			get
			{
				return string.Empty;
			}
			set
			{
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x0600095C RID: 2396 RVA: 0x00007182 File Offset: 0x00005382
		public virtual Stream ContentStream
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x0600095D RID: 2397 RVA: 0x00007188 File Offset: 0x00005388
		public IElementCollection All
		{
			get
			{
				if (!this.resources.Contains("All"))
				{
					HTMLElementCollection htmlelementCollection = new HTMLElementCollection(this.control);
					this.Recurse(htmlelementCollection, this.node);
					this.resources.Add("All", htmlelementCollection);
				}
				return this.resources["All"] as IElementCollection;
			}
		}

		// Token: 0x0600095E RID: 2398 RVA: 0x000071E8 File Offset: 0x000053E8
		private void Recurse(HTMLElementCollection col, nsIDOMNode parent)
		{
			nsIDOMNodeList nsIDOMNodeList;
			parent.getChildNodes(out nsIDOMNodeList);
			uint num;
			nsIDOMNodeList.getLength(out num);
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				nsIDOMNode nsIDOMNode;
				nsIDOMNodeList.item((uint)num2, out nsIDOMNode);
				ushort num3;
				nsIDOMNode.getNodeType(out num3);
				if (num3 == 1)
				{
					col.Add(new HTMLElement(this.control, (nsIDOMHTMLElement)nsIDOMNode));
					this.Recurse(col, nsIDOMNode);
				}
				num2++;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x0600095F RID: 2399 RVA: 0x00007250 File Offset: 0x00005450
		public IElementCollection Children
		{
			get
			{
				if (!this.resources.Contains("Children"))
				{
					nsIDOMNodeList nsIDOMNodeList;
					this.node.getChildNodes(out nsIDOMNodeList);
					this.resources.Add("Children", new HTMLElementCollection(this.control, nsIDOMNodeList));
				}
				return this.resources["Children"] as IElementCollection;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000960 RID: 2400 RVA: 0x000072AE File Offset: 0x000054AE
		// (set) Token: 0x06000961 RID: 2401 RVA: 0x000072B1 File Offset: 0x000054B1
		public virtual int TabIndex
		{
			get
			{
				return -1;
			}
			set
			{
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000962 RID: 2402 RVA: 0x000072B3 File Offset: 0x000054B3
		public virtual string TagName
		{
			get
			{
				this.node.getTagName(this.storage);
				return Base.StringGet(this.storage);
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000963 RID: 2403 RVA: 0x000072D2 File Offset: 0x000054D2
		// (set) Token: 0x06000964 RID: 2404 RVA: 0x000072D5 File Offset: 0x000054D5
		public virtual bool Disabled
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000965 RID: 2405 RVA: 0x000072D8 File Offset: 0x000054D8
		public virtual int ClientWidth
		{
			get
			{
				nsIDOMNSHTMLElement nsIDOMNSHTMLElement = this.node as nsIDOMNSHTMLElement;
				if (nsIDOMNSHTMLElement == null)
				{
					return 0;
				}
				int num = 0;
				nsIDOMNSHTMLElement.getClientWidth(out num);
				return num;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000966 RID: 2406 RVA: 0x00007304 File Offset: 0x00005504
		public virtual int ClientHeight
		{
			get
			{
				nsIDOMNSHTMLElement nsIDOMNSHTMLElement = this.node as nsIDOMNSHTMLElement;
				if (nsIDOMNSHTMLElement == null)
				{
					return 0;
				}
				int num = 0;
				nsIDOMNSHTMLElement.getClientHeight(out num);
				return num;
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000967 RID: 2407 RVA: 0x00007330 File Offset: 0x00005530
		public virtual int ScrollHeight
		{
			get
			{
				nsIDOMNSHTMLElement nsIDOMNSHTMLElement = this.node as nsIDOMNSHTMLElement;
				if (nsIDOMNSHTMLElement == null)
				{
					return 0;
				}
				int num = 0;
				nsIDOMNSHTMLElement.getScrollHeight(out num);
				return num;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000968 RID: 2408 RVA: 0x0000735C File Offset: 0x0000555C
		public virtual int ScrollWidth
		{
			get
			{
				nsIDOMNSHTMLElement nsIDOMNSHTMLElement = this.node as nsIDOMNSHTMLElement;
				if (nsIDOMNSHTMLElement == null)
				{
					return 0;
				}
				int num = 0;
				nsIDOMNSHTMLElement.getScrollWidth(out num);
				return num;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000969 RID: 2409 RVA: 0x00007388 File Offset: 0x00005588
		// (set) Token: 0x0600096A RID: 2410 RVA: 0x000073B4 File Offset: 0x000055B4
		public virtual int ScrollLeft
		{
			get
			{
				nsIDOMNSHTMLElement nsIDOMNSHTMLElement = this.node as nsIDOMNSHTMLElement;
				if (nsIDOMNSHTMLElement == null)
				{
					return 0;
				}
				int num = 0;
				nsIDOMNSHTMLElement.getScrollLeft(out num);
				return num;
			}
			set
			{
				nsIDOMNSHTMLElement nsIDOMNSHTMLElement = this.node as nsIDOMNSHTMLElement;
				if (nsIDOMNSHTMLElement == null)
				{
					return;
				}
				nsIDOMNSHTMLElement.setScrollLeft(value);
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x0600096B RID: 2411 RVA: 0x000073DC File Offset: 0x000055DC
		// (set) Token: 0x0600096C RID: 2412 RVA: 0x00007408 File Offset: 0x00005608
		public virtual int ScrollTop
		{
			get
			{
				nsIDOMNSHTMLElement nsIDOMNSHTMLElement = this.node as nsIDOMNSHTMLElement;
				if (nsIDOMNSHTMLElement == null)
				{
					return 0;
				}
				int num = 0;
				nsIDOMNSHTMLElement.getScrollTop(out num);
				return num;
			}
			set
			{
				nsIDOMNSHTMLElement nsIDOMNSHTMLElement = this.node as nsIDOMNSHTMLElement;
				if (nsIDOMNSHTMLElement == null)
				{
					return;
				}
				nsIDOMNSHTMLElement.setScrollTop(value);
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x0600096D RID: 2413 RVA: 0x00007430 File Offset: 0x00005630
		public virtual int OffsetHeight
		{
			get
			{
				nsIDOMNSHTMLElement nsIDOMNSHTMLElement = this.node as nsIDOMNSHTMLElement;
				if (nsIDOMNSHTMLElement == null)
				{
					return 0;
				}
				int num = 0;
				nsIDOMNSHTMLElement.getOffsetHeight(out num);
				return num;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x0600096E RID: 2414 RVA: 0x0000745C File Offset: 0x0000565C
		public virtual int OffsetWidth
		{
			get
			{
				nsIDOMNSHTMLElement nsIDOMNSHTMLElement = this.node as nsIDOMNSHTMLElement;
				if (nsIDOMNSHTMLElement == null)
				{
					return 0;
				}
				int num = 0;
				nsIDOMNSHTMLElement.getOffsetWidth(out num);
				return num;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x0600096F RID: 2415 RVA: 0x00007488 File Offset: 0x00005688
		public virtual int OffsetLeft
		{
			get
			{
				nsIDOMNSHTMLElement nsIDOMNSHTMLElement = this.node as nsIDOMNSHTMLElement;
				if (nsIDOMNSHTMLElement == null)
				{
					return 0;
				}
				int num = 0;
				nsIDOMNSHTMLElement.getOffsetLeft(out num);
				return num;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000970 RID: 2416 RVA: 0x000074B4 File Offset: 0x000056B4
		public virtual int OffsetTop
		{
			get
			{
				nsIDOMNSHTMLElement nsIDOMNSHTMLElement = this.node as nsIDOMNSHTMLElement;
				if (nsIDOMNSHTMLElement == null)
				{
					return 0;
				}
				int num = 0;
				nsIDOMNSHTMLElement.getOffsetTop(out num);
				return num;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000971 RID: 2417 RVA: 0x000074E0 File Offset: 0x000056E0
		public virtual IElement OffsetParent
		{
			get
			{
				nsIDOMNSHTMLElement nsIDOMNSHTMLElement = this.node as nsIDOMNSHTMLElement;
				if (nsIDOMNSHTMLElement == null)
				{
					return null;
				}
				nsIDOMElement nsIDOMElement;
				nsIDOMNSHTMLElement.getOffsetParent(out nsIDOMElement);
				if (nsIDOMElement is nsIDOMHTMLElement)
				{
					return new HTMLElement(this.control, nsIDOMElement as nsIDOMHTMLElement);
				}
				return new Element(this.control, nsIDOMElement);
			}
		}

		// Token: 0x06000972 RID: 2418 RVA: 0x00007530 File Offset: 0x00005730
		public void Blur()
		{
			nsIDOMNSHTMLElement nsIDOMNSHTMLElement = this.node as nsIDOMNSHTMLElement;
			if (nsIDOMNSHTMLElement != null)
			{
				nsIDOMNSHTMLElement.blur();
			}
		}

		// Token: 0x06000973 RID: 2419 RVA: 0x00007554 File Offset: 0x00005754
		public void Focus()
		{
			nsIDOMNSHTMLElement nsIDOMNSHTMLElement = this.node as nsIDOMNSHTMLElement;
			if (nsIDOMNSHTMLElement != null)
			{
				nsIDOMNSHTMLElement.focus();
			}
		}

		// Token: 0x06000974 RID: 2420 RVA: 0x00007578 File Offset: 0x00005778
		public IElementCollection GetElementsByTagName(string name)
		{
			if (!this.resources.Contains("GetElementsByTagName" + name))
			{
				nsIDOMNodeList nsIDOMNodeList;
				this.node.getElementsByTagName(this.storage, out nsIDOMNodeList);
				this.resources.Add("GetElementsByTagName" + name, new HTMLElementCollection(this.control, nsIDOMNodeList));
			}
			return this.resources["GetElementsByTagName" + name] as IElementCollection;
		}

		// Token: 0x06000975 RID: 2421 RVA: 0x000075EE File Offset: 0x000057EE
		public override int GetHashCode()
		{
			return this.hashcode;
		}

		// Token: 0x06000976 RID: 2422 RVA: 0x000075F8 File Offset: 0x000057F8
		public virtual bool HasAttribute(string name)
		{
			Base.StringSet(this.storage, name);
			bool flag;
			this.node.hasAttribute(this.storage, out flag);
			return flag;
		}

		// Token: 0x06000977 RID: 2423 RVA: 0x00007628 File Offset: 0x00005828
		public virtual string GetAttribute(string name)
		{
			UniString uniString = new UniString(string.Empty);
			Base.StringSet(this.storage, name);
			this.node.getAttribute(this.storage, uniString.Handle);
			return uniString.ToString();
		}

		// Token: 0x06000978 RID: 2424 RVA: 0x0000766C File Offset: 0x0000586C
		public void ScrollIntoView(bool alignWithTop)
		{
			nsIDOMNSHTMLElement nsIDOMNSHTMLElement = this.node as nsIDOMNSHTMLElement;
			if (nsIDOMNSHTMLElement != null)
			{
				nsIDOMNSHTMLElement.scrollIntoView(alignWithTop);
			}
		}

		// Token: 0x06000979 RID: 2425 RVA: 0x00007690 File Offset: 0x00005890
		public virtual void SetAttribute(string name, string value)
		{
			UniString uniString = new UniString(value);
			Base.StringSet(this.storage, name);
			this.node.setAttribute(this.storage, uniString.Handle);
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x0600097A RID: 2426 RVA: 0x000076C8 File Offset: 0x000058C8
		internal int Top
		{
			get
			{
				int num;
				((nsIDOMNSHTMLElement)this.node).getOffsetTop(out num);
				return num;
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x0600097B RID: 2427 RVA: 0x000076EC File Offset: 0x000058EC
		internal int Left
		{
			get
			{
				int num;
				((nsIDOMNSHTMLElement)this.node).getOffsetLeft(out num);
				return num;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x0600097C RID: 2428 RVA: 0x00007710 File Offset: 0x00005910
		internal int Width
		{
			get
			{
				int num;
				((nsIDOMNSHTMLElement)this.node).getOffsetWidth(out num);
				return num;
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x0600097D RID: 2429 RVA: 0x00007734 File Offset: 0x00005934
		internal int Height
		{
			get
			{
				int num;
				((nsIDOMNSHTMLElement)this.node).getOffsetHeight(out num);
				return num;
			}
		}
	}
}
