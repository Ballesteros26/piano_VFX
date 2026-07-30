using System;
using System.Collections;
using System.ComponentModel;

namespace System.Data.Common
{
	/// <summary>The base class for a collection of parameters relevant to a <see cref="T:System.Data.Common.DbCommand" />. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200034F RID: 847
	public abstract class DbParameterCollection : MarshalByRefObject, IDataParameterCollection, IList, ICollection, IEnumerable
	{
		/// <summary>Specifies the number of items in the collection.</summary>
		/// <returns>The number of items in the collection.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006FB RID: 1787
		// (get) Token: 0x06002827 RID: 10279
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public abstract int Count { get; }

		/// <summary>Specifies whether the collection is a fixed size.</summary>
		/// <returns>true if the collection is a fixed size; otherwise false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006FC RID: 1788
		// (get) Token: 0x06002828 RID: 10280 RVA: 0x000061D5 File Offset: 0x000043D5
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public virtual bool IsFixedSize
		{
			get
			{
				return false;
			}
		}

		/// <summary>Specifies whether the collection is read-only.</summary>
		/// <returns>true if the collection is read-only; otherwise false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x06002829 RID: 10281 RVA: 0x000061D5 File Offset: 0x000043D5
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Specifies whether the collection is synchronized.</summary>
		/// <returns>true if the collection is synchronized; otherwise false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170006FE RID: 1790
		// (get) Token: 0x0600282A RID: 10282 RVA: 0x000061D5 File Offset: 0x000043D5
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Specifies the <see cref="T:System.Object" /> to be used to synchronize access to the collection.</summary>
		/// <returns>A <see cref="T:System.Object" /> to be used to synchronize access to the <see cref="T:System.Data.Common.DbParameterCollection" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170006FF RID: 1791
		// (get) Token: 0x0600282B RID: 10283
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public abstract object SyncRoot { get; }

		/// <summary>Gets or sets the element at the specified index.</summary>
		/// <returns>The element at the specified index.</returns>
		/// <param name="index">The zero-based index of the element to get or set.</param>
		// Token: 0x17000700 RID: 1792
		object IList.this[int index]
		{
			get
			{
				return this.GetParameter(index);
			}
			set
			{
				this.SetParameter(index, (DbParameter)value);
			}
		}

		/// <summary>Gets or sets the parameter at the specified index.</summary>
		/// <returns>An <see cref="T:System.Object" /> at the specified index.</returns>
		/// <param name="parameterName">The name of the parameter to retrieve.</param>
		// Token: 0x17000701 RID: 1793
		object IDataParameterCollection.this[string parameterName]
		{
			get
			{
				return this.GetParameter(parameterName);
			}
			set
			{
				this.SetParameter(parameterName, (DbParameter)value);
			}
		}

		/// <summary>Gets and sets the <see cref="T:System.Data.Common.DbParameter" /> at the specified index.</summary>
		/// <returns>The <see cref="T:System.Data.Common.DbParameter" /> at the specified index.</returns>
		/// <param name="index">The zero-based index of the parameter.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">The specified index does not exist. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000702 RID: 1794
		public DbParameter this[int index]
		{
			get
			{
				return this.GetParameter(index);
			}
			set
			{
				this.SetParameter(index, value);
			}
		}

		/// <summary>Gets and sets the <see cref="T:System.Data.Common.DbParameter" /> with the specified name.</summary>
		/// <returns>The <see cref="T:System.Data.Common.DbParameter" /> with the specified name.</returns>
		/// <param name="parameterName">The name of the parameter.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">The specified index does not exist. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000703 RID: 1795
		public DbParameter this[string parameterName]
		{
			get
			{
				return this.GetParameter(parameterName);
			}
			set
			{
				this.SetParameter(parameterName, value);
			}
		}

		/// <summary>Adds the specified <see cref="T:System.Data.Common.DbParameter" /> object to the <see cref="T:System.Data.Common.DbParameterCollection" />.</summary>
		/// <returns>The index of the <see cref="T:System.Data.Common.DbParameter" /> object in the collection.</returns>
		/// <param name="value">The <see cref="P:System.Data.Common.DbParameter.Value" /> of the <see cref="T:System.Data.Common.DbParameter" /> to add to the collection.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002834 RID: 10292
		public abstract int Add(object value);

		/// <summary>Adds an array of items with the specified values to the <see cref="T:System.Data.Common.DbParameterCollection" />.</summary>
		/// <param name="values">An array of values of type <see cref="T:System.Data.Common.DbParameter" /> to add to the collection.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002835 RID: 10293
		public abstract void AddRange(Array values);

		/// <summary>Indicates whether a <see cref="T:System.Data.Common.DbParameter" /> with the specified <see cref="P:System.Data.Common.DbParameter.Value" /> is contained in the collection.</summary>
		/// <returns>true if the <see cref="T:System.Data.Common.DbParameter" /> is in the collection; otherwise false.</returns>
		/// <param name="value">The <see cref="P:System.Data.Common.DbParameter.Value" /> of the <see cref="T:System.Data.Common.DbParameter" /> to look for in the collection.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002836 RID: 10294
		public abstract bool Contains(object value);

