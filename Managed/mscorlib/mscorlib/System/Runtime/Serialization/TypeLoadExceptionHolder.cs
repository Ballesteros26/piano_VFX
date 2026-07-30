using System;

namespace System.Runtime.Serialization
{
	// Token: 0x020006E1 RID: 1761
	internal class TypeLoadExceptionHolder
	{
		// Token: 0x06004A7E RID: 19070 RVA: 0x0010AD3F File Offset: 0x00108F3F
		internal TypeLoadExceptionHolder(string typeName)
		{
			this.m_typeName = typeName;
		}

		// Token: 0x17000C80 RID: 3200
		// (get) Token: 0x06004A7F RID: 19071 RVA: 0x0010AD4E File Offset: 0x00108F4E
		internal string TypeName
		{
			get
			{
				return this.m_typeName;
			}
		}

		// Token: 0x040026F4 RID: 9972
		private string m_typeName;
	}
}
