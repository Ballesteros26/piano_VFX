using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;

namespace System.ComponentModel.Design.Serialization
{
	/// <summary>Provides the base class for implementing a CodeDOM-based designer loader.</summary>
	// Token: 0x02000147 RID: 327
	public abstract class CodeDomDesignerLoader : BasicDesignerLoader, INameCreationService, IDesignerSerializationService
	{
		/// <summary>Initializes services.</summary>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.ComponentModel.Design.Serialization.IDesignerLoaderHost" /> has not been initialized, or the designer loader did not supply a type resolution service, which is required for CodeDom serialization.</exception>
		// Token: 0x060009DD RID: 2525 RVA: 0x00011F98 File Offset: 0x00010198
		protected override void Initialize()
		{
			base.Initialize();
			base.LoaderHost.AddService(typeof(IDesignerSerializationService), this);
			base.LoaderHost.AddService(typeof(INameCreationService), this);
			base.LoaderHost.AddService(typeof(ComponentSerializationService), new CodeDomComponentSerializationService(base.LoaderHost));
			if (this.TypeResolutionService != null && base.LoaderHost.GetService(typeof(ITypeResolutionService)) == null)
			{
				base.LoaderHost.AddService(typeof(ITypeResolutionService), this.TypeResolutionService);
			}
			IDesignerSerializationManager designerSerializationManager = base.LoaderHost.GetService(typeof(IDesignerSerializationManager)) as IDesignerSerializationManager;
			if (designerSerializationManager != null)
			{
				designerSerializationManager.AddSerializationProvider(CodeDomSerializationProvider.Instance);
			}
		}

		/// <summary>Returns a value indicating whether a reload is required.</summary>
		/// <returns>true if the <see cref="P:System.ComponentModel.Design.Serialization.CodeDomDesignerLoader.CodeDomProvider" /> decides a reload is required; otherwise, false.</returns>
		/// <exception cref="T:System.NotSupportedException">The language did not provide a code parser for this file; this file type may not support a designer.</exception>
		/// <exception cref="T:System.InvalidOperationException">The class can be designed, but it is not the first class in the file, or the designer could not be shown for this file because none of the classes within it can be designed.</exception>
		// Token: 0x060009DE RID: 2526 RVA: 0x0001205A File Offset: 0x0001025A
		protected override bool IsReloadNeeded()
		{
			if (this.CodeDomProvider is ICodeDomDesignerReload)
			{
				return ((ICodeDomDesignerReload)this.CodeDomProvider).ShouldReloadDesigner(this.Parse());
			}
			return base.IsReloadNeeded();
		}

		/// <summary>Parses code from a CodeDOM provider.</summary>
		/// <param name="manager">The <see cref="T:System.ComponentModel.Design.Serialization.IDesignerSerializationManager" /> from which to request the serializer.</param>
		/// <exception cref="T:System.NotSupportedException">The language did not provide a code parser for this file; this file type may not support a designer.</exception>
		/// <exception cref="T:System.InvalidOperationException">The class can be designed, but it is not the first class in the file, or the designer could not be shown for this file because none of the classes within it can be designed.</exception>
		// Token: 0x060009DF RID: 2527 RVA: 0x00012088 File Offset: 0x00010288
		protected override void PerformLoad(IDesignerSerializationManager manager)
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			CodeCompileUnit codeCompileUnit = this.Parse();
			if (codeCompileUnit == null)
			{
				throw new NotSupportedException("The language did not provide a code parser for this file");
			}
			string text = null;
			CodeTypeDeclaration firstCodeTypeDecl = this.GetFirstCodeTypeDecl(codeCompileUnit, out text);
			if (firstCodeTypeDecl == null)
			{
				throw new InvalidOperationException("Cannot find a declaration in a namespace to load.");
			}
			this._rootSerializer = manager.GetSerializer(manager.GetType(firstCodeTypeDecl.BaseTypes[0].BaseType), typeof(RootCodeDomSerializer)) as CodeDomSerializer;
			if (this._rootSerializer == null)
			{
				throw new InvalidOperationException("Serialization not supported for this class");
			}
			this._rootSerializer.Deserialize(manager, firstCodeTypeDecl);
			base.SetBaseComponentClassName(text + "." + firstCodeTypeDecl.Name);
		}

