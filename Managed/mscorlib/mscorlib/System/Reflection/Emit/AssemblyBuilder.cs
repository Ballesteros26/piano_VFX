using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using Mono.Security;
using Unity;

namespace System.Reflection.Emit
{
	/// <summary>Defines and represents a dynamic assembly.</summary>
	// Token: 0x02000348 RID: 840
	[ComDefaultInterface(typeof(_AssemblyBuilder))]
	[ClassInterface(ClassInterfaceType.None)]
	[ComVisible(true)]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class AssemblyBuilder : Assembly, _AssemblyBuilder
	{
		// Token: 0x060024F6 RID: 9462
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void basic_init(AssemblyBuilder ab);

		// Token: 0x060024F7 RID: 9463
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void UpdateNativeCustomAttributes(AssemblyBuilder ab);

		// Token: 0x060024F8 RID: 9464 RVA: 0x00084BD4 File Offset: 0x00082DD4
		internal AssemblyBuilder(AssemblyName n, string directory, AssemblyBuilderAccess access, bool corlib_internal)
		{
			this.pekind = PEFileKinds.Dll;
			this.corlib_object_type = typeof(object);
			this.corlib_value_type = typeof(ValueType);
			this.corlib_enum_type = typeof(Enum);
			this.corlib_void_type = typeof(void);
			base..ctor();
			if ((access & (AssemblyBuilderAccess)2048) != (AssemblyBuilderAccess)0)
			{
				throw new NotImplementedException("COMPILER_ACCESS is no longer supperted, use a newer mcs.");
			}
			if (!Enum.IsDefined(typeof(AssemblyBuilderAccess), access))
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Argument value {0} is not valid.", (int)access), "access");
			}
			this.name = n.Name;
			this.access = (uint)access;
			this.flags = (uint)n.Flags;
			if (this.IsSave && (directory == null || directory.Length == 0))
			{
				this.dir = Directory.GetCurrentDirectory();
			}
			else
			{
				this.dir = directory;
			}
			if (n.CultureInfo != null)
			{
				this.culture = n.CultureInfo.Name;
				this.versioninfo_culture = n.CultureInfo.Name;
			}
			Version version = n.Version;
			if (version != null)
			{
				this.version = version.ToString();
			}
			if (n.KeyPair != null)
			{
				this.sn = n.KeyPair.StrongName();
			}
			else
			{
				byte[] publicKey = n.GetPublicKey();
				if (publicKey != null && publicKey.Length != 0)
				{
					this.sn = new StrongName(publicKey);
				}
			}
			if (this.sn != null)
			{
				this.flags |= 1U;
			}
			this.corlib_internal = corlib_internal;
			if (this.sn != null)
			{
				this.pktoken = new byte[this.sn.PublicKeyToken.Length * 2];
				int num = 0;
				foreach (byte b in this.sn.PublicKeyToken)
				{
					string text = b.ToString("x2");
					this.pktoken[num++] = (byte)text[0];
					this.pktoken[num++] = (byte)text[1];
				}
			}
			AssemblyBuilder.basic_init(this);
		}

		/// <summary>Gets the location of the assembly, as specified originally (such as in an <see cref="T:System.Reflection.AssemblyName" /> object).</summary>
		/// <returns>The location of the assembly, as specified originally.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x060024F9 RID: 9465 RVA: 0x00084DDE File Offset: 0x00082FDE
		public override string CodeBase
		{
			get
			{
				throw this.not_supported();
			}
		}

		/// <summary>Returns the entry point of this assembly.</summary>
		/// <returns>The entry point of this assembly.</returns>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x060024FA RID: 9466 RVA: 0x00084DE6 File Offset: 0x00082FE6
		public override MethodInfo EntryPoint
		{
			get
			{
				return this.entry_point;
			}
		}

		/// <summary>Gets the location, in codebase format, of the loaded file that contains the manifest if it is not shadow-copied.</summary>
		/// <returns>The location of the loaded file that contains the manifest. If the loaded file has been shadow-copied, the Location is that of the file before being shadow-copied.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x060024FB RID: 9467 RVA: 0x00084DDE File Offset: 0x00082FDE
		public override string Location
		{
			get
			{
				throw this.not_supported();
			}
		}

		/// <summary>Gets the version of the common language runtime that will be saved in the file containing the manifest.</summary>
		/// <returns>A string representing the common language runtime version.</returns>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x060024FC RID: 9468 RVA: 0x00084DEE File Offset: 0x00082FEE
		public override string ImageRuntimeVersion
		{
			get
			{
				return base.ImageRuntimeVersion;
			}
		}

		/// <summary>Gets a value indicating whether the dynamic assembly is in the reflection-only context.</summary>
		/// <returns>true if the dynamic assembly is in the reflection-only context; otherwise, false.</returns>
		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x060024FD RID: 9469 RVA: 0x00084DF6 File Offset: 0x00082FF6
		[MonoTODO]
		public override bool ReflectionOnly
		{
			get
			{
				return base.ReflectionOnly;
			}
		}

		/// <summary>Adds an existing resource file to this assembly.</summary>
		/// <param name="name">The logical name of the resource. </param>
		/// <param name="fileName">The physical file name (.resources file) to which the logical name is mapped. This should not include a path; the file must be in the same directory as the assembly to which it is added. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> has been previously defined.-or- There is another file in the assembly named <paramref name="fileName" />.-or- The length of <paramref name="name" /> is zero.-or- The length of <paramref name="fileName" /> is zero, or if <paramref name="fileName" /> includes a path. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> or <paramref name="fileName" /> is null. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file <paramref name="fileName" /> is not found. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060024FE RID: 9470 RVA: 0x00084DFE File Offset: 0x00082FFE
		public void AddResourceFile(string name, string fileName)
		{
			this.AddResourceFile(name, fileName, ResourceAttributes.Public);
		}

		/// <summary>Adds an existing resource file to this assembly.</summary>
		/// <param name="name">The logical name of the resource. </param>
		/// <param name="fileName">The physical file name (.resources file) to which the logical name is mapped. This should not include a path; the file must be in the same directory as the assembly to which it is added. </param>
		/// <param name="attribute">The resource attributes. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> has been previously defined.-or- There is another file in the assembly named <paramref name="fileName" />.-or- The length of <paramref name="name" /> is zero or if the length of <paramref name="fileName" /> is zero.-or- <paramref name="fileName" /> includes a path. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> or <paramref name="fileName" /> is null. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">If the file <paramref name="fileName" /> is not found. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060024FF RID: 9471 RVA: 0x00084E09 File Offset: 0x00083009
		public void AddResourceFile(string name, string fileName, ResourceAttributes attribute)
		{
			this.AddResourceFile(name, fileName, attribute, true);
		}

		// Token: 0x06002500 RID: 9472 RVA: 0x00084E18 File Offset: 0x00083018
		private void AddResourceFile(string name, string fileName, ResourceAttributes attribute, bool fileNeedsToExists)
		{
			this.check_name_and_filename(name, fileName, fileNeedsToExists);
			if (this.dir != null)
			{
				fileName = Path.Combine(this.dir, fileName);
			}
			if (this.resources != null)
			{
				MonoResource[] array = new MonoResource[this.resources.Length + 1];
				Array.Copy(this.resources, array, this.resources.Length);
				this.resources = array;
			}
			else
			{
				this.resources = new MonoResource[1];
			}
			int num = this.resources.Length - 1;
			this.resources[num].name = name;
			this.resources[num].filename = fileName;
			this.resources[num].attrs = attribute;
		}

