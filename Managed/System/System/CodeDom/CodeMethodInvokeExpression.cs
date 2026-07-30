using System;

namespace System.CodeDom
{
	/// <summary>Represents an expression that invokes a method.</summary>
	// Token: 0x0200077B RID: 1915
	[Serializable]
	public class CodeMethodInvokeExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeMethodInvokeExpression" /> class.</summary>
		// Token: 0x06003CB1 RID: 15537 RVA: 0x000D975A File Offset: 0x000D795A
		public CodeMethodInvokeExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeMethodInvokeExpression" /> class using the specified method and parameters.</summary>
		/// <param name="method">A <see cref="T:System.CodeDom.CodeMethodReferenceExpression" /> that indicates the method to invoke. </param>
		/// <param name="parameters">An array of <see cref="T:System.CodeDom.CodeExpression" /> objects that indicate the parameters with which to invoke the method. </param>
		// Token: 0x06003CB2 RID: 15538 RVA: 0x000D976D File Offset: 0x000D796D
		public CodeMethodInvokeExpression(CodeMethodReferenceExpression method, params CodeExpression[] parameters)
		{
			this._method = method;
			this.Parameters.AddRange(parameters);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeMethodInvokeExpression" /> class using the specified target object, method name, and parameters.</summary>
		/// <param name="targetObject">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the target object with the method to invoke. </param>
		/// <param name="methodName">The name of the method to invoke. </param>
		/// <param name="parameters">An array of <see cref="T:System.CodeDom.CodeExpression" /> objects that indicate the parameters to call the method with. </param>
		// Token: 0x06003CB3 RID: 15539 RVA: 0x000D9793 File Offset: 0x000D7993
		public CodeMethodInvokeExpression(CodeExpression targetObject, string methodName, params CodeExpression[] parameters)
		{
			this._method = new CodeMethodReferenceExpression(targetObject, methodName);
			this.Parameters.AddRange(parameters);
		}

		/// <summary>Gets or sets the method to invoke.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeMethodReferenceExpression" /> that indicates the method to invoke.</returns>
		// Token: 0x17000EB9 RID: 3769
		// (get) Token: 0x06003CB4 RID: 15540 RVA: 0x000D97C0 File Offset: 0x000D79C0
		// (set) Token: 0x06003CB5 RID: 15541 RVA: 0x000D97E5 File Offset: 0x000D79E5
		public CodeMethodReferenceExpression Method
		{
			get
			{
				CodeMethodReferenceExpression codeMethodReferenceExpression;
				if ((codeMethodReferenceExpression = this._method) == null)
				{
					codeMethodReferenceExpression = (this._method = new CodeMethodReferenceExpression());
				}
				return codeMethodReferenceExpression;
			}
			set
			{
				this._method = value;
			}
		}

		/// <summary>Gets the parameters to invoke the method with.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpressionCollection" /> that indicates the parameters to invoke the method with.</returns>
		// Token: 0x17000EBA RID: 3770
		// (get) Token: 0x06003CB6 RID: 15542 RVA: 0x000D97EE File Offset: 0x000D79EE
		public CodeExpressionCollection Parameters { get; } = new CodeExpressionCollection();

		// Token: 0x04002DBC RID: 11708
		private CodeMethodReferenceExpression _method;
	}
}
