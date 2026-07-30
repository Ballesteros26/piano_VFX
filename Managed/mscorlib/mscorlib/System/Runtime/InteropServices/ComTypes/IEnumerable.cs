using System;
using System.Collections;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x02000979 RID: 2425
	[Guid("496B0ABE-CDEE-11d3-88E8-00902754C43A")]
	internal interface IEnumerable
	{
		// Token: 0x060059B9 RID: 22969
		[DispId(-4)]
		IEnumerator GetEnumerator();
	}
}