		// Token: 0x06002501 RID: 9473 RVA: 0x00084EC8 File Offset: 0x000830C8
		internal void AddPermissionRequests(PermissionSet required, PermissionSet optional, PermissionSet refused)
		{
			if (this.created)
			{
				throw new InvalidOperationException("Assembly was already saved.");
			}
			this._minimum = required;
			this._optional = optional;
			this._refuse = refused;
			if (required != null)
			{
				this.permissions_minimum = new RefEmitPermissionSet[1];
				this.permissions_minimum[0] = new RefEmitPermissionSet(SecurityAction.RequestMinimum, required.ToXml().ToString());
			}
			if (optional != null)
			{
				this.permissions_optional = new RefEmitPermissionSet[1];
				this.permissions_optional[0] = new RefEmitPermissionSet(SecurityAction.RequestOptional, optional.ToXml().ToString());
			}
			if (refused != null)
			{
				this.permissions_refused = new RefEmitPermissionSet[1];
				this.permissions_refused[0] = new RefEmitPermissionSet(SecurityAction.RequestRefuse, refused.ToXml().ToString());
			}
		}

		// Token: 0x06002502 RID: 9474 RVA: 0x00084F83 File Offset: 0x00083183
		internal void EmbedResourceFile(string name, string fileName)
		{
			this.EmbedResourceFile(name, fileName, ResourceAttributes.Public);
		}

		// Token: 0x06002503 RID: 9475 RVA: 0x00084F90 File Offset: 0x00083190
		private void EmbedResourceFile(string name, string fileName, ResourceAttributes attribute)
		{
			if (this.resources != null)
			{
				MonoResource[] array = new MonoResource[this.resources.Length + 1];
				Array.Copy(this.resources, array, this.resources.Length);
				this.resources = array;
			}
			else
			{
				this.resources = new MonoResource[1];
			}
			int num = this.resources.Length - 1;
			this.resources[num].name = name;
			this.resources[num].attrs = attribute;
			try
			{
				FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
				long length = fileStream.Length;
				this.resources[num].data = new byte[length];
				fileStream.Read(this.resources[num].data, 0, (int)length);
				fileStream.Close();
			}
			catch
			{
			}
		}

