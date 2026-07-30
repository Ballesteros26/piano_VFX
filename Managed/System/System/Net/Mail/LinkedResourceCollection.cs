using System;
using System.Collections.ObjectModel;

namespace System.Net.Mail
{
	/// <summary>Stores linked resources to be sent as part of an e-mail message.</summary>
	// Token: 0x0200057D RID: 1405
	public sealed class LinkedResourceCollection : Collection<LinkedResource>, IDisposable
	{
		// Token: 0x06002B9D RID: 11165 RVA: 0x000AC717 File Offset: 0x000AA917
		internal LinkedResourceCollection()
		{
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Net.Mail.LinkedResourceCollection" />.</summary>
		// Token: 0x06002B9E RID: 11166 RVA: 0x000AC71F File Offset: 0x000AA91F
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06002B9F RID: 11167 RVA: 0x000027E8 File Offset: 0x000009E8
		private void Dispose(bool disposing)
		{
		}

		// Token: 0x06002BA0 RID: 11168 RVA: 0x000AC72E File Offset: 0x000AA92E
		protected override void ClearItems()
		{
			base.ClearItems();
		}

		// Token: 0x06002BA1 RID: 11169 RVA: 0x000AC736 File Offset: 0x000AA936
		protected override void InsertItem(int index, LinkedResource item)
		{
			base.InsertItem(index, item);
		}

		// Token: 0x06002BA2 RID: 11170 RVA: 0x000AC740 File Offset: 0x000AA940
		protected override void RemoveItem(int index)
		{
			base.RemoveItem(index);
		}

		// Token: 0x06002BA3 RID: 11171 RVA: 0x000AC749 File Offset: 0x000AA949
		protected override void SetItem(int index, LinkedResource item)
		{
			base.SetItem(index, item);
		}
	}
}
