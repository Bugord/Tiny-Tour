using Cars;
using Core;
using UnityEngine;

public class EditorSpawnPointView : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private CarLibrary carLibrary;
    
    public void SetVisual(CarType carType, Team team, Direction direction)
    {
        spriteRenderer.sprite = carLibrary.GetCarData(carType).visualsData[team].directionSprites[direction];
    }
}
