using System;
using System.Collections;
using System.ComponentModel.Design;
using System.Reflection;
using System.Windows.Forms;

namespace System.Drawing.Design
{
	/// <summary>Provides a default implementation of the <see cref="T:System.Drawing.Design.IToolboxService" /> interface.</summary>
	// Token: 0x0200001B RID: 27
	public abstract class ToolboxService : IComponentDiscoveryService, IToolboxService
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Design.ToolboxService" /> class. </summary>
		// Token: 0x06000063 RID: 99 RVA: 0x000035CE File Offset: 0x000017CE
		[MonoTODO]
		protected ToolboxService()
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets a collection of strings depicting available categories of the toolbox.</summary>
		/// <returns>A collection of category names.</returns>
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000064 RID: 100
		protected abstract CategoryNameCollection CategoryNames { get; }

		/// <summary>Gets or sets the name of the currently selected category.</summary>
		/// <returns>A string containing the name of the currently selected category.</returns>
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000065 RID: 101
		// (set) Token: 0x06000066 RID: 102
		protected abstract string SelectedCategory { get; set; }

		/// <summary>Gets or sets the currently selected item container.</summary>
		/// <returns>The item container for the currently selected toolbox item, or null if no item is selected.</returns>
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000067 RID: 103
		// (set) Token: 0x06000068 RID: 104
		protected abstract ToolboxItemContainer SelectedItemContainer { get; set; }

		/// <summary>Creates a new toolbox item container from a saved data object.</summary>
		/// <returns>A new toolbox item container.</returns>
		/// <param name="dataObject">A data object containing saved toolbox data.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dataObject" /> is null.</exception>
		// Token: 0x06000069 RID: 105 RVA: 0x0000359B File Offset: 0x0000179B
		[MonoTODO]
		protected virtual ToolboxItemContainer CreateItemContainer(IDataObject dataObject)
		{
			throw new NotImplementedException();
		}

		/// <summary>Creates a new toolbox item container from a toolbox item.</summary>
		/// <returns>A new toolbox item container.</returns>
		/// <param name="item">The toolbox item for which to create an item container.</param>
		/// <param name="link">An optional designer host that should be linked to this toolbox item. This parameter can be null.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="item" /> is null.</exception>
		// Token: 0x0600006A RID: 106 RVA: 0x0000359B File Offset: 0x0000179B
		[MonoTODO]
		protected virtual ToolboxItemContainer CreateItemContainer(ToolboxItem item, IDesignerHost link)
		{
			throw new NotImplementedException();
		}

		/// <summary>Occurs when the toolbox service detects that the active designer’s toolbox item filter has changed.</summary>
		// Token: 0x0600006B RID: 107 RVA: 0x0000359B File Offset: 0x0000179B
		[MonoTODO]
		protected virtual void FilterChanged()
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns an <see cref="T:System.Collections.IList" /> containing all items on the toolbox.</summary>
		/// <returns>An <see cref="T:System.Collections.IList" /> containing all items on the toolbox.</returns>
		// Token: 0x0600006C RID: 108
		protected abstract IList GetItemContainers();

		/// <summary>Returns an <see cref="T:System.Collections.IList" /> containing all items in a given category.</summary>
		/// <returns>An <see cref="T:System.Collections.IList" /> containing all items in the category specified by <paramref name="categoryName" />.</returns>
		/// <param name="categoryName">The category for which to retrieve the item container list.</param>
		// Token: 0x0600006D RID: 109
		protected abstract IList GetItemContainers(string categoryName);

		/// <summary>Returns a value indicating whether the given data object represents an item container.</summary>
		/// <returns>true if the given data object represents an item container; otherwise, false.</returns>
		/// <param name="dataObject">The data object to examine for the presence of a toolbox item container.</param>
		/// <param name="host">An optional designer host. This parameter can be null.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dataObject" /> is null.</exception>
		// Token: 0x0600006E RID: 110 RVA: 0x0000359B File Offset: 0x0000179B
		[MonoTODO]
		protected virtual bool IsItemContainer(IDataObject dataObject, IDesignerHost host)
		{
			throw new NotImplementedException();
		}

		/// <summary>Determines whether the toolbox item container is supported by the given designer host.</summary>
		/// <returns>true if the toolbox item container is supported by the given designer host; otherwise, false.</returns>
		/// <param name="container">The toolbox item container.</param>
		/// <param name="host">The given designer host.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="container" /> or <paramref name="host" /> is null.</exception>
		// Token: 0x0600006F RID: 111 RVA: 0x0000359B File Offset: 0x0000179B
		[MonoTODO]
		protected bool IsItemContainerSupported(ToolboxItemContainer container, IDesignerHost host)
		{
			throw new NotImplementedException();
		}

