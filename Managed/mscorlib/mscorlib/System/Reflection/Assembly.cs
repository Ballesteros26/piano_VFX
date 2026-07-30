using System;
using System.Collections.Generic;
using System.Configuration.Assemblies;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;
using System.Text;
using System.Threading;
using Mono;

namespace System.Reflection
{
	/// <summary>Represents an assembly, which is a reusable, versionable, and self-describing building block of a common language runtime application.</summary>
	// Token: 0x0200030A RID: 778
	[ComVisible(true)]
	[ComDefaultInterface(typeof(_Assembly))]
	[ClassInterface(ClassInterfaceType.None)]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public abstract class Assembly : ICustomAttributeProvider, _Assembly, IEvidenceFactory, ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.Assembly" /> class.</summary>
		// Token: 0x06002187 RID: 8583 RVA: 0x0007F8B5 File Offset: 0x0007DAB5
		protected Assembly()
		{
			this.resolve_event_holder = new Assembly.ResolveEventHolder();
		}

		/// <summary>Occurs when the common language runtime class loader cannot resolve a reference to an internal module of an assembly through normal means.</summary>
		// Token: 0x14000016 RID: 22
		// (add) Token: 0x06002188 RID: 8584 RVA: 0x0007F8C8 File Offset: 0x0007DAC8
		// (remove) Token: 0x06002189 RID: 8585 RVA: 0x0007F8D6 File Offset: 0x0007DAD6
		public virtual event ModuleResolveEventHandler ModuleResolve
		{
			[SecurityPermission(SecurityAction.LinkDemand, ControlAppDomain = true)]
			add
			{
				this.resolve_event_holder.ModuleResolve += value;
			}
			[SecurityPermission(SecurityAction.LinkDemand, ControlAppDomain = true)]
			remove
			{
				this.resolve_event_holder.ModuleResolve -= value;
			}
		}

		// Token: 0x0600218A RID: 8586
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern string get_code_base(bool escaped);

		// Token: 0x0600218B RID: 8587
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern string get_fullname();

		// Token: 0x0600218C RID: 8588
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern string get_location();

		// Token: 0x0600218D RID: 8589
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern string InternalImageRuntimeVersion();

		// Token: 0x0600218E RID: 8590
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string GetAotId();

		// Token: 0x0600218F RID: 8591 RVA: 0x0007F8E4 File Offset: 0x0007DAE4
		private string GetCodeBase(bool escaped)
		{
			string text = this.get_code_base(escaped);
			if (SecurityManager.SecurityEnabled && string.Compare("FILE://", 0, text, 0, 7, true, CultureInfo.InvariantCulture) == 0)
			{
				string text2 = text.Substring(7);
				new FileIOPermission(FileIOPermissionAccess.PathDiscovery, text2).Demand();
			}
			return text;
		}

		/// <summary>Gets the location of the assembly as specified originally, for example, in an <see cref="T:System.Reflection.AssemblyName" /> object.</summary>
		/// <returns>The location of the assembly as specified originally.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x06002190 RID: 8592 RVA: 0x0007F92B File Offset: 0x0007DB2B
		public virtual string CodeBase
		{
			get
			{
				return this.GetCodeBase(false);
			}
		}

		/// <summary>Gets the URI, including escape characters, that represents the codebase.</summary>
		/// <returns>A URI with escape characters.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x06002191 RID: 8593 RVA: 0x0007F934 File Offset: 0x0007DB34
		public virtual string EscapedCodeBase
		{
			[SecuritySafeCritical]
			get
			{
				return this.GetCodeBase(true);
			}
		}

		/// <summary>Gets the display name of the assembly.</summary>
		/// <returns>The display name of the assembly.</returns>
		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x06002192 RID: 8594 RVA: 0x0003D958 File Offset: 0x0003BB58
		public virtual string FullName
		{
			get
			{
				return this.ToString();
			}
		}

		/// <summary>Gets the entry point of this assembly.</summary>
		/// <returns>An object that represents the entry point of this assembly. If no entry point is found (for example, the assembly is a DLL), null is returned.</returns>
		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x06002193 RID: 8595
		public virtual extern MethodInfo EntryPoint
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		/// <summary>Gets the evidence for this assembly.</summary>
		/// <returns>The evidence for this assembly.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x06002194 RID: 8596 RVA: 0x0007F93D File Offset: 0x0007DB3D
		public virtual Evidence Evidence
		{
			[SecurityPermission(SecurityAction.Demand, ControlEvidence = true)]
			get
			{
				return this.UnprotectedGetEvidence();
			}
		}

		// Token: 0x06002195 RID: 8597 RVA: 0x0007F948 File Offset: 0x0007DB48
		internal Evidence UnprotectedGetEvidence()
		{
			if (this._evidence == null)
			{
				lock (this)
				{
					this._evidence = Evidence.GetDefaultHostEvidence(this);
				}
			}
			return this._evidence;
		}

		// Token: 0x06002196 RID: 8598
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern bool get_global_assembly_cache();

		// Token: 0x170004E7 RID: 1255
		// (set) Token: 0x06002197 RID: 8599 RVA: 0x0007F998 File Offset: 0x0007DB98
		internal bool FromByteArray
		{
			set
			{
				this.fromByteArray = value;
			}
		}

		/// <summary>Gets the full path or UNC location of the loaded file that contains the manifest.</summary>
		/// <returns>The location of the loaded file that contains the manifest. If the loaded file was shadow-copied, the location is that of the file after being shadow-copied. If the assembly is loaded from a byte array, such as when using the <see cref="M:System.Reflection.Assembly.Load(System.Byte[])" /> method overload, the value returned is an empty string ("").</returns>
		/// <exception cref="T:System.NotSupportedException">The current assembly is a dynamic assembly, represented by an <see cref="T:System.Reflection.Emit.AssemblyBuilder" /> object. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x06002198 RID: 8600 RVA: 0x0007F9A4 File Offset: 0x0007DBA4
		public virtual string Location
		{
			get
			{
				if (this.fromByteArray)
				{
					return string.Empty;
				}
				string location = this.get_location();
				if (location != string.Empty && SecurityManager.SecurityEnabled)
				{
					new FileIOPermission(FileIOPermissionAccess.PathDiscovery, location).Demand();
				}
				return location;
			}
		}

		/// <summary>Gets a string representing the version of the common language runtime (CLR) saved in the file containing the manifest.</summary>
		/// <returns>The CLR version folder name. This is not a full path.</returns>
		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x06002199 RID: 8601 RVA: 0x0007F9E7 File Offset: 0x0007DBE7
		[ComVisible(false)]
		public virtual string ImageRuntimeVersion
		{
			get
			{
				return this.InternalImageRuntimeVersion();
			}
		}

