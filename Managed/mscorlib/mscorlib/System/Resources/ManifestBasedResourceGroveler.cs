using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading;

namespace System.Resources
{
	// Token: 0x020002A0 RID: 672
	internal class ManifestBasedResourceGroveler : IResourceGroveler
	{
		// Token: 0x06001EEE RID: 7918 RVA: 0x00077F04 File Offset: 0x00076104
		public ManifestBasedResourceGroveler(ResourceManager.ResourceManagerMediator mediator)
		{
			this._mediator = mediator;
		}

		// Token: 0x06001EEF RID: 7919 RVA: 0x00077F14 File Offset: 0x00076114
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public ResourceSet GrovelForResourceSet(CultureInfo culture, Dictionary<string, ResourceSet> localResourceSets, bool tryParents, bool createIfNotExists, ref StackCrawlMark stackMark)
		{
			ResourceSet resourceSet = null;
			Stream stream = null;
			RuntimeAssembly runtimeAssembly = null;
			CultureInfo cultureInfo = this.UltimateFallbackFixup(culture);
			if (cultureInfo.HasInvariantCultureName && this._mediator.FallbackLoc == UltimateResourceFallbackLocation.MainAssembly)
			{
				runtimeAssembly = this._mediator.MainAssembly;
			}
			else
			{
				runtimeAssembly = this.GetSatelliteAssembly(cultureInfo, ref stackMark);
				if (runtimeAssembly == null && (culture.HasInvariantCultureName && this._mediator.FallbackLoc == UltimateResourceFallbackLocation.Satellite))
				{
					this.HandleSatelliteMissing();
				}
			}
			string resourceFileName = this._mediator.GetResourceFileName(cultureInfo);
			if (runtimeAssembly != null)
			{
				lock (localResourceSets)
				{
					localResourceSets.TryGetValue(culture.Name, out resourceSet);
				}
				stream = this.GetManifestResourceStream(runtimeAssembly, resourceFileName, ref stackMark);
			}
			if (createIfNotExists && stream != null && resourceSet == null)
			{
				resourceSet = this.CreateResourceSet(stream, runtimeAssembly);
			}
			else if (stream == null && tryParents && culture.HasInvariantCultureName)
			{
				this.HandleResourceStreamMissing(resourceFileName);
			}
			return resourceSet;
		}

