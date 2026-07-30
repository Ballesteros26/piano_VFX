using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.VFX;
using UnityEngine.VFX.Utility;

// Token: 0x02000003 RID: 3
[Serializable]
internal class VisualEffectActivationBehaviour : PlayableBehaviour
{
	// Token: 0x06000006 RID: 6 RVA: 0x00002091 File Offset: 0x00000291
	public override void OnPlayableCreate(Playable playable)
	{
	}

	// Token: 0x06000007 RID: 7 RVA: 0x000020BC File Offset: 0x000002BC
	public void SendEventEnter(VisualEffect component)
	{
		VFXEventAttribute vfxeventAttribute = VisualEffectActivationBehaviour.BuildEventAttribute(component, this.clipEnterEventAttributes);
		component.SendEvent(this.onClipEnter, vfxeventAttribute);
	}

	// Token: 0x06000008 RID: 8 RVA: 0x000020E8 File Offset: 0x000002E8
	public void SendEventExit(VisualEffect component)
	{
		VFXEventAttribute vfxeventAttribute = VisualEffectActivationBehaviour.BuildEventAttribute(component, this.clipExitEventAttributes);
		component.SendEvent(this.onClipExit, vfxeventAttribute);
	}

	// Token: 0x06000009 RID: 9 RVA: 0x00002114 File Offset: 0x00000314
	private static VFXEventAttribute BuildEventAttribute(VisualEffect component, VisualEffectActivationBehaviour.EventState[] states)
	{
		if (states == null || states.Length == 0)
		{
			return null;
		}
		VFXEventAttribute vfxeventAttribute = component.CreateVFXEventAttribute();
		foreach (VisualEffectActivationBehaviour.EventState eventState in states)
		{
			VisualEffectActivationBehaviour.AttributeType type = eventState.type;
			switch (type)
			{
			case VisualEffectActivationBehaviour.AttributeType.Float:
				vfxeventAttribute.SetFloat(eventState.attribute, eventState.values[0]);
				break;
			case VisualEffectActivationBehaviour.AttributeType.Float2:
				vfxeventAttribute.SetVector2(eventState.attribute, new Vector2(eventState.values[0], eventState.values[1]));
				break;
			case VisualEffectActivationBehaviour.AttributeType.Float3:
				vfxeventAttribute.SetVector3(eventState.attribute, new Vector3(eventState.values[0], eventState.values[1], eventState.values[2]));
				break;
			case VisualEffectActivationBehaviour.AttributeType.Float4:
				vfxeventAttribute.SetVector4(eventState.attribute, new Vector4(eventState.values[0], eventState.values[1], eventState.values[2], eventState.values[3]));
				break;
			case VisualEffectActivationBehaviour.AttributeType.Int32:
				vfxeventAttribute.SetInt(eventState.attribute, (int)eventState.values[0]);
				break;
			case VisualEffectActivationBehaviour.AttributeType.Uint32:
				vfxeventAttribute.SetUint(eventState.attribute, (uint)eventState.values[0]);
				break;
			default:
				if (type == VisualEffectActivationBehaviour.AttributeType.Boolean)
				{
					vfxeventAttribute.SetBool(eventState.attribute, eventState.values[0] != 0f);
				}
				break;
			}
		}
		return vfxeventAttribute;
	}

	// Token: 0x04000004 RID: 4
	[SerializeField]
	private ExposedProperty onClipEnter = "OnPlay";

	// Token: 0x04000005 RID: 5
	[SerializeField]
	private ExposedProperty onClipExit = "OnStop";

	// Token: 0x04000006 RID: 6
	[SerializeField]
	private VisualEffectActivationBehaviour.EventState[] clipEnterEventAttributes;

	// Token: 0x04000007 RID: 7
	[SerializeField]
	private VisualEffectActivationBehaviour.EventState[] clipExitEventAttributes;

	// Token: 0x0200002A RID: 42
	[Serializable]
	public enum AttributeType
	{
		// Token: 0x040000A5 RID: 165
		Float = 1,
		// Token: 0x040000A6 RID: 166
		Float2,
		// Token: 0x040000A7 RID: 167
		Float3,
		// Token: 0x040000A8 RID: 168
		Float4,
		// Token: 0x040000A9 RID: 169
		Int32,
		// Token: 0x040000AA RID: 170
		Uint32,
		// Token: 0x040000AB RID: 171
		Boolean = 17
	}

	// Token: 0x0200002B RID: 43
	[Serializable]
	public struct EventState
	{
		// Token: 0x040000AC RID: 172
		public ExposedProperty attribute;

		// Token: 0x040000AD RID: 173
		public VisualEffectActivationBehaviour.AttributeType type;

		// Token: 0x040000AE RID: 174
		public float[] values;
	}
}
