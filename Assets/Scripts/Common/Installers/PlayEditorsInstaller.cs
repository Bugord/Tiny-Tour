using Common.Editors.Logistic;
using Common.Editors.Road;
using Common.Editors.Terrain;
using UnityEngine;
using Zenject;

namespace Common.Installers
{
    [CreateAssetMenu(fileName = "installer_play_editors", menuName = "Installers/Play/Editors Installer")]
    public class PlayEditorsInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            
        }
    }
}