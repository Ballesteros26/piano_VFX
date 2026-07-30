using System;
using System.Collections;
using Unity;

namespace System.Diagnostics
{
	/// <summary>Defines size and enumerators for a collection of <see cref="T:System.Diagnostics.EventLogEntry" /> instances.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001F2 RID: 498
	public class EventLogEntryCollection : ICollection, IEnumerable
	{
		// Token: 0x06000FFA RID: 4090 RVA: 0x00049056 File Offset: 0x00047256
		internal EventLogEntryCollection(EventLogImpl impl)
		{
			this._impl = impl;
		}

		/// <summary>Gets the number of entries in the event log (that is, the number of elements in the <see cref="T:System.Diagnostics.EventLogEntry" /> collection).</summary>
		/// <returns>The number of entries currently in the event log.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06000FFB RID: 4091 RVA: 0x00049065 File Offset: 0x00047265
		public int Count
		{
			get
			{
				return this._impl.EntryCount;
			}
		}

		/// <summary>Gets an entry in the event log, based on an index that starts at 0 (zero).</summary>
		/// <returns>The event log entry at the location that is specified by the <paramref name="index" /> parameter.</returns>
		/// <param name="index">The zero-based index that is associated with the event log entry. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700031D RID: 797
		public virtual EventLogEntry this[int index]
		{
			get
			{
				return this._impl[index];
			}
		}

		/// <summary>Gets a value that indicates whether access to the <see cref="T:System.Diagnostics.EventLogEntryCollection" /> is synchronized (thread-safe).</summary>
		/// <returns>false if access to the collection is not synchronized (thread-safe).</returns>
		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06000FFD RID: 4093 RVA: 0x00004240 File Offset: 0x00002440
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Diagnostics.EventLogEntryCollection" /> object.</summary>
		/// <returns>An object that can be used to synchronize access to the collection.</returns>
		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06000FFE RID: 4094 RVA: 0x00002068 File Offset: 0x00000268
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Copies the elements of the <see cref="T:System.Diagnostics.EventLogEntryCollection" /> to an array of <see cref="T:System.Diagnostics.EventLogEntry" /> instances, starting at a particular array index.</summary>
		/// <param name="entries">The one-dimensional array of <see cref="T:System.Diagnostics.EventLogEntry" /> instances that is the destination of the elements copied from the collection. The array must have zero-based indexing. </param>
		/// <param name="index">The zero-based index in the array at which copying begins. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000FFF RID: 4095 RVA: 0x00049080 File Offset: 0x00047280
		public void CopyTo(EventLogEntry[] entries, int index)
		{
			EventLogEntry[] entries2 = this._impl.GetEntries();
			Array.Copy(entries2, 0, entries, index, entries2.Length);
		}

		/// <summary>Supports a simple iteration over the <see cref="T:System.Diagnostics.EventLogEntryCollection" /> object.</summary>
		/// <returns>An object that can be used to iterate over the collection.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001000 RID: 4096 RVA: 0x000490A5 File Offset: 0x000472A5
		public IEnumerator GetEnumerator()
		{
			return new EventLogEntryCollection.EventLogEntryEnumerator(this._impl);
		}

		/// <summary>Copies the elements of the collection to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements that are copied from the collection. The <see cref="T:System.Array" /> must have zero-based indexing. </param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins. </param>
		// Token: 0x06001001 RID: 4097 RVA: 0x000490B4 File Offset: 0x000472B4
		void ICollection.CopyTo(Array array, int index)
		{
			EventLogEntry[] entries = this._impl.GetEntries();
			Array.Copy(entries, 0, array, index, entries.Length);
		}

		// Token: 0x06001002 RID: 4098 RVA: 0x0000F0CE File Offset: 0x0000D2CE
		internal EventLogEntryCollection()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x0400113E RID: 4414
		private readonly EventLogImpl _impl;

		// Token: 0x020001F3 RID: 499
		private class EventLogEntryEnumerator : IEnumerator
		{
			// Token: 0x06001003 RID: 4099 RVA: 0x000490D9 File Offset: 0x000472D9
			internal EventLogEntryEnumerator(EventLogImpl impl)
			{
				this._impl = impl;
			}

			// Token: 0x17000320 RID: 800
			// (get) Token: 0x06001004 RID: 4100 RVA: 0x000490EF File Offset: 0x000472EF
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x17000321 RID: 801
			// (get) Token: 0x06001005 RID: 4101 RVA: 0x000490F7 File Offset: 0x000472F7
			public EventLogEntry Current
			{
				get
				{
					if (this._currentEntry != null)
					{
						return this._currentEntry;
					}
					throw new InvalidOperationException("No current EventLog entry available, cursor is located before the first or after the last element of the enumeration.");
				}
			}

			// Token: 0x06001006 RID: 4102 RVA: 0x00049114 File Offset: 0x00047314
			public bool MoveNext()
			{
				this._currentIndex++;
				if (this._currentIndex >= this._impl.EntryCount)
				{
					this._currentEntry = null;
					return false;
				}
				this._currentEntry = this._impl[this._currentIndex];
				return true;
			}

			// Token: 0x06001007 RID: 4103 RVA: 0x00049163 File Offset: 0x00047363
			public void Reset()
			{
				this._currentIndex = -1;
			}

			// Token: 0x0400113F RID: 4415
			private readonly EventLogImpl _impl;

			// Token: 0x04001140 RID: 4416
			private int _currentIndex = -1;

			// Token: 0x04001141 RID: 4417
			private EventLogEntry _currentEntry;
		}
	}
}
