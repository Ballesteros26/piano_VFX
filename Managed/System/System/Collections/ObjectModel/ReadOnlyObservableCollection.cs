using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace System.Collections.ObjectModel
{
	/// <summary>Represents a read-only <see cref="T:System.Collections.ObjectModel.ObservableCollection`1" />.</summary>
	/// <typeparam name="T">The type of elements in the collection.</typeparam>
	// Token: 0x020006E8 RID: 1768
	[TypeForwardedFrom("WindowsBase, Version=3.0.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
	[Serializable]
	public class ReadOnlyObservableCollection<T> : ReadOnlyCollection<T>, INotifyCollectionChanged, INotifyPropertyChanged
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.ObjectModel.ReadOnlyObservableCollection`1" /> class that serves as a wrapper around the specified <see cref="T:System.Collections.ObjectModel.ObservableCollection`1" />.</summary>
		/// <param name="list">The <see cref="T:System.Collections.ObjectModel.ObservableCollection`1" /> with which to create this instance of the <see cref="T:System.Collections.ObjectModel.ReadOnlyObservableCollection`1" /> class.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="list" /> is null.</exception>
		// Token: 0x06003729 RID: 14121 RVA: 0x000CB4F4 File Offset: 0x000C96F4
		public ReadOnlyObservableCollection(ObservableCollection<T> list)
			: base(list)
		{
			((INotifyCollectionChanged)base.Items).CollectionChanged += this.HandleCollectionChanged;
			((INotifyPropertyChanged)base.Items).PropertyChanged += this.HandlePropertyChanged;
		}

		/// <summary>Occurs when the collection changes.</summary>
		// Token: 0x14000066 RID: 102
		// (add) Token: 0x0600372A RID: 14122 RVA: 0x000CB540 File Offset: 0x000C9740
		// (remove) Token: 0x0600372B RID: 14123 RVA: 0x000CB549 File Offset: 0x000C9749
		event NotifyCollectionChangedEventHandler INotifyCollectionChanged.CollectionChanged
		{
			add
			{
				this.CollectionChanged += value;
			}
			remove
			{
				this.CollectionChanged -= value;
			}
		}

		/// <summary>Occurs when an item is added or removed.</summary>
		// Token: 0x14000067 RID: 103
		// (add) Token: 0x0600372C RID: 14124 RVA: 0x000CB554 File Offset: 0x000C9754
		// (remove) Token: 0x0600372D RID: 14125 RVA: 0x000CB58C File Offset: 0x000C978C
		[field: NonSerialized]
		protected virtual event NotifyCollectionChangedEventHandler CollectionChanged;

		/// <summary>Raises the <see cref="E:System.Collections.ObjectModel.ReadOnlyObservableCollection`1.CollectionChanged" /> event using the provided arguments.</summary>
		/// <param name="args">Arguments of the event being raised.</param>
		// Token: 0x0600372E RID: 14126 RVA: 0x000CB5C1 File Offset: 0x000C97C1
		protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs args)
		{
			if (this.CollectionChanged != null)
			{
				this.CollectionChanged(this, args);
			}
		}

		/// <summary>Occurs when a property value changes.</summary>
		// Token: 0x14000068 RID: 104
		// (add) Token: 0x0600372F RID: 14127 RVA: 0x000CB5D8 File Offset: 0x000C97D8
		// (remove) Token: 0x06003730 RID: 14128 RVA: 0x000CB5E1 File Offset: 0x000C97E1
		event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
		{
			add
			{
				this.PropertyChanged += value;
			}
			remove
			{
				this.PropertyChanged -= value;
			}
		}

		/// <summary>Occurs when a property value changes.</summary>
		// Token: 0x14000069 RID: 105
		// (add) Token: 0x06003731 RID: 14129 RVA: 0x000CB5EC File Offset: 0x000C97EC
		// (remove) Token: 0x06003732 RID: 14130 RVA: 0x000CB624 File Offset: 0x000C9824
		[field: NonSerialized]
		protected virtual event PropertyChangedEventHandler PropertyChanged;

		/// <summary>Raises the <see cref="E:System.Collections.ObjectModel.ReadOnlyObservableCollection`1.PropertyChanged" /> event using the provided arguments.</summary>
		/// <param name="args">Arguments of the event being raised.</param>
		// Token: 0x06003733 RID: 14131 RVA: 0x000CB659 File Offset: 0x000C9859
		protected virtual void OnPropertyChanged(PropertyChangedEventArgs args)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, args);
			}
		}

		// Token: 0x06003734 RID: 14132 RVA: 0x000CB670 File Offset: 0x000C9870
		private void HandleCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			this.OnCollectionChanged(e);
		}

		// Token: 0x06003735 RID: 14133 RVA: 0x000CB679 File Offset: 0x000C9879
		private void HandlePropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			this.OnPropertyChanged(e);
		}
	}
}
