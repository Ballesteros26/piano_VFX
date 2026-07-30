using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200018B RID: 395
	[NativeHeader("Runtime/Utilities/PropertyName.h")]
	internal class PropertyNameUtils
	{
		// Token: 0x0600129D RID: 4765 RVA: 0x0001E9DC File Offset: 0x0001CBDC
		[FreeFunction]
		public static PropertyName PropertyNameFromString([Unmarshalled] string name)
		{
			PropertyName propertyName;
			PropertyNameUtils.PropertyNameFromString_Injected(name, out propertyName);
			return propertyName;
		}

		// Token: 0x0600129F RID: 4767
		[MethodImpl(4096)]
		private static extern void PropertyNameFromString_Injected(string name, out PropertyName ret);
	}
}
