using System;
using System.Collections;

namespace System.Runtime.Serialization
{
	// Token: 0x020006F7 RID: 1783
	internal class SurrogateHashtable : Hashtable
	{
		// Token: 0x06004B13 RID: 19219 RVA: 0x0010C684 File Offset: 0x0010A884
		internal SurrogateHashtable(int size)
			: base(size)
		{
		}

		// Token: 0x06004B14 RID: 19220 RVA: 0x0010C690 File Offset: 0x0010A890
		protected override bool KeyEquals(object key, object item)
		{
			SurrogateKey surrogateKey = (SurrogateKey)item;
			SurrogateKey surrogateKey2 = (SurrogateKey)key;
			return surrogateKey2.m_type == surrogateKey.m_type && (surrogateKey2.m_context.m_state & surrogateKey.m_context.m_state) == surrogateKey.m_context.m_state && surrogateKey2.m_context.Context == surrogateKey.m_context.Context;
		}
	}
}
