using System;
using System.CodeDom;
using System.Collections;

namespace System.ComponentModel.Design.Serialization
{
	/// <summary>Serializes an object to a new type.</summary>
	// Token: 0x02000161 RID: 353
	public class TypeCodeDomSerializer : CodeDomSerializerBase
	{
		/// <summary>Serializes the object root by creating a new type declaration that defines root.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeDeclaration" /> that defines the root object.</returns>
		/// <param name="manager">The serialization manager to use for serialization.</param>
		/// <param name="root">The object to serialize.</param>
		/// <param name="members">Optional collection of members. Can be null or empty.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="manager" /> or <paramref name="root" /> is null.</exception>
		// Token: 0x06000AA0 RID: 2720 RVA: 0x000162E4 File Offset: 0x000144E4
		public virtual CodeTypeDeclaration Serialize(IDesignerSerializationManager manager, object root, ICollection members)
		{
			if (root == null)
			{
				throw new ArgumentNullException("root");
			}
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			RootContext rootContext = new RootContext(new CodeThisReferenceExpression(), root);
			StatementContext statementContext = new StatementContext();
			if (members != null)
			{
				statementContext.StatementCollection.Populate(members);
			}
			statementContext.StatementCollection.Populate(root);
			CodeTypeDeclaration codeTypeDeclaration = new CodeTypeDeclaration(manager.GetName(root));
			manager.Context.Push(rootContext);
			manager.Context.Push(statementContext);
			manager.Context.Push(codeTypeDeclaration);
			if (members != null)
			{
				foreach (object obj in members)
				{
					base.SerializeToExpression(manager, obj);
				}
			}
			base.SerializeToExpression(manager, root);
			manager.Context.Pop();
			manager.Context.Pop();
			manager.Context.Pop();
			return codeTypeDeclaration;
		}

		/// <summary>Deserializes the given type declaration.</summary>
		/// <returns>The root object.</returns>
		/// <param name="manager">The serialization manager to use for serialization.</param>
		/// <param name="declaration">Type declaration to use for serialization.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="manager" /> or <paramref name="typeDecl" /> is null.</exception>
		// Token: 0x06000AA1 RID: 2721 RVA: 0x0000234B File Offset: 0x0000054B
		public virtual object Deserialize(IDesignerSerializationManager manager, CodeTypeDeclaration declaration)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns the method where statements used to serialize a member are stored.</summary>
		/// <returns>The method used to emit all of the initialization code for the given member.</returns>
		/// <param name="manager">The serialization manager to use for serialization.</param>
		/// <param name="declaration">The type declaration to use for serialization.</param>
		/// <param name="value">The value to use for serialization.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="manager" />, <paramref name="typeDecl" />, or <paramref name="value" /> is null.</exception>
		// Token: 0x06000AA2 RID: 2722 RVA: 0x000163E8 File Offset: 0x000145E8
		protected virtual CodeMemberMethod GetInitializeMethod(IDesignerSerializationManager manager, CodeTypeDeclaration declaration, object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (declaration == null)
			{
				throw new ArgumentNullException("declaration");
			}
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			return new CodeConstructor();
		}

		/// <summary>Returns an array of methods to be interpreted during deserialization.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeMemberMethod" /> array of methods to be interpreted during deserialization.</returns>
		/// <param name="manager">The serialization manager to use for serialization.</param>
		/// <param name="declaration">The type declaration to use for serialization.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="manager" /> or <paramref name="typeDecl" /> is null.</exception>
		// Token: 0x06000AA3 RID: 2723 RVA: 0x00016419 File Offset: 0x00014619
		protected virtual CodeMemberMethod[] GetInitializeMethods(IDesignerSerializationManager manager, CodeTypeDeclaration declaration)
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			if (declaration == null)
			{
				throw new ArgumentNullException("declaration");
			}
			return new CodeMemberMethod[]
			{
				new CodeConstructor()
			};
		}
	}
}
