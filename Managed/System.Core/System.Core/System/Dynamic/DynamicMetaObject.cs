using System;
using System.Collections.Generic;
using System.Dynamic.Utils;
using System.Linq.Expressions;

namespace System.Dynamic
{
	/// <summary>Represents the dynamic binding and a binding logic of an object participating in the dynamic binding.</summary>
	// Token: 0x02000313 RID: 787
	public class DynamicMetaObject
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Dynamic.DynamicMetaObject" /> class.</summary>
		/// <param name="expression">The expression representing this <see cref="T:System.Dynamic.DynamicMetaObject" /> during the dynamic binding process.</param>
		/// <param name="restrictions">The set of binding restrictions under which the binding is valid.</param>
		// Token: 0x060017C5 RID: 6085 RVA: 0x0004D6A6 File Offset: 0x0004B8A6
		public DynamicMetaObject(Expression expression, BindingRestrictions restrictions)
		{
			ContractUtils.RequiresNotNull(expression, "expression");
			ContractUtils.RequiresNotNull(restrictions, "restrictions");
			this.Expression = expression;
			this.Restrictions = restrictions;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Dynamic.DynamicMetaObject" /> class.</summary>
		/// <param name="expression">The expression representing this <see cref="T:System.Dynamic.DynamicMetaObject" /> during the dynamic binding process.</param>
		/// <param name="restrictions">The set of binding restrictions under which the binding is valid.</param>
		/// <param name="value">The runtime value represented by the <see cref="T:System.Dynamic.DynamicMetaObject" />.</param>
		// Token: 0x060017C6 RID: 6086 RVA: 0x0004D6D2 File Offset: 0x0004B8D2
		public DynamicMetaObject(Expression expression, BindingRestrictions restrictions, object value)
			: this(expression, restrictions)
		{
			this.Value = value;
			this.HasValue = true;
		}

		/// <summary>The expression representing the <see cref="T:System.Dynamic.DynamicMetaObject" /> during the dynamic binding process.</summary>
		/// <returns>The expression representing the <see cref="T:System.Dynamic.DynamicMetaObject" /> during the dynamic binding process.</returns>
		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x060017C7 RID: 6087 RVA: 0x0004D6EA File Offset: 0x0004B8EA
		public Expression Expression { get; }

		/// <summary>The set of binding restrictions under which the binding is valid.</summary>
		/// <returns>The set of binding restrictions.</returns>
		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x060017C8 RID: 6088 RVA: 0x0004D6F2 File Offset: 0x0004B8F2
		public BindingRestrictions Restrictions { get; }

		/// <summary>The runtime value represented by this <see cref="T:System.Dynamic.DynamicMetaObject" />.</summary>
		/// <returns>The runtime value represented by this <see cref="T:System.Dynamic.DynamicMetaObject" />.</returns>
		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x060017C9 RID: 6089 RVA: 0x0004D6FA File Offset: 0x0004B8FA
		public object Value { get; }

		/// <summary>Gets a value indicating whether the <see cref="T:System.Dynamic.DynamicMetaObject" /> has the runtime value.</summary>
		/// <returns>True if the <see cref="T:System.Dynamic.DynamicMetaObject" /> has the runtime value, otherwise false.</returns>
		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x060017CA RID: 6090 RVA: 0x0004D702 File Offset: 0x0004B902
		public bool HasValue { get; }

		/// <summary>Gets the <see cref="T:System.Type" /> of the runtime value or null if the <see cref="T:System.Dynamic.DynamicMetaObject" /> has no value associated with it.</summary>
		/// <returns>The <see cref="T:System.Type" /> of the runtime value or null.</returns>
		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x060017CB RID: 6091 RVA: 0x0004D70C File Offset: 0x0004B90C
		public Type RuntimeType
		{
			get
			{
				if (!this.HasValue)
				{
					return null;
				}
				Type type = this.Expression.Type;
				if (type.IsValueType)
				{
					return type;
				}
				object value = this.Value;
				if (value == null)
				{
					return null;
				}
				return value.GetType();
			}
		}

		/// <summary>Gets the limit type of the <see cref="T:System.Dynamic.DynamicMetaObject" />.</summary>
		/// <returns>
		///   <see cref="P:System.Dynamic.DynamicMetaObject.RuntimeType" /> if runtime value is available, a type of the <see cref="P:System.Dynamic.DynamicMetaObject.Expression" /> otherwise.</returns>
		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x060017CC RID: 6092 RVA: 0x0004D74A File Offset: 0x0004B94A
		public Type LimitType
		{
			get
			{
				return this.RuntimeType ?? this.Expression.Type;
			}
		}

		/// <summary>Performs the binding of the dynamic conversion operation.</summary>
		/// <returns>The new <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		/// <param name="binder">An instance of the <see cref="T:System.Dynamic.ConvertBinder" /> that represents the details of the dynamic operation.</param>
		// Token: 0x060017CD RID: 6093 RVA: 0x0004D761 File Offset: 0x0004B961
		public virtual DynamicMetaObject BindConvert(ConvertBinder binder)
		{
			ContractUtils.RequiresNotNull(binder, "binder");
			return binder.FallbackConvert(this);
		}

		/// <summary>Performs the binding of the dynamic get member operation.</summary>
		/// <returns>The new <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		/// <param name="binder">An instance of the <see cref="T:System.Dynamic.GetMemberBinder" /> that represents the details of the dynamic operation.</param>
		// Token: 0x060017CE RID: 6094 RVA: 0x0004D775 File Offset: 0x0004B975
		public virtual DynamicMetaObject BindGetMember(GetMemberBinder binder)
		{
			ContractUtils.RequiresNotNull(binder, "binder");
			return binder.FallbackGetMember(this);
		}

		/// <summary>Performs the binding of the dynamic set member operation.</summary>
		/// <returns>The new <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		/// <param name="binder">An instance of the <see cref="T:System.Dynamic.SetMemberBinder" /> that represents the details of the dynamic operation.</param>
		/// <param name="value">The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the value for the set member operation.</param>
		// Token: 0x060017CF RID: 6095 RVA: 0x0004D789 File Offset: 0x0004B989
		public virtual DynamicMetaObject BindSetMember(SetMemberBinder binder, DynamicMetaObject value)
		{
			ContractUtils.RequiresNotNull(binder, "binder");
			return binder.FallbackSetMember(this, value);
		}

		/// <summary>Performs the binding of the dynamic delete member operation.</summary>
		/// <returns>The new <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		/// <param name="binder">An instance of the <see cref="T:System.Dynamic.DeleteMemberBinder" /> that represents the details of the dynamic operation.</param>
		// Token: 0x060017D0 RID: 6096 RVA: 0x0004D79E File Offset: 0x0004B99E
		public virtual DynamicMetaObject BindDeleteMember(DeleteMemberBinder binder)
		{
			ContractUtils.RequiresNotNull(binder, "binder");
			return binder.FallbackDeleteMember(this);
		}

		/// <summary>Performs the binding of the dynamic get index operation.</summary>
		/// <returns>The new <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		/// <param name="binder">An instance of the <see cref="T:System.Dynamic.GetIndexBinder" /> that represents the details of the dynamic operation.</param>
		/// <param name="indexes">An array of <see cref="T:System.Dynamic.DynamicMetaObject" /> instances - indexes for the get index operation.</param>
		// Token: 0x060017D1 RID: 6097 RVA: 0x0004D7B2 File Offset: 0x0004B9B2
		public virtual DynamicMetaObject BindGetIndex(GetIndexBinder binder, DynamicMetaObject[] indexes)
		{
			ContractUtils.RequiresNotNull(binder, "binder");
			return binder.FallbackGetIndex(this, indexes);
		}

		/// <summary>Performs the binding of the dynamic set index operation.</summary>
		/// <returns>The new <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		/// <param name="binder">An instance of the <see cref="T:System.Dynamic.SetIndexBinder" /> that represents the details of the dynamic operation.</param>
		/// <param name="indexes">An array of <see cref="T:System.Dynamic.DynamicMetaObject" /> instances - indexes for the set index operation.</param>
		/// <param name="value">The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the value for the set index operation.</param>
		// Token: 0x060017D2 RID: 6098 RVA: 0x0004D7C7 File Offset: 0x0004B9C7
		public virtual DynamicMetaObject BindSetIndex(SetIndexBinder binder, DynamicMetaObject[] indexes, DynamicMetaObject value)
		{
			ContractUtils.RequiresNotNull(binder, "binder");
			return binder.FallbackSetIndex(this, indexes, value);
		}

		/// <summary>Performs the binding of the dynamic delete index operation.</summary>
		/// <returns>The new <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		/// <param name="binder">An instance of the <see cref="T:System.Dynamic.DeleteIndexBinder" /> that represents the details of the dynamic operation.</param>
		/// <param name="indexes">An array of <see cref="T:System.Dynamic.DynamicMetaObject" /> instances - indexes for the delete index operation.</param>
		// Token: 0x060017D3 RID: 6099 RVA: 0x0004D7DD File Offset: 0x0004B9DD
		public virtual DynamicMetaObject BindDeleteIndex(DeleteIndexBinder binder, DynamicMetaObject[] indexes)
		{
			ContractUtils.RequiresNotNull(binder, "binder");
			return binder.FallbackDeleteIndex(this, indexes);
		}

		/// <summary>Performs the binding of the dynamic invoke member operation.</summary>
		/// <returns>The new <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		/// <param name="binder">An instance of the <see cref="T:System.Dynamic.InvokeMemberBinder" /> that represents the details of the dynamic operation.</param>
		/// <param name="args">An array of <see cref="T:System.Dynamic.DynamicMetaObject" /> instances - arguments to the invoke member operation.</param>
		// Token: 0x060017D4 RID: 6100 RVA: 0x0004D7F2 File Offset: 0x0004B9F2
		public virtual DynamicMetaObject BindInvokeMember(InvokeMemberBinder binder, DynamicMetaObject[] args)
		{
			ContractUtils.RequiresNotNull(binder, "binder");
			return binder.FallbackInvokeMember(this, args);
		}

		/// <summary>Performs the binding of the dynamic invoke operation.</summary>
		/// <returns>The new <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		/// <param name="binder">An instance of the <see cref="T:System.Dynamic.InvokeBinder" /> that represents the details of the dynamic operation.</param>
		/// <param name="args">An array of <see cref="T:System.Dynamic.DynamicMetaObject" /> instances - arguments to the invoke operation.</param>
		// Token: 0x060017D5 RID: 6101 RVA: 0x0004D807 File Offset: 0x0004BA07
		public virtual DynamicMetaObject BindInvoke(InvokeBinder binder, DynamicMetaObject[] args)
		{
			ContractUtils.RequiresNotNull(binder, "binder");
			return binder.FallbackInvoke(this, args);
		}

		/// <summary>Performs the binding of the dynamic create instance operation.</summary>
		/// <returns>The new <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		/// <param name="binder">An instance of the <see cref="T:System.Dynamic.CreateInstanceBinder" /> that represents the details of the dynamic operation.</param>
		/// <param name="args">An array of <see cref="T:System.Dynamic.DynamicMetaObject" /> instances - arguments to the create instance operation.</param>
		// Token: 0x060017D6 RID: 6102 RVA: 0x0004D81C File Offset: 0x0004BA1C
		public virtual DynamicMetaObject BindCreateInstance(CreateInstanceBinder binder, DynamicMetaObject[] args)
		{
			ContractUtils.RequiresNotNull(binder, "binder");
			return binder.FallbackCreateInstance(this, args);
		}

		/// <summary>Performs the binding of the dynamic unary operation.</summary>
		/// <returns>The new <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		/// <param name="binder">An instance of the <see cref="T:System.Dynamic.UnaryOperationBinder" /> that represents the details of the dynamic operation.</param>
		// Token: 0x060017D7 RID: 6103 RVA: 0x0004D831 File Offset: 0x0004BA31
		public virtual DynamicMetaObject BindUnaryOperation(UnaryOperationBinder binder)
		{
			ContractUtils.RequiresNotNull(binder, "binder");
			return binder.FallbackUnaryOperation(this);
		}

		/// <summary>Performs the binding of the dynamic binary operation.</summary>
		/// <returns>The new <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		/// <param name="binder">An instance of the <see cref="T:System.Dynamic.BinaryOperationBinder" /> that represents the details of the dynamic operation.</param>
		/// <param name="arg">An instance of the <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the right hand side of the binary operation.</param>
		// Token: 0x060017D8 RID: 6104 RVA: 0x0004D845 File Offset: 0x0004BA45
		public virtual DynamicMetaObject BindBinaryOperation(BinaryOperationBinder binder, DynamicMetaObject arg)
		{
			ContractUtils.RequiresNotNull(binder, "binder");
			return binder.FallbackBinaryOperation(this, arg);
		}

		/// <summary>Returns the enumeration of all dynamic member names.</summary>
		/// <returns>The list of dynamic member names.</returns>
		// Token: 0x060017D9 RID: 6105 RVA: 0x0004D85A File Offset: 0x0004BA5A
		public virtual IEnumerable<string> GetDynamicMemberNames()
		{
			return Array.Empty<string>();
		}

		// Token: 0x060017DA RID: 6106 RVA: 0x0004D864 File Offset: 0x0004BA64
		internal static Expression[] GetExpressions(DynamicMetaObject[] objects)
		{
			ContractUtils.RequiresNotNull(objects, "objects");
			Expression[] array = new Expression[objects.Length];
			for (int i = 0; i < objects.Length; i++)
			{
				DynamicMetaObject dynamicMetaObject = objects[i];
				ContractUtils.RequiresNotNull(dynamicMetaObject, "objects");
				Expression expression = dynamicMetaObject.Expression;
				array[i] = expression;
			}
			return array;
		}

		/// <summary>Creates a meta-object for the specified object.</summary>
		/// <returns>If the given object implements <see cref="T:System.Dynamic.IDynamicMetaObjectProvider" /> and is not a remote object from outside the current AppDomain, returns the object's specific meta-object returned by <see cref="M:System.Dynamic.IDynamicMetaObjectProvider.GetMetaObject(System.Linq.Expressions.Expression)" />. Otherwise a plain new meta-object with no restrictions is created and returned.</returns>
		/// <param name="value">The object to get a meta-object for.</param>
		/// <param name="expression">The expression representing this <see cref="T:System.Dynamic.DynamicMetaObject" /> during the dynamic binding process.</param>
		// Token: 0x060017DB RID: 6107 RVA: 0x0004D8AC File Offset: 0x0004BAAC
		public static DynamicMetaObject Create(object value, Expression expression)
		{
			ContractUtils.RequiresNotNull(expression, "expression");
			IDynamicMetaObjectProvider dynamicMetaObjectProvider = value as IDynamicMetaObjectProvider;
			if (dynamicMetaObjectProvider == null)
			{
				return new DynamicMetaObject(expression, BindingRestrictions.Empty, value);
			}
			DynamicMetaObject metaObject = dynamicMetaObjectProvider.GetMetaObject(expression);
			if (metaObject == null || !metaObject.HasValue || metaObject.Value == null || metaObject.Expression != expression)
			{
				throw Error.InvalidMetaObjectCreated(dynamicMetaObjectProvider.GetType());
			}
			return metaObject;
		}

		/// <summary>Represents an empty array of type <see cref="T:System.Dynamic.DynamicMetaObject" />. This field is read only.</summary>
		// Token: 0x04000AED RID: 2797
		public static readonly DynamicMetaObject[] EmptyMetaObjects = Array.Empty<DynamicMetaObject>();
	}
}
