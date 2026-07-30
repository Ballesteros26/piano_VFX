using System;

namespace System.CodeDom
{
	/// <summary>Represents an expression used as a method invoke parameter along with a reference direction indicator.</summary>
	// Token: 0x02000769 RID: 1897
	[Serializable]
	public class CodeDirectionExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeDirectionExpression" /> class.</summary>
		// Token: 0x06003C34 RID: 15412 RVA: 0x000D82AC File Offset: 0x000D64AC
		public CodeDirectionExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeDirectionExpression" /> class using the specified field direction and expression.</summary>
		/// <param name="direction">A <see cref="T:System.CodeDom.FieldDirection" /> that indicates the field direction of the expression. </param>
		/// <param name="expression">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the code expression to represent. </param>
		// Token: 0x06003C35 RID: 15413 RVA: 0x000D8EB6 File Offset: 0x000D70B6
		public CodeDirectionExpression(FieldDirection direction, CodeExpression expression)
		{
			this.Expression = expression;
			this.Direction = direction;
		}

		/// <summary>Gets or sets the code expression to represent.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the expression to represent.</returns>
		// Token: 0x17000E91 RID: 3729
		// (get) Token: 0x06003C36 RID: 15414 RVA: 0x000D8ECC File Offset: 0x000D70CC
		// (set) Token: 0x06003C37 RID: 15415 RVA: 0x000D8ED4 File Offset: 0x000D70D4
		public CodeExpression Expression { get; set; }

		/// <summary>Gets or sets the field direction for this direction expression.</summary>
		/// <returns>A <see cref="T:System.CodeDom.FieldDirection" /> that indicates the field direction for this direction expression.</returns>
		// Token: 0x17000E92 RID: 3730
		// (get) Token: 0x06003C38 RID: 15416 RVA: 0x000D8EDD File Offset: 0x000D70DD
		// (set) Token: 0x06003C39 RID: 15417 RVA: 0x000D8EE5 File Offset: 0x000D70E5
		public FieldDirection Direction { get; set; }
	}
}
