using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;
using System.Runtime.Serialization;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
	/// <summary>Implements the <see cref="T:System.Runtime.Remoting.Activation.IConstructionReturnMessage" /> interface to create a message that responds to a call to instantiate a remote object.</summary>
	// Token: 0x02000804 RID: 2052
	[CLSCompliant(false)]
	[ComVisible(true)]
	[Serializable]
	public class ConstructionResponse : MethodResponse, IConstructionReturnMessage, IMethodReturnMessage, IMethodMessage, IMessage
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Messaging.ConstructionResponse" /> class from an array of remoting headers and a request message.</summary>
		/// <param name="h">An array of remoting headers that contain key-value pairs. This array is used to initialize <see cref="T:System.Runtime.Remoting.Messaging.ConstructionResponse" /> fields for those headers that belong to the namespace "http://schemas.microsoft.com/clr/soap/messageProperties".</param>
		/// <param name="mcm">A request message that constitutes a constructor call on a remote object.</param>
		// Token: 0x0600522B RID: 21035 RVA: 0x00122A93 File Offset: 0x00120C93
		public ConstructionResponse(Header[] h, IMethodCallMessage mcm)
			: base(h, mcm)
		{
		}

		// Token: 0x0600522C RID: 21036 RVA: 0x00122A9D File Offset: 0x00120C9D
		internal ConstructionResponse(object resultObject, LogicalCallContext callCtx, IMethodCallMessage msg)
			: base(resultObject, null, callCtx, msg)
		{
		}

		// Token: 0x0600522D RID: 21037 RVA: 0x00122AA9 File Offset: 0x00120CA9
		internal ConstructionResponse(Exception e, IMethodCallMessage msg)
			: base(e, msg)
		{
		}

		// Token: 0x0600522E RID: 21038 RVA: 0x00122AB3 File Offset: 0x00120CB3
		internal ConstructionResponse(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		/// <summary>Gets an <see cref="T:System.Collections.IDictionary" /> interface that represents a collection of the remoting message's properties. </summary>
		/// <returns>An <see cref="T:System.Collections.IDictionary" /> interface that represents a collection of the remoting message's properties.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000E07 RID: 3591
		// (get) Token: 0x0600522F RID: 21039 RVA: 0x00122ABD File Offset: 0x00120CBD
		public override IDictionary Properties
		{
			[SecurityCritical]
			get
			{
				return base.Properties;
			}
		}
	}
}
