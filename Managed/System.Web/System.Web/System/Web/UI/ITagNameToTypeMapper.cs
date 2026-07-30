using System;
using System.Collections;

namespace System.Web.UI
{
	// Token: 0x020001E0 RID: 480
	internal interface ITagNameToTypeMapper
	{
		// Token: 0x0600138E RID: 5006
		Type GetControlType(string tagName, IDictionary attribs);
	}
}
