using System;
using System.ComponentModel;
using UnityEngine.Bindings;

namespace UnityEngine.Playables
{
	// Token: 0x020003A1 RID: 929
	public struct PlayableBinding
	{
		// Token: 0x17000620 RID: 1568
		// (get) Token: 0x0600201D RID: 8221 RVA: 0x00036754 File Offset: 0x00034954
		// (set) Token: 0x0600201E RID: 8222 RVA: 0x0003676C File Offset: 0x0003496C
		public string streamName
		{
			get
			{
				return this.m_StreamName;
			}
			set
			{
				this.m_StreamName = value;
			}
		}

		// Token: 0x17000621 RID: 1569
		// (get) Token: 0x0600201F RID: 8223 RVA: 0x00036778 File Offset: 0x00034978
		// (set) Token: 0x06002020 RID: 8224 RVA: 0x00036790 File Offset: 0x00034990
		public Object sourceObject
		{
			get
			{
				return this.m_SourceObject;
			}
			set
			{
				this.m_SourceObject = value;
			}
		}

		// Token: 0x17000622 RID: 1570
		// (get) Token: 0x06002021 RID: 8225 RVA: 0x0003679C File Offset: 0x0003499C
		public Type outputTargetType
		{
			get
			{
				return this.m_SourceBindingType;
			}
		}

		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x06002022 RID: 8226 RVA: 0x000367B4 File Offset: 0x000349B4
		// (set) Token: 0x06002023 RID: 8227 RVA: 0x00002EC3 File Offset: 0x000010C3
		[EditorBrowsable(1)]
		[Obsolete("sourceBindingType is no longer supported on PlayableBinding. Use outputBindingType instead to get the required output target type, and the appropriate binding create method (e.g. AnimationPlayableBinding.Create(name, key)) to create PlayableBindings", true)]
		public Type sourceBindingType
		{
			get
			{
				return this.m_SourceBindingType;
			}
			set
			{
			}
		}

		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x06002024 RID: 8228 RVA: 0x000367CC File Offset: 0x000349CC
		// (set) Token: 0x06002025 RID: 8229 RVA: 0x00002EC3 File Offset: 0x000010C3
		[Obsolete("streamType is no longer supported on PlayableBinding. Use the appropriate binding create method (e.g. AnimationPlayableBinding.Create(name, key)) instead.", true)]
		[EditorBrowsable(1)]
		public DataStreamType streamType
		{
			get
			{
				return DataStreamType.None;
			}
			set
			{
			}
		}

		// Token: 0x06002026 RID: 8230 RVA: 0x000367E0 File Offset: 0x000349E0
		internal PlayableOutput CreateOutput(PlayableGraph graph)
		{
			bool flag = this.m_CreateOutputMethod != null;
			PlayableOutput playableOutput;
			if (flag)
			{
				playableOutput = this.m_CreateOutputMethod(graph, this.m_StreamName);
			}
			else
			{
				playableOutput = PlayableOutput.Null;
			}
			return playableOutput;
		}

		// Token: 0x06002027 RID: 8231 RVA: 0x0003681C File Offset: 0x00034A1C
		[VisibleToOtherModules]
		internal static PlayableBinding CreateInternal(string name, Object sourceObject, Type sourceType, PlayableBinding.CreateOutputMethod createFunction)
		{
			return new PlayableBinding
			{
				m_StreamName = name,
				m_SourceObject = sourceObject,
				m_SourceBindingType = sourceType,
				m_CreateOutputMethod = createFunction
			};
		}

		// Token: 0x04000B97 RID: 2967
		private string m_StreamName;

		// Token: 0x04000B98 RID: 2968
		private Object m_SourceObject;

		// Token: 0x04000B99 RID: 2969
		private Type m_SourceBindingType;

		// Token: 0x04000B9A RID: 2970
		private PlayableBinding.CreateOutputMethod m_CreateOutputMethod;

		// Token: 0x04000B9B RID: 2971
		public static readonly PlayableBinding[] None = new PlayableBinding[0];

		// Token: 0x04000B9C RID: 2972
		public static readonly double DefaultDuration = double.PositiveInfinity;

		// Token: 0x020003A2 RID: 930
		// (Invoke) Token: 0x0600202A RID: 8234
		[VisibleToOtherModules]
		internal delegate PlayableOutput CreateOutputMethod(PlayableGraph graph, string name);
	}
}
