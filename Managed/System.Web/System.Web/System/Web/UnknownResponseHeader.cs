using System;
using System.Web.Util;

namespace System.Web
{
	// Token: 0x020000AA RID: 170
	internal sealed class UnknownResponseHeader : BaseResponseHeader
	{
		// Token: 0x17000377 RID: 887
		// (get) Token: 0x060008F9 RID: 2297 RVA: 0x0001602F File Offset: 0x0001422F
		// (set) Token: 0x060008FA RID: 2298 RVA: 0x00016038 File Offset: 0x00014238
		public string Name
		{
			get
			{
				return this.headerName;
			}
			set
			{
				string text;
				string text2;
				HttpEncoder.Current.HeaderNameValueEncode(value, null, out text, out text2);
				this.headerName = text;
			}
		}

		// Token: 0x060008FB RID: 2299 RVA: 0x0001605C File Offset: 0x0001425C
		public UnknownResponseHeader(string name, string val)
			: base(val)
		{
			this.Name = name;
		}

		// Token: 0x060008FC RID: 2300 RVA: 0x0001606C File Offset: 0x0001426C
		internal override void SendContent(HttpWorkerRequest wr)
		{
			wr.SendUnknownResponseHeader(this.Name, base.Value);
		}

		// Token: 0x04000FED RID: 4077
		private string headerName;
	}
}
