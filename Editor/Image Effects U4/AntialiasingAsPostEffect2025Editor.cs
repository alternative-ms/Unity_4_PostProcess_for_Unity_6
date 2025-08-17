using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AntialiasingAsPostEffect2025))]
class AntialiasingAsPostEffect2025Editor : Editor {
	SerializedObject serObj;	
	SerializedProperty AntialiasingMode;
	SerializedProperty showGeneratedNormals;
	SerializedProperty offsetScale;
	SerializedProperty blurRadius;
	SerializedProperty dlaaSharp;
	SerializedProperty edgeThresholdMin;
	SerializedProperty edgeThreshold;
	SerializedProperty edgeSharpness;	

	void OnEnable () {
		serObj = new SerializedObject (target);
		AntialiasingMode = serObj.FindProperty ("AntialiasingMode");
		showGeneratedNormals = serObj.FindProperty ("showGeneratedNormals");
		offsetScale = serObj.FindProperty ("offsetScale");
		blurRadius = serObj.FindProperty ("blurRadius");
		dlaaSharp = serObj.FindProperty ("dlaaSharp");
		edgeThresholdMin = serObj.FindProperty("edgeThresholdMin");
		edgeThreshold = serObj.FindProperty("edgeThreshold");
		edgeSharpness = serObj.FindProperty("edgeSharpness");
	}
    		
    void OnInspectorGUI () {        
    	serObj.Update ();
    	
		GUILayout.Label("Luminance based fullscreen antialiasing (edge blur)", EditorStyles.miniBoldLabel);
    	
    	EditorGUILayout.PropertyField (AntialiasingMode, new GUIContent ("Technique"));
    	
    	Material mat = (target as AntialiasingAsPostEffect2025).CurrentAAMaterial ();
    	if(null == mat && (target as AntialiasingAsPostEffect2025).enabled) {
			EditorGUILayout.HelpBox("This AA technique is currently not supported. Choose a different technique or disable the effect and use MSAA instead.", MessageType.Warning);    		
    	}

		if ((AAMode)AntialiasingMode.enumValueIndex == AAMode.NFAA) {
    		EditorGUILayout.PropertyField (offsetScale, new GUIContent ("Edge Detect Ofs"));
    		EditorGUILayout.PropertyField (blurRadius, new GUIContent ("Blur Radius"));
    		EditorGUILayout.PropertyField (showGeneratedNormals, new GUIContent ("Show Normals"));	
		} else if ((AAMode)AntialiasingMode.enumValueIndex == AAMode.DLAA) {
    		EditorGUILayout.PropertyField (dlaaSharp, new GUIContent ("Sharp"));			
		} else if ((AAMode)AntialiasingMode.enumValueIndex == AAMode.FXAA3Console) {
    		EditorGUILayout.PropertyField (edgeThresholdMin, new GUIContent ("Edge Min Threshhold"));
    		EditorGUILayout.PropertyField (edgeThreshold, new GUIContent ("Edge Threshhold"));
    		EditorGUILayout.PropertyField (edgeSharpness, new GUIContent ("Edge Sharpness"));
		}
    	
    	serObj.ApplyModifiedProperties();
    }
}

