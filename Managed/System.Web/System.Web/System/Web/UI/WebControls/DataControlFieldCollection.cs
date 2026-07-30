using System;
using System.Collections;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents a collection of <see cref="T:System.Web.UI.WebControls.DataControlField" /> objects that are used by data-bound controls such as <see cref="T:System.Web.UI.WebControls.GridView" /> and <see cref="T:System.Web.UI.WebControls.DetailsView" />.</summary>
	// Token: 0x02000373 RID: 883
	public sealed class DataControlFieldCollection : StateManagedCollection
	{
		/// <summary>Occurs when the fields in the collection change, usually as the result of a <see cref="M:System.Web.UI.StateManagedCollection.Clear" />, <see cref="M:System.Web.UI.WebControls.DataControlFieldCollection.Insert(System.Int32,System.Web.UI.WebControls.DataControlField)" />, <see cref="M:System.Web.UI.WebControls.DataControlFieldCollection.Remove(System.Web.UI.WebControls.DataControlField)" /> or <see cref="M:System.Web.UI.WebControls.DataControlFieldCollection.Add(System.Web.UI.WebControls.DataControlField)" /> method call. This event is also raised anytime a <see cref="T:System.Web.UI.WebControls.DataControlField" /> in the collection raises its FieldChanged event.</summary>
		// Token: 0x14000067 RID: 103
		// (add) Token: 0x0600215B RID: 8539 RVA: 0x000554B3 File Offset: 0x000536B3
		// (remove) Token: 0x0600215C RID: 8540 RVA: 0x000554C6 File Offset: 0x000536C6
		public event EventHandler FieldsChanged
		{
			add
			{
				this.events.AddHandler(DataControlFieldCollection.fieldsChangedEvent, value);
			}
			remove
			{
				this.events.RemoveHandler(DataControlFieldCollection.fieldsChangedEvent, value);
			}
		}

		/// <summary>Creates a copy of the current collection.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.DataControlFieldCollection" /> that contains a copy of each data control field in the current collection.</returns>
		// Token: 0x0600215D RID: 8541 RVA: 0x000554DC File Offset: 0x000536DC
		public DataControlFieldCollection CloneFields()
		{
			DataControlFieldCollection dataControlFieldCollection = new DataControlFieldCollection();
			foreach (object obj in this)
			{
				DataControlField dataControlField = (DataControlField)obj;
				dataControlFieldCollection.Add(dataControlField.CloneField());
			}
			return dataControlFieldCollection;
		}

		/// <summary>Appends the specified <see cref="T:System.Web.UI.WebControls.DataControlField" /> object to the end of the collection.</summary>
		/// <param name="field">The <see cref="T:System.Web.UI.WebControls.DataControlField" /> to append to the collection. </param>
		// Token: 0x0600215E RID: 8542 RVA: 0x0005553C File Offset: 0x0005373C
		public void Add(DataControlField field)
		{
			((IList)this).Add(field);
		}

		/// <summary>Determines whether the <see cref="T:System.Web.UI.WebControls.DataControlFieldCollection" /> collection contains a specific <see cref="T:System.Web.UI.WebControls.DataControlField" /> object.</summary>
		/// <returns>true if the <see cref="T:System.Web.UI.WebControls.DataControlFieldCollection" /> contains the specified field; otherwise, false.</returns>
		/// <param name="field">The <see cref="T:System.Web.UI.WebControls.DataControlField" /> to locate in the <see cref="T:System.Web.UI.WebControls.DataControlFieldCollection" />. </param>
		// Token: 0x0600215F RID: 8543 RVA: 0x00055546 File Offset: 0x00053746
		public bool Contains(DataControlField field)
		{
			return ((IList)this).Contains(field);
		}

		/// <summary>Copies the entire <see cref="T:System.Web.UI.WebControls.DataControlFieldCollection" /> collection to a compatible one-dimensional <see cref="T:System.Array" />, starting at the specified index of the target array.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Web.UI.WebControls.DataControlFieldCollection" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or-The number of fields in the source <see cref="T:System.Web.UI.WebControls.DataControlFieldCollection" /> collection is greater than the available space from <paramref name="index" /> to the end of the destination array.</exception>
		// Token: 0x06002160 RID: 8544 RVA: 0x0005554F File Offset: 0x0005374F
		public void CopyTo(DataControlField[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		/// <summary>Determines the index of a specific <see cref="T:System.Web.UI.WebControls.DataControlField" /> object in the collection.</summary>
		/// <returns>The index of the <paramref name="field" /> parameter, if it is found in the collection; otherwise, -1.</returns>
		/// <param name="field">The <see cref="T:System.Web.UI.WebControls.DataControlField" /> to locate in the collection.</param>
		// Token: 0x06002161 RID: 8545 RVA: 0x00055559 File Offset: 0x00053759
		public int IndexOf(DataControlField field)
		{
			return ((IList)this).IndexOf(field);
		}

		/// <summary>Inserts the specified <see cref="T:System.Web.UI.WebControls.DataControlField" /> object into the <see cref="T:System.Web.UI.WebControls.DataControlFieldCollection" /> collection at the specified index.</summary>
		/// <param name="index">The zero-based index at which the <see cref="T:System.Web.UI.WebControls.DataControlField" /> is inserted. </param>
		/// <param name="field">The <see cref="T:System.Web.UI.WebControls.DataControlField" /> to insert. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than 0.- or -<paramref name="index" /> is greater than <see cref="P:System.Web.UI.StateManagedCollection.Count" />. </exception>
		// Token: 0x06002162 RID: 8546 RVA: 0x00055562 File Offset: 0x00053762
		public void Insert(int index, DataControlField field)
		{
			((IList)this).Insert(index, field);
		}

		/// <summary>Removes the specified <see cref="T:System.Web.UI.WebControls.DataControlField" /> object from the <see cref="T:System.Web.UI.WebControls.DataControlFieldCollection" /> collection.</summary>
		/// <param name="field">The <see cref="T:System.Web.UI.WebControls.DataControlField" /> to remove from the <see cref="T:System.Web.UI.WebControls.DataControlFieldCollection" />. </param>
		// Token: 0x06002163 RID: 8547 RVA: 0x0005556C File Offset: 0x0005376C
		public void Remove(DataControlField field)
		{
			((IList)this).Remove(field);
		}

		/// <summary>Removes the <see cref="T:System.Web.UI.WebControls.DataControlField" /> object at the specified index from the <see cref="T:System.Web.UI.WebControls.DataControlFieldCollection" /> collection.</summary>
		/// <param name="index">The index of the <see cref="T:System.Web.UI.WebControls.DataControlField" /> to remove. </param>
		// Token: 0x06002164 RID: 8548 RVA: 0x00055575 File Offset: 0x00053775
		public void RemoveAt(int index)
		{
			((IList)this).RemoveAt(index);
		}

		/// <summary>Gets or sets the <see cref="T:System.Web.UI.WebControls.DataControlField" /> object at the specified index in the collection.</summary>
		/// <returns>The <see cref="T:System.Web.UI.WebControls.DataControlField" /> at the specified index in the collection.</returns>
		/// <param name="index">The zero-based index of the <see cref="T:System.Web.UI.WebControls.DataControlField" /> to retrieve from the collection. </param>
		// Token: 0x17000A7B RID: 2683
		[Browsable(false)]
		public DataControlField this[int index]
		{
			get
			{
				return (DataControlField)((IList)this)[index];
			}
		}

		// Token: 0x06002166 RID: 8550 RVA: 0x0005558C File Offset: 0x0005378C
		protected override void OnInsertComplete(int index, object value)
		{
			((DataControlField)value).FieldChanged += this.OnFieldChanged;
			this.OnFieldsChanged();
		}

		// Token: 0x06002167 RID: 8551 RVA: 0x000555AB File Offset: 0x000537AB
		protected override void OnRemoveComplete(int index, object value)
		{
			((DataControlField)value).FieldChanged -= this.OnFieldChanged;
			this.OnFieldsChanged();
		}

		// Token: 0x06002168 RID: 8552 RVA: 0x000555CA File Offset: 0x000537CA
		protected override void OnClearComplete()
		{
			this.OnFieldsChanged();
		}

		// Token: 0x06002169 RID: 8553 RVA: 0x000555CA File Offset: 0x000537CA
		private void OnFieldChanged(object sender, EventArgs args)
		{
			this.OnFieldsChanged();
		}

		// Token: 0x0600216A RID: 8554 RVA: 0x000555D4 File Offset: 0x000537D4
		private void OnFieldsChanged()
		{
			EventHandler eventHandler = this.events[DataControlFieldCollection.fieldsChangedEvent] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, EventArgs.Empty);
			}
		}

		// Token: 0x0600216B RID: 8555 RVA: 0x00055606 File Offset: 0x00053806
		[global::System.MonoTODO("Validate whatever needs to be validated here.")]
		protected override void OnValidate(object o)
		{
			base.OnValidate(o);
		}

		// Token: 0x0600216C RID: 8556 RVA: 0x0005560F File Offset: 0x0005380F
		protected override void SetDirtyObject(object o)
		{
			((DataControlField)o).SetDirty();
		}

		// Token: 0x0600216D RID: 8557 RVA: 0x0005561C File Offset: 0x0005381C
		protected override object CreateKnownType(int index)
		{
			switch (index)
			{
			case 0:
				return new BoundField();
			case 1:
				return new HyperLinkField();
			case 2:
				return new ImageField();
			case 3:
				return new TemplateField();
			case 4:
				return new AutoGeneratedField();
			case 5:
				return new CheckBoxField();
			case 6:
				return new ButtonField();
			case 7:
				return new CommandField();
			default:
				return null;
			}
		}

		// Token: 0x0600216E RID: 8558 RVA: 0x00055682 File Offset: 0x00053882
		protected override Type[] GetKnownTypes()
		{
			return DataControlFieldCollection.fieldTypes;
		}

		// Token: 0x040018CC RID: 6348
		private static readonly object fieldsChangedEvent = new object();

		// Token: 0x040018CD RID: 6349
		private static readonly Type[] fieldTypes = new Type[]
		{
			typeof(BoundField),
			typeof(HyperLinkField),
			typeof(ImageField),
			typeof(TemplateField),
			typeof(AutoGeneratedField),
			typeof(CheckBoxField),
			typeof(ButtonField),
			typeof(CommandField)
		};

		// Token: 0x040018CE RID: 6350
		private EventHandlerList events = new EventHandlerList();
	}
}
