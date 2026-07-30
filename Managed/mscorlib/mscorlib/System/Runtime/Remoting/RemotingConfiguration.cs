using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Channels;
using Mono.Xml;

namespace System.Runtime.Remoting
{
	/// <summary>Provides various static methods for configuring the remoting infrastructure.</summary>
	// Token: 0x02000754 RID: 1876
	[ComVisible(true)]
	public static class RemotingConfiguration
	{
		/// <summary>Gets the ID of the currently executing application.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the ID of the currently executing application.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000CF8 RID: 3320
		// (get) Token: 0x06004D79 RID: 19833 RVA: 0x001173B8 File Offset: 0x001155B8
		public static string ApplicationId
		{
			get
			{
				RemotingConfiguration.applicationID = RemotingConfiguration.ApplicationName;
				return RemotingConfiguration.applicationID;
			}
		}

		/// <summary>Gets or sets the name of a remoting application.</summary>
		/// <returns>The name of a remoting application.</returns>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the callstack does not have permission to configure remoting types and channels. This exception is thrown only when setting the property value. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration" />
		/// </PermissionSet>
		// Token: 0x17000CF9 RID: 3321
		// (get) Token: 0x06004D7A RID: 19834 RVA: 0x001173C9 File Offset: 0x001155C9
		// (set) Token: 0x06004D7B RID: 19835 RVA: 0x001173D0 File Offset: 0x001155D0
		public static string ApplicationName
		{
			get
			{
				return RemotingConfiguration.applicationName;
			}
			set
			{
				RemotingConfiguration.applicationName = value;
			}
		}

		/// <summary>Gets or sets value that indicates how custom errors are handled.</summary>
		/// <returns>A member of the <see cref="T:System.Runtime.Remoting.CustomErrorsModes" /> enumeration that indicates how custom errors are handled.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration" />
		/// </PermissionSet>
		// Token: 0x17000CFA RID: 3322
		// (get) Token: 0x06004D7C RID: 19836 RVA: 0x0002126B File Offset: 0x0001F46B
		// (set) Token: 0x06004D7D RID: 19837 RVA: 0x0002126B File Offset: 0x0001F46B
		[MonoTODO]
		public static CustomErrorsModes CustomErrorsMode
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the ID of the currently executing process.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the ID of the currently executing process.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000CFB RID: 3323
		// (get) Token: 0x06004D7E RID: 19838 RVA: 0x001173D8 File Offset: 0x001155D8
		public static string ProcessId
		{
			get
			{
				if (RemotingConfiguration.processGuid == null)
				{
					RemotingConfiguration.processGuid = AppDomain.GetProcessGuid();
				}
				return RemotingConfiguration.processGuid;
			}
		}

		/// <summary>Reads the configuration file and configures the remoting infrastructure.</summary>
		/// <param name="filename">The name of the remoting configuration file. Can be null.</param>
		/// <param name="ensureSecurity">If set to true security is required. If set to false, security is not required but still may be used.</param>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the callstack does not have permission to configure remoting types and channels. </exception>
		// Token: 0x06004D7F RID: 19839 RVA: 0x001173F0 File Offset: 0x001155F0
		[MonoTODO("ensureSecurity support has not been implemented")]
		public static void Configure(string filename, bool ensureSecurity)
		{
			Hashtable hashtable = RemotingConfiguration.channelTemplates;
			lock (hashtable)
			{
				if (!RemotingConfiguration.defaultConfigRead)
				{
					string bundledMachineConfig = Environment.GetBundledMachineConfig();
					if (bundledMachineConfig != null)
					{
						RemotingConfiguration.ReadConfigString(bundledMachineConfig);
					}
					if (File.Exists(Environment.GetMachineConfigPath()))
					{
						RemotingConfiguration.ReadConfigFile(Environment.GetMachineConfigPath());
					}
					RemotingConfiguration.defaultConfigRead = true;
				}
				if (filename != null)
				{
					RemotingConfiguration.ReadConfigFile(filename);
				}
			}
		}

