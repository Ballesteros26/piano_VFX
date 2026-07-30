using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Lifetime;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Runtime.Remoting.Services;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Permissions;
using System.Threading;
using Mono.Interop;

namespace System.Runtime.Remoting
{
	/// <summary>Provides several methods for using and publishing remoted objects and proxies. This class cannot be inherited.</summary>
	// Token: 0x0200075A RID: 1882
	[ComVisible(true)]
	public static class RemotingServices
	{
		// Token: 0x06004DC1 RID: 19905 RVA: 0x001190CC File Offset: 0x001172CC
		static RemotingServices()
		{
			ISurrogateSelector surrogateSelector = new RemotingSurrogateSelector();
			StreamingContext streamingContext = new StreamingContext(StreamingContextStates.Remoting, null);
			RemotingServices._serializationFormatter = new BinaryFormatter(surrogateSelector, streamingContext);
			RemotingServices._deserializationFormatter = new BinaryFormatter(null, streamingContext);
			RemotingServices._serializationFormatter.AssemblyFormat = FormatterAssemblyStyle.Full;
			RemotingServices._deserializationFormatter.AssemblyFormat = FormatterAssemblyStyle.Full;
			RemotingServices.RegisterInternalChannels();
			RemotingServices.CreateWellKnownServerIdentity(typeof(RemoteActivator), "RemoteActivationService.rem", WellKnownObjectMode.Singleton);
			RemotingServices.FieldSetterMethod = typeof(object).GetMethod("FieldSetter", BindingFlags.Instance | BindingFlags.NonPublic);
			RemotingServices.FieldGetterMethod = typeof(object).GetMethod("FieldGetter", BindingFlags.Instance | BindingFlags.NonPublic);
		}

		// Token: 0x06004DC2 RID: 19906
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern object InternalExecute(MethodBase method, object obj, object[] parameters, out object[] out_args);

		// Token: 0x06004DC3 RID: 19907
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern MethodBase GetVirtualMethod(Type type, MethodBase method);

		/// <summary>Returns a Boolean value that indicates whether the given object is a transparent proxy or a real object.</summary>
		/// <returns>A Boolean value that indicates whether the object specified in the <paramref name="proxy" /> parameter is a transparent proxy or a real object.</returns>
		/// <param name="proxy">The reference to the object to check. </param>
		// Token: 0x06004DC4 RID: 19908
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsTransparentProxy(object proxy);

		// Token: 0x06004DC5 RID: 19909 RVA: 0x00003B29 File Offset: 0x00001D29
		internal static bool ProxyCheckCast(RealProxy rp, RuntimeType castType)
		{
			return true;
		}

		// Token: 0x06004DC6 RID: 19910 RVA: 0x00119180 File Offset: 0x00117380
		internal static IMethodReturnMessage InternalExecuteMessage(MarshalByRefObject target, IMethodCallMessage reqMsg)
		{
			Type type = target.GetType();
			MethodBase methodBase;
			if (reqMsg.MethodBase.DeclaringType == type || reqMsg.MethodBase == RemotingServices.FieldSetterMethod || reqMsg.MethodBase == RemotingServices.FieldGetterMethod)
			{
				methodBase = reqMsg.MethodBase;
			}
			else
			{
				methodBase = RemotingServices.GetVirtualMethod(type, reqMsg.MethodBase);
				if (methodBase == null)
				{
					throw new RemotingException(string.Format("Cannot resolve method {0}:{1}", type, reqMsg.MethodName));
				}
			}
			if (reqMsg.MethodBase.IsGenericMethod)
			{
				Type[] genericArguments = reqMsg.MethodBase.GetGenericArguments();
				methodBase = ((MethodInfo)methodBase).GetGenericMethodDefinition().MakeGenericMethod(genericArguments);
			}
			LogicalCallContext logicalCallContext = CallContext.SetLogicalCallContext(reqMsg.LogicalCallContext);
			ReturnMessage returnMessage;
			try
			{
				object[] array;
				object obj = RemotingServices.InternalExecute(methodBase, target, reqMsg.Args, out array);
				ParameterInfo[] parameters = methodBase.GetParameters();
				object[] array2 = new object[parameters.Length];
				int num = 0;
				int num2 = 0;
				foreach (ParameterInfo parameterInfo in parameters)
				{
					if (parameterInfo.IsOut && !parameterInfo.ParameterType.IsByRef)
					{
						array2[num++] = reqMsg.GetArg(parameterInfo.Position);
					}
					else if (parameterInfo.ParameterType.IsByRef)
					{
						array2[num++] = array[num2++];
					}
					else
					{
						array2[num++] = null;
					}
				}
				LogicalCallContext logicalCallContext2 = Thread.CurrentThread.GetMutableExecutionContext().LogicalCallContext;
				returnMessage = new ReturnMessage(obj, array2, num, logicalCallContext2, reqMsg);
			}
			catch (Exception ex)
			{
				returnMessage = new ReturnMessage(ex, reqMsg);
			}
			CallContext.SetLogicalCallContext(logicalCallContext);
			return returnMessage;
		}

		/// <summary>Connects to the specified remote object, and executes the provided <see cref="T:System.Runtime.Remoting.Messaging.IMethodCallMessage" /> on it.</summary>
		/// <returns>The response of the remote method.</returns>
		/// <param name="target">The remote object whose method you want to call. </param>
		/// <param name="reqMsg">A method call message to the specified remote object's method. </param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">The method was called from a context other than the native context of the object.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06004DC7 RID: 19911 RVA: 0x00119320 File Offset: 0x00117520
		public static IMethodReturnMessage ExecuteMessage(MarshalByRefObject target, IMethodCallMessage reqMsg)
		{
			if (RemotingServices.IsTransparentProxy(target))
			{
				return (IMethodReturnMessage)RemotingServices.GetRealProxy(target).Invoke(reqMsg);
			}
			return RemotingServices.InternalExecuteMessage(target, reqMsg);
		}

