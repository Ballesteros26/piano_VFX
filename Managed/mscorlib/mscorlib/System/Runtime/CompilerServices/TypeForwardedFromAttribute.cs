using System;

namespace System.Runtime.CompilerServices
{
	/// <summary>Specifies a source <see cref="T:System.Type" /> in another assembly. </summary>
	// Token: 0x0200085A RID: 2138
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface | AttributeTargets.Delegate, Inherited = false, AllowMultiple = false)]
	public sealed class TypeForwardedFromAttribute : Attribute
	{
		// Token: 0x06005429 RID: 21545 RVA: 0x00002180 File Offset: 0x00000380
		private TypeForwardedFromAttribute()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.CompilerServices.TypeForwardedFromAttribute" /> class. </summary>
		/// <param name="assemblyFullName">The source <see cref="T:System.Type" /> in another assembly. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyFullName" /> is null or empty.</exception>
		// Token: 0x0600542A RID: 21546 RVA: 0x00127187 File Offset: 0x00125387
		public TypeForwardedFromAttribute(string assemblyFullName)
		{
			if (string.IsNullOrEmpty(assemblyFullName))
			{
				throw new ArgumentNullException("assemblyFullName");
			}
			this.assemblyFullName = assemblyFullName;
		}

		/// <summary>Gets the assembly-qualified name of the source type.</summary>
		/// <returns>The assembly-qualified name of the source type.</returns>
		// Token: 0x17000EB4 RID: 3764
		// (get) Token: 0x0600542B RID: 21547 RVA: 0x001271A9 File Offset: 0x001253A9
		public string AssemblyFullName
		{
			get
			{
				return this.assemblyFullName;
			}
		}

		// Token: 0x04002BAE RID: 11182
		private string assemblyFullName;
	}
}
