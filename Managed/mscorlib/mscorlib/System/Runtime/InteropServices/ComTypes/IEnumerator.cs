using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x0200097A RID: 2426
	[Guid("496B0ABF-CDEE-11d3-88E8-00902754C43A")]
	internal interface IEnumerator
	{
		// Token: 0x060059BA RID: 22970
		bool MoveNext();

		// Token: 0x17000FBB RID: 4027
		// (get) Token: 0x060059BB RID: 22971
		object Current { get; }

		// Token: 0x060059BC RID: 22972
		void Reset();
	}
}
