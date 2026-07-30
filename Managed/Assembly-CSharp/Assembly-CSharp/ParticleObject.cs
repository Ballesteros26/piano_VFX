using System;
using UnityEngine;

// Token: 0x02000019 RID: 25
public class ParticleObject
{
	// Token: 0x060000DE RID: 222 RVA: 0x0000C453 File Offset: 0x0000A653
	public ParticleObject(string name)
	{
		this.name = name;
	}

	// Token: 0x060000DF RID: 223 RVA: 0x0000C462 File Offset: 0x0000A662
	public string GetJSON()
	{
		return JsonUtility.ToJson(this);
	}

	// Token: 0x04000220 RID: 544
	public string name;

	// Token: 0x04000221 RID: 545
	public float mainRate;

	// Token: 0x04000222 RID: 546
	public float mainVel1X;

	// Token: 0x04000223 RID: 547
	public float mainVel2X;

	// Token: 0x04000224 RID: 548
	public float mainVel1Y;

	// Token: 0x04000225 RID: 549
	public float mainVel2Y;

	// Token: 0x04000226 RID: 550
	public float mainLifetime1;

	// Token: 0x04000227 RID: 551
	public float mainLifetime2;

	// Token: 0x04000228 RID: 552
	public float mainSpawnRad;

	// Token: 0x04000229 RID: 553
	public float mainTurbulence;

	// Token: 0x0400022A RID: 554
	public float mainTOctaves;

	// Token: 0x0400022B RID: 555
	public float mainTDrag;

	// Token: 0x0400022C RID: 556
	public float mainTFrequency;

	// Token: 0x0400022D RID: 557
	public float mainSize;

	// Token: 0x0400022E RID: 558
	public float mainNoise;

	// Token: 0x0400022F RID: 559
	public float mainGravity;

	// Token: 0x04000230 RID: 560
	public float childRate;

	// Token: 0x04000231 RID: 561
	public float childVel1X;

	// Token: 0x04000232 RID: 562
	public float childVel2X;

	// Token: 0x04000233 RID: 563
	public float childVel1Y;

	// Token: 0x04000234 RID: 564
	public float childVel2Y;

	// Token: 0x04000235 RID: 565
	public float childLifetime1;

	// Token: 0x04000236 RID: 566
	public float childLifetime2;

	// Token: 0x04000237 RID: 567
	public float childSpawnRad;

	// Token: 0x04000238 RID: 568
	public float childTurbulence;

	// Token: 0x04000239 RID: 569
	public float childTOctaves;

	// Token: 0x0400023A RID: 570
	public float childTDrag;

	// Token: 0x0400023B RID: 571
	public float childTFrequency;

	// Token: 0x0400023C RID: 572
	public float childSize;

	// Token: 0x0400023D RID: 573
	public float childNoise;

	// Token: 0x0400023E RID: 574
	public float childGravity;

	// Token: 0x0400023F RID: 575
	public float fogRate;

	// Token: 0x04000240 RID: 576
	public float fogVel1X;

	// Token: 0x04000241 RID: 577
	public float fogVel2X;

	// Token: 0x04000242 RID: 578
	public float fogVel1Y;

	// Token: 0x04000243 RID: 579
	public float fogVel2Y;

	// Token: 0x04000244 RID: 580
	public float fogLifetime1;

	// Token: 0x04000245 RID: 581
	public float fogLifetime2;

	// Token: 0x04000246 RID: 582
	public float fogSpawnRad;

	// Token: 0x04000247 RID: 583
	public float trailRate;

	// Token: 0x04000248 RID: 584
	public float trailVel1X;

	// Token: 0x04000249 RID: 585
	public float trailVel2X;

	// Token: 0x0400024A RID: 586
	public float trailVel1Y;

	// Token: 0x0400024B RID: 587
	public float trailVel2Y;

	// Token: 0x0400024C RID: 588
	public float trailLifetime1;

	// Token: 0x0400024D RID: 589
	public float trailLifetime2;

	// Token: 0x0400024E RID: 590
	public float trailTurbulence;

	// Token: 0x0400024F RID: 591
	public float trailLength;
}