		/// <summary>Creates a proxy for a well-known object, given the <see cref="T:System.Type" /> and URL.</summary>
		/// <returns>A proxy to the remote object that points to an endpoint served by the specified well-known object.</returns>
		/// <param name="classToProxy">The <see cref="T:System.Type" /> of a well-known object on the server end to which you want to connect. </param>
		/// <param name="url">The URL of the server class. </param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have permission to configure remoting types and channels. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, RemotingConfiguration" />
		/// </PermissionSet>
		// Token: 0x06004DC8 RID: 19912 RVA: 0x00119343 File Offset: 0x00117543
		[ComVisible(true)]
		public static object Connect(Type classToProxy, string url)
		{
			return RemotingServices.GetRemoteObject(new ObjRef(classToProxy, url, null), classToProxy);
		}

		/// <summary>Creates a proxy for a well-known object, given the <see cref="T:System.Type" />, URL, and channel-specific data.</summary>
		/// <returns>A proxy that points to an endpoint that is served by the requested well-known object.</returns>
		/// <param name="classToProxy">The <see cref="T:System.Type" /> of the well-known object to which you want to connect. </param>
		/// <param name="url">The URL of the well-known object. </param>
		/// <param name="data">Channel specific data. Can be null. </param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have permission to configure remoting types and channels. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, RemotingConfiguration" />
		/// </PermissionSet>
		// Token: 0x06004DC9 RID: 19913 RVA: 0x00119353 File Offset: 0x00117553
		[ComVisible(true)]
		public static object Connect(Type classToProxy, string url, object data)
		{
			return RemotingServices.GetRemoteObject(new ObjRef(classToProxy, url, data), classToProxy);
		}

		/// <summary>Stops an object from receiving any further messages through the registered remoting channels.</summary>
		/// <returns>true if the object was disconnected from the registered remoting channels successfully; otherwise, false.</returns>
		/// <param name="obj">Object to disconnect from its channel. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="obj" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="obj" /> parameter is a proxy. </exception>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have permission to configure remoting types and channels. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06004DCA RID: 19914 RVA: 0x00119364 File Offset: 0x00117564
		public static bool Disconnect(MarshalByRefObject obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			ServerIdentity serverIdentity;
			if (RemotingServices.IsTransparentProxy(obj))
			{
				RealProxy realProxy = RemotingServices.GetRealProxy(obj);
				if (!realProxy.GetProxiedType().IsContextful || !(realProxy.ObjectIdentity is ServerIdentity))
				{
					throw new ArgumentException("The obj parameter is a proxy.");
				}
				serverIdentity = realProxy.ObjectIdentity as ServerIdentity;
			}
			else
			{
				serverIdentity = obj.ObjectIdentity;
				obj.ObjectIdentity = null;
			}
			if (serverIdentity == null || !serverIdentity.IsConnected)
			{
				return false;
			}
			LifetimeServices.StopTrackingLifetime(serverIdentity);
			RemotingServices.DisposeIdentity(serverIdentity);
			TrackingServices.NotifyDisconnectedObject(obj);
			return true;
		}

		/// <summary>Returns the <see cref="T:System.Type" /> of the object with the specified URI.</summary>
		/// <returns>The <see cref="T:System.Type" /> of the object with the specified URI.</returns>
		/// <param name="URI">The URI of the object whose <see cref="T:System.Type" /> is requested. </param>
		/// <exception cref="T:System.Security.SecurityException">Either the immediate caller does not have infrastructure permission, or at least one of the callers higher in the callstack does not have permission to retrieve the type information of non-public members. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06004DCB RID: 19915 RVA: 0x001193F0 File Offset: 0x001175F0
		public static Type GetServerTypeForUri(string URI)
		{
			ServerIdentity serverIdentity = RemotingServices.GetIdentityForUri(URI) as ServerIdentity;
			if (serverIdentity == null)
			{
				return null;
			}
			return serverIdentity.ObjectType;
		}

		/// <summary>Retrieves the URI for the specified object.</summary>
		/// <returns>The URI of the specified object if it has one, or null if the object has not yet been marshaled.</returns>
		/// <param name="obj">The <see cref="T:System.MarshalByRefObject" /> for which a URI is requested. </param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06004DCC RID: 19916 RVA: 0x00119414 File Offset: 0x00117614
		public static string GetObjectUri(MarshalByRefObject obj)
		{
			Identity objectIdentity = RemotingServices.GetObjectIdentity(obj);
			if (objectIdentity is ClientIdentity)
			{
				return ((ClientIdentity)objectIdentity).TargetUri;
			}
			if (objectIdentity != null)
			{
				return objectIdentity.ObjectUri;
			}
			return null;
		}

		/// <summary>Takes a <see cref="T:System.Runtime.Remoting.ObjRef" /> and creates a proxy object out of it.</summary>
		/// <returns>A proxy to the object that the given <see cref="T:System.Runtime.Remoting.ObjRef" /> represents.</returns>
		/// <param name="objectRef">The <see cref="T:System.Runtime.Remoting.ObjRef" /> that represents the remote object for which the proxy is being created. </param>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Runtime.Remoting.ObjRef" /> instance specified in the <paramref name="objectRef" /> parameter is not well-formed. </exception>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the callstack does not have permission to configure remoting types and channels. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration, Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06004DCD RID: 19917 RVA: 0x00119447 File Offset: 0x00117647
		public static object Unmarshal(ObjRef objectRef)
		{
			return RemotingServices.Unmarshal(objectRef, true);
		}

