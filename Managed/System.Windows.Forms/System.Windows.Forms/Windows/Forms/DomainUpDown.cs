using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Represents a Windows spin box (also known as an up-down control) that displays string values.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200014D RID: 333
	[ClassInterface(1)]
	[DefaultProperty("Items")]
	[DefaultEvent("SelectedItemChanged")]
	[ComVisible(true)]
	[DefaultBindingProperty("SelectedItem")]
	public class DomainUpDown : UpDownBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DomainUpDown" /> class.</summary>
		// Token: 0x06001704 RID: 5892 RVA: 0x0005540C File Offset: 0x0005360C
		public DomainUpDown()
		{
			this.selected_index = -1;
			this.sorted = false;
			this.wrap = false;
			this.typed_to_index = -1;
			this.items = new DomainUpDown.DomainUpDownItemCollection();
			this.items.CollectionChanged += this.items_CollectionChanged;
			this.txtView.LostFocus += new EventHandler(this.TextBoxLostFocus);
			this.txtView.KeyPress += this.TextBoxKeyDown;
			this.UpdateEditText();
		}

		// Token: 0x06001705 RID: 5893 RVA: 0x000554A0 File Offset: 0x000536A0
		// Note: this type is marked as 'beforefieldinit'.
		static DomainUpDown()
		{
			DomainUpDown.SelectedItemChangedEvent = new object();
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.DomainUpDown.Padding" /> property changes.</summary>
		// Token: 0x14000184 RID: 388
		// (add) Token: 0x06001706 RID: 5894 RVA: 0x000554AC File Offset: 0x000536AC
		// (remove) Token: 0x06001707 RID: 5895 RVA: 0x000554B8 File Offset: 0x000536B8
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new event EventHandler PaddingChanged
		{
			add
			{
				base.PaddingChanged += value;
			}
			remove
			{
				base.PaddingChanged -= value;
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DomainUpDown.SelectedItem" /> property has been changed.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000185 RID: 389
		// (add) Token: 0x06001708 RID: 5896 RVA: 0x000554C4 File Offset: 0x000536C4
		// (remove) Token: 0x06001709 RID: 5897 RVA: 0x000554D8 File Offset: 0x000536D8
		public event EventHandler SelectedItemChanged
		{
			add
			{
				base.Events.AddHandler(DomainUpDown.SelectedItemChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DomainUpDown.SelectedItemChangedEvent, value);
			}
		}

		// Token: 0x0600170A RID: 5898 RVA: 0x000554EC File Offset: 0x000536EC
		internal void items_CollectionChanged(int index, int size_delta)
		{
			bool flag = false;
			if (index == this.selected_index && size_delta <= 0)
			{
				flag = true;
			}
			else if (index <= this.selected_index)
			{
				this.selected_index += size_delta;
			}
			if (this.sorted && index >= 0)
			{
				this.items.PrivSort();
			}
			this.UpdateEditText();
			if (flag)
			{
				this.OnSelectedItemChanged(this, EventArgs.Empty);
			}
		}

		// Token: 0x0600170B RID: 5899 RVA: 0x00055564 File Offset: 0x00053764
		private void go_to_user_input()
		{
			base.UserEdit = false;
			if (this.typed_to_index >= 0)
			{
				this.selected_index = this.typed_to_index;
				this.OnSelectedItemChanged(this, EventArgs.Empty);
			}
		}

		// Token: 0x0600170C RID: 5900 RVA: 0x00055594 File Offset: 0x00053794
		private void TextBoxLostFocus(object source, EventArgs e)
		{
			base.Select(this.txtView.SelectionStart + this.txtView.SelectionLength, 0);
		}

		// Token: 0x0600170D RID: 5901 RVA: 0x000555B4 File Offset: 0x000537B4
		private int SearchTextWithPrefix(char key_char)
		{
			string text = key_char.ToString();
			int num = ((this.selected_index != -1) ? this.selected_index : 0);
			int num2 = ((this.selected_index != -1 && this.selected_index + 1 < this.items.Count) ? (num + 1) : 0);
			for (;;)
			{
				string text2 = this.items[num2].ToString();
				if (string.Compare(text, 0, text2, 0, 1, true) == 0)
				{
					break;
				}
				if (num2 + 1 >= this.items.Count)
				{
					num2 = 0;
				}
				else
				{
					num2++;
				}
				if (num2 == num)
				{
					return -1;
				}
			}
			return num2;
		}

		// Token: 0x0600170E RID: 5902 RVA: 0x00055664 File Offset: 0x00053864
		private bool IsValidInput(char key_char)
		{
			return char.IsLetterOrDigit(key_char) || char.IsNumber(key_char) || char.IsPunctuation(key_char) || char.IsSymbol(key_char) || char.IsWhiteSpace(key_char);
		}

		// Token: 0x0600170F RID: 5903 RVA: 0x0005569C File Offset: 0x0005389C
		private void TextBoxKeyDown(object source, KeyPressEventArgs e)
		{
			if (base.ReadOnly)
			{
				char keyChar = e.KeyChar;
				if (this.IsValidInput(keyChar) && this.items.Count > 0)
				{
					int num = this.SearchTextWithPrefix(keyChar);
					if (num > -1)
					{
						this.SelectedIndex = num;
						e.Handled = true;
					}
				}
				return;
			}
			if (!base.UserEdit)
			{
				this.txtView.SelectionLength = 0;
				this.typed_to_index = -1;
			}
			if (this.txtView.SelectionLength == 0)
			{
				this.txtView.SelectionStart = 0;
			}
			if (this.txtView.SelectionStart != 0)
			{
				return;
			}
			if (e.KeyChar == '\b')
			{
				if (this.txtView.SelectionLength > 0)
				{
					string text = this.txtView.SelectedText.Substring(0, this.txtView.SelectionLength - 1);
					bool flag = false;
					if (this.typed_to_index < 0)
					{
						this.typed_to_index = 0;
					}
					if (this.sorted)
					{
						for (int i = this.typed_to_index; i >= 0; i--)
						{
							int num2 = string.Compare(text, 0, this.items[i].ToString(), 0, text.Length, true);
							if (num2 == 0)
							{
								flag = true;
								this.typed_to_index = i;
							}
							if (num2 > 0)
							{
								break;
							}
						}
					}
					else
					{
						for (int j = 0; j < this.items.Count; j++)
						{
							if (string.Compare(text, 0, this.items[j].ToString(), 0, text.Length, true) == 0)
							{
								flag = true;
								this.typed_to_index = j;
								break;
							}
						}
					}
					base.ChangingText = true;
					if (flag)
					{
						this.Text = this.items[this.typed_to_index].ToString();
					}
					else
					{
						this.Text = text;
					}
					base.Select(0, text.Length);
					base.UserEdit = true;
					e.Handled = true;
				}
			}
			else
			{
				char keyChar2 = e.KeyChar;
				if (this.IsValidInput(keyChar2))
				{
					string text2 = this.txtView.SelectedText + keyChar2;
					bool flag2 = false;
					if (this.typed_to_index < 0)
					{
						this.typed_to_index = 0;
					}
					if (this.sorted)
					{
						for (int k = this.typed_to_index; k < this.items.Count; k++)
						{
							int num3 = string.Compare(text2, 0, this.items[k].ToString(), 0, text2.Length, true);
							if (num3 == 0)
							{
								flag2 = true;
								this.typed_to_index = k;
							}
							if (num3 <= 0)
							{
								break;
							}
						}
					}
					else
					{
						for (int l = 0; l < this.items.Count; l++)
						{
							if (string.Compare(text2, 0, this.items[l].ToString(), 0, text2.Length, true) == 0)
							{
								flag2 = true;
								this.typed_to_index = l;
								break;
							}
						}
					}
					base.ChangingText = true;
					if (flag2)
					{
						this.Text = this.items[this.typed_to_index].ToString();
					}
					else
					{
						this.Text = text2;
					}
					base.Select(0, text2.Length);
					base.UserEdit = true;
					e.Handled = true;
				}
			}
		}

		/// <summary>A collection of objects assigned to the spin box (also known as an up-down control).</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DomainUpDown.DomainUpDownItemCollection" /> that contains an <see cref="T:System.Object" /> collection.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x06001710 RID: 5904 RVA: 0x00055A10 File Offset: 0x00053C10
		[DesignerSerializationVisibility(2)]
		[Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[Localizable(true)]
		public DomainUpDown.DomainUpDownItemCollection Items
		{
			get
			{
				return this.items;
			}
		}

		/// <summary>Gets or sets the spacing between the <see cref="T:System.Windows.Forms.DomainUpDown" /> control's contents and its edges.</summary>
		/// <returns>
		///   <see cref="F:System.Windows.Forms.Padding.Empty" /> in all cases.</returns>
		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x06001711 RID: 5905 RVA: 0x00055A18 File Offset: 0x00053C18
		// (set) Token: 0x06001712 RID: 5906 RVA: 0x00055A20 File Offset: 0x00053C20
		[EditorBrowsable(1)]
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public new Padding Padding
		{
			get
			{
				return Padding.Empty;
			}
			set
			{
			}
		}

		/// <summary>Gets or sets the index value of the selected item.</summary>
		/// <returns>The zero-based index value of the selected item. The default value is -1.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The assigned value is less than the default, -1.-or- The assigned value is greater than the <see cref="P:System.Windows.Forms.DomainUpDown.Items" /> count. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x06001713 RID: 5907 RVA: 0x00055A24 File Offset: 0x00053C24
		// (set) Token: 0x06001714 RID: 5908 RVA: 0x00055A2C File Offset: 0x00053C2C
		[Browsable(false)]
		[DefaultValue(-1)]
		public int SelectedIndex
		{
			get
			{
				return this.selected_index;
			}
			set
			{
				object obj = ((this.selected_index < 0) ? null : this.items[this.selected_index]);
				this.selected_index = value;
				this.UpdateEditText();
				object obj2 = ((this.selected_index < 0) ? null : this.items[this.selected_index]);
				if (!object.ReferenceEquals(obj, obj2))
				{
					this.OnSelectedItemChanged(this, EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets or sets the selected item based on the index value of the selected item in the collection.</summary>
		/// <returns>The selected item based on the <see cref="P:System.Windows.Forms.DomainUpDown.SelectedIndex" /> value. The default value is null.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x06001715 RID: 5909 RVA: 0x00055AA8 File Offset: 0x00053CA8
		// (set) Token: 0x06001716 RID: 5910 RVA: 0x00055ACC File Offset: 0x00053CCC
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		public object SelectedItem
		{
			get
			{
				if (this.selected_index >= 0)
				{
					return this.items[this.selected_index];
				}
				return null;
			}
			set
			{
				this.SelectedIndex = this.items.IndexOf(value);
			}
		}

		/// <summary>Gets or sets a value indicating whether the item collection is sorted.</summary>
		/// <returns>true if the item collection is sorted; otherwise, false. The default value is false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x06001717 RID: 5911 RVA: 0x00055AE0 File Offset: 0x00053CE0
		// (set) Token: 0x06001718 RID: 5912 RVA: 0x00055AE8 File Offset: 0x00053CE8
		[DefaultValue(false)]
		public bool Sorted
		{
			get
			{
				return this.sorted;
			}
			set
			{
				this.sorted = value;
				if (this.sorted)
				{
					this.items.PrivSort();
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the collection of items continues to the first or last item if the user continues past the end of the list.</summary>
		/// <returns>true if the list starts again when the user reaches the beginning or end of the collection; otherwise, false. The default value is false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x06001719 RID: 5913 RVA: 0x00055B08 File Offset: 0x00053D08
		// (set) Token: 0x0600171A RID: 5914 RVA: 0x00055B10 File Offset: 0x00053D10
		[Localizable(true)]
		[DefaultValue(false)]
		public bool Wrap
		{
			get
			{
				return this.wrap;
			}
			set
			{
				this.wrap = value;
			}
		}

		/// <summary>Displays the next item in the object collection.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600171B RID: 5915 RVA: 0x00055B1C File Offset: 0x00053D1C
		public override void DownButton()
		{
			if (base.UserEdit)
			{
				this.go_to_user_input();
			}
			int num = this.selected_index + 1;
			if (num >= this.items.Count)
			{
				if (!this.wrap)
				{
					return;
				}
				num = 0;
			}
			this.SelectedIndex = num;
			base.OnUIADownButtonClick(EventArgs.Empty);
		}

		/// <summary>Returns a string that represents the <see cref="T:System.Windows.Forms.DomainUpDown" /> control.</summary>
		/// <returns>A string that represents the current <see cref="T:System.Windows.Forms.DomainUpDown" />. </returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600171C RID: 5916 RVA: 0x00055B74 File Offset: 0x00053D74
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				base.ToString(),
				", Items.Count: ",
				this.items.Count,
				", SelectedIndex: ",
				this.selected_index
			});
		}

		/// <summary>Displays the previous item in the collection.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600171D RID: 5917 RVA: 0x00055BC8 File Offset: 0x00053DC8
		public override void UpButton()
		{
			if (base.UserEdit)
			{
				this.go_to_user_input();
			}
			int num = this.selected_index - 1;
			if (num < 0)
			{
				if (!this.wrap)
				{
					return;
				}
				num = this.items.Count - 1;
			}
			this.SelectedIndex = num;
			base.OnUIAUpButtonClick(EventArgs.Empty);
		}

		/// <summary>Creates a new accessibility object for the <see cref="T:System.Windows.Forms.DomainUpDown" /> control.</summary>
		/// <returns>A new <see cref="T:System.Windows.Forms.DomainUpDown.DomainUpDownAccessibleObject" /> for the control.</returns>
		// Token: 0x0600171E RID: 5918 RVA: 0x00055C24 File Offset: 0x00053E24
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			return new AccessibleObject(this)
			{
				role = AccessibleRole.SpinButton
			};
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DomainUpDown.SelectedItemChanged" /> event.</summary>
		/// <param name="source">The source of the event.</param>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600171F RID: 5919 RVA: 0x00055C44 File Offset: 0x00053E44
		protected override void OnChanged(object source, EventArgs e)
		{
			base.OnChanged(source, e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DomainUpDown.SelectedItemChanged" /> event.</summary>
		/// <param name="source">The source of the event. </param>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06001720 RID: 5920 RVA: 0x00055C50 File Offset: 0x00053E50
		protected void OnSelectedItemChanged(object source, EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[DomainUpDown.SelectedItemChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Updates the text in the spin box (also known as an up-down control) to display the selected item.</summary>
		// Token: 0x06001721 RID: 5921 RVA: 0x00055C84 File Offset: 0x00053E84
		protected override void UpdateEditText()
		{
			if (this.selected_index >= 0 && this.selected_index < this.items.Count)
			{
				base.ChangingText = true;
				this.Text = this.items[this.selected_index].ToString();
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.KeyPress" /> event. </summary>
		/// <param name="source">The source of the event. </param>
		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyPressEventArgs" /> that contains the event data. </param>
		// Token: 0x06001722 RID: 5922 RVA: 0x00055CD8 File Offset: 0x00053ED8
		protected override void OnTextBoxKeyPress(object source, KeyPressEventArgs e)
		{
			base.OnTextBoxKeyPress(source, e);
		}

		// Token: 0x04000CAE RID: 3246
		private DomainUpDown.DomainUpDownItemCollection items;

		// Token: 0x04000CAF RID: 3247
		private int selected_index = -1;

		// Token: 0x04000CB0 RID: 3248
		private bool sorted;

		// Token: 0x04000CB1 RID: 3249
		private bool wrap;

		// Token: 0x04000CB2 RID: 3250
		private int typed_to_index = -1;

		/// <summary>Provides information about the items in the <see cref="T:System.Windows.Forms.DomainUpDown" /> control to accessibility client applications.</summary>
		// Token: 0x0200014E RID: 334
		[ComVisible(true)]
		public class DomainItemAccessibleObject : AccessibleObject
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DomainUpDown.DomainItemAccessibleObject" /> class.</summary>
			/// <param name="name">The name of the <see cref="T:System.Windows.Forms.DomainUpDown.DomainItemAccessibleObject" />.</param>
			/// <param name="parent">The <see cref="T:System.Windows.Forms.AccessibleObject" /> that contains the items in the <see cref="T:System.Windows.Forms.DomainUpDown" /> control.</param>
			// Token: 0x06001723 RID: 5923 RVA: 0x00055CE4 File Offset: 0x00053EE4
			public DomainItemAccessibleObject(string name, AccessibleObject parent)
			{
				this.name = name;
				this.parent = parent;
			}

			/// <summary>Gets or sets the object name.</summary>
			/// <returns>The object name, or null if the property has not been set.</returns>
			// Token: 0x17000571 RID: 1393
			// (get) Token: 0x06001724 RID: 5924 RVA: 0x00055CFC File Offset: 0x00053EFC
			// (set) Token: 0x06001725 RID: 5925 RVA: 0x00055D04 File Offset: 0x00053F04
			public override string Name
			{
				get
				{
					return base.Name;
				}
				set
				{
					base.Name = value;
				}
			}

			/// <summary>Gets the parent of an accessible object.</summary>
			/// <returns>An <see cref="T:System.Windows.Forms.AccessibleObject" /> that represents the parent of an accessible object, or null if there is no parent object.</returns>
			// Token: 0x17000572 RID: 1394
			// (get) Token: 0x06001726 RID: 5926 RVA: 0x00055D10 File Offset: 0x00053F10
			public override AccessibleObject Parent
			{
				get
				{
					return this.parent;
				}
			}

			/// <summary>Gets the role of this accessible object.</summary>
			/// <returns>The <see cref="F:System.Windows.Forms.AccessibleRole.ListItem" /> value.</returns>
			// Token: 0x17000573 RID: 1395
			// (get) Token: 0x06001727 RID: 5927 RVA: 0x00055D18 File Offset: 0x00053F18
			public override AccessibleRole Role
			{
				get
				{
					return base.Role;
				}
			}

			/// <summary>Gets the state of the <see cref="T:System.Windows.Forms.RadioButton" /> control.</summary>
			/// <returns>If the <see cref="P:System.Windows.Forms.RadioButton.Checked" /> property is set to true, returns <see cref="F:System.Windows.Forms.AccessibleStates.Checked" />.</returns>
			// Token: 0x17000574 RID: 1396
			// (get) Token: 0x06001728 RID: 5928 RVA: 0x00055D20 File Offset: 0x00053F20
			public override AccessibleStates State
			{
				get
				{
					return base.State;
				}
			}

			/// <summary>Gets the value of an accessible object.</summary>
			/// <returns>The Name property of the <see cref="T:System.Windows.Forms.DomainUpDown.DomainItemAccessibleObject" />.</returns>
			// Token: 0x17000575 RID: 1397
			// (get) Token: 0x06001729 RID: 5929 RVA: 0x00055D28 File Offset: 0x00053F28
			public override string Value
			{
				get
				{
					return base.Value;
				}
			}

			// Token: 0x04000CB4 RID: 3252
			private AccessibleObject parent;
		}

		/// <summary>Provides information about the <see cref="T:System.Windows.Forms.DomainUpDown" /> control to accessibility client applications.</summary>
		// Token: 0x0200014F RID: 335
		[ComVisible(true)]
		public class DomainUpDownAccessibleObject : Control.ControlAccessibleObject
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DomainUpDown.DomainUpDownAccessibleObject" /> class. </summary>
			// Token: 0x0600172A RID: 5930 RVA: 0x00055D30 File Offset: 0x00053F30
			public DomainUpDownAccessibleObject(Control owner)
				: base(owner)
			{
			}

			/// <summary>Gets the role of this accessible object.</summary>
			/// <returns>The <see cref="F:System.Windows.Forms.AccessibleRole.ComboBox" /> value.</returns>
			// Token: 0x17000576 RID: 1398
			// (get) Token: 0x0600172B RID: 5931 RVA: 0x00055D3C File Offset: 0x00053F3C
			public override AccessibleRole Role
			{
				get
				{
					return base.Role;
				}
			}

			/// <summary>Gets the accessible child corresponding to the specified index.</summary>
			/// <returns>An <see cref="T:System.Windows.Forms.AccessibleObject" /> that represents the accessible child corresponding to the specified index.</returns>
			/// <param name="index">The zero-based index of the accessible child.</param>
			// Token: 0x0600172C RID: 5932 RVA: 0x00055D44 File Offset: 0x00053F44
			public override AccessibleObject GetChild(int index)
			{
				return base.GetChild(index);
			}

			/// <summary>Retrieves the number of children belonging to an accessible object.</summary>
			/// <returns>Returns 3 in all cases.</returns>
			// Token: 0x0600172D RID: 5933 RVA: 0x00055D50 File Offset: 0x00053F50
			public override int GetChildCount()
			{
				return base.GetChildCount();
			}
		}

		/// <summary>Encapsulates a collection of objects for use by the <see cref="T:System.Windows.Forms.DomainUpDown" /> class.</summary>
		// Token: 0x02000150 RID: 336
		public class DomainUpDownItemCollection : ArrayList
		{
			// Token: 0x0600172E RID: 5934 RVA: 0x00055D58 File Offset: 0x00053F58
			internal DomainUpDownItemCollection()
			{
			}

			// Token: 0x14000186 RID: 390
			// (add) Token: 0x0600172F RID: 5935 RVA: 0x00055D60 File Offset: 0x00053F60
			// (remove) Token: 0x06001730 RID: 5936 RVA: 0x00055D7C File Offset: 0x00053F7C
			internal event DomainUpDown.CollectionChangedEventHandler CollectionChanged;

			/// <summary>Gets or sets the item at the specified indexed location in the collection.</summary>
			/// <returns>An <see cref="T:System.Object" /> that represents the item at the specified indexed location.</returns>
			/// <param name="index">The indexed location of the item in the collection. </param>
			// Token: 0x17000577 RID: 1399
			[Browsable(false)]
			[DesignerSerializationVisibility(0)]
			public override object this[int index]
			{
				get
				{
					return base[index];
				}
				set
				{
					if (value == null)
					{
						throw new ArgumentNullException("value", "Cannot add null values to a DomainUpDownItemCollection");
					}
					base[index] = value;
					this.OnCollectionChanged(index, 0);
				}
			}

			/// <summary>Adds the specified object to the end of the collection.</summary>
			/// <returns>The zero-based index value of the <see cref="T:System.Object" /> added to the collection.</returns>
			/// <param name="item">The <see cref="T:System.Object" /> to be added to the end of the collection. </param>
			// Token: 0x06001733 RID: 5939 RVA: 0x00055DD8 File Offset: 0x00053FD8
			public override int Add(object item)
			{
				if (item == null)
				{
					throw new ArgumentNullException("value", "Cannot add null values to a DomainUpDownItemCollection");
				}
				int num = base.Add(item);
				this.OnCollectionChanged(this.Count - 1, 1);
				return num;
			}

			/// <summary>Inserts the specified object into the collection at the specified location.</summary>
			/// <param name="index">The indexed location within the collection to insert the <see cref="T:System.Object" />. </param>
			/// <param name="item">The <see cref="T:System.Object" /> to insert. </param>
			// Token: 0x06001734 RID: 5940 RVA: 0x00055E14 File Offset: 0x00054014
			public override void Insert(int index, object item)
			{
				if (item == null)
				{
					throw new ArgumentNullException("value", "Cannot add null values to a DomainUpDownItemCollection");
				}
				base.Insert(index, item);
				this.OnCollectionChanged(index, 1);
			}

			/// <summary>Removes the specified item from the collection.</summary>
			/// <param name="item">The <see cref="T:System.Object" /> to remove from the collection. </param>
			// Token: 0x06001735 RID: 5941 RVA: 0x00055E48 File Offset: 0x00054048
			public override void Remove(object item)
			{
				int num = this.IndexOf(item);
				if (num >= 0)
				{
					this.RemoveAt(num);
				}
			}

			/// <summary>Removes the item from the specified location in the collection.</summary>
			/// <param name="item">The indexed location of the <see cref="T:System.Object" /> in the collection. </param>
			// Token: 0x06001736 RID: 5942 RVA: 0x00055E6C File Offset: 0x0005406C
			public override void RemoveAt(int item)
			{
				base.RemoveAt(item);
				this.OnCollectionChanged(item, -1);
			}

			// Token: 0x06001737 RID: 5943 RVA: 0x00055E80 File Offset: 0x00054080
			internal void OnCollectionChanged(int index, int size_delta)
			{
				DomainUpDown.CollectionChangedEventHandler collectionChanged = this.CollectionChanged;
				if (collectionChanged != null)
				{
					collectionChanged(index, size_delta);
				}
			}

			// Token: 0x06001738 RID: 5944 RVA: 0x00055EA4 File Offset: 0x000540A4
			internal void PrivSort()
			{
				base.Sort(new DomainUpDown.DomainUpDownItemCollection.ToStringSorter());
			}

			// Token: 0x02000151 RID: 337
			private class ToStringSorter : IComparer
			{
				// Token: 0x0600173A RID: 5946 RVA: 0x00055EBC File Offset: 0x000540BC
				public int Compare(object x, object y)
				{
					return string.Compare(x.ToString(), y.ToString());
				}
			}
		}

		// Token: 0x02000636 RID: 1590
		// (Invoke) Token: 0x0600508A RID: 20618
		internal delegate void CollectionChangedEventHandler(int index, int size_delta);
	}
}
