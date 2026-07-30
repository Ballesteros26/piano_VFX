using System;
using System.Collections.Generic;
using Unity.Profiling;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000232 RID: 562
	internal class VectorImageManager : IDisposable
	{
		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x060010D7 RID: 4311 RVA: 0x00044010 File Offset: 0x00042210
		public Texture2D atlas
		{
			get
			{
				GradientSettingsAtlas gradientSettingsAtlas = this.m_GradientSettingsAtlas;
				return (gradientSettingsAtlas != null) ? gradientSettingsAtlas.atlas : null;
			}
		}

		// Token: 0x060010D8 RID: 4312 RVA: 0x00044034 File Offset: 0x00042234
		public VectorImageManager(UIRAtlasManager atlasManager)
		{
			this.m_AtlasManager = atlasManager;
			this.m_Registered = new Dictionary<VectorImage, VectorImageRenderInfo>(32);
			this.m_RenderInfoPool = new VectorImageRenderInfoPool();
			this.m_GradientRemapPool = new GradientRemapPool();
			this.m_GradientSettingsAtlas = new GradientSettingsAtlas(4096);
		}

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x060010D9 RID: 4313 RVA: 0x0004408E File Offset: 0x0004228E
		// (set) Token: 0x060010DA RID: 4314 RVA: 0x00044096 File Offset: 0x00042296
		private protected bool disposed { protected get; private set; }

		// Token: 0x060010DB RID: 4315 RVA: 0x0004409F File Offset: 0x0004229F
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060010DC RID: 4316 RVA: 0x000440B4 File Offset: 0x000422B4
		protected virtual void Dispose(bool disposing)
		{
			bool disposed = this.disposed;
			if (!disposed)
			{
				if (disposing)
				{
					this.m_Registered.Clear();
					this.m_RenderInfoPool.Clear();
					this.m_GradientRemapPool.Clear();
					this.m_GradientSettingsAtlas.Dispose();
				}
				this.disposed = true;
			}
		}

		// Token: 0x060010DD RID: 4317 RVA: 0x0004410F File Offset: 0x0004230F
		public static void MarkAllForReset()
		{
			VectorImageManager.s_GlobalResetVersion++;
		}

		// Token: 0x060010DE RID: 4318 RVA: 0x0004411E File Offset: 0x0004231E
		public void MarkForReset()
		{
			this.m_ResetVersion = VectorImageManager.s_GlobalResetVersion - 1;
		}

		// Token: 0x060010DF RID: 4319 RVA: 0x00044130 File Offset: 0x00042330
		public bool RequiresReset()
		{
			return this.m_ResetVersion != VectorImageManager.s_GlobalResetVersion;
		}

		// Token: 0x060010E0 RID: 4320 RVA: 0x00044154 File Offset: 0x00042354
		public void Reset()
		{
			bool disposed = this.disposed;
			if (disposed)
			{
				DisposeHelper.NotifyDisposedUsed(this);
			}
			else
			{
				this.m_Registered.Clear();
				this.m_RenderInfoPool.Clear();
				this.m_GradientRemapPool.Clear();
				this.m_GradientSettingsAtlas.Reset();
				this.m_ResetVersion = VectorImageManager.s_GlobalResetVersion;
			}
		}

		// Token: 0x060010E1 RID: 4321 RVA: 0x000441B4 File Offset: 0x000423B4
		public void Commit()
		{
			bool disposed = this.disposed;
			if (disposed)
			{
				DisposeHelper.NotifyDisposedUsed(this);
			}
			else
			{
				this.m_GradientSettingsAtlas.Commit();
			}
		}

		// Token: 0x060010E2 RID: 4322 RVA: 0x000441E4 File Offset: 0x000423E4
		public GradientRemap AddUser(VectorImage vi)
		{
			bool disposed = this.disposed;
			GradientRemap gradientRemap;
			if (disposed)
			{
				DisposeHelper.NotifyDisposedUsed(this);
				gradientRemap = null;
			}
			else
			{
				bool flag = vi == null;
				if (flag)
				{
					gradientRemap = null;
				}
				else
				{
					VectorImageRenderInfo vectorImageRenderInfo;
					bool flag2 = this.m_Registered.TryGetValue(vi, ref vectorImageRenderInfo);
					if (flag2)
					{
						vectorImageRenderInfo.useCount++;
					}
					else
					{
						vectorImageRenderInfo = this.Register(vi);
					}
					gradientRemap = vectorImageRenderInfo.firstGradientRemap;
				}
			}
			return gradientRemap;
		}

		// Token: 0x060010E3 RID: 4323 RVA: 0x00044250 File Offset: 0x00042450
		public void RemoveUser(VectorImage vi)
		{
			bool disposed = this.disposed;
			if (disposed)
			{
				DisposeHelper.NotifyDisposedUsed(this);
			}
			else
			{
				bool flag = vi == null;
				if (!flag)
				{
					VectorImageRenderInfo vectorImageRenderInfo;
					bool flag2 = this.m_Registered.TryGetValue(vi, ref vectorImageRenderInfo);
					if (flag2)
					{
						vectorImageRenderInfo.useCount--;
						bool flag3 = vectorImageRenderInfo.useCount == 0;
						if (flag3)
						{
							this.Unregister(vi, vectorImageRenderInfo);
						}
					}
				}
			}
		}

		// Token: 0x060010E4 RID: 4324 RVA: 0x000442BC File Offset: 0x000424BC
		private VectorImageRenderInfo Register(VectorImage vi)
		{
			VectorImageRenderInfo vectorImageRenderInfo = this.m_RenderInfoPool.Get();
			vectorImageRenderInfo.useCount = 1;
			this.m_Registered[vi] = vectorImageRenderInfo;
			GradientSettings[] settings = vi.settings;
			bool flag = settings != null && settings.Length != 0;
			if (flag)
			{
				int num = vi.settings.Length;
				Alloc alloc = this.m_GradientSettingsAtlas.Add(num);
				bool flag2 = alloc.size > 0U;
				if (flag2)
				{
					RectInt rectInt;
					bool flag3 = this.m_AtlasManager.TryGetLocation(vi.atlas, out rectInt);
					if (flag3)
					{
						GradientRemap gradientRemap = null;
						for (int i = 0; i < num; i++)
						{
							GradientRemap gradientRemap2 = this.m_GradientRemapPool.Get();
							bool flag4 = i > 0;
							if (flag4)
							{
								gradientRemap.next = gradientRemap2;
							}
							else
							{
								vectorImageRenderInfo.firstGradientRemap = gradientRemap2;
							}
							gradientRemap = gradientRemap2;
							gradientRemap2.origIndex = i;
							gradientRemap2.destIndex = (int)(alloc.start + (uint)i);
							GradientSettings gradientSettings = vi.settings[i];
							RectInt location = gradientSettings.location;
							location.x += rectInt.x;
							location.y += rectInt.y;
							gradientRemap2.location = location;
							gradientRemap2.isAtlassed = true;
						}
						this.m_GradientSettingsAtlas.Write(alloc, vi.settings, vectorImageRenderInfo.firstGradientRemap);
					}
					else
					{
						GradientRemap gradientRemap3 = null;
						for (int j = 0; j < num; j++)
						{
							GradientRemap gradientRemap4 = this.m_GradientRemapPool.Get();
							bool flag5 = j > 0;
							if (flag5)
							{
								gradientRemap3.next = gradientRemap4;
							}
							else
							{
								vectorImageRenderInfo.firstGradientRemap = gradientRemap4;
							}
							gradientRemap3 = gradientRemap4;
							gradientRemap4.origIndex = j;
							gradientRemap4.destIndex = (int)(alloc.start + (uint)j);
							gradientRemap4.isAtlassed = false;
						}
						this.m_GradientSettingsAtlas.Write(alloc, vi.settings, null);
					}
				}
				else
				{
					bool flag6 = !this.m_LoggedExhaustedSettingsAtlas;
					if (flag6)
					{
						object[] array = new object[4];
						array[0] = "Exhausted max gradient settings (";
						array[1] = this.m_GradientSettingsAtlas.length;
						array[2] = ") for atlas: ";
						int num2 = 3;
						Texture2D atlas = this.m_GradientSettingsAtlas.atlas;
						array[num2] = ((atlas != null) ? atlas.name : null);
						Debug.LogError(string.Concat(array));
						this.m_LoggedExhaustedSettingsAtlas = true;
					}
				}
			}
			return vectorImageRenderInfo;
		}

		// Token: 0x060010E5 RID: 4325 RVA: 0x0004451C File Offset: 0x0004271C
		private void Unregister(VectorImage vi, VectorImageRenderInfo renderInfo)
		{
			bool flag = renderInfo.gradientSettingsAlloc.size > 0U;
			if (flag)
			{
				this.m_GradientSettingsAtlas.Remove(renderInfo.gradientSettingsAlloc);
			}
			GradientRemap next;
			for (GradientRemap gradientRemap = renderInfo.firstGradientRemap; gradientRemap != null; gradientRemap = next)
			{
				next = gradientRemap.next;
				this.m_GradientRemapPool.Return(gradientRemap);
			}
			this.m_Registered.Remove(vi);
			this.m_RenderInfoPool.Return(renderInfo);
		}

		// Token: 0x0400078A RID: 1930
		private static ProfilerMarker s_MarkerRegister = new ProfilerMarker("UIR.VectorImageManager.Register");

		// Token: 0x0400078B RID: 1931
		private static ProfilerMarker s_MarkerUnregister = new ProfilerMarker("UIR.VectorImageManager.Unregister");

		// Token: 0x0400078C RID: 1932
		private readonly UIRAtlasManager m_AtlasManager;

		// Token: 0x0400078D RID: 1933
		private Dictionary<VectorImage, VectorImageRenderInfo> m_Registered;

		// Token: 0x0400078E RID: 1934
		private VectorImageRenderInfoPool m_RenderInfoPool;

		// Token: 0x0400078F RID: 1935
		private GradientRemapPool m_GradientRemapPool;

		// Token: 0x04000790 RID: 1936
		private GradientSettingsAtlas m_GradientSettingsAtlas;

		// Token: 0x04000791 RID: 1937
		private bool m_LoggedExhaustedSettingsAtlas;

		// Token: 0x04000793 RID: 1939
		private static int s_GlobalResetVersion;

		// Token: 0x04000794 RID: 1940
		private int m_ResetVersion = VectorImageManager.s_GlobalResetVersion;
	}
}