		/// <summary>Takes a <see cref="T:System.Runtime.Remoting.ObjRef" /> and creates a proxy object out of it, refining it to the type on the server.</summary>
		/// <returns>A proxy to the object that the given <see cref="T:System.Runtime.Remoting.ObjRef" /> represents.</returns>
		/// <param name="objectRef">The <see cref="T:System.Runtime.Remoting.ObjRef" /> that represents the remote object for which the proxy is being created. </param>
		/// <param name="fRefine">true to refine the proxy to the type on the server; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Runtime.Remoting.ObjRef" /> instance specified in the <paramref name="objectRef" /> parameter is not well-formed. </exception>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the callstack does not have permission to configure remoting types and channels. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration, Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06004DCE RID: 19918 RVA: 0x00119450 File Offset: 0x00117650
		public static object Unmarshal(ObjRef objectRef, bool fRefine)
		{
			Type type = (fRefine ? objectRef.ServerType : typeof(MarshalByRefObject));
			if (type == null)
			{
				type = typeof(MarshalByRefObject);
			}
			if (objectRef.IsReferenceToWellKnow)
			{
				object remoteObject = RemotingServices.GetRemoteObject(objectRef, type);
				TrackingServices.NotifyUnmarshaledObject(remoteObject, objectRef);
				return remoteObject;
			}
			if (type.IsContextful)
			{
				ProxyAttribute proxyAttribute = (ProxyAttribute)Attribute.GetCustomAttribute(type, typeof(ProxyAttribute), true);
				if (proxyAttribute != null)
				{
					object transparentProxy = proxyAttribute.CreateProxy(objectRef, type, null, null).GetTransparentProxy();
					TrackingServices.NotifyUnmarshaledObject(transparentProxy, objectRef);
					return transparentProxy;
				}
			}
			object proxyForRemoteObject = RemotingServices.GetProxyForRemoteObject(objectRef, type);
			TrackingServices.NotifyUnmarshaledObject(proxyForRemoteObject, objectRef);
			return proxyForRemoteObject;
		}

		/// <summary>Takes a <see cref="T:System.MarshalByRefObject" />, registers it with the remoting infrastructure, and converts it into an instance of the <see cref="T:System.Runtime.Remoting.ObjRef" /> class.</summary>
		/// <returns>An instance of the <see cref="T:System.Runtime.Remoting.ObjRef" /> class that represents the object specified in the <paramref name="Obj" /> parameter.</returns>
		/// <param name="Obj">The object to convert. </param>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">The <paramref name="Obj" /> parameter is an object proxy. </exception>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the callstack does not have permission to configure remoting types and channels. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration" />
		/// </PermissionSet>
		// Token: 0x06004DCF RID: 19919 RVA: 0x001194E5 File Offset: 0x001176E5
		public static ObjRef Marshal(MarshalByRefObject Obj)
		{
			return RemotingServices.Marshal(Obj, null, null);
		}

		/// <summary>Converts the given <see cref="T:System.MarshalByRefObject" /> into an instance of the <see cref="T:System.Runtime.Remoting.ObjRef" /> class with the specified URI.</summary>
		/// <returns>An instance of the <see cref="T:System.Runtime.Remoting.ObjRef" /> class that represents the object specified in the <paramref name="Obj" /> parameter.</returns>
		/// <param name="Obj">The object to convert. </param>
		/// <param name="URI">The specified URI with which to initialize the new <see cref="T:System.Runtime.Remoting.ObjRef" />. Can be null. </param>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">
		///   <paramref name="Obj" /> is an object proxy, and the <paramref name="URI" /> parameter is not null. </exception>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the callstack does not have permission to configure remoting types and channels. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration" />
		/// </PermissionSet>
		// Token: 0x06004DD0 RID: 19920 RVA: 0x001194EF File Offset: 0x001176EF
		public static ObjRef Marshal(MarshalByRefObject Obj, string URI)
		{
			return RemotingServices.Marshal(Obj, URI, null);
		}

		/// <summary>Takes a <see cref="T:System.MarshalByRefObject" /> and converts it into an instance of the <see cref="T:System.Runtime.Remoting.ObjRef" /> class with the specified URI, and the provided <see cref="T:System.Type" />.</summary>
		/// <returns>An instance of the <see cref="T:System.Runtime.Remoting.ObjRef" /> class that represents the object specified in the <paramref name="Obj" /> parameter.</returns>
		/// <param name="Obj">The object to convert into a <see cref="T:System.Runtime.Remoting.ObjRef" />. </param>
		/// <param name="ObjURI">The URI the object specified in the <paramref name="Obj" /> parameter is marshaled with. Can be null. </param>
		/// <param name="RequestedType">The <see cref="T:System.Type" /><paramref name="Obj" /> is marshaled as. Can be null. </param>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">
		///   <paramref name="Obj" /> is a proxy of a remote object, and the <paramref name="ObjUri" /> parameter is not null. </exception>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the callstack does not have permission to configure remoting types and channels. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration" />
		/// </PermissionSet>
		// Token: 0x06004DD1 RID: 19921 RVA: 0x001194FC File Offset: 0x001176FC
		public static ObjRef Marshal(MarshalByRefObject Obj, string ObjURI, Type RequestedType)
		{
			if (RemotingServices.IsTransparentProxy(Obj))
			{
				RealProxy realProxy = RemotingServices.GetRealProxy(Obj);
				Identity objectIdentity = realProxy.ObjectIdentity;
				if (objectIdentity != null)
				{
					if (realProxy.GetProxiedType().IsContextful && !objectIdentity.IsConnected)
					{
						ClientActivatedIdentity clientActivatedIdentity = (ClientActivatedIdentity)objectIdentity;
						if (ObjURI == null)
						{
							ObjURI = RemotingServices.NewUri();
						}
						clientActivatedIdentity.ObjectUri = ObjURI;
						RemotingServices.RegisterServerIdentity(clientActivatedIdentity);
						clientActivatedIdentity.StartTrackingLifetime((ILease)Obj.InitializeLifetimeService());
						return clientActivatedIdentity.CreateObjRef(RequestedType);
					}
					if (ObjURI != null)
					{
						throw new RemotingException("It is not possible marshal a proxy of a remote object.");
					}
					ObjRef objRef = realProxy.ObjectIdentity.CreateObjRef(RequestedType);
					TrackingServices.NotifyMarshaledObject(Obj, objRef);
					return objRef;
				}
			}
			if (RequestedType == null)
			{
				RequestedType = Obj.GetType();
			}
			if (ObjURI == null)
			{
				if (Obj.ObjectIdentity == null)
				{
					ObjURI = RemotingServices.NewUri();
					RemotingServices.CreateClientActivatedServerIdentity(Obj, RequestedType, ObjURI);
				}
			}
			else
			{
				ClientActivatedIdentity clientActivatedIdentity2 = RemotingServices.GetIdentityForUri("/" + ObjURI) as ClientActivatedIdentity;
				if (clientActivatedIdentity2 == null || Obj != clientActivatedIdentity2.GetServerObject())
				{
					RemotingServices.CreateClientActivatedServerIdentity(Obj, RequestedType, ObjURI);
				}
			}
			ObjRef objRef2;
			if (RemotingServices.IsTransparentProxy(Obj))
			{
				objRef2 = RemotingServices.GetRealProxy(Obj).ObjectIdentity.CreateObjRef(RequestedType);
			}
			else
			{
				objRef2 = Obj.CreateObjRef(RequestedType);
			}
			TrackingServices.NotifyMarshaledObject(Obj, objRef2);
			return objRef2;
		}

