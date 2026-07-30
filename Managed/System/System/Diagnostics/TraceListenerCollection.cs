using System;
using System.Collections;

namespace System.Diagnostics
{
	/// <summary>Provides a thread-safe list of <see cref="T:System.Diagnostics.TraceListener" /> objects.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001C9 RID: 457
	public class TraceListenerCollection : IList, ICollection, IEnumerable
	{
		// Token: 0x06000DE6 RID: 3558 RVA: 0x000419F1 File Offset: 0x0003FBF1
		internal TraceListenerCollection()
		{
			this.list = new ArrayList(1);
		}

		/// <summary>Gets or sets the <see cref="T:System.Diagnostics.TraceListener" /> at the specified index.</summary>
		/// <returns>A <see cref="T:System.Diagnostics.TraceListener" /> with the specified index.</returns>
		/// <param name="i">The zero-based index of the <see cref="T:System.Diagnostics.TraceListener" /> to get from the list. </param>
		/// <exception cref="T:System.ArgumentNullException">The value is null.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000281 RID: 641
		public TraceListener this[int i]
		{
			get
			{
				return (TraceListener)this.list[i];
			}
			set
			{
				this.InitializeListener(value);
				this.list[i] = value;
			}
		}

		/// <summary>Gets the first <see cref="T:System.Diagnostics.TraceListener" /> in the list with the specified name.</summary>
		/// <returns>The first <see cref="T:System.Diagnostics.TraceListener" /> in the list with the given <see cref="P:System.Diagnostics.TraceListener.Name" />. This item returns null if no <see cref="T:System.Diagnostics.TraceListener" /> with the given name can be found.</returns>
		/// <param name="name">The name of the <see cref="T:System.Diagnostics.TraceListener" /> to get from the list. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000282 RID: 642
		public TraceListener this[string name]
		{
			get
			{
				foreach (object obj in this)
				{
					TraceListener traceListener = (TraceListener)obj;
					if (traceListener.Name == name)
					{
						return traceListener;
					}
				}
				return null;
			}
		}

		/// <summary>Gets the number of listeners in the list.</summary>
		/// <returns>The number of listeners in the list.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000DEA RID: 3562 RVA: 0x00041A94 File Offset: 0x0003FC94
		public int Count
		{
			get
			{
				return this.list.Count;
			}
		}

		/// <summary>Adds a <see cref="T:System.Diagnostics.TraceListener" /> to the list.</summary>
		/// <returns>The position at which the new listener was inserted.</returns>
		/// <param name="listener">A <see cref="T:System.Diagnostics.TraceListener" /> to add to the list. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000DEB RID: 3563 RVA: 0x00041AA4 File Offset: 0x0003FCA4
		public int Add(TraceListener listener)
		{
			this.InitializeListener(listener);
			object critSec = TraceInternal.critSec;
			int num;
			lock (critSec)
			{
				num = this.list.Add(listener);
			}
			return num;
		}

		/// <summary>Adds an array of <see cref="T:System.Diagnostics.TraceListener" /> objects to the list.</summary>
		/// <param name="value">An array of <see cref="T:System.Diagnostics.TraceListener" /> objects to add to the list. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000DEC RID: 3564 RVA: 0x00041AF4 File Offset: 0x0003FCF4
		public void AddRange(TraceListener[] value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			for (int i = 0; i < value.Length; i++)
			{
				this.Add(value[i]);
			}
		}

		/// <summary>Adds the contents of another <see cref="T:System.Diagnostics.TraceListenerCollection" /> to the list.</summary>
		/// <param name="value">Another <see cref="T:System.Diagnostics.TraceListenerCollection" /> whose contents are added to the list. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000DED RID: 3565 RVA: 0x00041B28 File Offset: 0x0003FD28
		public void AddRange(TraceListenerCollection value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			int count = value.Count;
			for (int i = 0; i < count; i++)
			{
				this.Add(value[i]);
			}
		}

		/// <summary>Clears all the listeners from the list.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000DEE RID: 3566 RVA: 0x00041B64 File Offset: 0x0003FD64
		public void Clear()
		{
			this.list = new ArrayList();
		}

		/// <summary>Checks whether the list contains the specified listener.</summary>
		/// <returns>true if the listener is in the list; otherwise, false.</returns>
		/// <param name="listener">A <see cref="T:System.Diagnostics.TraceListener" /> to find in the list. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000DEF RID: 3567 RVA: 0x00041B71 File Offset: 0x0003FD71
		public bool Contains(TraceListener listener)
		{
			return ((IList)this).Contains(listener);
		}

		/// <summary>Copies a section of the current <see cref="T:System.Diagnostics.TraceListenerCollection" /> list to the specified array at the specified index.</summary>
		/// <param name="listeners">An array of type <see cref="T:System.Array" /> to copy the elements into. </param>
		/// <param name="index">The starting index number in the current list to copy from. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000DF0 RID: 3568 RVA: 0x00041B7A File Offset: 0x0003FD7A
		public void CopyTo(TraceListener[] listeners, int index)
		{
			((ICollection)this).CopyTo(listeners, index);
		}