		// Token: 0x06001EF0 RID: 7920 RVA: 0x00078014 File Offset: 0x00076214
		public bool HasNeutralResources(CultureInfo culture, string defaultResName)
		{
			string text = defaultResName;
			if (this._mediator.LocationInfo != null && this._mediator.LocationInfo.Namespace != null)
			{
				text = this._mediator.LocationInfo.Namespace + Type.Delimiter.ToString() + defaultResName;
			}
			string[] manifestResourceNames = this._mediator.MainAssembly.GetManifestResourceNames();
			for (int i = 0; i < manifestResourceNames.Length; i++)
			{
				if (manifestResourceNames[i].Equals(text))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001EF1 RID: 7921 RVA: 0x0007809C File Offset: 0x0007629C
		private CultureInfo UltimateFallbackFixup(CultureInfo lookForCulture)
		{
			CultureInfo cultureInfo = lookForCulture;
			if (lookForCulture.Name == this._mediator.NeutralResourcesCulture.Name && this._mediator.FallbackLoc == UltimateResourceFallbackLocation.MainAssembly)
			{
				cultureInfo = CultureInfo.InvariantCulture;
			}
			else if (lookForCulture.HasInvariantCultureName && this._mediator.FallbackLoc == UltimateResourceFallbackLocation.Satellite)
			{
				cultureInfo = this._mediator.NeutralResourcesCulture;
			}
			return cultureInfo;
		}

		// Token: 0x06001EF2 RID: 7922 RVA: 0x00078100 File Offset: 0x00076300
		[SecurityCritical]
		internal static CultureInfo GetNeutralResourcesLanguage(Assembly a, ref UltimateResourceFallbackLocation fallbackLocation)
		{
			string text = null;
			short num = 0;
			if (!ManifestBasedResourceGroveler.GetNeutralResourcesLanguageAttribute(a, ref text, ref num))
			{
				fallbackLocation = UltimateResourceFallbackLocation.MainAssembly;
				return CultureInfo.InvariantCulture;
			}
			if (num < 0 || num > 1)
			{
				throw new ArgumentException(Environment.GetResourceString("The NeutralResourcesLanguageAttribute specifies an invalid or unrecognized ultimate resource fallback location: \"{0}\".", new object[] { num }));
			}
			fallbackLocation = (UltimateResourceFallbackLocation)num;
			CultureInfo cultureInfo;
			try
			{
				cultureInfo = CultureInfo.GetCultureInfo(text);
			}
			catch (ArgumentException ex)
			{
				if (!(a == typeof(object).Assembly))
				{
					throw new ArgumentException(Environment.GetResourceString("The NeutralResourcesLanguageAttribute on the assembly \"{0}\" specifies an invalid culture name: \"{1}\".", new object[]
					{
						a.ToString(),
						text
					}), ex);
				}
				cultureInfo = CultureInfo.InvariantCulture;
			}
			return cultureInfo;
		}

		// Token: 0x06001EF3 RID: 7923 RVA: 0x000781B4 File Offset: 0x000763B4
		[SecurityCritical]
		internal ResourceSet CreateResourceSet(Stream store, Assembly assembly)
		{
			if (store.CanSeek && store.Length > 4L)
			{
				long position = store.Position;
				BinaryReader binaryReader = new BinaryReader(store);
				if (binaryReader.ReadInt32() == ResourceManager.MagicNumber)
				{
					int num = binaryReader.ReadInt32();
					string text;
					string text2;
					if (num == ResourceManager.HeaderVersionNumber)
					{
						binaryReader.ReadInt32();
						text = binaryReader.ReadString();
						text2 = binaryReader.ReadString();
					}
					else
					{
						if (num <= ResourceManager.HeaderVersionNumber)
						{
							throw new NotSupportedException(Environment.GetResourceString("Found an obsolete .resources file in assembly '{0}'. Rebuild that .resources file then rebuild that assembly.", new object[] { this._mediator.MainAssembly.GetSimpleName() }));
						}
						int num2 = binaryReader.ReadInt32();
						long num3 = binaryReader.BaseStream.Position + (long)num2;
						text = binaryReader.ReadString();
						text2 = binaryReader.ReadString();
						binaryReader.BaseStream.Seek(num3, SeekOrigin.Begin);
					}
					store.Position = position;
					if (this.CanUseDefaultResourceClasses(text, text2))
					{
						return new RuntimeResourceSet(store);
					}
					IResourceReader resourceReader = (IResourceReader)Activator.CreateInstance(Type.GetType(text, true), new object[] { store });
					object[] array = new object[] { resourceReader };
					Type type;
					if (this._mediator.UserResourceSet == null)
					{
						type = Type.GetType(text2, true, false);
					}
					else
					{
						type = this._mediator.UserResourceSet;
					}
					return (ResourceSet)Activator.CreateInstance(type, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.CreateInstance, null, array, null, null);
				}
				else
				{
					store.Position = position;
				}
			}
			if (this._mediator.UserResourceSet == null)
			{
				return new RuntimeResourceSet(store);
			}
			object[] array2 = new object[] { store, assembly };
			ResourceSet resourceSet;
			try
			{
				try
				{
					return (ResourceSet)Activator.CreateInstance(this._mediator.UserResourceSet, array2);
				}
				catch (MissingMethodException)
				{
				}
				array2 = new object[] { store };
				resourceSet = (ResourceSet)Activator.CreateInstance(this._mediator.UserResourceSet, array2);
			}
			catch (MissingMethodException ex)
			{
				throw new InvalidOperationException(Environment.GetResourceString("'{0}': ResourceSet derived classes must provide a constructor that takes a String file name and a constructor that takes a Stream.", new object[] { this._mediator.UserResourceSet.AssemblyQualifiedName }), ex);
			}
			return resourceSet;
		}

		// Token: 0x06001EF4 RID: 7924 RVA: 0x000783E0 File Offset: 0x000765E0
		[SecurityCritical]
		private Stream GetManifestResourceStream(RuntimeAssembly satellite, string fileName, ref StackCrawlMark stackMark)
		{
			bool flag = this._mediator.MainAssembly == satellite && this._mediator.CallingAssembly == this._mediator.MainAssembly;
			Stream stream = satellite.GetManifestResourceStream(this._mediator.LocationInfo, fileName, flag, ref stackMark);
			if (stream == null)
			{
				stream = this.CaseInsensitiveManifestResourceStreamLookup(satellite, fileName);
			}
			return stream;
		}

		// Token: 0x06001EF5 RID: 7925 RVA: 0x00078444 File Offset: 0x00076644
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		private Stream CaseInsensitiveManifestResourceStreamLookup(RuntimeAssembly satellite, string name)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this._mediator.LocationInfo != null)
			{
				string @namespace = this._mediator.LocationInfo.Namespace;
				if (@namespace != null)
				{
					stringBuilder.Append(@namespace);
					if (name != null)
					{
						stringBuilder.Append(Type.Delimiter);
					}
				}
			}
			stringBuilder.Append(name);
			string text = stringBuilder.ToString();
			CompareInfo compareInfo = CultureInfo.InvariantCulture.CompareInfo;
			string text2 = null;
			foreach (string text3 in satellite.GetManifestResourceNames())
			{
				if (compareInfo.Compare(text3, text, CompareOptions.IgnoreCase) == 0)
				{
					if (text2 != null)
					{
						throw new MissingManifestResourceException(Environment.GetResourceString("A case-insensitive lookup for resource file \"{0}\" in assembly \"{1}\" found multiple entries. Remove the duplicates or specify the exact case.", new object[]
						{
							text,
							satellite.ToString()
						}));
					}
					text2 = text3;
				}
			}
			if (text2 == null)
			{
				return null;
			}
			bool flag = this._mediator.MainAssembly == satellite && this._mediator.CallingAssembly == this._mediator.MainAssembly;
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return satellite.GetManifestResourceStream(text2, ref stackCrawlMark, flag);
		}

