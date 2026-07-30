using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;
using System.Security.Principal;
using System.Threading;
using Microsoft.CSharp;

namespace System.Xml.Serialization
{
	// Token: 0x020002DA RID: 730
	internal class Compiler
	{
		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x06001B65 RID: 7013 RVA: 0x000986DC File Offset: 0x000968DC
		protected string[] Imports
		{
			get
			{
				string[] array = new string[this.imports.Values.Count];
				this.imports.Values.CopyTo(array, 0);
				return array;
			}
		}

		// Token: 0x06001B66 RID: 7014 RVA: 0x00098714 File Offset: 0x00096914
		internal void AddImport(Type type, Hashtable types)
		{
			if (type == null)
			{
				return;
			}
			if (TypeScope.IsKnownType(type))
			{
				return;
			}
			if (types[type] != null)
			{
				return;
			}
			types[type] = type;
			Type baseType = type.BaseType;
			if (baseType != null)
			{
				this.AddImport(baseType, types);
			}
			Type declaringType = type.DeclaringType;
			if (declaringType != null)
			{
				this.AddImport(declaringType, types);
			}
			foreach (Type type2 in type.GetInterfaces())
			{
				this.AddImport(type2, types);
			}
			ConstructorInfo[] constructors = type.GetConstructors();
			for (int j = 0; j < constructors.Length; j++)
			{
				ParameterInfo[] parameters = constructors[j].GetParameters();
				for (int k = 0; k < parameters.Length; k++)
				{
					this.AddImport(parameters[k].ParameterType, types);
				}
			}
			if (type.IsGenericType)
			{
				Type[] genericArguments = type.GetGenericArguments();
				for (int l = 0; l < genericArguments.Length; l++)
				{
					this.AddImport(genericArguments[l], types);
				}
			}
			TempAssembly.FileIOPermission.Assert();
			Assembly assembly = type.Module.Assembly;
			if (DynamicAssemblies.IsTypeDynamic(type))
			{
				DynamicAssemblies.Add(assembly);
				return;
			}
			object[] customAttributes = type.GetCustomAttributes(typeof(TypeForwardedFromAttribute), false);
			if (customAttributes.Length != 0)
			{
				Assembly assembly2 = Assembly.Load((customAttributes[0] as TypeForwardedFromAttribute).AssemblyFullName);
				this.imports[assembly2] = assembly2.Location;
			}
			this.imports[assembly] = assembly.Location;
		}

		// Token: 0x06001B67 RID: 7015 RVA: 0x00098890 File Offset: 0x00096A90
		internal void AddImport(Assembly assembly)
		{
			TempAssembly.FileIOPermission.Assert();
			this.imports[assembly] = assembly.Location;
		}

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x06001B68 RID: 7016 RVA: 0x000988AE File Offset: 0x00096AAE
		internal TextWriter Source
		{
			get
			{
				return this.writer;
			}
		}

		// Token: 0x06001B69 RID: 7017 RVA: 0x00002F50 File Offset: 0x00001150
		internal void Close()
		{
		}

		// Token: 0x06001B6A RID: 7018 RVA: 0x000988B8 File Offset: 0x00096AB8
		internal static string GetTempAssemblyPath(string baseDir, Assembly assembly, string defaultNamespace)
		{
			if (assembly.IsDynamic)
			{
				throw new InvalidOperationException(Res.GetString("Cannot pre-generate serialization assembly. Pre-generation of serialization assemblies is not supported for dynamic assemblies. Save the assembly and load it from disk to use it with XmlSerialization."));
			}
			PermissionSet permissionSet = new PermissionSet(PermissionState.None);
			permissionSet.AddPermission(new FileIOPermission(PermissionState.Unrestricted));
			permissionSet.AddPermission(new EnvironmentPermission(PermissionState.Unrestricted));
			permissionSet.Assert();
			try
			{
				if (baseDir != null && baseDir.Length > 0)
				{
					if (!Directory.Exists(baseDir))
					{
						throw new UnauthorizedAccessException(Res.GetString("Could not find directory to save XmlSerializer generated assembly: {0}.", new object[] { baseDir }));
					}
				}
				else
				{
					baseDir = Path.GetTempPath();
					if (!Directory.Exists(baseDir))
					{
						throw new UnauthorizedAccessException(Res.GetString("Could not find TEMP directory to save XmlSerializer generated assemblies."));
					}
				}
				baseDir = Path.Combine(baseDir, Compiler.GetTempAssemblyName(assembly.GetName(), defaultNamespace));
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
			}
			return baseDir + ".dll";
		}

		// Token: 0x06001B6B RID: 7019 RVA: 0x00098988 File Offset: 0x00096B88
		internal static string GetTempAssemblyName(AssemblyName parent, string ns)
		{
			return parent.Name + ".XmlSerializers" + ((ns == null || ns.Length == 0) ? "" : ("." + ns.GetHashCode()));
		}

