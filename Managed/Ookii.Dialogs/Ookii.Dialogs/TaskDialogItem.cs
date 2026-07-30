using System;
using System.Collections;
using System.ComponentModel;
using Ookii.Dialogs.Properties;

namespace Ookii.Dialogs
{
	// Token: 0x0200001F RID: 31
	[ToolboxItem(false)]
	[DesignTimeVisible(false)]
	[DefaultProperty("Text")]
	[DefaultEvent("Click")]
	public abstract class TaskDialogItem : Component
	{
		// Token: 0x06000191 RID: 401 RVA: 0x00007B9B File Offset: 0x00005D9B
		protected TaskDialogItem()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00007BBC File Offset: 0x00005DBC
		protected TaskDialogItem(IContainer container)
		{
			bool flag = container != null;
			if (flag)
			{
				container.Add(this);
			}
			this.InitializeComponent();
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00007BF6 File Offset: 0x00005DF6
		internal TaskDialogItem(int id)
		{
			this.InitializeComponent();
			this._id = id;
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000194 RID: 404 RVA: 0x00007C1C File Offset: 0x00005E1C
		// (set) Token: 0x06000195 RID: 405 RVA: 0x00007C34 File Offset: 0x00005E34
		[Browsable(false)]
		public TaskDialog Owner
		{
			get
			{
				return this._owner;
			}
			internal set
			{
				this._owner = value;
				this.AutoAssignId();
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000196 RID: 406 RVA: 0x00007C48 File Offset: 0x00005E48
		// (set) Token: 0x06000197 RID: 407 RVA: 0x00007C69 File Offset: 0x00005E69
		[Localizable(true)]
		[Category("Appearance")]
		[Description("The text of the item.")]
		[DefaultValue("")]
		public string Text
		{
			get
			{
				return this._text ?? string.Empty;
			}
			set
			{
				this._text = value;
				this.UpdateOwner();
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000198 RID: 408 RVA: 0x00007C7C File Offset: 0x00005E7C
		// (set) Token: 0x06000199 RID: 409 RVA: 0x00007C94 File Offset: 0x00005E94
		[Category("Behavior")]
		[Description("Indicates whether the item is enabled.")]
		[DefaultValue(true)]
		public bool Enabled
		{
			get
			{
				return this._enabled;
			}
			set
			{
				this._enabled = value;
				bool flag = this.Owner != null;
				if (flag)
				{
					this.Owner.SetItemEnabled(this);
				}
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600019A RID: 410 RVA: 0x00007CC8 File Offset: 0x00005EC8
		// (set) Token: 0x0600019B RID: 411 RVA: 0x00007CE0 File Offset: 0x00005EE0
		[Category("Data")]
		[Description("The id of the item.")]
		[DefaultValue(0)]
		internal virtual int Id
		{
			get
			{
				return this._id;
			}
			set
			{
				this.CheckDuplicateId(null, value);
				this._id = value;
				this.UpdateOwner();
			}
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00007CFC File Offset: 0x00005EFC
		public void Click()
		{
			bool flag = this.Owner == null;
			if (flag)
			{
				throw new InvalidOperationException(Resources.NoAssociatedTaskDialogError);
			}
			this.Owner.ClickItem(this);
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x0600019D RID: 413
		protected abstract IEnumerable ItemCollection { get; }

		// Token: 0x0600019E RID: 414 RVA: 0x00007D30 File Offset: 0x00005F30
		protected void UpdateOwner()
		{
			bool flag = this.Owner != null;
			if (flag)
			{
				this.Owner.UpdateDialog();
			}
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00007D57 File Offset: 0x00005F57
		internal virtual void CheckDuplicate(TaskDialogItem itemToExclude)
		{
			this.CheckDuplicateId(itemToExclude, this._id);
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00007D68 File Offset: 0x00005F68
		internal virtual void AutoAssignId()
		{
			bool flag = this.ItemCollection != null;
			if (flag)
			{
				int num = 9;
				foreach (object obj in this.ItemCollection)
				{
					TaskDialogItem taskDialogItem = (TaskDialogItem)obj;
					bool flag2 = taskDialogItem.Id > num;
					if (flag2)
					{
						num = taskDialogItem.Id;
					}
				}
				this.Id = num + 1;
			}
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00007DF4 File Offset: 0x00005FF4
		private void CheckDuplicateId(TaskDialogItem itemToExclude, int id)
		{
			bool flag = id != 0;
			if (flag)
			{
				IEnumerable itemCollection = this.ItemCollection;
				bool flag2 = itemCollection != null;
				if (flag2)
				{
					foreach (object obj in itemCollection)
					{
						TaskDialogItem taskDialogItem = (TaskDialogItem)obj;
						bool flag3 = taskDialogItem != this && taskDialogItem != itemToExclude && taskDialogItem.Id == id;
						if (flag3)
						{
							throw new InvalidOperationException(Resources.DuplicateItemIdError);
						}
					}
				}
			}
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00007E90 File Offset: 0x00006090
		protected override void Dispose(bool disposing)
		{
			try
			{
				bool flag = disposing && this.components != null;
				if (flag)
				{
					this.components.Dispose();
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00007EE0 File Offset: 0x000060E0
		private void InitializeComponent()
		{
			this.components = new Container();
		}

		// Token: 0x0400009C RID: 156
		private TaskDialog _owner;

		// Token: 0x0400009D RID: 157
		private int _id;

		// Token: 0x0400009E RID: 158
		private bool _enabled = true;

		// Token: 0x0400009F RID: 159
		private string _text;

		// Token: 0x040000A0 RID: 160
		private IContainer components = null;
	}
}
