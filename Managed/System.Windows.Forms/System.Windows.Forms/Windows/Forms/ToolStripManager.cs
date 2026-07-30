using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace System.Windows.Forms
{
	/// <summary>Controls <see cref="T:System.Windows.Forms.ToolStrip" /> rendering and rafting, and the merging of <see cref="T:System.Windows.Forms.MenuStrip" />, <see cref="T:System.Windows.Forms.ToolStripDropDownMenu" />, and <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> objects. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000365 RID: 869
	public sealed class ToolStripManager
	{
		// Token: 0x06003E58 RID: 15960 RVA: 0x000F8898 File Offset: 0x000F6A98
		private ToolStripManager()
		{
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ToolStripManager.Renderer" /> property changes.</summary>
		// Token: 0x140003C4 RID: 964
		// (add) Token: 0x06003E5A RID: 15962 RVA: 0x000F88DC File Offset: 0x000F6ADC
		// (remove) Token: 0x06003E5B RID: 15963 RVA: 0x000F88F4 File Offset: 0x000F6AF4
		public static event EventHandler RendererChanged;

		// Token: 0x140003C5 RID: 965
		// (add) Token: 0x06003E5C RID: 15964 RVA: 0x000F890C File Offset: 0x000F6B0C
		// (remove) Token: 0x06003E5D RID: 15965 RVA: 0x000F8924 File Offset: 0x000F6B24
		internal static event EventHandler AppClicked;

		// Token: 0x140003C6 RID: 966
		// (add) Token: 0x06003E5E RID: 15966 RVA: 0x000F893C File Offset: 0x000F6B3C
		// (remove) Token: 0x06003E5F RID: 15967 RVA: 0x000F8954 File Offset: 0x000F6B54
		internal static event EventHandler AppFocusChange;

		/// <summary>Gets or sets the default painting styles for the form.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripRenderer" /> values.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17001051 RID: 4177
		// (get) Token: 0x06003E60 RID: 15968 RVA: 0x000F896C File Offset: 0x000F6B6C
		// (set) Token: 0x06003E61 RID: 15969 RVA: 0x000F8974 File Offset: 0x000F6B74
		public static ToolStripRenderer Renderer
		{
			get
			{
				return ToolStripManager.renderer;
			}
			set
			{
				if (ToolStripManager.Renderer != value)
				{
					ToolStripManager.renderer = value;
					ToolStripManager.OnRendererChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets or sets the default theme for the form.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripManagerRenderMode" /> values.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The set value was not one of the <see cref="T:System.Windows.Forms.ToolStripManagerRenderMode" /> values.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <see cref="T:System.Windows.Forms.ToolStripManagerRenderMode" /> is set to <see cref="F:System.Windows.Forms.ToolStripManagerRenderMode.Custom" />; use the <see cref="P:System.Windows.Forms.ToolStripManager.Renderer" /> property instead.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17001052 RID: 4178
		// (get) Token: 0x06003E62 RID: 15970 RVA: 0x000F8994 File Offset: 0x000F6B94
		// (set) Token: 0x06003E63 RID: 15971 RVA: 0x000F899C File Offset: 0x000F6B9C
		public static ToolStripManagerRenderMode RenderMode
		{
			get
			{
				return ToolStripManager.render_mode;
			}
			set
			{
				if (!Enum.IsDefined(typeof(ToolStripManagerRenderMode), value))
				{
					throw new InvalidEnumArgumentException(string.Format("Enum argument value '{0}' is not valid for ToolStripManagerRenderMode", value));
				}
				if (ToolStripManager.render_mode != value)
				{
					ToolStripManager.render_mode = value;
					switch (value)
					{
					case ToolStripManagerRenderMode.Custom:
						throw new NotSupportedException();
					case ToolStripManagerRenderMode.System:
						ToolStripManager.Renderer = new ToolStripSystemRenderer();
						break;
					case ToolStripManagerRenderMode.Professional:
						ToolStripManager.Renderer = new ToolStripProfessionalRenderer();
						break;
					}
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether a <see cref="T:System.Windows.Forms.ToolStrip" /> is rendered using visual style information called themes. </summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.ToolStripItem" /> is rendered using themes; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17001053 RID: 4179
		// (get) Token: 0x06003E64 RID: 15972 RVA: 0x000F8A28 File Offset: 0x000F6C28
		// (set) Token: 0x06003E65 RID: 15973 RVA: 0x000F8A30 File Offset: 0x000F6C30
		public static bool VisualStylesEnabled
		{
			get
			{
				return ToolStripManager.visual_styles_enabled;
			}
			set
			{
				if (ToolStripManager.visual_styles_enabled != value)
				{
					ToolStripManager.visual_styles_enabled = value;
					if (ToolStripManager.render_mode == ToolStripManagerRenderMode.Professional)
					{
						(ToolStripManager.renderer as ToolStripProfessionalRenderer).ColorTable.UseSystemColors = !value;
						ToolStripManager.OnRendererChanged(EventArgs.Empty);
					}
				}
			}
		}

		/// <summary>Finds the specified <see cref="T:System.Windows.Forms.ToolStrip" /> or a type derived from <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ToolStrip" /> or one of its derived types as specified by the <paramref name="toolStripName" /> parameter, or null if the <see cref="T:System.Windows.Forms.ToolStrip" /> is not found.</returns>
		/// <param name="toolStripName">A string specifying the name of the <see cref="T:System.Windows.Forms.ToolStrip" /> or derived <see cref="T:System.Windows.Forms.ToolStrip" /> type to find.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003E66 RID: 15974 RVA: 0x000F8A7C File Offset: 0x000F6C7C
		public static ToolStrip FindToolStrip(string toolStripName)
		{
			List<WeakReference> list = ToolStripManager.toolstrips;
			lock (list)
			{
				foreach (WeakReference weakReference in ToolStripManager.toolstrips)
				{
					ToolStrip toolStrip = (ToolStrip)weakReference.Target;
					if (toolStrip != null)
					{
						if (toolStrip.Name == toolStripName)
						{
							return toolStrip;
						}
					}
				}
			}
			return null;
		}

		/// <summary>Retrieves a value indicating whether the specified shortcut key is used by any of the <see cref="T:System.Windows.Forms.ToolStrip" /> controls of a form.</summary>
		/// <returns>true if the shortcut key is used by any <see cref="T:System.Windows.Forms.ToolStrip" /> on the form; otherwise, false. </returns>
		/// <param name="shortcut">The shortcut key for which to search.</param>
		// Token: 0x06003E67 RID: 15975 RVA: 0x000F8B40 File Offset: 0x000F6D40
		public static bool IsShortcutDefined(Keys shortcut)
		{
			List<ToolStripMenuItem> list = ToolStripManager.menu_items;
			lock (list)
			{
				foreach (ToolStripMenuItem toolStripMenuItem in ToolStripManager.menu_items)
				{
					if (toolStripMenuItem.ShortcutKeys == shortcut)
					{
						return true;
					}
				}
			}
			return false;
		}

		/// <summary>Retrieves a value indicating whether a defined shortcut key is valid.</summary>
		/// <returns>true if the shortcut key is valid; otherwise, false. </returns>
		/// <param name="shortcut">The shortcut key to test for validity.</param>
		// Token: 0x06003E68 RID: 15976 RVA: 0x000F8BE4 File Offset: 0x000F6DE4
		public static bool IsValidShortcut(Keys shortcut)
		{
			return (shortcut & Keys.F1) == Keys.F1 || (shortcut & Keys.F2) == Keys.F2 || (shortcut & Keys.F3) == Keys.F3 || (shortcut & Keys.F4) == Keys.F4 || (shortcut & Keys.F5) == Keys.F5 || (shortcut & Keys.F6) == Keys.F6 || (shortcut & Keys.F7) == Keys.F7 || (shortcut & Keys.F8) == Keys.F8 || (shortcut & Keys.F9) == Keys.F9 || (shortcut & Keys.F10) == Keys.F10 || (shortcut & Keys.F11) == Keys.F11 || (shortcut & Keys.F12) == Keys.F12 || (shortcut != Keys.Shift && shortcut != Keys.Control && shortcut != (Keys.Shift | Keys.Control) && shortcut != Keys.Alt && shortcut != (Keys.Shift | Keys.Alt) && shortcut != (Keys.Control | Keys.Alt) && shortcut != (Keys.Shift | Keys.Control | Keys.Alt) && ((shortcut & Keys.Alt) == Keys.Alt || (shortcut & Keys.Control) == Keys.Control || (shortcut & Keys.Shift) == Keys.Shift));
		}

		/// <summary>Loads settings for the given <see cref="T:System.Windows.Forms.Form" /> using the full name of the <see cref="T:System.Windows.Forms.Form" /> as the settings key.</summary>
		/// <param name="targetForm">The <see cref="T:System.Windows.Forms.Form" /> whose name is also the settings key.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="targetForm" /> parameter is null.</exception>
		// Token: 0x06003E69 RID: 15977 RVA: 0x000F8D20 File Offset: 0x000F6F20
		[MonoTODO("Stub, does nothing")]
		public static void LoadSettings(Form targetForm)
		{
			if (targetForm == null)
			{
				throw new ArgumentNullException("targetForm");
			}
		}

		/// <summary>Loads settings for the specified <see cref="T:System.Windows.Forms.Form" /> using the specified settings key.</summary>
		/// <param name="targetForm">The <see cref="T:System.Windows.Forms.Form" /> for which to load settings.</param>
		/// <param name="key">A <see cref="T:System.String" /> representing the settings key for this <see cref="T:System.Windows.Forms.Form" />.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="targetForm" /> parameter is null.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="key" /> parameter is null or empty.</exception>
		// Token: 0x06003E6A RID: 15978 RVA: 0x000F8D34 File Offset: 0x000F6F34
		[MonoTODO("Stub, does nothing")]
		public static void LoadSettings(Form targetForm, string key)
		{
			if (targetForm == null)
			{
				throw new ArgumentNullException("targetForm");
			}
			if (string.IsNullOrEmpty(key))
			{
				throw new ArgumentNullException("key");
			}
		}

		/// <summary>Combines two <see cref="T:System.Windows.Forms.ToolStrip" /> objects of the same type.</summary>
		/// <returns>true if the merge is successful; otherwise, false. </returns>
		/// <param name="sourceToolStrip">The <see cref="T:System.Windows.Forms.ToolStrip" /> to be combined with the <see cref="T:System.Windows.Forms.ToolStrip" /> referred to by the <paramref name="targetName" /> parameter.</param>
		/// <param name="targetName">The name of the <see cref="T:System.Windows.Forms.ToolStrip" /> that receives the <see cref="T:System.Windows.Forms.ToolStrip" /> referred to by the <paramref name="sourceToolStrip" /> parameter.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="sourceToolStrip" /> or <paramref name="targetName" /> parameter is null.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="sourceToolStrip" /> or <paramref name="targetName" /> parameters refer to the same <see cref="T:System.Windows.Forms.ToolStrip" />.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003E6B RID: 15979 RVA: 0x000F8D60 File Offset: 0x000F6F60
		[MonoLimitation("Only supports one level of merging, cannot merge the same ToolStrip multiple times")]
		public static bool Merge(ToolStrip sourceToolStrip, string targetName)
		{
			if (string.IsNullOrEmpty(targetName))
			{
				throw new ArgumentNullException("targetName");
			}
			return ToolStripManager.Merge(sourceToolStrip, ToolStripManager.FindToolStrip(targetName));
		}

		/// <summary>Combines two <see cref="T:System.Windows.Forms.ToolStrip" /> objects of different types.</summary>
		/// <returns>true if the merge is successful; otherwise, false.</returns>
		/// <param name="sourceToolStrip">The <see cref="T:System.Windows.Forms.ToolStrip" /> to be combined with the <see cref="T:System.Windows.Forms.ToolStrip" /> referred to by the <paramref name="targetToolStrip" /> parameter.</param>
		/// <param name="targetToolStrip">The <see cref="T:System.Windows.Forms.ToolStrip" /> that receives the <see cref="T:System.Windows.Forms.ToolStrip" /> referred to by the <paramref name="sourceToolStrip" /> parameter.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003E6C RID: 15980 RVA: 0x000F8D90 File Offset: 0x000F6F90
		[MonoLimitation("Only supports one level of merging, cannot merge the same ToolStrip multiple times")]
		public static bool Merge(ToolStrip sourceToolStrip, ToolStrip targetToolStrip)
		{
			if (sourceToolStrip == null)
			{
				throw new ArgumentNullException("sourceToolStrip");
			}
			if (targetToolStrip == null)
			{
				throw new ArgumentNullException("targetName");
			}
			if (targetToolStrip == sourceToolStrip)
			{
				throw new ArgumentException("Source and target ToolStrip must be different.");
			}
			if (!sourceToolStrip.AllowMerge || !targetToolStrip.AllowMerge)
			{
				return false;
			}
			if (sourceToolStrip.IsCurrentlyMerged || targetToolStrip.IsCurrentlyMerged)
			{
				return false;
			}
			List<ToolStripItem> list = new List<ToolStripItem>();
			foreach (object obj in sourceToolStrip.Items)
			{
				ToolStripItem toolStripItem = (ToolStripItem)obj;
				switch (toolStripItem.MergeAction)
				{
				default:
					list.Add(toolStripItem);
					break;
				case MergeAction.Insert:
					if (toolStripItem.MergeIndex >= 0)
					{
						list.Add(toolStripItem);
					}
					break;
				case MergeAction.Replace:
				case MergeAction.Remove:
				case MergeAction.MatchOnly:
					foreach (object obj2 in targetToolStrip.Items)
					{
						ToolStripItem toolStripItem2 = (ToolStripItem)obj2;
						if (toolStripItem.Text == toolStripItem2.Text)
						{
							list.Add(toolStripItem);
							break;
						}
					}
					break;
				}
			}
			if (list.Count == 0)
			{
				return false;
			}
			sourceToolStrip.BeginMerge();
			targetToolStrip.BeginMerge();
			sourceToolStrip.SuspendLayout();
			targetToolStrip.SuspendLayout();
			while (list.Count > 0)
			{
				ToolStripItem toolStripItem3 = list[0];
				list.Remove(toolStripItem3);
				switch (toolStripItem3.MergeAction)
				{
				default:
					ToolStrip.SetItemParent(toolStripItem3, targetToolStrip);
					break;
				case MergeAction.Insert:
					ToolStripManager.RemoveItemFromParentToolStrip(toolStripItem3);
					if (toolStripItem3.MergeIndex != -1)
					{
						if (toolStripItem3.MergeIndex >= ToolStripManager.CountRealToolStripItems(targetToolStrip))
						{
							targetToolStrip.Items.AddNoOwnerOrLayout(toolStripItem3);
						}
						else
						{
							targetToolStrip.Items.InsertNoOwnerOrLayout(ToolStripManager.AdjustItemMergeIndex(targetToolStrip, toolStripItem3), toolStripItem3);
						}
						toolStripItem3.Parent = targetToolStrip;
					}
					break;
				case MergeAction.Replace:
					foreach (object obj3 in targetToolStrip.Items)
					{
						ToolStripItem toolStripItem4 = (ToolStripItem)obj3;
						if (toolStripItem3.Text == toolStripItem4.Text)
						{
							ToolStripManager.RemoveItemFromParentToolStrip(toolStripItem3);
							targetToolStrip.Items.InsertNoOwnerOrLayout(targetToolStrip.Items.IndexOf(toolStripItem4), toolStripItem3);
							targetToolStrip.Items.RemoveNoOwnerOrLayout(toolStripItem4);
							targetToolStrip.HiddenMergedItems.Add(toolStripItem4);
							break;
						}
					}
					break;
				case MergeAction.Remove:
					foreach (object obj4 in targetToolStrip.Items)
					{
						ToolStripItem toolStripItem5 = (ToolStripItem)obj4;
						if (toolStripItem3.Text == toolStripItem5.Text)
						{
							targetToolStrip.Items.RemoveNoOwnerOrLayout(toolStripItem5);
							targetToolStrip.HiddenMergedItems.Add(toolStripItem5);
							break;
						}
					}
					break;
				case MergeAction.MatchOnly:
					foreach (object obj5 in targetToolStrip.Items)
					{
						ToolStripItem toolStripItem6 = (ToolStripItem)obj5;
						if (toolStripItem3.Text == toolStripItem6.Text)
						{
							if (toolStripItem6 is ToolStripMenuItem && toolStripItem3 is ToolStripMenuItem)
							{
								ToolStripMenuItem toolStripMenuItem = (ToolStripMenuItem)toolStripItem3;
								ToolStripMenuItem toolStripMenuItem2 = (ToolStripMenuItem)toolStripItem6;
								ToolStripManager.Merge(toolStripMenuItem.DropDown, toolStripMenuItem2.DropDown);
							}
							break;
						}
					}
					break;
				}
			}
			sourceToolStrip.ResumeLayout();
			targetToolStrip.ResumeLayout();
			sourceToolStrip.CurrentlyMergedWith = targetToolStrip;
			targetToolStrip.CurrentlyMergedWith = sourceToolStrip;
			return true;
		}

		/// <summary>Undoes a merging of two <see cref="T:System.Windows.Forms.ToolStrip" /> objects, returning the <see cref="T:System.Windows.Forms.ToolStrip" /> with the specified name to its state before the merge and nullifying all previous merge operations.</summary>
		/// <returns>true if the undoing of the merge is successful; otherwise, false. </returns>
		/// <param name="targetName">The name of the <see cref="T:System.Windows.Forms.ToolStripItem" /> for which to undo a merge operation.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003E6D RID: 15981 RVA: 0x000F9248 File Offset: 0x000F7448
		public static bool RevertMerge(string targetName)
		{
			return ToolStripManager.RevertMerge(ToolStripManager.FindToolStrip(targetName));
		}

		/// <summary>Undoes a merging of two <see cref="T:System.Windows.Forms.ToolStrip" /> objects, returning the specified <see cref="T:System.Windows.Forms.ToolStrip" /> to its state before the merge and nullifying all previous merge operations.</summary>
		/// <returns>true if the undoing of the merge is successful; otherwise, false. </returns>
		/// <param name="targetToolStrip">The <see cref="T:System.Windows.Forms.ToolStripItem" /> for which to undo a merge operation.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003E6E RID: 15982 RVA: 0x000F9258 File Offset: 0x000F7458
		public static bool RevertMerge(ToolStrip targetToolStrip)
		{
			return ToolStripManager.RevertMerge(targetToolStrip, targetToolStrip.CurrentlyMergedWith);
		}

		/// <summary>Undoes a merging of two <see cref="T:System.Windows.Forms.ToolStrip" /> objects, returning both <see cref="T:System.Windows.Forms.ToolStrip" /> controls to their state before the merge and nullifying all previous merge operations.</summary>
		/// <returns>true if the undoing of the merge is successful; otherwise, false.</returns>
		/// <param name="targetToolStrip">The name of the <see cref="T:System.Windows.Forms.ToolStripItem" /> for which to undo a merge operation.</param>
		/// <param name="sourceToolStrip">The <see cref="T:System.Windows.Forms.ToolStrip" /> that was merged with the <paramref name="targetToolStrip" />.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="sourceToolStrip" /> is null.</exception>
		// Token: 0x06003E6F RID: 15983 RVA: 0x000F9268 File Offset: 0x000F7468
		public static bool RevertMerge(ToolStrip targetToolStrip, ToolStrip sourceToolStrip)
		{
			if (sourceToolStrip == null)
			{
				throw new ArgumentNullException("sourceToolStrip");
			}
			List<ToolStripItem> list = new List<ToolStripItem>();
			foreach (object obj in targetToolStrip.Items)
			{
				ToolStripItem toolStripItem = (ToolStripItem)obj;
				if (toolStripItem.Owner == sourceToolStrip)
				{
					list.Add(toolStripItem);
				}
				else if (toolStripItem is ToolStripMenuItem)
				{
					foreach (object obj2 in (toolStripItem as ToolStripMenuItem).DropDownItems)
					{
						ToolStripItem toolStripItem2 = (ToolStripItem)obj2;
						foreach (object obj3 in sourceToolStrip.Items)
						{
							ToolStripMenuItem toolStripMenuItem = (ToolStripMenuItem)obj3;
							if (toolStripItem2.Owner == toolStripMenuItem.DropDown)
							{
								list.Add(toolStripItem2);
							}
						}
					}
				}
			}
			if (list.Count == 0 && targetToolStrip.HiddenMergedItems.Count == 0)
			{
				return false;
			}
			while (targetToolStrip.HiddenMergedItems.Count > 0)
			{
				targetToolStrip.RevertMergeItem(targetToolStrip.HiddenMergedItems[0]);
				targetToolStrip.HiddenMergedItems.RemoveAt(0);
			}
			sourceToolStrip.SuspendLayout();
			targetToolStrip.SuspendLayout();
			while (list.Count > 0)
			{
				sourceToolStrip.RevertMergeItem(list[0]);
				list.Remove(list[0]);
			}
			sourceToolStrip.ResumeLayout();
			targetToolStrip.ResumeLayout();
			sourceToolStrip.IsCurrentlyMerged = false;
			targetToolStrip.IsCurrentlyMerged = false;
			sourceToolStrip.CurrentlyMergedWith = null;
			targetToolStrip.CurrentlyMergedWith = null;
			return true;
		}

		/// <summary>Saves settings for the given <see cref="T:System.Windows.Forms.Form" /> using the full name of the <see cref="T:System.Windows.Forms.Form" /> as the settings key.</summary>
		/// <param name="sourceForm">The <see cref="T:System.Windows.Forms.Form" /> whose name is also the settings key.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="sourceForm" /> parameter is null.</exception>
		// Token: 0x06003E70 RID: 15984 RVA: 0x000F949C File Offset: 0x000F769C
		public static void SaveSettings(Form sourceForm)
		{
			if (sourceForm == null)
			{
				throw new ArgumentNullException("sourceForm");
			}
		}

		/// <summary>Saves settings for the specified <see cref="T:System.Windows.Forms.Form" /> using the specified settings key.</summary>
		/// <param name="sourceForm">The <see cref="T:System.Windows.Forms.Form" /> for which to save settings.</param>
		/// <param name="key">A <see cref="T:System.String" /> representing the settings key for this <see cref="T:System.Windows.Forms.Form" />.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="sourceForm" /> parameter is null.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="key" /> parameter is null or empty.</exception>
		// Token: 0x06003E71 RID: 15985 RVA: 0x000F94B0 File Offset: 0x000F76B0
		public static void SaveSettings(Form sourceForm, string key)
		{
			if (sourceForm == null)
			{
				throw new ArgumentNullException("sourceForm");
			}
			if (string.IsNullOrEmpty(key))
			{
				throw new ArgumentNullException("key");
			}
		}

		// Token: 0x17001054 RID: 4180
		// (get) Token: 0x06003E72 RID: 15986 RVA: 0x000F94DC File Offset: 0x000F76DC
		// (set) Token: 0x06003E73 RID: 15987 RVA: 0x000F94E4 File Offset: 0x000F76E4
		internal static bool ActivatedByKeyboard
		{
			get
			{
				return ToolStripManager.activated_by_keyboard;
			}
			set
			{
				ToolStripManager.activated_by_keyboard = value;
			}
		}

		// Token: 0x06003E74 RID: 15988 RVA: 0x000F94EC File Offset: 0x000F76EC
		internal static void AddToolStrip(ToolStrip ts)
		{
			List<WeakReference> list = ToolStripManager.toolstrips;
			lock (list)
			{
				ToolStripManager.toolstrips.Add(new WeakReference(ts));
			}
		}

		// Token: 0x06003E75 RID: 15989 RVA: 0x000F9540 File Offset: 0x000F7740
		private static int AdjustItemMergeIndex(ToolStrip ts, ToolStripItem tsi)
		{
			if (ts.Items[0] is MdiControlStrip.SystemMenuItem)
			{
				return tsi.MergeIndex + 1;
			}
			return tsi.MergeIndex;
		}

		// Token: 0x06003E76 RID: 15990 RVA: 0x000F9574 File Offset: 0x000F7774
		private static int CountRealToolStripItems(ToolStrip ts)
		{
			int num = 0;
			foreach (object obj in ts.Items)
			{
				ToolStripItem toolStripItem = (ToolStripItem)obj;
				if (!(toolStripItem is MdiControlStrip.ControlBoxMenuItem) && !(toolStripItem is MdiControlStrip.SystemMenuItem))
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x06003E77 RID: 15991 RVA: 0x000F95FC File Offset: 0x000F77FC
		internal static ToolStrip GetNextToolStrip(ToolStrip ts, bool forward)
		{
			List<WeakReference> list = ToolStripManager.toolstrips;
			lock (list)
			{
				List<ToolStrip> list2 = new List<ToolStrip>();
				foreach (WeakReference weakReference in ToolStripManager.toolstrips)
				{
					ToolStrip toolStrip = (ToolStrip)weakReference.Target;
					if (toolStrip != null)
					{
						list2.Add(toolStrip);
					}
				}
				int num = list2.IndexOf(ts);
				if (forward)
				{
					for (int i = num + 1; i < list2.Count; i++)
					{
						if (list2[i].TopLevelControl == ts.TopLevelControl && !(list2[i] is StatusStrip))
						{
							return list2[i];
						}
					}
					for (int j = 0; j < num; j++)
					{
						if (list2[j].TopLevelControl == ts.TopLevelControl && !(list2[j] is StatusStrip))
						{
							return list2[j];
						}
					}
				}
				else
				{
					for (int k = num - 1; k >= 0; k--)
					{
						if (list2[k].TopLevelControl == ts.TopLevelControl && !(list2[k] is StatusStrip))
						{
							return list2[k];
						}
					}
					for (int l = list2.Count - 1; l > num; l--)
					{
						if (list2[l].TopLevelControl == ts.TopLevelControl && !(list2[l] is StatusStrip))
						{
							return list2[l];
						}
					}
				}
			}
			return null;
		}

		// Token: 0x06003E78 RID: 15992 RVA: 0x000F9814 File Offset: 0x000F7A14
		internal static bool ProcessCmdKey(ref Message m, Keys keyData)
		{
			List<ToolStripMenuItem> list = ToolStripManager.menu_items;
			lock (list)
			{
				foreach (ToolStripMenuItem toolStripMenuItem in ToolStripManager.menu_items)
				{
					if (toolStripMenuItem.ProcessCmdKey(ref m, keyData))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06003E79 RID: 15993 RVA: 0x000F98BC File Offset: 0x000F7ABC
		internal static bool ProcessMenuKey(ref Message m)
		{
			if (Application.KeyboardCapture != null && Application.KeyboardCapture.OnMenuKey())
			{
				return true;
			}
			Form form = (Form)Control.FromHandle(m.HWnd).TopLevelControl;
			if (form == null)
			{
				return false;
			}
			if (form.MainMenuStrip != null && form.MainMenuStrip.OnMenuKey())
			{
				return true;
			}
			List<WeakReference> list = ToolStripManager.toolstrips;
			lock (list)
			{
				foreach (WeakReference weakReference in ToolStripManager.toolstrips)
				{
					ToolStrip toolStrip = (ToolStrip)weakReference.Target;
					if (toolStrip != null)
					{
						if (toolStrip.TopLevelControl == form && toolStrip.OnMenuKey())
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06003E7A RID: 15994 RVA: 0x000F99E0 File Offset: 0x000F7BE0
		internal static void SetActiveToolStrip(ToolStrip toolStrip, bool keyboard)
		{
			if (Application.KeyboardCapture != null)
			{
				Application.KeyboardCapture.KeyboardActive = false;
			}
			if (toolStrip == null)
			{
				ToolStripManager.activated_by_keyboard = false;
				return;
			}
			ToolStripManager.activated_by_keyboard = keyboard;
			toolStrip.KeyboardActive = true;
		}

		// Token: 0x06003E7B RID: 15995 RVA: 0x000F9A14 File Offset: 0x000F7C14
		internal static void AddToolStripMenuItem(ToolStripMenuItem tsmi)
		{
			List<ToolStripMenuItem> list = ToolStripManager.menu_items;
			lock (list)
			{
				ToolStripManager.menu_items.Add(tsmi);
			}
		}

		// Token: 0x06003E7C RID: 15996 RVA: 0x000F9A60 File Offset: 0x000F7C60
		internal static void RemoveToolStrip(ToolStrip ts)
		{
			List<WeakReference> list = ToolStripManager.toolstrips;
			lock (list)
			{
				foreach (WeakReference weakReference in ToolStripManager.toolstrips)
				{
					if (weakReference.Target == ts)
					{
						ToolStripManager.toolstrips.Remove(weakReference);
						break;
					}
				}
			}
		}

		// Token: 0x06003E7D RID: 15997 RVA: 0x000F9B0C File Offset: 0x000F7D0C
		internal static void RemoveToolStripMenuItem(ToolStripMenuItem tsmi)
		{
			List<ToolStripMenuItem> list = ToolStripManager.menu_items;
			lock (list)
			{
				ToolStripManager.menu_items.Remove(tsmi);
			}
		}

		// Token: 0x06003E7E RID: 15998 RVA: 0x000F9B5C File Offset: 0x000F7D5C
		internal static void FireAppClicked()
		{
			if (ToolStripManager.AppClicked != null)
			{
				ToolStripManager.AppClicked.Invoke(null, EventArgs.Empty);
			}
			if (Application.KeyboardCapture != null)
			{
				Application.KeyboardCapture.Dismiss(ToolStripDropDownCloseReason.AppClicked);
			}
		}

		// Token: 0x06003E7F RID: 15999 RVA: 0x000F9B90 File Offset: 0x000F7D90
		internal static void FireAppFocusChanged(Form form)
		{
			if (ToolStripManager.AppFocusChange != null)
			{
				ToolStripManager.AppFocusChange.Invoke(form, EventArgs.Empty);
			}
			if (Application.KeyboardCapture != null)
			{
				Application.KeyboardCapture.Dismiss(ToolStripDropDownCloseReason.AppFocusChange);
			}
		}

		// Token: 0x06003E80 RID: 16000 RVA: 0x000F9BC4 File Offset: 0x000F7DC4
		internal static void FireAppFocusChanged(object sender)
		{
			if (ToolStripManager.AppFocusChange != null)
			{
				ToolStripManager.AppFocusChange.Invoke(sender, EventArgs.Empty);
			}
			if (Application.KeyboardCapture != null)
			{
				Application.KeyboardCapture.Dismiss(ToolStripDropDownCloseReason.AppFocusChange);
			}
		}

		// Token: 0x06003E81 RID: 16001 RVA: 0x000F9BF8 File Offset: 0x000F7DF8
		private static void OnRendererChanged(EventArgs e)
		{
			if (ToolStripManager.RendererChanged != null)
			{
				ToolStripManager.RendererChanged.Invoke(null, e);
			}
		}

		// Token: 0x06003E82 RID: 16002 RVA: 0x000F9C10 File Offset: 0x000F7E10
		private static void RemoveItemFromParentToolStrip(ToolStripItem tsi)
		{
			if (tsi.Owner != null)
			{
				tsi.Owner.Items.RemoveNoOwnerOrLayout(tsi);
				if (tsi.Owner is ToolStripOverflow)
				{
					(tsi.Owner as ToolStripOverflow).ParentToolStrip.Items.RemoveNoOwnerOrLayout(tsi);
				}
			}
		}

		// Token: 0x04001B17 RID: 6935
		private static ToolStripRenderer renderer = new ToolStripProfessionalRenderer();

		// Token: 0x04001B18 RID: 6936
		private static ToolStripManagerRenderMode render_mode = ToolStripManagerRenderMode.Professional;

		// Token: 0x04001B19 RID: 6937
		private static bool visual_styles_enabled = Application.RenderWithVisualStyles;

		// Token: 0x04001B1A RID: 6938
		private static List<WeakReference> toolstrips = new List<WeakReference>();

		// Token: 0x04001B1B RID: 6939
		private static List<ToolStripMenuItem> menu_items = new List<ToolStripMenuItem>();

		// Token: 0x04001B1C RID: 6940
		private static bool activated_by_keyboard;
	}
}
