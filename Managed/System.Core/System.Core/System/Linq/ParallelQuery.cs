using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Parallel;
using Unity;

namespace System.Linq
{
	/// <summary>Represents a parallel sequence.</summary>
	// Token: 0x02000095 RID: 149
	public class ParallelQuery : IEnumerable
	{
		// Token: 0x06000359 RID: 857 RVA: 0x000086D4 File Offset: 0x000068D4
		internal ParallelQuery(QuerySettings specifiedSettings)
		{
			this._specifiedSettings = specifiedSettings;
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x0600035A RID: 858 RVA: 0x000086E3 File Offset: 0x000068E3
		internal QuerySettings SpecifiedQuerySettings
		{
			get
			{
				return this._specifiedSettings;
			}
		}

		// Token: 0x0600035B RID: 859 RVA: 0x00003CCF File Offset: 0x00001ECF
		[ExcludeFromCodeCoverage]
		internal virtual ParallelQuery<TCastTo> Cast<TCastTo>()
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600035C RID: 860 RVA: 0x00003CCF File Offset: 0x00001ECF
		[ExcludeFromCodeCoverage]
		internal virtual ParallelQuery<TCastTo> OfType<TCastTo>()
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600035D RID: 861 RVA: 0x00003CCF File Offset: 0x00001ECF
		[ExcludeFromCodeCoverage]
		internal virtual IEnumerator GetEnumeratorUntyped()
		{
			throw new NotSupportedException();
		}

		/// <summary>Returns an enumerator that iterates through the sequence.</summary>
		/// <returns>An enumerator that iterates through the sequence.</returns>
		// Token: 0x0600035E RID: 862 RVA: 0x000086EB File Offset: 0x000068EB
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumeratorUntyped();
		}

		// Token: 0x0600035F RID: 863 RVA: 0x0000220F File Offset: 0x0000040F
		internal ParallelQuery()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000320 RID: 800
		private QuerySettings _specifiedSettings;
	}
}