		/// <summary>Refreshes the state of the toolbox items.</summary>
		// Token: 0x06000070 RID: 112
		protected abstract void Refresh();

		/// <summary>Receives a call from the toolbox service when a user reports that a selected toolbox item has been used.</summary>
		// Token: 0x06000071 RID: 113 RVA: 0x0000359B File Offset: 0x0000179B
		[MonoTODO]
		protected virtual void SelectedItemContainerUsed()
		{
			throw new NotImplementedException();
		}

		/// <summary>Sets the current application's cursor to a cursor that represents the currently selected tool.</summary>
		/// <returns>true if there is an item selected; otherwise, false.</returns>
		// Token: 0x06000072 RID: 114 RVA: 0x0000359B File Offset: 0x0000179B
		[MonoTODO]
		protected virtual bool SetCursor()
		{
			throw new NotImplementedException();
		}

		/// <summary>Unloads any assemblies that were locked as a result of calling the <see cref="Overload:System.Drawing.Design.ToolboxService.GetToolboxItems" /> method.</summary>
		// Token: 0x06000073 RID: 115 RVA: 0x0000359B File Offset: 0x0000179B
		[MonoTODO]
		public static void UnloadToolboxItems()
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns a toolbox item for a given type.</summary>
		/// <returns>A toolbox item associated with the given type, or null if the type has no corresponding toolbox item.</returns>
		/// <param name="toolType">The type of component for which to retrieve the toolbox item.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="toolType" /> is null.</exception>
		// Token: 0x06000074 RID: 116 RVA: 0x0000359B File Offset: 0x0000179B
		[MonoTODO]
		public static ToolboxItem GetToolboxItem(Type toolType)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns a toolbox item for a given type.</summary>
		/// <returns>A toolbox item associated with the given type, or null if the type has no corresponding toolbox item.</returns>
		/// <param name="toolType">The type of component for which to retrieve the toolbox item.</param>
		/// <param name="nonPublic">true to search for non-public constructors on the type; false to search for public constructors.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="toolType" /> is null.</exception>
		// Token: 0x06000075 RID: 117 RVA: 0x0000359B File Offset: 0x0000179B
		[MonoTODO]
		public static ToolboxItem GetToolboxItem(Type toolType, bool nonPublic)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns an <see cref="T:System.Collections.ICollection" /> of <see cref="T:System.Drawing.Design.ToolboxItem" /> objects for the given assembly.</summary>
		/// <returns>A collection containing all the toolbox items in the assembly represented by the given assembly name.</returns>
		/// <param name="an">An assembly name from which to load an assembly.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="an" /> is null.</exception>
		// Token: 0x06000076 RID: 118 RVA: 0x0000359B File Offset: 0x0000179B
		[MonoTODO]
		public static ICollection GetToolboxItems(AssemblyName an)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns an <see cref="T:System.Collections.ICollection" /> of <see cref="T:System.Drawing.Design.ToolboxItem" /> objects for the given assembly.</summary>
		/// <returns>A collection containing all the toolbox items in the assembly represented by the given assembly name.</returns>
		/// <param name="an">An assembly name from which to load an assembly.</param>
		/// <param name="throwOnError">true to throw an exception on error; otherwise, false.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="an" /> is null.</exception>
		// Token: 0x06000077 RID: 119 RVA: 0x0000359B File Offset: 0x0000179B
		[MonoTODO]
		public static ICollection GetToolboxItems(AssemblyName an, bool throwOnError)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns an <see cref="T:System.Collections.ICollection" /> containing all the toolbox items in the given assembly.</summary>
		/// <returns>A collection containing all the toolbox items in the given assembly.</returns>
		/// <param name="a">The assembly to enumerate.</param>
		/// <param name="newCodeBase">A string that is the URL location of the assembly.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="a" /> is null.</exception>
		// Token: 0x06000078 RID: 120 RVA: 0x0000359B File Offset: 0x0000179B
		[MonoTODO]
		public static ICollection GetToolboxItems(Assembly a, string newCodeBase)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns an <see cref="T:System.Collections.ICollection" /> of <see cref="T:System.Drawing.Design.ToolboxItem" /> objects for the given assembly.</summary>
		/// <returns>A collection containing all the toolbox items in the assembly represented by the given assembly name.</returns>
		/// <param name="a">The assembly to enumerate.</param>
		/// <param name="newCodeBase">A string that is the URL location of the assembly.</param>
		/// <param name="throwOnError">true to throw an exception on error; otherwise, false.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="a" /> is null.</exception>
		// Token: 0x06000079 RID: 121 RVA: 0x0000359B File Offset: 0x0000179B
		[MonoTODO]
		public static ICollection GetToolboxItems(Assembly a, string newCodeBase, bool throwOnError)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the list of available component types.</summary>
		/// <returns>The list of available component types.</returns>
		/// <param name="designerHost">The designer host providing design-time services.</param>
		/// <param name="baseType">The base type specifying the components to retrieve. Can be null.</param>
		// Token: 0x0600007A RID: 122 RVA: 0x0000359B File Offset: 0x0000179B
		ICollection IComponentDiscoveryService.GetComponentTypes(IDesignerHost designerHost, Type baseType)
		{
			throw new NotImplementedException();
		}

