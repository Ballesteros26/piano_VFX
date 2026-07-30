using System;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
	// Token: 0x02000041 RID: 65
	[ExecuteAlways]
	public abstract class BaseMeshEffect : UIBehaviour, IMeshModifier
	{
		// Token: 0x17000141 RID: 321
		// (get) Token: 0x0600048E RID: 1166 RVA: 0x00015A45 File Offset: 0x00013C45
		protected Graphic graphic
		{
			get
			{
				if (this.m_Graphic == null)
				{
					this.m_Graphic = base.GetComponent<Graphic>();
				}
				return this.m_Graphic;
			}
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x00015A67 File Offset: 0x00013C67
		protected override void OnEnable()
		{
			base.OnEnable();
			if (this.graphic != null)
			{
				this.graphic.SetVerticesDirty();
			}
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x00015A88 File Offset: 0x00013C88
		protected override void OnDisable()
		{
			if (this.graphic != null)
			{
				this.graphic.SetVerticesDirty();
			}
			base.OnDisable();
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x00015AA9 File Offset: 0x00013CA9
		protected override void OnDidApplyAnimationProperties()
		{
			if (this.graphic != null)
			{
				this.graphic.SetVerticesDirty();
			}
			base.OnDidApplyAnimationProperties();
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x00015ACC File Offset: 0x00013CCC
		public virtual void ModifyMesh(Mesh mesh)
		{
			using (VertexHelper vertexHelper = new VertexHelper(mesh))
			{
				this.ModifyMesh(vertexHelper);
				vertexHelper.FillMesh(mesh);
			}
		}

		// Token: 0x06000493 RID: 1171
		public abstract void ModifyMesh(VertexHelper vh);

		// Token: 0x04000187 RID: 391
		[NonSerialized]
		private Graphic m_Graphic;
	}
}
