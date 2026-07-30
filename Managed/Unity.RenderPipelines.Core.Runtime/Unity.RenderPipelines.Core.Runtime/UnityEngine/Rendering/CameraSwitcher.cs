using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000018 RID: 24
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.render-pipelines.core@8.0/manual/Camera-Switcher.html")]
	public class CameraSwitcher : MonoBehaviour
	{
		// Token: 0x06000092 RID: 146 RVA: 0x00004208 File Offset: 0x00002408
		private void OnEnable()
		{
			this.m_OriginalCamera = base.GetComponent<Camera>();
			this.m_CurrentCamera = this.m_OriginalCamera;
			if (this.m_OriginalCamera == null)
			{
				Debug.LogError("Camera Switcher needs a Camera component attached");
				return;
			}
			this.m_CurrentCameraIndex = this.GetCameraCount() - 1;
			this.m_CameraNames = new GUIContent[this.GetCameraCount()];
			this.m_CameraIndices = new int[this.GetCameraCount()];
			for (int i = 0; i < this.m_Cameras.Length; i++)
			{
				Camera camera = this.m_Cameras[i];
				if (camera != null)
				{
					this.m_CameraNames[i] = new GUIContent(camera.name);
				}
				else
				{
					this.m_CameraNames[i] = new GUIContent("null");
				}
				this.m_CameraIndices[i] = i;
			}
			this.m_CameraNames[this.GetCameraCount() - 1] = new GUIContent("Original Camera");
			this.m_CameraIndices[this.GetCameraCount() - 1] = this.GetCameraCount() - 1;
			this.m_DebugEntry = new DebugUI.EnumField
			{
				displayName = "Camera Switcher",
				getter = () => this.m_CurrentCameraIndex,
				setter = delegate(int value)
				{
					this.SetCameraIndex(value);
				},
				enumNames = this.m_CameraNames,
				enumValues = this.m_CameraIndices,
				getIndex = () => this.m_DebugEntryEnumIndex,
				setIndex = delegate(int value)
				{
					this.m_DebugEntryEnumIndex = value;
				}
			};
			DebugManager.instance.GetPanel("Camera", true, 0, false).children.Add(this.m_DebugEntry);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00004393 File Offset: 0x00002593
		private void OnDisable()
		{
			if (this.m_DebugEntry != null && this.m_DebugEntry.panel != null)
			{
				this.m_DebugEntry.panel.children.Remove(this.m_DebugEntry);
			}
		}

		// Token: 0x06000094 RID: 148 RVA: 0x000043C6 File Offset: 0x000025C6
		private int GetCameraCount()
		{
			return this.m_Cameras.Length + 1;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000043D2 File Offset: 0x000025D2
		private Camera GetNextCamera()
		{
			if (this.m_CurrentCameraIndex == this.m_Cameras.Length)
			{
				return this.m_OriginalCamera;
			}
			return this.m_Cameras[this.m_CurrentCameraIndex];
		}

		// Token: 0x06000096 RID: 150 RVA: 0x000043F8 File Offset: 0x000025F8
		private void SetCameraIndex(int index)
		{
			if (index > 0 || index < this.GetCameraCount())
			{
				this.m_CurrentCameraIndex = index;
				if (this.m_CurrentCamera == this.m_OriginalCamera)
				{
					this.m_OriginalCameraPosition = this.m_OriginalCamera.transform.position;
					this.m_OriginalCameraRotation = this.m_OriginalCamera.transform.rotation;
				}
				this.m_CurrentCamera = this.GetNextCamera();
				if (this.m_CurrentCamera != null)
				{
					if (this.m_CurrentCamera == this.m_OriginalCamera)
					{
						this.m_OriginalCamera.transform.position = this.m_OriginalCameraPosition;
						this.m_OriginalCamera.transform.rotation = this.m_OriginalCameraRotation;
					}
					base.transform.position = this.m_CurrentCamera.transform.position;
					base.transform.rotation = this.m_CurrentCamera.transform.rotation;
				}
			}
		}

		// Token: 0x04000071 RID: 113
		public Camera[] m_Cameras;

		// Token: 0x04000072 RID: 114
		private int m_CurrentCameraIndex = -1;

		// Token: 0x04000073 RID: 115
		private Camera m_OriginalCamera;

		// Token: 0x04000074 RID: 116
		private Vector3 m_OriginalCameraPosition;

		// Token: 0x04000075 RID: 117
		private Quaternion m_OriginalCameraRotation;

		// Token: 0x04000076 RID: 118
		private Camera m_CurrentCamera;

		// Token: 0x04000077 RID: 119
		private GUIContent[] m_CameraNames;

		// Token: 0x04000078 RID: 120
		private int[] m_CameraIndices;

		// Token: 0x04000079 RID: 121
		private DebugUI.EnumField m_DebugEntry;

		// Token: 0x0400007A RID: 122
		private int m_DebugEntryEnumIndex;
	}
}
