using System;
using System.Collections.Generic;
using System.Linq;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x02000097 RID: 151
	public class DebugUIHandlerCanvas : MonoBehaviour
	{
		// Token: 0x0600039F RID: 927 RVA: 0x0000E1C8 File Offset: 0x0000C3C8
		private void OnEnable()
		{
			if (this.prefabs == null)
			{
				this.prefabs = new List<DebugUIPrefabBundle>();
			}
			if (this.m_PrefabsMap == null)
			{
				this.m_PrefabsMap = new Dictionary<Type, Transform>();
			}
			if (this.m_UIPanels == null)
			{
				this.m_UIPanels = new List<DebugUIHandlerPanel>();
			}
			DebugManager.instance.RegisterRootCanvas(this);
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x0000E21C File Offset: 0x0000C41C
		private void Update()
		{
			int state = DebugManager.instance.GetState();
			if (this.m_DebugTreeState != state)
			{
				this.ResetAllHierarchy();
			}
			this.HandleInput();
			if (this.m_UIPanels != null && this.m_SelectedPanel < this.m_UIPanels.Count && this.m_UIPanels[this.m_SelectedPanel] != null)
			{
				this.m_UIPanels[this.m_SelectedPanel].ScrollTo(this.m_SelectedWidget);
			}
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x0000E29C File Offset: 0x0000C49C
		internal void ResetAllHierarchy()
		{
			foreach (object obj in base.transform)
			{
				CoreUtils.Destroy(((Transform)obj).gameObject);
			}
			this.Rebuild();
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x0000E300 File Offset: 0x0000C500
		private void Rebuild()
		{
			this.m_PrefabsMap.Clear();
			foreach (DebugUIPrefabBundle debugUIPrefabBundle in this.prefabs)
			{
				Type type = Type.GetType(debugUIPrefabBundle.type);
				if (type != null && debugUIPrefabBundle.prefab != null)
				{
					this.m_PrefabsMap.Add(type, debugUIPrefabBundle.prefab);
				}
			}
			this.m_UIPanels.Clear();
			this.m_DebugTreeState = DebugManager.instance.GetState();
			foreach (DebugUI.Panel panel in DebugManager.instance.panels)
			{
				if (!panel.isEditorOnly)
				{
					if (panel.children.Count((DebugUI.Widget x) => !x.isEditorOnly) != 0)
					{
						GameObject gameObject = Object.Instantiate<Transform>(this.panelPrefab, base.transform, false).gameObject;
						gameObject.name = panel.displayName;
						DebugUIHandlerPanel component = gameObject.GetComponent<DebugUIHandlerPanel>();
						component.SetPanel(panel);
						this.m_UIPanels.Add(component);
						DebugUIHandlerContainer component2 = gameObject.GetComponent<DebugUIHandlerContainer>();
						this.Traverse(panel, component2.contentHolder, null);
					}
				}
			}
			this.ActivatePanel(this.m_SelectedPanel, true);
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x0000E488 File Offset: 0x0000C688
		private void Traverse(DebugUI.IContainer container, Transform parentTransform, DebugUIHandlerWidget parentUIHandler)
		{
			DebugUIHandlerWidget debugUIHandlerWidget = null;
			for (int i = 0; i < container.children.Count; i++)
			{
				DebugUI.Widget widget = container.children[i];
				if (!widget.isEditorOnly)
				{
					Transform transform;
					if (!this.m_PrefabsMap.TryGetValue(widget.GetType(), out transform))
					{
						Debug.LogWarning("DebugUI widget doesn't have a prefab: " + widget.GetType());
					}
					else
					{
						GameObject gameObject = Object.Instantiate<Transform>(transform, parentTransform, false).gameObject;
						gameObject.name = widget.displayName;
						DebugUIHandlerWidget component = gameObject.GetComponent<DebugUIHandlerWidget>();
						if (component == null)
						{
							Debug.LogWarning("DebugUI prefab is missing a DebugUIHandler for: " + widget.GetType());
						}
						else
						{
							if (debugUIHandlerWidget != null)
							{
								debugUIHandlerWidget.nextUIHandler = component;
							}
							component.previousUIHandler = debugUIHandlerWidget;
							debugUIHandlerWidget = component;
							component.parentUIHandler = parentUIHandler;
							component.SetWidget(widget);
							DebugUIHandlerContainer component2 = gameObject.GetComponent<DebugUIHandlerContainer>();
							if (component2 != null && widget is DebugUI.IContainer)
							{
								this.Traverse(widget as DebugUI.IContainer, component2.contentHolder, component);
							}
						}
					}
				}
			}
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x0000E59C File Offset: 0x0000C79C
		private DebugUIHandlerWidget GetWidgetFromPath(string queryPath)
		{
			if (string.IsNullOrEmpty(queryPath))
			{
				return null;
			}
			return this.m_UIPanels[this.m_SelectedPanel].GetComponentsInChildren<DebugUIHandlerWidget>().FirstOrDefault((DebugUIHandlerWidget w) => w.GetWidget().queryPath == queryPath);
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x0000E5EC File Offset: 0x0000C7EC
		private void ActivatePanel(int index, bool tryAndKeepSelection = false)
		{
			if (this.m_UIPanels.Count == 0)
			{
				return;
			}
			if (index >= this.m_UIPanels.Count)
			{
				index = this.m_UIPanels.Count - 1;
			}
			this.m_UIPanels.ForEach(delegate(DebugUIHandlerPanel p)
			{
				p.gameObject.SetActive(false);
			});
			this.m_UIPanels[index].gameObject.SetActive(true);
			this.m_SelectedPanel = index;
			DebugUIHandlerWidget debugUIHandlerWidget = null;
			if (tryAndKeepSelection && !string.IsNullOrEmpty(this.m_CurrentQueryPath))
			{
				debugUIHandlerWidget = this.m_UIPanels[this.m_SelectedPanel].GetComponentsInChildren<DebugUIHandlerWidget>().FirstOrDefault((DebugUIHandlerWidget w) => w.GetWidget().queryPath == this.m_CurrentQueryPath);
			}
			if (debugUIHandlerWidget == null)
			{
				debugUIHandlerWidget = this.m_UIPanels[index].GetFirstItem();
			}
			this.ChangeSelection(debugUIHandlerWidget, true);
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x0000E6CC File Offset: 0x0000C8CC
		internal void ChangeSelection(DebugUIHandlerWidget widget, bool fromNext)
		{
			if (widget == null)
			{
				return;
			}
			if (this.m_SelectedWidget != null)
			{
				this.m_SelectedWidget.OnDeselection();
			}
			DebugUIHandlerWidget selectedWidget = this.m_SelectedWidget;
			this.m_SelectedWidget = widget;
			if (!this.m_SelectedWidget.OnSelection(fromNext, selectedWidget))
			{
				if (fromNext)
				{
					this.SelectNextItem();
					return;
				}
				this.SelectPreviousItem();
				return;
			}
			else
			{
				if (this.m_SelectedWidget == null || this.m_SelectedWidget.GetWidget() == null)
				{
					this.m_CurrentQueryPath = string.Empty;
					return;
				}
				this.m_CurrentQueryPath = this.m_SelectedWidget.GetWidget().queryPath;
				return;
			}
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x0000E768 File Offset: 0x0000C968
		private void SelectPreviousItem()
		{
			if (this.m_SelectedWidget == null)
			{
				return;
			}
			DebugUIHandlerWidget debugUIHandlerWidget = this.m_SelectedWidget.Previous();
			if (debugUIHandlerWidget != null)
			{
				this.ChangeSelection(debugUIHandlerWidget, false);
			}
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x0000E7A4 File Offset: 0x0000C9A4
		private void SelectNextItem()
		{
			if (this.m_SelectedWidget == null)
			{
				return;
			}
			DebugUIHandlerWidget debugUIHandlerWidget = this.m_SelectedWidget.Next();
			if (debugUIHandlerWidget != null)
			{
				this.ChangeSelection(debugUIHandlerWidget, true);
			}
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x0000E7E0 File Offset: 0x0000C9E0
		private void ChangeSelectionValue(float multiplier)
		{
			if (this.m_SelectedWidget == null)
			{
				return;
			}
			bool flag = DebugManager.instance.GetAction(DebugAction.Multiplier) != 0f;
			if (multiplier < 0f)
			{
				this.m_SelectedWidget.OnDecrement(flag);
				return;
			}
			this.m_SelectedWidget.OnIncrement(flag);
		}

		// Token: 0x060003AA RID: 938 RVA: 0x0000E833 File Offset: 0x0000CA33
		private void ActivateSelection()
		{
			if (this.m_SelectedWidget == null)
			{
				return;
			}
			this.m_SelectedWidget.OnAction();
		}

		// Token: 0x060003AB RID: 939 RVA: 0x0000E850 File Offset: 0x0000CA50
		private void HandleInput()
		{
			if (DebugManager.instance.GetAction(DebugAction.PreviousDebugPanel) != 0f)
			{
				int num = this.m_SelectedPanel - 1;
				if (num < 0)
				{
					num = this.m_UIPanels.Count - 1;
				}
				num = Mathf.Clamp(num, 0, this.m_UIPanels.Count - 1);
				this.ActivatePanel(num, false);
			}
			if (DebugManager.instance.GetAction(DebugAction.NextDebugPanel) != 0f)
			{
				int num2 = this.m_SelectedPanel + 1;
				if (num2 >= this.m_UIPanels.Count)
				{
					num2 = 0;
				}
				num2 = Mathf.Clamp(num2, 0, this.m_UIPanels.Count - 1);
				this.ActivatePanel(num2, false);
			}
			if (DebugManager.instance.GetAction(DebugAction.Action) != 0f)
			{
				this.ActivateSelection();
			}
			if (DebugManager.instance.GetAction(DebugAction.MakePersistent) != 0f && this.m_SelectedWidget != null)
			{
				DebugManager.instance.TogglePersistent(this.m_SelectedWidget.GetWidget());
			}
			float action = DebugManager.instance.GetAction(DebugAction.MoveHorizontal);
			if (action != 0f)
			{
				this.ChangeSelectionValue(action);
			}
			float action2 = DebugManager.instance.GetAction(DebugAction.MoveVertical);
			if (action2 != 0f)
			{
				if (action2 < 0f)
				{
					this.SelectNextItem();
					return;
				}
				this.SelectPreviousItem();
			}
		}

		// Token: 0x040001DD RID: 477
		private int m_DebugTreeState;

		// Token: 0x040001DE RID: 478
		private Dictionary<Type, Transform> m_PrefabsMap;

		// Token: 0x040001DF RID: 479
		public Transform panelPrefab;

		// Token: 0x040001E0 RID: 480
		public List<DebugUIPrefabBundle> prefabs;

		// Token: 0x040001E1 RID: 481
		private List<DebugUIHandlerPanel> m_UIPanels;

		// Token: 0x040001E2 RID: 482
		private int m_SelectedPanel;

		// Token: 0x040001E3 RID: 483
		private DebugUIHandlerWidget m_SelectedWidget;

		// Token: 0x040001E4 RID: 484
		private string m_CurrentQueryPath;
	}
}
