using Characters.NPC;
using UnityEngine;
using UnityEditor;

namespace CustomEditors.Scripts{
	[CustomEditor(typeof(NpcCombatManager))]
	public class NpcCombatManagerEditor : Editor {
		NpcCombatManager _manager;
		Transform objTransform;
		void OnSceneGUI() {
			_manager = target as NpcCombatManager;
			objTransform = _manager.transform;
			DrawSightCone();
		}

		void DrawSightCone() {
			float radius = _manager.EditorLineSightRadius;
			float angle = _manager.EditorLineSightAngle;
			Vector3 arcOrigin = Quaternion.AngleAxis(-angle, objTransform.up) * objTransform.forward;
			Vector3 arcEnd = Quaternion.AngleAxis(angle, objTransform.up) * objTransform.forward;

			Handles.color = Color.red;
			Handles.DrawLine(objTransform.position, arcOrigin * radius + objTransform.position);
			Handles.DrawLine(objTransform.position, arcEnd * radius + objTransform.position);
			Handles.DrawWireArc(objTransform.position, objTransform.up, arcOrigin, angle * 2, radius);

			radius = Handles.ScaleValueHandle(radius, objTransform.position + objTransform.forward * radius,
			objTransform.rotation, 1, Handles.CubeHandleCap, 0.1f);
			_manager.EditorLineSightRadius = Mathf.Clamp(radius, 0, 1000);

			angle = Handles.ScaleValueHandle(angle, objTransform.position + objTransform.forward * radius,
			Quaternion.AngleAxis(90, Vector3.up) * objTransform.rotation, 5, Handles.ArrowHandleCap, 0.1f);
			_manager.EditorLineSightAngle = Mathf.Clamp(angle, 0, 180);
		}
	}
}
