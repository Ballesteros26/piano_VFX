using System;

namespace UnityEngine.AI
{
	// Token: 0x0200000F RID: 15
	public struct NavMeshDataInstance
	{
		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x000026B0 File Offset: 0x000008B0
		public bool valid
		{
			get
			{
				return this.id != 0 && NavMesh.IsValidNavMeshDataHandle(this.id);
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000CA RID: 202 RVA: 0x000026C8 File Offset: 0x000008C8
		// (set) Token: 0x060000CB RID: 203 RVA: 0x000026D0 File Offset: 0x000008D0
		internal int id { get; set; }

		// Token: 0x060000CC RID: 204 RVA: 0x000026D9 File Offset: 0x000008D9
		public void Remove()
		{
			NavMesh.RemoveNavMeshDataInternal(this.id);
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000CD RID: 205 RVA: 0x000026E8 File Offset: 0x000008E8
		// (set) Token: 0x060000CE RID: 206 RVA: 0x00002708 File Offset: 0x00000908
		public Object owner
		{
			get
			{
				return NavMesh.InternalGetOwner(this.id);
			}
			set
			{
				int num = ((value != null) ? value.GetInstanceID() : 0);
				bool flag = !NavMesh.InternalSetOwner(this.id, num);
				if (flag)
				{
					Debug.LogError("Cannot set 'owner' on an invalid NavMeshDataInstance");
				}
			}
		}
	}
}
