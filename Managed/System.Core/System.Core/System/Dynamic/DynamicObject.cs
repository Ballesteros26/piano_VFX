using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace System.Dynamic
{
	/// <summary>Provides a base class for specifying dynamic behavior at run time. This class must be inherited from; you cannot instantiate it directly.</summary>
	// Token: 0x02000315 RID: 789
	[Serializable]
	public class DynamicObject : IDynamicMetaObjectProvider
	{
		/// <summary>Enables derived types to initialize a new instance of the <see cref="T:System.Dynamic.DynamicObject" /> type.</summary>
		// Token: 0x060017E7 RID: 6119 RVA: 0x00002320 File Offset: 0x00000520
		protected DynamicObject()
		{
		}

		/// <summary>Provides the implementation for operations that get member values. Classes derived from the <see cref="T:System.Dynamic.DynamicObject" /> class can override this method to specify dynamic behavior for operations such as getting a value for a property.</summary>
		/// <returns>true if the operation is successful; otherwise, false. If this method returns false, the run-time binder of the language determines the behavior. (In most cases, a run-time exception is thrown.)</returns>
		/// <param name="binder">Provides information about the object that called the dynamic operation. The binder.Name property provides the name of the member on which the dynamic operation is performed. For example, for the Console.WriteLine(sampleObject.SampleProperty) statement, where sampleObject is an instance of the class derived from the <see cref="T:System.Dynamic.DynamicObject" /> class, binder.Name returns "SampleProperty". The binder.IgnoreCase property specifies whether the member name is case-sensitive.</param>
		/// <param name="result">The result of the get operation. For example, if the method is called for a property, you can assign the property value to <paramref name="result" />.</param>
		// Token: 0x060017E8 RID: 6120 RVA: 0x0004DBB2 File Offset: 0x0004BDB2
		public virtual bool TryGetMember(GetMemberBinder binder, out object result)
		{
			result = null;
			return false;
		}

		/// <summary>Provides the implementation for operations that set member values. Classes derived from the <see cref="T:System.Dynamic.DynamicObject" /> class can override this method to specify dynamic behavior for operations such as setting a value for a property.</summary>
		/// <returns>true if the operation is successful; otherwise, false. If this method returns false, the run-time binder of the language determines the behavior. (In most cases, a language-specific run-time exception is thrown.)</returns>
		/// <param name="binder">Provides information about the object that called the dynamic operation. The binder.Name property provides the name of the member to which the value is being assigned. For example, for the statement sampleObject.SampleProperty = "Test", where sampleObject is an instance of the class derived from the <see cref="T:System.Dynamic.DynamicObject" /> class, binder.Name returns "SampleProperty". The binder.IgnoreCase property specifies whether the member name is case-sensitive.</param>
		/// <param name="value">The value to set to the member. For example, for sampleObject.SampleProperty = "Test", where sampleObject is an instance of the class derived from the <see cref="T:System.Dynamic.DynamicObject" /> class, the <paramref name="value" /> is "Test".</param>
		// Token: 0x060017E9 RID: 6121 RVA: 0x00002285 File Offset: 0x00000485
		public virtual bool TrySetMember(SetMemberBinder binder, object value)
		{
			return false;
		}

		/// <summary>Provides the implementation for operations that delete an object member. This method is not intended for use in C# or Visual Basic.</summary>
		/// <returns>true if the operation is successful; otherwise, false. If this method returns false, the run-time binder of the language determines the behavior. (In most cases, a language-specific run-time exception is thrown.)</returns>
		/// <param name="binder">Provides information about the deletion.</param>
		// Token: 0x060017EA RID: 6122 RVA: 0x00002285 File Offset: 0x00000485
		public virtual bool TryDeleteMember(DeleteMemberBinder binder)
		{
			return false;
		}

		/// <summary>Provides the implementation for operations that invoke a member. Classes derived from the <see cref="T:System.Dynamic.DynamicObject" /> class can override this method to specify dynamic behavior for operations such as calling a method.</summary>
		/// <returns>true if the operation is successful; otherwise, false. If this method returns false, the run-time binder of the language determines the behavior. (In most cases, a language-specific run-time exception is thrown.)</returns>
		/// <param name="binder">Provides information about the dynamic operation. The binder.Name property provides the name of the member on which the dynamic operation is performed. For example, for the statement sampleObject.SampleMethod(100), where sampleObject is an instance of the class derived from the <see cref="T:System.Dynamic.DynamicObject" /> class, binder.Name returns "SampleMethod". The binder.IgnoreCase property specifies whether the member name is case-sensitive.</param>
		/// <param name="args">The arguments that are passed to the object member during the invoke operation. For example, for the statement sampleObject.SampleMethod(100), where sampleObject is derived from the <see cref="T:System.Dynamic.DynamicObject" /> class, <paramref name="args[0]" /> is equal to 100.</param>
		/// <param name="result">The result of the member invocation.</param>
		// Token: 0x060017EB RID: 6123 RVA: 0x0004DBB8 File Offset: 0x0004BDB8
		public virtual bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
		{
			result = null;
			return false;
		}

		/// <summary>Provides implementation for type conversion operations. Classes derived from the <see cref="T:System.Dynamic.DynamicObject" /> class can override this method to specify dynamic behavior for operations that convert an object from one type to another.</summary>
		/// <returns>true if the operation is successful; otherwise, false. If this method returns false, the run-time binder of the language determines the behavior. (In most cases, a language-specific run-time exception is thrown.)</returns>
		/// <param name="binder">Provides information about the conversion operation. The binder.Type property provides the type to which the object must be converted. For example, for the statement (String)sampleObject in C# (CType(sampleObject, Type) in Visual Basic), where sampleObject is an instance of the class derived from the <see cref="T:System.Dynamic.DynamicObject" /> class, binder.Type returns the <see cref="T:System.String" /> type. The binder.Explicit property provides information about the kind of conversion that occurs. It returns true for explicit conversion and false for implicit conversion.</param>
		/// <param name="result">The result of the type conversion operation.</param>
		// Token: 0x060017EC RID: 6124 RVA: 0x0004DBB2 File Offset: 0x0004BDB2
		public virtual bool TryConvert(ConvertBinder binder, out object result)
		{
			result = null;
			return false;
		}

		/// <summary>Provides the implementation for operations that initialize a new instance of a dynamic object. This method is not intended for use in C# or Visual Basic.</summary>
		/// <returns>true if the operation is successful; otherwise, false. If this method returns false, the run-time binder of the language determines the behavior. (In most cases, a language-specific run-time exception is thrown.)</returns>
		/// <param name="binder">Provides information about the initialization operation.</param>
		/// <param name="args">The arguments that are passed to the object during initialization. For example, for the new SampleType(100) operation, where SampleType is the type derived from the <see cref="T:System.Dynamic.DynamicObject" /> class, <paramref name="args[0]" /> is equal to 100.</param>
		/// <param name="result">The result of the initialization.</param>
		// Token: 0x060017ED RID: 6125 RVA: 0x0004DBB8 File Offset: 0x0004BDB8
		public virtual bool TryCreateInstance(CreateInstanceBinder binder, object[] args, out object result)
		{
			result = null;
			return false;
		}

		/// <summary>Provides the implementation for operations that invoke an object. Classes derived from the <see cref="T:System.Dynamic.DynamicObject" /> class can override this method to specify dynamic behavior for operations such as invoking an object or a delegate.</summary>
		/// <returns>true if the operation is successful; otherwise, false. If this method returns false, the run-time binder of the language determines the behavior. (In most cases, a language-specific run-time exception is thrown.</returns>
		/// <param name="binder">Provides information about the invoke operation.</param>
		/// <param name="args">The arguments that are passed to the object during the invoke operation. For example, for the sampleObject(100) operation, where sampleObject is derived from the <see cref="T:System.Dynamic.DynamicObject" /> class, <paramref name="args[0]" /> is equal to 100.</param>
		/// <param name="result">The result of the object invocation.</param>
		// Token: 0x060017EE RID: 6126 RVA: 0x0004DBB8 File Offset: 0x0004BDB8
		public virtual bool TryInvoke(InvokeBinder binder, object[] args, out object result)
		{
			result = null;
			return false;
		}

		/// <summary>Provides implementation for binary operations. Classes derived from the <see cref="T:System.Dynamic.DynamicObject" /> class can override this method to specify dynamic behavior for operations such as addition and multiplication.</summary>
		/// <returns>true if the operation is successful; otherwise, false. If this method returns false, the run-time binder of the language determines the behavior. (In most cases, a language-specific run-time exception is thrown.)</returns>
		/// <param name="binder">Provides information about the binary operation. The binder.Operation property returns an <see cref="T:System.Linq.Expressions.ExpressionType" /> object. For example, for the sum = first + second statement, where first and second are derived from the DynamicObject class, binder.Operation returns ExpressionType.Add.</param>
		/// <param name="arg">The right operand for the binary operation. For example, for the sum = first + second statement, where first and second are derived from the DynamicObject class, <paramref name="arg" /> is equal to second.</param>
		/// <param name="result">The result of the binary operation.</param>
		// Token: 0x060017EF RID: 6127 RVA: 0x0004DBB8 File Offset: 0x0004BDB8
		public virtual bool TryBinaryOperation(BinaryOperationBinder binder, object arg, out object result)
		{
			result = null;
			return false;
		}

		/// <summary>Provides implementation for unary operations. Classes derived from the <see cref="T:System.Dynamic.DynamicObject" /> class can override this method to specify dynamic behavior for operations such as negation, increment, or decrement.</summary>
		/// <returns>true if the operation is successful; otherwise, false. If this method returns false, the run-time binder of the language determines the behavior. (In most cases, a language-specific run-time exception is thrown.)</returns>
		/// <param name="binder">Provides information about the unary operation. The binder.Operation property returns an <see cref="T:System.Linq.Expressions.ExpressionType" /> object. For example, for the negativeNumber = -number statement, where number is derived from the DynamicObject class, binder.Operation returns "Negate".</param>
		/// <param name="result">The result of the unary operation.</param>
		// Token: 0x060017F0 RID: 6128 RVA: 0x0004DBB2 File Offset: 0x0004BDB2
		public virtual bool TryUnaryOperation(UnaryOperationBinder binder, out object result)
		{
			result = null;
			return false;
		}

		/// <summary>Provides the implementation for operations that get a value by index. Classes derived from the <see cref="T:System.Dynamic.DynamicObject" /> class can override this method to specify dynamic behavior for indexing operations.</summary>
		/// <returns>true if the operation is successful; otherwise, false. If this method returns false, the run-time binder of the language determines the behavior. (In most cases, a run-time exception is thrown.)</returns>
		/// <param name="binder">Provides information about the operation. </param>
		/// <param name="indexes">The indexes that are used in the operation. For example, for the sampleObject[3] operation in C# (sampleObject(3) in Visual Basic), where sampleObject is derived from the DynamicObject class, <paramref name="indexes[0]" /> is equal to 3.</param>
		/// <param name="result">The result of the index operation.</param>
		// Token: 0x060017F1 RID: 6129 RVA: 0x0004DBB8 File Offset: 0x0004BDB8
		public virtual bool TryGetIndex(GetIndexBinder binder, object[] indexes, out object result)
		{
			result = null;
			return false;
		}

		/// <summary>Provides the implementation for operations that set a value by index. Classes derived from the <see cref="T:System.Dynamic.DynamicObject" /> class can override this method to specify dynamic behavior for operations that access objects by a specified index.</summary>
		/// <returns>true if the operation is successful; otherwise, false. If this method returns false, the run-time binder of the language determines the behavior. (In most cases, a language-specific run-time exception is thrown.</returns>
		/// <param name="binder">Provides information about the operation. </param>
		/// <param name="indexes">The indexes that are used in the operation. For example, for the sampleObject[3] = 10 operation in C# (sampleObject(3) = 10 in Visual Basic), where sampleObject is derived from the <see cref="T:System.Dynamic.DynamicObject" /> class, <paramref name="indexes[0]" /> is equal to 3.</param>
		/// <param name="value">The value to set to the object that has the specified index. For example, for the sampleObject[3] = 10 operation in C# (sampleObject(3) = 10 in Visual Basic), where sampleObject is derived from the <see cref="T:System.Dynamic.DynamicObject" /> class, <paramref name="value" /> is equal to 10.</param>
		// Token: 0x060017F2 RID: 6130 RVA: 0x00002285 File Offset: 0x00000485
		public virtual bool TrySetIndex(SetIndexBinder binder, object[] indexes, object value)
		{
			return false;
		}

		/// <summary>Provides the implementation for operations that delete an object by index. This method is not intended for use in C# or Visual Basic.</summary>
		/// <returns>true if the operation is successful; otherwise, false. If this method returns false, the run-time binder of the language determines the behavior. (In most cases, a language-specific run-time exception is thrown.)</returns>
		/// <param name="binder">Provides information about the deletion.</param>
		/// <param name="indexes">The indexes to be deleted.</param>
		// Token: 0x060017F3 RID: 6131 RVA: 0x00002285 File Offset: 0x00000485
		public virtual bool TryDeleteIndex(DeleteIndexBinder binder, object[] indexes)
		{
			return false;
		}

		/// <summary>Returns the enumeration of all dynamic member names. </summary>
		/// <returns>A sequence that contains dynamic member names.</returns>
		// Token: 0x060017F4 RID: 6132 RVA: 0x0004D85A File Offset: 0x0004BA5A
		public virtual IEnumerable<string> GetDynamicMemberNames()
		{
			return Array.Empty<string>();
		}

		/// <summary>Provides a <see cref="T:System.Dynamic.DynamicMetaObject" /> that dispatches to the dynamic virtual methods. The object can be encapsulated inside another <see cref="T:System.Dynamic.DynamicMetaObject" /> to provide custom behavior for individual actions. This method supports the Dynamic Language Runtime infrastructure for language implementers and it is not intended to be used directly from your code.</summary>
		/// <returns>An object of the <see cref="T:System.Dynamic.DynamicMetaObject" /> type.</returns>
		/// <param name="parameter">The expression that represents <see cref="T:System.Dynamic.DynamicMetaObject" /> to dispatch to the dynamic virtual methods.</param>
		// Token: 0x060017F5 RID: 6133 RVA: 0x0004DBBE File Offset: 0x0004BDBE
		public virtual DynamicMetaObject GetMetaObject(Expression parameter)
		{
			return new DynamicObject.MetaDynamic(parameter, this);
		}

		// Token: 0x02000316 RID: 790
		private sealed class MetaDynamic : DynamicMetaObject
		{
			// Token: 0x060017F6 RID: 6134 RVA: 0x0004DBC7 File Offset: 0x0004BDC7
			internal MetaDynamic(Expression expression, DynamicObject value)
				: base(expression, BindingRestrictions.Empty, value)
			{
			}

			// Token: 0x060017F7 RID: 6135 RVA: 0x0004DBD6 File Offset: 0x0004BDD6
			public override IEnumerable<string> GetDynamicMemberNames()
			{
				return this.Value.GetDynamicMemberNames();
			}

			// Token: 0x060017F8 RID: 6136 RVA: 0x0004DBE4 File Offset: 0x0004BDE4
			public override DynamicMetaObject BindGetMember(GetMemberBinder binder)
			{
				if (this.IsOverridden(CachedReflectionInfo.DynamicObject_TryGetMember))
				{
					return this.CallMethodWithResult<GetMemberBinder>(CachedReflectionInfo.DynamicObject_TryGetMember, binder, DynamicObject.MetaDynamic.s_noArgs, (DynamicObject.MetaDynamic @this, GetMemberBinder b, DynamicMetaObject e) => b.FallbackGetMember(@this, e));
				}
				return base.BindGetMember(binder);
			}

			// Token: 0x060017F9 RID: 6137 RVA: 0x0004DC38 File Offset: 0x0004BE38
			public override DynamicMetaObject BindSetMember(SetMemberBinder binder, DynamicMetaObject value)
			{
				if (this.IsOverridden(CachedReflectionInfo.DynamicObject_TrySetMember))
				{
					DynamicMetaObject localValue = value;
					return this.CallMethodReturnLast<SetMemberBinder>(CachedReflectionInfo.DynamicObject_TrySetMember, binder, DynamicObject.MetaDynamic.s_noArgs, value.Expression, (DynamicObject.MetaDynamic @this, SetMemberBinder b, DynamicMetaObject e) => b.FallbackSetMember(@this, localValue, e));
				}
				return base.BindSetMember(binder, value);
			}

			// Token: 0x060017FA RID: 6138 RVA: 0x0004DC8C File Offset: 0x0004BE8C
			public override DynamicMetaObject BindDeleteMember(DeleteMemberBinder binder)
			{
				if (this.IsOverridden(CachedReflectionInfo.DynamicObject_TryDeleteMember))
				{
					return this.CallMethodNoResult<DeleteMemberBinder>(CachedReflectionInfo.DynamicObject_TryDeleteMember, binder, DynamicObject.MetaDynamic.s_noArgs, (DynamicObject.MetaDynamic @this, DeleteMemberBinder b, DynamicMetaObject e) => b.FallbackDeleteMember(@this, e));
				}
				return base.BindDeleteMember(binder);
			}

			// Token: 0x060017FB RID: 6139 RVA: 0x0004DCE0 File Offset: 0x0004BEE0
			public override DynamicMetaObject BindConvert(ConvertBinder binder)
			{
				if (this.IsOverridden(CachedReflectionInfo.DynamicObject_TryConvert))
				{
					return this.CallMethodWithResult<ConvertBinder>(CachedReflectionInfo.DynamicObject_TryConvert, binder, DynamicObject.MetaDynamic.s_noArgs, (DynamicObject.MetaDynamic @this, ConvertBinder b, DynamicMetaObject e) => b.FallbackConvert(@this, e));
				}
				return base.BindConvert(binder);
			}

			// Token: 0x060017FC RID: 6140 RVA: 0x0004DD34 File Offset: 0x0004BF34
			public override DynamicMetaObject BindInvokeMember(InvokeMemberBinder binder, DynamicMetaObject[] args)
			{
				DynamicMetaObject dynamicMetaObject = this.BuildCallMethodWithResult<InvokeMemberBinder>(CachedReflectionInfo.DynamicObject_TryInvokeMember, binder, DynamicMetaObject.GetExpressions(args), this.BuildCallMethodWithResult<GetMemberBinder>(CachedReflectionInfo.DynamicObject_TryGetMember, new DynamicObject.MetaDynamic.GetBinderAdapter(binder), DynamicObject.MetaDynamic.s_noArgs, binder.FallbackInvokeMember(this, args, null), (DynamicObject.MetaDynamic @this, GetMemberBinder ignored, DynamicMetaObject e) => binder.FallbackInvoke(e, args, null)), null);
				return binder.FallbackInvokeMember(this, args, dynamicMetaObject);
			}

			// Token: 0x060017FD RID: 6141 RVA: 0x0004DDC0 File Offset: 0x0004BFC0
			public override DynamicMetaObject BindCreateInstance(CreateInstanceBinder binder, DynamicMetaObject[] args)
			{
				if (this.IsOverridden(CachedReflectionInfo.DynamicObject_TryCreateInstance))
				{
					DynamicMetaObject[] localArgs = args;
					return this.CallMethodWithResult<CreateInstanceBinder>(CachedReflectionInfo.DynamicObject_TryCreateInstance, binder, DynamicMetaObject.GetExpressions(args), (DynamicObject.MetaDynamic @this, CreateInstanceBinder b, DynamicMetaObject e) => b.FallbackCreateInstance(@this, localArgs, e));
				}
				return base.BindCreateInstance(binder, args);
			}

			// Token: 0x060017FE RID: 6142 RVA: 0x0004DE10 File Offset: 0x0004C010
			public override DynamicMetaObject BindInvoke(InvokeBinder binder, DynamicMetaObject[] args)
			{
				if (this.IsOverridden(CachedReflectionInfo.DynamicObject_TryInvoke))
				{
					DynamicMetaObject[] localArgs = args;
					return this.CallMethodWithResult<InvokeBinder>(CachedReflectionInfo.DynamicObject_TryInvoke, binder, DynamicMetaObject.GetExpressions(args), (DynamicObject.MetaDynamic @this, InvokeBinder b, DynamicMetaObject e) => b.FallbackInvoke(@this, localArgs, e));
				}
				return base.BindInvoke(binder, args);
			}

			// Token: 0x060017FF RID: 6143 RVA: 0x0004DE60 File Offset: 0x0004C060
			public override DynamicMetaObject BindBinaryOperation(BinaryOperationBinder binder, DynamicMetaObject arg)
			{
				if (this.IsOverridden(CachedReflectionInfo.DynamicObject_TryBinaryOperation))
				{
					DynamicMetaObject localArg = arg;
					return this.CallMethodWithResult<BinaryOperationBinder>(CachedReflectionInfo.DynamicObject_TryBinaryOperation, binder, new Expression[] { arg.Expression }, (DynamicObject.MetaDynamic @this, BinaryOperationBinder b, DynamicMetaObject e) => b.FallbackBinaryOperation(@this, localArg, e));
				}
				return base.BindBinaryOperation(binder, arg);
			}

			// Token: 0x06001800 RID: 6144 RVA: 0x0004DEB8 File Offset: 0x0004C0B8
			public override DynamicMetaObject BindUnaryOperation(UnaryOperationBinder binder)
			{
				if (this.IsOverridden(CachedReflectionInfo.DynamicObject_TryUnaryOperation))
				{
					return this.CallMethodWithResult<UnaryOperationBinder>(CachedReflectionInfo.DynamicObject_TryUnaryOperation, binder, DynamicObject.MetaDynamic.s_noArgs, (DynamicObject.MetaDynamic @this, UnaryOperationBinder b, DynamicMetaObject e) => b.FallbackUnaryOperation(@this, e));
				}
				return base.BindUnaryOperation(binder);
			}

			// Token: 0x06001801 RID: 6145 RVA: 0x0004DF0C File Offset: 0x0004C10C
			public override DynamicMetaObject BindGetIndex(GetIndexBinder binder, DynamicMetaObject[] indexes)
			{
				if (this.IsOverridden(CachedReflectionInfo.DynamicObject_TryGetIndex))
				{
					DynamicMetaObject[] localIndexes = indexes;
					return this.CallMethodWithResult<GetIndexBinder>(CachedReflectionInfo.DynamicObject_TryGetIndex, binder, DynamicMetaObject.GetExpressions(indexes), (DynamicObject.MetaDynamic @this, GetIndexBinder b, DynamicMetaObject e) => b.FallbackGetIndex(@this, localIndexes, e));
				}
				return base.BindGetIndex(binder, indexes);
			}

			// Token: 0x06001802 RID: 6146 RVA: 0x0004DF5C File Offset: 0x0004C15C
			public override DynamicMetaObject BindSetIndex(SetIndexBinder binder, DynamicMetaObject[] indexes, DynamicMetaObject value)
			{
				if (this.IsOverridden(CachedReflectionInfo.DynamicObject_TrySetIndex))
				{
					DynamicMetaObject[] localIndexes = indexes;
					DynamicMetaObject localValue = value;
					return this.CallMethodReturnLast<SetIndexBinder>(CachedReflectionInfo.DynamicObject_TrySetIndex, binder, DynamicMetaObject.GetExpressions(indexes), value.Expression, (DynamicObject.MetaDynamic @this, SetIndexBinder b, DynamicMetaObject e) => b.FallbackSetIndex(@this, localIndexes, localValue, e));
				}
				return base.BindSetIndex(binder, indexes, value);
			}

			// Token: 0x06001803 RID: 6147 RVA: 0x0004DFB8 File Offset: 0x0004C1B8
			public override DynamicMetaObject BindDeleteIndex(DeleteIndexBinder binder, DynamicMetaObject[] indexes)
			{
				if (this.IsOverridden(CachedReflectionInfo.DynamicObject_TryDeleteIndex))
				{
					DynamicMetaObject[] localIndexes = indexes;
					return this.CallMethodNoResult<DeleteIndexBinder>(CachedReflectionInfo.DynamicObject_TryDeleteIndex, binder, DynamicMetaObject.GetExpressions(indexes), (DynamicObject.MetaDynamic @this, DeleteIndexBinder b, DynamicMetaObject e) => b.FallbackDeleteIndex(@this, localIndexes, e));
				}
				return base.BindDeleteIndex(binder, indexes);
			}

			// Token: 0x06001804 RID: 6148 RVA: 0x0004E008 File Offset: 0x0004C208
			private static ReadOnlyCollection<Expression> GetConvertedArgs(params Expression[] args)
			{
				Expression[] array = new Expression[args.Length];
				for (int i = 0; i < args.Length; i++)
				{
					array[i] = Expression.Convert(args[i], typeof(object));
				}
				return new TrueReadOnlyCollection<Expression>(array);
			}

			// Token: 0x06001805 RID: 6149 RVA: 0x0004E048 File Offset: 0x0004C248
			private static Expression ReferenceArgAssign(Expression callArgs, Expression[] args)
			{
				ReadOnlyCollectionBuilder<Expression> readOnlyCollectionBuilder = null;
				for (int i = 0; i < args.Length; i++)
				{
					ParameterExpression parameterExpression = args[i] as ParameterExpression;
					ContractUtils.Requires(parameterExpression != null, "args");
					if (parameterExpression.IsByRef)
					{
						if (readOnlyCollectionBuilder == null)
						{
							readOnlyCollectionBuilder = new ReadOnlyCollectionBuilder<Expression>();
						}
						readOnlyCollectionBuilder.Add(Expression.Assign(parameterExpression, Expression.Convert(Expression.ArrayIndex(callArgs, Utils.Constant(i)), parameterExpression.Type)));
					}
				}
				if (readOnlyCollectionBuilder != null)
				{
					return Expression.Block(readOnlyCollectionBuilder);
				}
				return Utils.Empty;
			}

			// Token: 0x06001806 RID: 6150 RVA: 0x0004E0C0 File Offset: 0x0004C2C0
			private static Expression[] BuildCallArgs<TBinder>(TBinder binder, Expression[] parameters, Expression arg0, Expression arg1) where TBinder : DynamicMetaObjectBinder
			{
				if (parameters != DynamicObject.MetaDynamic.s_noArgs)
				{
					if (arg1 == null)
					{
						return new Expression[]
						{
							DynamicObject.MetaDynamic.Constant<TBinder>(binder),
							arg0
						};
					}
					return new Expression[]
					{
						DynamicObject.MetaDynamic.Constant<TBinder>(binder),
						arg0,
						arg1
					};
				}
				else
				{
					if (arg1 == null)
					{
						return new Expression[] { DynamicObject.MetaDynamic.Constant<TBinder>(binder) };
					}
					return new Expression[]
					{
						DynamicObject.MetaDynamic.Constant<TBinder>(binder),
						arg1
					};
				}
			}

			// Token: 0x06001807 RID: 6151 RVA: 0x0004E12A File Offset: 0x0004C32A
			private static ConstantExpression Constant<TBinder>(TBinder binder)
			{
				return Expression.Constant(binder, typeof(TBinder));
			}

			// Token: 0x06001808 RID: 6152 RVA: 0x0004E141 File Offset: 0x0004C341
			private DynamicMetaObject CallMethodWithResult<TBinder>(MethodInfo method, TBinder binder, Expression[] args, DynamicObject.MetaDynamic.Fallback<TBinder> fallback) where TBinder : DynamicMetaObjectBinder
			{
				return this.CallMethodWithResult<TBinder>(method, binder, args, fallback, null);
			}

			// Token: 0x06001809 RID: 6153 RVA: 0x0004E150 File Offset: 0x0004C350
			private DynamicMetaObject CallMethodWithResult<TBinder>(MethodInfo method, TBinder binder, Expression[] args, DynamicObject.MetaDynamic.Fallback<TBinder> fallback, DynamicObject.MetaDynamic.Fallback<TBinder> fallbackInvoke) where TBinder : DynamicMetaObjectBinder
			{
				DynamicMetaObject dynamicMetaObject = fallback(this, binder, null);
				DynamicMetaObject dynamicMetaObject2 = this.BuildCallMethodWithResult<TBinder>(method, binder, args, dynamicMetaObject, fallbackInvoke);
				return fallback(this, binder, dynamicMetaObject2);
			}

			// Token: 0x0600180A RID: 6154 RVA: 0x0004E180 File Offset: 0x0004C380
			private DynamicMetaObject BuildCallMethodWithResult<TBinder>(MethodInfo method, TBinder binder, Expression[] args, DynamicMetaObject fallbackResult, DynamicObject.MetaDynamic.Fallback<TBinder> fallbackInvoke) where TBinder : DynamicMetaObjectBinder
			{
				if (!this.IsOverridden(method))
				{
					return fallbackResult;
				}
				ParameterExpression parameterExpression = Expression.Parameter(typeof(object), null);
				ParameterExpression parameterExpression2 = ((method != CachedReflectionInfo.DynamicObject_TryBinaryOperation) ? Expression.Parameter(typeof(object[]), null) : Expression.Parameter(typeof(object), null));
				ReadOnlyCollection<Expression> convertedArgs = DynamicObject.MetaDynamic.GetConvertedArgs(args);
				DynamicMetaObject dynamicMetaObject = new DynamicMetaObject(parameterExpression, BindingRestrictions.Empty);
				if (binder.ReturnType != typeof(object))
				{
					UnaryExpression unaryExpression = Expression.Convert(dynamicMetaObject.Expression, binder.ReturnType);
					string text = Strings.DynamicObjectResultNotAssignable("{0}", this.Value.GetType(), binder.GetType(), binder.ReturnType);
					Expression expression;
					if (binder.ReturnType.IsValueType && Nullable.GetUnderlyingType(binder.ReturnType) == null)
					{
						expression = Expression.TypeIs(dynamicMetaObject.Expression, binder.ReturnType);
					}
					else
					{
						expression = Expression.OrElse(Expression.Equal(dynamicMetaObject.Expression, Utils.Null), Expression.TypeIs(dynamicMetaObject.Expression, binder.ReturnType));
					}
					dynamicMetaObject = new DynamicMetaObject(Expression.Condition(expression, unaryExpression, Expression.Throw(Expression.New(CachedReflectionInfo.InvalidCastException_Ctor_String, new TrueReadOnlyCollection<Expression>(new Expression[] { Expression.Call(CachedReflectionInfo.String_Format_String_ObjectArray, Expression.Constant(text), Expression.NewArrayInit(typeof(object), new TrueReadOnlyCollection<Expression>(new Expression[] { Expression.Condition(Expression.Equal(dynamicMetaObject.Expression, Utils.Null), Expression.Constant("null"), Expression.Call(dynamicMetaObject.Expression, CachedReflectionInfo.Object_GetType), typeof(object)) }))) })), binder.ReturnType), binder.ReturnType), dynamicMetaObject.Restrictions);
				}
				if (fallbackInvoke != null)
				{
					dynamicMetaObject = fallbackInvoke(this, binder, dynamicMetaObject);
				}
				return new DynamicMetaObject(Expression.Block(new TrueReadOnlyCollection<ParameterExpression>(new ParameterExpression[] { parameterExpression, parameterExpression2 }), new TrueReadOnlyCollection<Expression>(new Expression[]
				{
					(method != CachedReflectionInfo.DynamicObject_TryBinaryOperation) ? Expression.Assign(parameterExpression2, Expression.NewArrayInit(typeof(object), convertedArgs)) : Expression.Assign(parameterExpression2, convertedArgs[0]),
					Expression.Condition(Expression.Call(this.GetLimitedSelf(), method, DynamicObject.MetaDynamic.BuildCallArgs<TBinder>(binder, args, parameterExpression2, parameterExpression)), Expression.Block((method != CachedReflectionInfo.DynamicObject_TryBinaryOperation) ? DynamicObject.MetaDynamic.ReferenceArgAssign(parameterExpression2, args) : Utils.Empty, dynamicMetaObject.Expression), fallbackResult.Expression, binder.ReturnType)
				})), this.GetRestrictions().Merge(dynamicMetaObject.Restrictions).Merge(fallbackResult.Restrictions));
			}

			// Token: 0x0600180B RID: 6155 RVA: 0x0004E450 File Offset: 0x0004C650
			private DynamicMetaObject CallMethodReturnLast<TBinder>(MethodInfo method, TBinder binder, Expression[] args, Expression value, DynamicObject.MetaDynamic.Fallback<TBinder> fallback) where TBinder : DynamicMetaObjectBinder
			{
				DynamicMetaObject dynamicMetaObject = fallback(this, binder, null);
				ParameterExpression parameterExpression = Expression.Parameter(typeof(object), null);
				ParameterExpression parameterExpression2 = Expression.Parameter(typeof(object[]), null);
				ReadOnlyCollection<Expression> convertedArgs = DynamicObject.MetaDynamic.GetConvertedArgs(args);
				DynamicMetaObject dynamicMetaObject2 = new DynamicMetaObject(Expression.Block(new TrueReadOnlyCollection<ParameterExpression>(new ParameterExpression[] { parameterExpression, parameterExpression2 }), new TrueReadOnlyCollection<Expression>(new Expression[]
				{
					Expression.Assign(parameterExpression2, Expression.NewArrayInit(typeof(object), convertedArgs)),
					Expression.Condition(Expression.Call(this.GetLimitedSelf(), method, DynamicObject.MetaDynamic.BuildCallArgs<TBinder>(binder, args, parameterExpression2, Expression.Assign(parameterExpression, Expression.Convert(value, typeof(object))))), Expression.Block(DynamicObject.MetaDynamic.ReferenceArgAssign(parameterExpression2, args), parameterExpression), dynamicMetaObject.Expression, typeof(object))
				})), this.GetRestrictions().Merge(dynamicMetaObject.Restrictions));
				return fallback(this, binder, dynamicMetaObject2);
			}

			// Token: 0x0600180C RID: 6156 RVA: 0x0004E540 File Offset: 0x0004C740
			private DynamicMetaObject CallMethodNoResult<TBinder>(MethodInfo method, TBinder binder, Expression[] args, DynamicObject.MetaDynamic.Fallback<TBinder> fallback) where TBinder : DynamicMetaObjectBinder
			{
				DynamicMetaObject dynamicMetaObject = fallback(this, binder, null);
				ParameterExpression parameterExpression = Expression.Parameter(typeof(object[]), null);
				ReadOnlyCollection<Expression> convertedArgs = DynamicObject.MetaDynamic.GetConvertedArgs(args);
				DynamicMetaObject dynamicMetaObject2 = new DynamicMetaObject(Expression.Block(new TrueReadOnlyCollection<ParameterExpression>(new ParameterExpression[] { parameterExpression }), new TrueReadOnlyCollection<Expression>(new Expression[]
				{
					Expression.Assign(parameterExpression, Expression.NewArrayInit(typeof(object), convertedArgs)),
					Expression.Condition(Expression.Call(this.GetLimitedSelf(), method, DynamicObject.MetaDynamic.BuildCallArgs<TBinder>(binder, args, parameterExpression, null)), Expression.Block(DynamicObject.MetaDynamic.ReferenceArgAssign(parameterExpression, args), Utils.Empty), dynamicMetaObject.Expression, typeof(void))
				})), this.GetRestrictions().Merge(dynamicMetaObject.Restrictions));
				return fallback(this, binder, dynamicMetaObject2);
			}

			// Token: 0x0600180D RID: 6157 RVA: 0x0004E608 File Offset: 0x0004C808
			private bool IsOverridden(MethodInfo method)
			{
				foreach (MethodInfo methodInfo in this.Value.GetType().GetMember(method.Name, MemberTypes.Method, BindingFlags.Instance | BindingFlags.Public))
				{
					if (methodInfo.DeclaringType != typeof(DynamicObject) && methodInfo.GetBaseDefinition() == method)
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x0600180E RID: 6158 RVA: 0x0004E66E File Offset: 0x0004C86E
			private BindingRestrictions GetRestrictions()
			{
				return BindingRestrictions.GetTypeRestriction(this);
			}

			// Token: 0x0600180F RID: 6159 RVA: 0x0004E676 File Offset: 0x0004C876
			private Expression GetLimitedSelf()
			{
				if (TypeUtils.AreEquivalent(base.Expression.Type, typeof(DynamicObject)))
				{
					return base.Expression;
				}
				return Expression.Convert(base.Expression, typeof(DynamicObject));
			}

			// Token: 0x1700043F RID: 1087
			// (get) Token: 0x06001810 RID: 6160 RVA: 0x0004E6B0 File Offset: 0x0004C8B0
			private new DynamicObject Value
			{
				get
				{
					return (DynamicObject)base.Value;
				}
			}

			// Token: 0x04000AF2 RID: 2802
			private static readonly Expression[] s_noArgs = new Expression[0];

			// Token: 0x02000317 RID: 791
			// (Invoke) Token: 0x06001813 RID: 6163
			private delegate DynamicMetaObject Fallback<TBinder>(DynamicObject.MetaDynamic @this, TBinder binder, DynamicMetaObject errorSuggestion);

			// Token: 0x02000318 RID: 792
			private sealed class GetBinderAdapter : GetMemberBinder
			{
				// Token: 0x06001816 RID: 6166 RVA: 0x0004E6CA File Offset: 0x0004C8CA
				internal GetBinderAdapter(InvokeMemberBinder binder)
					: base(binder.Name, binder.IgnoreCase)
				{
				}

				// Token: 0x06001817 RID: 6167 RVA: 0x00003CCF File Offset: 0x00001ECF
				public override DynamicMetaObject FallbackGetMember(DynamicMetaObject target, DynamicMetaObject errorSuggestion)
				{
					throw new NotSupportedException();
				}
			}
		}
	}
}