		// Token: 0x060009E0 RID: 2528 RVA: 0x0001213C File Offset: 0x0001033C
		private CodeTypeDeclaration GetFirstCodeTypeDecl(CodeCompileUnit document, out string namespaceName)
		{
			namespaceName = null;
			foreach (object obj in document.Namespaces)
			{
				CodeNamespace codeNamespace = (CodeNamespace)obj;
				foreach (object obj2 in codeNamespace.Types)
				{
					CodeTypeDeclaration codeTypeDeclaration = (CodeTypeDeclaration)obj2;
					if (codeTypeDeclaration.IsClass)
					{
						namespaceName = codeNamespace.Name;
						return codeTypeDeclaration;
					}
				}
			}
			return null;
		}

		/// <summary>Requests serialization of the root component of the designer.</summary>
		/// <param name="manager">The <see cref="T:System.ComponentModel.Design.Serialization.IDesignerSerializationManager" /> from which to request the serializer.</param>
		/// <exception cref="T:System.NotSupportedException">The language did not provide a code parser for this file; this file type may not support a designer.</exception>
		/// <exception cref="T:System.InvalidOperationException">The class can be designed, but it is not the first class in the file, or the designer could not be shown for this file because none of the classes within it can be designed.</exception>
		// Token: 0x060009E1 RID: 2529 RVA: 0x000121F4 File Offset: 0x000103F4
		protected override void PerformFlush(IDesignerSerializationManager manager)
		{
			if (this._rootSerializer != null)
			{
				CodeTypeDeclaration codeTypeDeclaration = (CodeTypeDeclaration)this._rootSerializer.Serialize(manager, base.LoaderHost.RootComponent);
				this.Write(this.MergeTypeDeclWithCompileUnit(codeTypeDeclaration, this.Parse()));
			}
		}

		// Token: 0x060009E2 RID: 2530 RVA: 0x0001223C File Offset: 0x0001043C
		private CodeCompileUnit MergeTypeDeclWithCompileUnit(CodeTypeDeclaration typeDecl, CodeCompileUnit unit)
		{
			CodeNamespace codeNamespace = null;
			int num = -1;
			foreach (object obj in unit.Namespaces)
			{
				CodeNamespace codeNamespace2 = (CodeNamespace)obj;
				for (int i = 0; i < codeNamespace2.Types.Count; i++)
				{
					if (codeNamespace2.Types[i].IsClass)
					{
						num = i;
						codeNamespace = codeNamespace2;
					}
				}
			}
			if (num != -1)
			{
				codeNamespace.Types.RemoveAt(num);
			}
			codeNamespace.Types.Add(typeDecl);
			return unit;
		}

		/// <summary>Notifies the designer loader that loading is about to begin.</summary>
		// Token: 0x060009E3 RID: 2531 RVA: 0x000122E8 File Offset: 0x000104E8
		protected override void OnBeginLoad()
		{
			base.OnBeginLoad();
			IComponentChangeService componentChangeService = base.GetService(typeof(IComponentChangeService)) as IComponentChangeService;
			if (componentChangeService != null)
			{
				componentChangeService.ComponentRename += this.OnComponentRename_EventHandler;
			}
		}

		/// <summary>Notifies the designer loader that unloading is about to begin.</summary>
		// Token: 0x060009E4 RID: 2532 RVA: 0x00012328 File Offset: 0x00010528
		protected override void OnBeginUnload()
		{
			base.OnBeginUnload();
			IComponentChangeService componentChangeService = base.GetService(typeof(IComponentChangeService)) as IComponentChangeService;
			if (componentChangeService != null)
			{
				componentChangeService.ComponentRename -= this.OnComponentRename_EventHandler;
			}
		}

