using System;
using System.Collections;
using System.Diagnostics;
using System.Diagnostics.SymbolStore;
using System.Reflection;
using System.Reflection.Emit;
using System.Security;
using System.Security.Permissions;
using System.Threading;
using System.Xml.Xsl.Runtime;

namespace System.Xml.Xsl.IlGen
{
	// Token: 0x0200066D RID: 1645
	internal class XmlILModule
	{
		// Token: 0x06004227 RID: 16935 RVA: 0x00160FD0 File Offset: 0x0015F1D0
		static XmlILModule()
		{
			XmlILModule.CreateModulePermissionSet.AddPermission(new ReflectionPermission(ReflectionPermissionFlag.MemberAccess));
			XmlILModule.CreateModulePermissionSet.AddPermission(new SecurityPermission(SecurityPermissionFlag.UnmanagedCode | SecurityPermissionFlag.ControlEvidence));
			XmlILModule.AssemblyId = 0L;
			AssemblyName assemblyName = XmlILModule.CreateAssemblyName();
			AssemblyBuilder assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
			try
			{
				XmlILModule.CreateModulePermissionSet.Assert();
				assemblyBuilder.SetCustomAttribute(new CustomAttributeBuilder(XmlILConstructors.Transparent, new object[0]));
				XmlILModule.LREModule = assemblyBuilder.DefineDynamicModule("System.Xml.Xsl.CompiledQuery", false);
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
			}
		}

		// Token: 0x06004228 RID: 16936 RVA: 0x001610D4 File Offset: 0x0015F2D4
		public XmlILModule(TypeBuilder typeBldr)
		{
			this.typeBldr = typeBldr;
			this.emitSymbols = ((ModuleBuilder)this.typeBldr.Module).GetSymWriter() != null;
			this.useLRE = false;
			this.persistAsm = false;
			this.methods = new Hashtable();
			if (this.emitSymbols)
			{
				this.urlToSymWriter = new Hashtable();
			}
		}

		// Token: 0x17000CEF RID: 3311
		// (get) Token: 0x06004229 RID: 16937 RVA: 0x00161138 File Offset: 0x0015F338
		public bool EmitSymbols
		{
			get
			{
				return this.emitSymbols;
			}
		}

		// Token: 0x0600422A RID: 16938 RVA: 0x00161140 File Offset: 0x0015F340
		public XmlILModule(bool useLRE, bool emitSymbols)
		{
			this.useLRE = useLRE;
			this.emitSymbols = emitSymbols;
			this.persistAsm = false;
			this.methods = new Hashtable();
			if (!useLRE)
			{
				AssemblyName assemblyName = XmlILModule.CreateAssemblyName();
				AssemblyBuilder assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, this.persistAsm ? AssemblyBuilderAccess.RunAndSave : AssemblyBuilderAccess.Run);
				assemblyBuilder.SetCustomAttribute(new CustomAttributeBuilder(XmlILConstructors.Transparent, new object[0]));
				if (emitSymbols)
				{
					this.urlToSymWriter = new Hashtable();
					DebuggableAttribute.DebuggingModes debuggingModes = DebuggableAttribute.DebuggingModes.Default | DebuggableAttribute.DebuggingModes.DisableOptimizations | DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints;
					assemblyBuilder.SetCustomAttribute(new CustomAttributeBuilder(XmlILConstructors.Debuggable, new object[] { debuggingModes }));
				}
				ModuleBuilder moduleBuilder;
				if (this.persistAsm)
				{
					moduleBuilder = assemblyBuilder.DefineDynamicModule("System.Xml.Xsl.CompiledQuery", this.modFile + ".dll", emitSymbols);
				}
				else
				{
					moduleBuilder = assemblyBuilder.DefineDynamicModule("System.Xml.Xsl.CompiledQuery", emitSymbols);
				}
				this.typeBldr = moduleBuilder.DefineType("System.Xml.Xsl.CompiledQuery.Query", TypeAttributes.Public);
			}
		}

