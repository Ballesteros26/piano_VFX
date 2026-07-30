using System;

namespace System.CodeDom
{
	/// <summary>Represents a reference to a field.</summary>
	// Token: 0x02000771 RID: 1905
	[Serializable]
	public class CodeFieldReferenceExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeFieldReferenceExpression" /> class.</summary>
		// Token: 0x06003C61 RID: 15457 RVA: 0x000D82AC File Offset: 0x000D64AC
		public CodeFieldReferenceExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeFieldReferenceExpression" /> class using the specified target object and field name.</summary>
		/// <param name="targetObject">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the object that contains the field. </param>
		/// <param name="fieldName">The name of the field. </param>
		// Token: 0x06003C62 RID: 15458 RVA: 0x000D909C File Offset: 0x000D729C
		public CodeFieldReferenceExpression(CodeExpression targetObject, string fieldName)
		{
			this.TargetObject = targetObject;
			this.FieldName = fieldName;
		}

		/// <summary>Gets or sets the object that contains the field to reference.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the object that contains the field to reference.</returns>
		// Token: 0x17000E98 RID: 3736
		// (get) Token: 0x06003C63 RID: 15459 RVA: 0x000D90B2 File Offset: 0x000D72B2
		// (set) Token: 0x06003C64 RID: 15460 RVA: 0x000D90BA File Offset: 0x000D72BA
		public CodeExpression TargetObject { get; set; }

		/// <summary>Gets or sets the name of the field to reference.</summary>
		/// <returns>A string containing the field name.</returns>
		// Token: 0x17000E99 RID: 3737
		// (get) Token: 0x06003C65 RID: 15461 RVA: 0x000D90C3 File Offset: 0x000D72C3
		// (set) Token: 0x06003C66 RID: 15462 RVA: 0x000D90D4 File Offset: 0x000D72D4
		public string FieldName
		{
			get
			{
				return this._fieldName ?? string.Empty;
			}
			set
			{
				this._fieldName = value;
			}
		}

		// Token: 0x04002D94 RID: 11668
		private string _fieldName;
	}
}
