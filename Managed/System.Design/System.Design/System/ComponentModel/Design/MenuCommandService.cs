using System;
using System.Collections;
using System.Collections.Generic;

namespace System.ComponentModel.Design
{
	/// <summary>Implements the <see cref="T:System.ComponentModel.Design.IMenuCommandService" /> interface.</summary>
	// Token: 0x0200012D RID: 301
	public class MenuCommandService : IMenuCommandService, IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.MenuCommandService" /> class. </summary>
		/// <param name="serviceProvider">The service provider that this service uses to obtain other services.</param>
		// Token: 0x060008E7 RID: 2279 RVA: 0x0000F1B8 File Offset: 0x0000D3B8
		public MenuCommandService(IServiceProvider serviceProvider)
		{
			if (serviceProvider == null)
			{
				throw new ArgumentNullException("serviceProvider");
			}
			this._serviceProvider = serviceProvider;
			ISelectionService selectionService = this._serviceProvider.GetService(typeof(ISelectionService)) as ISelectionService;
			if (selectionService != null)
			{
				selectionService.SelectionChanged += this.OnSelectionChanged;
			}
		}

		// Token: 0x060008E8 RID: 2280 RVA: 0x0000F210 File Offset: 0x0000D410
		private void OnSelectionChanged(object sender, EventArgs arg)
		{
			this.OnCommandsChanged(new MenuCommandsChangedEventArgs(MenuCommandsChangedType.CommandChanged, null));
		}

		/// <summary>Occurs when the status of a menu command has changed.</summary>
		// Token: 0x14000033 RID: 51
		// (add) Token: 0x060008E9 RID: 2281 RVA: 0x0000F220 File Offset: 0x0000D420
		// (remove) Token: 0x060008EA RID: 2282 RVA: 0x0000F258 File Offset: 0x0000D458
		public event MenuCommandsChangedEventHandler MenuCommandsChanged;

		/// <summary>Gets a collection of the designer verbs that are currently available.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.Design.DesignerVerbCollection" /> of the designer verbs that are currently available.</returns>
		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x060008EB RID: 2283 RVA: 0x0000F28D File Offset: 0x0000D48D
		public virtual DesignerVerbCollection Verbs
		{
			get
			{
				this.EnsureVerbs();
				return this._verbs;
			}
		}

		/// <summary>Adds a command handler to the menu command service.</summary>
		/// <param name="command">The <see cref="T:System.ComponentModel.Design.MenuCommand" /> to add.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="command" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">A command handler <paramref name="command" /> already exists.</exception>
		// Token: 0x060008EC RID: 2284 RVA: 0x0000F29C File Offset: 0x0000D49C
		public virtual void AddCommand(MenuCommand command)
		{
			if (command == null)
			{
				throw new ArgumentNullException("command");
			}
			if (this._commands == null)
			{
				this._commands = new Dictionary<CommandID, MenuCommand>();
			}
			this._commands.Add(command.CommandID, command);
			this.OnCommandsChanged(new MenuCommandsChangedEventArgs(MenuCommandsChangedType.CommandAdded, command));
		}

		/// <summary>Adds a verb to the verb table of the <see cref="T:System.ComponentModel.Design.MenuCommandService" />.</summary>
		/// <param name="verb">The <see cref="T:System.ComponentModel.Design.DesignerVerb" /> to add.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="verb" /> is null.</exception>
		// Token: 0x060008ED RID: 2285 RVA: 0x0000F2EC File Offset: 0x0000D4EC
		public virtual void AddVerb(DesignerVerb verb)
		{
			if (verb == null)
			{
				throw new ArgumentNullException("verb");
			}
			this.EnsureVerbs();
			if (!this._verbs.Contains(verb))
			{
				if (this._globalVerbs == null)
				{
					this._globalVerbs = new DesignerVerbCollection();
				}
				this._globalVerbs.Add(verb);
			}
			this.OnCommandsChanged(new MenuCommandsChangedEventArgs(MenuCommandsChangedType.CommandAdded, verb));
		}

		/// <summary>Releases all resources used by the <see cref="T:System.ComponentModel.Design.MenuCommandService" />.</summary>
		// Token: 0x060008EE RID: 2286 RVA: 0x0000F348 File Offset: 0x0000D548
		public void Dispose()
		{
			this.Dispose(true);
		}

