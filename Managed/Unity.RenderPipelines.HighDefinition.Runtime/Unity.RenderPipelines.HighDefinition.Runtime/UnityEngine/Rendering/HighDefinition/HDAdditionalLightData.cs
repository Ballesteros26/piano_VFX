using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Serialization;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000049 RID: 73
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.render-pipelines.high-definition@8.0/manual/Light-Component.html")]
	[RequireComponent(typeof(Light))]
	[ExecuteAlways]
	public class HDAdditionalLightData : MonoBehaviour, ISerializationCallbackReceiver, IVersionable<HDAdditionalLightData.Version>
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x0000BC5E File Offset: 0x00009E5E
		// (set) Token: 0x060001B1 RID: 433 RVA: 0x0000BC66 File Offset: 0x00009E66
		HDAdditionalLightData.Version IVersionable<HDAdditionalLightData.Version>.version
		{
			get
			{
				return this.m_Version;
			}
			set
			{
				this.m_Version = value;
			}
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00002646 File Offset: 0x00000846
		void ISerializationCallbackReceiver.OnAfterDeserialize()
		{
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x0000BC6F File Offset: 0x00009E6F
		void ISerializationCallbackReceiver.OnBeforeSerialize()
		{
			this.UpdateBounds();
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x0000BC77 File Offset: 0x00009E77
		private void OnEnable()
		{
			if (this.shadowUpdateMode == ShadowUpdateMode.OnEnable)
			{
				this.m_ShadowMapRenderedSinceLastRequest = false;
			}
			this.SetEmissiveMeshRendererEnabled(true);
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x0000BC90 File Offset: 0x00009E90
		private void Migrate()
		{
			HDAdditionalLightData.k_HDLightMigrationSteps.Migrate(this);
			this.OnValidate();
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x0000BCB2 File Offset: 0x00009EB2
		private void Awake()
		{
			this.Migrate();
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x0000BCBA File Offset: 0x00009EBA
		// (set) Token: 0x060001B8 RID: 440 RVA: 0x0000BCC8 File Offset: 0x00009EC8
		public HDLightType type
		{
			get
			{
				return this.ComputeLightType(this.legacyLight);
			}
			set
			{
				if (this.type != value)
				{
					switch (value)
					{
					case HDLightType.Spot:
						this.legacyLight.type = LightType.Spot;
						this.m_PointlightHDType = HDAdditionalLightData.PointLightHDType.Punctual;
						break;
					case HDLightType.Directional:
						this.legacyLight.type = LightType.Directional;
						this.m_PointlightHDType = HDAdditionalLightData.PointLightHDType.Punctual;
						break;
					case HDLightType.Point:
						this.legacyLight.type = LightType.Point;
						this.m_PointlightHDType = HDAdditionalLightData.PointLightHDType.Punctual;
						break;
					case HDLightType.Area:
						this.ResolveAreaShape();
						break;
					}
					LightUnit[] supportedLightUnits = HDAdditionalLightData.GetSupportedLightUnits(value, this.m_SpotLightShape);
					if (!supportedLightUnits.Any((LightUnit u) => u == this.lightUnit))
					{
						this.lightUnit = supportedLightUnits.First<LightUnit>();
					}
					this.UpdateAllLightValues();
				}
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x0000BD71 File Offset: 0x00009F71
		// (set) Token: 0x060001BA RID: 442 RVA: 0x0000BD7C File Offset: 0x00009F7C
		public SpotLightShape spotLightShape
		{
			get
			{
				return this.m_SpotLightShape;
			}
			set
			{
				if (this.m_SpotLightShape == value)
				{
					return;
				}
				this.m_SpotLightShape = value;
				LightUnit[] supportedLightUnits = HDAdditionalLightData.GetSupportedLightUnits(this.type, value);
				if (!supportedLightUnits.Any((LightUnit u) => u == this.lightUnit))
				{
					this.lightUnit = supportedLightUnits.First<LightUnit>();
				}
				this.UpdateAllLightValues();
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x060001BB RID: 443 RVA: 0x0000BDCD File Offset: 0x00009FCD
		// (set) Token: 0x060001BC RID: 444 RVA: 0x0000BDD5 File Offset: 0x00009FD5
		public AreaLightShape areaLightShape
		{
			get
			{
				return this.m_AreaLightShape;
			}
			set
			{
				if (this.m_AreaLightShape == value)
				{
					return;
				}
				this.m_AreaLightShape = value;
				if (this.type == HDLightType.Area)
				{
					this.ResolveAreaShape();
				}
				this.UpdateAllLightValues();
			}
		}

		// Token: 0x060001BD RID: 445 RVA: 0x0000BDFD File Offset: 0x00009FFD
		private void ResolveAreaShape()
		{
			this.m_PointlightHDType = HDAdditionalLightData.PointLightHDType.Area;
			if (this.areaLightShape == AreaLightShape.Disc)
			{
				this.legacyLight.type = LightType.Disc;
				return;
			}
			this.legacyLight.type = LightType.Point;
		}

		// Token: 0x060001BE RID: 446 RVA: 0x0000BE28 File Offset: 0x0000A028
		public void SetLightTypeAndShape(HDLightTypeAndShape typeAndShape)
		{
			switch (typeAndShape)
			{
			case HDLightTypeAndShape.Point:
				this.type = HDLightType.Point;
				return;
			case HDLightTypeAndShape.BoxSpot:
				this.type = HDLightType.Spot;
				this.spotLightShape = SpotLightShape.Box;
				return;
			case HDLightTypeAndShape.PyramidSpot:
				this.type = HDLightType.Spot;
				this.spotLightShape = SpotLightShape.Pyramid;
				return;
			case HDLightTypeAndShape.ConeSpot:
				this.type = HDLightType.Spot;
				this.spotLightShape = SpotLightShape.Cone;
				return;
			case HDLightTypeAndShape.Directional:
				this.type = HDLightType.Directional;
				return;
			case HDLightTypeAndShape.RectangleArea:
				this.type = HDLightType.Area;
				this.areaLightShape = AreaLightShape.Rectangle;
				return;
			case HDLightTypeAndShape.TubeArea:
				this.type = HDLightType.Area;
				this.areaLightShape = AreaLightShape.Tube;
				return;
			case HDLightTypeAndShape.DiscArea:
				this.type = HDLightType.Area;
				this.areaLightShape = AreaLightShape.Disc;
				return;
			default:
				return;
			}
		}

		// Token: 0x060001BF RID: 447 RVA: 0x0000BEC8 File Offset: 0x0000A0C8
		public HDLightTypeAndShape GetLightTypeAndShape()
		{
			switch (this.type)
			{
			case HDLightType.Spot:
				switch (this.spotLightShape)
				{
				case SpotLightShape.Cone:
					return HDLightTypeAndShape.ConeSpot;
				case SpotLightShape.Pyramid:
					return HDLightTypeAndShape.PyramidSpot;
				case SpotLightShape.Box:
					return HDLightTypeAndShape.BoxSpot;
				default:
					throw new Exception(string.Format("Unknown {0}: {1}", typeof(SpotLightShape), this.spotLightShape));
				}
				break;
			case HDLightType.Directional:
				return HDLightTypeAndShape.Directional;
			case HDLightType.Point:
				return HDLightTypeAndShape.Point;
			case HDLightType.Area:
				switch (this.areaLightShape)
				{
				case AreaLightShape.Rectangle:
					return HDLightTypeAndShape.RectangleArea;
				case AreaLightShape.Tube:
					return HDLightTypeAndShape.TubeArea;
				case AreaLightShape.Disc:
					return HDLightTypeAndShape.DiscArea;
				default:
					throw new Exception(string.Format("Unknown {0}: {1}", typeof(AreaLightShape), this.areaLightShape));
				}
				break;
			default:
				throw new Exception(string.Format("Unknown {0}: {1}", typeof(HDLightType), this.type));
			}
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x0000BFAC File Offset: 0x0000A1AC
		private string GetLightTypeName()
		{
			if (this.type == HDLightType.Area)
			{
				return string.Format("{0}AreaLight", this.areaLightShape);
			}
			if (this.legacyLight.type == LightType.Spot)
			{
				return string.Format("{0}SpotLight", this.spotLightShape);
			}
			return string.Format("{0}Light", this.legacyLight.type);
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x0000C018 File Offset: 0x0000A218
		public static LightUnit[] GetSupportedLightUnits(HDLightType type, SpotLightShape spotLightShape)
		{
			int num = (int)(type & (HDLightType)255);
			num |= (int)((int)(spotLightShape & (SpotLightShape)255) << 8);
			LightUnit[] array;
			if (HDAdditionalLightData.supportedLightTypeCache.TryGetValue(num, out array))
			{
				return array;
			}
			if (type == HDLightType.Area)
			{
				array = Enum.GetValues(typeof(AreaLightUnit)).Cast<LightUnit>().ToArray<LightUnit>();
			}
			else if (type == HDLightType.Directional || (type == HDLightType.Spot && spotLightShape == SpotLightShape.Box))
			{
				array = Enum.GetValues(typeof(DirectionalLightUnit)).Cast<LightUnit>().ToArray<LightUnit>();
			}
			else
			{
				array = Enum.GetValues(typeof(PunctualLightUnit)).Cast<LightUnit>().ToArray<LightUnit>();
			}
			HDAdditionalLightData.supportedLightTypeCache[num] = array;
			return array;
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x0000C0B8 File Offset: 0x0000A2B8
		public static bool IsValidLightUnitForType(HDLightType type, SpotLightShape spotLightShape, LightUnit unit)
		{
			return HDAdditionalLightData.GetSupportedLightUnits(type, spotLightShape).Any((LightUnit u) => u == unit);
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x0000C0EC File Offset: 0x0000A2EC
		internal HDLightType ComputeLightType(Light attachedLight)
		{
			if (attachedLight == null)
			{
				return HDLightType.Point;
			}
			switch (attachedLight.type)
			{
			case LightType.Spot:
				return HDLightType.Spot;
			case LightType.Directional:
				return HDLightType.Directional;
			case LightType.Point:
			{
				HDAdditionalLightData.PointLightHDType pointlightHDType = this.m_PointlightHDType;
				if (pointlightHDType == HDAdditionalLightData.PointLightHDType.Punctual)
				{
					return HDLightType.Point;
				}
				if (pointlightHDType != HDAdditionalLightData.PointLightHDType.Area)
				{
					return HDLightType.Point;
				}
				return HDLightType.Area;
			}
			case LightType.Area:
				if (this != HDUtils.s_DefaultHDAdditionalLightData)
				{
					this.legacyLight.type = LightType.Point;
					this.m_PointlightHDType = HDAdditionalLightData.PointLightHDType.Area;
					this.m_AreaLightShape = AreaLightShape.Rectangle;
				}
				return HDLightType.Area;
			case LightType.Disc:
				return HDLightType.Area;
			default:
				return HDLightType.Point;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x0000C16D File Offset: 0x0000A36D
		// (set) Token: 0x060001C5 RID: 453 RVA: 0x0000C175 File Offset: 0x0000A375
		public float intensity
		{
			get
			{
				return this.m_Intensity;
			}
			set
			{
				if (this.m_Intensity == value)
				{
					return;
				}
				this.m_Intensity = Mathf.Clamp(value, 0f, float.MaxValue);
				this.UpdateLightIntensity();
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x0000C19D File Offset: 0x0000A39D
		// (set) Token: 0x060001C7 RID: 455 RVA: 0x0000C1A5 File Offset: 0x0000A3A5
		public bool enableSpotReflector
		{
			get
			{
				return this.m_EnableSpotReflector;
			}
			set
			{
				if (this.m_EnableSpotReflector == value)
				{
					return;
				}
				this.m_EnableSpotReflector = value;
				this.UpdateLightIntensity();
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x0000C1BE File Offset: 0x0000A3BE
		// (set) Token: 0x060001C9 RID: 457 RVA: 0x0000C1C6 File Offset: 0x0000A3C6
		public float luxAtDistance
		{
			get
			{
				return this.m_LuxAtDistance;
			}
			set
			{
				if (this.m_LuxAtDistance == value)
				{
					return;
				}
				this.m_LuxAtDistance = Mathf.Clamp(value, 0f, float.MaxValue);
				this.UpdateLightIntensity();
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060001CA RID: 458 RVA: 0x0000C1EE File Offset: 0x0000A3EE
		// (set) Token: 0x060001CB RID: 459 RVA: 0x0000C1F6 File Offset: 0x0000A3F6
		public float innerSpotPercent
		{
			get
			{
				return this.m_InnerSpotPercent;
			}
			set
			{
				if (this.m_InnerSpotPercent == value)
				{
					return;
				}
				this.m_InnerSpotPercent = Mathf.Clamp(value, 0f, 100f);
				this.UpdateLightIntensity();
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060001CC RID: 460 RVA: 0x0000C21E File Offset: 0x0000A41E
		public float innerSpotPercent01
		{
			get
			{
				return this.innerSpotPercent / 100f;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060001CD RID: 461 RVA: 0x0000C22C File Offset: 0x0000A42C
		// (set) Token: 0x060001CE RID: 462 RVA: 0x0000C234 File Offset: 0x0000A434
		public float lightDimmer
		{
			get
			{
				return this.m_LightDimmer;
			}
			set
			{
				if (this.m_LightDimmer == value)
				{
					return;
				}
				this.m_LightDimmer = Mathf.Clamp01(value);
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060001CF RID: 463 RVA: 0x0000C24C File Offset: 0x0000A44C
		// (set) Token: 0x060001D0 RID: 464 RVA: 0x0000C262 File Offset: 0x0000A462
		public float volumetricDimmer
		{
			get
			{
				if (!this.useVolumetric)
				{
					return 0f;
				}
				return this.m_VolumetricDimmer;
			}
			set
			{
				if (this.m_VolumetricDimmer == value)
				{
					return;
				}
				this.m_VolumetricDimmer = Mathf.Clamp01(value);
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x0000C27A File Offset: 0x0000A47A
		// (set) Token: 0x060001D2 RID: 466 RVA: 0x0000C284 File Offset: 0x0000A484
		public LightUnit lightUnit
		{
			get
			{
				return this.m_LightUnit;
			}
			set
			{
				if (this.m_LightUnit == value)
				{
					return;
				}
				if (!HDAdditionalLightData.IsValidLightUnitForType(this.type, this.m_SpotLightShape, value))
				{
					string text = string.Join<LightUnit>(", ", HDAdditionalLightData.GetSupportedLightUnits(this.type, this.m_SpotLightShape));
					Debug.LogError(string.Format("Set Light Unit '{0}' to a {1} is not allowed, only {2} are supported.", value, this.GetLightTypeName(), text));
					return;
				}
				LightUtils.ConvertLightIntensity(this.m_LightUnit, value, this, this.legacyLight);
				this.m_LightUnit = value;
				this.UpdateLightIntensity();
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x0000C308 File Offset: 0x0000A508
		// (set) Token: 0x060001D4 RID: 468 RVA: 0x0000C310 File Offset: 0x0000A510
		public float fadeDistance
		{
			get
			{
				return this.m_FadeDistance;
			}
			set
			{
				if (this.m_FadeDistance == value)
				{
					return;
				}
				this.m_FadeDistance = Mathf.Clamp(value, 0f, float.MaxValue);
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x0000C332 File Offset: 0x0000A532
		// (set) Token: 0x060001D6 RID: 470 RVA: 0x0000C33A File Offset: 0x0000A53A
		public bool affectDiffuse
		{
			get
			{
				return this.m_AffectDiffuse;
			}
			set
			{
				if (this.m_AffectDiffuse == value)
				{
					return;
				}
				this.m_AffectDiffuse = value;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x0000C34D File Offset: 0x0000A54D
		// (set) Token: 0x060001D8 RID: 472 RVA: 0x0000C355 File Offset: 0x0000A555
		public bool affectSpecular
		{
			get
			{
				return this.m_AffectSpecular;
			}
			set
			{
				if (this.m_AffectSpecular == value)
				{
					return;
				}
				this.m_AffectSpecular = value;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x0000C368 File Offset: 0x0000A568
		// (set) Token: 0x060001DA RID: 474 RVA: 0x0000C370 File Offset: 0x0000A570
		public bool nonLightmappedOnly
		{
			get
			{
				return this.m_NonLightmappedOnly;
			}
			set
			{
				if (this.m_NonLightmappedOnly == value)
				{
					return;
				}
				this.m_NonLightmappedOnly = value;
				this.legacyLight.lightShadowCasterMode = (value ? LightShadowCasterMode.NonLightmappedOnly : LightShadowCasterMode.Everything);
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060001DB RID: 475 RVA: 0x0000C395 File Offset: 0x0000A595
		// (set) Token: 0x060001DC RID: 476 RVA: 0x0000C3A0 File Offset: 0x0000A5A0
		public float shapeWidth
		{
			get
			{
				return this.m_ShapeWidth;
			}
			set
			{
				if (this.m_ShapeWidth == value)
				{
					return;
				}
				if (this.type == HDLightType.Area)
				{
					this.m_ShapeWidth = Mathf.Clamp(value, 0.01f, float.MaxValue);
				}
				else
				{
					this.m_ShapeWidth = Mathf.Clamp(value, 0f, float.MaxValue);
				}
				this.UpdateAllLightValues();
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060001DD RID: 477 RVA: 0x0000C3F4 File Offset: 0x0000A5F4
		// (set) Token: 0x060001DE RID: 478 RVA: 0x0000C3FC File Offset: 0x0000A5FC
		public float shapeHeight
		{
			get
			{
				return this.m_ShapeHeight;
			}
			set
			{
				if (this.m_ShapeHeight == value)
				{
					return;
				}
				if (this.type == HDLightType.Area)
				{
					this.m_ShapeHeight = Mathf.Clamp(value, 0.01f, float.MaxValue);
				}
				else
				{
					this.m_ShapeHeight = Mathf.Clamp(value, 0f, float.MaxValue);
				}
				this.UpdateAllLightValues();
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060001DF RID: 479 RVA: 0x0000C450 File Offset: 0x0000A650
		// (set) Token: 0x060001E0 RID: 480 RVA: 0x0000C458 File Offset: 0x0000A658
		public float aspectRatio
		{
			get
			{
				return this.m_AspectRatio;
			}
			set
			{
				if (this.m_AspectRatio == value)
				{
					return;
				}
				this.m_AspectRatio = Mathf.Clamp(value, 0.05f, 20f);
				this.UpdateAllLightValues();
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x0000C480 File Offset: 0x0000A680
		// (set) Token: 0x060001E2 RID: 482 RVA: 0x0000C488 File Offset: 0x0000A688
		public float shapeRadius
		{
			get
			{
				return this.m_ShapeRadius;
			}
			set
			{
				if (this.m_ShapeRadius == value)
				{
					return;
				}
				this.m_ShapeRadius = Mathf.Clamp(value, 0f, float.MaxValue);
				this.UpdateAllLightValues();
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x0000C4B0 File Offset: 0x0000A6B0
		// (set) Token: 0x060001E4 RID: 484 RVA: 0x0000C4B8 File Offset: 0x0000A6B8
		public float softnessScale
		{
			get
			{
				return this.m_SoftnessScale;
			}
			set
			{
				if (this.m_SoftnessScale == value)
				{
					return;
				}
				this.m_SoftnessScale = Mathf.Clamp(value, 0f, float.MaxValue);
				this.UpdateAllLightValues();
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x0000C4E0 File Offset: 0x0000A6E0
		// (set) Token: 0x060001E6 RID: 486 RVA: 0x0000C4E8 File Offset: 0x0000A6E8
		public bool useCustomSpotLightShadowCone
		{
			get
			{
				return this.m_UseCustomSpotLightShadowCone;
			}
			set
			{
				if (this.m_UseCustomSpotLightShadowCone == value)
				{
					return;
				}
				this.m_UseCustomSpotLightShadowCone = value;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x0000C4FB File Offset: 0x0000A6FB
		// (set) Token: 0x060001E8 RID: 488 RVA: 0x0000C503 File Offset: 0x0000A703
		public float customSpotLightShadowCone
		{
			get
			{
				return this.m_CustomSpotLightShadowCone;
			}
			set
			{
				if (this.m_CustomSpotLightShadowCone == value)
				{
					return;
				}
				this.m_CustomSpotLightShadowCone = value;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x0000C516 File Offset: 0x0000A716
		// (set) Token: 0x060001EA RID: 490 RVA: 0x0000C51E File Offset: 0x0000A71E
		public float maxSmoothness
		{
			get
			{
				return this.m_MaxSmoothness;
			}
			set
			{
				if (this.m_MaxSmoothness == value)
				{
					return;
				}
				this.m_MaxSmoothness = Mathf.Clamp01(value);
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060001EB RID: 491 RVA: 0x0000C536 File Offset: 0x0000A736
		// (set) Token: 0x060001EC RID: 492 RVA: 0x0000C53E File Offset: 0x0000A73E
		public bool applyRangeAttenuation
		{
			get
			{
				return this.m_ApplyRangeAttenuation;
			}
			set
			{
				if (this.m_ApplyRangeAttenuation == value)
				{
					return;
				}
				this.m_ApplyRangeAttenuation = value;
				this.UpdateAllLightValues();
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060001ED RID: 493 RVA: 0x0000C557 File Offset: 0x0000A757
		// (set) Token: 0x060001EE RID: 494 RVA: 0x0000C55F File Offset: 0x0000A75F
		internal bool displayAreaLightEmissiveMesh
		{
			get
			{
				return this.m_DisplayAreaLightEmissiveMesh;
			}
			set
			{
				if (this.m_DisplayAreaLightEmissiveMesh == value)
				{
					return;
				}
				this.m_DisplayAreaLightEmissiveMesh = value;
				this.UpdateAllLightValues();
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060001EF RID: 495 RVA: 0x0000C578 File Offset: 0x0000A778
		// (set) Token: 0x060001F0 RID: 496 RVA: 0x0000C580 File Offset: 0x0000A780
		public Texture areaLightCookie
		{
			get
			{
				return this.m_AreaLightCookie;
			}
			set
			{
				if (this.m_AreaLightCookie == value)
				{
					return;
				}
				this.m_AreaLightCookie = value;
				this.UpdateAllLightValues();
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x0000C59E File Offset: 0x0000A79E
		// (set) Token: 0x060001F2 RID: 498 RVA: 0x0000C5A6 File Offset: 0x0000A7A6
		public float areaLightShadowCone
		{
			get
			{
				return this.m_AreaLightShadowCone;
			}
			set
			{
				if (this.m_AreaLightShadowCone == value)
				{
					return;
				}
				this.m_AreaLightShadowCone = Mathf.Clamp(value, 10f, 179f);
				this.UpdateAllLightValues();
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x0000C5CE File Offset: 0x0000A7CE
		// (set) Token: 0x060001F4 RID: 500 RVA: 0x0000C5D6 File Offset: 0x0000A7D6
		public bool useScreenSpaceShadows
		{
			get
			{
				return this.m_UseScreenSpaceShadows;
			}
			set
			{
				if (this.m_UseScreenSpaceShadows == value)
				{
					return;
				}
				this.m_UseScreenSpaceShadows = value;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x0000C5E9 File Offset: 0x0000A7E9
		// (set) Token: 0x060001F6 RID: 502 RVA: 0x0000C5F1 File Offset: 0x0000A7F1
		public bool interactsWithSky
		{
			get
			{
				return this.m_InteractsWithSky;
			}
			set
			{
				if (this.m_InteractsWithSky == value)
				{
					return;
				}
				this.m_InteractsWithSky = value;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x0000C604 File Offset: 0x0000A804
		// (set) Token: 0x060001F8 RID: 504 RVA: 0x0000C60C File Offset: 0x0000A80C
		public float angularDiameter
		{
			get
			{
				return this.m_AngularDiameter;
			}
			set
			{
				if (this.m_AngularDiameter == value)
				{
					return;
				}
				this.m_AngularDiameter = value;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x0000C61F File Offset: 0x0000A81F
		// (set) Token: 0x060001FA RID: 506 RVA: 0x0000C627 File Offset: 0x0000A827
		public float flareSize
		{
			get
			{
				return this.m_FlareSize;
			}
			set
			{
				if (this.m_FlareSize == value)
				{
					return;
				}
				this.m_FlareSize = value;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060001FB RID: 507 RVA: 0x0000C63A File Offset: 0x0000A83A
		// (set) Token: 0x060001FC RID: 508 RVA: 0x0000C642 File Offset: 0x0000A842
		public Color flareTint
		{
			get
			{
				return this.m_FlareTint;
			}
			set
			{
				if (this.m_FlareTint == value)
				{
					return;
				}
				this.m_FlareTint = value;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060001FD RID: 509 RVA: 0x0000C65A File Offset: 0x0000A85A
		// (set) Token: 0x060001FE RID: 510 RVA: 0x0000C662 File Offset: 0x0000A862
		public float flareFalloff
		{
			get
			{
				return this.m_FlareFalloff;
			}
			set
			{
				if (this.m_FlareFalloff == value)
				{
					return;
				}
				this.m_FlareFalloff = value;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060001FF RID: 511 RVA: 0x0000C675 File Offset: 0x0000A875
		// (set) Token: 0x06000200 RID: 512 RVA: 0x0000C67D File Offset: 0x0000A87D
		public Texture2D surfaceTexture
		{
			get
			{
				return this.m_SurfaceTexture;
			}
			set
			{
				if (this.m_SurfaceTexture == value)
				{
					return;
				}
				this.m_SurfaceTexture = value;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000201 RID: 513 RVA: 0x0000C695 File Offset: 0x0000A895
		// (set) Token: 0x06000202 RID: 514 RVA: 0x0000C69D File Offset: 0x0000A89D
		public Color surfaceTint
		{
			get
			{
				return this.m_SurfaceTint;
			}
			set
			{
				if (this.m_SurfaceTint == value)
				{
					return;
				}
				this.m_SurfaceTint = value;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000203 RID: 515 RVA: 0x0000C6B5 File Offset: 0x0000A8B5
		// (set) Token: 0x06000204 RID: 516 RVA: 0x0000C6BD File Offset: 0x0000A8BD
		public float distance
		{
			get
			{
				return this.m_Distance;
			}
			set
			{
				if (this.m_Distance == value)
				{
					return;
				}
				this.m_Distance = value;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000205 RID: 517 RVA: 0x0000C6D0 File Offset: 0x0000A8D0
		// (set) Token: 0x06000206 RID: 518 RVA: 0x0000C6D8 File Offset: 0x0000A8D8
		public bool useRayTracedShadows
		{
			get
			{
				return this.m_UseRayTracedShadows;
			}
			set
			{
				if (this.m_UseRayTracedShadows == value)
				{
					return;
				}
				this.m_UseRayTracedShadows = value;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000207 RID: 519 RVA: 0x0000C6EB File Offset: 0x0000A8EB
		// (set) Token: 0x06000208 RID: 520 RVA: 0x0000C6F3 File Offset: 0x0000A8F3
		public int numRayTracingSamples
		{
			get
			{
				return this.m_NumRayTracingSamples;
			}
			set
			{
				if (this.m_NumRayTracingSamples == value)
				{
					return;
				}
				this.m_NumRayTracingSamples = Mathf.Clamp(value, 1, 32);
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000209 RID: 521 RVA: 0x0000C70E File Offset: 0x0000A90E
		// (set) Token: 0x0600020A RID: 522 RVA: 0x0000C716 File Offset: 0x0000A916
		public bool filterTracedShadow
		{
			get
			{
				return this.m_FilterTracedShadow;
			}
			set
			{
				if (this.m_FilterTracedShadow == value)
				{
					return;
				}
				this.m_FilterTracedShadow = value;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600020B RID: 523 RVA: 0x0000C729 File Offset: 0x0000A929
		// (set) Token: 0x0600020C RID: 524 RVA: 0x0000C731 File Offset: 0x0000A931
		public int filterSizeTraced
		{
			get
			{
				return this.m_FilterSizeTraced;
			}
			set
			{
				if (this.m_FilterSizeTraced == value)
				{
					return;
				}
				this.m_FilterSizeTraced = Mathf.Clamp(value, 1, 32);
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600020D RID: 525 RVA: 0x0000C74C File Offset: 0x0000A94C
		// (set) Token: 0x0600020E RID: 526 RVA: 0x0000C754 File Offset: 0x0000A954
		public float sunLightConeAngle
		{
			get
			{
				return this.m_SunLightConeAngle;
			}
			set
			{
				if (this.m_SunLightConeAngle == value)
				{
					return;
				}
				this.m_SunLightConeAngle = Mathf.Clamp(value, 0f, 2f);
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600020F RID: 527 RVA: 0x0000C776 File Offset: 0x0000A976
		// (set) Token: 0x06000210 RID: 528 RVA: 0x0000C77E File Offset: 0x0000A97E
		public float lightShadowRadius
		{
			get
			{
				return this.m_LightShadowRadius;
			}
			set
			{
				if (this.m_LightShadowRadius == value)
				{
					return;
				}
				this.m_LightShadowRadius = Mathf.Max(value, 0.001f);
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000211 RID: 529 RVA: 0x0000C79B File Offset: 0x0000A99B
		// (set) Token: 0x06000212 RID: 530 RVA: 0x0000C7A3 File Offset: 0x0000A9A3
		public bool semiTransparentShadow
		{
			get
			{
				return this.m_SemiTransparentShadow;
			}
			set
			{
				if (this.m_SemiTransparentShadow == value)
				{
					return;
				}
				this.m_SemiTransparentShadow = value;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000213 RID: 531 RVA: 0x0000C7B6 File Offset: 0x0000A9B6
		// (set) Token: 0x06000214 RID: 532 RVA: 0x0000C7BE File Offset: 0x0000A9BE
		public bool colorShadow
		{
			get
			{
				return this.m_ColorShadow;
			}
			set
			{
				if (this.m_ColorShadow == value)
				{
					return;
				}
				this.m_ColorShadow = value;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000215 RID: 533 RVA: 0x0000C7D1 File Offset: 0x0000A9D1
		// (set) Token: 0x06000216 RID: 534 RVA: 0x0000C7D9 File Offset: 0x0000A9D9
		public float evsmExponent
		{
			get
			{
				return this.m_EvsmExponent;
			}
			set
			{
				if (this.m_EvsmExponent == value)
				{
					return;
				}
				this.m_EvsmExponent = Mathf.Clamp(value, 5f, 42f);
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000217 RID: 535 RVA: 0x0000C7FB File Offset: 0x0000A9FB
		// (set) Token: 0x06000218 RID: 536 RVA: 0x0000C803 File Offset: 0x0000AA03
		public float evsmLightLeakBias
		{
			get
			{
				return this.m_EvsmLightLeakBias;
			}
			set
			{
				if (this.m_EvsmLightLeakBias == value)
				{
					return;
				}
				this.m_EvsmLightLeakBias = Mathf.Clamp(value, 0f, 1f);
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000219 RID: 537 RVA: 0x0000C825 File Offset: 0x0000AA25
		// (set) Token: 0x0600021A RID: 538 RVA: 0x0000C82D File Offset: 0x0000AA2D
		public float evsmVarianceBias
		{
			get
			{
				return this.m_EvsmVarianceBias;
			}
			set
			{
				if (this.m_EvsmVarianceBias == value)
				{
					return;
				}
				this.m_EvsmVarianceBias = Mathf.Clamp(value, 0f, 0.001f);
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600021B RID: 539 RVA: 0x0000C84F File Offset: 0x0000AA4F
		// (set) Token: 0x0600021C RID: 540 RVA: 0x0000C857 File Offset: 0x0000AA57
		public int evsmBlurPasses
		{
			get
			{
				return this.m_EvsmBlurPasses;
			}
			set
			{
				if (this.m_EvsmBlurPasses == value)
				{
					return;
				}
				this.m_EvsmBlurPasses = Mathf.Clamp(value, 0, 8);
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600021D RID: 541 RVA: 0x0000C871 File Offset: 0x0000AA71
		// (set) Token: 0x0600021E RID: 542 RVA: 0x0000C892 File Offset: 0x0000AA92
		public LightLayerEnum lightlayersMask
		{
			get
			{
				if (!this.linkShadowLayers)
				{
					return this.m_LightlayersMask;
				}
				return (LightLayerEnum)HDAdditionalLightData.RenderingLayerMaskToLightLayer(this.legacyLight.renderingLayerMask);
			}
			set
			{
				this.m_LightlayersMask = value;
				if (this.linkShadowLayers)
				{
					this.legacyLight.renderingLayerMask = HDAdditionalLightData.LightLayerToRenderingLayerMask((int)this.m_LightlayersMask, this.legacyLight.renderingLayerMask);
				}
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600021F RID: 543 RVA: 0x0000C8C4 File Offset: 0x0000AAC4
		// (set) Token: 0x06000220 RID: 544 RVA: 0x0000C8CC File Offset: 0x0000AACC
		public bool linkShadowLayers
		{
			get
			{
				return this.m_LinkShadowLayers;
			}
			set
			{
				this.m_LinkShadowLayers = value;
			}
		}

		// Token: 0x06000221 RID: 545 RVA: 0x0000C8D8 File Offset: 0x0000AAD8
		public uint GetLightLayers()
		{
			int lightlayersMask = (int)this.lightlayersMask;
			if (lightlayersMask >= 0)
			{
				return (uint)lightlayersMask;
			}
			return 255U;
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000222 RID: 546 RVA: 0x0000C8F7 File Offset: 0x0000AAF7
		// (set) Token: 0x06000223 RID: 547 RVA: 0x0000C8FF File Offset: 0x0000AAFF
		public float shadowNearPlane
		{
			get
			{
				return this.m_ShadowNearPlane;
			}
			set
			{
				if (this.m_ShadowNearPlane == value)
				{
					return;
				}
				this.m_ShadowNearPlane = Mathf.Clamp(value, HDShadowUtils.k_MinShadowNearPlane, HDShadowUtils.k_MaxShadowNearPlane);
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000224 RID: 548 RVA: 0x0000C921 File Offset: 0x0000AB21
		// (set) Token: 0x06000225 RID: 549 RVA: 0x0000C929 File Offset: 0x0000AB29
		public int blockerSampleCount
		{
			get
			{
				return this.m_BlockerSampleCount;
			}
			set
			{
				if (this.m_BlockerSampleCount == value)
				{
					return;
				}
				this.m_BlockerSampleCount = Mathf.Clamp(value, 1, 64);
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000226 RID: 550 RVA: 0x0000C944 File Offset: 0x0000AB44
		// (set) Token: 0x06000227 RID: 551 RVA: 0x0000C94C File Offset: 0x0000AB4C
		public int filterSampleCount
		{
			get
			{
				return this.m_FilterSampleCount;
			}
			set
			{
				if (this.m_FilterSampleCount == value)
				{
					return;
				}
				this.m_FilterSampleCount = Mathf.Clamp(value, 1, 64);
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000228 RID: 552 RVA: 0x0000C967 File Offset: 0x0000AB67
		// (set) Token: 0x06000229 RID: 553 RVA: 0x0000C96F File Offset: 0x0000AB6F
		public float minFilterSize
		{
			get
			{
				return this.m_MinFilterSize;
			}
			set
			{
				if (this.m_MinFilterSize == value)
				{
					return;
				}
				this.m_MinFilterSize = Mathf.Clamp(value, 0f, 1f);
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600022A RID: 554 RVA: 0x0000C991 File Offset: 0x0000AB91
		// (set) Token: 0x0600022B RID: 555 RVA: 0x0000C999 File Offset: 0x0000AB99
		public int kernelSize
		{
			get
			{
				return this.m_KernelSize;
			}
			set
			{
				if (this.m_KernelSize == value)
				{
					return;
				}
				this.m_KernelSize = Mathf.Clamp(value, 1, 32);
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600022C RID: 556 RVA: 0x0000C9B4 File Offset: 0x0000ABB4
		// (set) Token: 0x0600022D RID: 557 RVA: 0x0000C9BC File Offset: 0x0000ABBC
		public float lightAngle
		{
			get
			{
				return this.m_LightAngle;
			}
			set
			{
				if (this.m_LightAngle == value)
				{
					return;
				}
				this.m_LightAngle = Mathf.Clamp(value, 0f, 9f);
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600022E RID: 558 RVA: 0x0000C9DE File Offset: 0x0000ABDE
		// (set) Token: 0x0600022F RID: 559 RVA: 0x0000C9E6 File Offset: 0x0000ABE6
		public float maxDepthBias
		{
			get
			{
				return this.m_MaxDepthBias;
			}
			set
			{
				if (this.m_MaxDepthBias == value)
				{
					return;
				}
				this.m_MaxDepthBias = Mathf.Clamp(value, 0.0001f, 0.01f);
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000230 RID: 560 RVA: 0x0000CA08 File Offset: 0x0000AC08
		// (set) Token: 0x06000231 RID: 561 RVA: 0x0000CA15 File Offset: 0x0000AC15
		public float range
		{
			get
			{
				return this.legacyLight.range;
			}
			set
			{
				this.legacyLight.range = value;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000232 RID: 562 RVA: 0x0000CA23 File Offset: 0x0000AC23
		// (set) Token: 0x06000233 RID: 563 RVA: 0x0000CA30 File Offset: 0x0000AC30
		public Color color
		{
			get
			{
				return this.legacyLight.color;
			}
			set
			{
				this.legacyLight.color = value;
				this.UpdateAreaLightEmissiveMesh();
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000234 RID: 564 RVA: 0x0000CA44 File Offset: 0x0000AC44
		public IntScalableSettingValue shadowResolution
		{
			get
			{
				return this.m_ShadowResolution;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000235 RID: 565 RVA: 0x0000CA4C File Offset: 0x0000AC4C
		// (set) Token: 0x06000236 RID: 566 RVA: 0x0000CA54 File Offset: 0x0000AC54
		public float shadowDimmer
		{
			get
			{
				return this.m_ShadowDimmer;
			}
			set
			{
				if (this.m_ShadowDimmer == value)
				{
					return;
				}
				this.m_ShadowDimmer = Mathf.Clamp01(value);
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000237 RID: 567 RVA: 0x0000CA6C File Offset: 0x0000AC6C
		// (set) Token: 0x06000238 RID: 568 RVA: 0x0000CA82 File Offset: 0x0000AC82
		public float volumetricShadowDimmer
		{
			get
			{
				if (!this.useVolumetric)
				{
					return 0f;
				}
				return this.m_VolumetricShadowDimmer;
			}
			set
			{
				if (this.m_VolumetricShadowDimmer == value)
				{
					return;
				}
				this.m_VolumetricShadowDimmer = Mathf.Clamp01(value);
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000239 RID: 569 RVA: 0x0000CA9A File Offset: 0x0000AC9A
		// (set) Token: 0x0600023A RID: 570 RVA: 0x0000CAA2 File Offset: 0x0000ACA2
		public float shadowFadeDistance
		{
			get
			{
				return this.m_ShadowFadeDistance;
			}
			set
			{
				if (this.m_ShadowFadeDistance == value)
				{
					return;
				}
				this.m_ShadowFadeDistance = Mathf.Clamp(value, 0f, float.MaxValue);
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600023B RID: 571 RVA: 0x0000CAC4 File Offset: 0x0000ACC4
		public BoolScalableSettingValue useContactShadow
		{
			get
			{
				return this.m_UseContactShadow;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600023C RID: 572 RVA: 0x0000CACC File Offset: 0x0000ACCC
		// (set) Token: 0x0600023D RID: 573 RVA: 0x0000CAD4 File Offset: 0x0000ACD4
		public bool rayTraceContactShadow
		{
			get
			{
				return this.m_RayTracedContactShadow;
			}
			set
			{
				if (this.m_RayTracedContactShadow == value)
				{
					return;
				}
				this.m_RayTracedContactShadow = value;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600023E RID: 574 RVA: 0x0000CAE7 File Offset: 0x0000ACE7
		// (set) Token: 0x0600023F RID: 575 RVA: 0x0000CAEF File Offset: 0x0000ACEF
		public Color shadowTint
		{
			get
			{
				return this.m_ShadowTint;
			}
			set
			{
				if (this.m_ShadowTint == value)
				{
					return;
				}
				this.m_ShadowTint = value;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000240 RID: 576 RVA: 0x0000CB07 File Offset: 0x0000AD07
		// (set) Token: 0x06000241 RID: 577 RVA: 0x0000CB0F File Offset: 0x0000AD0F
		public bool penumbraTint
		{
			get
			{
				return this.m_PenumbraTint;
			}
			set
			{
				if (this.m_PenumbraTint == value)
				{
					return;
				}
				this.m_PenumbraTint = value;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000242 RID: 578 RVA: 0x0000CB22 File Offset: 0x0000AD22
		// (set) Token: 0x06000243 RID: 579 RVA: 0x0000CB2A File Offset: 0x0000AD2A
		public float normalBias
		{
			get
			{
				return this.m_NormalBias;
			}
			set
			{
				if (this.m_NormalBias == value)
				{
					return;
				}
				this.m_NormalBias = value;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000244 RID: 580 RVA: 0x0000CB3D File Offset: 0x0000AD3D
		// (set) Token: 0x06000245 RID: 581 RVA: 0x0000CB45 File Offset: 0x0000AD45
		public float slopeBias
		{
			get
			{
				return this.m_SlopeBias;
			}
			set
			{
				if (this.m_SlopeBias == value)
				{
					return;
				}
				this.m_SlopeBias = value;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000246 RID: 582 RVA: 0x0000CB58 File Offset: 0x0000AD58
		// (set) Token: 0x06000247 RID: 583 RVA: 0x0000CB60 File Offset: 0x0000AD60
		public ShadowUpdateMode shadowUpdateMode
		{
			get
			{
				return this.m_ShadowUpdateMode;
			}
			set
			{
				if (this.m_ShadowUpdateMode == value)
				{
					return;
				}
				this.m_ShadowUpdateMode = value;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000248 RID: 584 RVA: 0x0000CB73 File Offset: 0x0000AD73
		// (set) Token: 0x06000249 RID: 585 RVA: 0x0000CB7B File Offset: 0x0000AD7B
		public float barnDoorAngle
		{
			get
			{
				return this.m_BarnDoorAngle;
			}
			set
			{
				if (this.m_BarnDoorAngle == value)
				{
					return;
				}
				this.m_BarnDoorAngle = Mathf.Clamp(value, 0f, 90f);
				this.UpdateAllLightValues();
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600024A RID: 586 RVA: 0x0000CBA3 File Offset: 0x0000ADA3
		// (set) Token: 0x0600024B RID: 587 RVA: 0x0000CBAB File Offset: 0x0000ADAB
		public float barnDoorLength
		{
			get
			{
				return this.m_BarnDoorLength;
			}
			set
			{
				if (this.m_BarnDoorLength == value)
				{
					return;
				}
				this.m_BarnDoorLength = Mathf.Clamp(value, 0f, float.MaxValue);
				this.UpdateAllLightValues();
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600024C RID: 588 RVA: 0x0000CBD3 File Offset: 0x0000ADD3
		// (set) Token: 0x0600024D RID: 589 RVA: 0x0000CBDB File Offset: 0x0000ADDB
		public bool affectsVolumetric
		{
			get
			{
				return this.useVolumetric;
			}
			set
			{
				this.useVolumetric = value;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600024E RID: 590 RVA: 0x0000CBE4 File Offset: 0x0000ADE4
		// (set) Token: 0x0600024F RID: 591 RVA: 0x0000CBEC File Offset: 0x0000ADEC
		internal float[] shadowCascadeRatios
		{
			get
			{
				return this.m_ShadowCascadeRatios;
			}
			set
			{
				this.m_ShadowCascadeRatios = value;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000250 RID: 592 RVA: 0x0000CBF5 File Offset: 0x0000ADF5
		// (set) Token: 0x06000251 RID: 593 RVA: 0x0000CBFD File Offset: 0x0000ADFD
		internal float[] shadowCascadeBorders
		{
			get
			{
				return this.m_ShadowCascadeBorders;
			}
			set
			{
				this.m_ShadowCascadeBorders = value;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000252 RID: 594 RVA: 0x0000CC06 File Offset: 0x0000AE06
		// (set) Token: 0x06000253 RID: 595 RVA: 0x0000CC0E File Offset: 0x0000AE0E
		internal int shadowAlgorithm
		{
			get
			{
				return this.m_ShadowAlgorithm;
			}
			set
			{
				this.m_ShadowAlgorithm = value;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000254 RID: 596 RVA: 0x0000CC17 File Offset: 0x0000AE17
		// (set) Token: 0x06000255 RID: 597 RVA: 0x0000CC1F File Offset: 0x0000AE1F
		internal int shadowVariant
		{
			get
			{
				return this.m_ShadowVariant;
			}
			set
			{
				this.m_ShadowVariant = value;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000256 RID: 598 RVA: 0x0000CC28 File Offset: 0x0000AE28
		// (set) Token: 0x06000257 RID: 599 RVA: 0x0000CC30 File Offset: 0x0000AE30
		internal int shadowPrecision
		{
			get
			{
				return this.m_ShadowPrecision;
			}
			set
			{
				this.m_ShadowPrecision = value;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000258 RID: 600 RVA: 0x0000CC39 File Offset: 0x0000AE39
		internal Light legacyLight
		{
			get
			{
				base.TryGetComponent<Light>(out this.m_Light);
				return this.m_Light;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000259 RID: 601 RVA: 0x0000CC4E File Offset: 0x0000AE4E
		internal MeshRenderer emissiveMeshRenderer
		{
			get
			{
				if (this.m_EmissiveMeshRenderer == null)
				{
					base.TryGetComponent<MeshRenderer>(out this.m_EmissiveMeshRenderer);
				}
				return this.m_EmissiveMeshRenderer;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600025A RID: 602 RVA: 0x0000CC71 File Offset: 0x0000AE71
		internal MeshFilter emissiveMeshFilter
		{
			get
			{
				if (this.m_EmissiveMeshFilter == null)
				{
					base.TryGetComponent<MeshFilter>(out this.m_EmissiveMeshFilter);
				}
				return this.m_EmissiveMeshFilter;
			}
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0000CC94 File Offset: 0x0000AE94
		private void DisableCachedShadowSlot()
		{
			if (this.WillRenderShadowMap() && !this.ShadowIsUpdatedEveryFrame())
			{
				HDShadowManager.instance.MarkCachedShadowSlotsAsEmpty(this.shadowMapType, base.GetInstanceID());
				HDShadowManager.instance.PruneEmptyCachedSlots(this.shadowMapType);
				this.m_ShadowMapRenderedSinceLastRequest = false;
			}
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0000CCD3 File Offset: 0x0000AED3
		private void OnDestroy()
		{
			this.DisableCachedShadowSlot();
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0000CCDB File Offset: 0x0000AEDB
		private void OnDisable()
		{
			this.DisableCachedShadowSlot();
			this.SetEmissiveMeshRendererEnabled(false);
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0000CCEA File Offset: 0x0000AEEA
		private void SetEmissiveMeshRendererEnabled(bool enabled)
		{
			if (this.displayAreaLightEmissiveMesh && this.emissiveMeshRenderer)
			{
				this.emissiveMeshRenderer.enabled = enabled;
			}
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0000CD10 File Offset: 0x0000AF10
		private int GetShadowRequestCount(HDShadowSettings shadowSettings)
		{
			HDLightType type = this.type;
			if (type == HDLightType.Point)
			{
				return 6;
			}
			if (type != HDLightType.Directional)
			{
				return 1;
			}
			return shadowSettings.cascadeShadowSplitCount.value;
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0000CD3B File Offset: 0x0000AF3B
		public void RequestShadowMapRendering()
		{
			if (this.shadowUpdateMode == ShadowUpdateMode.OnDemand)
			{
				this.m_ShadowMapRenderedSinceLastRequest = false;
			}
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0000CD50 File Offset: 0x0000AF50
		internal bool ShouldRenderShadows()
		{
			switch (this.shadowUpdateMode)
			{
			case ShadowUpdateMode.EveryFrame:
				return true;
			case ShadowUpdateMode.OnEnable:
				return !this.m_ShadowMapRenderedSinceLastRequest;
			case ShadowUpdateMode.OnDemand:
				return !this.m_ShadowMapRenderedSinceLastRequest;
			default:
				return true;
			}
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0000CD8F File Offset: 0x0000AF8F
		internal bool ShadowIsUpdatedEveryFrame()
		{
			return this.shadowUpdateMode == ShadowUpdateMode.EveryFrame;
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0000CD9C File Offset: 0x0000AF9C
		internal void EvaluateShadowState(HDCamera hdCamera, in ProcessedLightData processedLight, CullingResults cullResults, FrameSettings frameSettings, int lightIndex)
		{
			this.m_WillRenderShadowMap = this.legacyLight.shadows != LightShadows.None && frameSettings.IsEnabled(FrameSettingsField.ShadowMaps);
			Bounds bounds;
			this.m_WillRenderShadowMap &= cullResults.GetShadowCasterBounds(lightIndex, out bounds);
			this.m_WillRenderShadowMap &= this.shadowDimmer > 0f;
			this.m_WillRenderShadowMap &= this.type == HDLightType.Directional || processedLight.distanceToCamera < this.shadowFadeDistance;
			this.m_WillRenderScreenSpaceShadow = false;
			this.m_WillRenderRayTracedShadow = false;
			if (!hdCamera.frameSettings.IsEnabled(FrameSettingsField.ScreenSpaceShadows) || !this.m_WillRenderShadowMap)
			{
				return;
			}
			if (frameSettings.IsEnabled(FrameSettingsField.RayTracing) && this.m_UseRayTracedShadows)
			{
				bool flag = false;
				if (processedLight.gpuLightType == GPULightType.Rectangle && hdCamera.frameSettings.litShaderMode == LitShaderMode.Deferred)
				{
					flag = true;
				}
				else if (processedLight.gpuLightType == GPULightType.Point || (processedLight.gpuLightType == GPULightType.Spot && processedLight.lightVolumeType == LightVolumeType.Cone))
				{
					flag = true;
				}
				if (flag)
				{
					this.m_WillRenderScreenSpaceShadow = true;
					this.m_WillRenderRayTracedShadow = true;
				}
			}
			if (this.useScreenSpaceShadows && processedLight.gpuLightType == GPULightType.Directional)
			{
				this.m_WillRenderScreenSpaceShadow = true;
				if (frameSettings.IsEnabled(FrameSettingsField.RayTracing) && this.m_UseRayTracedShadows)
				{
					this.m_WillRenderRayTracedShadow = true;
				}
			}
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0000CEDC File Offset: 0x0000B0DC
		private int GetResolutionFromSettings(ShadowMapType shadowMapType, HDShadowInitParameters initParameters)
		{
			switch (shadowMapType)
			{
			case ShadowMapType.CascadedDirectional:
				return Math.Min(this.m_ShadowResolution.Value(initParameters.shadowResolutionDirectional), initParameters.maxDirectionalShadowMapResolution);
			case ShadowMapType.PunctualAtlas:
				return Math.Min(this.m_ShadowResolution.Value(initParameters.shadowResolutionPunctual), initParameters.maxPunctualShadowMapResolution);
			case ShadowMapType.AreaLightAtlas:
				return Math.Min(this.m_ShadowResolution.Value(initParameters.shadowResolutionArea), initParameters.maxAreaShadowMapResolution);
			default:
				return 0;
			}
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0000CF58 File Offset: 0x0000B158
		internal void ReserveShadowMap(Camera camera, HDShadowManager shadowManager, HDShadowSettings shadowSettings, HDShadowInitParameters initParameters, Rect screenRect)
		{
			if (!this.m_WillRenderShadowMap)
			{
				return;
			}
			if (this.shadowRequests == null || this.m_ShadowRequestIndices == null)
			{
				this.shadowRequests = new HDShadowRequest[6];
				this.m_ShadowRequestIndices = new int[6];
				for (int i = 0; i < 6; i++)
				{
					this.shadowRequests[i] = new HDShadowRequest();
				}
			}
			int resolutionFromSettings = this.GetResolutionFromSettings(this.shadowMapType, initParameters);
			Vector2 vector = new Vector2((float)resolutionFromSettings, (float)resolutionFromSettings);
			bool flag = false | (this.shadowMapType == ShadowMapType.PunctualAtlas && initParameters.punctualLightShadowAtlas.useDynamicViewportRescale) | (this.shadowMapType == ShadowMapType.AreaLightAtlas && initParameters.areaLightShadowAtlas.useDynamicViewportRescale);
			bool flag2 = !this.ShouldRenderShadows();
			if (flag2)
			{
				vector = this.m_CachedShadowResolution;
			}
			else
			{
				this.m_CachedShadowResolution = vector;
			}
			if (flag && !flag2)
			{
				float num = screenRect.width * screenRect.height;
				vector *= Mathf.Lerp(64f / vector.x, 1f, num);
				vector = Vector2.Max(new Vector2(64f, 64f) / vector, vector);
				vector.x = Mathf.Round(vector.x);
				vector.y = Mathf.Round(vector.y);
			}
			vector = Vector2.Max(vector, new Vector2(16f, 16f));
			if (this.type == HDLightType.Directional)
			{
				shadowManager.UpdateDirectionalShadowResolution((int)vector.x, shadowSettings.cascadeShadowSplitCount.value);
			}
			int shadowRequestCount = this.GetShadowRequestCount(shadowSettings);
			bool flag3 = flag2 && !this.ShadowIsUpdatedEveryFrame() && this.type != HDLightType.Directional;
			for (int j = 0; j < shadowRequestCount; j++)
			{
				this.m_ShadowRequestIndices[j] = shadowManager.ReserveShadowResolutions(flag3 ? new Vector2((float)resolutionFromSettings, (float)resolutionFromSettings) : vector, this.shadowMapType, base.GetInstanceID(), j, flag3, out this.m_CachedResolutionRequestIndices[j]);
			}
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0000D13D File Offset: 0x0000B33D
		internal bool WillRenderShadowMap()
		{
			return this.m_WillRenderShadowMap;
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0000D145 File Offset: 0x0000B345
		internal bool WillRenderScreenSpaceShadow()
		{
			return this.m_WillRenderScreenSpaceShadow;
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0000D14D File Offset: 0x0000B34D
		internal bool WillRenderRayTracedShadow()
		{
			return this.m_WillRenderRayTracedShadow;
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0000D158 File Offset: 0x0000B358
		internal static float GetAreaLightOffsetForShadows(Vector2 shapeSize, float coneAngle)
		{
			float magnitude = shapeSize.magnitude;
			float num = coneAngle * 0.5f;
			float num2 = 1f / Mathf.Tan(num * 0.017453292f);
			return -(magnitude * num2);
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0000D18C File Offset: 0x0000B38C
		private void UpdateDirectionalShadowRequest(HDShadowManager manager, HDShadowSettings shadowSettings, VisibleLight visibleLight, CullingResults cullResults, Vector2 viewportSize, int requestIndex, int lightIndex, Vector3 cameraPos, HDShadowRequest shadowRequest, out Matrix4x4 invViewProjection)
		{
			float shadowNearPlaneOffset = QualitySettings.shadowNearPlaneOffset;
			HDShadowUtils.ExtractDirectionalLightData(visibleLight, viewportSize, (uint)requestIndex, shadowSettings.cascadeShadowSplitCount.value, shadowSettings.cascadeShadowSplits, shadowNearPlaneOffset, cullResults, lightIndex, out shadowRequest.view, out invViewProjection, out shadowRequest.deviceProjectionYFlip, out shadowRequest.deviceProjection, out shadowRequest.splitData);
			Vector4 cullingSphere = shadowRequest.splitData.cullingSphere;
			if (ShaderConfig.s_CameraRelativeRendering != 0)
			{
				cullingSphere.x -= cameraPos.x;
				cullingSphere.y -= cameraPos.y;
				cullingSphere.z -= cameraPos.z;
			}
			manager.UpdateCascade(requestIndex, cullingSphere, shadowSettings.cascadeShadowBorders[requestIndex]);
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0000D23C File Offset: 0x0000B43C
		internal int UpdateShadowRequest(HDCamera hdCamera, HDShadowManager manager, HDShadowSettings shadowSettings, VisibleLight visibleLight, CullingResults cullResults, int lightIndex, LightingDebugSettings lightingDebugSettings, out int shadowRequestCount)
		{
			int num = -1;
			Vector3 worldSpaceCameraPos = hdCamera.mainViewConstants.worldSpaceCameraPos;
			shadowRequestCount = 0;
			int shadowRequestCount2 = this.GetShadowRequestCount(shadowSettings);
			bool flag = !this.ShouldRenderShadows() && !lightingDebugSettings.clearShadowAtlas;
			this.ShadowIsUpdatedEveryFrame();
			bool flag2 = !this.ShadowIsUpdatedEveryFrame() && this.legacyLight.type != LightType.Directional;
			bool flag3 = flag && flag2 && !manager.AtlasHasResized(this.shadowMapType);
			bool flag4 = (flag && this.m_CachedDataIsValid && manager.GetAtlasShapeID(this.shadowMapType) == this.m_AtlasShapeID && manager.CachedDataIsValid(this.shadowMapType)) || this.legacyLight.type == LightType.Directional;
			flag = flag && ((flag2 && flag4) || this.legacyLight.type == LightType.Directional);
			for (int i = 0; i < shadowRequestCount2; i++)
			{
				HDShadowRequest hdshadowRequest = this.shadowRequests[i];
				Matrix4x4 identity = Matrix4x4.identity;
				int num2 = this.m_ShadowRequestIndices[i];
				HDShadowResolutionRequest resolutionRequest = manager.GetResolutionRequest(this.shadowMapType, flag3, flag3 ? this.m_CachedResolutionRequestIndices[i] : num2);
				if (resolutionRequest != null)
				{
					Vector2 resolution = resolutionRequest.resolution;
					HDLightType type = this.type;
					if (num2 != -1)
					{
						if (flag)
						{
							hdshadowRequest.cachedShadowData.cacheTranslationDelta = worldSpaceCameraPos - this.m_CachedViewPos;
							hdshadowRequest.shouldUseCachedShadow = true;
							if (type == HDLightType.Directional)
							{
								this.UpdateDirectionalShadowRequest(manager, shadowSettings, visibleLight, cullResults, resolution, i, lightIndex, worldSpaceCameraPos, hdshadowRequest, out identity);
							}
						}
						else
						{
							this.m_CachedViewPos = worldSpaceCameraPos;
							hdshadowRequest.shouldUseCachedShadow = false;
							this.m_ShadowMapRenderedSinceLastRequest = true;
							switch (type)
							{
							case HDLightType.Spot:
							{
								float num3 = (this.useCustomSpotLightShadowCone ? Math.Min(this.customSpotLightShadowCone, visibleLight.light.spotAngle) : visibleLight.light.spotAngle);
								HDShadowUtils.ExtractSpotLightData(this.spotLightShape, num3, this.shadowNearPlane, this.aspectRatio, this.shapeWidth, this.shapeHeight, visibleLight, resolution, this.normalBias, out hdshadowRequest.view, out identity, out hdshadowRequest.deviceProjectionYFlip, out hdshadowRequest.deviceProjection, out hdshadowRequest.splitData);
								break;
							}
							case HDLightType.Directional:
								this.UpdateDirectionalShadowRequest(manager, shadowSettings, visibleLight, cullResults, resolution, i, lightIndex, worldSpaceCameraPos, hdshadowRequest, out identity);
								break;
							case HDLightType.Point:
								HDShadowUtils.ExtractPointLightData(visibleLight, resolution, this.shadowNearPlane, this.normalBias, (uint)i, out hdshadowRequest.view, out identity, out hdshadowRequest.deviceProjectionYFlip, out hdshadowRequest.deviceProjection, out hdshadowRequest.splitData);
								break;
							case HDLightType.Area:
							{
								AreaLightShape areaLightShape = this.areaLightShape;
								if (areaLightShape != AreaLightShape.Rectangle)
								{
									if (areaLightShape != AreaLightShape.Tube)
									{
									}
								}
								else
								{
									Vector2 vector = new Vector2(this.shapeWidth, this.m_ShapeHeight);
									Vector3 vector2 = HDAdditionalLightData.GetAreaLightOffsetForShadows(vector, this.areaLightShadowCone) * visibleLight.GetForward();
									HDShadowUtils.ExtractRectangleAreaLightData(visibleLight, visibleLight.GetPosition() + vector2, this.areaLightShadowCone, this.shadowNearPlane, vector, resolution, this.normalBias, out hdshadowRequest.view, out identity, out hdshadowRequest.deviceProjectionYFlip, out hdshadowRequest.deviceProjection, out hdshadowRequest.splitData);
								}
								break;
							}
							}
							this.SetCommonShadowRequestSettings(hdshadowRequest, worldSpaceCameraPos, identity, hdshadowRequest.deviceProjectionYFlip * hdshadowRequest.view, resolution, lightIndex);
						}
						hdshadowRequest.atlasViewport = resolutionRequest.atlasViewport;
						manager.UpdateShadowRequest(num2, hdshadowRequest);
						hdshadowRequest.shouldUseCachedShadow = hdshadowRequest.shouldUseCachedShadow && flag4;
						this.m_CachedDataIsValid = manager.CachedDataIsValid(this.shadowMapType);
						this.m_AtlasShapeID = manager.GetAtlasShapeID(this.shadowMapType);
						if (num == -1)
						{
							num = num2;
						}
						shadowRequestCount++;
					}
				}
			}
			return num;
		}

		// Token: 0x0600026C RID: 620 RVA: 0x0000D5EC File Offset: 0x0000B7EC
		private void SetCommonShadowRequestSettings(HDShadowRequest shadowRequest, Vector3 cameraPos, Matrix4x4 invViewProjection, Matrix4x4 viewProjection, Vector2 viewportSize, int lightIndex)
		{
			float range = this.legacyLight.range;
			float shadowNearPlane = this.shadowNearPlane;
			shadowRequest.zBufferParam = new Vector4((range - shadowNearPlane) / shadowNearPlane, 1f, (range - shadowNearPlane) / (shadowNearPlane * range), 1f / range);
			shadowRequest.worldTexelSize = 2f / shadowRequest.deviceProjectionYFlip.m00 / viewportSize.x * Mathf.Sqrt(2f);
			shadowRequest.normalBias = this.normalBias;
			if (ShaderConfig.s_CameraRelativeRendering != 0)
			{
				Matrix4x4 matrix4x = Matrix4x4.Translate(cameraPos);
				shadowRequest.view *= matrix4x;
				matrix4x.SetColumn(3, -cameraPos);
				matrix4x[15] = 1f;
				invViewProjection = matrix4x * invViewProjection;
			}
			HDLightType type = this.type;
			if (type == HDLightType.Directional || (type == HDLightType.Spot && this.spotLightShape == SpotLightShape.Box))
			{
				shadowRequest.position = new Vector3(shadowRequest.view.m03, shadowRequest.view.m13, shadowRequest.view.m23);
			}
			else
			{
				shadowRequest.position = ((ShaderConfig.s_CameraRelativeRendering != 0) ? (base.transform.position - cameraPos) : base.transform.position);
			}
			shadowRequest.shadowToWorld = invViewProjection.transpose;
			shadowRequest.zClip = type != HDLightType.Directional;
			shadowRequest.lightIndex = lightIndex;
			if (type == HDLightType.Directional)
			{
				shadowRequest.shadowMapType = ShadowMapType.CascadedDirectional;
			}
			else if (type == HDLightType.Area && this.areaLightShape == AreaLightShape.Rectangle)
			{
				shadowRequest.shadowMapType = ShadowMapType.AreaLightAtlas;
			}
			else
			{
				shadowRequest.shadowMapType = ShadowMapType.PunctualAtlas;
			}
			GeometryUtility.CalculateFrustumPlanes(viewProjection, this.m_ShadowFrustumPlanes);
			Vector4[] frustumPlanes = shadowRequest.frustumPlanes;
			if (frustumPlanes == null || frustumPlanes.Length != 6)
			{
				shadowRequest.frustumPlanes = new Vector4[6];
			}
			for (int i = 0; i < 6; i++)
			{
				shadowRequest.frustumPlanes[i] = new Vector4(this.m_ShadowFrustumPlanes[i].normal.x, this.m_ShadowFrustumPlanes[i].normal.y, this.m_ShadowFrustumPlanes[i].normal.z, this.m_ShadowFrustumPlanes[i].distance);
			}
			float num2;
			if (type == HDLightType.Directional)
			{
				Matrix4x4 deviceProjection = shadowRequest.deviceProjection;
				float num = Vector4.Dot(new Vector4(deviceProjection.m32, -deviceProjection.m32, -deviceProjection.m22, deviceProjection.m22), new Vector4(deviceProjection.m22, deviceProjection.m32, deviceProjection.m23, deviceProjection.m33)) / (deviceProjection.m22 * (deviceProjection.m22 - deviceProjection.m32));
				num2 = Mathf.Abs(Mathf.Tan(0.008726646f * (this.softnessScale * this.m_AngularDiameter) / 2f) * num / (2f * shadowRequest.splitData.cullingSphere.w));
				float num3 = Mathf.Abs(2f * (1f / deviceProjection.m22)) / 100f;
				shadowRequest.zBufferParam.x = num3;
			}
			else
			{
				float num4 = this.m_ShapeRadius * this.softnessScale;
				float num5 = num4 * num4;
				num2 = 0.02403461f + 3.452916f * num4 - 1.362672f * num5 + 0.6700115f * num5 * num4 + 0.2159474f * num5 * num5;
				num2 /= 100f;
			}
			num2 *= shadowRequest.atlasViewport.width / 512f;
			float num6 = 5f;
			if (HDRenderPipeline.currentAsset.currentPlatformRenderPipelineSettings.hdShadowInitParams.shadowFilteringQuality == HDShadowFilteringQuality.High && num2 > 0.01f)
			{
				float num7 = 18f;
				num6 = Mathf.Lerp(num6, num7, Mathf.Min(1f, num2 * 100f / 5f));
			}
			shadowRequest.slopeBias = HDShadowUtils.GetSlopeBias(num6, this.slopeBias);
			shadowRequest.shadowSoftness = num2;
			shadowRequest.blockerSampleCount = this.blockerSampleCount;
			shadowRequest.filterSampleCount = this.filterSampleCount;
			shadowRequest.minFilterSize = this.minFilterSize * 0.001f;
			shadowRequest.kernelSize = this.kernelSize;
			shadowRequest.lightAngle = this.lightAngle * 3.1415927f / 180f;
			shadowRequest.maxDepthBias = this.maxDepthBias;
			shadowRequest.evsmParams.x = this.evsmExponent * 1.442695f;
			shadowRequest.evsmParams.y = this.evsmLightLeakBias;
			shadowRequest.evsmParams.z = this.m_EvsmVarianceBias;
			shadowRequest.evsmParams.w = (float)this.evsmBlurPasses;
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600026D RID: 621 RVA: 0x0000DA6D File Offset: 0x0000BC6D
		// (set) Token: 0x0600026E RID: 622 RVA: 0x0000DA7A File Offset: 0x0000BC7A
		internal bool useColorTemperature
		{
			get
			{
				return this.legacyLight.useColorTemperature;
			}
			set
			{
				if (this.legacyLight.useColorTemperature == value)
				{
					return;
				}
				this.legacyLight.useColorTemperature = value;
			}
		}

		// Token: 0x0600026F RID: 623 RVA: 0x0000DA97 File Offset: 0x0000BC97
		private void Start()
		{
			this.m_Animated = base.GetComponent<Animator>() != null;
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0000DAAC File Offset: 0x0000BCAC
		private void LateUpdate()
		{
			if (!this.m_Animated)
			{
				return;
			}
			new Vector3(this.shapeWidth, this.m_ShapeHeight, this.shapeRadius);
			if (this.legacyLight.enabled != this.timelineWorkaround.lightEnabled)
			{
				this.SetEmissiveMeshRendererEnabled(this.legacyLight.enabled);
				this.timelineWorkaround.lightEnabled = this.legacyLight.enabled;
			}
			if (this.timelineWorkaround.oldLossyScale != base.transform.lossyScale || this.intensity != this.timelineWorkaround.oldIntensity || this.legacyLight.colorTemperature != this.timelineWorkaround.oldLightColorTemperature)
			{
				this.UpdateLightIntensity();
				this.UpdateAreaLightEmissiveMesh();
				this.timelineWorkaround.oldLossyScale = base.transform.lossyScale;
				this.timelineWorkaround.oldIntensity = this.intensity;
				this.timelineWorkaround.oldLightColorTemperature = this.legacyLight.colorTemperature;
			}
			if (this.type == HDLightType.Spot && this.timelineWorkaround.oldSpotAngle != this.legacyLight.spotAngle)
			{
				this.UpdateLightIntensity();
				this.timelineWorkaround.oldSpotAngle = this.legacyLight.spotAngle;
			}
			if (this.legacyLight.color != this.timelineWorkaround.oldLightColor || this.timelineWorkaround.oldLossyScale != base.transform.lossyScale || this.displayAreaLightEmissiveMesh != this.timelineWorkaround.oldDisplayAreaLightEmissiveMesh || this.legacyLight.colorTemperature != this.timelineWorkaround.oldLightColorTemperature)
			{
				this.UpdateAreaLightEmissiveMesh();
				this.timelineWorkaround.oldLightColor = this.legacyLight.color;
				this.timelineWorkaround.oldLossyScale = base.transform.lossyScale;
				this.timelineWorkaround.oldDisplayAreaLightEmissiveMesh = this.displayAreaLightEmissiveMesh;
				this.timelineWorkaround.oldLightColorTemperature = this.legacyLight.colorTemperature;
			}
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0000DCA4 File Offset: 0x0000BEA4
		private void OnDidApplyAnimationProperties()
		{
			this.UpdateAllLightValues();
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0000DCAC File Offset: 0x0000BEAC
		public void CopyTo(HDAdditionalLightData data)
		{
			data.enableSpotReflector = this.enableSpotReflector;
			data.luxAtDistance = this.luxAtDistance;
			data.m_InnerSpotPercent = this.m_InnerSpotPercent;
			data.lightDimmer = this.lightDimmer;
			data.volumetricDimmer = this.volumetricDimmer;
			data.lightUnit = this.lightUnit;
			data.m_FadeDistance = this.m_FadeDistance;
			data.affectDiffuse = this.affectDiffuse;
			data.m_AffectSpecular = this.m_AffectSpecular;
			data.nonLightmappedOnly = this.nonLightmappedOnly;
			data.m_PointlightHDType = this.m_PointlightHDType;
			data.spotLightShape = this.spotLightShape;
			data.shapeWidth = this.shapeWidth;
			data.m_ShapeHeight = this.m_ShapeHeight;
			data.aspectRatio = this.aspectRatio;
			data.shapeRadius = this.shapeRadius;
			data.m_MaxSmoothness = this.maxSmoothness;
			data.m_ApplyRangeAttenuation = this.m_ApplyRangeAttenuation;
			data.useOldInspector = this.useOldInspector;
			data.featuresFoldout = this.featuresFoldout;
			data.showAdditionalSettings = this.showAdditionalSettings;
			data.m_Intensity = this.m_Intensity;
			data.displayAreaLightEmissiveMesh = this.displayAreaLightEmissiveMesh;
			data.interactsWithSky = this.interactsWithSky;
			data.angularDiameter = this.angularDiameter;
			data.flareSize = this.flareSize;
			data.flareTint = this.flareTint;
			data.surfaceTexture = this.surfaceTexture;
			data.surfaceTint = this.surfaceTint;
			data.distance = this.distance;
			this.shadowResolution.CopyTo(data.shadowResolution);
			data.shadowDimmer = this.shadowDimmer;
			data.volumetricShadowDimmer = this.volumetricShadowDimmer;
			data.shadowFadeDistance = this.shadowFadeDistance;
			this.useContactShadow.CopyTo(data.useContactShadow);
			data.slopeBias = this.slopeBias;
			data.normalBias = this.normalBias;
			data.shadowCascadeRatios = new float[this.shadowCascadeRatios.Length];
			this.shadowCascadeRatios.CopyTo(data.shadowCascadeRatios, 0);
			data.shadowCascadeBorders = new float[this.shadowCascadeBorders.Length];
			this.shadowCascadeBorders.CopyTo(data.shadowCascadeBorders, 0);
			data.shadowAlgorithm = this.shadowAlgorithm;
			data.shadowVariant = this.shadowVariant;
			data.shadowPrecision = this.shadowPrecision;
			data.shadowUpdateMode = this.shadowUpdateMode;
			data.m_UseCustomSpotLightShadowCone = this.useCustomSpotLightShadowCone;
			data.m_CustomSpotLightShadowCone = this.customSpotLightShadowCone;
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0000DF14 File Offset: 0x0000C114
		public static void InitDefaultHDAdditionalLightData(HDAdditionalLightData lightData)
		{
			Light component = lightData.gameObject.GetComponent<Light>();
			switch (lightData.type)
			{
			case HDLightType.Spot:
			case HDLightType.Point:
				lightData.lightUnit = LightUnit.Lumen;
				lightData.intensity = 600f;
				break;
			case HDLightType.Directional:
				lightData.lightUnit = LightUnit.Lux;
				lightData.intensity = 3.1415927f;
				break;
			case HDLightType.Area:
			{
				AreaLightShape areaLightShape = lightData.areaLightShape;
				if (areaLightShape != AreaLightShape.Rectangle)
				{
					if (areaLightShape != AreaLightShape.Disc)
					{
					}
				}
				else
				{
					lightData.lightUnit = LightUnit.Lumen;
					lightData.intensity = 200f;
					component.shadows = LightShadows.None;
				}
				break;
			}
			}
			component.lightShadowCasterMode = LightShadowCasterMode.Everything;
			lightData.normalBias = 0.75f;
			lightData.slopeBias = 0.5f;
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0000DFBA File Offset: 0x0000C1BA
		private void OnValidate()
		{
			this.UpdateBounds();
			this.DisableCachedShadowSlot();
			this.m_ShadowMapRenderedSinceLastRequest = false;
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0000DFD0 File Offset: 0x0000C1D0
		private void SetLightIntensityPunctual(float intensity)
		{
			switch (this.type)
			{
			case HDLightType.Spot:
				if (this.lightUnit == LightUnit.Candela)
				{
					this.legacyLight.intensity = intensity;
					return;
				}
				if (!this.enableSpotReflector)
				{
					this.legacyLight.intensity = LightUtils.ConvertPointLightLumenToCandela(intensity);
					return;
				}
				if (this.spotLightShape == SpotLightShape.Cone)
				{
					this.legacyLight.intensity = LightUtils.ConvertSpotLightLumenToCandela(intensity, this.legacyLight.spotAngle * 0.017453292f, true);
					return;
				}
				if (this.spotLightShape == SpotLightShape.Pyramid)
				{
					float num;
					float num2;
					LightUtils.CalculateAnglesForPyramid(this.aspectRatio, this.legacyLight.spotAngle * 0.017453292f, out num, out num2);
					this.legacyLight.intensity = LightUtils.ConvertFrustrumLightLumenToCandela(intensity, num, num2);
					return;
				}
				this.legacyLight.intensity = LightUtils.ConvertPointLightLumenToCandela(intensity);
				return;
			case HDLightType.Directional:
				this.legacyLight.intensity = intensity;
				return;
			case HDLightType.Point:
				if (this.lightUnit == LightUnit.Candela)
				{
					this.legacyLight.intensity = intensity;
					return;
				}
				this.legacyLight.intensity = LightUtils.ConvertPointLightLumenToCandela(intensity);
				return;
			default:
				return;
			}
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0000E0D8 File Offset: 0x0000C2D8
		private void UpdateLightIntensity()
		{
			if (this.lightUnit == LightUnit.Lumen)
			{
				if (this.m_PointlightHDType == HDAdditionalLightData.PointLightHDType.Punctual)
				{
					this.SetLightIntensityPunctual(this.intensity);
					return;
				}
				this.legacyLight.intensity = LightUtils.ConvertAreaLightLumenToLuminance(this.areaLightShape, this.intensity, this.shapeWidth, this.m_ShapeHeight);
				return;
			}
			else
			{
				if (this.lightUnit == LightUnit.Ev100)
				{
					this.legacyLight.intensity = LightUtils.ConvertEvToLuminance(this.m_Intensity);
					return;
				}
				HDLightType type = this.type;
				if ((type != HDLightType.Spot && type != HDLightType.Point) || this.lightUnit != LightUnit.Lux)
				{
					this.legacyLight.intensity = this.m_Intensity;
					return;
				}
				if (type == HDLightType.Spot && this.spotLightShape == SpotLightShape.Box)
				{
					this.legacyLight.intensity = this.m_Intensity;
					return;
				}
				this.legacyLight.intensity = LightUtils.ConvertLuxToCandela(this.m_Intensity, this.luxAtDistance);
				return;
			}
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000E1B0 File Offset: 0x0000C3B0
		internal void UpdateAreaLightEmissiveMesh()
		{
			if (this.type == HDLightType.Area && this.displayAreaLightEmissiveMesh)
			{
				if (this.emissiveMeshRenderer == null)
				{
					this.m_EmissiveMeshRenderer = base.gameObject.AddComponent<MeshRenderer>();
				}
				if (this.emissiveMeshFilter == null)
				{
					this.m_EmissiveMeshFilter = base.gameObject.AddComponent<MeshFilter>();
				}
				Vector3 vector;
				if (this.timelineWorkaround.oldLossyScale != base.transform.lossyScale)
				{
					vector = base.transform.lossyScale;
				}
				else
				{
					vector = new Vector3(this.m_ShapeWidth, this.m_ShapeHeight, base.transform.localScale.z);
				}
				if (this.areaLightShape == AreaLightShape.Tube)
				{
					vector.y = 0.01f;
				}
				vector.z = 0.01f;
				vector = Vector3.Max(Vector3.one * 0.01f, vector);
				Vector3 vector2 = vector;
				if (base.transform.parent != null)
				{
					vector2 = new Vector3(vector.x / base.transform.parent.lossyScale.x, vector.y / base.transform.parent.lossyScale.y, vector.z / base.transform.parent.lossyScale.z);
				}
				this.legacyLight.transform.localScale = vector2;
				AreaLightShape areaLightShape = this.areaLightShape;
				if (areaLightShape != AreaLightShape.Rectangle)
				{
					if (areaLightShape == AreaLightShape.Tube)
					{
						this.m_ShapeWidth = vector.x;
					}
				}
				else
				{
					this.m_ShapeWidth = vector.x;
					this.m_ShapeHeight = vector.y;
				}
				if (this.emissiveMeshRenderer.sharedMaterial == null || this.emissiveMeshRenderer.sharedMaterial.name != base.gameObject.name)
				{
					this.emissiveMeshRenderer.sharedMaterial = new Material(Shader.Find("HDRP/Unlit"));
					this.emissiveMeshRenderer.sharedMaterial.SetFloat("_IncludeIndirectLighting", 0f);
					this.emissiveMeshRenderer.sharedMaterial.name = base.gameObject.name;
				}
				this.emissiveMeshRenderer.sharedMaterial.SetColor("_UnlitColor", Color.black);
				Color color = this.legacyLight.color.linear * this.legacyLight.intensity;
				color *= this.lightDimmer;
				this.emissiveMeshRenderer.sharedMaterial.SetColor("_EmissiveColor", color);
				this.emissiveMeshRenderer.sharedMaterial.SetTexture("_EmissiveColorMap", this.areaLightCookie);
				CoreUtils.SetKeyword(this.emissiveMeshRenderer.sharedMaterial, "_EMISSIVE_COLOR_MAP", this.areaLightCookie != null);
				return;
			}
			if (this.emissiveMeshRenderer != null)
			{
				CoreUtils.Destroy(this.emissiveMeshRenderer);
			}
			if (this.emissiveMeshFilter != null)
			{
				CoreUtils.Destroy(this.emissiveMeshFilter);
			}
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0000E4A4 File Offset: 0x0000C6A4
		private void UpdateRectangleLightBounds()
		{
			this.legacyLight.useShadowMatrixOverride = false;
			this.legacyLight.useBoundingSphereOverride = true;
			float num = this.m_ShapeWidth * 0.5f;
			float num2 = this.m_ShapeHeight * 0.5f;
			float num3 = Mathf.Sqrt(num * num + num2 * num2);
			this.legacyLight.boundingSphereOverride = new Vector4(0f, 0f, 0f, Mathf.Max(this.range, num3));
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000E51C File Offset: 0x0000C71C
		private void UpdateTubeLightBounds()
		{
			this.legacyLight.useShadowMatrixOverride = false;
			this.legacyLight.useBoundingSphereOverride = true;
			this.legacyLight.boundingSphereOverride = new Vector4(0f, 0f, 0f, Mathf.Max(this.range, this.m_ShapeWidth * 0.5f));
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0000E578 File Offset: 0x0000C778
		private void UpdateBoxLightBounds()
		{
			this.legacyLight.useShadowMatrixOverride = true;
			this.legacyLight.useBoundingSphereOverride = true;
			Matrix4x4 matrix4x = Matrix4x4.Scale(new Vector3(1f, 1f, -1f));
			this.legacyLight.shadowMatrixOverride = HDShadowUtils.ExtractBoxLightProjectionMatrix(this.legacyLight.range, this.shapeWidth, this.m_ShapeHeight, this.shadowNearPlane) * matrix4x;
			float magnitude = new Vector3(this.shapeWidth * 0.5f, this.m_ShapeHeight * 0.5f, this.legacyLight.range * 0.5f).magnitude;
			this.legacyLight.boundingSphereOverride = new Vector4(0f, 0f, this.legacyLight.range * 0.5f, magnitude);
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000E650 File Offset: 0x0000C850
		private void UpdatePyramidLightBounds()
		{
			this.legacyLight.useShadowMatrixOverride = true;
			this.legacyLight.useBoundingSphereOverride = true;
			Matrix4x4 matrix4x = Matrix4x4.Scale(new Vector3(1f, 1f, -1f));
			this.legacyLight.shadowMatrixOverride = HDShadowUtils.ExtractSpotLightProjectionMatrix(this.legacyLight.range, this.legacyLight.spotAngle, this.shadowNearPlane, this.aspectRatio, 0f) * matrix4x;
			this.legacyLight.boundingSphereOverride = new Vector4(0f, 0f, 0f, this.legacyLight.range);
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000E6F8 File Offset: 0x0000C8F8
		private void UpdateBounds()
		{
			HDLightType type = this.type;
			if (type != HDLightType.Spot)
			{
				if (type != HDLightType.Area)
				{
					this.legacyLight.useBoundingSphereOverride = false;
					this.legacyLight.useShadowMatrixOverride = false;
					return;
				}
				AreaLightShape areaLightShape = this.areaLightShape;
				if (areaLightShape == AreaLightShape.Rectangle)
				{
					this.UpdateRectangleLightBounds();
					return;
				}
				if (areaLightShape != AreaLightShape.Tube)
				{
					return;
				}
				this.UpdateTubeLightBounds();
				return;
			}
			else
			{
				SpotLightShape spotLightShape = this.spotLightShape;
				if (spotLightShape == SpotLightShape.Pyramid)
				{
					this.UpdatePyramidLightBounds();
					return;
				}
				if (spotLightShape == SpotLightShape.Box)
				{
					this.UpdateBoxLightBounds();
					return;
				}
				this.legacyLight.useBoundingSphereOverride = false;
				this.legacyLight.useShadowMatrixOverride = false;
				return;
			}
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000E780 File Offset: 0x0000C980
		private void UpdateShapeSize()
		{
			this.shapeWidth = this.m_ShapeWidth;
			this.shapeHeight = this.m_ShapeHeight;
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0000E79A File Offset: 0x0000C99A
		public void UpdateAllLightValues()
		{
			this.UpdateShapeSize();
			this.UpdateLightIntensity();
			this.UpdateBounds();
			this.UpdateAreaLightEmissiveMesh();
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000E7B4 File Offset: 0x0000C9B4
		public void SetColor(Color color, float colorTemperature = -1f)
		{
			if (colorTemperature != -1f)
			{
				this.legacyLight.colorTemperature = colorTemperature;
				this.useColorTemperature = true;
			}
			this.color = color;
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000E7D8 File Offset: 0x0000C9D8
		public void EnableColorTemperature(bool enable)
		{
			this.useColorTemperature = enable;
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000E7E1 File Offset: 0x0000C9E1
		public void SetIntensity(float intensity)
		{
			this.intensity = intensity;
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000E7EA File Offset: 0x0000C9EA
		public void SetIntensity(float intensity, LightUnit unit)
		{
			this.lightUnit = unit;
			this.intensity = intensity;
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000E7FA File Offset: 0x0000C9FA
		public void SetSpotLightLuxAt(float luxIntensity, float distance)
		{
			this.lightUnit = LightUnit.Lux;
			this.luxAtDistance = distance;
			this.intensity = luxIntensity;
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0000E814 File Offset: 0x0000CA14
		public void SetCookie(Texture cookie, Vector2 directionalLightCookieSize)
		{
			HDLightType type = this.type;
			if (type == HDLightType.Area)
			{
				if (cookie.dimension != TextureDimension.Tex2D)
				{
					Debug.LogError("Texture dimension " + cookie.dimension + " is not supported for area lights.");
					return;
				}
				this.areaLightCookie = cookie;
				return;
			}
			else
			{
				if (type == HDLightType.Point && cookie.dimension != TextureDimension.Cube)
				{
					Debug.LogError("Texture dimension " + cookie.dimension + " is not supported for point lights.");
					return;
				}
				if ((type == HDLightType.Directional || type == HDLightType.Spot) && cookie.dimension != TextureDimension.Tex2D)
				{
					Debug.LogError("Texture dimension " + cookie.dimension + " is not supported for Directional/Spot lights.");
					return;
				}
				if (type == HDLightType.Directional)
				{
					this.shapeWidth = directionalLightCookieSize.x;
					this.shapeHeight = directionalLightCookieSize.y;
				}
				this.legacyLight.cookie = cookie;
				return;
			}
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000E8E2 File Offset: 0x0000CAE2
		public void SetCookie(Texture cookie)
		{
			this.SetCookie(cookie, Vector2.zero);
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000E8F0 File Offset: 0x0000CAF0
		public void SetSpotAngle(float angle, float innerSpotPercent = 0f)
		{
			this.legacyLight.spotAngle = angle;
			this.innerSpotPercent = innerSpotPercent;
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000E905 File Offset: 0x0000CB05
		public void SetLightDimmer(float dimmer = 1f, float volumetricDimmer = 1f)
		{
			this.lightDimmer = dimmer;
			this.volumetricDimmer = volumetricDimmer;
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000E915 File Offset: 0x0000CB15
		public void SetLightUnit(LightUnit unit)
		{
			this.lightUnit = unit;
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0000E91E File Offset: 0x0000CB1E
		public void EnableShadows(bool enabled)
		{
			this.legacyLight.shadows = (enabled ? LightShadows.Soft : LightShadows.None);
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0000E932 File Offset: 0x0000CB32
		public void SetShadowResolution(int resolution)
		{
			this.shadowResolution.@override = resolution;
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0000E940 File Offset: 0x0000CB40
		public void SetShadowResolutionLevel(int level)
		{
			this.shadowResolution.level = level;
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0000E94E File Offset: 0x0000CB4E
		public void SetShadowResolutionOverride(bool useOverride)
		{
			this.shadowResolution.useOverride = useOverride;
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0000E95C File Offset: 0x0000CB5C
		public void SetShadowNearPlane(float nearPlaneDistance)
		{
			this.shadowNearPlane = nearPlaneDistance;
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0000E965 File Offset: 0x0000CB65
		public void SetPCSSParams(int blockerSampleCount = 16, int filterSampleCount = 24, float minFilterSize = 0.01f, float radiusScaleForSoftness = 1f)
		{
			this.blockerSampleCount = blockerSampleCount;
			this.filterSampleCount = filterSampleCount;
			this.minFilterSize = minFilterSize;
			this.softnessScale = radiusScaleForSoftness;
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000E984 File Offset: 0x0000CB84
		public void SetLightLayer(LightLayerEnum lightLayerMask, LightLayerEnum shadowLayerMask)
		{
			this.linkShadowLayers = false;
			this.legacyLight.renderingLayerMask = HDAdditionalLightData.LightLayerToRenderingLayerMask((int)shadowLayerMask, this.legacyLight.renderingLayerMask);
			this.lightlayersMask = lightLayerMask;
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000E9B0 File Offset: 0x0000CBB0
		public void SetShadowDimmer(float shadowDimmer = 1f, float volumetricShadowDimmer = 1f)
		{
			this.shadowDimmer = shadowDimmer;
			this.volumetricShadowDimmer = volumetricShadowDimmer;
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000E9C0 File Offset: 0x0000CBC0
		public void SetShadowFadeDistance(float distance)
		{
			this.shadowFadeDistance = distance;
		}

		// Token: 0x06000292 RID: 658 RVA: 0x0000E9C9 File Offset: 0x0000CBC9
		public void SetDirectionalShadowTint(Color tint)
		{
			this.shadowTint = tint;
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0000E9D2 File Offset: 0x0000CBD2
		public void SetShadowUpdateMode(ShadowUpdateMode updateMode)
		{
			this.shadowUpdateMode = updateMode;
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0000CA15 File Offset: 0x0000AC15
		public void SetRange(float range)
		{
			this.legacyLight.range = range;
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0000E9DB File Offset: 0x0000CBDB
		public void SetShadowLightLayer(LightLayerEnum shadowLayerMask)
		{
			this.legacyLight.renderingLayerMask = HDAdditionalLightData.LightLayerToRenderingLayerMask((int)shadowLayerMask, this.legacyLight.renderingLayerMask);
		}

		// Token: 0x06000296 RID: 662 RVA: 0x0000E9F9 File Offset: 0x0000CBF9
		public void SetCullingMask(int cullingMask)
		{
			this.legacyLight.cullingMask = cullingMask;
		}

		// Token: 0x06000297 RID: 663 RVA: 0x0000EA08 File Offset: 0x0000CC08
		public float[] SetLayerShadowCullDistances(float[] layerShadowCullDistances)
		{
			this.legacyLight.layerShadowCullDistances = layerShadowCullDistances;
			return layerShadowCullDistances;
		}

		// Token: 0x06000298 RID: 664 RVA: 0x0000EA24 File Offset: 0x0000CC24
		public LightUnit[] GetSupportedLightUnits()
		{
			return HDAdditionalLightData.GetSupportedLightUnits(this.type, this.m_SpotLightShape);
		}

		// Token: 0x06000299 RID: 665 RVA: 0x0000EA37 File Offset: 0x0000CC37
		public void SetAreaLightSize(Vector2 size)
		{
			if (this.type == HDLightType.Area)
			{
				this.m_ShapeWidth = size.x;
				this.m_ShapeHeight = size.y;
				this.UpdateAllLightValues();
			}
		}

		// Token: 0x0600029A RID: 666 RVA: 0x0000EA60 File Offset: 0x0000CC60
		public void SetBoxSpotSize(Vector2 size)
		{
			if (this.type == HDLightType.Spot)
			{
				this.shapeWidth = size.x;
				this.shapeHeight = size.y;
			}
		}

		// Token: 0x0600029B RID: 667 RVA: 0x0000EA84 File Offset: 0x0000CC84
		internal static int LightLayerToRenderingLayerMask(int lightLayer, int renderingLayerMask)
		{
			byte b = (byte)lightLayer;
			return (renderingLayerMask & -256) | (int)b;
		}

		// Token: 0x0600029C RID: 668 RVA: 0x0000EA9D File Offset: 0x0000CC9D
		internal static int RenderingLayerMaskToLightLayer(int renderingLayerMask)
		{
			return (int)((byte)renderingLayerMask);
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600029D RID: 669 RVA: 0x0000EAA1 File Offset: 0x0000CCA1
		private ShadowMapType shadowMapType
		{
			get
			{
				if (this.type == HDLightType.Area && this.areaLightShape == AreaLightShape.Rectangle)
				{
					return ShadowMapType.AreaLightAtlas;
				}
				if (this.type == HDLightType.Directional)
				{
					return ShadowMapType.CascadedDirectional;
				}
				return ShadowMapType.PunctualAtlas;
			}
		}

		// Token: 0x040001CD RID: 461
		[SerializeField]
		private HDAdditionalLightData.Version m_Version = MigrationDescription.LastVersion<HDAdditionalLightData.Version>();

		// Token: 0x040001CE RID: 462
		private static readonly MigrationDescription<HDAdditionalLightData.Version, HDAdditionalLightData> k_HDLightMigrationSteps = MigrationDescription.New<HDAdditionalLightData.Version, HDAdditionalLightData>(new MigrationStep<HDAdditionalLightData.Version, HDAdditionalLightData>[]
		{
			MigrationStep.New<HDAdditionalLightData.Version, HDAdditionalLightData>(HDAdditionalLightData.Version.ShadowNearPlane, delegate(HDAdditionalLightData data)
			{
				data.shadowNearPlane = data.legacyLight.shadowNearPlane;
			}),
			MigrationStep.New<HDAdditionalLightData.Version, HDAdditionalLightData>(HDAdditionalLightData.Version.LightLayer, delegate(HDAdditionalLightData data)
			{
				data.legacyLight.renderingLayerMask = HDAdditionalLightData.LightLayerToRenderingLayerMask((int)data.m_LightLayers, data.legacyLight.renderingLayerMask);
			}),
			MigrationStep.New<HDAdditionalLightData.Version, HDAdditionalLightData>(HDAdditionalLightData.Version.ShadowLayer, delegate(HDAdditionalLightData data)
			{
				data.lightlayersMask = (LightLayerEnum)HDAdditionalLightData.RenderingLayerMaskToLightLayer(data.legacyLight.renderingLayerMask);
			}),
			MigrationStep.New<HDAdditionalLightData.Version, HDAdditionalLightData>(HDAdditionalLightData.Version.ShadowResolution, delegate(HDAdditionalLightData data)
			{
				AdditionalShadowData component = data.GetComponent<AdditionalShadowData>();
				if (component != null)
				{
					data.m_ObsoleteCustomShadowResolution = component.customResolution;
					data.m_ObsoleteContactShadows = component.contactShadows;
					data.shadowDimmer = component.shadowDimmer;
					data.volumetricShadowDimmer = component.volumetricShadowDimmer;
					data.shadowFadeDistance = component.shadowFadeDistance;
					data.shadowTint = component.shadowTint;
					data.normalBias = component.normalBias;
					data.shadowUpdateMode = component.shadowUpdateMode;
					data.shadowCascadeRatios = component.shadowCascadeRatios;
					data.shadowCascadeBorders = component.shadowCascadeBorders;
					data.shadowAlgorithm = component.shadowAlgorithm;
					data.shadowVariant = component.shadowVariant;
					data.shadowPrecision = component.shadowPrecision;
					CoreUtils.Destroy(component);
				}
				data.shadowResolution.@override = data.m_ObsoleteCustomShadowResolution;
				switch (data.m_ObsoleteShadowResolutionTier)
				{
				case HDAdditionalLightData.ShadowResolutionTier.Low:
					data.shadowResolution.level = 0;
					break;
				case HDAdditionalLightData.ShadowResolutionTier.Medium:
					data.shadowResolution.level = 1;
					break;
				case HDAdditionalLightData.ShadowResolutionTier.High:
					data.shadowResolution.level = 2;
					break;
				case HDAdditionalLightData.ShadowResolutionTier.VeryHigh:
					data.shadowResolution.level = 3;
					break;
				}
				data.shadowResolution.useOverride = !data.m_ObsoleteUseShadowQualitySettings;
				data.useContactShadow.@override = data.m_ObsoleteContactShadows;
			}),
			MigrationStep.New<HDAdditionalLightData.Version, HDAdditionalLightData>(HDAdditionalLightData.Version.RemoveAdditionalShadowData, delegate(HDAdditionalLightData data)
			{
				AdditionalShadowData component2 = data.GetComponent<AdditionalShadowData>();
				if (component2 != null)
				{
					CoreUtils.Destroy(component2);
				}
			}),
			MigrationStep.New<HDAdditionalLightData.Version, HDAdditionalLightData>(HDAdditionalLightData.Version.AreaLightShapeTypeLogicIsolation, delegate(HDAdditionalLightData data)
			{
				switch (data.m_PointlightHDType)
				{
				case HDAdditionalLightData.PointLightHDType.Punctual:
					data.m_PointlightHDType = HDAdditionalLightData.PointLightHDType.Punctual;
					return;
				case HDAdditionalLightData.PointLightHDType.Area:
					data.m_PointlightHDType = HDAdditionalLightData.PointLightHDType.Area;
					data.m_AreaLightShape = AreaLightShape.Rectangle;
					return;
				case (HDAdditionalLightData.PointLightHDType)2:
					data.m_PointlightHDType = HDAdditionalLightData.PointLightHDType.Area;
					data.m_AreaLightShape = AreaLightShape.Tube;
					return;
				default:
					return;
				}
			}),
			MigrationStep.New<HDAdditionalLightData.Version, HDAdditionalLightData>(HDAdditionalLightData.Version.PCSSUIUpdate, delegate(HDAdditionalLightData data)
			{
				data.minFilterSize *= 1000f;
			})
		});

		// Token: 0x040001CF RID: 463
		[Obsolete("Use Light.renderingLayerMask instead")]
		[FormerlySerializedAs("lightLayers")]
		private LightLayerEnum m_LightLayers = LightLayerEnum.LightLayerDefault;

		// Token: 0x040001D0 RID: 464
		[Obsolete]
		[SerializeField]
		[FormerlySerializedAs("m_ShadowResolutionTier")]
		private HDAdditionalLightData.ShadowResolutionTier m_ObsoleteShadowResolutionTier = HDAdditionalLightData.ShadowResolutionTier.Medium;

		// Token: 0x040001D1 RID: 465
		[Obsolete]
		[SerializeField]
		[FormerlySerializedAs("m_UseShadowQualitySettings")]
		private bool m_ObsoleteUseShadowQualitySettings;

		// Token: 0x040001D2 RID: 466
		[FormerlySerializedAs("m_CustomShadowResolution")]
		[Obsolete]
		[SerializeField]
		private int m_ObsoleteCustomShadowResolution = 512;

		// Token: 0x040001D3 RID: 467
		[FormerlySerializedAs("m_ContactShadows")]
		[Obsolete]
		[SerializeField]
		private bool m_ObsoleteContactShadows;

		// Token: 0x040001D4 RID: 468
		[NonSerialized]
		private static Dictionary<int, LightUnit[]> supportedLightTypeCache = new Dictionary<int, LightUnit[]>();

		// Token: 0x040001D5 RID: 469
		[SerializeField]
		[FormerlySerializedAs("lightTypeExtent")]
		[FormerlySerializedAs("m_LightTypeExtent")]
		private HDAdditionalLightData.PointLightHDType m_PointlightHDType;

		// Token: 0x040001D6 RID: 470
		[SerializeField]
		[FormerlySerializedAs("spotLightShape")]
		private SpotLightShape m_SpotLightShape;

		// Token: 0x040001D7 RID: 471
		[SerializeField]
		private AreaLightShape m_AreaLightShape;

		// Token: 0x040001D8 RID: 472
		public const float k_DefaultDirectionalLightIntensity = 3.1415927f;

		// Token: 0x040001D9 RID: 473
		public const float k_DefaultPunctualLightIntensity = 600f;

		// Token: 0x040001DA RID: 474
		public const float k_DefaultAreaLightIntensity = 200f;

		// Token: 0x040001DB RID: 475
		public const float k_MinSpotAngle = 1f;

		// Token: 0x040001DC RID: 476
		public const float k_MaxSpotAngle = 179f;

		// Token: 0x040001DD RID: 477
		public const float k_MinAspectRatio = 0.05f;

		// Token: 0x040001DE RID: 478
		public const float k_MaxAspectRatio = 20f;

		// Token: 0x040001DF RID: 479
		public const float k_MinViewBiasScale = 0f;

		// Token: 0x040001E0 RID: 480
		public const float k_MaxViewBiasScale = 15f;

		// Token: 0x040001E1 RID: 481
		public const float k_MinAreaWidth = 0.01f;

		// Token: 0x040001E2 RID: 482
		public const int k_DefaultShadowResolution = 512;

		// Token: 0x040001E3 RID: 483
		internal const float k_MinEvsmExponent = 5f;

		// Token: 0x040001E4 RID: 484
		internal const float k_MaxEvsmExponent = 42f;

		// Token: 0x040001E5 RID: 485
		internal const float k_MinEvsmLightLeakBias = 0f;

		// Token: 0x040001E6 RID: 486
		internal const float k_MaxEvsmLightLeakBias = 1f;

		// Token: 0x040001E7 RID: 487
		internal const float k_MinEvsmVarianceBias = 0f;

		// Token: 0x040001E8 RID: 488
		internal const float k_MaxEvsmVarianceBias = 0.001f;

		// Token: 0x040001E9 RID: 489
		internal const int k_MinEvsmBlurPasses = 0;

		// Token: 0x040001EA RID: 490
		internal const int k_MaxEvsmBlurPasses = 8;

		// Token: 0x040001EB RID: 491
		internal const float k_MinSpotInnerPercent = 0f;

		// Token: 0x040001EC RID: 492
		internal const float k_MaxSpotInnerPercent = 100f;

		// Token: 0x040001ED RID: 493
		internal const float k_MinAreaLightShadowCone = 10f;

		// Token: 0x040001EE RID: 494
		internal const float k_MaxAreaLightShadowCone = 179f;

		// Token: 0x040001EF RID: 495
		[SerializeField]
		[FormerlySerializedAs("displayLightIntensity")]
		private float m_Intensity;

		// Token: 0x040001F0 RID: 496
		[SerializeField]
		[FormerlySerializedAs("enableSpotReflector")]
		private bool m_EnableSpotReflector;

		// Token: 0x040001F1 RID: 497
		[SerializeField]
		[FormerlySerializedAs("luxAtDistance")]
		private float m_LuxAtDistance = 1f;

		// Token: 0x040001F2 RID: 498
		[Range(0f, 100f)]
		[SerializeField]
		private float m_InnerSpotPercent;

		// Token: 0x040001F3 RID: 499
		[Range(0f, 1f)]
		[SerializeField]
		[FormerlySerializedAs("lightDimmer")]
		private float m_LightDimmer = 1f;

		// Token: 0x040001F4 RID: 500
		[Range(0f, 1f)]
		[SerializeField]
		[FormerlySerializedAs("volumetricDimmer")]
		private float m_VolumetricDimmer = 1f;

		// Token: 0x040001F5 RID: 501
		[SerializeField]
		[FormerlySerializedAs("lightUnit")]
		private LightUnit m_LightUnit;

		// Token: 0x040001F6 RID: 502
		[SerializeField]
		[FormerlySerializedAs("fadeDistance")]
		private float m_FadeDistance = 10000f;

		// Token: 0x040001F7 RID: 503
		[SerializeField]
		[FormerlySerializedAs("affectDiffuse")]
		private bool m_AffectDiffuse = true;

		// Token: 0x040001F8 RID: 504
		[SerializeField]
		[FormerlySerializedAs("affectSpecular")]
		private bool m_AffectSpecular = true;

		// Token: 0x040001F9 RID: 505
		[SerializeField]
		[FormerlySerializedAs("nonLightmappedOnly")]
		private bool m_NonLightmappedOnly;

		// Token: 0x040001FA RID: 506
		[SerializeField]
		[FormerlySerializedAs("shapeWidth")]
		private float m_ShapeWidth = 0.5f;

		// Token: 0x040001FB RID: 507
		[SerializeField]
		[FormerlySerializedAs("shapeHeight")]
		private float m_ShapeHeight = 0.5f;

		// Token: 0x040001FC RID: 508
		[SerializeField]
		[FormerlySerializedAs("aspectRatio")]
		private float m_AspectRatio = 1f;

		// Token: 0x040001FD RID: 509
		[SerializeField]
		[FormerlySerializedAs("shapeRadius")]
		private float m_ShapeRadius = 0.025f;

		// Token: 0x040001FE RID: 510
		[SerializeField]
		private float m_SoftnessScale = 1f;

		// Token: 0x040001FF RID: 511
		[SerializeField]
		[FormerlySerializedAs("useCustomSpotLightShadowCone")]
		private bool m_UseCustomSpotLightShadowCone;

		// Token: 0x04000200 RID: 512
		[SerializeField]
		[FormerlySerializedAs("customSpotLightShadowCone")]
		private float m_CustomSpotLightShadowCone = 30f;

		// Token: 0x04000201 RID: 513
		[Range(0f, 1f)]
		[SerializeField]
		[FormerlySerializedAs("maxSmoothness")]
		private float m_MaxSmoothness = 0.99f;

		// Token: 0x04000202 RID: 514
		[SerializeField]
		[FormerlySerializedAs("applyRangeAttenuation")]
		private bool m_ApplyRangeAttenuation = true;

		// Token: 0x04000203 RID: 515
		[SerializeField]
		[FormerlySerializedAs("displayAreaLightEmissiveMesh")]
		private bool m_DisplayAreaLightEmissiveMesh;

		// Token: 0x04000204 RID: 516
		[SerializeField]
		[FormerlySerializedAs("areaLightCookie")]
		private Texture m_AreaLightCookie;

		// Token: 0x04000205 RID: 517
		[Range(10f, 179f)]
		[SerializeField]
		[FormerlySerializedAs("areaLightShadowCone")]
		private float m_AreaLightShadowCone = 120f;

		// Token: 0x04000206 RID: 518
		[SerializeField]
		[FormerlySerializedAs("useScreenSpaceShadows")]
		private bool m_UseScreenSpaceShadows;

		// Token: 0x04000207 RID: 519
		[SerializeField]
		[FormerlySerializedAs("interactsWithSky")]
		private bool m_InteractsWithSky = true;

		// Token: 0x04000208 RID: 520
		[SerializeField]
		[FormerlySerializedAs("angularDiameter")]
		private float m_AngularDiameter = 0.5f;

		// Token: 0x04000209 RID: 521
		[SerializeField]
		[FormerlySerializedAs("flareSize")]
		private float m_FlareSize = 2f;

		// Token: 0x0400020A RID: 522
		[SerializeField]
		[FormerlySerializedAs("flareTint")]
		private Color m_FlareTint = Color.white;

		// Token: 0x0400020B RID: 523
		[SerializeField]
		[FormerlySerializedAs("flareFalloff")]
		private float m_FlareFalloff = 4f;

		// Token: 0x0400020C RID: 524
		[SerializeField]
		[FormerlySerializedAs("surfaceTexture")]
		private Texture2D m_SurfaceTexture;

		// Token: 0x0400020D RID: 525
		[SerializeField]
		[FormerlySerializedAs("surfaceTint")]
		private Color m_SurfaceTint = Color.white;

		// Token: 0x0400020E RID: 526
		[SerializeField]
		[FormerlySerializedAs("distance")]
		private float m_Distance = 1.5E+11f;

		// Token: 0x0400020F RID: 527
		[SerializeField]
		[FormerlySerializedAs("useRayTracedShadows")]
		private bool m_UseRayTracedShadows;

		// Token: 0x04000210 RID: 528
		[Range(1f, 32f)]
		[SerializeField]
		[FormerlySerializedAs("numRayTracingSamples")]
		private int m_NumRayTracingSamples = 4;

		// Token: 0x04000211 RID: 529
		[SerializeField]
		[FormerlySerializedAs("filterTracedShadow")]
		private bool m_FilterTracedShadow = true;

		// Token: 0x04000212 RID: 530
		[Range(1f, 32f)]
		[SerializeField]
		[FormerlySerializedAs("filterSizeTraced")]
		private int m_FilterSizeTraced = 16;

		// Token: 0x04000213 RID: 531
		[Range(0f, 2f)]
		[SerializeField]
		[FormerlySerializedAs("sunLightConeAngle")]
		private float m_SunLightConeAngle = 0.5f;

		// Token: 0x04000214 RID: 532
		[SerializeField]
		[FormerlySerializedAs("lightShadowRadius")]
		private float m_LightShadowRadius = 0.5f;

		// Token: 0x04000215 RID: 533
		[SerializeField]
		private bool m_SemiTransparentShadow;

		// Token: 0x04000216 RID: 534
		[SerializeField]
		private bool m_ColorShadow = true;

		// Token: 0x04000217 RID: 535
		[Range(5f, 42f)]
		[SerializeField]
		[FormerlySerializedAs("evsmExponent")]
		private float m_EvsmExponent = 15f;

		// Token: 0x04000218 RID: 536
		[Range(0f, 1f)]
		[SerializeField]
		[FormerlySerializedAs("evsmLightLeakBias")]
		private float m_EvsmLightLeakBias;

		// Token: 0x04000219 RID: 537
		[Range(0f, 0.001f)]
		[SerializeField]
		[FormerlySerializedAs("evsmVarianceBias")]
		private float m_EvsmVarianceBias = 1E-05f;

		// Token: 0x0400021A RID: 538
		[Range(0f, 8f)]
		[SerializeField]
		[FormerlySerializedAs("evsmBlurPasses")]
		private int m_EvsmBlurPasses;

		// Token: 0x0400021B RID: 539
		[SerializeField]
		[FormerlySerializedAs("lightlayersMask")]
		private LightLayerEnum m_LightlayersMask = LightLayerEnum.LightLayerDefault;

		// Token: 0x0400021C RID: 540
		[SerializeField]
		[FormerlySerializedAs("linkShadowLayers")]
		private bool m_LinkShadowLayers = true;

		// Token: 0x0400021D RID: 541
		[SerializeField]
		[FormerlySerializedAs("shadowNearPlane")]
		private float m_ShadowNearPlane = 0.1f;

		// Token: 0x0400021E RID: 542
		[Range(1f, 64f)]
		[SerializeField]
		[FormerlySerializedAs("blockerSampleCount")]
		private int m_BlockerSampleCount = 24;

		// Token: 0x0400021F RID: 543
		[Range(1f, 64f)]
		[SerializeField]
		[FormerlySerializedAs("filterSampleCount")]
		private int m_FilterSampleCount = 16;

		// Token: 0x04000220 RID: 544
		[Range(0f, 1f)]
		[SerializeField]
		[FormerlySerializedAs("minFilterSize")]
		private float m_MinFilterSize = 0.1f;

		// Token: 0x04000221 RID: 545
		[Range(1f, 32f)]
		[SerializeField]
		[FormerlySerializedAs("kernelSize")]
		private int m_KernelSize = 5;

		// Token: 0x04000222 RID: 546
		[Range(0f, 9f)]
		[SerializeField]
		[FormerlySerializedAs("lightAngle")]
		private float m_LightAngle = 1f;

		// Token: 0x04000223 RID: 547
		[Range(0.0001f, 0.01f)]
		[SerializeField]
		[FormerlySerializedAs("maxDepthBias")]
		private float m_MaxDepthBias = 0.001f;

		// Token: 0x04000224 RID: 548
		[SerializeField]
		private IntScalableSettingValue m_ShadowResolution = new IntScalableSettingValue
		{
			@override = 512,
			useOverride = true
		};

		// Token: 0x04000225 RID: 549
		[Range(0f, 1f)]
		[SerializeField]
		private float m_ShadowDimmer = 1f;

		// Token: 0x04000226 RID: 550
		[Range(0f, 1f)]
		[SerializeField]
		private float m_VolumetricShadowDimmer = 1f;

		// Token: 0x04000227 RID: 551
		[SerializeField]
		private float m_ShadowFadeDistance = 10000f;

		// Token: 0x04000228 RID: 552
		[SerializeField]
		private BoolScalableSettingValue m_UseContactShadow = new BoolScalableSettingValue
		{
			useOverride = true
		};

		// Token: 0x04000229 RID: 553
		[SerializeField]
		private bool m_RayTracedContactShadow;

		// Token: 0x0400022A RID: 554
		[SerializeField]
		private Color m_ShadowTint = Color.black;

		// Token: 0x0400022B RID: 555
		[SerializeField]
		private bool m_PenumbraTint;

		// Token: 0x0400022C RID: 556
		[SerializeField]
		private float m_NormalBias = 0.75f;

		// Token: 0x0400022D RID: 557
		[SerializeField]
		private float m_SlopeBias = 0.5f;

		// Token: 0x0400022E RID: 558
		[SerializeField]
		private ShadowUpdateMode m_ShadowUpdateMode;

		// Token: 0x0400022F RID: 559
		[Range(0f, 90f)]
		[SerializeField]
		private float m_BarnDoorAngle = 90f;

		// Token: 0x04000230 RID: 560
		[SerializeField]
		private float m_BarnDoorLength = 0.05f;

		// Token: 0x04000231 RID: 561
		[SerializeField]
		private float[] m_ShadowCascadeRatios = new float[] { 0.05f, 0.2f, 0.3f };

		// Token: 0x04000232 RID: 562
		[SerializeField]
		private float[] m_ShadowCascadeBorders = new float[] { 0.2f, 0.2f, 0.2f, 0.2f };

		// Token: 0x04000233 RID: 563
		[SerializeField]
		private int m_ShadowAlgorithm;

		// Token: 0x04000234 RID: 564
		[SerializeField]
		private int m_ShadowVariant;

		// Token: 0x04000235 RID: 565
		[SerializeField]
		private int m_ShadowPrecision;

		// Token: 0x04000236 RID: 566
		[SerializeField]
		[FormerlySerializedAs("useOldInspector")]
		private bool useOldInspector;

		// Token: 0x04000237 RID: 567
		[SerializeField]
		[FormerlySerializedAs("useVolumetric")]
		private bool useVolumetric = true;

		// Token: 0x04000238 RID: 568
		[SerializeField]
		[FormerlySerializedAs("featuresFoldout")]
		private bool featuresFoldout = true;

		// Token: 0x04000239 RID: 569
		[SerializeField]
		[FormerlySerializedAs("showAdditionalSettings")]
		private byte showAdditionalSettings;

		// Token: 0x0400023A RID: 570
		private HDShadowRequest[] shadowRequests;

		// Token: 0x0400023B RID: 571
		private bool m_WillRenderShadowMap;

		// Token: 0x0400023C RID: 572
		private bool m_WillRenderScreenSpaceShadow;

		// Token: 0x0400023D RID: 573
		private bool m_WillRenderRayTracedShadow;

		// Token: 0x0400023E RID: 574
		private int[] m_ShadowRequestIndices;

		// Token: 0x0400023F RID: 575
		private bool m_ShadowMapRenderedSinceLastRequest;

		// Token: 0x04000240 RID: 576
		private Vector2 m_CachedShadowResolution = new Vector2(0f, 0f);

		// Token: 0x04000241 RID: 577
		private Vector3 m_CachedViewPos = new Vector3(0f, 0f, 0f);

		// Token: 0x04000242 RID: 578
		private int[] m_CachedResolutionRequestIndices = new int[6];

		// Token: 0x04000243 RID: 579
		private bool m_CachedDataIsValid = true;

		// Token: 0x04000244 RID: 580
		private int m_AtlasShapeID;

		// Token: 0x04000245 RID: 581
		[NonSerialized]
		private Plane[] m_ShadowFrustumPlanes = new Plane[6];

		// Token: 0x04000246 RID: 582
		[NonSerialized]
		internal Matrix4x4 previousTransform;

		// Token: 0x04000247 RID: 583
		[NonSerialized]
		internal int shadowIndex = -1;

		// Token: 0x04000248 RID: 584
		private Light m_Light;

		// Token: 0x04000249 RID: 585
		private MeshRenderer m_EmissiveMeshRenderer;

		// Token: 0x0400024A RID: 586
		private MeshFilter m_EmissiveMeshFilter;

		// Token: 0x0400024B RID: 587
		[NonSerialized]
		private TimelineWorkaround timelineWorkaround;

		// Token: 0x0400024C RID: 588
		[NonSerialized]
		private bool m_Animated;

		// Token: 0x02000199 RID: 409
		private enum Version
		{
			// Token: 0x04001110 RID: 4368
			_Unused00,
			// Token: 0x04001111 RID: 4369
			_Unused01,
			// Token: 0x04001112 RID: 4370
			ShadowNearPlane,
			// Token: 0x04001113 RID: 4371
			LightLayer,
			// Token: 0x04001114 RID: 4372
			ShadowLayer,
			// Token: 0x04001115 RID: 4373
			_Unused02,
			// Token: 0x04001116 RID: 4374
			ShadowResolution,
			// Token: 0x04001117 RID: 4375
			RemoveAdditionalShadowData,
			// Token: 0x04001118 RID: 4376
			AreaLightShapeTypeLogicIsolation,
			// Token: 0x04001119 RID: 4377
			PCSSUIUpdate
		}

		// Token: 0x0200019A RID: 410
		[Obsolete]
		private enum ShadowResolutionTier
		{
			// Token: 0x0400111B RID: 4379
			Low,
			// Token: 0x0400111C RID: 4380
			Medium,
			// Token: 0x0400111D RID: 4381
			High,
			// Token: 0x0400111E RID: 4382
			VeryHigh
		}

		// Token: 0x0200019B RID: 411
		[Obsolete]
		private enum LightTypeExtent
		{
			// Token: 0x04001120 RID: 4384
			Punctual,
			// Token: 0x04001121 RID: 4385
			Rectangle,
			// Token: 0x04001122 RID: 4386
			Tube
		}

		// Token: 0x0200019C RID: 412
		private enum PointLightHDType
		{
			// Token: 0x04001124 RID: 4388
			Punctual,
			// Token: 0x04001125 RID: 4389
			Area
		}

		// Token: 0x0200019D RID: 413
		internal static class ScalableSettings
		{
			// Token: 0x06000B3F RID: 2879 RVA: 0x00054C1E File Offset: 0x00052E1E
			public static IntScalableSetting ShadowResolutionArea(HDRenderPipelineAsset hdrp)
			{
				return hdrp.currentPlatformRenderPipelineSettings.hdShadowInitParams.shadowResolutionArea;
			}

			// Token: 0x06000B40 RID: 2880 RVA: 0x00054C30 File Offset: 0x00052E30
			public static IntScalableSetting ShadowResolutionPunctual(HDRenderPipelineAsset hdrp)
			{
				return hdrp.currentPlatformRenderPipelineSettings.hdShadowInitParams.shadowResolutionPunctual;
			}

			// Token: 0x06000B41 RID: 2881 RVA: 0x00054C42 File Offset: 0x00052E42
			public static IntScalableSetting ShadowResolutionDirectional(HDRenderPipelineAsset hdrp)
			{
				return hdrp.currentPlatformRenderPipelineSettings.hdShadowInitParams.shadowResolutionDirectional;
			}

			// Token: 0x06000B42 RID: 2882 RVA: 0x00054C54 File Offset: 0x00052E54
			public static BoolScalableSetting UseContactShadow(HDRenderPipelineAsset hdrp)
			{
				return hdrp.currentPlatformRenderPipelineSettings.lightSettings.useContactShadow;
			}
		}
	}
}