		// Token: 0x06001EF6 RID: 7926 RVA: 0x00078554 File Offset: 0x00076754
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		private RuntimeAssembly GetSatelliteAssembly(CultureInfo lookForCulture, ref StackCrawlMark stackMark)
		{
			if (!this._mediator.LookedForSatelliteContractVersion)
			{
				this._mediator.SatelliteContractVersion = this._mediator.ObtainSatelliteContractVersion(this._mediator.MainAssembly);
				this._mediator.LookedForSatelliteContractVersion = true;
			}
			RuntimeAssembly runtimeAssembly = null;
			string satelliteAssemblyName = this.GetSatelliteAssemblyName();
			try
			{
				runtimeAssembly = this._mediator.MainAssembly.InternalGetSatelliteAssembly(satelliteAssemblyName, lookForCulture, this._mediator.SatelliteContractVersion, false, ref stackMark);
			}
			catch (FileLoadException)
			{
			}
			catch (BadImageFormatException)
			{
			}
			return runtimeAssembly;
		}

		// Token: 0x06001EF7 RID: 7927 RVA: 0x000785EC File Offset: 0x000767EC
		private bool CanUseDefaultResourceClasses(string readerTypeName, string resSetTypeName)
		{
			if (this._mediator.UserResourceSet != null)
			{
				return false;
			}
			AssemblyName assemblyName = new AssemblyName(ResourceManager.MscorlibName);
			return (readerTypeName == null || ResourceManager.CompareNames(readerTypeName, ResourceManager.ResReaderTypeName, assemblyName)) && (resSetTypeName == null || ResourceManager.CompareNames(resSetTypeName, ResourceManager.ResSetTypeName, assemblyName));
		}

