using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Design;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace System.ComponentModel.Design
{
	/// <summary>Provides a user interface that can edit most types of collections at design time.</summary>
	// Token: 0x020000F5 RID: 245
	public class CollectionEditor : UITypeEditor
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.CollectionEditor" /> class using the specified collection type.</summary>
		/// <param name="type">The type of the collection for this editor to edit. </param>
		// Token: 0x060006F0 RID: 1776 RVA: 0x0000A752 File Offset: 0x00008952
		public CollectionEditor(Type type)
		{
			this.type = type;
			this.collectionItemType = this.CreateCollectionItemType();
			this.newItemTypes = this.CreateNewItemTypes();
		}

		/// <summary>Gets the data type of each item in the collection.</summary>
		/// <returns>The data type of the collection items.</returns>
		// Token: 0x1700019D RID: 413
		// (get) Token: 0x060006F1 RID: 1777 RVA: 0x0000A779 File Offset: 0x00008979
		protected Type CollectionItemType
		{
			get
			{
				return this.collectionItemType;
			}
		}

		/// <summary>Gets the data type of the collection object.</summary>
		/// <returns>The data type of the collection object.</returns>
		// Token: 0x1700019E RID: 414
		// (get) Token: 0x060006F2 RID: 1778 RVA: 0x0000A781 File Offset: 0x00008981
		protected Type CollectionType
		{
			get
			{
				return this.type;
			}
		}

		/// <summary>Gets a type descriptor that indicates the current context.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that indicates the context currently in use, or null if no context is available.</returns>
		// Token: 0x1700019F RID: 415
		// (get) Token: 0x060006F3 RID: 1779 RVA: 0x0000A789 File Offset: 0x00008989
		protected ITypeDescriptorContext Context
		{
			get
			{
				return this.context;
			}
		}

		/// <summary>Gets the Help keyword to display the Help topic or topic list for when the editor's dialog box Help button or the F1 key is pressed.</summary>
		/// <returns>The Help keyword to display the Help topic or topic list for when Help is requested from the editor.</returns>
		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x060006F4 RID: 1780 RVA: 0x0000A791 File Offset: 0x00008991
		protected virtual string HelpTopic
		{
			get
			{
				return "CollectionEditor";
			}
		}

		/// <summary>Gets the available types of items that can be created for this collection.</summary>
		/// <returns>The types of items that can be created.</returns>
		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x060006F5 RID: 1781 RVA: 0x0000A798 File Offset: 0x00008998
		protected Type[] NewItemTypes
		{
			get
			{
				return this.newItemTypes;
			}
		}

		/// <summary>Cancels changes to the collection.</summary>
		// Token: 0x060006F6 RID: 1782 RVA: 0x00002432 File Offset: 0x00000632
		protected virtual void CancelChanges()
		{
		}

		/// <summary>Indicates whether original members of the collection can be removed.</summary>
		/// <returns>true if it is permissible to remove this value from the collection; otherwise, false. The default implementation always returns true.</returns>
		/// <param name="value">The value to remove. </param>
		// Token: 0x060006F7 RID: 1783 RVA: 0x000023D8 File Offset: 0x000005D8
		protected virtual bool CanRemoveInstance(object value)
		{
			return true;
		}

		/// <summary>Indicates whether multiple collection items can be selected at once.</summary>
		/// <returns>true if it multiple collection members can be selected at the same time; otherwise, false. By default, this returns true.</returns>
		// Token: 0x060006F8 RID: 1784 RVA: 0x000023D8 File Offset: 0x000005D8
		protected virtual bool CanSelectMultipleInstances()
		{
			return true;
		}

		/// <summary>Creates a new form to display and edit the current collection.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.Design.CollectionEditor.CollectionForm" /> to provide as the user interface for editing the collection.</returns>
		// Token: 0x060006F9 RID: 1785 RVA: 0x0000A7A0 File Offset: 0x000089A0
		protected virtual CollectionEditor.CollectionForm CreateCollectionForm()
		{
			return new CollectionEditor.ConcreteCollectionForm(this);
		}

		/// <summary>Gets the data type that this collection contains.</summary>
		/// <returns>The data type of the items in the collection, or an <see cref="T:System.Object" /> if no Item property can be located on the collection.</returns>
		// Token: 0x060006FA RID: 1786 RVA: 0x0000A7A8 File Offset: 0x000089A8
		protected virtual Type CreateCollectionItemType()
		{
			foreach (PropertyInfo propertyInfo in this.type.GetProperties())
			{
				if (propertyInfo.Name == "Item")
				{
					return propertyInfo.PropertyType;
				}
			}
			return typeof(object);
		}

		/// <summary>Creates a new instance of the specified collection item type.</summary>
		/// <returns>A new instance of the specified object.</returns>
		/// <param name="itemType">The type of item to create. </param>
		// Token: 0x060006FB RID: 1787 RVA: 0x0000A7F8 File Offset: 0x000089F8
		protected virtual object CreateInstance(Type itemType)
		{
			object obj = null;
			if (typeof(IComponent).IsAssignableFrom(itemType))
			{
				IDesignerHost designerHost = this.GetService(typeof(IDesignerHost)) as IDesignerHost;
				if (designerHost != null)
				{
					obj = designerHost.CreateComponent(itemType);
				}
			}
			if (obj == null)
			{
				obj = TypeDescriptor.CreateInstance(this.provider, itemType, null, null);
			}
			return obj;
		}

		/// <summary>Gets the data types that this collection editor can contain.</summary>
		/// <returns>An array of data types that this collection can contain.</returns>
		// Token: 0x060006FC RID: 1788 RVA: 0x0000A84D File Offset: 0x00008A4D
		protected virtual Type[] CreateNewItemTypes()
		{
			return new Type[] { this.collectionItemType };
		}

		/// <summary>Destroys the specified instance of the object.</summary>
		/// <param name="instance">The object to destroy. </param>
		// Token: 0x060006FD RID: 1789 RVA: 0x0000A860 File Offset: 0x00008A60
		protected virtual void DestroyInstance(object instance)
		{
			IComponent component = instance as IComponent;
			if (component != null)
			{
				IDesignerHost designerHost = this.GetService(typeof(IDesignerHost)) as IDesignerHost;
				if (designerHost != null)
				{
					designerHost.DestroyComponent(component);
				}
			}
		}

		/// <summary>Edits the value of the specified object using the specified service provider and context.</summary>
		/// <returns>The new value of the object. If the value of the object has not changed, this should return the same object it was passed.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to gain additional context information. </param>
		/// <param name="provider">A service provider object through which editing services can be obtained. </param>
		/// <param name="value">The object to edit the value of. </param>
		/// <exception cref="T:System.ComponentModel.Design.CheckoutException">An attempt to check out a file that is checked into a source code management program did not succeed.</exception>
		// Token: 0x060006FE RID: 1790 RVA: 0x0000A898 File Offset: 0x00008A98
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			this.context = context;
			this.provider = provider;
			if (context != null && provider != null)
			{
				this.editorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
				if (this.editorService != null)
				{
					CollectionEditor.CollectionForm collectionForm = this.CreateCollectionForm();
					collectionForm.EditValue = value;
					collectionForm.ShowEditorDialog(this.editorService);
					return collectionForm.EditValue;
				}
			}
			return base.EditValue(context, provider, value);
		}

		/// <summary>Retrieves the display text for the given list item.</summary>
		/// <returns>The display text for <paramref name="value" />.</returns>
		/// <param name="value">The list item for which to retrieve display text.</param>
		// Token: 0x060006FF RID: 1791 RVA: 0x0000A908 File Offset: 0x00008B08
		protected virtual string GetDisplayText(object value)
		{
			if (value == null)
			{
				return string.Empty;
			}
			PropertyInfo property = value.GetType().GetProperty("Name");
			if (property != null)
			{
				string text = property.GetValue(value, null) as string;
				if (text != null && text.Length != 0)
				{
					return text;
				}
			}
			if (Type.GetTypeCode(value.GetType()) == TypeCode.Object)
			{
				return value.GetType().Name;
			}
			return value.ToString();
		}

		/// <summary>Gets the edit style used by the <see cref="M:System.ComponentModel.Design.CollectionEditor.EditValue(System.ComponentModel.ITypeDescriptorContext,System.IServiceProvider,System.Object)" /> method.</summary>
		/// <returns>A <see cref="T:System.Drawing.Design.UITypeEditorEditStyle" /> enumeration value indicating the provided editing style. If the method is not supported in the specified context, this method will return the <see cref="F:System.Drawing.Design.UITypeEditorEditStyle.None" /> identifier.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to gain additional context information. </param>
		// Token: 0x06000700 RID: 1792 RVA: 0x00004FAC File Offset: 0x000031AC
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.Modal;
		}

		/// <summary>Gets an array of objects containing the specified collection.</summary>
		/// <returns>An array containing the collection objects, or an empty object array if the specified collection does not inherit from <see cref="T:System.Collections.ICollection" />.</returns>
		/// <param name="editValue">The collection to edit. </param>
		// Token: 0x06000701 RID: 1793 RVA: 0x0000A974 File Offset: 0x00008B74
		protected virtual object[] GetItems(object editValue)
		{
			if (editValue == null)
			{
				return new object[0];
			}
			ICollection collection = editValue as ICollection;
			if (collection == null)
			{
				return new object[0];
			}
			object[] array = new object[collection.Count];
			collection.CopyTo(array, 0);
			return array;
		}

		/// <summary>Returns a list containing the given object</summary>
		/// <returns>An <see cref="T:System.Collections.ArrayList" /> which contains the individual objects to be created.</returns>
		/// <param name="instance">An <see cref="T:System.Collections.ArrayList" /> returned as an object.</param>
		// Token: 0x06000702 RID: 1794 RVA: 0x0000A9B1 File Offset: 0x00008BB1
		protected virtual IList GetObjectsFromInstance(object instance)
		{
			return new ArrayList { instance };
		}

		/// <summary>Gets the requested service, if it is available.</summary>
		/// <returns>An instance of the service, or null if the service cannot be found.</returns>
		/// <param name="serviceType">The type of service to retrieve. </param>
		// Token: 0x06000703 RID: 1795 RVA: 0x0000A9C0 File Offset: 0x00008BC0
		protected object GetService(Type serviceType)
		{
			return this.context.GetService(serviceType);
		}

		/// <summary>Sets the specified array as the items of the collection.</summary>
		/// <returns>The newly created collection object or, otherwise, the collection indicated by the <paramref name="editValue" /> parameter.</returns>
		/// <param name="editValue">The collection to edit. </param>
		/// <param name="value">An array of objects to set as the collection items. </param>
		// Token: 0x06000704 RID: 1796 RVA: 0x0000A9D0 File Offset: 0x00008BD0
		protected virtual object SetItems(object editValue, object[] value)
		{
			IList list = (IList)editValue;
			if (list == null)
			{
				return null;
			}
			list.Clear();
			foreach (object obj in value)
			{
				list.Add(obj);
			}
			return list;
		}

		/// <summary>Displays the default Help topic for the collection editor.</summary>
		// Token: 0x06000705 RID: 1797 RVA: 0x0000AA0C File Offset: 0x00008C0C
		protected virtual void ShowHelp()
		{
			Help.ShowHelp(null, "", this.HelpTopic);
		}

		// Token: 0x04000166 RID: 358
		private Type type;

		// Token: 0x04000167 RID: 359
		private Type collectionItemType;

		// Token: 0x04000168 RID: 360
		private Type[] newItemTypes;

		// Token: 0x04000169 RID: 361
		private ITypeDescriptorContext context;

		// Token: 0x0400016A RID: 362
		private IServiceProvider provider;

		// Token: 0x0400016B RID: 363
		private IWindowsFormsEditorService editorService;

		/// <summary>Provides a modal dialog box for editing the contents of a collection using a <see cref="T:System.Drawing.Design.UITypeEditor" />.</summary>
		// Token: 0x020000F6 RID: 246
		protected abstract class CollectionForm : Form
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.CollectionEditor.CollectionForm" /> class.</summary>
			/// <param name="editor">The <see cref="T:System.ComponentModel.Design.CollectionEditor" /> to use for editing the collection. </param>
			// Token: 0x06000706 RID: 1798 RVA: 0x0000AA1F File Offset: 0x00008C1F
			public CollectionForm(CollectionEditor editor)
			{
				this.editor = editor;
			}

			/// <summary>Gets the data type of each item in the collection.</summary>
			/// <returns>The data type of the collection items.</returns>
			// Token: 0x170001A2 RID: 418
			// (get) Token: 0x06000707 RID: 1799 RVA: 0x0000AA2E File Offset: 0x00008C2E
			protected Type CollectionItemType
			{
				get
				{
					return this.editor.CollectionItemType;
				}
			}

			/// <summary>Gets the data type of the collection object.</summary>
			/// <returns>The data type of the collection object.</returns>
			// Token: 0x170001A3 RID: 419
			// (get) Token: 0x06000708 RID: 1800 RVA: 0x0000AA3B File Offset: 0x00008C3B
			protected Type CollectionType
			{
				get
				{
					return this.editor.CollectionType;
				}
			}

			/// <summary>Gets a type descriptor that indicates the current context.</summary>
			/// <returns>An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that indicates the context currently in use, or null if no context is available.</returns>
			// Token: 0x170001A4 RID: 420
			// (get) Token: 0x06000709 RID: 1801 RVA: 0x0000AA48 File Offset: 0x00008C48
			protected ITypeDescriptorContext Context
			{
				get
				{
					return this.editor.Context;
				}
			}

			/// <summary>Gets or sets the collection object to edit.</summary>
			/// <returns>The collection object to edit.</returns>
			// Token: 0x170001A5 RID: 421
			// (get) Token: 0x0600070A RID: 1802 RVA: 0x0000AA55 File Offset: 0x00008C55
			// (set) Token: 0x0600070B RID: 1803 RVA: 0x0000AA5D File Offset: 0x00008C5D
			public object EditValue
			{
				get
				{
					return this.editValue;
				}
				set
				{
					this.editValue = value;
					this.OnEditValueChanged();
				}
			}

			/// <summary>Gets or sets the array of items for this form to display.</summary>
			/// <returns>An array of objects for the form to display.</returns>
			// Token: 0x170001A6 RID: 422
			// (get) Token: 0x0600070C RID: 1804 RVA: 0x0000AA6C File Offset: 0x00008C6C
			// (set) Token: 0x0600070D RID: 1805 RVA: 0x0000AA80 File Offset: 0x00008C80
			protected object[] Items
			{
				get
				{
					return this.editor.GetItems(this.editValue);
				}
				set
				{
					if (this.editValue == null)
					{
						object obj = null;
						try
						{
							if (typeof(Array).IsAssignableFrom(this.CollectionType))
							{
								obj = Array.CreateInstance(this.CollectionItemType, 0);
							}
							else
							{
								obj = Activator.CreateInstance(this.CollectionType);
							}
						}
						catch
						{
						}
						object obj2 = this.editor.SetItems(obj, value);
						if (obj2 != obj)
						{
							this.EditValue = obj2;
							return;
						}
					}
					else
					{
						object obj3 = this.editor.SetItems(this.editValue, value);
						if (obj3 != this.editValue)
						{
							this.EditValue = obj3;
						}
					}
				}
			}

			/// <summary>Gets the available item types that can be created for this collection.</summary>
			/// <returns>The types of items that can be created.</returns>
			// Token: 0x170001A7 RID: 423
			// (get) Token: 0x0600070E RID: 1806 RVA: 0x0000AB1C File Offset: 0x00008D1C
			protected Type[] NewItemTypes
			{
				get
				{
					return this.editor.NewItemTypes;
				}
			}

			/// <summary>Indicates whether you can remove the original members of the collection.</summary>
			/// <returns>true if it is permissible to remove this value from the collection; otherwise, false. By default, this method returns the value from <see cref="M:System.ComponentModel.Design.CollectionEditor.CanRemoveInstance(System.Object)" /> of the <see cref="T:System.ComponentModel.Design.CollectionEditor" /> for this form.</returns>
			/// <param name="value">The value to remove. </param>
			// Token: 0x0600070F RID: 1807 RVA: 0x0000AB29 File Offset: 0x00008D29
			protected bool CanRemoveInstance(object value)
			{
				return this.editor.CanRemoveInstance(value);
			}

			/// <summary>Indicates whether multiple collection items can be selected at once.</summary>
			/// <returns>true if it multiple collection members can be selected at the same time; otherwise, false. By default, this method returns the value from <see cref="M:System.ComponentModel.Design.CollectionEditor.CanSelectMultipleInstances" /> of the <see cref="T:System.ComponentModel.Design.CollectionEditor" /> for this form.</returns>
			// Token: 0x06000710 RID: 1808 RVA: 0x0000AB37 File Offset: 0x00008D37
			protected virtual bool CanSelectMultipleInstances()
			{
				return this.editor.CanSelectMultipleInstances();
			}

			/// <summary>Creates a new instance of the specified collection item type.</summary>
			/// <returns>A new instance of the specified object, or null if the user chose to cancel the creation of this instance.</returns>
			/// <param name="itemType">The type of item to create. </param>
			// Token: 0x06000711 RID: 1809 RVA: 0x0000AB44 File Offset: 0x00008D44
			protected object CreateInstance(Type itemType)
			{
				return this.editor.CreateInstance(itemType);
			}

			/// <summary>Destroys the specified instance of the object.</summary>
			/// <param name="instance">The object to destroy. </param>
			// Token: 0x06000712 RID: 1810 RVA: 0x0000AB52 File Offset: 0x00008D52
			protected void DestroyInstance(object instance)
			{
				this.editor.DestroyInstance(instance);
			}

			/// <summary>Displays the specified exception to the user.</summary>
			/// <param name="e">The exception to display. </param>
			// Token: 0x06000713 RID: 1811 RVA: 0x0000AB60 File Offset: 0x00008D60
			protected virtual void DisplayError(Exception e)
			{
				MessageBox.Show(e.Message, "Error", 0, 64);
			}

			/// <summary>Gets the requested service, if it is available.</summary>
			/// <returns>An instance of the service, or null if the service cannot be found.</returns>
			/// <param name="serviceType">The type of service to retrieve. </param>
			// Token: 0x06000714 RID: 1812 RVA: 0x0000AB76 File Offset: 0x00008D76
			protected override object GetService(Type serviceType)
			{
				return this.editor.GetService(serviceType);
			}

			/// <summary>Provides an opportunity to perform processing when a collection value has changed.</summary>
			// Token: 0x06000715 RID: 1813
			protected abstract void OnEditValueChanged();

			/// <summary>Shows the dialog box for the collection editor using the specified <see cref="T:System.Windows.Forms.Design.IWindowsFormsEditorService" /> object.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.DialogResult" /> that indicates the result code returned from the dialog box.</returns>
			/// <param name="edSvc">An <see cref="T:System.Windows.Forms.Design.IWindowsFormsEditorService" /> that can be used to show the dialog box. </param>
			// Token: 0x06000716 RID: 1814 RVA: 0x0000AB84 File Offset: 0x00008D84
			protected internal virtual DialogResult ShowEditorDialog(IWindowsFormsEditorService edSvc)
			{
				return edSvc.ShowDialog(this);
			}

			// Token: 0x0400016C RID: 364
			private CollectionEditor editor;

			// Token: 0x0400016D RID: 365
			private object editValue;
		}

		// Token: 0x020000F7 RID: 247
		private class ConcreteCollectionForm : CollectionEditor.CollectionForm
		{
			// Token: 0x06000717 RID: 1815 RVA: 0x0000AB90 File Offset: 0x00008D90
			public ConcreteCollectionForm(CollectionEditor editor)
				: base(editor)
			{
				this.editor = editor;
				this.labelMember = new Label();
				this.labelProperty = new Label();
				this.itemsList = new CollectionEditor.ConcreteCollectionForm.UpdateableListbox();
				this.itemDisplay = new PropertyGrid();
				this.doClose = new Button();
				this.moveUp = new Button();
				this.moveDown = new Button();
				this.doAdd = new Button();
				this.doRemove = new Button();
				this.doCancel = new Button();
				this.addType = new ComboBox();
				base.SuspendLayout();
				this.labelMember.Location = new Point(12, 9);
				this.labelMember.Size = new Size(55, 13);
				this.labelMember.Text = "Members:";
				this.labelProperty.Anchor = 13;
				this.labelProperty.Location = new Point(172, 9);
				this.labelProperty.Size = new Size(347, 13);
				this.labelProperty.Text = "Properties:";
				this.itemsList.Anchor = 7;
				this.itemsList.HorizontalScrollbar = true;
				this.itemsList.Location = new Point(12, 25);
				this.itemsList.SelectionMode = 3;
				this.itemsList.Size = new Size(120, 290);
				this.itemsList.TabIndex = 0;
				this.itemsList.SelectedIndexChanged += this.itemsList_SelectedIndexChanged;
				this.itemDisplay.Anchor = 15;
				this.itemDisplay.HelpVisible = false;
				this.itemDisplay.Location = new Point(175, 25);
				this.itemDisplay.Size = new Size(344, 314);
				this.itemDisplay.TabIndex = 6;
				this.itemDisplay.PropertyValueChanged += new PropertyValueChangedEventHandler(this.itemDisplay_PropertyValueChanged);
				this.doClose.Anchor = 10;
				this.doClose.Location = new Point(341, 345);
				this.doClose.Size = new Size(86, 26);
				this.doClose.TabIndex = 7;
				this.doClose.Text = "OK";
				this.doClose.Click += this.doClose_Click;
				this.moveUp.Location = new Point(138, 25);
				this.moveUp.Size = new Size(31, 28);
				this.moveUp.TabIndex = 4;
				this.moveUp.Enabled = false;
				this.moveUp.Text = "Up";
				this.moveUp.Click += this.moveUp_Click;
				this.moveDown.Location = new Point(138, 59);
				this.moveDown.Size = new Size(31, 28);
				this.moveDown.TabIndex = 5;
				this.moveDown.Enabled = false;
				this.moveDown.Text = "Dn";
				this.moveDown.Click += this.moveDown_Click;
				this.doAdd.Anchor = 6;
				this.doAdd.Location = new Point(12, 346);
				this.doAdd.Size = new Size(59, 25);
				this.doAdd.TabIndex = 1;
				this.doAdd.Text = "Add";
				this.doAdd.Click += this.doAdd_Click;
				this.doRemove.Anchor = 6;
				this.doRemove.Location = new Point(77, 346);
				this.doRemove.Size = new Size(55, 25);
				this.doRemove.TabIndex = 2;
				this.doRemove.Text = "Remove";
				this.doRemove.Click += this.doRemove_Click;
				this.doCancel.Anchor = 10;
				this.doCancel.DialogResult = 2;
				this.doCancel.Location = new Point(433, 345);
				this.doCancel.Size = new Size(86, 26);
				this.doCancel.TabIndex = 8;
				this.doCancel.Text = "Cancel";
				this.doCancel.Click += this.doCancel_Click;
				this.addType.Anchor = 6;
				this.addType.DropDownStyle = 2;
				this.addType.Location = new Point(12, 319);
				this.addType.Size = new Size(120, 21);
				this.addType.TabIndex = 3;
				base.AcceptButton = this.doClose;
				base.CancelButton = this.doCancel;
				base.ClientSize = new Size(531, 381);
				base.ControlBox = false;
				base.Controls.Add(this.addType);
				base.Controls.Add(this.doCancel);
				base.Controls.Add(this.doRemove);
				base.Controls.Add(this.doAdd);
				base.Controls.Add(this.moveDown);
				base.Controls.Add(this.moveUp);
				base.Controls.Add(this.doClose);
				base.Controls.Add(this.itemDisplay);
				base.Controls.Add(this.itemsList);
				base.Controls.Add(this.labelProperty);
				base.Controls.Add(this.labelMember);
				base.HelpButton = true;
				base.MaximizeBox = false;
				base.MinimizeBox = false;
				this.MinimumSize = new Size(400, 300);
				base.ShowInTaskbar = false;
				base.StartPosition = 1;
				base.ResumeLayout(false);
				if (editor.CollectionType.IsGenericType)
				{
					this.Text = editor.CollectionItemType.Name + " Collection Editor";
				}
				else
				{
					this.Text = editor.CollectionType.Name + " Collection Editor";
				}
				foreach (Type type in editor.NewItemTypes)
				{
					this.addType.Items.Add(type.Name);
				}
				if (this.addType.Items.Count > 0)
				{
					this.addType.SelectedIndex = 0;
				}
			}

			// Token: 0x06000718 RID: 1816 RVA: 0x0000B230 File Offset: 0x00009430
			private void UpdateItems()
			{
				object[] items = this.editor.GetItems(base.EditValue);
				if (items != null)
				{
					this.itemsList.BeginUpdate();
					this.itemsList.Items.Clear();
					foreach (object obj in items)
					{
						this.itemsList.Items.Add(new CollectionEditor.ConcreteCollectionForm.ObjectContainer(obj, this.editor));
					}
					if (this.itemsList.Items.Count > 0)
					{
						this.itemsList.SelectedIndex = 0;
					}
					this.itemsList.EndUpdate();
				}
			}

			// Token: 0x06000719 RID: 1817 RVA: 0x0000B2C8 File Offset: 0x000094C8
			private void doClose_Click(object sender, EventArgs e)
			{
				this.SetEditValue();
				base.Close();
			}

			// Token: 0x0600071A RID: 1818 RVA: 0x0000B2D8 File Offset: 0x000094D8
			private void SetEditValue()
			{
				object[] array = new object[this.itemsList.Items.Count];
				for (int i = 0; i < this.itemsList.Items.Count; i++)
				{
					array[i] = ((CollectionEditor.ConcreteCollectionForm.ObjectContainer)this.itemsList.Items[i]).Object;
				}
				base.Items = array;
			}

			// Token: 0x0600071B RID: 1819 RVA: 0x0000B33B File Offset: 0x0000953B
			private void doCancel_Click(object sender, EventArgs e)
			{
				this.editor.CancelChanges();
				base.Close();
			}

			// Token: 0x0600071C RID: 1820 RVA: 0x0000B350 File Offset: 0x00009550
			private void itemsList_SelectedIndexChanged(object sender, EventArgs e)
			{
				if (this.itemsList.SelectedIndex == -1)
				{
					this.itemDisplay.SelectedObject = null;
					return;
				}
				if (this.itemsList.SelectedIndex <= 0 || this.itemsList.SelectedItems.Count > 1)
				{
					this.moveUp.Enabled = false;
				}
				else
				{
					this.moveUp.Enabled = true;
				}
				if (this.itemsList.SelectedIndex > this.itemsList.Items.Count - 2 || this.itemsList.SelectedItems.Count > 1)
				{
					this.moveDown.Enabled = false;
				}
				else
				{
					this.moveDown.Enabled = true;
				}
				if (this.itemsList.SelectedItems.Count == 1)
				{
					CollectionEditor.ConcreteCollectionForm.ObjectContainer objectContainer = (CollectionEditor.ConcreteCollectionForm.ObjectContainer)this.itemsList.SelectedItem;
					if (Type.GetTypeCode(objectContainer.Object.GetType()) != TypeCode.Object)
					{
						this.itemDisplay.SelectedObject = objectContainer;
					}
					else
					{
						this.itemDisplay.SelectedObject = objectContainer.Object;
					}
				}
				else
				{
					object[] array = new object[this.itemsList.SelectedItems.Count];
					for (int i = 0; i < this.itemsList.SelectedItems.Count; i++)
					{
						if (Type.GetTypeCode(((CollectionEditor.ConcreteCollectionForm.ObjectContainer)this.itemsList.SelectedItem).Object.GetType()) != TypeCode.Object)
						{
							array[i] = (CollectionEditor.ConcreteCollectionForm.ObjectContainer)this.itemsList.SelectedItems[i];
						}
						else
						{
							array[i] = ((CollectionEditor.ConcreteCollectionForm.ObjectContainer)this.itemsList.SelectedItems[i]).Object;
						}
					}
					this.itemDisplay.SelectedObjects = array;
				}
				this.labelProperty.Text = ((CollectionEditor.ConcreteCollectionForm.ObjectContainer)this.itemsList.SelectedItem).Name + " properties:";
			}

			// Token: 0x0600071D RID: 1821 RVA: 0x0000B520 File Offset: 0x00009720
			private void itemDisplay_PropertyValueChanged(object sender, EventArgs e)
			{
				int[] array = new int[this.itemsList.SelectedItems.Count];
				for (int i = 0; i < this.itemsList.SelectedItems.Count; i++)
				{
					array[i] = this.itemsList.Items.IndexOf(this.itemsList.SelectedItems[i]);
				}
				this.SetEditValue();
				this.itemsList.BeginUpdate();
				this.itemsList.ClearSelected();
				foreach (int num in array)
				{
					this.itemsList.DoRefreshItem(num);
					this.itemsList.SetSelected(num, true);
				}
				this.itemsList.SelectedIndex = array[0];
				this.itemsList.EndUpdate();
			}

			// Token: 0x0600071E RID: 1822 RVA: 0x0000B5E8 File Offset: 0x000097E8
			private void moveUp_Click(object sender, EventArgs e)
			{
				if (this.itemsList.SelectedIndex <= 0)
				{
					return;
				}
				object selectedItem = this.itemsList.SelectedItem;
				int selectedIndex = this.itemsList.SelectedIndex;
				this.itemsList.Items.RemoveAt(selectedIndex);
				this.itemsList.Items.Insert(selectedIndex - 1, selectedItem);
				this.itemsList.SelectedIndex = selectedIndex - 1;
			}

			// Token: 0x0600071F RID: 1823 RVA: 0x0000B650 File Offset: 0x00009850
			private void moveDown_Click(object sender, EventArgs e)
			{
				if (this.itemsList.SelectedIndex > this.itemsList.Items.Count - 2)
				{
					return;
				}
				object selectedItem = this.itemsList.SelectedItem;
				int selectedIndex = this.itemsList.SelectedIndex;
				this.itemsList.Items.RemoveAt(selectedIndex);
				this.itemsList.Items.Insert(selectedIndex + 1, selectedItem);
				this.itemsList.SelectedIndex = selectedIndex + 1;
			}

			// Token: 0x06000720 RID: 1824 RVA: 0x0000B6C8 File Offset: 0x000098C8
			private void doAdd_Click(object sender, EventArgs e)
			{
				object obj;
				try
				{
					obj = this.editor.CreateInstance(this.editor.NewItemTypes[this.addType.SelectedIndex]);
				}
				catch (Exception ex)
				{
					this.DisplayError(ex);
					return;
				}
				this.itemsList.Items.Add(new CollectionEditor.ConcreteCollectionForm.ObjectContainer(obj, this.editor));
				this.itemsList.SelectedIndex = -1;
				this.itemsList.SelectedIndex = this.itemsList.Items.Count - 1;
			}

			// Token: 0x06000721 RID: 1825 RVA: 0x0000B75C File Offset: 0x0000995C
			private void doRemove_Click(object sender, EventArgs e)
			{
				if (this.itemsList.SelectedIndex != -1)
				{
					int[] array = new int[this.itemsList.SelectedItems.Count];
					for (int i = 0; i < this.itemsList.SelectedItems.Count; i++)
					{
						array[i] = this.itemsList.Items.IndexOf(this.itemsList.SelectedItems[i]);
					}
					for (int j = array.Length - 1; j >= 0; j--)
					{
						this.itemsList.Items.RemoveAt(array[j]);
					}
					this.itemsList.SelectedIndex = Math.Min(array[0], this.itemsList.Items.Count - 1);
				}
			}

			// Token: 0x06000722 RID: 1826 RVA: 0x0000B817 File Offset: 0x00009A17
			protected override void OnEditValueChanged()
			{
				this.UpdateItems();
			}

			// Token: 0x0400016E RID: 366
			private CollectionEditor editor;

			// Token: 0x0400016F RID: 367
			private Label labelMember;

			// Token: 0x04000170 RID: 368
			private Label labelProperty;

			// Token: 0x04000171 RID: 369
			private CollectionEditor.ConcreteCollectionForm.UpdateableListbox itemsList;

			// Token: 0x04000172 RID: 370
			private PropertyGrid itemDisplay;

			// Token: 0x04000173 RID: 371
			private Button doClose;

			// Token: 0x04000174 RID: 372
			private Button moveUp;

			// Token: 0x04000175 RID: 373
			private Button moveDown;

			// Token: 0x04000176 RID: 374
			private Button doAdd;

			// Token: 0x04000177 RID: 375
			private Button doRemove;

			// Token: 0x04000178 RID: 376
			private Button doCancel;

			// Token: 0x04000179 RID: 377
			private ComboBox addType;

			// Token: 0x020000F8 RID: 248
			internal class ObjectContainerConverter : TypeConverter
			{
				// Token: 0x06000723 RID: 1827 RVA: 0x0000B820 File Offset: 0x00009A20
				public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
				{
					CollectionEditor.ConcreteCollectionForm.ObjectContainer objectContainer = (CollectionEditor.ConcreteCollectionForm.ObjectContainer)value;
					CollectionEditor.ConcreteCollectionForm.ObjectContainerConverter.ObjectContainerPropertyDescriptor objectContainerPropertyDescriptor = new CollectionEditor.ConcreteCollectionForm.ObjectContainerConverter.ObjectContainerPropertyDescriptor(value.GetType(), objectContainer.editor.CollectionItemType);
					return new PropertyDescriptorCollection(new PropertyDescriptor[] { objectContainerPropertyDescriptor });
				}

				// Token: 0x06000724 RID: 1828 RVA: 0x000023D8 File Offset: 0x000005D8
				public override bool GetPropertiesSupported(ITypeDescriptorContext context)
				{
					return true;
				}

				// Token: 0x020000F9 RID: 249
				private class ObjectContainerPropertyDescriptor : TypeConverter.SimplePropertyDescriptor
				{
					// Token: 0x06000726 RID: 1830 RVA: 0x0000B85C File Offset: 0x00009A5C
					public ObjectContainerPropertyDescriptor(Type componentType, Type propertyType)
						: base(componentType, "Value", propertyType)
					{
						CategoryAttribute categoryAttribute = new CategoryAttribute(propertyType.Name);
						this.attributes = new AttributeCollection(new Attribute[] { categoryAttribute });
					}

					// Token: 0x06000727 RID: 1831 RVA: 0x0000B897 File Offset: 0x00009A97
					public override object GetValue(object component)
					{
						return ((CollectionEditor.ConcreteCollectionForm.ObjectContainer)component).Object;
					}

					// Token: 0x06000728 RID: 1832 RVA: 0x0000B8A4 File Offset: 0x00009AA4
					public override void SetValue(object component, object value)
					{
						((CollectionEditor.ConcreteCollectionForm.ObjectContainer)component).Object = value;
					}

					// Token: 0x170001A8 RID: 424
					// (get) Token: 0x06000729 RID: 1833 RVA: 0x0000B8B2 File Offset: 0x00009AB2
					public override AttributeCollection Attributes
					{
						get
						{
							return this.attributes;
						}
					}

					// Token: 0x0400017A RID: 378
					private AttributeCollection attributes;
				}
			}

			// Token: 0x020000FA RID: 250
			[TypeConverter(typeof(CollectionEditor.ConcreteCollectionForm.ObjectContainerConverter))]
			private class ObjectContainer
			{
				// Token: 0x0600072A RID: 1834 RVA: 0x0000B8BA File Offset: 0x00009ABA
				public ObjectContainer(object obj, CollectionEditor editor)
				{
					this.Object = obj;
					this.editor = editor;
				}

				// Token: 0x170001A9 RID: 425
				// (get) Token: 0x0600072B RID: 1835 RVA: 0x0000B8D0 File Offset: 0x00009AD0
				internal string Name
				{
					get
					{
						return this.editor.GetDisplayText(this.Object);
					}
				}

				// Token: 0x0600072C RID: 1836 RVA: 0x0000B8E3 File Offset: 0x00009AE3
				public override string ToString()
				{
					return this.Name;
				}

				// Token: 0x0400017B RID: 379
				internal object Object;

				// Token: 0x0400017C RID: 380
				internal CollectionEditor editor;
			}

			// Token: 0x020000FB RID: 251
			private class UpdateableListbox : ListBox
			{
				// Token: 0x0600072D RID: 1837 RVA: 0x0000B8EB File Offset: 0x00009AEB
				public void DoRefreshItem(int index)
				{
					base.RefreshItem(index);
				}
			}
		}
	}
}
