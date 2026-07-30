using System;

namespace System.Runtime.Serialization
{
	// Token: 0x020006F6 RID: 1782
	[Serializable]
	internal class SurrogateKey
	{
		// Token: 0x06004B11 RID: 19217 RVA: 0x0010C661 File Offset: 0x0010A861
		internal SurrogateKey(Type type, StreamingContext context)
		{
			this.m_type = type;
			this.m_context = context;
		}

		// Token: 0x06004B12 RID: 19218 RVA: 0x0010C677 File Offset: 0x0010A877
		public override int GetHashCode()
		{
			return this.m_type.GetHashCode();
		}

		// Token: 0x04002731 RID: 10033
		internal Type m_type;

		// Token: 0x04002732 RID: 10034
		internal StreamingContext m_context;
	}
}
