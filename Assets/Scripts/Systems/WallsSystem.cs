using UnityEngine;

public class WallsSystem
{
    private GameObject[] _smallWalls;
    private GameObject[] _bigWalls;
    private GameObject _roof;

    public void InitWithWalls(GameObject[] smallWalls, GameObject[] bigWalls, GameObject roof)
    {
        _smallWalls = new GameObject[smallWalls.Length];
        _bigWalls = new GameObject[bigWalls.Length];

        _smallWalls = smallWalls;
        _bigWalls = bigWalls;
        _roof = roof;

        //Debug.Log("InitWithWalls");
        SwitchToRoof();
    }

    public void SwitchToBig()
    {
        if (_smallWalls == null || _bigWalls == null || _roof == null)
        {
            //Debug.Log("WallsSystem Not Inited!");
            return;
        }

        SwitchGameObjects(_smallWalls, _bigWalls,false);
    }

    public void SwitchToSmall()
    {
        if (_smallWalls == null || _bigWalls == null || _roof == null)
        {
            //Debug.Log("WallsSystem Not Inited!");
            return;
        }

        SwitchGameObjects(_bigWalls , _smallWalls, false);
    }

    public void SwitchToRoof()
    {
        if (_smallWalls == null || _bigWalls == null || _roof == null)
        {
           // Debug.Log("WallsSystem Not Inited!");
            return;
        }
        SwitchGameObjects(_smallWalls, _bigWalls, true);
    }

    private void SwitchGameObjects(GameObject[] offWalls, GameObject[] onWalls, bool isRoof)
    {
        foreach (var wall in offWalls)
        {
            wall.SetActive(false);
        }
        foreach (var wall in onWalls)
        {
            wall.SetActive(true);
        }

        if (isRoof)
        {
            _roof.SetActive(true);
        }
        else
        {
            _roof.SetActive(false);
        }
    }
}
