using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Text;
using System.Threading;

namespace System.Resources
{
	/// <summary>Represents a resource manager that provides convenient access to culture-specific resources at run time.</summary>
	// Token: 0x020002A6 RID: 678
	[ComVisible(true)]
	[Serializable]
	public class ResourceManager
	{
		// Token: 0x06001F13 RID: 7955 RVA: 0x00078B24 File Offset: 0x00076D24
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void Init()
		{
			this.m_callingAssembly = (RuntimeAssembly)Assembly.GetCallingAssembly();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.ResourceManager" /> class with default values.</summary>
		// Token: 0x06001F14 RID: 7956 RVA: 0x00078B38 File Offset: 0x00076D38
		protected ResourceManager()
		{
			this.Init();
			this._lastUsedResourceCache = new ResourceManager.CultureNameResourceSetPair();
			ResourceManager.ResourceManagerMediator resourceManagerMediator = new ResourceManager.ResourceManagerMediator(this);
			this.resourceGroveler = new ManifestBasedResourceGroveler(resourceManagerMediator);
		}

		// Token: 0x06001F15 RID: 7957 RVA: 0x00078B70 File Offset: 0x00076D70
		private ResourceManager(string baseName, string resourceDir, Type usingResourceSet)
		{
			if (baseName == null)
			{
				throw new ArgumentNullException("baseName");
			}
			if (resourceDir == null)
			{
				throw new ArgumentNullException("resourceDir");
			}
			this.BaseNameField = baseName;
			this.moduleDir = resourceDir;
			this._userResourceSet = usingResourceSet;
			this.ResourceSets = new Hashtable();
			this._resourceSets = new Dictionary<string, ResourceSet>();
			this._lastUsedResourceCache = new ResourceManager.CultureNameResourceSetPair();
			this.UseManifest = false;
			ResourceManager.ResourceManagerMediator resourceManagerMediator = new ResourceManager.ResourceManagerMediator(this);
			this.resourceGroveler = new FileBasedResourceGroveler(resourceManagerMediator);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.ResourceManager" /> class that looks up resources contained in files with the specified root name in the given assembly.</summary>
		/// <param name="baseName">The root name of the resource file without its extension but including any fully qualified namespace name. For example, the root name for the resource file named MyApplication.MyResource.en-US.resources is MyApplication.MyResource. </param>
		/// <param name="assembly">The main assembly for the resources. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="baseName" /> or <paramref name="assembly" /> parameter is null. </exception>
		// Token: 0x06001F16 RID: 7958 RVA: 0x00078BF0 File Offset: 0x00076DF0
		[MethodImpl(MethodImplOptions.NoInlining)]
		public ResourceManager(string baseName, Assembly assembly)
		{
			if (baseName == null)
			{
				throw new ArgumentNullException("baseName");
			}
			if (null == assembly)
			{
				throw new ArgumentNullException("assembly");
			}
			if (!(assembly is RuntimeAssembly))
			{
				throw new ArgumentException(Environment.GetResourceString("Assembly must be a runtime Assembly object."));
			}
			this.MainAssembly = assembly;
			this.BaseNameField = baseName;
			this.SetAppXConfiguration();
			this.CommonAssemblyInit();
			this.m_callingAssembly = (RuntimeAssembly)Assembly.GetCallingAssembly();
			if (assembly == typeof(object).Assembly && this.m_callingAssembly != assembly)
			{
				this.m_callingAssembly = null;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.ResourceManager" /> class that uses a specified <see cref="T:System.Resources.ResourceSet" /> class to look up resources contained in files with the specified root name in the given assembly.</summary>
		/// <param name="baseName">The root name of the resource file without its extension but including any fully qualified namespace name. For example, the root name for the resource file named MyApplication.MyResource.en-US.resources is MyApplication.MyResource. </param>
		/// <param name="assembly">The main assembly for the resources. </param>
		/// <param name="usingResourceSet">The type of the custom <see cref="T:System.Resources.ResourceSet" /> to use. If null, the default runtime <see cref="T:System.Resources.ResourceSet" /> object is used. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="usingResourceset" /> is not a derived class of <see cref="T:System.Resources.ResourceSet" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="baseName" /> or <paramref name="assembly" /> parameter is null. </exception>
		// Token: 0x06001F17 RID: 7959 RVA: 0x00078C94 File Offset: 0x00076E94
		[MethodImpl(MethodImplOptions.NoInlining)]
		public ResourceManager(string baseName, Assembly assembly, Type usingResourceSet)
		{
			if (baseName == null)
			{
				throw new ArgumentNullException("baseName");
			}
			if (null == assembly)
			{
				throw new ArgumentNullException("assembly");
			}
			if (!(assembly is RuntimeAssembly))
			{
				throw new ArgumentException(Environment.GetResourceString("Assembly must be a runtime Assembly object."));
			}
			this.MainAssembly = assembly;
			this.BaseNameField = baseName;
			if (usingResourceSet != null && usingResourceSet != ResourceManager._minResourceSet && !usingResourceSet.IsSubclassOf(ResourceManager._minResourceSet))
			{
				throw new ArgumentException(Environment.GetResourceString("Type parameter must refer to a subclass of ResourceSet."), "usingResourceSet");
			}
			this._userResourceSet = usingResourceSet;
			this.CommonAssemblyInit();
			this.m_callingAssembly = (RuntimeAssembly)Assembly.GetCallingAssembly();
			if (assembly == typeof(object).Assembly && this.m_callingAssembly != assembly)
			{
				this.m_callingAssembly = null;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.ResourceManager" /> class that looks up resources in satellite assemblies based on information from the specified type object.</summary>
		/// <param name="resourceSource">A type from which the resource manager derives all information for finding .resources files. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="resourceSource" /> parameter is null. </exception>
		// Token: 0x06001F18 RID: 7960 RVA: 0x00078D70 File Offset: 0x00076F70
		[MethodImpl(MethodImplOptions.NoInlining)]
		public ResourceManager(Type resourceSource)
		{
			if (null == resourceSource)
			{
				throw new ArgumentNullException("resourceSource");
			}
			if (!(resourceSource is RuntimeType))
			{
				throw new ArgumentException(Environment.GetResourceString("Type must be a runtime Type object."));
			}
			this._locationInfo = resourceSource;
			this.MainAssembly = this._locationInfo.Assembly;
			this.BaseNameField = resourceSource.Name;
			this.SetAppXConfiguration();
			this.CommonAssemblyInit();
			this.m_callingAssembly = (RuntimeAssembly)Assembly.GetCallingAssembly();
			if (this.MainAssembly == typeof(object).Assembly && this.m_callingAssembly != this.MainAssembly)
			{
				this.m_callingAssembly = null;
			}
		}

		// Token: 0x06001F19 RID: 7961 RVA: 0x00078E25 File Offset: 0x00077025
		[OnDeserializing]
		private void OnDeserializing(StreamingContext ctx)
		{
			this._resourceSets = null;
			this.resourceGroveler = null;
			this._lastUsedResourceCache = null;
		}

		// Token: 0x06001F1A RID: 7962 RVA: 0x00078E3C File Offset: 0x0007703C
		[SecuritySafeCritical]
		[OnDeserialized]
		private void OnDeserialized(StreamingContext ctx)
		{
			this._resourceSets = new Dictionary<string, ResourceSet>();
			this._lastUsedResourceCache = new ResourceManager.CultureNameResourceSetPair();
			ResourceManager.ResourceManagerMediator resourceManagerMediator = new ResourceManager.ResourceManagerMediator(this);
			if (this.UseManifest)
			{
				this.resourceGroveler = new ManifestBasedResourceGroveler(resourceManagerMediator);
			}
			else
			{
				this.resourceGroveler = new FileBasedResourceGroveler(resourceManagerMediator);
			}
			if (this.m_callingAssembly == null)
			{
				this.m_callingAssembly = (RuntimeAssembly)this._callingAssembly;
			}
			if (this.UseManifest && this._neutralResourcesCulture == null)
			{
				this._neutralResourcesCulture = ManifestBasedResourceGroveler.GetNeutralResourcesLanguage(this.MainAssembly, ref this._fallbackLoc);
			}
		}

		// Token: 0x06001F1B RID: 7963 RVA: 0x00078ECE File Offset: 0x000770CE
		[OnSerializing]
		private void OnSerializing(StreamingContext ctx)
		{
			this._callingAssembly = this.m_callingAssembly;
			this.UseSatelliteAssem = this.UseManifest;
			this.ResourceSets = new Hashtable();
		}

		// Token: 0x06001F1C RID: 7964 RVA: 0x00078EF4 File Offset: 0x000770F4
		[SecuritySafeCritical]
		private void CommonAssemblyInit()
		{
			this.UseManifest = true;
			this._resourceSets = new Dictionary<string, ResourceSet>();
			this._lastUsedResourceCache = new ResourceManager.CultureNameResourceSetPair();
			this._fallbackLoc = UltimateResourceFallbackLocation.MainAssembly;
			ResourceManager.ResourceManagerMediator resourceManagerMediator = new ResourceManager.ResourceManagerMediator(this);
			this.resourceGroveler = new ManifestBasedResourceGroveler(resourceManagerMediator);
			this._neutralResourcesCulture = ManifestBasedResourceGroveler.GetNeutralResourcesLanguage(this.MainAssembly, ref this._fallbackLoc);
			this.ResourceSets = new Hashtable();
		}

		/// <summary>Gets the root name of the resource files that the <see cref="T:System.Resources.ResourceManager" /> searches for resources.</summary>
		/// <returns>The root name of the resource files that the <see cref="T:System.Resources.ResourceManager" /> searches for resources.</returns>
		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x06001F1D RID: 7965 RVA: 0x00078F5A File Offset: 0x0007715A
		public virtual string BaseName
		{
			get
			{
				return this.BaseNameField;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the resource manager allows case-insensitive resource lookups in the <see cref="M:System.Resources.ResourceManager.GetString(System.String)" /> and <see cref="M:System.Resources.ResourceManager.GetObject(System.String)" /> methods.</summary>
		/// <returns>true to ignore case during resource lookup; otherwise, false.</returns>
		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x06001F1E RID: 7966 RVA: 0x00078F62 File Offset: 0x00077162
		// (set) Token: 0x06001F1F RID: 7967 RVA: 0x00078F6A File Offset: 0x0007716A
		public virtual bool IgnoreCase
		{
			get
			{
				return this._ignoreCase;
			}
			set
			{
				this._ignoreCase = value;
			}
		}

		/// <summary>Gets the type of the resource set object that the resource manager uses to construct a <see cref="T:System.Resources.ResourceSet" /> object.</summary>
		/// <returns>The type of the resource set object that the resource manager uses to construct a <see cref="T:System.Resources.ResourceSet" /> object.</returns>
		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x06001F20 RID: 7968 RVA: 0x00078F73 File Offset: 0x00077173
		public virtual Type ResourceSetType
		{
			get
			{
				if (!(this._userResourceSet == null))
				{
					return this._userResourceSet;
				}
				return typeof(RuntimeResourceSet);
			}
		}

		/// <summary>Gets or sets the location from which to retrieve default fallback resources.</summary>
		/// <returns>One of the enumeration values that specifies where the resource manager can look for fallback resources.</returns>
		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x06001F21 RID: 7969 RVA: 0x00078F94 File Offset: 0x00077194
		// (set) Token: 0x06001F22 RID: 7970 RVA: 0x00078F9C File Offset: 0x0007719C
		protected UltimateResourceFallbackLocation FallbackLocation
		{
			get
			{
				return this._fallbackLoc;
			}
			set
			{
				this._fallbackLoc = value;
			}
		}

		/// <summary>Tells the resource manager to call the <see cref="M:System.Resources.ResourceSet.Close" /> method on all <see cref="T:System.Resources.ResourceSet" /> objects and release all resources.</summary>
		// Token: 0x06001F23 RID: 7971 RVA: 0x00078FA8 File Offset: 0x000771A8
		public virtual void ReleaseAllResources()
		{
			Dictionary<string, ResourceSet> resourceSets = this._resourceSets;
			this._resourceSets = new Dictionary<string, ResourceSet>();
			this._lastUsedResourceCache = new ResourceManager.CultureNameResourceSetPair();
			Dictionary<string, ResourceSet> dictionary = resourceSets;
			lock (dictionary)
			{
				IDictionaryEnumerator dictionaryEnumerator = resourceSets.GetEnumerator();
				IDictionaryEnumerator dictionaryEnumerator2 = null;
				if (this.ResourceSets != null)
				{
					dictionaryEnumerator2 = this.ResourceSets.GetEnumerator();
				}
				this.ResourceSets = new Hashtable();
				while (dictionaryEnumerator.MoveNext())
				{
					((ResourceSet)dictionaryEnumerator.Value).Close();
				}
				if (dictionaryEnumerator2 != null)
				{
					while (dictionaryEnumerator2.MoveNext())
					{
						((ResourceSet)dictionaryEnumerator2.Value).Close();
					}
				}
			}
		}

		/// <summary>Returns a <see cref="T:System.Resources.ResourceManager" /> object that searches a specific directory instead of an assembly manifest for resources.</summary>
		/// <returns>A new instance of a resource manager that searches the specified directory instead of an assembly manifest for resources.</returns>
		/// <param name="baseName">The root name of the resources. For example, the root name for the resource file named "MyResource.en-US.resources" is "MyResource". </param>
		/// <param name="resourceDir">The name of the directory to search for the resources. <paramref name="resourceDir" /> can be an absolute path or a relative path from the application directory. </param>
		/// <param name="usingResourceSet">The type of the custom <see cref="T:System.Resources.ResourceSet" /> to use. If null, the default runtime <see cref="T:System.Resources.ResourceSet" /> object is used. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="baseName" /> or <paramref name="resourceDir" /> parameter is null. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001F24 RID: 7972 RVA: 0x00079064 File Offset: 0x00077264
		public static ResourceManager CreateFileBasedResourceManager(string baseName, string resourceDir, Type usingResourceSet)
		{
			return new ResourceManager(baseName, resourceDir, usingResourceSet);
		}

		/// <summary>Generates the name of the resource file for the given <see cref="T:System.Globalization.CultureInfo" /> object.</summary>
		/// <returns>The name that can be used for a resource file for the given <see cref="T:System.Globalization.CultureInfo" /> object.</returns>
		/// <param name="culture">The culture object for which a resource file name is constructed. </param>
		// Token: 0x06001F25 RID: 7973 RVA: 0x00079070 File Offset: 0x00077270
		protected virtual string GetResourceFileName(CultureInfo culture)
		{
			StringBuilder stringBuilder = new StringBuilder(255);
			stringBuilder.Append(this.BaseNameField);
			if (!culture.HasInvariantCultureName)
			{
				CultureInfo.VerifyCultureName(culture.Name, true);
				stringBuilder.Append('.');
				stringBuilder.Append(culture.Name);
			}
			stringBuilder.Append(".resources");
			return stringBuilder.ToString();
		}

		// Token: 0x06001F26 RID: 7974 RVA: 0x000790D4 File Offset: 0x000772D4
		internal ResourceSet GetFirstResourceSet(CultureInfo culture)
		{
			if (this._neutralResourcesCulture != null && culture.Name == this._neutralResourcesCulture.Name)
			{
				culture = CultureInfo.InvariantCulture;
			}
			if (this._lastUsedResourceCache != null)
			{
				ResourceManager.CultureNameResourceSetPair cultureNameResourceSetPair = this._lastUsedResourceCache;
				lock (cultureNameResourceSetPair)
				{
					if (culture.Name == this._lastUsedResourceCache.lastCultureName)
					{
						return this._lastUsedResourceCache.lastResourceSet;
					}
				}
			}
			Dictionary<string, ResourceSet> resourceSets = this._resourceSets;
			ResourceSet resourceSet = null;
			if (resourceSets != null)
			{
				Dictionary<string, ResourceSet> dictionary = resourceSets;
				lock (dictionary)
				{
					resourceSets.TryGetValue(culture.Name, out resourceSet);
				}
			}
			if (resourceSet != null)
			{
				if (this._lastUsedResourceCache != null)
				{
					ResourceManager.CultureNameResourceSetPair cultureNameResourceSetPair = this._lastUsedResourceCache;
					lock (cultureNameResourceSetPair)
					{
						this._lastUsedResourceCache.lastCultureName = culture.Name;
						this._lastUsedResourceCache.lastResourceSet = resourceSet;
					}
				}
				return resourceSet;
			}
			return null;
		}

		/// <summary>Retrieves the resource set for a particular culture.</summary>
		/// <returns>The resource set for the specified culture.</returns>
		/// <param name="culture">The culture whose resources are to be retrieved. </param>
		/// <param name="createIfNotExists">true to load the resource set, if it has not been loaded yet; otherwise, false. </param>
		/// <param name="tryParents">true to use resource fallback to load an appropriate resource if the resource set cannot be found; false to bypass the resource fallback process. (See the Remarks section.)</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="culture" /> parameter is null. </exception>
		/// <exception cref="T:System.Resources.MissingManifestResourceException">
		///   <paramref name="tryParents" /> is true, no usable set of resources has been found, and there are no default culture resources. </exception>
		// Token: 0x06001F27 RID: 7975 RVA: 0x00079200 File Offset: 0x00077400
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public virtual ResourceSet GetResourceSet(CultureInfo culture, bool createIfNotExists, bool tryParents)
		{
			if (culture == null)
			{
				throw new ArgumentNullException("culture");
			}
			Dictionary<string, ResourceSet> resourceSets = this._resourceSets;
			if (resourceSets != null)
			{
				Dictionary<string, ResourceSet> dictionary = resourceSets;
				lock (dictionary)
				{
					ResourceSet resourceSet;
					if (resourceSets.TryGetValue(culture.Name, out resourceSet))
					{
						return resourceSet;
					}
				}
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			if (this.UseManifest && culture.HasInvariantCultureName)
			{
				string resourceFileName = this.GetResourceFileName(culture);
				Stream manifestResourceStream = ((RuntimeAssembly)this.MainAssembly).GetManifestResourceStream(this._locationInfo, resourceFileName, this.m_callingAssembly == this.MainAssembly, ref stackCrawlMark);
				if (createIfNotExists && manifestResourceStream != null)
				{
					ResourceSet resourceSet = ((ManifestBasedResourceGroveler)this.resourceGroveler).CreateResourceSet(manifestResourceStream, this.MainAssembly);
					ResourceManager.AddResourceSet(resourceSets, culture.Name, ref resourceSet);
					return resourceSet;
				}
			}
			return this.InternalGetResourceSet(culture, createIfNotExists, tryParents);
		}

		/// <summary>Provides the implementation for finding a resource set.</summary>
		/// <returns>The specified resource set.</returns>
		/// <param name="culture">The culture object to look for. </param>
		/// <param name="createIfNotExists">true to load the resource set, if it has not been loaded yet; otherwise, false. </param>
		/// <param name="tryParents">true to check parent <see cref="T:System.Globalization.CultureInfo" /> objects if the resource set cannot be loaded; otherwise, false.</param>
		/// <exception cref="T:System.Resources.MissingManifestResourceException">The main assembly does not contain a .resources file, which is required to look up a resource. </exception>
		/// <exception cref="T:System.ExecutionEngineException">There was an internal error in the runtime.</exception>
		/// <exception cref="T:System.Resources.MissingSatelliteAssemblyException">The satellite assembly associated with <paramref name="culture" /> could not be located.</exception>
		// Token: 0x06001F28 RID: 7976 RVA: 0x000792EC File Offset: 0x000774EC
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		protected virtual ResourceSet InternalGetResourceSet(CultureInfo culture, bool createIfNotExists, bool tryParents)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return this.InternalGetResourceSet(culture, createIfNotExists, tryParents, ref stackCrawlMark);
		}

		// Token: 0x06001F29 RID: 7977 RVA: 0x00079308 File Offset: 0x00077508
		[SecurityCritical]
		private ResourceSet InternalGetResourceSet(CultureInfo requestedCulture, bool createIfNotExists, bool tryParents, ref StackCrawlMark stackMark)
		{
			Dictionary<string, ResourceSet> resourceSets = this._resourceSets;
			ResourceSet resourceSet = null;
			CultureInfo cultureInfo = null;
			Dictionary<string, ResourceSet> dictionary = resourceSets;
			lock (dictionary)
			{
				if (resourceSets.TryGetValue(requestedCulture.Name, out resourceSet))
				{
					return resourceSet;
				}
			}
			ResourceFallbackManager resourceFallbackManager = new ResourceFallbackManager(requestedCulture, this._neutralResourcesCulture, tryParents);
			foreach (CultureInfo cultureInfo2 in resourceFallbackManager)
			{
				dictionary = resourceSets;
				lock (dictionary)
				{
					if (resourceSets.TryGetValue(cultureInfo2.Name, out resourceSet))
					{
						if (requestedCulture != cultureInfo2)
						{
							cultureInfo = cultureInfo2;
						}
						break;
					}
				}
				resourceSet = this.resourceGroveler.GrovelForResourceSet(cultureInfo2, resourceSets, tryParents, createIfNotExists, ref stackMark);
				if (resourceSet != null)
				{
					cultureInfo = cultureInfo2;
					break;
				}
			}
			if (resourceSet != null && cultureInfo != null)
			{
				foreach (CultureInfo cultureInfo3 in resourceFallbackManager)
				{
					ResourceManager.AddResourceSet(resourceSets, cultureInfo3.Name, ref resourceSet);
					if (cultureInfo3 == cultureInfo)
					{
						break;
					}
				}
			}
			return resourceSet;
		}

		// Token: 0x06001F2A RID: 7978 RVA: 0x00079460 File Offset: 0x00077660
		private static void AddResourceSet(Dictionary<string, ResourceSet> localResourceSets, string cultureName, ref ResourceSet rs)
		{
			lock (localResourceSets)
			{
				ResourceSet resourceSet;
				if (localResourceSets.TryGetValue(cultureName, out resourceSet))
				{
					if (resourceSet != rs)
					{
						if (!localResourceSets.ContainsValue(rs))
						{
							rs.Dispose();
						}
						rs = resourceSet;
					}
				}
				else
				{
					localResourceSets.Add(cultureName, rs);
				}
			}
		}

		/// <summary>Returns the version specified by the <see cref="T:System.Resources.SatelliteContractVersionAttribute" /> attribute in the given assembly.</summary>
		/// <returns>The satellite contract version of the given assembly, or null if no version was found.</returns>
		/// <param name="a">The assembly to check for the <see cref="T:System.Resources.SatelliteContractVersionAttribute" /> attribute. </param>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Version" /> found in the assembly <paramref name="a" /> is invalid. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="a" /> is null. </exception>
		// Token: 0x06001F2B RID: 7979 RVA: 0x000794C4 File Offset: 0x000776C4
		protected static Version GetSatelliteContractVersion(Assembly a)
		{
			if (a == null)
			{
				throw new ArgumentNullException("a", Environment.GetResourceString("Assembly cannot be null."));
			}
			string text = null;
			if (a.ReflectionOnly)
			{
				foreach (CustomAttributeData customAttributeData in CustomAttributeData.GetCustomAttributes(a))
				{
					if (customAttributeData.Constructor.DeclaringType == typeof(SatelliteContractVersionAttribute))
					{
						text = (string)customAttributeData.ConstructorArguments[0].Value;
						break;
					}
				}
				if (text == null)
				{
					return null;
				}
			}
			else
			{
				object[] customAttributes = a.GetCustomAttributes(typeof(SatelliteContractVersionAttribute), false);
				if (customAttributes.Length == 0)
				{
					return null;
				}
				text = ((SatelliteContractVersionAttribute)customAttributes[0]).Version;
			}
			Version version;
			try
			{
				version = new Version(text);
			}
			catch (ArgumentOutOfRangeException ex)
			{
				if (a == typeof(object).Assembly)
				{
					return null;
				}
				throw new ArgumentException(Environment.GetResourceString("Satellite contract version attribute on the assembly '{0}' specifies an invalid version: {1}.", new object[]
				{
					a.ToString(),
					text
				}), ex);
			}
			return version;
		}

		/// <summary>Returns culture-specific information for the main assembly's default resources by retrieving the value of the <see cref="T:System.Resources.NeutralResourcesLanguageAttribute" /> attribute on a specified assembly.</summary>
		/// <returns>The culture from the <see cref="T:System.Resources.NeutralResourcesLanguageAttribute" /> attribute, if found; otherwise, the invariant culture.</returns>
		/// <param name="a">The assembly for which to return culture-specific information. </param>
		// Token: 0x06001F2C RID: 7980 RVA: 0x000795F8 File Offset: 0x000777F8
		[SecuritySafeCritical]
		protected static CultureInfo GetNeutralResourcesLanguage(Assembly a)
		{
			UltimateResourceFallbackLocation ultimateResourceFallbackLocation = UltimateResourceFallbackLocation.MainAssembly;
			return ManifestBasedResourceGroveler.GetNeutralResourcesLanguage(a, ref ultimateResourceFallbackLocation);
		}

		// Token: 0x06001F2D RID: 7981 RVA: 0x00079610 File Offset: 0x00077810
		internal static bool CompareNames(string asmTypeName1, string typeName2, AssemblyName asmName2)
		{
			int num = asmTypeName1.IndexOf(',');
			if (((num == -1) ? asmTypeName1.Length : num) != typeName2.Length)
			{
				return false;
			}
			if (string.Compare(asmTypeName1, 0, typeName2, 0, typeName2.Length, StringComparison.Ordinal) != 0)
			{
				return false;
			}
			if (num == -1)
			{
				return true;
			}
			while (char.IsWhiteSpace(asmTypeName1[++num]))
			{
			}
			AssemblyName assemblyName = new AssemblyName(asmTypeName1.Substring(num));
			if (string.Compare(assemblyName.Name, asmName2.Name, StringComparison.OrdinalIgnoreCase) != 0)
			{
				return false;
			}
			if (string.Compare(assemblyName.Name, "mscorlib", StringComparison.OrdinalIgnoreCase) == 0)
			{
				return true;
			}
			if (assemblyName.CultureInfo != null && asmName2.CultureInfo != null && assemblyName.CultureInfo.LCID != asmName2.CultureInfo.LCID)
			{
				return false;
			}
			byte[] publicKeyToken = assemblyName.GetPublicKeyToken();
			byte[] publicKeyToken2 = asmName2.GetPublicKeyToken();
			if (publicKeyToken != null && publicKeyToken2 != null)
			{
				if (publicKeyToken.Length != publicKeyToken2.Length)
				{
					return false;
				}
				for (int i = 0; i < publicKeyToken.Length; i++)
				{
					if (publicKeyToken[i] != publicKeyToken2[i])
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06001F2E RID: 7982 RVA: 0x00002194 File Offset: 0x00000394
		private void SetAppXConfiguration()
		{
		}

		/// <summary>Returns the value of the specified string resource.</summary>
		/// <returns>The value of the resource localized for the caller's current UI culture, or null if <paramref name="name" /> cannot be found in a resource set.</returns>
		/// <param name="name">The name of the resource to retrieve. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The value of the specified resource is not a string. </exception>
		/// <exception cref="T:System.Resources.MissingManifestResourceException">No usable set of resources has been found, and there are no resources for the default culture. For information about how to handle this exception, see the "Handling MissingManifestResourceException and MissingSatelliteAssemblyException Exceptions" section in the <see cref="T:System.Resources.ResourceManager" /> class topic. </exception>
		/// <exception cref="T:System.Resources.MissingSatelliteAssemblyException">The default culture's resources reside in a satellite assembly that could not be found. For information about how to handle this exception, see the "Handling MissingManifestResourceException and MissingSatelliteAssemblyException Exceptions" section in the <see cref="T:System.Resources.ResourceManager" /> class topic.</exception>
		// Token: 0x06001F2F RID: 7983 RVA: 0x00079708 File Offset: 0x00077908
		public virtual string GetString(string name)
		{
			return this.GetString(name, null);
		}

		/// <summary>Returns the value of the string resource localized for the specified culture.</summary>
		/// <returns>The value of the resource localized for the specified culture, or null if <paramref name="name" /> cannot be found in a resource set.</returns>
		/// <param name="name">The name of the resource to retrieve. </param>
		/// <param name="culture">An object that represents the culture for which the resource is localized. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The value of the specified resource is not a string. </exception>
		/// <exception cref="T:System.Resources.MissingManifestResourceException">No usable set of resources has been found, and there are no resources for a default culture. For information about how to handle this exception, see the "Handling MissingManifestResourceException and MissingSatelliteAssemblyException Exceptions" section in the <see cref="T:System.Resources.ResourceManager" /> class topic. </exception>
		/// <exception cref="T:System.Resources.MissingSatelliteAssemblyException">The default culture's resources reside in a satellite assembly that could not be found. For information about how to handle this exception, see the "Handling MissingManifestResourceException and MissingSatelliteAssemblyException Exceptions" section in the <see cref="T:System.Resources.ResourceManager" /> class topic.</exception>
		// Token: 0x06001F30 RID: 7984 RVA: 0x00079714 File Offset: 0x00077914
		public virtual string GetString(string name, CultureInfo culture)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (culture == null)
			{
				culture = Thread.CurrentThread.GetCurrentUICultureNoAppX();
			}
			ResourceSet resourceSet = this.GetFirstResourceSet(culture);
			if (resourceSet != null)
			{
				string @string = resourceSet.GetString(name, this._ignoreCase);
				if (@string != null)
				{
					return @string;
				}
			}
			foreach (CultureInfo cultureInfo in new ResourceFallbackManager(culture, this._neutralResourcesCulture, true))
			{
				ResourceSet resourceSet2 = this.InternalGetResourceSet(cultureInfo, true, true);
				if (resourceSet2 == null)
				{
					break;
				}
				if (resourceSet2 != resourceSet)
				{
					string string2 = resourceSet2.GetString(name, this._ignoreCase);
					if (string2 != null)
					{
						if (this._lastUsedResourceCache != null)
						{
							ResourceManager.CultureNameResourceSetPair lastUsedResourceCache = this._lastUsedResourceCache;
							lock (lastUsedResourceCache)
							{
								this._lastUsedResourceCache.lastCultureName = cultureInfo.Name;
								this._lastUsedResourceCache.lastResourceSet = resourceSet2;
							}
						}
						return string2;
					}
					resourceSet = resourceSet2;
				}
			}
			return null;
		}

		/// <summary>Returns the value of the specified non-string resource.</summary>
		/// <returns>The value of the resource localized for the caller's current culture settings. If an appropriate resource set exists but <paramref name="name" /> cannot be found, the method returns null. </returns>
		/// <param name="name">The name of the resource to get. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is null. </exception>
		/// <exception cref="T:System.Resources.MissingManifestResourceException">No usable set of localized resources has been found, and there are no default culture resources. For information about how to handle this exception, see the "Handling MissingManifestResourceException and MissingSatelliteAssemblyException Exceptions" section in the <see cref="T:System.Resources.ResourceManager" /> class topic.</exception>
		/// <exception cref="T:System.Resources.MissingSatelliteAssemblyException">The default culture's resources reside in a satellite assembly that could not be found. For information about how to handle this exception, see the "Handling MissingManifestResourceException and MissingSatelliteAssemblyException Exceptions" section in the <see cref="T:System.Resources.ResourceManager" /> class topic.</exception>
		// Token: 0x06001F31 RID: 7985 RVA: 0x00079830 File Offset: 0x00077A30
		public virtual object GetObject(string name)
		{
			return this.GetObject(name, null, true);
		}

		/// <summary>Gets the value of the specified non-string resource localized for the specified culture.</summary>
		/// <returns>The value of the resource, localized for the specified culture. If an appropriate resource set exists but <paramref name="name" /> cannot be found, the method returns null.</returns>
		/// <param name="name">The name of the resource to get. </param>
		/// <param name="culture">The culture for which the resource is localized. If the resource is not localized for this culture, the resource manager uses fallback rules to locate an appropriate resource.If this value is null, the <see cref="T:System.Globalization.CultureInfo" /> object is obtained by using the <see cref="P:System.Globalization.CultureInfo.CurrentUICulture" /> property. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is null. </exception>
		/// <exception cref="T:System.Resources.MissingManifestResourceException">No usable set of resources have been found, and there are no default culture resources. For information about how to handle this exception, see the "Handling MissingManifestResourceException and MissingSatelliteAssemblyException Exceptions" section in the <see cref="T:System.Resources.ResourceManager" /> class topic. </exception>
		/// <exception cref="T:System.Resources.MissingSatelliteAssemblyException">The default culture's resources reside in a satellite assembly that could not be found. For information about how to handle this exception, see the "Handling MissingManifestResourceException and MissingSatelliteAssemblyException Exceptions" section in the <see cref="T:System.Resources.ResourceManager" /> class topic. </exception>
		// Token: 0x06001F32 RID: 7986 RVA: 0x0007983B File Offset: 0x00077A3B
		public virtual object GetObject(string name, CultureInfo culture)
		{
			return this.GetObject(name, culture, true);
		}

		// Token: 0x06001F33 RID: 7987 RVA: 0x00079848 File Offset: 0x00077A48
		private object GetObject(string name, CultureInfo culture, bool wrapUnmanagedMemStream)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (culture == null)
			{
				culture = Thread.CurrentThread.GetCurrentUICultureNoAppX();
			}
			ResourceSet resourceSet = this.GetFirstResourceSet(culture);
			if (resourceSet != null)
			{
				object @object = resourceSet.GetObject(name, this._ignoreCase);
				if (@object != null)
				{
					UnmanagedMemoryStream unmanagedMemoryStream = @object as UnmanagedMemoryStream;
					if (unmanagedMemoryStream != null && wrapUnmanagedMemStream)
					{
						return new UnmanagedMemoryStreamWrapper(unmanagedMemoryStream);
					}
					return @object;
				}
			}
			foreach (CultureInfo cultureInfo in new ResourceFallbackManager(culture, this._neutralResourcesCulture, true))
			{
				ResourceSet resourceSet2 = this.InternalGetResourceSet(cultureInfo, true, true);
				if (resourceSet2 == null)
				{
					break;
				}
				if (resourceSet2 != resourceSet)
				{
					object object2 = resourceSet2.GetObject(name, this._ignoreCase);
					if (object2 != null)
					{
						if (this._lastUsedResourceCache != null)
						{
							ResourceManager.CultureNameResourceSetPair lastUsedResourceCache = this._lastUsedResourceCache;
							lock (lastUsedResourceCache)
							{
								this._lastUsedResourceCache.lastCultureName = cultureInfo.Name;
								this._lastUsedResourceCache.lastResourceSet = resourceSet2;
							}
						}
						UnmanagedMemoryStream unmanagedMemoryStream2 = object2 as UnmanagedMemoryStream;
						if (unmanagedMemoryStream2 != null && wrapUnmanagedMemStream)
						{
							return new UnmanagedMemoryStreamWrapper(unmanagedMemoryStream2);
						}
						return object2;
					}
					else
					{
						resourceSet = resourceSet2;
					}
				}
			}
			return null;
		}

		/// <summary>Returns an unmanaged memory stream object from the specified resource.</summary>
		/// <returns>An unmanaged memory stream object that represents a resource .</returns>
		/// <param name="name">The name of a resource.</param>
		/// <exception cref="T:System.InvalidOperationException">The value of the specified resource is not a <see cref="T:System.IO.MemoryStream" /> object.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null.</exception>
		/// <exception cref="T:System.Resources.MissingManifestResourceException">No usable set of resources is found, and there are no default resources. For information about how to handle this exception, see the "Handling MissingManifestResourceException and MissingSatelliteAssemblyException Exceptions" section in the <see cref="T:System.Resources.ResourceManager" /> class topic. </exception>
		/// <exception cref="T:System.Resources.MissingSatelliteAssemblyException">The default culture's resources reside in a satellite assembly that could not be found. For information about how to handle this exception, see the "Handling MissingManifestResourceException and MissingSatelliteAssemblyException Exceptions" section in the <see cref="T:System.Resources.ResourceManager" /> class topic.</exception>
		// Token: 0x06001F34 RID: 7988 RVA: 0x000799A0 File Offset: 0x00077BA0
		[ComVisible(false)]
		public UnmanagedMemoryStream GetStream(string name)
		{
			return this.GetStream(name, null);
		}

		/// <summary>Returns an unmanaged memory stream object from the specified resource, using the specified culture.</summary>
		/// <returns>An unmanaged memory stream object that represents a resource.</returns>
		/// <param name="name">The name of a resource.</param>
		/// <param name="culture">An  object that specifies the culture to use for the resource lookup. If <paramref name="culture" /> is null, the culture for the current thread is used.</param>
		/// <exception cref="T:System.InvalidOperationException">The value of the specified resource is not a <see cref="T:System.IO.MemoryStream" /> object.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null.</exception>
		/// <exception cref="T:System.Resources.MissingManifestResourceException">No usable set of resources is found, and there are no default resources. For information about how to handle this exception, see the "Handling MissingManifestResourceException and MissingSatelliteAssemblyException Exceptions" section in the <see cref="T:System.Resources.ResourceManager" /> class topic. </exception>
		/// <exception cref="T:System.Resources.MissingSatelliteAssemblyException">The default culture's resources reside in a satellite assembly that could not be found. For information about how to handle this exception, see the "Handling MissingManifestResourceException and MissingSatelliteAssemblyException Exceptions" section in the <see cref="T:System.Resources.ResourceManager" /> class topic.</exception>
		// Token: 0x06001F35 RID: 7989 RVA: 0x000799AC File Offset: 0x00077BAC
		[ComVisible(false)]
		public UnmanagedMemoryStream GetStream(string name, CultureInfo culture)
		{
			object @object = this.GetObject(name, culture, false);
			UnmanagedMemoryStream unmanagedMemoryStream = @object as UnmanagedMemoryStream;
			if (unmanagedMemoryStream == null && @object != null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Resource '{0}' was not a Stream - call GetObject instead.", new object[] { name }));
			}
			return unmanagedMemoryStream;
		}

		/// <summary>Specifies the root name of the resource files that the <see cref="T:System.Resources.ResourceManager" /> searches for resources.</summary>
		// Token: 0x040010C6 RID: 4294
		protected string BaseNameField;

		/// <summary>Contains a <see cref="T:System.Collections.Hashtable" /> that returns a mapping from cultures to <see cref="T:System.Resources.ResourceSet" /> objects. </summary>
		// Token: 0x040010C7 RID: 4295
		[Obsolete("call InternalGetResourceSet instead")]
		protected Hashtable ResourceSets;

		// Token: 0x040010C8 RID: 4296
		[NonSerialized]
		private Dictionary<string, ResourceSet> _resourceSets;

		// Token: 0x040010C9 RID: 4297
		private string moduleDir;

		/// <summary>Specifies the main assembly that contains the resources.</summary>
		// Token: 0x040010CA RID: 4298
		protected Assembly MainAssembly;

		// Token: 0x040010CB RID: 4299
		private Type _locationInfo;

		// Token: 0x040010CC RID: 4300
		private Type _userResourceSet;

		// Token: 0x040010CD RID: 4301
		private CultureInfo _neutralResourcesCulture;

		// Token: 0x040010CE RID: 4302
		[NonSerialized]
		private ResourceManager.CultureNameResourceSetPair _lastUsedResourceCache;

		// Token: 0x040010CF RID: 4303
		private bool _ignoreCase;

		// Token: 0x040010D0 RID: 4304
		private bool UseManifest;

		// Token: 0x040010D1 RID: 4305
		[OptionalField(VersionAdded = 1)]
		private bool UseSatelliteAssem;

		// Token: 0x040010D2 RID: 4306
		[OptionalField]
		private UltimateResourceFallbackLocation _fallbackLoc;

		// Token: 0x040010D3 RID: 4307
		[OptionalField]
		private Version _satelliteContractVersion;

		// Token: 0x040010D4 RID: 4308
		[OptionalField]
		private bool _lookedForSatelliteContractVersion;

		// Token: 0x040010D5 RID: 4309
		[OptionalField(VersionAdded = 1)]
		private Assembly _callingAssembly;

		// Token: 0x040010D6 RID: 4310
		[OptionalField(VersionAdded = 4)]
		private RuntimeAssembly m_callingAssembly;

		// Token: 0x040010D7 RID: 4311
		[NonSerialized]
		private IResourceGroveler resourceGroveler;

		/// <summary>Holds the number used to identify resource files.</summary>
		// Token: 0x040010D8 RID: 4312
		public static readonly int MagicNumber = -1091581234;

		/// <summary> Specifies the version of resource file headers that the current implementation of <see cref="T:System.Resources.ResourceManager" /> can interpret and produce.</summary>
		// Token: 0x040010D9 RID: 4313
		public static readonly int HeaderVersionNumber = 1;

		// Token: 0x040010DA RID: 4314
		private static readonly Type _minResourceSet = typeof(ResourceSet);

		// Token: 0x040010DB RID: 4315
		internal static readonly string ResReaderTypeName = typeof(ResourceReader).FullName;

		// Token: 0x040010DC RID: 4316
		internal static readonly string ResSetTypeName = typeof(RuntimeResourceSet).FullName;

		// Token: 0x040010DD RID: 4317
		internal static readonly string MscorlibName = typeof(ResourceReader).Assembly.FullName;

		// Token: 0x040010DE RID: 4318
		internal const string ResFileExtension = ".resources";

		// Token: 0x040010DF RID: 4319
		internal const int ResFileExtensionLength = 10;

		// Token: 0x040010E0 RID: 4320
		internal static readonly int DEBUG = 0;

		// Token: 0x020002A7 RID: 679
		internal class CultureNameResourceSetPair
		{
			// Token: 0x040010E1 RID: 4321
			public string lastCultureName;

			// Token: 0x040010E2 RID: 4322
			public ResourceSet lastResourceSet;
		}

		// Token: 0x020002A8 RID: 680
		internal class ResourceManagerMediator
		{
			// Token: 0x06001F38 RID: 7992 RVA: 0x00079A5F File Offset: 0x00077C5F
			internal ResourceManagerMediator(ResourceManager rm)
			{
				if (rm == null)
				{
					throw new ArgumentNullException("rm");
				}
				this._rm = rm;
			}

			// Token: 0x17000442 RID: 1090
			// (get) Token: 0x06001F39 RID: 7993 RVA: 0x00079A7C File Offset: 0x00077C7C
			internal string ModuleDir
			{
				get
				{
					return this._rm.moduleDir;
				}
			}

			// Token: 0x17000443 RID: 1091
			// (get) Token: 0x06001F3A RID: 7994 RVA: 0x00079A89 File Offset: 0x00077C89
			internal Type LocationInfo
			{
				get
				{
					return this._rm._locationInfo;
				}
			}

			// Token: 0x17000444 RID: 1092
			// (get) Token: 0x06001F3B RID: 7995 RVA: 0x00079A96 File Offset: 0x00077C96
			internal Type UserResourceSet
			{
				get
				{
					return this._rm._userResourceSet;
				}
			}

			// Token: 0x17000445 RID: 1093
			// (get) Token: 0x06001F3C RID: 7996 RVA: 0x00079AA3 File Offset: 0x00077CA3
			internal string BaseNameField
			{
				get
				{
					return this._rm.BaseNameField;
				}
			}

			// Token: 0x17000446 RID: 1094
			// (get) Token: 0x06001F3D RID: 7997 RVA: 0x00079AB0 File Offset: 0x00077CB0
			// (set) Token: 0x06001F3E RID: 7998 RVA: 0x00079ABD File Offset: 0x00077CBD
			internal CultureInfo NeutralResourcesCulture
			{
				get
				{
					return this._rm._neutralResourcesCulture;
				}
				set
				{
					this._rm._neutralResourcesCulture = value;
				}
			}

			// Token: 0x06001F3F RID: 7999 RVA: 0x00079ACB File Offset: 0x00077CCB
			internal string GetResourceFileName(CultureInfo culture)
			{
				return this._rm.GetResourceFileName(culture);
			}

			// Token: 0x17000447 RID: 1095
			// (get) Token: 0x06001F40 RID: 8000 RVA: 0x00079AD9 File Offset: 0x00077CD9
			// (set) Token: 0x06001F41 RID: 8001 RVA: 0x00079AE6 File Offset: 0x00077CE6
			internal bool LookedForSatelliteContractVersion
			{
				get
				{
					return this._rm._lookedForSatelliteContractVersion;
				}
				set
				{
					this._rm._lookedForSatelliteContractVersion = value;
				}
			}

			// Token: 0x17000448 RID: 1096
			// (get) Token: 0x06001F42 RID: 8002 RVA: 0x00079AF4 File Offset: 0x00077CF4
			// (set) Token: 0x06001F43 RID: 8003 RVA: 0x00079B01 File Offset: 0x00077D01
			internal Version SatelliteContractVersion
			{
				get
				{
					return this._rm._satelliteContractVersion;
				}
				set
				{
					this._rm._satelliteContractVersion = value;
				}
			}

			// Token: 0x06001F44 RID: 8004 RVA: 0x00079B0F File Offset: 0x00077D0F
			internal Version ObtainSatelliteContractVersion(Assembly a)
			{
				return ResourceManager.GetSatelliteContractVersion(a);
			}

			// Token: 0x17000449 RID: 1097
			// (get) Token: 0x06001F45 RID: 8005 RVA: 0x00079B17 File Offset: 0x00077D17
			// (set) Token: 0x06001F46 RID: 8006 RVA: 0x00079B24 File Offset: 0x00077D24
			internal UltimateResourceFallbackLocation FallbackLoc
			{
				get
				{
					return this._rm.FallbackLocation;
				}
				set
				{
					this._rm._fallbackLoc = value;
				}
			}

			// Token: 0x1700044A RID: 1098
			// (get) Token: 0x06001F47 RID: 8007 RVA: 0x00079B32 File Offset: 0x00077D32
			internal RuntimeAssembly CallingAssembly
			{
				get
				{
					return this._rm.m_callingAssembly;
				}
			}

			// Token: 0x1700044B RID: 1099
			// (get) Token: 0x06001F48 RID: 8008 RVA: 0x00079B3F File Offset: 0x00077D3F
			internal RuntimeAssembly MainAssembly
			{
				get
				{
					return (RuntimeAssembly)this._rm.MainAssembly;
				}
			}

			// Token: 0x1700044C RID: 1100
			// (get) Token: 0x06001F49 RID: 8009 RVA: 0x00079B51 File Offset: 0x00077D51
			internal string BaseName
			{
				get
				{
					return this._rm.BaseName;
				}
			}

			// Token: 0x040010E3 RID: 4323
			private ResourceManager _rm;
		}
	}
}
