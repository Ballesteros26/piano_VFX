using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Dynamic.Utils;
using System.Reflection;
using Unity;

namespace System.Linq.Expressions
{
	/// <summary>Represents indexing a property or array.</summary>
	// Token: 0x02000277 RID: 631
	[DebuggerTypeProxy(typeof(Expression.IndexExpressionProxy))]
	public sealed class IndexExpression : Expression, IArgumentProvider
	{
		// Token: 0x0600127F RID: 4735 RVA: 0x0003AEFC File Offset: 0x000390FC
		internal IndexExpression(Expression instance, PropertyInfo indexer, IReadOnlyList<Expression> arguments)
		{
			indexer == null;
			this.Object = instance;
			this.Indexer = indexer;
			this._arguments = arguments;
		}

		/// <summary>Returns the node type of this <see cref="T:System.Linq.Expressions.Expression" />.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.ExpressionType" /> that represents this expression.</returns>
		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06001280 RID: 4736 RVA: 0x0003AF21 File Offset: 0x00039121
		public sealed override ExpressionType NodeType
		{
			get
			{
				return ExpressionType.Index;
			}
		}

		/// <summary>Gets the static type of the expression that this <see cref="T:System.Linq.Expressions.Expression" /> represents.</summary>
		/// <returns>The <see cref="P:System.Linq.Expressions.IndexExpression.Type" /> that represents the static type of the expression.</returns>
		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06001281 RID: 4737 RVA: 0x0003AF25 File Offset: 0x00039125
		public sealed override Type Type
		{
			get
			{
				if (this.Indexer != null)
				{
					return this.Indexer.PropertyType;
				}
				return this.Object.Type.GetElementType();
			}
		}

		/// <summary>An object to index.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.Expression" /> representing the object to index.</returns>
		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06001282 RID: 4738 RVA: 0x0003AF51 File Offset: 0x00039151
		public Expression Object { get; }

		/// <summary>Gets the <see cref="T:System.Reflection.PropertyInfo" /> for the property if the expression represents an indexed property, returns null otherwise.</summary>
		/// <returns>The <see cref="T:System.Reflection.PropertyInfo" /> for the property if the expression represents an indexed property, otherwise null.</returns>
		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06001283 RID: 4739 RVA: 0x0003AF59 File Offset: 0x00039159
		public PropertyInfo Indexer { get; }

		/// <summary>Gets the arguments that will be used to index the property or array.</summary>
		/// <returns>The read-only collection containing the arguments that will be used to index the property or array.</returns>
		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06001284 RID: 4740 RVA: 0x0003AF61 File Offset: 0x00039161
		public ReadOnlyCollection<Expression> Arguments
		{
			get
			{
				return ExpressionUtils.ReturnReadOnly<Expression>(ref this._arguments);
			}
		}

		/// <summary>Creates a new expression that is like this one, but using the supplied children. If all of the children are the same, it will return this expression.</summary>
		/// <returns>This expression if no children are changed or an expression with the updated children.</returns>
		/// <param name="object">The <see cref="P:System.Linq.Expressions.IndexExpression.Object" /> property of the result.</param>
		/// <param name="arguments">The <see cref="P:System.Linq.Expressions.IndexExpression.Arguments" /> property of the result.</param>
		// Token: 0x06001285 RID: 4741 RVA: 0x0003AF6E File Offset: 0x0003916E
		public IndexExpression Update(Expression @object, IEnumerable<Expression> arguments)
		{
			if (((@object == this.Object) & (arguments != null)) && ExpressionUtils.SameElements<Expression>(ref arguments, this.Arguments))
			{
				return this;
			}
			return Expression.MakeIndex(@object, this.Indexer, arguments);
		}

		// Token: 0x06001286 RID: 4742 RVA: 0x0003AF9E File Offset: 0x0003919E
		public Expression GetArgument(int index)
		{
			return this._arguments[index];
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06001287 RID: 4743 RVA: 0x0003AFAC File Offset: 0x000391AC
		public int ArgumentCount
		{
			get
			{
				return this._arguments.Count;
			}
		}

		// Token: 0x06001288 RID: 4744 RVA: 0x0003AFB9 File Offset: 0x000391B9
		protected internal override Expression Accept(ExpressionVisitor visitor)
		{
			return visitor.VisitIndex(this);
		}

		// Token: 0x06001289 RID: 4745 RVA: 0x0003AFC4 File Offset: 0x000391C4
		internal Expression Rewrite(Expression instance, Expression[] arguments)
		{
			return Expression.MakeIndex(instance, this.Indexer, arguments ?? this._arguments);
		}

		// Token: 0x0600128A RID: 4746 RVA: 0x0000220F File Offset: 0x0000040F
		internal IndexExpression()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000962 RID: 2402
		private IReadOnlyList<Expression> _arguments;
	}
}