		/// <summary>Indicates whether a <see cref="T:System.Data.Common.DbParameter" /> with the specified name exists in the collection.</summary>
		/// <returns>true if the <see cref="T:System.Data.Common.DbParameter" /> is in the collection; otherwise false.</returns>
		/// <param name="value">The name of the <see cref="T:System.Data.Common.DbParameter" /> to look for in the collection.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002837 RID: 10295
		public abstract bool Contains(string value);

		/// <summary>Copies an array of items to the collection starting at the specified index.</summary>
		/// <param name="array">The array of items to copy to the collection.</param>
		/// <param name="index">The index in the collection to copy the items.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002838 RID: 10296
		public abstract void CopyTo(Array array, int index);

		/// <summary>Removes all <see cref="T:System.Data.Common.DbParameter" /> values from the <see cref="T:System.Data.Common.DbParameterCollection" />.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002839 RID: 10297
		public abstract void Clear();

		/// <summary>Exposes the <see cref="M:System.Collections.IEnumerable.GetEnumerator" /> method, which supports a simple iteration over a collection by a .NET Framework data provider.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600283A RID: 10298
		[EditorBrowsable(EditorBrowsableState.Never)]
		public abstract IEnumerator GetEnumerator();

		/// <summary>Returns the <see cref="T:System.Data.Common.DbParameter" /> object at the specified index in the collection.</summary>
		/// <returns>The <see cref="T:System.Data.Common.DbParameter" /> object at the specified index in the collection.</returns>
		/// <param name="index">The index of the <see cref="T:System.Data.Common.DbParameter" /> in the collection.</param>
		// Token: 0x0600283B RID: 10299
		protected abstract DbParameter GetParameter(int index);

		/// <summary>Returns <see cref="T:System.Data.Common.DbParameter" /> the object with the specified name.</summary>
		/// <returns>The <see cref="T:System.Data.Common.DbParameter" /> the object with the specified name.</returns>
		/// <param name="parameterName">The name of the <see cref="T:System.Data.Common.DbParameter" /> in the collection.</param>
		// Token: 0x0600283C RID: 10300
		protected abstract DbParameter GetParameter(string parameterName);

		/// <summary>Returns the index of the specified <see cref="T:System.Data.Common.DbParameter" /> object.</summary>
		/// <returns>The index of the specified <see cref="T:System.Data.Common.DbParameter" /> object.</returns>
		/// <param name="value">The <see cref="T:System.Data.Common.DbParameter" /> object in the collection.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600283D RID: 10301
		public abstract int IndexOf(object value);

		/// <summary>Returns the index of the <see cref="T:System.Data.Common.DbParameter" /> object with the specified name.</summary>
		/// <returns>The index of the <see cref="T:System.Data.Common.DbParameter" /> object with the specified name.</returns>
		/// <param name="parameterName">The name of the <see cref="T:System.Data.Common.DbParameter" /> object in the collection.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600283E RID: 10302
		public abstract int IndexOf(string parameterName);

		/// <summary>Inserts the specified index of the <see cref="T:System.Data.Common.DbParameter" /> object with the specified name into the collection at the specified index.</summary>
		/// <param name="index">The index at which to insert the <see cref="T:System.Data.Common.DbParameter" /> object.</param>
		/// <param name="value">The <see cref="T:System.Data.Common.DbParameter" /> object to insert into the collection.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600283F RID: 10303
		public abstract void Insert(int index, object value);

		/// <summary>Removes the specified <see cref="T:System.Data.Common.DbParameter" /> object from the collection.</summary>
		/// <param name="value">The <see cref="T:System.Data.Common.DbParameter" /> object to remove.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002840 RID: 10304
		public abstract void Remove(object value);

		/// <summary>Removes the <see cref="T:System.Data.Common.DbParameter" /> object at the specified from the collection.</summary>
		/// <param name="index">The index where the <see cref="T:System.Data.Common.DbParameter" /> object is located.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002841 RID: 10305
		public abstract void RemoveAt(int index);

		/// <summary>Removes the <see cref="T:System.Data.Common.DbParameter" /> object with the specified name from the collection.</summary>
		/// <param name="parameterName">The name of the <see cref="T:System.Data.Common.DbParameter" /> object to remove.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002842 RID: 10306
		public abstract void RemoveAt(string parameterName);

		/// <summary>Sets the <see cref="T:System.Data.Common.DbParameter" /> object at the specified index to a new value. </summary>
		/// <param name="index">The index where the <see cref="T:System.Data.Common.DbParameter" /> object is located.</param>
		/// <param name="value">The new <see cref="T:System.Data.Common.DbParameter" /> value.</param>
		// Token: 0x06002843 RID: 10307
		protected abstract void SetParameter(int index, DbParameter value);

		/// <summary>Sets the <see cref="T:System.Data.Common.DbParameter" /> object with the specified name to a new value.</summary>
		/// <param name="parameterName">The name of the <see cref="T:System.Data.Common.DbParameter" /> object in the collection.</param>
		/// <param name="value">The new <see cref="T:System.Data.Common.DbParameter" /> value.</param>
		// Token: 0x06002844 RID: 10308
		protected abstract void SetParameter(string parameterName, DbParameter value);
	}
}
