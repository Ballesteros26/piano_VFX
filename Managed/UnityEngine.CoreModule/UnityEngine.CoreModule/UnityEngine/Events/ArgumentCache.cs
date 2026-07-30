using System;
using UnityEngine.Serialization;

namespace UnityEngine.Events
{
	// Token: 0x0200024E RID: 590
	[Serializable]
	internal class ArgumentCache : ISerializationCallbackReceiver
	{
		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x06001922 RID: 6434 RVA: 0x00028644 File Offset: 0x00026844
		// (set) Token: 0x06001923 RID: 6435 RVA: 0x0002865C File Offset: 0x0002685C
		public Object unityObjectArgument
		{
			get
			{
				return this.m_ObjectArgument;
			}
			set
			{
				this.m_ObjectArgument = value;
				this.m_ObjectArgumentAssemblyTypeName = ((value != null) ? value.GetType().AssemblyQualifiedName : string.Empty);
			}
		}

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x06001924 RID: 6436 RVA: 0x00028688 File Offset: 0x00026888
		public string unityObjectArgumentAssemblyTypeName
		{
			get
			{
				return this.m_ObjectArgumentAssemblyTypeName;
			}
		}

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x06001925 RID: 6437 RVA: 0x000286A0 File Offset: 0x000268A0
		// (set) Token: 0x06001926 RID: 6438 RVA: 0x000286B8 File Offset: 0x000268B8
		public int intArgument
		{
			get
			{
				return this.m_IntArgument;
			}
			set
			{
				this.m_IntArgument = value;
			}
		}

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x06001927 RID: 6439 RVA: 0x000286C4 File Offset: 0x000268C4
		// (set) Token: 0x06001928 RID: 6440 RVA: 0x000286DC File Offset: 0x000268DC
		public float floatArgument
		{
			get
			{
				return this.m_FloatArgument;
			}
			set
			{
				this.m_FloatArgument = value;
			}
		}

		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x06001929 RID: 6441 RVA: 0x000286E8 File Offset: 0x000268E8
		// (set) Token: 0x0600192A RID: 6442 RVA: 0x00028700 File Offset: 0x00026900
		public string stringArgument
		{
			get
			{
				return this.m_StringArgument;
			}
			set
			{
				this.m_StringArgument = value;
			}
		}

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x0600192B RID: 6443 RVA: 0x0002870C File Offset: 0x0002690C
		// (set) Token: 0x0600192C RID: 6444 RVA: 0x00028724 File Offset: 0x00026924
		public bool boolArgument
		{
			get
			{
				return this.m_BoolArgument;
			}
			set
			{
				this.m_BoolArgument = value;
			}
		}

		// Token: 0x0600192D RID: 6445 RVA: 0x0002872E File Offset: 0x0002692E
		public void OnBeforeSerialize()
		{
			this.m_ObjectArgumentAssemblyTypeName = UnityEventTools.TidyAssemblyTypeName(this.m_ObjectArgumentAssemblyTypeName);
		}

		// Token: 0x0600192E RID: 6446 RVA: 0x0002872E File Offset: 0x0002692E
		public void OnAfterDeserialize()
		{
			this.m_ObjectArgumentAssemblyTypeName = UnityEventTools.TidyAssemblyTypeName(this.m_ObjectArgumentAssemblyTypeName);
		}

		// Token: 0x040007CB RID: 1995
		[FormerlySerializedAs("objectArgument")]
		[SerializeField]
		private Object m_ObjectArgument;

		// Token: 0x040007CC RID: 1996
		[FormerlySerializedAs("objectArgumentAssemblyTypeName")]
		[SerializeField]
		private string m_ObjectArgumentAssemblyTypeName;

		// Token: 0x040007CD RID: 1997
		[FormerlySerializedAs("intArgument")]
		[SerializeField]
		private int m_IntArgument;

		// Token: 0x040007CE RID: 1998
		[FormerlySerializedAs("floatArgument")]
		[SerializeField]
		private float m_FloatArgument;

		// Token: 0x040007CF RID: 1999
		[SerializeField]
		[FormerlySerializedAs("stringArgument")]
		private string m_StringArgument;

		// Token: 0x040007D0 RID: 2000
		[SerializeField]
		private bool m_BoolArgument;
	}
}
