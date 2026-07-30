using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Channels
{
	/// <summary>Provides a base implementation of a channel object that exposes a dictionary interface to its properties.</summary>
	// Token: 0x02000793 RID: 1939
	[ComVisible(true)]
	public abstract class BaseChannelObjectWithProperties : IDictionary, ICollection, IEnumerable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Channels.BaseChannelObjectWithProperties" /> class.</summary>
		// Token: 0x06004F6B RID: 20331 RVA: 0x0011DC5D File Offset: 0x0011BE5D
		protected BaseChannelObjectWithProperties()
		{
			this.table = new Hashtable();
		}

		/// <summary>Gets the number of properties associated with the channel object.</summary>
		/// <returns>The number of properties associated with the channel object.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000D48 RID: 3400
		// (get) Token: 0x06004F6C RID: 20332 RVA: 0x0011DC70 File Offset: 0x0011BE70
		public virtual int Count
		{
			[SecuritySafeCritical]
			get
			{
				return this.table.Count;
			}
		}

		/// <summary>Gets a value that indicates whether the number of properties that can be entered into the channel object is fixed.</summary>
		/// <returns>true if the number of properties that can be entered into the channel object is fixed; otherwise, false.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000D49 RID: 3401
		// (get) Token: 0x06004F6D RID: 20333 RVA: 0x00003B29 File Offset: 0x00001D29
		public virtual bool IsFixedSize
		{
			[SecuritySafeCritical]
			get
			{
				return true;
			}
		}

		/// <summary>Gets a value that indicates whether the collection of properties in the channel object is read-only.</summary>
		/// <returns>true if the collection of properties in the channel object is read-only; otherwise, false.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000D4A RID: 3402
		// (get) Token: 0x06004F6E RID: 20334 RVA: 0x00015ED5 File Offset: 0x000140D5
		public virtual bool IsReadOnly
		{
			[SecuritySafeCritical]
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value that indicates whether the dictionary of channel object properties is synchronized.</summary>
		/// <returns>true if the dictionary of channel object properties is synchronized; otherwise, false.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000D4B RID: 3403
		// (get) Token: 0x06004F6F RID: 20335 RVA: 0x00015ED5 File Offset: 0x000140D5
		public virtual bool IsSynchronized
		{
			[SecuritySafeCritical]
			get
			{
				return false;
			}
		}

		/// <summary>When overridden in a derived class, gets or sets the property that is associated with the specified key.</summary>
		/// <returns>The property that is associated with the specified key.</returns>
		/// <param name="key">The key of the property to get or set. </param>
		/// <exception cref="T:System.NotImplementedException">The property is accessed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000D4C RID: 3404
		public virtual object this[object key]
		{
			[SecuritySafeCritical]
			get
			{
				throw new NotImplementedException();
			}
			[SecuritySafeCritical]
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets a <see cref="T:System.Collections.ICollection" /> of keys that the channel object properties are associated with.</summary>
		/// <returns>A <see cref="T:System.Collections.ICollection" /> of keys that the channel object properties are associated with.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000D4D RID: 3405
		// (get) Token: 0x06004F72 RID: 20338 RVA: 0x0011DC7D File Offset: 0x0011BE7D
		public virtual ICollection Keys
		{
			[SecuritySafeCritical]
			get
			{
				return this.table.Keys;
			}
		}

		/// <summary>Gets a <see cref="T:System.Collections.IDictionary" /> of the channel properties associated with the channel object.</summary>
		/// <returns>A <see cref="T:System.Collections.IDictionary" /> of the channel properties associated with the channel object.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		// Token: 0x17000D4E RID: 3406
		// (get) Token: 0x06004F73 RID: 20339 RVA: 0x00002119 File Offset: 0x00000319
		public virtual IDictionary Properties
		{
			get
			{
				return this;
			}
		}

		/// <summary>Gets an object that is used to synchronize access to the <see cref="T:System.Runtime.Remoting.Channels.BaseChannelObjectWithProperties" />.</summary>
		/// <returns>An object that is used to synchronize access to the <see cref="T:System.Runtime.Remoting.Channels.BaseChannelObjectWithProperties" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000D4F RID: 3407
		// (get) Token: 0x06004F74 RID: 20340 RVA: 0x00002119 File Offset: 0x00000319
		public virtual object SyncRoot
		{
			[SecuritySafeCritical]
			get
			{
				return this;
			}
		}

		/// <summary>Gets a <see cref="T:System.Collections.ICollection" /> of the values of the properties associated with the channel object.</summary>
		/// <returns>A <see cref="T:System.Collections.ICollection" /> of the values of the properties associated with the channel object.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000D50 RID: 3408
		// (get) Token: 0x06004F75 RID: 20341 RVA: 0x0011DC8A File Offset: 0x0011BE8A
		public virtual ICollection Values
		{
			[SecuritySafeCritical]
			get
			{
				return this.table.Values;
			}
		}

		/// <summary>Throws a <see cref="T:System.NotSupportedException" />.</summary>
		/// <param name="key">The key that is associated with the object in the <paramref name="value" /> parameter. </param>
		/// <param name="value">The value to add. </param>
		/// <exception cref="T:System.NotSupportedException">The method is called. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06004F76 RID: 20342 RVA: 0x00014B5A File Offset: 0x00012D5A
		[SecuritySafeCritical]
		public virtual void Add(object key, object value)
		{
			throw new NotSupportedException();
		}

		/// <summary>Throws a <see cref="T:System.NotSupportedException" />.</summary>
		/// <exception cref="T:System.NotSupportedException">The method is called. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06004F77 RID: 20343 RVA: 0x00014B5A File Offset: 0x00012D5A
		[SecuritySafeCritical]
		public virtual void Clear()
		{
			throw new NotSupportedException();
		}

		/// <summary>Returns a value that indicates whether the channel object contains a property that is associated with the specified key.</summary>
		/// <returns>true if the channel object contains a property associated with the specified key; otherwise, false.</returns>
		/// <param name="key">The key of the property to look for. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06004F78 RID: 20344 RVA: 0x0011DC97 File Offset: 0x0011BE97
		[SecuritySafeCritical]
		public virtual bool Contains(object key)
		{
			return this.table.Contains(key);
		}

		/// <summary>Throws a <see cref="T:System.NotSupportedException" />.</summary>
		/// <param name="array">The array to copy the properties to. </param>
		/// <param name="index">The index at which to begin copying. </param>
		/// <exception cref="T:System.NotSupportedException">The method is called. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06004F79 RID: 20345 RVA: 0x00014B5A File Offset: 0x00012D5A
		[SecuritySafeCritical]
		public virtual void CopyTo(Array array, int index)
		{
			throw new NotSupportedException();
		}

		/// <summary>Returns a <see cref="T:System.Collections.IDictionaryEnumerator" /> that enumerates over all the properties associated with the channel object.</summary>
		/// <returns>A <see cref="T:System.Collections.IDictionaryEnumerator" /> that enumerates over all the properties associated with the channel object.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06004F7A RID: 20346 RVA: 0x0011DCA5 File Offset: 0x0011BEA5
		[SecuritySafeCritical]
		public virtual IDictionaryEnumerator GetEnumerator()
		{
			return this.table.GetEnumerator();
		}

		/// <summary>Returns a <see cref="T:System.Collections.IEnumerator" /> that enumerates over all the properties that are associated with the channel object.</summary>
		/// <returns>A <see cref="T:System.Collections.IEnumerator" /> that enumerates over all the properties that are associated with the channel object.</returns>
		// Token: 0x06004F7B RID: 20347 RVA: 0x0011DCA5 File Offset: 0x0011BEA5
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.table.GetEnumerator();
		}

		/// <summary>Throws a <see cref="T:System.NotSupportedException" />.</summary>
		/// <param name="key">The key of the object to be removed. </param>
		/// <exception cref="T:System.NotSupportedException">The method is called. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06004F7C RID: 20348 RVA: 0x00014B5A File Offset: 0x00012D5A
		[SecuritySafeCritical]
		public virtual void Remove(object key)
		{
			throw new NotSupportedException();
		}

		// Token: 0x04002A43 RID: 10819
		private Hashtable table;
	}
}