		// Token: 0x0600422B RID: 16939 RVA: 0x00161228 File Offset: 0x0015F428
		public MethodInfo DefineMethod(string name, Type returnType, Type[] paramTypes, string[] paramNames, XmlILMethodAttributes xmlAttrs)
		{
			int num = 1;
			string text = name;
			bool flag = (xmlAttrs & XmlILMethodAttributes.Raw) > XmlILMethodAttributes.None;
			while (this.methods[name] != null)
			{
				num++;
				name = string.Concat(new object[] { text, " (", num, ")" });
			}
			if (!flag)
			{
				Type[] array = new Type[paramTypes.Length + 1];
				array[0] = typeof(XmlQueryRuntime);
				Array.Copy(paramTypes, 0, array, 1, paramTypes.Length);
				paramTypes = array;
			}
			MethodInfo methodInfo;
			if (!this.useLRE)
			{
				MethodBuilder methodBuilder = this.typeBldr.DefineMethod(name, MethodAttributes.Private | MethodAttributes.Static, returnType, paramTypes);
				if (this.emitSymbols && (xmlAttrs & XmlILMethodAttributes.NonUser) != XmlILMethodAttributes.None)
				{
					methodBuilder.SetCustomAttribute(new CustomAttributeBuilder(XmlILConstructors.StepThrough, new object[0]));
					methodBuilder.SetCustomAttribute(new CustomAttributeBuilder(XmlILConstructors.NonUserCode, new object[0]));
				}
				if (!flag)
				{
					methodBuilder.DefineParameter(1, ParameterAttributes.None, "{urn:schemas-microsoft-com:xslt-debug}runtime");
				}
				for (int i = 0; i < paramNames.Length; i++)
				{
					if (paramNames[i] != null && paramNames[i].Length != 0)
					{
						methodBuilder.DefineParameter(i + (flag ? 1 : 2), ParameterAttributes.None, paramNames[i]);
					}
				}
				methodInfo = methodBuilder;
			}
			else
			{
				DynamicMethod dynamicMethod = new DynamicMethod(name, returnType, paramTypes, XmlILModule.LREModule);
				dynamicMethod.InitLocals = true;
				if (!flag)
				{
					dynamicMethod.DefineParameter(1, ParameterAttributes.None, "{urn:schemas-microsoft-com:xslt-debug}runtime");
				}
				for (int j = 0; j < paramNames.Length; j++)
				{
					if (paramNames[j] != null && paramNames[j].Length != 0)
					{
						dynamicMethod.DefineParameter(j + (flag ? 1 : 2), ParameterAttributes.None, paramNames[j]);
					}
				}
				methodInfo = dynamicMethod;
			}
			this.methods[name] = methodInfo;
			return methodInfo;
		}

		// Token: 0x0600422C RID: 16940 RVA: 0x001613D8 File Offset: 0x0015F5D8
		public static ILGenerator DefineMethodBody(MethodBase methInfo)
		{
			DynamicMethod dynamicMethod = methInfo as DynamicMethod;
			if (dynamicMethod != null)
			{
				return dynamicMethod.GetILGenerator();
			}
			MethodBuilder methodBuilder = methInfo as MethodBuilder;
			if (methodBuilder != null)
			{
				return methodBuilder.GetILGenerator();
			}
			return ((ConstructorBuilder)methInfo).GetILGenerator();
		}

		// Token: 0x0600422D RID: 16941 RVA: 0x0016141E File Offset: 0x0015F61E
		public MethodInfo FindMethod(string name)
		{
			return (MethodInfo)this.methods[name];
		}

		// Token: 0x0600422E RID: 16942 RVA: 0x00161431 File Offset: 0x0015F631
		public FieldInfo DefineInitializedData(string name, byte[] data)
		{
			return this.typeBldr.DefineInitializedData(name, data, FieldAttributes.Private | FieldAttributes.Static);
		}

		// Token: 0x0600422F RID: 16943 RVA: 0x00161442 File Offset: 0x0015F642
		public FieldInfo DefineField(string fieldName, Type type)
		{
			return this.typeBldr.DefineField(fieldName, type, FieldAttributes.Private | FieldAttributes.Static);
		}