		// Token: 0x06001B6C RID: 7020 RVA: 0x000989C4 File Offset: 0x00096BC4
		internal Assembly Compile(Assembly parent, string ns, XmlSerializerCompilerParameters xmlParameters, Evidence evidence)
		{
			CodeDomProvider codeDomProvider = new CSharpCodeProvider();
			CompilerParameters codeDomParameters = xmlParameters.CodeDomParameters;
			codeDomParameters.ReferencedAssemblies.AddRange(this.Imports);
			if (this.debugEnabled)
			{
				codeDomParameters.GenerateInMemory = false;
				codeDomParameters.IncludeDebugInformation = true;
				codeDomParameters.TempFiles.KeepFiles = true;
			}
			PermissionSet permissionSet = new PermissionSet(PermissionState.None);
			if (xmlParameters.IsNeedTempDirAccess)
			{
				permissionSet.AddPermission(TempAssembly.FileIOPermission);
			}
			permissionSet.AddPermission(new EnvironmentPermission(PermissionState.Unrestricted));
			permissionSet.AddPermission(new SecurityPermission(SecurityPermissionFlag.UnmanagedCode));
			permissionSet.AddPermission(new SecurityPermission(SecurityPermissionFlag.ControlEvidence));
			permissionSet.Assert();
			if (parent != null && (codeDomParameters.OutputAssembly == null || codeDomParameters.OutputAssembly.Length == 0))
			{
				string text = Compiler.AssemblyNameFromOptions(codeDomParameters.CompilerOptions);
				if (text == null)
				{
					text = Compiler.GetTempAssemblyPath(codeDomParameters.TempFiles.TempDir, parent, ns);
				}
				codeDomParameters.OutputAssembly = text;
			}
			if (codeDomParameters.CompilerOptions == null || codeDomParameters.CompilerOptions.Length == 0)
			{
				codeDomParameters.CompilerOptions = "/nostdlib";
			}
			else
			{
				CompilerParameters compilerParameters = codeDomParameters;
				compilerParameters.CompilerOptions += " /nostdlib";
			}
			CompilerParameters compilerParameters2 = codeDomParameters;
			compilerParameters2.CompilerOptions += " /D:_DYNAMIC_XMLSERIALIZER_COMPILATION";
			codeDomParameters.Evidence = evidence;
			CompilerResults compilerResults = null;
			Assembly assembly = null;
			try
			{
				compilerResults = codeDomProvider.CompileAssemblyFromSource(codeDomParameters, new string[] { this.writer.ToString() });
				if (compilerResults.Errors.Count > 0)
				{
					StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
					stringWriter.WriteLine(Res.GetString("Unable to generate a temporary class (result={0}).", new object[] { compilerResults.NativeCompilerReturnValue.ToString(CultureInfo.InvariantCulture) }));
					bool flag = false;
					foreach (object obj in compilerResults.Errors)
					{
						CompilerError compilerError = (CompilerError)obj;
						compilerError.FileName = "";
						if (!compilerError.IsWarning || compilerError.ErrorNumber == "CS1595")
						{
							flag = true;
							stringWriter.WriteLine(compilerError.ToString());
						}
					}
					if (flag)
					{
						throw new InvalidOperationException(stringWriter.ToString());
					}
				}
				assembly = compilerResults.CompiledAssembly;
			}
			catch (UnauthorizedAccessException)
			{
				string currentUser = Compiler.GetCurrentUser();
				if (currentUser == null || currentUser.Length == 0)
				{
					throw new UnauthorizedAccessException(Res.GetString("Access to the temp directory is denied.  The process under which XmlSerializer is running does not have sufficient permission to access the temp directory.  CodeDom will use the user account the process is using to do the compilation, so if the user doesn\ufffdt have access to system temp directory, you will not be able to compile.  Use Path.GetTempPath() API to find out the temp directory location."));
				}
				throw new UnauthorizedAccessException(Res.GetString("Access to the temp directory is denied.  Identity '{0}' under which XmlSerializer is running does not have sufficient permission to access the temp directory.  CodeDom will use the user account the process is using to do the compilation, so if the user doesn\ufffdt have access to system temp directory, you will not be able to compile.  Use Path.GetTempPath() API to find out the temp directory location.", new object[] { currentUser }));
			}
			catch (FileLoadException ex)
			{
				throw new InvalidOperationException(Res.GetString("Cannot load dynamically generated serialization assembly. In some hosting environments assembly load functionality is restricted, consider using pre-generated serializer. Please see inner exception for more information."), ex);
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
			}
			if (assembly == null)
			{
				throw new InvalidOperationException(Res.GetString("Internal error."));
			}
			return assembly;
		}

		// Token: 0x06001B6D RID: 7021 RVA: 0x00098CD8 File Offset: 0x00096ED8
		private static string AssemblyNameFromOptions(string options)
		{
			if (options == null || options.Length == 0)
			{
				return null;
			}
			string text = null;
			string[] array = options.ToLower(CultureInfo.InvariantCulture).Split(null);
			for (int i = 0; i < array.Length; i++)
			{
				string text2 = array[i].Trim();
				if (text2.StartsWith("/out:", StringComparison.Ordinal))
				{
					text = text2.Substring(5);
				}
			}
			return text;
		}

		// Token: 0x06001B6E RID: 7022 RVA: 0x00098D34 File Offset: 0x00096F34
		internal static string GetCurrentUser()
		{
			try
			{
				WindowsIdentity current = WindowsIdentity.GetCurrent();
				if (current != null && current.Name != null)
				{
					return current.Name;
				}
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
			}
			return "";
		}

		// Token: 0x040015E6 RID: 5606
		private bool debugEnabled = DiagnosticsSwitches.KeepTempFiles.Enabled;

		// Token: 0x040015E7 RID: 5607
		private Hashtable imports = new Hashtable();

		// Token: 0x040015E8 RID: 5608
		private StringWriter writer = new StringWriter(CultureInfo.InvariantCulture);
	}
}
