using System;

namespace System.ComponentModel
{
	/// <summary>Identifies a data operation method exposed by a type, what type of operation the method performs, and whether the method is the default data method. This class cannot be inherited.</summary>
	// Token: 0x02000253 RID: 595
	[AttributeUsage(AttributeTargets.Method)]
	public sealed class DataObjectMethodAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataObjectMethodAttribute" /> class and identifies the type of data operation the method performs.</summary>
		/// <param name="methodType">One of the <see cref="T:System.ComponentModel.DataObjectMethodType" /> values that describes the data operation the method performs.</param>
		// Token: 0x06001324 RID: 4900 RVA: 0x00050A07 File Offset: 0x0004EC07
		public DataObjectMethodAttribute(DataObjectMethodType methodType)
			: this(methodType, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataObjectMethodAttribute" /> class, identifies the type of data operation the method performs, and identifies whether the method is the default data method that the data object exposes.</summary>
		/// <param name="methodType">One of the <see cref="T:System.ComponentModel.DataObjectMethodType" /> values that describes the data operation the method performs.</param>
		/// <param name="isDefault">true to indicate the method that the attribute is applied to is the default method of the data object for the specified <paramref name="methodType" />; otherwise, false.</param>
		// Token: 0x06001325 RID: 4901 RVA: 0x00050A11 File Offset: 0x0004EC11
		public DataObjectMethodAttribute(DataObjectMethodType methodType, bool isDefault)
		{
			this._methodType = methodType;
			this._isDefault = isDefault;
		}

		/// <summary>Gets a value indicating whether the method that the <see cref="T:System.ComponentModel.DataObjectMethodAttribute" /> is applied to is the default data method exposed by the data object for a specific method type.</summary>
		/// <returns>true if the method is the default method exposed by the object for a method type; otherwise, false.</returns>
		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x06001326 RID: 4902 RVA: 0x00050A27 File Offset: 0x0004EC27
		public bool IsDefault
		{
			get
			{
				return this._isDefault;
			}
		}

		/// <summary>Gets a <see cref="T:System.ComponentModel.DataObjectMethodType" /> value indicating the type of data operation the method performs.</summary>
		/// <returns>One of the <see cref="T:System.ComponentModel.DataObjectMethodType" /> values that identifies the type of data operation performed by the method to which the <see cref="T:System.ComponentModel.DataObjectMethodAttribute" /> is applied.</returns>
		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x06001327 RID: 4903 RVA: 0x00050A2F File Offset: 0x0004EC2F
		public DataObjectMethodType MethodType
		{
			get
			{
				return this._methodType;
			}
		}

		/// <summary>Returns a value indicating whether this instance is equal to a specified object.</summary>
		/// <returns>true if this instance is the same as the instance specified by the <paramref name="obj" /> parameter; otherwise, false.</returns>
		/// <param name="obj">An object to compare with this instance of <see cref="T:System.ComponentModel.DataObjectMethodAttribute" />.</param>
		// Token: 0x06001328 RID: 4904 RVA: 0x00050A38 File Offset: 0x0004EC38
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DataObjectMethodAttribute dataObjectMethodAttribute = obj as DataObjectMethodAttribute;
			return dataObjectMethodAttribute != null && dataObjectMethodAttribute.MethodType == this.MethodType && dataObjectMethodAttribute.IsDefault == this.IsDefault;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06001329 RID: 4905 RVA: 0x00050A74 File Offset: 0x0004EC74
		public override int GetHashCode()
		{
			int methodType = (int)this._methodType;
			return methodType.GetHashCode() ^ this._isDefault.GetHashCode();
		}

		/// <summary>Gets a value indicating whether this instance shares a common pattern with a specified attribute.</summary>
		/// <returns>true if this instance is the same as the instance specified by the <paramref name="obj" /> parameter; otherwise, false.</returns>
		/// <param name="obj">An object to compare with this instance of <see cref="T:System.ComponentModel.DataObjectMethodAttribute" />.</param>
		// Token: 0x0600132A RID: 4906 RVA: 0x00050A9C File Offset: 0x0004EC9C
		public override bool Match(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DataObjectMethodAttribute dataObjectMethodAttribute = obj as DataObjectMethodAttribute;
			return dataObjectMethodAttribute != null && dataObjectMethodAttribute.MethodType == this.MethodType;
		}

		// Token: 0x0400129E RID: 4766
		private bool _isDefault;

		// Token: 0x0400129F RID: 4767
		private DataObjectMethodType _methodType;
	}
}
