using System;
using System.Globalization;
using Unity;

namespace System.Web.Globalization
{
	// Token: 0x02000776 RID: 1910
	public sealed class ResourceFileStringLocalizerProvider : IStringLocalizerProvider
	{
		// Token: 0x06004D68 RID: 19816 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public ResourceFileStringLocalizerProvider()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x06004D69 RID: 19817 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public string GetLocalizedString(CultureInfo culture, string name, object[] arguments)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x040025EB RID: 9707
		public const string ResourceFileName = "DataAnnotation.Localization";
	}
}
