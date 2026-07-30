using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x02000390 RID: 912
	internal sealed class ConstraintStruct
	{
		// Token: 0x17000757 RID: 1879
		// (get) Token: 0x060024E9 RID: 9449 RVA: 0x000DF768 File Offset: 0x000DD968
		internal int TableDim
		{
			get
			{
				return this.tableDim;
			}
		}

		// Token: 0x060024EA RID: 9450 RVA: 0x000DF770 File Offset: 0x000DD970
		internal ConstraintStruct(CompiledIdentityConstraint constraint)
		{
			this.constraint = constraint;
			this.tableDim = constraint.Fields.Length;
			this.axisFields = new ArrayList();
			this.axisSelector = new SelectorActiveAxis(constraint.Selector, this);
			if (this.constraint.Role != CompiledIdentityConstraint.ConstraintRole.Keyref)
			{
				this.qualifiedTable = new Hashtable();
			}
		}

		// Token: 0x04001904 RID: 6404
		internal CompiledIdentityConstraint constraint;

		// Token: 0x04001905 RID: 6405
		internal SelectorActiveAxis axisSelector;

		// Token: 0x04001906 RID: 6406
		internal ArrayList axisFields;

		// Token: 0x04001907 RID: 6407
		internal Hashtable qualifiedTable;

		// Token: 0x04001908 RID: 6408
		internal Hashtable keyrefTable;

		// Token: 0x04001909 RID: 6409
		private int tableDim;
	}
}
