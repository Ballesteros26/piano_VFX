using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Unity;

namespace System.Drawing.Design
{
	/// <summary>Provides a base implementation of a toolbox item.</summary>
	// Token: 0x0200012C RID: 300
	[MonoTODO("Implementation is incomplete.")]
	[PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
	[PermissionSet(SecurityAction.LinkDemand, Unrestricted = true)]
	[Serializable]
	public class ToolboxItem : ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Design.ToolboxItem" /> class.</summary>
		// Token: 0x06000D9A RID: 3482 RVA: 0x0001DAE0 File Offset: 0x0001BCE0
		public ToolboxItem()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Design.ToolboxItem" /> class that creates the specified type of component.</summary>
		/// <param name="toolType">The type of <see cref="T:System.ComponentModel.IComponent" /> that the toolbox item creates. </param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Drawing.Design.ToolboxItem" /> was locked. </exception>
		// Token: 0x06000D9B RID: 3483 RVA: 0x0001DAF3 File Offset: 0x0001BCF3
		public ToolboxItem(Type toolType)
		{
			this.Initialize(toolType);
		}

		/// <summary>Gets or sets the name of the assembly that contains the type or types that the toolbox item creates.</summary>
		/// <returns>An <see cref="T:System.Reflection.AssemblyName" /> that indicates the assembly containing the type or types to create.</returns>
		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x06000D9C RID: 3484 RVA: 0x0001DB0D File Offset: 0x0001BD0D
		// (set) Token: 0x06000D9D RID: 3485 RVA: 0x0001DB24 File Offset: 0x0001BD24
		public AssemblyName AssemblyName
		{
			get
			{
				return (AssemblyName)this.properties["AssemblyName"];
			}
			set
			{
				this.SetValue("AssemblyName", value);
			}
		}

		/// <summary>Gets or sets a bitmap to represent the toolbox item in the toolbox.</summary>
		/// <returns>A <see cref="T:System.Drawing.Bitmap" /> that represents the toolbox item in the toolbox.</returns>
		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06000D9E RID: 3486 RVA: 0x0001DB32 File Offset: 0x0001BD32
		// (set) Token: 0x06000D9F RID: 3487 RVA: 0x0001DB49 File Offset: 0x0001BD49
		public Bitmap Bitmap
		{
			get
			{
				return (Bitmap)this.properties["Bitmap"];
			}
			set
			{
				this.SetValue("Bitmap", value);
			}
		}

		/// <summary>Gets or sets the display name for the toolbox item.</summary>
		/// <returns>The display name for the toolbox item.</returns>
		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06000DA0 RID: 3488 RVA: 0x0001DB57 File Offset: 0x0001BD57
		// (set) Token: 0x06000DA1 RID: 3489 RVA: 0x0001DB64 File Offset: 0x0001BD64
		public string DisplayName
		{
			get
			{
				return this.GetValue("DisplayName");
			}
			set
			{
				this.SetValue("DisplayName", value);
			}
		}

		/// <summary>Gets or sets the filter that determines whether the toolbox item can be used on a destination component.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> of <see cref="T:System.ComponentModel.ToolboxItemFilterAttribute" /> objects.</returns>
		// Token: 0x170003AC RID: 940
		// (get) Token: 0x06000DA2 RID: 3490 RVA: 0x0001DB74 File Offset: 0x0001BD74
		// (set) Token: 0x06000DA3 RID: 3491 RVA: 0x0001DBA2 File Offset: 0x0001BDA2
		public ICollection Filter
		{
			get
			{
				ICollection collection = (ICollection)this.properties["Filter"];
				if (collection == null)
				{
					collection = new ToolboxItemFilterAttribute[0];
				}
				return collection;
			}
			set
			{
				this.SetValue("Filter", value);
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Drawing.Design.ToolboxItem" /> is currently locked.</summary>
		/// <returns>true if the toolbox item is locked; otherwise, false.</returns>
		// Token: 0x170003AD RID: 941
		// (get) Token: 0x06000DA4 RID: 3492 RVA: 0x0001DBB0 File Offset: 0x0001BDB0
		public virtual bool Locked
		{
			get
			{
				return this.locked;
			}
		}

		/// <summary>Gets or sets the fully qualified name of the type of <see cref="T:System.ComponentModel.IComponent" /> that the toolbox item creates when invoked.</summary>
		/// <returns>The fully qualified type name of the type of component that this toolbox item creates.</returns>
		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06000DA5 RID: 3493 RVA: 0x0001DBB8 File Offset: 0x0001BDB8
		// (set) Token: 0x06000DA6 RID: 3494 RVA: 0x0001DBC5 File Offset: 0x0001BDC5
		public string TypeName
		{
			get
			{
				return this.GetValue("TypeName");
			}
			set
			{
				this.SetValue("TypeName", value);
			}
		}

		/// <summary>Gets or sets the company name for this <see cref="T:System.Drawing.Design.ToolboxItem" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that specifies the company for this <see cref="T:System.Drawing.Design.ToolboxItem" />.</returns>
		// Token: 0x170003AF RID: 943
		// (get) Token: 0x06000DA7 RID: 3495 RVA: 0x0001DBD3 File Offset: 0x0001BDD3
		// (set) Token: 0x06000DA8 RID: 3496 RVA: 0x0001DBEA File Offset: 0x0001BDEA
		public string Company
		{
			get
			{
				return (string)this.properties["Company"];
			}
			set
			{
				this.SetValue("Company", value);
			}
		}

		/// <summary>Gets the component type for this <see cref="T:System.Drawing.Design.ToolboxItem" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that specifies the component type for this <see cref="T:System.Drawing.Design.ToolboxItem" />.</returns>
		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x06000DA9 RID: 3497 RVA: 0x0001DBF8 File Offset: 0x0001BDF8
		public virtual string ComponentType
		{
			get
			{
				return ".NET Component";
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Reflection.AssemblyName" /> for the toolbox item.</summary>
		/// <returns>An array of <see cref="T:System.Reflection.AssemblyName" /> objects.</returns>
		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06000DAA RID: 3498 RVA: 0x0001DBFF File Offset: 0x0001BDFF
		// (set) Token: 0x06000DAB RID: 3499 RVA: 0x0001DC18 File Offset: 0x0001BE18
		public AssemblyName[] DependentAssemblies
		{
			get
			{
				return (AssemblyName[])this.properties["DependentAssemblies"];
			}
			set
			{
				AssemblyName[] array = new AssemblyName[value.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = value[i];
				}
				this.SetValue("DependentAssemblies", array);
			}
		}

		/// <summary>Gets or sets the description for this <see cref="T:System.Drawing.Design.ToolboxItem" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that specifies the description for this <see cref="T:System.Drawing.Design.ToolboxItem" />.</returns>
		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06000DAC RID: 3500 RVA: 0x0001DC4E File Offset: 0x0001BE4E
		// (set) Token: 0x06000DAD RID: 3501 RVA: 0x0001DC65 File Offset: 0x0001BE65
		public string Description
		{
			get
			{
				return (string)this.properties["Description"];
			}
			set
			{
				this.SetValue("Description", value);
			}
		}

		/// <summary>Gets a value indicating whether the toolbox item is transient.</summary>
		/// <returns>true, if this toolbox item should not be stored in any toolbox database when an application that is providing a toolbox closes; otherwise, false.</returns>
		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06000DAE RID: 3502 RVA: 0x0001DC74 File Offset: 0x0001BE74
		// (set) Token: 0x06000DAF RID: 3503 RVA: 0x0001DC9D File Offset: 0x0001BE9D
		public bool IsTransient
		{
			get
			{
				object obj = this.properties["IsTransient"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.SetValue("IsTransient", value);
			}
		}

		/// <summary>Gets a dictionary of properties.</summary>
		/// <returns>A dictionary of name/value pairs (the names are property names and the values are property values).</returns>
		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06000DB0 RID: 3504 RVA: 0x0001DCB0 File Offset: 0x0001BEB0
		public IDictionary Properties
		{
			get
			{
				return this.properties;
			}
		}

		/// <summary>Gets the version for this <see cref="T:System.Drawing.Design.ToolboxItem" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that specifies the version for this <see cref="T:System.Drawing.Design.ToolboxItem" />.</returns>
		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06000DB1 RID: 3505 RVA: 0x0001DCB8 File Offset: 0x0001BEB8
		public virtual string Version
		{
			get
			{
				return string.Empty;
			}
		}

		/// <summary>Throws an exception if the toolbox item is currently locked.</summary>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Drawing.Design.ToolboxItem" /> is locked. </exception>
		// Token: 0x06000DB2 RID: 3506 RVA: 0x0001DCBF File Offset: 0x0001BEBF
		protected void CheckUnlocked()
		{
			if (this.locked)
			{
				throw new InvalidOperationException("The ToolboxItem is locked");
			}
		}

		/// <summary>Creates the components that the toolbox item is configured to create.</summary>
		/// <returns>An array of created <see cref="T:System.ComponentModel.IComponent" /> objects.</returns>
		// Token: 0x06000DB3 RID: 3507 RVA: 0x0001DCD4 File Offset: 0x0001BED4
		public IComponent[] CreateComponents()
		{
			return this.CreateComponents(null);
		}

		/// <summary>Creates the components that the toolbox item is configured to create, using the specified designer host.</summary>
		/// <returns>An array of created <see cref="T:System.ComponentModel.IComponent" /> objects.</returns>
		/// <param name="host">The <see cref="T:System.ComponentModel.Design.IDesignerHost" /> to use when creating the components. </param>
		// Token: 0x06000DB4 RID: 3508 RVA: 0x0001DCE0 File Offset: 0x0001BEE0
		public IComponent[] CreateComponents(IDesignerHost host)
		{
			this.OnComponentsCreating(new ToolboxComponentsCreatingEventArgs(host));
			IComponent[] array = this.CreateComponentsCore(host);
			this.OnComponentsCreated(new ToolboxComponentsCreatedEventArgs(array));
			return array;
		}

		/// <summary>Creates a component or an array of components when the toolbox item is invoked.</summary>
		/// <returns>An array of created <see cref="T:System.ComponentModel.IComponent" /> objects.</returns>
		/// <param name="host">The <see cref="T:System.ComponentModel.Design.IDesignerHost" /> to host the toolbox item. </param>
		// Token: 0x06000DB5 RID: 3509 RVA: 0x0001DD10 File Offset: 0x0001BF10
		protected virtual IComponent[] CreateComponentsCore(IDesignerHost host)
		{
			if (host == null)
			{
				throw new ArgumentNullException("host");
			}
			Type type = this.GetType(host, this.AssemblyName, this.TypeName, true);
			IComponent[] array;
			if (type == null)
			{
				array = new IComponent[0];
			}
			else
			{
				array = new IComponent[] { host.CreateComponent(type) };
			}
			return array;
		}

		/// <summary>Creates an array of components when the toolbox item is invoked.</summary>
		/// <returns>An array of created <see cref="T:System.ComponentModel.IComponent" /> objects.</returns>
		/// <param name="host">The designer host to use when creating components.</param>
		/// <param name="defaultValues">A dictionary of property name/value pairs of default values with which to initialize the component.</param>
		// Token: 0x06000DB6 RID: 3510 RVA: 0x0001DD64 File Offset: 0x0001BF64
		protected virtual IComponent[] CreateComponentsCore(IDesignerHost host, IDictionary defaultValues)
		{
			IComponent[] array = this.CreateComponentsCore(host);
			foreach (Component component in array)
			{
				(host.GetDesigner(component) as IComponentInitializer).InitializeNewComponent(defaultValues);
			}
			return array;
		}

		/// <summary>Creates the components that the toolbox item is configured to create, using the specified designer host and default values.</summary>
		/// <returns>An array of created <see cref="T:System.ComponentModel.IComponent" /> objects.</returns>
		/// <param name="host">The <see cref="T:System.ComponentModel.Design.IDesignerHost" /> to use when creating the components.</param>
		/// <param name="defaultValues">A dictionary of property name/value pairs of default values with which to initialize the component.</param>
		// Token: 0x06000DB7 RID: 3511 RVA: 0x0001DDA8 File Offset: 0x0001BFA8
		public IComponent[] CreateComponents(IDesignerHost host, IDictionary defaultValues)
		{
			this.OnComponentsCreating(new ToolboxComponentsCreatingEventArgs(host));
			IComponent[] array = this.CreateComponentsCore(host, defaultValues);
			this.OnComponentsCreated(new ToolboxComponentsCreatedEventArgs(array));
			return array;
		}

		/// <summary>Filters a property value before returning it.</summary>
		/// <returns>A filtered property value.</returns>
		/// <param name="propertyName">The name of the property to filter.</param>
		/// <param name="value">The value against which to filter the property.</param>
		// Token: 0x06000DB8 RID: 3512 RVA: 0x0001DDD8 File Offset: 0x0001BFD8
		protected virtual object FilterPropertyValue(string propertyName, object value)
		{
			if (!(propertyName == "AssemblyName"))
			{
				if (!(propertyName == "DisplayName") && !(propertyName == "TypeName"))
				{
					if (!(propertyName == "Filter"))
					{
						return value;
					}
					if (value != null)
					{
						return value;
					}
					return new ToolboxItemFilterAttribute[0];
				}
				else
				{
					if (value != null)
					{
						return value;
					}
					return string.Empty;
				}
			}
			else
			{
				if (value != null)
				{
					return (value as ICloneable).Clone();
				}
				return null;
			}
		}

		/// <summary>Loads the state of the toolbox item from the specified serialization information object.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to load from. </param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that indicates the stream characteristics. </param>
		// Token: 0x06000DB9 RID: 3513 RVA: 0x0001DE44 File Offset: 0x0001C044
		protected virtual void Deserialize(SerializationInfo info, StreamingContext context)
		{
			this.AssemblyName = (AssemblyName)info.GetValue("AssemblyName", typeof(AssemblyName));
			this.Bitmap = (Bitmap)info.GetValue("Bitmap", typeof(Bitmap));
			this.Filter = (ICollection)info.GetValue("Filter", typeof(ICollection));
			this.DisplayName = info.GetString("DisplayName");
			this.locked = info.GetBoolean("Locked");
			this.TypeName = info.GetString("TypeName");
		}

		/// <summary>Determines whether two <see cref="T:System.Drawing.Design.ToolboxItem" /> instances are equal.</summary>
		/// <returns>true if the specified <see cref="T:System.Drawing.Design.ToolboxItem" /> is equal to the current <see cref="T:System.Drawing.Design.ToolboxItem" />; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.Drawing.Design.ToolboxItem" /> to compare with the current <see cref="T:System.Drawing.Design.ToolboxItem" />.</param>
		// Token: 0x06000DBA RID: 3514 RVA: 0x0001DEE4 File Offset: 0x0001C0E4
		public override bool Equals(object obj)
		{
			ToolboxItem toolboxItem = obj as ToolboxItem;
			return toolboxItem != null && (obj == this || (toolboxItem.AssemblyName.Equals(this.AssemblyName) && toolboxItem.Locked.Equals(this.locked) && toolboxItem.TypeName.Equals(this.TypeName) && toolboxItem.DisplayName.Equals(this.DisplayName) && toolboxItem.Bitmap.Equals(this.Bitmap)));
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Drawing.Design.ToolboxItem" />.</returns>
		// Token: 0x06000DBB RID: 3515 RVA: 0x0001DF65 File Offset: 0x0001C165
		public override int GetHashCode()
		{
			return (this.TypeName + this.DisplayName).GetHashCode();
		}

		/// <summary>Enables access to the type associated with the toolbox item.</summary>
		/// <returns>The type associated with the toolbox item.</returns>
		/// <param name="host">The designer host to query for <see cref="T:System.ComponentModel.Design.ITypeResolutionService" />.</param>
		// Token: 0x06000DBC RID: 3516 RVA: 0x0001DF7D File Offset: 0x0001C17D
		public Type GetType(IDesignerHost host)
		{
			return this.GetType(host, this.AssemblyName, this.TypeName, false);
		}

		/// <summary>Creates an instance of the specified type, optionally using a specified designer host and assembly name.</summary>
		/// <returns>An instance of the specified type, if it can be located.</returns>
		/// <param name="host">The <see cref="T:System.ComponentModel.Design.IDesignerHost" /> for the current document. This can be null. </param>
		/// <param name="assemblyName">An <see cref="T:System.Reflection.AssemblyName" /> that indicates the assembly that contains the type to load. This can be null. </param>
		/// <param name="typeName">The name of the type to create an instance of. </param>
		/// <param name="reference">A value indicating whether or not to add a reference to the assembly that contains the specified type to the designer host's set of references. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="typeName" /> is not specified. </exception>
		// Token: 0x06000DBD RID: 3517 RVA: 0x0001DF94 File Offset: 0x0001C194
		protected virtual Type GetType(IDesignerHost host, AssemblyName assemblyName, string typeName, bool reference)
		{
			if (typeName == null)
			{
				throw new ArgumentNullException("typeName");
			}
			if (host == null)
			{
				return null;
			}
			ITypeResolutionService typeResolutionService = host.GetService(typeof(ITypeResolutionService)) as ITypeResolutionService;
			Type type = null;
			if (typeResolutionService != null)
			{
				typeResolutionService.GetAssembly(assemblyName, true);
				if (reference)
				{
					typeResolutionService.ReferenceAssembly(assemblyName);
				}
				type = typeResolutionService.GetType(typeName, true);
			}
			else
			{
				Assembly assembly = Assembly.Load(assemblyName);
				if (assembly != null)
				{
					type = assembly.GetType(typeName);
				}
			}
			return type;
		}

		/// <summary>Initializes the current toolbox item with the specified type to create.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> that the toolbox item creates. </param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Drawing.Design.ToolboxItem" /> was locked. </exception>
		// Token: 0x06000DBE RID: 3518 RVA: 0x0001E008 File Offset: 0x0001C208
		public virtual void Initialize(Type type)
		{
			this.CheckUnlocked();
			if (type == null)
			{
				return;
			}
			this.AssemblyName = type.Assembly.GetName();
			this.DisplayName = type.Name;
			this.TypeName = type.FullName;
			Image image = null;
			object[] customAttributes = type.GetCustomAttributes(true);
			for (int i = 0; i < customAttributes.Length; i++)
			{
				ToolboxBitmapAttribute toolboxBitmapAttribute = customAttributes[i] as ToolboxBitmapAttribute;
				if (toolboxBitmapAttribute != null)
				{
					image = toolboxBitmapAttribute.GetImage(type);
					break;
				}
			}
			if (image == null)
			{
				image = ToolboxBitmapAttribute.GetImageFromResource(type, null, false);
			}
			if (image != null)
			{
				this.Bitmap = image as Bitmap;
				if (this.Bitmap == null)
				{
					this.Bitmap = new Bitmap(image);
				}
			}
			this.Filter = type.GetCustomAttributes(typeof(ToolboxItemFilterAttribute), true);
		}

		/// <summary>For a description of this member, see the <see cref="M:System.Runtime.Serialization.ISerializable.GetObjectData(System.Runtime.Serialization.SerializationInfo,System.Runtime.Serialization.StreamingContext)" /> method.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data.</param>
		/// <param name="context">The destination (see <see cref="T:System.Runtime.Serialization.StreamingContext" />) for this serialization.</param>
		// Token: 0x06000DBF RID: 3519 RVA: 0x0001E0C2 File Offset: 0x0001C2C2
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			this.Serialize(info, context);
		}

		/// <summary>Locks the toolbox item and prevents changes to its properties.</summary>
		// Token: 0x06000DC0 RID: 3520 RVA: 0x0001E0CC File Offset: 0x0001C2CC
		public virtual void Lock()
		{
			this.locked = true;
		}

		/// <summary>Raises the <see cref="E:System.Drawing.Design.ToolboxItem.ComponentsCreated" /> event.</summary>
		/// <param name="args">A <see cref="T:System.Drawing.Design.ToolboxComponentsCreatedEventArgs" /> that provides data for the event. </param>
		// Token: 0x06000DC1 RID: 3521 RVA: 0x0001E0D5 File Offset: 0x0001C2D5
		protected virtual void OnComponentsCreated(ToolboxComponentsCreatedEventArgs args)
		{
			if (this.ComponentsCreated != null)
			{
				this.ComponentsCreated(this, args);
			}
		}

		/// <summary>Raises the <see cref="E:System.Drawing.Design.ToolboxItem.ComponentsCreating" /> event.</summary>
		/// <param name="args">A <see cref="T:System.Drawing.Design.ToolboxComponentsCreatingEventArgs" /> that provides data for the event. </param>
		// Token: 0x06000DC2 RID: 3522 RVA: 0x0001E0EC File Offset: 0x0001C2EC
		protected virtual void OnComponentsCreating(ToolboxComponentsCreatingEventArgs args)
		{
			if (this.ComponentsCreating != null)
			{
				this.ComponentsCreating(this, args);
			}
		}

		/// <summary>Saves the state of the toolbox item to the specified serialization information object.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to save to. </param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that indicates the stream characteristics. </param>
		// Token: 0x06000DC3 RID: 3523 RVA: 0x0001E104 File Offset: 0x0001C304
		protected virtual void Serialize(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("AssemblyName", this.AssemblyName);
			info.AddValue("Bitmap", this.Bitmap);
			info.AddValue("Filter", this.Filter);
			info.AddValue("DisplayName", this.DisplayName);
			info.AddValue("Locked", this.locked);
			info.AddValue("TypeName", this.TypeName);
		}

		/// <summary>Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Drawing.Design.ToolboxItem" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that represents the current <see cref="T:System.Drawing.Design.ToolboxItem" />.</returns>
		// Token: 0x06000DC4 RID: 3524 RVA: 0x0001E177 File Offset: 0x0001C377
		public override string ToString()
		{
			return this.DisplayName;
		}

		/// <summary>Validates that an object is of a given type.</summary>
		/// <param name="propertyName">The name of the property to validate.</param>
		/// <param name="value">Optional value against which to validate.</param>
		/// <param name="expectedType">The expected type of the property.</param>
		/// <param name="allowNull">true to allow null; otherwise, false.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null, and <paramref name="allowNull" /> is false.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is not the type specified by <paramref name="expectedType" />.</exception>
		// Token: 0x06000DC5 RID: 3525 RVA: 0x0001E180 File Offset: 0x0001C380
		protected void ValidatePropertyType(string propertyName, object value, Type expectedType, bool allowNull)
		{
			if (!allowNull && value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (value != null && !expectedType.Equals(value.GetType()))
			{
				throw new ArgumentException(Locale.GetText("Type mismatch between value ({0}) and expected type ({1}).", new object[]
				{
					value.GetType(),
					expectedType
				}), "value");
			}
		}

		/// <summary>Validates a property before it is assigned to the property dictionary.</summary>
		/// <returns>The value used to perform validation.</returns>
		/// <param name="propertyName">The name of the property to validate.</param>
		/// <param name="value">The value against which to validate.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null, and <paramref name="propertyName" /> is "IsTransient".</exception>
		// Token: 0x06000DC6 RID: 3526 RVA: 0x0001E1D8 File Offset: 0x0001C3D8
		protected virtual object ValidatePropertyValue(string propertyName, object value)
		{
			uint num = <PrivateImplementationDetails>.ComputeStringHash(propertyName);
			if (num <= 1629252038U)
			{
				if (num <= 982935374U)
				{
					if (num != 278446637U)
					{
						if (num != 982935374U)
						{
							return value;
						}
						if (!(propertyName == "TypeName"))
						{
							return value;
						}
					}
					else
					{
						if (!(propertyName == "IsTransient"))
						{
							return value;
						}
						this.ValidatePropertyType(propertyName, value, typeof(bool), false);
						return value;
					}
				}
				else if (num != 1561053712U)
				{
					if (num != 1629252038U)
					{
						return value;
					}
					if (!(propertyName == "AssemblyName"))
					{
						return value;
					}
					this.ValidatePropertyType(propertyName, value, typeof(AssemblyName), true);
					return value;
				}
				else
				{
					if (!(propertyName == "DependentAssemblies"))
					{
						return value;
					}
					this.ValidatePropertyType(propertyName, value, typeof(AssemblyName[]), true);
					return value;
				}
			}
			else if (num <= 1725856265U)
			{
				if (num != 1651150918U)
				{
					if (num != 1725856265U)
					{
						return value;
					}
					if (!(propertyName == "Description"))
					{
						return value;
					}
				}
				else
				{
					if (!(propertyName == "Bitmap"))
					{
						return value;
					}
					this.ValidatePropertyType(propertyName, value, typeof(Bitmap), true);
					return value;
				}
			}
			else if (num != 3250523996U)
			{
				if (num != 4104765591U)
				{
					if (num != 4176258230U)
					{
						return value;
					}
					if (!(propertyName == "DisplayName"))
					{
						return value;
					}
				}
				else
				{
					if (!(propertyName == "Filter"))
					{
						return value;
					}
					this.ValidatePropertyType(propertyName, value, typeof(ToolboxItemFilterAttribute[]), true);
					if (value == null)
					{
						return new ToolboxItemFilterAttribute[0];
					}
					return value;
				}
			}
			else if (!(propertyName == "Company"))
			{
				return value;
			}
			this.ValidatePropertyType(propertyName, value, typeof(string), true);
			if (value == null)
			{
				value = string.Empty;
			}
			return value;
		}

		// Token: 0x06000DC7 RID: 3527 RVA: 0x0001E3AF File Offset: 0x0001C5AF
		private void SetValue(string propertyName, object value)
		{
			this.CheckUnlocked();
			this.properties[propertyName] = this.ValidatePropertyValue(propertyName, value);
		}

		// Token: 0x06000DC8 RID: 3528 RVA: 0x0001E3CC File Offset: 0x0001C5CC
		private string GetValue(string propertyName)
		{
			string text = (string)this.properties[propertyName];
			if (text != null)
			{
				return text;
			}
			return string.Empty;
		}

		/// <summary>Occurs immediately after components are created.</summary>
		// Token: 0x14000008 RID: 8
		// (add) Token: 0x06000DC9 RID: 3529 RVA: 0x0001E3F8 File Offset: 0x0001C5F8
		// (remove) Token: 0x06000DCA RID: 3530 RVA: 0x0001E430 File Offset: 0x0001C630
		public event ToolboxComponentsCreatedEventHandler ComponentsCreated;

		/// <summary>Occurs when components are about to be created.</summary>
		// Token: 0x14000009 RID: 9
		// (add) Token: 0x06000DCB RID: 3531 RVA: 0x0001E468 File Offset: 0x0001C668
		// (remove) Token: 0x06000DCC RID: 3532 RVA: 0x0001E4A0 File Offset: 0x0001C6A0
		public event ToolboxComponentsCreatingEventHandler ComponentsCreating;

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06000DCD RID: 3533 RVA: 0x0001E4D5 File Offset: 0x0001C6D5
		// (set) Token: 0x06000DCE RID: 3534 RVA: 0x00003B8D File Offset: 0x00001D8D
		public Bitmap OriginalBitmap
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				ThrowStub.ThrowNotSupportedException();
			}
		}

		// Token: 0x04000A8A RID: 2698
		private bool locked;

		// Token: 0x04000A8B RID: 2699
		private Hashtable properties = new Hashtable();
	}
}
