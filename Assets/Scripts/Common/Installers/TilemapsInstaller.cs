using Common.Tilemaps;
using Level;
using Tiles;
using UnityEngine;
using Zenject;

namespace Common
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
            Container.BindInterfacesTo<TilemapInput>().AsSingle();
            Container.Bind<ITileLibrary>().To<TileLibraryData>().FromInstance(tileLibraryData);
        }
    }
}