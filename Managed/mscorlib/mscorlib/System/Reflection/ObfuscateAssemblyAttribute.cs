using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Instructs obfuscation tools to use their standard obfuscation rules for the appropriate assembly type.</summary>
	// Token: 0x020002F7 RID: 759
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
	[ComVisible(true)]
	public sealed class ObfuscateAssemblyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.ObfuscateAssemblyAttribute" /> class, specifying whether the assembly to be obfuscated is public or private.</summary>
		/// <param name="assemblyIsPrivate">true if the assembly is used within the scope of one application; otherwise, false.</param>
		// Token: 0x060020EE RID: 8430 RVA: 0x0007ECCE File Offset: 0x0007CECE
		public ObfuscateAssemblyAttribute(bool assemblyIsPrivate)
		{
			this.m_assemblyIsPrivate = assemblyIsPrivate;
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value indicating whether the assembly was marked private.</summary>
		/// <returns>true if the assembly was marked private; otherwise, false. </returns>
		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x060020EF RID: 8431 RVA: 0x0007ECE4 File Offset: 0x0007CEE4
		public bool AssemblyIsPrivate
		{
			get
			{
				return this.m_assemblyIsPrivate;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value indicating whether the obfuscation tool should remove the attribute after processing.</summary>
		/// <returns>true if the obfuscation tool should remove the attribute after processing; otherwise, false. The default value for this property is true.</returns>
		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x060020F0 RID: 8432 RVA: 0x0007ECEC File Offset: 0x0007CEEC
		// (set) Token: 0x060020F1 RID: 8433 RVA: 0x0007ECF4 File Offset: 0x0007CEF4
		public bool StripAfterObfuscation
		{
			get
			{
				return this.m_strip;
			}
			set
			{
				this.m_strip = value;
			}
		}

		// Token: 0x0400128B RID: 4747
		private bool m_assemblyIsPrivate;

		// Token: 0x0400128C RID: 4748
		private bool m_strip = true;
	}
}
