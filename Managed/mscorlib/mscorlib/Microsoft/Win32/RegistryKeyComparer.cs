using System;
using System.Collections;

namespace Microsoft.Win32
{
	// Token: 0x020000B6 RID: 182
	internal class RegistryKeyComparer : IEqualityComparer
	{
		// Token: 0x060005C6 RID: 1478 RVA: 0x0001FC14 File Offset: 0x0001DE14
		public bool Equals(object x, object y)
		{
			return RegistryKey.IsEquals((RegistryKey)x, (RegistryKey)y);
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x0001FC28 File Offset: 0x0001DE28
		public int GetHashCode(object obj)
		{
			string name = ((RegistryKey)obj).Name;
			if (name == null)
			{
				return 0;
			}
			return name.GetHashCode();
		}
	}
}
