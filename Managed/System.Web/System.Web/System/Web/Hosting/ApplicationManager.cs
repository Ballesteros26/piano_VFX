using System;
using System.Collections.Generic;
using System.Security.Permissions;
using System.Threading;
using Unity;

namespace System.Web.Hosting
{
	/// <summary>Manages ASP.NET application domains for an ASP.NET hosting application.</summary>
	// Token: 0x0200054A RID: 1354
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class ApplicationManager : MarshalByRefObject
	{
		// Token: 0x06003A9C RID: 15004 RVA: 0x0009E00C File Offset: 0x0009C20C
		private ApplicationManager()
		{
			this.id_to_host = new Dictionary<string, BareApplicationHost>();
		}

		/// <summary>Shuts down all application domains.</summary>
		// Token: 0x06003A9D RID: 15005 RVA: 0x0009E01F File Offset: 0x0009C21F
		public void Close()
		{
			if (Interlocked.Decrement(ref this.users) == 0)
			{
				this.ShutdownAll();
			}
		}

		/// <summary>Creates an object for the specified application domain, based on type.</summary>
		/// <returns>A new object of the type specified in <paramref name="type" />.</returns>
		/// <param name="appHost">An <see cref="System.Web.Hosting.IApplicationHost" /> object.</param>
		/// <param name="type">The type of the object to create.</param>
		/// <exception cref="T:System.ArgumentException">A physical path for the application does not exist.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="appHost" /> is null.- or -<paramref name="type" /> is null.</exception>
		// Token: 0x06003A9E RID: 15006 RVA: 0x0009E034 File Offset: 0x0009C234
		[global::System.MonoTODO("Need to take advantage of the configuration mapping capabilities of IApplicationHost")]
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		public IRegisteredObject CreateObject(IApplicationHost appHost, Type type)
		{
			if (appHost == null)
			{
				throw new ArgumentNullException("appHost");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			return this.CreateObject(appHost.GetSiteID(), type, appHost.GetVirtualPath(), appHost.GetPhysicalPath(), true, true);
		}

		/// <summary>Creates an object for the specified application domain based on type, virtual and physical paths, and a Boolean value indicating failure behavior when an object of the specified type already exists.</summary>
		/// <returns>A new object of the specified <paramref name="type" />.</returns>
		/// <param name="appId">The unique identifier for the application that owns the object.</param>
		/// <param name="type">The type of the object to create.</param>
		/// <param name="virtualPath">The virtual path to the application.</param>
		/// <param name="physicalPath">The physical path to the application.</param>
		/// <param name="failIfExists">true to throw an exception if an object of the specified type is currently registered; false to return the existing registered object of the specified type.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="physicalPath" /> is null- or -<paramref name="physicalPath" /> is not a valid application path.- or -<paramref name="type" /> does not implement the <see cref="T:System.Web.Hosting.IRegisteredObject" /> interface.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="appID" /> is null.- or -<paramref name="type" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="failIfExists" /> is true and an object of the specified type is already registered.</exception>
		// Token: 0x06003A9F RID: 15007 RVA: 0x0009E073 File Offset: 0x0009C273
		public IRegisteredObject CreateObject(string appId, Type type, string virtualPath, string physicalPath, bool failIfExists)
		{
			return this.CreateObject(appId, type, virtualPath, physicalPath, failIfExists, true);
		}

		/// <summary>Creates an object for the specified application domain based on type, virtual and physical paths, a Boolean value indicating failure behavior when an object of the specified type already exists, and a Boolean value indicating whether hosting initialization error exceptions are thrown.</summary>
		/// <returns>A new object of the specified <paramref name="type" />.</returns>
		/// <param name="appId">The unique identifier for the application that owns the object.</param>
		/// <param name="type">The type of the object to create.</param>
		/// <param name="virtualPath">The virtual path to the application.</param>
		/// <param name="physicalPath">The physical path to the application.</param>
		/// <param name="failIfExists">true to throw an exception if an object of the specified type is currently registered; false to return the existing registered object of the specified type.</param>
		/// <param name="throwOnError">true to throw exceptions for hosting initialization errors; false to not throw hosting initialization exceptions.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="physicalPath" /> is null- or -<paramref name="physicalPath" /> is not a valid application path.- or -<paramref name="type" /> does not implement the <see cref="T:System.Web.Hosting.IRegisteredObject" /> interface.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="appID" /> is null.- or -<paramref name="type" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="failIfExists" /> is true and an object of the specified type is already registered.</exception>
		// Token: 0x06003AA0 RID: 15008 RVA: 0x0009E084 File Offset: 0x0009C284
		public IRegisteredObject CreateObject(string appId, Type type, string virtualPath, string physicalPath, bool failIfExists, bool throwOnError)
		{
			if (appId == null)
			{
				throw new ArgumentNullException("appId");
			}
			if (!VirtualPathUtility.IsAbsolute(virtualPath))
			{
				throw new ArgumentException("Relative path no allowed.", "virtualPath");
			}
			if (string.IsNullOrEmpty(physicalPath))
			{
				throw new ArgumentException("Cannot be null or empty", "physicalPath");
			}
			if (!typeof(IRegisteredObject).IsAssignableFrom(type))
			{
				throw new ArgumentException("Type '" + type.Name + "' does not implement IRegisteredObject.", "type");
			}
			BareApplicationHost bareApplicationHost = null;
			if (this.id_to_host.ContainsKey(appId))
			{
				bareApplicationHost = this.id_to_host[appId];
			}
			IRegisteredObject registeredObject = null;
			if (bareApplicationHost != null)
			{
				registeredObject = this.CheckIfExists(bareApplicationHost, type, failIfExists);
				if (registeredObject != null)
				{
					return registeredObject;
				}
			}
			try
			{
				if (bareApplicationHost == null)
				{
					bareApplicationHost = this.CreateHost(appId, virtualPath, physicalPath);
				}
				registeredObject = bareApplicationHost.CreateInstance(type);
			}
			catch (Exception)
			{
				if (throwOnError)
				{
					throw;
				}
			}
			if (registeredObject != null && bareApplicationHost.GetObject(type) == null)
			{
				bareApplicationHost.RegisterObject(registeredObject, true);
			}
			return registeredObject;
		}

		// Token: 0x06003AA1 RID: 15009 RVA: 0x0009E17C File Offset: 0x0009C37C
		internal BareApplicationHost CreateHostWithCheck(string appId, string vpath, string ppath)
		{
			if (this.id_to_host.ContainsKey(appId))
			{
				throw new InvalidOperationException("Already have a host with the same appId");
			}
			return this.CreateHost(appId, vpath, ppath);
		}

		// Token: 0x06003AA2 RID: 15010 RVA: 0x0009E1A0 File Offset: 0x0009C3A0
		private BareApplicationHost CreateHost(string appId, string vpath, string ppath)
		{
			BareApplicationHost bareApplicationHost = (BareApplicationHost)ApplicationHost.CreateApplicationHost(typeof(BareApplicationHost), vpath, ppath);
			bareApplicationHost.Manager = this;
			bareApplicationHost.AppID = appId;
			this.id_to_host[appId] = bareApplicationHost;
			return bareApplicationHost;
		}

		// Token: 0x06003AA3 RID: 15011 RVA: 0x0009E1E0 File Offset: 0x0009C3E0
		internal void RemoveHost(string appId)
		{
			this.id_to_host.Remove(appId);
		}

		// Token: 0x06003AA4 RID: 15012 RVA: 0x0009E1F0 File Offset: 0x0009C3F0
		private IRegisteredObject CheckIfExists(BareApplicationHost host, Type type, bool failIfExists)
		{
			IRegisteredObject @object = host.GetObject(type);
			if (@object == null)
			{
				return null;
			}
			if (failIfExists)
			{
				throw new InvalidOperationException("Well known object of type '" + type.Name + "' already exists in this domain.");
			}
			return @object;
		}

		/// <summary>Returns the single instance of the <see cref="T:System.Web.Hosting.ApplicationManager" /> object associated with this ASP.NET host process.</summary>
		/// <returns>The single instance of the <see cref="T:System.Web.Hosting.ApplicationManager" /> object associated with the ASP.NET host process that is running.</returns>
		// Token: 0x06003AA5 RID: 15013 RVA: 0x0009E229 File Offset: 0x0009C429
		public static ApplicationManager GetApplicationManager()
		{
			return ApplicationManager.instance;
		}

		/// <summary>Returns the registered object of the specified type from the specified application.</summary>
		/// <returns>The registered object of the specified type; or null if the type has not been registered through a call to the <see cref="M:System.Web.Hosting.ApplicationManager.CreateObject(System.String,System.Type,System.String,System.String,System.Boolean)" /> method.</returns>
		/// <param name="appId">The unique identifier for the application that owns the object.</param>
		/// <param name="type">The type of the object to return.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="appId" /> is null.—or—<paramref name="type" /> is null.</exception>
		// Token: 0x06003AA6 RID: 15014 RVA: 0x0009E230 File Offset: 0x0009C430
		public IRegisteredObject GetObject(string appId, Type type)
		{
			if (appId == null)
			{
				throw new ArgumentNullException("appId");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (!this.id_to_host.ContainsKey(appId))
			{
				return null;
			}
			return this.id_to_host[appId].GetObject(type);
		}

		/// <summary>Returns a snapshot of running applications.</summary>
		/// <returns>An array of <see cref="T:System.Web.Hosting.ApplicationInfo" /> objects that contain information about the applications managed by this <see cref="T:System.Web.Hosting.ApplicationManager" /> instance.</returns>
		// Token: 0x06003AA7 RID: 15015 RVA: 0x0009E284 File Offset: 0x0009C484
		public ApplicationInfo[] GetRunningApplications()
		{
			Dictionary<string, BareApplicationHost>.KeyCollection keys = this.id_to_host.Keys;
			string[] array = new string[((ICollection<string>)keys).Count];
			((ICollection<string>)keys).CopyTo(array, 0);
			ApplicationInfo[] array2 = new ApplicationInfo[((ICollection<string>)keys).Count];
			int num = 0;
			foreach (string text in array)
			{
				BareApplicationHost bareApplicationHost = this.id_to_host[text];
				array2[num++] = new ApplicationInfo(text, bareApplicationHost.PhysicalPath, bareApplicationHost.VirtualPath);
			}
			return array2;
		}

		/// <summary>Gives the application domain an infinite lifetime by preventing a lease from being created.</summary>
		/// <returns>Always null.</returns>
		// Token: 0x06003AA8 RID: 15016 RVA: 0x00003BEA File Offset: 0x00001DEA
		public override object InitializeLifetimeService()
		{
			return null;
		}

		/// <summary>Returns a value indicating whether all applications hosted by the process are idle and not processing requests.</summary>
		/// <returns>true if all applications in the process are idle; otherwise, false.</returns>
		// Token: 0x06003AA9 RID: 15017 RVA: 0x00003A1F File Offset: 0x00001C1F
		public bool IsIdle()
		{
			throw new NotImplementedException();
		}

		/// <summary>Makes a thread-safe increment to the user reference count of the application manager instance.</summary>
		// Token: 0x06003AAA RID: 15018 RVA: 0x0009E303 File Offset: 0x0009C503
		public void Open()
		{
			Interlocked.Increment(ref this.users);
		}

		/// <summary>Unloads all application resources.</summary>
		// Token: 0x06003AAB RID: 15019 RVA: 0x0009E314 File Offset: 0x0009C514
		public void ShutdownAll()
		{
			Dictionary<string, BareApplicationHost>.KeyCollection keys = this.id_to_host.Keys;
			string[] array = new string[((ICollection<string>)keys).Count];
			((ICollection<string>)keys).CopyTo(array, 0);
			foreach (string text in array)
			{
				this.id_to_host[text].Shutdown();
			}
			this.id_to_host.Clear();
		}

		/// <summary>Unloads the specified application.</summary>
		/// <param name="appId">The unique identifier of the application to unload.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="appId" /> is null.</exception>
		// Token: 0x06003AAC RID: 15020 RVA: 0x0009E370 File Offset: 0x0009C570
		public void ShutdownApplication(string appId)
		{
			if (appId == null)
			{
				throw new ArgumentNullException("appId");
			}
			BareApplicationHost bareApplicationHost = this.id_to_host[appId];
			if (bareApplicationHost == null)
			{
				return;
			}
			bareApplicationHost.Shutdown();
		}

		/// <summary>Removes the specified object from the list of registered objects in an application. If the object to be removed is the last remaining object in the list of registered objects in an application, the application is unloaded.</summary>
		/// <param name="appId">The unique identifier for the application that owns the object.</param>
		/// <param name="type">The type of the object to unload.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="appId" /> is null.- or -<paramref name="type" /> is null.</exception>
		// Token: 0x06003AAD RID: 15021 RVA: 0x0009E3A4 File Offset: 0x0009C5A4
		public void StopObject(string appId, Type type)
		{
			if (appId == null)
			{
				throw new ArgumentNullException("appId");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (!this.id_to_host.ContainsKey(appId))
			{
				return;
			}
			BareApplicationHost bareApplicationHost = this.id_to_host[appId];
			if (bareApplicationHost == null)
			{
				return;
			}
			bareApplicationHost.StopObject(type);
		}

		/// <summary>Gets the application domain of the specified application.</summary>
		/// <returns>The application domain of the application.</returns>
		/// <param name="appId">The unique identifier for the application.</param>
		// Token: 0x06003AAF RID: 15023 RVA: 0x0000E80B File Offset: 0x0000CA0B
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		public AppDomain GetAppDomain(string appId)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Gets the application domain of an application using the specified host.</summary>
		/// <returns>The application domain of the application.</returns>
		/// <param name="appHost">The host to get the application domain for.</param>
		// Token: 0x06003AB0 RID: 15024 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public AppDomain GetAppDomain(IApplicationHost appHost)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x04001FD8 RID: 8152
		private static ApplicationManager instance = new ApplicationManager();

		// Token: 0x04001FD9 RID: 8153
		private int users;

		// Token: 0x04001FDA RID: 8154
		private Dictionary<string, BareApplicationHost> id_to_host;
	}
}
