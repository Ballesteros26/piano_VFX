using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.Security.Policy
{
	/// <summary>Represents a collection of <see cref="T:System.Security.Policy.ApplicationTrust" /> objects. This class cannot be inherited. </summary>
	// Token: 0x0200055A RID: 1370
	[ComVisible(true)]
	public sealed class ApplicationTrustCollection : ICollection, IEnumerable
	{
		// Token: 0x06003D97 RID: 15767 RVA: 0x000DD2C1 File Offset: 0x000DB4C1
		internal ApplicationTrustCollection()
		{
			this._list = new ArrayList();
		}

		/// <summary>Gets the number of items contained in the collection.</summary>
		/// <returns>The number of items contained in the collection.</returns>
		// Token: 0x17000A17 RID: 2583
		// (get) Token: 0x06003D98 RID: 15768 RVA: 0x000DD2D4 File Offset: 0x000DB4D4
		public int Count
		{
			[SecuritySafeCritical]
			get
			{
				return this._list.Count;
			}
		}

		/// <summary>Gets a value indicating whether access to the collection is synchronized (thread safe).</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x17000A18 RID: 2584
		// (get) Token: 0x06003D99 RID: 15769 RVA: 0x00015ED5 File Offset: 0x000140D5
		public bool IsSynchronized
		{
			[SecuritySafeCritical]
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
		/// <returns>The object to use to synchronize access to the collection.</returns>
		// Token: 0x17000A19 RID: 2585
		// (get) Token: 0x06003D9A RID: 15770 RVA: 0x00002119 File Offset: 0x00000319
		public object SyncRoot
		{
			[SecuritySafeCritical]
			get
			{
				return this;
			}
		}

		/// <summary>Gets the <see cref="T:System.Security.Policy.ApplicationTrust" /> object located at the specified index in the collection.</summary>
		/// <returns>The <see cref="T:System.Security.Policy.ApplicationTrust" /> object at the specified index in the collection.</returns>
		/// <param name="index">The zero-based index of the object within the collection.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is greater than or equal to the count of objects in the collection. </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="index" /> is negative. </exception>
		// Token: 0x17000A1A RID: 2586
		public ApplicationTrust this[int index]
		{
			get
			{
				return (ApplicationTrust)this._list[index];
			}
		}

		/// <summary>Gets the <see cref="T:System.Security.Policy.ApplicationTrust" /> object for the specified application.</summary>
		/// <returns>The <see cref="T:System.Security.Policy.ApplicationTrust" /> object for the specified application, or null if the object cannot be found.</returns>
		/// <param name="appFullName">The full name of the application.</param>
		// Token: 0x17000A1B RID: 2587
		public ApplicationTrust this[string appFullName]
		{
			get
			{
				for (int i = 0; i < this._list.Count; i++)
				{
					ApplicationTrust applicationTrust = this._list[i] as ApplicationTrust;
					if (applicationTrust.ApplicationIdentity.FullName == appFullName)
					{
						return applicationTrust;
					}
				}
				return null;
			}
		}

		/// <summary>Adds an element to the collection.</summary>
		/// <returns>The index at which the new element was inserted.</returns>
		/// <param name="trust">The <see cref="T:System.Security.Policy.ApplicationTrust" /> object to add.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="trust" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Security.Policy.ApplicationTrust.ApplicationIdentity" /> property of the <see cref="T:System.Security.Policy.ApplicationTrust" /> specified in <paramref name="trust" /> is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, ControlPolicy" />
		/// </PermissionSet>
		// Token: 0x06003D9D RID: 15773 RVA: 0x000DD33F File Offset: 0x000DB53F
		public int Add(ApplicationTrust trust)
		{
			if (trust == null)
			{
				throw new ArgumentNullException("trust");
			}
			if (trust.ApplicationIdentity == null)
			{
				throw new ArgumentException(Locale.GetText("ApplicationTrust.ApplicationIdentity can't be null."), "trust");
			}
			return this._list.Add(trust);
		}

		/// <summary>Copies the elements of the specified <see cref="T:System.Security.Policy.ApplicationTrust" /> array to the end of the collection.</summary>
		/// <param name="trusts">An array of type <see cref="T:System.Security.Policy.ApplicationTrust" /> containing the objects to add to the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="trusts" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Security.Policy.ApplicationTrust.ApplicationIdentity" /> property of an <see cref="T:System.Security.Policy.ApplicationTrust" /> specified in <paramref name="trust" /> is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, ControlPolicy" />
		/// </PermissionSet>
		// Token: 0x06003D9E RID: 15774 RVA: 0x000DD378 File Offset: 0x000DB578
		public void AddRange(ApplicationTrust[] trusts)
		{
			if (trusts == null)
			{
				throw new ArgumentNullException("trusts");
			}
			foreach (ApplicationTrust applicationTrust in trusts)
			{
				if (applicationTrust.ApplicationIdentity == null)
				{
					throw new ArgumentException(Locale.GetText("ApplicationTrust.ApplicationIdentity can't be null."), "trust");
				}
				this._list.Add(applicationTrust);
			}
		}

		/// <summary>Copies the elements of the specified <see cref="T:System.Security.Policy.ApplicationTrustCollection" /> to the end of the collection.</summary>
		/// <param name="trusts">A <see cref="T:System.Security.Policy.ApplicationTrustCollection" /> containing the objects to add to the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="trusts" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Security.Policy.ApplicationTrust.ApplicationIdentity" /> property of an <see cref="T:System.Security.Policy.ApplicationTrust" /> specified in <paramref name="trust" /> is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, ControlPolicy" />
		/// </PermissionSet>
		// Token: 0x06003D9F RID: 15775 RVA: 0x000DD3D4 File Offset: 0x000DB5D4
		public void AddRange(ApplicationTrustCollection trusts)
		{
			if (trusts == null)
			{
				throw new ArgumentNullException("trusts");
			}
			foreach (ApplicationTrust applicationTrust in trusts)
			{
				if (applicationTrust.ApplicationIdentity == null)
				{
					throw new ArgumentException(Locale.GetText("ApplicationTrust.ApplicationIdentity can't be null."), "trust");
				}
				this._list.Add(applicationTrust);
			}
		}

		/// <summary>Removes all the application trusts from the collection.</summary>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Security.Policy.ApplicationTrust.ApplicationIdentity" /> property of an item in the collection is null.</exception>
		// Token: 0x06003DA0 RID: 15776 RVA: 0x000DD431 File Offset: 0x000DB631
		public void Clear()
		{
			this._list.Clear();
		}

		/// <summary>Copies the entire collection to a compatible one-dimensional array, starting at the specified index of the target array.</summary>
		/// <param name="array">The one-dimensional array of type <see cref="T:System.Security.Policy.ApplicationTrust" /> that is the destination of the elements copied from the current collection. </param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than the lower bound of <paramref name="array" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or- The number of elements in the <see cref="T:System.Security.Policy.ApplicationTrustCollection" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlPolicy" />
		/// </PermissionSet>
		// Token: 0x06003DA1 RID: 15777 RVA: 0x000DD43E File Offset: 0x000DB63E
		public void CopyTo(ApplicationTrust[] array, int index)
		{
			this._list.CopyTo(array, index);
		}

		/// <summary>Copies the elements of the <see cref="T:System.Collections.ICollection" /> to the specified <see cref="T:System.Array" />, starting at the specified <see cref="T:System.Array" /> index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from the <see cref="T:System.Collections.ICollection" />. The <see cref="T:System.Array" /> must have zero-based indexing. </param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or- The number of elements in the source <see cref="T:System.Collections.ICollection" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />. </exception>
		// Token: 0x06003DA2 RID: 15778 RVA: 0x000DD43E File Offset: 0x000DB63E
		void ICollection.CopyTo(Array array, int index)
		{
			this._list.CopyTo(array, index);
		}

		/// <summary>Gets the application trusts in the collection that match the specified application identity.</summary>
		/// <returns>An <see cref="T:System.Security.Policy.ApplicationTrustCollection" /> containing all matching <see cref="T:System.Security.Policy.ApplicationTrust" /> objects.</returns>
		/// <param name="applicationIdentity">An <see cref="T:System.ApplicationIdentity" /> object describing the application to find.</param>
		/// <param name="versionMatch">One of the <see cref="T:System.Security.Policy.ApplicationVersionMatch" /> values. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, ControlPolicy" />
		/// </PermissionSet>
		// Token: 0x06003DA3 RID: 15779 RVA: 0x000DD450 File Offset: 0x000DB650
		public ApplicationTrustCollection Find(ApplicationIdentity applicationIdentity, ApplicationVersionMatch versionMatch)
		{
			if (applicationIdentity == null)
			{
				throw new ArgumentNullException("applicationIdentity");
			}
			string text = applicationIdentity.FullName;
			if (versionMatch != ApplicationVersionMatch.MatchExactVersion)
			{
				if (versionMatch != ApplicationVersionMatch.MatchAllVersions)
				{
					throw new ArgumentException("versionMatch");
				}
				int num = text.IndexOf(", Version=");
				if (num >= 0)
				{
					text = text.Substring(0, num);
				}
			}
			ApplicationTrustCollection applicationTrustCollection = new ApplicationTrustCollection();
			foreach (object obj in this._list)
			{
				ApplicationTrust applicationTrust = (ApplicationTrust)obj;
				if (applicationTrust.ApplicationIdentity.FullName.StartsWith(text))
				{
					applicationTrustCollection.Add(applicationTrust);
				}
			}
			return applicationTrustCollection;
		}

		/// <summary>Returns an object that can be used to iterate over the collection.</summary>
		/// <returns>An <see cref="T:System.Security.Policy.ApplicationTrustEnumerator" /> that can be used to iterate over the collection.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlPolicy" />
		/// </PermissionSet>
		// Token: 0x06003DA4 RID: 15780 RVA: 0x000DD50C File Offset: 0x000DB70C
		public ApplicationTrustEnumerator GetEnumerator()
		{
			return new ApplicationTrustEnumerator(this);
		}

		/// <summary>Returns an enumerator that iterates through the collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
		// Token: 0x06003DA5 RID: 15781 RVA: 0x000DD50C File Offset: 0x000DB70C
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new ApplicationTrustEnumerator(this);
		}

		/// <summary>Removes the specified application trust from the collection.</summary>
		/// <param name="trust">The <see cref="T:System.Security.Policy.ApplicationTrust" /> object to remove.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="trust" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Security.Policy.ApplicationTrust.ApplicationIdentity" /> property of the <see cref="T:System.Security.Policy.ApplicationTrust" /> object specified by <paramref name="trust" /> is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, ControlPolicy" />
		/// </PermissionSet>
		// Token: 0x06003DA6 RID: 15782 RVA: 0x000DD514 File Offset: 0x000DB714
		public void Remove(ApplicationTrust trust)
		{
			if (trust == null)
			{
				throw new ArgumentNullException("trust");
			}
			if (trust.ApplicationIdentity == null)
			{
				throw new ArgumentException(Locale.GetText("ApplicationTrust.ApplicationIdentity can't be null."), "trust");
			}
			this.RemoveAllInstances(trust);
		}

		/// <summary>Removes all application trust objects that match the specified criteria from the collection.</summary>
		/// <param name="applicationIdentity">The <see cref="T:System.ApplicationIdentity" /> of the <see cref="T:System.Security.Policy.ApplicationTrust" /> object to be removed.</param>
		/// <param name="versionMatch">One of the <see cref="T:System.Security.Policy.ApplicationVersionMatch" /> values.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, ControlPolicy" />
		/// </PermissionSet>
		// Token: 0x06003DA7 RID: 15783 RVA: 0x000DD548 File Offset: 0x000DB748
		public void Remove(ApplicationIdentity applicationIdentity, ApplicationVersionMatch versionMatch)
		{
			foreach (ApplicationTrust applicationTrust in this.Find(applicationIdentity, versionMatch))
			{
				this.RemoveAllInstances(applicationTrust);
			}
		}

		/// <summary>Removes the application trust objects in the specified array from the collection.</summary>
		/// <param name="trusts">A one-dimensional array of type <see cref="T:System.Security.Policy.ApplicationTrust" /> that contains items to be removed from the current collection. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="trusts" /> is null. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, ControlPolicy" />
		/// </PermissionSet>
		// Token: 0x06003DA8 RID: 15784 RVA: 0x000DD57C File Offset: 0x000DB77C
		public void RemoveRange(ApplicationTrust[] trusts)
		{
			if (trusts == null)
			{
				throw new ArgumentNullException("trusts");
			}
			foreach (ApplicationTrust applicationTrust in trusts)
			{
				this.RemoveAllInstances(applicationTrust);
			}
		}

		/// <summary>Removes the application trust objects in the specified collection from the collection.</summary>
		/// <param name="trusts">An <see cref="T:System.Security.Policy.ApplicationTrustCollection" /> that contains items to be removed from the currentcollection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="trusts" /> is null. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, ControlPolicy" />
		/// </PermissionSet>
		// Token: 0x06003DA9 RID: 15785 RVA: 0x000DD5B4 File Offset: 0x000DB7B4
		public void RemoveRange(ApplicationTrustCollection trusts)
		{
			if (trusts == null)
			{
				throw new ArgumentNullException("trusts");
			}
			foreach (ApplicationTrust applicationTrust in trusts)
			{
				this.RemoveAllInstances(applicationTrust);
			}
		}

		// Token: 0x06003DAA RID: 15786 RVA: 0x000DD5F0 File Offset: 0x000DB7F0
		internal void RemoveAllInstances(ApplicationTrust trust)
		{
			for (int i = this._list.Count - 1; i >= 0; i--)
			{
				if (trust.Equals(this._list[i]))
				{
					this._list.RemoveAt(i);
				}
			}
		}

		// Token: 0x04001FA1 RID: 8097
		private ArrayList _list;
	}
}
