using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;

namespace NatSuite.Devices
{
	// Token: 0x02000035 RID: 53
	public interface ICameraDevice : IMediaDevice, IEquatable<IMediaDevice>
	{
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060001D5 RID: 469
		bool frontFacing { get; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060001D6 RID: 470
		// (set) Token: 0x060001D7 RID: 471
		[TupleElementNames(new string[] { "width", "height" })]
		ValueTuple<int, int> previewResolution
		{
			[return: TupleElementNames(new string[] { "width", "height" })]
			get;
			[param: TupleElementNames(new string[] { "width", "height" })]
			set;
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060001D8 RID: 472
		// (set) Token: 0x060001D9 RID: 473
		int frameRate { get; set; }

		// Token: 0x060001DA RID: 474
		Task<Texture2D> StartRunning();
	}
}
