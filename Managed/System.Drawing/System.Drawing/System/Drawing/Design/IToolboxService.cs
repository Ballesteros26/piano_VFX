using System;
using System.Collections;
using System.ComponentModel.Design;
using System.Runtime.InteropServices;

namespace System.Drawing.Design
{
	/// <summary>Provides methods and properties to manage and query the toolbox in the development environment.</summary>
	// Token: 0x0200011E RID: 286
	[Guid("4BACD258-DE64-4048-BC4E-FEDBEF9ACB76")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IToolboxService
	{
		/// <summary>Gets the names of all the tool categories currently on the toolbox.</summary>
		/// <returns>A <see cref="T:System.Drawing.Design.CategoryNameCollection" /> containing the tool categories.</returns>
		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06000D47 RID: 3399
		CategoryNameCollection CategoryNames { get; }

		/// <summary>Gets or sets the name of the currently selected tool category from the toolbox.</summary>
		/// <returns>The name of the currently selected category.</returns>
		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06000D48 RID: 3400
		// (set) Token: 0x06000D49 RID: 3401
		string SelectedCategory { get; set; }

		/// <summary>Adds a new toolbox item creator for a specified data format.</summary>
		/// <param name="creator">A <see cref="T:System.Drawing.Design.ToolboxItemCreatorCallback" /> that can create a component when the toolbox item is invoked. </param>
		/// <param name="format">The data format that the creator handles. </param>
		// Token: 0x06000D4A RID: 3402
		void AddCreator(ToolboxItemCreatorCallback creator, string format);

		/// <summary>Adds a new toolbox item creator for a specified data format and designer host.</summary>
		/// <param name="creator">A <see cref="T:System.Drawing.Design.ToolboxItemCreatorCallback" /> that can create a component when the toolbox item is invoked. </param>
		/// <param name="format">The data format that the creator handles. </param>
		/// <param name="host">The <see cref="T:System.ComponentModel.Design.IDesignerHost" /> that represents the designer host to associate with the creator. </param>
		// Token: 0x06000D4B RID: 3403
		void AddCreator(ToolboxItemCreatorCallback creator, string format, IDesignerHost host);

		/// <summary>Adds the specified project-linked toolbox item to the toolbox.</summary>
		/// <param name="toolboxItem">The linked <see cref="T:System.Drawing.Design.ToolboxItem" /> to add to the toolbox. </param>
		/// <param name="host">The <see cref="T:System.ComponentModel.Design.IDesignerHost" /> for the current design document. </param>
		// Token: 0x06000D4C RID: 3404
		void AddLinkedToolboxItem(ToolboxItem toolboxItem, IDesignerHost host);

		/// <summary>Adds the specified project-linked toolbox item to the toolbox in the specified category.</summary>
		/// <param name="toolboxItem">The linked <see cref="T:System.Drawing.Design.ToolboxItem" /> to add to the toolbox. </param>
		/// <param name="category">The toolbox item category to add the toolbox item to. </param>
		/// <param name="host">The <see cref="T:System.ComponentModel.Design.IDesignerHost" /> for the current design document. </param>
		// Token: 0x06000D4D RID: 3405
		void AddLinkedToolboxItem(ToolboxItem toolboxItem, string category, IDesignerHost host);

		/// <summary>Adds the specified toolbox item to the toolbox.</summary>
		/// <param name="toolboxItem">The <see cref="T:System.Drawing.Design.ToolboxItem" /> to add to the toolbox. </param>
		// Token: 0x06000D4E RID: 3406
		void AddToolboxItem(ToolboxItem toolboxItem);

		/// <summary>Adds the specified toolbox item to the toolbox in the specified category.</summary>
		/// <param name="toolboxItem">The <see cref="T:System.Drawing.Design.ToolboxItem" /> to add to the toolbox. </param>
		/// <param name="category">The toolbox item category to add the <see cref="T:System.Drawing.Design.ToolboxItem" /> to. </param>
		// Token: 0x06000D4F RID: 3407
		void AddToolboxItem(ToolboxItem toolboxItem, string category);

		/// <summary>Gets a toolbox item from the specified object that represents a toolbox item in serialized form.</summary>
		/// <returns>The <see cref="T:System.Drawing.Design.ToolboxItem" /> created from the serialized object.</returns>
		/// <param name="serializedObject">The object that contains the <see cref="T:System.Drawing.Design.ToolboxItem" /> to retrieve. </param>
		// Token: 0x06000D50 RID: 3408
		ToolboxItem DeserializeToolboxItem(object serializedObject);

		/// <summary>Gets a toolbox item from the specified object that represents a toolbox item in serialized form, using the specified designer host.</summary>
		/// <returns>The <see cref="T:System.Drawing.Design.ToolboxItem" /> created from deserialization.</returns>
		/// <param name="serializedObject">The object that contains the <see cref="T:System.Drawing.Design.ToolboxItem" /> to retrieve. </param>
		/// <param name="host">The <see cref="T:System.ComponentModel.Design.IDesignerHost" /> to associate with this <see cref="T:System.Drawing.Design.ToolboxItem" />. </param>
		// Token: 0x06000D51 RID: 3409
		ToolboxItem DeserializeToolboxItem(object serializedObject, IDesignerHost host);

		/// <summary>Gets the currently selected toolbox item.</summary>
		/// <returns>The <see cref="T:System.Drawing.Design.ToolboxItem" /> that is currently selected, or null if no toolbox item has been selected.</returns>
		// Token: 0x06000D52 RID: 3410
		ToolboxItem GetSelectedToolboxItem();

		/// <summary>Gets the currently selected toolbox item if it is available to all designers, or if it supports the specified designer.</summary>
		/// <returns>The <see cref="T:System.Drawing.Design.ToolboxItem" /> that is currently selected, or null if no toolbox item is currently selected.</returns>
		/// <param name="host">The <see cref="T:System.ComponentModel.Design.IDesignerHost" /> that the selected tool must be associated with for it to be returned. </param>
		// Token: 0x06000D53 RID: 3411
		ToolboxItem GetSelectedToolboxItem(IDesignerHost host);

		/// <summary>Gets the entire collection of toolbox items from the toolbox.</summary>
		/// <returns>A <see cref="T:System.Drawing.Design.ToolboxItemCollection" /> that contains the current toolbox items.</returns>
		// Token: 0x06000D54 RID: 3412
		ToolboxItemCollection GetToolboxItems();

		/// <summary>Gets the collection of toolbox items that are associated with the specified designer host from the toolbox.</summary>
		/// <returns>A <see cref="T:System.Drawing.Design.ToolboxItemCollection" /> that contains the current toolbox items that are associated with the specified designer host.</returns>
		/// <param name="host">The <see cref="T:System.ComponentModel.Design.IDesignerHost" /> that is associated with the toolbox items to retrieve. </param>
		// Token: 0x06000D55 RID: 3413
		ToolboxItemCollection GetToolboxItems(IDesignerHost host);

		/// <summary>Gets a collection of toolbox items from the toolbox that match the specified category.</summary>
		/// <returns>A <see cref="T:System.Drawing.Design.ToolboxItemCollection" /> that contains the current toolbox items that are associated with the specified category.</returns>
		/// <param name="category">The toolbox item category to retrieve all the toolbox items from. </param>
		// Token: 0x06000D56 RID: 3414
		ToolboxItemCollection GetToolboxItems(string category);

		/// <summary>Gets the collection of toolbox items that are associated with the specified designer host and category from the toolbox.</summary>
		/// <returns>A <see cref="T:System.Drawing.Design.ToolboxItemCollection" /> that contains the current toolbox items that are associated with the specified category and designer host.</returns>
		/// <param name="category">The toolbox item category to retrieve the toolbox items from. </param>
		/// <param name="host">The <see cref="T:System.ComponentModel.Design.IDesignerHost" /> that is associated with the toolbox items to retrieve. </param>
		// Token: 0x06000D57 RID: 3415
		ToolboxItemCollection GetToolboxItems(string category, IDesignerHost host);

		/// <summary>Gets a value indicating whether the specified object which represents a serialized toolbox item can be used by the specified designer host.</summary>
		/// <returns>true if the specified object is compatible with the specified designer host; otherwise, false.</returns>
		/// <param name="serializedObject">The object that contains the <see cref="T:System.Drawing.Design.ToolboxItem" /> to retrieve. </param>
		/// <param name="host">The <see cref="T:System.ComponentModel.Design.IDesignerHost" /> to test for support for the <see cref="T:System.Drawing.Design.ToolboxItem" />. </param>
		// Token: 0x06000D58 RID: 3416
		bool IsSupported(object serializedObject, IDesignerHost host);

		/// <summary>Gets a value indicating whether the specified object which represents a serialized toolbox item matches the specified attributes.</summary>
		/// <returns>true if the object matches the specified attributes; otherwise, false.</returns>
		/// <param name="serializedObject">The object that contains the <see cref="T:System.Drawing.Design.ToolboxItem" /> to retrieve. </param>
		/// <param name="filterAttributes">An <see cref="T:System.Collections.ICollection" /> that contains the attributes to test the serialized object for. </param>
		// Token: 0x06000D59 RID: 3417
		bool IsSupported(object serializedObject, ICollection filterAttributes);

		/// <summary>Gets a value indicating whether the specified object is a serialized toolbox item.</summary>
		/// <returns>true if the object contains a toolbox item object; otherwise, false.</returns>
		/// <param name="serializedObject">The object to inspect. </param>
		// Token: 0x06000D5A RID: 3418
		bool IsToolboxItem(object serializedObject);

		/// <summary>Gets a value indicating whether the specified object is a serialized toolbox item, using the specified designer host.</summary>
		/// <returns>true if the object contains a toolbox item object; otherwise, false.</returns>
		/// <param name="serializedObject">The object to inspect. </param>
		/// <param name="host">The <see cref="T:System.ComponentModel.Design.IDesignerHost" /> that is making this request. </param>
		// Token: 0x06000D5B RID: 3419
		bool IsToolboxItem(object serializedObject, IDesignerHost host);

		/// <summary>Refreshes the state of the toolbox items.</summary>
		// Token: 0x06000D5C RID: 3420
		void Refresh();

		/// <summary>Removes a previously added toolbox item creator of the specified data format.</summary>
		/// <param name="format">The data format of the creator to remove. </param>
		// Token: 0x06000D5D RID: 3421
		void RemoveCreator(string format);

		/// <summary>Removes a previously added toolbox creator that is associated with the specified data format and the specified designer host.</summary>
		/// <param name="format">The data format of the creator to remove. </param>
		/// <param name="host">The <see cref="T:System.ComponentModel.Design.IDesignerHost" /> that is associated with the creator to remove. </param>
		// Token: 0x06000D5E RID: 3422
		void RemoveCreator(string format, IDesignerHost host);

		/// <summary>Removes the specified toolbox item from the toolbox.</summary>
		/// <param name="toolboxItem">The <see cref="T:System.Drawing.Design.ToolboxItem" /> to remove from the toolbox. </param>
		// Token: 0x06000D5F RID: 3423
		void RemoveToolboxItem(ToolboxItem toolboxItem);

		/// <summary>Removes the specified toolbox item from the toolbox.</summary>
		/// <param name="toolboxItem">The <see cref="T:System.Drawing.Design.ToolboxItem" /> to remove from the toolbox. </param>
		/// <param name="category">The toolbox item category to remove the <see cref="T:System.Drawing.Design.ToolboxItem" /> from. </param>
		// Token: 0x06000D60 RID: 3424
		void RemoveToolboxItem(ToolboxItem toolboxItem, string category);

		/// <summary>Notifies the toolbox service that the selected tool has been used.</summary>
		// Token: 0x06000D61 RID: 3425
		void SelectedToolboxItemUsed();

		/// <summary>Gets a serializable object that represents the specified toolbox item.</summary>
		/// <returns>An object that represents the specified <see cref="T:System.Drawing.Design.ToolboxItem" />.</returns>
		/// <param name="toolboxItem">The <see cref="T:System.Drawing.Design.ToolboxItem" /> to serialize. </param>
		// Token: 0x06000D62 RID: 3426
		object SerializeToolboxItem(ToolboxItem toolboxItem);

		/// <summary>Sets the current application's cursor to a cursor that represents the currently selected tool.</summary>
		/// <returns>true if the cursor is set by the currently selected tool, false if there is no tool selected and the cursor is set to the standard windows cursor.</returns>
		// Token: 0x06000D63 RID: 3427
		bool SetCursor();

		/// <summary>Selects the specified toolbox item.</summary>
		/// <param name="toolboxItem">The <see cref="T:System.Drawing.Design.ToolboxItem" /> to select. </param>
		// Token: 0x06000D64 RID: 3428
		void SetSelectedToolboxItem(ToolboxItem toolboxItem);
	}
}