		/// <summary>For a description of this member, see the <see cref="P:System.Drawing.Design.IToolboxService.CategoryNames" /> property.</summary>
		/// <returns>A <see cref="T:System.Drawing.Design.CategoryNameCollection" /> containing the tool categories.</returns>
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600007B RID: 123 RVA: 0x0000359B File Offset: 0x0000179B
		CategoryNameCollection IToolboxService.CategoryNames
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>For a description of this member, see the <see cref="P:System.Drawing.Design.IToolboxService.SelectedCategory" /> property.</summary>
		/// <returns>The name of the currently selected category.</returns>
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600007C RID: 124 RVA: 0x0000359B File Offset: 0x0000179B
		// (set) Token: 0x0600007D RID: 125 RVA: 0x0000359B File Offset: 0x0000179B
		string IToolboxService.SelectedCategory
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>For a description of this member, see the <see cref="M:System.Drawing.Design.IToolboxService.AddCreator(System.Drawing.Design.ToolboxItemCreatorCallback,System.String)" /> method.</summary>
		/// <param name="creator">A <see cref="T:System.Drawing.Design.ToolboxItemCreatorCallback" /> that can create a component when the toolbox item is invoked.</param>
		/// <param name="format">The data format that the creator handles.</param>
		// Token: 0x0600007E RID: 126 RVA: 0x0000359B File Offset: 0x0000179B
		void IToolboxService.AddCreator(ToolboxItemCreatorCallback creator, string format)
		{
			throw new NotImplementedException();
		}

		/// <summary>For a description of this member, see the <see cref="M:System.Drawing.Design.IToolboxService.AddCreator(System.Drawing.Design.ToolboxItemCreatorCallback,System.String,System.ComponentModel.Design.IDesignerHost)" /> method.</summary>
		/// <param name="creator">A <see cref="T:System.Drawing.Design.ToolboxItemCreatorCallback" /> that can create a component when the toolbox item is invoked.</param>
		/// <param name="format">The data format that the creator handles.</param>
		/// <param name="host">The <see cref="T:System.ComponentModel.Design.IDesignerHost" /> that represents the designer host to associate with the creator.</param>
		// Token: 0x0600007F RID: 127 RVA: 0x0000359B File Offset: 0x0000179B
		void IToolboxService.AddCreator(ToolboxItemCreatorCallback creator, string format, IDesignerHost host)
		{
			throw new NotImplementedException();
		}

		/// <summary>For a description of this member, see the <see cref="M:System.Drawing.Design.IToolboxService.AddLinkedToolboxItem(System.Drawing.Design.ToolboxItem,System.ComponentModel.Design.IDesignerHost)" /> method.</summary>
		/// <param name="toolboxItem">The linked <see cref="T:System.Drawing.Design.ToolboxItem" /> to add to the toolbox.</param>
		/// <param name="host">The <see cref="T:System.ComponentModel.Design.IDesignerHost" /> for the current design document.</param>
		// Token: 0x06000080 RID: 128 RVA: 0x0000359B File Offset: 0x0000179B
		void IToolboxService.AddLinkedToolboxItem(ToolboxItem toolboxItem, IDesignerHost host)
		{
			throw new NotImplementedException();
		}

