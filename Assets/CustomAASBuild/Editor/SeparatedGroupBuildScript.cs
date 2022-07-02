using System.Diagnostics;
using UnityEditor.AddressableAssets.Build;
using UnityEditor.AddressableAssets.Build.DataBuilders;
using UnityEditor.AddressableAssets.Settings;
using UnityEngine;

namespace CustomAASBuild.Editor
{
    [CreateAssetMenu(fileName = "CustomBuild.asset", menuName = "Addressables/Content Builders/Separated Group Build")]
    public class SeparatedGroupBuildScript : BuildScriptBase
    {
        public override string Name => "Separated Group Build Script";

        /// <summary>
        /// このビルダーが、型Tのデータをビルドできるか判断するために使われる
        /// </summary>
        /// <typeparam name="T">The type of data needed to be built.</typeparam>
        /// <returns>True if this builder can build this data.</returns>
        public override bool CanBuildData<T>()
        {
            return typeof(T).IsAssignableFrom(typeof(AddressablesPlayerBuildResult));
        }

        /// <summary>
        /// 引数として与えられたBuildInputを使って指定されたデータをビルドする。
        /// </summary>
        /// <param name="builderInput">ビルドするデータの種類</param>
        /// <typeparam name="TResult">ビルドに使用するBuildInputオブジェクト</typeparam>
        /// <returns>ビルド結果</returns>
        protected override TResult BuildDataImplementation<TResult>(AddressablesDataBuilderInput builderInput)
        {
            TResult result = default(TResult);

            var timer = new Stopwatch();
            timer.Start();

            timer.Stop();

            return result;
        }

        private void PreprocessAddressableBuild(AddressableAssetSettings settings)
        {
            foreach (var group in settings.groups)
            {
                var groupName = group.Name;
                
            }
        }
    }
}