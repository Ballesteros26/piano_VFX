using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Permissions;

namespace System.Threading
{
	/// <summary>Provides thread-local storage of data.</summary>
	/// <typeparam name="T">Specifies the type of data stored per-thread.</typeparam>
	// Token: 0x02000463 RID: 1123
	[DebuggerTypeProxy(typeof(SystemThreading_ThreadLocalDebugView<>))]
	[DebuggerDisplay("IsValueCreated={IsValueCreated}, Value={ValueForDebugDisplay}, Count={ValuesCountForDebugDisplay}")]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public class ThreadLocal<T> : IDisposable
	{
		/// <summary>Initializes the <see cref="T:System.Threading.ThreadLocal`1" /> instance.</summary>
		// Token: 0x0600358C RID: 13708 RVA: 0x000C6232 File Offset: 0x000C4432
		public ThreadLocal()
		{
			this.Initialize(null, false);
		}

		/// <summary>Initializes the <see cref="T:System.Threading.ThreadLocal`1" /> instance.</summary>
		/// <param name="trackAllValues">Whether to track all values set on the instance and expose them through the <see cref="P:System.Threading.ThreadLocal`1.Values" /> property.</param>
		// Token: 0x0600358D RID: 13709 RVA: 0x000C624E File Offset: 0x000C444E
		public ThreadLocal(bool trackAllValues)
		{
			this.Initialize(null, trackAllValues);
		}

		/// <summary>Initializes the <see cref="T:System.Threading.ThreadLocal`1" /> instance with the specified <paramref name="valueFactory" /> function.</summary>
		/// <param name="valueFactory">The  <see cref="T:System.Func`1" /> invoked to produce a lazily-initialized value when an attempt is made to retrieve <see cref="P:System.Threading.ThreadLocal`1.Value" /> without it having been previously initialized.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="valueFactory" /> is a null reference (Nothing in Visual Basic).</exception>
		// Token: 0x0600358E RID: 13710 RVA: 0x000C626A File Offset: 0x000C446A
		public ThreadLocal(Func<T> valueFactory)
		{
			if (valueFactory == null)
			{
				throw new ArgumentNullException("valueFactory");
			}
			this.Initialize(valueFactory, false);
		}

		/// <summary>Initializes the <see cref="T:System.Threading.ThreadLocal`1" /> instance with the specified <paramref name="valueFactory" /> function.</summary>
		/// <param name="valueFactory">The <see cref="T:System.Func`1" /> invoked to produce a lazily-initialized value when an attempt is made to retrieve <see cref="P:System.Threading.ThreadLocal`1.Value" /> without it having been previously initialized.</param>
		/// <param name="trackAllValues">Whether to track all values set on the instance and expose them via the <see cref="P:System.Threading.ThreadLocal`1.Values" /> property.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="valueFactory" /> is a null reference (Nothing in Visual Basic).</exception>
		// Token: 0x0600358F RID: 13711 RVA: 0x000C6294 File Offset: 0x000C4494
		public ThreadLocal(Func<T> valueFactory, bool trackAllValues)
		{
			if (valueFactory == null)
			{
				throw new ArgumentNullException("valueFactory");
			}
			this.Initialize(valueFactory, trackAllValues);
		}

		// Token: 0x06003590 RID: 13712 RVA: 0x000C62C0 File Offset: 0x000C44C0
		private void Initialize(Func<T> valueFactory, bool trackAllValues)
		{
			this.m_valueFactory = valueFactory;
			this.m_trackAllValues = trackAllValues;
			try
			{
			}
			finally
			{
				this.m_idComplement = ~ThreadLocal<T>.s_idManager.GetId();
				this.m_initialized = true;
			}
		}

		// Token: 0x06003591 RID: 13713 RVA: 0x000C6308 File Offset: 0x000C4508
		~ThreadLocal()
		{
			this.Dispose(false);
		}

		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.Threading.ThreadLocal`1" /> class.</summary>
		// Token: 0x06003592 RID: 13714 RVA: 0x000C6338 File Offset: 0x000C4538
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases the resources used by this <see cref="T:System.Threading.ThreadLocal`1" /> instance.</summary>
		/// <param name="disposing">A Boolean value that indicates whether this method is being called due to a call to <see cref="M:System.Threading.ThreadLocal`1.Dispose" />.</param>
		// Token: 0x06003593 RID: 13715 RVA: 0x000C6348 File Offset: 0x000C4548
		protected virtual void Dispose(bool disposing)
		{
			ThreadLocal<T>.IdManager idManager = ThreadLocal<T>.s_idManager;
			int num;
			lock (idManager)
			{
				num = ~this.m_idComplement;
				this.m_idComplement = 0;
				if (num < 0 || !this.m_initialized)
				{
					return;
				}
				this.m_initialized = false;
				for (ThreadLocal<T>.LinkedSlot linkedSlot = this.m_linkedSlot.Next; linkedSlot != null; linkedSlot = linkedSlot.Next)
				{
					ThreadLocal<T>.LinkedSlotVolatile[] slotArray = linkedSlot.SlotArray;
					if (slotArray != null)
					{
						linkedSlot.SlotArray = null;
						slotArray[num].Value.Value = default(T);
						slotArray[num].Value = null;
					}
				}
			}
			this.m_linkedSlot = null;
			ThreadLocal<T>.s_idManager.ReturnId(num);
		}

		/// <summary>Creates and returns a string representation of this instance for the current thread.</summary>
		/// <returns>The result of calling <see cref="M:System.Object.ToString" /> on the <see cref="P:System.Threading.ThreadLocal`1.Value" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.ThreadLocal`1" /> instance has been disposed.</exception>
		/// <exception cref="T:System.NullReferenceException">The <see cref="P:System.Threading.ThreadLocal`1.Value" /> for the current thread is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.InvalidOperationException">The initialization function attempted to reference <see cref="P:System.Threading.ThreadLocal`1.Value" /> recursively.</exception>
		/// <exception cref="T:System.MissingMemberException">No default constructor is provided and no value factory is supplied.</exception>
		// Token: 0x06003594 RID: 13716 RVA: 0x000C641C File Offset: 0x000C461C
		public override string ToString()
		{
			T value = this.Value;
			return value.ToString();
		}

		/// <summary>Gets or sets the value of this instance for the current thread.</summary>
		/// <returns>Returns an instance of the object that this ThreadLocal is responsible for initializing.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.ThreadLocal`1" /> instance has been disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The initialization function attempted to reference <see cref="P:System.Threading.ThreadLocal`1.Value" /> recursively.</exception>
		/// <exception cref="T:System.MissingMemberException">No default constructor is provided and no value factory is supplied.</exception>
		// Token: 0x170008F6 RID: 2294
		// (get) Token: 0x06003595 RID: 13717 RVA: 0x000C6440 File Offset: 0x000C4640
		// (set) Token: 0x06003596 RID: 13718 RVA: 0x000C6494 File Offset: 0x000C4694
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public T Value
		{
			get
			{
				ThreadLocal<T>.LinkedSlotVolatile[] array = ThreadLocal<T>.ts_slotArray;
				int num = ~this.m_idComplement;
				ThreadLocal<T>.LinkedSlot value;
				if (array != null && num >= 0 && num < array.Length && (value = array[num].Value) != null && this.m_initialized)
				{
					return value.Value;
				}
				return this.GetValueSlow();
			}
			set
			{
				ThreadLocal<T>.LinkedSlotVolatile[] array = ThreadLocal<T>.ts_slotArray;
				int num = ~this.m_idComplement;
				ThreadLocal<T>.LinkedSlot value2;
				if (array != null && num >= 0 && num < array.Length && (value2 = array[num].Value) != null && this.m_initialized)
				{
					value2.Value = value;
					return;
				}
				this.SetValueSlow(value, array);
			}
		}

		// Token: 0x06003597 RID: 13719 RVA: 0x000C64E8 File Offset: 0x000C46E8
		private T GetValueSlow()
		{
			if (~this.m_idComplement < 0)
			{
				throw new ObjectDisposedException(Environment.GetResourceString("The ThreadLocal object has been disposed."));
			}
			Debugger.NotifyOfCrossThreadDependency();
			T t;
			if (this.m_valueFactory == null)
			{
				t = default(T);
			}
			else
			{
				t = this.m_valueFactory();
				if (this.IsValueCreated)
				{
					throw new InvalidOperationException(Environment.GetResourceString("ValueFactory attempted to access the Value property of this instance."));
				}
			}
			this.Value = t;
			return t;
		}

		// Token: 0x06003598 RID: 13720 RVA: 0x000C6554 File Offset: 0x000C4754
		private void SetValueSlow(T value, ThreadLocal<T>.LinkedSlotVolatile[] slotArray)
		{
			int num = ~this.m_idComplement;
			if (num < 0)
			{
				throw new ObjectDisposedException(Environment.GetResourceString("The ThreadLocal object has been disposed."));
			}
			if (slotArray == null)
			{
				slotArray = new ThreadLocal<T>.LinkedSlotVolatile[ThreadLocal<T>.GetNewTableSize(num + 1)];
				ThreadLocal<T>.ts_finalizationHelper = new ThreadLocal<T>.FinalizationHelper(slotArray, this.m_trackAllValues);
				ThreadLocal<T>.ts_slotArray = slotArray;
			}
			if (num >= slotArray.Length)
			{
				this.GrowTable(ref slotArray, num + 1);
				ThreadLocal<T>.ts_finalizationHelper.SlotArray = slotArray;
				ThreadLocal<T>.ts_slotArray = slotArray;
			}
			if (slotArray[num].Value == null)
			{
				this.CreateLinkedSlot(slotArray, num, value);
				return;
			}
			ThreadLocal<T>.LinkedSlot value2 = slotArray[num].Value;
			if (!this.m_initialized)
			{
				throw new ObjectDisposedException(Environment.GetResourceString("The ThreadLocal object has been disposed."));
			}
			value2.Value = value;
		}

		// Token: 0x06003599 RID: 13721 RVA: 0x000C6610 File Offset: 0x000C4810
		private void CreateLinkedSlot(ThreadLocal<T>.LinkedSlotVolatile[] slotArray, int id, T value)
		{
			ThreadLocal<T>.LinkedSlot linkedSlot = new ThreadLocal<T>.LinkedSlot(slotArray);
			ThreadLocal<T>.IdManager idManager = ThreadLocal<T>.s_idManager;
			lock (idManager)
			{
				if (!this.m_initialized)
				{
					throw new ObjectDisposedException(Environment.GetResourceString("The ThreadLocal object has been disposed."));
				}
				ThreadLocal<T>.LinkedSlot next = this.m_linkedSlot.Next;
				linkedSlot.Next = next;
				linkedSlot.Previous = this.m_linkedSlot;
				linkedSlot.Value = value;
				if (next != null)
				{
					next.Previous = linkedSlot;
				}
				this.m_linkedSlot.Next = linkedSlot;
				slotArray[id].Value = linkedSlot;
			}
		}

		/// <summary>Gets a list for all of the values currently stored by all of the threads that have accessed this instance.</summary>
		/// <returns>A list for all of the values currently stored by all of the threads that have accessed this instance.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.ThreadLocal`1" /> instance has been disposed.</exception>
		// Token: 0x170008F7 RID: 2295
		// (get) Token: 0x0600359A RID: 13722 RVA: 0x000C66C0 File Offset: 0x000C48C0
		public IList<T> Values
		{
			get
			{
				if (!this.m_trackAllValues)
				{
					throw new InvalidOperationException(Environment.GetResourceString("The ThreadLocal object is not tracking values. To use the Values property, use a ThreadLocal constructor that accepts the trackAllValues parameter and set the parameter to true."));
				}
				List<T> valuesAsList = this.GetValuesAsList();
				if (valuesAsList == null)
				{
					throw new ObjectDisposedException(Environment.GetResourceString("The ThreadLocal object has been disposed."));
				}
				return valuesAsList;
			}
		}

		// Token: 0x0600359B RID: 13723 RVA: 0x000C66F4 File Offset: 0x000C48F4
		private List<T> GetValuesAsList()
		{
			List<T> list = new List<T>();
			if (~this.m_idComplement == -1)
			{
				return null;
			}
			for (ThreadLocal<T>.LinkedSlot linkedSlot = this.m_linkedSlot.Next; linkedSlot != null; linkedSlot = linkedSlot.Next)
			{
				list.Add(linkedSlot.Value);
			}
			return list;
		}

		// Token: 0x170008F8 RID: 2296
		// (get) Token: 0x0600359C RID: 13724 RVA: 0x000C673C File Offset: 0x000C493C
		private int ValuesCountForDebugDisplay
		{
			get
			{
				int num = 0;
				for (ThreadLocal<T>.LinkedSlot linkedSlot = this.m_linkedSlot.Next; linkedSlot != null; linkedSlot = linkedSlot.Next)
				{
					num++;
				}
				return num;
			}
		}

		/// <summary>Gets whether <see cref="P:System.Threading.ThreadLocal`1.Value" /> is initialized on the current thread.</summary>
		/// <returns>true if <see cref="P:System.Threading.ThreadLocal`1.Value" /> is initialized on the current thread; otherwise false.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.ThreadLocal`1" /> instance has been disposed.</exception>
		// Token: 0x170008F9 RID: 2297
		// (get) Token: 0x0600359D RID: 13725 RVA: 0x000C676C File Offset: 0x000C496C
		public bool IsValueCreated
		{
			get
			{
				int num = ~this.m_idComplement;
				if (num < 0)
				{
					throw new ObjectDisposedException(Environment.GetResourceString("The ThreadLocal object has been disposed."));
				}
				ThreadLocal<T>.LinkedSlotVolatile[] array = ThreadLocal<T>.ts_slotArray;
				return array != null && num < array.Length && array[num].Value != null;
			}
		}

		// Token: 0x170008FA RID: 2298
		// (get) Token: 0x0600359E RID: 13726 RVA: 0x000C67B8 File Offset: 0x000C49B8
		internal T ValueForDebugDisplay
		{
			get
			{
				ThreadLocal<T>.LinkedSlotVolatile[] array = ThreadLocal<T>.ts_slotArray;
				int num = ~this.m_idComplement;
				ThreadLocal<T>.LinkedSlot value;
				if (array == null || num >= array.Length || (value = array[num].Value) == null || !this.m_initialized)
				{
					return default(T);
				}
				return value.Value;
			}
		}

		// Token: 0x170008FB RID: 2299
		// (get) Token: 0x0600359F RID: 13727 RVA: 0x000C6808 File Offset: 0x000C4A08
		internal List<T> ValuesForDebugDisplay
		{
			get
			{
				return this.GetValuesAsList();
			}
		}

		// Token: 0x060035A0 RID: 13728 RVA: 0x000C6810 File Offset: 0x000C4A10
		private void GrowTable(ref ThreadLocal<T>.LinkedSlotVolatile[] table, int minLength)
		{
			ThreadLocal<T>.LinkedSlotVolatile[] array = new ThreadLocal<T>.LinkedSlotVolatile[ThreadLocal<T>.GetNewTableSize(minLength)];
			ThreadLocal<T>.IdManager idManager = ThreadLocal<T>.s_idManager;
			lock (idManager)
			{
				for (int i = 0; i < table.Length; i++)
				{
					ThreadLocal<T>.LinkedSlot value = table[i].Value;
					if (value != null && value.SlotArray != null)
					{
						value.SlotArray = array;
						array[i] = table[i];
					}
				}
			}
			table = array;
		}

		// Token: 0x060035A1 RID: 13729 RVA: 0x000C68A0 File Offset: 0x000C4AA0
		private static int GetNewTableSize(int minSize)
		{
			if (minSize > 2146435071)
			{
				return int.MaxValue;
			}
			int num = minSize - 1;
			num |= num >> 1;
			num |= num >> 2;
			num |= num >> 4;
			num |= num >> 8;
			num |= num >> 16;
			num++;
			if (num > 2146435071)
			{
				num = 2146435071;
			}
			return num;
		}

		// Token: 0x04001C8D RID: 7309
		private Func<T> m_valueFactory;

		// Token: 0x04001C8E RID: 7310
		[ThreadStatic]
		private static ThreadLocal<T>.LinkedSlotVolatile[] ts_slotArray;

		// Token: 0x04001C8F RID: 7311
		[ThreadStatic]
		private static ThreadLocal<T>.FinalizationHelper ts_finalizationHelper;

		// Token: 0x04001C90 RID: 7312
		private int m_idComplement;

		// Token: 0x04001C91 RID: 7313
		private volatile bool m_initialized;

		// Token: 0x04001C92 RID: 7314
		private static ThreadLocal<T>.IdManager s_idManager = new ThreadLocal<T>.IdManager();

		// Token: 0x04001C93 RID: 7315
		private ThreadLocal<T>.LinkedSlot m_linkedSlot = new ThreadLocal<T>.LinkedSlot(null);

		// Token: 0x04001C94 RID: 7316
		private bool m_trackAllValues;

		// Token: 0x02000464 RID: 1124
		private struct LinkedSlotVolatile
		{
			// Token: 0x04001C95 RID: 7317
			internal volatile ThreadLocal<T>.LinkedSlot Value;
		}

		// Token: 0x02000465 RID: 1125
		private sealed class LinkedSlot
		{
			// Token: 0x060035A3 RID: 13731 RVA: 0x000C68FF File Offset: 0x000C4AFF
			internal LinkedSlot(ThreadLocal<T>.LinkedSlotVolatile[] slotArray)
			{
				this.SlotArray = slotArray;
			}

			// Token: 0x04001C96 RID: 7318
			internal volatile ThreadLocal<T>.LinkedSlot Next;

			// Token: 0x04001C97 RID: 7319
			internal volatile ThreadLocal<T>.LinkedSlot Previous;

			// Token: 0x04001C98 RID: 7320
			internal volatile ThreadLocal<T>.LinkedSlotVolatile[] SlotArray;

			// Token: 0x04001C99 RID: 7321
			internal T Value;
		}

		// Token: 0x02000466 RID: 1126
		private class IdManager
		{
			// Token: 0x060035A4 RID: 13732 RVA: 0x000C6910 File Offset: 0x000C4B10
			internal int GetId()
			{
				List<bool> freeIds = this.m_freeIds;
				int num2;
				lock (freeIds)
				{
					int num = this.m_nextIdToTry;
					while (num < this.m_freeIds.Count && !this.m_freeIds[num])
					{
						num++;
					}
					if (num == this.m_freeIds.Count)
					{
						this.m_freeIds.Add(false);
					}
					else
					{
						this.m_freeIds[num] = false;
					}
					this.m_nextIdToTry = num + 1;
					num2 = num;
				}
				return num2;
			}

			// Token: 0x060035A5 RID: 13733 RVA: 0x000C69A8 File Offset: 0x000C4BA8
			internal void ReturnId(int id)
			{
				List<bool> freeIds = this.m_freeIds;
				lock (freeIds)
				{
					this.m_freeIds[id] = true;
					if (id < this.m_nextIdToTry)
					{
						this.m_nextIdToTry = id;
					}
				}
			}

			// Token: 0x04001C9A RID: 7322
			private int m_nextIdToTry;

			// Token: 0x04001C9B RID: 7323
			private List<bool> m_freeIds = new List<bool>();
		}

		// Token: 0x02000467 RID: 1127
		private class FinalizationHelper
		{
			// Token: 0x060035A7 RID: 13735 RVA: 0x000C6A13 File Offset: 0x000C4C13
			internal FinalizationHelper(ThreadLocal<T>.LinkedSlotVolatile[] slotArray, bool trackAllValues)
			{
				this.SlotArray = slotArray;
				this.m_trackAllValues = trackAllValues;
			}

			// Token: 0x060035A8 RID: 13736 RVA: 0x000C6A2C File Offset: 0x000C4C2C
			protected override void Finalize()
			{
				try
				{
					ThreadLocal<T>.LinkedSlotVolatile[] slotArray = this.SlotArray;
					for (int i = 0; i < slotArray.Length; i++)
					{
						ThreadLocal<T>.LinkedSlot value = slotArray[i].Value;
						if (value != null)
						{
							if (this.m_trackAllValues)
							{
								value.SlotArray = null;
							}
							else
							{
								ThreadLocal<T>.IdManager s_idManager = ThreadLocal<T>.s_idManager;
								lock (s_idManager)
								{
									if (value.Next != null)
									{
										value.Next.Previous = value.Previous;
									}
									value.Previous.Next = value.Next;
								}
							}
						}
					}
				}
				finally
				{
					base.Finalize();
				}
			}

			// Token: 0x04001C9C RID: 7324
			internal ThreadLocal<T>.LinkedSlotVolatile[] SlotArray;

			// Token: 0x04001C9D RID: 7325
			private bool m_trackAllValues;
		}
	}
}