		/// <summary>Gets an enumerator for this list.</summary>
		/// <returns>An enumerator of type <see cref="T:System.Collections.IEnumerator" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000DF1 RID: 3569 RVA: 0x00041B84 File Offset: 0x0003FD84
		public IEnumerator GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		// Token: 0x06000DF2 RID: 3570 RVA: 0x00041B91 File Offset: 0x0003FD91
		internal void InitializeListener(TraceListener listener)
		{
			if (listener == null)
			{
				throw new ArgumentNullException("listener");
			}
			listener.IndentSize = TraceInternal.IndentSize;
			listener.IndentLevel = TraceInternal.IndentLevel;
		}

		/// <summary>Gets the index of the specified listener.</summary>
		/// <returns>The index of the listener, if it can be found in the list; otherwise, -1.</returns>
		/// <param name="listener">A <see cref="T:System.Diagnostics.TraceListener" /> to find in the list. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000DF3 RID: 3571 RVA: 0x00041BB7 File Offset: 0x0003FDB7
		public int IndexOf(TraceListener listener)
		{
			return ((IList)this).IndexOf(listener);
		}

		/// <summary>Inserts the listener at the specified index.</summary>
		/// <param name="index">The position in the list to insert the new <see cref="T:System.Diagnostics.TraceListener" />. </param>
		/// <param name="listener">A <see cref="T:System.Diagnostics.TraceListener" /> to insert in the list. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> is not a valid index in the list. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="listener" /> is null.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000DF4 RID: 3572 RVA: 0x00041BC0 File Offset: 0x0003FDC0
		public void Insert(int index, TraceListener listener)
		{
			this.InitializeListener(listener);
			object critSec = TraceInternal.critSec;
			lock (critSec)
			{
				this.list.Insert(index, listener);
			}
		}

		/// <summary>Removes from the collection the specified <see cref="T:System.Diagnostics.TraceListener" />.</summary>
		/// <param name="listener">A <see cref="T:System.Diagnostics.TraceListener" /> to remove from the list. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000DF5 RID: 3573 RVA: 0x00041C10 File Offset: 0x0003FE10
		public void Remove(TraceListener listener)
		{
			((IList)this).Remove(listener);
		}

		/// <summary>Removes from the collection the first <see cref="T:System.Diagnostics.TraceListener" /> with the specified name.</summary>
		/// <param name="name">The name of the <see cref="T:System.Diagnostics.TraceListener" /> to remove from the list. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000DF6 RID: 3574 RVA: 0x00041C1C File Offset: 0x0003FE1C
		public void Remove(string name)
		{
			TraceListener traceListener = this[name];
			if (traceListener != null)
			{
				((IList)this).Remove(traceListener);
			}
		}

