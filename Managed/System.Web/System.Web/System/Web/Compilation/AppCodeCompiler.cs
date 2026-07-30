using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Web.Configuration;
using System.Web.Profile;

namespace System.Web.Compilation
{
	// Token: 0x0200060E RID: 1550
	internal class AppCodeCompiler
	{
		// Token: 0x060042CA RID: 17098 RVA: 0x000B04AF File Offset: 0x000AE6AF
		public AppCodeCompiler()
		{
			this.assemblies = new List<AppCodeAssembly>();
		}

		// Token: 0x060042CB RID: 17099 RVA: 0x000B04C4 File Offset: 0x000AE6C4
		private bool ProcessAppCodeDir(string appCode, AppCodeAssembly defasm)
		{
			CompilationSection compilationSection = (CompilationSection)WebConfigurationManager.GetWebApplicationSection("system.web/compilation");
			if (compilationSection != null)
			{
				for (int i = 0; i < compilationSection.CodeSubDirectories.Count; i++)
				{
					string text = "App_SubCode_" + compilationSection.CodeSubDirectories[i].DirectoryName;
					this.assemblies.Add(new AppCodeAssembly(text, Path.Combine(appCode, compilationSection.CodeSubDirectories[i].DirectoryName)));
				}
			}
			return this.CollectFiles(appCode, defasm);
		}

		// Token: 0x060042CC RID: 17100 RVA: 0x000B0546 File Offset: 0x000AE746
		private CodeTypeReference GetProfilePropertyType(string type)
		{
			if (string.IsNullOrEmpty(type))
			{
				throw new ArgumentException("String size cannot be 0", "type");
			}
			return new CodeTypeReference(type);
		}

		// Token: 0x060042CD RID: 17101 RVA: 0x000B0568 File Offset: 0x000AE768
		private string FindProviderTypeName(ProfileSection ps, string providerName)
		{
			if (ps.Providers == null || ps.Providers.Count == 0)
			{
				return null;
			}
			ProviderSettings providerSettings = ps.Providers[providerName];
			if (providerSettings == null)
			{
				return null;
			}
			return providerSettings.Type;
		}

		// Token: 0x060042CE RID: 17102 RVA: 0x000B05A4 File Offset: 0x000AE7A4
		private void GetProfileProviderAttribute(ProfileSection ps, CodeAttributeDeclarationCollection collection, string providerName)
		{
			if (string.IsNullOrEmpty(providerName))
			{
				this.providerTypeName = this.FindProviderTypeName(ps, ps.DefaultProvider);
			}
			else
			{
				this.providerTypeName = this.FindProviderTypeName(ps, providerName);
			}
			if (this.providerTypeName == null)
			{
				throw new HttpException(string.Format("Profile provider type not defined: {0}", providerName));
			}
			collection.Add(new CodeAttributeDeclaration("ProfileProvider", new CodeAttributeArgument[]
			{
				new CodeAttributeArgument(new CodePrimitiveExpression(this.providerTypeName))
			}));
		}

		// Token: 0x060042CF RID: 17103 RVA: 0x000B0620 File Offset: 0x000AE820
		private void GetProfileSettingsSerializeAsAttribute(ProfileSection ps, CodeAttributeDeclarationCollection collection, SerializationMode mode)
		{
			string text = "SettingsSerializeAs." + mode.ToString();
			collection.Add(new CodeAttributeDeclaration("SettingsSerializeAs", new CodeAttributeArgument[]
			{
				new CodeAttributeArgument(new CodeSnippetExpression(text))
			}));
		}

		// Token: 0x060042D0 RID: 17104 RVA: 0x000B066C File Offset: 0x000AE86C
		private void AddProfileClassGetProfileMethod(CodeTypeDeclaration profileClass)
		{
			CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(new CodeTypeReferenceExpression(typeof(ProfileBase)), "Create"), new CodeExpression[]
			{
				new CodeVariableReferenceExpression("username")
			});
			CodeCastExpression codeCastExpression = new CodeCastExpression();
			codeCastExpression.TargetType = new CodeTypeReference("ProfileCommon");
			codeCastExpression.Expression = codeMethodInvokeExpression;
			CodeMethodReturnStatement codeMethodReturnStatement = new CodeMethodReturnStatement();
			codeMethodReturnStatement.Expression = codeCastExpression;
			CodeMemberMethod codeMemberMethod = new CodeMemberMethod();
			codeMemberMethod.Name = "GetProfile";
			codeMemberMethod.ReturnType = new CodeTypeReference("ProfileCommon");
			codeMemberMethod.Parameters.Add(new CodeParameterDeclarationExpression("System.String", "username"));
			codeMemberMethod.Statements.Add(codeMethodReturnStatement);
			codeMemberMethod.Attributes = MemberAttributes.Public;
			profileClass.Members.Add(codeMemberMethod);
		}