		/// <summary>Defines a dynamic assembly that has the specified name and access rights.</summary>
		/// <returns>An object that represents the new assembly.</returns>
		/// <param name="name">The name of the assembly.</param>
		/// <param name="access">The access rights of the assembly.</param>
		// Token: 0x06002504 RID: 9476 RVA: 0x0008506C File Offset: 0x0008326C
		public static AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			return new AssemblyBuilder(name, null, access, false);
		}

		/// <summary>Defines a new assembly that has the specified name, access rights, and attributes.</summary>
		/// <returns>An object that represents the new assembly.</returns>
		/// <param name="name">The name of the assembly.</param>
		/// <param name="access">The access rights of the assembly.</param>
		/// <param name="assemblyAttributes">A collection that contains the attributes of the assembly.</param>
		// Token: 0x06002505 RID: 9477 RVA: 0x00085088 File Offset: 0x00083288
		public static AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, IEnumerable<CustomAttributeBuilder> assemblyAttributes)
		{
			AssemblyBuilder assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(name, access);
			foreach (CustomAttributeBuilder customAttributeBuilder in assemblyAttributes)
			{
				assemblyBuilder.SetCustomAttribute(customAttributeBuilder);
			}
			return assemblyBuilder;
		}

		/// <summary>Defines a named transient dynamic module in this assembly.</summary>
		/// <returns>A <see cref="T:System.Reflection.Emit.ModuleBuilder" /> representing the defined dynamic module.</returns>
		/// <param name="name">The name of the dynamic module. Must be less than 260 characters in length. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> begins with white space.-or- The length of <paramref name="name" /> is zero.-or- The length of <paramref name="name" /> is greater than or equal to 260. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.ExecutionEngineException">The assembly for default symbol writer cannot be loaded.-or- The type that implements the default symbol writer interface cannot be found. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x06002506 RID: 9478 RVA: 0x000850DC File Offset: 0x000832DC
		public ModuleBuilder DefineDynamicModule(string name)
		{
			return this.DefineDynamicModule(name, name, false, true);
		}

		/// <summary>Defines a named transient dynamic module in this assembly and specifies whether symbol information should be emitted.</summary>
		/// <returns>A <see cref="T:System.Reflection.Emit.ModuleBuilder" /> representing the defined dynamic module.</returns>
		/// <param name="name">The name of the dynamic module. Must be less than 260 characters in length. </param>
		/// <param name="emitSymbolInfo">true if symbol information is to be emitted; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> begins with white space.-or- The length of <paramref name="name" /> is zero.-or- The length of <paramref name="name" /> is greater than or equal to 260. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.ExecutionEngineException">The assembly for default symbol writer cannot be loaded.-or- The type that implements the default symbol writer interface cannot be found. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x06002507 RID: 9479 RVA: 0x000850E8 File Offset: 0x000832E8
		public ModuleBuilder DefineDynamicModule(string name, bool emitSymbolInfo)
		{
			return this.DefineDynamicModule(name, name, emitSymbolInfo, true);
		}

		/// <summary>Defines a persistable dynamic module with the given name that will be saved to the specified file. No symbol information is emitted.</summary>
		/// <returns>A <see cref="T:System.Reflection.Emit.ModuleBuilder" /> object representing the defined dynamic module.</returns>
		/// <param name="name">The name of the dynamic module. Must be less than 260 characters in length. </param>
		/// <param name="fileName">The name of the file to which the dynamic module should be saved. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> or <paramref name="fileName" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">The length of <paramref name="name" /> or <paramref name="fileName" /> is zero.-or- The length of <paramref name="name" /> is greater than or equal to 260.-or- <paramref name="fileName" /> contains a path specification (a directory component, for example).-or- There is a conflict with the name of another file that belongs to this assembly. </exception>
		/// <exception cref="T:System.InvalidOperationException">This assembly has been previously saved. </exception>
		/// <exception cref="T:System.NotSupportedException">This assembly was called on a dynamic assembly with <see cref="F:System.Reflection.Emit.AssemblyBuilderAccess.Run" /> attribute. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.ExecutionEngineException">The assembly for default symbol writer cannot be loaded.-or- The type that implements the default symbol writer interface cannot be found. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x06002508 RID: 9480 RVA: 0x000850F4 File Offset: 0x000832F4
		public ModuleBuilder DefineDynamicModule(string name, string fileName)
		{
			return this.DefineDynamicModule(name, fileName, false, false);
		}

		/// <summary>Defines a persistable dynamic module, specifying the module name, the name of the file to which the module will be saved, and whether symbol information should be emitted using the default symbol writer.</summary>
		/// <returns>A <see cref="T:System.Reflection.Emit.ModuleBuilder" /> object representing the defined dynamic module.</returns>
		/// <param name="name">The name of the dynamic module. Must be less than 260 characters in length. </param>
		/// <param name="fileName">The name of the file to which the dynamic module should be saved. </param>
		/// <param name="emitSymbolInfo">If true, symbolic information is written using the default symbol writer. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> or <paramref name="fileName" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">The length of <paramref name="name" /> or <paramref name="fileName" /> is zero.-or- The length of <paramref name="name" /> is greater than or equal to 260.-or- <paramref name="fileName" /> contains a path specification (a directory component, for example).-or- There is a conflict with the name of another file that belongs to this assembly. </exception>
		/// <exception cref="T:System.InvalidOperationException">This assembly has been previously saved. </exception>
		/// <exception cref="T:System.NotSupportedException">This assembly was called on a dynamic assembly with the <see cref="F:System.Reflection.Emit.AssemblyBuilderAccess.Run" /> attribute. </exception>
		/// <exception cref="T:System.ExecutionEngineException">The assembly for default symbol writer cannot be loaded.-or- The type that implements the default symbol writer interface cannot be found. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x06002509 RID: 9481 RVA: 0x00085100 File Offset: 0x00083300
		public ModuleBuilder DefineDynamicModule(string name, string fileName, bool emitSymbolInfo)
		{
			return this.DefineDynamicModule(name, fileName, emitSymbolInfo, false);
		}

		// Token: 0x0600250A RID: 9482 RVA: 0x0008510C File Offset: 0x0008330C
		private ModuleBuilder DefineDynamicModule(string name, string fileName, bool emitSymbolInfo, bool transient)
		{
			this.check_name_and_filename(name, fileName, false);
			if (!transient)
			{
				if (Path.GetExtension(fileName) == string.Empty)
				{
					throw new ArgumentException("Module file name '" + fileName + "' must have file extension.");
				}
				if (!this.IsSave)
				{
					throw new NotSupportedException("Persistable modules are not supported in a dynamic assembly created with AssemblyBuilderAccess.Run");
				}
				if (this.created)
				{
					throw new InvalidOperationException("Assembly was already saved.");
				}
			}
			ModuleBuilder moduleBuilder = new ModuleBuilder(this, name, fileName, emitSymbolInfo, transient);
			if (this.modules != null && this.is_module_only)
			{
				throw new InvalidOperationException("A module-only assembly can only contain one module.");
			}
			if (this.modules != null)
			{
				ModuleBuilder[] array = new ModuleBuilder[this.modules.Length + 1];
				Array.Copy(this.modules, array, this.modules.Length);
				this.modules = array;
			}
			else
			{
				this.modules = new ModuleBuilder[1];
			}
			this.modules[this.modules.Length - 1] = moduleBuilder;
			return moduleBuilder;
		}

		/// <summary>Defines a standalone managed resource for this assembly with the default public resource attribute.</summary>
		/// <returns>A <see cref="T:System.Resources.ResourceWriter" /> object for the specified resource.</returns>
		/// <param name="name">The logical name of the resource. </param>
		/// <param name="description">A textual description of the resource. </param>
		/// <param name="fileName">The physical file name (.resources file) to which the logical name is mapped. This should not include a path. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> has been previously defined.-or- There is another file in the assembly named <paramref name="fileName" />.-or- The length of <paramref name="name" /> is zero.-or- The length of <paramref name="fileName" /> is zero.-or- <paramref name="fileName" /> includes a path. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> or <paramref name="fileName" /> is null. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600250B RID: 9483 RVA: 0x000851EF File Offset: 0x000833EF
		public IResourceWriter DefineResource(string name, string description, string fileName)
		{
			return this.DefineResource(name, description, fileName, ResourceAttributes.Public);
		}

		/// <summary>Defines a standalone managed resource for this assembly. Attributes can be specified for the managed resource.</summary>
		/// <returns>A <see cref="T:System.Resources.ResourceWriter" /> object for the specified resource.</returns>
		/// <param name="name">The logical name of the resource. </param>
		/// <param name="description">A textual description of the resource. </param>
		/// <param name="fileName">The physical file name (.resources file) to which the logical name is mapped. This should not include a path. </param>
		/// <param name="attribute">The resource attributes. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> has been previously defined or if there is another file in the assembly named <paramref name="fileName" />.-or- The length of <paramref name="name" /> is zero.-or- The length of <paramref name="fileName" /> is zero.-or- <paramref name="fileName" /> includes a path. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> or <paramref name="fileName" /> is null. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600250C RID: 9484 RVA: 0x000851FC File Offset: 0x000833FC
		public IResourceWriter DefineResource(string name, string description, string fileName, ResourceAttributes attribute)
		{
			this.AddResourceFile(name, fileName, attribute, false);
			IResourceWriter resourceWriter = new ResourceWriter(fileName);
			if (this.resource_writers == null)
			{
				this.resource_writers = new ArrayList();
			}
			this.resource_writers.Add(resourceWriter);
			return resourceWriter;
		}

		// Token: 0x0600250D RID: 9485 RVA: 0x0008523C File Offset: 0x0008343C
		private void AddUnmanagedResource(Win32Resource res)
		{
			MemoryStream memoryStream = new MemoryStream();
			res.WriteTo(memoryStream);
			if (this.win32_resources != null)
			{
				MonoWin32Resource[] array = new MonoWin32Resource[this.win32_resources.Length + 1];
				Array.Copy(this.win32_resources, array, this.win32_resources.Length);
				this.win32_resources = array;
			}
			else
			{
				this.win32_resources = new MonoWin32Resource[1];
			}
			this.win32_resources[this.win32_resources.Length - 1] = new MonoWin32Resource(res.Type.Id, res.Name.Id, res.Language, memoryStream.ToArray());
		}

		/// <summary>Defines an unmanaged resource for this assembly as an opaque blob of bytes.</summary>
		/// <param name="resource">The opaque blob of bytes representing the unmanaged resource. </param>
		/// <exception cref="T:System.ArgumentException">An unmanaged resource was previously defined. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="resource" /> is null. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600250E RID: 9486 RVA: 0x000852D3 File Offset: 0x000834D3
		[MonoTODO("Not currently implemenented")]
		public void DefineUnmanagedResource(byte[] resource)
		{
			if (resource == null)
			{
				throw new ArgumentNullException("resource");
			}
			if (this.native_resource != NativeResourceType.None)
			{
				throw new ArgumentException("Native resource has already been defined.");
			}
			this.native_resource = NativeResourceType.Unmanaged;
			throw new NotImplementedException();
		}

		/// <summary>Defines an unmanaged resource file for this assembly given the name of the resource file.</summary>
		/// <param name="resourceFileName">The name of the resource file. </param>
		/// <exception cref="T:System.ArgumentException">An unmanaged resource was previously defined.-or- The file <paramref name="resourceFileName" /> is not readable.-or- <paramref name="resourceFileName" /> is the empty string (""). </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="resourceFileName" /> is null. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="resourceFileName" /> is not found.-or- <paramref name="resourceFileName" /> is a directory. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600250F RID: 9487 RVA: 0x00085304 File Offset: 0x00083504
		public void DefineUnmanagedResource(string resourceFileName)
		{
			if (resourceFileName == null)
			{
				throw new ArgumentNullException("resourceFileName");
			}
			if (resourceFileName.Length == 0)
			{
				throw new ArgumentException("resourceFileName");
			}
			if (!File.Exists(resourceFileName) || Directory.Exists(resourceFileName))
			{
				throw new FileNotFoundException("File '" + resourceFileName + "' does not exist or is a directory.");
			}
			if (this.native_resource != NativeResourceType.None)
			{
				throw new ArgumentException("Native resource has already been defined.");
			}
			this.native_resource = NativeResourceType.Unmanaged;
			using (FileStream fileStream = new FileStream(resourceFileName, FileMode.Open, FileAccess.Read))
			{
				foreach (object obj in new Win32ResFileReader(fileStream).ReadResources())
				{
					Win32EncodedResource win32EncodedResource = (Win32EncodedResource)obj;
					if (win32EncodedResource.Name.IsName || win32EncodedResource.Type.IsName)
					{
						throw new InvalidOperationException("resource files with named resources or non-default resource types are not supported.");
					}
					this.AddUnmanagedResource(win32EncodedResource);
				}
			}
		}

		/// <summary>Defines an unmanaged version information resource using the information specified in the assembly's AssemblyName object and the assembly's custom attributes.</summary>
		/// <exception cref="T:System.ArgumentException">An unmanaged version information resource was previously defined.-or- The unmanaged version information is too large to persist. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x06002510 RID: 9488 RVA: 0x00085408 File Offset: 0x00083608
		public void DefineVersionInfoResource()
		{
			if (this.native_resource != NativeResourceType.None)
			{
				throw new ArgumentException("Native resource has already been defined.");
			}
			this.native_resource = NativeResourceType.Assembly;
			this.version_res = new Win32VersionResource(1, 0, false);
		}

		/// <summary>Defines an unmanaged version information resource for this assembly with the given specifications.</summary>
		/// <param name="product">The name of the product with which this assembly is distributed. </param>
		/// <param name="productVersion">The version of the product with which this assembly is distributed. </param>
		/// <param name="company">The name of the company that produced this assembly. </param>
		/// <param name="copyright">Describes all copyright notices, trademarks, and registered trademarks that apply to this assembly. This should include the full text of all notices, legal symbols, copyright dates, trademark numbers, and so on. In English, this string should be in the format "Copyright Microsoft Corp. 1990-2001". </param>
		/// <param name="trademark">Describes all trademarks and registered trademarks that apply to this assembly. This should include the full text of all notices, legal symbols, trademark numbers, and so on. In English, this string should be in the format "Windows is a trademark of Microsoft Corporation". </param>
		/// <exception cref="T:System.ArgumentException">An unmanaged version information resource was previously defined.-or- The unmanaged version information is too large to persist. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x06002511 RID: 9489 RVA: 0x00085434 File Offset: 0x00083634
		public void DefineVersionInfoResource(string product, string productVersion, string company, string copyright, string trademark)
		{
			if (this.native_resource != NativeResourceType.None)
			{
				throw new ArgumentException("Native resource has already been defined.");
			}
			this.native_resource = NativeResourceType.Explicit;
			this.version_res = new Win32VersionResource(1, 0, false);
			this.version_res.ProductName = ((product != null) ? product : " ");
			this.version_res.ProductVersion = ((productVersion != null) ? productVersion : " ");
			this.version_res.CompanyName = ((company != null) ? company : " ");
			this.version_res.LegalCopyright = ((copyright != null) ? copyright : " ");
			this.version_res.LegalTrademarks = ((trademark != null) ? trademark : " ");
		}

		// Token: 0x06002512 RID: 9490 RVA: 0x000854DC File Offset: 0x000836DC
		private void DefineVersionInfoResourceImpl(string fileName)
		{
			if (this.versioninfo_culture != null)
			{
				this.version_res.FileLanguage = new CultureInfo(this.versioninfo_culture).LCID;
			}
			this.version_res.Version = ((this.version == null) ? "0.0.0.0" : this.version);
			if (this.cattrs != null)
			{
				NativeResourceType nativeResourceType = this.native_resource;
				if (nativeResourceType != NativeResourceType.Assembly)
				{
					if (nativeResourceType == NativeResourceType.Explicit)
					{
						foreach (CustomAttributeBuilder customAttributeBuilder in this.cattrs)
						{
							string fullName = customAttributeBuilder.Ctor.ReflectedType.FullName;
							if (fullName == "System.Reflection.AssemblyCultureAttribute")
							{
								this.version_res.FileLanguage = new CultureInfo(customAttributeBuilder.string_arg()).LCID;
							}
							else if (fullName == "System.Reflection.AssemblyDescriptionAttribute")
							{
								this.version_res.Comments = customAttributeBuilder.string_arg();
							}
						}
					}
				}
				else
				{
					foreach (CustomAttributeBuilder customAttributeBuilder2 in this.cattrs)
					{
						string fullName2 = customAttributeBuilder2.Ctor.ReflectedType.FullName;
						if (fullName2 == "System.Reflection.AssemblyProductAttribute")
						{
							this.version_res.ProductName = customAttributeBuilder2.string_arg();
						}
						else if (fullName2 == "System.Reflection.AssemblyCompanyAttribute")
						{
							this.version_res.CompanyName = customAttributeBuilder2.string_arg();
						}
						else if (fullName2 == "System.Reflection.AssemblyCopyrightAttribute")
						{
							this.version_res.LegalCopyright = customAttributeBuilder2.string_arg();
						}
						else if (fullName2 == "System.Reflection.AssemblyTrademarkAttribute")
						{
							this.version_res.LegalTrademarks = customAttributeBuilder2.string_arg();
						}
						else if (fullName2 == "System.Reflection.AssemblyCultureAttribute")
						{
							this.version_res.FileLanguage = new CultureInfo(customAttributeBuilder2.string_arg()).LCID;
						}
						else if (fullName2 == "System.Reflection.AssemblyFileVersionAttribute")
						{
							this.version_res.FileVersion = customAttributeBuilder2.string_arg();
						}
						else if (fullName2 == "System.Reflection.AssemblyInformationalVersionAttribute")
						{
							this.version_res.ProductVersion = customAttributeBuilder2.string_arg();
						}
						else if (fullName2 == "System.Reflection.AssemblyTitleAttribute")
						{
							this.version_res.FileDescription = customAttributeBuilder2.string_arg();
						}
						else if (fullName2 == "System.Reflection.AssemblyDescriptionAttribute")
						{
							this.version_res.Comments = customAttributeBuilder2.string_arg();
						}
					}
				}
			}
			this.version_res.OriginalFilename = fileName;
			this.version_res.InternalName = Path.GetFileNameWithoutExtension(fileName);
			this.AddUnmanagedResource(this.version_res);
		}

		/// <summary>Returns the dynamic module with the specified name.</summary>
		/// <returns>A ModuleBuilder object representing the requested dynamic module.</returns>
		/// <param name="name">The name of the requested dynamic module. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">The length of <paramref name="name" /> is zero. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x06002513 RID: 9491 RVA: 0x00085764 File Offset: 0x00083964
		public ModuleBuilder GetDynamicModule(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException("Empty name is not legal.", "name");
			}
			if (this.modules != null)
			{
				for (int i = 0; i < this.modules.Length; i++)
				{
					if (this.modules[i].name == name)
					{
						return this.modules[i];
					}
				}
			}
			return null;
		}

		/// <summary>Gets the exported types defined in this assembly.</summary>
		/// <returns>An array of <see cref="T:System.Type" /> containing the exported types defined in this assembly.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not implemented. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x06002514 RID: 9492 RVA: 0x00084DDE File Offset: 0x00082FDE
		public override Type[] GetExportedTypes()
		{
			throw this.not_supported();
		}

		/// <summary>Gets a <see cref="T:System.IO.FileStream" /> for the specified file in the file table of the manifest of this assembly.</summary>
		/// <returns>A <see cref="T:System.IO.FileStream" /> for the specified file, or null, if the file is not found.</returns>
		/// <param name="name">The name of the specified file. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x06002515 RID: 9493 RVA: 0x00084DDE File Offset: 0x00082FDE
		public override FileStream GetFile(string name)
		{
			throw this.not_supported();
		}

		/// <summary>Gets the files in the file table of an assembly manifest, specifying whether to include resource modules.</summary>
		/// <returns>An array of <see cref="T:System.IO.FileStream" /> objects.</returns>
		/// <param name="getResourceModules">true to include resource modules; otherwise, false. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x06002516 RID: 9494 RVA: 0x00084DDE File Offset: 0x00082FDE
		public override FileStream[] GetFiles(bool getResourceModules)
		{
			throw this.not_supported();
		}

		// Token: 0x06002517 RID: 9495 RVA: 0x000857D1 File Offset: 0x000839D1
		internal override Module[] GetModulesInternal()
		{
			if (this.modules == null)
			{
				return new Module[0];
			}
			return (Module[])this.modules.Clone();
		}

		// Token: 0x06002518 RID: 9496 RVA: 0x000857F4 File Offset: 0x000839F4
		internal override Type[] GetTypes(bool exportedOnly)
		{
			Type[] array = null;
			if (this.modules != null)
			{
				for (int i = 0; i < this.modules.Length; i++)
				{
					Type[] types = this.modules[i].GetTypes();
					if (array == null)
					{
						array = types;
					}
					else
					{
						Type[] array2 = new Type[array.Length + types.Length];
						Array.Copy(array, 0, array2, 0, array.Length);
						Array.Copy(types, 0, array2, array.Length, types.Length);
					}
				}
			}
			if (this.loaded_modules != null)
			{
				for (int j = 0; j < this.loaded_modules.Length; j++)
				{
					Type[] types2 = this.loaded_modules[j].GetTypes();
					if (array == null)
					{
						array = types2;
					}
					else
					{
						Type[] array3 = new Type[array.Length + types2.Length];
						Array.Copy(array, 0, array3, 0, array.Length);
						Array.Copy(types2, 0, array3, array.Length, types2.Length);
					}
				}
			}
			if (array != null)
			{
				List<Exception> list = null;
				foreach (Type type in array)
				{
					if (type is TypeBuilder)
					{
						if (list == null)
						{
							list = new List<Exception>();
						}
						list.Add(new TypeLoadException(string.Format("Type '{0}' is not finished", type.FullName)));
					}
				}
				if (list != null)
				{
					throw new ReflectionTypeLoadException(new Type[list.Count], list.ToArray());
				}
			}
			if (array != null)
			{
				return array;
			}
			return Type.EmptyTypes;
		}

		/// <summary>Returns information about how the given resource has been persisted.</summary>
		/// <returns>
		///   <see cref="T:System.Reflection.ManifestResourceInfo" /> populated with information about the resource's topology, or null if the resource is not found.</returns>
		/// <param name="resourceName">The name of the resource. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x06002519 RID: 9497 RVA: 0x00084DDE File Offset: 0x00082FDE
		public override ManifestResourceInfo GetManifestResourceInfo(string resourceName)
		{
			throw this.not_supported();
		}

		/// <summary>Loads the specified manifest resource from this assembly.</summary>
		/// <returns>An array of type String containing the names of all the resources.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not supported on a dynamic assembly. To get the manifest resource names, use <see cref="M:System.Reflection.Assembly.GetManifestResourceNames" />. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x0600251A RID: 9498 RVA: 0x00084DDE File Offset: 0x00082FDE
		public override string[] GetManifestResourceNames()
		{
			throw this.not_supported();
		}

		/// <summary>Loads the specified manifest resource from this assembly.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> representing this manifest resource.</returns>
		/// <param name="name">The name of the manifest resource being requested. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x0600251B RID: 9499 RVA: 0x00084DDE File Offset: 0x00082FDE
		public override Stream GetManifestResourceStream(string name)
		{
			throw this.not_supported();
		}

		/// <summary>Loads the specified manifest resource, scoped by the namespace of the specified type, from this assembly.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> representing this manifest resource.</returns>
		/// <param name="type">The type whose namespace is used to scope the manifest resource name. </param>
		/// <param name="name">The name of the manifest resource being requested. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x0600251C RID: 9500 RVA: 0x00084DDE File Offset: 0x00082FDE
		public override Stream GetManifestResourceStream(Type type, string name)
		{
			throw this.not_supported();
		}

		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x0600251D RID: 9501 RVA: 0x00085939 File Offset: 0x00083B39
		internal bool IsSave
		{
			get
			{
				return this.access != 1U;
			}
		}

		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x0600251E RID: 9502 RVA: 0x00085947 File Offset: 0x00083B47
		internal bool IsRun
		{
			get
			{
				return this.access == 1U || this.access == 3U || this.access == 9U;
			}
		}

		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x0600251F RID: 9503 RVA: 0x00085967 File Offset: 0x00083B67
		internal string AssemblyDir
		{
			get
			{
				return this.dir;
			}
		}

		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x06002520 RID: 9504 RVA: 0x0008596F File Offset: 0x00083B6F
		// (set) Token: 0x06002521 RID: 9505 RVA: 0x00085977 File Offset: 0x00083B77
		internal bool IsModuleOnly
		{
			get
			{
				return this.is_module_only;
			}
			set
			{
				this.is_module_only = value;
			}
		}

		// Token: 0x06002522 RID: 9506 RVA: 0x00085980 File Offset: 0x00083B80
		internal override Module GetManifestModule()
		{
			if (this.manifest_module == null)
			{
				this.manifest_module = this.DefineDynamicModule("Default Dynamic Module");
			}
			return this.manifest_module;
		}

		/// <summary>Saves this dynamic assembly to disk, specifying the nature of code in the assembly's executables and the target platform.</summary>
		/// <param name="assemblyFileName">The file name of the assembly.</param>
		/// <param name="portableExecutableKind">A bitwise combination of the <see cref="T:System.Reflection.PortableExecutableKinds" /> values that specifies the nature of the code.</param>
		/// <param name="imageFileMachine">One of the <see cref="T:System.Reflection.ImageFileMachine" /> values that specifies the target platform.</param>
		/// <exception cref="T:System.ArgumentException">The length of <paramref name="assemblyFileName" /> is 0.-or- There are two or more modules resource files in the assembly with the same name.-or- The target directory of the assembly is invalid.-or- <paramref name="assemblyFileName" /> is not a simple file name (for example, has a directory or drive component), or more than one unmanaged resource, including a version information resources, was defined in this assembly.-or- The CultureInfo string in <see cref="T:System.Reflection.AssemblyCultureAttribute" /> is not a valid string and <see cref="M:System.Reflection.Emit.AssemblyBuilder.DefineVersionInfoResource(System.String,System.String,System.String,System.String,System.String)" /> was called prior to calling this method. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyFileName" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">This assembly has been saved before.-or- This assembly has access Run<see cref="T:System.Reflection.Emit.AssemblyBuilderAccess" /></exception>
		/// <exception cref="T:System.IO.IOException">An output error occurs during the save. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> has not been called for any of the types in the modules of the assembly to be written to disk. </exception>
		// Token: 0x06002523 RID: 9507 RVA: 0x000859A8 File Offset: 0x00083BA8
		[MonoLimitation("No support for PE32+ assemblies for AMD64 and IA64")]
		public void Save(string assemblyFileName, PortableExecutableKinds portableExecutableKind, ImageFileMachine imageFileMachine)
		{
			this.peKind = portableExecutableKind;
			this.machine = imageFileMachine;
			if ((this.peKind & PortableExecutableKinds.PE32Plus) != PortableExecutableKinds.NotAPortableExecutableImage || (this.peKind & PortableExecutableKinds.Unmanaged32Bit) != PortableExecutableKinds.NotAPortableExecutableImage)
			{
				throw new NotImplementedException(this.peKind.ToString());
			}
			if (this.machine == ImageFileMachine.IA64 || this.machine == ImageFileMachine.AMD64)
			{
				throw new NotImplementedException(this.machine.ToString());
			}
			if (this.resource_writers != null)
			{
				foreach (object obj in this.resource_writers)
				{
					IResourceWriter resourceWriter = (IResourceWriter)obj;
					resourceWriter.Generate();
					resourceWriter.Close();
				}
			}
			ModuleBuilder moduleBuilder = null;
			if (this.modules != null)
			{
				foreach (ModuleBuilder moduleBuilder2 in this.modules)
				{
					if (moduleBuilder2.FullyQualifiedName == assemblyFileName)
					{
						moduleBuilder = moduleBuilder2;
					}
				}
			}
			if (moduleBuilder == null)
			{
				moduleBuilder = this.DefineDynamicModule("RefEmit_OnDiskManifestModule", assemblyFileName);
			}
			if (!this.is_module_only)
			{
				moduleBuilder.IsMain = true;
			}
			if (this.entry_point != null && this.entry_point.DeclaringType.Module != moduleBuilder)
			{
				Type[] array2;
				if (this.entry_point.GetParametersCount() == 1)
				{
					array2 = new Type[] { typeof(string) };
				}
				else
				{
					array2 = Type.EmptyTypes;
				}
				MethodBuilder methodBuilder = moduleBuilder.DefineGlobalMethod("__EntryPoint$", MethodAttributes.Static, this.entry_point.ReturnType, array2);
				ILGenerator ilgenerator = methodBuilder.GetILGenerator();
				if (array2.Length == 1)
				{
					ilgenerator.Emit(OpCodes.Ldarg_0);
				}
				ilgenerator.Emit(OpCodes.Tailcall);
				ilgenerator.Emit(OpCodes.Call, this.entry_point);
				ilgenerator.Emit(OpCodes.Ret);
				this.entry_point = methodBuilder;
			}
			if (this.version_res != null)
			{
				this.DefineVersionInfoResourceImpl(assemblyFileName);
			}
			if (this.sn != null)
			{
				this.public_key = this.sn.PublicKey;
			}
			foreach (ModuleBuilder moduleBuilder3 in this.modules)
			{
				if (moduleBuilder3 != moduleBuilder)
				{
					moduleBuilder3.Save();
				}
			}
			moduleBuilder.Save();
			if (this.sn != null && this.sn.CanSign)
			{
				this.sn.Sign(Path.Combine(this.AssemblyDir, assemblyFileName));
			}
			this.created = true;
		}

		/// <summary>Saves this dynamic assembly to disk.</summary>
		/// <param name="assemblyFileName">The file name of the assembly. </param>
		/// <exception cref="T:System.ArgumentException">The length of <paramref name="assemblyFileName" /> is 0.-or- There are two or more modules resource files in the assembly with the same name.-or- The target directory of the assembly is invalid.-or- <paramref name="assemblyFileName" /> is not a simple file name (for example, has a directory or drive component), or more than one unmanaged resource, including a version information resource, was defined in this assembly.-or- The CultureInfo string in <see cref="T:System.Reflection.AssemblyCultureAttribute" /> is not a valid string and <see cref="M:System.Reflection.Emit.AssemblyBuilder.DefineVersionInfoResource(System.String,System.String,System.String,System.String,System.String)" /> was called prior to calling this method. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyFileName" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">This assembly has been saved before.-or- This assembly has access Run<see cref="T:System.Reflection.Emit.AssemblyBuilderAccess" /></exception>
		/// <exception cref="T:System.IO.IOException">An output error occurs during the save. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> has not been called for any of the types in the modules of the assembly to be written to disk. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002524 RID: 9508 RVA: 0x00085C2C File Offset: 0x00083E2C
		public void Save(string assemblyFileName)
		{
			this.Save(assemblyFileName, PortableExecutableKinds.ILOnly, ImageFileMachine.I386);
		}

		/// <summary>Sets the entry point for this dynamic assembly, assuming that a console application is being built.</summary>
		/// <param name="entryMethod">A reference to the method that represents the entry point for this dynamic assembly. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="entryMethod" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="entryMethod" /> is not contained within this assembly. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x06002525 RID: 9509 RVA: 0x00085C3B File Offset: 0x00083E3B
		public void SetEntryPoint(MethodInfo entryMethod)
		{
			this.SetEntryPoint(entryMethod, PEFileKinds.ConsoleApplication);
		}

		/// <summary>Sets the entry point for this assembly and defines the type of the portable executable (PE file) being built.</summary>
		/// <param name="entryMethod">A reference to the method that represents the entry point for this dynamic assembly. </param>
		/// <param name="fileKind">The type of the assembly executable being built. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="entryMethod" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="entryMethod" /> is not contained within this assembly. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x06002526 RID: 9510 RVA: 0x00085C48 File Offset: 0x00083E48
		public void SetEntryPoint(MethodInfo entryMethod, PEFileKinds fileKind)
		{
			if (entryMethod == null)
			{
				throw new ArgumentNullException("entryMethod");
			}
			if (entryMethod.DeclaringType.Assembly != this)
			{
				throw new InvalidOperationException("Entry method is not defined in the same assembly.");
			}
			this.entry_point = entryMethod;
			this.pekind = fileKind;
		}

		/// <summary>Set a custom attribute on this assembly using a custom attribute builder.</summary>
		/// <param name="customBuilder">An instance of a helper class to define the custom attribute. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="con" /> is null. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x06002527 RID: 9511 RVA: 0x00085C98 File Offset: 0x00083E98
		public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
		{
			if (customBuilder == null)
			{
				throw new ArgumentNullException("customBuilder");
			}
			if (this.cattrs != null)
			{
				CustomAttributeBuilder[] array = new CustomAttributeBuilder[this.cattrs.Length + 1];
				this.cattrs.CopyTo(array, 0);
				array[this.cattrs.Length] = customBuilder;
				this.cattrs = array;
			}
			else
			{
				this.cattrs = new CustomAttributeBuilder[1];
				this.cattrs[0] = customBuilder;
			}
			if (customBuilder.Ctor != null && customBuilder.Ctor.DeclaringType == typeof(RuntimeCompatibilityAttribute))
			{
				AssemblyBuilder.UpdateNativeCustomAttributes(this);
			}
		}

		/// <summary>Set a custom attribute on this assembly using a specified custom attribute blob.</summary>
		/// <param name="con">The constructor for the custom attribute. </param>
		/// <param name="binaryAttribute">A byte blob representing the attributes. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="con" /> or <paramref name="binaryAttribute" /> is null. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="con" /> is not a RuntimeConstructorInfo.</exception>
		// Token: 0x06002528 RID: 9512 RVA: 0x00085D31 File Offset: 0x00083F31
		[ComVisible(true)]
		public void SetCustomAttribute(ConstructorInfo con, byte[] binaryAttribute)
		{
			if (con == null)
			{
				throw new ArgumentNullException("con");
			}
			if (binaryAttribute == null)
			{
				throw new ArgumentNullException("binaryAttribute");
			}
			this.SetCustomAttribute(new CustomAttributeBuilder(con, binaryAttribute));
		}

		// Token: 0x06002529 RID: 9513 RVA: 0x00085D62 File Offset: 0x00083F62
		private Exception not_supported()
		{
			return new NotSupportedException("The invoked member is not supported in a dynamic module.");
		}

		// Token: 0x0600252A RID: 9514 RVA: 0x00085D70 File Offset: 0x00083F70
		private void check_name_and_filename(string name, string fileName, bool fileNeedsToExists)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (fileName == null)
			{
				throw new ArgumentNullException("fileName");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException("Empty name is not legal.", "name");
			}
			if (fileName.Length == 0)
			{
				throw new ArgumentException("Empty file name is not legal.", "fileName");
			}
			if (Path.GetFileName(fileName) != fileName)
			{
				throw new ArgumentException("fileName '" + fileName + "' must not include a path.", "fileName");
			}
			string text = fileName;
			if (this.dir != null)
			{
				text = Path.Combine(this.dir, fileName);
			}
			if (fileNeedsToExists && !File.Exists(text))
			{
				throw new FileNotFoundException("Could not find file '" + fileName + "'");
			}
			if (this.resources != null)
			{
				for (int i = 0; i < this.resources.Length; i++)
				{
					if (this.resources[i].filename == text)
					{
						throw new ArgumentException("Duplicate file name '" + fileName + "'");
					}
					if (this.resources[i].name == name)
					{
						throw new ArgumentException("Duplicate name '" + name + "'");
					}
				}
			}
			if (this.modules != null)
			{
				for (int j = 0; j < this.modules.Length; j++)
				{
					if (!this.modules[j].IsTransient() && this.modules[j].FileName == fileName)
					{
						throw new ArgumentException("Duplicate file name '" + fileName + "'");
					}
					if (this.modules[j].Name == name)
					{
						throw new ArgumentException("Duplicate name '" + name + "'");
					}
				}
			}
		}

		// Token: 0x0600252B RID: 9515 RVA: 0x00085F24 File Offset: 0x00084124
		private string create_assembly_version(string version)
		{
			string[] array = version.Split(new char[] { '.' });
			int[] array2 = new int[4];
			if (array.Length < 0 || array.Length > 4)
			{
				throw new ArgumentException("The version specified '" + version + "' is invalid");
			}
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] == "*")
				{
					DateTime now = DateTime.Now;
					if (i == 2)
					{
						array2[2] = (now - new DateTime(2000, 1, 1)).Days;
						if (array.Length == 3)
						{
							array2[3] = (now.Second + now.Minute * 60 + now.Hour * 3600) / 2;
						}
					}
					else
					{
						if (i != 3)
						{
							throw new ArgumentException("The version specified '" + version + "' is invalid");
						}
						array2[3] = (now.Second + now.Minute * 60 + now.Hour * 3600) / 2;
					}
				}
				else
				{
					try
					{
						array2[i] = int.Parse(array[i]);
					}
					catch (FormatException)
					{
						throw new ArgumentException("The version specified '" + version + "' is invalid");
					}
				}
			}
			return string.Concat(new object[]
			{
				array2[0],
				".",
				array2[1],
				".",
				array2[2],
				".",
				array2[3]
			});
		}

		// Token: 0x0600252C RID: 9516 RVA: 0x000860B0 File Offset: 0x000842B0
		private string GetCultureString(string str)
		{
			if (!(str == "neutral"))
			{
				return str;
			}
			return string.Empty;
		}

		// Token: 0x0600252D RID: 9517 RVA: 0x000860C6 File Offset: 0x000842C6
		internal Type MakeGenericType(Type gtd, Type[] typeArguments)
		{
			return new TypeBuilderInstantiation(gtd, typeArguments);
		}

		/// <summary>Maps a set of names to a corresponding set of dispatch identifiers.</summary>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="rgszNames">Passed-in array of names to be mapped.</param>
		/// <param name="cNames">Count of the names to be mapped.</param>
		/// <param name="lcid">The locale context in which to interpret the names.</param>
		/// <param name="rgDispId">Caller-allocated array which receives the IDs corresponding to the names.</param>
		/// <exception cref="T:System.NotImplementedException">The method is called late-bound using the COM IDispatch interface.</exception>
		// Token: 0x0600252E RID: 9518 RVA: 0x0002126B File Offset: 0x0001F46B
		void _AssemblyBuilder.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the type information for an object, which can then be used to get the type information for an interface.</summary>
		/// <param name="iTInfo">The type information to return.</param>
		/// <param name="lcid">The locale identifier for the type information.</param>
		/// <param name="ppTInfo">Receives a pointer to the requested type information object.</param>
		/// <exception cref="T:System.NotImplementedException">The method is called late-bound using the COM IDispatch interface.</exception>
		// Token: 0x0600252F RID: 9519 RVA: 0x0002126B File Offset: 0x0001F46B
		void _AssemblyBuilder.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the number of type information interfaces that an object provides (either 0 or 1).</summary>
		/// <param name="pcTInfo">Points to a location that receives the number of type information interfaces provided by the object.</param>
		/// <exception cref="T:System.NotImplementedException">The method is called late-bound using the COM IDispatch interface.</exception>
		// Token: 0x06002530 RID: 9520 RVA: 0x0002126B File Offset: 0x0001F46B
		void _AssemblyBuilder.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Provides access to properties and methods exposed by an object.</summary>
		/// <param name="dispIdMember">Identifies the member.</param>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="lcid">The locale context in which to interpret arguments.</param>
		/// <param name="wFlags">Flags describing the context of the call.</param>
		/// <param name="pDispParams">Pointer to a structure containing an array of arguments, an array of argument DISPIDs for named arguments, and counts for the number of elements in the arrays.</param>
		/// <param name="pVarResult">Pointer to the location where the result is to be stored.</param>
		/// <param name="pExcepInfo">Pointer to a structure that contains exception information.</param>
		/// <param name="puArgErr">The index of the first argument that has an error.</param>
		/// <exception cref="T:System.NotImplementedException">The method is called late-bound using the COM IDispatch interface.</exception>
		// Token: 0x06002531 RID: 9521 RVA: 0x0002126B File Offset: 0x0001F46B
		void _AssemblyBuilder.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the specified type from the types that have been defined and created in the current <see cref="T:System.Reflection.Emit.AssemblyBuilder" />.</summary>
		/// <returns>The specified type, or null if the type is not found or has not been created yet. </returns>
		/// <param name="name">The name of the type to search for.</param>
		/// <param name="throwOnError">true to throw an exception if the type is not found; otherwise, false.</param>
		/// <param name="ignoreCase">true to ignore the case of the type name when searching; otherwise, false.</param>
		// Token: 0x06002532 RID: 9522 RVA: 0x000860D0 File Offset: 0x000842D0
		public override Type GetType(string name, bool throwOnError, bool ignoreCase)
		{
			if (name == null)
			{
				throw new ArgumentNullException(name);
			}
			if (name.Length == 0)
			{
				throw new ArgumentException("name", "Name cannot be empty");
			}
			Type type = base.InternalGetType(null, name, throwOnError, ignoreCase);
			if (!(type is TypeBuilder))
			{
				return type;
			}
			if (throwOnError)
			{
				throw new TypeLoadException(string.Format("Could not load type '{0}' from assembly '{1}'", name, this.name));
			}
			return null;
		}

		/// <summary>Gets the specified module in this assembly.</summary>
		/// <returns>The module being requested, or null if the module is not found.</returns>
		/// <param name="name">The name of the requested module.</param>
		// Token: 0x06002533 RID: 9523 RVA: 0x00086130 File Offset: 0x00084330
		public override Module GetModule(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException("Name can't be empty");
			}
			if (this.modules == null)
			{
				return null;
			}
			foreach (ModuleBuilder module in this.modules)
			{
				if (module.ScopeName == name)
				{
					return module;
				}
			}
			return null;
		}

		/// <summary>Gets all the modules that are part of this assembly, and optionally includes resource modules.</summary>
		/// <returns>The modules that are part of this assembly.</returns>
		/// <param name="getResourceModules">true to include resource modules; otherwise, false.</param>
		// Token: 0x06002534 RID: 9524 RVA: 0x00086194 File Offset: 0x00084394
		public override Module[] GetModules(bool getResourceModules)
		{
			Module[] modulesInternal = this.GetModulesInternal();
			if (!getResourceModules)
			{
				List<Module> list = new List<Module>(modulesInternal.Length);
				foreach (Module module in modulesInternal)
				{
					if (!module.IsResource())
					{
						list.Add(module);
					}
				}
				return list.ToArray();
			}
			return modulesInternal;
		}

		/// <summary>Gets the <see cref="T:System.Reflection.AssemblyName" /> that was specified when the current dynamic assembly was created, and sets the code base as specified.</summary>
		/// <returns>The name of the dynamic assembly.</returns>
		/// <param name="copiedName">true to set the code base to the location of the assembly after it is shadow-copied; false to set the code base to the original location.</param>
		// Token: 0x06002535 RID: 9525 RVA: 0x000861E4 File Offset: 0x000843E4
		public override AssemblyName GetName(bool copiedName)
		{
			AssemblyName assemblyName = AssemblyName.Create(this, false);
			if (this.sn != null)
			{
				assemblyName.SetPublicKey(this.sn.PublicKey);
				assemblyName.SetPublicKeyToken(this.sn.PublicKeyToken);
			}
			return assemblyName;
		}

		/// <summary>Gets an incomplete list of <see cref="T:System.Reflection.AssemblyName" /> objects for the assemblies that are referenced by this <see cref="T:System.Reflection.Emit.AssemblyBuilder" />. </summary>
		/// <returns>An array of assembly names for the referenced assemblies. This array is not a complete list.</returns>
		// Token: 0x06002536 RID: 9526 RVA: 0x00082209 File Offset: 0x00080409
		[MonoTODO("This always returns an empty array")]
		public override AssemblyName[] GetReferencedAssemblies()
		{
			return Assembly.GetReferencedAssemblies(this);
		}

		/// <summary>Returns all the loaded modules that are part of this assembly, and optionally includes resource modules. </summary>
		/// <returns>The loaded modules that are part of this assembly.</returns>
		/// <param name="getResourceModules">true to include resource modules; otherwise, false.</param>
		// Token: 0x06002537 RID: 9527 RVA: 0x00082262 File Offset: 0x00080462
		public override Module[] GetLoadedModules(bool getResourceModules)
		{
			return this.GetModules(getResourceModules);
		}

		/// <summary>Gets the satellite assembly for the specified culture.</summary>
		/// <returns>The specified satellite assembly.</returns>
		/// <param name="culture">The specified culture. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="culture" /> is null. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The assembly cannot be found. </exception>
		/// <exception cref="T:System.IO.FileLoadException">The satellite assembly with a matching file name was found, but the CultureInfo did not match the one specified. </exception>
		/// <exception cref="T:System.BadImageFormatException">The satellite assembly is not a valid assembly. </exception>
		// Token: 0x06002538 RID: 9528 RVA: 0x0008226B File Offset: 0x0008046B
		public override Assembly GetSatelliteAssembly(CultureInfo culture)
		{
			return base.GetSatelliteAssembly(culture, null, true);
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
		// Token: 0x06002539 RID: 9529 RVA: 0x00082276 File Offset: 0x00080476
		public override Assembly GetSatelliteAssembly(CultureInfo culture, Version version)
		{
			return base.GetSatelliteAssembly(culture, version, true);
		}

		/// <summary>Gets the module in the current <see cref="T:System.Reflection.Emit.AssemblyBuilder" /> that contains the assembly manifest.</summary>
		/// <returns>The manifest module.</returns>
		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x0600253A RID: 9530 RVA: 0x00082281 File Offset: 0x00080481
		public override Module ManifestModule
		{
			get
			{
				return this.GetManifestModule();
			}
		}

		/// <summary>Gets a value that indicates whether the assembly was loaded from the global assembly cache.</summary>
		/// <returns>Always false.</returns>
		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x0600253B RID: 9531 RVA: 0x00015ED5 File Offset: 0x000140D5
		public override bool GlobalAssemblyCache
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value that indicates that the current assembly is a dynamic assembly.</summary>
		/// <returns>Always true.</returns>
		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x0600253C RID: 9532 RVA: 0x00003B29 File Offset: 0x00001D29
		public override bool IsDynamic
		{
			get
			{
				return true;
			}
		}

		/// <summary>Returns a value that indicates whether this instance is equal to the specified object.</summary>
		/// <returns>true if <paramref name="obj" /> equals the type and value of this instance; otherwise, false.</returns>
		/// <param name="obj">An object to compare with this instance, or null.</param>
		// Token: 0x0600253D RID: 9533 RVA: 0x00086224 File Offset: 0x00084424
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x0600253E RID: 9534 RVA: 0x0008622D File Offset: 0x0008442D
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Returns a value that indicates whether one or more instances of the specified attribute type is applied to this member.</summary>
		/// <returns>true if one or more instances of <paramref name="attributeType" /> is applied to this dynamic assembly; otherwise, false.</returns>
		/// <param name="attributeType">The type of attribute to test for.</param>
		/// <param name="inherit">This argument is ignored for objects of this type.</param>
		// Token: 0x0600253F RID: 9535 RVA: 0x00086235 File Offset: 0x00084435
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return base.IsDefined(attributeType, inherit);
		}

		/// <summary>Returns all the custom attributes that have been applied to the current <see cref="T:System.Reflection.Emit.AssemblyBuilder" />.</summary>
		/// <returns>An array that contains the custom attributes; the array is empty if there are no attributes.</returns>
		/// <param name="inherit">This argument is ignored for objects of this type.</param>
		// Token: 0x06002540 RID: 9536 RVA: 0x0008623F File Offset: 0x0008443F
		public override object[] GetCustomAttributes(bool inherit)
		{
			return base.GetCustomAttributes(inherit);
		}

		/// <summary>Returns all the custom attributes that have been applied to the current <see cref="T:System.Reflection.Emit.AssemblyBuilder" />, and that derive from a specified attribute type.</summary>
		/// <returns>An array that contains the custom attributes that are derived at any level from <paramref name="attributeType" />; the array is empty if there are no such attributes.</returns>
		/// <param name="attributeType">The base type from which attributes derive.</param>
		/// <param name="inherit">This argument is ignored for objects of this type.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="attributeType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="attributeType" /> is not a <see cref="T:System.Type" /> object supplied by the runtime. For example, <paramref name="attributeType" /> is a <see cref="T:System.Reflection.Emit.TypeBuilder" /> object.</exception>
		// Token: 0x06002541 RID: 9537 RVA: 0x00086248 File Offset: 0x00084448
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return base.GetCustomAttributes(attributeType, inherit);
		}

		/// <summary>Gets the display name of the current dynamic assembly. </summary>
		/// <returns>The display name of the dynamic assembly.</returns>
		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x06002542 RID: 9538 RVA: 0x00086252 File Offset: 0x00084452
		public override string FullName
		{
			get
			{
				return base.FullName;
			}
		}

		// Token: 0x06002543 RID: 9539 RVA: 0x0001FB35 File Offset: 0x0001DD35
		internal AssemblyBuilder()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001397 RID: 5015
		private UIntPtr dynamic_assembly;

		// Token: 0x04001398 RID: 5016
		private MethodInfo entry_point;

		// Token: 0x04001399 RID: 5017
		private ModuleBuilder[] modules;

		// Token: 0x0400139A RID: 5018
		private string name;

		// Token: 0x0400139B RID: 5019
		private string dir;

		// Token: 0x0400139C RID: 5020
		private CustomAttributeBuilder[] cattrs;

		// Token: 0x0400139D RID: 5021
		private MonoResource[] resources;

		// Token: 0x0400139E RID: 5022
		private byte[] public_key;

		// Token: 0x0400139F RID: 5023
		private string version;

		// Token: 0x040013A0 RID: 5024
		private string culture;

		// Token: 0x040013A1 RID: 5025
		private uint algid;

		// Token: 0x040013A2 RID: 5026
		private uint flags;

		// Token: 0x040013A3 RID: 5027
		private PEFileKinds pekind;

		// Token: 0x040013A4 RID: 5028
		private bool delay_sign;

		// Token: 0x040013A5 RID: 5029
		private uint access;

		// Token: 0x040013A6 RID: 5030
		private Module[] loaded_modules;

		// Token: 0x040013A7 RID: 5031
		private MonoWin32Resource[] win32_resources;

		// Token: 0x040013A8 RID: 5032
		private RefEmitPermissionSet[] permissions_minimum;

		// Token: 0x040013A9 RID: 5033
		private RefEmitPermissionSet[] permissions_optional;

		// Token: 0x040013AA RID: 5034
		private RefEmitPermissionSet[] permissions_refused;

		// Token: 0x040013AB RID: 5035
		private PortableExecutableKinds peKind;

		// Token: 0x040013AC RID: 5036
		private ImageFileMachine machine;

		// Token: 0x040013AD RID: 5037
		private bool corlib_internal;

		// Token: 0x040013AE RID: 5038
		private Type[] type_forwarders;

		// Token: 0x040013AF RID: 5039
		private byte[] pktoken;

		// Token: 0x040013B0 RID: 5040
		internal Type corlib_object_type;

		// Token: 0x040013B1 RID: 5041
		internal Type corlib_value_type;

		// Token: 0x040013B2 RID: 5042
		internal Type corlib_enum_type;

		// Token: 0x040013B3 RID: 5043
		internal Type corlib_void_type;

		// Token: 0x040013B4 RID: 5044
		private ArrayList resource_writers;

		// Token: 0x040013B5 RID: 5045
		private Win32VersionResource version_res;

		// Token: 0x040013B6 RID: 5046
		private bool created;

		// Token: 0x040013B7 RID: 5047
		private bool is_module_only;

		// Token: 0x040013B8 RID: 5048
		private StrongName sn;

		// Token: 0x040013B9 RID: 5049
		private NativeResourceType native_resource;

		// Token: 0x040013BA RID: 5050
		private string versioninfo_culture;

		// Token: 0x040013BB RID: 5051
		private const AssemblyBuilderAccess COMPILER_ACCESS = (AssemblyBuilderAccess)2048;

		// Token: 0x040013BC RID: 5052
		private ModuleBuilder manifest_module;
	}
}