		/// <summary>Removes from the collection the <see cref="T:System.Diagnostics.TraceListener" /> at the specified index.</summary>
		/// <param name="index">The zero-based index of the <see cref="T:System.Diagnostics.TraceListener" /> to remove from the list. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> is not a valid index in the list. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000DF7 RID: 3575 RVA: 0x00041C3C File Offset: 0x0003FE3C
		public void RemoveAt(int index)
		{
			object critSec = TraceInternal.critSec;
			lock (critSec)
			{
				this.list.RemoveAt(index);
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Diagnostics.TraceListener" /> at the specified index in the <see cref="T:System.Diagnostics.TraceListenerCollection" />.</summary>
		/// <returns>The <see cref="T:System.Diagnostics.TraceListener" /> at the specified index.</returns>
		/// <param name="index">The zero-based index of the <paramref name="value" /> to get.</param>
		// Token: 0x17000284 RID: 644
		object IList.this[int index]
		{
			get
			{
				return this.list[index];
			}
			set
			{
				TraceListener traceListener = value as TraceListener;
				if (traceListener == null)
				{
					throw new ArgumentException(global::SR.GetString("Only TraceListeners can be added to a TraceListenerCollection."), "value");
				}
				this.InitializeListener(traceListener);
				this.list[index] = traceListener;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Diagnostics.TraceListenerCollection" /> is read-only</summary>
		/// <returns>Always false.</returns>
		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06000DFA RID: 3578 RVA: 0x00004240 File Offset: 0x00002440
		bool IList.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Diagnostics.TraceListenerCollection" /> has a fixed size.</summary>
		/// <returns>Always false.</returns>
		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000DFB RID: 3579 RVA: 0x00004240 File Offset: 0x00002440
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		/// <summary>Adds a trace listener to the <see cref="T:System.Diagnostics.TraceListenerCollection" />.</summary>
		/// <returns>The position into which the new trace listener was inserted.</returns>
		/// <param name="value">The object to add to the <see cref="T:System.Diagnostics.TraceListenerCollection" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is null. -or-<paramref name="value" /> is not a <see cref="T:System.Diagnostics.TraceListener" />.</exception>
		// Token: 0x06000DFC RID: 3580 RVA: 0x00041CD4 File Offset: 0x0003FED4
		int IList.Add(object value)
		{
			TraceListener traceListener = value as TraceListener;
			if (traceListener == null)
			{
				throw new ArgumentException(global::SR.GetString("Only TraceListeners can be added to a TraceListenerCollection."), "value");
			}
			this.InitializeListener(traceListener);
			object critSec = TraceInternal.critSec;
			int num;
			lock (critSec)
			{
				num = this.list.Add(value);
			}
			return num;
		}

		/// <summary>Determines whether the <see cref="T:System.Diagnostics.TraceListenerCollection" /> contains a specific object.</summary>
		/// <returns>true if the <see cref="T:System.Object" /> is found in the <see cref="T:System.Diagnostics.TraceListenerCollection" />; otherwise, false.</returns>
		/// <param name="value">The object to locate in the <see cref="T:System.Diagnostics.TraceListenerCollection" />.</param>
		// Token: 0x06000DFD RID: 3581 RVA: 0x00041D44 File Offset: 0x0003FF44
		bool IList.Contains(object value)
		{
			return this.list.Contains(value);
		}

		/// <summary>Determines the index of a specific object in the <see cref="T:System.Diagnostics.TraceListenerCollection" />.</summary>
		/// <returns>The index of <paramref name="value" /> if found in the <see cref="T:System.Diagnostics.TraceListenerCollection" />; otherwise, -1.</returns>
		/// <param name="value">The object to locate in the <see cref="T:System.Diagnostics.TraceListenerCollection" />.</param>
		// Token: 0x06000DFE RID: 3582 RVA: 0x00041D52 File Offset: 0x0003FF52
		int IList.IndexOf(object value)
		{
			return this.list.IndexOf(value);
		}

		/// <summary>Inserts a <see cref="T:System.Diagnostics.TraceListener" /> object at the specified position in the <see cref="T:System.Diagnostics.TraceListenerCollection" />.</summary>
		/// <param name="index">The zero-based index at which <paramref name="value" /> should be inserted.</param>
		/// <param name="value">The <see cref="T:System.Diagnostics.TraceListener" /> object to insert into the <see cref="T:System.Diagnostics.TraceListenerCollection" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is not a <see cref="T:System.Diagnostics.TraceListener" /> object.</exception>
		// Token: 0x06000DFF RID: 3583 RVA: 0x00041D60 File Offset: 0x0003FF60
		void IList.Insert(int index, object value)
		{
			TraceListener traceListener = value as TraceListener;
			if (traceListener == null)
			{
				throw new ArgumentException(global::SR.GetString("Only TraceListeners can be added to a TraceListenerCollection."), "value");
			}
			this.InitializeListener(traceListener);
			object critSec = TraceInternal.critSec;
			lock (critSec)
			{
				this.list.Insert(index, value);
			}
		}

		/// <summary>Removes an object from the <see cref="T:System.Diagnostics.TraceListenerCollection" />.</summary>
		/// <param name="value">The object to remove from the <see cref="T:System.Diagnostics.TraceListenerCollection" />.</param>
		// Token: 0x06000E00 RID: 3584 RVA: 0x00041DCC File Offset: 0x0003FFCC
		void IList.Remove(object value)
		{
			object critSec = TraceInternal.critSec;
			lock (critSec)
			{
				this.list.Remove(value);
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Diagnostics.TraceListenerCollection" />.</summary>
		/// <returns>The current <see cref="T:System.Diagnostics.TraceListenerCollection" /> object.</returns>
		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000E01 RID: 3585 RVA: 0x00002068 File Offset: 0x00000268
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Diagnostics.TraceListenerCollection" /> is synchronized (thread safe).</summary>
		/// <returns>Always true.</returns>
		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000E02 RID: 3586 RVA: 0x000027E2 File Offset: 0x000009E2
		bool ICollection.IsSynchronized
		{
			get
			{
				return true;
			}
		}

		/// <summary>Copies a section of the current <see cref="T:System.Diagnostics.TraceListenerCollection" /> to the specified array of <see cref="T:System.Diagnostics.TraceListener" /> objects. </summary>
		/// <param name="array">The one-dimensional array of <see cref="T:System.Diagnostics.TraceListener" /> objects that is the destination of the elements copied from the <see cref="T:System.Diagnostics.TraceListenerCollection" />. The array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		// Token: 0x06000E03 RID: 3587 RVA: 0x00041E14 File Offset: 0x00040014
		void ICollection.CopyTo(Array array, int index)
		{
			object critSec = TraceInternal.critSec;
			lock (critSec)
			{
				this.list.CopyTo(array, index);
			}
		}

		// Token: 0x04001078 RID: 4216
		private ArrayList list;
	}
}