		/// <summary>Releases all resources used by the <see cref="T:System.ComponentModel.Design.MenuCommandService" />.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		// Token: 0x060008EF RID: 2287 RVA: 0x0000F354 File Offset: 0x0000D554
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this._globalVerbs != null)
				{
					this._globalVerbs.Clear();
					this._globalVerbs = null;
				}
				if (this._verbs != null)
				{
					this._verbs.Clear();
					this._verbs = null;
				}
				if (this._commands != null)
				{
					this._commands.Clear();
					this._commands = null;
				}
				if (this._serviceProvider != null)
				{
					ISelectionService selectionService = this._serviceProvider.GetService(typeof(ISelectionService)) as ISelectionService;
					if (selectionService != null)
					{
						selectionService.SelectionChanged -= this.OnSelectionChanged;
					}
					this._serviceProvider = null;
				}
			}
		}

		/// <summary>Ensures that the verb list has been created.</summary>
		// Token: 0x060008F0 RID: 2288 RVA: 0x0000F3F4 File Offset: 0x0000D5F4
		protected void EnsureVerbs()
		{
			DesignerVerbCollection designerVerbCollection = null;
			ISelectionService selectionService = this.GetService(typeof(ISelectionService)) as ISelectionService;
			IDesignerHost designerHost = this.GetService(typeof(IDesignerHost)) as IDesignerHost;
			if (selectionService != null && designerHost != null && selectionService.SelectionCount == 1)
			{
				IComponent component = selectionService.PrimarySelection as IComponent;
				if (component != null)
				{
					IDesigner designer = designerHost.GetDesigner(component);
					if (designer != null)
					{
						designerVerbCollection = designer.Verbs;
					}
				}
			}
			Dictionary<string, DesignerVerb> dictionary = new Dictionary<string, DesignerVerb>();
			if (this._globalVerbs != null)
			{
				foreach (object obj in this._globalVerbs)
				{
					DesignerVerb designerVerb = (DesignerVerb)obj;
					dictionary[designerVerb.Text] = designerVerb;
				}
			}
			if (designerVerbCollection != null)
			{
				foreach (object obj2 in designerVerbCollection)
				{
					DesignerVerb designerVerb2 = (DesignerVerb)obj2;
					dictionary[designerVerb2.Text] = designerVerb2;
				}
			}
			if (this._verbs == null)
			{
				this._verbs = new DesignerVerbCollection();
			}
			else
			{
				this._verbs.Clear();
			}
			foreach (DesignerVerb designerVerb3 in dictionary.Values)
			{
				this._verbs.Add(designerVerb3);
			}
		}

		/// <summary>Searches for the <see cref="T:System.ComponentModel.Design.MenuCommand" /> associated with the given command.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.Design.MenuCommand" /> associated with the given command; otherwise, null if the command is not found.</returns>
		/// <param name="guid">The GUID of the command.</param>
		/// <param name="id">The ID of the command.</param>
		// Token: 0x060008F1 RID: 2289 RVA: 0x0000F590 File Offset: 0x0000D790
		protected MenuCommand FindCommand(Guid guid, int id)
		{
			return this.FindCommand(new CommandID(guid, id));
		}

		/// <summary>Searches for the <see cref="T:System.ComponentModel.Design.MenuCommand" /> associated with the given command ID.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.Design.MenuCommand" /> associated with the given command; otherwise, null if the command is not found.</returns>
		/// <param name="commandID">The <see cref="T:System.ComponentModel.Design.CommandID" /> to find.</param>
		// Token: 0x060008F2 RID: 2290 RVA: 0x0000F5A0 File Offset: 0x0000D7A0
		public MenuCommand FindCommand(CommandID commandID)
		{
			if (commandID == null)
			{
				throw new ArgumentNullException("commandID");
			}
			MenuCommand menuCommand = null;
			if (this._commands != null)
			{
				this._commands.TryGetValue(commandID, out menuCommand);
			}
			if (menuCommand == null)
			{
				this.EnsureVerbs();
				foreach (object obj in this._verbs)
				{
					DesignerVerb designerVerb = (DesignerVerb)obj;
					if (designerVerb.CommandID.Equals(commandID))
					{
						menuCommand = designerVerb;
						break;
					}
				}
			}
			return menuCommand;
		}

		/// <summary>Gets the command list for a given GUID.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> of commands.</returns>
		/// <param name="guid">The GUID of the command list.</param>
		// Token: 0x060008F3 RID: 2291 RVA: 0x0000F638 File Offset: 0x0000D838
		protected ICollection GetCommandList(Guid guid)
		{
			List<MenuCommand> list = new List<MenuCommand>();
			if (this._commands != null)
			{
				foreach (MenuCommand menuCommand in this._commands.Values)
				{
					if (menuCommand.CommandID.Guid == guid)
					{
						list.Add(menuCommand);
					}
				}
			}
			return list;
		}

		/// <summary>Invokes the given command on the local form or in the global environment.</summary>
		/// <returns>true, if the command was found; otherwise, false.</returns>
		/// <param name="commandID">The command to invoke.</param>
		// Token: 0x060008F4 RID: 2292 RVA: 0x0000F6B4 File Offset: 0x0000D8B4
		public virtual bool GlobalInvoke(CommandID commandID)
		{
			if (commandID == null)
			{
				throw new ArgumentNullException("commandID");
			}
			MenuCommand menuCommand = this.FindCommand(commandID);
			if (menuCommand != null)
			{
				menuCommand.Invoke();
				return true;
			}
			return false;
		}

		/// <summary>Invokes the given command with the given parameter on the local form or in the global environment.</summary>
		/// <returns>true, if the command was found; otherwise, false.</returns>
		/// <param name="commandId">The command to invoke.</param>
		/// <param name="arg">A parameter for the invocation.</param>
		// Token: 0x060008F5 RID: 2293 RVA: 0x0000F6E4 File Offset: 0x0000D8E4
		public virtual bool GlobalInvoke(CommandID commandId, object arg)
		{
			if (commandId == null)
			{
				throw new ArgumentNullException("commandId");
			}
			MenuCommand menuCommand = this.FindCommand(commandId);
			if (menuCommand != null)
			{
				menuCommand.Invoke(arg);
				return true;
			}
			return false;
		}

		/// <summary>Raises the <see cref="E:System.ComponentModel.Design.MenuCommandService.MenuCommandsChanged" /> event. </summary>
		/// <param name="e">A <see cref="T:System.ComponentModel.Design.MenuCommandsChangedEventArgs" /> that contains the event data. </param>
		// Token: 0x060008F6 RID: 2294 RVA: 0x0000F714 File Offset: 0x0000D914
		protected virtual void OnCommandsChanged(MenuCommandsChangedEventArgs e)
		{
			if (this.MenuCommandsChanged != null)
			{
				this.MenuCommandsChanged(this, e);
			}
		}

		/// <summary>Removes the given menu command from the document.</summary>
		/// <param name="command">The command to remove.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="command" /> is null.</exception>
		// Token: 0x060008F7 RID: 2295 RVA: 0x0000F72B File Offset: 0x0000D92B
		public virtual void RemoveCommand(MenuCommand command)
		{
			if (command == null)
			{
				throw new ArgumentNullException("command");
			}
			if (this._commands != null)
			{
				this._commands.Remove(command.CommandID);
			}
			this.OnCommandsChanged(new MenuCommandsChangedEventArgs(MenuCommandsChangedType.CommandRemoved, null));
		}

		/// <summary>Removes the given verb from the document.</summary>
		/// <param name="verb">The verb to remove.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="verb" /> is null.</exception>
		// Token: 0x060008F8 RID: 2296 RVA: 0x0000F762 File Offset: 0x0000D962
		public virtual void RemoveVerb(DesignerVerb verb)
		{
			if (verb == null)
			{
				throw new ArgumentNullException("verb");
			}
			if (this._globalVerbs.Contains(verb))
			{
				this._globalVerbs.Remove(verb);
			}
			this.OnCommandsChanged(new MenuCommandsChangedEventArgs(MenuCommandsChangedType.CommandRemoved, verb));
		}

		/// <summary>Shows the shortcut menu with the given command ID at the given location.</summary>
		/// <param name="menuID">The shortcut menu to display.</param>
		/// <param name="x">The x-coordinate of the shortcut menu's location.</param>
		/// <param name="y">The y-coordinate of the shortcut menu's location.</param>
		// Token: 0x060008F9 RID: 2297 RVA: 0x00002432 File Offset: 0x00000632
		public virtual void ShowContextMenu(CommandID menuID, int x, int y)
		{
		}

		/// <summary>Gets a reference to the requested service.</summary>
		/// <returns>A reference to <paramref name="serviceType" />; otherwise, null if the service is not found.</returns>
		/// <param name="serviceType">The <see cref="T:System.Type" /> of the service to retrieve.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="serviceType" /> is null.</exception>
		// Token: 0x060008FA RID: 2298 RVA: 0x0000F799 File Offset: 0x0000D999
		protected object GetService(Type serviceType)
		{
			if (this._serviceProvider != null)
			{
				return this._serviceProvider.GetService(serviceType);
			}
			return null;
		}

		// Token: 0x040001F8 RID: 504
		private IServiceProvider _serviceProvider;

		// Token: 0x040001F9 RID: 505
		private DesignerVerbCollection _globalVerbs;

		// Token: 0x040001FA RID: 506
		private DesignerVerbCollection _verbs;

		// Token: 0x040001FB RID: 507
		private Dictionary<CommandID, MenuCommand> _commands;
	}
}
