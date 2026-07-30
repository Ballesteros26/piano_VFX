using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Specifies a bitwise combination of <see cref="T:System.Reflection.AssemblyNameFlags" /> flags for an assembly, describing just-in-time (JIT) compiler options, whether the assembly is retargetable, and whether it has a full or tokenized public key. This class cannot be inherited.</summary>
	// Token: 0x020002D0 RID: 720
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class AssemblyFlagsAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AssemblyFlagsAttribute" /> class with the specified combination of <see cref="T:System.Reflection.AssemblyNameFlags" /> flags, cast as an unsigned integer value.</summary>
		/// <param name="flags">A bitwise combination of <see cref="T:System.Reflection.AssemblyNameFlags" /> flags, cast as an unsigned integer value, representing just-in-time (JIT) compiler options, longevity, whether an assembly is retargetable, and whether it has a full or tokenized public key.</param>
		// Token: 0x0600203E RID: 8254 RVA: 0x0007DF49 File Offset: 0x0007C149
		[Obsolete("This constructor has been deprecated. Please use AssemblyFlagsAttribute(AssemblyNameFlags) instead. http://go.microsoft.com/fwlink/?linkid=14202")]
		[CLSCompliant(false)]
		public AssemblyFlagsAttribute(uint flags)
		{
			this.m_flags = (AssemblyNameFlags)flags;
		}

		/// <summary>Gets an unsigned integer value representing the combination of <see cref="T:System.Reflection.AssemblyNameFlags" /> flags specified when this attribute instance was created.</summary>
		/// <returns>An unsigned integer value representing a bitwise combination of <see cref="T:System.Reflection.AssemblyNameFlags" /> flags.</returns>
		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x0600203F RID: 8255 RVA: 0x0007DF58 File Offset: 0x0007C158
		[CLSCompliant(false)]
		[Obsolete("This property has been deprecated. Please use AssemblyFlags instead. http://go.microsoft.com/fwlink/?linkid=14202")]
		public uint Flags
		{
			get
			{
				return (uint)this.m_flags;
			}
		}

		/// <summary>Gets an integer value representing the combination of <see cref="T:System.Reflection.AssemblyNameFlags" /> flags specified when this attribute instance was created.</summary>
		/// <returns>An integer value representing a bitwise combination of <see cref="T:System.Reflection.AssemblyNameFlags" /> flags.</returns>
		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x06002040 RID: 8256 RVA: 0x0007DF58 File Offset: 0x0007C158
		public int AssemblyFlags
		{
			get
			{
				return (int)this.m_flags;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AssemblyFlagsAttribute" /> class with the specified combination of <see cref="T:System.Reflection.AssemblyNameFlags" /> flags, cast as an integer value.</summary>
		/// <param name="assemblyFlags">A bitwise combination of <see cref="T:System.Reflection.AssemblyNameFlags" /> flags, cast as an integer value, representing just-in-time (JIT) compiler options, longevity, whether an assembly is retargetable, and whether it has a full or tokenized public key.</param>
		// Token: 0x06002041 RID: 8257 RVA: 0x0007DF49 File Offset: 0x0007C149
		[Obsolete("This constructor has been deprecated. Please use AssemblyFlagsAttribute(AssemblyNameFlags) instead. http://go.microsoft.com/fwlink/?linkid=14202")]
		public AssemblyFlagsAttribute(int assemblyFlags)
		{
			this.m_flags = (AssemblyNameFlags)assemblyFlags;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AssemblyFlagsAttribute" /> class with the specified combination of <see cref="T:System.Reflection.AssemblyNameFlags" /> flags.</summary>
		/// <param name="assemblyFlags">A bitwise combination of <see cref="T:System.Reflection.AssemblyNameFlags" /> flags representing just-in-time (JIT) compiler options, longevity, whether an assembly is retargetable, and whether it has a full or tokenized public key.</param>
		// Token: 0x06002042 RID: 8258 RVA: 0x0007DF49 File Offset: 0x0007C149
		public AssemblyFlagsAttribute(AssemblyNameFlags assemblyFlags)
		{
			this.m_flags = assemblyFlags;
		}

		// Token: 0x04001170 RID: 4464
		private AssemblyNameFlags m_flags;
	}
}
