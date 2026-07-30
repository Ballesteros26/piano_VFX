using System;
using System.Diagnostics;
using System.Dynamic.Utils;
using Unity;

namespace System.Linq.Expressions
{
	/// <summary>Represents a named parameter expression.</summary>
	// Token: 0x020002A8 RID: 680
	[DebuggerTypeProxy(typeof(Expression.ParameterExpressionProxy))]
	public class ParameterExpression : Expression
	{
		// Token: 0x060013C4 RID: 5060 RVA: 0x0003CBD3 File Offset: 0x0003ADD3
		internal ParameterExpression(string name)
		{
			this.Name = name;
		}

		// Token: 0x060013C5 RID: 5061 RVA: 0x0003CBE4 File Offset: 0x0003ADE4
		internal static ParameterExpression Make(Type type, string name, bool isByRef)
		{
			if (isByRef)
			{
				return new ByRefParameterExpression(type, name);
			}
			if (!type.IsEnum)
			{
				switch (type.GetTypeCode())
				{
				case TypeCode.Object:
					if (type == typeof(object))
					{
						return new ParameterExpression(name);
					}
					if (type == typeof(Exception))
					{
						return new PrimitiveParameterExpression<Exception>(name);
					}
					if (type == typeof(object[]))
					{
						return new PrimitiveParameterExpression<object[]>(name);
					}
					break;
				case TypeCode.Boolean:
					return new PrimitiveParameterExpression<bool>(name);
				case TypeCode.Char:
					return new PrimitiveParameterExpression<char>(name);
				case TypeCode.SByte:
					return new PrimitiveParameterExpression<sbyte>(name);
				case TypeCode.Byte:
					return new PrimitiveParameterExpression<byte>(name);
				case TypeCode.Int16:
					return new PrimitiveParameterExpression<short>(name);
				case TypeCode.UInt16:
					return new PrimitiveParameterExpression<ushort>(name);
				case TypeCode.Int32:
					return new PrimitiveParameterExpression<int>(name);
				case TypeCode.UInt32:
					return new PrimitiveParameterExpression<uint>(name);
				case TypeCode.Int64:
					return new PrimitiveParameterExpression<long>(name);
				case TypeCode.UInt64:
					return new PrimitiveParameterExpression<ulong>(name);
				case TypeCode.Single:
					return new PrimitiveParameterExpression<float>(name);
				case TypeCode.Double:
					return new PrimitiveParameterExpression<double>(name);
				case TypeCode.Decimal:
					return new PrimitiveParameterExpression<decimal>(name);
				case TypeCode.DateTime:
					return new PrimitiveParameterExpression<DateTime>(name);
				case TypeCode.String:
					return new PrimitiveParameterExpression<string>(name);
				}
			}
			return new TypedParameterExpression(type, name);
		}

		/// <summary>Gets the static type of the expression that this <see cref="T:System.Linq.Expressions.Expression" /> represents.</summary>
		/// <returns>The <see cref="P:System.Linq.Expressions.ParameterExpression.Type" /> that represents the static type of the expression.</returns>
		// Token: 0x1700036A RID: 874
		// (get) Token: 0x060013C6 RID: 5062 RVA: 0x00037AE6 File Offset: 0x00035CE6
		public override Type Type
		{
			get
			{
				return typeof(object);
			}
		}

		/// <summary>Returns the node type of this <see cref="T:System.Linq.Expressions.Expression" />.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.ExpressionType" /> that represents this expression.</returns>
		// Token: 0x1700036B RID: 875
		// (get) Token: 0x060013C7 RID: 5063 RVA: 0x0003CD1E File Offset: 0x0003AF1E
		public sealed override ExpressionType NodeType
		{
			get
			{
				return ExpressionType.Parameter;
			}
		}

		/// <summary>Gets the name of the parameter or variable.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the name of the parameter.</returns>
		// Token: 0x1700036C RID: 876
		// (get) Token: 0x060013C8 RID: 5064 RVA: 0x0003CD22 File Offset: 0x0003AF22
		public string Name { get; }

		/// <summary>Indicates that this ParameterExpression is to be treated as a ByRef parameter.</summary>
		/// <returns>True if this ParameterExpression is a ByRef parameter, otherwise false.</returns>
		// Token: 0x1700036D RID: 877
		// (get) Token: 0x060013C9 RID: 5065 RVA: 0x0003CD2A File Offset: 0x0003AF2A
		public bool IsByRef
		{
			get
			{
				return this.GetIsByRef();
			}
		}

		// Token: 0x060013CA RID: 5066 RVA: 0x00002285 File Offset: 0x00000485
		internal virtual bool GetIsByRef()
		{
			return false;
		}

		/// <summary>Dispatches to the specific visit method for this node type. For example, <see cref="T:System.Linq.Expressions.MethodCallExpression" /> calls the <see cref="M:System.Linq.Expressions.ExpressionVisitor.VisitMethodCall(System.Linq.Expressions.MethodCallExpression)" />.</summary>
		/// <returns>The result of visiting this node.</returns>
		/// <param name="visitor">The visitor to visit this node with.</param>
		// Token: 0x060013CB RID: 5067 RVA: 0x0003CD32 File Offset: 0x0003AF32
		protected internal override Expression Accept(ExpressionVisitor visitor)
		{
			return visitor.VisitParameter(this);
		}

		// Token: 0x060013CC RID: 5068 RVA: 0x0000220F File Offset: 0x0000040F
		internal ParameterExpression()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
