using System;

namespace System.CodeDom
{
	/// <summary>Represents a reference to a method.</summary>
	// Token: 0x020007A1 RID: 1953
	[Serializable]
	public class CodeMethodReferenceExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeMethodReferenceExpression" /> class.</summary>
		// Token: 0x06003DD0 RID: 15824 RVA: 0x000D82AC File Offset: 0x000D64AC
		public CodeMethodReferenceExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeMethodReferenceExpression" /> class using the specified target object and method name.</summary>
		/// <param name="targetObject">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the object to target. </param>
		/// <param name="methodName">The name of the method to call. </param>
		// Token: 0x06003DD1 RID: 15825 RVA: 0x000DACAC File Offset: 0x000D8EAC
		public CodeMethodReferenceExpression(CodeExpression targetObject, string methodName)
		{
			this.TargetObject = targetObject;
			this.MethodName = methodName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeMethodReferenceExpression" /> class using the specified target object, method name, and generic type arguments.</summary>
		/// <param name="targetObject">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the object to target. </param>
		/// <param name="methodName">The name of the method to call. </param>
		/// <param name="typeParameters">An array of <see cref="T:System.CodeDom.CodeTypeReference" /> values that specify the <see cref="P:System.CodeDom.CodeMethodReferenceExpression.TypeArguments" /> for this <see cref="T:System.CodeDom.CodeMethodReferenceExpression" />.</param>
		// Token: 0x06003DD2 RID: 15826 RVA: 0x000DACC2 File Offset: 0x000D8EC2
		public CodeMethodReferenceExpression(CodeExpression targetObject, string methodName, params CodeTypeReference[] typeParameters)
		{
			this.TargetObject = targetObject;
			this.MethodName = methodName;
			if (typeParameters != null && typeParameters.Length != 0)
			{
				this.TypeArguments.AddRange(typeParameters);
			}
		}

		/// <summary>Gets or sets the expression that indicates the method to reference.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that represents the method to reference.</returns>
		// Token: 0x17000F05 RID: 3845
		// (get) Token: 0x06003DD3 RID: 15827 RVA: 0x000DACEB File Offset: 0x000D8EEB
		// (set) Token: 0x06003DD4 RID: 15828 RVA: 0x000DACF3 File Offset: 0x000D8EF3
		public CodeExpression TargetObject { get; set; }

		/// <summary>Gets or sets the name of the method to reference.</summary>
		/// <returns>The name of the method to reference.</returns>
		// Token: 0x17000F06 RID: 3846
		// (get) Token: 0x06003DD5 RID: 15829 RVA: 0x000DACFC File Offset: 0x000D8EFC
		// (set) Token: 0x06003DD6 RID: 15830 RVA: 0x000DAD0D File Offset: 0x000D8F0D
		public string MethodName
		{
			get
			{
				return this._methodName ?? string.Empty;
			}
			set
			{
				this._methodName = value;
			}
		}

		/// <summary>Gets the type arguments for the current generic method reference expression.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReferenceCollection" /> containing the type arguments for the current code <see cref="T:System.CodeDom.CodeMethodReferenceExpression" />.</returns>
		// Token: 0x17000F07 RID: 3847
		// (get) Token: 0x06003DD7 RID: 15831 RVA: 0x000DAD18 File Offset: 0x000D8F18
		public CodeTypeReferenceCollection TypeArguments
		{
			get
			{
				CodeTypeReferenceCollection codeTypeReferenceCollection;
				if ((codeTypeReferenceCollection = this._typeArguments) == null)
				{
					codeTypeReferenceCollection = (this._typeArguments = new CodeTypeReferenceCollection());
				}
				return codeTypeReferenceCollection;
			}
		}

		// Token: 0x04002E1F RID: 11807
		private string _methodName;

		// Token: 0x04002E20 RID: 11808
		private CodeTypeReferenceCollection _typeArguments;
	}
}
