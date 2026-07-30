using System;

namespace System.Web.Util
{
	// Token: 0x0200011D RID: 285
	internal interface ITypedWebObjectFactory : IWebObjectFactory
	{
		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x06000E19 RID: 3609
		Type InstantiatedType { get; }
	}
}
