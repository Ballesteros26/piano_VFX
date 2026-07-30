using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000893 RID: 2195
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface, AllowMultiple = true, Inherited = false)]
	internal sealed class TypeDependencyAttribute : Attribute
	{
		// Token: 0x06005473 RID: 21619 RVA: 0x001276D8 File Offset: 0x001258D8
		public TypeDependencyAttribute(string typeName)
		{
			if (typeName == null)
			{
				throw new ArgumentNullException("typeName");
			}
			this.typeName = typeName;
		}

		// Token: 0x04002BD4 RID: 11220
		private string typeName;
	}
}
