using System;

namespace System.Data
{
	// Token: 0x0200005C RID: 92
	internal sealed class ChildForeignKeyConstraintEnumerator : ForeignKeyConstraintEnumerator
	{
		// Token: 0x06000301 RID: 769 RVA: 0x0001071B File Offset: 0x0000E91B
		public ChildForeignKeyConstraintEnumerator(DataSet dataSet, DataTable inTable)
			: base(dataSet)
		{
			this._table = inTable;
		}

		// Token: 0x06000302 RID: 770 RVA: 0x0001072B File Offset: 0x0000E92B
		protected override bool IsValidCandidate(Constraint constraint)
		{
			return constraint is ForeignKeyConstraint && ((ForeignKeyConstraint)constraint).Table == this._table;
		}

		// Token: 0x04000513 RID: 1299
		private readonly DataTable _table;
	}
}
