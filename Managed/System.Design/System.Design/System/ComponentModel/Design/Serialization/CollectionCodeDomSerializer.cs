using System;
using System.CodeDom;
using System.Collections;
using System.Reflection;

namespace System.ComponentModel.Design.Serialization
{
	/// <summary>Serializes collections.</summary>
	// Token: 0x02000150 RID: 336
	public class CollectionCodeDomSerializer : CodeDomSerializer
	{
		/// <summary>Verifies serialization support by the <paramref name="method" />.</summary>
		/// <returns>true if the <paramref name="method" /> supports serialization; otherwise, false.</returns>
		/// <param name="method">The <see cref="T:System.Reflection.MethodInfo" /> to check for serialization attributes.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="method" /> is null.</exception>
		// Token: 0x06000A35 RID: 2613 RVA: 0x000023D8 File Offset: 0x000005D8
		protected bool MethodSupportsSerialization(MethodInfo method)
		{
			return true;
		}

		/// <summary>Serializes the given collection into a CodeDOM object.</summary>
		/// <returns>A CodeDOM object representing <paramref name="value" />.</returns>
		/// <param name="manager">The <see cref="T:System.ComponentModel.Design.Serialization.IDesignerSerializationManager" /> to use during serialization.</param>
		/// <param name="value">The object to serialize.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="manager" /> or <paramref name="value" /> is null.</exception>
		// Token: 0x06000A36 RID: 2614 RVA: 0x000147A0 File Offset: 0x000129A0
		public override object Serialize(IDesignerSerializationManager manager, object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			ICollection collection = value as ICollection;
			if (collection == null)
			{
				throw new ArgumentException("originalCollection is not an ICollection");
			}
			CodeExpression codeExpression = null;
			ExpressionContext expressionContext = manager.Context[typeof(ExpressionContext)] as ExpressionContext;
			RootContext rootContext = manager.Context[typeof(RootContext)] as RootContext;
			if (expressionContext != null && expressionContext.PresetValue == value)
			{
				codeExpression = expressionContext.Expression;
			}
			else if (rootContext != null)
			{
				codeExpression = rootContext.Expression;
			}
			ArrayList arrayList = new ArrayList();
			foreach (object obj in collection)
			{
				arrayList.Add(obj);
			}
			return this.SerializeCollection(manager, codeExpression, value.GetType(), collection, arrayList);
		}

		/// <summary>Serializes the given collection.</summary>
		/// <returns>Serialized collection if the serialization process succeeded; otherwise, null.</returns>
		/// <param name="manager">The <see cref="T:System.ComponentModel.Design.Serialization.IDesignerSerializationManager" /> to use during serialization.</param>
		/// <param name="targetExpression">The <see cref="T:System.CodeDom.CodeExpression" /> that refers to the collection</param>
		/// <param name="targetType">The <see cref="T:System.Type" /> of the collection.</param>
		/// <param name="originalCollection">The collection to serialize.</param>
		/// <param name="valuesToSerialize">The values to serialize.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="manager" />, <paramref name="targetType" />, <paramref name="originalCollection" />, or <paramref name="valuesToSerialize" /> is null.</exception>
		// Token: 0x06000A37 RID: 2615 RVA: 0x0001489C File Offset: 0x00012A9C
		protected virtual object SerializeCollection(IDesignerSerializationManager manager, CodeExpression targetExpression, Type targetType, ICollection originalCollection, ICollection valuesToSerialize)
		{
			if (valuesToSerialize == null)
			{
				throw new ArgumentNullException("valuesToSerialize");
			}
			if (originalCollection == null)
			{
				throw new ArgumentNullException("originalCollection");
			}
			if (targetType == null)
			{
				throw new ArgumentNullException("targetType");
			}
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			if (valuesToSerialize.Count == 0)
			{
				return null;
			}
			MethodInfo methodInfo = null;
			try
			{
				IEnumerator enumerator = valuesToSerialize.GetEnumerator();
				enumerator.MoveNext();
				object obj = enumerator.Current;
				methodInfo = this.GetExactMethod(targetType, "Add", new object[] { obj });
			}
			catch
			{
				base.ReportError(manager, "A compatible Add/AddRange method is missing in the collection type '" + targetType.Name + "'");
			}
			if (methodInfo == null)
			{
				return null;
			}
			CodeStatementCollection codeStatementCollection = new CodeStatementCollection();
			foreach (object obj2 in valuesToSerialize)
			{
				CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression();
				codeMethodInvokeExpression.Method = new CodeMethodReferenceExpression(targetExpression, "Add");
				CodeExpression codeExpression = base.SerializeToExpression(manager, obj2);
				if (codeExpression != null)
				{
					codeMethodInvokeExpression.Parameters.AddRange(new CodeExpression[] { codeExpression });
					codeStatementCollection.Add(codeMethodInvokeExpression);
				}
			}
			return codeStatementCollection;
		}

		// Token: 0x06000A38 RID: 2616 RVA: 0x000149EC File Offset: 0x00012BEC
		private MethodInfo GetExactMethod(Type type, string methodName, ICollection argsCollection)
		{
			Type[] array = Type.EmptyTypes;
			if (argsCollection != null)
			{
				object[] array2 = new object[argsCollection.Count];
				array = new Type[argsCollection.Count];
				argsCollection.CopyTo(array2, 0);
				for (int i = 0; i < array2.Length; i++)
				{
					if (array2[i] == null)
					{
						array[i] = null;
					}
					else
					{
						array[i] = array2[i].GetType();
					}
				}
			}
			return type.GetMethod(methodName, array);
		}
	}
}
