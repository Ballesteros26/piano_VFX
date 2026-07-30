using System;
using System.Collections;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x0200051F RID: 1311
	internal class HtmlAttributeProps
	{
		// Token: 0x060034CF RID: 13519 RVA: 0x0012AA4E File Offset: 0x00128C4E
		public static HtmlAttributeProps Create(bool abr, bool uri, bool name)
		{
			return new HtmlAttributeProps
			{
				abr = abr,
				uri = uri,
				name = name
			};
		}

		// Token: 0x17000B17 RID: 2839
		// (get) Token: 0x060034D0 RID: 13520 RVA: 0x0012AA6A File Offset: 0x00128C6A
		public bool Abr
		{
			get
			{
				return this.abr;
			}
		}

		// Token: 0x17000B18 RID: 2840
		// (get) Token: 0x060034D1 RID: 13521 RVA: 0x0012AA72 File Offset: 0x00128C72
		public bool Uri
		{
			get
			{
				return this.uri;
			}
		}

		// Token: 0x17000B19 RID: 2841
		// (get) Token: 0x060034D2 RID: 13522 RVA: 0x0012AA7A File Offset: 0x00128C7A
		public bool Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x060034D3 RID: 13523 RVA: 0x0012AA82 File Offset: 0x00128C82
		public static HtmlAttributeProps GetProps(string name)
		{
			return (HtmlAttributeProps)HtmlAttributeProps.s_table[name];
		}

		// Token: 0x060034D4 RID: 13524 RVA: 0x0012AA94 File Offset: 0x00128C94
		private static Hashtable CreatePropsTable()
		{
			bool flag = false;
			bool flag2 = true;
			return new Hashtable(26, StringComparer.OrdinalIgnoreCase)
			{
				{
					"action",
					HtmlAttributeProps.Create(flag, flag2, flag)
				},
				{
					"checked",
					HtmlAttributeProps.Create(flag2, flag, flag)
				},
				{
					"cite",
					HtmlAttributeProps.Create(flag, flag2, flag)
				},
				{
					"classid",
					HtmlAttributeProps.Create(flag, flag2, flag)
				},
				{
					"codebase",
					HtmlAttributeProps.Create(flag, flag2, flag)
				},
				{
					"compact",
					HtmlAttributeProps.Create(flag2, flag, flag)
				},
				{
					"data",
					HtmlAttributeProps.Create(flag, flag2, flag)
				},
				{
					"datasrc",
					HtmlAttributeProps.Create(flag, flag2, flag)
				},
				{
					"declare",
					HtmlAttributeProps.Create(flag2, flag, flag)
				},
				{
					"defer",
					HtmlAttributeProps.Create(flag2, flag, flag)
				},
				{
					"disabled",
					HtmlAttributeProps.Create(flag2, flag, flag)
				},
				{
					"for",
					HtmlAttributeProps.Create(flag, flag2, flag)
				},
				{
					"href",
					HtmlAttributeProps.Create(flag, flag2, flag)
				},
				{
					"ismap",
					HtmlAttributeProps.Create(flag2, flag, flag)
				},
				{
					"longdesc",
					HtmlAttributeProps.Create(flag, flag2, flag)
				},
				{
					"multiple",
					HtmlAttributeProps.Create(flag2, flag, flag)
				},
				{
					"name",
					HtmlAttributeProps.Create(flag, flag, flag2)
				},
				{
					"nohref",
					HtmlAttributeProps.Create(flag2, flag, flag)
				},
				{
					"noresize",
					HtmlAttributeProps.Create(flag2, flag, flag)
				},
				{
					"noshade",
					HtmlAttributeProps.Create(flag2, flag, flag)
				},
				{
					"nowrap",
					HtmlAttributeProps.Create(flag2, flag, flag)
				},
				{
					"profile",
					HtmlAttributeProps.Create(flag, flag2, flag)
				},
				{
					"readonly",
					HtmlAttributeProps.Create(flag2, flag, flag)
				},
				{
					"selected",
					HtmlAttributeProps.Create(flag2, flag, flag)
				},
				{
					"src",
					HtmlAttributeProps.Create(flag, flag2, flag)
				},
				{
					"usemap",
					HtmlAttributeProps.Create(flag, flag2, flag)
				}
			};
		}

		// Token: 0x040021BE RID: 8638
		private bool abr;

		// Token: 0x040021BF RID: 8639
		private bool uri;

		// Token: 0x040021C0 RID: 8640
		private bool name;

		// Token: 0x040021C1 RID: 8641
		private static Hashtable s_table = HtmlAttributeProps.CreatePropsTable();
	}
}
