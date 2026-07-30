using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x0200013C RID: 316
	internal struct FrameSettingsHistory
	{
		// Token: 0x17000168 RID: 360
		// (get) Token: 0x0600093F RID: 2367 RVA: 0x0004B580 File Offset: 0x00049780
		public static bool enabled
		{
			get
			{
				if (!FrameSettingsHistory.s_PossiblyInUse)
				{
					return FrameSettingsHistory.s_PossiblyInUse = DebugManager.instance.displayEditorUI || DebugManager.instance.displayRuntimeUI;
				}
				if (DebugManager.instance.displayEditorUI || DebugManager.instance.displayRuntimeUI)
				{
					return true;
				}
				if (FrameSettingsHistory.s_PossiblyInUse)
				{
					return FrameSettingsHistory.s_PossiblyInUse = FrameSettingsHistory.containers.Any((IFrameSettingsHistoryContainer history) => history.frameSettingsHistory.hasDebug);
				}
				return false;
			}
		}

		// Token: 0x06000940 RID: 2368 RVA: 0x0004B608 File Offset: 0x00049808
		static FrameSettingsHistory()
		{
			Type typeFromHandle = typeof(FrameSettingsField);
			foreach (object obj in Enum.GetValues(typeFromHandle))
			{
				FrameSettingsField frameSettingsField = (FrameSettingsField)obj;
				FrameSettingsHistory.attributes[frameSettingsField] = typeFromHandle.GetField(Enum.GetName(typeFromHandle, frameSettingsField)).GetCustomAttribute<FrameSettingsFieldAttribute>();
			}
		}

		// Token: 0x06000941 RID: 2369 RVA: 0x0004B708 File Offset: 0x00049908
		public static void AggregateFrameSettings(ref FrameSettings aggregatedFrameSettings, Camera camera, HDAdditionalCameraData additionalData, HDRenderPipelineAsset hdrpAsset, HDRenderPipelineAsset defaultHdrpAsset)
		{
			FrameSettingsHistory.AggregateFrameSettings(ref aggregatedFrameSettings, camera, additionalData, defaultHdrpAsset.GetDefaultFrameSettings((additionalData != null) ? additionalData.defaultFrameSettings : FrameSettingsRenderType.Camera), hdrpAsset.currentPlatformRenderPipelineSettings);
		}

		// Token: 0x06000942 RID: 2370 RVA: 0x0004B72C File Offset: 0x0004992C
		public static void AggregateFrameSettings(ref FrameSettings aggregatedFrameSettings, Camera camera, IFrameSettingsHistoryContainer historyContainer, ref FrameSettings defaultFrameSettings, RenderPipelineSettings supportedFeatures)
		{
			FrameSettingsHistory frameSettingsHistory = historyContainer.frameSettingsHistory;
			aggregatedFrameSettings = defaultFrameSettings;
			bool flag = false;
			if (historyContainer.hasCustomFrameSettings)
			{
				FrameSettings.Override(ref aggregatedFrameSettings, historyContainer.frameSettings, historyContainer.frameSettingsMask);
				flag = frameSettingsHistory.customMask.mask != historyContainer.frameSettingsMask.mask;
				frameSettingsHistory.customMask = historyContainer.frameSettingsMask;
			}
			frameSettingsHistory.overridden = aggregatedFrameSettings;
			FrameSettings.Sanitize(ref aggregatedFrameSettings, camera, supportedFeatures);
			frameSettingsHistory.hasDebug = frameSettingsHistory.debug != aggregatedFrameSettings;
			flag |= frameSettingsHistory.sanitazed != aggregatedFrameSettings;
			bool flag2 = !frameSettingsHistory.hasDebug || flag;
			frameSettingsHistory.sanitazed = aggregatedFrameSettings;
			if (flag2)
			{
				frameSettingsHistory.debug = frameSettingsHistory.sanitazed;
			}
			else
			{
				FrameSettings.Sanitize(ref frameSettingsHistory.debug, camera, supportedFeatures);
			}
			aggregatedFrameSettings = frameSettingsHistory.debug;
			historyContainer.frameSettingsHistory = frameSettingsHistory;
		}

		// Token: 0x06000943 RID: 2371 RVA: 0x0004B820 File Offset: 0x00049A20
		private static DebugUI.HistoryBoolField GenerateHistoryBoolField(HDRenderPipelineAsset defaultHdrpAsset, IFrameSettingsHistoryContainer frameSettingsContainer, FrameSettingsField field, FrameSettingsFieldAttribute attribute)
		{
			string text = "";
			for (int i = 0; i < attribute.indentLevel; i++)
			{
				text += "  ";
			}
			return new DebugUI.HistoryBoolField
			{
				displayName = text + attribute.displayedName,
				getter = () => frameSettingsContainer.frameSettingsHistory.debug.IsEnabled(field),
				setter = delegate(bool value)
				{
					FrameSettingsHistory frameSettingsHistory = frameSettingsContainer.frameSettingsHistory;
					frameSettingsHistory.debug.SetEnabled(field, value);
					frameSettingsContainer.frameSettingsHistory = frameSettingsHistory;
				},
				historyGetter = new Func<bool>[]
				{
					() => frameSettingsContainer.frameSettingsHistory.sanitazed.IsEnabled(field),
					() => frameSettingsContainer.frameSettingsHistory.overridden.IsEnabled(field),
					() => defaultHdrpAsset.GetDefaultFrameSettings(frameSettingsContainer.frameSettingsHistory.defaultType).IsEnabled(field)
				}
			};
		}

		// Token: 0x06000944 RID: 2372 RVA: 0x0004B8E4 File Offset: 0x00049AE4
		private static DebugUI.HistoryEnumField GenerateHistoryEnumField(HDRenderPipelineAsset defaultHdrpAsset, IFrameSettingsHistoryContainer frameSettingsContainer, FrameSettingsField field, FrameSettingsFieldAttribute attribute, Type autoEnum)
		{
			string text = "";
			for (int i = 0; i < attribute.indentLevel; i++)
			{
				text += "  ";
			}
			DebugUI.HistoryEnumField historyEnumField = new DebugUI.HistoryEnumField();
			historyEnumField.displayName = text + attribute.displayedName;
			historyEnumField.getter = delegate
			{
				if (!frameSettingsContainer.frameSettingsHistory.debug.IsEnabled(field))
				{
					return 0;
				}
				return 1;
			};
			historyEnumField.setter = delegate(int value)
			{
				FrameSettingsHistory frameSettingsHistory = frameSettingsContainer.frameSettingsHistory;
				frameSettingsHistory.debug.SetEnabled(field, value == 1);
				frameSettingsContainer.frameSettingsHistory = frameSettingsHistory;
			};
			historyEnumField.autoEnum = autoEnum;
			historyEnumField.getIndex = delegate
			{
				if (!frameSettingsContainer.frameSettingsHistory.debug.IsEnabled(field))
				{
					return 0;
				}
				return 1;
			};
			historyEnumField.setIndex = delegate(int a)
			{
			};
			historyEnumField.historyIndexGetter = new Func<int>[]
			{
				delegate
				{
					if (!frameSettingsContainer.frameSettingsHistory.sanitazed.IsEnabled(field))
					{
						return 0;
					}
					return 1;
				},
				delegate
				{
					if (!frameSettingsContainer.frameSettingsHistory.overridden.IsEnabled(field))
					{
						return 0;
					}
					return 1;
				},
				delegate
				{
					if (!defaultHdrpAsset.GetDefaultFrameSettings(frameSettingsContainer.frameSettingsHistory.defaultType).IsEnabled(field))
					{
						return 0;
					}
					return 1;
				}
			};
			return historyEnumField;
		}

		// Token: 0x06000945 RID: 2373 RVA: 0x0004B9E4 File Offset: 0x00049BE4
		private static ObservableList<DebugUI.Widget> GenerateHistoryArea(HDRenderPipelineAsset defaultHdrpAsset, IFrameSettingsHistoryContainer frameSettingsContainer, int groupIndex)
		{
			if (!FrameSettingsHistory.attributesGroup.ContainsKey(groupIndex) || FrameSettingsHistory.attributesGroup[groupIndex] == null)
			{
				Dictionary<int, IOrderedEnumerable<KeyValuePair<FrameSettingsField, FrameSettingsFieldAttribute>>> dictionary = FrameSettingsHistory.attributesGroup;
				int groupIndex2 = groupIndex;
				Dictionary<FrameSettingsField, FrameSettingsFieldAttribute> dictionary2 = FrameSettingsHistory.attributes;
				IOrderedEnumerable<KeyValuePair<FrameSettingsField, FrameSettingsFieldAttribute>> orderedEnumerable;
				if (dictionary2 == null)
				{
					orderedEnumerable = null;
				}
				else
				{
					IEnumerable<KeyValuePair<FrameSettingsField, FrameSettingsFieldAttribute>> enumerable = dictionary2.Where(delegate(KeyValuePair<FrameSettingsField, FrameSettingsFieldAttribute> pair)
					{
						FrameSettingsFieldAttribute value = pair.Value;
						return value != null && value.group == groupIndex;
					});
					if (enumerable == null)
					{
						orderedEnumerable = null;
					}
					else
					{
						orderedEnumerable = enumerable.OrderBy((KeyValuePair<FrameSettingsField, FrameSettingsFieldAttribute> pair) => pair.Value.orderInGroup);
					}
				}
				dictionary[groupIndex2] = orderedEnumerable;
			}
			if (!FrameSettingsHistory.attributesGroup.ContainsKey(groupIndex))
			{
				throw new ArgumentException("Unknown groupIndex");
			}
			ObservableList<DebugUI.Widget> observableList = new ObservableList<DebugUI.Widget>();
			foreach (KeyValuePair<FrameSettingsField, FrameSettingsFieldAttribute> keyValuePair in FrameSettingsHistory.attributesGroup[groupIndex])
			{
				switch (keyValuePair.Value.type)
				{
				case FrameSettingsFieldAttribute.DisplayType.BoolAsCheckbox:
					observableList.Add(FrameSettingsHistory.GenerateHistoryBoolField(defaultHdrpAsset, frameSettingsContainer, keyValuePair.Key, keyValuePair.Value));
					break;
				case FrameSettingsFieldAttribute.DisplayType.BoolAsEnumPopup:
					observableList.Add(FrameSettingsHistory.GenerateHistoryEnumField(defaultHdrpAsset, frameSettingsContainer, keyValuePair.Key, keyValuePair.Value, FrameSettingsHistory.RetrieveEnumTypeByField(keyValuePair.Key)));
					break;
				}
			}
			return observableList;
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x0004BB48 File Offset: 0x00049D48
		private static DebugUI.Widget[] GenerateFrameSettingsPanelContent(HDRenderPipelineAsset defaultHdrpAsset, IFrameSettingsHistoryContainer frameSettingsContainer)
		{
			DebugUI.Widget[] array = new DebugUI.Widget[FrameSettingsHistory.foldoutNames.Length];
			for (int i = 0; i < FrameSettingsHistory.foldoutNames.Length; i++)
			{
				array[i] = new DebugUI.Foldout(FrameSettingsHistory.foldoutNames[i], FrameSettingsHistory.GenerateHistoryArea(defaultHdrpAsset, frameSettingsContainer, i), FrameSettingsHistory.columnNames);
			}
			return array;
		}

		// Token: 0x06000947 RID: 2375 RVA: 0x0004BB94 File Offset: 0x00049D94
		private static void GenerateFrameSettingsPanel(string menuName, IFrameSettingsHistoryContainer frameSettingsContainer)
		{
			HDRenderPipelineAsset defaultAsset = HDRenderPipeline.defaultAsset;
			List<DebugUI.Widget> list = new List<DebugUI.Widget>();
			list.AddRange(FrameSettingsHistory.GenerateFrameSettingsPanelContent(defaultAsset, frameSettingsContainer));
			DebugManager.instance.GetPanel(menuName, true, 2, true).children.Add(list.ToArray());
		}

		// Token: 0x06000948 RID: 2376 RVA: 0x0004BBD8 File Offset: 0x00049DD8
		private static Type RetrieveEnumTypeByField(FrameSettingsField field)
		{
			if (field == FrameSettingsField.LitShaderMode)
			{
				return typeof(LitShaderMode);
			}
			throw new ArgumentException("Unknown enum type for this field");
		}

		// Token: 0x06000949 RID: 2377 RVA: 0x0004BBF2 File Offset: 0x00049DF2
		public static IDebugData RegisterDebug(IFrameSettingsHistoryContainer frameSettingsContainer, bool sceneViewCamera = false)
		{
			FrameSettingsHistory.GenerateFrameSettingsPanel(frameSettingsContainer.panelName, frameSettingsContainer);
			FrameSettingsHistory.containers.Add(frameSettingsContainer);
			return frameSettingsContainer;
		}

		// Token: 0x0600094A RID: 2378 RVA: 0x0004BC0D File Offset: 0x00049E0D
		public static void UnRegisterDebug(IFrameSettingsHistoryContainer container)
		{
			DebugManager.instance.RemovePanel(container.panelName);
			FrameSettingsHistory.containers.Remove(container);
		}

		// Token: 0x0600094B RID: 2379 RVA: 0x0004BC2B File Offset: 0x00049E2B
		public static bool IsRegistered(IFrameSettingsHistoryContainer container, bool sceneViewCamera = false)
		{
			return sceneViewCamera || FrameSettingsHistory.containers.Contains(container);
		}

		// Token: 0x0600094C RID: 2380 RVA: 0x0004BC3D File Offset: 0x00049E3D
		internal void TriggerReset()
		{
			this.debug = this.sanitazed;
			this.hasDebug = false;
		}

		// Token: 0x04000EBE RID: 3774
		internal static readonly string[] foldoutNames = new string[] { "Rendering", "Lighting", "Async Compute", "Light Loop" };

		// Token: 0x04000EBF RID: 3775
		private static readonly string[] columnNames = new string[] { "Debug", "Sanitized", "Overridden", "Default" };

		// Token: 0x04000EC0 RID: 3776
		private static readonly Dictionary<FrameSettingsField, FrameSettingsFieldAttribute> attributes = new Dictionary<FrameSettingsField, FrameSettingsFieldAttribute>();

		// Token: 0x04000EC1 RID: 3777
		private static Dictionary<int, IOrderedEnumerable<KeyValuePair<FrameSettingsField, FrameSettingsFieldAttribute>>> attributesGroup = new Dictionary<int, IOrderedEnumerable<KeyValuePair<FrameSettingsField, FrameSettingsFieldAttribute>>>();

		// Token: 0x04000EC2 RID: 3778
		internal static HashSet<IFrameSettingsHistoryContainer> containers = new HashSet<IFrameSettingsHistoryContainer>();

		// Token: 0x04000EC3 RID: 3779
		public FrameSettingsRenderType defaultType;

		// Token: 0x04000EC4 RID: 3780
		public FrameSettings overridden;

		// Token: 0x04000EC5 RID: 3781
		public FrameSettingsOverrideMask customMask;

		// Token: 0x04000EC6 RID: 3782
		public FrameSettings sanitazed;

		// Token: 0x04000EC7 RID: 3783
		public FrameSettings debug;

		// Token: 0x04000EC8 RID: 3784
		private bool hasDebug;

		// Token: 0x04000EC9 RID: 3785
		private static bool s_PossiblyInUse;
	}
}
