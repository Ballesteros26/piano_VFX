using System;

namespace System.Runtime
{
	/// <summary>Specifies patch band information for targeted patching of the .NET Framework.</summary>
	// Token: 0x020006B3 RID: 1715
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class AssemblyTargetedPatchBandAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.AssemblyTargetedPatchBandAttribute" /> class.</summary>
		/// <param name="targetedPatchBand">The patch band.</param>
		// Token: 0x0600495E RID: 18782 RVA: 0x0010799C File Offset: 0x00105B9C
		public AssemblyTargetedPatchBandAttribute(string targetedPatchBand)
		{
			this.m_targetedPatchBand = targetedPatchBand;
		}

		/// <summary>Gets the patch band. </summary>
		/// <returns>The patch band information.</returns>
		// Token: 0x17000C4F RID: 3151
		// (get) Token: 0x0600495F RID: 18783 RVA: 0x001079AB File Offset: 0x00105BAB
		public string TargetedPatchBand
		{
			get
			{
				return this.m_targetedPatchBand;
			}
		}

		// Token: 0x04002670 RID: 9840
		private string m_targetedPatchBand;
	}
}
