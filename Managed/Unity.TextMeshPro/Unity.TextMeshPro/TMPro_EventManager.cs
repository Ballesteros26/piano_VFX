using System;
using UnityEngine;

namespace TMPro
{
	// Token: 0x0200005F RID: 95
	public static class TMPro_EventManager
	{
		// Token: 0x060004B1 RID: 1201 RVA: 0x00023163 File Offset: 0x00021363
		public static void ON_PRE_RENDER_OBJECT_CHANGED()
		{
			TMPro_EventManager.OnPreRenderObject_Event.Call();
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x0002316F File Offset: 0x0002136F
		public static void ON_MATERIAL_PROPERTY_CHANGED(bool isChanged, Material mat)
		{
			TMPro_EventManager.MATERIAL_PROPERTY_EVENT.Call(isChanged, mat);
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x0002317D File Offset: 0x0002137D
		public static void ON_FONT_PROPERTY_CHANGED(bool isChanged, TMP_FontAsset font)
		{
			TMPro_EventManager.FONT_PROPERTY_EVENT.Call(isChanged, font);
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x0002318B File Offset: 0x0002138B
		public static void ON_SPRITE_ASSET_PROPERTY_CHANGED(bool isChanged, global::UnityEngine.Object obj)
		{
			TMPro_EventManager.SPRITE_ASSET_PROPERTY_EVENT.Call(isChanged, obj);
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x00023199 File Offset: 0x00021399
		public static void ON_TEXTMESHPRO_PROPERTY_CHANGED(bool isChanged, TextMeshPro obj)
		{
			TMPro_EventManager.TEXTMESHPRO_PROPERTY_EVENT.Call(isChanged, obj);
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x000231A7 File Offset: 0x000213A7
		public static void ON_DRAG_AND_DROP_MATERIAL_CHANGED(GameObject sender, Material currentMaterial, Material newMaterial)
		{
			TMPro_EventManager.DRAG_AND_DROP_MATERIAL_EVENT.Call(sender, currentMaterial, newMaterial);
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x000231B6 File Offset: 0x000213B6
		public static void ON_TEXT_STYLE_PROPERTY_CHANGED(bool isChanged)
		{
			TMPro_EventManager.TEXT_STYLE_PROPERTY_EVENT.Call(isChanged);
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x000231C3 File Offset: 0x000213C3
		public static void ON_COLOR_GRADIENT_PROPERTY_CHANGED(TMP_ColorGradient gradient)
		{
			TMPro_EventManager.COLOR_GRADIENT_PROPERTY_EVENT.Call(gradient);
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x000231D0 File Offset: 0x000213D0
		public static void ON_TEXT_CHANGED(global::UnityEngine.Object obj)
		{
			TMPro_EventManager.TEXT_CHANGED_EVENT.Call(obj);
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x000231DD File Offset: 0x000213DD
		public static void ON_TMP_SETTINGS_CHANGED()
		{
			TMPro_EventManager.TMP_SETTINGS_PROPERTY_EVENT.Call();
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x000231E9 File Offset: 0x000213E9
		public static void ON_RESOURCES_LOADED()
		{
			TMPro_EventManager.RESOURCE_LOAD_EVENT.Call();
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x000231F5 File Offset: 0x000213F5
		public static void ON_TEXTMESHPRO_UGUI_PROPERTY_CHANGED(bool isChanged, TextMeshProUGUI obj)
		{
			TMPro_EventManager.TEXTMESHPRO_UGUI_PROPERTY_EVENT.Call(isChanged, obj);
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x00023203 File Offset: 0x00021403
		public static void ON_COMPUTE_DT_EVENT(object Sender, Compute_DT_EventArgs e)
		{
			TMPro_EventManager.COMPUTE_DT_EVENT.Call(Sender, e);
		}

		// Token: 0x0400044E RID: 1102
		public static readonly FastAction<object, Compute_DT_EventArgs> COMPUTE_DT_EVENT = new FastAction<object, Compute_DT_EventArgs>();

		// Token: 0x0400044F RID: 1103
		public static readonly FastAction<bool, Material> MATERIAL_PROPERTY_EVENT = new FastAction<bool, Material>();

		// Token: 0x04000450 RID: 1104
		public static readonly FastAction<bool, TMP_FontAsset> FONT_PROPERTY_EVENT = new FastAction<bool, TMP_FontAsset>();

		// Token: 0x04000451 RID: 1105
		public static readonly FastAction<bool, global::UnityEngine.Object> SPRITE_ASSET_PROPERTY_EVENT = new FastAction<bool, global::UnityEngine.Object>();

		// Token: 0x04000452 RID: 1106
		public static readonly FastAction<bool, TextMeshPro> TEXTMESHPRO_PROPERTY_EVENT = new FastAction<bool, TextMeshPro>();

		// Token: 0x04000453 RID: 1107
		public static readonly FastAction<GameObject, Material, Material> DRAG_AND_DROP_MATERIAL_EVENT = new FastAction<GameObject, Material, Material>();

		// Token: 0x04000454 RID: 1108
		public static readonly FastAction<bool> TEXT_STYLE_PROPERTY_EVENT = new FastAction<bool>();

		// Token: 0x04000455 RID: 1109
		public static readonly FastAction<TMP_ColorGradient> COLOR_GRADIENT_PROPERTY_EVENT = new FastAction<TMP_ColorGradient>();

		// Token: 0x04000456 RID: 1110
		public static readonly FastAction TMP_SETTINGS_PROPERTY_EVENT = new FastAction();

		// Token: 0x04000457 RID: 1111
		public static readonly FastAction RESOURCE_LOAD_EVENT = new FastAction();

		// Token: 0x04000458 RID: 1112
		public static readonly FastAction<bool, TextMeshProUGUI> TEXTMESHPRO_UGUI_PROPERTY_EVENT = new FastAction<bool, TextMeshProUGUI>();

		// Token: 0x04000459 RID: 1113
		public static readonly FastAction OnPreRenderObject_Event = new FastAction();

		// Token: 0x0400045A RID: 1114
		public static readonly FastAction<global::UnityEngine.Object> TEXT_CHANGED_EVENT = new FastAction<global::UnityEngine.Object>();
	}
}
