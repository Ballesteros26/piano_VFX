using System;

namespace UnityEngine.AI
{
	// Token: 0x02000011 RID: 17
	public struct NavMeshLinkInstance
	{
		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000DD RID: 221 RVA: 0x0000284A File Offset: 0x00000A4A
		public bool valid
		{
			get
			{
				return this.id != 0 && NavMesh.IsValidLinkHandle(this.id);
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000DE RID: 222 RVA: 0x00002862 File Offset: 0x00000A62
		// (set) Token: 0x060000DF RID: 223 RVA: 0x0000286A File Offset: 0x00000A6A
		internal int id { get; set; }

		// Token: 0x060000E0 RID: 224 RVA: 0x00002873 File Offset: 0x00000A73
		public void Remove()
		{
			NavMesh.RemoveLinkInternal(this.id);
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x00002884 File Offset: 0x00000A84
		// (set) Token: 0x060000E2 RID: 226 RVA: 0x000028A4 File Offset: 0x00000AA4
		public Object owner
		{
			get
			{
				return NavMesh.InternalGetLinkOwner(this.id);
			}
			set
			{
				int num = ((value != null) ? value.GetInstanceID() : 0);
				bool flag = !NavMesh.InternalSetLinkOwner(this.id, num);
				if (flag)
				{
					Debug.LogError("Cannot set 'owner' on an invalid NavMeshLinkInstance");
				}
			}
		}
	}
}
