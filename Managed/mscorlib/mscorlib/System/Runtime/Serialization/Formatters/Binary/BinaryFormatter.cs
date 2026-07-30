using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	/// <summary>Serializes and deserializes an object, or an entire graph of connected objects, in binary format.</summary>
	// Token: 0x0200072E RID: 1838
	[ComVisible(true)]
	public sealed class BinaryFormatter : IRemotingFormatter, IFormatter
	{
		/// <summary>Gets or sets the format in which type descriptions are laid out in the serialized stream.</summary>
		/// <returns>The style of type layouts to use.</returns>
		// Token: 0x17000CBB RID: 3259
		// (get) Token: 0x06004BEE RID: 19438 RVA: 0x0010F5DF File Offset: 0x0010D7DF
		// (set) Token: 0x06004BEF RID: 19439 RVA: 0x0010F5E7 File Offset: 0x0010D7E7
		public FormatterTypeStyle TypeFormat
		{
			get
			{
				return this.m_typeFormat;
			}
			set
			{
				this.m_typeFormat = value;
			}
		}

		/// <summary>Gets or sets the behavior of the deserializer with regards to finding and loading assemblies.</summary>
		/// <returns>One of the <see cref="T:System.Runtime.Serialization.Formatters.FormatterAssemblyStyle" /> values that specifies the deserializer behavior.</returns>
		// Token: 0x17000CBC RID: 3260
		// (get) Token: 0x06004BF0 RID: 19440 RVA: 0x0010F5F0 File Offset: 0x0010D7F0
		// (set) Token: 0x06004BF1 RID: 19441 RVA: 0x0010F5F8 File Offset: 0x0010D7F8
		public FormatterAssemblyStyle AssemblyFormat
		{
			get
			{
				return this.m_assemblyFormat;
			}
			set
			{
				this.m_assemblyFormat = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Runtime.Serialization.Formatters.TypeFilterLevel" /> of automatic deserialization the <see cref="T:System.Runtime.Serialization.Formatters.Binary.BinaryFormatter" /> performs.</summary>
		/// <returns>The <see cref="T:System.Runtime.Serialization.Formatters.TypeFilterLevel" /> that represents the current automatic deserialization level.</returns>
		// Token: 0x17000CBD RID: 3261
		// (get) Token: 0x06004BF2 RID: 19442 RVA: 0x0010F601 File Offset: 0x0010D801
		// (set) Token: 0x06004BF3 RID: 19443 RVA: 0x0010F609 File Offset: 0x0010D809
		public TypeFilterLevel FilterLevel
		{
			get
			{
				return this.m_securityLevel;
			}
			set
			{
				this.m_securityLevel = value;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Runtime.Serialization.ISurrogateSelector" /> that controls type substitution during serialization and deserialization.</summary>
		/// <returns>The surrogate selector to use with this formatter.</returns>
		// Token: 0x17000CBE RID: 3262
		// (get) Token: 0x06004BF4 RID: 19444 RVA: 0x0010F612 File Offset: 0x0010D812
		// (set) Token: 0x06004BF5 RID: 19445 RVA: 0x0010F61A File Offset: 0x0010D81A
		public ISurrogateSelector SurrogateSelector
		{
			get
			{
				return this.m_surrogates;
			}
			set
			{
				this.m_surrogates = value;
			}
		}

		/// <summary>Gets or sets an object of type <see cref="T:System.Runtime.Serialization.SerializationBinder" /> that controls the binding of a serialized object to a type.</summary>
		/// <returns>The serialization binder to use with this formatter.</returns>
		// Token: 0x17000CBF RID: 3263
		// (get) Token: 0x06004BF6 RID: 19446 RVA: 0x0010F623 File Offset: 0x0010D823
		// (set) Token: 0x06004BF7 RID: 19447 RVA: 0x0010F62B File Offset: 0x0010D82B
		public SerializationBinder Binder
		{
			get
			{
				return this.m_binder;
			}
			set
			{
				this.m_binder = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Runtime.Serialization.StreamingContext" /> for this formatter.</summary>
		/// <returns>The streaming context to use with this formatter.</returns>
		// Token: 0x17000CC0 RID: 3264
		// (get) Token: 0x06004BF8 RID: 19448 RVA: 0x0010F634 File Offset: 0x0010D834
		// (set) Token: 0x06004BF9 RID: 19449 RVA: 0x0010F63C File Offset: 0x0010D83C
		public StreamingContext Context
		{
			get
			{
				return this.m_context;
			}
			set
			{
				this.m_context = value;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.Formatters.Binary.BinaryFormatter" /> class with default values.</summary>
		// Token: 0x06004BFA RID: 19450 RVA: 0x0010F645 File Offset: 0x0010D845
		public BinaryFormatter()
		{
			this.m_surrogates = null;
			this.m_context = new StreamingContext(StreamingContextStates.All);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.Formatters.Binary.BinaryFormatter" /> class with a given surrogate selector and streaming context.</summary>
		/// <param name="selector">The <see cref="T:System.Runtime.Serialization.ISurrogateSelector" /> to use. Can be null. </param>
		/// <param name="context">The source and destination for the serialized data. </param>
		// Token: 0x06004BFB RID: 19451 RVA: 0x0010F672 File Offset: 0x0010D872
		public BinaryFormatter(ISurrogateSelector selector, StreamingContext context)
		{
			this.m_surrogates = selector;
			this.m_context = context;
		}

		/// <summary>Deserializes the specified stream into an object graph.</summary>
		/// <returns>The top (root) of the object graph.</returns>
		/// <param name="serializationStream">The stream from which to deserialize the object graph. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="serializationStream" /> is null. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">The <paramref name="serializationStream" /> supports seeking, but its length is 0. -or-The target type is a <see cref="T:System.Decimal" />, but the value is out of range of the <see cref="T:System.Decimal" /> type.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06004BFC RID: 19452 RVA: 0x0010F696 File Offset: 0x0010D896
		public object Deserialize(Stream serializationStream)
		{
			return this.Deserialize(serializationStream, null);
		}

		// Token: 0x06004BFD RID: 19453 RVA: 0x0010F6A0 File Offset: 0x0010D8A0
		[SecurityCritical]
		internal object Deserialize(Stream serializationStream, HeaderHandler handler, bool fCheck)
		{
			return this.Deserialize(serializationStream, handler, fCheck, null);
		}

		/// <summary>Deserializes the specified stream into an object graph. The provided <see cref="T:System.Runtime.Remoting.Messaging.HeaderHandler" /> handles any headers in that stream.</summary>
		/// <returns>The deserialized object or the top object (root) of the object graph.</returns>
		/// <param name="serializationStream">The stream from which to deserialize the object graph. </param>
		/// <param name="handler">The <see cref="T:System.Runtime.Remoting.Messaging.HeaderHandler" /> that handles any headers in the <paramref name="serializationStream" />. Can be null. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="serializationStream" /> is null. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">The <paramref name="serializationStream" /> supports seeking, but its length is 0. -or-The target type is a <see cref="T:System.Decimal" />, but the value is out of range of the <see cref="T:System.Decimal" /> type.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06004BFE RID: 19454 RVA: 0x0010F6AC File Offset: 0x0010D8AC
		[SecuritySafeCritical]
		public object Deserialize(Stream serializationStream, HeaderHandler handler)
		{
			return this.Deserialize(serializationStream, handler, true);
		}

		/// <summary>Deserializes a response to a remote method call from the provided <see cref="T:System.IO.Stream" />.</summary>
		/// <returns>The deserialized response to the remote method call.</returns>
		/// <param name="serializationStream">The stream from which to deserialize the object graph. </param>
		/// <param name="handler">The <see cref="T:System.Runtime.Remoting.Messaging.HeaderHandler" /> that handles any headers in the <paramref name="serializationStream" />. Can be null. </param>
		/// <param name="methodCallMessage">The <see cref="T:System.Runtime.Remoting.Messaging.IMethodCallMessage" /> that contains details about where the call came from. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="serializationStream" /> is null. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">The <paramref name="serializationStream" /> supports seeking, but its length is 0. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06004BFF RID: 19455 RVA: 0x0010F6B7 File Offset: 0x0010D8B7
		[SecuritySafeCritical]
		public object DeserializeMethodResponse(Stream serializationStream, HeaderHandler handler, IMethodCallMessage methodCallMessage)
		{
			return this.Deserialize(serializationStream, handler, true, methodCallMessage);
		}

		/// <summary>Deserializes the specified stream into an object graph. The provided <see cref="T:System.Runtime.Remoting.Messaging.HeaderHandler" /> handles any headers in that stream.</summary>
		/// <returns>The deserialized object or the top object (root) of the object graph.</returns>
		/// <param name="serializationStream">The stream from which to deserialize the object graph. </param>
		/// <param name="handler">The <see cref="T:System.Runtime.Remoting.Messaging.HeaderHandler" /> that handles any headers in the <paramref name="serializationStream" />. Can be null. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="serializationStream" /> is null. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">The <paramref name="serializationStream" /> supports seeking, but its length is 0. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, SerializationFormatter" />
		/// </PermissionSet>
		// Token: 0x06004C00 RID: 19456 RVA: 0x0010F6C3 File Offset: 0x0010D8C3
		[ComVisible(false)]
		[SecurityCritical]
		public object UnsafeDeserialize(Stream serializationStream, HeaderHandler handler)
		{
			return this.Deserialize(serializationStream, handler, false);
		}

		/// <summary>Deserializes a response to a remote method call from the provided <see cref="T:System.IO.Stream" />.</summary>
		/// <returns>The deserialized response to the remote method call.</returns>
		/// <param name="serializationStream">The stream from which to deserialize the object graph. </param>
		/// <param name="handler">The <see cref="T:System.Runtime.Remoting.Messaging.HeaderHandler" /> that handles any headers in the <paramref name="serializationStream" />. Can be null. </param>
		/// <param name="methodCallMessage">The <see cref="T:System.Runtime.Remoting.Messaging.IMethodCallMessage" /> that contains details about where the call came from. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="serializationStream" /> is null. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">The <paramref name="serializationStream" /> supports seeking, but its length is 0. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, SerializationFormatter" />
		/// </PermissionSet>
		// Token: 0x06004C01 RID: 19457 RVA: 0x0010F6CE File Offset: 0x0010D8CE
		[SecurityCritical]
		[ComVisible(false)]
		public object UnsafeDeserializeMethodResponse(Stream serializationStream, HeaderHandler handler, IMethodCallMessage methodCallMessage)
		{
			return this.Deserialize(serializationStream, handler, false, methodCallMessage);
		}

		// Token: 0x06004C02 RID: 19458 RVA: 0x0010F6DA File Offset: 0x0010D8DA
		[SecurityCritical]
		internal object Deserialize(Stream serializationStream, HeaderHandler handler, bool fCheck, IMethodCallMessage methodCallMessage)
		{
			return this.Deserialize(serializationStream, handler, fCheck, false, methodCallMessage);
		}

		// Token: 0x06004C03 RID: 19459 RVA: 0x0010F6E8 File Offset: 0x0010D8E8
		[SecurityCritical]
		internal object Deserialize(Stream serializationStream, HeaderHandler handler, bool fCheck, bool isCrossAppDomain, IMethodCallMessage methodCallMessage)
		{
			if (serializationStream == null)
			{
				throw new ArgumentNullException("serializationStream", Environment.GetResourceString("Parameter '{0}' cannot be null.", new object[] { serializationStream }));
			}
			if (serializationStream.CanSeek && serializationStream.Length == 0L)
			{
				throw new SerializationException(Environment.GetResourceString("Attempting to deserialize an empty stream."));
			}
			InternalFE internalFE = new InternalFE();
			internalFE.FEtypeFormat = this.m_typeFormat;
			internalFE.FEserializerTypeEnum = InternalSerializerTypeE.Binary;
			internalFE.FEassemblyFormat = this.m_assemblyFormat;
			internalFE.FEsecurityLevel = this.m_securityLevel;
			ObjectReader objectReader = new ObjectReader(serializationStream, this.m_surrogates, this.m_context, internalFE, this.m_binder);
			objectReader.crossAppDomainArray = this.m_crossAppDomainArray;
			return objectReader.Deserialize(handler, new __BinaryParser(serializationStream, objectReader), fCheck, isCrossAppDomain, methodCallMessage);
		}

		/// <summary>Serializes the object, or graph of objects with the specified top (root), to the given stream.</summary>
		/// <param name="serializationStream">The stream to which the graph is to be serialized. </param>
		/// <param name="graph">The object at the root of the graph to serialize. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="serializationStream" /> is null. -or-The <paramref name="graph" /> is null.</exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">An error has occurred during serialization, such as if an object in the <paramref name="graph" /> parameter is not marked as serializable. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x06004C04 RID: 19460 RVA: 0x0010F7A1 File Offset: 0x0010D9A1
		public void Serialize(Stream serializationStream, object graph)
		{
			this.Serialize(serializationStream, graph, null);
		}

		/// <summary>Serializes the object, or graph of objects with the specified top (root), to the given stream attaching the provided headers.</summary>
		/// <param name="serializationStream">The stream to which the object is to be serialized. </param>
		/// <param name="graph">The object at the root of the graph to serialize. </param>
		/// <param name="headers">Remoting headers to include in the serialization. Can be null. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="serializationStream" /> is null. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">An error has occurred during serialization, such as if an object in the <paramref name="graph" /> parameter is not marked as serializable. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x06004C05 RID: 19461 RVA: 0x0010F7AC File Offset: 0x0010D9AC
		[SecuritySafeCritical]
		public void Serialize(Stream serializationStream, object graph, Header[] headers)
		{
			this.Serialize(serializationStream, graph, headers, true);
		}

		// Token: 0x06004C06 RID: 19462 RVA: 0x0010F7B8 File Offset: 0x0010D9B8
		[SecurityCritical]
		internal void Serialize(Stream serializationStream, object graph, Header[] headers, bool fCheck)
		{
			if (serializationStream == null)
			{
				throw new ArgumentNullException("serializationStream", Environment.GetResourceString("Parameter '{0}' cannot be null.", new object[] { serializationStream }));
			}
			InternalFE internalFE = new InternalFE();
			internalFE.FEtypeFormat = this.m_typeFormat;
			internalFE.FEserializerTypeEnum = InternalSerializerTypeE.Binary;
			internalFE.FEassemblyFormat = this.m_assemblyFormat;
			ObjectWriter objectWriter = new ObjectWriter(this.m_surrogates, this.m_context, internalFE, this.m_binder);
			__BinaryWriter _BinaryWriter = new __BinaryWriter(serializationStream, objectWriter, this.m_typeFormat);
			objectWriter.Serialize(graph, headers, _BinaryWriter, fCheck);
			this.m_crossAppDomainArray = objectWriter.crossAppDomainArray;
		}

		// Token: 0x06004C07 RID: 19463 RVA: 0x0010F84C File Offset: 0x0010DA4C
		internal static TypeInformation GetTypeInformation(Type type)
		{
			Dictionary<Type, TypeInformation> dictionary = BinaryFormatter.typeNameCache;
			TypeInformation typeInformation2;
			lock (dictionary)
			{
				TypeInformation typeInformation = null;
				if (!BinaryFormatter.typeNameCache.TryGetValue(type, out typeInformation))
				{
					bool flag2;
					string clrAssemblyName = FormatterServices.GetClrAssemblyName(type, out flag2);
					typeInformation = new TypeInformation(FormatterServices.GetClrTypeFullName(type), clrAssemblyName, flag2);
					BinaryFormatter.typeNameCache.Add(type, typeInformation);
				}
				typeInformation2 = typeInformation;
			}
			return typeInformation2;
		}

		// Token: 0x0400287E RID: 10366
		internal ISurrogateSelector m_surrogates;

		// Token: 0x0400287F RID: 10367
		internal StreamingContext m_context;

		// Token: 0x04002880 RID: 10368
		internal SerializationBinder m_binder;

		// Token: 0x04002881 RID: 10369
		internal FormatterTypeStyle m_typeFormat = FormatterTypeStyle.TypesAlways;

		// Token: 0x04002882 RID: 10370
		internal FormatterAssemblyStyle m_assemblyFormat;

		// Token: 0x04002883 RID: 10371
		internal TypeFilterLevel m_securityLevel = TypeFilterLevel.Full;

		// Token: 0x04002884 RID: 10372
		internal object[] m_crossAppDomainArray;

		// Token: 0x04002885 RID: 10373
		private static Dictionary<Type, TypeInformation> typeNameCache = new Dictionary<Type, TypeInformation>();
	}
}