		/// <summary>Notifies the designer loader that loading is complete.</summary>
		/// <param name="successful">true to indicate that the load completed successfully; otherwise, false.</param>
		/// <param name="errors">An <see cref="T:System.Collections.ICollection" /> of objects (usually exceptions) that were reported as errors.</param>
		// Token: 0x060009E5 RID: 2533 RVA: 0x00012366 File Offset: 0x00010566
		protected override void OnEndLoad(bool successful, ICollection errors)
		{
			base.OnEndLoad(successful, errors);
		}

		// Token: 0x060009E6 RID: 2534 RVA: 0x00012370 File Offset: 0x00010570
		private void OnComponentRename_EventHandler(object sender, ComponentRenameEventArgs args)
		{
			this.OnComponentRename(args.Component, args.OldName, args.NewName);
		}

		/// <summary>Raises the <see cref="E:System.ComponentModel.Design.IComponentChangeService.ComponentRename" /> event. </summary>
		/// <param name="component">The component to rename.</param>
		/// <param name="oldName">The original name of the component.</param>
		/// <param name="newName">The new name of the component.</param>
		// Token: 0x060009E7 RID: 2535 RVA: 0x00002432 File Offset: 0x00000632
		protected virtual void OnComponentRename(object component, string oldName, string newName)
		{
		}

		/// <summary>Gets the <see cref="P:System.ComponentModel.Design.Serialization.CodeDomDesignerLoader.CodeDomProvider" /> this designer loader will use. </summary>
		/// <returns>The <see cref="P:System.ComponentModel.Design.Serialization.CodeDomDesignerLoader.CodeDomProvider" /> this designer loader will use</returns>
		// Token: 0x17000216 RID: 534
		// (get) Token: 0x060009E8 RID: 2536
		protected abstract CodeDomProvider CodeDomProvider { get; }

		/// <summary>Gets the type resolution service to be used with this designer loader.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.Design.ITypeResolutionService" /> that the CodeDOM serializers will use when resolving types.</returns>
		// Token: 0x17000217 RID: 535
		// (get) Token: 0x060009E9 RID: 2537
		protected abstract ITypeResolutionService TypeResolutionService { get; }

		/// <summary>Parses the text or other persistent storage and returns a <see cref="T:System.CodeDom.CodeCompileUnit" />.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeCompileUnit" /> resulting from a parse operation.</returns>
		// Token: 0x060009EA RID: 2538
		protected abstract CodeCompileUnit Parse();

		/// <summary>Writes compile-unit changes to persistent storage.</summary>
		/// <param name="unit">The <see cref="T:System.CodeDom.CodeCompileUnit" /> to be persisted.</param>
		// Token: 0x060009EB RID: 2539
		protected abstract void Write(CodeCompileUnit unit);

		/// <summary>Releases the resources used by the <see cref="T:System.ComponentModel.Design.Serialization.CodeDomDesignerLoader" /> class.</summary>
		// Token: 0x060009EC RID: 2540 RVA: 0x0001238A File Offset: 0x0001058A
		public override void Dispose()
		{
			base.Dispose();
		}

		/// <summary>Creates a new name that is unique to all components in the specified container.</summary>
		/// <returns>A unique name for the data type.</returns>
		/// <param name="container">The container where the new object is added.</param>
		/// <param name="dataType">The data type of the object that receives the name.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dataType" /> is null.</exception>
		// Token: 0x060009ED RID: 2541 RVA: 0x00012394 File Offset: 0x00010594
		string INameCreationService.CreateName(IContainer container, Type dataType)
		{
			if (dataType == null)
			{
				throw new ArgumentNullException("dataType");
			}
			string text = dataType.Name;
			char c = char.ToLower(text[0]);
			text = text.Remove(0, 1);
			text = text.Insert(0, char.ToString(c));
			int num = 1;
			bool flag = false;
			while (!flag)
			{
				if (container != null && container.Components[text + num] != null)
				{
					num++;
				}
				else
				{
					flag = true;
					text += num;
				}
			}
			if (this.CodeDomProvider != null)
			{
				text = this.CodeDomProvider.CreateValidIdentifier(text);
			}
			return text;
		}