		/// <summary>Reads the configuration file and configures the remoting infrastructure. <see cref="M:System.Runtime.Remoting.RemotingConfiguration.Configure(System.String)" /> is obsolete. Please use <see cref="M:System.Runtime.Remoting.RemotingConfiguration.Configure(System.String,System.Boolean)" /> instead.</summary>
		/// <param name="filename">The name of the remoting configuration file. Can be null. </param>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the callstack does not have permission to configure remoting types and channels. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration" />
		/// </PermissionSet>
		// Token: 0x06004D80 RID: 19840 RVA: 0x00117464 File Offset: 0x00115664
		[Obsolete("Use Configure(String,Boolean)")]
		public static void Configure(string filename)
		{
			RemotingConfiguration.Configure(filename, false);
		}

		// Token: 0x06004D81 RID: 19841 RVA: 0x00117470 File Offset: 0x00115670
		private static void ReadConfigString(string filename)
		{
			try
			{
				SmallXmlParser smallXmlParser = new SmallXmlParser();
				using (TextReader textReader = new StringReader(filename))
				{
					ConfigHandler configHandler = new ConfigHandler(false);
					smallXmlParser.Parse(textReader, configHandler);
				}
			}
			catch (Exception ex)
			{
				throw new RemotingException("Configuration string could not be loaded: " + ex.Message, ex);
			}
		}

		// Token: 0x06004D82 RID: 19842 RVA: 0x001174DC File Offset: 0x001156DC
		private static void ReadConfigFile(string filename)
		{
			try
			{
				SmallXmlParser smallXmlParser = new SmallXmlParser();
				using (TextReader textReader = new StreamReader(filename))
				{
					ConfigHandler configHandler = new ConfigHandler(false);
					smallXmlParser.Parse(textReader, configHandler);
				}
			}
			catch (Exception ex)
			{
				throw new RemotingException("Configuration file '" + filename + "' could not be loaded: " + ex.Message, ex);
			}
		}

		// Token: 0x06004D83 RID: 19843 RVA: 0x00117550 File Offset: 0x00115750
		internal static void LoadDefaultDelayedChannels()
		{
			Hashtable hashtable = RemotingConfiguration.channelTemplates;
			lock (hashtable)
			{
				if (!RemotingConfiguration.defaultDelayedConfigRead && !RemotingConfiguration.defaultConfigRead)
				{
					SmallXmlParser smallXmlParser = new SmallXmlParser();
					using (TextReader textReader = new StreamReader(Environment.GetMachineConfigPath()))
					{
						ConfigHandler configHandler = new ConfigHandler(true);
						smallXmlParser.Parse(textReader, configHandler);
					}
					RemotingConfiguration.defaultDelayedConfigRead = true;
				}
			}
		}

		/// <summary>Retrieves an array of object types registered on the client as types that will be activated remotely.</summary>
		/// <returns>An array of object types registered on the client as types that will be activated remotely.</returns>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the callstack does not have permission to configure remoting types and channels. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration" />
		/// </PermissionSet>
		// Token: 0x06004D84 RID: 19844 RVA: 0x001175DC File Offset: 0x001157DC
		public static ActivatedClientTypeEntry[] GetRegisteredActivatedClientTypes()
		{
			Hashtable hashtable = RemotingConfiguration.channelTemplates;
			ActivatedClientTypeEntry[] array2;
			lock (hashtable)
			{
				ActivatedClientTypeEntry[] array = new ActivatedClientTypeEntry[RemotingConfiguration.activatedClientEntries.Count];
				RemotingConfiguration.activatedClientEntries.Values.CopyTo(array, 0);
				array2 = array;
			}
			return array2;
		}

		/// <summary>Retrieves an array of object types registered on the service end that can be activated on request from a client.</summary>
		/// <returns>An array of object types registered on the service end that can be activated on request from a client.</returns>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the callstack does not have permission to configure remoting types and channels. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration" />
		/// </PermissionSet>
		// Token: 0x06004D85 RID: 19845 RVA: 0x0011763C File Offset: 0x0011583C
		public static ActivatedServiceTypeEntry[] GetRegisteredActivatedServiceTypes()
		{
			Hashtable hashtable = RemotingConfiguration.channelTemplates;
			ActivatedServiceTypeEntry[] array2;
			lock (hashtable)
			{
				ActivatedServiceTypeEntry[] array = new ActivatedServiceTypeEntry[RemotingConfiguration.activatedServiceEntries.Count];
				RemotingConfiguration.activatedServiceEntries.Values.CopyTo(array, 0);
				array2 = array;
			}
			return array2;
		}

