using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000056 RID: 86
	[RequireComponent(typeof(Transform))]
	[NativeHeader("Modules/ParticleSystem/ParticleSystem.h")]
	[NativeHeader("Modules/ParticleSystem/ParticleSystemForceField.h")]
	[NativeHeader("Modules/ParticleSystem/ParticleSystemForceFieldManager.h")]
	[NativeHeader("Modules/ParticleSystem/ScriptBindings/ParticleSystemScriptBindings.h")]
	[NativeHeader("ParticleSystemScriptingClasses.h")]
	public class ParticleSystemForceField : Component
	{
		// Token: 0x170001CF RID: 463
		// (get) Token: 0x060006CB RID: 1739
		// (set) Token: 0x060006CC RID: 1740
		[NativeName("ForceShape")]
		public extern ParticleSystemForceFieldShape shape
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x060006CD RID: 1741
		// (set) Token: 0x060006CE RID: 1742
		public extern float startRange
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x060006CF RID: 1743
		// (set) Token: 0x060006D0 RID: 1744
		public extern float endRange
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x060006D1 RID: 1745
		// (set) Token: 0x060006D2 RID: 1746
		public extern float length
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x060006D3 RID: 1747
		// (set) Token: 0x060006D4 RID: 1748
		public extern float gravityFocus
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x060006D5 RID: 1749 RVA: 0x00006198 File Offset: 0x00004398
		// (set) Token: 0x060006D6 RID: 1750 RVA: 0x000061AE File Offset: 0x000043AE
		public Vector2 rotationRandomness
		{
			get
			{
				Vector2 vector;
				this.get_rotationRandomness_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_rotationRandomness_Injected(ref value);
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x060006D7 RID: 1751
		// (set) Token: 0x060006D8 RID: 1752
		public extern bool multiplyDragByParticleSize
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x060006D9 RID: 1753
		// (set) Token: 0x060006DA RID: 1754
		public extern bool multiplyDragByParticleVelocity
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x060006DB RID: 1755
		// (set) Token: 0x060006DC RID: 1756
		public extern Texture3D vectorField
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x060006DD RID: 1757 RVA: 0x000061B8 File Offset: 0x000043B8
		// (set) Token: 0x060006DE RID: 1758 RVA: 0x000061CE File Offset: 0x000043CE
		public ParticleSystem.MinMaxCurve directionX
		{
			get
			{
				ParticleSystem.MinMaxCurve minMaxCurve;
				this.get_directionX_Injected(out minMaxCurve);
				return minMaxCurve;
			}
			set
			{
				this.set_directionX_Injected(ref value);
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x060006DF RID: 1759 RVA: 0x000061D8 File Offset: 0x000043D8
		// (set) Token: 0x060006E0 RID: 1760 RVA: 0x000061EE File Offset: 0x000043EE
		public ParticleSystem.MinMaxCurve directionY
		{
			get
			{
				ParticleSystem.MinMaxCurve minMaxCurve;
				this.get_directionY_Injected(out minMaxCurve);
				return minMaxCurve;
			}
			set
			{
				this.set_directionY_Injected(ref value);
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x060006E1 RID: 1761 RVA: 0x000061F8 File Offset: 0x000043F8
		// (set) Token: 0x060006E2 RID: 1762 RVA: 0x0000620E File Offset: 0x0000440E
		public ParticleSystem.MinMaxCurve directionZ
		{
			get
			{
				ParticleSystem.MinMaxCurve minMaxCurve;
				this.get_directionZ_Injected(out minMaxCurve);
				return minMaxCurve;
			}
			set
			{
				this.set_directionZ_Injected(ref value);
			}
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x060006E3 RID: 1763 RVA: 0x00006218 File Offset: 0x00004418
		// (set) Token: 0x060006E4 RID: 1764 RVA: 0x0000622E File Offset: 0x0000442E
		public ParticleSystem.MinMaxCurve gravity
		{
			get
			{
				ParticleSystem.MinMaxCurve minMaxCurve;
				this.get_gravity_Injected(out minMaxCurve);
				return minMaxCurve;
			}
			set
			{
				this.set_gravity_Injected(ref value);
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x060006E5 RID: 1765 RVA: 0x00006238 File Offset: 0x00004438
		// (set) Token: 0x060006E6 RID: 1766 RVA: 0x0000624E File Offset: 0x0000444E
		public ParticleSystem.MinMaxCurve rotationSpeed
		{
			get
			{
				ParticleSystem.MinMaxCurve minMaxCurve;
				this.get_rotationSpeed_Injected(out minMaxCurve);
				return minMaxCurve;
			}
			set
			{
				this.set_rotationSpeed_Injected(ref value);
			}
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x060006E7 RID: 1767 RVA: 0x00006258 File Offset: 0x00004458
		// (set) Token: 0x060006E8 RID: 1768 RVA: 0x0000626E File Offset: 0x0000446E
		public ParticleSystem.MinMaxCurve rotationAttraction
		{
			get
			{
				ParticleSystem.MinMaxCurve minMaxCurve;
				this.get_rotationAttraction_Injected(out minMaxCurve);
				return minMaxCurve;
			}
			set
			{
				this.set_rotationAttraction_Injected(ref value);
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x060006E9 RID: 1769 RVA: 0x00006278 File Offset: 0x00004478
		// (set) Token: 0x060006EA RID: 1770 RVA: 0x0000628E File Offset: 0x0000448E
		public ParticleSystem.MinMaxCurve drag
		{
			get
			{
				ParticleSystem.MinMaxCurve minMaxCurve;
				this.get_drag_Injected(out minMaxCurve);
				return minMaxCurve;
			}
			set
			{
				this.set_drag_Injected(ref value);
			}
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x060006EB RID: 1771 RVA: 0x00006298 File Offset: 0x00004498
		// (set) Token: 0x060006EC RID: 1772 RVA: 0x000062AE File Offset: 0x000044AE
		public ParticleSystem.MinMaxCurve vectorFieldSpeed
		{
			get
			{
				ParticleSystem.MinMaxCurve minMaxCurve;
				this.get_vectorFieldSpeed_Injected(out minMaxCurve);
				return minMaxCurve;
			}
			set
			{
				this.set_vectorFieldSpeed_Injected(ref value);
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x060006ED RID: 1773 RVA: 0x000062B8 File Offset: 0x000044B8
		// (set) Token: 0x060006EE RID: 1774 RVA: 0x000062CE File Offset: 0x000044CE
		public ParticleSystem.MinMaxCurve vectorFieldAttraction
		{
			get
			{
				ParticleSystem.MinMaxCurve minMaxCurve;
				this.get_vectorFieldAttraction_Injected(out minMaxCurve);
				return minMaxCurve;
			}
			set
			{
				this.set_vectorFieldAttraction_Injected(ref value);
			}
		}

		// Token: 0x060006F0 RID: 1776
		[MethodImpl(4096)]
		private extern void get_rotationRandomness_Injected(out Vector2 ret);

		// Token: 0x060006F1 RID: 1777
		[MethodImpl(4096)]
		private extern void set_rotationRandomness_Injected(ref Vector2 value);

		// Token: 0x060006F2 RID: 1778
		[MethodImpl(4096)]
		private extern void get_directionX_Injected(out ParticleSystem.MinMaxCurve ret);

		// Token: 0x060006F3 RID: 1779
		[MethodImpl(4096)]
		private extern void set_directionX_Injected(ref ParticleSystem.MinMaxCurve value);

		// Token: 0x060006F4 RID: 1780
		[MethodImpl(4096)]
		private extern void get_directionY_Injected(out ParticleSystem.MinMaxCurve ret);

		// Token: 0x060006F5 RID: 1781
		[MethodImpl(4096)]
		private extern void set_directionY_Injected(ref ParticleSystem.MinMaxCurve value);

		// Token: 0x060006F6 RID: 1782
		[MethodImpl(4096)]
		private extern void get_directionZ_Injected(out ParticleSystem.MinMaxCurve ret);

		// Token: 0x060006F7 RID: 1783
		[MethodImpl(4096)]
		private extern void set_directionZ_Injected(ref ParticleSystem.MinMaxCurve value);

		// Token: 0x060006F8 RID: 1784
		[MethodImpl(4096)]
		private extern void get_gravity_Injected(out ParticleSystem.MinMaxCurve ret);

		// Token: 0x060006F9 RID: 1785
		[MethodImpl(4096)]
		private extern void set_gravity_Injected(ref ParticleSystem.MinMaxCurve value);

		// Token: 0x060006FA RID: 1786
		[MethodImpl(4096)]
		private extern void get_rotationSpeed_Injected(out ParticleSystem.MinMaxCurve ret);

		// Token: 0x060006FB RID: 1787
		[MethodImpl(4096)]
		private extern void set_rotationSpeed_Injected(ref ParticleSystem.MinMaxCurve value);

		// Token: 0x060006FC RID: 1788
		[MethodImpl(4096)]
		private extern void get_rotationAttraction_Injected(out ParticleSystem.MinMaxCurve ret);

		// Token: 0x060006FD RID: 1789
		[MethodImpl(4096)]
		private extern void set_rotationAttraction_Injected(ref ParticleSystem.MinMaxCurve value);

		// Token: 0x060006FE RID: 1790
		[MethodImpl(4096)]
		private extern void get_drag_Injected(out ParticleSystem.MinMaxCurve ret);

		// Token: 0x060006FF RID: 1791
		[MethodImpl(4096)]
		private extern void set_drag_Injected(ref ParticleSystem.MinMaxCurve value);

		// Token: 0x06000700 RID: 1792
		[MethodImpl(4096)]
		private extern void get_vectorFieldSpeed_Injected(out ParticleSystem.MinMaxCurve ret);

		// Token: 0x06000701 RID: 1793
		[MethodImpl(4096)]
		private extern void set_vectorFieldSpeed_Injected(ref ParticleSystem.MinMaxCurve value);

		// Token: 0x06000702 RID: 1794
		[MethodImpl(4096)]
		private extern void get_vectorFieldAttraction_Injected(out ParticleSystem.MinMaxCurve ret);

		// Token: 0x06000703 RID: 1795
		[MethodImpl(4096)]
		private extern void set_vectorFieldAttraction_Injected(ref ParticleSystem.MinMaxCurve value);
	}
}
