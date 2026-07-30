using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;
using System.Reflection;
using Unity;

namespace System.Linq.Expressions
{
	/// <summary>Represents initializing the elements of a collection member of a newly created object.</summary>
	// Token: 0x02000293 RID: 659
	public sealed class MemberListBinding : MemberBinding
	{
		// Token: 0x06001349 RID: 4937 RVA: 0x0003BE80 File Offset: 0x0003A080
		internal MemberListBinding(MemberInfo member, ReadOnlyCollection<ElementInit> initializers)
			: base(MemberBindingType.ListBinding, member)
		{
			this.Initializers = initializers;
		}

		/// <summary>Gets the element initializers for initializing a collection member of a newly created object.</summary>
		/// <returns>A <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" /> of <see cref="T:System.Linq.Expressions.ElementInit" /> objects to initialize a collection member with.</returns>
		// Token: 0x1700034B RID: 843
		// (get) Token: 0x0600134A RID: 4938 RVA: 0x0003BE91 File Offset: 0x0003A091
		public ReadOnlyCollection<ElementInit> Initializers { get; }

		/// <summary>Creates a new expression that is like this one, but using the supplied children. If all of the children are the same, it will return this expression.</summary>
		/// <returns>This expression if no children are changed or an expression with the updated children.</returns>
		/// <param name="initializers">The <see cref="P:System.Linq.Expressions.MemberListBinding.Initializers" /> property of the result.</param>
		// Token: 0x0600134B RID: 4939 RVA: 0x0003BE99 File Offset: 0x0003A099
		public MemberListBinding Update(IEnumerable<ElementInit> initializers)
		{
			if (initializers != null && ExpressionUtils.SameElements<ElementInit>(ref initializers, this.Initializers))
			{
				return this;
			}
			return Expression.ListBind(base.Member, initializers);
		}

		// Token: 0x0600134C RID: 4940 RVA: 0x00003C4C File Offset: 0x00001E4C
		internal override void ValidateAsDefinedHere(int index)
		{
		}

		// Token: 0x0600134D RID: 4941 RVA: 0x0000220F File Offset: 0x0000040F
		internal MemberListBinding()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
