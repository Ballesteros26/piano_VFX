using System;
using System.Collections.Specialized;

namespace System.Web.Mail
{
	// Token: 0x020000F6 RID: 246
	internal class MailHeader
	{
		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x06000D08 RID: 3336 RVA: 0x00023656 File Offset: 0x00021856
		// (set) Token: 0x06000D09 RID: 3337 RVA: 0x00023668 File Offset: 0x00021868
		public string To
		{
			get
			{
				return this.data["To"];
			}
			set
			{
				this.data["To"] = value;
			}
		}

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x06000D0A RID: 3338 RVA: 0x0002367B File Offset: 0x0002187B
		// (set) Token: 0x06000D0B RID: 3339 RVA: 0x0002368D File Offset: 0x0002188D
		public string From
		{
			get
			{
				return this.data["From"];
			}
			set
			{
				this.data["From"] = value;
			}
		}

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x06000D0C RID: 3340 RVA: 0x000236A0 File Offset: 0x000218A0
		// (set) Token: 0x06000D0D RID: 3341 RVA: 0x000236B2 File Offset: 0x000218B2
		public string Cc
		{
			get
			{
				return this.data["Cc"];
			}
			set
			{
				this.data["Cc"] = value;
			}
		}

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x06000D0E RID: 3342 RVA: 0x000236C5 File Offset: 0x000218C5
		// (set) Token: 0x06000D0F RID: 3343 RVA: 0x000236D7 File Offset: 0x000218D7
		public string Bcc
		{
			get
			{
				return this.data["Bcc"];
			}
			set
			{
				this.data["Bcc"] = value;
			}
		}

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x06000D10 RID: 3344 RVA: 0x000236EA File Offset: 0x000218EA
		// (set) Token: 0x06000D11 RID: 3345 RVA: 0x000236FC File Offset: 0x000218FC
		public string Subject
		{
			get
			{
				return this.data["Subject"];
			}
			set
			{
				this.data["Subject"] = value;
			}
		}

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x06000D12 RID: 3346 RVA: 0x0002370F File Offset: 0x0002190F
		// (set) Token: 0x06000D13 RID: 3347 RVA: 0x00023721 File Offset: 0x00021921
		public string Importance
		{
			get
			{
				return this.data["Importance"];
			}
			set
			{
				this.data["Importance"] = value;
			}
		}

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x06000D14 RID: 3348 RVA: 0x00023734 File Offset: 0x00021934
		// (set) Token: 0x06000D15 RID: 3349 RVA: 0x00023746 File Offset: 0x00021946
		public string Priority
		{
			get
			{
				return this.data["Priority"];
			}
			set
			{
				this.data["Priority"] = value;
			}
		}

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x06000D16 RID: 3350 RVA: 0x00023759 File Offset: 0x00021959
		// (set) Token: 0x06000D17 RID: 3351 RVA: 0x0002376B File Offset: 0x0002196B
		public string MimeVersion
		{
			get
			{
				return this.data["Mime-Version"];
			}
			set
			{
				this.data["Mime-Version"] = value;
			}
		}

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x06000D18 RID: 3352 RVA: 0x0002377E File Offset: 0x0002197E
		// (set) Token: 0x06000D19 RID: 3353 RVA: 0x00023790 File Offset: 0x00021990
		public string ContentType
		{
			get
			{
				return this.data["Content-Type"];
			}
			set
			{
				this.data["Content-Type"] = value;
			}
		}

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x06000D1A RID: 3354 RVA: 0x000237A3 File Offset: 0x000219A3
		// (set) Token: 0x06000D1B RID: 3355 RVA: 0x000237B5 File Offset: 0x000219B5
		public string ContentTransferEncoding
		{
			get
			{
				return this.data["Content-Transfer-Encoding"];
			}
			set
			{
				this.data["Content-Transfer-Encoding"] = value;
			}
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x06000D1C RID: 3356 RVA: 0x000237C8 File Offset: 0x000219C8
		// (set) Token: 0x06000D1D RID: 3357 RVA: 0x000237DA File Offset: 0x000219DA
		public string ContentDisposition
		{
			get
			{
				return this.data["Content-Disposition"];
			}
			set
			{
				this.data["Content-Disposition"] = value;
			}
		}

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x06000D1E RID: 3358 RVA: 0x000237ED File Offset: 0x000219ED
		// (set) Token: 0x06000D1F RID: 3359 RVA: 0x000237FF File Offset: 0x000219FF
		public string ContentBase
		{
			get
			{
				return this.data["Content-Base"];
			}
			set
			{
				this.data["Content-Base"] = value;
			}
		}

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x06000D20 RID: 3360 RVA: 0x00023812 File Offset: 0x00021A12
		// (set) Token: 0x06000D21 RID: 3361 RVA: 0x00023824 File Offset: 0x00021A24
		public string ContentLocation
		{
			get
			{
				return this.data["Content-Location"];
			}
			set
			{
				this.data["Content-Location"] = value;
			}
		}

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x06000D22 RID: 3362 RVA: 0x00023837 File Offset: 0x00021A37
		public NameValueCollection Data
		{
			get
			{
				return this.data;
			}
		}

		// Token: 0x0400112F RID: 4399
		protected NameValueCollection data = new NameValueCollection();
	}
}