		// Token: 0x060042D1 RID: 17105 RVA: 0x000B0738 File Offset: 0x000AE938
		private void AddProfileClassProperty(ProfileSection ps, CodeTypeDeclaration profileClass, ProfilePropertySettings pset)
		{
			string name = pset.Name;
			if (string.IsNullOrEmpty(name))
			{
				throw new HttpException("Profile property 'Name' attribute cannot be null.");
			}
			CodeMemberProperty codeMemberProperty = new CodeMemberProperty();
			string text = pset.Type;
			if (text == "string")
			{
				text = "System.String";
			}
			codeMemberProperty.Name = name;
			codeMemberProperty.Type = this.GetProfilePropertyType(text);
			codeMemberProperty.Attributes = MemberAttributes.Public;
			CodeAttributeDeclarationCollection codeAttributeDeclarationCollection = new CodeAttributeDeclarationCollection();
			this.GetProfileProviderAttribute(ps, codeAttributeDeclarationCollection, pset.Provider);
			this.GetProfileSettingsSerializeAsAttribute(ps, codeAttributeDeclarationCollection, pset.SerializeAs);
			codeMemberProperty.CustomAttributes = codeAttributeDeclarationCollection;
			CodeMethodReturnStatement codeMethodReturnStatement = new CodeMethodReturnStatement();
			CodeCastExpression codeCastExpression = new CodeCastExpression();
			codeMethodReturnStatement.Expression = codeCastExpression;
			CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(new CodeThisReferenceExpression(), "GetPropertyValue"), new CodeExpression[]
			{
				new CodePrimitiveExpression(name)
			});
			codeCastExpression.TargetType = new CodeTypeReference(text);
			codeCastExpression.Expression = codeMethodInvokeExpression;
			codeMemberProperty.GetStatements.Add(codeMethodReturnStatement);
			if (!pset.ReadOnly)
			{
				codeMethodInvokeExpression = new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(new CodeThisReferenceExpression(), "SetPropertyValue"), new CodeExpression[]
				{
					new CodePrimitiveExpression(name),
					new CodeSnippetExpression("value")
				});
				codeMemberProperty.SetStatements.Add(codeMethodInvokeExpression);
			}
			profileClass.Members.Add(codeMemberProperty);
		}

		// Token: 0x060042D2 RID: 17106 RVA: 0x000B087C File Offset: 0x000AEA7C
		private void AddProfileClassGroupProperty(string groupName, string memberName, CodeTypeDeclaration profileClass)
		{
			CodeMemberProperty codeMemberProperty = new CodeMemberProperty();
			codeMemberProperty.Name = memberName;
			codeMemberProperty.Type = new CodeTypeReference(groupName);
			codeMemberProperty.Attributes = MemberAttributes.Public;
			CodeMethodReturnStatement codeMethodReturnStatement = new CodeMethodReturnStatement();
			CodeCastExpression codeCastExpression = new CodeCastExpression();
			codeMethodReturnStatement.Expression = codeCastExpression;
			CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(new CodeThisReferenceExpression(), "GetProfileGroup"), new CodeExpression[]
			{
				new CodePrimitiveExpression(memberName)
			});
			codeCastExpression.TargetType = new CodeTypeReference(groupName);
			codeCastExpression.Expression = codeMethodInvokeExpression;
			codeMemberProperty.GetStatements.Add(codeMethodReturnStatement);
			profileClass.Members.Add(codeMemberProperty);
		}

		// Token: 0x060042D3 RID: 17107 RVA: 0x000B0914 File Offset: 0x000AEB14
		private void BuildProfileClass(ProfileSection ps, string className, ProfilePropertySettingsCollection psc, CodeNamespace ns, string baseClass, bool baseIsGlobal, SortedList<string, string> groupProperties)
		{
			CodeTypeDeclaration codeTypeDeclaration = new CodeTypeDeclaration(className);
			CodeTypeReference codeTypeReference = new CodeTypeReference(baseClass);
			if (baseIsGlobal)
			{
				codeTypeReference.Options |= CodeTypeReferenceOptions.GlobalReference;
			}
			codeTypeDeclaration.BaseTypes.Add(codeTypeReference);
			codeTypeDeclaration.TypeAttributes = TypeAttributes.Public;
			ns.Types.Add(codeTypeDeclaration);
			foreach (object obj in psc)
			{
				ProfilePropertySettings profilePropertySettings = (ProfilePropertySettings)obj;
				this.AddProfileClassProperty(ps, codeTypeDeclaration, profilePropertySettings);
			}
			if (groupProperties != null && groupProperties.Count > 0)
			{
				foreach (KeyValuePair<string, string> keyValuePair in groupProperties)
				{
					this.AddProfileClassGroupProperty(keyValuePair.Key, keyValuePair.Value, codeTypeDeclaration);
				}
			}
			this.AddProfileClassGetProfileMethod(codeTypeDeclaration);
		}

		// Token: 0x060042D4 RID: 17108 RVA: 0x000B0A14 File Offset: 0x000AEC14
		private string MakeGroupName(string name)
		{
			return "ProfileGroup" + name;
		}

		// Token: 0x060042D5 RID: 17109 RVA: 0x000B0A24 File Offset: 0x000AEC24
		private bool ProcessCustomProfile(ProfileSection ps, AppCodeAssembly defasm)
		{
			CodeCompileUnit codeCompileUnit = new CodeCompileUnit();
			CodeNamespace codeNamespace = new CodeNamespace(null);
			codeCompileUnit.Namespaces.Add(codeNamespace);
			defasm.AddUnit(codeCompileUnit);
			codeNamespace.Imports.Add(new CodeNamespaceImport("System"));
			codeNamespace.Imports.Add(new CodeNamespaceImport("System.Configuration"));
			codeNamespace.Imports.Add(new CodeNamespaceImport("System.Web"));
			codeNamespace.Imports.Add(new CodeNamespaceImport("System.Web.Profile"));
			RootProfilePropertySettingsCollection propertySettings = ps.PropertySettings;
			if (propertySettings == null)
			{
				return true;
			}
			SortedList<string, string> sortedList = new SortedList<string, string>();
			foreach (object obj in propertySettings.GroupSettings)
			{
				ProfileGroupSettings profileGroupSettings = (ProfileGroupSettings)obj;
				string text = this.MakeGroupName(profileGroupSettings.Name);
				sortedList.Add(text, profileGroupSettings.Name);
				this.BuildProfileClass(ps, text, profileGroupSettings.PropertySettings, codeNamespace, "System.Web.Profile.ProfileGroupBase", true, null);
			}
			string text2 = ps.Inherits;
			if (string.IsNullOrEmpty(text2))
			{
				text2 = "System.Web.Profile.ProfileBase";
			}
			else
			{
				string[] array = text2.Split(new char[] { ',' });
				if (array.Length > 1)
				{
					text2 = array[0].Trim();
				}
			}
			bool flag = text2.IndexOf('.') != -1;
			this.BuildProfileClass(ps, "ProfileCommon", propertySettings, codeNamespace, text2, flag, sortedList);
			return true;
		}

		// Token: 0x060042D6 RID: 17110 RVA: 0x000B0BA4 File Offset: 0x000AEDA4
		public static bool HaveCustomProfile(ProfileSection ps)
		{
			if (ps == null || !ps.Enabled)
			{
				return false;
			}
			RootProfilePropertySettingsCollection propertySettings = ps.PropertySettings;
			ProfileGroupSettingsCollection profileGroupSettingsCollection = ((propertySettings != null) ? propertySettings.GroupSettings : null);
			return !string.IsNullOrEmpty(ps.Inherits) || (propertySettings != null && propertySettings.Count > 0) || (profileGroupSettingsCollection != null && profileGroupSettingsCollection.Count > 0);
		}

		// Token: 0x060042D7 RID: 17111 RVA: 0x000B0BFC File Offset: 0x000AEDFC
		public void Compile()
		{
			if (AppCodeCompiler._alreadyCompiled)
			{
				return;
			}
			string text = Path.Combine(HttpRuntime.AppDomainAppPath, "App_Code");
			ProfileSection profileSection = WebConfigurationManager.GetWebApplicationSection("system.web/profile") as ProfileSection;
			bool flag = Directory.Exists(text);
			bool flag2 = AppCodeCompiler.HaveCustomProfile(profileSection);
			if (!flag && !flag2)
			{
				return;
			}
			AppCodeAssembly appCodeAssembly = new AppCodeAssembly("App_Code", text);
			this.assemblies.Add(appCodeAssembly);
			bool flag3 = false;
			if (flag)
			{
				flag3 = this.ProcessAppCodeDir(text, appCodeAssembly);
			}
			if (flag2 && this.ProcessCustomProfile(profileSection, appCodeAssembly))
			{
				flag3 = true;
			}
			if (!flag3)
			{
				return;
			}
			HttpRuntime.EnableAssemblyMapping(true);
			string[] binDirectoryAssemblies = HttpApplication.BinDirectoryAssemblies;
			foreach (AppCodeAssembly appCodeAssembly2 in this.assemblies)
			{
				appCodeAssembly2.Build(binDirectoryAssemblies);
			}
			AppCodeCompiler._alreadyCompiled = true;
			AppCodeCompiler.DefaultAppCodeAssemblyName = Path.GetFileNameWithoutExtension(appCodeAssembly.OutputAssemblyName);
			this.RunAppInitialize();
			if (flag2 && this.providerTypeName != null)
			{
				if (Type.GetType(this.providerTypeName, false) == null)
				{
					using (IEnumerator enumerator2 = BuildManager.TopLevelAssemblies.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							object obj = enumerator2.Current;
							Assembly assembly = (Assembly)obj;
							if (!(assembly == null) && assembly.GetType(this.providerTypeName, false) != null)
							{
								return;
							}
						}
						goto IL_0162;
					}
					return;
					IL_0162:
					Exception ex = null;
					Type type = null;
					try
					{
						type = HttpApplication.LoadTypeFromBin(this.providerTypeName);
					}
					catch (Exception ex)
					{
					}
					if (type == null)
					{
						throw new HttpException(string.Format("Profile provider type not found: {0}", this.providerTypeName), ex);
					}
					return;
				}
				return;
			}
		}

		// Token: 0x060042D8 RID: 17112 RVA: 0x000B0DD0 File Offset: 0x000AEFD0
		private void RunAppInitialize()
		{
			MethodInfo methodInfo = null;
			foreach (object obj in BuildManager.CodeAssemblies)
			{
				Type[] exportedTypes = ((Assembly)obj).GetExportedTypes();
				if (exportedTypes != null && exportedTypes.Length != 0)
				{
					Type[] array = exportedTypes;
					for (int i = 0; i < array.Length; i++)
					{
						MethodInfo method = array[i].GetMethod("AppInitialize", BindingFlags.IgnoreCase | BindingFlags.Static | BindingFlags.Public, null, Type.EmptyTypes, null);
						if (!(method == null))
						{
							if (methodInfo != null)
							{
								throw new HttpException("The static AppInitialize method found in more than one type in the App_Code directory.");
							}
							methodInfo = method;
						}
					}
				}
			}
			if (methodInfo == null)
			{
				return;
			}
			methodInfo.Invoke(null, null);
		}

		// Token: 0x060042D9 RID: 17113 RVA: 0x000B0E94 File Offset: 0x000AF094
		private bool CollectFiles(string dir, AppCodeAssembly aca)
		{
			bool flag = false;
			AppCodeAssembly appCodeAssembly = aca;
			foreach (string text in Directory.GetFiles(dir))
			{
				aca.AddFile(text);
				flag = true;
			}
			foreach (string text2 in Directory.GetDirectories(dir))
			{
				foreach (AppCodeAssembly appCodeAssembly2 in this.assemblies)
				{
					if (appCodeAssembly2.SourcePath == text2)
					{
						appCodeAssembly = appCodeAssembly2;
						break;
					}
				}
				if (this.CollectFiles(text2, appCodeAssembly))
				{
					flag = true;
				}
				appCodeAssembly = aca;
			}
			return flag;
		}

		// Token: 0x040023C5 RID: 9157
		private static bool _alreadyCompiled;

		// Token: 0x040023C6 RID: 9158
		internal static string DefaultAppCodeAssemblyName;

		// Token: 0x040023C7 RID: 9159
		private List<AppCodeAssembly> assemblies;

		// Token: 0x040023C8 RID: 9160
		private string providerTypeName;
	}
}
