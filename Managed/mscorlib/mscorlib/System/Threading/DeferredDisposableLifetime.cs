using System;

namespace System.Threading
{
	// Token: 0x0200044B RID: 1099
	internal struct DeferredDisposableLifetime<T> where T : class, IDeferredDisposable
	{
		// Token: 0x060034A1 RID: 13473 RVA: 0x000C30E4 File Offset: 0x000C12E4
		static DeferredDisposableLifetime()
		{
			if (!Environment.IsRunningOnWindows)
			{
				throw new PlatformNotSupportedException();
			}
		}

		// Token: 0x060034A2 RID: 13474 RVA: 0x000C30F4 File Offset: 0x000C12F4
		public bool AddRef(T obj)
		{
			for (;;)
			{
				int num = Volatile.Read(ref this._count);
				if (num < 0)
				{
					break;
				}
				int num2 = checked(num + 1);
				if (Interlocked.CompareExchange(ref this._count, num2, num) == num)
				{
					return true;
				}
			}
			throw new ObjectDisposedException(typeof(T).ToString());
		}

		// Token: 0x060034A3 RID: 13475 RVA: 0x000C313C File Offset: 0x000C133C
		public void Release(T obj)
		{
			int num2;
			int num3;
			for (;;)
			{
				int num = Volatile.Read(ref this._count);
				if (num > 0)
				{
					num2 = num - 1;
					if (Interlocked.CompareExchange(ref this._count, num2, num) == num)
					{
						break;
					}
				}
				else
				{
					num3 = num + 1;
					if (Interlocked.CompareExchange(ref this._count, num3, num) == num)
					{
						goto Block_3;
					}
				}
			}
			if (num2 == 0)
			{
				obj.OnFinalRelease(false);
			}
			return;
			Block_3:
			if (num3 == -1)
			{
				obj.OnFinalRelease(true);
			}
		}

		// Token: 0x060034A4 RID: 13476 RVA: 0x000C31A4 File Offset: 0x000C13A4
		public void Dispose(T obj)
		{
			int num2;
			for (;;)
			{
				int num = Volatile.Read(ref this._count);
				if (num < 0)
				{
					break;
				}
				num2 = -1 - num;
				if (Interlocked.CompareExchange(ref this._count, num2, num) == num)
				{
					goto Block_1;
				}
			}
			return;
			Block_1:
			if (num2 == -1)
			{
				obj.OnFinalRelease(true);
			}
		}

		// Token: 0x04001C21 RID: 7201
		private int _count;
	}
}
