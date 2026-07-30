using System;
using UnityEngine.Playables;

namespace UnityEngine.Timeline
{
	// Token: 0x0200002B RID: 43
	[CustomStyle("SignalEmitter")]
	[Serializable]
	public class SignalEmitter : Marker, INotification, INotificationOptionProvider
	{
		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000233 RID: 563 RVA: 0x00008057 File Offset: 0x00006257
		// (set) Token: 0x06000234 RID: 564 RVA: 0x0000805F File Offset: 0x0000625F
		public bool retroactive
		{
			get
			{
				return this.m_Retroactive;
			}
			set
			{
				this.m_Retroactive = value;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000235 RID: 565 RVA: 0x00008068 File Offset: 0x00006268
		// (set) Token: 0x06000236 RID: 566 RVA: 0x00008070 File Offset: 0x00006270
		public bool emitOnce
		{
			get
			{
				return this.m_EmitOnce;
			}
			set
			{
				this.m_EmitOnce = value;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000237 RID: 567 RVA: 0x00008079 File Offset: 0x00006279
		// (set) Token: 0x06000238 RID: 568 RVA: 0x00008081 File Offset: 0x00006281
		public SignalAsset asset
		{
			get
			{
				return this.m_Asset;
			}
			set
			{
				this.m_Asset = value;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000239 RID: 569 RVA: 0x0000808A File Offset: 0x0000628A
		PropertyName INotification.id
		{
			get
			{
				if (this.m_Asset != null)
				{
					return new PropertyName(this.m_Asset.name);
				}
				return new PropertyName(string.Empty);
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x0600023A RID: 570 RVA: 0x000080B5 File Offset: 0x000062B5
		NotificationFlags INotificationOptionProvider.flags
		{
			get
			{
				return (this.retroactive ? NotificationFlags.Retroactive : ((NotificationFlags)0)) | (this.emitOnce ? NotificationFlags.TriggerOnce : ((NotificationFlags)0)) | NotificationFlags.TriggerInEditMode;
			}
		}

		// Token: 0x040000CA RID: 202
		[SerializeField]
		private bool m_Retroactive;

		// Token: 0x040000CB RID: 203
		[SerializeField]
		private bool m_EmitOnce;

		// Token: 0x040000CC RID: 204
		[SerializeField]
		private SignalAsset m_Asset;
	}
}
