using System;
using System.Collections.Specialized;
using System.Globalization;
using System.Text;

namespace System.Net.Cache
{
	// Token: 0x020006B9 RID: 1721
	internal class RequestCacheEntry
	{
		// Token: 0x060035F1 RID: 13809 RVA: 0x000C5D80 File Offset: 0x000C3F80
		internal RequestCacheEntry()
		{
			this.m_ExpiresUtc = (this.m_LastAccessedUtc = (this.m_LastModifiedUtc = (this.m_LastSynchronizedUtc = DateTime.MinValue)));
		}

		// Token: 0x17000D04 RID: 3332
		// (get) Token: 0x060035F2 RID: 13810 RVA: 0x000C5DB9 File Offset: 0x000C3FB9
		// (set) Token: 0x060035F3 RID: 13811 RVA: 0x000C5DC1 File Offset: 0x000C3FC1
		internal bool IsPrivateEntry
		{
			get
			{
				return this.m_IsPrivateEntry;
			}
			set
			{
				this.m_IsPrivateEntry = value;
			}
		}

		// Token: 0x17000D05 RID: 3333
		// (get) Token: 0x060035F4 RID: 13812 RVA: 0x000C5DCA File Offset: 0x000C3FCA
		// (set) Token: 0x060035F5 RID: 13813 RVA: 0x000C5DD2 File Offset: 0x000C3FD2
		internal long StreamSize
		{
			get
			{
				return this.m_StreamSize;
			}
			set
			{
				this.m_StreamSize = value;
			}
		}

		// Token: 0x17000D06 RID: 3334
		// (get) Token: 0x060035F6 RID: 13814 RVA: 0x000C5DDB File Offset: 0x000C3FDB
		// (set) Token: 0x060035F7 RID: 13815 RVA: 0x000C5DE3 File Offset: 0x000C3FE3
		internal DateTime ExpiresUtc
		{
			get
			{
				return this.m_ExpiresUtc;
			}
			set
			{
				this.m_ExpiresUtc = value;
			}
		}

		// Token: 0x17000D07 RID: 3335
		// (get) Token: 0x060035F8 RID: 13816 RVA: 0x000C5DEC File Offset: 0x000C3FEC
		// (set) Token: 0x060035F9 RID: 13817 RVA: 0x000C5DF4 File Offset: 0x000C3FF4
		internal DateTime LastAccessedUtc
		{
			get
			{
				return this.m_LastAccessedUtc;
			}
			set
			{
				this.m_LastAccessedUtc = value;
			}
		}

		// Token: 0x17000D08 RID: 3336
		// (get) Token: 0x060035FA RID: 13818 RVA: 0x000C5DFD File Offset: 0x000C3FFD
		// (set) Token: 0x060035FB RID: 13819 RVA: 0x000C5E05 File Offset: 0x000C4005
		internal DateTime LastModifiedUtc
		{
			get
			{
				return this.m_LastModifiedUtc;
			}
			set
			{
				this.m_LastModifiedUtc = value;
			}
		}

		// Token: 0x17000D09 RID: 3337
		// (get) Token: 0x060035FC RID: 13820 RVA: 0x000C5E0E File Offset: 0x000C400E
		// (set) Token: 0x060035FD RID: 13821 RVA: 0x000C5E16 File Offset: 0x000C4016
		internal DateTime LastSynchronizedUtc
		{
			get
			{
				return this.m_LastSynchronizedUtc;
			}
			set
			{
				this.m_LastSynchronizedUtc = value;
			}
		}

		// Token: 0x17000D0A RID: 3338
		// (get) Token: 0x060035FE RID: 13822 RVA: 0x000C5E1F File Offset: 0x000C401F
		// (set) Token: 0x060035FF RID: 13823 RVA: 0x000C5E27 File Offset: 0x000C4027
		internal TimeSpan MaxStale
		{
			get
			{
				return this.m_MaxStale;
			}
			set
			{
				this.m_MaxStale = value;
			}
		}

		// Token: 0x17000D0B RID: 3339
		// (get) Token: 0x06003600 RID: 13824 RVA: 0x000C5E30 File Offset: 0x000C4030
		// (set) Token: 0x06003601 RID: 13825 RVA: 0x000C5E38 File Offset: 0x000C4038
		internal int HitCount
		{
			get
			{
				return this.m_HitCount;
			}
			set
			{
				this.m_HitCount = value;
			}
		}

		// Token: 0x17000D0C RID: 3340
		// (get) Token: 0x06003602 RID: 13826 RVA: 0x000C5E41 File Offset: 0x000C4041
		// (set) Token: 0x06003603 RID: 13827 RVA: 0x000C5E49 File Offset: 0x000C4049
		internal int UsageCount
		{
			get
			{
				return this.m_UsageCount;
			}
			set
			{
				this.m_UsageCount = value;
			}
		}

