using System;
using System.Collections;
using System.Security.Permissions;

namespace System.Web.UI
{
	/// <summary>Provides a collection container that enables ASP.NET server controls to maintain a list of their child controls.</summary>
	// Token: 0x020001B8 RID: 440
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class ControlCollection : ICollection, IEnumerable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.ControlCollection" /> class for the specified parent server control.</summary>
		/// <param name="owner">The ASP.NET server control that the control collection is created for. </param>
		/// <exception cref="T:System.ArgumentNullException">Occurs if the <paramref name="owner" /> parameter is null. </exception>
		// Token: 0x060011E3 RID: 4579 RVA: 0x000316ED File Offset: 0x0002F8ED
		public ControlCollection(Control owner)
		{
			if (owner == null)
			{
				throw new ArgumentException("owner");
			}
			this.owner = owner;
		}

		/// <summary>Gets the number of server controls in the <see cref="T:System.Web.UI.ControlCollection" /> object for the specified ASP.NET server control.</summary>
		/// <returns>The number of server controls in the <see cref="T:System.Web.UI.ControlCollection" />.</returns>
		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x060011E4 RID: 4580 RVA: 0x0003170A File Offset: 0x0002F90A
		public virtual int Count
		{
			get
			{
				return this.count;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.ControlCollection" /> object is read-only.</summary>
		/// <returns>true if the control is read-only; otherwise, false. The default is false.</returns>
		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x060011E5 RID: 4581 RVA: 0x00031712 File Offset: 0x0002F912
		public bool IsReadOnly
		{
			get
			{
				return this.readOnly;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.ControlCollection" /> object is synchronized.</summary>
		/// <returns>This property is always false.</returns>
		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x060011E6 RID: 4582 RVA: 0x00008A69 File Offset: 0x00006C69
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a reference to the server control at the specified index location in the <see cref="T:System.Web.UI.ControlCollection" /> object.</summary>
		/// <returns>The reference to the control.</returns>
		/// <param name="index">The location of the server control in the <see cref="T:System.Web.UI.ControlCollection" />. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is less than zero or greater than or equal to <see cref="P:System.Web.UI.ControlCollection.Count" />. </exception>
		// Token: 0x170005CC RID: 1484
		public virtual Control this[int index]
		{
			get
			{
				if (index < 0 || index >= this.count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				return this.controls[index];
			}
		}

		/// <summary>Gets the ASP.NET server control to which the <see cref="T:System.Web.UI.ControlCollection" /> object belongs.</summary>
		/// <returns>The <see cref="T:System.Web.UI.Control" /> to which the <see cref="T:System.Web.UI.ControlCollection" /> belongs.</returns>
		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x060011E8 RID: 4584 RVA: 0x0003173C File Offset: 0x0002F93C
		protected Control Owner
		{
			get
			{
				return this.owner;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the collection of controls.</summary>
		/// <returns>The <see cref="T:System.Object" /> used to synchronize the collection.</returns>
		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x060011E9 RID: 4585 RVA: 0x00002058 File Offset: 0x00000258
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x060011EA RID: 4586 RVA: 0x00031744 File Offset: 0x0002F944
		private void EnsureControls()
		{
			if (this.controls == null)
			{
				this.controls = new Control[5];
				return;
			}
			if (this.controls.Length < this.count + 1)
			{
				int num = ((this.controls.Length == 5) ? 3 : 2);
				Control[] array = new Control[this.controls.Length * num];
				Array.Copy(this.controls, 0, array, 0, this.controls.Length);
				this.controls = array;
			}
		}

		/// <summary>Adds the specified <see cref="T:System.Web.UI.Control" /> object to the collection.</summary>
		/// <param name="child">The <see cref="T:System.Web.UI.Control" /> to add to the collection. </param>
		/// <exception cref="T:System.ArgumentNullException">Thrown if the <paramref name="child" /> parameter does not specify a control. </exception>
		/// <exception cref="T:System.Web.HttpException">Thrown if the <see cref="T:System.Web.UI.ControlCollection" /> is read-only. </exception>
		// Token: 0x060011EB RID: 4587 RVA: 0x000317B8 File Offset: 0x0002F9B8
		public virtual void Add(Control child)
		{
			if (child == null)
			{
				throw new ArgumentNullException("child");
			}
			if (this.readOnly)
			{
				throw new HttpException(global::Locale.GetText("Collection is read-only."));
			}
			if (this.owner == child)
			{
				throw new HttpException(global::Locale.GetText("Cannot add collection's owner."));
			}
			this.EnsureControls();
			this.version++;
			Control[] array = this.controls;
			int num = this.count;
			this.count = num + 1;
			array[num] = child;
			this.owner.AddedControl(child, this.count - 1);
		}

		/// <summary>Adds the specified <see cref="T:System.Web.UI.Control" /> object to the collection at the specified index location.</summary>
		/// <param name="index">The location in the array at which to add the child control. </param>
		/// <param name="child">The <see cref="T:System.Web.UI.Control" /> to add to the collection. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="child" /> parameter does not specify a control. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is less than zero or greater than the <see cref="P:System.Web.UI.ControlCollection.Count" /> property. </exception>
		/// <exception cref="T:System.Web.HttpException">The <see cref="T:System.Web.UI.ControlCollection" /> is read-only. </exception>
		// Token: 0x060011EC RID: 4588 RVA: 0x00031848 File Offset: 0x0002FA48
		public virtual void AddAt(int index, Control child)
		{
			if (child == null)
			{
				throw new ArgumentNullException();
			}
			if (index < -1 || index > this.count)
			{
				throw new ArgumentOutOfRangeException();
			}
			if (this.readOnly)
			{
				throw new HttpException(global::Locale.GetText("Collection is read-only."));
			}
			if (this.owner == child)
			{
				throw new HttpException(global::Locale.GetText("Cannot add collection's owner."));
			}
			if (index == -1)
			{
				this.Add(child);
				return;
			}
			this.EnsureControls();
			this.version++;
			Array.Copy(this.controls, index, this.controls, index + 1, this.count - index);
			this.count++;
			this.controls[index] = child;
			this.owner.AddedControl(child, index);
		}

		/// <summary>Removes all controls from the current server control's <see cref="T:System.Web.UI.ControlCollection" /> object.</summary>
		// Token: 0x060011ED RID: 4589 RVA: 0x00031904 File Offset: 0x0002FB04
		public virtual void Clear()
		{
			if (this.controls == null)
			{
				return;
			}
			this.version++;
			for (int i = 0; i < this.count; i++)
			{
				this.owner.RemovedControl(this.controls[i]);
			}
			this.count = 0;
			if (this.owner != null)
			{
				this.owner.ResetChildNames();
			}
		}

		/// <summary>Determines whether the specified server control is in the parent server control's <see cref="T:System.Web.UI.ControlCollection" /> object.</summary>
		/// <returns>true if the specified server control exists in the collection; otherwise, false.</returns>
		/// <param name="c">The server control to search for in the collection. </param>
		// Token: 0x060011EE RID: 4590 RVA: 0x00031966 File Offset: 0x0002FB66
		public virtual bool Contains(Control c)
		{
			return this.controls != null && Array.IndexOf<Control>(this.controls, c) != -1;
		}

		/// <summary>Copies the child controls stored in the <see cref="T:System.Web.UI.ControlCollection" /> object to an <see cref="T:System.Array" /> object, beginning at the specified index location in the <see cref="T:System.Array" />.</summary>
		/// <param name="array">The <see cref="T:System.Array" /> to copy the child controls to. </param>
		/// <param name="index">The zero-based relative index in <paramref name="array" /> where copying begins. </param>
		/// <exception cref="T:System.Web.HttpException">
		///   <paramref name="array" /> is not null and not one-dimensional. </exception>
		// Token: 0x060011EF RID: 4591 RVA: 0x00031984 File Offset: 0x0002FB84
		public virtual void CopyTo(Array array, int index)
		{
			if (this.controls == null)
			{
				return;
			}
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index + this.count > array.GetLowerBound(0) + array.GetLength(0))
			{
				throw new ArgumentException();
			}
			if (array.Rank > 1)
			{
				throw new RankException(global::Locale.GetText("Only single dimension arrays are supported."));
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", global::Locale.GetText("Value has to be >= 0."));
			}
			for (int i = 0; i < this.count; i++)
			{
				array.SetValue(this.controls[i], i + index);
			}
		}

		/// <summary>Retrieves an enumerator that can iterate through the <see cref="T:System.Web.UI.ControlCollection" /> object.</summary>
		/// <returns>The enumerator to iterate through the collection.</returns>
		// Token: 0x060011F0 RID: 4592 RVA: 0x00031A1B File Offset: 0x0002FC1B
		public virtual IEnumerator GetEnumerator()
		{
			return new ControlCollection.SimpleEnumerator(this);
		}

		/// <summary>Retrieves the index of a specified <see cref="T:System.Web.UI.Control" /> object in the collection.</summary>
		/// <returns>The index of the specified server control. If the server control is not currently a member of the collection, it returns -1.</returns>
		/// <param name="value">The <see cref="T:System.Web.UI.Control" /> for which the index is returned. </param>
		// Token: 0x060011F1 RID: 4593 RVA: 0x00031A23 File Offset: 0x0002FC23
		public virtual int IndexOf(Control value)
		{
			if (this.controls == null || value == null)
			{
				return -1;
			}
			return Array.IndexOf<Control>(this.controls, value);
		}

		/// <summary>Removes the specified server control from the parent server control's <see cref="T:System.Web.UI.ControlCollection" /> object.</summary>
		/// <param name="value">The server control to be removed. </param>
		// Token: 0x060011F2 RID: 4594 RVA: 0x00031A40 File Offset: 0x0002FC40
		public virtual void Remove(Control value)
		{
			int num = this.IndexOf(value);
			if (num == -1)
			{
				return;
			}
			this.RemoveAt(num);
		}

		/// <summary>Removes a child control, at the specified index location, from the <see cref="T:System.Web.UI.ControlCollection" /> object.</summary>
		/// <param name="index">The ordinal index of the server control to be removed from the collection. </param>
		/// <exception cref="T:System.Web.HttpException">Thrown if the <see cref="T:System.Web.UI.ControlCollection" /> is read-only. </exception>
		// Token: 0x060011F3 RID: 4595 RVA: 0x00031A64 File Offset: 0x0002FC64
		public virtual void RemoveAt(int index)
		{
			if (this.readOnly)
			{
				throw new HttpException();
			}
			this.version++;
			Control control = this.controls[index];
			this.count--;
			if (this.count - index > 0)
			{
				Array.Copy(this.controls, index + 1, this.controls, index, this.count - index);
			}
			this.controls[this.count] = null;
			this.owner.RemovedControl(control);
		}

		// Token: 0x060011F4 RID: 4596 RVA: 0x00031AE6 File Offset: 0x0002FCE6
		internal void SetReadonly(bool readOnly)
		{
			this.readOnly = readOnly;
		}

		// Token: 0x04001402 RID: 5122
		private Control owner;

		// Token: 0x04001403 RID: 5123
		private Control[] controls;

		// Token: 0x04001404 RID: 5124
		private int version;

		// Token: 0x04001405 RID: 5125
		private int count;

		// Token: 0x04001406 RID: 5126
		private bool readOnly;

		// Token: 0x020001B9 RID: 441
		private sealed class SimpleEnumerator : IEnumerator
		{
			// Token: 0x060011F5 RID: 4597 RVA: 0x00031AEF File Offset: 0x0002FCEF
			public SimpleEnumerator(ControlCollection coll)
			{
				this.coll = coll;
				this.index = -1;
				this.version = coll.version;
			}

			// Token: 0x060011F6 RID: 4598 RVA: 0x00031B14 File Offset: 0x0002FD14
			public bool MoveNext()
			{
				if (this.version != this.coll.version)
				{
					throw new InvalidOperationException("List has changed.");
				}
				if (this.index >= -1)
				{
					int num = this.index + 1;
					this.index = num;
					if (num < this.coll.Count)
					{
						this.currentElement = this.coll[this.index];
						return true;
					}
				}
				this.index = -2;
				return false;
			}

			// Token: 0x170005CF RID: 1487
			// (get) Token: 0x060011F7 RID: 4599 RVA: 0x00031B88 File Offset: 0x0002FD88
			public object Current
			{
				get
				{
					if (this.index < 0)
					{
						throw new InvalidOperationException((this.index == -1) ? "Enumerator not started" : "Enumerator ended");
					}
					return this.currentElement;
				}
			}

			// Token: 0x060011F8 RID: 4600 RVA: 0x00031BB4 File Offset: 0x0002FDB4
			public void Reset()
			{
				if (this.version != this.coll.version)
				{
					throw new InvalidOperationException("List has changed.");
				}
				this.index = -1;
			}

			// Token: 0x04001407 RID: 5127
			private ControlCollection coll;

			// Token: 0x04001408 RID: 5128
			private int index;

			// Token: 0x04001409 RID: 5129
			private int version;

			// Token: 0x0400140A RID: 5130
			private object currentElement;
		}
	}
}
