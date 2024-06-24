using Characters.NPC;
using UnityEngine;
using UnityEditor;

namespace CustomEditors.Scripts{
	[CustomEditor(typeof(NpcAgroController))]
	public class NpcAgroManagerEditor : Editor {
		NpcAgroController _controller;
		Transform objTransform;
		void OnSceneGUI() {
			_controller = target as NpcAgroController;
			objTransform = _controller?.transform;
			DrawSightCone();
		}

		void DrawSightCone() {
			float radius = _controller.EditorLineSightRadius;
			float angle = _controller.EditorLineSightAngle;
			Vector3 arcOrigin = Quaternion.AngleAxis(-angle, objTransform.up) * objTransform.forward;
			Vector3 arcEnd = Quaternion.AngleAxis(angle, objTransform.up) * objTransform.forward;

			Handles.color = Color.red;
			Handles.DrawLine(objTransform.position, arcOrigin * radius + objTransform.position);
			Handles.DrawLine(objTransform.position, arcEnd * radius + objTransform.position);
			Handles.DrawWireArc(objTransform.position, objTransform.up, arcOrigin, angle * 2, radius);

			radius = Handles.ScaleValueHandle(radius, objTransform.position + objTransform.forward * radius,
			objTransform.rotation, 1, Handles.CubeHandleCap, 0.1f);
			_controller.EditorLineSightRadius = Mathf.Clamp(radius, 0, 1000);

			angle = Handles.ScaleValueHandle(angle, objTransform.position + objTransform.forward * radius,
			Quaternion.AngleAxis(90, Vector3.up) * objTransform.rotation, 5, Handles.ArrowHandleCap, 0.1f);
			_controller.EditorLineSightAngle = Mathf.Clamp(angle, 0, 180);
		}
	}
}
