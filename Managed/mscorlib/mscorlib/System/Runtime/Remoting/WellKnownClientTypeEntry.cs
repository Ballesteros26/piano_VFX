using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting
{
	/// <summary>Holds values for an object type registered on the client as a server-activated type (single call or singleton).</summary>
	// Token: 0x02000767 RID: 1895
	[ComVisible(true)]
	public class WellKnownClientTypeEntry : TypeEntry
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.WellKnownClientTypeEntry" /> class with the given type and URL.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> of the server-activated type. </param>
		/// <param name="objectUrl">The URL of the server-activated type. </param>
		// Token: 0x06004E4F RID: 20047 RVA: 0x0011B071 File Offset: 0x00119271
		public WellKnownClientTypeEntry(Type type, string objectUrl)
		{
			base.AssemblyName = type.Assembly.FullName;
			base.TypeName = type.FullName;
			this.obj_type = type;
			this.obj_url = objectUrl;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.WellKnownClientTypeEntry" /> class with the given type, assembly name, and URL.</summary>
		/// <param name="typeName">The type name of the server-activated type. </param>
		/// <param name="assemblyName">The assembly name of the server-activated type. </param>
		/// <param name="objectUrl">The URL of the server-activated type. </param>
		// Token: 0x06004E50 RID: 20048 RVA: 0x0011B0A4 File Offset: 0x001192A4
		public WellKnownClientTypeEntry(string typeName, string assemblyName, string objectUrl)
		{
			this.obj_url = objectUrl;
			base.AssemblyName = assemblyName;
			base.TypeName = typeName;
			Assembly assembly = Assembly.Load(assemblyName);
			this.obj_type = assembly.GetType(typeName);
			if (this.obj_type == null)
			{
				throw new RemotingException("Type not found: " + typeName + ", " + assemblyName);
			}
		}

		/// <summary>Gets or sets the URL of the application to activate the type in.</summary>
		/// <returns>The URL of the application to activate the type in.</returns>
		// Token: 0x17000D0A RID: 3338
		// (get) Token: 0x06004E51 RID: 20049 RVA: 0x0011B105 File Offset: 0x00119305
		// (set) Token: 0x06004E52 RID: 20050 RVA: 0x0011B10D File Offset: 0x0011930D
		public string ApplicationUrl
		{
			get
			{
				return this.app_url;
			}
			set
			{
				this.app_url = value;
			}
		}

		/// <summary>Gets the <see cref="T:System.Type" /> of the server-activated client type.</summary>
		/// <returns>Gets the <see cref="T:System.Type" /> of the server-activated client type.</returns>
		// Token: 0x17000D0B RID: 3339
		// (get) Token: 0x06004E53 RID: 20051 RVA: 0x0011B116 File Offset: 0x00119316
		public Type ObjectType
		{
			get
			{
				return this.obj_type;
			}
		}

		/// <summary>Gets the URL of the server-activated client object.</summary>
		/// <returns>The URL of the server-activated client object.</returns>
		// Token: 0x17000D0C RID: 3340
		// (get) Token: 0x06004E54 RID: 20052 RVA: 0x0011B11E File Offset: 0x0011931E
		public string ObjectUrl
		{
			get
			{
				return this.obj_url;
			}
		}

		/// <summary>Returns the full type name, assembly name, and object URL of the server-activated client type as a <see cref="T:System.String" />.</summary>
		/// <returns>The full type name, assembly name, and object URL of the server-activated client type as a <see cref="T:System.String" />.</returns>
		// Token: 0x06004E55 RID: 20053 RVA: 0x0011B126 File Offset: 0x00119326
		public override string ToString()
		{
			if (this.ApplicationUrl != null)
			{
				return base.TypeName + base.AssemblyName + this.ObjectUrl + this.ApplicationUrl;
			}
			return base.TypeName + base.AssemblyName + this.ObjectUrl;
		}

		// Token: 0x040029E3 RID: 10723
		private Type obj_type;

		// Token: 0x040029E4 RID: 10724
		private string obj_url;

		// Token: 0x040029E5 RID: 10725
		private string app_url;
	}
}
