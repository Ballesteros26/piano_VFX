using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;

namespace System.Runtime.CompilerServices
{
	/// <summary>Generates debug information for lambda expressions in an expression tree.</summary>
	// Token: 0x020002FA RID: 762
	public abstract class DebugInfoGenerator
	{
		/// <summary>Creates a program database (PDB) symbol generator.</summary>
		/// <returns>A PDB symbol generator.</returns>
		// Token: 0x0600172E RID: 5934 RVA: 0x0004C349 File Offset: 0x0004A549
		public static DebugInfoGenerator CreatePdbGenerator()
		{
			throw new PlatformNotSupportedException();
		}

		/// <summary>Marks a sequence point in Microsoft intermediate language (MSIL) code.</summary>
		/// <param name="method">The lambda expression that is generated.</param>
		/// <param name="ilOffset">The offset within MSIL code at which to mark the sequence point.</param>
		/// <param name="sequencePoint">Debug information that corresponds to the sequence point.</param>
		// Token: 0x0600172F RID: 5935
		public abstract void MarkSequencePoint(LambdaExpression method, int ilOffset, DebugInfoExpression sequencePoint);

		// Token: 0x06001730 RID: 5936 RVA: 0x0004C350 File Offset: 0x0004A550
		internal virtual void MarkSequencePoint(LambdaExpression method, MethodBase methodBase, ILGenerator ilg, DebugInfoExpression sequencePoint)
		{
			this.MarkSequencePoint(method, ilg.ILOffset, sequencePoint);
		}

		// Token: 0x06001731 RID: 5937 RVA: 0x00003C4C File Offset: 0x00001E4C
		internal virtual void SetLocalName(LocalBuilder localBuilder, string name)
		{
		}
	}
}
