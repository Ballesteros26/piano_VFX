using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000019 RID: 25
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.render-pipelines.core@8.0/manual/Free-Camera.html")]
	[ExecuteAlways]
	public class FreeCamera : MonoBehaviour
	{
		// Token: 0x0600009C RID: 156 RVA: 0x0000451B File Offset: 0x0000271B
		private void OnEnable()
		{
			this.RegisterInputs();
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00002788 File Offset: 0x00000988
		private void RegisterInputs()
		{
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00004524 File Offset: 0x00002724
		private void Update()
		{
			if (DebugManager.instance.displayRuntimeUI)
			{
				return;
			}
			float num = 0f;
			float num2 = 0f;
			if (Input.GetMouseButton(1))
			{
				num = Input.GetAxis(FreeCamera.kMouseX) * this.m_LookSpeedMouse;
				num2 = Input.GetAxis(FreeCamera.kMouseY) * this.m_LookSpeedMouse;
			}
			num += Input.GetAxis(FreeCamera.kRightStickX) * this.m_LookSpeedController * Time.deltaTime;
			num2 += Input.GetAxis(FreeCamera.kRightStickY) * this.m_LookSpeedController * Time.deltaTime;
			float axis = Input.GetAxis(FreeCamera.kSpeedAxis);
			if (axis != 0f)
			{
				this.m_MoveSpeed += axis * this.m_MoveSpeedIncrement;
				if (this.m_MoveSpeed < this.m_MoveSpeedIncrement)
				{
					this.m_MoveSpeed = this.m_MoveSpeedIncrement;
				}
			}
			float axis2 = Input.GetAxis(FreeCamera.kVertical);
			float axis3 = Input.GetAxis(FreeCamera.kHorizontal);
			float axis4 = Input.GetAxis(FreeCamera.kYAxis);
			if (num != 0f || num2 != 0f || axis2 != 0f || axis3 != 0f || axis4 != 0f)
			{
				float x = base.transform.localEulerAngles.x;
				float num3 = base.transform.localEulerAngles.y + num;
				float num4 = x - num2;
				if (x <= 90f && num4 >= 0f)
				{
					num4 = Mathf.Clamp(num4, 0f, 90f);
				}
				if (x >= 270f)
				{
					num4 = Mathf.Clamp(num4, 270f, 360f);
				}
				base.transform.localRotation = Quaternion.Euler(num4, num3, base.transform.localEulerAngles.z);
				float num5 = Time.deltaTime * this.m_MoveSpeed;
				if (Input.GetMouseButton(1))
				{
					num5 *= (Input.GetKey(KeyCode.LeftShift) ? this.m_Turbo : 1f);
				}
				else
				{
					num5 *= ((Input.GetAxis("Fire1") > 0f) ? this.m_Turbo : 1f);
				}
				base.transform.position += base.transform.forward * num5 * axis2;
				base.transform.position += base.transform.right * num5 * axis3;
				base.transform.position += Vector3.up * num5 * axis4;
			}
		}

		// Token: 0x0400007B RID: 123
		public float m_LookSpeedController = 120f;

		// Token: 0x0400007C RID: 124
		public float m_LookSpeedMouse = 10f;

		// Token: 0x0400007D RID: 125
		public float m_MoveSpeed = 10f;

		// Token: 0x0400007E RID: 126
		public float m_MoveSpeedIncrement = 2.5f;

		// Token: 0x0400007F RID: 127
		public float m_Turbo = 10f;

		// Token: 0x04000080 RID: 128
		private static string kMouseX = "Mouse X";

		// Token: 0x04000081 RID: 129
		private static string kMouseY = "Mouse Y";

		// Token: 0x04000082 RID: 130
		private static string kRightStickX = "Controller Right Stick X";

		// Token: 0x04000083 RID: 131
		private static string kRightStickY = "Controller Right Stick Y";

		// Token: 0x04000084 RID: 132
		private static string kVertical = "Vertical";

		// Token: 0x04000085 RID: 133
		private static string kHorizontal = "Horizontal";

		// Token: 0x04000086 RID: 134
		private static string kYAxis = "YAxis";

		// Token: 0x04000087 RID: 135
		private static string kSpeedAxis = "Speed Axis";
	}
}
