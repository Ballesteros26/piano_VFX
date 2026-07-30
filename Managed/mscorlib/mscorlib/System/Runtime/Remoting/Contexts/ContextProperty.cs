using System;
using System.Runtime.InteropServices;
using Unity;

namespace System.Runtime.Remoting.Contexts
{
	/// <summary>Holds the name/value pair of the property name and the object representing the property of a context.</summary>
	// Token: 0x0200077F RID: 1919
	[ComVisible(true)]
	public class ContextProperty
	{
		// Token: 0x06004F16 RID: 20246 RVA: 0x0011D2E1 File Offset: 0x0011B4E1
		private ContextProperty(string name, object prop)
		{
			this.name = name;
			this.prop = prop;
		}

		/// <summary>Gets the name of the T:System.Runtime.Remoting.Contexts.ContextProperty class.</summary>
		/// <returns>The name of the <see cref="T:System.Runtime.Remoting.Contexts.ContextProperty" /> class.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000D31 RID: 3377
		// (get) Token: 0x06004F17 RID: 20247 RVA: 0x0011D2F7 File Offset: 0x0011B4F7
		public virtual string Name
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Gets the object representing the property of a context.</summary>
		/// <returns>The object representing the property of a context.</returns>
		// Token: 0x17000D32 RID: 3378
		// (get) Token: 0x06004F18 RID: 20248 RVA: 0x0011D2FF File Offset: 0x0011B4FF
		public virtual object Property
		{
			get
			{
				return this.prop;
			}
		}

		// Token: 0x06004F19 RID: 20249 RVA: 0x0001FB35 File Offset: 0x0001DD35
		internal ContextProperty()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04002A28 RID: 10792
		private string name;

		// Token: 0x04002A29 RID: 10793
		private object prop;
	}
}