		/// <summary>For a description of this member, see the <see cref="M:System.Drawing.Design.IToolboxService.AddLinkedToolboxItem(System.Drawing.Design.ToolboxItem,System.String,System.ComponentModel.Design.IDesignerHost)" />method.</summary>
		/// <param name="toolboxItem">The linked <see cref="T:System.Drawing.Design.ToolboxItem" /> to add to the toolbox.</param>
		/// <param name="category">The toolbox item category to add the toolbox item to.</param>
		/// <param name="host">The <see cref="T:System.ComponentModel.Design.IDesignerHost" /> for the current design document.</param>
		// Token: 0x06000081 RID: 129 RVA: 0x0000359B File Offset: 0x0000179B
		void IToolboxService.AddLinkedToolboxItem(ToolboxItem toolboxItem, string category, IDesignerHost host)
		{
			throw new NotImplementedException();
		}

		/// <summary>For a description of this member, see the <see cref="M:System.Drawing.Design.IToolboxService.AddToolboxItem(System.Drawing.Design.ToolboxItem,System.String)" /> method.</summary>
		/// <param name="toolboxItem">The <see cref="T:System.Drawing.Design.ToolboxItem" /> to add to the toolbox.</param>
		/// <param name="category">The toolbox item category to add the <see cref="T:System.Drawing.Design.ToolboxItem" /> to.</param>
		// Token: 0x06000082 RID: 130 RVA: 0x0000359B File Offset: 0x0000179B
		void IToolboxService.AddToolboxItem(ToolboxItem toolboxItem, string category)
		{
			throw new NotImplementedException();
		}

		/// <summary>For a description of this member, see the <see cref="M:System.Drawing.Design.IToolboxService.AddToolboxItem(System.Drawing.Design.ToolboxItem)" /> method.</summary>
		/// <param name="toolboxItem">The <see cref="T:System.Drawing.Design.ToolboxItem" /> to add to the toolbox.</param>
		// Token: 0x06000083 RID: 131 RVA: 0x0000359B File Offset: 0x0000179B
		void IToolboxService.AddToolboxItem(ToolboxItem toolboxItem)
		{
			throw new NotImplementedException();
		}

		/// <summary>For a description of this member, see the <see cref="M:System.Drawing.Design.IToolboxService.DeserializeToolboxItem(System.Object)" /> method.</summary>
		/// <returns>The <see cref="T:System.Drawing.Design.ToolboxItem" /> created from deserialization.</returns>
		/// <param name="serializedObject">The object that contains the <see cref="T:System.Drawing.Design.ToolboxItem" /> to retrieve.</param>
		// Token: 0x06000084 RID: 132 RVA: 0x0000359B File Offset: 0x0000179B
		ToolboxItem IToolboxService.DeserializeToolboxItem(object serializedObject)
		{
			throw new NotImplementedException();
		}

		/// <summary>For a description of this member, see the <see cref="M:System.Drawing.Design.IToolboxService.DeserializeToolboxItem(System.Object,System.ComponentModel.Design.IDesignerHost)" /> method.</summary>
		/// <returns>The <see cref="T:System.Drawing.Design.ToolboxItem" /> created from deserialization.</returns>
		/// <param name="serializedObject">The object that contains the <see cref="T:System.Drawing.Design.ToolboxItem" /> to retrieve.</param>
		/// <param name="host">The <see cref="T:System.ComponentModel.Design.IDesignerHost" /> to associate with this <see cref="T:System.Drawing.Design.ToolboxItem" />.</param>
		// Token: 0x06000085 RID: 133 RVA: 0x0000359B File Offset: 0x0000179B
		ToolboxItem IToolboxService.DeserializeToolboxItem(object serializedObject, IDesignerHost host)
		{
			throw new NotImplementedException();
		}

		/// <summary>For a description of this member, see the <see cref="Overload:System.Drawing.Design.IToolboxService.GetSelectedToolboxItem" /> method.</summary>
		/// <returns>The <see cref="T:System.Drawing.Design.ToolboxItem" /> that is currently selected, or null if no toolbox item is currently selected.</returns>
		// Token: 0x06000086 RID: 134 RVA: 0x0000359B File Offset: 0x0000179B
		ToolboxItem IToolboxService.GetSelectedToolboxItem()
		{
			throw new NotImplementedException();
		}

