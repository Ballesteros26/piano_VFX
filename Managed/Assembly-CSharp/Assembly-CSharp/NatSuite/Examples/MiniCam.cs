using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using NatSuite.Devices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NatSuite.Examples
{
	// Token: 0x02000040 RID: 64
	public class MiniCam : MonoBehaviour
	{
		// Token: 0x06000267 RID: 615 RVA: 0x00013BD0 File Offset: 0x00011DD0
		private async void Start()
		{
			TaskAwaiter<bool> taskAwaiter = MediaDeviceQuery.RequestPermissions<CameraDevice>().GetAwaiter();
			if (!taskAwaiter.IsCompleted)
			{
				await taskAwaiter;
				TaskAwaiter<bool> taskAwaiter2;
				taskAwaiter = taskAwaiter2;
				taskAwaiter2 = default(TaskAwaiter<bool>);
			}
			if (!taskAwaiter.GetResult())
			{
				Debug.LogError("User did not grant camera permissions");
			}
			else
			{
				this.deviceQuery = new MediaDeviceQuery(new MediaDeviceQuery.Criterion[] { MediaDeviceQuery.Criteria.GenericCameraDevice });
				ICameraDevice device = this.deviceQuery.currentDevice as ICameraDevice;
				Texture2D texture2D = await device.StartRunning();
				Debug.Log(string.Format("Started camera preview with resolution {0}x{1}", texture2D.width, texture2D.height));
				this.previewPanel.texture = texture2D;
				this.previewAspectFitter.aspectRatio = (float)texture2D.width / (float)texture2D.height;
				this.switchIcon.color = ((this.deviceQuery.count > 1) ? Color.white : Color.gray);
				CameraDevice cameraDevice;
				this.flashIcon.color = (((cameraDevice = device as CameraDevice) != null && cameraDevice.flashSupported) ? Color.white : Color.gray);
			}
		}

		// Token: 0x06000268 RID: 616 RVA: 0x00013C0C File Offset: 0x00011E0C
		public async void CapturePhoto()
		{
			CameraDevice cameraDevice;
			if ((cameraDevice = this.deviceQuery.currentDevice as CameraDevice) != null)
			{
				Texture2D texture2D = await cameraDevice.CapturePhoto();
				Texture2D photoTexture = texture2D;
				Debug.Log(string.Format("Captured photo with resolution {0}x{1}", photoTexture.width, photoTexture.height));
				this.photoPanel.gameObject.SetActive(true);
				this.photoPanel.texture = photoTexture;
				this.photoAspectFitter.aspectRatio = (float)photoTexture.width / (float)photoTexture.height;
				await Task.Delay(3000);
				this.photoPanel.gameObject.SetActive(false);
				global::UnityEngine.Object.Destroy(photoTexture);
				photoTexture = null;
			}
		}

		// Token: 0x06000269 RID: 617 RVA: 0x00013C48 File Offset: 0x00011E48
		public async void SwitchCamera()
		{
			if (this.deviceQuery.count >= 2)
			{
				(this.deviceQuery.currentDevice as ICameraDevice).StopRunning();
				this.deviceQuery.Advance();
				Texture2D texture2D = await (this.deviceQuery.currentDevice as ICameraDevice).StartRunning();
				this.previewPanel.texture = texture2D;
				this.previewAspectFitter.aspectRatio = (float)texture2D.width / (float)texture2D.height;
			}
		}

		// Token: 0x0600026A RID: 618 RVA: 0x00013C84 File Offset: 0x00011E84
		public void FocusCamera(BaseEventData e)
		{
			CameraDevice cameraDevice;
			if ((cameraDevice = this.deviceQuery.currentDevice as CameraDevice) != null)
			{
				PointerEventData pointerEventData = e as PointerEventData;
				RectTransform component = pointerEventData.pointerPress.GetComponent<RectTransform>();
				Vector3 vector;
				if (!RectTransformUtility.ScreenPointToWorldPointInRectangle(component, pointerEventData.pressPosition, pointerEventData.pressEventCamera, out vector))
				{
					return;
				}
				Vector3[] array = new Vector3[4];
				component.GetWorldCorners(array);
				Vector3 vector2 = vector - array[0];
				Vector2 vector3 = new Vector2(array[3].x, array[1].y) - array[0];
				cameraDevice.focusPoint = new ValueTuple<float, float>(vector2.x / vector3.x, vector2.y / vector3.y);
			}
		}

		// Token: 0x0600026B RID: 619 RVA: 0x00013D50 File Offset: 0x00011F50
		public void ToggleFlashMode()
		{
			CameraDevice cameraDevice;
			if ((cameraDevice = this.deviceQuery.currentDevice as CameraDevice) != null)
			{
				cameraDevice.flashMode = ((cameraDevice.flashMode == FlashMode.On) ? FlashMode.Off : FlashMode.On);
				this.flashIcon.color = ((cameraDevice.flashMode == FlashMode.On) ? Color.white : Color.gray);
			}
		}

		// Token: 0x040003CF RID: 975
		[Header("Camera Preview")]
		public RawImage previewPanel;

		// Token: 0x040003D0 RID: 976
		public AspectRatioFitter previewAspectFitter;

		// Token: 0x040003D1 RID: 977
		[Header("Photo Capture")]
		public RawImage photoPanel;

		// Token: 0x040003D2 RID: 978
		public AspectRatioFitter photoAspectFitter;

		// Token: 0x040003D3 RID: 979
		public Image flashIcon;

		// Token: 0x040003D4 RID: 980
		public Image switchIcon;

		// Token: 0x040003D5 RID: 981
		private MediaDeviceQuery deviceQuery;
	}
}
