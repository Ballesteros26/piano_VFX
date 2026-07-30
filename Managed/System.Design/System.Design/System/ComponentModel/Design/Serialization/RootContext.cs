using System;
using System.CodeDom;

namespace System.ComponentModel.Design.Serialization
{
	/// <summary>A <see cref="T:System.ComponentModel.Design.Serialization.CodeDomSerializer" /> adds a root context to provide a definition of the root object. This class cannot be inherited</summary>
	// Token: 0x0200015E RID: 350
	public sealed class RootContext
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Serialization.RootContext" /> class. </summary>
		/// <param name="expression">The expression representing the root object in the object graph.</param>
		/// <param name="value">The root object of the object graph.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="expression" /> or <paramref name="value" /> is null.</exception>
		// Token: 0x06000A96 RID: 2710 RVA: 0x00016265 File Offset: 0x00014465
		public RootContext(CodeExpression expression, object value)
		{
			if (expression == null)
			{
				throw new ArgumentNullException("expression");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this._expression = expression;
			this._value = value;
		}

		/// <summary>Gets the expression representing the root object in the object graph.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> representing the root object in the object graph.</returns>
		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000A97 RID: 2711 RVA: 0x00016297 File Offset: 0x00014497
		public CodeExpression Expression
		{
			get
			{
				return this._expression;
			}
		}

		/// <summary>Gets the root object of the object graph.</summary>
		/// <returns>The root object of the object graph.</returns>
		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000A98 RID: 2712 RVA: 0x0001629F File Offset: 0x0001449F
		public object Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x04000275 RID: 629
		private CodeExpression _expression;

		// Token: 0x04000276 RID: 630
		private object _value;
	}
}
