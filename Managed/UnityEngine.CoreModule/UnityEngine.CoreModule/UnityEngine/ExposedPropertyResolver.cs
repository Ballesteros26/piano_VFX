using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x020000C0 RID: 192
	[NativeHeader("Runtime/Director/Core/ExposedPropertyTable.bindings.h")]
	[NativeHeader("Runtime/Utilities/PropertyName.h")]
	public struct ExposedPropertyResolver
	{
		// Token: 0x060004A3 RID: 1187 RVA: 0x00006DD0 File Offset: 0x00004FD0
		internal static Object ResolveReferenceInternal(IntPtr ptr, PropertyName name, out bool isValid)
		{
			bool flag = ptr == IntPtr.Zero;
			if (flag)
			{
				throw new ArgumentNullException("Argument \"ptr\" can't be null.");
			}
			return ExposedPropertyResolver.ResolveReferenceBindingsInternal(ptr, name, out isValid);
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x00006E04 File Offset: 0x00005004
		[FreeFunction("ExposedPropertyTableBindings::ResolveReferenceInternal")]
		private static Object ResolveReferenceBindingsInternal(IntPtr ptr, PropertyName name, out bool isValid)
		{
			return ExposedPropertyResolver.ResolveReferenceBindingsInternal_Injected(ptr, ref name, out isValid);
		}

		// Token: 0x060004A5 RID: 1189
		[MethodImpl(4096)]
		private static extern Object ResolveReferenceBindingsInternal_Injected(IntPtr ptr, ref PropertyName name, out bool isValid);

		// Token: 0x04000234 RID: 564
		internal IntPtr table;
	}
}
