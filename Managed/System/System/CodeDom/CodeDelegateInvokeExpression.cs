using System;

namespace System.CodeDom
{
	/// <summary>Represents an expression that raises an event.</summary>
	// Token: 0x02000768 RID: 1896
	[Serializable]
	public class CodeDelegateInvokeExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeDelegateInvokeExpression" /> class.</summary>
		// Token: 0x06003C2E RID: 15406 RVA: 0x000D8E4A File Offset: 0x000D704A
		public CodeDelegateInvokeExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeDelegateInvokeExpression" /> class using the specified target object.</summary>
		/// <param name="targetObject">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the target object. </param>
		// Token: 0x06003C2F RID: 15407 RVA: 0x000D8E5D File Offset: 0x000D705D
		public CodeDelegateInvokeExpression(CodeExpression targetObject)
		{
			this.TargetObject = targetObject;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeDelegateInvokeExpression" /> class using the specified target object and parameters.</summary>
		/// <param name="targetObject">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the target object. </param>
		/// <param name="parameters">An array of <see cref="T:System.CodeDom.CodeExpression" /> objects that indicate the parameters. </param>
		// Token: 0x06003C30 RID: 15408 RVA: 0x000D8E77 File Offset: 0x000D7077
		public CodeDelegateInvokeExpression(CodeExpression targetObject, params CodeExpression[] parameters)
		{
			this.TargetObject = targetObject;
			this.Parameters.AddRange(parameters);
		}

		/// <summary>Gets or sets the event to invoke.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the event to invoke.</returns>
		// Token: 0x17000E8F RID: 3727
		// (get) Token: 0x06003C31 RID: 15409 RVA: 0x000D8E9D File Offset: 0x000D709D
		// (set) Token: 0x06003C32 RID: 15410 RVA: 0x000D8EA5 File Offset: 0x000D70A5
		public CodeExpression TargetObject { get; set; }

		/// <summary>Gets or sets the parameters to pass to the event handling methods attached to the event.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the parameters to pass to the event handling methods attached to the event.</returns>
		// Token: 0x17000E90 RID: 3728
		// (get) Token: 0x06003C33 RID: 15411 RVA: 0x000D8EAE File Offset: 0x000D70AE
		public CodeExpressionCollection Parameters { get; } = new CodeExpressionCollection();
	}
}
