using Cars;
using Core;
using UnityEngine;

public class EditorSpawnPointView : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private CarLibrary carLibrary;
    
    public void SetVisual(CarType carType, TeamColor teamColor, Direction direction)
    {
        spriteRenderer.sprite = carLibrary.GetCarData(carType).visualsData[teamColor].directionSprites[direction];
    }
}
