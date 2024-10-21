using Common.Editors;
using Common.Editors.Terrain;
using Game.Common.Editors.Goals;
using Zenject;

namespace Game.Common.Installers
{
    public class BaseEditorOptionsInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<TerrainEditor>().AsCached();
            Container.BindInterfacesTo<ObstaclesEditor>().AsCached();
            Container.BindInterfacesTo<GoalLevelEditor>().AsCached();
        }
    }
}