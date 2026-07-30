using System;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace System.ComponentModel.Design
{
	/// <summary>Implements the basic functionality that can be used to design value editors. These editors can, in turn, provide a user interface for representing and editing the values of objects of the supported data types.</summary>
	// Token: 0x02000133 RID: 307
	public abstract class ObjectSelectorEditor : UITypeEditor
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.ObjectSelectorEditor" /> class.</summary>
		// Token: 0x06000907 RID: 2311 RVA: 0x00002050 File Offset: 0x00000250
		public ObjectSelectorEditor()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.ObjectSelectorEditor" /> class.</summary>
		/// <param name="subObjectSelector">The specified sub-object selector value.</param>
		// Token: 0x06000908 RID: 2312 RVA: 0x0000F8BC File Offset: 0x0000DABC
		public ObjectSelectorEditor(bool subObjectSelector)
		{
			this.SubObjectSelector = subObjectSelector;
		}

		/// <summary>Edits the value of the specified object using the editor style indicated by <see cref="Overload:System.ComponentModel.Design.ObjectSelectorEditor.GetEditStyle" />.</summary>
		/// <returns>The new value of the object. If the value of the object has not changed, the method should return the same object it was passed.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to gain additional context information.</param>
		/// <param name="provider">An <see cref="T:System.IServiceProvider" /> that this editor can use to obtain services.</param>
		/// <param name="value">The object to edit.</param>
		// Token: 0x06000909 RID: 2313 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			throw new NotImplementedException();
		}

		/// <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />.</summary>
		/// <returns>true if the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />; otherwise, false.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to compare with the current <see cref="T:System.Object" />.</param>
		// Token: 0x0600090A RID: 2314 RVA: 0x0000F8CB File Offset: 0x0000DACB
		public bool EqualsToValue(object value)
		{
			return this.currValue == value;
		}

		/// <summary>Fills a hierarchical collection of labeled items, with each item represented by a <see cref="T:System.Windows.Forms.TreeNode" />.</summary>
		/// <param name="selector">A hierarchical collection of labeled items.</param>
		/// <param name="context">The context information for a component.</param>
		/// <param name="provider">The <see cref="M:System.IServiceProvider.GetService(System.Type)" /> method of this interface that obtains the object that provides the service.</param>
		// Token: 0x0600090B RID: 2315 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		protected virtual void FillTreeWithData(ObjectSelectorEditor.Selector selector, ITypeDescriptorContext context, IServiceProvider provider)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the editor style used by the <see cref="Overload:System.ComponentModel.Design.ObjectSelectorEditor.EditValue" /> method.</summary>
		/// <returns>A <see cref="T:System.Drawing.Design.UITypeEditorEditStyle" /> value that indicates the style of editor used by <see cref="Overload:System.ComponentModel.Design.ObjectSelectorEditor.EditValue" />.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" />  that can be used to gain additional context information.</param>
		// Token: 0x0600090C RID: 2316 RVA: 0x000020A5 File Offset: 0x000002A5
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.DropDown;
		}

		/// <summary>Sets the current <see cref="T:System.ComponentModel.Design.ObjectSelectorEditor" /> to the specified value.</summary>
		/// <param name="value">The specified value.</param>
		// Token: 0x0600090D RID: 2317 RVA: 0x0000F8D6 File Offset: 0x0000DAD6
		public virtual void SetValue(object value)
		{
			this.currValue = value;
		}

		/// <summary>Represents the current value of <see cref="T:System.ComponentModel.Design.ObjectSelectorEditor" />.</summary>
		// Token: 0x04000205 RID: 517
		protected object currValue;

		/// <summary>Represents the previous value of <see cref="T:System.ComponentModel.Design.ObjectSelectorEditor" />.</summary>
		// Token: 0x04000206 RID: 518
		protected object prevValue;

		/// <summary>Controls whether or not the nodes within the hierarchical collection of labeled items are accessible.</summary>
		// Token: 0x04000207 RID: 519
		public bool SubObjectSelector;

		/// <summary>Displays a hierarchical collection of labeled items, each represented by a <see cref="T:System.Windows.Forms.TreeNode" />.</summary>
		// Token: 0x02000134 RID: 308
		public class Selector : TreeView
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.ObjectSelectorEditor.Selector" /> class.</summary>
			/// <param name="editor">The <see cref="T:System.ComponentModel.Design.ObjectSelectorEditor" />.</param>
			// Token: 0x0600090E RID: 2318 RVA: 0x0000F8DF File Offset: 0x0000DADF
			[MonoTODO]
			public Selector(ObjectSelectorEditor editor)
			{
				throw new NotImplementedException();
			}

			/// <summary>Adds a new tree node to the collection.</summary>
			/// <returns>A <see cref="T:System.ComponentModel.Design.ObjectSelectorEditor.SelectorNode" /> added to the collection. </returns>
			/// <param name="label">The label for the node.</param>
			/// <param name="value">The <see cref="T:System.Object" /> that represents the value for the node.</param>
			/// <param name="parent">The parent of the node.</param>
			// Token: 0x0600090F RID: 2319 RVA: 0x0000234B File Offset: 0x0000054B
			[MonoTODO]
			public ObjectSelectorEditor.SelectorNode AddNode(string label, object value, ObjectSelectorEditor.SelectorNode parent)
			{
				throw new NotImplementedException();
			}

			/// <summary>Removes all tree nodes from the collection.</summary>
			// Token: 0x06000910 RID: 2320 RVA: 0x0000234B File Offset: 0x0000054B
			[MonoTODO]
			public void Clear()
			{
				throw new NotImplementedException();
			}

			/// <summary>Occurs after the tree node is selected.</summary>
			/// <param name="sender">The source of an event.</param>
			/// <param name="e">A <see cref="T:System.Windows.Forms.TreeViewEventArgs" />  that contains the event data.</param>
			// Token: 0x06000911 RID: 2321 RVA: 0x0000234B File Offset: 0x0000054B
			[MonoTODO]
			protected void OnAfterSelect(object sender, TreeViewEventArgs e)
			{
				throw new NotImplementedException();
			}

			/// <summary>Occurs when a key is pressed while the control has focus.</summary>
			/// <param name="e">Provides data for the <see cref="E:System.Windows.Forms.Control.KeyDown" />  event.</param>
			// Token: 0x06000912 RID: 2322 RVA: 0x0000234B File Offset: 0x0000054B
			[MonoTODO]
			protected override void OnKeyDown(KeyEventArgs e)
			{
				throw new NotImplementedException();
			}

			/// <summary>Occurs when a key is pressed while the control has focus.</summary>
			/// <param name="e">Provides data for the <see cref="E:System.Windows.Forms.Control.KeyPress" /> event.</param>
			// Token: 0x06000913 RID: 2323 RVA: 0x0000234B File Offset: 0x0000054B
			[MonoTODO]
			protected override void OnKeyPress(KeyPressEventArgs e)
			{
				throw new NotImplementedException();
			}

			/// <summary>Occurs when the mouse pointer is over the control and a mouse button is clicked.</summary>
			/// <param name="e">Provides data for the <see cref="E:System.Windows.Forms.Control.MouseUp" />, <see cref="E:System.Windows.Forms.Control.MouseDown" />, and <see cref="E:System.Windows.Forms.Control.MouseMove" /> events.</param>
			// Token: 0x06000914 RID: 2324 RVA: 0x0000234B File Offset: 0x0000054B
			[MonoTODO]
			protected override void OnNodeMouseClick(TreeNodeMouseClickEventArgs e)
			{
				throw new NotImplementedException();
			}

			/// <summary>Sets the collection nodes to a specific value.</summary>
			/// <returns>true if the collection nodes were set; otherwise, false.</returns>
			/// <param name="value">The value to be set.</param>
			/// <param name="nodes">The nodes collection.</param>
			// Token: 0x06000915 RID: 2325 RVA: 0x0000234B File Offset: 0x0000054B
			[MonoTODO]
			public bool SetSelection(object value, TreeNodeCollection nodes)
			{
				throw new NotImplementedException();
			}

			/// <summary>Initializes the editor service.</summary>
			/// <param name="edSvc">The editor service.</param>
			/// <param name="value">The value to be set.</param>
			// Token: 0x06000916 RID: 2326 RVA: 0x0000234B File Offset: 0x0000054B
			[MonoTODO]
			public void Start(IWindowsFormsEditorService edSvc, object value)
			{
				throw new NotImplementedException();
			}

			/// <summary>Removes the editor service.</summary>
			// Token: 0x06000917 RID: 2327 RVA: 0x0000234B File Offset: 0x0000054B
			[MonoTODO]
			public void Stop()
			{
				throw new NotImplementedException();
			}

			/// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message" /> to process.</param>
			// Token: 0x06000918 RID: 2328 RVA: 0x0000234B File Offset: 0x0000054B
			[MonoTODO]
			protected override void WndProc(ref Message m)
			{
				throw new NotImplementedException();
			}

			/// <summary>This field is for internal use only.</summary>
			// Token: 0x04000208 RID: 520
			[MonoTODO]
			public bool clickSeen;
		}

		/// <summary>Represents a node of a <see cref="T:System.Windows.Forms.TreeView" />.</summary>
		// Token: 0x02000135 RID: 309
		public class SelectorNode : TreeNode
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.ObjectSelectorEditor.SelectorNode" /> class.</summary>
			/// <param name="label">The label for the node.</param>
			/// <param name="value">The <see cref="T:System.Object" /> that represents the value for the node.</param>
			// Token: 0x06000919 RID: 2329 RVA: 0x0000F8EC File Offset: 0x0000DAEC
			public SelectorNode(string label, object value)
				: base(label)
			{
				this.value = value;
			}

			/// <summary>Represents the value for the node.</summary>
			// Token: 0x04000209 RID: 521
			public object value;
		}
	}
}
