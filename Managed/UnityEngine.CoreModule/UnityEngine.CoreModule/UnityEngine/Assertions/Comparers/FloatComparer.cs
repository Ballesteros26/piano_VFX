using System;
using System.Collections.Generic;

namespace UnityEngine.Assertions.Comparers
{
	// Token: 0x020003E6 RID: 998
	public class FloatComparer : IEqualityComparer<float>
	{
		// Token: 0x060022B6 RID: 8886 RVA: 0x0003A63E File Offset: 0x0003883E
		public FloatComparer()
			: this(1E-05f, false)
		{
		}

		// Token: 0x060022B7 RID: 8887 RVA: 0x0003A64E File Offset: 0x0003884E
		public FloatComparer(bool relative)
			: this(1E-05f, relative)
		{
		}

		// Token: 0x060022B8 RID: 8888 RVA: 0x0003A65E File Offset: 0x0003885E
		public FloatComparer(float error)
			: this(error, false)
		{
		}

		// Token: 0x060022B9 RID: 8889 RVA: 0x0003A66A File Offset: 0x0003886A
		public FloatComparer(float error, bool relative)
		{
			this.m_Error = error;
			this.m_Relative = relative;
		}

		// Token: 0x060022BA RID: 8890 RVA: 0x0003A684 File Offset: 0x00038884
		public bool Equals(float a, float b)
		{
			return this.m_Relative ? FloatComparer.AreEqualRelative(a, b, this.m_Error) : FloatComparer.AreEqual(a, b, this.m_Error);
		}

		// Token: 0x060022BB RID: 8891 RVA: 0x0003A6BC File Offset: 0x000388BC
		public int GetHashCode(float obj)
		{
			return base.GetHashCode();
		}

		// Token: 0x060022BC RID: 8892 RVA: 0x0003A6D4 File Offset: 0x000388D4
		public static bool AreEqual(float expected, float actual, float error)
		{
			return Math.Abs(actual - expected) <= error;
		}

		// Token: 0x060022BD RID: 8893 RVA: 0x0003A6F4 File Offset: 0x000388F4
		public static bool AreEqualRelative(float expected, float actual, float error)
		{
			bool flag = expected == actual;
			bool flag2;
			if (flag)
			{
				flag2 = true;
			}
			else
			{
				float num = Math.Abs(expected);
				float num2 = Math.Abs(actual);
				float num3 = Math.Abs((actual - expected) / ((num > num2) ? num : num2));
				flag2 = num3 <= error;
			}
			return flag2;
		}

		// Token: 0x04000D03 RID: 3331
		private readonly float m_Error;

		// Token: 0x04000D04 RID: 3332
		private readonly bool m_Relative;

		// Token: 0x04000D05 RID: 3333
		public static readonly FloatComparer s_ComparerWithDefaultTolerance = new FloatComparer(1E-05f);

		// Token: 0x04000D06 RID: 3334
		public const float kEpsilon = 1E-05f;
	}
}
