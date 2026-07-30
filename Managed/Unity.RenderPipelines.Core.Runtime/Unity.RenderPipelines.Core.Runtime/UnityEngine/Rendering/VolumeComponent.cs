using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace UnityEngine.Rendering
{
	// Token: 0x02000066 RID: 102
	[Serializable]
	public class VolumeComponent : ScriptableObject
	{
		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060002E2 RID: 738 RVA: 0x0000C461 File Offset: 0x0000A661
		// (set) Token: 0x060002E3 RID: 739 RVA: 0x0000C469 File Offset: 0x0000A669
		public string displayName { get; protected set; } = "";

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060002E4 RID: 740 RVA: 0x0000C472 File Offset: 0x0000A672
		// (set) Token: 0x060002E5 RID: 741 RVA: 0x0000C47A File Offset: 0x0000A67A
		public ReadOnlyCollection<VolumeParameter> parameters { get; private set; }

		// Token: 0x060002E6 RID: 742 RVA: 0x0000C484 File Offset: 0x0000A684
		protected virtual void OnEnable()
		{
			this.parameters = (from t in base.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
				where t.FieldType.IsSubclassOf(typeof(VolumeParameter))
				orderby t.MetadataToken
				select (VolumeParameter)t.GetValue(this)).ToList<VolumeParameter>().AsReadOnly();
			foreach (VolumeParameter volumeParameter in this.parameters)
			{
				volumeParameter.OnEnable();
			}
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x0000C544 File Offset: 0x0000A744
		protected virtual void OnDisable()
		{
			if (this.parameters == null)
			{
				return;
			}
			foreach (VolumeParameter volumeParameter in this.parameters)
			{
				volumeParameter.OnDisable();
			}
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x0000C598 File Offset: 0x0000A798
		public virtual void Override(VolumeComponent state, float interpFactor)
		{
			int count = this.parameters.Count;
			for (int i = 0; i < count; i++)
			{
				VolumeParameter volumeParameter = state.parameters[i];
				VolumeParameter volumeParameter2 = this.parameters[i];
				volumeParameter.overrideState = volumeParameter2.overrideState;
				if (volumeParameter2.overrideState)
				{
					volumeParameter.Interp(volumeParameter, volumeParameter2, interpFactor);
				}
			}
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x0000C5F4 File Offset: 0x0000A7F4
		public void SetAllOverridesTo(bool state)
		{
			this.SetAllOverridesTo(this.parameters, state);
		}

		// Token: 0x060002EA RID: 746 RVA: 0x0000C604 File Offset: 0x0000A804
		private void SetAllOverridesTo(IEnumerable<VolumeParameter> enumerable, bool state)
		{
			foreach (VolumeParameter volumeParameter in enumerable)
			{
				volumeParameter.overrideState = state;
				Type type = volumeParameter.GetType();
				if (VolumeParameter.IsObjectParameter(type))
				{
					ReadOnlyCollection<VolumeParameter> readOnlyCollection = (ReadOnlyCollection<VolumeParameter>)type.GetProperty("parameters", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(volumeParameter, null);
					if (readOnlyCollection != null)
					{
						this.SetAllOverridesTo(readOnlyCollection, state);
					}
				}
			}
		}

		// Token: 0x060002EB RID: 747 RVA: 0x0000C680 File Offset: 0x0000A880
		public override int GetHashCode()
		{
			int num = 17;
			for (int i = 0; i < this.parameters.Count; i++)
			{
				num = num * 23 + this.parameters[i].GetHashCode();
			}
			return num;
		}

		// Token: 0x0400019E RID: 414
		public bool active = true;

		// Token: 0x040001A1 RID: 417
		[SerializeField]
		private bool m_AdvancedMode;
	}
}
