using System;

namespace System.Web.Services.Description
{
	// Token: 0x020000D9 RID: 217
	internal abstract class MimeReflector
	{
		// Token: 0x06000587 RID: 1415
		internal abstract bool ReflectParameters();

		// Token: 0x06000588 RID: 1416
		internal abstract bool ReflectReturn();

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000589 RID: 1417 RVA: 0x00018D4E File Offset: 0x00016F4E
		// (set) Token: 0x0600058A RID: 1418 RVA: 0x00018D56 File Offset: 0x00016F56
		internal HttpProtocolReflector ReflectionContext
		{
			get
			{
				return this.protocol;
			}
			set
			{
				this.protocol = value;
			}
		}

		// Token: 0x04000399 RID: 921
		private HttpProtocolReflector protocol;
	}
}
