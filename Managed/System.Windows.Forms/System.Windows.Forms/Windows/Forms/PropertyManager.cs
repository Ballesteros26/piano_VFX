using System;
using System.Collections;
using System.ComponentModel;

namespace System.Windows.Forms
{
	/// <summary>Maintains a <see cref="T:System.Windows.Forms.Binding" /> between an object's property and a data-bound control property.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020002A9 RID: 681
	public class PropertyManager : BindingManagerBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.PropertyManager" /> class.</summary>
		// Token: 0x06002DB1 RID: 11697 RVA: 0x000B0ED8 File Offset: 0x000AF0D8
		public PropertyManager()
		{
		}

		// Token: 0x06002DB2 RID: 11698 RVA: 0x000B0EE0 File Offset: 0x000AF0E0
		internal PropertyManager(object data_source)
		{
			this.SetDataSource(data_source);
		}

		// Token: 0x06002DB3 RID: 11699 RVA: 0x000B0EF0 File Offset: 0x000AF0F0
		internal PropertyManager(object data_source, string property_name)
		{
			this.property_name = property_name;
			this.SetDataSource(data_source);
		}

		// Token: 0x06002DB4 RID: 11700 RVA: 0x000B0F08 File Offset: 0x000AF108
		internal void SetDataSource(object new_data_source)
		{
			if (this.changed_event != null)
			{
				this.changed_event.RemoveEventHandler(this.data_source, this.property_value_changed_handler);
			}
			this.data_source = new_data_source;
			if (this.property_name != null)
			{
				this.prop_desc = TypeDescriptor.GetProperties(this.data_source).Find(this.property_name, true);
				if (this.prop_desc == null)
				{
					return;
				}
				this.changed_event = TypeDescriptor.GetEvents(this.data_source).Find(this.property_name + "Changed", false);
				if (this.changed_event != null)
				{
					this.property_value_changed_handler = new EventHandler(this.PropertyValueChanged);
					this.changed_event.AddEventHandler(this.data_source, this.property_value_changed_handler);
				}
			}
		}

		// Token: 0x06002DB5 RID: 11701 RVA: 0x000B0FD0 File Offset: 0x000AF1D0
		private void PropertyValueChanged(object sender, EventArgs args)
		{
			this.OnCurrentChanged(args);
		}

		/// <summary>Gets the object to which the data-bound property belongs.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the object to which the property belongs.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000B93 RID: 2963
		// (get) Token: 0x06002DB6 RID: 11702 RVA: 0x000B0FDC File Offset: 0x000AF1DC
		public override object Current
		{
			get
			{
				return (this.prop_desc != null) ? this.prop_desc.GetValue(this.data_source) : this.data_source;
			}
		}

		/// <returns>A zero-based index that specifies a position in the underlying list.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000B94 RID: 2964
		// (get) Token: 0x06002DB7 RID: 11703 RVA: 0x000B1008 File Offset: 0x000AF208
		// (set) Token: 0x06002DB8 RID: 11704 RVA: 0x000B100C File Offset: 0x000AF20C
		public override int Position
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		/// <returns>The number of rows managed by the <see cref="T:System.Windows.Forms.BindingManagerBase" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000B95 RID: 2965
		// (get) Token: 0x06002DB9 RID: 11705 RVA: 0x000B1010 File Offset: 0x000AF210
		public override int Count
		{
			get
			{
				return 1;
			}
		}

		/// <filterpriority>1</filterpriority>
		// Token: 0x06002DBA RID: 11706 RVA: 0x000B1014 File Offset: 0x000AF214
		public override void AddNew()
		{
			throw new NotSupportedException("AddNew is not supported for property to property binding");
		}

		/// <filterpriority>1</filterpriority>
		// Token: 0x06002DBB RID: 11707 RVA: 0x000B1020 File Offset: 0x000AF220
		public override void CancelCurrentEdit()
		{
			IEditableObject editableObject = this.data_source as IEditableObject;
			if (editableObject == null)
			{
				return;
			}
			editableObject.CancelEdit();
			base.PushData();
		}

		/// <filterpriority>1</filterpriority>
		// Token: 0x06002DBC RID: 11708 RVA: 0x000B104C File Offset: 0x000AF24C
		public override void EndCurrentEdit()
		{
			base.PullData();
			IEditableObject editableObject = this.data_source as IEditableObject;
			if (editableObject == null)
			{
				return;
			}
			editableObject.EndEdit();
		}

		// Token: 0x06002DBD RID: 11709 RVA: 0x000B1078 File Offset: 0x000AF278
		internal override PropertyDescriptorCollection GetItemPropertiesInternal()
		{
			return TypeDescriptor.GetProperties(this.data_source);
		}

		/// <param name="index">The index of the row to delete. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002DBE RID: 11710 RVA: 0x000B1088 File Offset: 0x000AF288
		public override void RemoveAt(int index)
		{
			throw new NotSupportedException("RemoveAt is not supported for property to property binding");
		}

		/// <filterpriority>1</filterpriority>
		// Token: 0x06002DBF RID: 11711 RVA: 0x000B1094 File Offset: 0x000AF294
		public override void ResumeBinding()
		{
		}

		/// <summary>Suspends the data binding between a data source and a data-bound property.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002DC0 RID: 11712 RVA: 0x000B1098 File Offset: 0x000AF298
		public override void SuspendBinding()
		{
		}

		// Token: 0x17000B96 RID: 2966
		// (get) Token: 0x06002DC1 RID: 11713 RVA: 0x000B109C File Offset: 0x000AF29C
		internal override bool IsSuspended
		{
			get
			{
				return this.data_source == null;
			}
		}

		/// <returns>The name of the list supplying the data for the binding.</returns>
		/// <param name="listAccessors">An <see cref="T:System.Collections.ArrayList" /> containing the table's bound properties. </param>
		// Token: 0x06002DC2 RID: 11714 RVA: 0x000B10A8 File Offset: 0x000AF2A8
		protected internal override string GetListName(ArrayList listAccessors)
		{
			return string.Empty;
		}

		/// <summary>Updates the current <see cref="T:System.Windows.Forms.Binding" /> between a data binding and a data-bound property.</summary>
		// Token: 0x06002DC3 RID: 11715 RVA: 0x000B10B0 File Offset: 0x000AF2B0
		[MonoTODO("Stub, does nothing")]
		protected override void UpdateIsBinding()
		{
		}

		/// <param name="ea"></param>
		// Token: 0x06002DC4 RID: 11716 RVA: 0x000B10B4 File Offset: 0x000AF2B4
		protected internal override void OnCurrentChanged(EventArgs ea)
		{
			base.PushData();
			if (this.onCurrentChangedHandler != null)
			{
				this.onCurrentChangedHandler.Invoke(this, ea);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.BindingManagerBase.CurrentItemChanged" /> event.</summary>
		/// <param name="ea">An <see cref="T:System.EventArgs" /> containing the event data.</param>
		// Token: 0x06002DC5 RID: 11717 RVA: 0x000B10D4 File Offset: 0x000AF2D4
		protected override void OnCurrentItemChanged(EventArgs ea)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04001600 RID: 5632
		internal string property_name;

		// Token: 0x04001601 RID: 5633
		private PropertyDescriptor prop_desc;

		// Token: 0x04001602 RID: 5634
		private object data_source;

		// Token: 0x04001603 RID: 5635
		private EventDescriptor changed_event;

		// Token: 0x04001604 RID: 5636
		private EventHandler property_value_changed_handler;
	}
}
