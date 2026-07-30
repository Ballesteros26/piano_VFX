using System;
using System.ComponentModel;
using System.Data.Common;
using System.Globalization;

namespace System.Data
{
	/// <summary>Represents a constraint that can be enforced on one or more <see cref="T:System.Data.DataColumn" /> objects.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000057 RID: 87
	[TypeConverter(typeof(ConstraintConverter))]
	[DefaultProperty("ConstraintName")]
	public abstract class Constraint
	{
		/// <summary>The name of a constraint in the <see cref="T:System.Data.ConstraintCollection" />.</summary>
		/// <returns>The name of the <see cref="T:System.Data.Constraint" />.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Data.Constraint" /> name is a null value or empty string. </exception>
		/// <exception cref="T:System.Data.DuplicateNameException">The <see cref="T:System.Data.ConstraintCollection" /> already contains a <see cref="T:System.Data.Constraint" /> with the same name (The comparison is not case-sensitive.). </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060002B1 RID: 689 RVA: 0x0000F3ED File Offset: 0x0000D5ED
		// (set) Token: 0x060002B2 RID: 690 RVA: 0x0000F3F8 File Offset: 0x0000D5F8
		[DefaultValue("")]
		public virtual string ConstraintName
		{
			get
			{
				return this._name;
			}
			set
			{
				if (value == null)
				{
					value = string.Empty;
				}
				if (string.IsNullOrEmpty(value) && this.Table != null && this.InCollection)
				{
					throw ExceptionBuilder.NoConstraintName();
				}
				CultureInfo cultureInfo = ((this.Table != null) ? this.Table.Locale : CultureInfo.CurrentCulture);
				if (string.Compare(this._name, value, true, cultureInfo) != 0)
				{
					if (this.Table != null && this.InCollection)
					{
						this.Table.Constraints.RegisterName(value);
						if (this._name.Length != 0)
						{
							this.Table.Constraints.UnregisterName(this._name);
						}
					}
					this._name = value;
					return;
				}
				if (string.Compare(this._name, value, false, cultureInfo) != 0)
				{
					this._name = value;
				}
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060002B3 RID: 691 RVA: 0x0000F4BB File Offset: 0x0000D6BB
		// (set) Token: 0x060002B4 RID: 692 RVA: 0x0000F4D7 File Offset: 0x0000D6D7
		internal string SchemaName
		{
			get
			{
				if (!string.IsNullOrEmpty(this._schemaName))
				{
					return this._schemaName;
				}
				return this.ConstraintName;
			}
			set
			{
				if (!string.IsNullOrEmpty(value))
				{
					this._schemaName = value;
				}
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060002B5 RID: 693 RVA: 0x0000F4E8 File Offset: 0x0000D6E8
		// (set) Token: 0x060002B6 RID: 694 RVA: 0x0000F4F0 File Offset: 0x0000D6F0
		internal virtual bool InCollection
		{
			get
			{
				return this._inCollection;
			}
			set
			{
				this._inCollection = value;
				this._dataSet = (value ? this.Table.DataSet : null);
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.DataTable" /> to which the constraint applies.</summary>
		/// <returns>A <see cref="T:System.Data.DataTable" /> to which the constraint applies.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060002B7 RID: 695
		public abstract DataTable Table { get; }

		/// <summary>Gets the collection of user-defined constraint properties.</summary>
		/// <returns>A <see cref="T:System.Data.PropertyCollection" /> of custom information.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060002B8 RID: 696 RVA: 0x0000F510 File Offset: 0x0000D710
		[Browsable(false)]
		public PropertyCollection ExtendedProperties
		{
			get
			{
				PropertyCollection propertyCollection;
				if ((propertyCollection = this._extendedProperties) == null)
				{
					propertyCollection = (this._extendedProperties = new PropertyCollection());
				}
				return propertyCollection;
			}
		}

		// Token: 0x060002B9 RID: 697
		internal abstract bool ContainsColumn(DataColumn column);

		// Token: 0x060002BA RID: 698
		internal abstract bool CanEnableConstraint();

		// Token: 0x060002BB RID: 699
		internal abstract Constraint Clone(DataSet destination);

		// Token: 0x060002BC RID: 700
		internal abstract Constraint Clone(DataSet destination, bool ignoreNSforTableLookup);

		// Token: 0x060002BD RID: 701 RVA: 0x0000F535 File Offset: 0x0000D735
		internal void CheckConstraint()
		{
			if (!this.CanEnableConstraint())
			{
				throw ExceptionBuilder.ConstraintViolation(this.ConstraintName);
			}
		}

		// Token: 0x060002BE RID: 702
		internal abstract void CheckCanAddToCollection(ConstraintCollection constraint);

		// Token: 0x060002BF RID: 703
		internal abstract bool CanBeRemovedFromCollection(ConstraintCollection constraint, bool fThrowException);

		// Token: 0x060002C0 RID: 704
		internal abstract void CheckConstraint(DataRow row, DataRowAction action);

		// Token: 0x060002C1 RID: 705
		internal abstract void CheckState();

		/// <summary>Gets the <see cref="T:System.Data.DataSet" /> to which this constraint belongs.</summary>
		// Token: 0x060002C2 RID: 706 RVA: 0x0000F54C File Offset: 0x0000D74C
		protected void CheckStateForProperty()
		{
			try
			{
				this.CheckState();
			}
			catch (Exception ex) when (ADP.IsCatchableExceptionType(ex))
			{
				throw ExceptionBuilder.BadObjectPropertyAccess(ex.Message);
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.DataSet" /> to which this constraint belongs.</summary>
		/// <returns>The <see cref="T:System.Data.DataSet" /> to which the constraint belongs.</returns>
		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060002C3 RID: 707 RVA: 0x0000F598 File Offset: 0x0000D798
		[CLSCompliant(false)]
		protected virtual DataSet _DataSet
		{
			get
			{
				return this._dataSet;
			}
		}

		/// <summary>Sets the constraint's <see cref="T:System.Data.DataSet" />.</summary>
		/// <param name="dataSet">The <see cref="T:System.Data.DataSet" /> to which this constraint will belong. </param>
		// Token: 0x060002C4 RID: 708 RVA: 0x0000F5A0 File Offset: 0x0000D7A0
		protected internal void SetDataSet(DataSet dataSet)
		{
			this._dataSet = dataSet;
		}

		// Token: 0x060002C5 RID: 709
		internal abstract bool IsConstraintViolated();

		/// <summary>Gets the <see cref="P:System.Data.Constraint.ConstraintName" />, if there is one, as a string.</summary>
		/// <returns>The string value of the <see cref="P:System.Data.Constraint.ConstraintName" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060002C6 RID: 710 RVA: 0x0000F5A9 File Offset: 0x0000D7A9
		public override string ToString()
		{
			return this.ConstraintName;
		}

		// Token: 0x04000505 RID: 1285
		private string _schemaName = string.Empty;

		// Token: 0x04000506 RID: 1286
		private bool _inCollection;

		// Token: 0x04000507 RID: 1287
		private DataSet _dataSet;

		// Token: 0x04000508 RID: 1288
		internal string _name = string.Empty;

		// Token: 0x04000509 RID: 1289
		internal PropertyCollection _extendedProperties;
	}
}
