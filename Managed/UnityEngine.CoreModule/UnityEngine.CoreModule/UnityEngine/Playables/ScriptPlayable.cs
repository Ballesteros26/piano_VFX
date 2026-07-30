using System;

namespace UnityEngine.Playables
{
	// Token: 0x020003AC RID: 940
	public struct ScriptPlayable<T> : IPlayable, IEquatable<ScriptPlayable<T>> where T : class, IPlayableBehaviour, new()
	{
		// Token: 0x17000628 RID: 1576
		// (get) Token: 0x06002142 RID: 8514 RVA: 0x00037BE4 File Offset: 0x00035DE4
		public static ScriptPlayable<T> Null
		{
			get
			{
				return ScriptPlayable<T>.m_NullPlayable;
			}
		}

		// Token: 0x06002143 RID: 8515 RVA: 0x00037BFC File Offset: 0x00035DFC
		public static ScriptPlayable<T> Create(PlayableGraph graph, int inputCount = 0)
		{
			PlayableHandle playableHandle = ScriptPlayable<T>.CreateHandle(graph, default(T), inputCount);
			return new ScriptPlayable<T>(playableHandle);
		}

		// Token: 0x06002144 RID: 8516 RVA: 0x00037C28 File Offset: 0x00035E28
		public static ScriptPlayable<T> Create(PlayableGraph graph, T template, int inputCount = 0)
		{
			PlayableHandle playableHandle = ScriptPlayable<T>.CreateHandle(graph, template, inputCount);
			return new ScriptPlayable<T>(playableHandle);
		}

		// Token: 0x06002145 RID: 8517 RVA: 0x00037C4C File Offset: 0x00035E4C
		private static PlayableHandle CreateHandle(PlayableGraph graph, T template, int inputCount)
		{
			bool flag = template == null;
			object obj;
			if (flag)
			{
				obj = ScriptPlayable<T>.CreateScriptInstance();
			}
			else
			{
				obj = ScriptPlayable<T>.CloneScriptInstance(template);
			}
			bool flag2 = obj == null;
			PlayableHandle playableHandle;
			if (flag2)
			{
				Debug.LogError("Could not create a ScriptPlayable of Type " + typeof(T));
				playableHandle = PlayableHandle.Null;
			}
			else
			{
				PlayableHandle playableHandle2 = graph.CreatePlayableHandle();
				bool flag3 = !playableHandle2.IsValid();
				if (flag3)
				{
					playableHandle = PlayableHandle.Null;
				}
				else
				{
					playableHandle2.SetInputCount(inputCount);
					playableHandle2.SetScriptInstance(obj);
					playableHandle = playableHandle2;
				}
			}
			return playableHandle;
		}

		// Token: 0x06002146 RID: 8518 RVA: 0x00037CE8 File Offset: 0x00035EE8
		private static object CreateScriptInstance()
		{
			bool flag = typeof(ScriptableObject).IsAssignableFrom(typeof(T));
			IPlayableBehaviour playableBehaviour;
			if (flag)
			{
				playableBehaviour = ScriptableObject.CreateInstance(typeof(T)) as T;
			}
			else
			{
				playableBehaviour = new T();
			}
			return playableBehaviour;
		}

		// Token: 0x06002147 RID: 8519 RVA: 0x00037D48 File Offset: 0x00035F48
		private static object CloneScriptInstance(IPlayableBehaviour source)
		{
			Object @object = source as Object;
			bool flag = @object != null;
			object obj;
			if (flag)
			{
				obj = ScriptPlayable<T>.CloneScriptInstanceFromEngineObject(@object);
			}
			else
			{
				ICloneable cloneable = source as ICloneable;
				bool flag2 = cloneable != null;
				if (flag2)
				{
					obj = ScriptPlayable<T>.CloneScriptInstanceFromIClonable(cloneable);
				}
				else
				{
					obj = null;
				}
			}
			return obj;
		}

		// Token: 0x06002148 RID: 8520 RVA: 0x00037D90 File Offset: 0x00035F90
		private static object CloneScriptInstanceFromEngineObject(Object source)
		{
			Object @object = Object.Instantiate(source);
			bool flag = @object != null;
			if (flag)
			{
				@object.hideFlags |= HideFlags.DontSave;
			}
			return @object;
		}

		// Token: 0x06002149 RID: 8521 RVA: 0x00037DC8 File Offset: 0x00035FC8
		private static object CloneScriptInstanceFromIClonable(ICloneable source)
		{
			return source.Clone();
		}

		// Token: 0x0600214A RID: 8522 RVA: 0x00037DE0 File Offset: 0x00035FE0
		internal ScriptPlayable(PlayableHandle handle)
		{
			bool flag = handle.IsValid();
			if (flag)
			{
				bool flag2 = !typeof(T).IsAssignableFrom(handle.GetPlayableType());
				if (flag2)
				{
					throw new InvalidCastException(string.Format("Incompatible handle: Trying to assign a playable data of type `{0}` that is not compatible with the PlayableBehaviour of type `{1}`.", handle.GetPlayableType(), typeof(T)));
				}
			}
			this.m_Handle = handle;
		}

		// Token: 0x0600214B RID: 8523 RVA: 0x00037E40 File Offset: 0x00036040
		public PlayableHandle GetHandle()
		{
			return this.m_Handle;
		}

		// Token: 0x0600214C RID: 8524 RVA: 0x00037E58 File Offset: 0x00036058
		public T GetBehaviour()
		{
			return this.m_Handle.GetObject<T>();
		}

		// Token: 0x0600214D RID: 8525 RVA: 0x00037E78 File Offset: 0x00036078
		public static implicit operator Playable(ScriptPlayable<T> playable)
		{
			return new Playable(playable.GetHandle());
		}

		// Token: 0x0600214E RID: 8526 RVA: 0x00037E98 File Offset: 0x00036098
		public static explicit operator ScriptPlayable<T>(Playable playable)
		{
			return new ScriptPlayable<T>(playable.GetHandle());
		}

		// Token: 0x0600214F RID: 8527 RVA: 0x00037EB8 File Offset: 0x000360B8
		public bool Equals(ScriptPlayable<T> other)
		{
			return this.GetHandle() == other.GetHandle();
		}

		// Token: 0x04000BB3 RID: 2995
		private PlayableHandle m_Handle;

		// Token: 0x04000BB4 RID: 2996
		private static readonly ScriptPlayable<T> m_NullPlayable = new ScriptPlayable<T>(PlayableHandle.Null);
	}
}
