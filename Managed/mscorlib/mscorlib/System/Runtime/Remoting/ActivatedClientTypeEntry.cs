using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;

namespace System.Runtime.Remoting
{
	/// <summary>Holds values for an object type registered on the client end as a type that can be activated on the server.</summary>
	// Token: 0x02000747 RID: 1863
	[ComVisible(true)]
	public class ActivatedClientTypeEntry : TypeEntry
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.ActivatedClientTypeEntry" /> class with the given <see cref="T:System.Type" /> and application URL.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> of the client activated type. </param>
		/// <param name="appUrl">The URL of the application to activate the type in. </param>
		// Token: 0x06004D27 RID: 19751 RVA: 0x00116B04 File Offset: 0x00114D04
		public ActivatedClientTypeEntry(Type type, string appUrl)
		{
			base.AssemblyName = type.Assembly.FullName;
			base.TypeName = type.FullName;
			this.applicationUrl = appUrl;
			this.obj_type = type;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.ActivatedClientTypeEntry" /> class with the given type name, assembly name, and application URL.</summary>
		/// <param name="typeName">The type name of the client activated type. </param>
		/// <param name="assemblyName">The assembly name of the client activated type. </param>
		/// <param name="appUrl">The URL of the application to activate the type in. </param>
		// Token: 0x06004D28 RID: 19752 RVA: 0x00116B38 File Offset: 0x00114D38
		public ActivatedClientTypeEntry(string typeName, string assemblyName, string appUrl)
		{
			base.AssemblyName = assemblyName;
			base.TypeName = typeName;
			this.applicationUrl = appUrl;
			Assembly assembly = Assembly.Load(assemblyName);
			this.obj_type = assembly.GetType(typeName);
			if (this.obj_type == null)
			{
				throw new RemotingException("Type not found: " + typeName + ", " + assemblyName);
			}
		}

		/// <summary>Gets the URL of the application to activate the type in.</summary>
		/// <returns>The URL of the application to activate the type in.</returns>
		// Token: 0x17000CDD RID: 3293
		// (get) Token: 0x06004D29 RID: 19753 RVA: 0x00116B99 File Offset: 0x00114D99
		public string ApplicationUrl
		{
			get
			{
				return this.applicationUrl;
			}
		}

		/// <summary>Gets or sets the context attributes for the client-activated type.</summary>
		/// <returns>The context attributes for the client activated type.</returns>
		// Token: 0x17000CDE RID: 3294
		// (get) Token: 0x06004D2A RID: 19754 RVA: 0x0000A42E File Offset: 0x0000862E
		// (set) Token: 0x06004D2B RID: 19755 RVA: 0x00002194 File Offset: 0x00000394
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

		/// <summary>Gets the <see cref="T:System.Type" /> of the client-activated type.</summary>
		/// <returns>Gets the <see cref="T:System.Type" /> of the client-activated type.</returns>
		// Token: 0x17000CDF RID: 3295
		// (get) Token: 0x06004D2C RID: 19756 RVA: 0x00116BA1 File Offset: 0x00114DA1
		public Type ObjectType
		{
			get
			{
				return this.obj_type;
			}
		}

		/// <summary>Returns the type name, assembly name, and application URL of the client-activated type as a <see cref="T:System.String" />.</summary>
		/// <returns>The type name, assembly name, and application URL of the client-activated type as a <see cref="T:System.String" />.</returns>
		// Token: 0x06004D2D RID: 19757 RVA: 0x00116BA9 File Offset: 0x00114DA9
		public override string ToString()
		{
			return base.TypeName + base.AssemblyName + this.ApplicationUrl;
		}

		// Token: 0x04002989 RID: 10633
		private string applicationUrl;

		// Token: 0x0400298A RID: 10634
		private Type obj_type;
	}
}
