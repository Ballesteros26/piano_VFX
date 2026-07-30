using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;
using UnityEngine.TextCore.LowLevel;

namespace UnityEngine.TextCore
{
	// Token: 0x02000004 RID: 4
	[UsedByNativeCode]
	[Serializable]
	public struct FaceInfo
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002290 File Offset: 0x00000490
		// (set) Token: 0x06000009 RID: 9 RVA: 0x000022A8 File Offset: 0x000004A8
		public string familyName
		{
			get
			{
				return this.m_FamilyName;
			}
			set
			{
				this.m_FamilyName = value;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000A RID: 10 RVA: 0x000022B4 File Offset: 0x000004B4
		// (set) Token: 0x0600000B RID: 11 RVA: 0x000022CC File Offset: 0x000004CC
		public string styleName
		{
			get
			{
				return this.m_StyleName;
			}
			set
			{
				this.m_StyleName = value;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000022D8 File Offset: 0x000004D8
		// (set) Token: 0x0600000D RID: 13 RVA: 0x000022F0 File Offset: 0x000004F0
		public int pointSize
		{
			get
			{
				return this.m_PointSize;
			}
			set
			{
				this.m_PointSize = value;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000E RID: 14 RVA: 0x000022FC File Offset: 0x000004FC
		// (set) Token: 0x0600000F RID: 15 RVA: 0x00002314 File Offset: 0x00000514
		public float scale
		{
			get
			{
				return this.m_Scale;
			}
			set
			{
				this.m_Scale = value;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000010 RID: 16 RVA: 0x00002320 File Offset: 0x00000520
		// (set) Token: 0x06000011 RID: 17 RVA: 0x00002338 File Offset: 0x00000538
		public float lineHeight
		{
			get
			{
				return this.m_LineHeight;
			}
			set
			{
				this.m_LineHeight = value;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00002344 File Offset: 0x00000544
		// (set) Token: 0x06000013 RID: 19 RVA: 0x0000235C File Offset: 0x0000055C
		public float ascentLine
		{
			get
			{
				return this.m_AscentLine;
			}
			set
			{
				this.m_AscentLine = value;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000014 RID: 20 RVA: 0x00002368 File Offset: 0x00000568
		// (set) Token: 0x06000015 RID: 21 RVA: 0x00002380 File Offset: 0x00000580
		public float capLine
		{
			get
			{
				return this.m_CapLine;
			}
			set
			{
				this.m_CapLine = value;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000016 RID: 22 RVA: 0x0000238C File Offset: 0x0000058C
		// (set) Token: 0x06000017 RID: 23 RVA: 0x000023A4 File Offset: 0x000005A4
		public float meanLine
		{
			get
			{
				return this.m_MeanLine;
			}
			set
			{
				this.m_MeanLine = value;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000018 RID: 24 RVA: 0x000023B0 File Offset: 0x000005B0
		// (set) Token: 0x06000019 RID: 25 RVA: 0x000023C8 File Offset: 0x000005C8
		public float baseline
		{
			get
			{
				return this.m_Baseline;
			}
			set
			{
				this.m_Baseline = value;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600001A RID: 26 RVA: 0x000023D4 File Offset: 0x000005D4
		// (set) Token: 0x0600001B RID: 27 RVA: 0x000023EC File Offset: 0x000005EC
		public float descentLine
		{
			get
			{
				return this.m_DescentLine;
			}
			set
			{
				this.m_DescentLine = value;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600001C RID: 28 RVA: 0x000023F8 File Offset: 0x000005F8
		// (set) Token: 0x0600001D RID: 29 RVA: 0x00002410 File Offset: 0x00000610
		public float superscriptOffset
		{
			get
			{
				return this.m_SuperscriptOffset;
			}
			set
			{
				this.m_SuperscriptOffset = value;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600001E RID: 30 RVA: 0x0000241C File Offset: 0x0000061C
		// (set) Token: 0x0600001F RID: 31 RVA: 0x00002434 File Offset: 0x00000634
		public float superscriptSize
		{
			get
			{
				return this.m_SuperscriptSize;
			}
			set
			{
				this.m_SuperscriptSize = value;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002440 File Offset: 0x00000640
		// (set) Token: 0x06000021 RID: 33 RVA: 0x00002458 File Offset: 0x00000658
		public float subscriptOffset
		{
			get
			{
				return this.m_SubscriptOffset;
			}
			set
			{
				this.m_SubscriptOffset = value;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000022 RID: 34 RVA: 0x00002464 File Offset: 0x00000664
		// (set) Token: 0x06000023 RID: 35 RVA: 0x0000247C File Offset: 0x0000067C
		public float subscriptSize
		{
			get
			{
				return this.m_SubscriptSize;
			}
			set
			{
				this.m_SubscriptSize = value;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002488 File Offset: 0x00000688
		// (set) Token: 0x06000025 RID: 37 RVA: 0x000024A0 File Offset: 0x000006A0
		public float underlineOffset
		{
			get
			{
				return this.m_UnderlineOffset;
			}
			set
			{
				this.m_UnderlineOffset = value;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000026 RID: 38 RVA: 0x000024AC File Offset: 0x000006AC
		// (set) Token: 0x06000027 RID: 39 RVA: 0x000024C4 File Offset: 0x000006C4
		public float underlineThickness
		{
			get
			{
				return this.m_UnderlineThickness;
			}
			set
			{
				this.m_UnderlineThickness = value;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000028 RID: 40 RVA: 0x000024D0 File Offset: 0x000006D0
		// (set) Token: 0x06000029 RID: 41 RVA: 0x000024E8 File Offset: 0x000006E8
		public float strikethroughOffset
		{
			get
			{
				return this.m_StrikethroughOffset;
			}
			set
			{
				this.m_StrikethroughOffset = value;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600002A RID: 42 RVA: 0x000024F4 File Offset: 0x000006F4
		// (set) Token: 0x0600002B RID: 43 RVA: 0x0000250C File Offset: 0x0000070C
		public float strikethroughThickness
		{
			get
			{
				return this.m_StrikethroughThickness;
			}
			set
			{
				this.m_StrikethroughThickness = value;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00002518 File Offset: 0x00000718
		// (set) Token: 0x0600002D RID: 45 RVA: 0x00002530 File Offset: 0x00000730
		public float tabWidth
		{
			get
			{
				return this.m_TabWidth;
			}
			set
			{
				this.m_TabWidth = value;
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x0000253C File Offset: 0x0000073C
		internal FaceInfo(string familyName, string styleName, int pointSize, float scale, float lineHeight, float ascentLine, float capLine, float meanLine, float baseline, float descentLine, float superscriptOffset, float superscriptSize, float subscriptOffset, float subscriptSize, float underlineOffset, float underlineThickness, float strikethroughOffset, float strikethroughThickness, float tabWidth)
		{
			this.m_FamilyName = familyName;
			this.m_StyleName = styleName;
			this.m_PointSize = pointSize;
			this.m_Scale = scale;
			this.m_LineHeight = lineHeight;
			this.m_AscentLine = ascentLine;
			this.m_CapLine = capLine;
			this.m_MeanLine = meanLine;
			this.m_Baseline = baseline;
			this.m_DescentLine = descentLine;
			this.m_SuperscriptOffset = superscriptOffset;
			this.m_SuperscriptSize = superscriptSize;
			this.m_SubscriptOffset = subscriptOffset;
			this.m_SubscriptSize = subscriptSize;
			this.m_UnderlineOffset = underlineOffset;
			this.m_UnderlineThickness = underlineThickness;
			this.m_StrikethroughOffset = strikethroughOffset;
			this.m_StrikethroughThickness = strikethroughThickness;
			this.m_TabWidth = tabWidth;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000025E0 File Offset: 0x000007E0
		public bool Compare(FaceInfo other)
		{
			return this.familyName == other.familyName && this.styleName == other.styleName && this.pointSize == other.pointSize && FontEngineUtilities.Approximately(this.scale, other.scale) && FontEngineUtilities.Approximately(this.lineHeight, other.lineHeight) && FontEngineUtilities.Approximately(this.ascentLine, other.ascentLine) && FontEngineUtilities.Approximately(this.capLine, other.capLine) && FontEngineUtilities.Approximately(this.meanLine, other.meanLine) && FontEngineUtilities.Approximately(this.baseline, other.baseline) && FontEngineUtilities.Approximately(this.descentLine, other.descentLine) && FontEngineUtilities.Approximately(this.superscriptOffset, other.superscriptOffset) && FontEngineUtilities.Approximately(this.superscriptSize, other.superscriptSize) && FontEngineUtilities.Approximately(this.subscriptOffset, other.subscriptOffset) && FontEngineUtilities.Approximately(this.subscriptSize, other.subscriptSize) && FontEngineUtilities.Approximately(this.underlineOffset, other.underlineOffset) && FontEngineUtilities.Approximately(this.underlineThickness, other.underlineThickness) && FontEngineUtilities.Approximately(this.strikethroughOffset, other.strikethroughOffset) && FontEngineUtilities.Approximately(this.strikethroughThickness, other.strikethroughThickness) && FontEngineUtilities.Approximately(this.tabWidth, other.tabWidth);
		}

		// Token: 0x04000001 RID: 1
		[SerializeField]
		[NativeName("familyName")]
		private string m_FamilyName;

		// Token: 0x04000002 RID: 2
		[SerializeField]
		[NativeName("styleName")]
		private string m_StyleName;

		// Token: 0x04000003 RID: 3
		[SerializeField]
		[NativeName("pointSize")]
		private int m_PointSize;

		// Token: 0x04000004 RID: 4
		[SerializeField]
		[NativeName("scale")]
		private float m_Scale;

		// Token: 0x04000005 RID: 5
		[SerializeField]
		[NativeName("lineHeight")]
		private float m_LineHeight;

		// Token: 0x04000006 RID: 6
		[SerializeField]
		[NativeName("ascentLine")]
		private float m_AscentLine;

		// Token: 0x04000007 RID: 7
		[SerializeField]
		[NativeName("capLine")]
		private float m_CapLine;

		// Token: 0x04000008 RID: 8
		[NativeName("meanLine")]
		[SerializeField]
		private float m_MeanLine;

		// Token: 0x04000009 RID: 9
		[SerializeField]
		[NativeName("baseline")]
		private float m_Baseline;

		// Token: 0x0400000A RID: 10
		[NativeName("descentLine")]
		[SerializeField]
		private float m_DescentLine;

		// Token: 0x0400000B RID: 11
		[SerializeField]
		[NativeName("superscriptOffset")]
		private float m_SuperscriptOffset;

		// Token: 0x0400000C RID: 12
		[NativeName("superscriptSize")]
		[SerializeField]
		private float m_SuperscriptSize;

		// Token: 0x0400000D RID: 13
		[SerializeField]
		[NativeName("subscriptOffset")]
		private float m_SubscriptOffset;

		// Token: 0x0400000E RID: 14
		[SerializeField]
		[NativeName("subscriptSize")]
		private float m_SubscriptSize;

		// Token: 0x0400000F RID: 15
		[SerializeField]
		[NativeName("underlineOffset")]
		private float m_UnderlineOffset;

		// Token: 0x04000010 RID: 16
		[SerializeField]
		[NativeName("underlineThickness")]
		private float m_UnderlineThickness;

		// Token: 0x04000011 RID: 17
		[NativeName("strikethroughOffset")]
		[SerializeField]
		private float m_StrikethroughOffset;

		// Token: 0x04000012 RID: 18
		[NativeName("strikethroughThickness")]
		[SerializeField]
		private float m_StrikethroughThickness;

		// Token: 0x04000013 RID: 19
		[SerializeField]
		[NativeName("tabWidth")]
		private float m_TabWidth;
	}
}
