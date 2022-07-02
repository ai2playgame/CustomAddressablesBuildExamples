using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.AddressableAssets.Settings;
using UnityEngine;

namespace CustomAASBuild.Editor
{
    [DisplayName("For Separated Build Setting")]
    public class SeparatedGroupBuildSchema : AddressableAssetGroupSchema
    {
        public enum BuildGroupSeries
        {
            GroupA,
            GroupB,
            IncludedInAllGroup,
        }

        [SerializeField]
        private BuildGroupSeries buildGroup = BuildGroupSeries.IncludedInAllGroup;

        public BuildGroupSeries BuildGroup 
        {
            get => buildGroup;
            set
            {
                if (buildGroup != value)
                {
                    buildGroup = value;
                    SetDirty(true);
                }
            }
        }

        private void ShowAssetPacks(SerializedObject so, List<AddressableAssetGroupSchema> otherSchemas = null)
        {
            var current = BuildGroup;
            
            SerializedProperty prop = so.FindProperty("buildGroup");
            if (otherSchemas != null)
            {
                ShowMixedValue(prop, otherSchemas, typeof(BuildGroupSeries), "buildGroup");
            }
            
            EditorGUI.BeginChangeCheck();
            var newGroupValue = (BuildGroupSeries)EditorGUILayout.EnumPopup("Build Group", current);
            if (EditorGUI.EndChangeCheck())
            {
                BuildGroup = newGroupValue;
                if (otherSchemas != null)
                {
                    foreach (var s in otherSchemas)
                    {
                        var separatedGroupBuildSchema = s as SeparatedGroupBuildSchema;
                        if (separatedGroupBuildSchema != null)
                            separatedGroupBuildSchema.BuildGroup = newGroupValue;
                    }
                }

                if (otherSchemas != null) EditorGUI.showMixedValue = false;
            }
        }

        public override void OnGUI()
        {
            var so = new SerializedObject(this);
            ShowAssetPacks(so);
            so.ApplyModifiedProperties();
        }

        public override void OnGUIMultiple(List<AddressableAssetGroupSchema> otherSchemas)
        {
            var so = new SerializedObject(this);
            ShowAssetPacks(so, otherSchemas);
            so.ApplyModifiedProperties();
        }
    }
}