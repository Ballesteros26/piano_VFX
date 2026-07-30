using System;

namespace System.Collections.Generic
{
	// Token: 0x02000A4C RID: 2636
	[Serializable]
	internal sealed class InternalStringComparer : EqualityComparer<string>
	{
		// Token: 0x060060D2 RID: 24786 RVA: 0x0013DDB7 File Offset: 0x0013BFB7
		public override int GetHashCode(string obj)
		{
			if (obj == null)
			{
				return 0;
			}
			return obj.GetHashCode();
		}

		// Token: 0x060060D3 RID: 24787 RVA: 0x0013E930 File Offset: 0x0013CB30
		public override bool Equals(string x, string y)
		{
			if (x == null)
			{
				return y == null;
			}
			return x == y || x.Equals(y);
		}

		// Token: 0x060060D4 RID: 24788 RVA: 0x0013E948 File Offset: 0x0013CB48
		internal override int IndexOf(string[] array, string value, int startIndex, int count)
		{
			int num = startIndex + count;
			for (int i = startIndex; i < num; i++)
			{
				if (Array.UnsafeLoad<string>(array, i) == value)
				{
					return i;
				}
			}
			return -1;
		}
	}
}
