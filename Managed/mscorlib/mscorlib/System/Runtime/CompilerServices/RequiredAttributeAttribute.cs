using System;
using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
	/// <summary>Specifies that an importing compiler must fully understand the semantics of a type definition, or refuse to use it.  This class cannot be inherited. </summary>
	// Token: 0x0200088E RID: 2190
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class RequiredAttributeAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.CompilerServices.RequiredAttributeAttribute" /> class.</summary>
		/// <param name="requiredContract">A type that an importing compiler must fully understand.This parameter is not supported in the .NET Framework version 2.0 and later. </param>
		// Token: 0x0600546D RID: 21613 RVA: 0x001276C1 File Offset: 0x001258C1
		public RequiredAttributeAttribute(Type requiredContract)
		{
			this.requiredContract = requiredContract;
		}

		/// <summary>Gets a type that an importing compiler must fully understand.</summary>
		/// <returns>A type that an importing compiler must fully understand. </returns>
		// Token: 0x17000EC6 RID: 3782
		// (get) Token: 0x0600546E RID: 21614 RVA: 0x001276D0 File Offset: 0x001258D0
		public Type RequiredContract
		{
			get
			{
				return this.requiredContract;
			}
		}

		// Token: 0x04002BD3 RID: 11219
		private Type requiredContract;
	}
}
