using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using Microsoft.Win32.SafeHandles;

namespace System.Net.Sockets
{
	// Token: 0x020005DF RID: 1503
	internal sealed class SafeSocketHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06002F98 RID: 12184 RVA: 0x000BBF91 File Offset: 0x000BA191
		public SafeSocketHandle(IntPtr preexistingHandle, bool ownsHandle)
			: base(ownsHandle)
		{
			base.SetHandle(preexistingHandle);
			if (SafeSocketHandle.THROW_ON_ABORT_RETRIES)
			{
				this.threads_stacktraces = new Dictionary<Thread, StackTrace>();
			}
		}

		// Token: 0x06002F99 RID: 12185 RVA: 0x0000F070 File Offset: 0x0000D270
		internal SafeSocketHandle()
			: base(true)
		{
		}

		// Token: 0x06002F9A RID: 12186 RVA: 0x000BBFB4 File Offset: 0x000BA1B4
		protected override bool ReleaseHandle()
		{
			int num = 0;
			Socket.Blocking_internal(this.handle, false, out num);
			if (this.blocking_threads != null)
			{
				List<Thread> list = this.blocking_threads;
				lock (list)
				{
					int num2 = 0;
					while (this.blocking_threads.Count > 0)
					{
						if (num2++ >= 10)
						{
							if (SafeSocketHandle.THROW_ON_ABORT_RETRIES)
							{
								StringBuilder stringBuilder = new StringBuilder();
								stringBuilder.AppendLine("Could not abort registered blocking threads before closing socket.");
								foreach (Thread thread in this.blocking_threads)
								{
									stringBuilder.AppendLine("Thread StackTrace:");
									stringBuilder.AppendLine(this.threads_stacktraces[thread].ToString());
								}
								stringBuilder.AppendLine();
								throw new Exception(stringBuilder.ToString());
							}
							break;
						}
						else
						{
							if (this.blocking_threads.Count == 1 && this.blocking_threads[0] == Thread.CurrentThread)
							{
								break;
							}
							foreach (Thread thread2 in this.blocking_threads)
							{
								Socket.cancel_blocking_socket_operation(thread2);
							}
							this.in_cleanup = true;
							Monitor.Wait(this.blocking_threads, 100);
						}
					}
				}
			}
			Socket.Close_internal(this.handle, out num);
			return num == 0;
		}

		// Token: 0x06002F9B RID: 12187 RVA: 0x000BC174 File Offset: 0x000BA374
		public void RegisterForBlockingSyscall()
		{
			if (this.blocking_threads == null)
			{
				Interlocked.CompareExchange<List<Thread>>(ref this.blocking_threads, new List<Thread>(), null);
			}
			bool flag = false;
			try
			{
				base.DangerousAddRef(ref flag);
			}
			finally
			{
				List<Thread> list = this.blocking_threads;
				lock (list)
				{
					this.blocking_threads.Add(Thread.CurrentThread);
					if (SafeSocketHandle.THROW_ON_ABORT_RETRIES)
					{
						this.threads_stacktraces.Add(Thread.CurrentThread, new StackTrace(true));
					}
				}
				if (flag)
				{
					base.DangerousRelease();
				}
				if (base.IsClosed)
				{
					throw new SocketException(10004);
				}
			}
		}

		// Token: 0x06002F9C RID: 12188 RVA: 0x000BC22C File Offset: 0x000BA42C
		public void UnRegisterForBlockingSyscall()
		{
			List<Thread> list = this.blocking_threads;
			lock (list)
			{
				Thread currentThread = Thread.CurrentThread;
				this.blocking_threads.Remove(currentThread);
				if (SafeSocketHandle.THROW_ON_ABORT_RETRIES && this.blocking_threads.IndexOf(currentThread) == -1)
				{
					this.threads_stacktraces.Remove(currentThread);
				}
				if (this.in_cleanup && this.blocking_threads.Count == 0)
				{
					Monitor.Pulse(this.blocking_threads);
				}
			}
		}

		// Token: 0x04002724 RID: 10020
		private List<Thread> blocking_threads;

		// Token: 0x04002725 RID: 10021
		private Dictionary<Thread, StackTrace> threads_stacktraces;

		// Token: 0x04002726 RID: 10022
		private bool in_cleanup;

		// Token: 0x04002727 RID: 10023
		private const int SOCKET_CLOSED = 10004;

		// Token: 0x04002728 RID: 10024
		private const int ABORT_RETRIES = 10;

		// Token: 0x04002729 RID: 10025
		private static bool THROW_ON_ABORT_RETRIES = Environment.GetEnvironmentVariable("MONO_TESTS_IN_PROGRESS") == "yes";
	}
}
