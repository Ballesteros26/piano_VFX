using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000019 RID: 25
	public struct EventDispatcherGate : IDisposable, IEquatable<EventDispatcherGate>
	{
		// Token: 0x06000076 RID: 118 RVA: 0x00003608 File Offset: 0x00001808
		public EventDispatcherGate(EventDispatcher d)
		{
			bool flag = d == null;
			if (flag)
			{
				throw new ArgumentNullException("d");
			}
			this.m_Dispatcher = d;
			this.m_Dispatcher.CloseGate();
		}

		// Token: 0x06000077 RID: 119 RVA: 0x0000363D File Offset: 0x0000183D
		public void Dispose()
		{
			this.m_Dispatcher.OpenGate();
		}

		// Token: 0x06000078 RID: 120 RVA: 0x0000364C File Offset: 0x0000184C
		public bool Equals(EventDispatcherGate other)
		{
			return object.Equals(this.m_Dispatcher, other.m_Dispatcher);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003670 File Offset: 0x00001870
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is EventDispatcherGate && this.Equals((EventDispatcherGate)obj);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x000036A8 File Offset: 0x000018A8
		public override int GetHashCode()
		{
			return (this.m_Dispatcher != null) ? this.m_Dispatcher.GetHashCode() : 0;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x000036D0 File Offset: 0x000018D0
		public static bool operator ==(EventDispatcherGate left, EventDispatcherGate right)
		{
			return left.Equals(right);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x000036EC File Offset: 0x000018EC
		public static bool operator !=(EventDispatcherGate left, EventDispatcherGate right)
		{
			return !left.Equals(right);
		}

		// Token: 0x04000038 RID: 56
		private readonly EventDispatcher m_Dispatcher;
	}
}
