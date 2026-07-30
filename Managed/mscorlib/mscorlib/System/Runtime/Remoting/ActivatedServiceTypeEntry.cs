using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;

namespace System.Runtime.Remoting
{
	/// <summary>Holds values for an object type registered on the service end as one that can be activated on request from a client.</summary>
	// Token: 0x02000748 RID: 1864
	[ComVisible(true)]
	public class ActivatedServiceTypeEntry : TypeEntry
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.ActivatedServiceTypeEntry" /> class with the given <see cref="T:System.Type" />.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> of the client-activated service type. </param>
		// Token: 0x06004D2E RID: 19758 RVA: 0x00116BC2 File Offset: 0x00114DC2
		public ActivatedServiceTypeEntry(Type type)
		{
			base.AssemblyName = type.Assembly.FullName;
			base.TypeName = type.FullName;
			this.obj_type = type;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.ActivatedServiceTypeEntry" /> class with the given type name and assembly name.</summary>
		/// <param name="typeName">The type name of the client-activated service type. </param>
		/// <param name="assemblyName">The assembly name of the client-activated service type. </param>
		// Token: 0x06004D2F RID: 19759 RVA: 0x00116BF0 File Offset: 0x00114DF0
		public ActivatedServiceTypeEntry(string typeName, string assemblyName)
		{
			base.AssemblyName = assemblyName;
			base.TypeName = typeName;
			Assembly assembly = Assembly.Load(assemblyName);
			this.obj_type = assembly.GetType(typeName);
			if (this.obj_type == null)
			{
				throw new RemotingException("Type not found: " + typeName + ", " + assemblyName);
			}
		}

		/// <summary>Gets or sets the context attributes for the client-activated service type.</summary>
		/// <returns>The context attributes for the client-activated service type.</returns>
		// Token: 0x17000CE0 RID: 3296
		// (get) Token: 0x06004D30 RID: 19760 RVA: 0x0000A42E File Offset: 0x0000862E
		// (set) Token: 0x06004D31 RID: 19761 RVA: 0x00002194 File Offset: 0x00000394
		public IContextAttribute[] ContextAttributes
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		/// <summary>Gets the <see cref="T:System.Type" /> of the client-activated service type.</summary>
		/// <returns>The <see cref="T:System.Type" /> of the client-activated service type.</returns>
		// Token: 0x17000CE1 RID: 3297
		// (get) Token: 0x06004D32 RID: 19762 RVA: 0x00116C4A File Offset: 0x00114E4A
		public Type ObjectType
		{
			get
			{
				return this.obj_type;
			}
		}

		/// <summary>Returns the type and assembly name of the client-activated service type as a <see cref="T:System.String" />.</summary>
		/// <returns>The type and assembly name of the client-activated service type as a <see cref="T:System.String" />.</returns>
		// Token: 0x06004D33 RID: 19763 RVA: 0x00116C52 File Offset: 0x00114E52
		public override string ToString()
		{
			return base.AssemblyName + base.TypeName;
		}

		// Token: 0x0400298B RID: 10635
		private Type obj_type;
	}
}
