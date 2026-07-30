using System;
using System.Collections;
using System.ComponentModel;

namespace System.Windows.Forms
{
	/// <summary>Represents a collection of <see cref="T:System.Windows.Forms.Binding" /> objects for a control.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000063 RID: 99
	[DefaultEvent("CollectionChanged")]
	public class BindingsCollection : BaseCollection
	{
		// Token: 0x06000478 RID: 1144 RVA: 0x000156BC File Offset: 0x000138BC
		internal BindingsCollection()
		{
		}

		/// <summary>Occurs when the collection has changed.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000047 RID: 71
		// (add) Token: 0x06000479 RID: 1145 RVA: 0x000156C4 File Offset: 0x000138C4
		// (remove) Token: 0x0600047A RID: 1146 RVA: 0x000156E0 File Offset: 0x000138E0
		public event CollectionChangeEventHandler CollectionChanged;

		/// <summary>Occurs when the collection is about to change.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000048 RID: 72
		// (add) Token: 0x0600047B RID: 1147 RVA: 0x000156FC File Offset: 0x000138FC
		// (remove) Token: 0x0600047C RID: 1148 RVA: 0x00015718 File Offset: 0x00013918
		public event CollectionChangeEventHandler CollectionChanging;

		/// <summary>Gets the total number of bindings in the collection.</summary>
		/// <returns>The total number of bindings in the collection.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000101 RID: 257
		// (get) Token: 0x0600047D RID: 1149 RVA: 0x00015734 File Offset: 0x00013934
		public override int Count
		{
			get
			{
				return base.Count;
			}
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.Binding" /> at the specified index.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Binding" /> at the specified index.</returns>
		/// <param name="index">The index of the <see cref="T:System.Windows.Forms.Binding" /> to find. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The collection doesn't contain an item at the specified index. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000102 RID: 258
		public Binding this[int index]
		{
			get
			{
				return (Binding)base.List[index];
			}
		}

		/// <summary>Gets the bindings in the collection as an object.</summary>
		/// <returns>An <see cref="T:System.Collections.ArrayList" /> containing all of the collection members.</returns>
		// Token: 0x17000103 RID: 259
		// (get) Token: 0x0600047F RID: 1151 RVA: 0x00015750 File Offset: 0x00013950
		protected override ArrayList List
		{
			get
			{
				return base.List;
			}
		}

		/// <summary>Adds the specified binding to the collection.</summary>
		/// <param name="binding">The <see cref="T:System.Windows.Forms.Binding" /> to add to the collection. </param>
		// Token: 0x06000480 RID: 1152 RVA: 0x00015758 File Offset: 0x00013958
		protected internal void Add(Binding binding)
		{
			this.AddCore(binding);
		}

		/// <summary>Adds a <see cref="T:System.Windows.Forms.Binding" /> to the collection.</summary>
		/// <param name="dataBinding">The <see cref="T:System.Windows.Forms.Binding" /> to add to the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="dataBinding" /> argument was null. </exception>
		// Token: 0x06000481 RID: 1153 RVA: 0x00015764 File Offset: 0x00013964
		protected virtual void AddCore(Binding dataBinding)
		{
			CollectionChangeEventArgs collectionChangeEventArgs = new CollectionChangeEventArgs(1, dataBinding);
			this.OnCollectionChanging(collectionChangeEventArgs);
			base.List.Add(dataBinding);
			this.OnCollectionChanged(collectionChangeEventArgs);
		}

		/// <summary>Clears the collection of binding objects.</summary>
		// Token: 0x06000482 RID: 1154 RVA: 0x00015794 File Offset: 0x00013994
		protected internal void Clear()
		{
			this.ClearCore();
		}

		/// <summary>Clears the collection of any members.</summary>
		// Token: 0x06000483 RID: 1155 RVA: 0x0001579C File Offset: 0x0001399C
		protected virtual void ClearCore()
		{
			CollectionChangeEventArgs collectionChangeEventArgs = new CollectionChangeEventArgs(3, null);
			this.OnCollectionChanging(collectionChangeEventArgs);
			base.List.Clear();
			this.OnCollectionChanged(collectionChangeEventArgs);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.BindingsCollection.CollectionChanged" /> event.</summary>
		/// <param name="ccevent">A <see cref="T:System.ComponentModel.CollectionChangeEventArgs" /> that contains the event data. </param>
		// Token: 0x06000484 RID: 1156 RVA: 0x000157CC File Offset: 0x000139CC
		protected virtual void OnCollectionChanged(CollectionChangeEventArgs ccevent)
		{
			if (this.CollectionChanged != null)
			{
				this.CollectionChanged.Invoke(this, ccevent);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.BindingsCollection.CollectionChanging" /> event. </summary>
		/// <param name="e">A <see cref="T:System.ComponentModel.CollectionChangeEventArgs" /> that contains event data.</param>
		// Token: 0x06000485 RID: 1157 RVA: 0x000157E8 File Offset: 0x000139E8
		protected virtual void OnCollectionChanging(CollectionChangeEventArgs e)
		{
			if (this.CollectionChanging != null)
			{
				this.CollectionChanging.Invoke(this, e);
			}
		}

		/// <summary>Deletes the specified binding from the collection.</summary>
		/// <param name="binding">The Binding to remove from the collection. </param>
		// Token: 0x06000486 RID: 1158 RVA: 0x00015804 File Offset: 0x00013A04
		protected internal void Remove(Binding binding)
		{
			this.RemoveCore(binding);
		}

		/// <summary>Deletes the binding from the collection at the specified index.</summary>
		/// <param name="index">The index of the <see cref="T:System.Windows.Forms.Binding" /> to remove. </param>
		// Token: 0x06000487 RID: 1159 RVA: 0x00015810 File Offset: 0x00013A10
		protected internal void RemoveAt(int index)
		{
			base.List.RemoveAt(index);
			this.OnCollectionChanged(new CollectionChangeEventArgs(2, base.List));
		}

		/// <summary>Removes the specified <see cref="T:System.Windows.Forms.Binding" /> from the collection.</summary>
		/// <param name="dataBinding">The <see cref="T:System.Windows.Forms.Binding" /> to remove. </param>
		// Token: 0x06000488 RID: 1160 RVA: 0x0001583C File Offset: 0x00013A3C
		protected virtual void RemoveCore(Binding dataBinding)
		{
			CollectionChangeEventArgs collectionChangeEventArgs = new CollectionChangeEventArgs(2, dataBinding);
			this.OnCollectionChanging(collectionChangeEventArgs);
			base.List.Remove(dataBinding);
			this.OnCollectionChanged(collectionChangeEventArgs);
		}

		/// <summary>Gets a value that indicates whether the collection should be serialized.</summary>
		/// <returns>true if the collection count is greater than zero; otherwise, false.</returns>
		// Token: 0x06000489 RID: 1161 RVA: 0x0001586C File Offset: 0x00013A6C
		protected internal bool ShouldSerializeMyAll()
		{
			return this.Count > 0;
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x00015880 File Offset: 0x00013A80
		internal bool Contains(Binding binding)
		{
			return this.List.Contains(binding);
		}
	}
}
