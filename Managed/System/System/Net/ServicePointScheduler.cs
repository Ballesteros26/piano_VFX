using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net
{
	// Token: 0x02000545 RID: 1349
	internal class ServicePointScheduler
	{
		// Token: 0x170008F3 RID: 2291
		// (get) Token: 0x06002A0F RID: 10767 RVA: 0x000A1C26 File Offset: 0x0009FE26
		public ServicePoint ServicePoint { get; }

		// Token: 0x170008F4 RID: 2292
		// (get) Token: 0x06002A10 RID: 10768 RVA: 0x000A1C2E File Offset: 0x0009FE2E
		// (set) Token: 0x06002A11 RID: 10769 RVA: 0x000A1C36 File Offset: 0x0009FE36
		public int MaxIdleTime
		{
			get
			{
				return this.maxIdleTime;
			}
			set
			{
				if (value < -1 || value > 2147483647)
				{
					throw new ArgumentOutOfRangeException();
				}
				if (value == this.maxIdleTime)
				{
					return;
				}
				this.maxIdleTime = value;
				this.Run();
			}
		}

		// Token: 0x170008F5 RID: 2293
		// (get) Token: 0x06002A12 RID: 10770 RVA: 0x000A1C61 File Offset: 0x0009FE61
		// (set) Token: 0x06002A13 RID: 10771 RVA: 0x000A1C69 File Offset: 0x0009FE69
		public int ConnectionLimit
		{
			get
			{
				return this.connectionLimit;
			}
			set
			{
				if (value <= 0)
				{
					throw new ArgumentOutOfRangeException();
				}
				if (value == this.connectionLimit)
				{
					return;
				}
				this.connectionLimit = value;
				this.Run();
			}
		}

		// Token: 0x06002A14 RID: 10772 RVA: 0x000A1C8C File Offset: 0x0009FE8C
		public ServicePointScheduler(ServicePoint servicePoint, int connectionLimit, int maxIdleTime)
		{
			this.ServicePoint = servicePoint;
			this.connectionLimit = connectionLimit;
			this.maxIdleTime = maxIdleTime;
			this.schedulerEvent = new ServicePointScheduler.AsyncManualResetEvent(false);
			this.defaultGroup = new ServicePointScheduler.ConnectionGroup(this, string.Empty);
			this.operations = new LinkedList<ValueTuple<ServicePointScheduler.ConnectionGroup, WebOperation>>();
			this.idleConnections = new LinkedList<ValueTuple<ServicePointScheduler.ConnectionGroup, WebConnection, Task>>();
			this.idleSince = DateTime.UtcNow;
		}

		// Token: 0x06002A15 RID: 10773 RVA: 0x000027E8 File Offset: 0x000009E8
		[Conditional("MONO_WEB_DEBUG")]
		private void Debug(string message, params object[] args)
		{
		}

		// Token: 0x06002A16 RID: 10774 RVA: 0x000027E8 File Offset: 0x000009E8
		[Conditional("MONO_WEB_DEBUG")]
		private void Debug(string message)
		{
		}

		// Token: 0x170008F6 RID: 2294
		// (get) Token: 0x06002A17 RID: 10775 RVA: 0x000A1D10 File Offset: 0x0009FF10
		public int CurrentConnections
		{
			get
			{
				return this.currentConnections;
			}
		}

		// Token: 0x170008F7 RID: 2295
		// (get) Token: 0x06002A18 RID: 10776 RVA: 0x000A1D18 File Offset: 0x0009FF18
		public DateTime IdleSince
		{
			get
			{
				return this.idleSince;
			}
		}

		// Token: 0x170008F8 RID: 2296
		// (get) Token: 0x06002A19 RID: 10777 RVA: 0x000A1D20 File Offset: 0x0009FF20
		internal string ME { get; }

		// Token: 0x06002A1A RID: 10778 RVA: 0x000A1D28 File Offset: 0x0009FF28
		public void Run()
		{
			ServicePoint servicePoint = this.ServicePoint;
			lock (servicePoint)
			{
				if (Interlocked.CompareExchange(ref this.running, 1, 0) == 0)
				{
					this.StartScheduler();
				}
				this.schedulerEvent.Set();
			}
		}

		// Token: 0x06002A1B RID: 10779 RVA: 0x000A1D84 File Offset: 0x0009FF84
		private async void StartScheduler()
		{
			this.idleSince = DateTime.UtcNow + TimeSpan.FromDays(3650.0);
			for (;;)
			{
				List<Task> taskList = new List<Task>();
				ServicePoint servicePoint = this.ServicePoint;
				ValueTuple<ServicePointScheduler.ConnectionGroup, WebOperation>[] operationArray;
				ValueTuple<ServicePointScheduler.ConnectionGroup, WebConnection, Task>[] idleArray;
				lock (servicePoint)
				{
					this.Cleanup();
					if (this.groups == null && this.defaultGroup.IsEmpty() && this.operations.Count == 0 && this.idleConnections.Count == 0)
					{
						this.running = 0;
						this.idleSince = DateTime.UtcNow;
						this.schedulerEvent.Reset();
						break;
					}
					operationArray = new ValueTuple<ServicePointScheduler.ConnectionGroup, WebOperation>[this.operations.Count];
					this.operations.CopyTo(operationArray, 0);
					idleArray = new ValueTuple<ServicePointScheduler.ConnectionGroup, WebConnection, Task>[this.idleConnections.Count];
					this.idleConnections.CopyTo(idleArray, 0);
					taskList.Add(this.schedulerEvent.WaitAsync(this.maxIdleTime));
					foreach (ValueTuple<ServicePointScheduler.ConnectionGroup, WebOperation> valueTuple in operationArray)
					{
						taskList.Add(valueTuple.Item2.WaitForCompletion(true));
					}
					foreach (ValueTuple<ServicePointScheduler.ConnectionGroup, WebConnection, Task> valueTuple2 in idleArray)
					{
						taskList.Add(valueTuple2.Item3);
					}
				}
				Task task = await Task.WhenAny(taskList).ConfigureAwait(false);
				servicePoint = this.ServicePoint;
				lock (servicePoint)
				{
					if (task == taskList[0])
					{
						this.RunSchedulerIteration();
						continue;
					}
					int num = -1;
					for (int j = 0; j < operationArray.Length; j++)
					{
						if (task == taskList[j + 1])
						{
							num = j;
							break;
						}
					}
					if (num >= 0)
					{
						ValueTuple<ServicePointScheduler.ConnectionGroup, WebOperation> valueTuple3 = operationArray[num];
						this.operations.Remove(valueTuple3);
						Task<ValueTuple<bool, WebOperation>> task2 = (Task<ValueTuple<bool, WebOperation>>)task;
						if (this.OperationCompleted(valueTuple3.Item1, valueTuple3.Item2, task2))
						{
							this.RunSchedulerIteration();
						}
						continue;
					}
					for (int k = 0; k < idleArray.Length; k++)
					{
						if (task == taskList[k + 1 + operationArray.Length])
						{
							num = k;
							break;
						}
					}
					if (num >= 0)
					{
						ValueTuple<ServicePointScheduler.ConnectionGroup, WebConnection, Task> valueTuple4 = idleArray[num];
						this.idleConnections.Remove(valueTuple4);
						this.CloseIdleConnection(valueTuple4.Item1, valueTuple4.Item2);
					}
				}
				operationArray = null;
				idleArray = null;
				taskList = null;
			}
		}

		// Token: 0x06002A1C RID: 10780 RVA: 0x000A1DC0 File Offset: 0x0009FFC0
		private void Cleanup()
		{
			if (this.groups != null)
			{
				string[] array = new string[this.groups.Count];
				this.groups.Keys.CopyTo(array, 0);
				foreach (string text in array)
				{
					if (this.groups.ContainsKey(text) && this.groups[text].IsEmpty())
					{
						this.groups.Remove(text);
					}
				}
				if (this.groups.Count == 0)
				{
					this.groups = null;
				}
			}
		}

		// Token: 0x06002A1D RID: 10781 RVA: 0x000A1E50 File Offset: 0x000A0050
		private void RunSchedulerIteration()
		{
			this.schedulerEvent.Reset();
			bool flag;
			do
			{
				flag = this.SchedulerIteration(this.defaultGroup);
				if (this.groups != null)
				{
					foreach (KeyValuePair<string, ServicePointScheduler.ConnectionGroup> keyValuePair in this.groups)
					{
						flag |= this.SchedulerIteration(keyValuePair.Value);
					}
				}
			}
			while (flag);
		}

		// Token: 0x06002A1E RID: 10782 RVA: 0x000A1ED0 File Offset: 0x000A00D0
		private bool OperationCompleted(ServicePointScheduler.ConnectionGroup group, WebOperation operation, Task<ValueTuple<bool, WebOperation>> task)
		{
			ValueTuple<bool, WebOperation> valueTuple = ((task.Status == TaskStatus.RanToCompletion) ? task.Result : new ValueTuple<bool, WebOperation>(false, null));
			bool flag = valueTuple.Item1;
			WebOperation item = valueTuple.Item2;
			if (!flag || !operation.Connection.Continue(item))
			{
				group.RemoveConnection(operation.Connection);
				if (item == null)
				{
					return true;
				}
				flag = false;
			}
			if (item == null)
			{
				if (flag)
				{
					Task task2 = Task.Delay(this.MaxIdleTime);
					this.idleConnections.AddLast(new ValueTuple<ServicePointScheduler.ConnectionGroup, WebConnection, Task>(group, operation.Connection, task2));
				}
				return true;
			}
			this.operations.AddLast(new ValueTuple<ServicePointScheduler.ConnectionGroup, WebOperation>(group, item));
			if (flag)
			{
				this.RemoveIdleConnection(operation.Connection);
				return false;
			}
			group.Cleanup();
			group.CreateOrReuseConnection(item, true);
			return false;
		}

		// Token: 0x06002A1F RID: 10783 RVA: 0x000A1F87 File Offset: 0x000A0187
		private void CloseIdleConnection(ServicePointScheduler.ConnectionGroup group, WebConnection connection)
		{
			group.RemoveConnection(connection);
			this.RemoveIdleConnection(connection);
		}

		// Token: 0x06002A20 RID: 10784 RVA: 0x000A1F98 File Offset: 0x000A0198
		private bool SchedulerIteration(ServicePointScheduler.ConnectionGroup group)
		{
			group.Cleanup();
			WebOperation nextOperation = group.GetNextOperation();
			if (nextOperation == null)
			{
				return false;
			}
			WebConnection item = group.CreateOrReuseConnection(nextOperation, false).Item1;
			if (item == null)
			{
				return false;
			}
			this.operations.AddLast(new ValueTuple<ServicePointScheduler.ConnectionGroup, WebOperation>(group, nextOperation));
			this.RemoveIdleConnection(item);
			return true;
		}

		// Token: 0x06002A21 RID: 10785 RVA: 0x000A1FE8 File Offset: 0x000A01E8
		private void RemoveOperation(WebOperation operation)
		{
			LinkedListNode<ValueTuple<ServicePointScheduler.ConnectionGroup, WebOperation>> linkedListNode = this.operations.First;
			while (linkedListNode != null)
			{
				LinkedListNode<ValueTuple<ServicePointScheduler.ConnectionGroup, WebOperation>> linkedListNode2 = linkedListNode;
				linkedListNode = linkedListNode.Next;
				if (linkedListNode2.Value.Item2 == operation)
				{
					this.operations.Remove(linkedListNode2);
				}
			}
		}

		// Token: 0x06002A22 RID: 10786 RVA: 0x000A202C File Offset: 0x000A022C
		private void RemoveIdleConnection(WebConnection connection)
		{
			LinkedListNode<ValueTuple<ServicePointScheduler.ConnectionGroup, WebConnection, Task>> linkedListNode = this.idleConnections.First;
			while (linkedListNode != null)
			{
				LinkedListNode<ValueTuple<ServicePointScheduler.ConnectionGroup, WebConnection, Task>> linkedListNode2 = linkedListNode;
				linkedListNode = linkedListNode.Next;
				if (linkedListNode2.Value.Item2 == connection)
				{
					this.idleConnections.Remove(linkedListNode2);
				}
			}
		}

		// Token: 0x06002A23 RID: 10787 RVA: 0x000A2070 File Offset: 0x000A0270
		public void SendRequest(WebOperation operation, string groupName)
		{
			ServicePoint servicePoint = this.ServicePoint;
			lock (servicePoint)
			{
				this.GetConnectionGroup(groupName).EnqueueOperation(operation);
				this.Run();
			}
		}

		// Token: 0x06002A24 RID: 10788 RVA: 0x000A20C0 File Offset: 0x000A02C0
		public bool CloseConnectionGroup(string groupName)
		{
			ServicePoint servicePoint = this.ServicePoint;
			bool flag2;
			lock (servicePoint)
			{
				ServicePointScheduler.ConnectionGroup connectionGroup;
				if (string.IsNullOrEmpty(groupName))
				{
					connectionGroup = this.defaultGroup;
				}
				else if (this.groups == null || !this.groups.TryGetValue(groupName, out connectionGroup))
				{
					return false;
				}
				if (connectionGroup != this.defaultGroup)
				{
					this.groups.Remove(groupName);
					if (this.groups.Count == 0)
					{
						this.groups = null;
					}
				}
				connectionGroup.Close();
				this.Run();
				flag2 = true;
			}
			return flag2;
		}

		// Token: 0x06002A25 RID: 10789 RVA: 0x000A2160 File Offset: 0x000A0360
		private ServicePointScheduler.ConnectionGroup GetConnectionGroup(string name)
		{
			ServicePoint servicePoint = this.ServicePoint;
			ServicePointScheduler.ConnectionGroup connectionGroup;
			lock (servicePoint)
			{
				if (string.IsNullOrEmpty(name))
				{
					connectionGroup = this.defaultGroup;
				}
				else
				{
					if (this.groups == null)
					{
						this.groups = new Dictionary<string, ServicePointScheduler.ConnectionGroup>();
					}
					ServicePointScheduler.ConnectionGroup connectionGroup2;
					if (this.groups.TryGetValue(name, out connectionGroup2))
					{
						connectionGroup = connectionGroup2;
					}
					else
					{
						connectionGroup2 = new ServicePointScheduler.ConnectionGroup(this, name);
						this.groups.Add(name, connectionGroup2);
						connectionGroup = connectionGroup2;
					}
				}
			}
			return connectionGroup;
		}

		// Token: 0x06002A26 RID: 10790 RVA: 0x000A21EC File Offset: 0x000A03EC
		private void OnConnectionCreated(WebConnection connection)
		{
			Interlocked.Increment(ref this.currentConnections);
		}

		// Token: 0x06002A27 RID: 10791 RVA: 0x000A21FA File Offset: 0x000A03FA
		private void OnConnectionClosed(WebConnection connection)
		{
			this.RemoveIdleConnection(connection);
			Interlocked.Decrement(ref this.currentConnections);
		}

		// Token: 0x040022D1 RID: 8913
		private int running;

		// Token: 0x040022D2 RID: 8914
		private int maxIdleTime = 100000;

		// Token: 0x040022D3 RID: 8915
		private ServicePointScheduler.AsyncManualResetEvent schedulerEvent;

		// Token: 0x040022D4 RID: 8916
		private ServicePointScheduler.ConnectionGroup defaultGroup;

		// Token: 0x040022D5 RID: 8917
		private Dictionary<string, ServicePointScheduler.ConnectionGroup> groups;

		// Token: 0x040022D6 RID: 8918
		private LinkedList<ValueTuple<ServicePointScheduler.ConnectionGroup, WebOperation>> operations;

		// Token: 0x040022D7 RID: 8919
		private LinkedList<ValueTuple<ServicePointScheduler.ConnectionGroup, WebConnection, Task>> idleConnections;

		// Token: 0x040022D8 RID: 8920
		private int currentConnections;

		// Token: 0x040022D9 RID: 8921
		private int connectionLimit;

		// Token: 0x040022DA RID: 8922
		private DateTime idleSince;

		// Token: 0x040022DB RID: 8923
		private static int nextId;

		// Token: 0x040022DC RID: 8924
		public readonly int ID = ++ServicePointScheduler.nextId;

		// Token: 0x02000546 RID: 1350
		private class ConnectionGroup
		{
			// Token: 0x170008F9 RID: 2297
			// (get) Token: 0x06002A28 RID: 10792 RVA: 0x000A220F File Offset: 0x000A040F
			public ServicePointScheduler Scheduler { get; }

			// Token: 0x170008FA RID: 2298
			// (get) Token: 0x06002A29 RID: 10793 RVA: 0x000A2217 File Offset: 0x000A0417
			public string Name { get; }

			// Token: 0x170008FB RID: 2299
			// (get) Token: 0x06002A2A RID: 10794 RVA: 0x000A221F File Offset: 0x000A041F
			public bool IsDefault
			{
				get
				{
					return string.IsNullOrEmpty(this.Name);
				}
			}

			// Token: 0x06002A2B RID: 10795 RVA: 0x000A222C File Offset: 0x000A042C
			public ConnectionGroup(ServicePointScheduler scheduler, string name)
			{
				this.Scheduler = scheduler;
				this.Name = name;
				this.connections = new LinkedList<WebConnection>();
				this.queue = new LinkedList<WebOperation>();
			}

			// Token: 0x06002A2C RID: 10796 RVA: 0x000A226B File Offset: 0x000A046B
			public bool IsEmpty()
			{
				return this.connections.Count == 0 && this.queue.Count == 0;
			}

			// Token: 0x06002A2D RID: 10797 RVA: 0x000A228A File Offset: 0x000A048A
			public void RemoveConnection(WebConnection connection)
			{
				this.connections.Remove(connection);
				connection.Dispose();
				this.Scheduler.OnConnectionClosed(connection);
			}

			// Token: 0x06002A2E RID: 10798 RVA: 0x000A22AC File Offset: 0x000A04AC
			public void Cleanup()
			{
				LinkedListNode<WebConnection> linkedListNode = this.connections.First;
				while (linkedListNode != null)
				{
					WebConnection value = linkedListNode.Value;
					LinkedListNode<WebConnection> linkedListNode2 = linkedListNode;
					linkedListNode = linkedListNode.Next;
					if (value.Closed)
					{
						this.connections.Remove(linkedListNode2);
						this.Scheduler.OnConnectionClosed(value);
					}
				}
			}

			// Token: 0x06002A2F RID: 10799 RVA: 0x000A22FC File Offset: 0x000A04FC
			public void Close()
			{
				foreach (WebOperation webOperation in this.queue)
				{
					webOperation.Abort();
					this.Scheduler.RemoveOperation(webOperation);
				}
				this.queue.Clear();
				foreach (WebConnection webConnection in this.connections)
				{
					webConnection.Dispose();
					this.Scheduler.OnConnectionClosed(webConnection);
				}
				this.connections.Clear();
			}

			// Token: 0x06002A30 RID: 10800 RVA: 0x000A23C0 File Offset: 0x000A05C0
			public void EnqueueOperation(WebOperation operation)
			{
				this.queue.AddLast(operation);
			}

			// Token: 0x06002A31 RID: 10801 RVA: 0x000A23D0 File Offset: 0x000A05D0
			public WebOperation GetNextOperation()
			{
				LinkedListNode<WebOperation> linkedListNode = this.queue.First;
				while (linkedListNode != null)
				{
					WebOperation value = linkedListNode.Value;
					LinkedListNode<WebOperation> linkedListNode2 = linkedListNode;
					linkedListNode = linkedListNode.Next;
					if (!value.Aborted)
					{
						return value;
					}
					this.queue.Remove(linkedListNode2);
					this.Scheduler.RemoveOperation(value);
				}
				return null;
			}

			// Token: 0x06002A32 RID: 10802 RVA: 0x000A2424 File Offset: 0x000A0624
			public WebConnection FindIdleConnection(WebOperation operation)
			{
				WebConnection webConnection = null;
				foreach (WebConnection webConnection2 in this.connections)
				{
					if (webConnection2.CanReuseConnection(operation) && (webConnection == null || webConnection2.IdleSince > webConnection.IdleSince))
					{
						webConnection = webConnection2;
					}
				}
				if (webConnection != null && webConnection.StartOperation(operation, true))
				{
					this.queue.Remove(operation);
					return webConnection;
				}
				foreach (WebConnection webConnection3 in this.connections)
				{
					if (webConnection3.StartOperation(operation, true))
					{
						this.queue.Remove(operation);
						return webConnection3;
					}
				}
				return null;
			}

			// Token: 0x06002A33 RID: 10803 RVA: 0x000A250C File Offset: 0x000A070C
			[return: TupleElementNames(new string[] { "connection", "created" })]
			public ValueTuple<WebConnection, bool> CreateOrReuseConnection(WebOperation operation, bool force)
			{
				WebConnection webConnection = this.FindIdleConnection(operation);
				if (webConnection != null)
				{
					return new ValueTuple<WebConnection, bool>(webConnection, false);
				}
				if (force || this.Scheduler.ServicePoint.ConnectionLimit > this.connections.Count || this.connections.Count == 0)
				{
					webConnection = new WebConnection(this.Scheduler.ServicePoint);
					webConnection.StartOperation(operation, false);
					this.connections.AddFirst(webConnection);
					this.Scheduler.OnConnectionCreated(webConnection);
					this.queue.Remove(operation);
					return new ValueTuple<WebConnection, bool>(webConnection, true);
				}
				return new ValueTuple<WebConnection, bool>(null, false);
			}

			// Token: 0x040022E0 RID: 8928
			private static int nextId;

			// Token: 0x040022E1 RID: 8929
			public readonly int ID = ++ServicePointScheduler.ConnectionGroup.nextId;

			// Token: 0x040022E2 RID: 8930
			private LinkedList<WebConnection> connections;

			// Token: 0x040022E3 RID: 8931
			private LinkedList<WebOperation> queue;
		}

		// Token: 0x02000547 RID: 1351
		private class AsyncManualResetEvent
		{
			// Token: 0x06002A34 RID: 10804 RVA: 0x000A25A8 File Offset: 0x000A07A8
			public Task WaitAsync()
			{
				return this.m_tcs.Task;
			}

			// Token: 0x06002A35 RID: 10805 RVA: 0x000A25B7 File Offset: 0x000A07B7
			public bool WaitOne(int millisecondTimeout)
			{
				return this.m_tcs.Task.Wait(millisecondTimeout);
			}

			// Token: 0x06002A36 RID: 10806 RVA: 0x000A25CC File Offset: 0x000A07CC
			public async Task<bool> WaitAsync(int millisecondTimeout)
			{
				Task timeoutTask = Task.Delay(millisecondTimeout);
				ConfiguredTaskAwaitable<Task>.ConfiguredTaskAwaiter configuredTaskAwaiter = Task.WhenAny(new Task[]
				{
					this.m_tcs.Task,
					timeoutTask
				}).ConfigureAwait(false).GetAwaiter();
				if (!configuredTaskAwaiter.IsCompleted)
				{
					await configuredTaskAwaiter;
					ConfiguredTaskAwaitable<Task>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
					configuredTaskAwaiter = configuredTaskAwaiter2;
					configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<Task>.ConfiguredTaskAwaiter);
				}
				return configuredTaskAwaiter.GetResult() != timeoutTask;
			}

			// Token: 0x06002A37 RID: 10807 RVA: 0x000A261C File Offset: 0x000A081C
			public void Set()
			{
				TaskCompletionSource<bool> tcs = this.m_tcs;
				Task.Factory.StartNew<bool>((object s) => ((TaskCompletionSource<bool>)s).TrySetResult(true), tcs, CancellationToken.None, TaskCreationOptions.PreferFairness, TaskScheduler.Default);
				tcs.Task.Wait();
			}

			// Token: 0x06002A38 RID: 10808 RVA: 0x000A2674 File Offset: 0x000A0874
			public void Reset()
			{
				TaskCompletionSource<bool> tcs;
				do
				{
					tcs = this.m_tcs;
				}
				while (tcs.Task.IsCompleted && Interlocked.CompareExchange<TaskCompletionSource<bool>>(ref this.m_tcs, new TaskCompletionSource<bool>(), tcs) != tcs);
			}

			// Token: 0x06002A39 RID: 10809 RVA: 0x000A26AB File Offset: 0x000A08AB
			public AsyncManualResetEvent(bool state)
			{
				if (state)
				{
					this.Set();
				}
			}

			// Token: 0x040022E4 RID: 8932
			private volatile TaskCompletionSource<bool> m_tcs = new TaskCompletionSource<bool>();
		}
	}
}
