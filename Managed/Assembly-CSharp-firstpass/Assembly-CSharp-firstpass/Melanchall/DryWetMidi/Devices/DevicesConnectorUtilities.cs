using System;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.Devices
{
	// Token: 0x020000E8 RID: 232
	public static class DevicesConnectorUtilities
	{
		// Token: 0x060005B6 RID: 1462 RVA: 0x00018E06 File Offset: 0x00017006
		public static DevicesConnector Connect(this InputDevice inputDevice, params IOutputDevice[] outputDevices)
		{
			ThrowIfArgument.IsNull("inputDevice", inputDevice);
			ThrowIfArgument.IsNull("outputDevices", outputDevices);
			DevicesConnector devicesConnector = new DevicesConnector(inputDevice, outputDevices);
			devicesConnector.Connect();
			return devicesConnector;
		}
	}
}
