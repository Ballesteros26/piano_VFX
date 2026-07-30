using System;
using System.Collections;

namespace System.Data
{
	// Token: 0x0200005A RID: 90
	internal class ConstraintEnumerator
	{
		// Token: 0x060002F9 RID: 761 RVA: 0x00010635 File Offset: 0x0000E835
		public ConstraintEnumerator(DataSet dataSet)
		{
			this._tables = ((dataSet != null) ? dataSet.Tables.GetEnumerator() : null);
			this._currentObject = null;
		}

		// Token: 0x060002FA RID: 762 RVA: 0x0001065C File Offset: 0x0000E85C
		public bool GetNext()
		{
			this._currentObject = null;
			while (this._tables != null)
			{
				if (this._constraints == null)
				{
					if (!this._tables.MoveNext())
					{
						this._tables = null;
						return false;
					}
					this._constraints = ((DataTable)this._tables.Current).Constraints.GetEnumerator();
				}
				if (!this._constraints.MoveNext())
				{
					this._constraints = null;
				}
				else
				{
					Constraint constraint = (Constraint)this._constraints.Current;
					if (this.IsValidCandidate(constraint))
					{
						this._currentObject = constraint;
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060002FB RID: 763 RVA: 0x000106F2 File Offset: 0x0000E8F2
		public Constraint GetConstraint()
		{
			return this._currentObject;
		}

		// Token: 0x060002FC RID: 764 RVA: 0x0000EF2B File Offset: 0x0000D12B
		protected virtual bool IsValidCandidate(Constraint constraint)
		{
			return true;
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060002FD RID: 765 RVA: 0x000106F2 File Offset: 0x0000E8F2
		protected Constraint CurrentObject
		{
			get
			{
				return this._currentObject;
			}
		}

		// Token: 0x04000510 RID: 1296
		private IEnumerator _tables;

		// Token: 0x04000511 RID: 1297
		private IEnumerator _constraints;

		// Token: 0x04000512 RID: 1298
		private Constraint _currentObject;
	}
}
