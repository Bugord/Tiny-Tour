using System.Collections.Generic;
using Cars;
using Core;
using Game.Common.Cars.Data;
using Game.Common.Tiles.Data;
using Tiles;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Editor.CarGeneration
{
    public class CarGeneratorWindow : EditorWindow
    {
        private CarsData carsData;
        private TileLibraryData tileLibraryData;

        private const string DefaultTilesPath = "Assets/Tiles/Cars";
        private string path = UnityEngine.Application.dataPath + DefaultTilesPath;

        [MenuItem("Tools/Car Generator")]
        public static void ShowWindow()
        {
            GetWindow<CarGeneratorWindow>("Car Generator");
        }

        private void OnGUI()
        {
            GUILayout.Label("Car Generator", EditorStyles.boldLabel);

            carsData = EditorGUILayout.ObjectField("Car Data", carsData, typeof(CarsData), false) as CarsData;
            tileLibraryData =
                EditorGUILayout.ObjectField("Tile library Data", tileLibraryData, typeof(TileLibraryData), false) as
                    TileLibraryData;

            path = EditorGUILayout.TextField(path);
            if (GUILayout.Button("Select path")) {
                var selectedFolderPath = EditorUtility.OpenFolderPanel("Car tiles folder", DefaultTilesPath, "Cars");
                path = selectedFolderPath;
            }

            if (GUILayout.Button("Generate Car Tiles")) {
                GenerateCarTiles(path);
            }
        }

        private void GenerateCarTiles(string folderPath)
        {
            var shouldSaveToLibrary = tileLibraryData != null;
            var carSpawnPointsData = new List<CarSpawnPointData>();

            Debug.Log($"Generating car tiles in {folderPath}");
            foreach (var typeCarData in carsData.TypesData) {
                foreach (var colorCarData in typeCarData.Value.ColorData) {
                    foreach (var directionCarData in colorCarData.Value.DirectionData) {
                        var tile = CreateTile(typeCarData.Key, colorCarData.Key, directionCarData.Key,
                            directionCarData.Value);
                        if (shouldSaveToLibrary) {
                            carSpawnPointsData.Add(new CarSpawnPointData(typeCarData.Key, colorCarData.Key,
                                directionCarData.Key, tile));
                        }
                    }
                }
            }

            if (shouldSaveToLibrary) {
                tileLibraryData.SetCarSpawnPointsData(carSpawnPointsData);
            }

            AssetDatabase.SaveAssets();
        }

        private Tile CreateTile(CarType carType, TeamColor color, Direction direction, Sprite sprite)
        {
            var fileName = $"tile_car_{carType}_{color}_{direction}".ToLower();
            var tile = CreateInstance<Tile>();
            tile.name = fileName;
            tile.sprite = sprite;

            Debug.Log($"Generating {fileName} tile");
            AssetDatabase.CreateAsset(tile, $"{DefaultTilesPath}/{fileName}.asset");

            return tile;
        }
    }
}