		// Token: 0x06001EF8 RID: 7928 RVA: 0x00078640 File Offset: 0x00076840
		[SecurityCritical]
		private string GetSatelliteAssemblyName()
		{
			return this._mediator.MainAssembly.GetSimpleName() + ".resources";
		}

		// Token: 0x06001EF9 RID: 7929 RVA: 0x0007865C File Offset: 0x0007685C
		[SecurityCritical]
		private void HandleSatelliteMissing()
		{
			string text = this._mediator.MainAssembly.GetSimpleName() + ".resources.dll";
			if (this._mediator.SatelliteContractVersion != null)
			{
				text = text + ", Version=" + this._mediator.SatelliteContractVersion.ToString();
			}
			AssemblyName assemblyName = new AssemblyName();
			assemblyName.SetPublicKey(this._mediator.MainAssembly.GetPublicKey());
			byte[] publicKeyToken = assemblyName.GetPublicKeyToken();
			int num = publicKeyToken.Length;
			StringBuilder stringBuilder = new StringBuilder(num * 2);
			for (int i = 0; i < num; i++)
			{
				stringBuilder.Append(publicKeyToken[i].ToString("x", CultureInfo.InvariantCulture));
			}
			text = text + ", PublicKeyToken=" + stringBuilder;
			string text2 = this._mediator.NeutralResourcesCulture.Name;
			if (text2.Length == 0)
			{
				text2 = "<invariant>";
			}
			throw new MissingSatelliteAssemblyException(Environment.GetResourceString("The satellite assembly named \"{1}\" for fallback culture \"{0}\" either could not be found or could not be loaded. This is generally a setup problem. Please consider reinstalling or repairing the application.", new object[]
			{
				this._mediator.NeutralResourcesCulture,
				text
			}), text2);
		}

		// Token: 0x06001EFA RID: 7930 RVA: 0x00078768 File Offset: 0x00076968
		[SecurityCritical]
		private void HandleResourceStreamMissing(string fileName)
		{
			if (this._mediator.MainAssembly == typeof(object).Assembly && this._mediator.BaseName.Equals("mscorlib"))
			{
				Environment.FailFast("mscorlib.resources couldn't be found!  Large parts of the BCL won't work!");
			}
			string text = string.Empty;
			if (this._mediator.LocationInfo != null && this._mediator.LocationInfo.Namespace != null)
			{
				text = this._mediator.LocationInfo.Namespace + Type.Delimiter.ToString();
			}
			text += fileName;
			throw new MissingManifestResourceException(Environment.GetResourceString("Could not find any resources appropriate for the specified culture or the neutral culture.  Make sure \"{0}\" was correctly embedded or linked into assembly \"{1}\" at compile time, or that all the satellite assemblies required are loadable and fully signed.", new object[]
			{
				text,
				this._mediator.MainAssembly.GetSimpleName()
			}));
		}

		// Token: 0x06001EFB RID: 7931 RVA: 0x0007883C File Offset: 0x00076A3C
		private static bool GetNeutralResourcesLanguageAttribute(Assembly assembly, ref string cultureName, ref short fallbackLocation)
		{
			NeutralResourcesLanguageAttribute customAttribute = assembly.GetCustomAttribute<NeutralResourcesLanguageAttribute>();
			if (customAttribute == null)
			{
				return false;
			}
			cultureName = customAttribute.CultureName;
			fallbackLocation = (short)customAttribute.Location;
			return true;
		}

		// Token: 0x040010BA RID: 4282
		private ResourceManager.ResourceManagerMediator _mediator;
	}
}
