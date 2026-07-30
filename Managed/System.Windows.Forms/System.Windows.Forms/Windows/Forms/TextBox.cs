using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Represents a Windows text box control.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200030F RID: 783
	[ClassInterface(1)]
	[ComVisible(true)]
	[Designer("System.Windows.Forms.Design.TextBoxDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	public class TextBox : TextBoxBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.TextBox" /> class.</summary>
		// Token: 0x060033ED RID: 13293 RVA: 0x000C46D8 File Offset: 0x000C28D8
		public TextBox()
		{
			this.scrollbars = RichTextBoxScrollBars.None;
			this.alignment = HorizontalAlignment.Left;
			base.LostFocus += new EventHandler(this.TextBox_LostFocus);
			base.RightToLeftChanged += new EventHandler(this.TextBox_RightToLeftChanged);
			base.MouseWheel += this.TextBox_MouseWheel;
			this.BackColor = SystemColors.Window;
			this.ForeColor = SystemColors.WindowText;
			this.backcolor_set = false;
			base.SetStyle(ControlStyles.StandardClick | ControlStyles.StandardDoubleClick, false);
			base.SetStyle(ControlStyles.FixedHeight, true);
			this.undo = new MenuItem(Locale.GetText("&Undo"));
			this.cut = new MenuItem(Locale.GetText("Cu&t"));
			this.copy = new MenuItem(Locale.GetText("&Copy"));
			this.paste = new MenuItem(Locale.GetText("&Paste"));
			this.delete = new MenuItem(Locale.GetText("&Delete"));
			this.select_all = new MenuItem(Locale.GetText("Select &All"));
			this.menu = new ContextMenu(new MenuItem[]
			{
				this.undo,
				new MenuItem("-"),
				this.cut,
				this.copy,
				this.paste,
				this.delete,
				new MenuItem("-"),
				this.select_all
			});
			this.ContextMenu = this.menu;
			this.menu.Popup += new EventHandler(this.menu_Popup);
			this.undo.Click += new EventHandler(this.undo_Click);
			this.cut.Click += new EventHandler(this.cut_Click);
			this.copy.Click += new EventHandler(this.copy_Click);
			this.paste.Click += new EventHandler(this.paste_Click);
			this.delete.Click += new EventHandler(this.delete_Click);
			this.select_all.Click += new EventHandler(this.select_all_Click);
			this.document.multiline = false;
		}

		// Token: 0x060033EE RID: 13294 RVA: 0x000C490C File Offset: 0x000C2B0C
		// Note: this type is marked as 'beforefieldinit'.
		static TextBox()
		{
			TextBox.TextAlignChangedEvent = new object();
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.TextBox.TextAlign" /> property has changed.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400032D RID: 813
		// (add) Token: 0x060033EF RID: 13295 RVA: 0x000C4918 File Offset: 0x000C2B18
		// (remove) Token: 0x060033F0 RID: 13296 RVA: 0x000C492C File Offset: 0x000C2B2C
		public event EventHandler TextAlignChanged
		{
			add
			{
				base.Events.AddHandler(TextBox.TextAlignChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(TextBox.TextAlignChangedEvent, value);
			}
		}

		// Token: 0x060033F1 RID: 13297 RVA: 0x000C4940 File Offset: 0x000C2B40
		private void TextBox_RightToLeftChanged(object sender, EventArgs e)
		{
			this.UpdateAlignment();
		}

		// Token: 0x060033F2 RID: 13298 RVA: 0x000C4948 File Offset: 0x000C2B48
		private void TextBox_LostFocus(object sender, EventArgs e)
		{
			if (this.hide_selection)
			{
				this.document.InvalidateSelectionArea();
			}
			if (this.auto_complete_listbox != null && this.auto_complete_listbox.Visible)
			{
				this.auto_complete_listbox.HideListBox(false);
			}
		}

		// Token: 0x060033F3 RID: 13299 RVA: 0x000C4988 File Offset: 0x000C2B88
		private void TextBox_MouseWheel(object o, MouseEventArgs args)
		{
			if (this.auto_complete_listbox == null || !this.auto_complete_listbox.Visible)
			{
				return;
			}
			int num = args.Delta / 120;
			this.auto_complete_listbox.Scroll(-num);
		}

		// Token: 0x060033F4 RID: 13300 RVA: 0x000C49C8 File Offset: 0x000C2BC8
		private void ProcessAutoCompleteInput(ref Message m, bool deleting_chars)
		{
			base.WndProc(ref m);
			this.auto_complete_original_text = this.Text;
			this.ShowAutoCompleteListBox(deleting_chars);
		}

		// Token: 0x060033F5 RID: 13301 RVA: 0x000C49E4 File Offset: 0x000C2BE4
		private void ShowAutoCompleteListBox(bool deleting_chars)
		{
			IList list2;
			if (this.auto_complete_cb_source == null)
			{
				IList list = this.auto_complete_custom_source;
				list2 = list;
			}
			else
			{
				list2 = this.auto_complete_cb_source.Items;
			}
			IList list3 = list2;
			bool flag = this.auto_complete_mode == AutoCompleteMode.Append || this.auto_complete_mode == AutoCompleteMode.SuggestAppend;
			bool flag2 = this.auto_complete_mode == AutoCompleteMode.Suggest || this.auto_complete_mode == AutoCompleteMode.SuggestAppend;
			if (this.Text.Length == 0)
			{
				if (this.auto_complete_listbox != null)
				{
					this.auto_complete_listbox.HideListBox(false);
				}
				return;
			}
			if (this.auto_complete_matches == null)
			{
				this.auto_complete_matches = new List<string>();
			}
			string text = this.Text;
			this.auto_complete_matches.Clear();
			for (int i = 0; i < list3.Count; i++)
			{
				string text2 = ((this.auto_complete_cb_source != null) ? this.auto_complete_cb_source.GetItemText(this.auto_complete_cb_source.Items[i]) : this.auto_complete_custom_source[i]);
				if (text2.StartsWith(text, 1))
				{
					this.auto_complete_matches.Add(text2);
				}
			}
			this.auto_complete_matches.Sort();
			if (this.auto_complete_matches.Count == 0 || (this.auto_complete_matches.Count == 1 && this.auto_complete_matches[0].Equals(text, 1)))
			{
				if (this.auto_complete_listbox != null && this.auto_complete_listbox.Visible)
				{
					this.auto_complete_listbox.HideListBox(false);
				}
				return;
			}
			this.auto_complete_selected_index = ((!flag2) ? 0 : (-1));
			if (flag2)
			{
				if (this.auto_complete_listbox == null)
				{
					this.auto_complete_listbox = new TextBox.AutoCompleteListBox(this);
				}
				this.auto_complete_listbox.Location = base.PointToScreen(new Point(0, base.Height));
				this.auto_complete_listbox.ShowListBox();
			}
			if (flag && !deleting_chars)
			{
				this.AppendAutoCompleteMatch(0);
			}
			this.document.MoveCaret(CaretDirection.End);
		}

		// Token: 0x060033F6 RID: 13302 RVA: 0x000C4BF0 File Offset: 0x000C2DF0
		internal void HideAutoCompleteList()
		{
			if (this.auto_complete_listbox != null)
			{
				this.auto_complete_listbox.HideListBox(false);
			}
		}

		// Token: 0x17000D8D RID: 3469
		// (get) Token: 0x060033F7 RID: 13303 RVA: 0x000C4C0C File Offset: 0x000C2E0C
		internal bool IsAutoCompleteAvailable
		{
			get
			{
				if (this.auto_complete_source == AutoCompleteSource.None || this.auto_complete_mode == AutoCompleteMode.None)
				{
					return false;
				}
				if (this.auto_complete_source != AutoCompleteSource.CustomSource)
				{
					return false;
				}
				IList list2;
				if (this.auto_complete_cb_source == null)
				{
					IList list = this.auto_complete_custom_source;
					list2 = list;
				}
				else
				{
					list2 = this.auto_complete_cb_source.Items;
				}
				IList list3 = list2;
				return list3 != null && list3.Count != 0;
			}
		}

		// Token: 0x17000D8E RID: 3470
		// (get) Token: 0x060033F8 RID: 13304 RVA: 0x000C4C80 File Offset: 0x000C2E80
		// (set) Token: 0x060033F9 RID: 13305 RVA: 0x000C4C88 File Offset: 0x000C2E88
		internal ComboBox AutoCompleteInternalSource
		{
			get
			{
				return this.auto_complete_cb_source;
			}
			set
			{
				this.auto_complete_cb_source = value;
			}
		}

		// Token: 0x17000D8F RID: 3471
		// (get) Token: 0x060033FA RID: 13306 RVA: 0x000C4C94 File Offset: 0x000C2E94
		internal bool CanNavigateAutoCompleteList
		{
			get
			{
				if (this.auto_complete_mode == AutoCompleteMode.None)
				{
					return false;
				}
				if (this.auto_complete_matches == null || this.auto_complete_matches.Count == 0)
				{
					return false;
				}
				bool flag = this.auto_complete_listbox != null && this.auto_complete_listbox.Visible;
				return this.auto_complete_mode != AutoCompleteMode.Suggest || flag;
			}
		}

		// Token: 0x060033FB RID: 13307 RVA: 0x000C4CFC File Offset: 0x000C2EFC
		private bool NavigateAutoCompleteList(Keys key)
		{
			if (this.auto_complete_matches == null || this.auto_complete_matches.Count == 0)
			{
				return false;
			}
			bool flag = this.auto_complete_listbox != null && this.auto_complete_listbox.Visible;
			if (!flag && this.auto_complete_mode == AutoCompleteMode.Suggest)
			{
				return false;
			}
			int num = this.auto_complete_selected_index;
			switch (key)
			{
			case Keys.PageUp:
				if (this.auto_complete_mode != AutoCompleteMode.Append && flag)
				{
					if (num == -1)
					{
						num = this.auto_complete_matches.Count - 1;
					}
					else if (num == 0)
					{
						num = -1;
					}
					else
					{
						num -= this.auto_complete_listbox.page_size - 1;
						if (num < 0)
						{
							num = 0;
						}
					}
					goto IL_0190;
				}
				break;
			case Keys.PageDown:
				if (this.auto_complete_mode == AutoCompleteMode.Append || !flag)
				{
					goto IL_00A0;
				}
				if (num == -1)
				{
					num = 0;
				}
				else if (num == this.auto_complete_matches.Count - 1)
				{
					num = -1;
				}
				else
				{
					num += this.auto_complete_listbox.page_size - 1;
					if (num >= this.auto_complete_matches.Count)
					{
						num = this.auto_complete_matches.Count - 1;
					}
				}
				goto IL_0190;
			case Keys.End:
			case Keys.Home:
			case Keys.Left:
			case Keys.Right:
				goto IL_018B;
			case Keys.Up:
				break;
			case Keys.Down:
				goto IL_00A0;
			default:
				goto IL_018B;
			}
			num--;
			if (num < -1)
			{
				num = this.auto_complete_matches.Count - 1;
			}
			goto IL_0190;
			IL_00A0:
			num++;
			if (num >= this.auto_complete_matches.Count)
			{
				num = -1;
			}
			IL_018B:
			IL_0190:
			bool flag2 = this.auto_complete_mode == AutoCompleteMode.Suggest || this.auto_complete_mode == AutoCompleteMode.SuggestAppend;
			if (flag2 && flag)
			{
				this.Text = ((num != -1) ? this.auto_complete_matches[num] : this.auto_complete_original_text);
				this.auto_complete_listbox.HighlightedIndex = num;
			}
			else
			{
				this.AppendAutoCompleteMatch((num >= 0) ? num : 0);
			}
			this.auto_complete_selected_index = num;
			this.document.MoveCaret(CaretDirection.End);
			return true;
		}

		// Token: 0x060033FC RID: 13308 RVA: 0x000C4F1C File Offset: 0x000C311C
		private void AppendAutoCompleteMatch(int index)
		{
			this.Text = this.auto_complete_original_text + this.auto_complete_matches[index].Substring(this.auto_complete_original_text.Length);
			base.SelectionStart = this.auto_complete_original_text.Length;
			this.SelectionLength = this.auto_complete_matches[index].Length - this.auto_complete_original_text.Length;
		}

		// Token: 0x060033FD RID: 13309 RVA: 0x000C4F8C File Offset: 0x000C318C
		internal virtual void OnAutoCompleteValueSelected(EventArgs args)
		{
		}

		// Token: 0x060033FE RID: 13310 RVA: 0x000C4F90 File Offset: 0x000C3190
		private void UpdateAlignment()
		{
			HorizontalAlignment horizontalAlignment = this.alignment;
			RightToLeft inheritedRtoL = base.GetInheritedRtoL();
			if (inheritedRtoL == RightToLeft.Yes)
			{
				if (horizontalAlignment == HorizontalAlignment.Left)
				{
					horizontalAlignment = HorizontalAlignment.Right;
				}
				else if (horizontalAlignment == HorizontalAlignment.Right)
				{
					horizontalAlignment = HorizontalAlignment.Left;
				}
			}
			this.document.alignment = horizontalAlignment;
			if (this.Multiline)
			{
				if (this.alignment != HorizontalAlignment.Left)
				{
					this.document.Wrap = true;
				}
				else
				{
					this.document.Wrap = this.word_wrap;
				}
			}
			for (int i = 1; i <= this.document.Lines; i++)
			{
				this.document.GetLine(i).Alignment = horizontalAlignment;
			}
			this.document.RecalculateDocument(base.CreateGraphicsInternal());
			base.Invalidate();
		}

		// Token: 0x060033FF RID: 13311 RVA: 0x000C5054 File Offset: 0x000C3254
		internal override Color ChangeBackColor(Color backColor)
		{
			if (backColor == Color.Empty)
			{
				if (!base.ReadOnly)
				{
					backColor = SystemColors.Window;
				}
				this.backcolor_set = false;
			}
			return backColor;
		}

		// Token: 0x06003400 RID: 13312 RVA: 0x000C508C File Offset: 0x000C328C
		private void OnAutoCompleteCustomSourceChanged(object sender, CollectionChangeEventArgs e)
		{
			if (this.auto_complete_source == AutoCompleteSource.CustomSource)
			{
			}
		}

		/// <summary>Gets or sets a custom <see cref="T:System.Collections.Specialized.StringCollection" /> to use when the <see cref="P:System.Windows.Forms.TextBox.AutoCompleteSource" /> property is set to CustomSource.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.StringCollection" /> to use with <see cref="P:System.Windows.Forms.TextBox.AutoCompleteSource" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000D90 RID: 3472
		// (get) Token: 0x06003401 RID: 13313 RVA: 0x000C509C File Offset: 0x000C329C
		// (set) Token: 0x06003402 RID: 13314 RVA: 0x000C50D4 File Offset: 0x000C32D4
		[Localizable(true)]
		[MonoTODO("AutoCompletion algorithm is currently not implemented.")]
		[DesignerSerializationVisibility(2)]
		[EditorBrowsable(0)]
		[Browsable(true)]
		[Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public AutoCompleteStringCollection AutoCompleteCustomSource
		{
			get
			{
				if (this.auto_complete_custom_source == null)
				{
					this.auto_complete_custom_source = new AutoCompleteStringCollection();
					this.auto_complete_custom_source.CollectionChanged += new CollectionChangeEventHandler(this.OnAutoCompleteCustomSourceChanged);
				}
				return this.auto_complete_custom_source;
			}
			set
			{
				if (this.auto_complete_custom_source == value)
				{
					return;
				}
				if (this.auto_complete_custom_source != null)
				{
					this.auto_complete_custom_source.CollectionChanged -= new CollectionChangeEventHandler(this.OnAutoCompleteCustomSourceChanged);
				}
				this.auto_complete_custom_source = value;
				if (this.auto_complete_custom_source != null)
				{
					this.auto_complete_custom_source.CollectionChanged += new CollectionChangeEventHandler(this.OnAutoCompleteCustomSourceChanged);
				}
			}
		}

		/// <summary>Gets or sets an option that controls how automatic completion works for the <see cref="T:System.Windows.Forms.TextBox" />.</summary>
		/// <returns>One of the values of <see cref="T:System.Windows.Forms.AutoCompleteMode" />. The values are <see cref="F:System.Windows.Forms.AutoCompleteMode.Append" />, <see cref="F:System.Windows.Forms.AutoCompleteMode.None" />, <see cref="F:System.Windows.Forms.AutoCompleteMode.Suggest" />, and <see cref="F:System.Windows.Forms.AutoCompleteMode.SuggestAppend" />. The default is <see cref="F:System.Windows.Forms.AutoCompleteMode.None" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value is not one of the values of <see cref="T:System.Windows.Forms.AutoCompleteMode" />. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000D91 RID: 3473
		// (get) Token: 0x06003403 RID: 13315 RVA: 0x000C513C File Offset: 0x000C333C
		// (set) Token: 0x06003404 RID: 13316 RVA: 0x000C5144 File Offset: 0x000C3344
		[DefaultValue(AutoCompleteMode.None)]
		[MonoTODO("AutoCompletion algorithm is currently not implemented.")]
		[Browsable(true)]
		[EditorBrowsable(0)]
		public AutoCompleteMode AutoCompleteMode
		{
			get
			{
				return this.auto_complete_mode;
			}
			set
			{
				if (this.auto_complete_mode == value)
				{
					return;
				}
				if (value < AutoCompleteMode.None || value > AutoCompleteMode.SuggestAppend)
				{
					throw new InvalidEnumArgumentException(Locale.GetText("Enum argument value '{0}' is not valid for AutoCompleteMode", new object[] { value }));
				}
				this.auto_complete_mode = value;
			}
		}

		/// <summary>Gets or sets a value specifying the source of complete strings used for automatic completion.</summary>
		/// <returns>One of the values of <see cref="T:System.Windows.Forms.AutoCompleteSource" />. The options are AllSystemSources, AllUrl, FileSystem, HistoryList, RecentlyUsedList, CustomSource, and None. The default is None.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value is not one of the values of <see cref="T:System.Windows.Forms.AutoCompleteSource" />. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000D92 RID: 3474
		// (get) Token: 0x06003405 RID: 13317 RVA: 0x000C5194 File Offset: 0x000C3394
		// (set) Token: 0x06003406 RID: 13318 RVA: 0x000C519C File Offset: 0x000C339C
		[DefaultValue(AutoCompleteSource.None)]
		[MonoTODO("AutoCompletion algorithm is currently not implemented.")]
		[EditorBrowsable(0)]
		[TypeConverter(typeof(TextBoxAutoCompleteSourceConverter))]
		[Browsable(true)]
		public AutoCompleteSource AutoCompleteSource
		{
			get
			{
				return this.auto_complete_source;
			}
			set
			{
				if (this.auto_complete_source == value)
				{
					return;
				}
				if (!Enum.IsDefined(typeof(AutoCompleteSource), value))
				{
					throw new InvalidEnumArgumentException(Locale.GetText("Enum argument value '{0}' is not valid for AutoCompleteSource", new object[] { value }));
				}
				this.auto_complete_source = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the text in the <see cref="T:System.Windows.Forms.TextBox" /> control should appear as the default password character.</summary>
		/// <returns>true if the text in the <see cref="T:System.Windows.Forms.TextBox" /> control should appear as the default password character; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000D93 RID: 3475
		// (get) Token: 0x06003407 RID: 13319 RVA: 0x000C51F8 File Offset: 0x000C33F8
		// (set) Token: 0x06003408 RID: 13320 RVA: 0x000C5200 File Offset: 0x000C3400
		[DefaultValue(false)]
		[RefreshProperties(2)]
		public bool UseSystemPasswordChar
		{
			get
			{
				return this.use_system_password_char;
			}
			set
			{
				if (this.use_system_password_char != value)
				{
					this.use_system_password_char = value;
					if (!this.Multiline)
					{
						this.document.PasswordChar = this.PasswordChar.ToString();
					}
					else
					{
						this.document.PasswordChar = string.Empty;
					}
					base.Invalidate();
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether pressing ENTER in a multiline <see cref="T:System.Windows.Forms.TextBox" /> control creates a new line of text in the control or activates the default button for the form.</summary>
		/// <returns>true if the ENTER key creates a new line of text in a multiline version of the control; false if the ENTER key activates the default button for the form. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000D94 RID: 3476
		// (get) Token: 0x06003409 RID: 13321 RVA: 0x000C5260 File Offset: 0x000C3460
		// (set) Token: 0x0600340A RID: 13322 RVA: 0x000C5268 File Offset: 0x000C3468
		[MWFCategory("Behavior")]
		[DefaultValue(false)]
		public bool AcceptsReturn
		{
			get
			{
				return this.accepts_return;
			}
			set
			{
				if (value != this.accepts_return)
				{
					this.accepts_return = value;
				}
			}
		}

		/// <summary>Gets or sets whether the <see cref="T:System.Windows.Forms.TextBox" /> control modifies the case of characters as they are typed.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.CharacterCasing" /> enumeration values that specifies whether the <see cref="T:System.Windows.Forms.TextBox" /> control modifies the case of characters. The default is CharacterCasing.Normal.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">A value that is not within the range of valid values for the enumeration was assigned to the property. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000D95 RID: 3477
		// (get) Token: 0x0600340B RID: 13323 RVA: 0x000C5280 File Offset: 0x000C3480
		// (set) Token: 0x0600340C RID: 13324 RVA: 0x000C5288 File Offset: 0x000C3488
		[DefaultValue(CharacterCasing.Normal)]
		[MWFCategory("Behavior")]
		public CharacterCasing CharacterCasing
		{
			get
			{
				return this.character_casing;
			}
			set
			{
				if (value != this.character_casing)
				{
					this.character_casing = value;
				}
			}
		}

		/// <summary>Gets or sets the character used to mask characters of a password in a single-line <see cref="T:System.Windows.Forms.TextBox" /> control.</summary>
		/// <returns>The character used to mask characters entered in a single-line <see cref="T:System.Windows.Forms.TextBox" /> control. Set the value of this property to 0 (character value) if you do not want the control to mask characters as they are typed. Equals 0 (character value) by default.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000D96 RID: 3478
		// (get) Token: 0x0600340D RID: 13325 RVA: 0x000C52A0 File Offset: 0x000C34A0
		// (set) Token: 0x0600340E RID: 13326 RVA: 0x000C52B8 File Offset: 0x000C34B8
		[RefreshProperties(2)]
		[Localizable(true)]
		[DefaultValue('\0')]
		[MWFCategory("Behavior")]
		public char PasswordChar
		{
			get
			{
				if (this.use_system_password_char)
				{
					return '*';
				}
				return this.password_char;
			}
			set
			{
				if (value != this.password_char)
				{
					this.password_char = value;
					if (!this.Multiline)
					{
						this.document.PasswordChar = this.PasswordChar.ToString();
					}
					else
					{
						this.document.PasswordChar = string.Empty;
					}
					base.CalculateDocument();
				}
			}
		}

		/// <summary>Gets or sets which scroll bars should appear in a multiline <see cref="T:System.Windows.Forms.TextBox" /> control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ScrollBars" /> enumeration values that indicates whether a multiline <see cref="T:System.Windows.Forms.TextBox" /> control appears with no scroll bars, a horizontal scroll bar, a vertical scroll bar, or both. The default is ScrollBars.None.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">A value that is not within the range of valid values for the enumeration was assigned to the property. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000D97 RID: 3479
		// (get) Token: 0x0600340F RID: 13327 RVA: 0x000C5318 File Offset: 0x000C3518
		// (set) Token: 0x06003410 RID: 13328 RVA: 0x000C5320 File Offset: 0x000C3520
		[MWFCategory("Appearance")]
		[Localizable(true)]
		[DefaultValue(ScrollBars.None)]
		public ScrollBars ScrollBars
		{
			get
			{
				return (ScrollBars)this.scrollbars;
			}
			set
			{
				if (!Enum.IsDefined(typeof(ScrollBars), value))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(ScrollBars));
				}
				if (value != (ScrollBars)this.scrollbars)
				{
					this.scrollbars = (RichTextBoxScrollBars)value;
					base.CalculateScrollBars();
				}
			}
		}

		/// <summary>Gets or sets the current text in the <see cref="T:System.Windows.Forms.TextBox" />.</summary>
		/// <returns>The text displayed in the control.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000D98 RID: 3480
		// (get) Token: 0x06003411 RID: 13329 RVA: 0x000C5378 File Offset: 0x000C3578
		// (set) Token: 0x06003412 RID: 13330 RVA: 0x000C5380 File Offset: 0x000C3580
		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				base.Text = value;
			}
		}

		/// <summary>Gets or sets how text is aligned in a <see cref="T:System.Windows.Forms.TextBox" /> control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.HorizontalAlignment" /> enumeration values that specifies how text is aligned in the control. The default is HorizontalAlignment.Left.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">A value that is not within the range of valid values for the enumeration was assigned to the property. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000D99 RID: 3481
		// (get) Token: 0x06003413 RID: 13331 RVA: 0x000C538C File Offset: 0x000C358C
		// (set) Token: 0x06003414 RID: 13332 RVA: 0x000C5394 File Offset: 0x000C3594
		[DefaultValue(HorizontalAlignment.Left)]
		[Localizable(true)]
		[MWFCategory("Appearance")]
		public HorizontalAlignment TextAlign
		{
			get
			{
				return this.alignment;
			}
			set
			{
				if (value != this.alignment)
				{
					this.alignment = value;
					this.UpdateAlignment();
					this.OnTextAlignChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Sets the selected text to the specified text without clearing the undo buffer.</summary>
		/// <param name="text">The text to replace.</param>
		// Token: 0x06003415 RID: 13333 RVA: 0x000C53C8 File Offset: 0x000C35C8
		public void Paste(string text)
		{
			this.document.ReplaceSelection(base.CaseAdjust(text), false);
			base.ScrollToCaret();
			this.OnTextChanged(EventArgs.Empty);
		}

		/// <returns>A <see cref="T:System.Windows.Forms.CreateParams" /> representing the information needed when creating a control.</returns>
		// Token: 0x17000D9A RID: 3482
		// (get) Token: 0x06003416 RID: 13334 RVA: 0x000C53FC File Offset: 0x000C35FC
		protected override CreateParams CreateParams
		{
			get
			{
				return base.CreateParams;
			}
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.TextBox" /> and optionally releases the managed resources. </summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06003417 RID: 13335 RVA: 0x000C5404 File Offset: 0x000C3604
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}

		/// <summary>Determines whether the specified key is an input key or a special key that requires preprocessing.</summary>
		/// <returns>true if the specified key is an input key; otherwise, false.</returns>
		/// <param name="keyData">One of the key's values.</param>
		// Token: 0x06003418 RID: 13336 RVA: 0x000C5410 File Offset: 0x000C3610
		protected override bool IsInputKey(Keys keyData)
		{
			return base.IsInputKey(keyData);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.GotFocus" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06003419 RID: 13337 RVA: 0x000C541C File Offset: 0x000C361C
		protected override void OnGotFocus(EventArgs e)
		{
			base.OnGotFocus(e);
			if (this.selection_length == -1 && !this.has_been_focused)
			{
				base.SelectAllNoScroll();
			}
			this.has_been_focused = true;
		}

		// Token: 0x0600341A RID: 13338 RVA: 0x000C544C File Offset: 0x000C364C
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.TextBox.TextAlignChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x0600341B RID: 13339 RVA: 0x000C5458 File Offset: 0x000C3658
		protected virtual void OnTextAlignChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[TextBox.TextAlignChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <param name="m">A Windows Message object. </param>
		// Token: 0x0600341C RID: 13340 RVA: 0x000C548C File Offset: 0x000C368C
		protected override void WndProc(ref Message m)
		{
			Msg msg = (Msg)m.Msg;
			switch (msg)
			{
			case Msg.WM_KEYDOWN:
				if (this.IsAutoCompleteAvailable)
				{
					Keys keys = (Keys)m.WParam.ToInt32();
					Keys keys2 = keys;
					switch (keys2)
					{
					case Keys.PageUp:
					case Keys.PageDown:
					case Keys.Up:
					case Keys.Down:
						if (this.NavigateAutoCompleteList(keys))
						{
							m.Result = IntPtr.Zero;
							return;
						}
						break;
					default:
						if (keys2 != Keys.Return)
						{
							if (keys2 != Keys.Escape)
							{
								if (keys2 == Keys.Delete)
								{
									this.ProcessAutoCompleteInput(ref m, true);
									return;
								}
							}
							else if (this.auto_complete_listbox != null && this.auto_complete_listbox.Visible)
							{
								this.auto_complete_listbox.HideListBox(false);
							}
						}
						else
						{
							if (this.auto_complete_listbox != null && this.auto_complete_listbox.Visible)
							{
								this.auto_complete_listbox.HideListBox(false);
							}
							base.SelectAll();
						}
						break;
					}
				}
				break;
			default:
				if (msg == Msg.WM_LBUTTONDOWN)
				{
					this.has_been_focused = true;
					this.FocusInternal(true);
				}
				break;
			case Msg.WM_CHAR:
				if (this.IsAutoCompleteAvailable)
				{
					int num = m.WParam.ToInt32();
					if (num != 13 && num != 27)
					{
						this.ProcessAutoCompleteInput(ref m, num == 8);
						return;
					}
				}
				break;
			}
			base.WndProc(ref m);
		}

		// Token: 0x17000D9B RID: 3483
		// (get) Token: 0x0600341D RID: 13341 RVA: 0x000C5620 File Offset: 0x000C3820
		// (set) Token: 0x0600341E RID: 13342 RVA: 0x000C5644 File Offset: 0x000C3844
		internal override ContextMenu ContextMenuInternal
		{
			get
			{
				ContextMenu contextMenuInternal = base.ContextMenuInternal;
				if (contextMenuInternal == this.menu)
				{
					return null;
				}
				return contextMenuInternal;
			}
			set
			{
				base.ContextMenuInternal = value;
			}
		}

		// Token: 0x0600341F RID: 13343 RVA: 0x000C5650 File Offset: 0x000C3850
		internal void RestoreContextMenu()
		{
			this.ContextMenuInternal = this.menu;
		}

		// Token: 0x06003420 RID: 13344 RVA: 0x000C5660 File Offset: 0x000C3860
		private void menu_Popup(object sender, EventArgs e)
		{
			if (this.SelectionLength == 0)
			{
				this.cut.Enabled = false;
				this.copy.Enabled = false;
			}
			else
			{
				this.cut.Enabled = true;
				this.copy.Enabled = true;
			}
			if (this.SelectionLength == this.TextLength)
			{
				this.select_all.Enabled = false;
			}
			else
			{
				this.select_all.Enabled = true;
			}
			if (!base.CanUndo)
			{
				this.undo.Enabled = false;
			}
			else
			{
				this.undo.Enabled = true;
			}
			if (base.ReadOnly)
			{
				MenuItem menuItem = this.undo;
				bool flag = false;
				this.delete.Enabled = flag;
				flag = flag;
				this.paste.Enabled = flag;
				flag = flag;
				this.cut.Enabled = flag;
				menuItem.Enabled = flag;
			}
		}

		// Token: 0x06003421 RID: 13345 RVA: 0x000C5744 File Offset: 0x000C3944
		private void undo_Click(object sender, EventArgs e)
		{
			base.Undo();
		}

		// Token: 0x06003422 RID: 13346 RVA: 0x000C574C File Offset: 0x000C394C
		private void cut_Click(object sender, EventArgs e)
		{
			base.Cut();
		}

		// Token: 0x06003423 RID: 13347 RVA: 0x000C5754 File Offset: 0x000C3954
		private void copy_Click(object sender, EventArgs e)
		{
			base.Copy();
		}

		// Token: 0x06003424 RID: 13348 RVA: 0x000C575C File Offset: 0x000C395C
		private void paste_Click(object sender, EventArgs e)
		{
			base.Paste();
		}

		// Token: 0x06003425 RID: 13349 RVA: 0x000C5764 File Offset: 0x000C3964
		private void delete_Click(object sender, EventArgs e)
		{
			this.SelectedText = string.Empty;
		}

		// Token: 0x06003426 RID: 13350 RVA: 0x000C5774 File Offset: 0x000C3974
		private void select_all_Click(object sender, EventArgs e)
		{
			base.SelectAll();
		}

		/// <summary>Gets or sets a value indicating whether this is a multiline <see cref="T:System.Windows.Forms.TextBox" /> control.</summary>
		/// <returns>true if the control is a multiline <see cref="T:System.Windows.Forms.TextBox" /> control; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000D9C RID: 3484
		// (get) Token: 0x06003427 RID: 13351 RVA: 0x000C577C File Offset: 0x000C397C
		// (set) Token: 0x06003428 RID: 13352 RVA: 0x000C5784 File Offset: 0x000C3984
		public override bool Multiline
		{
			get
			{
				return base.Multiline;
			}
			set
			{
				base.Multiline = value;
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.BackColorChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06003429 RID: 13353 RVA: 0x000C5790 File Offset: 0x000C3990
		protected override void OnBackColorChanged(EventArgs e)
		{
			base.OnBackColorChanged(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.FontChanged" /> event. </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x0600342A RID: 13354 RVA: 0x000C579C File Offset: 0x000C399C
		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);
		}

		/// <summary>Raises the <see cref="M:System.Windows.Forms.Control.OnHandleDestroyed(System.EventArgs)" /> event. </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x0600342B RID: 13355 RVA: 0x000C57A8 File Offset: 0x000C39A8
		protected override void OnHandleDestroyed(EventArgs e)
		{
			base.OnHandleDestroyed(e);
		}

		// Token: 0x0400188B RID: 6283
		private ContextMenu menu;

		// Token: 0x0400188C RID: 6284
		private MenuItem undo;

		// Token: 0x0400188D RID: 6285
		private MenuItem cut;

		// Token: 0x0400188E RID: 6286
		private MenuItem copy;

		// Token: 0x0400188F RID: 6287
		private MenuItem paste;

		// Token: 0x04001890 RID: 6288
		private MenuItem delete;

		// Token: 0x04001891 RID: 6289
		private MenuItem select_all;

		// Token: 0x04001892 RID: 6290
		private bool use_system_password_char;

		// Token: 0x04001893 RID: 6291
		private AutoCompleteStringCollection auto_complete_custom_source;

		// Token: 0x04001894 RID: 6292
		private AutoCompleteMode auto_complete_mode;

		// Token: 0x04001895 RID: 6293
		private AutoCompleteSource auto_complete_source = AutoCompleteSource.None;

		// Token: 0x04001896 RID: 6294
		private TextBox.AutoCompleteListBox auto_complete_listbox;

		// Token: 0x04001897 RID: 6295
		private string auto_complete_original_text;

		// Token: 0x04001898 RID: 6296
		private int auto_complete_selected_index = -1;

		// Token: 0x04001899 RID: 6297
		private List<string> auto_complete_matches;

		// Token: 0x0400189A RID: 6298
		private ComboBox auto_complete_cb_source;

		// Token: 0x02000310 RID: 784
		private class AutoCompleteListBox : Control
		{
			// Token: 0x0600342C RID: 13356 RVA: 0x000C57B4 File Offset: 0x000C39B4
			public AutoCompleteListBox(TextBox tb)
			{
				this.owner = tb;
				this.item_height = base.FontHeight + 2;
				this.vscroll = new VScrollBar();
				this.vscroll.ValueChanged += new EventHandler(this.VScrollValueChanged);
				base.Controls.Add(this.vscroll);
				this.is_visible = false;
				base.InternalBorderStyle = BorderStyle.FixedSingle;
			}

			// Token: 0x17000D9D RID: 3485
			// (get) Token: 0x0600342D RID: 13357 RVA: 0x000C5824 File Offset: 0x000C3A24
			protected override CreateParams CreateParams
			{
				get
				{
					CreateParams createParams = base.CreateParams;
					createParams.Style ^= 1073741824;
					createParams.Style ^= 268435456;
					createParams.Style |= int.MinValue;
					createParams.ExStyle |= 136;
					return createParams;
				}
			}

			// Token: 0x17000D9E RID: 3486
			// (get) Token: 0x0600342E RID: 13358 RVA: 0x000C5884 File Offset: 0x000C3A84
			// (set) Token: 0x0600342F RID: 13359 RVA: 0x000C588C File Offset: 0x000C3A8C
			public int HighlightedIndex
			{
				get
				{
					return this.highlighted_index;
				}
				set
				{
					if (value == this.highlighted_index)
					{
						return;
					}
					if (this.highlighted_index != -1)
					{
						base.Invalidate(this.GetItemBounds(this.highlighted_index));
					}
					this.highlighted_index = value;
					if (this.highlighted_index != -1)
					{
						base.Invalidate(this.GetItemBounds(this.highlighted_index));
					}
					if (this.highlighted_index != -1)
					{
						this.EnsureVisible(this.highlighted_index);
					}
				}
			}

			// Token: 0x06003430 RID: 13360 RVA: 0x000C5904 File Offset: 0x000C3B04
			public void Scroll(int lines)
			{
				int num = this.vscroll.Maximum - this.page_size + 1;
				int num2 = this.vscroll.Value + lines;
				if (num2 > num)
				{
					num2 = num;
				}
				else if (num2 < this.vscroll.Minimum)
				{
					num2 = this.vscroll.Minimum;
				}
				this.vscroll.Value = num2;
			}

			// Token: 0x06003431 RID: 13361 RVA: 0x000C596C File Offset: 0x000C3B6C
			public void EnsureVisible(int index)
			{
				if (index < this.top_item)
				{
					this.vscroll.Value = index;
				}
				else
				{
					int num = this.vscroll.Maximum - this.page_size + 1;
					int num2 = base.Height / this.item_height;
					if (index > this.top_item + num2 - 1)
					{
						index = index - num2 + 1;
						this.vscroll.Value = ((index <= num) ? index : num);
					}
				}
			}

			// Token: 0x17000D9F RID: 3487
			// (get) Token: 0x06003432 RID: 13362 RVA: 0x000C59EC File Offset: 0x000C3BEC
			internal override bool ActivateOnShow
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06003433 RID: 13363 RVA: 0x000C59F0 File Offset: 0x000C3BF0
			private void VScrollValueChanged(object o, EventArgs args)
			{
				if (this.top_item == this.vscroll.Value)
				{
					return;
				}
				this.top_item = this.vscroll.Value;
				this.last_item = this.GetLastVisibleItem();
				base.Invalidate();
			}

			// Token: 0x06003434 RID: 13364 RVA: 0x000C5A38 File Offset: 0x000C3C38
			private int GetLastVisibleItem()
			{
				int height = base.Height;
				for (int i = this.top_item; i < this.owner.auto_complete_matches.Count; i++)
				{
					int num = i - this.top_item;
					if (num * this.item_height + this.item_height >= height)
					{
						return i;
					}
				}
				return this.owner.auto_complete_matches.Count - 1;
			}

			// Token: 0x06003435 RID: 13365 RVA: 0x000C5AA4 File Offset: 0x000C3CA4
			private Rectangle GetItemBounds(int index)
			{
				int num = index - this.top_item;
				Rectangle rectangle;
				rectangle..ctor(0, num * this.item_height, base.Width, this.item_height);
				if (this.vscroll.Visible)
				{
					rectangle.Width -= this.vscroll.Width;
				}
				return rectangle;
			}

			// Token: 0x06003436 RID: 13366 RVA: 0x000C5B00 File Offset: 0x000C3D00
			private int GetItemAt(Point loc)
			{
				if (loc.Y > (this.last_item - this.top_item) * this.item_height + this.item_height)
				{
					return -1;
				}
				int num = loc.Y / this.item_height;
				return num + this.top_item;
			}

			// Token: 0x06003437 RID: 13367 RVA: 0x000C5B50 File Offset: 0x000C3D50
			private void LayoutListBox()
			{
				int num = this.owner.auto_complete_matches.Count * this.item_height;
				this.page_size = Math.Max(base.Height / this.item_height, 1);
				this.last_item = this.GetLastVisibleItem();
				if (base.Height < num)
				{
					this.vscroll.Visible = true;
					this.vscroll.Maximum = this.owner.auto_complete_matches.Count - 1;
					this.vscroll.LargeChange = this.page_size;
					this.vscroll.Location = new Point(base.Width - this.vscroll.Width, 0);
					this.vscroll.Height = base.Height - this.item_height;
				}
				else
				{
					this.vscroll.Visible = false;
				}
				this.resizer_bounds = new Rectangle(base.Width - this.item_height, base.Height - this.item_height, this.item_height, this.item_height);
			}

			// Token: 0x06003438 RID: 13368 RVA: 0x000C5C60 File Offset: 0x000C3E60
			public void HideListBox(bool set_text)
			{
				if (set_text)
				{
					this.owner.Text = this.owner.auto_complete_matches[this.HighlightedIndex];
				}
				base.Capture = false;
				base.Hide();
			}

			// Token: 0x06003439 RID: 13369 RVA: 0x000C5CA4 File Offset: 0x000C3EA4
			public void ShowListBox()
			{
				if (!this.user_defined_size)
				{
					int num = ((this.owner.auto_complete_matches.Count <= 7) ? ((this.owner.auto_complete_matches.Count + 1) * this.item_height) : (7 * this.item_height));
					base.Size = new Size(this.owner.Width, num);
				}
				else
				{
					this.LayoutListBox();
				}
				this.vscroll.Value = 0;
				this.HighlightedIndex = -1;
				base.Show();
				XplatUI.SetZOrder(this.Handle, IntPtr.Zero, true, false);
				base.Invalidate();
			}

			// Token: 0x0600343A RID: 13370 RVA: 0x000C5D50 File Offset: 0x000C3F50
			protected override void OnResize(EventArgs args)
			{
				base.OnResize(args);
				this.LayoutListBox();
				this.Refresh();
			}

			// Token: 0x0600343B RID: 13371 RVA: 0x000C5D68 File Offset: 0x000C3F68
			protected override void OnMouseDown(MouseEventArgs args)
			{
				base.OnMouseDown(args);
				if (!this.resizer_bounds.Contains(args.Location))
				{
					return;
				}
				this.user_defined_size = true;
				this.resizing = true;
				base.Capture = true;
			}

			// Token: 0x0600343C RID: 13372 RVA: 0x000C5DA8 File Offset: 0x000C3FA8
			protected override void OnMouseMove(MouseEventArgs args)
			{
				base.OnMouseMove(args);
				if (this.resizing)
				{
					Point mousePosition = Control.MousePosition;
					Point point = base.PointToScreen(Point.Empty);
					Size size;
					size..ctor(mousePosition.X - point.X, mousePosition.Y - point.Y);
					if (size.Height < this.item_height)
					{
						size.Height = this.item_height;
					}
					if (size.Width < this.item_height)
					{
						size.Width = this.item_height;
					}
					base.Size = size;
					return;
				}
				this.Cursor = ((!this.resizer_bounds.Contains(args.Location)) ? Cursors.Default : Cursors.SizeNWSE);
				int itemAt = this.GetItemAt(args.Location);
				if (itemAt != -1)
				{
					this.HighlightedIndex = itemAt;
				}
			}

			// Token: 0x0600343D RID: 13373 RVA: 0x000C5E8C File Offset: 0x000C408C
			protected override void OnMouseUp(MouseEventArgs args)
			{
				base.OnMouseUp(args);
				int itemAt = this.GetItemAt(args.Location);
				if (itemAt != -1 && !this.resizing)
				{
					this.HideListBox(true);
				}
				this.owner.OnAutoCompleteValueSelected(EventArgs.Empty);
				this.resizing = false;
				base.Capture = false;
			}

			// Token: 0x0600343E RID: 13374 RVA: 0x000C5EE4 File Offset: 0x000C40E4
			internal override void OnPaintInternal(PaintEventArgs args)
			{
				Graphics graphics = args.Graphics;
				Brush solidBrush = ThemeEngine.Current.ResPool.GetSolidBrush(this.ForeColor);
				int highlightedIndex = this.HighlightedIndex;
				int num = 0;
				int lastVisibleItem = this.GetLastVisibleItem();
				for (int i = this.top_item; i <= lastVisibleItem; i++)
				{
					Rectangle itemBounds = this.GetItemBounds(i);
					if (itemBounds.IntersectsWith(args.ClipRectangle))
					{
						if (i == highlightedIndex)
						{
							graphics.FillRectangle(SystemBrushes.Highlight, itemBounds);
							graphics.DrawString(this.owner.auto_complete_matches[i], this.Font, SystemBrushes.HighlightText, itemBounds);
						}
						else
						{
							graphics.DrawString(this.owner.auto_complete_matches[i], this.Font, solidBrush, itemBounds);
						}
						num += this.item_height;
					}
				}
				ThemeEngine.Current.CPDrawSizeGrip(graphics, SystemColors.Control, this.resizer_bounds);
			}

			// Token: 0x0400189C RID: 6300
			private const int DefaultDropDownItems = 7;

			// Token: 0x0400189D RID: 6301
			private TextBox owner;

			// Token: 0x0400189E RID: 6302
			private VScrollBar vscroll;

			// Token: 0x0400189F RID: 6303
			private int top_item;

			// Token: 0x040018A0 RID: 6304
			private int last_item;

			// Token: 0x040018A1 RID: 6305
			internal int page_size;

			// Token: 0x040018A2 RID: 6306
			private int item_height;

			// Token: 0x040018A3 RID: 6307
			private int highlighted_index = -1;

			// Token: 0x040018A4 RID: 6308
			private bool user_defined_size;

			// Token: 0x040018A5 RID: 6309
			private bool resizing;

			// Token: 0x040018A6 RID: 6310
			private Rectangle resizer_bounds;
		}
	}
}
