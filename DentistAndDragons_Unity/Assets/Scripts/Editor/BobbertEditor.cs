using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
 
[CustomEditor(typeof(BobbertController))]
public class BobbertEditor : Editor
{
    private BobbertController _bobbertController;
    override public void  OnInspectorGUI () {
        _bobbertController = (BobbertController) target;
        DrawDefaultInspector();
        if(GUILayout.Button("Idle")) {
            _bobbertController.idle();
        }
        if(GUILayout.Button("Open")) {
            _bobbertController.openMouth();
        }
        if(GUILayout.Button("Close")) {
            _bobbertController.closeMouth();
        }
        if(GUILayout.Button("Hurt")) {
            _bobbertController.hurt();
        }
    }
}

