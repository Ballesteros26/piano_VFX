using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;

namespace System.Runtime.Remoting
{
	/// <summary>Holds values for an object type registered on the service end as a server-activated type object (single call or singleton).</summary>
	// Token: 0x02000769 RID: 1897
	[ComVisible(true)]
	public class WellKnownServiceTypeEntry : TypeEntry
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.WellKnownServiceTypeEntry" /> class with the given <see cref="T:System.Type" />, object URI, and <see cref="T:System.Runtime.Remoting.WellKnownObjectMode" />.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> of the server-activated service type object. </param>
		/// <param name="objectUri">The URI of the server-activated type. </param>
		/// <param name="mode">The <see cref="T:System.Runtime.Remoting.WellKnownObjectMode" /> of the type, which defines how the object is activated. </param>
		// Token: 0x06004E56 RID: 20054 RVA: 0x0011B165 File Offset: 0x00119365
		public WellKnownServiceTypeEntry(Type type, string objectUri, WellKnownObjectMode mode)
		{
			base.AssemblyName = type.Assembly.FullName;
			base.TypeName = type.FullName;
			this.obj_type = type;
			this.obj_uri = objectUri;
			this.obj_mode = mode;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.WellKnownServiceTypeEntry" /> class with the given type name, assembly name, object URI, and <see cref="T:System.Runtime.Remoting.WellKnownObjectMode" />.</summary>
		/// <param name="typeName">The full type name of the server-activated service type. </param>
		/// <param name="assemblyName">The assembly name of the server-activated service type. </param>
		/// <param name="objectUri">The URI of the server-activated object. </param>
		/// <param name="mode">The <see cref="T:System.Runtime.Remoting.WellKnownObjectMode" /> of the type, which defines how the object is activated. </param>
		// Token: 0x06004E57 RID: 20055 RVA: 0x0011B1A0 File Offset: 0x001193A0
		public WellKnownServiceTypeEntry(string typeName, string assemblyName, string objectUri, WellKnownObjectMode mode)
		{
			base.AssemblyName = assemblyName;
			base.TypeName = typeName;
			Assembly assembly = Assembly.Load(assemblyName);
			this.obj_type = assembly.GetType(typeName);
			this.obj_uri = objectUri;
			this.obj_mode = mode;
			if (this.obj_type == null)
			{
				throw new RemotingException("Type not found: " + typeName + ", " + assemblyName);
			}
		}

		/// <summary>Gets or sets the context attributes for the server-activated service type.</summary>
		/// <returns>Gets or sets the context attributes for the server-activated service type.</returns>
		// Token: 0x17000D0D RID: 3341
		// (get) Token: 0x06004E58 RID: 20056 RVA: 0x0000A42E File Offset: 0x0000862E
		// (set) Token: 0x06004E59 RID: 20057 RVA: 0x00002194 File Offset: 0x00000394
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

		/// <summary>Gets the <see cref="T:System.Runtime.Remoting.WellKnownObjectMode" /> of the server-activated service type.</summary>
		/// <returns>The <see cref="T:System.Runtime.Remoting.WellKnownObjectMode" /> of the server-activated service type.</returns>
		// Token: 0x17000D0E RID: 3342
		// (get) Token: 0x06004E5A RID: 20058 RVA: 0x0011B209 File Offset: 0x00119409
		public WellKnownObjectMode Mode
		{
			get
			{
				return this.obj_mode;
			}
		}

		/// <summary>Gets the <see cref="T:System.Type" /> of the server-activated service type.</summary>
		/// <returns>The <see cref="T:System.Type" /> of the server-activated service type.</returns>
		// Token: 0x17000D0F RID: 3343
		// (get) Token: 0x06004E5B RID: 20059 RVA: 0x0011B211 File Offset: 0x00119411
		public Type ObjectType
		{
			get
			{
				return this.obj_type;
			}
		}

		/// <summary>Gets the URI of the well-known service type.</summary>
		/// <returns>The URI of the server-activated service type.</returns>
		// Token: 0x17000D10 RID: 3344
		// (get) Token: 0x06004E5C RID: 20060 RVA: 0x0011B219 File Offset: 0x00119419
		public string ObjectUri
		{
			get
			{
				return this.obj_uri;
			}
		}

		/// <summary>Returns the type name, assembly name, object URI and the <see cref="T:System.Runtime.Remoting.WellKnownObjectMode" /> of the server-activated type as a <see cref="T:System.String" />.</summary>
		/// <returns>The type name, assembly name, object URI, and the <see cref="T:System.Runtime.Remoting.WellKnownObjectMode" /> of the server-activated type as a <see cref="T:System.String" />.</returns>
		// Token: 0x06004E5D RID: 20061 RVA: 0x0011B221 File Offset: 0x00119421
		public override string ToString()
		{
			return string.Concat(new string[] { base.TypeName, ", ", base.AssemblyName, " ", this.ObjectUri });
		}

		// Token: 0x040029E9 RID: 10729
		private Type obj_type;

		// Token: 0x040029EA RID: 10730
		private string obj_uri;

		// Token: 0x040029EB RID: 10731
		private WellKnownObjectMode obj_mode;
	}
}
