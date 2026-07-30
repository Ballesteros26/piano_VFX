using System;

namespace System.CodeDom
{
	/// <summary>Represents a reference to the value of a property.</summary>
	// Token: 0x02000785 RID: 1925
	[Serializable]
	public class CodePropertyReferenceExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodePropertyReferenceExpression" /> class.</summary>
		// Token: 0x06003D18 RID: 15640 RVA: 0x000D82AC File Offset: 0x000D64AC
		public CodePropertyReferenceExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodePropertyReferenceExpression" /> class using the specified target object and property name.</summary>
		/// <param name="targetObject">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the object that contains the property to reference. </param>
		/// <param name="propertyName">The name of the property to reference. </param>
		// Token: 0x06003D19 RID: 15641 RVA: 0x000D9FDC File Offset: 0x000D81DC
		public CodePropertyReferenceExpression(CodeExpression targetObject, string propertyName)
		{
			this.TargetObject = targetObject;
			this.PropertyName = propertyName;
		}

		/// <summary>Gets or sets the object that contains the property to reference.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the object that contains the property to reference.</returns>
		// Token: 0x17000ED3 RID: 3795
		// (get) Token: 0x06003D1A RID: 15642 RVA: 0x000D9FF2 File Offset: 0x000D81F2
		// (set) Token: 0x06003D1B RID: 15643 RVA: 0x000D9FFA File Offset: 0x000D81FA
		public CodeExpression TargetObject { get; set; }

		/// <summary>Gets or sets the name of the property to reference.</summary>
		/// <returns>The name of the property to reference.</returns>
		// Token: 0x17000ED4 RID: 3796
		// (get) Token: 0x06003D1C RID: 15644 RVA: 0x000DA003 File Offset: 0x000D8203
		// (set) Token: 0x06003D1D RID: 15645 RVA: 0x000DA014 File Offset: 0x000D8214
		public string PropertyName
		{
			get
			{
				return this._propertyName ?? string.Empty;
			}
			set
			{
				this._propertyName = value;
			}
		}

		// Token: 0x04002DD5 RID: 11733
		private string _propertyName;
	}
}
