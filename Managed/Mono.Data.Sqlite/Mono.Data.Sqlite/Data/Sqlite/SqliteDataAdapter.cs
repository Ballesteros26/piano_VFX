using System;
using System.ComponentModel;
using System.Data.Common;

namespace Mono.Data.Sqlite
{
	// Token: 0x0200001E RID: 30
	[DefaultEvent("RowUpdated")]
	[ToolboxItem("SQLite.Designer.SqliteDataAdapterToolboxItem, SQLite.Designer, Version=1.0.36.0, Culture=neutral, PublicKeyToken=db937bc2d44ff139")]
	[Designer("Microsoft.VSDesigner.Data.VS.SqlDataAdapterDesigner, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public sealed class SqliteDataAdapter : DbDataAdapter
	{
		// Token: 0x060001A0 RID: 416 RVA: 0x00009CF5 File Offset: 0x00007EF5
		public SqliteDataAdapter()
		{
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00009CFD File Offset: 0x00007EFD
		public SqliteDataAdapter(SqliteCommand cmd)
		{
			this.SelectCommand = cmd;
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00009D0C File Offset: 0x00007F0C
		public SqliteDataAdapter(string commandText, SqliteConnection connection)
		{
			this.SelectCommand = new SqliteCommand(commandText, connection);
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00009D24 File Offset: 0x00007F24
		public SqliteDataAdapter(string commandText, string connectionString)
		{
			SqliteConnection sqliteConnection = new SqliteConnection(connectionString);
			this.SelectCommand = new SqliteCommand(commandText, sqliteConnection);
		}

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x060001A4 RID: 420 RVA: 0x00009D4C File Offset: 0x00007F4C
		// (remove) Token: 0x060001A5 RID: 421 RVA: 0x00009DB0 File Offset: 0x00007FB0
		public event EventHandler<RowUpdatingEventArgs> RowUpdating
		{
			add
			{
				EventHandler<RowUpdatingEventArgs> eventHandler = (EventHandler<RowUpdatingEventArgs>)base.Events[SqliteDataAdapter._updatingEventPH];
				if (eventHandler != null && value.Target is DbCommandBuilder)
				{
					EventHandler<RowUpdatingEventArgs> eventHandler2 = (EventHandler<RowUpdatingEventArgs>)SqliteDataAdapter.FindBuilder(eventHandler);
					if (eventHandler2 != null)
					{
						base.Events.RemoveHandler(SqliteDataAdapter._updatingEventPH, eventHandler2);
					}
				}
				base.Events.AddHandler(SqliteDataAdapter._updatingEventPH, value);
			}
			remove
			{
				base.Events.RemoveHandler(SqliteDataAdapter._updatingEventPH, value);
			}
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00009DC4 File Offset: 0x00007FC4
		internal static Delegate FindBuilder(MulticastDelegate mcd)
		{
			if (mcd != null)
			{
				Delegate[] invocationList = mcd.GetInvocationList();
				for (int i = 0; i < invocationList.Length; i++)
				{
					if (invocationList[i].Target is DbCommandBuilder)
					{
						return invocationList[i];
					}
				}
			}
			return null;
		}

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x060001A7 RID: 423 RVA: 0x00009DFD File Offset: 0x00007FFD
		// (remove) Token: 0x060001A8 RID: 424 RVA: 0x00009E10 File Offset: 0x00008010
		public event EventHandler<RowUpdatedEventArgs> RowUpdated
		{
			add
			{
				base.Events.AddHandler(SqliteDataAdapter._updatedEventPH, value);
			}
			remove
			{
				base.Events.RemoveHandler(SqliteDataAdapter._updatedEventPH, value);
			}
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00009E24 File Offset: 0x00008024
		protected override void OnRowUpdating(RowUpdatingEventArgs value)
		{
			EventHandler<RowUpdatingEventArgs> eventHandler = base.Events[SqliteDataAdapter._updatingEventPH] as EventHandler<RowUpdatingEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, value);
			}
		}

		// Token: 0x060001AA RID: 426 RVA: 0x00009E54 File Offset: 0x00008054
		protected override void OnRowUpdated(RowUpdatedEventArgs value)
		{
			EventHandler<RowUpdatedEventArgs> eventHandler = base.Events[SqliteDataAdapter._updatedEventPH] as EventHandler<RowUpdatedEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, value);
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060001AB RID: 427 RVA: 0x00009E82 File Offset: 0x00008082
		// (set) Token: 0x060001AC RID: 428 RVA: 0x00009E8F File Offset: 0x0000808F
		[DefaultValue(null)]
		[Editor("Microsoft.VSDesigner.Data.Design.DBCommandEditor, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public new SqliteCommand SelectCommand
		{
			get
			{
				return (SqliteCommand)base.SelectCommand;
			}
			set
			{
				base.SelectCommand = value;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060001AD RID: 429 RVA: 0x00009E98 File Offset: 0x00008098
		// (set) Token: 0x060001AE RID: 430 RVA: 0x00009EA5 File Offset: 0x000080A5
		[DefaultValue(null)]
		[Editor("Microsoft.VSDesigner.Data.Design.DBCommandEditor, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public new SqliteCommand InsertCommand
		{
			get
			{
				return (SqliteCommand)base.InsertCommand;
			}
			set
			{
				base.InsertCommand = value;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060001AF RID: 431 RVA: 0x00009EAE File Offset: 0x000080AE
		// (set) Token: 0x060001B0 RID: 432 RVA: 0x00009EBB File Offset: 0x000080BB
		[DefaultValue(null)]
		[Editor("Microsoft.VSDesigner.Data.Design.DBCommandEditor, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public new SqliteCommand UpdateCommand
		{
			get
			{
				return (SqliteCommand)base.UpdateCommand;
			}
			set
			{
				base.UpdateCommand = value;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x00009EC4 File Offset: 0x000080C4
		// (set) Token: 0x060001B2 RID: 434 RVA: 0x00009ED1 File Offset: 0x000080D1
		[DefaultValue(null)]
		[Editor("Microsoft.VSDesigner.Data.Design.DBCommandEditor, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public new SqliteCommand DeleteCommand
		{
			get
			{
				return (SqliteCommand)base.DeleteCommand;
			}
			set
			{
				base.DeleteCommand = value;
			}
		}

		// Token: 0x04000095 RID: 149
		private static object _updatingEventPH = new object();

		// Token: 0x04000096 RID: 150
		private static object _updatedEventPH = new object();
	}
}
