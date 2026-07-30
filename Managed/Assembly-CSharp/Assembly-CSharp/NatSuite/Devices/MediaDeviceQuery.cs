using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using NatSuite.Devices.Internal;
using UnityEngine;
using UnityEngine.Android;

namespace NatSuite.Devices
{
	// Token: 0x02000037 RID: 55
	[Doc("MediaDeviceQuery")]
	public sealed class MediaDeviceQuery
	{
		// Token: 0x060001DF RID: 479 RVA: 0x00013074 File Offset: 0x00011274
		[Doc("RequestPermissions")]
		public static Task<bool> RequestPermissions<T>() where T : IMediaDevice
		{
			bool flag = typeof(ICameraDevice).IsAssignableFrom(typeof(T));
			bool flag2 = typeof(IAudioDevice).IsAssignableFrom(typeof(T));
			if (!flag && !flag2)
			{
				return Task.FromResult<bool>(true);
			}
			TaskCompletionSource<bool> permissionTask = new TaskCompletionSource<bool>();
			MediaDeviceQuery.MediaDeviceQueryPermissionsHelper requester = new GameObject("MediaDeviceQuery Permissions Helper").AddComponent<MediaDeviceQuery.MediaDeviceQueryPermissionsHelper>();
			requester.Request(flag, delegate(bool granted)
			{
				permissionTask.SetResult(granted);
				global::UnityEngine.Object.Destroy(requester);
			});
			return permissionTask.Task;
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x0001310B File Offset: 0x0001130B
		[Doc("MediaDeviceQueryCurrentDevice")]
		public IMediaDevice currentDevice
		{
			get
			{
				if (this.index >= this.count)
				{
					return null;
				}
				return this.devices[this.index];
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x0001312A File Offset: 0x0001132A
		[Doc("MediaDeviceQueryCount")]
		public int count
		{
			get
			{
				return this.devices.Length;
			}
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00013134 File Offset: 0x00011334
		[Doc("MediaDeviceQueryCtor")]
		public MediaDeviceQuery(params MediaDeviceQuery.Criterion[] criteria)
		{
			List<IMediaDevice> list = new List<IMediaDevice>();
			RuntimePlatform platform = Application.platform;
			switch (platform)
			{
			case RuntimePlatform.OSXEditor:
			case RuntimePlatform.OSXPlayer:
			case RuntimePlatform.WindowsPlayer:
			case RuntimePlatform.WindowsEditor:
				list.AddRange(MediaDeviceQuery.AudioDevices());
				list.AddRange(MediaDeviceQuery.WebCamDevices());
				goto IL_0089;
			case RuntimePlatform.OSXWebPlayer:
			case RuntimePlatform.OSXDashboardPlayer:
			case RuntimePlatform.WindowsWebPlayer:
			case (RuntimePlatform)6:
				goto IL_007E;
			case RuntimePlatform.IPhonePlayer:
				break;
			default:
				if (platform != RuntimePlatform.Android)
				{
					goto IL_007E;
				}
				break;
			}
			list.AddRange(MediaDeviceQuery.AudioDevices());
			list.AddRange(MediaDeviceQuery.CameraDevices());
			goto IL_0089;
			IL_007E:
			list.AddRange(MediaDeviceQuery.WebCamDevices());
			IL_0089:
			this.devices = list.Where((IMediaDevice device) => criteria.All((MediaDeviceQuery.Criterion criterion) => criterion(device))).ToArray<IMediaDevice>();
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x000131E7 File Offset: 0x000113E7
		[Doc("MediaDeviceQueryAdvance")]
		[MethodImpl(MethodImplOptions.Synchronized)]
		public void Advance()
		{
			this.index = (this.index + 1) % this.devices.Length;
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00013200 File Offset: 0x00011400
		private static IEnumerable<AudioDevice> AudioDevices()
		{
			IntPtr intPtr;
			int num;
			Bridge.AudioDevices(out intPtr, out num);
			AudioDevice[] array = new AudioDevice[num];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new NativeAudioDevice(Marshal.ReadIntPtr(intPtr, i * Marshal.SizeOf(typeof(IntPtr))));
			}
			Marshal.FreeCoTaskMem(intPtr);
			return array;
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00013254 File Offset: 0x00011454
		private static IEnumerable<CameraDevice> CameraDevices()
		{
			IntPtr intPtr;
			int num;
			Bridge.CameraDevices(out intPtr, out num);
			CameraDevice[] array = new CameraDevice[num];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new NativeCameraDevice(Marshal.ReadIntPtr(intPtr, i * Marshal.SizeOf(typeof(IntPtr))));
			}
			Marshal.FreeCoTaskMem(intPtr);
			return array;
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x000132A5 File Offset: 0x000114A5
		private static IEnumerable<WebCameraDevice> WebCamDevices()
		{
			return WebCamTexture.devices.Select((WebCamDevice device) => new WebCameraDevice(device));
		}

		// Token: 0x040003BF RID: 959
		[Doc("MediaDeviceQueryDevices")]
		public readonly IMediaDevice[] devices;

		// Token: 0x040003C0 RID: 960
		private int index;

		// Token: 0x02000071 RID: 113
		// (Invoke) Token: 0x0600034E RID: 846
		[Doc("Criterion")]
		public delegate bool Criterion(IMediaDevice device);

		// Token: 0x02000072 RID: 114
		[Doc("Criteria")]
		public static class Criteria
		{
			// Token: 0x04000488 RID: 1160
			[Doc("CriterionAudioDevice")]
			public static readonly MediaDeviceQuery.Criterion AudioDevice = (IMediaDevice device) => device is AudioDevice;

			// Token: 0x04000489 RID: 1161
			[Doc("CriterionCameraDevice")]
			public static readonly MediaDeviceQuery.Criterion CameraDevice = (IMediaDevice device) => device is CameraDevice;

			// Token: 0x0400048A RID: 1162
			[Doc("CriterionGenericCameraDevice")]
			public static readonly MediaDeviceQuery.Criterion GenericCameraDevice = (IMediaDevice device) => device is ICameraDevice;

			// Token: 0x0400048B RID: 1163
			[Doc("CriterionRearFacing")]
			public static readonly MediaDeviceQuery.Criterion RearFacing = delegate(IMediaDevice device)
			{
				ICameraDevice cameraDevice;
				return (cameraDevice = device as ICameraDevice) != null && !cameraDevice.frontFacing;
			};

			// Token: 0x0400048C RID: 1164
			[Doc("CriterionFrontFacing")]
			public static readonly MediaDeviceQuery.Criterion FrontFacing = delegate(IMediaDevice device)
			{
				ICameraDevice cameraDevice2;
				return (cameraDevice2 = device as ICameraDevice) != null && cameraDevice2.frontFacing;
			};

			// Token: 0x0400048D RID: 1165
			[Doc("CriterionEchoCancellation")]
			public static readonly MediaDeviceQuery.Criterion EchoCancellation = delegate(IMediaDevice device)
			{
				AudioDevice audioDevice;
				return (audioDevice = device as AudioDevice) != null && audioDevice.echoCancellation;
			};

			// Token: 0x0400048E RID: 1166
			[Doc("CriterionTorch")]
			public static readonly MediaDeviceQuery.Criterion Torch = delegate(IMediaDevice device)
			{
				CameraDevice cameraDevice3;
				return (cameraDevice3 = device as CameraDevice) != null && cameraDevice3.torchSupported;
			};
		}

		// Token: 0x02000073 RID: 115
		private sealed class MediaDeviceQueryPermissionsHelper : MonoBehaviour
		{
			// Token: 0x06000352 RID: 850 RVA: 0x00017D58 File Offset: 0x00015F58
			private void Awake()
			{
				global::UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
			}

			// Token: 0x06000353 RID: 851 RVA: 0x00017D68 File Offset: 0x00015F68
			public void Request(bool camera, Action<bool> completionHandler)
			{
				RuntimePlatform platform = Application.platform;
				switch (platform)
				{
				case RuntimePlatform.OSXEditor:
				case RuntimePlatform.OSXPlayer:
				case RuntimePlatform.WindowsPlayer:
				case RuntimePlatform.WindowsEditor:
				case RuntimePlatform.IPhonePlayer:
					base.StartCoroutine(this.RequestiOS(camera, completionHandler));
					return;
				case RuntimePlatform.OSXWebPlayer:
				case RuntimePlatform.OSXDashboardPlayer:
				case RuntimePlatform.WindowsWebPlayer:
				case (RuntimePlatform)6:
					break;
				default:
					if (platform == RuntimePlatform.Android)
					{
						base.StartCoroutine(this.RequestAndroid(camera, completionHandler));
						return;
					}
					break;
				}
				completionHandler(true);
			}

			// Token: 0x06000354 RID: 852 RVA: 0x00017DD1 File Offset: 0x00015FD1
			private IEnumerator RequestAndroid(bool camera, Action<bool> completionHandler)
			{
				string permission = (camera ? "android.permission.CAMERA" : "android.permission.RECORD_AUDIO");
				if (Permission.HasUserAuthorizedPermission(permission))
				{
					completionHandler(true);
				}
				else
				{
					Permission.RequestUserPermission(permission);
					yield return new WaitUntil(() => Permission.HasUserAuthorizedPermission(permission));
					completionHandler(true);
				}
				yield break;
			}

			// Token: 0x06000355 RID: 853 RVA: 0x00017DE7 File Offset: 0x00015FE7
			private IEnumerator RequestiOS(bool camera, Action<bool> completionHandler)
			{
				UserAuthorization permission = (camera ? UserAuthorization.WebCam : UserAuthorization.Microphone);
				if (Application.HasUserAuthorization(permission))
				{
					completionHandler(true);
				}
				else
				{
					yield return Application.RequestUserAuthorization(permission);
					completionHandler(Application.HasUserAuthorization(permission));
				}
				yield break;
			}
		}
	}
}
