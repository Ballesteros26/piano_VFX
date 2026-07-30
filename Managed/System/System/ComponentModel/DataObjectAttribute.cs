using System;

namespace System.ComponentModel
{
	/// <summary>Identifies a type as an object suitable for binding to an <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> object. This class cannot be inherited.</summary>
	// Token: 0x02000251 RID: 593
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class DataObjectAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataObjectAttribute" /> class. </summary>
		// Token: 0x06001313 RID: 4883 RVA: 0x000508BB File Offset: 0x0004EABB
		public DataObjectAttribute()
			: this(true)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataObjectAttribute" /> class and indicates whether an object is suitable for binding to an <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> object.</summary>
		/// <param name="isDataObject">true if the object is suitable for binding to an <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> object; otherwise, false.</param>
		// Token: 0x06001314 RID: 4884 RVA: 0x000508C4 File Offset: 0x0004EAC4
		public DataObjectAttribute(bool isDataObject)
		{
			this._isDataObject = isDataObject;
		}

		/// <summary>Gets a value indicating whether an object should be considered suitable for binding to an <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> object at design time.</summary>
		/// <returns>true if the object should be considered suitable for binding to an <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> object; otherwise, false.</returns>
		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06001315 RID: 4885 RVA: 0x000508D3 File Offset: 0x0004EAD3
		public bool IsDataObject
		{
			get
			{
				return this._isDataObject;
			}
		}

		/// <summary>Determines whether this instance of <see cref="T:System.ComponentModel.DataObjectAttribute" /> fits the pattern of another object.</summary>
		/// <returns>true if this instance is the same as the instance specified by the <paramref name="obj" /> parameter; otherwise, false.</returns>
		/// <param name="obj">An object to compare with this instance of <see cref="T:System.ComponentModel.DataObjectAttribute" />. </param>
		// Token: 0x06001316 RID: 4886 RVA: 0x000508DC File Offset: 0x0004EADC
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DataObjectAttribute dataObjectAttribute = obj as DataObjectAttribute;
			return dataObjectAttribute != null && dataObjectAttribute.IsDataObject == this.IsDataObject;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06001317 RID: 4887 RVA: 0x00050909 File Offset: 0x0004EB09
		public override int GetHashCode()
		{
			return this._isDataObject.GetHashCode();
		}

		/// <summary>Gets a value indicating whether the current value of the attribute is the default value for the attribute.</summary>
		/// <returns>true if the current value of the attribute is the default; otherwise, false.</returns>
		// Token: 0x06001318 RID: 4888 RVA: 0x00050916 File Offset: 0x0004EB16
		public override bool IsDefaultAttribute()
		{
			return this.Equals(DataObjectAttribute.Default);
		}

		/// <summary>Indicates that the class is suitable for binding to an <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> object at design time. This field is read-only.</summary>
		// Token: 0x04001296 RID: 4758
		public static readonly DataObjectAttribute DataObject = new DataObjectAttribute(true);

		/// <summary>Indicates that the class is not suitable for binding to an <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> object at design time. This field is read-only.</summary>
		// Token: 0x04001297 RID: 4759
		public static readonly DataObjectAttribute NonDataObject = new DataObjectAttribute(false);

		/// <summary>Represents the default value of the <see cref="T:System.ComponentModel.DataObjectAttribute" /> class, which indicates that the class is suitable for binding to an <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> object at design time. This field is read-only.</summary>
		// Token: 0x04001298 RID: 4760
		public static readonly DataObjectAttribute Default = DataObjectAttribute.NonDataObject;

		// Token: 0x04001299 RID: 4761
		private bool _isDataObject;
	}
}
