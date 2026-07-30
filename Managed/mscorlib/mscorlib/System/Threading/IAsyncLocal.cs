using System;
using System.Security;

namespace System.Threading
{
	// Token: 0x0200046C RID: 1132
	internal interface IAsyncLocal
	{
		// Token: 0x060035BC RID: 13756
		[SecurityCritical]
		void OnValueChanged(object previousValue, object currentValue, bool contextChanged);
	}
}