		/// <summary>Retrieves an array of object types registered on the client end as well-known types.</summary>
		/// <returns>An array of object types registered on the client end as well-known types.</returns>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the callstack does not have permission to configure remoting types and channels. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration" />
		/// </PermissionSet>
		// Token: 0x06004D86 RID: 19846 RVA: 0x0011769C File Offset: 0x0011589C
		public static WellKnownClientTypeEntry[] GetRegisteredWellKnownClientTypes()
		{
			Hashtable hashtable = RemotingConfiguration.channelTemplates;
			WellKnownClientTypeEntry[] array2;
			lock (hashtable)
			{
				WellKnownClientTypeEntry[] array = new WellKnownClientTypeEntry[RemotingConfiguration.wellKnownClientEntries.Count];
				RemotingConfiguration.wellKnownClientEntries.Values.CopyTo(array, 0);
				array2 = array;
			}
			return array2;
		}

		/// <summary>Retrieves an array of object types registered on the service end as well-known types.</summary>
		/// <returns>An array of object types registered on the service end as well-known types.</returns>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the callstack does not have permission to configure remoting types and channels. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration" />
		/// </PermissionSet>
		// Token: 0x06004D87 RID: 19847 RVA: 0x001176FC File Offset: 0x001158FC
		public static WellKnownServiceTypeEntry[] GetRegisteredWellKnownServiceTypes()
		{
			Hashtable hashtable = RemotingConfiguration.channelTemplates;
			WellKnownServiceTypeEntry[] array2;
			lock (hashtable)
			{
				WellKnownServiceTypeEntry[] array = new WellKnownServiceTypeEntry[RemotingConfiguration.wellKnownServiceEntries.Count];
				RemotingConfiguration.wellKnownServiceEntries.Values.CopyTo(array, 0);
				array2 = array;
			}
			return array2;
		}

		/// <summary>Returns a Boolean value that indicates whether the specified <see cref="T:System.Type" /> is allowed to be client activated.</summary>
		/// <returns>true if the specified <see cref="T:System.Type" /> is allowed to be client activated; otherwise, false.</returns>
		/// <param name="svrType">The object <see cref="T:System.Type" /> to check. </param>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the callstack does not have permission to configure remoting types and channels. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration" />
		/// </PermissionSet>
		// Token: 0x06004D88 RID: 19848 RVA: 0x0011775C File Offset: 0x0011595C
		public static bool IsActivationAllowed(Type svrType)
		{
			Hashtable hashtable = RemotingConfiguration.channelTemplates;
			bool flag2;
			lock (hashtable)
			{
				flag2 = RemotingConfiguration.activatedServiceEntries.ContainsKey(svrType);
			}
			return flag2;
		}

		/// <summary>Checks whether the specified object <see cref="T:System.Type" /> is registered as a remotely activated client type.</summary>
		/// <returns>The <see cref="T:System.Runtime.Remoting.ActivatedClientTypeEntry" /> that corresponds to the specified object type.</returns>
		/// <param name="svrType">The object type to check. </param>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the callstack does not have permission to configure remoting types and channels. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration" />
		/// </PermissionSet>
		// Token: 0x06004D89 RID: 19849 RVA: 0x001177A4 File Offset: 0x001159A4
		public static ActivatedClientTypeEntry IsRemotelyActivatedClientType(Type svrType)
		{
			Hashtable hashtable = RemotingConfiguration.channelTemplates;
			ActivatedClientTypeEntry activatedClientTypeEntry;
			lock (hashtable)
			{
				activatedClientTypeEntry = RemotingConfiguration.activatedClientEntries[svrType] as ActivatedClientTypeEntry;
			}
			return activatedClientTypeEntry;
		}

