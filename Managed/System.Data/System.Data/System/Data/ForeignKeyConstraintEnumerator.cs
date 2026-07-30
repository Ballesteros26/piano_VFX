using System;

namespace System.Data
{
	// Token: 0x0200005B RID: 91
	internal class ForeignKeyConstraintEnumerator : ConstraintEnumerator
	{
		// Token: 0x060002FE RID: 766 RVA: 0x000106FA File Offset: 0x0000E8FA
		public ForeignKeyConstraintEnumerator(DataSet dataSet)
			: base(dataSet)
		{
		}

		// Token: 0x060002FF RID: 767 RVA: 0x00010703 File Offset: 0x0000E903
		protected override bool IsValidCandidate(Constraint constraint)
		{
			return constraint is ForeignKeyConstraint;
		}

		// Token: 0x06000300 RID: 768 RVA: 0x0001070E File Offset: 0x0000E90E
		public ForeignKeyConstraint GetForeignKeyConstraint()
		{
			return (ForeignKeyConstraint)base.CurrentObject;
		}
	}
}
