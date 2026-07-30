using System;

namespace UnityEngine.Timeline
{
	// Token: 0x02000025 RID: 37
	public abstract class Marker : ScriptableObject, IMarker
	{
		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000214 RID: 532 RVA: 0x00007CAD File Offset: 0x00005EAD
		// (set) Token: 0x06000215 RID: 533 RVA: 0x00007CB5 File Offset: 0x00005EB5
		public TrackAsset parent { get; private set; }

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000216 RID: 534 RVA: 0x00007CBE File Offset: 0x00005EBE
		// (set) Token: 0x06000217 RID: 535 RVA: 0x00007CC6 File Offset: 0x00005EC6
		public double time
		{
			get
			{
				return this.m_Time;
			}
			set
			{
				this.m_Time = Math.Max(value, 0.0);
			}
		}

		// Token: 0x06000218 RID: 536 RVA: 0x00007CE0 File Offset: 0x00005EE0
		void IMarker.Initialize(TrackAsset parentTrack)
		{
			if (this.parent == null)
			{
				this.parent = parentTrack;
				try
				{
					this.OnInitialize(parentTrack);
				}
				catch (Exception ex)
				{
					Debug.LogError(ex.Message, this);
				}
			}
		}

		// Token: 0x06000219 RID: 537 RVA: 0x000028DC File Offset: 0x00000ADC
		public virtual void OnInitialize(TrackAsset aPent)
		{
		}

		// Token: 0x040000C3 RID: 195
		[SerializeField]
		[TimeField(TimeFieldAttribute.UseEditMode.ApplyEditMode)]
		[Tooltip("Time for the marker")]
		private double m_Time;
	}
}
