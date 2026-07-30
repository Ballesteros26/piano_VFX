using System;

namespace System.Web.Compilation
{
	/// <summary>Specifies the design-time editor of the expression builder. This class cannot be inherited.</summary>
	// Token: 0x02000604 RID: 1540
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public sealed class ExpressionEditorAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Compilation.ExpressionEditorAttribute" /> class using the specified type object.</summary>
		/// <param name="type">The type reference to associate with the <see cref="T:System.Web.UI.Design.ExpressionEditor" />.</param>
		// Token: 0x060042A0 RID: 17056 RVA: 0x000AFABE File Offset: 0x000ADCBE
		public ExpressionEditorAttribute(Type type)
			: this((type != null) ? type.AssemblyQualifiedName : null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Compilation.ExpressionEditorAttribute" /> class using the specified type name.</summary>
		/// <param name="typeName">The name of the type to associate with the <see cref="T:System.Web.UI.Design.ExpressionEditor" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="typeName" /> is null (Nothing in Visual Basic).</exception>
		// Token: 0x060042A1 RID: 17057 RVA: 0x000AFAD8 File Offset: 0x000ADCD8
		public ExpressionEditorAttribute(string typeName)
		{
			if (string.IsNullOrEmpty(typeName))
			{
				throw new ArgumentNullException("typeName");
			}
			this._editorTypeName = typeName;
		}

		/// <summary>Used by an expression editor to retrieve the editor type name.</summary>
		/// <returns>The name of the editor type.</returns>
		// Token: 0x1700151F RID: 5407
		// (get) Token: 0x060042A2 RID: 17058 RVA: 0x000AFAFA File Offset: 0x000ADCFA
		public string EditorTypeName
		{
			get
			{
				return this._editorTypeName;
			}
		}

		/// <summary>Indicates whether this instance of the <see cref="T:System.Web.Compilation.ExpressionEditorAttribute" /> class and a specified object are equal.</summary>
		/// <returns>true if value is not null and <see cref="P:System.Web.Compilation.ExpressionEditorAttribute.EditorTypeName" /> is equal; otherwise, false.</returns>
		/// <param name="obj">An instance of the <see cref="T:System.Web.Compilation.ExpressionEditorAttribute" /> class or a class that derives from it.</param>
		// Token: 0x060042A3 RID: 17059 RVA: 0x000AFB04 File Offset: 0x000ADD04
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			ExpressionEditorAttribute expressionEditorAttribute = obj as ExpressionEditorAttribute;
			return expressionEditorAttribute != null && expressionEditorAttribute.EditorTypeName == this.EditorTypeName;
		}

		/// <summary>Retrieves the hash code for the value of this <see cref="T:System.Web.Compilation.ExpressionEditorAttribute" /> attribute.</summary>
		/// <returns>The hash code of the value of this <see cref="T:System.Web.Compilation.ExpressionEditorAttribute" />.</returns>
		// Token: 0x060042A4 RID: 17060 RVA: 0x000AFB34 File Offset: 0x000ADD34
		public override int GetHashCode()
		{
			return this.EditorTypeName.GetHashCode();
		}

		// Token: 0x040023AF RID: 9135
		private string _editorTypeName;
	}
}
