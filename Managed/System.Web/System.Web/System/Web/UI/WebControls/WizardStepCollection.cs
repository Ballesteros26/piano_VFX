using System;
using System.Collections;
using Unity;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents a collection of <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived objects in a control that acts as a wizard. This class cannot be inherited.</summary>
	// Token: 0x0200044F RID: 1103
	public sealed class WizardStepCollection : IList, ICollection, IEnumerable
	{
		// Token: 0x06003315 RID: 13077 RVA: 0x00089518 File Offset: 0x00087718
		internal WizardStepCollection(Wizard wizard)
		{
			this.list = new ArrayList();
			base..ctor();
			this.wizard = wizard;
		}

		/// <summary>Gets the number of <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived objects in the <see cref="T:System.Web.UI.WebControls.Wizard" /> control's <see cref="T:System.Web.UI.WebControls.WizardStepCollection" /> collection.</summary>
		/// <returns>The number of <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived objects in the <see cref="T:System.Web.UI.WebControls.Wizard" /> control.</returns>
		// Token: 0x17001025 RID: 4133
		// (get) Token: 0x06003316 RID: 13078 RVA: 0x00089532 File Offset: 0x00087732
		public int Count
		{
			get
			{
				return this.list.Count;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived objects in the collection can be modified.</summary>
		/// <returns>true if the <see cref="T:System.Web.UI.WebControls.WizardStepCollection" /> collection can be modified; otherwise, false. </returns>
		// Token: 0x17001026 RID: 4134
		// (get) Token: 0x06003317 RID: 13079 RVA: 0x00008A69 File Offset: 0x00006C69
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether access to the collection is synchronized (thread-safe).</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x17001027 RID: 4135
		// (get) Token: 0x06003318 RID: 13080 RVA: 0x00008A69 File Offset: 0x00006C69
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived object from the collection at the specified index.</summary>
		/// <returns>The <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived object in the <see cref="T:System.Web.UI.WebControls.WizardStepCollection" /> collection at the specified index location.</returns>
		/// <param name="index">The index of the <see cref="T:System.Web.UI.WebControls.WizardStep" /> object to retrieve.</param>
		// Token: 0x17001028 RID: 4136
		public WizardStepBase this[int index]
		{
			get
			{
				return (WizardStepBase)this.list[index];
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Web.UI.WebControls.WizardStepCollection" /> collection.</returns>
		// Token: 0x17001029 RID: 4137
		// (get) Token: 0x0600331A RID: 13082 RVA: 0x00002058 File Offset: 0x00000258
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Appends the specified <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived object to the end of the collection.</summary>
		/// <param name="wizardStep">The <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived object to append to the <see cref="T:System.Web.UI.WebControls.WizardStepCollection" /> collection.</param>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived object passed in is null.</exception>
		// Token: 0x0600331B RID: 13083 RVA: 0x00089552 File Offset: 0x00087752
		public void Add(WizardStepBase wizardStep)
		{
			if (wizardStep == null)
			{
				throw new ArgumentNullException("wizardStep");
			}
			wizardStep.SetWizard(this.wizard);
			this.list.Add(wizardStep);
			this.wizard.UpdateViews();
		}

		/// <summary>Adds the specified <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived object to the collection at the specified index location.</summary>
		/// <param name="index">The index location at which to insert <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived object.</param>
		/// <param name="wizardStep">The <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived object to append to the <see cref="T:System.Web.UI.WebControls.WizardStepCollection" /> collection.</param>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived object passed in is null.</exception>
		// Token: 0x0600331C RID: 13084 RVA: 0x00089586 File Offset: 0x00087786
		public void AddAt(int index, WizardStepBase wizardStep)
		{
			if (wizardStep == null)
			{
				throw new ArgumentNullException("wizardStep");
			}
			wizardStep.SetWizard(this.wizard);
			this.list.Insert(index, wizardStep);
			this.wizard.UpdateViews();
		}

		/// <summary>Removes all <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived objects from the collection.</summary>
		// Token: 0x0600331D RID: 13085 RVA: 0x000895BA File Offset: 0x000877BA
		public void Clear()
		{
			this.list.Clear();
			this.wizard.UpdateViews();
		}

		/// <summary>Determines whether the <see cref="T:System.Web.UI.WebControls.WizardStepCollection" /> collection contains a specific <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived object.</summary>
		/// <returns>true if the <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived object is found in the <see cref="T:System.Web.UI.WebControls.WizardStepCollection" /> collection; otherwise, false.</returns>
		/// <param name="wizardStep">The <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived object to find in the <see cref="T:System.Web.UI.WebControls.WizardStepCollection" /> collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="wizardStep" /> is null.</exception>
		// Token: 0x0600331E RID: 13086 RVA: 0x000895D2 File Offset: 0x000877D2
		public bool Contains(WizardStepBase wizardStep)
		{
			if (wizardStep == null)
			{
				throw new ArgumentNullException("wizardStep");
			}
			return this.list.Contains(wizardStep);
		}

		/// <summary>Copies all the items from a <see cref="T:System.Web.UI.WebControls.WizardStepCollection" /> collection to a compatible one-dimensional array of <see cref="T:System.Web.UI.WebControls.WizardStepBase" /> objects, starting at the specified index in the target array.</summary>
		/// <param name="array">A zero-based array of <see cref="T:System.Web.UI.WebControls.WizardStepBase" /> objects that receives the items copied from the collection.</param>
		/// <param name="index">The position in the target array at which the array starts receiving the copied items.</param>
		// Token: 0x0600331F RID: 13087 RVA: 0x000895EE File Offset: 0x000877EE
		public void CopyTo(WizardStepBase[] array, int index)
		{
			this.list.CopyTo(array, index);
		}

		/// <summary>Returns an <see cref="T:System.Collections.IEnumerator" />-implemented object that can be used to iterate through the <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived objects in the collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" />-implemented object that contains all the <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived objects in the <see cref="T:System.Web.UI.WebControls.WizardStepCollection" /> collection.</returns>
		// Token: 0x06003320 RID: 13088 RVA: 0x000895FD File Offset: 0x000877FD
		public IEnumerator GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		/// <summary>Determines the index value that represents the position of the specified <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived object in the collection.</summary>
		/// <returns>If found, the zero-based index of the first occurrence of the <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived object passed in within the current <see cref="T:System.Web.UI.WebControls.WizardStepCollection" /> collection; otherwise, -1.</returns>
		/// <param name="wizardStep">The <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived object to search for in the <see cref="T:System.Web.UI.WebControls.WizardStepCollection" /> collection.</param>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived object passed in is null.</exception>
		// Token: 0x06003321 RID: 13089 RVA: 0x0008960A File Offset: 0x0008780A
		public int IndexOf(WizardStepBase wizardStep)
		{
			if (wizardStep == null)
			{
				throw new ArgumentNullException("wizardStep");
			}
			return this.list.IndexOf(wizardStep);
		}

		/// <summary>Inserts the specified <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived object into the collection at the specified index location.</summary>
		/// <param name="index">The index location at which to insert the <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived object.</param>
		/// <param name="wizardStep">The <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived object to insert into the <see cref="T:System.Web.UI.WebControls.WizardStepCollection" /> collection.</param>
		// Token: 0x06003322 RID: 13090 RVA: 0x00089626 File Offset: 0x00087826
		public void Insert(int index, WizardStepBase wizardStep)
		{
			this.AddAt(index, wizardStep);
		}

		/// <summary>Removes the specified <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived object from the collection.</summary>
		/// <param name="wizardStep">The <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived object to remove from the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived object passed in is null.</exception>
		// Token: 0x06003323 RID: 13091 RVA: 0x00089630 File Offset: 0x00087830
		public void Remove(WizardStepBase wizardStep)
		{
			if (wizardStep == null)
			{
				throw new ArgumentNullException("wizardStep");
			}
			this.list.Remove(wizardStep);
			this.wizard.UpdateViews();
		}

		/// <summary>Removes the <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived object from the collection at the specified location.</summary>
		/// <param name="index">The index of the <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived object to remove.</param>
		// Token: 0x06003324 RID: 13092 RVA: 0x00089657 File Offset: 0x00087857
		public void RemoveAt(int index)
		{
			this.list.RemoveAt(index);
			this.wizard.UpdateViews();
		}

		/// <summary>Gets a value indicating whether the collection has a fixed size.</summary>
		/// <returns>true if the collection has a fixed size; otherwise, false.</returns>
		// Token: 0x1700102A RID: 4138
		// (get) Token: 0x06003325 RID: 13093 RVA: 0x00008A69 File Offset: 0x00006C69
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets the object at the specified index in the collection.</summary>
		/// <returns>The object to be retrieved.</returns>
		/// <param name="index">The index of the object to get from the collection.</param>
		// Token: 0x1700102B RID: 4139
		object IList.this[int index]
		{
			get
			{
				return this.list[index];
			}
			set
			{
				this.list[index] = value;
			}
		}

		/// <summary>Appends the specified object to the end of the collection.</summary>
		/// <returns>The position into which the new element was inserted.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to append to the end of the collection.</param>
		// Token: 0x06003328 RID: 13096 RVA: 0x0008968D File Offset: 0x0008788D
		int IList.Add(object ob)
		{
			int num = this.list.Add((WizardStepBase)ob);
			this.wizard.UpdateViews();
			return num;
		}

		/// <summary>Determines whether the collection contains the specified object.</summary>
		/// <returns>true if the <see cref="T:System.Object" /> is found in the <see cref="T:System.Collections.IList" />; otherwise false.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to locate in the collection.</param>
		// Token: 0x06003329 RID: 13097 RVA: 0x000896AB File Offset: 0x000878AB
		bool IList.Contains(object ob)
		{
			return this.Contains((WizardStepBase)ob);
		}

		/// <summary>Determines the index value that represents the position of the specified object in the collection.</summary>
		/// <returns>The index value of the specified object in the collection.</returns>
		/// <param name="value">The object to search for in the collection.</param>
		// Token: 0x0600332A RID: 13098 RVA: 0x000896B9 File Offset: 0x000878B9
		int IList.IndexOf(object ob)
		{
			return this.IndexOf((WizardStepBase)ob);
		}

		/// <summary>Inserts the specified object in the collection at the specified position.</summary>
		/// <param name="index">The index at which to insert the object into the collection.</param>
		/// <param name="value">The object to insert into the collection.</param>
		// Token: 0x0600332B RID: 13099 RVA: 0x000896C7 File Offset: 0x000878C7
		void IList.Insert(int index, object ob)
		{
			this.AddAt(index, (WizardStepBase)ob);
		}

		/// <summary>Removes the specified object from the collection.</summary>
		/// <param name="value">The object to remove from the collection.</param>
		// Token: 0x0600332C RID: 13100 RVA: 0x000896D6 File Offset: 0x000878D6
		void IList.Remove(object ob)
		{
			this.Remove((WizardStepBase)ob);
		}

		/// <summary>Copies all the items from a <see cref="T:System.Web.UI.WebControls.WizardStepCollection" /> collection to a one-dimensional array, starting at the specified index in the target array.</summary>
		/// <param name="array">A zero-based <see cref="T:System.Array" /> that receives the items copied from the <see cref="T:System.Web.UI.WebControls.WizardStepCollection" /> collection.</param>
		/// <param name="index">The position in the target array at which to start receiving the copied content.</param>
		// Token: 0x0600332D RID: 13101 RVA: 0x000895EE File Offset: 0x000877EE
		void ICollection.CopyTo(Array array, int index)
		{
			this.list.CopyTo(array, index);
		}

		// Token: 0x0600332E RID: 13102 RVA: 0x0000B3E4 File Offset: 0x000095E4
		internal WizardStepCollection()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001CC0 RID: 7360
		private ArrayList list;

		// Token: 0x04001CC1 RID: 7361
		private Wizard wizard;
	}
}
