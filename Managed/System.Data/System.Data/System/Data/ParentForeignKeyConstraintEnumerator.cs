using System;

namespace System.Data
{
	// Token: 0x0200005D RID: 93
	internal sealed class ParentForeignKeyConstraintEnumerator : ForeignKeyConstraintEnumerator
	{
		// Token: 0x06000303 RID: 771 RVA: 0x0001074A File Offset: 0x0000E94A
		public ParentForeignKeyConstraintEnumerator(DataSet dataSet, DataTable inTable)
			: base(dataSet)
		{
			this._table = inTable;
		}

		// Token: 0x06000304 RID: 772 RVA: 0x0001075A File Offset: 0x0000E95A
		protected override bool IsValidCandidate(Constraint constraint)
		{
			return constraint is ForeignKeyConstraint && ((ForeignKeyConstraint)constraint).RelatedTable == this._table;
		}

		// Token: 0x04000514 RID: 1300
		private readonly DataTable _table;
	}
}
