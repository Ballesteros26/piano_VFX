using System;
using System.ComponentModel;
using Unity;

namespace System.Data
{
	/// <summary>Represents a customized view of a <see cref="T:System.Data.DataRow" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000086 RID: 134
	public class DataRowView : ICustomTypeDescriptor, IEditableObject, IDataErrorInfo, INotifyPropertyChanged
	{
		// Token: 0x0600066C RID: 1644 RVA: 0x00019BEB File Offset: 0x00017DEB
		internal DataRowView(DataView dataView, DataRow row)
		{
			this._dataView = dataView;
			this._row = row;
		}

		/// <summary>Gets a value indicating whether the current <see cref="T:System.Data.DataRowView" /> is identical to the specified object.</summary>
		/// <returns>true if <paramref name="object" /> is a <see cref="T:System.Data.DataRowView" /> and it returns the same row as the current <see cref="T:System.Data.DataRowView" />; otherwise false.</returns>
		/// <param name="other">An <see cref="T:System.Object" /> to be compared. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600066D RID: 1645 RVA: 0x00019C01 File Offset: 0x00017E01
		public override bool Equals(object other)
		{
			return this == other;
		}

		/// <summary>Returns the hash code of the <see cref="T:System.Data.DataRow" /> object.</summary>
		/// <returns>A 32-bit signed integer hash code 1, which represents Boolean true if the value of this instance is nonzero; otherwise the integer zero, which represents Boolean false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600066E RID: 1646 RVA: 0x00019C07 File Offset: 0x00017E07
		public override int GetHashCode()
		{
			return this.Row.GetHashCode();
		}

