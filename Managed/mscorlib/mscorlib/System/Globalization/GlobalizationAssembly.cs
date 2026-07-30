using System;
using System.IO;
using System.Reflection;
using System.Security;

namespace System.Globalization
{
	// Token: 0x0200040E RID: 1038
	internal sealed class GlobalizationAssembly
	{
		// Token: 0x06003155 RID: 12629 RVA: 0x000B1798 File Offset: 0x000AF998
		[SecurityCritical]
		internal unsafe static byte* GetGlobalizationResourceBytePtr(Assembly assembly, string tableName)
		{
			UnmanagedMemoryStream unmanagedMemoryStream = assembly.GetManifestResourceStream(tableName) as UnmanagedMemoryStream;
			if (unmanagedMemoryStream != null)
			{
				byte* positionPointer = unmanagedMemoryStream.PositionPointer;
				if (positionPointer != null)
				{
					return positionPointer;
				}
			}
			throw new InvalidOperationException();
		}
	}
}