		/// <summary>Checks whether the object specified by its type name and assembly name is registered as a remotely activated client type.</summary>
		/// <returns>The <see cref="T:System.Runtime.Remoting.ActivatedClientTypeEntry" /> that corresponds to the specified object type.</returns>
		/// <param name="typeName">The type name of the object to check. </param>
		/// <param name="assemblyName">The assembly name of the object to check. </param>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the callstack does not have permission to configure remoting types and channels. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration" />
		/// </PermissionSet>
		// Token: 0x06004D8A RID: 19850 RVA: 0x001177F0 File Offset: 0x001159F0
		public static ActivatedClientTypeEntry IsRemotelyActivatedClientType(string typeName, string assemblyName)
		{
			return RemotingConfiguration.IsRemotelyActivatedClientType(Assembly.Load(assemblyName).GetType(typeName));
		}

		/// <summary>Checks whether the specified object <see cref="T:System.Type" /> is registered as a well-known client type.</summary>
		/// <returns>The <see cref="T:System.Runtime.Remoting.WellKnownClientTypeEntry" /> that corresponds to the specified object type.</returns>
		/// <param name="svrType">The object <see cref="T:System.Type" /> to check. </param>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the callstack does not have permission to configure remoting types and channels. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration" />
		/// </PermissionSet>
		// Token: 0x06004D8B RID: 19851 RVA: 0x00117804 File Offset: 0x00115A04
		public static WellKnownClientTypeEntry IsWellKnownClientType(Type svrType)
		{
			Hashtable hashtable = RemotingConfiguration.channelTemplates;
			WellKnownClientTypeEntry wellKnownClientTypeEntry;
			lock (hashtable)
			{
				wellKnownClientTypeEntry = RemotingConfiguration.wellKnownClientEntries[svrType] as WellKnownClientTypeEntry;
			}
			return wellKnownClientTypeEntry;
		}

		/// <summary>Checks whether the object specified by its type name and assembly name is registered as a well-known client type.</summary>
		/// <returns>The <see cref="T:System.Runtime.Remoting.WellKnownClientTypeEntry" /> that corresponds to the specified object type.</returns>
		/// <param name="typeName">The type name of the object to check. </param>
		/// <param name="assemblyName">The assembly name of the object to check. </param>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the callstack does not have permission to configure remoting types and channels. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration" />
		/// </PermissionSet>
		// Token: 0x06004D8C RID: 19852 RVA: 0x00117850 File Offset: 0x00115A50
		public static WellKnownClientTypeEntry IsWellKnownClientType(string typeName, string assemblyName)
		{
			return RemotingConfiguration.IsWellKnownClientType(Assembly.Load(assemblyName).GetType(typeName));
		}

		/// <summary>Registers an object <see cref="T:System.Type" /> recorded in the provided <see cref="T:System.Runtime.Remoting.ActivatedClientTypeEntry" /> on the client end as a type that can be activated on the server.</summary>
		/// <param name="entry">Configuration settings for the client-activated type. </param>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the callstack does not have permission to configure remoting types and channels. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration" />
		/// </PermissionSet>
		// Token: 0x06004D8D RID: 19853 RVA: 0x00117864 File Offset: 0x00115A64
		public static void RegisterActivatedClientType(ActivatedClientTypeEntry entry)
		{
			Hashtable hashtable = RemotingConfiguration.channelTemplates;
			lock (hashtable)
			{
				if (RemotingConfiguration.wellKnownClientEntries.ContainsKey(entry.ObjectType) || RemotingConfiguration.activatedClientEntries.ContainsKey(entry.ObjectType))
				{
					throw new RemotingException("Attempt to redirect activation of type '" + entry.ObjectType.FullName + "' which is already redirected.");
				}
				RemotingConfiguration.activatedClientEntries[entry.ObjectType] = entry;
				ActivationServices.EnableProxyActivation(entry.ObjectType, true);
			}
		}

