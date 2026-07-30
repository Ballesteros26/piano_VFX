using System;
using System.Configuration.Assemblies;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Specifies an algorithm to hash all files in an assembly. This class cannot be inherited.</summary>
	// Token: 0x020002CF RID: 719
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class AssemblyAlgorithmIdAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AssemblyAlgorithmIdAttribute" /> class with the specified hash algorithm, using one of the members of <see cref="T:System.Configuration.Assemblies.AssemblyHashAlgorithm" /> to represent the hash algorithm.</summary>
		/// <param name="algorithmId">A member of AssemblyHashAlgorithm that represents the hash algorithm. </param>
		// Token: 0x0600203B RID: 8251 RVA: 0x0007DF32 File Offset: 0x0007C132
		public AssemblyAlgorithmIdAttribute(AssemblyHashAlgorithm algorithmId)
		{
			this.m_algId = (uint)algorithmId;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AssemblyAlgorithmIdAttribute" /> class with the specified hash algorithm, using an unsigned integer to represent the hash algorithm.</summary>
		/// <param name="algorithmId">An unsigned integer representing the hash algorithm. </param>
		// Token: 0x0600203C RID: 8252 RVA: 0x0007DF32 File Offset: 0x0007C132
		[CLSCompliant(false)]
		public AssemblyAlgorithmIdAttribute(uint algorithmId)
		{
			this.m_algId = algorithmId;
		}

		/// <summary>Gets the hash algorithm of an assembly manifest's contents.</summary>
		/// <returns>An unsigned integer representing the assembly hash algorithm.</returns>
		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x0600203D RID: 8253 RVA: 0x0007DF41 File Offset: 0x0007C141
		[CLSCompliant(false)]
		public uint AlgorithmId
		{
			get
			{
				return this.m_algId;
			}
		}

		// Token: 0x0400116F RID: 4463
		private uint m_algId;
	}
}
