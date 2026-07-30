using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200018C RID: 396
	[UsedByNativeCode]
	public struct PropertyName : IEquatable<PropertyName>
	{
		// Token: 0x060012A0 RID: 4768 RVA: 0x0001E9F2 File Offset: 0x0001CBF2
		public PropertyName(string name)
		{
			this = new PropertyName(PropertyNameUtils.PropertyNameFromString(name));
		}

		// Token: 0x060012A1 RID: 4769 RVA: 0x0001EA02 File Offset: 0x0001CC02
		public PropertyName(PropertyName other)
		{
			this.id = other.id;
		}

		// Token: 0x060012A2 RID: 4770 RVA: 0x0001EA11 File Offset: 0x0001CC11
		public PropertyName(int id)
		{
			this.id = id;
		}

		// Token: 0x060012A3 RID: 4771 RVA: 0x0001EA1C File Offset: 0x0001CC1C
		public static bool IsNullOrEmpty(PropertyName prop)
		{
			return prop.id == 0;
		}

		// Token: 0x060012A4 RID: 4772 RVA: 0x0001EA38 File Offset: 0x0001CC38
		public static bool operator ==(PropertyName lhs, PropertyName rhs)
		{
			return lhs.id == rhs.id;
		}

		// Token: 0x060012A5 RID: 4773 RVA: 0x0001EA58 File Offset: 0x0001CC58
		public static bool operator !=(PropertyName lhs, PropertyName rhs)
		{
			return lhs.id != rhs.id;
		}

		// Token: 0x060012A6 RID: 4774 RVA: 0x0001EA7C File Offset: 0x0001CC7C
		public override int GetHashCode()
		{
			return this.id;
		}

		// Token: 0x060012A7 RID: 4775 RVA: 0x0001EA94 File Offset: 0x0001CC94
		public override bool Equals(object other)
		{
			return other is PropertyName && this.Equals((PropertyName)other);
		}

		// Token: 0x060012A8 RID: 4776 RVA: 0x0001EAC0 File Offset: 0x0001CCC0
		public bool Equals(PropertyName other)
		{
			return this == other;
		}

		// Token: 0x060012A9 RID: 4777 RVA: 0x0001EAE0 File Offset: 0x0001CCE0
		public static implicit operator PropertyName(string name)
		{
			return new PropertyName(name);
		}

		// Token: 0x060012AA RID: 4778 RVA: 0x0001EAF8 File Offset: 0x0001CCF8
		public static implicit operator PropertyName(int id)
		{
			return new PropertyName(id);
		}

		// Token: 0x060012AB RID: 4779 RVA: 0x0001EB10 File Offset: 0x0001CD10
		public override string ToString()
		{
			return string.Format("Unknown:{0}", this.id);
		}

		// Token: 0x0400062F RID: 1583
		internal int id;
	}
}
