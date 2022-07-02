using UnityEditor.AddressableAssets.Build;
using UnityEditor.AddressableAssets.Build.DataBuilders;
using UnityEngine;

namespace CustomAASBuild.Editor
{
    [CreateAssetMenu(fileName = "GroupAWithDebugBuild.asset",
        menuName = "Addressables/Content Builders/GroupA With Debug Build")]
    public class GroupAWithDebugBuildScript : SeparatedBuildScriptBase
    {
        public override string Name => "GroupA With Debug Build Script";
        public override BuildGroupSeries BuildTargetGroup => BuildGroupSeries.GroupA;
        public override bool IsDebug => true;
    }
    
    [CreateAssetMenu(fileName = "GroupBWithDebugBuild.asset",
        menuName = "Addressables/Content Builders/GroupB With Debug Build")]
    public class GroupBWithDebugBuildScript : SeparatedBuildScriptBase
    {
        public override string Name => "GroupB With Debug Build Script";
        public override BuildGroupSeries BuildTargetGroup => BuildGroupSeries.GroupB;
        public override bool IsDebug => true;
    }
    
    [CreateAssetMenu(fileName = "GroupAReleaseBuild.asset",
        menuName = "Addressables/Content Builders/GroupA Release Build")]
    public class GroupAReleaseBuild : SeparatedBuildScriptBase
    {
        public override string Name => "GroupA Release Build Script";
        public override BuildGroupSeries BuildTargetGroup => BuildGroupSeries.GroupA;
        public override bool IsDebug => false;
    }
    
    [CreateAssetMenu(fileName = "GroupBReleaseBuild.asset",
        menuName = "Addressables/Content Builders/GroupB Release Build")]
    public class GroupBReleaseBuild : SeparatedBuildScriptBase
    {
        public override string Name => "GroupB Release Script";
        public override BuildGroupSeries BuildTargetGroup => BuildGroupSeries.GroupB;
        public override bool IsDebug => false;
    }
    
    [CreateAssetMenu(fileName = "AllGroupWithDebugBuild.asset",
        menuName = "Addressables/Content Builders/AllGroup With Debug Build")]
    public class AllGroupWithDebugBuild : SeparatedBuildScriptBase
    {
        public override string Name => "AllGroup With Debug Build Script";
        public override BuildGroupSeries BuildTargetGroup => BuildGroupSeries.Debug;
        public override bool IsDebug => true;
    }
    
    [CreateAssetMenu(fileName = "AllGroupReleaseBuild.asset",
        menuName = "Addressables/Content Builders/AllGroup Release Build")]
    public class AllGroupReleaseBuild : SeparatedBuildScriptBase
    {
        public override string Name => "AllGroup With Release Script";
        public override BuildGroupSeries BuildTargetGroup => BuildGroupSeries.IncludedInAllGroup;
        public override bool IsDebug => false;
    }
}
