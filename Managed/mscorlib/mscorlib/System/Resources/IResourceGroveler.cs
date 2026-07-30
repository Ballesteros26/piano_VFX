using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;

namespace System.Resources
{
	// Token: 0x0200029D RID: 669
	internal interface IResourceGroveler
	{
		// Token: 0x06001EE5 RID: 7909
		ResourceSet GrovelForResourceSet(CultureInfo culture, Dictionary<string, ResourceSet> localResourceSets, bool tryParents, bool createIfNotExists, ref StackCrawlMark stackMark);

		// Token: 0x06001EE6 RID: 7910
		bool HasNeutralResources(CultureInfo culture, string defaultResName);
	}
}
