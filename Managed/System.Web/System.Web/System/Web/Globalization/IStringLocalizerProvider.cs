using System;
using System.Globalization;

namespace System.Web.Globalization
{
	// Token: 0x02000775 RID: 1909
	public interface IStringLocalizerProvider
	{
		// Token: 0x06004D67 RID: 19815
		string GetLocalizedString(CultureInfo culture, string name, object[] arguments);
	}
}
