using System;
using System.Runtime.InteropServices;
using Unity;

namespace System.Threading
{
	/// <summary>Represents a handle that has been registered when calling <see cref="M:System.Threading.ThreadPool.RegisterWaitForSingleObject(System.Threading.WaitHandle,System.Threading.WaitOrTimerCallback,System.Object,System.UInt32,System.Boolean)" />. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020004AC RID: 1196
	[ComVisible(true)]
	public sealed class RegisteredWaitHandle : MarshalByRefObject
	{
		// Token: 0x0600381C RID: 14364 RVA: 0x000CBAD8 File Offset: 0x000C9CD8
		internal RegisteredWaitHandle(WaitHandle waitObject, WaitOrTimerCallback callback, object state, TimeSpan timeout, bool executeOnlyOnce)
		{
			this._waitObject = waitObject;
			this._callback = callback;
			this._state = state;
			this._timeout = timeout;
			this._executeOnlyOnce = executeOnlyOnce;
			this._finalEvent = null;
			this._cancelEvent = new ManualResetEvent(false);
			this._callsInProcess = 0;
			this._unregistered = false;
		}

		// Token: 0x0600381D RID: 14365 RVA: 0x000CBB34 File Offset: 0x000C9D34
		internal void Wait(object state)
		{
			bool flag = false;
			try
			{
				this._waitObject.SafeWaitHandle.DangerousAddRef(ref flag);
				RegisteredWaitHandle registeredWaitHandle;
				try
				{
					WaitHandle[] array = new WaitHandle[] { this._waitObject, this._cancelEvent };
					do
					{
						int num = WaitHandle.WaitAny(array, this._timeout, false);
						if (!this._unregistered)
						{
							registeredWaitHandle = this;
							lock (registeredWaitHandle)
							{
								this._callsInProcess++;
							}
							ThreadPool.QueueUserWorkItem(new WaitCallback(this.DoCallBack), num == 258);
						}
					}
					while (!this._unregistered && !this._executeOnlyOnce);
				}
				catch
				{
				}
				registeredWaitHandle = this;
				lock (registeredWaitHandle)
				{
					this._unregistered = true;
					if (this._callsInProcess == 0 && this._finalEvent != null)
					{
						NativeEventCalls.SetEvent(this._finalEvent.SafeWaitHandle);
					}
				}
			}
			catch (ObjectDisposedException)
			{
				if (flag)
				{
					throw;
				}
			}
			finally
			{
				if (flag)
				{
					this._waitObject.SafeWaitHandle.DangerousRelease();
				}
			}
		}

		// Token: 0x0600381E RID: 14366 RVA: 0x000CBC80 File Offset: 0x000C9E80
		private void DoCallBack(object timedOut)
		{
			try
			{
				if (this._callback != null)
				{
					this._callback(this._state, (bool)timedOut);
				}
			}
			finally
			{
				lock (this)
				{
					this._callsInProcess--;
					if (this._unregistered && this._callsInProcess == 0 && this._finalEvent != null)
					{
						NativeEventCalls.SetEvent(this._finalEvent.SafeWaitHandle);
					}
				}
			}
		}

		/// <summary>Cancels a registered wait operation issued by the <see cref="M:System.Threading.ThreadPool.RegisterWaitForSingleObject(System.Threading.WaitHandle,System.Threading.WaitOrTimerCallback,System.Object,System.UInt32,System.Boolean)" /> method.</summary>
		/// <returns>true if the function succeeds; otherwise, false.</returns>
		/// <param name="waitObject">The <see cref="T:System.Threading.WaitHandle" /> to be signaled. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600381F RID: 14367 RVA: 0x000CBD1C File Offset: 0x000C9F1C
		[ComVisible(true)]
		public bool Unregister(WaitHandle waitObject)
		{
			bool flag2;
			lock (this)
			{
				if (this._unregistered)
				{
					flag2 = false;
				}
				else
				{
					this._finalEvent = waitObject;
					this._unregistered = true;
					this._cancelEvent.Set();
					flag2 = true;
				}
			}
			return flag2;
		}

		// Token: 0x06003820 RID: 14368 RVA: 0x0001FB35 File Offset: 0x0001DD35
		internal RegisteredWaitHandle()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001D53 RID: 7507
		private WaitHandle _waitObject;

		// Token: 0x04001D54 RID: 7508
		private WaitOrTimerCallback _callback;

		// Token: 0x04001D55 RID: 7509
		private object _state;

		// Token: 0x04001D56 RID: 7510
		private WaitHandle _finalEvent;

		// Token: 0x04001D57 RID: 7511
		private ManualResetEvent _cancelEvent;

		// Token: 0x04001D58 RID: 7512
		private TimeSpan _timeout;

		// Token: 0x04001D59 RID: 7513
		private int _callsInProcess;

		// Token: 0x04001D5A RID: 7514
		private bool _executeOnlyOnce;

		// Token: 0x04001D5B RID: 7515
		private bool _unregistered;
	}
}
