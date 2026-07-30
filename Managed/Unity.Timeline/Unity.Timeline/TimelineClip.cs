using System;
using System.Collections.Generic;
using UnityEngine.Playables;
using UnityEngine.Serialization;

namespace UnityEngine.Timeline
{
	// Token: 0x0200000E RID: 14
	[Serializable]
	public class TimelineClip : ICurvesOwner, ISerializationCallbackReceiver
	{
		// Token: 0x06000095 RID: 149 RVA: 0x0000338E File Offset: 0x0000158E
		private void UpgradeToLatestVersion()
		{
			if (this.m_Version < 1)
			{
				TimelineClip.TimelineClipUpgrade.UpgradeClipInFromGlobalToLocal(this);
			}
		}

		// Token: 0x06000096 RID: 150 RVA: 0x0000339F File Offset: 0x0000159F
		internal TimelineClip(TrackAsset parent)
		{
			this.parentTrack = parent;
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000097 RID: 151 RVA: 0x000033DB File Offset: 0x000015DB
		public bool hasPreExtrapolation
		{
			get
			{
				return this.m_PreExtrapolationMode != TimelineClip.ClipExtrapolation.None && this.m_PreExtrapolationTime > 0.0;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000098 RID: 152 RVA: 0x000033F8 File Offset: 0x000015F8
		public bool hasPostExtrapolation
		{
			get
			{
				return this.m_PostExtrapolationMode != TimelineClip.ClipExtrapolation.None && this.m_PostExtrapolationTime > 0.0;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00003415 File Offset: 0x00001615
		// (set) Token: 0x0600009A RID: 154 RVA: 0x0000344C File Offset: 0x0000164C
		public double timeScale
		{
			get
			{
				if (!this.clipCaps.HasAny(ClipCaps.SpeedMultiplier))
				{
					return 1.0;
				}
				return Math.Max(TimelineClip.kTimeScaleMin, Math.Min(this.m_TimeScale, TimelineClip.kTimeScaleMax));
			}
			set
			{
				this.UpdateDirty(this.m_TimeScale, value);
				this.m_TimeScale = (this.clipCaps.HasAny(ClipCaps.SpeedMultiplier) ? Math.Max(TimelineClip.kTimeScaleMin, Math.Min(value, TimelineClip.kTimeScaleMax)) : 1.0);
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600009B RID: 155 RVA: 0x0000349A File Offset: 0x0000169A
		// (set) Token: 0x0600009C RID: 156 RVA: 0x000034A4 File Offset: 0x000016A4
		public double start
		{
			get
			{
				return this.m_Start;
			}
			set
			{
				this.UpdateDirty(value, this.m_Start);
				double num = Math.Max(TimelineClip.SanitizeTimeValue(value, this.m_Start), 0.0);
				if (this.m_ParentTrack != null && this.m_Start != num)
				{
					this.m_ParentTrack.OnClipMove();
				}
				this.m_Start = num;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600009D RID: 157 RVA: 0x00003502 File Offset: 0x00001702
		// (set) Token: 0x0600009E RID: 158 RVA: 0x0000350A File Offset: 0x0000170A
		public double duration
		{
			get
			{
				return this.m_Duration;
			}
			set
			{
				this.UpdateDirty(this.m_Duration, value);
				this.m_Duration = Math.Max(TimelineClip.SanitizeTimeValue(value, this.m_Duration), double.Epsilon);
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600009F RID: 159 RVA: 0x00003539 File Offset: 0x00001739
		public double end
		{
			get
			{
				return this.m_Start + this.m_Duration;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x00003548 File Offset: 0x00001748
		// (set) Token: 0x060000A1 RID: 161 RVA: 0x00003568 File Offset: 0x00001768
		public double clipIn
		{
			get
			{
				if (!this.clipCaps.HasAny(ClipCaps.ClipIn))
				{
					return 0.0;
				}
				return this.m_ClipIn;
			}
			set
			{
				this.UpdateDirty(this.m_ClipIn, value);
				this.m_ClipIn = (this.clipCaps.HasAny(ClipCaps.ClipIn) ? Math.Max(Math.Min(TimelineClip.SanitizeTimeValue(value, this.m_ClipIn), TimelineClip.kMaxTimeValue), 0.0) : 0.0);
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x000035C5 File Offset: 0x000017C5
		// (set) Token: 0x060000A3 RID: 163 RVA: 0x000035CD File Offset: 0x000017CD
		public string displayName
		{
			get
			{
				return this.m_DisplayName;
			}
			set
			{
				this.m_DisplayName = value;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x000035D8 File Offset: 0x000017D8
		public double clipAssetDuration
		{
			get
			{
				IPlayableAsset playableAsset = this.m_Asset as IPlayableAsset;
				if (playableAsset == null)
				{
					return double.MaxValue;
				}
				return playableAsset.duration;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x00003604 File Offset: 0x00001804
		// (set) Token: 0x060000A6 RID: 166 RVA: 0x0000360C File Offset: 0x0000180C
		public AnimationClip curves
		{
			get
			{
				return this.m_AnimationCurves;
			}
			internal set
			{
				this.m_AnimationCurves = value;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x00003615 File Offset: 0x00001815
		string ICurvesOwner.defaultCurvesName
		{
			get
			{
				return TimelineClip.kDefaultCurvesName;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x0000361C File Offset: 0x0000181C
		public bool hasCurves
		{
			get
			{
				return this.m_AnimationCurves != null && !this.m_AnimationCurves.empty;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x0000363C File Offset: 0x0000183C
		// (set) Token: 0x060000AA RID: 170 RVA: 0x00003644 File Offset: 0x00001844
		public Object asset
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

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000AB RID: 171 RVA: 0x0000364D File Offset: 0x0000184D
		Object ICurvesOwner.assetOwner
		{
			get
			{
				return this.parentTrack;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000AC RID: 172 RVA: 0x0000364D File Offset: 0x0000184D
		TrackAsset ICurvesOwner.targetTrack
		{
			get
			{
				return this.parentTrack;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000AD RID: 173 RVA: 0x00003655 File Offset: 0x00001855
		// (set) Token: 0x060000AE RID: 174 RVA: 0x000028DC File Offset: 0x00000ADC
		[Obsolete("underlyingAsset property is obsolete. Use asset property instead", true)]
		public Object underlyingAsset
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000AF RID: 175 RVA: 0x00003658 File Offset: 0x00001858
		// (set) Token: 0x060000B0 RID: 176 RVA: 0x00003660 File Offset: 0x00001860
		public TrackAsset parentTrack
		{
			get
			{
				return this.m_ParentTrack;
			}
			set
			{
				if (this.m_ParentTrack == value)
				{
					return;
				}
				if (this.m_ParentTrack != null)
				{
					this.m_ParentTrack.RemoveClip(this);
				}
				this.m_ParentTrack = value;
				if (this.m_ParentTrack != null)
				{
					this.m_ParentTrack.AddClip(this);
				}
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x000036B8 File Offset: 0x000018B8
		// (set) Token: 0x060000B2 RID: 178 RVA: 0x00003708 File Offset: 0x00001908
		public double easeInDuration
		{
			get
			{
				if (!this.clipCaps.HasAny(ClipCaps.Blending))
				{
					return 0.0;
				}
				return Math.Min(Math.Max(this.m_EaseInDuration, 0.0), this.duration * 0.49);
			}
			set
			{
				this.m_EaseInDuration = (this.clipCaps.HasAny(ClipCaps.Blending) ? Math.Max(0.0, Math.Min(TimelineClip.SanitizeTimeValue(value, this.m_EaseInDuration), this.duration * 0.49)) : 0.0);
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x00003764 File Offset: 0x00001964
		// (set) Token: 0x060000B4 RID: 180 RVA: 0x000037B4 File Offset: 0x000019B4
		public double easeOutDuration
		{
			get
			{
				if (!this.clipCaps.HasAny(ClipCaps.Blending))
				{
					return 0.0;
				}
				return Math.Min(Math.Max(this.m_EaseOutDuration, 0.0), this.duration * 0.49);
			}
			set
			{
				this.m_EaseOutDuration = (this.clipCaps.HasAny(ClipCaps.Blending) ? Math.Max(0.0, Math.Min(TimelineClip.SanitizeTimeValue(value, this.m_EaseOutDuration), this.duration * 0.49)) : 0.0);
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x00003810 File Offset: 0x00001A10
		[Obsolete("Use easeOutTime instead (UnityUpgradable) -> easeOutTime", true)]
		public double eastOutTime
		{
			get
			{
				return this.duration - this.easeOutDuration + this.m_Start;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x00003810 File Offset: 0x00001A10
		public double easeOutTime
		{
			get
			{
				return this.duration - this.easeOutDuration + this.m_Start;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x00003826 File Offset: 0x00001A26
		// (set) Token: 0x060000B8 RID: 184 RVA: 0x00003847 File Offset: 0x00001A47
		public double blendInDuration
		{
			get
			{
				if (!this.clipCaps.HasAny(ClipCaps.Blending))
				{
					return 0.0;
				}
				return this.m_BlendInDuration;
			}
			set
			{
				this.m_BlendInDuration = (this.clipCaps.HasAny(ClipCaps.Blending) ? TimelineClip.SanitizeTimeValue(value, this.m_BlendInDuration) : 0.0);
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x00003875 File Offset: 0x00001A75
		// (set) Token: 0x060000BA RID: 186 RVA: 0x00003896 File Offset: 0x00001A96
		public double blendOutDuration
		{
			get
			{
				if (!this.clipCaps.HasAny(ClipCaps.Blending))
				{
					return 0.0;
				}
				return this.m_BlendOutDuration;
			}
			set
			{
				this.m_BlendOutDuration = (this.clipCaps.HasAny(ClipCaps.Blending) ? TimelineClip.SanitizeTimeValue(value, this.m_BlendOutDuration) : 0.0);
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000BB RID: 187 RVA: 0x000038C4 File Offset: 0x00001AC4
		// (set) Token: 0x060000BC RID: 188 RVA: 0x000038CC File Offset: 0x00001ACC
		public TimelineClip.BlendCurveMode blendInCurveMode
		{
			get
			{
				return this.m_BlendInCurveMode;
			}
			set
			{
				this.m_BlendInCurveMode = value;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000BD RID: 189 RVA: 0x000038D5 File Offset: 0x00001AD5
		// (set) Token: 0x060000BE RID: 190 RVA: 0x000038DD File Offset: 0x00001ADD
		public TimelineClip.BlendCurveMode blendOutCurveMode
		{
			get
			{
				return this.m_BlendOutCurveMode;
			}
			set
			{
				this.m_BlendOutCurveMode = value;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000BF RID: 191 RVA: 0x000038E6 File Offset: 0x00001AE6
		public bool hasBlendIn
		{
			get
			{
				return this.clipCaps.HasAny(ClipCaps.Blending) && this.m_BlendInDuration > 0.0;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x0000390A File Offset: 0x00001B0A
		public bool hasBlendOut
		{
			get
			{
				return this.clipCaps.HasAny(ClipCaps.Blending) && this.m_BlendOutDuration > 0.0;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x0000392E File Offset: 0x00001B2E
		// (set) Token: 0x060000C2 RID: 194 RVA: 0x00003957 File Offset: 0x00001B57
		public AnimationCurve mixInCurve
		{
			get
			{
				if (this.m_MixInCurve == null || this.m_MixInCurve.length < 2)
				{
					this.m_MixInCurve = TimelineClip.GetDefaultMixInCurve();
				}
				return this.m_MixInCurve;
			}
			set
			{
				this.m_MixInCurve = value;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x00003960 File Offset: 0x00001B60
		public float mixInPercentage
		{
			get
			{
				return (float)(this.mixInDuration / this.duration);
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x00003970 File Offset: 0x00001B70
		public double mixInDuration
		{
			get
			{
				if (!this.hasBlendIn)
				{
					return this.easeInDuration;
				}
				return this.blendInDuration;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x00003987 File Offset: 0x00001B87
		// (set) Token: 0x060000C6 RID: 198 RVA: 0x000039B0 File Offset: 0x00001BB0
		public AnimationCurve mixOutCurve
		{
			get
			{
				if (this.m_MixOutCurve == null || this.m_MixOutCurve.length < 2)
				{
					this.m_MixOutCurve = TimelineClip.GetDefaultMixOutCurve();
				}
				return this.m_MixOutCurve;
			}
			set
			{
				this.m_MixOutCurve = value;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x000039B9 File Offset: 0x00001BB9
		public double mixOutTime
		{
			get
			{
				return this.duration - this.mixOutDuration + this.m_Start;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x000039CF File Offset: 0x00001BCF
		public double mixOutDuration
		{
			get
			{
				if (!this.hasBlendOut)
				{
					return this.easeOutDuration;
				}
				return this.blendOutDuration;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x000039E6 File Offset: 0x00001BE6
		public float mixOutPercentage
		{
			get
			{
				return (float)(this.mixOutDuration / this.duration);
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000CA RID: 202 RVA: 0x000039F6 File Offset: 0x00001BF6
		// (set) Token: 0x060000CB RID: 203 RVA: 0x000039FE File Offset: 0x00001BFE
		public bool recordable
		{
			get
			{
				return this.m_Recordable;
			}
			internal set
			{
				this.m_Recordable = value;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000CC RID: 204 RVA: 0x00003A08 File Offset: 0x00001C08
		[Obsolete("exposedParameter is deprecated and will be removed in a future release", true)]
		public List<string> exposedParameters
		{
			get
			{
				List<string> list;
				if ((list = this.m_ExposedParameterNames) == null)
				{
					list = (this.m_ExposedParameterNames = new List<string>());
				}
				return list;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060000CD RID: 205 RVA: 0x00003A30 File Offset: 0x00001C30
		public ClipCaps clipCaps
		{
			get
			{
				ITimelineClipAsset timelineClipAsset = this.asset as ITimelineClipAsset;
				if (timelineClipAsset == null)
				{
					return TimelineClip.kDefaultClipCaps;
				}
				return timelineClipAsset.clipCaps;
			}
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00003A58 File Offset: 0x00001C58
		internal int Hash()
		{
			int hashCode = this.m_Start.GetHashCode();
			int hashCode2 = this.m_Duration.GetHashCode();
			int hashCode3 = this.m_TimeScale.GetHashCode();
			int hashCode4 = this.m_ClipIn.GetHashCode();
			int num = (int)this.m_PreExtrapolationMode;
			int hashCode5 = num.GetHashCode();
			num = (int)this.m_PostExtrapolationMode;
			return HashUtility.CombineHash(hashCode, hashCode2, hashCode3, hashCode4, hashCode5, num.GetHashCode());
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00003AB4 File Offset: 0x00001CB4
		public float EvaluateMixOut(double time)
		{
			if (!this.clipCaps.HasAny(ClipCaps.Blending))
			{
				return 1f;
			}
			if (this.mixOutDuration > (double)Mathf.Epsilon)
			{
				float num = (float)(time - this.mixOutTime) / (float)this.mixOutDuration;
				return Mathf.Clamp01(this.mixOutCurve.Evaluate(num));
			}
			return 1f;
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00003B10 File Offset: 0x00001D10
		public float EvaluateMixIn(double time)
		{
			if (!this.clipCaps.HasAny(ClipCaps.Blending))
			{
				return 1f;
			}
			if (this.mixInDuration > (double)Mathf.Epsilon)
			{
				float num = (float)(time - this.m_Start) / (float)this.mixInDuration;
				return Mathf.Clamp01(this.mixInCurve.Evaluate(num));
			}
			return 1f;
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00003B6B File Offset: 0x00001D6B
		private static AnimationCurve GetDefaultMixInCurve()
		{
			return AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00003B86 File Offset: 0x00001D86
		private static AnimationCurve GetDefaultMixOutCurve()
		{
			return AnimationCurve.EaseInOut(0f, 1f, 1f, 0f);
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00003BA4 File Offset: 0x00001DA4
		public double ToLocalTime(double time)
		{
			if (time < 0.0)
			{
				return time;
			}
			if (this.IsPreExtrapolatedTime(time))
			{
				time = TimelineClip.GetExtrapolatedTime(time - this.m_Start, this.m_PreExtrapolationMode, this.m_Duration);
			}
			else if (this.IsPostExtrapolatedTime(time))
			{
				time = TimelineClip.GetExtrapolatedTime(time - this.m_Start, this.m_PostExtrapolationMode, this.m_Duration);
			}
			else
			{
				time -= this.m_Start;
			}
			time *= this.timeScale;
			time += this.clipIn;
			return time;
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00003C2A File Offset: 0x00001E2A
		public double ToLocalTimeUnbound(double time)
		{
			return (time - this.m_Start) * this.timeScale + this.clipIn;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00003C42 File Offset: 0x00001E42
		internal double FromLocalTimeUnbound(double time)
		{
			return (time - this.clipIn) / this.timeScale + this.m_Start;
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x00003C5C File Offset: 0x00001E5C
		public AnimationClip animationClip
		{
			get
			{
				if (this.m_Asset == null)
				{
					return null;
				}
				AnimationPlayableAsset animationPlayableAsset = this.m_Asset as AnimationPlayableAsset;
				if (!(animationPlayableAsset != null))
				{
					return null;
				}
				return animationPlayableAsset.clip;
			}
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00003C96 File Offset: 0x00001E96
		private static double SanitizeTimeValue(double value, double defaultValue)
		{
			if (double.IsInfinity(value) || double.IsNaN(value))
			{
				Debug.LogError("Invalid time value assigned");
				return defaultValue;
			}
			return Math.Max(-TimelineClip.kMaxTimeValue, Math.Min(TimelineClip.kMaxTimeValue, value));
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00003CCA File Offset: 0x00001ECA
		// (set) Token: 0x060000D9 RID: 217 RVA: 0x00003CE2 File Offset: 0x00001EE2
		public TimelineClip.ClipExtrapolation postExtrapolationMode
		{
			get
			{
				if (!this.clipCaps.HasAny(ClipCaps.Extrapolation))
				{
					return TimelineClip.ClipExtrapolation.None;
				}
				return this.m_PostExtrapolationMode;
			}
			internal set
			{
				this.m_PostExtrapolationMode = (this.clipCaps.HasAny(ClipCaps.Extrapolation) ? value : TimelineClip.ClipExtrapolation.None);
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00003CFC File Offset: 0x00001EFC
		// (set) Token: 0x060000DB RID: 219 RVA: 0x00003D14 File Offset: 0x00001F14
		public TimelineClip.ClipExtrapolation preExtrapolationMode
		{
			get
			{
				if (!this.clipCaps.HasAny(ClipCaps.Extrapolation))
				{
					return TimelineClip.ClipExtrapolation.None;
				}
				return this.m_PreExtrapolationMode;
			}
			internal set
			{
				this.m_PreExtrapolationMode = (this.clipCaps.HasAny(ClipCaps.Extrapolation) ? value : TimelineClip.ClipExtrapolation.None);
			}
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00003D2E File Offset: 0x00001F2E
		internal void SetPostExtrapolationTime(double time)
		{
			this.m_PostExtrapolationTime = time;
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00003D37 File Offset: 0x00001F37
		internal void SetPreExtrapolationTime(double time)
		{
			this.m_PreExtrapolationTime = time;
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00003D40 File Offset: 0x00001F40
		public bool IsExtrapolatedTime(double sequenceTime)
		{
			return this.IsPreExtrapolatedTime(sequenceTime) || this.IsPostExtrapolatedTime(sequenceTime);
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00003D54 File Offset: 0x00001F54
		public bool IsPreExtrapolatedTime(double sequenceTime)
		{
			return this.preExtrapolationMode != TimelineClip.ClipExtrapolation.None && sequenceTime < this.m_Start && sequenceTime >= this.m_Start - this.m_PreExtrapolationTime;
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00003D7C File Offset: 0x00001F7C
		public bool IsPostExtrapolatedTime(double sequenceTime)
		{
			return this.postExtrapolationMode != TimelineClip.ClipExtrapolation.None && sequenceTime > this.end && sequenceTime - this.end < this.m_PostExtrapolationTime;
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x00003DA1 File Offset: 0x00001FA1
		public double extrapolatedStart
		{
			get
			{
				if (this.m_PreExtrapolationMode != TimelineClip.ClipExtrapolation.None)
				{
					return this.m_Start - this.m_PreExtrapolationTime;
				}
				return this.m_Start;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x00003DC0 File Offset: 0x00001FC0
		public double extrapolatedDuration
		{
			get
			{
				double num = this.m_Duration;
				if (this.m_PostExtrapolationMode != TimelineClip.ClipExtrapolation.None)
				{
					num += Math.Min(this.m_PostExtrapolationTime, TimelineClip.kMaxTimeValue);
				}
				if (this.m_PreExtrapolationMode != TimelineClip.ClipExtrapolation.None)
				{
					num += this.m_PreExtrapolationTime;
				}
				return num;
			}
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00003E04 File Offset: 0x00002004
		private static double GetExtrapolatedTime(double time, TimelineClip.ClipExtrapolation mode, double duration)
		{
			if (duration == 0.0)
			{
				return 0.0;
			}
			switch (mode)
			{
			case TimelineClip.ClipExtrapolation.Hold:
				if (time < 0.0)
				{
					return 0.0;
				}
				if (time > duration)
				{
					return duration;
				}
				break;
			case TimelineClip.ClipExtrapolation.Loop:
				if (time < 0.0)
				{
					time = duration - -time % duration;
				}
				else if (time > duration)
				{
					time %= duration;
				}
				break;
			case TimelineClip.ClipExtrapolation.PingPong:
				if (time < 0.0)
				{
					time = duration * 2.0 - -time % (duration * 2.0);
					time = duration - Math.Abs(time - duration);
				}
				else
				{
					time %= duration * 2.0;
					time = duration - Math.Abs(time - duration);
				}
				break;
			}
			return time;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00003ED5 File Offset: 0x000020D5
		public void CreateCurves(string curvesClipName)
		{
			if (this.m_AnimationCurves != null)
			{
				return;
			}
			this.m_AnimationCurves = TimelineCreateUtilities.CreateAnimationClipForTrack(string.IsNullOrEmpty(curvesClipName) ? TimelineClip.kDefaultCurvesName : curvesClipName, this.parentTrack, true);
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00003F08 File Offset: 0x00002108
		void ISerializationCallbackReceiver.OnBeforeSerialize()
		{
			this.m_Version = 1;
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00003F11 File Offset: 0x00002111
		void ISerializationCallbackReceiver.OnAfterDeserialize()
		{
			if (this.m_Version < 1)
			{
				this.UpgradeToLatestVersion();
			}
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00003F24 File Offset: 0x00002124
		public override string ToString()
		{
			return UnityString.Format("{0} ({1:F2}, {2:F2}):{3:F2} | {4}", new object[] { this.displayName, this.start, this.end, this.clipIn, this.parentTrack });
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x000028DC File Offset: 0x00000ADC
		private void UpdateDirty(double oldValue, double newValue)
		{
		}

		// Token: 0x04000043 RID: 67
		private const int k_LatestVersion = 1;

		// Token: 0x04000044 RID: 68
		[SerializeField]
		[HideInInspector]
		private int m_Version;

		// Token: 0x04000045 RID: 69
		public static readonly ClipCaps kDefaultClipCaps = ClipCaps.Blending;

		// Token: 0x04000046 RID: 70
		public static readonly float kDefaultClipDurationInSeconds = 5f;

		// Token: 0x04000047 RID: 71
		public static readonly double kTimeScaleMin = 0.001;

		// Token: 0x04000048 RID: 72
		public static readonly double kTimeScaleMax = 1000.0;

		// Token: 0x04000049 RID: 73
		internal static readonly string kDefaultCurvesName = "Clip Parameters";

		// Token: 0x0400004A RID: 74
		internal static readonly double kMinDuration = 0.016666666666666666;

		// Token: 0x0400004B RID: 75
		internal static readonly double kMaxTimeValue = 1000000.0;

		// Token: 0x0400004C RID: 76
		[SerializeField]
		private double m_Start;

		// Token: 0x0400004D RID: 77
		[SerializeField]
		private double m_ClipIn;

		// Token: 0x0400004E RID: 78
		[SerializeField]
		private Object m_Asset;

		// Token: 0x0400004F RID: 79
		[SerializeField]
		[FormerlySerializedAs("m_HackDuration")]
		private double m_Duration;

		// Token: 0x04000050 RID: 80
		[SerializeField]
		private double m_TimeScale = 1.0;

		// Token: 0x04000051 RID: 81
		[SerializeField]
		private TrackAsset m_ParentTrack;

		// Token: 0x04000052 RID: 82
		[SerializeField]
		private double m_EaseInDuration;

		// Token: 0x04000053 RID: 83
		[SerializeField]
		private double m_EaseOutDuration;

		// Token: 0x04000054 RID: 84
		[SerializeField]
		private double m_BlendInDuration = -1.0;

		// Token: 0x04000055 RID: 85
		[SerializeField]
		private double m_BlendOutDuration = -1.0;

		// Token: 0x04000056 RID: 86
		[SerializeField]
		private AnimationCurve m_MixInCurve;

		// Token: 0x04000057 RID: 87
		[SerializeField]
		private AnimationCurve m_MixOutCurve;

		// Token: 0x04000058 RID: 88
		[SerializeField]
		private TimelineClip.BlendCurveMode m_BlendInCurveMode;

		// Token: 0x04000059 RID: 89
		[SerializeField]
		private TimelineClip.BlendCurveMode m_BlendOutCurveMode;

		// Token: 0x0400005A RID: 90
		[SerializeField]
		private List<string> m_ExposedParameterNames;

		// Token: 0x0400005B RID: 91
		[SerializeField]
		private AnimationClip m_AnimationCurves;

		// Token: 0x0400005C RID: 92
		[SerializeField]
		private bool m_Recordable;

		// Token: 0x0400005D RID: 93
		[SerializeField]
		private TimelineClip.ClipExtrapolation m_PostExtrapolationMode;

		// Token: 0x0400005E RID: 94
		[SerializeField]
		private TimelineClip.ClipExtrapolation m_PreExtrapolationMode;

		// Token: 0x0400005F RID: 95
		[SerializeField]
		private double m_PostExtrapolationTime;

		// Token: 0x04000060 RID: 96
		[SerializeField]
		private double m_PreExtrapolationTime;

		// Token: 0x04000061 RID: 97
		[SerializeField]
		private string m_DisplayName;

		// Token: 0x02000059 RID: 89
		private enum Versions
		{
			// Token: 0x04000115 RID: 277
			Initial,
			// Token: 0x04000116 RID: 278
			ClipInFromGlobalToLocal
		}

		// Token: 0x0200005A RID: 90
		private static class TimelineClipUpgrade
		{
			// Token: 0x060002FA RID: 762 RVA: 0x0000A833 File Offset: 0x00008A33
			public static void UpgradeClipInFromGlobalToLocal(TimelineClip clip)
			{
				if (clip.m_ClipIn > 0.0 && clip.m_TimeScale > 1.401298464324817E-45)
				{
					clip.m_ClipIn *= clip.m_TimeScale;
				}
			}
		}

		// Token: 0x0200005B RID: 91
		public enum ClipExtrapolation
		{
			// Token: 0x04000118 RID: 280
			None,
			// Token: 0x04000119 RID: 281
			Hold,
			// Token: 0x0400011A RID: 282
			Loop,
			// Token: 0x0400011B RID: 283
			PingPong,
			// Token: 0x0400011C RID: 284
			Continue
		}

		// Token: 0x0200005C RID: 92
		public enum BlendCurveMode
		{
			// Token: 0x0400011E RID: 286
			Auto,
			// Token: 0x0400011F RID: 287
			Manual
		}
	}
}
