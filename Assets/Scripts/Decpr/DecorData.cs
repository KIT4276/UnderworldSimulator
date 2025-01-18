using UnityEngine.SceneManagement;

public class DecorData
{
    public PositionOnLevel PositionOnLevel { get; private set; }
    public Decor ThisDecorComponent { get; private set; }

    public DecorData(Decor decor)
    {
        var posX = ThisDecorComponent.transform.position.x;
        var posY = ThisDecorComponent.transform.position.y;
        var posZ = ThisDecorComponent.transform.position.z;

        PositionOnLevel = new(SceneManager.GetActiveScene().name, new Vector3Data(posX, posY, posZ));
        ThisDecorComponent = decor;
    }
}
