using System;
using System.Collections.ObjectModel;

namespace System.Net.Mail
{
	/// <summary>Represents a collection of <see cref="T:System.Net.Mail.AlternateView" /> objects.</summary>
	// Token: 0x02000576 RID: 1398
	public sealed class AlternateViewCollection : Collection<AlternateView>, IDisposable
	{
		// Token: 0x06002B65 RID: 11109 RVA: 0x000A9FC0 File Offset: 0x000A81C0
		internal AlternateViewCollection()
		{
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Net.Mail.AlternateViewCollection" />.</summary>
		// Token: 0x06002B66 RID: 11110 RVA: 0x000027E8 File Offset: 0x000009E8
		public void Dispose()
		{
		}

		// Token: 0x06002B67 RID: 11111 RVA: 0x000A9FC8 File Offset: 0x000A81C8
		protected override void ClearItems()
		{
			base.ClearItems();
		}

		// Token: 0x06002B68 RID: 11112 RVA: 0x000A9FD0 File Offset: 0x000A81D0
		protected override void InsertItem(int index, AlternateView item)
		{
			base.InsertItem(index, item);
		}

		// Token: 0x06002B69 RID: 11113 RVA: 0x000A9FDA File Offset: 0x000A81DA
		protected override void RemoveItem(int index)
		{
			base.RemoveItem(index);
		}

		// Token: 0x06002B6A RID: 11114 RVA: 0x000A9FE3 File Offset: 0x000A81E3
		protected override void SetItem(int index, AlternateView item)
		{
			base.SetItem(index, item);
		}
	}
}
