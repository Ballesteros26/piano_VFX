using System;
using System.IO;

namespace System.Web.Services.Protocols
{
	// Token: 0x02000090 RID: 144
	internal class UnsupportedRequestProtocol : ServerProtocol
	{
		// Token: 0x060003C9 RID: 969 RVA: 0x00011F68 File Offset: 0x00010168
		internal UnsupportedRequestProtocol(int httpCode)
		{
			this.httpCode = httpCode;
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060003CA RID: 970 RVA: 0x00011F77 File Offset: 0x00010177
		internal int HttpCode
		{
			get
			{
				return this.httpCode;
			}
		}

		// Token: 0x060003CB RID: 971 RVA: 0x00002B54 File Offset: 0x00000D54
		internal override bool Initialize()
		{
			return true;
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060003CC RID: 972 RVA: 0x00002B51 File Offset: 0x00000D51
		internal override bool IsOneWay
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060003CD RID: 973 RVA: 0x00006C2F File Offset: 0x00004E2F
		internal override LogicalMethodInfo MethodInfo
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060003CE RID: 974 RVA: 0x00006C2F File Offset: 0x00004E2F
		internal override ServerType ServerType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060003CF RID: 975 RVA: 0x00004419 File Offset: 0x00002619
		internal override object[] ReadParameters()
		{
			return new object[0];
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x0000210D File Offset: 0x0000030D
		internal override void WriteReturns(object[] returnValues, Stream outputStream)
		{
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x00002B51 File Offset: 0x00000D51
		internal override bool WriteException(Exception e, Stream outputStream)
		{
			return false;
		}

		// Token: 0x0400030F RID: 783
		private int httpCode;
	}
}
