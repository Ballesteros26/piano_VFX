using System;
using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
	/// <summary>Implements the <see cref="T:System.Runtime.Remoting.Messaging.IMethodReturnMessage" /> interface to create a message that acts as a method response on a remote object.</summary>
	// Token: 0x0200081A RID: 2074
	[ComVisible(true)]
	[CLSCompliant(false)]
	[Serializable]
	public class MethodResponse : IMethodReturnMessage, IMethodMessage, IMessage, ISerializable, IInternalMessage, ISerializationRootObject
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Messaging.MethodResponse" /> class from an array of remoting headers and a request message. </summary>
		/// <param name="h1">An array of remoting headers that contains key/value pairs. This array is used to initialize <see cref="T:System.Runtime.Remoting.Messaging.MethodResponse" /> fields for headers that belong to the namespace "http://schemas.microsoft.com/clr/soap/messageProperties".</param>
		/// <param name="mcm">A request message that acts as a method call on a remote object.</param>
		// Token: 0x060052D9 RID: 21209 RVA: 0x00124058 File Offset: 0x00122258
		public MethodResponse(Header[] h1, IMethodCallMessage mcm)
		{
			if (mcm != null)
			{
				this._methodName = mcm.MethodName;
				this._uri = mcm.Uri;
				this._typeName = mcm.TypeName;
				this._methodBase = mcm.MethodBase;
				this._methodSignature = (Type[])mcm.MethodSignature;
				this._args = mcm.Args;
			}
			if (h1 != null)
			{
				foreach (Header header in h1)
				{
					this.InitMethodProperty(header.Name, header.Value);
				}
			}
		}

		// Token: 0x060052DA RID: 21210 RVA: 0x001240E4 File Offset: 0x001222E4
		internal MethodResponse(Exception e, IMethodCallMessage msg)
		{
			this._callMsg = msg;
			if (msg != null)
			{
				this._uri = msg.Uri;
			}
			else
			{
				this._uri = string.Empty;
			}
			this._exception = e;
			this._returnValue = null;
			this._outArgs = new object[0];
		}

		// Token: 0x060052DB RID: 21211 RVA: 0x00124134 File Offset: 0x00122334
		internal MethodResponse(object returnValue, object[] outArgs, LogicalCallContext callCtx, IMethodCallMessage msg)
		{
			this._callMsg = msg;
			this._uri = msg.Uri;
			this._exception = null;
			this._returnValue = returnValue;
			this._args = outArgs;
		}

		// Token: 0x060052DC RID: 21212 RVA: 0x00124168 File Offset: 0x00122368
		internal MethodResponse(IMethodCallMessage msg, CADMethodReturnMessage retmsg)
		{
			this._callMsg = msg;
			this._methodBase = msg.MethodBase;
			this._uri = msg.Uri;
			this._methodName = msg.MethodName;
			ArrayList arguments = retmsg.GetArguments();
			this._exception = retmsg.GetException(arguments);
			this._returnValue = retmsg.GetReturnValue(arguments);
			this._args = retmsg.GetArgs(arguments);
			this._callContext = retmsg.GetLogicalCallContext(arguments);
			if (this._callContext == null)
			{
				this._callContext = new LogicalCallContext();
			}
			if (retmsg.PropertiesCount > 0)
			{
				CADMessageBase.UnmarshalProperties(this.Properties, retmsg.PropertiesCount, arguments);
			}
		}

		// Token: 0x060052DD RID: 21213 RVA: 0x00124210 File Offset: 0x00122410
		internal MethodResponse(IMethodCallMessage msg, object handlerObject, BinaryMethodReturnMessage smuggledMrm)
		{
			if (msg != null)
			{
				this._methodBase = msg.MethodBase;
				this._methodName = msg.MethodName;
				this._uri = msg.Uri;
			}
			this._returnValue = smuggledMrm.ReturnValue;
			this._args = smuggledMrm.Args;
			this._exception = smuggledMrm.Exception;
			this._callContext = smuggledMrm.LogicalCallContext;
			if (smuggledMrm.HasProperties)
			{
				smuggledMrm.PopulateMessageProperties(this.Properties);
			}
		}

		// Token: 0x060052DE RID: 21214 RVA: 0x00124290 File Offset: 0x00122490
		internal MethodResponse(SerializationInfo info, StreamingContext context)
		{
			foreach (SerializationEntry serializationEntry in info)
			{
				this.InitMethodProperty(serializationEntry.Name, serializationEntry.Value);
			}
		}

		// Token: 0x060052DF RID: 21215 RVA: 0x001242D0 File Offset: 0x001224D0
		internal void InitMethodProperty(string key, object value)
		{
			uint num = <PrivateImplementationDetails>.ComputeStringHash(key);
			if (num <= 1960967436U)
			{
				if (num <= 1201911322U)
				{
					if (num != 990701179U)
					{
						if (num == 1201911322U)
						{
							if (key == "__CallContext")
							{
								this._callContext = (LogicalCallContext)value;
								return;
							}
						}
					}
					else if (key == "__Uri")
					{
						this._uri = (string)value;
						return;
					}
				}
				else if (num != 1637783905U)
				{
					if (num == 1960967436U)
					{
						if (key == "__OutArgs")
						{
							this._args = (object[])value;
							return;
						}
					}
				}
				else if (key == "__Return")
				{
					this._returnValue = value;
					return;
				}
			}
			else if (num <= 3166241401U)
			{
				if (num != 2010141056U)
				{
					if (num == 3166241401U)
					{
						if (key == "__MethodName")
						{
							this._methodName = (string)value;
							return;
						}
					}
				}
				else if (key == "__TypeName")
				{
					this._typeName = (string)value;
					return;
				}
			}
			else if (num != 3626951189U)
			{
				if (num == 3679129400U)
				{
					if (key == "__MethodSignature")
					{
						this._methodSignature = (Type[])value;
						return;
					}
				}
			}
			else if (key == "__fault")
			{
				this._exception = (Exception)value;
				return;
			}
			this.Properties[key] = value;
		}

		/// <summary>Gets the number of arguments passed to the method. </summary>
		/// <returns>A <see cref="T:System.Int32" /> that represents the number of arguments passed to a method.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000E51 RID: 3665
		// (get) Token: 0x060052E0 RID: 21216 RVA: 0x00124456 File Offset: 0x00122656
		public int ArgCount
		{
			[SecurityCritical]
			get
			{
				if (this._args == null)
				{
					return 0;
				}
				return this._args.Length;
			}
		}

		/// <summary>Gets an array of arguments passed to the method. </summary>
		/// <returns>An array of type <see cref="T:System.Object" /> that represents the arguments passed to a method.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000E52 RID: 3666
		// (get) Token: 0x060052E1 RID: 21217 RVA: 0x0012446A File Offset: 0x0012266A
		public object[] Args
		{
			[SecurityCritical]
			get
			{
				return this._args;
			}
		}

		/// <summary>Gets the exception thrown during the method call, or null if the method did not throw an exception. </summary>
		/// <returns>The <see cref="T:System.Exception" /> thrown during the method call, or null if the method did not throw an exception.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000E53 RID: 3667
		// (get) Token: 0x060052E2 RID: 21218 RVA: 0x00124472 File Offset: 0x00122672
		public Exception Exception
		{
			[SecurityCritical]
			get
			{
				return this._exception;
			}
		}

		/// <summary>Gets a value that indicates whether the method can accept a variable number of arguments. </summary>
		/// <returns>true if the method can accept a variable number of arguments; otherwise, false.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000E54 RID: 3668
		// (get) Token: 0x060052E3 RID: 21219 RVA: 0x0012447A File Offset: 0x0012267A
		public bool HasVarArgs
		{
			[SecurityCritical]
			get
			{
				return (this.MethodBase.CallingConvention | CallingConventions.VarArgs) > (CallingConventions)0;
			}
		}

		/// <summary>Gets the <see cref="T:System.Runtime.Remoting.Messaging.LogicalCallContext" /> for the current method call. </summary>
		/// <returns>The <see cref="T:System.Runtime.Remoting.Messaging.LogicalCallContext" /> for the current method call.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000E55 RID: 3669
		// (get) Token: 0x060052E4 RID: 21220 RVA: 0x0012448C File Offset: 0x0012268C
		public LogicalCallContext LogicalCallContext
		{
			[SecurityCritical]
			get
			{
				if (this._callContext == null)
				{
					this._callContext = new LogicalCallContext();
				}
				return this._callContext;
			}
		}

		/// <summary>Gets the <see cref="T:System.Reflection.MethodBase" /> of the called method. </summary>
		/// <returns>The <see cref="T:System.Reflection.MethodBase" /> of the called method.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000E56 RID: 3670
		// (get) Token: 0x060052E5 RID: 21221 RVA: 0x001244A8 File Offset: 0x001226A8
		public MethodBase MethodBase
		{
			[SecurityCritical]
			get
			{
				if (null == this._methodBase)
				{
					if (this._callMsg != null)
					{
						this._methodBase = this._callMsg.MethodBase;
					}
					else if (this.MethodName != null && this.TypeName != null)
					{
						this._methodBase = RemotingServices.GetMethodBaseFromMethodMessage(this);
					}
				}
				return this._methodBase;
			}
		}

		/// <summary>Gets the name of the invoked method. </summary>
		/// <returns>A <see cref="T:System.String" /> that contains the name of the invoked method.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000E57 RID: 3671
		// (get) Token: 0x060052E6 RID: 21222 RVA: 0x00124500 File Offset: 0x00122700
		public string MethodName
		{
			[SecurityCritical]
			get
			{
				if (this._methodName == null && this._callMsg != null)
				{
					this._methodName = this._callMsg.MethodName;
				}
				return this._methodName;
			}
		}

		/// <summary>Gets an object that contains the method signature. </summary>
		/// <returns>A <see cref="T:System.Object" /> that contains the method signature.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000E58 RID: 3672
		// (get) Token: 0x060052E7 RID: 21223 RVA: 0x00124529 File Offset: 0x00122729
		public object MethodSignature
		{
			[SecurityCritical]
			get
			{
				if (this._methodSignature == null && this._callMsg != null)
				{
					this._methodSignature = (Type[])this._callMsg.MethodSignature;
				}
				return this._methodSignature;
			}
		}

		/// <summary>Gets the number of arguments in the method call marked as ref or out parameters.</summary>
		/// <returns>A <see cref="T:System.Int32" /> that represents the number of arguments in the method call marked as ref or out parameters.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000E59 RID: 3673
		// (get) Token: 0x060052E8 RID: 21224 RVA: 0x00124557 File Offset: 0x00122757
		public int OutArgCount
		{
			[SecurityCritical]
			get
			{
				if (this._args == null || this._args.Length == 0)
				{
					return 0;
				}
				if (this._inArgInfo == null)
				{
					this._inArgInfo = new ArgInfo(this.MethodBase, ArgInfoType.Out);
				}
				return this._inArgInfo.GetInOutArgCount();
			}
		}

		/// <summary>Gets an array of arguments in the method call that are marked as ref or out parameters.</summary>
		/// <returns>An array of type <see cref="T:System.Object" /> that represents the arguments in the method call that are marked as ref or out parameters.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000E5A RID: 3674
		// (get) Token: 0x060052E9 RID: 21225 RVA: 0x00124594 File Offset: 0x00122794
		public object[] OutArgs
		{
			[SecurityCritical]
			get
			{
				if (this._outArgs == null && this._args != null)
				{
					if (this._inArgInfo == null)
					{
						this._inArgInfo = new ArgInfo(this.MethodBase, ArgInfoType.Out);
					}
					this._outArgs = this._inArgInfo.GetInOutArgs(this._args);
				}
				return this._outArgs;
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.IDictionary" /> interface that represents a collection of the remoting message's properties. </summary>
		/// <returns>An <see cref="T:System.Collections.IDictionary" /> interface that represents a collection of the remoting message's properties.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000E5B RID: 3675
		// (get) Token: 0x060052EA RID: 21226 RVA: 0x001245E8 File Offset: 0x001227E8
		public virtual IDictionary Properties
		{
			[SecurityCritical]
			get
			{
				if (this.ExternalProperties == null)
				{
					MethodReturnDictionary methodReturnDictionary = new MethodReturnDictionary(this);
					this.ExternalProperties = methodReturnDictionary;
					this.InternalProperties = methodReturnDictionary.GetInternalProperties();
				}
				return this.ExternalProperties;
			}
		}

		/// <summary>Gets the return value of the method call. </summary>
		/// <returns>A <see cref="T:System.Object" /> that represents the return value of the method call.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000E5C RID: 3676
		// (get) Token: 0x060052EB RID: 21227 RVA: 0x0012461D File Offset: 0x0012281D
		public object ReturnValue
		{
			[SecurityCritical]
			get
			{
				return this._returnValue;
			}
		}

		/// <summary>Gets the full type name of the remote object on which the method call is being made. </summary>
		/// <returns>A <see cref="T:System.String" /> that contains the full type name of the remote object on which the method call is being made.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000E5D RID: 3677
		// (get) Token: 0x060052EC RID: 21228 RVA: 0x00124625 File Offset: 0x00122825
		public string TypeName
		{
			[SecurityCritical]
			get
			{
				if (this._typeName == null && this._callMsg != null)
				{
					this._typeName = this._callMsg.TypeName;
				}
				return this._typeName;
			}
		}

		/// <summary>Gets the Uniform Resource Identifier (URI) of the remote object on which the method call is being made. </summary>
		/// <returns>The URI of a remote object.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000E5E RID: 3678
		// (get) Token: 0x060052ED RID: 21229 RVA: 0x0012464E File Offset: 0x0012284E
		// (set) Token: 0x060052EE RID: 21230 RVA: 0x00124677 File Offset: 0x00122877
		public string Uri
		{
			[SecurityCritical]
			get
			{
				if (this._uri == null && this._callMsg != null)
				{
					this._uri = this._callMsg.Uri;
				}
				return this._uri;
			}
			set
			{
				this._uri = value;
			}
		}

		// Token: 0x17000E5F RID: 3679
		// (get) Token: 0x060052EF RID: 21231 RVA: 0x00124680 File Offset: 0x00122880
		// (set) Token: 0x060052F0 RID: 21232 RVA: 0x00124688 File Offset: 0x00122888
		string IInternalMessage.Uri
		{
			get
			{
				return this.Uri;
			}
			set
			{
				this.Uri = value;
			}
		}

		/// <summary>Gets a method argument, as an object, at a specified index. </summary>
		/// <returns>The method argument as an object.</returns>
		/// <param name="argNum">The index of the requested argument.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x060052F1 RID: 21233 RVA: 0x00124691 File Offset: 0x00122891
		[SecurityCritical]
		public object GetArg(int argNum)
		{
			if (this._args == null)
			{
				return null;
			}
			return this._args[argNum];
		}

		/// <summary>Gets the name of a method argument at a specified index. </summary>
		/// <returns>The name of the method argument.</returns>
		/// <param name="index">The index of the requested argument.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x060052F2 RID: 21234 RVA: 0x001246A5 File Offset: 0x001228A5
		[SecurityCritical]
		public string GetArgName(int index)
		{
			return this.MethodBase.GetParameters()[index].Name;
		}

		/// <summary>The <see cref="M:System.Runtime.Remoting.Messaging.MethodResponse.GetObjectData(System.Runtime.Serialization.SerializationInfo,System.Runtime.Serialization.StreamingContext)" /> method is not implemented. </summary>
		/// <param name="info">Data for serializing or deserializing the remote object.</param>
		/// <param name="context">Context of a certain serialized stream.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="SerializationFormatter, Infrastructure" />
		/// </PermissionSet>
		// Token: 0x060052F3 RID: 21235 RVA: 0x001246BC File Offset: 0x001228BC
		[SecurityCritical]
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (this._exception == null)
			{
				info.AddValue("__TypeName", this._typeName);
				info.AddValue("__MethodName", this._methodName);
				info.AddValue("__MethodSignature", this._methodSignature);
				info.AddValue("__Uri", this._uri);
				info.AddValue("__Return", this._returnValue);
				info.AddValue("__OutArgs", this._args);
			}
			else
			{
				info.AddValue("__fault", this._exception);
			}
			info.AddValue("__CallContext", this._callContext);
			if (this.InternalProperties != null)
			{
				foreach (object obj in this.InternalProperties)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					info.AddValue((string)dictionaryEntry.Key, dictionaryEntry.Value);
				}
			}
		}

		/// <summary>Returns the specified argument marked as a ref parameter or an out parameter. </summary>
		/// <returns>The specified argument marked as a ref parameter or an out parameter.</returns>
		/// <param name="argNum">The index of the requested argument.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x060052F4 RID: 21236 RVA: 0x001247C4 File Offset: 0x001229C4
		[SecurityCritical]
		public object GetOutArg(int argNum)
		{
			if (this._args == null)
			{
				return null;
			}
			if (this._inArgInfo == null)
			{
				this._inArgInfo = new ArgInfo(this.MethodBase, ArgInfoType.Out);
			}
			return this._args[this._inArgInfo.GetInOutArgIndex(argNum)];
		}

		/// <summary>Returns the name of the specified argument marked as a ref parameter or an out parameter.</summary>
		/// <returns>The argument name, or null if the current method is not implemented.</returns>
		/// <param name="index">The index of the requested argument.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x060052F5 RID: 21237 RVA: 0x00124800 File Offset: 0x00122A00
		[SecurityCritical]
		public string GetOutArgName(int index)
		{
			if (null == this._methodBase)
			{
				return "__method_" + index;
			}
			if (this._inArgInfo == null)
			{
				this._inArgInfo = new ArgInfo(this.MethodBase, ArgInfoType.Out);
			}
			return this._inArgInfo.GetInOutArgName(index);
		}

		/// <summary>Initializes an internal serialization handler from an array of remoting headers that are applied to a method. </summary>
		/// <returns>An internal serialization handler.</returns>
		/// <param name="h">An array of remoting headers that contain key/value pairs. This array is used to initialize <see cref="T:System.Runtime.Remoting.Messaging.MethodResponse" /> fields for headers that belong to the namespace "http://schemas.microsoft.com/clr/soap/messageProperties".</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x060052F6 RID: 21238 RVA: 0x0002126B File Offset: 0x0001F46B
		[MonoTODO]
		public virtual object HeaderHandler(Header[] h)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sets method information from serialization settings. </summary>
		/// <param name="info">The data for serializing or deserializing the remote object.</param>
		/// <param name="ctx">The context of a certain serialized stream.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x060052F7 RID: 21239 RVA: 0x0002126B File Offset: 0x0001F46B
		[MonoTODO]
		public void RootSetObjectData(SerializationInfo info, StreamingContext ctx)
		{
			throw new NotImplementedException();
		}

		// Token: 0x17000E60 RID: 3680
		// (get) Token: 0x060052F8 RID: 21240 RVA: 0x00124852 File Offset: 0x00122A52
		// (set) Token: 0x060052F9 RID: 21241 RVA: 0x0012485A File Offset: 0x00122A5A
		Identity IInternalMessage.TargetIdentity
		{
			get
			{
				return this._targetIdentity;
			}
			set
			{
				this._targetIdentity = value;
			}
		}

		// Token: 0x060052FA RID: 21242 RVA: 0x00124863 File Offset: 0x00122A63
		bool IInternalMessage.HasProperties()
		{
			return this.ExternalProperties != null || this.InternalProperties != null;
		}

		// Token: 0x04002B23 RID: 11043
		private string _methodName;

		// Token: 0x04002B24 RID: 11044
		private string _uri;

		// Token: 0x04002B25 RID: 11045
		private string _typeName;

		// Token: 0x04002B26 RID: 11046
		private MethodBase _methodBase;

		// Token: 0x04002B27 RID: 11047
		private object _returnValue;

		// Token: 0x04002B28 RID: 11048
		private Exception _exception;

		// Token: 0x04002B29 RID: 11049
		private Type[] _methodSignature;

		// Token: 0x04002B2A RID: 11050
		private ArgInfo _inArgInfo;

		// Token: 0x04002B2B RID: 11051
		private object[] _args;

		// Token: 0x04002B2C RID: 11052
		private object[] _outArgs;

		// Token: 0x04002B2D RID: 11053
		private IMethodCallMessage _callMsg;

		// Token: 0x04002B2E RID: 11054
		private LogicalCallContext _callContext;

		// Token: 0x04002B2F RID: 11055
		private Identity _targetIdentity;

		/// <summary>Specifies an <see cref="T:System.Collections.IDictionary" /> interface that represents a collection of the remoting message's properties. </summary>
		// Token: 0x04002B30 RID: 11056
		protected IDictionary ExternalProperties;

		/// <summary>Specifies an <see cref="T:System.Collections.IDictionary" /> interface that represents a collection of the remoting message's properties. </summary>
		// Token: 0x04002B31 RID: 11057
		protected IDictionary InternalProperties;
	}
}
