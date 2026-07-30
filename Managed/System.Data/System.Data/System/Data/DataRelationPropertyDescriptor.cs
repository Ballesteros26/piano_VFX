using System;
using System.ComponentModel;

namespace System.Data
{
	// Token: 0x0200007A RID: 122
	internal sealed class DataRelationPropertyDescriptor : PropertyDescriptor
	{
		// Token: 0x060005D6 RID: 1494 RVA: 0x00017FCA File Offset: 0x000161CA
		internal DataRelationPropertyDescriptor(DataRelation dataRelation)
			: base(dataRelation.RelationName, null)
		{
			this.Relation = dataRelation;
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x060005D7 RID: 1495 RVA: 0x00017FE0 File Offset: 0x000161E0
		internal DataRelation Relation { get; }

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x060005D8 RID: 1496 RVA: 0x00013FD2 File Offset: 0x000121D2
		public override Type ComponentType
		{
			get
			{
				return typeof(DataRowView);
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x060005D9 RID: 1497 RVA: 0x000061D5 File Offset: 0x000043D5
		public override bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x060005DA RID: 1498 RVA: 0x00017FE8 File Offset: 0x000161E8
		public override Type PropertyType
		{
			get
			{
				return typeof(IBindingList);
			}
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x00017FF4 File Offset: 0x000161F4
		public override bool Equals(object other)
		{
			return other is DataRelationPropertyDescriptor && ((DataRelationPropertyDescriptor)other).Relation == this.Relation;
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x00018013 File Offset: 0x00016213
		public override int GetHashCode()
		{
			return this.Relation.GetHashCode();
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x000061D5 File Offset: 0x000043D5
		public override bool CanResetValue(object component)
		{
			return false;
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x00018020 File Offset: 0x00016220
		public override object GetValue(object component)
		{
			return ((DataRowView)component).CreateChildView(this.Relation);
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x00005E03 File Offset: 0x00004003
		public override void ResetValue(object component)
		{
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x00005E03 File Offset: 0x00004003
		public override void SetValue(object component, object value)
		{
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x000061D5 File Offset: 0x000043D5
		public override bool ShouldSerializeValue(object component)
		{
			return false;
		}
	}
}
