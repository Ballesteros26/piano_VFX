using System;
using System.Collections.Generic;

namespace System.Linq.Parallel
{
	// Token: 0x0200013C RID: 316
	internal struct ConcatKey<TLeftKey, TRightKey>
	{
		// Token: 0x060009CB RID: 2507 RVA: 0x00020203 File Offset: 0x0001E403
		private ConcatKey(TLeftKey leftKey, TRightKey rightKey, bool isLeft)
		{
			this._leftKey = leftKey;
			this._rightKey = rightKey;
			this._isLeft = isLeft;
		}

		// Token: 0x060009CC RID: 2508 RVA: 0x0002021C File Offset: 0x0001E41C
		internal static ConcatKey<TLeftKey, TRightKey> MakeLeft(TLeftKey leftKey)
		{
			return new ConcatKey<TLeftKey, TRightKey>(leftKey, default(TRightKey), true);
		}

		// Token: 0x060009CD RID: 2509 RVA: 0x0002023C File Offset: 0x0001E43C
		internal static ConcatKey<TLeftKey, TRightKey> MakeRight(TRightKey rightKey)
		{
			return new ConcatKey<TLeftKey, TRightKey>(default(TLeftKey), rightKey, false);
		}

		// Token: 0x060009CE RID: 2510 RVA: 0x00020259 File Offset: 0x0001E459
		internal static IComparer<ConcatKey<TLeftKey, TRightKey>> MakeComparer(IComparer<TLeftKey> leftComparer, IComparer<TRightKey> rightComparer)
		{
			return new ConcatKey<TLeftKey, TRightKey>.ConcatKeyComparer(leftComparer, rightComparer);
		}

		// Token: 0x040005F5 RID: 1525
		private readonly TLeftKey _leftKey;

		// Token: 0x040005F6 RID: 1526
		private readonly TRightKey _rightKey;

		// Token: 0x040005F7 RID: 1527
		private readonly bool _isLeft;

		// Token: 0x0200013D RID: 317
		private class ConcatKeyComparer : IComparer<ConcatKey<TLeftKey, TRightKey>>
		{
			// Token: 0x060009CF RID: 2511 RVA: 0x00020262 File Offset: 0x0001E462
			internal ConcatKeyComparer(IComparer<TLeftKey> leftComparer, IComparer<TRightKey> rightComparer)
			{
				this._leftComparer = leftComparer;
				this._rightComparer = rightComparer;
			}

			// Token: 0x060009D0 RID: 2512 RVA: 0x00020278 File Offset: 0x0001E478
			public int Compare(ConcatKey<TLeftKey, TRightKey> x, ConcatKey<TLeftKey, TRightKey> y)
			{
				if (x._isLeft != y._isLeft)
				{
					if (!x._isLeft)
					{
						return 1;
					}
					return -1;
				}
				else
				{
					if (x._isLeft)
					{
						return this._leftComparer.Compare(x._leftKey, y._leftKey);
					}
					return this._rightComparer.Compare(x._rightKey, y._rightKey);
				}
			}

			// Token: 0x040005F8 RID: 1528
			private IComparer<TLeftKey> _leftComparer;

			// Token: 0x040005F9 RID: 1529
			private IComparer<TRightKey> _rightComparer;
		}
	}
}
