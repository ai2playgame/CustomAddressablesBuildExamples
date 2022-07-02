using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor.AddressableAssets.Build;
using UnityEditor.AddressableAssets.Build.DataBuilders;
using UnityEditor.AddressableAssets.Settings;
using UnityEditor.AddressableAssets.Settings.GroupSchemas;
using UnityEditor.Build.Pipeline.Interfaces;
using UnityEngine;

namespace CustomAASBuild.Editor
{
    public class SeparatedBuildScriptBase : BuildScriptPackedMode
    {
        public override string Name => "Separated Group Build Script";

        public virtual BuildGroupSeries BuildTargetGroup => BuildGroupSeries.GroupA;
        public virtual bool IsDebug => true;

        private readonly Dictionary<AddressableAssetGroup, bool> m_SavedIncludeInBuildState = new();

        protected override TResult BuildDataImplementation<TResult>(AddressablesDataBuilderInput builderInput)
        {
            TResult result = default(TResult);

            var timer = new Stopwatch();
            timer.Start();

            ExcludeNotBuildTargetGroup(builderInput.AddressableSettings);
            InitializeBuildContext(builderInput, out AddressableAssetsBuildContext aaContext);

            using (Log.ScopedStep(LogLevel.Info, "ProcessAllGroups"))
            {
                var errorString = ProcessAllGroups(aaContext);
                if (!string.IsNullOrEmpty(errorString))
                    result = AddressableAssetBuildResult.CreateResult<TResult>(null, 0, errorString);
            }

            if (result == null)
            {
                result = DoBuild<TResult>(builderInput, aaContext);
                RevertGroupSettings(builderInput.AddressableSettings);
            }

            if (result != null)
                result.Duration = timer.Elapsed.TotalSeconds;

            return result;
        }


        private void ExcludeNotBuildTargetGroup(AddressableAssetSettings settings)
        {
            foreach (var group in settings.groups)
            {
                var bundledAssetGroupSchema = group.GetSchema<BundledAssetGroupSchema>();
                var separatedBuildGroupSchema = group.GetSchema<SeparatedBuildGroupSchema>();
                if (bundledAssetGroupSchema == null || separatedBuildGroupSchema == null)
                    continue;

                var buildGroup = separatedBuildGroupSchema.BuildGroup;
                m_SavedIncludeInBuildState.Add(group, bundledAssetGroupSchema.IncludeInBuild);

                // 一旦IncludeInBuildをfalseに設定する
                bundledAssetGroupSchema.IncludeInBuild = false;

                if (BuildTargetGroup == BuildGroupSeries.IncludedInAllGroup)
                {
                    // Debug以外をビルド対象とする
                    bundledAssetGroupSchema.IncludeInBuild = (buildGroup != BuildGroupSeries.Debug);
                    return;
                }
                
                if (BuildTargetGroup == BuildGroupSeries.Debug)
                {
                    // Debugも含めてすべてをビルド対象とする
                    bundledAssetGroupSchema.IncludeInBuild = true;
                    return;
                }
                
                // IncludedInAllGroup + GroupA or B + Debug (Debugフラグが立っているときのみ)
                // をビルド対象とする
                bundledAssetGroupSchema.IncludeInBuild =
                    (buildGroup == BuildTargetGroup) ||
                    (IsDebug && buildGroup == BuildGroupSeries.Debug) ||
                    (buildGroup == BuildGroupSeries.IncludedInAllGroup);
                return;
            }
        }
        
        private void RevertGroupSettings(AddressableAssetSettings settings)
        {
            foreach (var group in settings.groups)
            {
                var schema = group.GetSchema<BundledAssetGroupSchema>();
                if (schema == null || !m_SavedIncludeInBuildState.ContainsKey(group))
                    continue;
                schema.IncludeInBuild = m_SavedIncludeInBuildState[group];
            }

            m_SavedIncludeInBuildState.Clear();
        }
    }
}