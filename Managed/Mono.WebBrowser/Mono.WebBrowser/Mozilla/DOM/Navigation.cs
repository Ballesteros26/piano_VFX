using System;
using Mono.WebBrowser;
using Mono.WebBrowser.DOM;

namespace Mono.Mozilla.DOM
{
	// Token: 0x0200013E RID: 318
	internal class Navigation : DOMObject, INavigation
	{
		// Token: 0x060009AF RID: 2479 RVA: 0x00008389 File Offset: 0x00006589
		public Navigation(WebBrowser control, nsIWebNavigation webNav)
			: base(control)
		{
			this.navigation = webNav;
		}

		// Token: 0x060009B0 RID: 2480 RVA: 0x00008399 File Offset: 0x00006599
		protected override void Dispose(bool disposing)
		{
			if (!this.disposed && disposing)
			{
				this.navigation = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060009B1 RID: 2481 RVA: 0x000083B4 File Offset: 0x000065B4
		public bool CanGoBack
		{
			get
			{
				if (this.navigation == null)
				{
					return false;
				}
				bool flag;
				this.navigation.getCanGoBack(out flag);
				return flag;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060009B2 RID: 2482 RVA: 0x000083DC File Offset: 0x000065DC
		public bool CanGoForward
		{
			get
			{
				if (this.navigation == null)
				{
					return false;
				}
				bool flag;
				this.navigation.getCanGoForward(out flag);
				return flag;
			}
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x00008402 File Offset: 0x00006602
		public bool Back()
		{
			if (this.navigation == null)
			{
				return false;
			}
			this.control.Reset();
			return this.navigation.goBack() == 0;
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x00008427 File Offset: 0x00006627
		public bool Forward()
		{
			if (this.navigation == null)
			{
				return false;
			}
			this.control.Reset();
			return this.navigation.goForward() == 0;
		}

		// Token: 0x060009B5 RID: 2485 RVA: 0x0000844C File Offset: 0x0000664C
		public void Home()
		{
			this.control.Reset();
			Base.Home(this.control);
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x00008464 File Offset: 0x00006664
		public void Reload()
		{
			this.Reload(ReloadOption.None);
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x00008470 File Offset: 0x00006670
		public void Reload(ReloadOption option)
		{
			if (this.navigation == null)
			{
				return;
			}
			this.control.Reset();
			if (option == ReloadOption.None)
			{
				this.navigation.reload(0U);
				return;
			}
			if (option == ReloadOption.Proxy)
			{
				this.navigation.reload(256U);
				return;
			}
			if (option == ReloadOption.Full)
			{
				this.navigation.reload(512U);
			}
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x000084CD File Offset: 0x000066CD
		public void Stop()
		{
			if (this.navigation == null)
			{
				return;
			}
			this.navigation.stop(3U);
		}

		// Token: 0x060009B9 RID: 2489 RVA: 0x000084E8 File Offset: 0x000066E8
		public void Go(int index)
		{
			if (this.navigation == null || index < 0)
			{
				return;
			}
			nsISHistory nsISHistory;
			this.navigation.getSessionHistory(out nsISHistory);
			int num;
			nsISHistory.getCount(out num);
			if (index > num)
			{
				return;
			}
			this.control.Reset();
			this.navigation.gotoIndex(index);
		}

		// Token: 0x060009BA RID: 2490 RVA: 0x00008538 File Offset: 0x00006738
		public void Go(int index, bool relative)
		{
			if (relative)
			{
				nsISHistory nsISHistory;
				this.navigation.getSessionHistory(out nsISHistory);
				int num;
				nsISHistory.getCount(out num);
				int num2;
				nsISHistory.getIndex(out num2);
				index = num2 + index;
			}
			this.Go(index);
		}

		// Token: 0x060009BB RID: 2491 RVA: 0x00008574 File Offset: 0x00006774
		public void Go(string url)
		{
			if (this.navigation == null)
			{
				return;
			}
			this.control.Reset();
			this.navigation.loadURI(url, 0U, null, null, null);
		}

		// Token: 0x060009BC RID: 2492 RVA: 0x0000859B File Offset: 0x0000679B
		public void Go(string url, LoadFlags flags)
		{
			if (this.navigation == null)
			{
				return;
			}
			this.control.Reset();
			this.navigation.loadURI(url, (uint)flags, null, null, null);
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060009BD RID: 2493 RVA: 0x000085C4 File Offset: 0x000067C4
		public int HistoryCount
		{
			get
			{
				nsISHistory nsISHistory;
				this.navigation.getSessionHistory(out nsISHistory);
				int num;
				nsISHistory.getCount(out num);
				return num;
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060009BE RID: 2494 RVA: 0x000085EC File Offset: 0x000067EC
		internal Document Document
		{
			get
			{
				nsIDOMDocument nsIDOMDocument;
				this.navigation.getDocument(out nsIDOMDocument);
				int hashCode = nsIDOMDocument.GetHashCode();
				if (!this.resources.ContainsKey(hashCode))
				{
					this.resources.Add(hashCode, new Document(this.control, nsIDOMDocument as nsIDOMHTMLDocument));
				}
				return this.resources[hashCode] as Document;
			}
		}

		// Token: 0x060009BF RID: 2495 RVA: 0x00008659 File Offset: 0x00006859
		public override int GetHashCode()
		{
			return this.navigation.GetHashCode();
		}

		// Token: 0x04000123 RID: 291
		internal nsIWebNavigation navigation;
	}
}