		/// <summary>Registers an object <see cref="T:System.Type" /> on the client end as a type that can be activated on the server, using the given parameters to initialize a new instance of the <see cref="T:System.Runtime.Remoting.ActivatedClientTypeEntry" /> class.</summary>
		/// <param name="type">The object <see cref="T:System.Type" />. </param>
		/// <param name="appUrl">URL of the application where this type is activated. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="typeName" /> or <paramref name="URI" /> parameter is null. </exception>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the callstack does not have permission to configure remoting types and channels. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration" />
		/// </PermissionSet>
		// Token: 0x06004D8E RID: 19854 RVA: 0x00117900 File Offset: 0x00115B00
		public static void RegisterActivatedClientType(Type type, string appUrl)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (appUrl == null)
			{
				throw new ArgumentNullException("appUrl");
			}
			RemotingConfiguration.RegisterActivatedClientType(new ActivatedClientTypeEntry(type, appUrl));
		}

		/// <summary>Registers an object type recorded in the provided <see cref="T:System.Runtime.Remoting.ActivatedServiceTypeEntry" /> on the service end as one that can be activated on request from a client.</summary>
		/// <param name="entry">Configuration settings for the client-activated type. </param>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the callstack does not have permission to configure remoting types and channels. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration" />
		/// </PermissionSet>
		// Token: 0x06004D8F RID: 19855 RVA: 0x00117930 File Offset: 0x00115B30
		public static void RegisterActivatedServiceType(ActivatedServiceTypeEntry entry)
		{
			Hashtable hashtable = RemotingConfiguration.channelTemplates;
			lock (hashtable)
			{
				RemotingConfiguration.activatedServiceEntries.Add(entry.ObjectType, entry);
			}
		}

		/// <summary>Registers a specified object type on the service end as a type that can be activated on request from a client.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> of object to register. </param>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the callstack does not have permission to configure remoting types and channels. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration" />
		/// </PermissionSet>
		// Token: 0x06004D90 RID: 19856 RVA: 0x0011797C File Offset: 0x00115B7C
		public static void RegisterActivatedServiceType(Type type)
		{
			RemotingConfiguration.RegisterActivatedServiceType(new ActivatedServiceTypeEntry(type));
		}

