using System;
using System.Reflection;

namespace System.Web.UI
{
	// Token: 0x02000180 RID: 384
	internal interface IScriptResourceMapping
	{
		// Token: 0x06000F8D RID: 3981
		IScriptResourceDefinition GetDefinition(string resourceName);

		// Token: 0x06000F8E RID: 3982
		IScriptResourceDefinition GetDefinition(string resourceName, Assembly resourceAssembly);
	}
}
