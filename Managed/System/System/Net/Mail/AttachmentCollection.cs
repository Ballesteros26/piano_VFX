using System;
using System.Collections.ObjectModel;

namespace System.Net.Mail
{
	/// <summary>Stores attachments to be sent as part of an e-mail message.</summary>
	// Token: 0x0200057A RID: 1402
	public sealed class AttachmentCollection : Collection<Attachment>, IDisposable
	{
		// Token: 0x06002B8C RID: 11148 RVA: 0x000AC5CA File Offset: 0x000AA7CA
		internal AttachmentCollection()
		{
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Net.Mail.AttachmentCollection" />. </summary>
		// Token: 0x06002B8D RID: 11149 RVA: 0x000AC5D4 File Offset: 0x000AA7D4
		public void Dispose()
		{
			for (int i = 0; i < base.Count; i++)
			{
				base[i].Dispose();
			}
		}

		// Token: 0x06002B8E RID: 11150 RVA: 0x000AC5FE File Offset: 0x000AA7FE
		protected override void ClearItems()
		{
			base.ClearItems();
		}

		// Token: 0x06002B8F RID: 11151 RVA: 0x000AC606 File Offset: 0x000AA806
		protected override void InsertItem(int index, Attachment item)
		{
			base.InsertItem(index, item);
		}

		// Token: 0x06002B90 RID: 11152 RVA: 0x000AC610 File Offset: 0x000AA810
		protected override void RemoveItem(int index)
		{
			base.RemoveItem(index);
		}

		// Token: 0x06002B91 RID: 11153 RVA: 0x000AC619 File Offset: 0x000AA819
		protected override void SetItem(int index, Attachment item)
		{
			base.SetItem(index, item);
		}
	}
}