		/// <summary>Registers an object <see cref="T:System.Type" /> on the client end as a well-known type that can be activated on the server, using the given parameters to initialize a new instance of the <see cref="T:System.Runtime.Remoting.WellKnownClientTypeEntry" /> class.</summary>
		/// <param name="type">The object <see cref="T:System.Type" />. </param>
		/// <param name="objectUrl">URL of a well-known client object. </param>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the callstack does not have permission to configure remoting types and channels. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration" />
		/// </PermissionSet>
		// Token: 0x06004D91 RID: 19857 RVA: 0x00117989 File Offset: 0x00115B89
		public static void RegisterWellKnownClientType(Type type, string objectUrl)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (objectUrl == null)
			{
				throw new ArgumentNullException("objectUrl");
			}
			RemotingConfiguration.RegisterWellKnownClientType(new WellKnownClientTypeEntry(type, objectUrl));
		}

		/// <summary>Registers an object <see cref="T:System.Type" /> recorded in the provided <see cref="T:System.Runtime.Remoting.WellKnownClientTypeEntry" /> on the client end as a well-known type that can be activated on the server.</summary>
		/// <param name="entry">Configuration settings for the well-known type. </param>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the callstack does not have permission to configure remoting types and channels. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration" />
		/// </PermissionSet>
		// Token: 0x06004D92 RID: 19858 RVA: 0x001179BC File Offset: 0x00115BBC
		public static void RegisterWellKnownClientType(WellKnownClientTypeEntry entry)
		{
			Hashtable hashtable = RemotingConfiguration.channelTemplates;
			lock (hashtable)
			{
				if (RemotingConfiguration.wellKnownClientEntries.ContainsKey(entry.ObjectType) || RemotingConfiguration.activatedClientEntries.ContainsKey(entry.ObjectType))
				{
					throw new RemotingException("Attempt to redirect activation of type '" + entry.ObjectType.FullName + "' which is already redirected.");
				}
				RemotingConfiguration.wellKnownClientEntries[entry.ObjectType] = entry;
				ActivationServices.EnableProxyActivation(entry.ObjectType, true);
			}
		}

		/// <summary>Registers an object <see cref="T:System.Type" /> on the service end as a well-known type, using the given parameters to initialize a new instance of <see cref="T:System.Runtime.Remoting.WellKnownServiceTypeEntry" />.</summary>
		/// <param name="type">The object <see cref="T:System.Type" />. </param>
		/// <param name="objectUri">The object URI. </param>
		/// <param name="mode">The activation mode of the well-known object type being registered. (See <see cref="T:System.Runtime.Remoting.WellKnownObjectMode" />.) </param>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the callstack does not have permission to configure remoting types and channels. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration" />
		/// </PermissionSet>
		// Token: 0x06004D93 RID: 19859 RVA: 0x00117A58 File Offset: 0x00115C58
		public static void RegisterWellKnownServiceType(Type type, string objectUri, WellKnownObjectMode mode)
		{
			RemotingConfiguration.RegisterWellKnownServiceType(new WellKnownServiceTypeEntry(type, objectUri, mode));
		}

		/// <summary>Registers an object <see cref="T:System.Type" /> recorded in the provided <see cref="T:System.Runtime.Remoting.WellKnownServiceTypeEntry" /> on the service end as a well-known type.</summary>
		/// <param name="entry">Configuration settings for the well-known type. </param>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the callstack does not have permission to configure remoting types and channels. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration" />
		/// </PermissionSet>
		// Token: 0x06004D94 RID: 19860 RVA: 0x00117A68 File Offset: 0x00115C68
		public static void RegisterWellKnownServiceType(WellKnownServiceTypeEntry entry)
		{
			Hashtable hashtable = RemotingConfiguration.channelTemplates;
			lock (hashtable)
			{
				RemotingConfiguration.wellKnownServiceEntries[entry.ObjectUri] = entry;
				RemotingServices.CreateWellKnownServerIdentity(entry.ObjectType, entry.ObjectUri, entry.Mode);
			}
		}

		// Token: 0x06004D95 RID: 19861 RVA: 0x00117ACC File Offset: 0x00115CCC
		internal static void RegisterChannelTemplate(ChannelData channel)
		{
			RemotingConfiguration.channelTemplates[channel.Id] = channel;
		}

		// Token: 0x06004D96 RID: 19862 RVA: 0x00117ADF File Offset: 0x00115CDF
		internal static void RegisterClientProviderTemplate(ProviderData prov)
		{
			RemotingConfiguration.clientProviderTemplates[prov.Id] = prov;
		}

		// Token: 0x06004D97 RID: 19863 RVA: 0x00117AF2 File Offset: 0x00115CF2
		internal static void RegisterServerProviderTemplate(ProviderData prov)
		{
			RemotingConfiguration.serverProviderTemplates[prov.Id] = prov;
		}

		// Token: 0x06004D98 RID: 19864 RVA: 0x00117B08 File Offset: 0x00115D08
		internal static void RegisterChannels(ArrayList channels, bool onlyDelayed)
		{
			foreach (object obj in channels)
			{
				ChannelData channelData = (ChannelData)obj;
				if ((!onlyDelayed || !(channelData.DelayLoadAsClientChannel != "true")) && (!RemotingConfiguration.defaultDelayedConfigRead || !(channelData.DelayLoadAsClientChannel == "true")))
				{
					if (channelData.Ref != null)
					{
						ChannelData channelData2 = (ChannelData)RemotingConfiguration.channelTemplates[channelData.Ref];
						if (channelData2 == null)
						{
							throw new RemotingException("Channel template '" + channelData.Ref + "' not found");
						}
						channelData.CopyFrom(channelData2);
					}
					foreach (object obj2 in channelData.ServerProviders)
					{
						ProviderData providerData = (ProviderData)obj2;
						if (providerData.Ref != null)
						{
							ProviderData providerData2 = (ProviderData)RemotingConfiguration.serverProviderTemplates[providerData.Ref];
							if (providerData2 == null)
							{
								throw new RemotingException("Provider template '" + providerData.Ref + "' not found");
							}
							providerData.CopyFrom(providerData2);
						}
					}
					foreach (object obj3 in channelData.ClientProviders)
					{
						ProviderData providerData3 = (ProviderData)obj3;
						if (providerData3.Ref != null)
						{
							ProviderData providerData4 = (ProviderData)RemotingConfiguration.clientProviderTemplates[providerData3.Ref];
							if (providerData4 == null)
							{
								throw new RemotingException("Provider template '" + providerData3.Ref + "' not found");
							}
							providerData3.CopyFrom(providerData4);
						}
					}
					ChannelServices.RegisterChannelConfig(channelData);
				}
			}
		}

		// Token: 0x06004D99 RID: 19865 RVA: 0x00117D20 File Offset: 0x00115F20
		internal static void RegisterTypes(ArrayList types)
		{
			foreach (object obj in types)
			{
				TypeEntry typeEntry = (TypeEntry)obj;
				if (typeEntry is ActivatedClientTypeEntry)
				{
					RemotingConfiguration.RegisterActivatedClientType((ActivatedClientTypeEntry)typeEntry);
				}
				else if (typeEntry is ActivatedServiceTypeEntry)
				{
					RemotingConfiguration.RegisterActivatedServiceType((ActivatedServiceTypeEntry)typeEntry);
				}
				else if (typeEntry is WellKnownClientTypeEntry)
				{
					RemotingConfiguration.RegisterWellKnownClientType((WellKnownClientTypeEntry)typeEntry);
				}
				else if (typeEntry is WellKnownServiceTypeEntry)
				{
					RemotingConfiguration.RegisterWellKnownServiceType((WellKnownServiceTypeEntry)typeEntry);
				}
			}
		}

		/// <summary>Indicates whether the server channels in this application domain return filtered or complete exception information to local or remote callers.</summary>
		/// <returns>true if only filtered exception information is returned to local or remote callers, as specified by the <paramref name="isLocalRequest" /> parameter; false if complete exception information is returned.</returns>
		/// <param name="isLocalRequest">true to specify local callers; false to specify remote callers. </param>
		// Token: 0x06004D9A RID: 19866 RVA: 0x00117DC0 File Offset: 0x00115FC0
		public static bool CustomErrorsEnabled(bool isLocalRequest)
		{
			return !(RemotingConfiguration._errorMode == "off") && (RemotingConfiguration._errorMode == "on" || !isLocalRequest);
		}

		// Token: 0x06004D9B RID: 19867 RVA: 0x00117DEC File Offset: 0x00115FEC
		internal static void SetCustomErrorsMode(string mode)
		{
			if (mode == null)
			{
				throw new RemotingException("mode attribute is required");
			}
			string text = mode.ToLower();
			if (text != "on" && text != "off" && text != "remoteonly")
			{
				throw new RemotingException("Invalid custom error mode: " + mode);
			}
			RemotingConfiguration._errorMode = text;
		}

		// Token: 0x040029A3 RID: 10659
		private static string applicationID = null;

		// Token: 0x040029A4 RID: 10660
		private static string applicationName = null;

		// Token: 0x040029A5 RID: 10661
		private static string processGuid = null;

		// Token: 0x040029A6 RID: 10662
		private static bool defaultConfigRead = false;

		// Token: 0x040029A7 RID: 10663
		private static bool defaultDelayedConfigRead = false;

		// Token: 0x040029A8 RID: 10664
		private static string _errorMode;

		// Token: 0x040029A9 RID: 10665
		private static Hashtable wellKnownClientEntries = new Hashtable();

		// Token: 0x040029AA RID: 10666
		private static Hashtable activatedClientEntries = new Hashtable();

		// Token: 0x040029AB RID: 10667
		private static Hashtable wellKnownServiceEntries = new Hashtable();

		// Token: 0x040029AC RID: 10668
		private static Hashtable activatedServiceEntries = new Hashtable();

		// Token: 0x040029AD RID: 10669
		private static Hashtable channelTemplates = new Hashtable();

		// Token: 0x040029AE RID: 10670
		private static Hashtable clientProviderTemplates = new Hashtable();

		// Token: 0x040029AF RID: 10671
		private static Hashtable serverProviderTemplates = new Hashtable();
	}
}