		// Token: 0x06004230 RID: 16944 RVA: 0x00161453 File Offset: 0x0015F653
		public ConstructorInfo DefineTypeInitializer()
		{
			return this.typeBldr.DefineTypeInitializer();
		}

		// Token: 0x06004231 RID: 16945 RVA: 0x00161460 File Offset: 0x0015F660
		public ISymbolDocumentWriter AddSourceDocument(string fileName)
		{
			ISymbolDocumentWriter symbolDocumentWriter = this.urlToSymWriter[fileName] as ISymbolDocumentWriter;
			if (symbolDocumentWriter == null)
			{
				symbolDocumentWriter = ((ModuleBuilder)this.typeBldr.Module).DefineDocument(fileName, XmlILModule.LanguageGuid, XmlILModule.VendorGuid, Guid.Empty);
				this.urlToSymWriter.Add(fileName, symbolDocumentWriter);
			}
			return symbolDocumentWriter;
		}

		// Token: 0x06004232 RID: 16946 RVA: 0x001614B8 File Offset: 0x0015F6B8
		public void BakeMethods()
		{
			if (!this.useLRE)
			{
				Type type = this.typeBldr.CreateType();
				if (this.persistAsm)
				{
					((AssemblyBuilder)this.typeBldr.Module.Assembly).Save(this.modFile + ".dll");
				}
				Hashtable hashtable = new Hashtable(this.methods.Count);
				foreach (object obj in this.methods.Keys)
				{
					string text = (string)obj;
					hashtable[text] = type.GetMethod(text, BindingFlags.Static | BindingFlags.NonPublic);
				}
				this.methods = hashtable;
				this.typeBldr = null;
				this.urlToSymWriter = null;
			}
		}

		// Token: 0x06004233 RID: 16947 RVA: 0x00161594 File Offset: 0x0015F794
		public Delegate CreateDelegate(string name, Type typDelegate)
		{
			if (!this.useLRE)
			{
				return Delegate.CreateDelegate(typDelegate, (MethodInfo)this.methods[name]);
			}
			return ((DynamicMethod)this.methods[name]).CreateDelegate(typDelegate);
		}

		// Token: 0x06004234 RID: 16948 RVA: 0x001615CD File Offset: 0x0015F7CD
		private static AssemblyName CreateAssemblyName()
		{
			Interlocked.Increment(ref XmlILModule.AssemblyId);
			return new AssemblyName
			{
				Name = "System.Xml.Xsl.CompiledQuery." + XmlILModule.AssemblyId
			};
		}

		// Token: 0x04002A6F RID: 10863
		public static readonly PermissionSet CreateModulePermissionSet = new PermissionSet(PermissionState.None);

		// Token: 0x04002A70 RID: 10864
		private static long AssemblyId;

		// Token: 0x04002A71 RID: 10865
		private static ModuleBuilder LREModule;

		// Token: 0x04002A72 RID: 10866
		private TypeBuilder typeBldr;

		// Token: 0x04002A73 RID: 10867
		private Hashtable methods;

		// Token: 0x04002A74 RID: 10868
		private Hashtable urlToSymWriter;

		// Token: 0x04002A75 RID: 10869
		private string modFile;

		// Token: 0x04002A76 RID: 10870
		private bool persistAsm;

		// Token: 0x04002A77 RID: 10871
		private bool useLRE;

		// Token: 0x04002A78 RID: 10872
		private bool emitSymbols;

		// Token: 0x04002A79 RID: 10873
		private static readonly Guid LanguageGuid = new Guid(1177373246U, 45655, 19182, 151, 205, 89, 24, 199, 83, 23, 88);

		// Token: 0x04002A7A RID: 10874
		private static readonly Guid VendorGuid = new Guid(2571847108U, 59113, 4562, 144, 63, 0, 192, 79, 163, 2, 161);

		// Token: 0x04002A7B RID: 10875
		private const string RuntimeName = "{urn:schemas-microsoft-com:xslt-debug}runtime";
	}
}
