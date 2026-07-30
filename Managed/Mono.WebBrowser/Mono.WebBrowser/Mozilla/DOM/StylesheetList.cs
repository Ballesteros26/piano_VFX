using System;
using System.Collections;
using System.Collections.Generic;
using Mono.WebBrowser.DOM;

namespace Mono.Mozilla.DOM
{
	// Token: 0x02000142 RID: 322
	internal class StylesheetList : DOMObject, IStylesheetList, IEnumerable
	{
		// Token: 0x06000A27 RID: 2599 RVA: 0x00009A35 File Offset: 0x00007C35
		public StylesheetList(WebBrowser control, nsIDOMStyleSheetList stylesheetList)
			: base(control)
		{
			if (control.platform != control.enginePlatform)
			{
				this.unmanagedStyles = nsDOMStyleSheetList.GetProxy(control, stylesheetList);
			}
			else
			{
				this.unmanagedStyles = stylesheetList;
			}
			this.styles = new List<IStylesheet>();
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x00009A6D File Offset: 0x00007C6D
		protected override void Dispose(bool disposing)
		{
			if (!this.disposed && disposing)
			{
				this.Clear();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x00009A87 File Offset: 0x00007C87
		protected void Clear()
		{
			this.styles.Clear();
		}

		// Token: 0x06000A2A RID: 2602 RVA: 0x00009A94 File Offset: 0x00007C94
		internal void Load()
		{
			this.Clear();
			uint num;
			this.unmanagedStyles.getLength(out num);
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				nsIDOMStyleSheet nsIDOMStyleSheet;
				this.unmanagedStyles.item((uint)num2, out nsIDOMStyleSheet);
				this.styles.Add(new Stylesheet(this.control, nsIDOMStyleSheet));
				num2++;
			}
		}

		// Token: 0x06000A2B RID: 2603 RVA: 0x00009AE9 File Offset: 0x00007CE9
		IEnumerator IEnumerable.GetEnumerator()
		{
			if (this.styles.Count == 0)
			{
				this.Load();
			}
			return this.styles.GetEnumerator();
		}

		// Token: 0x1700010C RID: 268
		public IStylesheet this[int index]
		{
			get
			{
				return this.styles[index];
			}
			set
			{
				this.styles[index] = value;
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000A2E RID: 2606 RVA: 0x00009B2B File Offset: 0x00007D2B
		public int Count
		{
			get
			{
				if (this.styles.Count == 0)
				{
					this.Load();
				}
				return this.styles.Count;
			}
		}

		// Token: 0x0400012F RID: 303
		private nsIDOMStyleSheetList unmanagedStyles;

		// Token: 0x04000130 RID: 304
		private List<IStylesheet> styles;
	}
}
