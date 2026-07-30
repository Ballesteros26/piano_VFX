using System;
using System.CodeDom;

namespace System.ComponentModel.Design.Serialization
{
	/// <summary>Provides a means of passing context state among serializers. This class cannot be inherited.</summary>
	// Token: 0x02000156 RID: 342
	public sealed class ExpressionContext
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Serialization.ExpressionContext" /> class with the given expression and owner. </summary>
		/// <param name="expression">The given code expression.</param>
		/// <param name="expressionType">The given code expression type.</param>
		/// <param name="owner">The given code expression owner.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="expression" />, <paramref name="expressionType" />, or <paramref name="owner" /> is null.</exception>
		// Token: 0x06000A71 RID: 2673 RVA: 0x00015739 File Offset: 0x00013939
		public ExpressionContext(CodeExpression expression, Type expressionType, object owner)
		{
			this._expression = expression;
			this._expressionType = expressionType;
			this._owner = owner;
			this._presetValue = null;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Serialization.ExpressionContext" /> class with a current value.</summary>
		/// <param name="expression">The given code expression.</param>
		/// <param name="expressionType">The given code expression type.</param>
		/// <param name="owner">The given code expression owner.</param>
		/// <param name="presetValue">The given code expression preset value.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="expression" />, <paramref name="expressionType" />, or <paramref name="owner" /> is null.</exception>
		// Token: 0x06000A72 RID: 2674 RVA: 0x0001575D File Offset: 0x0001395D
		public ExpressionContext(CodeExpression expression, Type expressionType, object owner, object presetValue)
		{
			this._expression = expression;
			this._expressionType = expressionType;
			this._owner = owner;
			this._presetValue = presetValue;
		}

		/// <summary>Gets the preset value of an expression.</summary>
		/// <returns>The preset value of this expression, or null if not assigned.</returns>
		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000A73 RID: 2675 RVA: 0x00015782 File Offset: 0x00013982
		public object PresetValue
		{
			get
			{
				return this._presetValue;
			}
		}

		/// <summary>Gets the expression this context represents.</summary>
		/// <returns>The expression this context represents.</returns>
		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000A74 RID: 2676 RVA: 0x0001578A File Offset: 0x0001398A
		public CodeExpression Expression
		{
			get
			{
				return this._expression;
			}
		}

		/// <summary>Gets the <see cref="T:System.Type" /> of the expression.</summary>
		/// <returns>The <see cref="T:System.Type" /> of the expression.</returns>
		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000A75 RID: 2677 RVA: 0x00015792 File Offset: 0x00013992
		public Type ExpressionType
		{
			get
			{
				return this._expressionType;
			}
		}

		/// <summary>Gets the object owning this expression.</summary>
		/// <returns>The object owning this expression.</returns>
		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000A76 RID: 2678 RVA: 0x0001579A File Offset: 0x0001399A
		public object Owner
		{
			get
			{
				return this._owner;
			}
		}

		// Token: 0x04000268 RID: 616
		private object _owner;

		// Token: 0x04000269 RID: 617
		private Type _expressionType;

		// Token: 0x0400026A RID: 618
		private CodeExpression _expression;

		// Token: 0x0400026B RID: 619
		private object _presetValue;
	}
}