		// Token: 0x17000D0D RID: 3341
		// (get) Token: 0x06003604 RID: 13828 RVA: 0x000C5E52 File Offset: 0x000C4052
		// (set) Token: 0x06003605 RID: 13829 RVA: 0x000C5E5A File Offset: 0x000C405A
		internal bool IsPartialEntry
		{
			get
			{
				return this.m_IsPartialEntry;
			}
			set
			{
				this.m_IsPartialEntry = value;
			}
		}

		// Token: 0x17000D0E RID: 3342
		// (get) Token: 0x06003606 RID: 13830 RVA: 0x000C5E63 File Offset: 0x000C4063
		// (set) Token: 0x06003607 RID: 13831 RVA: 0x000C5E6B File Offset: 0x000C406B
		internal StringCollection EntryMetadata
		{
			get
			{
				return this.m_EntryMetadata;
			}
			set
			{
				this.m_EntryMetadata = value;
			}
		}

		// Token: 0x17000D0F RID: 3343
		// (get) Token: 0x06003608 RID: 13832 RVA: 0x000C5E74 File Offset: 0x000C4074
		// (set) Token: 0x06003609 RID: 13833 RVA: 0x000C5E7C File Offset: 0x000C407C
		internal StringCollection SystemMetadata
		{
			get
			{
				return this.m_SystemMetadata;
			}
			set
			{
				this.m_SystemMetadata = value;
			}
		}

		// Token: 0x0600360A RID: 13834 RVA: 0x000C5E88 File Offset: 0x000C4088
		internal virtual string ToString(bool verbose)
		{
			StringBuilder stringBuilder = new StringBuilder(512);
			stringBuilder.Append("\r\nIsPrivateEntry   = ").Append(this.IsPrivateEntry);
			stringBuilder.Append("\r\nIsPartialEntry   = ").Append(this.IsPartialEntry);
			stringBuilder.Append("\r\nStreamSize       = ").Append(this.StreamSize);
			stringBuilder.Append("\r\nExpires          = ").Append((this.ExpiresUtc == DateTime.MinValue) ? "" : this.ExpiresUtc.ToString("r", CultureInfo.CurrentCulture));
			stringBuilder.Append("\r\nLastAccessed     = ").Append((this.LastAccessedUtc == DateTime.MinValue) ? "" : this.LastAccessedUtc.ToString("r", CultureInfo.CurrentCulture));
			stringBuilder.Append("\r\nLastModified     = ").Append((this.LastModifiedUtc == DateTime.MinValue) ? "" : this.LastModifiedUtc.ToString("r", CultureInfo.CurrentCulture));
			stringBuilder.Append("\r\nLastSynchronized = ").Append((this.LastSynchronizedUtc == DateTime.MinValue) ? "" : this.LastSynchronizedUtc.ToString("r", CultureInfo.CurrentCulture));
			stringBuilder.Append("\r\nMaxStale(sec)    = ").Append((this.MaxStale == TimeSpan.MinValue) ? "" : ((int)this.MaxStale.TotalSeconds).ToString(NumberFormatInfo.CurrentInfo));
			stringBuilder.Append("\r\nHitCount         = ").Append(this.HitCount.ToString(NumberFormatInfo.CurrentInfo));
			stringBuilder.Append("\r\nUsageCount       = ").Append(this.UsageCount.ToString(NumberFormatInfo.CurrentInfo));
			stringBuilder.Append("\r\n");
			if (verbose)
			{
				stringBuilder.Append("EntryMetadata:\r\n");
				if (this.m_EntryMetadata != null)
				{
					foreach (string text in this.m_EntryMetadata)
					{
						stringBuilder.Append(text).Append("\r\n");
					}
				}
				stringBuilder.Append("---\r\nSystemMetadata:\r\n");
				if (this.m_SystemMetadata != null)
				{
					foreach (string text2 in this.m_SystemMetadata)
					{
						stringBuilder.Append(text2).Append("\r\n");
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04002AAD RID: 10925
		private bool m_IsPrivateEntry;

		// Token: 0x04002AAE RID: 10926
		private long m_StreamSize;

		// Token: 0x04002AAF RID: 10927
		private DateTime m_ExpiresUtc;

		// Token: 0x04002AB0 RID: 10928
		private int m_HitCount;

		// Token: 0x04002AB1 RID: 10929
		private DateTime m_LastAccessedUtc;

		// Token: 0x04002AB2 RID: 10930
		private DateTime m_LastModifiedUtc;

		// Token: 0x04002AB3 RID: 10931
		private DateTime m_LastSynchronizedUtc;

		// Token: 0x04002AB4 RID: 10932
		private TimeSpan m_MaxStale;

		// Token: 0x04002AB5 RID: 10933
		private int m_UsageCount;

		// Token: 0x04002AB6 RID: 10934
		private bool m_IsPartialEntry;

		// Token: 0x04002AB7 RID: 10935
		private StringCollection m_EntryMetadata;

		// Token: 0x04002AB8 RID: 10936
		private StringCollection m_SystemMetadata;
	}
}
