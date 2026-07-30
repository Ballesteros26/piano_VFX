using System;

namespace System.ComponentModel
{
	/// <summary>Provides metadata for a property representing a data field. This class cannot be inherited.</summary>
	// Token: 0x02000252 RID: 594
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class DataObjectFieldAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataObjectFieldAttribute" /> class and indicates whether the field is the primary key for the data row.</summary>
		/// <param name="primaryKey">true to indicate that the field is in the primary key of the data row; otherwise, false.</param>
		// Token: 0x0600131A RID: 4890 RVA: 0x00050945 File Offset: 0x0004EB45
		public DataObjectFieldAttribute(bool primaryKey)
			: this(primaryKey, false, false, -1)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataObjectFieldAttribute" /> class and indicates whether the field is the primary key for the data row, and whether the field is a database identity field.</summary>
		/// <param name="primaryKey">true to indicate that the field is in the primary key of the data row; otherwise, false.</param>
		/// <param name="isIdentity">true to indicate that the field is an identity field that uniquely identifies the data row; otherwise, false.</param>
		// Token: 0x0600131B RID: 4891 RVA: 0x00050951 File Offset: 0x0004EB51
		public DataObjectFieldAttribute(bool primaryKey, bool isIdentity)
			: this(primaryKey, isIdentity, false, -1)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataObjectFieldAttribute" /> class and indicates whether the field is the primary key for the data row, whether the field is a database identity field, and whether the field can be null.</summary>
		/// <param name="primaryKey">true to indicate that the field is in the primary key of the data row; otherwise, false.</param>
		/// <param name="isIdentity">true to indicate that the field is an identity field that uniquely identifies the data row; otherwise, false.</param>
		/// <param name="isNullable">true to indicate that the field can be null in the data store; otherwise, false.</param>
		// Token: 0x0600131C RID: 4892 RVA: 0x0005095D File Offset: 0x0004EB5D
		public DataObjectFieldAttribute(bool primaryKey, bool isIdentity, bool isNullable)
			: this(primaryKey, isIdentity, isNullable, -1)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataObjectFieldAttribute" /> class and indicates whether the field is the primary key for the data row, whether it is a database identity field, and whether it can be null and sets the length of the field.</summary>
		/// <param name="primaryKey">true to indicate that the field is in the primary key of the data row; otherwise, false.</param>
		/// <param name="isIdentity">true to indicate that the field is an identity field that uniquely identifies the data row; otherwise, false.</param>
		/// <param name="isNullable">true to indicate that the field can be null in the data store; otherwise, false.</param>
		/// <param name="length">The length of the field in bytes.</param>
		// Token: 0x0600131D RID: 4893 RVA: 0x00050969 File Offset: 0x0004EB69
		public DataObjectFieldAttribute(bool primaryKey, bool isIdentity, bool isNullable, int length)
		{
			this._primaryKey = primaryKey;
			this._isIdentity = isIdentity;
			this._isNullable = isNullable;
			this._length = length;
		}

		/// <summary>Gets a value indicating whether a property represents an identity field in the underlying data.</summary>
		/// <returns>true if the property represents an identity field in the underlying data; otherwise, false. The default value is false.</returns>
		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x0600131E RID: 4894 RVA: 0x0005098E File Offset: 0x0004EB8E
		public bool IsIdentity
		{
			get
			{
				return this._isIdentity;
			}
		}

		/// <summary>Gets a value indicating whether a property represents a field that can be null in the underlying data store.</summary>
		/// <returns>true if the property represents a field that can be null in the underlying data store; otherwise, false.</returns>
		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x0600131F RID: 4895 RVA: 0x00050996 File Offset: 0x0004EB96
		public bool IsNullable
		{
			get
			{
				return this._isNullable;
			}
		}

		/// <summary>Gets the length of the property in bytes.</summary>
		/// <returns>The length of the property in bytes, or -1 if not set.</returns>
		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06001320 RID: 4896 RVA: 0x0005099E File Offset: 0x0004EB9E
		public int Length
		{
			get
			{
				return this._length;
			}
		}

		/// <summary>Gets a value indicating whether a property is in the primary key in the underlying data.</summary>
		/// <returns>true if the property is in the primary key of the data store; otherwise, false.</returns>
		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x06001321 RID: 4897 RVA: 0x000509A6 File Offset: 0x0004EBA6
		public bool PrimaryKey
		{
			get
			{
				return this._primaryKey;
			}
		}

		/// <summary>Returns a value indicating whether this instance is equal to a specified object.</summary>
		/// <returns>true if this instance is the same as the instance specified by the <paramref name="obj" /> parameter; otherwise, false.</returns>
		/// <param name="obj">An object to compare with this instance of <see cref="T:System.ComponentModel.DataObjectFieldAttribute" />.</param>
		// Token: 0x06001322 RID: 4898 RVA: 0x000509B0 File Offset: 0x0004EBB0
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DataObjectFieldAttribute dataObjectFieldAttribute = obj as DataObjectFieldAttribute;
			return dataObjectFieldAttribute != null && dataObjectFieldAttribute.IsIdentity == this.IsIdentity && dataObjectFieldAttribute.IsNullable == this.IsNullable && dataObjectFieldAttribute.Length == this.Length && dataObjectFieldAttribute.PrimaryKey == this.PrimaryKey;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06001323 RID: 4899 RVA: 0x0004C98A File Offset: 0x0004AB8A
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0400129A RID: 4762
		private bool _primaryKey;

		// Token: 0x0400129B RID: 4763
		private bool _isIdentity;

		// Token: 0x0400129C RID: 4764
		private bool _isNullable;

		// Token: 0x0400129D RID: 4765
		private int _length;
	}
}