		/// <summary>Gets the <see cref="T:System.Data.DataView" /> to which this row belongs.</summary>
		/// <returns>The DataView to which this row belongs.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000133 RID: 307
		// (get) Token: 0x0600066F RID: 1647 RVA: 0x00019C14 File Offset: 0x00017E14
		public DataView DataView
		{
			get
			{
				return this._dataView;
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000670 RID: 1648 RVA: 0x00019C1C File Offset: 0x00017E1C
		internal int ObjectID
		{
			get
			{
				return this._row._objectID;
			}
		}

		/// <summary>Gets or sets a value in a specified column.</summary>
		/// <returns>The value of the column.</returns>
		/// <param name="ndx">The specified column. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000135 RID: 309
		public object this[int ndx]
		{
			get
			{
				return this.Row[ndx, this.RowVersionDefault];
			}
			set
			{
				if (!this._dataView.AllowEdit && !this.IsNew)
				{
					throw ExceptionBuilder.CanNotEdit();
				}
				this.SetColumnValue(this._dataView.Table.Columns[ndx], value);
			}
		}

		/// <summary>Gets or sets a value in a specified column.</summary>
		/// <returns>The value of the column.</returns>
		/// <param name="property">String that contains the specified column. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000136 RID: 310
		public object this[string property]
		{
			get
			{
				DataColumn dataColumn = this._dataView.Table.Columns[property];
				if (dataColumn != null)
				{
					return this.Row[dataColumn, this.RowVersionDefault];
				}
				if (this._dataView.Table.DataSet != null && this._dataView.Table.DataSet.Relations.Contains(property))
				{
					return this.CreateChildView(property);
				}
				throw ExceptionBuilder.PropertyNotFound(property, this._dataView.Table.TableName);
			}
			set
			{
				DataColumn dataColumn = this._dataView.Table.Columns[property];
				if (dataColumn == null)
				{
					throw ExceptionBuilder.SetFailed(property);
				}
				if (!this._dataView.AllowEdit && !this.IsNew)
				{
					throw ExceptionBuilder.CanNotEdit();
				}
				this.SetColumnValue(dataColumn, value);
			}
		}

		/// <summary>Gets the error message for the property with the given name.</summary>
		/// <returns>The error message for the property. The default is an empty string ("").</returns>
		/// <param name="colName">The name of the property whose error message to get. </param>
		// Token: 0x17000137 RID: 311
		string IDataErrorInfo.this[string colName]
		{
			get
			{
				return this.Row.GetColumnError(colName);
			}
		}

		/// <summary>Gets a message that describes any validation errors for the object.</summary>
		/// <returns>The validation error on the object.</returns>
		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000676 RID: 1654 RVA: 0x00019D5F File Offset: 0x00017F5F
		string IDataErrorInfo.Error
		{
			get
			{
				return this.Row.RowError;
			}
		}

		/// <summary>Gets the current version description of the <see cref="T:System.Data.DataRow" />.</summary>
		/// <returns>One of the <see cref="T:System.Data.DataRowVersion" /> values. Possible values for the <see cref="P:System.Data.DataRowView.RowVersion" /> property are Default, Original, Current, and Proposed.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000677 RID: 1655 RVA: 0x00019D6C File Offset: 0x00017F6C
		public DataRowVersion RowVersion
		{
			get
			{
				return this.RowVersionDefault & (DataRowVersion)(-1025);
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000678 RID: 1656 RVA: 0x00019D7A File Offset: 0x00017F7A
		private DataRowVersion RowVersionDefault
		{
			get
			{
				return this.Row.GetDefaultRowVersion(this._dataView.RowStateFilter);
			}
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x00019D92 File Offset: 0x00017F92
		internal int GetRecord()
		{
			return this.Row.GetRecordFromVersion(this.RowVersionDefault);
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x00019DA5 File Offset: 0x00017FA5
		internal bool HasRecord()
		{
			return this.Row.HasVersion(this.RowVersionDefault);
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x00019DB8 File Offset: 0x00017FB8
		internal object GetColumnValue(DataColumn column)
		{
			return this.Row[column, this.RowVersionDefault];
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x00019DCC File Offset: 0x00017FCC
		internal void SetColumnValue(DataColumn column, object value)
		{
			if (this._delayBeginEdit)
			{
				this._delayBeginEdit = false;
				this.Row.BeginEdit();
			}
			if (DataRowVersion.Original == this.RowVersionDefault)
			{
				throw ExceptionBuilder.SetFailed(column.ColumnName);
			}
			this.Row[column] = value;
		}

		/// <summary>Returns a <see cref="T:System.Data.DataView" /> for the child <see cref="T:System.Data.DataTable" /> with the specified <see cref="T:System.Data.DataRelation" /> and parent..</summary>
		/// <returns>A <see cref="T:System.Data.DataView" /> for the child <see cref="T:System.Data.DataTable" />.</returns>
		/// <param name="relation">The <see cref="T:System.Data.DataRelation" /> object.</param>
		/// <param name="followParent">The parent object.</param>
		// Token: 0x0600067D RID: 1661 RVA: 0x00019E1C File Offset: 0x0001801C
		public DataView CreateChildView(DataRelation relation, bool followParent)
		{
			if (relation == null || relation.ParentKey.Table != this.DataView.Table)
			{
				throw ExceptionBuilder.CreateChildView();
			}
			RelatedView relatedView;
			if (!followParent)
			{
				int record = this.GetRecord();
				object[] keyValues = relation.ParentKey.GetKeyValues(record);
				relatedView = new RelatedView(relation.ChildColumnsReference, keyValues);
			}
			else
			{
				relatedView = new RelatedView(this, relation.ParentKey, relation.ChildColumnsReference);
			}
			relatedView.SetIndex("", DataViewRowState.CurrentRows, null);
			relatedView.SetDataViewManager(this.DataView.DataViewManager);
			return relatedView;
		}

		/// <summary>Returns a <see cref="T:System.Data.DataView" /> for the child <see cref="T:System.Data.DataTable" /> with the specified child <see cref="T:System.Data.DataRelation" />.</summary>
		/// <returns>a <see cref="T:System.Data.DataView" /> for the child <see cref="T:System.Data.DataTable" />.</returns>
		/// <param name="relation">The <see cref="T:System.Data.DataRelation" /> object. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600067E RID: 1662 RVA: 0x00019EA9 File Offset: 0x000180A9
		public DataView CreateChildView(DataRelation relation)
		{
			return this.CreateChildView(relation, false);
		}

		/// <summary>Returns a <see cref="T:System.Data.DataView" /> for the child <see cref="T:System.Data.DataTable" /> with the specified <see cref="T:System.Data.DataRelation" /> name and parent.</summary>
		/// <returns>a <see cref="T:System.Data.DataView" /> for the child <see cref="T:System.Data.DataTable" />.</returns>
		/// <param name="relationName">A string containing the <see cref="T:System.Data.DataRelation" /> name.</param>
		/// <param name="followParent">The parent</param>
		// Token: 0x0600067F RID: 1663 RVA: 0x00019EB3 File Offset: 0x000180B3
		public DataView CreateChildView(string relationName, bool followParent)
		{
			return this.CreateChildView(this.DataView.Table.ChildRelations[relationName], followParent);
		}

		/// <summary>Returns a <see cref="T:System.Data.DataView" /> for the child <see cref="T:System.Data.DataTable" /> with the specified child <see cref="T:System.Data.DataRelation" /> name.</summary>
		/// <returns>a <see cref="T:System.Data.DataView" /> for the child <see cref="T:System.Data.DataTable" />.</returns>
		/// <param name="relationName">A string containing the <see cref="T:System.Data.DataRelation" /> name. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000680 RID: 1664 RVA: 0x00019ED2 File Offset: 0x000180D2
		public DataView CreateChildView(string relationName)
		{
			return this.CreateChildView(relationName, false);
		}

		/// <summary>Gets the <see cref="T:System.Data.DataRow" /> being viewed.</summary>
		/// <returns>The <see cref="T:System.Data.DataRow" /> being viewed by the <see cref="T:System.Data.DataRowView" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000681 RID: 1665 RVA: 0x00019EDC File Offset: 0x000180DC
		public DataRow Row
		{
			get
			{
				return this._row;
			}
		}

		/// <summary>Begins an edit procedure.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000682 RID: 1666 RVA: 0x00019EE4 File Offset: 0x000180E4
		public void BeginEdit()
		{
			this._delayBeginEdit = true;
		}

		/// <summary>Cancels an edit procedure.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000683 RID: 1667 RVA: 0x00019EF0 File Offset: 0x000180F0
		public void CancelEdit()
		{
			DataRow row = this.Row;
			if (this.IsNew)
			{
				this._dataView.FinishAddNew(false);
			}
			else
			{
				row.CancelEdit();
			}
			this._delayBeginEdit = false;
		}

		/// <summary>Commits changes to the underlying <see cref="T:System.Data.DataRow" /> and ends the editing session that was begun with <see cref="M:System.Data.DataRowView.BeginEdit" />.  Use <see cref="M:System.Data.DataRowView.CancelEdit" /> to discard the changes made to the <see cref="T:System.Data.DataRow" />.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000684 RID: 1668 RVA: 0x00019F27 File Offset: 0x00018127
		public void EndEdit()
		{
			if (this.IsNew)
			{
				this._dataView.FinishAddNew(true);
			}
			else
			{
				this.Row.EndEdit();
			}
			this._delayBeginEdit = false;
		}

		/// <summary>Indicates whether a <see cref="T:System.Data.DataRowView" /> is new.</summary>
		/// <returns>true if the row is new; otherwise false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000685 RID: 1669 RVA: 0x00019F51 File Offset: 0x00018151
		public bool IsNew
		{
			get
			{
				return this._row == this._dataView._addNewRow;
			}
		}

		/// <summary>Indicates whether the row is in edit mode.</summary>
		/// <returns>true if the row is in edit mode; otherwise false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000686 RID: 1670 RVA: 0x00019F66 File Offset: 0x00018166
		public bool IsEdit
		{
			get
			{
				return this.Row.HasVersion(DataRowVersion.Proposed) || this._delayBeginEdit;
			}
		}

		/// <summary>Deletes a row.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000687 RID: 1671 RVA: 0x00019F82 File Offset: 0x00018182
		public void Delete()
		{
			this._dataView.Delete(this.Row);
		}

		/// <summary>Event that is raised when a <see cref="T:System.Data.DataRowView" /> property is changed.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1400000A RID: 10
		// (add) Token: 0x06000688 RID: 1672 RVA: 0x00019F98 File Offset: 0x00018198
		// (remove) Token: 0x06000689 RID: 1673 RVA: 0x00019FD0 File Offset: 0x000181D0
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x0600068A RID: 1674 RVA: 0x0001A005 File Offset: 0x00018205
		internal void RaisePropertyChangedEvent(string propName)
		{
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if (propertyChanged == null)
			{
				return;
			}
			propertyChanged(this, new PropertyChangedEventArgs(propName));
		}

		/// <summary>Returns a collection of custom attributes for this instance of a component.</summary>
		/// <returns>An AttributeCollection containing the attributes for this object.</returns>
		// Token: 0x0600068B RID: 1675 RVA: 0x0001A01E File Offset: 0x0001821E
		AttributeCollection ICustomTypeDescriptor.GetAttributes()
		{
			return new AttributeCollection(null);
		}

		/// <summary>Returns the class name of this instance of a component.</summary>
		/// <returns>The class name of this instance of a component.</returns>
		// Token: 0x0600068C RID: 1676 RVA: 0x00004526 File Offset: 0x00002726
		string ICustomTypeDescriptor.GetClassName()
		{
			return null;
		}

		/// <summary>Returns the name of this instance of a component.</summary>
		/// <returns>The name of this instance of a component.</returns>
		// Token: 0x0600068D RID: 1677 RVA: 0x00004526 File Offset: 0x00002726
		string ICustomTypeDescriptor.GetComponentName()
		{
			return null;
		}

		/// <summary>Returns a type converter for this instance of a component.</summary>
		/// <returns>The type converter for this instance of a component.</returns>
		// Token: 0x0600068E RID: 1678 RVA: 0x00004526 File Offset: 0x00002726
		TypeConverter ICustomTypeDescriptor.GetConverter()
		{
			return null;
		}

		/// <summary>Returns the default event for this instance of a component.</summary>
		/// <returns>The default event for this instance of a component.</returns>
		// Token: 0x0600068F RID: 1679 RVA: 0x00004526 File Offset: 0x00002726
		EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
		{
			return null;
		}

		/// <summary>Returns the default property for this instance of a component.</summary>
		/// <returns>The default property for this instance of a component.</returns>
		// Token: 0x06000690 RID: 1680 RVA: 0x00004526 File Offset: 0x00002726
		PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
		{
			return null;
		}

		/// <summary>Returns an editor of the specified type for this instance of a component.</summary>
		/// <returns>An <see cref="T:System.Object" /> of the specified type that is the editor for this object, or null if the editor cannot be found.</returns>
		/// <param name="editorBaseType">A <see cref="T:System.Type" /> that represents the editor for this object. </param>
		// Token: 0x06000691 RID: 1681 RVA: 0x00004526 File Offset: 0x00002726
		object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
		{
			return null;
		}

		/// <summary>Returns the events for this instance of a component.</summary>
		/// <returns>The events for this instance of a component.</returns>
		// Token: 0x06000692 RID: 1682 RVA: 0x0001A026 File Offset: 0x00018226
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
		{
			return new EventDescriptorCollection(null);
		}

		/// <summary>Returns the events for this instance of a component with specified attributes.</summary>
		/// <returns>The events for this instance of a component.</returns>
		/// <param name="attributes">The attributes</param>
		// Token: 0x06000693 RID: 1683 RVA: 0x0001A026 File Offset: 0x00018226
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
		{
			return new EventDescriptorCollection(null);
		}

		/// <summary>Returns the properties for this instance of a component.</summary>
		/// <returns>The properties for this instance of a component.</returns>
		// Token: 0x06000694 RID: 1684 RVA: 0x0001A02E File Offset: 0x0001822E
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
		{
			return ((ICustomTypeDescriptor)this).GetProperties(null);
		}

		/// <summary>Returns the properties for this instance of a component with specified attributes.</summary>
		/// <returns>The properties for this instance of a component.</returns>
		/// <param name="attributes">The attributes.</param>
		// Token: 0x06000695 RID: 1685 RVA: 0x0001A037 File Offset: 0x00018237
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
		{
			if (this._dataView.Table == null)
			{
				return DataRowView.s_zeroPropertyDescriptorCollection;
			}
			return this._dataView.Table.GetPropertyDescriptorCollection(attributes);
		}

		/// <summary>Returns an object that contains the property described by the specified property descriptor.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the owner of the specified property.</returns>
		/// <param name="pd">A <see cref="T:System.ComponentModel.PropertyDescriptor" /> that represents the property whose owner is to be found. </param>
		// Token: 0x06000696 RID: 1686 RVA: 0x00005D82 File Offset: 0x00003F82
		object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
		{
			return this;
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x00010468 File Offset: 0x0000E668
		internal DataRowView()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040005A5 RID: 1445
		private readonly DataView _dataView;

		// Token: 0x040005A6 RID: 1446
		private readonly DataRow _row;

		// Token: 0x040005A7 RID: 1447
		private bool _delayBeginEdit;

		// Token: 0x040005A8 RID: 1448
		private static readonly PropertyDescriptorCollection s_zeroPropertyDescriptorCollection = new PropertyDescriptorCollection(null);
	}
}