		/// <summary>For a description of this member, see the <see cref="M:System.Drawing.Design.IToolboxService.GetSelectedToolboxItem(System.ComponentModel.Design.IDesignerHost)" /> method.</summary>
		/// <returns>The <see cref="T:System.Drawing.Design.ToolboxItem" /> that is currently selected, or null if no toolbox item is currently selected.</returns>
		/// <param name="host">The <see cref="T:System.ComponentModel.Design.IDesignerHost" /> that the selected tool must be associated with for it to be returned.</param>
		// Token: 0x06000087 RID: 135 RVA: 0x0000359B File Offset: 0x0000179B
		ToolboxItem IToolboxService.GetSelectedToolboxItem(IDesignerHost host)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the entire collection of toolbox items from the toolbox.</summary>
		/// <returns>A <see cref="T:System.Drawing.Design.ToolboxItemCollection" /> that contains the current toolbox items.</returns>
		// Token: 0x06000088 RID: 136 RVA: 0x0000359B File Offset: 0x0000179B
		ToolboxItemCollection IToolboxService.GetToolboxItems()
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the collection of toolbox items that are associated with the specified designer host from the toolbox.</summary>
		/// <returns>A <see cref="T:System.Drawing.Design.ToolboxItemCollection" /> that contains the current toolbox items that are associated with the specified designer host.</returns>
		/// <param name="host">The <see cref="T:System.ComponentModel.Design.IDesignerHost" /> that is associated with the toolbox items to retrieve.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="host" /> is null.</exception>
		// Token: 0x06000089 RID: 137 RVA: 0x0000359B File Offset: 0x0000179B
		ToolboxItemCollection IToolboxService.GetToolboxItems(IDesignerHost host)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets a collection of toolbox items from the toolbox that match the specified category.</summary>
		/// <returns>A <see cref="T:System.Drawing.Design.ToolboxItemCollection" /> that contains the current toolbox items that are associated with the specified category.</returns>
		/// <param name="category">The toolbox item category from which to retrieve all the toolbox items.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="host" /> is null.</exception>
		// Token: 0x0600008A RID: 138 RVA: 0x0000359B File Offset: 0x0000179B
		ToolboxItemCollection IToolboxService.GetToolboxItems(string category)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the collection of toolbox items that are associated with the specified designer host and category from the toolbox.</summary>
		/// <returns>A <see cref="T:System.Drawing.Design.ToolboxItemCollection" /> that contains the current toolbox items that are associated with the specified category and designer host.</returns>
		/// <param name="category">The toolbox item category to retrieve the toolbox items from.</param>
		/// <param name="host">The <see cref="T:System.ComponentModel.Design.IDesignerHost" /> that is associated with the toolbox items to retrieve.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="category " />or <paramref name="host" /> is null.</exception>
		// Token: 0x0600008B RID: 139 RVA: 0x0000359B File Offset: 0x0000179B
		ToolboxItemCollection IToolboxService.GetToolboxItems(string category, IDesignerHost host)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets a value indicating whether the specified object, which represents a serialized toolbox item, matches the specified attributes.</summary>
		/// <returns>true if the object matches the specified attributes; otherwise, false.</returns>
		/// <param name="serializedObject">The object that contains the <see cref="T:System.Drawing.Design.ToolboxItem" /> to retrieve.</param>
		/// <param name="filterAttributes">An <see cref="T:System.Collections.ICollection" /> that contains the attributes to test the serialized object for.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="serializedObject" /> or <paramref name="filterAttributes" /> is null.</exception>
		// Token: 0x0600008C RID: 140 RVA: 0x0000359B File Offset: 0x0000179B
		bool IToolboxService.IsSupported(object serializedObject, ICollection filterAttributes)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets a value indicating whether the specified object, which represents a serialized toolbox item, can be used by the specified designer host.</summary>
		/// <returns>true if the specified object is compatible with the specified designer host; otherwise, false.</returns>
		/// <param name="serializedObject">The object that contains the <see cref="T:System.Drawing.Design.ToolboxItem" /> to retrieve.</param>
		/// <param name="host">The <see cref="T:System.ComponentModel.Design.IDesignerHost" /> to test for support for the <see cref="T:System.Drawing.Design.ToolboxItem" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="serializedObject" /> or <paramref name="host" /> is null.</exception>
		// Token: 0x0600008D RID: 141 RVA: 0x0000359B File Offset: 0x0000179B
		bool IToolboxService.IsSupported(object serializedObject, IDesignerHost host)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets a value indicating whether the specified object is a serialized toolbox item.</summary>
		/// <returns>true if the object contains a toolbox item object; otherwise, false.</returns>
		/// <param name="serializedObject">The object to inspect.</param>
		// Token: 0x0600008E RID: 142 RVA: 0x0000359B File Offset: 0x0000179B
		bool IToolboxService.IsToolboxItem(object serializedObject)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets a value indicating whether the specified object is a serialized toolbox item byusing the specified designer host.</summary>
		/// <returns>true if the object contains a toolbox item object; otherwise, false.</returns>
		/// <param name="serializedObject">The object to inspect.</param>
		/// <param name="host">The <see cref="T:System.ComponentModel.Design.IDesignerHost" /> that is making this request.</param>
		// Token: 0x0600008F RID: 143 RVA: 0x0000359B File Offset: 0x0000179B
		bool IToolboxService.IsToolboxItem(object serializedObject, IDesignerHost host)
		{
			throw new NotImplementedException();
		}