		// Token: 0x06004DD2 RID: 19922 RVA: 0x00119618 File Offset: 0x00117818
		private static string NewUri()
		{
			if (RemotingServices.app_id == null)
			{
				object obj = RemotingServices.app_id_lock;
				lock (obj)
				{
					if (RemotingServices.app_id == null)
					{
						RemotingServices.app_id = Guid.NewGuid().ToString().Replace('-', '_') + "/";
					}
				}
			}
			int num = Interlocked.Increment(ref RemotingServices.next_id);
			return string.Concat(new object[]
			{
				RemotingServices.app_id,
				Environment.TickCount.ToString("x"),
				"_",
				num,
				".rem"
			});
		}

		/// <summary>Returns the real proxy backing the specified transparent proxy.</summary>
		/// <returns>The real proxy instance backing the transparent proxy.</returns>
		/// <param name="proxy">A transparent proxy. </param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06004DD3 RID: 19923 RVA: 0x001196D8 File Offset: 0x001178D8
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static RealProxy GetRealProxy(object proxy)
		{
			if (!RemotingServices.IsTransparentProxy(proxy))
			{
				throw new RemotingException("Cannot get the real proxy from an object that is not a transparent proxy.");
			}
			return ((TransparentProxy)proxy)._rp;
		}

		/// <summary>Returns the method base from the given <see cref="T:System.Runtime.Remoting.Messaging.IMethodMessage" />.</summary>
		/// <returns>The method base extracted from the <paramref name="msg" /> parameter.</returns>
		/// <param name="msg">The method message to extract the method base from. </param>
		/// <exception cref="T:System.Security.SecurityException">Either the immediate caller does not have infrastructure permission, or at least one of the callers higher in the callstack does not have permission to retrieve the type information of non-public members. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06004DD4 RID: 19924 RVA: 0x001196F8 File Offset: 0x001178F8
		public static MethodBase GetMethodBaseFromMethodMessage(IMethodMessage msg)
		{
			Type type = Type.GetType(msg.TypeName);
			if (type == null)
			{
				throw new RemotingException("Type '" + msg.TypeName + "' not found.");
			}
			return RemotingServices.GetMethodBaseFromName(type, msg.MethodName, (Type[])msg.MethodSignature);
		}

