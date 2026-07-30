using System;
using System.Web.Configuration;

namespace System.Web
{
	// Token: 0x020000E5 RID: 229
	internal sealed class TraceManager
	{
		// Token: 0x06000C42 RID: 3138 RVA: 0x000210E8 File Offset: 0x0001F2E8
		public TraceManager()
		{
			try
			{
				this.mode = TraceMode.SortByTime;
				TraceSection traceSection = WebConfigurationManager.GetWebApplicationSection("system.web/trace") as TraceSection;
				if (traceSection == null)
				{
					traceSection = new TraceSection();
				}
				if (traceSection != null)
				{
					this.enabled = traceSection.Enabled;
					this.local_only = traceSection.LocalOnly;
					this.page_output = traceSection.PageOutput;
					if (traceSection.TraceMode == TraceDisplayMode.SortByTime)
					{
						this.mode = TraceMode.SortByTime;
					}
					else
					{
						this.mode = TraceMode.SortByCategory;
					}
					this.request_limit = traceSection.RequestLimit;
				}
			}
			catch (Exception ex)
			{
				this.initialException = ex;
			}
		}

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x06000C43 RID: 3139 RVA: 0x00021194 File Offset: 0x0001F394
		public bool HasException
		{
			get
			{
				return this.initialException != null;
			}
		}

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x06000C44 RID: 3140 RVA: 0x0002119F File Offset: 0x0001F39F
		public Exception InitialException
		{
			get
			{
				return this.initialException;
			}
		}

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x06000C45 RID: 3141 RVA: 0x000211A7 File Offset: 0x0001F3A7
		// (set) Token: 0x06000C46 RID: 3142 RVA: 0x000211AF File Offset: 0x0001F3AF
		public bool Enabled
		{
			get
			{
				return this.enabled;
			}
			set
			{
				this.enabled = value;
			}
		}

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x06000C47 RID: 3143 RVA: 0x000211B8 File Offset: 0x0001F3B8
		// (set) Token: 0x06000C48 RID: 3144 RVA: 0x000211C0 File Offset: 0x0001F3C0
		public bool LocalOnly
		{
			get
			{
				return this.local_only;
			}
			set
			{
				this.local_only = value;
			}
		}

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x06000C49 RID: 3145 RVA: 0x000211C9 File Offset: 0x0001F3C9
		// (set) Token: 0x06000C4A RID: 3146 RVA: 0x000211D1 File Offset: 0x0001F3D1
		public bool PageOutput
		{
			get
			{
				return this.page_output;
			}
			set
			{
				this.page_output = value;
			}
		}

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x06000C4B RID: 3147 RVA: 0x000211DA File Offset: 0x0001F3DA
		// (set) Token: 0x06000C4C RID: 3148 RVA: 0x000211E4 File Offset: 0x0001F3E4
		public int RequestLimit
		{
			get
			{
				return this.request_limit;
			}
			set
			{
				if (this.request_limit == value)
				{
					return;
				}
				TraceData[] array = new TraceData[value];
				Array.Copy(this.data, array, (this.cur_item > value) ? value : this.cur_item);
				if (this.cur_item > value)
				{
					this.cur_item = value;
				}
				this.request_limit = value;
			}
		}

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x06000C4D RID: 3149 RVA: 0x00021237 File Offset: 0x0001F437
		// (set) Token: 0x06000C4E RID: 3150 RVA: 0x0002123F File Offset: 0x0001F43F
		public TraceMode TraceMode
		{
			get
			{
				return this.mode;
			}
			set
			{
				this.mode = value;
			}
		}

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x06000C4F RID: 3151 RVA: 0x00021248 File Offset: 0x0001F448
		public TraceData[] TraceData
		{
			get
			{
				return this.data;
			}
		}

		// Token: 0x06000C50 RID: 3152 RVA: 0x00021250 File Offset: 0x0001F450
		public void AddTraceData(TraceData item)
		{
			if (this.data == null)
			{
				this.data = new TraceData[this.request_limit];
			}
			if (this.cur_item == this.request_limit)
			{
				return;
			}
			TraceData[] array = this.data;
			int num = this.cur_item;
			this.cur_item = num + 1;
			array[num] = item;
		}

		// Token: 0x06000C51 RID: 3153 RVA: 0x0002129E File Offset: 0x0001F49E
		public void Clear()
		{
			if (this.data == null)
			{
				return;
			}
			Array.Clear(this.data, 0, this.data.Length);
			this.cur_item = 0;
		}

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x06000C52 RID: 3154 RVA: 0x000212C4 File Offset: 0x0001F4C4
		public int ItemCount
		{
			get
			{
				return this.cur_item;
			}
		}

		// Token: 0x040010F1 RID: 4337
		private bool enabled;

		// Token: 0x040010F2 RID: 4338
		private bool local_only = true;

		// Token: 0x040010F3 RID: 4339
		private bool page_output;

		// Token: 0x040010F4 RID: 4340
		private TraceMode mode;

		// Token: 0x040010F5 RID: 4341
		private int request_limit = 10;

		// Token: 0x040010F6 RID: 4342
		private int cur_item;

		// Token: 0x040010F7 RID: 4343
		private TraceData[] data;

		// Token: 0x040010F8 RID: 4344
		private Exception initialException;
	}
}
