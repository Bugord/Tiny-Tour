using Common.Editors.Logistic;
using Common.Editors.Road;
using Common.Editors.Terrain;
using UnityEngine;
using Zenject;

namespace Common.Installers
{
    [CreateAssetMenu(fileName = "installer_common_editors", menuName = "Installers/Common/Editors Installer")]
    public class EditorsInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IRoadEditor>().To<RoadEditor>().AsSingle();
            Container.Bind<ITerrainEditor>().To<TerrainEditor>().AsSingle();
            Container.Bind<IGoalEditor>().To<GoalEditor>().AsSingle();
        }
    }
}