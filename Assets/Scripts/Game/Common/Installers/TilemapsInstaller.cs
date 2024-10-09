using Common;
using Common.Tilemaps;
using Level;
using Tiles;
using UnityEngine;
using Zenject;

namespace Game.Common.Installers
{
    public class TilemapsInstaller : MonoInstaller
    {
        [SerializeField]
        private TilemapsProvider tilemapsProvider;

        [SerializeField]
        private TileLibraryData tileLibraryData;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<TilemapsProvider>().FromInstance(tilemapsProvider);
            Container.BindInterfacesTo<TilemapInputService>().AsSingle();
            Container.Bind<ITileLibrary>().To<TileLibraryData>().FromInstance(tileLibraryData);
        }
    }
}