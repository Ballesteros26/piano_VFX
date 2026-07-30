using System;
using System.Collections.Specialized;
using System.Security.Permissions;
using System.Threading;

namespace System.Web
{
	/// <summary>Enables sharing of global information across multiple sessions and requests within an ASP.NET application.</summary>
	// Token: 0x0200007F RID: 127
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class HttpApplicationState : NameObjectCollectionBase
	{
		// Token: 0x06000589 RID: 1417 RVA: 0x0000DD24 File Offset: 0x0000BF24
		internal HttpApplicationState()
		{
			this._Lock = new ReaderWriterLockSlim();
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x0000DD37 File Offset: 0x0000BF37
		internal HttpApplicationState(HttpStaticObjectsCollection AppObj, HttpStaticObjectsCollection SessionObj)
		{
			this._AppObjects = AppObj;
			this._SessionObjects = SessionObj;
			this._Lock = new ReaderWriterLockSlim();
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x0600058B RID: 1419 RVA: 0x0000DD58 File Offset: 0x0000BF58
		private bool IsLockHeld
		{
			get
			{
				return this._Lock.IsReadLockHeld || this._Lock.IsWriteLockHeld;
			}
		}

		/// <summary>Adds a new object to the <see cref="T:System.Web.HttpApplicationState" /> collection.</summary>
		/// <param name="name">The name of the object to be added to the collection. </param>
		/// <param name="value">The value of the object. </param>
		// Token: 0x0600058C RID: 1420 RVA: 0x0000DD74 File Offset: 0x0000BF74
		public void Add(string name, object value)
		{
			bool flag = false;
			try
			{
				if (!this.IsLockHeld)
				{
					this._Lock.EnterWriteLock();
					flag = true;
				}
				base.BaseAdd(name, value);
			}
			finally
			{
				if (flag && this.IsLockHeld)
				{
					this._Lock.ExitWriteLock();
				}
			}
		}

		/// <summary>Removes all objects from an <see cref="T:System.Web.HttpApplicationState" /> collection.</summary>
		// Token: 0x0600058D RID: 1421 RVA: 0x0000DDCC File Offset: 0x0000BFCC
		public void Clear()
		{
			bool flag = false;
			try
			{
				if (!this.IsLockHeld)
				{
					this._Lock.EnterWriteLock();
					flag = true;
				}
				base.BaseClear();
			}
			finally
			{
				if (flag && this.IsLockHeld)
				{
					this._Lock.ExitWriteLock();
				}
			}
		}

		/// <summary>Gets an <see cref="T:System.Web.HttpApplicationState" /> object by name.</summary>
		/// <returns>The object referenced by <paramref name="name" />.</returns>
		/// <param name="name">The name of the object. </param>
		// Token: 0x0600058E RID: 1422 RVA: 0x0000DE20 File Offset: 0x0000C020
		public object Get(string name)
		{
			object obj = null;
			bool flag = false;
			try
			{
				if (!this.IsLockHeld)
				{
					this._Lock.EnterReadLock();
					flag = true;
				}
				obj = base.BaseGet(name);
			}
			finally
			{
				if (flag && this.IsLockHeld)
				{
					this._Lock.ExitReadLock();
				}
			}
			return obj;
		}

		/// <summary>Gets an <see cref="T:System.Web.HttpApplicationState" /> object by numerical index.</summary>
		/// <returns>The object referenced by <paramref name="index" />.</returns>
		/// <param name="index">The index of the application state object. </param>
		// Token: 0x0600058F RID: 1423 RVA: 0x0000DE78 File Offset: 0x0000C078
		public object Get(int index)
		{
			bool flag = false;
			object obj;
			try
			{
				if (!this.IsLockHeld)
				{
					this._Lock.EnterReadLock();
					flag = true;
				}
				obj = base.BaseGet(index);
			}
			finally
			{
				if (flag && this.IsLockHeld)
				{
					this._Lock.ExitReadLock();
				}
			}
			return obj;
		}

		/// <summary>Gets an <see cref="T:System.Web.HttpApplicationState" /> object name by index.</summary>
		/// <returns>The name under which the application state object was saved.</returns>
		/// <param name="index">The index of the application state object. </param>
		// Token: 0x06000590 RID: 1424 RVA: 0x0000DED0 File Offset: 0x0000C0D0
		public string GetKey(int index)
		{
			bool flag = false;
			string text;
			try
			{
				if (!this.IsLockHeld)
				{
					this._Lock.EnterReadLock();
					flag = true;
				}
				text = base.BaseGetKey(index);
			}
			finally
			{
				if (flag && this.IsLockHeld)
				{
					this._Lock.ExitReadLock();
				}
			}
			return text;
		}

		/// <summary>Locks access to an <see cref="T:System.Web.HttpApplicationState" /> variable to facilitate access synchronization.</summary>
		// Token: 0x06000591 RID: 1425 RVA: 0x0000DF28 File Offset: 0x0000C128
		public void Lock()
		{
			if (!this._Lock.IsWriteLockHeld)
			{
				this._Lock.EnterWriteLock();
			}
		}

		/// <summary>Removes the named object from an <see cref="T:System.Web.HttpApplicationState" /> collection.</summary>
		/// <param name="name">The name of the object to be removed from the collection. </param>
		// Token: 0x06000592 RID: 1426 RVA: 0x0000DF44 File Offset: 0x0000C144
		public void Remove(string name)
		{
			bool flag = false;
			try
			{
				if (!this.IsLockHeld)
				{
					this._Lock.EnterWriteLock();
					flag = true;
				}
				base.BaseRemove(name);
			}
			finally
			{
				if (flag && this.IsLockHeld)
				{
					this._Lock.ExitWriteLock();
				}
			}
		}

		/// <summary>Removes all objects from an <see cref="T:System.Web.HttpApplicationState" /> collection.</summary>
		// Token: 0x06000593 RID: 1427 RVA: 0x0000DF98 File Offset: 0x0000C198
		public void RemoveAll()
		{
			this.Clear();
		}

		/// <summary>Removes an <see cref="T:System.Web.HttpApplicationState" /> object from a collection by index.</summary>
		/// <param name="index">The position in the collection of the item to remove. </param>
		// Token: 0x06000594 RID: 1428 RVA: 0x0000DFA0 File Offset: 0x0000C1A0
		public void RemoveAt(int index)
		{
			bool flag = false;
			try
			{
				if (!this.IsLockHeld)
				{
					this._Lock.EnterWriteLock();
					flag = true;
				}
				base.BaseRemoveAt(index);
			}
			finally
			{
				if (flag && this.IsLockHeld)
				{
					this._Lock.ExitWriteLock();
				}
			}
		}

		/// <summary>Updates the value of an object in an <see cref="T:System.Web.HttpApplicationState" /> collection.</summary>
		/// <param name="name">The name of the object to be updated. </param>
		/// <param name="value">The updated value of the object. </param>
		// Token: 0x06000595 RID: 1429 RVA: 0x0000DFF4 File Offset: 0x0000C1F4
		public void Set(string name, object value)
		{
			bool flag = false;
			try
			{
				if (!this.IsLockHeld)
				{
					this._Lock.EnterWriteLock();
					flag = true;
				}
				base.BaseSet(name, value);
			}
			finally
			{
				if (flag && this.IsLockHeld)
				{
					this._Lock.ExitWriteLock();
				}
			}
		}

		/// <summary>Unlocks access to an <see cref="T:System.Web.HttpApplicationState" /> variable to facilitate access synchronization.</summary>
		// Token: 0x06000596 RID: 1430 RVA: 0x0000E04C File Offset: 0x0000C24C
		public void UnLock()
		{
			if (this._Lock.IsWriteLockHeld)
			{
				this._Lock.ExitWriteLock();
			}
		}

		/// <summary>Gets the access keys in the <see cref="T:System.Web.HttpApplicationState" /> collection.</summary>
		/// <returns>A string array of <see cref="T:System.Web.HttpApplicationState" /> object names.</returns>
		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000597 RID: 1431 RVA: 0x0000E068 File Offset: 0x0000C268
		public string[] AllKeys
		{
			get
			{
				bool flag = false;
				string[] array;
				try
				{
					if (!this.IsLockHeld)
					{
						this._Lock.EnterReadLock();
						flag = true;
					}
					array = base.BaseGetAllKeys();
				}
				finally
				{
					if (flag && this.IsLockHeld)
					{
						this._Lock.ExitReadLock();
					}
				}
				return array;
			}
		}

		/// <summary>Gets a reference to the <see cref="T:System.Web.HttpApplicationState" /> object.</summary>
		/// <returns>A reference to the <see cref="T:System.Web.HttpApplicationState" /> object.</returns>
		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06000598 RID: 1432 RVA: 0x00002058 File Offset: 0x00000258
		public HttpApplicationState Contents
		{
			get
			{
				return this;
			}
		}

		/// <summary>Gets the number of objects in the <see cref="T:System.Web.HttpApplicationState" /> collection.</summary>
		/// <returns>The number of item objects in the collection. The default is 0.</returns>
		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000599 RID: 1433 RVA: 0x0000E0C0 File Offset: 0x0000C2C0
		public override int Count
		{
			get
			{
				bool flag = false;
				int count;
				try
				{
					if (!this.IsLockHeld)
					{
						this._Lock.EnterReadLock();
						flag = true;
					}
					count = base.Count;
				}
				finally
				{
					if (flag && this.IsLockHeld)
					{
						this._Lock.ExitReadLock();
					}
				}
				return count;
			}
		}

		/// <summary>Gets the value of a single <see cref="T:System.Web.HttpApplicationState" /> object by name.</summary>
		/// <returns>The object referenced by <paramref name="name" />.</returns>
		/// <param name="name">The name of the object in the collection. </param>
		// Token: 0x1700021D RID: 541
		public object this[string name]
		{
			get
			{
				return this.Get(name);
			}
			set
			{
				this.Set(name, value);
			}
		}

		/// <summary>Gets a single <see cref="T:System.Web.HttpApplicationState" /> object by index.</summary>
		/// <returns>The object referenced by <paramref name="index" />.</returns>
		/// <param name="index">The numerical index of the object in the collection. </param>
		// Token: 0x1700021E RID: 542
		public object this[int index]
		{
			get
			{
				return this.Get(index);
			}
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x0600059D RID: 1437 RVA: 0x0000E134 File Offset: 0x0000C334
		internal HttpStaticObjectsCollection SessionObjects
		{
			get
			{
				if (this._SessionObjects == null)
				{
					this._SessionObjects = new HttpStaticObjectsCollection();
				}
				return this._SessionObjects;
			}
		}

		/// <summary>Gets all objects declared by an &lt;object&gt; tag where the scope is set to "Application" within the ASP.NET application.</summary>
		/// <returns>A collection of objects on the page.</returns>
		// Token: 0x17000220 RID: 544
		// (get) Token: 0x0600059E RID: 1438 RVA: 0x0000E14F File Offset: 0x0000C34F
		public HttpStaticObjectsCollection StaticObjects
		{
			get
			{
				if (this._AppObjects == null)
				{
					this._AppObjects = new HttpStaticObjectsCollection();
				}
				return this._AppObjects;
			}
		}

		// Token: 0x04000EFA RID: 3834
		private HttpStaticObjectsCollection _AppObjects;

		// Token: 0x04000EFB RID: 3835
		private HttpStaticObjectsCollection _SessionObjects;

		// Token: 0x04000EFC RID: 3836
		private ReaderWriterLockSlim _Lock;
	}
}