		/// <summary>Gets serialization information with all of the data needed to reinstantiate this assembly.</summary>
		/// <param name="info">The object to be populated with serialization information. </param>
		/// <param name="context">The destination context of the serialization. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="SerializationFormatter" />
		/// </PermissionSet>
		// Token: 0x0600219A RID: 8602 RVA: 0x0002126B File Offset: 0x0001F46B
		[SecurityCritical]
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new NotImplementedException();
		}

		/// <summary>Indicates whether or not a specified attribute has been applied to the assembly.</summary>
		/// <returns>true if the attribute has been applied to the assembly; otherwise, false.</returns>
		/// <param name="attributeType">The type of the attribute to be checked for this assembly. </param>
		/// <param name="inherit">This argument is ignored for objects of this type. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="attributeType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="attributeType" /> uses an invalid type.</exception>
		// Token: 0x0600219B RID: 8603 RVA: 0x000330F9 File Offset: 0x000312F9
		public virtual bool IsDefined(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.IsDefined(this, attributeType, inherit);
		}

		/// <summary>Gets all the custom attributes for this assembly.</summary>
		/// <returns>An array that contains the custom attributes for this assembly.</returns>
		/// <param name="inherit">This argument is ignored for objects of type <see cref="T:System.Reflection.Assembly" />. </param>
		// Token: 0x0600219C RID: 8604 RVA: 0x0007F7D9 File Offset: 0x0007D9D9
		public virtual object[] GetCustomAttributes(bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, inherit);
		}

		/// <summary>Gets the custom attributes for this assembly as specified by type.</summary>
		/// <returns>An array that contains the custom attributes for this assembly as specified by <paramref name="attributeType" />.</returns>
		/// <param name="attributeType">The type for which the custom attributes are to be returned. </param>
		/// <param name="inherit">This argument is ignored for objects of type <see cref="T:System.Reflection.Assembly" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="attributeType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="attributeType" /> is not a runtime type. </exception>
		// Token: 0x0600219D RID: 8605 RVA: 0x0007F7E2 File Offset: 0x0007D9E2
		public virtual object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, attributeType, inherit);
		}

		// Token: 0x0600219E RID: 8606
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern object GetFilesInternal(string name, bool getResourceModules);

		/// <summary>Gets the files in the file table of an assembly manifest.</summary>
		/// <returns>An array of streams that contain the files.</returns>
		/// <exception cref="T:System.IO.FileLoadException">A file that was found could not be loaded. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">A file was not found. </exception>
		/// <exception cref="T:System.BadImageFormatException">A file was not a valid assembly. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600219F RID: 8607 RVA: 0x0007F9EF File Offset: 0x0007DBEF
		public virtual FileStream[] GetFiles()
		{
			return this.GetFiles(false);
		}

		/// <summary>Gets the files in the file table of an assembly manifest, specifying whether to include resource modules.</summary>
		/// <returns>An array of streams that contain the files.</returns>
		/// <param name="getResourceModules">true to include resource modules; otherwise, false. </param>
		/// <exception cref="T:System.IO.FileLoadException">A file that was found could not be loaded. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">A file was not found. </exception>
		/// <exception cref="T:System.BadImageFormatException">A file was not a valid assembly. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060021A0 RID: 8608 RVA: 0x0007F9F8 File Offset: 0x0007DBF8
		public virtual FileStream[] GetFiles(bool getResourceModules)
		{
			string[] array = (string[])this.GetFilesInternal(null, getResourceModules);
			if (array == null)
			{
				return EmptyArray<FileStream>.Value;
			}
			string location = this.Location;
			FileStream[] array2;
			if (location != string.Empty)
			{
				array2 = new FileStream[array.Length + 1];
				array2[0] = new FileStream(location, FileMode.Open, FileAccess.Read);
				for (int i = 0; i < array.Length; i++)
				{
					array2[i + 1] = new FileStream(array[i], FileMode.Open, FileAccess.Read);
				}
			}
			else
			{
				array2 = new FileStream[array.Length];
				for (int j = 0; j < array.Length; j++)
				{
					array2[j] = new FileStream(array[j], FileMode.Open, FileAccess.Read);
				}
			}
			return array2;
		}

		/// <summary>Gets a <see cref="T:System.IO.FileStream" /> for the specified file in the file table of the manifest of this assembly.</summary>
		/// <returns>A stream that contains the specified file, or null if the file is not found.</returns>
		/// <param name="name">The name of the specified file. Do not include the path to the file.</param>
		/// <exception cref="T:System.IO.FileLoadException">A file that was found could not be loaded. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="name" /> parameter is an empty string (""). </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="name" /> was not found. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="name" /> is not a valid assembly. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060021A1 RID: 8609 RVA: 0x0007FA90 File Offset: 0x0007DC90
		public virtual FileStream GetFile(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException(null, "Name cannot be null.");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException("Empty name is not valid");
			}
			string text = (string)this.GetFilesInternal(name, true);
			if (text != null)
			{
				return new FileStream(text, FileMode.Open, FileAccess.Read);
			}
			return null;
		}

		// Token: 0x060021A2 RID: 8610
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern IntPtr GetManifestResourceInternal(string name, out int size, out Module module);

		/// <summary>Loads the specified manifest resource from this assembly.</summary>
		/// <returns>The manifest resource; or null if no resources were specified during compilation or if the resource is not visible to the caller.</returns>
		/// <param name="name">The case-sensitive name of the manifest resource being requested. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="name" /> parameter is an empty string (""). </exception>
		/// <exception cref="T:System.IO.FileLoadException">NoteIn the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.IO.IOException" />, instead.A file that was found could not be loaded. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="name" /> was not found. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="name" /> is not a valid assembly. </exception>
		/// <exception cref="T:System.NotImplementedException">Resource length is greater than <see cref="F:System.Int64.MaxValue" />.</exception>
		// Token: 0x060021A3 RID: 8611 RVA: 0x0007FADC File Offset: 0x0007DCDC
		public unsafe virtual Stream GetManifestResourceStream(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException("String cannot have zero length.", "name");
			}
			ManifestResourceInfo manifestResourceInfo = this.GetManifestResourceInfo(name);
			if (manifestResourceInfo == null)
			{
				Assembly assembly = AppDomain.CurrentDomain.DoResourceResolve(name, this);
				if (assembly != null && assembly != this)
				{
					return assembly.GetManifestResourceStream(name);
				}
				return null;
			}
			else
			{
				if (manifestResourceInfo.ReferencedAssembly != null)
				{
					return manifestResourceInfo.ReferencedAssembly.GetManifestResourceStream(name);
				}
				if (manifestResourceInfo.FileName != null && manifestResourceInfo.ResourceLocation == (ResourceLocation)0)
				{
					if (this.fromByteArray)
					{
						throw new FileNotFoundException(manifestResourceInfo.FileName);
					}
					return new FileStream(Path.Combine(Path.GetDirectoryName(this.Location), manifestResourceInfo.FileName), FileMode.Open, FileAccess.Read);
				}
				else
				{
					int num;
					Module module;
					IntPtr manifestResourceInternal = this.GetManifestResourceInternal(name, out num, out module);
					if (manifestResourceInternal == (IntPtr)0)
					{
						return null;
					}
					return new Assembly.UnmanagedMemoryStreamForModule((byte*)(void*)manifestResourceInternal, (long)num, module);
				}
			}
		}

		/// <summary>Loads the specified manifest resource, scoped by the namespace of the specified type, from this assembly.</summary>
		/// <returns>The manifest resource; or null if no resources were specified during compilation or if the resource is not visible to the caller.</returns>
		/// <param name="type">The type whose namespace is used to scope the manifest resource name. </param>
		/// <param name="name">The case-sensitive name of the manifest resource being requested. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="name" /> parameter is an empty string (""). </exception>
		/// <exception cref="T:System.IO.FileLoadException">A file that was found could not be loaded. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="name" /> was not found. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="name" /> is not a valid assembly. </exception>
		/// <exception cref="T:System.NotImplementedException">Resource length is greater than <see cref="F:System.Int64.MaxValue" />.</exception>
		// Token: 0x060021A4 RID: 8612 RVA: 0x0007FBD0 File Offset: 0x0007DDD0
		public virtual Stream GetManifestResourceStream(Type type, string name)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return this.GetManifestResourceStream(type, name, false, ref stackCrawlMark);
		}

		// Token: 0x060021A5 RID: 8613 RVA: 0x0007FBEC File Offset: 0x0007DDEC
		internal Stream GetManifestResourceStream(Type type, string name, bool skipSecurityCheck, ref StackCrawlMark stackMark)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (type == null)
			{
				if (name == null)
				{
					throw new ArgumentNullException("type");
				}
			}
			else
			{
				string @namespace = type.Namespace;
				if (@namespace != null)
				{
					stringBuilder.Append(@namespace);
					if (name != null)
					{
						stringBuilder.Append(Type.Delimiter);
					}
				}
			}
			if (name != null)
			{
				stringBuilder.Append(name);
			}
			return this.GetManifestResourceStream(stringBuilder.ToString());
		}

		// Token: 0x060021A6 RID: 8614 RVA: 0x0007FC4E File Offset: 0x0007DE4E
		internal Stream GetManifestResourceStream(string name, ref StackCrawlMark stackMark, bool skipSecurityCheck)
		{
			return this.GetManifestResourceStream(null, name, skipSecurityCheck, ref stackMark);
		}

		// Token: 0x060021A7 RID: 8615 RVA: 0x0007FC5A File Offset: 0x0007DE5A
		internal string GetSimpleName()
		{
			return this.GetName(true).Name;
		}

		// Token: 0x060021A8 RID: 8616 RVA: 0x0007FC68 File Offset: 0x0007DE68
		internal byte[] GetPublicKey()
		{
			return this.GetName(true).GetPublicKey();
		}

		// Token: 0x060021A9 RID: 8617 RVA: 0x0007FC76 File Offset: 0x0007DE76
		internal Version GetVersion()
		{
			return this.GetName(true).Version;
		}

		// Token: 0x060021AA RID: 8618 RVA: 0x0007FC84 File Offset: 0x0007DE84
		private AssemblyNameFlags GetFlags()
		{
			return this.GetName(true).Flags;
		}

		// Token: 0x060021AB RID: 8619
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal virtual extern Type[] GetTypes(bool exportedOnly);

		/// <summary>Gets the types defined in this assembly.</summary>
		/// <returns>An array that contains all the types that are defined in this assembly.</returns>
		/// <exception cref="T:System.Reflection.ReflectionTypeLoadException">The assembly contains one or more types that cannot be loaded. The array returned by the <see cref="P:System.Reflection.ReflectionTypeLoadException.Types" /> property of this exception contains a <see cref="T:System.Type" /> object for each type that was loaded and null for each type that could not be loaded, while the <see cref="P:System.Reflection.ReflectionTypeLoadException.LoaderExceptions" /> property contains an exception for each type that could not be loaded.</exception>
		// Token: 0x060021AC RID: 8620 RVA: 0x0007FC92 File Offset: 0x0007DE92
		public virtual Type[] GetTypes()
		{
			return this.GetTypes(false);
		}

		/// <summary>Gets the public types defined in this assembly that are visible outside the assembly.</summary>
		/// <returns>An array that represents the types defined in this assembly that are visible outside the assembly.</returns>
		/// <exception cref="T:System.NotSupportedException">The assembly is a dynamic assembly.</exception>
		// Token: 0x060021AD RID: 8621 RVA: 0x0007FC9B File Offset: 0x0007DE9B
		public virtual Type[] GetExportedTypes()
		{
			return this.GetTypes(true);
		}

		/// <summary>Gets the <see cref="T:System.Type" /> object with the specified name in the assembly instance and optionally throws an exception if the type is not found.</summary>
		/// <returns>An object that represents the specified class.</returns>
		/// <param name="name">The full name of the type. </param>
		/// <param name="throwOnError">true to throw an exception if the type is not found; false to return null. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is invalid.-or- The length of <paramref name="name" /> exceeds 1024 characters. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.TypeLoadException">
		///   <paramref name="throwOnError" /> is true, and the type cannot be found.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="name" /> requires a dependent assembly that could not be found. </exception>
		/// <exception cref="T:System.IO.FileLoadException">
		///   <paramref name="name" /> requires a dependent assembly that was found but could not be loaded.-or-The current assembly was loaded into the reflection-only context, and <paramref name="name" /> requires a dependent assembly that was not preloaded. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="name" /> requires a dependent assembly, but the file is not a valid assembly. -or-<paramref name="name" /> requires a dependent assembly which was compiled for a version of the runtime later than the currently loaded version.</exception>
		// Token: 0x060021AE RID: 8622 RVA: 0x0007FCA4 File Offset: 0x0007DEA4
		public virtual Type GetType(string name, bool throwOnError)
		{
			return this.GetType(name, throwOnError, false);
		}

		/// <summary>Gets the <see cref="T:System.Type" /> object with the specified name in the assembly instance.</summary>
		/// <returns>An object that represents the specified class, or null if the class is not found.</returns>
		/// <param name="name">The full name of the type. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is invalid. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="name" /> requires a dependent assembly that could not be found. </exception>
		/// <exception cref="T:System.IO.FileLoadException">NoteIn the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.IO.IOException" />, instead.<paramref name="name" /> requires a dependent assembly that was found but could not be loaded.-or-The current assembly was loaded into the reflection-only context, and <paramref name="name" /> requires a dependent assembly that was not preloaded. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="name" /> requires a dependent assembly, but the file is not a valid assembly. -or-<paramref name="name" /> requires a dependent assembly which was compiled for a version of the runtime later than the currently loaded version. </exception>
		// Token: 0x060021AF RID: 8623 RVA: 0x0007FCAF File Offset: 0x0007DEAF
		public virtual Type GetType(string name)
		{
			return this.GetType(name, false, false);
		}

		// Token: 0x060021B0 RID: 8624
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern Type InternalGetType(Module module, string name, bool throwOnError, bool ignoreCase);

		// Token: 0x060021B1 RID: 8625
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void InternalGetAssemblyName(string assemblyFile, out MonoAssemblyName aname, out string codebase);

		/// <summary>Gets an <see cref="T:System.Reflection.AssemblyName" /> for this assembly, setting the codebase as specified by <paramref name="copiedName" />.</summary>
		/// <returns>An object that contains the fully parsed display name for this assembly.</returns>
		/// <param name="copiedName">true to set the <see cref="P:System.Reflection.Assembly.CodeBase" /> to the location of the assembly after it was shadow copied; false to set <see cref="P:System.Reflection.Assembly.CodeBase" /> to the original location. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060021B2 RID: 8626 RVA: 0x0002126B File Offset: 0x0001F46B
		public virtual AssemblyName GetName(bool copiedName)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets an <see cref="T:System.Reflection.AssemblyName" /> for this assembly.</summary>
		/// <returns>An object that contains the fully parsed display name for this assembly.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060021B3 RID: 8627 RVA: 0x0007FCBA File Offset: 0x0007DEBA
		public virtual AssemblyName GetName()
		{
			return this.GetName(false);
		}

		/// <summary>Returns the full name of the assembly, also known as the display name.</summary>
		/// <returns>The full name of the assembly, or the class name if the full name of the assembly cannot be determined.</returns>
		// Token: 0x060021B4 RID: 8628 RVA: 0x0007FCC3 File Offset: 0x0007DEC3
		public override string ToString()
		{
			if (this.assemblyName != null)
			{
				return this.assemblyName;
			}
			this.assemblyName = this.get_fullname();
			return this.assemblyName;
		}

		/// <summary>Creates the name of a type qualified by the display name of its assembly.</summary>
		/// <returns>The full name of the type qualified by the display name of the assembly.</returns>
		/// <param name="assemblyName">The display name of an assembly. </param>
		/// <param name="typeName">The full name of a type. </param>
		// Token: 0x060021B5 RID: 8629 RVA: 0x0007FCE6 File Offset: 0x0007DEE6
		public static string CreateQualifiedName(string assemblyName, string typeName)
		{
			return typeName + ", " + assemblyName;
		}

		/// <summary>Gets the currently loaded assembly in which the specified class is defined.</summary>
		/// <returns>The assembly in which the specified class is defined.</returns>
		/// <param name="type">An object representing a class in the assembly that will be returned. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is null. </exception>
		// Token: 0x060021B6 RID: 8630 RVA: 0x0007FCF4 File Offset: 0x0007DEF4
		public static Assembly GetAssembly(Type type)
		{
			if (type != null)
			{
				return type.Assembly;
			}
			throw new ArgumentNullException("type");
		}

		/// <summary>Gets the process executable in the default application domain. In other application domains, this is the first executable that was executed by <see cref="M:System.AppDomain.ExecuteAssembly(System.String)" />.</summary>
		/// <returns>The assembly that is the process executable in the default application domain, or the first executable that was executed by <see cref="M:System.AppDomain.ExecuteAssembly(System.String)" />. Can return null when called from unmanaged code.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060021B7 RID: 8631
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Assembly GetEntryAssembly();

		// Token: 0x060021B8 RID: 8632 RVA: 0x0007FD10 File Offset: 0x0007DF10
		internal Assembly GetSatelliteAssembly(CultureInfo culture, Version version, bool throwOnError)
		{
			if (culture == null)
			{
				throw new ArgumentNullException("culture");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			string text = this.GetSimpleName() + ".resources";
			return this.InternalGetSatelliteAssembly(text, culture, version, true, ref stackCrawlMark);
		}

		// Token: 0x060021B9 RID: 8633 RVA: 0x0007FD4C File Offset: 0x0007DF4C
		internal RuntimeAssembly InternalGetSatelliteAssembly(string name, CultureInfo culture, Version version, bool throwOnFileNotFound, ref StackCrawlMark stackMark)
		{
			AssemblyName assemblyName = new AssemblyName();
			assemblyName.SetPublicKey(this.GetPublicKey());
			assemblyName.Flags = this.GetFlags() | AssemblyNameFlags.PublicKey;
			if (version == null)
			{
				assemblyName.Version = this.GetVersion();
			}
			else
			{
				assemblyName.Version = version;
			}
			assemblyName.CultureInfo = culture;
			assemblyName.Name = name;
			try
			{
				Assembly assembly = AppDomain.CurrentDomain.LoadSatellite(assemblyName, false);
				if (assembly != null)
				{
					return (RuntimeAssembly)assembly;
				}
			}
			catch (FileNotFoundException)
			{
			}
			if (string.IsNullOrEmpty(this.Location))
			{
				return null;
			}
			string text = Path.Combine(Path.GetDirectoryName(this.Location), Path.Combine(culture.Name, assemblyName.Name + ".dll"));
			RuntimeAssembly runtimeAssembly;
			try
			{
				runtimeAssembly = (RuntimeAssembly)Assembly.LoadFrom(text);
			}
			catch
			{
				if (throwOnFileNotFound || File.Exists(text))
				{
					throw;
				}
				runtimeAssembly = null;
			}
			return runtimeAssembly;
		}

		/// <summary>Returns the type of the current instance.</summary>
		/// <returns>An object that represents the <see cref="T:System.Reflection.Assembly" /> type.</returns>
		// Token: 0x060021BA RID: 8634 RVA: 0x00033A19 File Offset: 0x00031C19
		Type _Assembly.GetType()
		{
			return base.GetType();
		}

		// Token: 0x060021BB RID: 8635
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Assembly LoadFrom(string assemblyFile, bool refonly);

		/// <summary>Loads an assembly given its file name or path.</summary>
		/// <returns>The loaded assembly.</returns>
		/// <param name="assemblyFile">The name or path of the file that contains the manifest of the assembly. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyFile" /> is null. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyFile" /> is not found, or the module you are trying to load does not specify a filename extension. </exception>
		/// <exception cref="T:System.IO.FileLoadException">A file that was found could not be loaded. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyFile" /> is not a valid assembly; for example, a 32-bit assembly in a 64-bit process. See the exception topic for more information. -or-Version 2.0 or later of the common language runtime is currently loaded and <paramref name="assemblyFile" /> was compiled with a later version.</exception>
		/// <exception cref="T:System.Security.SecurityException">A codebase that does not start with "file://" was specified without the required <see cref="T:System.Net.WebPermission" />. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="assemblyFile" /> parameter is an empty string (""). </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The assembly name is longer than MAX_PATH characters.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060021BC RID: 8636 RVA: 0x0007FE44 File Offset: 0x0007E044
		public static Assembly LoadFrom(string assemblyFile)
		{
			return Assembly.LoadFrom(assemblyFile, false);
		}

		/// <summary>Loads an assembly given its file name or path and supplying security evidence.</summary>
		/// <returns>The loaded assembly.</returns>
		/// <param name="assemblyFile">The name or path of the file that contains the manifest of the assembly. </param>
		/// <param name="securityEvidence">Evidence for loading the assembly. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyFile" /> is null. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyFile" /> is not found, or the module you are trying to load does not specify a filename extension. </exception>
		/// <exception cref="T:System.IO.FileLoadException">A file that was found could not be loaded.-or-The <paramref name="securityEvidence" /> is not ambiguous and is determined to be invalid.</exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyFile" /> is not a valid assembly; for example, a 32-bit assembly in a 64-bit process. See the exception topic for more information. -or-Version 2.0 or later of the common language runtime is currently loaded and <paramref name="assemblyFile" /> was compiled with a later version.</exception>
		/// <exception cref="T:System.Security.SecurityException">A codebase that does not start with "file://" was specified without the required <see cref="T:System.Net.WebPermission" />. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="assemblyFile" /> parameter is an empty string (""). </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The assembly name is longer than MAX_PATH characters.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060021BD RID: 8637 RVA: 0x0007FE50 File Offset: 0x0007E050
		[Obsolete]
		public static Assembly LoadFrom(string assemblyFile, Evidence securityEvidence)
		{
			Assembly assembly = Assembly.LoadFrom(assemblyFile, false);
			if (assembly != null && securityEvidence != null)
			{
				assembly.Evidence.Merge(securityEvidence);
			}
			return assembly;
		}

		/// <summary>Loads an assembly given its file name or path, security evidence, hash value, and hash algorithm.</summary>
		/// <returns>The loaded assembly.</returns>
		/// <param name="assemblyFile">The name or path of the file that contains the manifest of the assembly. </param>
		/// <param name="securityEvidence">Evidence for loading the assembly. </param>
		/// <param name="hashValue">The value of the computed hash code. </param>
		/// <param name="hashAlgorithm">The hash algorithm used for hashing files and for generating the strong name. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyFile" /> is null. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyFile" /> is not found, or the module you are trying to load does not specify a filename extension. </exception>
		/// <exception cref="T:System.IO.FileLoadException">A file that was found could not be loaded.-or-The <paramref name="securityEvidence" /> is not ambiguous and is determined to be invalid. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyFile" /> is not a valid assembly; for example, a 32-bit assembly in a 64-bit process. See the exception topic for more information. -or-Version 2.0 or later of the common language runtime is currently loaded and <paramref name="assemblyFile" /> was compiled with a later version.</exception>
		/// <exception cref="T:System.Security.SecurityException">A codebase that does not start with "file://" was specified without the required <see cref="T:System.Net.WebPermission" />. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="assemblyFile" /> parameter is an empty string (""). </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The assembly name is longer than MAX_PATH characters.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060021BE RID: 8638 RVA: 0x0002126B File Offset: 0x0001F46B
		[MonoTODO("This overload is not currently implemented")]
		[Obsolete]
		public static Assembly LoadFrom(string assemblyFile, Evidence securityEvidence, byte[] hashValue, AssemblyHashAlgorithm hashAlgorithm)
		{
			throw new NotImplementedException();
		}

		/// <summary>Loads an assembly given its file name or path, hash value, and hash algorithm.</summary>
		/// <returns>The loaded assembly.</returns>
		/// <param name="assemblyFile">The name or path of the file that contains the manifest of the assembly. </param>
		/// <param name="hashValue">The value of the computed hash code. </param>
		/// <param name="hashAlgorithm">The hash algorithm used for hashing files and for generating the strong name. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyFile" /> is null. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyFile" /> is not found, or the module you are trying to load does not specify a file name extension. </exception>
		/// <exception cref="T:System.IO.FileLoadException">A file that was found could not be loaded.</exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyFile" /> is not a valid assembly; for example, a 32-bit assembly in a 64-bit process. See the exception topic for more information. -or-<paramref name="assemblyFile" /> was compiled with a later version of the common language runtime than the version that is currently loaded.</exception>
		/// <exception cref="T:System.Security.SecurityException">A codebase that does not start with "file://" was specified without the required <see cref="T:System.Net.WebPermission" />. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="assemblyFile" /> parameter is an empty string (""). </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The assembly name is longer than MAX_PATH characters.</exception>
		// Token: 0x060021BF RID: 8639 RVA: 0x0002126B File Offset: 0x0001F46B
		[MonoTODO]
		public static Assembly LoadFrom(string assemblyFile, byte[] hashValue, AssemblyHashAlgorithm hashAlgorithm)
		{
			throw new NotImplementedException();
		}

		/// <summary>Loads an assembly into the load-from context, bypassing some security checks.</summary>
		/// <returns>The loaded assembly.</returns>
		/// <param name="assemblyFile">The name or path of the file that contains the manifest of the assembly.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyFile" /> is null. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyFile" /> is not found, or the module you are trying to load does not specify a filename extension. </exception>
		/// <exception cref="T:System.IO.FileLoadException">A file that was found could not be loaded. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyFile" /> is not a valid assembly. -or-<paramref name="assemblyFile" /> was compiled with a later version of the common language runtime than the version that is currently loaded.</exception>
		/// <exception cref="T:System.Security.SecurityException">A codebase that does not start with "file://" was specified without the required <see cref="T:System.Net.WebPermission" />. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="assemblyFile" /> parameter is an empty string (""). </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The assembly name is longer than MAX_PATH characters.</exception>
		// Token: 0x060021C0 RID: 8640 RVA: 0x0007FE7E File Offset: 0x0007E07E
		public static Assembly UnsafeLoadFrom(string assemblyFile)
		{
			return Assembly.LoadFrom(assemblyFile);
		}

		/// <summary>Loads an assembly given its path, loading the assembly into the domain of the caller using the supplied evidence.</summary>
		/// <returns>The loaded assembly.</returns>
		/// <param name="path">The path of the assembly file. </param>
		/// <param name="securityEvidence">Evidence for loading the assembly. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="path" /> parameter is null. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The <paramref name="path" /> parameter is an empty string ("") or does not exist. </exception>
		/// <exception cref="T:System.IO.FileLoadException">A file that was found could not be loaded. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="path" /> is not a valid assembly. -or-Version 2.0 or later of the common language runtime is currently loaded and <paramref name="path" /> was compiled with a later version.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="securityEvidence" /> is not null. By default, legacy CAS policy is not enabled in the .NET Framework 4; when it is not enabled, <paramref name="securityEvidence" /> must be null. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060021C1 RID: 8641 RVA: 0x0007FE86 File Offset: 0x0007E086
		[Obsolete]
		public static Assembly LoadFile(string path, Evidence securityEvidence)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (path == string.Empty)
			{
				throw new ArgumentException("Path can't be empty", "path");
			}
			return Assembly.LoadFrom(path, securityEvidence);
		}

		/// <summary>Loads the contents of an assembly file on the specified path.</summary>
		/// <returns>The loaded assembly.</returns>
		/// <param name="path">The path of the file to load. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="path" /> parameter is null. </exception>
		/// <exception cref="T:System.IO.FileLoadException">A file that was found could not be loaded. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The <paramref name="path" /> parameter is an empty string ("") or does not exist. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="path" /> is not a valid assembly. -or-Version 2.0 or later of the common language runtime is currently loaded and <paramref name="path" /> was compiled with a later version.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060021C2 RID: 8642 RVA: 0x0007FEBA File Offset: 0x0007E0BA
		public static Assembly LoadFile(string path)
		{
			return Assembly.LoadFile(path, null);
		}

		/// <summary>Loads an assembly given the long form of its name.</summary>
		/// <returns>The loaded assembly.</returns>
		/// <param name="assemblyString">The long form of the assembly name. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyString" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="assemblyString" /> is a zero-length string. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyString" /> is not found. </exception>
		/// <exception cref="T:System.IO.FileLoadException">A file that was found could not be loaded. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyString" /> is not a valid assembly. -or-Version 2.0 or later of the common language runtime is currently loaded and <paramref name="assemblyString" /> was compiled with a later version.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060021C3 RID: 8643 RVA: 0x0007FEC3 File Offset: 0x0007E0C3
		public static Assembly Load(string assemblyString)
		{
			return AppDomain.CurrentDomain.Load(assemblyString);
		}

		/// <summary>Loads an assembly given its display name, loading the assembly into the domain of the caller using the supplied evidence.</summary>
		/// <returns>The loaded assembly.</returns>
		/// <param name="assemblyString">The display name of the assembly. </param>
		/// <param name="assemblySecurity">Evidence for loading the assembly. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyString" /> is null. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyString" /> is not found. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyString" /> is not a valid assembly. -or-Version 2.0 or later of the common language runtime is currently loaded and <paramref name="assemblyString" /> was compiled with a later version.</exception>
		/// <exception cref="T:System.IO.FileLoadException">A file that was found could not be loaded.-or-An assembly or module was loaded twice with two different evidences. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060021C4 RID: 8644 RVA: 0x0007FED0 File Offset: 0x0007E0D0
		[Obsolete]
		public static Assembly Load(string assemblyString, Evidence assemblySecurity)
		{
			return AppDomain.CurrentDomain.Load(assemblyString, assemblySecurity);
		}

		/// <summary>Loads an assembly given its <see cref="T:System.Reflection.AssemblyName" />.</summary>
		/// <returns>The loaded assembly.</returns>
		/// <param name="assemblyRef">The object that describes the assembly to be loaded. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyRef" /> is null. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyRef" /> is not found. </exception>
		/// <exception cref="T:System.IO.FileLoadException">NoteIn the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.IO.IOException" />, instead.A file that was found could not be loaded. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyRef" /> is not a valid assembly. -or-Version 2.0 or later of the common language runtime is currently loaded and <paramref name="assemblyRef" /> was compiled with a later version.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060021C5 RID: 8645 RVA: 0x0007FEDE File Offset: 0x0007E0DE
		public static Assembly Load(AssemblyName assemblyRef)
		{
			return AppDomain.CurrentDomain.Load(assemblyRef);
		}

		/// <summary>Loads an assembly given its <see cref="T:System.Reflection.AssemblyName" />. The assembly is loaded into the domain of the caller using the supplied evidence.</summary>
		/// <returns>The loaded assembly.</returns>
		/// <param name="assemblyRef">The object that describes the assembly to be loaded. </param>
		/// <param name="assemblySecurity">Evidence for loading the assembly. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyRef" /> is null. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyRef" /> is not found. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyRef" /> is not a valid assembly. -or-Version 2.0 or later of the common language runtime is currently loaded and <paramref name="assemblyRef" /> was compiled with a later version.</exception>
		/// <exception cref="T:System.IO.FileLoadException">An assembly or module was loaded twice with two different evidences. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060021C6 RID: 8646 RVA: 0x0007FEEB File Offset: 0x0007E0EB
		[Obsolete]
		public static Assembly Load(AssemblyName assemblyRef, Evidence assemblySecurity)
		{
			return AppDomain.CurrentDomain.Load(assemblyRef, assemblySecurity);
		}

		/// <summary>Loads the assembly with a common object file format (COFF)-based image containing an emitted assembly. The assembly is loaded into the application domain of the caller.</summary>
		/// <returns>The loaded assembly.</returns>
		/// <param name="rawAssembly">A byte array that is a COFF-based image containing an emitted assembly. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rawAssembly" /> is null. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="rawAssembly" /> is not a valid assembly. -or-Version 2.0 or later of the common language runtime is currently loaded and <paramref name="rawAssembly" /> was compiled with a later version.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060021C7 RID: 8647 RVA: 0x0007FEF9 File Offset: 0x0007E0F9
		public static Assembly Load(byte[] rawAssembly)
		{
			return AppDomain.CurrentDomain.Load(rawAssembly);
		}

		/// <summary>Loads the assembly with a common object file format (COFF)-based image containing an emitted assembly, optionally including symbols for the assembly. The assembly is loaded into the application domain of the caller.</summary>
		/// <returns>The loaded assembly.</returns>
		/// <param name="rawAssembly">A byte array that is a COFF-based image containing an emitted assembly. </param>
		/// <param name="rawSymbolStore">A byte array that contains the raw bytes representing the symbols for the assembly. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rawAssembly" /> is null. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="rawAssembly" /> is not a valid assembly. -or-Version 2.0 or later of the common language runtime is currently loaded and <paramref name="rawAssembly" /> was compiled with a later version.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060021C8 RID: 8648 RVA: 0x0007FF06 File Offset: 0x0007E106
		public static Assembly Load(byte[] rawAssembly, byte[] rawSymbolStore)
		{
			return AppDomain.CurrentDomain.Load(rawAssembly, rawSymbolStore);
		}

		/// <summary>Loads the assembly with a common object file format (COFF)-based image containing an emitted assembly, optionally including symbols and evidence for the assembly. The assembly is loaded into the application domain of the caller.</summary>
		/// <returns>The loaded assembly.</returns>
		/// <param name="rawAssembly">A byte array that is a COFF-based image containing an emitted assembly. </param>
		/// <param name="rawSymbolStore">A byte array that contains the raw bytes representing the symbols for the assembly. </param>
		/// <param name="securityEvidence">Evidence for loading the assembly. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rawAssembly" /> is null. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="rawAssembly" /> is not a valid assembly. -or-Version 2.0 or later of the common language runtime is currently loaded and <paramref name="rawAssembly" /> was compiled with a later version.</exception>
		/// <exception cref="T:System.IO.FileLoadException">An assembly or module was loaded twice with two different evidences. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="securityEvidence" /> is not null.  By default, legacy CAS policy is not enabled in the .NET Framework 4; when it is not enabled, <paramref name="securityEvidence" /> must be null. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060021C9 RID: 8649 RVA: 0x0007FF14 File Offset: 0x0007E114
		[Obsolete]
		public static Assembly Load(byte[] rawAssembly, byte[] rawSymbolStore, Evidence securityEvidence)
		{
			return AppDomain.CurrentDomain.Load(rawAssembly, rawSymbolStore, securityEvidence);
		}

		/// <summary>Loads the assembly with a common object file format (COFF)-based image containing an emitted assembly, optionally including symbols and specifying the source for the security context. The assembly is loaded into the application domain of the caller.</summary>
		/// <returns>The loaded assembly.</returns>
		/// <param name="rawAssembly">A byte array that is a COFF-based image containing an emitted assembly. </param>
		/// <param name="rawSymbolStore">A byte array that contains the raw bytes representing the symbols for the assembly. </param>
		/// <param name="securityContextSource">The source of the security context. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rawAssembly" /> is null. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="rawAssembly" /> is not a valid assembly. -or-<paramref name="rawAssembly" /> was compiled with a later version of the common language runtime than the version that is currently loaded.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of <paramref name="securityContextSource" /> is not one of the enumeration values.</exception>
		// Token: 0x060021CA RID: 8650 RVA: 0x0007FF06 File Offset: 0x0007E106
		[MonoLimitation("Argument securityContextSource is ignored")]
		public static Assembly Load(byte[] rawAssembly, byte[] rawSymbolStore, SecurityContextSource securityContextSource)
		{
			return AppDomain.CurrentDomain.Load(rawAssembly, rawSymbolStore);
		}

		/// <summary>Loads the assembly from a common object file format (COFF)-based image containing an emitted assembly. The assembly is loaded into the reflection-only context of the caller's application domain.</summary>
		/// <returns>The loaded assembly.</returns>
		/// <param name="rawAssembly">A byte array that is a COFF-based image containing an emitted assembly.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rawAssembly" /> is null. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="rawAssembly" /> is not a valid assembly. -or-Version 2.0 or later of the common language runtime is currently loaded and <paramref name="rawAssembly" /> was compiled with a later version.</exception>
		/// <exception cref="T:System.IO.FileLoadException">
		///   <paramref name="rawAssembly" /> cannot be loaded. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060021CB RID: 8651 RVA: 0x0007FF23 File Offset: 0x0007E123
		public static Assembly ReflectionOnlyLoad(byte[] rawAssembly)
		{
			return AppDomain.CurrentDomain.Load(rawAssembly, null, null, true);
		}

		/// <summary>Loads an assembly into the reflection-only context, given its display name.</summary>
		/// <returns>The loaded assembly.</returns>
		/// <param name="assemblyString">The display name of the assembly, as returned by the <see cref="P:System.Reflection.AssemblyName.FullName" /> property.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyString" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="assemblyString" /> is an empty string (""). </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyString" /> is not found. </exception>
		/// <exception cref="T:System.IO.FileLoadException">
		///   <paramref name="assemblyString" /> is found, but cannot be loaded. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyString" /> is not a valid assembly. -or-Version 2.0 or later of the common language runtime is currently loaded and <paramref name="assemblyString" /> was compiled with a later version.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060021CC RID: 8652 RVA: 0x0007FF33 File Offset: 0x0007E133
		public static Assembly ReflectionOnlyLoad(string assemblyString)
		{
			return AppDomain.CurrentDomain.Load(assemblyString, null, true);
		}

		/// <summary>Loads an assembly into the reflection-only context, given its path.</summary>
		/// <returns>The loaded assembly.</returns>
		/// <param name="assemblyFile">The path of the file that contains the manifest of the assembly.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyFile" /> is null. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyFile" /> is not found, or the module you are trying to load does not specify a file name extension. </exception>
		/// <exception cref="T:System.IO.FileLoadException">
		///   <paramref name="assemblyFile" /> is found, but could not be loaded. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyFile" /> is not a valid assembly. -or-Version 2.0 or later of the common language runtime is currently loaded and <paramref name="assemblyFile" /> was compiled with a later version.</exception>
		/// <exception cref="T:System.Security.SecurityException">A codebase that does not start with "file://" was specified without the required <see cref="T:System.Net.WebPermission" />. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The assembly name is longer than MAX_PATH characters. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="assemblyFile" /> is an empty string (""). </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060021CD RID: 8653 RVA: 0x0007FF42 File Offset: 0x0007E142
		public static Assembly ReflectionOnlyLoadFrom(string assemblyFile)
		{
			if (assemblyFile == null)
			{
				throw new ArgumentNullException("assemblyFile");
			}
			return Assembly.LoadFrom(assemblyFile, true);
		}

		/// <summary>Loads an assembly from the application directory or from the global assembly cache using a partial name.</summary>
		/// <returns>The loaded assembly. If <paramref name="partialName" /> is not found, this method returns null.</returns>
		/// <param name="partialName">The display name of the assembly. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="partialName" /> parameter is null. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyFile" /> is not a valid assembly. -or-Version 2.0 or later of the common language runtime is currently loaded and <paramref name="partialName" /> was compiled with a later version.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060021CE RID: 8654 RVA: 0x0007FF59 File Offset: 0x0007E159
		[Obsolete("This method has been deprecated. Please use Assembly.Load() instead. http://go.microsoft.com/fwlink/?linkid=14202")]
		public static Assembly LoadWithPartialName(string partialName)
		{
			return Assembly.LoadWithPartialName(partialName, null);
		}

		/// <summary>Loads the module, internal to this assembly, with a common object file format (COFF)-based image containing an emitted module, or a resource file.</summary>
		/// <returns>The loaded module.</returns>
		/// <param name="moduleName">The name of the module. This string must correspond to a file name in this assembly's manifest. </param>
		/// <param name="rawModule">A byte array that is a COFF-based image containing an emitted module, or a resource. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="moduleName" /> or <paramref name="rawModule" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="moduleName" /> does not match a file entry in this assembly's manifest. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="rawModule" /> is not a valid module. </exception>
		/// <exception cref="T:System.IO.FileLoadException">A file that was found could not be loaded. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060021CF RID: 8655 RVA: 0x0002126B File Offset: 0x0001F46B
		[MonoTODO("Not implemented")]
		public Module LoadModule(string moduleName, byte[] rawModule)
		{
			throw new NotImplementedException();
		}

		/// <summary>Loads the module, internal to this assembly, with a common object file format (COFF)-based image containing an emitted module, or a resource file. The raw bytes representing the symbols for the module are also loaded.</summary>
		/// <returns>The loaded module.</returns>
		/// <param name="moduleName">The name of the module. This string must correspond to a file name in this assembly's manifest. </param>
		/// <param name="rawModule">A byte array that is a COFF-based image containing an emitted module, or a resource. </param>
		/// <param name="rawSymbolStore">A byte array containing the raw bytes representing the symbols for the module. Must be null if this is a resource file. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="moduleName" /> or <paramref name="rawModule" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="moduleName" /> does not match a file entry in this assembly's manifest. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="rawModule" /> is not a valid module. </exception>
		/// <exception cref="T:System.IO.FileLoadException">A file that was found could not be loaded. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060021D0 RID: 8656 RVA: 0x0002126B File Offset: 0x0001F46B
		[MonoTODO("Not implemented")]
		public virtual Module LoadModule(string moduleName, byte[] rawModule, byte[] rawSymbolStore)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060021D1 RID: 8657
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Assembly load_with_partial_name(string name, Evidence e);

		/// <summary>Loads an assembly from the application directory or from the global assembly cache using a partial name. The assembly is loaded into the domain of the caller using the supplied evidence.</summary>
		/// <returns>The loaded assembly. If <paramref name="partialName" /> is not found, this method returns null.</returns>
		/// <param name="partialName">The display name of the assembly. </param>
		/// <param name="securityEvidence">Evidence for loading the assembly. </param>
		/// <exception cref="T:System.IO.FileLoadException">An assembly or module was loaded twice with two different sets of evidence. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="partialName" /> parameter is null. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyFile" /> is not a valid assembly. -or-Version 2.0 or later of the common language runtime is currently loaded and <paramref name="partialName" /> was compiled with a later version.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060021D2 RID: 8658 RVA: 0x0007FF62 File Offset: 0x0007E162
		[Obsolete("This method has been deprecated. Please use Assembly.Load() instead. http://go.microsoft.com/fwlink/?linkid=14202")]
		public static Assembly LoadWithPartialName(string partialName, Evidence securityEvidence)
		{
			return Assembly.LoadWithPartialName(partialName, securityEvidence, true);
		}

		// Token: 0x060021D3 RID: 8659 RVA: 0x0007FF6C File Offset: 0x0007E16C
		internal static Assembly LoadWithPartialName(string partialName, Evidence securityEvidence, bool oldBehavior)
		{
			if (!oldBehavior)
			{
				throw new NotImplementedException();
			}
			if (partialName == null)
			{
				throw new NullReferenceException();
			}
			return Assembly.load_with_partial_name(partialName, securityEvidence);
		}

		/// <summary>Locates the specified type from this assembly and creates an instance of it using the system activator, using case-sensitive search.</summary>
		/// <returns>An instance of the specified type created with the default constructor; or null if <paramref name="typeName" /> is not found. The type is resolved using the default binder, without specifying culture or activation attributes, and with <see cref="T:System.Reflection.BindingFlags" /> set to Public or Instance. </returns>
		/// <param name="typeName">The <see cref="P:System.Type.FullName" /> of the type to locate. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="typeName" /> is an empty string ("") or a string beginning with a null character.-or-The current assembly was loaded into the reflection-only context.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="typeName" /> is null. </exception>
		/// <exception cref="T:System.MissingMethodException">No matching constructor was found. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="typeName" /> requires a dependent assembly that could not be found. </exception>
		/// <exception cref="T:System.IO.FileLoadException">
		///   <paramref name="typeName" /> requires a dependent assembly that was found but could not be loaded.-or-The current assembly was loaded into the reflection-only context, and <paramref name="typeName" /> requires a dependent assembly that was not preloaded. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="typeName" /> requires a dependent assembly, but the file is not a valid assembly. -or-<paramref name="typeName" /> requires a dependent assembly which was compiled for a version of the runtime later than the currently loaded version.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x060021D4 RID: 8660 RVA: 0x0007FF87 File Offset: 0x0007E187
		public object CreateInstance(string typeName)
		{
			return this.CreateInstance(typeName, false);
		}

		/// <summary>Locates the specified type from this assembly and creates an instance of it using the system activator, with optional case-sensitive search.</summary>
		/// <returns>An instance of the specified type created with the default constructor; or null if <paramref name="typeName" /> is not found. The type is resolved using the default binder, without specifying culture or activation attributes, and with <see cref="T:System.Reflection.BindingFlags" /> set to Public or Instance.</returns>
		/// <param name="typeName">The <see cref="P:System.Type.FullName" /> of the type to locate. </param>
		/// <param name="ignoreCase">true to ignore the case of the type name; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="typeName" /> is an empty string ("") or a string beginning with a null character. -or-The current assembly was loaded into the reflection-only context.</exception>
		/// <exception cref="T:System.MissingMethodException">No matching constructor was found. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="typeName" /> is null. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="typeName" /> requires a dependent assembly that could not be found. </exception>
		/// <exception cref="T:System.IO.FileLoadException">
		///   <paramref name="typeName" /> requires a dependent assembly that was found but could not be loaded.-or-The current assembly was loaded into the reflection-only context, and <paramref name="typeName" /> requires a dependent assembly that was not preloaded. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="typeName" /> requires a dependent assembly, but the file is not a valid assembly. -or-<paramref name="typeName" /> requires a dependent assembly which was compiled for a version of the runtime later than the currently loaded version.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x060021D5 RID: 8661 RVA: 0x0007FF94 File Offset: 0x0007E194
		public object CreateInstance(string typeName, bool ignoreCase)
		{
			Type type = this.GetType(typeName, false, ignoreCase);
			if (type == null)
			{
				return null;
			}
			object obj;
			try
			{
				obj = Activator.CreateInstance(type);
			}
			catch (InvalidOperationException)
			{
				throw new ArgumentException("It is illegal to invoke a method on a Type loaded via ReflectionOnly methods.");
			}
			return obj;
		}

		/// <summary>Locates the specified type from this assembly and creates an instance of it using the system activator, with optional case-sensitive search and having the specified culture, arguments, and binding and activation attributes.</summary>
		/// <returns>An instance of the specified type, or null if <paramref name="typeName" /> is not found. The supplied arguments are used to resolve the type, and to bind the constructor that is used to create the instance.</returns>
		/// <param name="typeName">The <see cref="P:System.Type.FullName" /> of the type to locate. </param>
		/// <param name="ignoreCase">true to ignore the case of the type name; otherwise, false. </param>
		/// <param name="bindingAttr">A bitmask that affects the way in which the search is conducted. The value is a combination of bit flags from <see cref="T:System.Reflection.BindingFlags" />. </param>
		/// <param name="binder">An object that enables the binding, coercion of argument types, invocation of members, and retrieval of MemberInfo objects via reflection. If <paramref name="binder" /> is null, the default binder is used. </param>
		/// <param name="args">An array that contains the arguments to be passed to the constructor. This array of arguments must match in number, order, and type the parameters of the constructor to be invoked. If the default constructor is desired, <paramref name="args" /> must be an empty array or null. </param>
		/// <param name="culture">An instance of CultureInfo used to govern the coercion of types. If this is null, the CultureInfo for the current thread is used. (This is necessary to convert a String that represents 1000 to a Double value, for example, since 1000 is represented differently by different cultures.) </param>
		/// <param name="activationAttributes">An array of one or more attributes that can participate in activation. Typically, an array that contains a single <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> object. The <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> specifies the URL that is required to activate a remote object.  </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="typeName" /> is an empty string ("") or a string beginning with a null character. -or-The current assembly was loaded into the reflection-only context.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="typeName" /> is null. </exception>
		/// <exception cref="T:System.MissingMethodException">No matching constructor was found. </exception>
		/// <exception cref="T:System.NotSupportedException">A non-empty activation attributes array is passed to a type that does not inherit from <see cref="T:System.MarshalByRefObject" />. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="typeName" /> requires a dependent assembly that could not be found. </exception>
		/// <exception cref="T:System.IO.FileLoadException">
		///   <paramref name="typeName" /> requires a dependent assembly that was found but could not be loaded.-or-The current assembly was loaded into the reflection-only context, and <paramref name="typeName" /> requires a dependent assembly that was not preloaded. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="typeName" /> requires a dependent assembly, but the file is not a valid assembly. -or-<paramref name="typeName" /> requires a dependent assembly which was compiled for a version of the runtime later than the currently loaded version.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x060021D6 RID: 8662 RVA: 0x0007FFE0 File Offset: 0x0007E1E0
		public virtual object CreateInstance(string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes)
		{
			Type type = this.GetType(typeName, false, ignoreCase);
			if (type == null)
			{
				return null;
			}
			object obj;
			try
			{
				obj = Activator.CreateInstance(type, bindingAttr, binder, args, culture, activationAttributes);
			}
			catch (InvalidOperationException)
			{
				throw new ArgumentException("It is illegal to invoke a method on a Type loaded via ReflectionOnly methods.");
			}
			return obj;
		}

		/// <summary>Gets all the loaded modules that are part of this assembly.</summary>
		/// <returns>An array of modules.</returns>
		// Token: 0x060021D7 RID: 8663 RVA: 0x00080034 File Offset: 0x0007E234
		public Module[] GetLoadedModules()
		{
			return this.GetLoadedModules(false);
		}

		/// <summary>Gets all the modules that are part of this assembly.</summary>
		/// <returns>An array of modules.</returns>
		/// <exception cref="T:System.IO.FileNotFoundException">The module to be loaded does not specify a file name extension. </exception>
		// Token: 0x060021D8 RID: 8664 RVA: 0x0008003D File Offset: 0x0007E23D
		public Module[] GetModules()
		{
			return this.GetModules(false);
		}

		// Token: 0x060021D9 RID: 8665
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal virtual extern Module[] GetModulesInternal();

		/// <summary>Returns the names of all the resources in this assembly.</summary>
		/// <returns>An array that contains the names of all the resources.</returns>
		// Token: 0x060021DA RID: 8666
		[MethodImpl(MethodImplOptions.InternalCall)]
		public virtual extern string[] GetManifestResourceNames();

		/// <summary>Gets the assembly that contains the code that is currently executing.</summary>
		/// <returns>The assembly that contains the code that is currently executing. </returns>
		// Token: 0x060021DB RID: 8667
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Assembly GetExecutingAssembly();

		/// <summary>Returns the <see cref="T:System.Reflection.Assembly" /> of the method that invoked the currently executing method.</summary>
		/// <returns>The Assembly object of the method that invoked the currently executing method.</returns>
		// Token: 0x060021DC RID: 8668
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Assembly GetCallingAssembly();

		// Token: 0x060021DD RID: 8669
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr InternalGetReferencedAssemblies(Assembly module);

		// Token: 0x060021DE RID: 8670 RVA: 0x00080048 File Offset: 0x0007E248
		internal unsafe static AssemblyName[] GetReferencedAssemblies(Assembly module)
		{
			AssemblyName[] array2;
			using (SafeGPtrArrayHandle safeGPtrArrayHandle = new SafeGPtrArrayHandle(Assembly.InternalGetReferencedAssemblies(module)))
			{
				int length = safeGPtrArrayHandle.Length;
				try
				{
					AssemblyName[] array = new AssemblyName[length];
					for (int i = 0; i < length; i++)
					{
						AssemblyName assemblyName = new AssemblyName();
						MonoAssemblyName* ptr = (MonoAssemblyName*)(void*)safeGPtrArrayHandle[i];
						assemblyName.FillName(ptr, null, true, false, true, true);
						array[i] = assemblyName;
					}
					array2 = array;
				}
				finally
				{
					for (int j = 0; j < length; j++)
					{
						MonoAssemblyName* ptr2 = (MonoAssemblyName*)(void*)safeGPtrArrayHandle[j];
						RuntimeMarshal.FreeAssemblyName(ref *ptr2, true);
					}
				}
			}
			return array2;
		}

		// Token: 0x060021DF RID: 8671
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool GetManifestResourceInfoInternal(string name, ManifestResourceInfo info);

		/// <summary>Returns information about how the given resource has been persisted.</summary>
		/// <returns>An object that is populated with information about the resource's topology, or null if the resource is not found.</returns>
		/// <param name="resourceName">The case-sensitive name of the resource. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="resourceName" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="resourceName" /> parameter is an empty string (""). </exception>
		// Token: 0x060021E0 RID: 8672 RVA: 0x00080100 File Offset: 0x0007E300
		public virtual ManifestResourceInfo GetManifestResourceInfo(string resourceName)
		{
			if (resourceName == null)
			{
				throw new ArgumentNullException("resourceName");
			}
			if (resourceName.Length == 0)
			{
				throw new ArgumentException("String cannot have zero length.");
			}
			ManifestResourceInfo manifestResourceInfo = new ManifestResourceInfo(null, null, (ResourceLocation)0);
			if (this.GetManifestResourceInfoInternal(resourceName, manifestResourceInfo))
			{
				return manifestResourceInfo;
			}
			return null;
		}

		/// <summary>Gets the host context with which the assembly was loaded.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that indicates the host context with which the assembly was loaded, if any.</returns>
		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x060021E1 RID: 8673 RVA: 0x0005BC4E File Offset: 0x00059E4E
		[MonoTODO("Currently it always returns zero")]
		[ComVisible(false)]
		public virtual long HostContext
		{
			get
			{
				return 0L;
			}
		}

		// Token: 0x060021E2 RID: 8674 RVA: 0x00080144 File Offset: 0x0007E344
		internal virtual Module GetManifestModule()
		{
			return this.GetManifestModuleInternal();
		}

		// Token: 0x060021E3 RID: 8675
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern Module GetManifestModuleInternal();

		/// <summary>Gets a <see cref="T:System.Boolean" /> value indicating whether this assembly was loaded into the reflection-only context.</summary>
		/// <returns>true if the assembly was loaded into the reflection-only context, rather than the execution context; otherwise, false.</returns>
		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x060021E4 RID: 8676
		[ComVisible(false)]
		public virtual extern bool ReflectionOnly
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x060021E5 RID: 8677 RVA: 0x0007E1C5 File Offset: 0x0007C3C5
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Determines whether this assembly and the specified object are equal.</summary>
		/// <returns>true if <paramref name="o" /> is equal to this instance; otherwise, false.</returns>
		/// <param name="o">The object to compare with this instance. </param>
		// Token: 0x060021E6 RID: 8678 RVA: 0x0008014C File Offset: 0x0007E34C
		public override bool Equals(object o)
		{
			return this == o || (o != null && ((Assembly)o)._mono_assembly == this._mono_assembly);
		}

		// Token: 0x060021E7 RID: 8679 RVA: 0x00080170 File Offset: 0x0007E370
		internal void Resolve()
		{
			lock (this)
			{
				this.LoadAssemblyPermissions();
				Evidence evidence = new Evidence(this.UnprotectedGetEvidence());
				evidence.AddHost(new PermissionRequestEvidence(this._minimum, this._optional, this._refuse));
				this._granted = SecurityManager.ResolvePolicy(evidence, this._minimum, this._optional, this._refuse, out this._denied);
			}
		}

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x060021E8 RID: 8680 RVA: 0x000801F8 File Offset: 0x0007E3F8
		internal PermissionSet GrantedPermissionSet
		{
			get
			{
				if (this._granted == null)
				{
					if (SecurityManager.ResolvingPolicyLevel != null)
					{
						if (SecurityManager.ResolvingPolicyLevel.IsFullTrustAssembly(this))
						{
							return DefaultPolicies.FullTrust;
						}
						return null;
					}
					else
					{
						this.Resolve();
					}
				}
				return this._granted;
			}
		}

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x060021E9 RID: 8681 RVA: 0x0008022A File Offset: 0x0007E42A
		internal PermissionSet DeniedPermissionSet
		{
			get
			{
				if (this._granted == null)
				{
					if (SecurityManager.ResolvingPolicyLevel != null)
					{
						if (SecurityManager.ResolvingPolicyLevel.IsFullTrustAssembly(this))
						{
							return null;
						}
						return DefaultPolicies.FullTrust;
					}
					else
					{
						this.Resolve();
					}
				}
				return this._denied;
			}
		}

		// Token: 0x060021EA RID: 8682
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool LoadPermissions(Assembly a, ref IntPtr minimum, ref int minLength, ref IntPtr optional, ref int optLength, ref IntPtr refused, ref int refLength);

		// Token: 0x060021EB RID: 8683 RVA: 0x0008025C File Offset: 0x0007E45C
		private void LoadAssemblyPermissions()
		{
			IntPtr zero = IntPtr.Zero;
			IntPtr zero2 = IntPtr.Zero;
			IntPtr zero3 = IntPtr.Zero;
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			if (Assembly.LoadPermissions(this, ref zero, ref num, ref zero2, ref num2, ref zero3, ref num3))
			{
				if (num > 0)
				{
					byte[] array = new byte[num];
					Marshal.Copy(zero, array, 0, num);
					this._minimum = SecurityManager.Decode(array);
				}
				if (num2 > 0)
				{
					byte[] array2 = new byte[num2];
					Marshal.Copy(zero2, array2, 0, num2);
					this._optional = SecurityManager.Decode(array2);
				}
				if (num3 > 0)
				{
					byte[] array3 = new byte[num3];
					Marshal.Copy(zero3, array3, 0, num3);
					this._refuse = SecurityManager.Decode(array3);
				}
			}
		}

		/// <summary>Gets the grant set of the current assembly.</summary>
		/// <returns>The grant set of the current assembly.</returns>
		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x060021EC RID: 8684 RVA: 0x00080306 File Offset: 0x0007E506
		public virtual PermissionSet PermissionSet
		{
			get
			{
				return this.GrantedPermissionSet;
			}
		}

		/// <summary>Gets a value that indicates which set of security rules the common language runtime (CLR) enforces for this assembly.</summary>
		/// <returns>The security rule set that the CLR enforces for this assembly.</returns>
		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x060021ED RID: 8685 RVA: 0x0008030E File Offset: 0x0007E50E
		public virtual SecurityRuleSet SecurityRuleSet
		{
			get
			{
				throw Assembly.CreateNIE();
			}
		}

		// Token: 0x060021EE RID: 8686 RVA: 0x00080315 File Offset: 0x0007E515
		private static Exception CreateNIE()
		{
			return new NotImplementedException("Derived classes must implement it");
		}

		/// <summary>Returns information about the attributes that have been applied to the current <see cref="T:System.Reflection.Assembly" />, expressed as <see cref="T:System.Reflection.CustomAttributeData" /> objects.</summary>
		/// <returns>A generic list of <see cref="T:System.Reflection.CustomAttributeData" /> objects representing data about the attributes that have been applied to the current assembly.</returns>
		// Token: 0x060021EF RID: 8687 RVA: 0x00080321 File Offset: 0x0007E521
		public virtual IList<CustomAttributeData> GetCustomAttributesData()
		{
			return CustomAttributeData.GetCustomAttributes(this);
		}

		/// <summary>Gets a value that indicates whether the current assembly is loaded with full trust.</summary>
		/// <returns>true if the current assembly is loaded with full trust; otherwise, false.</returns>
		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x060021F0 RID: 8688 RVA: 0x00003B29 File Offset: 0x00001D29
		[MonoTODO]
		public bool IsFullyTrusted
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets the <see cref="T:System.Type" /> object with the specified name in the assembly instance, with the options of ignoring the case, and of throwing an exception if the type is not found.</summary>
		/// <returns>An object that represents the specified class.</returns>
		/// <param name="name">The full name of the type. </param>
		/// <param name="throwOnError">true to throw an exception if the type is not found; false to return null. </param>
		/// <param name="ignoreCase">true to ignore the case of the type name; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is invalid.-or- The length of <paramref name="name" /> exceeds 1024 characters. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.TypeLoadException">
		///   <paramref name="throwOnError" /> is true, and the type cannot be found.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="name" /> requires a dependent assembly that could not be found. </exception>
		/// <exception cref="T:System.IO.FileLoadException">
		///   <paramref name="name" /> requires a dependent assembly that was found but could not be loaded.-or-The current assembly was loaded into the reflection-only context, and <paramref name="name" /> requires a dependent assembly that was not preloaded. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="name" /> requires a dependent assembly, but the file is not a valid assembly. -or-<paramref name="name" /> requires a dependent assembly which was compiled for a version of the runtime later than the currently loaded version.</exception>
		// Token: 0x060021F1 RID: 8689 RVA: 0x0008030E File Offset: 0x0007E50E
		public virtual Type GetType(string name, bool throwOnError, bool ignoreCase)
		{
			throw Assembly.CreateNIE();
		}

		/// <summary>Gets the specified module in this assembly.</summary>
		/// <returns>The module being requested, or null if the module is not found.</returns>
		/// <param name="name">The name of the module being requested. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="name" /> parameter is an empty string (""). </exception>
		/// <exception cref="T:System.IO.FileLoadException">A file that was found could not be loaded. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="name" /> was not found. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="name" /> is not a valid assembly. </exception>
		// Token: 0x060021F2 RID: 8690 RVA: 0x0008030E File Offset: 0x0007E50E
		public virtual Module GetModule(string name)
		{
			throw Assembly.CreateNIE();
		}

		/// <summary>Gets the <see cref="T:System.Reflection.AssemblyName" /> objects for all the assemblies referenced by this assembly.</summary>
		/// <returns>An array that contains the fully parsed display names of all the assemblies referenced by this assembly.</returns>
		// Token: 0x060021F3 RID: 8691 RVA: 0x0008030E File Offset: 0x0007E50E
		public virtual AssemblyName[] GetReferencedAssemblies()
		{
			throw Assembly.CreateNIE();
		}

		/// <summary>Gets all the modules that are part of this assembly, specifying whether to include resource modules.</summary>
		/// <returns>An array of modules.</returns>
		/// <param name="getResourceModules">true to include resource modules; otherwise, false. </param>
		// Token: 0x060021F4 RID: 8692 RVA: 0x0008030E File Offset: 0x0007E50E
		public virtual Module[] GetModules(bool getResourceModules)
		{
			throw Assembly.CreateNIE();
		}

		/// <summary>Gets all the loaded modules that are part of this assembly, specifying whether to include resource modules.</summary>
		/// <returns>An array of modules.</returns>
		/// <param name="getResourceModules">true to include resource modules; otherwise, false. </param>
		// Token: 0x060021F5 RID: 8693 RVA: 0x0008030E File Offset: 0x0007E50E
		[MonoTODO("Always returns the same as GetModules")]
		public virtual Module[] GetLoadedModules(bool getResourceModules)
		{
			throw Assembly.CreateNIE();
		}

		/// <summary>Gets the satellite assembly for the specified culture.</summary>
		/// <returns>The specified satellite assembly.</returns>
		/// <param name="culture">The specified culture. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="culture" /> is null. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The assembly cannot be found. </exception>
		/// <exception cref="T:System.IO.FileLoadException">The satellite assembly with a matching file name was found, but the CultureInfo did not match the one specified. </exception>
		/// <exception cref="T:System.BadImageFormatException">The satellite assembly is not a valid assembly. </exception>
		// Token: 0x060021F6 RID: 8694 RVA: 0x0008030E File Offset: 0x0007E50E
		public virtual Assembly GetSatelliteAssembly(CultureInfo culture)
		{
			throw Assembly.CreateNIE();
		}

		/// <summary>Gets the specified version of the satellite assembly for the specified culture.</summary>
		/// <returns>The specified satellite assembly.</returns>
		/// <param name="culture">The specified culture. </param>
		/// <param name="version">The version of the satellite assembly. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="culture" /> is null. </exception>
		/// <exception cref="T:System.IO.FileLoadException">The satellite assembly with a matching file name was found, but the CultureInfo or the version did not match the one specified. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The assembly cannot be found. </exception>
		/// <exception cref="T:System.BadImageFormatException">The satellite assembly is not a valid assembly. </exception>
		// Token: 0x060021F7 RID: 8695 RVA: 0x0008030E File Offset: 0x0007E50E
		public virtual Assembly GetSatelliteAssembly(CultureInfo culture, Version version)
		{
			throw Assembly.CreateNIE();
		}

		/// <summary>Gets the module that contains the manifest for the current assembly. </summary>
		/// <returns>The module that contains the manifest for the assembly. </returns>
		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x060021F8 RID: 8696 RVA: 0x0008030E File Offset: 0x0007E50E
		public virtual Module ManifestModule
		{
			get
			{
				throw Assembly.CreateNIE();
			}
		}

		/// <summary>Gets a value indicating whether the assembly was loaded from the global assembly cache.</summary>
		/// <returns>true if the assembly was loaded from the global assembly cache; otherwise, false.</returns>
		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x060021F9 RID: 8697 RVA: 0x0008030E File Offset: 0x0007E50E
		public virtual bool GlobalAssemblyCache
		{
			get
			{
				throw Assembly.CreateNIE();
			}
		}

		/// <summary>Gets a value that indicates whether the current assembly was generated dynamically in the current process by using reflection emit.</summary>
		/// <returns>true if the current assembly was generated dynamically in the current process; otherwise, false.</returns>
		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x060021FA RID: 8698 RVA: 0x00015ED5 File Offset: 0x000140D5
		public virtual bool IsDynamic
		{
			get
			{
				return false;
			}
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.Assembly" /> objects are equal.</summary>
		/// <returns>true if <paramref name="left" /> is equal to <paramref name="right" />; otherwise, false.</returns>
		/// <param name="left">The assembly to compare to <paramref name="right" />. </param>
		/// <param name="right">The assembly to compare to <paramref name="left" />.</param>
		// Token: 0x060021FB RID: 8699 RVA: 0x00080329 File Offset: 0x0007E529
		public static bool operator ==(Assembly left, Assembly right)
		{
			return left == right || (!((left == null) ^ (right == null)) && left.Equals(right));
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.Assembly" /> objects are not equal.</summary>
		/// <returns>true if <paramref name="left" /> is not equal to <paramref name="right" />; otherwise, false.</returns>
		/// <param name="left">The assembly to compare to <paramref name="right" />.</param>
		/// <param name="right">The assembly to compare to <paramref name="left" />.</param>
		// Token: 0x060021FC RID: 8700 RVA: 0x00080345 File Offset: 0x0007E545
		public static bool operator !=(Assembly left, Assembly right)
		{
			return left != right && (((left == null) ^ (right == null)) || !left.Equals(right));
		}

		/// <summary>Gets a collection of the types defined in this assembly.</summary>
		/// <returns>A collection of the types defined in this assembly.</returns>
		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x060021FD RID: 8701 RVA: 0x00080364 File Offset: 0x0007E564
		public virtual IEnumerable<TypeInfo> DefinedTypes
		{
			get
			{
				foreach (Type type in this.GetTypes())
				{
					yield return type.GetTypeInfo();
				}
				Type[] array = null;
				yield break;
			}
		}

		/// <summary>Gets a collection of the public types defined in this assembly that are visible outside the assembly.</summary>
		/// <returns>A collection of the public types defined in this assembly that are visible outside the assembly.</returns>
		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x060021FE RID: 8702 RVA: 0x00080374 File Offset: 0x0007E574
		public virtual IEnumerable<Type> ExportedTypes
		{
			get
			{
				return this.GetExportedTypes();
			}
		}

		/// <summary>Gets a collection that contains the modules in this assembly.</summary>
		/// <returns>A collection that contains the modules in this assembly.</returns>
		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x060021FF RID: 8703 RVA: 0x0008037C File Offset: 0x0007E57C
		public virtual IEnumerable<Module> Modules
		{
			get
			{
				return this.GetModules();
			}
		}

		/// <summary>Gets a collection that contains this assembly's custom attributes.</summary>
		/// <returns>A collection that contains this assembly's custom attributes.</returns>
		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x06002200 RID: 8704 RVA: 0x00080384 File Offset: 0x0007E584
		public virtual IEnumerable<CustomAttributeData> CustomAttributes
		{
			get
			{
				return this.GetCustomAttributesData();
			}
		}

		// Token: 0x040012E5 RID: 4837
		internal IntPtr _mono_assembly;

		// Token: 0x040012E6 RID: 4838
		private Assembly.ResolveEventHolder resolve_event_holder;

		// Token: 0x040012E7 RID: 4839
		private Evidence _evidence;

		// Token: 0x040012E8 RID: 4840
		internal PermissionSet _minimum;

		// Token: 0x040012E9 RID: 4841
		internal PermissionSet _optional;

		// Token: 0x040012EA RID: 4842
		internal PermissionSet _refuse;

		// Token: 0x040012EB RID: 4843
		private PermissionSet _granted;

		// Token: 0x040012EC RID: 4844
		private PermissionSet _denied;

		// Token: 0x040012ED RID: 4845
		private bool fromByteArray;

		// Token: 0x040012EE RID: 4846
		private string assemblyName;

		// Token: 0x0200030B RID: 779
		internal class ResolveEventHolder
		{
			// Token: 0x14000017 RID: 23
			// (add) Token: 0x06002201 RID: 8705 RVA: 0x0008038C File Offset: 0x0007E58C
			// (remove) Token: 0x06002202 RID: 8706 RVA: 0x000803C4 File Offset: 0x0007E5C4
			public event ModuleResolveEventHandler ModuleResolve;
		}

		// Token: 0x0200030C RID: 780
		internal class UnmanagedMemoryStreamForModule : UnmanagedMemoryStream
		{
			// Token: 0x06002204 RID: 8708 RVA: 0x000803F9 File Offset: 0x0007E5F9
			public unsafe UnmanagedMemoryStreamForModule(byte* pointer, long length, Module module)
				: base(pointer, length)
			{
				this.module = module;
			}

			// Token: 0x06002205 RID: 8709 RVA: 0x0008040A File Offset: 0x0007E60A
			protected override void Dispose(bool disposing)
			{
				if (this._isOpen)
				{
					this.module = null;
				}
				base.Dispose(disposing);
			}

			// Token: 0x040012F0 RID: 4848
			private Module module;
		}
	}
}
