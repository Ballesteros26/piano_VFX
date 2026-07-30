using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine.Rendering.UI;

namespace UnityEngine.Rendering
{
	// Token: 0x02000032 RID: 50
	public sealed class DebugManager
	{
		// Token: 0x0600011A RID: 282 RVA: 0x000059E8 File Offset: 0x00003BE8
		private void RegisterActions()
		{
			this.m_DebugActions = new DebugActionDesc[9];
			this.m_DebugActionStates = new DebugActionState[9];
			this.AddAction(DebugAction.EnableDebugMenu, new DebugActionDesc
			{
				buttonTriggerList = { new string[] { "Enable Debug Button 1", "Enable Debug Button 2" } },
				keyTriggerList = { new KeyCode[]
				{
					KeyCode.LeftControl,
					KeyCode.Backspace
				} },
				repeatMode = DebugActionRepeatMode.Never
			});
			this.AddAction(DebugAction.ResetAll, new DebugActionDesc
			{
				buttonTriggerList = { new string[] { "Debug Reset", "Enable Debug Button 2" } },
				keyTriggerList = { new KeyCode[]
				{
					KeyCode.LeftAlt,
					KeyCode.Backspace
				} },
				repeatMode = DebugActionRepeatMode.Never
			});
			this.AddAction(DebugAction.NextDebugPanel, new DebugActionDesc
			{
				buttonTriggerList = { new string[] { "Debug Next" } },
				repeatMode = DebugActionRepeatMode.Never
			});
			this.AddAction(DebugAction.PreviousDebugPanel, new DebugActionDesc
			{
				buttonTriggerList = { new string[] { "Debug Previous" } },
				repeatMode = DebugActionRepeatMode.Never
			});
			DebugActionDesc debugActionDesc = new DebugActionDesc();
			debugActionDesc.buttonTriggerList.Add(new string[] { "Debug Validate" });
			debugActionDesc.repeatMode = DebugActionRepeatMode.Never;
			this.AddAction(DebugAction.Action, debugActionDesc);
			this.AddAction(DebugAction.MakePersistent, new DebugActionDesc
			{
				buttonTriggerList = { new string[] { "Debug Persistent" } },
				repeatMode = DebugActionRepeatMode.Never
			});
			DebugActionDesc debugActionDesc2 = new DebugActionDesc();
			debugActionDesc2.buttonTriggerList.Add(new string[] { "Debug Multiplier" });
			debugActionDesc2.repeatMode = DebugActionRepeatMode.Delay;
			debugActionDesc.repeatDelay = 0f;
			this.AddAction(DebugAction.Multiplier, debugActionDesc2);
			this.AddAction(DebugAction.MoveVertical, new DebugActionDesc
			{
				axisTrigger = "Debug Vertical",
				repeatMode = DebugActionRepeatMode.Delay,
				repeatDelay = 0.16f
			});
			this.AddAction(DebugAction.MoveHorizontal, new DebugActionDesc
			{
				axisTrigger = "Debug Horizontal",
				repeatMode = DebugActionRepeatMode.Delay,
				repeatDelay = 0.16f
			});
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00005C08 File Offset: 0x00003E08
		private void AddAction(DebugAction action, DebugActionDesc desc)
		{
			this.m_DebugActions[(int)action] = desc;
			this.m_DebugActionStates[(int)action] = new DebugActionState();
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00005C30 File Offset: 0x00003E30
		private void SampleAction(int actionIndex)
		{
			DebugActionDesc debugActionDesc = this.m_DebugActions[actionIndex];
			DebugActionState debugActionState = this.m_DebugActionStates[actionIndex];
			if (!debugActionState.runningAction)
			{
				for (int i = 0; i < debugActionDesc.buttonTriggerList.Count; i++)
				{
					string[] array = debugActionDesc.buttonTriggerList[i];
					bool flag = true;
					string[] array2 = array;
					for (int j = 0; j < array2.Length; j++)
					{
						flag = Input.GetButton(array2[j]);
						if (!flag)
						{
							break;
						}
					}
					if (flag)
					{
						debugActionState.TriggerWithButton(array, 1f);
						break;
					}
				}
				if (debugActionDesc.axisTrigger != "")
				{
					float axis = Input.GetAxis(debugActionDesc.axisTrigger);
					if (axis != 0f)
					{
						debugActionState.TriggerWithAxis(debugActionDesc.axisTrigger, axis);
					}
				}
				for (int k = 0; k < debugActionDesc.keyTriggerList.Count; k++)
				{
					bool flag2 = true;
					KeyCode[] array3 = debugActionDesc.keyTriggerList[k];
					KeyCode[] array4 = array3;
					for (int j = 0; j < array4.Length; j++)
					{
						flag2 = Input.GetKey(array4[j]);
						if (!flag2)
						{
							break;
						}
					}
					if (flag2)
					{
						debugActionState.TriggerWithKey(array3, 1f);
						return;
					}
				}
			}
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00005D54 File Offset: 0x00003F54
		private void UpdateAction(int actionIndex)
		{
			DebugActionDesc debugActionDesc = this.m_DebugActions[actionIndex];
			DebugActionState debugActionState = this.m_DebugActionStates[actionIndex];
			if (debugActionState.runningAction)
			{
				debugActionState.Update(debugActionDesc);
			}
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00005D84 File Offset: 0x00003F84
		internal void UpdateActions()
		{
			for (int i = 0; i < this.m_DebugActions.Length; i++)
			{
				this.UpdateAction(i);
				this.SampleAction(i);
			}
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00005DB2 File Offset: 0x00003FB2
		internal float GetAction(DebugAction action)
		{
			return this.m_DebugActionStates[(int)action].actionState;
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00002788 File Offset: 0x00000988
		private void RegisterInputs()
		{
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000121 RID: 289 RVA: 0x00005DC1 File Offset: 0x00003FC1
		public static DebugManager instance
		{
			get
			{
				return DebugManager.s_Instance.Value;
			}
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00005DCD File Offset: 0x00003FCD
		private void UpdateReadOnlyCollection()
		{
			this.m_Panels.Sort();
			this.m_ReadOnlyPanels = this.m_Panels.AsReadOnly();
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000123 RID: 291 RVA: 0x00005DEB File Offset: 0x00003FEB
		public ReadOnlyCollection<DebugUI.Panel> panels
		{
			get
			{
				if (this.m_ReadOnlyPanels == null)
				{
					this.UpdateReadOnlyCollection();
				}
				return this.m_ReadOnlyPanels;
			}
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000124 RID: 292 RVA: 0x00005E04 File Offset: 0x00004004
		// (remove) Token: 0x06000125 RID: 293 RVA: 0x00005E3C File Offset: 0x0000403C
		public event Action<bool> onDisplayRuntimeUIChanged = delegate
		{
		};

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000126 RID: 294 RVA: 0x00005E74 File Offset: 0x00004074
		// (remove) Token: 0x06000127 RID: 295 RVA: 0x00005EAC File Offset: 0x000040AC
		public event Action onSetDirty = delegate
		{
		};

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000128 RID: 296 RVA: 0x00005EE4 File Offset: 0x000040E4
		// (remove) Token: 0x06000129 RID: 297 RVA: 0x00005F1C File Offset: 0x0000411C
		private event Action resetData;

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600012A RID: 298 RVA: 0x00005F51 File Offset: 0x00004151
		public bool displayEditorUI
		{
			get
			{
				return this.m_EditorOpen;
			}
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00005F59 File Offset: 0x00004159
		public void ToggleEditorUI(bool open)
		{
			this.m_EditorOpen = open;
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600012C RID: 300 RVA: 0x00005F62 File Offset: 0x00004162
		// (set) Token: 0x0600012D RID: 301 RVA: 0x00005F80 File Offset: 0x00004180
		public bool displayRuntimeUI
		{
			get
			{
				return this.m_Root != null && this.m_Root.activeInHierarchy;
			}
			set
			{
				if (value)
				{
					this.m_Root = Object.Instantiate<Transform>(Resources.Load<Transform>("DebugUI Canvas")).gameObject;
					this.m_Root.name = "[Debug Canvas]";
					this.m_Root.transform.localPosition = Vector3.zero;
					this.m_RootUICanvas = this.m_Root.GetComponent<DebugUIHandlerCanvas>();
					this.m_Root.SetActive(true);
				}
				else
				{
					CoreUtils.Destroy(this.m_Root);
					this.m_Root = null;
					this.m_RootUICanvas = null;
				}
				this.onDisplayRuntimeUIChanged(value);
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600012E RID: 302 RVA: 0x00006013 File Offset: 0x00004213
		// (set) Token: 0x0600012F RID: 303 RVA: 0x00006030 File Offset: 0x00004230
		public bool displayPersistentRuntimeUI
		{
			get
			{
				return this.m_RootUIPersistentCanvas != null && this.m_PersistentRoot.activeInHierarchy;
			}
			set
			{
				this.CheckPersistentCanvas();
				this.m_PersistentRoot.SetActive(value);
			}
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00006044 File Offset: 0x00004244
		private DebugManager()
		{
			if (!Debug.isDebugBuild)
			{
				return;
			}
			this.RegisterInputs();
			this.RegisterActions();
		}

		// Token: 0x06000131 RID: 305 RVA: 0x000060C0 File Offset: 0x000042C0
		public void RefreshEditor()
		{
			this.refreshEditorRequested = true;
		}

		// Token: 0x06000132 RID: 306 RVA: 0x000060C9 File Offset: 0x000042C9
		public void Reset()
		{
			Action action = this.resetData;
			if (action != null)
			{
				action();
			}
			this.ReDrawOnScreenDebug();
		}

		// Token: 0x06000133 RID: 307 RVA: 0x000060E2 File Offset: 0x000042E2
		public void ReDrawOnScreenDebug()
		{
			if (this.displayRuntimeUI)
			{
				DebugUIHandlerCanvas rootUICanvas = this.m_RootUICanvas;
				if (rootUICanvas == null)
				{
					return;
				}
				rootUICanvas.ResetAllHierarchy();
			}
		}

		// Token: 0x06000134 RID: 308 RVA: 0x000060FC File Offset: 0x000042FC
		public void RegisterData(IDebugData data)
		{
			this.resetData += data.GetReset();
		}

		// Token: 0x06000135 RID: 309 RVA: 0x0000610A File Offset: 0x0000430A
		public void UnregisterData(IDebugData data)
		{
			this.resetData -= data.GetReset();
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00006118 File Offset: 0x00004318
		public int GetState()
		{
			int num = 17;
			foreach (DebugUI.Panel panel in this.m_Panels)
			{
				num = num * 23 + panel.GetHashCode();
			}
			return num;
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00006174 File Offset: 0x00004374
		internal void RegisterRootCanvas(DebugUIHandlerCanvas root)
		{
			this.m_Root = root.gameObject;
			this.m_RootUICanvas = root;
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00006189 File Offset: 0x00004389
		internal void ChangeSelection(DebugUIHandlerWidget widget, bool fromNext)
		{
			this.m_RootUICanvas.ChangeSelection(widget, fromNext);
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00006198 File Offset: 0x00004398
		private void CheckPersistentCanvas()
		{
			if (this.m_RootUIPersistentCanvas == null)
			{
				DebugUIHandlerPersistentCanvas debugUIHandlerPersistentCanvas = Object.FindObjectOfType<DebugUIHandlerPersistentCanvas>();
				if (debugUIHandlerPersistentCanvas == null)
				{
					this.m_PersistentRoot = Object.Instantiate<Transform>(Resources.Load<Transform>("DebugUI Persistent Canvas")).gameObject;
					this.m_PersistentRoot.name = "[Debug Canvas - Persistent]";
					this.m_PersistentRoot.transform.localPosition = Vector3.zero;
				}
				else
				{
					this.m_PersistentRoot = debugUIHandlerPersistentCanvas.gameObject;
				}
				this.m_RootUIPersistentCanvas = this.m_PersistentRoot.GetComponent<DebugUIHandlerPersistentCanvas>();
			}
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00006220 File Offset: 0x00004420
		internal void TogglePersistent(DebugUI.Widget widget)
		{
			if (widget == null)
			{
				return;
			}
			DebugUI.Value value = widget as DebugUI.Value;
			if (value == null)
			{
				Debug.Log("Only DebugUI.Value items can be made persistent.");
				return;
			}
			this.CheckPersistentCanvas();
			this.m_RootUIPersistentCanvas.Toggle(value);
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00006258 File Offset: 0x00004458
		private void OnPanelDirty(DebugUI.Panel panel)
		{
			this.onSetDirty();
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00006268 File Offset: 0x00004468
		public DebugUI.Panel GetPanel(string displayName, bool createIfNull = false, int groupIndex = 0, bool overrideIfExist = false)
		{
			DebugUI.Panel panel = null;
			foreach (DebugUI.Panel panel2 in this.m_Panels)
			{
				if (panel2.displayName == displayName)
				{
					panel = panel2;
					break;
				}
			}
			if (panel != null)
			{
				if (!overrideIfExist)
				{
					return panel;
				}
				panel.onSetDirty -= this.OnPanelDirty;
				this.RemovePanel(panel);
				panel = null;
			}
			if (createIfNull)
			{
				panel = new DebugUI.Panel
				{
					displayName = displayName,
					groupIndex = groupIndex
				};
				panel.onSetDirty += this.OnPanelDirty;
				this.m_Panels.Add(panel);
				this.UpdateReadOnlyCollection();
			}
			return panel;
		}

		// Token: 0x0600013D RID: 317 RVA: 0x0000632C File Offset: 0x0000452C
		public void RemovePanel(string displayName)
		{
			DebugUI.Panel panel = null;
			foreach (DebugUI.Panel panel2 in this.m_Panels)
			{
				if (panel2.displayName == displayName)
				{
					panel2.onSetDirty -= this.OnPanelDirty;
					panel = panel2;
					break;
				}
			}
			this.RemovePanel(panel);
		}

		// Token: 0x0600013E RID: 318 RVA: 0x000063A8 File Offset: 0x000045A8
		public void RemovePanel(DebugUI.Panel panel)
		{
			if (panel == null)
			{
				return;
			}
			this.m_Panels.Remove(panel);
			this.UpdateReadOnlyCollection();
		}

		// Token: 0x0600013F RID: 319 RVA: 0x000063C4 File Offset: 0x000045C4
		public DebugUI.Widget GetItem(string queryPath)
		{
			foreach (DebugUI.Panel panel in this.m_Panels)
			{
				DebugUI.Widget item = this.GetItem(queryPath, panel);
				if (item != null)
				{
					return item;
				}
			}
			return null;
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00006424 File Offset: 0x00004624
		private DebugUI.Widget GetItem(string queryPath, DebugUI.IContainer container)
		{
			foreach (DebugUI.Widget widget in container.children)
			{
				if (widget.queryPath == queryPath)
				{
					return widget;
				}
				DebugUI.IContainer container2 = widget as DebugUI.IContainer;
				if (container2 != null)
				{
					DebugUI.Widget item = this.GetItem(queryPath, container2);
					if (item != null)
					{
						return item;
					}
				}
			}
			return null;
		}

		// Token: 0x040000D3 RID: 211
		private const string kEnableDebugBtn1 = "Enable Debug Button 1";

		// Token: 0x040000D4 RID: 212
		private const string kEnableDebugBtn2 = "Enable Debug Button 2";

		// Token: 0x040000D5 RID: 213
		private const string kDebugPreviousBtn = "Debug Previous";

		// Token: 0x040000D6 RID: 214
		private const string kDebugNextBtn = "Debug Next";

		// Token: 0x040000D7 RID: 215
		private const string kValidateBtn = "Debug Validate";

		// Token: 0x040000D8 RID: 216
		private const string kPersistentBtn = "Debug Persistent";

		// Token: 0x040000D9 RID: 217
		private const string kDPadVertical = "Debug Vertical";

		// Token: 0x040000DA RID: 218
		private const string kDPadHorizontal = "Debug Horizontal";

		// Token: 0x040000DB RID: 219
		private const string kMultiplierBtn = "Debug Multiplier";

		// Token: 0x040000DC RID: 220
		private const string kResetBtn = "Debug Reset";

		// Token: 0x040000DD RID: 221
		private DebugActionDesc[] m_DebugActions;

		// Token: 0x040000DE RID: 222
		private DebugActionState[] m_DebugActionStates;

		// Token: 0x040000DF RID: 223
		private static readonly Lazy<DebugManager> s_Instance = new Lazy<DebugManager>(() => new DebugManager());

		// Token: 0x040000E0 RID: 224
		private ReadOnlyCollection<DebugUI.Panel> m_ReadOnlyPanels;

		// Token: 0x040000E1 RID: 225
		private readonly List<DebugUI.Panel> m_Panels = new List<DebugUI.Panel>();

		// Token: 0x040000E5 RID: 229
		public bool refreshEditorRequested;

		// Token: 0x040000E6 RID: 230
		private GameObject m_Root;

		// Token: 0x040000E7 RID: 231
		private DebugUIHandlerCanvas m_RootUICanvas;

		// Token: 0x040000E8 RID: 232
		private GameObject m_PersistentRoot;

		// Token: 0x040000E9 RID: 233
		private DebugUIHandlerPersistentCanvas m_RootUIPersistentCanvas;

		// Token: 0x040000EA RID: 234
		private bool m_EditorOpen;
	}
}