		/// <summary>Refreshes the state of the toolbox items.</summary>
		// Token: 0x06000090 RID: 144 RVA: 0x0000359B File Offset: 0x0000179B
		void IToolboxService.Refresh()
		{
			throw new NotImplementedException();
		}

		/// <summary>Removes a previously added toolbox item creator of the specified data format.</summary>
		/// <param name="format">The data format of the creator to remove.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> is null.</exception>
		// Token: 0x06000091 RID: 145 RVA: 0x0000359B File Offset: 0x0000179B
		void IToolboxService.RemoveCreator(string format)
		{
			throw new NotImplementedException();
		}

		/// <summary>Removes a previously added toolbox creator that is associated with the specified data format and the specified designer host.</summary>
		/// <param name="format">The data format of the creator to remove.</param>
		/// <param name="host">The <see cref="T:System.ComponentModel.Design.IDesignerHost" /> that is associated with the creator to remove.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> or <paramref name="host" /> is null.</exception>
		// Token: 0x06000092 RID: 146 RVA: 0x0000359B File Offset: 0x0000179B
		void IToolboxService.RemoveCreator(string format, IDesignerHost host)
		{
			throw new NotImplementedException();
		}

		/// <summary>Removes the specified toolbox item from the toolbox.</summary>
		/// <param name="toolboxItem">The <see cref="T:System.Drawing.Design.ToolboxItem" /> to remove from the toolbox.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="toolboxItem" /> is null.</exception>
		// Token: 0x06000093 RID: 147 RVA: 0x0000359B File Offset: 0x0000179B
		void IToolboxService.RemoveToolboxItem(ToolboxItem toolboxItem)
		{
			throw new NotImplementedException();
		}

		/// <summary>Removes the specified toolbox item from the toolbox.</summary>
		/// <param name="toolboxItem">The <see cref="T:System.Drawing.Design.ToolboxItem" /> to remove from the toolbox.</param>
		/// <param name="category">The toolbox item category to remove the <see cref="T:System.Drawing.Design.ToolboxItem" /> from.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="toolboxItem" /> or <paramref name="category" /> is null.</exception>
		// Token: 0x06000094 RID: 148 RVA: 0x0000359B File Offset: 0x0000179B
		void IToolboxService.RemoveToolboxItem(ToolboxItem toolboxItem, string category)
		{
			throw new NotImplementedException();
		}

		/// <summary>Notifies the toolbox service that the selected tool has been used.</summary>
		// Token: 0x06000095 RID: 149 RVA: 0x0000359B File Offset: 0x0000179B
		void IToolboxService.SelectedToolboxItemUsed()
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets a serializable object that represents the specified toolbox item.</summary>
		/// <returns>An object that represents the specified <see cref="T:System.Drawing.Design.ToolboxItem" />.</returns>
		/// <param name="toolboxItem">The <see cref="T:System.Drawing.Design.ToolboxItem" /> to serialize.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="toolboxItem" /> is null.</exception>
		// Token: 0x06000096 RID: 150 RVA: 0x0000359B File Offset: 0x0000179B
		object IToolboxService.SerializeToolboxItem(ToolboxItem toolboxItem)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sets the current application's cursor to a cursor that represents the currently selected tool.</summary>
		/// <returns>true if the cursor is set by the currently selected tool; false if there is no tool selected and the cursor is set to the standard Windows cursor.</returns>
		// Token: 0x06000097 RID: 151 RVA: 0x0000359B File Offset: 0x0000179B
		bool IToolboxService.SetCursor()
		{
			throw new NotImplementedException();
		}

		/// <summary>Selects the specified toolbox item.</summary>
		/// <param name="toolboxItem">The <see cref="T:System.Drawing.Design.ToolboxItem" /> to select.</param>
		// Token: 0x06000098 RID: 152 RVA: 0x0000359B File Offset: 0x0000179B
		void IToolboxService.SetSelectedToolboxItem(ToolboxItem toolboxItem)
		{
			throw new NotImplementedException();
		}
	}
}
