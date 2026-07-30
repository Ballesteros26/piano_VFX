using System;
using System.Collections;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x0200051E RID: 1310
	internal class HtmlElementProps
	{
		// Token: 0x060034C3 RID: 13507 RVA: 0x0012A33F File Offset: 0x0012853F
		public static HtmlElementProps Create(bool empty, bool abrParent, bool uriParent, bool noEntities, bool blockWS, bool head, bool nameParent)
		{
			return new HtmlElementProps
			{
				empty = empty,
				abrParent = abrParent,
				uriParent = uriParent,
				noEntities = noEntities,
				blockWS = blockWS,
				head = head,
				nameParent = nameParent
			};
		}

		// Token: 0x17000B10 RID: 2832
		// (get) Token: 0x060034C4 RID: 13508 RVA: 0x0012A37A File Offset: 0x0012857A
		public bool Empty
		{
			get
			{
				return this.empty;
			}
		}

		// Token: 0x17000B11 RID: 2833
		// (get) Token: 0x060034C5 RID: 13509 RVA: 0x0012A382 File Offset: 0x00128582
		public bool AbrParent
		{
			get
			{
				return this.abrParent;
			}
		}

		// Token: 0x17000B12 RID: 2834
		// (get) Token: 0x060034C6 RID: 13510 RVA: 0x0012A38A File Offset: 0x0012858A
		public bool UriParent
		{
			get
			{
				return this.uriParent;
			}
		}

		// Token: 0x17000B13 RID: 2835
		// (get) Token: 0x060034C7 RID: 13511 RVA: 0x0012A392 File Offset: 0x00128592
		public bool NoEntities
		{
			get
			{
				return this.noEntities;
			}
		}

		// Token: 0x17000B14 RID: 2836
		// (get) Token: 0x060034C8 RID: 13512 RVA: 0x0012A39A File Offset: 0x0012859A
		public bool BlockWS
		{
			get
			{
				return this.blockWS;
			}
		}

		// Token: 0x17000B15 RID: 2837
		// (get) Token: 0x060034C9 RID: 13513 RVA: 0x0012A3A2 File Offset: 0x001285A2
		public bool Head
		{
			get
			{
				return this.head;
			}
		}

		// Token: 0x17000B16 RID: 2838
		// (get) Token: 0x060034CA RID: 13514 RVA: 0x0012A3AA File Offset: 0x001285AA
		public bool NameParent
		{
			get
			{
				return this.nameParent;
			}
		}

		// Token: 0x060034CB RID: 13515 RVA: 0x0012A3B2 File Offset: 0x001285B2
		public static HtmlElementProps GetProps(string name)
		{
			return (HtmlElementProps)HtmlElementProps.s_table[name];
		}

		// Token: 0x060034CC RID: 13516 RVA: 0x0012A3C4 File Offset: 0x001285C4
		private static Hashtable CreatePropsTable()
		{
			bool flag = false;
			bool flag2 = true;
			return new Hashtable(71, StringComparer.OrdinalIgnoreCase)
			{
				{
					"a",
					HtmlElementProps.Create(flag, flag, flag2, flag, flag, flag, flag2)
				},
				{
					"address",
					HtmlElementProps.Create(flag, flag, flag, flag, flag2, flag, flag)
				},
				{
					"applet",
					HtmlElementProps.Create(flag, flag, flag, flag, flag2, flag, flag)
				},
				{
					"area",
					HtmlElementProps.Create(flag2, flag2, flag2, flag, flag2, flag, flag)
				},
				{
					"base",
					HtmlElementProps.Create(flag2, flag, flag2, flag, flag2, flag, flag)
				},
				{
					"basefont",
					HtmlElementProps.Create(flag2, flag, flag, flag, flag2, flag, flag)
				},
				{
					"blockquote",
					HtmlElementProps.Create(flag, flag, flag2, flag, flag2, flag, flag)
				},
				{
					"body",
					HtmlElementProps.Create(flag, flag, flag, flag, flag2, flag, flag)
				},
				{
					"br",
					HtmlElementProps.Create(flag2, flag, flag, flag, flag, flag, flag)
				},
				{
					"button",
					HtmlElementProps.Create(flag, flag2, flag, flag, flag, flag, flag)
				},
				{
					"caption",
					HtmlElementProps.Create(flag, flag, flag, flag, flag2, flag, flag)
				},
				{
					"center",
					HtmlElementProps.Create(flag, flag, flag, flag, flag2, flag, flag)
				},
				{
					"col",
					HtmlElementProps.Create(flag2, flag, flag, flag, flag2, flag, flag)
				},
				{
					"colgroup",
					HtmlElementProps.Create(flag, flag, flag, flag, flag2, flag, flag)
				},
				{
					"dd",
					HtmlElementProps.Create(flag, flag, flag, flag, flag2, flag, flag)
				},
				{
					"del",
					HtmlElementProps.Create(flag, flag, flag2, flag, flag2, flag, flag)
				},
				{
					"dir",
					HtmlElementProps.Create(flag, flag2, flag, flag, flag2, flag, flag)
				},
				{
					"div",
					HtmlElementProps.Create(flag, flag, flag, flag, flag2, flag, flag)
				},
				{
					"dl",
					HtmlElementProps.Create(flag, flag2, flag, flag, flag2, flag, flag)
				},
				{
					"dt",
					HtmlElementProps.Create(flag, flag, flag, flag, flag2, flag, flag)
				},
				{
					"fieldset",
					HtmlElementProps.Create(flag, flag, flag, flag, flag2, flag, flag)
				},
				{
					"font",
					HtmlElementProps.Create(flag, flag, flag, flag, flag2, flag, flag)
				},
				{
					"form",
					HtmlElementProps.Create(flag, flag, flag2, flag, flag2, flag, flag)
				},
				{
					"frame",
					HtmlElementProps.Create(flag2, flag2, flag, flag, flag2, flag, flag)
				},
				{
					"frameset",
					HtmlElementProps.Create(flag, flag, flag, flag, flag2, flag, flag)
				},
				{
					"h1",
					HtmlElementProps.Create(flag, flag, flag, flag, flag2, flag, flag)
				},
				{
					"h2",
					HtmlElementProps.Create(flag, flag, flag, flag, flag2, flag, flag)
				},
				{
					"h3",
					HtmlElementProps.Create(flag, flag, flag, flag, flag2, flag, flag)
				},
				{
					"h4",
					HtmlElementProps.Create(flag, flag, flag, flag, flag2, flag, flag)
				},
				{
					"h5",
					HtmlElementProps.Create(flag, flag, flag, flag, flag2, flag, flag)
				},
				{
					"h6",
					HtmlElementProps.Create(flag, flag, flag, flag, flag2, flag, flag)
				},
				{
					"head",
					HtmlElementProps.Create(flag, flag, flag2, flag, flag2, flag2, flag)
				},
				{
					"hr",
					HtmlElementProps.Create(flag2, flag2, flag, flag, flag2, flag, flag)
				},
				{
					"html",
					HtmlElementProps.Create(flag, flag, flag, flag, flag2, flag, flag)
				},
				{
					"iframe",
					HtmlElementProps.Create(flag, flag, flag, flag, flag2, flag, flag)
				},
				{
					"img",
					HtmlElementProps.Create(flag2, flag2, flag2, flag, flag, flag, flag)
				},
				{
					"input",
					HtmlElementProps.Create(flag2, flag2, flag2, flag, flag, flag, flag)
				},
				{
					"ins",
					HtmlElementProps.Create(flag, flag, flag2, flag, flag2, flag, flag)
				},
				{
					"isindex",
					HtmlElementProps.Create(flag2, flag, flag, flag, flag2, flag, flag)
				},
				{
					"legend",
					HtmlElementProps.Create(flag, flag, flag, flag, flag2, flag, flag)
				},
				{
					"li",
					HtmlElementProps.Create(flag, flag, flag, flag, flag2, flag, flag)
				},
				{
					"link",
					HtmlElementProps.Create(flag2, flag, flag2, flag, flag2, flag, flag)
				},
				{
					"map",
					HtmlElementProps.Create(flag, flag, flag, flag, flag2, flag, flag)
				},
				{
					"menu",
					HtmlElementProps.Create(flag, flag2, flag, flag, flag2, flag, flag)
				},
				{
					"meta",
					HtmlElementProps.Create(flag2, flag, flag, flag, flag2, flag, flag)
				},
				{
					"noframes",
					HtmlElementProps.Create(flag, flag, flag, flag, flag2, flag, flag)
				},
				{
					"noscript",
					HtmlElementProps.Create(flag, flag, flag, flag, flag2, flag, flag)
				},
				{
					"object",
					HtmlElementProps.Create(flag, flag2, flag2, flag, flag, flag, flag)
				},
				{
					"ol",
					HtmlElementProps.Create(flag, flag2, flag, flag, flag2, flag, flag)
				},
				{
					"optgroup",
					HtmlElementProps.Create(flag, flag2, flag, flag, flag2, flag, flag)
				},
				{
					"option",
					HtmlElementProps.Create(flag, flag2, flag, flag, flag2, flag, flag)
				},
				{
					"p",
					HtmlElementProps.Create(flag, flag, flag, flag, flag2, flag, flag)
				},
				{
					"param",
					HtmlElementProps.Create(flag2, flag, flag, flag, flag2, flag, flag)
				},
				{
					"pre",
					HtmlElementProps.Create(flag, flag, flag, flag, flag2, flag, flag)
				},
				{
					"q",
					HtmlElementProps.Create(flag, flag, flag2, flag, flag, flag, flag)
				},
				{
					"s",
					HtmlElementProps.Create(flag, flag, flag, flag, flag2, flag, flag)
				},
				{
					"script",
					HtmlElementProps.Create(flag, flag2, flag2, flag2, flag, flag, flag)
				},
				{
					"select",
					HtmlElementProps.Create(flag, flag2, flag, flag, flag, flag, flag)
				},
				{
					"strike",
					HtmlElementProps.Create(flag, flag, flag, flag, flag2, flag, flag)
				},
				{
					"style",
					HtmlElementProps.Create(flag, flag, flag, flag2, flag2, flag, flag)
				},
				{
					"table",
					HtmlElementProps.Create(flag, flag, flag2, flag, flag2, flag, flag)
				},
				{
					"tbody",
					HtmlElementProps.Create(flag, flag, flag, flag, flag2, flag, flag)
				},
				{
					"td",
					HtmlElementProps.Create(flag, flag2, flag, flag, flag2, flag, flag)
				},
				{
					"textarea",
					HtmlElementProps.Create(flag, flag2, flag, flag, flag, flag, flag)
				},
				{
					"tfoot",
					HtmlElementProps.Create(flag, flag, flag, flag, flag2, flag, flag)
				},
				{
					"th",
					HtmlElementProps.Create(flag, flag2, flag, flag, flag2, flag, flag)
				},
				{
					"thead",
					HtmlElementProps.Create(flag, flag, flag, flag, flag2, flag, flag)
				},
				{
					"title",
					HtmlElementProps.Create(flag, flag, flag, flag, flag2, flag, flag)
				},
				{
					"tr",
					HtmlElementProps.Create(flag, flag, flag, flag, flag2, flag, flag)
				},
				{
					"ul",
					HtmlElementProps.Create(flag, flag2, flag, flag, flag2, flag, flag)
				},
				{
					"xmp",
					HtmlElementProps.Create(flag, flag, flag, flag, flag, flag, flag)
				}
			};
		}

		// Token: 0x040021B6 RID: 8630
		private bool empty;

		// Token: 0x040021B7 RID: 8631
		private bool abrParent;

		// Token: 0x040021B8 RID: 8632
		private bool uriParent;

		// Token: 0x040021B9 RID: 8633
		private bool noEntities;

		// Token: 0x040021BA RID: 8634
		private bool blockWS;

		// Token: 0x040021BB RID: 8635
		private bool head;

		// Token: 0x040021BC RID: 8636
		private bool nameParent;

		// Token: 0x040021BD RID: 8637
		private static Hashtable s_table = HtmlElementProps.CreatePropsTable();
	}
}
