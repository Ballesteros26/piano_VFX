using System;
using System.Collections;

namespace System.Web.Util
{
	// Token: 0x02000126 RID: 294
	internal class AssemblySet : ObjectSet
	{
		// Token: 0x06000E32 RID: 3634 RVA: 0x000265DF File Offset: 0x000247DF
		internal AssemblySet()
		{
		}

		// Token: 0x06000E33 RID: 3635 RVA: 0x000265EF File Offset: 0x000247EF
		internal static AssemblySet Create(ICollection c)
		{
			AssemblySet assemblySet = new AssemblySet();
			assemblySet.AddCollection(c);
			return assemblySet;
		}
	}
}
