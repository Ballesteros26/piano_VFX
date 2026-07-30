using System;
using System.Security;

namespace System.Threading
{
	// Token: 0x0200046B RID: 1131
	public sealed class AsyncLocal<T> : IAsyncLocal
	{
		// Token: 0x060035B7 RID: 13751 RVA: 0x00002111 File Offset: 0x00000311
		public AsyncLocal()
		{
		}

		// Token: 0x060035B8 RID: 13752 RVA: 0x000C6C32 File Offset: 0x000C4E32
		[SecurityCritical]
		public AsyncLocal(Action<AsyncLocalValueChangedArgs<T>> valueChangedHandler)
		{
			this.m_valueChangedHandler = valueChangedHandler;
		}

		// Token: 0x17000901 RID: 2305
		// (get) Token: 0x060035B9 RID: 13753 RVA: 0x000C6C44 File Offset: 0x000C4E44
		// (set) Token: 0x060035BA RID: 13754 RVA: 0x000C6C6B File Offset: 0x000C4E6B
		public T Value
		{
			[SecuritySafeCritical]
			get
			{
				object localValue = ExecutionContext.GetLocalValue(this);
				if (localValue != null)
				{
					return (T)((object)localValue);
				}
				return default(T);
			}
			[SecuritySafeCritical]
			set
			{
				ExecutionContext.SetLocalValue(this, value, this.m_valueChangedHandler != null);
			}
		}

		// Token: 0x060035BB RID: 13755 RVA: 0x000C6C84 File Offset: 0x000C4E84
		[SecurityCritical]
		void IAsyncLocal.OnValueChanged(object previousValueObj, object currentValueObj, bool contextChanged)
		{
			T t = ((previousValueObj == null) ? default(T) : ((T)((object)previousValueObj)));
			T t2 = ((currentValueObj == null) ? default(T) : ((T)((object)currentValueObj)));
			this.m_valueChangedHandler(new AsyncLocalValueChangedArgs<T>(t, t2, contextChanged));
		}

		// Token: 0x04001CA5 RID: 7333
		[SecurityCritical]
		private readonly Action<AsyncLocalValueChangedArgs<T>> m_valueChangedHandler;
	}
}
