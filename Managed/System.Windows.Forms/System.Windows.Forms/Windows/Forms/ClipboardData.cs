using System;
using System.Collections;
using System.Collections.Specialized;
using System.Drawing;

namespace System.Windows.Forms
{
	// Token: 0x020003C5 RID: 965
	internal class ClipboardData
	{
		// Token: 0x0600457C RID: 17788 RVA: 0x0010EFE0 File Offset: 0x0010D1E0
		public ClipboardData()
		{
			this.source_data = new ListDictionary();
		}

		// Token: 0x0600457D RID: 17789 RVA: 0x0010EFF4 File Offset: 0x0010D1F4
		public void ClearSources()
		{
			this.source_data.Clear();
			this.plain_text_source = null;
			this.image_source = null;
		}

		// Token: 0x0600457E RID: 17790 RVA: 0x0010F010 File Offset: 0x0010D210
		public void AddSource(int type, object source)
		{
			if (source is string && (type == DataFormats.GetFormat(DataFormats.Text).Id || type == -1))
			{
				this.plain_text_source = source as string;
			}
			else if (source is Image)
			{
				this.image_source = source as Image;
			}
			this.source_data[type] = source;
		}

		// Token: 0x0600457F RID: 17791 RVA: 0x0010F080 File Offset: 0x0010D280
		public object GetSource(int type)
		{
			return this.source_data[type];
		}

		// Token: 0x06004580 RID: 17792 RVA: 0x0010F094 File Offset: 0x0010D294
		public string GetPlainText()
		{
			return this.plain_text_source;
		}

		// Token: 0x06004581 RID: 17793 RVA: 0x0010F09C File Offset: 0x0010D29C
		public string GetRtfText()
		{
			DataFormats.Format format = DataFormats.GetFormat(DataFormats.Rtf);
			if (format == null)
			{
				return null;
			}
			return (string)this.GetSource(format.Id);
		}

		// Token: 0x06004582 RID: 17794 RVA: 0x0010F0D0 File Offset: 0x0010D2D0
		public Image GetImage()
		{
			return this.image_source;
		}

		// Token: 0x170011DF RID: 4575
		// (get) Token: 0x06004583 RID: 17795 RVA: 0x0010F0D8 File Offset: 0x0010D2D8
		public bool IsSourceText
		{
			get
			{
				return this.plain_text_source != null;
			}
		}

		// Token: 0x170011E0 RID: 4576
		// (get) Token: 0x06004584 RID: 17796 RVA: 0x0010F0E8 File Offset: 0x0010D2E8
		public bool IsSourceImage
		{
			get
			{
				return this.image_source != null;
			}
		}

		// Token: 0x04001D4D RID: 7501
		private ListDictionary source_data;

		// Token: 0x04001D4E RID: 7502
		private string plain_text_source;

		// Token: 0x04001D4F RID: 7503
		private Image image_source;

		// Token: 0x04001D50 RID: 7504
		internal object Item;

		// Token: 0x04001D51 RID: 7505
		internal ArrayList Formats;

		// Token: 0x04001D52 RID: 7506
		internal bool Retrieving;

		// Token: 0x04001D53 RID: 7507
		internal bool Enumerating;

		// Token: 0x04001D54 RID: 7508
		internal XplatUI.ObjectToClipboard Converter;
	}
}