		/// <summary>Gets a value indicating whether the specified name is valid.</summary>
		/// <returns>true if the name is valid; otherwise, false.</returns>
		/// <param name="name">The name to validate.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null.</exception>
		// Token: 0x060009EE RID: 2542 RVA: 0x00012430 File Offset: 0x00010630
		bool INameCreationService.IsValidName(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			bool flag = true;
			if (base.LoaderHost != null && base.LoaderHost.Container.Components[name] != null)
			{
				flag = false;
			}
			else if (this.CodeDomProvider != null)
			{
				flag = this.CodeDomProvider.IsValidIdentifier(name);
			}
			else
			{
				if (name.Trim().Length == 0)
				{
					flag = false;
				}
				for (int i = 0; i < name.Length; i++)
				{
					if (!char.IsLetterOrDigit(name[i]))
					{
						flag = false;
						break;
					}
				}
			}
			return flag;
		}

		/// <summary>Gets a value indicating whether the specified name is valid.</summary>
		/// <param name="name">The name to validate.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is not a valid identifier, or there is already a component with the same name. </exception>
		// Token: 0x060009EF RID: 2543 RVA: 0x000124BD File Offset: 0x000106BD
		void INameCreationService.ValidateName(string name)
		{
			if (!((INameCreationService)this).IsValidName(name))
			{
				throw new ArgumentException("Invalid name '" + name + "'");
			}
		}

		/// <summary>Deserializes the specified serialization data object and returns a collection of objects represented by that data.</summary>
		/// <returns>A collection of objects represented by <paramref name="serializationData" />.</returns>
		/// <param name="serializationData">An object consisting of serialized data.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="serializationData" /> is not a <see cref="T:System.ComponentModel.Design.Serialization.SerializationStore" />. </exception>
		// Token: 0x060009F0 RID: 2544 RVA: 0x000124E0 File Offset: 0x000106E0
		ICollection IDesignerSerializationService.Deserialize(object serializationData)
		{
			if (serializationData == null)
			{
				throw new ArgumentNullException("serializationData");
			}
			ComponentSerializationService componentSerializationService = base.LoaderHost.GetService(typeof(ComponentSerializationService)) as ComponentSerializationService;
			SerializationStore serializationStore = serializationData as SerializationStore;
			if (componentSerializationService != null && serializationData != null)
			{
				return componentSerializationService.Deserialize(serializationStore, base.LoaderHost.Container);
			}
			return new object[0];
		}

		/// <summary>Serializes the specified collection of objects and stores them in a serialization data object.</summary>
		/// <returns>An object that contains the serialized state of the specified collection of objects.</returns>
		/// <param name="objects">A collection of objects to serialize.</param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.ComponentModel.Design.Serialization.ComponentSerializationService" />  was not found.</exception>
		// Token: 0x060009F1 RID: 2545 RVA: 0x0001253C File Offset: 0x0001073C
		object IDesignerSerializationService.Serialize(ICollection objects)
		{
			if (objects == null)
			{
				throw new ArgumentNullException("objects");
			}
			ComponentSerializationService componentSerializationService = base.LoaderHost.GetService(typeof(ComponentSerializationService)) as ComponentSerializationService;
			if (componentSerializationService != null)
			{
				SerializationStore serializationStore = componentSerializationService.CreateStore();
				foreach (object obj in objects)
				{
					componentSerializationService.Serialize(serializationStore, obj);
				}
				serializationStore.Close();
				return serializationStore;
			}
			return null;
		}

		// Token: 0x04000245 RID: 581
		private CodeDomSerializer _rootSerializer;
	}
}