		// Token: 0x06004DD5 RID: 19925 RVA: 0x0011974C File Offset: 0x0011794C
		internal static MethodBase GetMethodBaseFromName(Type type, string methodName, Type[] signature)
		{
			if (type.IsInterface)
			{
				return RemotingServices.FindInterfaceMethod(type, methodName, signature);
			}
			MethodBase methodBase;
			if (signature == null)
			{
				methodBase = type.GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			}
			else
			{
				methodBase = type.GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, signature, null);
			}
			if (methodBase != null)
			{
				return methodBase;
			}
			if (methodName == "FieldSetter")
			{
				return RemotingServices.FieldSetterMethod;
			}
			if (methodName == "FieldGetter")
			{
				return RemotingServices.FieldGetterMethod;
			}
			if (signature == null)
			{
				return type.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, Type.EmptyTypes, null);
			}
			return type.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, signature, null);
		}

		// Token: 0x06004DD6 RID: 19926 RVA: 0x001197D8 File Offset: 0x001179D8
		private static MethodBase FindInterfaceMethod(Type type, string methodName, Type[] signature)
		{
			MethodBase methodBase;
			if (signature == null)
			{
				methodBase = type.GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			}
			else
			{
				methodBase = type.GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, signature, null);
			}
			if (methodBase != null)
			{
				return methodBase;
			}
			Type[] interfaces = type.GetInterfaces();
			for (int i = 0; i < interfaces.Length; i++)
			{
				methodBase = RemotingServices.FindInterfaceMethod(interfaces[i], methodName, signature);
				if (methodBase != null)
				{
					return methodBase;
				}
			}
			return null;
		}

		/// <summary>Serializes the specified marshal by reference object into the provided <see cref="T:System.Runtime.Serialization.SerializationInfo" />.</summary>
		/// <param name="obj">The object to serialize. </param>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> into which the object is serialized. </param>
		/// <param name="context">The source and destination of the serialization. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="obj" /> or <paramref name="info" /> parameter is null. </exception>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06004DD7 RID: 19927 RVA: 0x0011983A File Offset: 0x00117A3A
		public static void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			RemotingServices.Marshal((MarshalByRefObject)obj).GetObjectData(info, context);
		}

		/// <summary>Returns the <see cref="T:System.Runtime.Remoting.ObjRef" /> that represents the remote object from the specified proxy.</summary>
		/// <returns>A <see cref="T:System.Runtime.Remoting.ObjRef" /> that represents the remote object the specified proxy is connected to, or null if the object or proxy have not been marshaled.</returns>
		/// <param name="obj">A proxy connected to the object you want to create a <see cref="T:System.Runtime.Remoting.ObjRef" /> for. </param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06004DD8 RID: 19928 RVA: 0x0011985C File Offset: 0x00117A5C
		public static ObjRef GetObjRefForProxy(MarshalByRefObject obj)
		{
			Identity objectIdentity = RemotingServices.GetObjectIdentity(obj);
			if (objectIdentity == null)
			{
				return null;
			}
			return objectIdentity.CreateObjRef(null);
		}

		/// <summary>Returns a lifetime service object that controls the lifetime policy of the specified object.</summary>
		/// <returns>The object that controls the lifetime of <paramref name="obj" />.</returns>
		/// <param name="obj">The object to obtain lifetime service for. </param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration" />
		/// </PermissionSet>
		// Token: 0x06004DD9 RID: 19929 RVA: 0x0011987C File Offset: 0x00117A7C
		public static object GetLifetimeService(MarshalByRefObject obj)
		{
			if (obj == null)
			{
				return null;
			}
			return obj.GetLifetimeService();
		}

		/// <summary>Returns a chain of envoy sinks that should be used when sending messages to the remote object represented by the specified proxy.</summary>
		/// <returns>A chain of envoy sinks associated with the specified proxy.</returns>
		/// <param name="obj">The proxy of the remote object that requested envoy sinks are associated with. </param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06004DDA RID: 19930 RVA: 0x00119889 File Offset: 0x00117A89
		public static IMessageSink GetEnvoyChainForProxy(MarshalByRefObject obj)
		{
			if (RemotingServices.IsTransparentProxy(obj))
			{
				return ((ClientIdentity)RemotingServices.GetRealProxy(obj).ObjectIdentity).EnvoySink;
			}
			throw new ArgumentException("obj must be a proxy.", "obj");
		}

		/// <summary>Logs the stage in a remoting exchange to an external debugger.</summary>
		/// <param name="stage">An internally defined constant that identifies the stage in a remoting exchange.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06004DDB RID: 19931 RVA: 0x0002126B File Offset: 0x0001F46B
		[Obsolete("It existed for only internal use in .NET and unimplemented in mono")]
		[MonoTODO]
		[Conditional("REMOTING_PERF")]
		public static void LogRemotingStage(int stage)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves a session ID for a message.</summary>
		/// <returns>A session ID string that uniquely identifies the current session.</returns>
		/// <param name="msg">The <see cref="T:System.Runtime.Remoting.Messaging.IMethodMessage" /> for which a session ID is requested. </param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06004DDC RID: 19932 RVA: 0x001198B8 File Offset: 0x00117AB8
		public static string GetSessionIdForMethodMessage(IMethodMessage msg)
		{
			return msg.Uri;
		}

		/// <summary>Returns a Boolean value that indicates whether the method in the given message is overloaded.</summary>
		/// <returns>true if the method called in <paramref name="msg" /> is overloaded; otherwise, false.</returns>
		/// <param name="msg">The message that contains a call to the method in question. </param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06004DDD RID: 19933 RVA: 0x001198C0 File Offset: 0x00117AC0
		public static bool IsMethodOverloaded(IMethodMessage msg)
		{
			RuntimeType runtimeType = (RuntimeType)msg.MethodBase.DeclaringType;
			return runtimeType.GetMethodsByName(msg.MethodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, false, runtimeType).Length > 1;
		}

		/// <summary>Returns a Boolean value that indicates whether the object specified by the given transparent proxy is contained in a different application domain than the object that called the current method.</summary>
		/// <returns>true if the object is out of the current application domain; otherwise, false.</returns>
		/// <param name="tp">The object to check. </param>
		// Token: 0x06004DDE RID: 19934 RVA: 0x001198F4 File Offset: 0x00117AF4
		public static bool IsObjectOutOfAppDomain(object tp)
		{
			MarshalByRefObject marshalByRefObject = tp as MarshalByRefObject;
			return marshalByRefObject != null && RemotingServices.GetObjectIdentity(marshalByRefObject) is ClientIdentity;
		}

		/// <summary>Returns a Boolean value that indicates whether the object represented by the given proxy is contained in a different context than the object that called the current method.</summary>
		/// <returns>true if the object is out of the current context; otherwise, false.</returns>
		/// <param name="tp">The object to check. </param>
		// Token: 0x06004DDF RID: 19935 RVA: 0x0011991C File Offset: 0x00117B1C
		public static bool IsObjectOutOfContext(object tp)
		{
			MarshalByRefObject marshalByRefObject = tp as MarshalByRefObject;
			if (marshalByRefObject == null)
			{
				return false;
			}
			Identity objectIdentity = RemotingServices.GetObjectIdentity(marshalByRefObject);
			if (objectIdentity == null)
			{
				return false;
			}
			ServerIdentity serverIdentity = objectIdentity as ServerIdentity;
			return serverIdentity == null || serverIdentity.Context != Thread.CurrentContext;
		}

		/// <summary>Returns a Boolean value that indicates whether the client that called the method specified in the given message is waiting for the server to finish processing the method before continuing execution.</summary>
		/// <returns>true if the method is one way; otherwise, false.</returns>
		/// <param name="method">The method in question. </param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06004DE0 RID: 19936 RVA: 0x0011995D File Offset: 0x00117B5D
		public static bool IsOneWay(MethodBase method)
		{
			return method.IsDefined(typeof(OneWayAttribute), false);
		}

		// Token: 0x06004DE1 RID: 19937 RVA: 0x00119970 File Offset: 0x00117B70
		internal static bool IsAsyncMessage(IMessage msg)
		{
			return msg is MonoMethodMessage && (((MonoMethodMessage)msg).IsAsync || RemotingServices.IsOneWay(((MonoMethodMessage)msg).MethodBase));
		}

		/// <summary>Sets the URI for the subsequent call to the <see cref="M:System.Runtime.Remoting.RemotingServices.Marshal(System.MarshalByRefObject)" /> method.</summary>
		/// <param name="obj">The object to set a URI for. </param>
		/// <param name="uri">The URI to assign to the specified object. </param>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">
		///   <paramref name="obj" /> is not a local object, has already been marshaled, or the current method has already been called on. </exception>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the callstack does not have permission to configure remoting types and channels. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration" />
		/// </PermissionSet>
		// Token: 0x06004DE2 RID: 19938 RVA: 0x001199A0 File Offset: 0x00117BA0
		public static void SetObjectUriForMarshal(MarshalByRefObject obj, string uri)
		{
			if (RemotingServices.IsTransparentProxy(obj))
			{
				RealProxy realProxy = RemotingServices.GetRealProxy(obj);
				Identity objectIdentity = realProxy.ObjectIdentity;
				if (objectIdentity != null && !(objectIdentity is ServerIdentity) && !realProxy.GetProxiedType().IsContextful)
				{
					throw new RemotingException("SetObjectUriForMarshal method should only be called for MarshalByRefObjects that exist in the current AppDomain.");
				}
			}
			RemotingServices.Marshal(obj, uri);
		}

		// Token: 0x06004DE3 RID: 19939 RVA: 0x001199F0 File Offset: 0x00117BF0
		internal static object CreateClientProxy(ActivatedClientTypeEntry entry, object[] activationAttributes)
		{
			if (entry.ContextAttributes != null || activationAttributes != null)
			{
				ArrayList arrayList = new ArrayList();
				if (entry.ContextAttributes != null)
				{
					arrayList.AddRange(entry.ContextAttributes);
				}
				if (activationAttributes != null)
				{
					arrayList.AddRange(activationAttributes);
				}
				return RemotingServices.CreateClientProxy(entry.ObjectType, entry.ApplicationUrl, arrayList.ToArray());
			}
			return RemotingServices.CreateClientProxy(entry.ObjectType, entry.ApplicationUrl, null);
		}

		// Token: 0x06004DE4 RID: 19940 RVA: 0x00119A58 File Offset: 0x00117C58
		internal static object CreateClientProxy(Type objectType, string url, object[] activationAttributes)
		{
			string text = url;
			if (!text.EndsWith("/"))
			{
				text += "/";
			}
			text += "RemoteActivationService.rem";
			string text2;
			RemotingServices.GetClientChannelSinkChain(text, null, out text2);
			return new RemotingProxy(objectType, text, activationAttributes).GetTransparentProxy();
		}

		// Token: 0x06004DE5 RID: 19941 RVA: 0x00119AA3 File Offset: 0x00117CA3
		internal static object CreateClientProxy(WellKnownClientTypeEntry entry)
		{
			return RemotingServices.Connect(entry.ObjectType, entry.ObjectUrl, null);
		}

		// Token: 0x06004DE6 RID: 19942 RVA: 0x00119AB8 File Offset: 0x00117CB8
		internal static object CreateClientProxyForContextBound(Type type, object[] activationAttributes)
		{
			if (type.IsContextful)
			{
				ProxyAttribute proxyAttribute = (ProxyAttribute)Attribute.GetCustomAttribute(type, typeof(ProxyAttribute), true);
				if (proxyAttribute != null)
				{
					return proxyAttribute.CreateInstance(type);
				}
			}
			return new RemotingProxy(type, ChannelServices.CrossContextUrl, activationAttributes).GetTransparentProxy();
		}

		// Token: 0x06004DE7 RID: 19943 RVA: 0x00119B00 File Offset: 0x00117D00
		internal static object CreateClientProxyForComInterop(Type type)
		{
			return ComInteropProxy.CreateProxy(type).GetTransparentProxy();
		}

		// Token: 0x06004DE8 RID: 19944 RVA: 0x00119B10 File Offset: 0x00117D10
		internal static Identity GetIdentityForUri(string uri)
		{
			string text = RemotingServices.GetNormalizedUri(uri);
			Hashtable hashtable = RemotingServices.uri_hash;
			Identity identity2;
			lock (hashtable)
			{
				Identity identity = (Identity)RemotingServices.uri_hash[text];
				if (identity == null)
				{
					text = RemotingServices.RemoveAppNameFromUri(uri);
					if (text != null)
					{
						identity = (Identity)RemotingServices.uri_hash[text];
					}
				}
				identity2 = identity;
			}
			return identity2;
		}

		// Token: 0x06004DE9 RID: 19945 RVA: 0x00119B84 File Offset: 0x00117D84
		private static string RemoveAppNameFromUri(string uri)
		{
			string text = RemotingConfiguration.ApplicationName;
			if (text == null)
			{
				return null;
			}
			text = "/" + text + "/";
			if (uri.StartsWith(text))
			{
				return uri.Substring(text.Length);
			}
			return null;
		}

		// Token: 0x06004DEA RID: 19946 RVA: 0x00119BC4 File Offset: 0x00117DC4
		internal static Identity GetObjectIdentity(MarshalByRefObject obj)
		{
			if (RemotingServices.IsTransparentProxy(obj))
			{
				return RemotingServices.GetRealProxy(obj).ObjectIdentity;
			}
			return obj.ObjectIdentity;
		}

		// Token: 0x06004DEB RID: 19947 RVA: 0x00119BE0 File Offset: 0x00117DE0
		internal static ClientIdentity GetOrCreateClientIdentity(ObjRef objRef, Type proxyType, out object clientProxy)
		{
			object obj = ((objRef.ChannelInfo != null) ? objRef.ChannelInfo.ChannelData : null);
			string uri;
			IMessageSink clientChannelSinkChain = RemotingServices.GetClientChannelSinkChain(objRef.URI, obj, out uri);
			if (uri == null)
			{
				uri = objRef.URI;
			}
			Hashtable hashtable = RemotingServices.uri_hash;
			ClientIdentity clientIdentity2;
			lock (hashtable)
			{
				clientProxy = null;
				string normalizedUri = RemotingServices.GetNormalizedUri(objRef.URI);
				ClientIdentity clientIdentity = RemotingServices.uri_hash[normalizedUri] as ClientIdentity;
				if (clientIdentity != null)
				{
					clientProxy = clientIdentity.ClientProxy;
					if (clientProxy != null)
					{
						return clientIdentity;
					}
					RemotingServices.DisposeIdentity(clientIdentity);
				}
				clientIdentity = new ClientIdentity(uri, objRef);
				clientIdentity.ChannelSink = clientChannelSinkChain;
				RemotingServices.uri_hash[normalizedUri] = clientIdentity;
				if (proxyType != null)
				{
					RemotingProxy remotingProxy = new RemotingProxy(proxyType, clientIdentity);
					CrossAppDomainSink crossAppDomainSink = clientChannelSinkChain as CrossAppDomainSink;
					if (crossAppDomainSink != null)
					{
						remotingProxy.SetTargetDomain(crossAppDomainSink.TargetDomainId);
					}
					clientProxy = remotingProxy.GetTransparentProxy();
					clientIdentity.ClientProxy = (MarshalByRefObject)clientProxy;
				}
				clientIdentity2 = clientIdentity;
			}
			return clientIdentity2;
		}

		// Token: 0x06004DEC RID: 19948 RVA: 0x00119CF8 File Offset: 0x00117EF8
		private static IMessageSink GetClientChannelSinkChain(string url, object channelData, out string objectUri)
		{
			IMessageSink messageSink = ChannelServices.CreateClientChannelSinkChain(url, channelData, out objectUri);
			if (messageSink != null)
			{
				return messageSink;
			}
			if (url != null)
			{
				throw new RemotingException(string.Format("Cannot create channel sink to connect to URL {0}. An appropriate channel has probably not been registered.", url));
			}
			throw new RemotingException(string.Format("Cannot create channel sink to connect to the remote object. An appropriate channel has probably not been registered.", url));
		}

		// Token: 0x06004DED RID: 19949 RVA: 0x00119D2A File Offset: 0x00117F2A
		internal static ClientActivatedIdentity CreateContextBoundObjectIdentity(Type objectType)
		{
			return new ClientActivatedIdentity(null, objectType)
			{
				ChannelSink = ChannelServices.CrossContextChannel
			};
		}

		// Token: 0x06004DEE RID: 19950 RVA: 0x00119D3E File Offset: 0x00117F3E
		internal static ClientActivatedIdentity CreateClientActivatedServerIdentity(MarshalByRefObject realObject, Type objectType, string objectUri)
		{
			ClientActivatedIdentity clientActivatedIdentity = new ClientActivatedIdentity(objectUri, objectType);
			clientActivatedIdentity.AttachServerObject(realObject, Context.DefaultContext);
			RemotingServices.RegisterServerIdentity(clientActivatedIdentity);
			clientActivatedIdentity.StartTrackingLifetime((ILease)realObject.InitializeLifetimeService());
			return clientActivatedIdentity;
		}

		// Token: 0x06004DEF RID: 19951 RVA: 0x00119D6C File Offset: 0x00117F6C
		internal static ServerIdentity CreateWellKnownServerIdentity(Type objectType, string objectUri, WellKnownObjectMode mode)
		{
			ServerIdentity serverIdentity;
			if (mode == WellKnownObjectMode.SingleCall)
			{
				serverIdentity = new SingleCallIdentity(objectUri, Context.DefaultContext, objectType);
			}
			else
			{
				serverIdentity = new SingletonIdentity(objectUri, Context.DefaultContext, objectType);
			}
			RemotingServices.RegisterServerIdentity(serverIdentity);
			return serverIdentity;
		}

		// Token: 0x06004DF0 RID: 19952 RVA: 0x00119DA0 File Offset: 0x00117FA0
		private static void RegisterServerIdentity(ServerIdentity identity)
		{
			Hashtable hashtable = RemotingServices.uri_hash;
			lock (hashtable)
			{
				if (RemotingServices.uri_hash.ContainsKey(identity.ObjectUri))
				{
					throw new RemotingException("Uri already in use: " + identity.ObjectUri + ".");
				}
				RemotingServices.uri_hash[identity.ObjectUri] = identity;
			}
		}

		// Token: 0x06004DF1 RID: 19953 RVA: 0x00119E18 File Offset: 0x00118018
		internal static object GetProxyForRemoteObject(ObjRef objref, Type classToProxy)
		{
			ClientActivatedIdentity clientActivatedIdentity = RemotingServices.GetIdentityForUri(objref.URI) as ClientActivatedIdentity;
			if (clientActivatedIdentity != null)
			{
				return clientActivatedIdentity.GetServerObject();
			}
			return RemotingServices.GetRemoteObject(objref, classToProxy);
		}

		// Token: 0x06004DF2 RID: 19954 RVA: 0x00119E48 File Offset: 0x00118048
		internal static object GetRemoteObject(ObjRef objRef, Type proxyType)
		{
			object obj;
			RemotingServices.GetOrCreateClientIdentity(objRef, proxyType, out obj);
			return obj;
		}

		// Token: 0x06004DF3 RID: 19955 RVA: 0x00119E60 File Offset: 0x00118060
		internal static object GetServerObject(string uri)
		{
			ClientActivatedIdentity clientActivatedIdentity = RemotingServices.GetIdentityForUri(uri) as ClientActivatedIdentity;
			if (clientActivatedIdentity == null)
			{
				throw new RemotingException("Server for uri '" + uri + "' not found");
			}
			return clientActivatedIdentity.GetServerObject();
		}

		// Token: 0x06004DF4 RID: 19956 RVA: 0x00119E8C File Offset: 0x0011808C
		[SecurityPermission(SecurityAction.Assert, SerializationFormatter = true)]
		internal static byte[] SerializeCallData(object obj)
		{
			LogicalCallContext.Reader logicalCallContext = Thread.CurrentThread.GetExecutionContextReader().LogicalCallContext;
			if (!logicalCallContext.IsNull)
			{
				obj = new RemotingServices.CACD
				{
					d = obj,
					c = logicalCallContext.Clone()
				};
			}
			if (obj == null)
			{
				return null;
			}
			MemoryStream memoryStream = new MemoryStream();
			RemotingServices._serializationFormatter.Serialize(memoryStream, obj);
			return memoryStream.ToArray();
		}

		// Token: 0x06004DF5 RID: 19957 RVA: 0x00119EF0 File Offset: 0x001180F0
		[SecurityPermission(SecurityAction.Assert, SerializationFormatter = true)]
		internal static object DeserializeCallData(byte[] array)
		{
			if (array == null)
			{
				return null;
			}
			MemoryStream memoryStream = new MemoryStream(array);
			object obj = RemotingServices._deserializationFormatter.Deserialize(memoryStream);
			if (obj is RemotingServices.CACD)
			{
				RemotingServices.CACD cacd = (RemotingServices.CACD)obj;
				obj = cacd.d;
				LogicalCallContext logicalCallContext = (LogicalCallContext)cacd.c;
				if (logicalCallContext.HasInfo)
				{
					Thread.CurrentThread.GetMutableExecutionContext().LogicalCallContext.Merge(logicalCallContext);
				}
			}
			return obj;
		}

		// Token: 0x06004DF6 RID: 19958 RVA: 0x00119F54 File Offset: 0x00118154
		[SecurityPermission(SecurityAction.Assert, SerializationFormatter = true)]
		internal static byte[] SerializeExceptionData(Exception ex)
		{
			byte[] array = null;
			try
			{
			}
			finally
			{
				MemoryStream memoryStream = new MemoryStream();
				RemotingServices._serializationFormatter.Serialize(memoryStream, ex);
				array = memoryStream.ToArray();
			}
			return array;
		}

		// Token: 0x06004DF7 RID: 19959 RVA: 0x00119F90 File Offset: 0x00118190
		internal static object GetDomainProxy(AppDomain domain)
		{
			byte[] array = null;
			Context currentContext = Thread.CurrentContext;
			try
			{
				array = (byte[])AppDomain.InvokeInDomain(domain, typeof(AppDomain).GetMethod("GetMarshalledDomainObjRef", BindingFlags.Instance | BindingFlags.NonPublic), domain, null);
			}
			finally
			{
				AppDomain.InternalSetContext(currentContext);
			}
			byte[] array2 = new byte[array.Length];
			array.CopyTo(array2, 0);
			return (AppDomain)RemotingServices.Unmarshal((ObjRef)CADSerializer.DeserializeObject(new MemoryStream(array2)));
		}

		// Token: 0x06004DF8 RID: 19960 RVA: 0x0011A010 File Offset: 0x00118210
		private static void RegisterInternalChannels()
		{
			CrossAppDomainChannel.RegisterCrossAppDomainChannel();
		}

		// Token: 0x06004DF9 RID: 19961 RVA: 0x0011A018 File Offset: 0x00118218
		internal static void DisposeIdentity(Identity ident)
		{
			Hashtable hashtable = RemotingServices.uri_hash;
			lock (hashtable)
			{
				if (!ident.Disposed)
				{
					ClientIdentity clientIdentity = ident as ClientIdentity;
					if (clientIdentity != null)
					{
						RemotingServices.uri_hash.Remove(RemotingServices.GetNormalizedUri(clientIdentity.TargetUri));
					}
					else
					{
						RemotingServices.uri_hash.Remove(ident.ObjectUri);
					}
					ident.Disposed = true;
				}
			}
		}

		// Token: 0x06004DFA RID: 19962 RVA: 0x0011A094 File Offset: 0x00118294
		internal static Identity GetMessageTargetIdentity(IMessage msg)
		{
			if (msg is IInternalMessage)
			{
				return ((IInternalMessage)msg).TargetIdentity;
			}
			Hashtable hashtable = RemotingServices.uri_hash;
			Identity identity;
			lock (hashtable)
			{
				string normalizedUri = RemotingServices.GetNormalizedUri(((IMethodMessage)msg).Uri);
				identity = RemotingServices.uri_hash[normalizedUri] as ServerIdentity;
			}
			return identity;
		}

		// Token: 0x06004DFB RID: 19963 RVA: 0x0011A104 File Offset: 0x00118304
		internal static void SetMessageTargetIdentity(IMessage msg, Identity ident)
		{
			if (msg is IInternalMessage)
			{
				((IInternalMessage)msg).TargetIdentity = ident;
			}
		}

		// Token: 0x06004DFC RID: 19964 RVA: 0x0011A11C File Offset: 0x0011831C
		internal static bool UpdateOutArgObject(ParameterInfo pi, object local, object remote)
		{
			if (pi.ParameterType.IsArray && ((Array)local).Rank == 1)
			{
				Array array = (Array)local;
				if (array.Rank == 1)
				{
					Array.Copy((Array)remote, array, array.Length);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06004DFD RID: 19965 RVA: 0x0011A169 File Offset: 0x00118369
		private static string GetNormalizedUri(string uri)
		{
			if (uri.StartsWith("/"))
			{
				return uri.Substring(1);
			}
			return uri;
		}

		// Token: 0x040029C4 RID: 10692
		private static Hashtable uri_hash = new Hashtable();

		// Token: 0x040029C5 RID: 10693
		private static BinaryFormatter _serializationFormatter;

		// Token: 0x040029C6 RID: 10694
		private static BinaryFormatter _deserializationFormatter;

		// Token: 0x040029C7 RID: 10695
		private static string app_id;

		// Token: 0x040029C8 RID: 10696
		private static readonly object app_id_lock = new object();

		// Token: 0x040029C9 RID: 10697
		private static int next_id = 1;

		// Token: 0x040029CA RID: 10698
		private const BindingFlags methodBindings = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

		// Token: 0x040029CB RID: 10699
		private static readonly MethodInfo FieldSetterMethod;

		// Token: 0x040029CC RID: 10700
		private static readonly MethodInfo FieldGetterMethod;

		// Token: 0x0200075B RID: 1883
		[Serializable]
		private class CACD
		{
			// Token: 0x040029CD RID: 10701
			public object d;

			// Token: 0x040029CE RID: 10702
			public object c;
		}
	}
}
