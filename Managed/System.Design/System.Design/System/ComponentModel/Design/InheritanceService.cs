using System;
using System.Reflection;

namespace System.ComponentModel.Design
{
	/// <summary>Provides a set of methods for identifying inherited components.</summary>
	// Token: 0x02000129 RID: 297
	public class InheritanceService : IInheritanceService, IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.InheritanceService" /> class.</summary>
		// Token: 0x060008CE RID: 2254 RVA: 0x00002352 File Offset: 0x00000552
		[MonoTODO]
		public InheritanceService()
		{
		}

		/// <summary>Adds the components inherited by the specified component to the <see cref="T:System.ComponentModel.Design.InheritanceService" />.</summary>
		/// <param name="component">The component to search for inherited components to add to the specified container. </param>
		/// <param name="container">The container to add the inherited components to. </param>
		// Token: 0x060008CF RID: 2255 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public void AddInheritedComponents(IComponent component, IContainer container)
		{
			throw new NotImplementedException();
		}

		/// <summary>Adds the components of the specified type that are inherited by the specified component to the <see cref="T:System.ComponentModel.Design.InheritanceService" />.</summary>
		/// <param name="type">The base type to search for. </param>
		/// <param name="component">The component to search for inherited components to add to the specified container. </param>
		/// <param name="container">The container to add the inherited components to. </param>
		// Token: 0x060008D0 RID: 2256 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		protected virtual void AddInheritedComponents(Type type, IComponent component, IContainer container)
		{
			throw new NotImplementedException();
		}

		/// <summary>Releases all resources used by the <see cref="T:System.ComponentModel.Design.InheritanceService" />.</summary>
		// Token: 0x060008D1 RID: 2257 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public void Dispose()
		{
			throw new NotImplementedException();
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.ComponentModel.Design.InheritanceService" /> and optionally releases the managed resources. </summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x060008D2 RID: 2258 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		protected virtual void Dispose(bool disposing)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the inheritance attribute of the specified component.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.InheritanceAttribute" /> that describes the level of inheritance that this component comes from.</returns>
		/// <param name="component">The component to retrieve the inheritance attribute for. </param>
		// Token: 0x060008D3 RID: 2259 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public InheritanceAttribute GetInheritanceAttribute(IComponent component)
		{
			throw new NotImplementedException();
		}

		/// <summary>Indicates whether to ignore the specified member.</summary>
		/// <returns>true if the specified member should be included in the set of inherited components; otherwise, false.</returns>
		/// <param name="member">The member to check. This member is either a <see cref="T:System.Reflection.FieldInfo" /> or a <see cref="T:System.Reflection.MethodInfo" />. </param>
		/// <param name="component">The component instance this member is bound to. </param>
		// Token: 0x060008D4 RID: 2260 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		protected virtual bool IgnoreInheritedMember(MemberInfo member, IComponent component)
		{
			throw new NotImplementedException();
		}
	}
}
