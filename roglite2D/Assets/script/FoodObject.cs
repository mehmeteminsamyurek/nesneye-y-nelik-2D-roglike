using UnityEngine;

public class FoodObject : CellObject // yemek nesnesi
{
    public int AmountGranted = 10;

    public override void PlayerEntered() // yemek yeme kodu
    {
        Destroy(gameObject);

        //increase food
        GameManager.Instance.ChangeFood(AmountGranted);
    }
}
