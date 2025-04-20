using UnityEngine;

public class WallsSystem
{
    private GameObject[] _smallWalls;
    private GameObject[] _bigWalls;
    private GameObject[] _roof;


    public void InitWithWalls(GameObject[] smallWalls, GameObject[] bigWalls, GameObject[] roof)
    {
        _smallWalls = new GameObject[smallWalls.Length];
        _bigWalls = new GameObject[bigWalls.Length];
        _roof = new GameObject[roof.Length];

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

        SwitchGameObjects(null, _bigWalls,false);
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
        if (_smallWalls == null || _bigWalls == null || _roof == null )
        {
           // Debug.Log("WallsSystem Not Inited!");
            return;
        }
        SwitchGameObjects(null, _bigWalls, true);
    }

    private void SwitchGameObjects(GameObject[] offWalls, GameObject[] onWalls, bool isRoof)
    {
        if (offWalls != null)
        {
            foreach (var wall in offWalls)
            {
                wall.SetActive(false);
            }
        }

        foreach (var wall in onWalls)
        {
            wall.SetActive(true);
        }

        if (isRoof)
        {
            foreach(var roof in _roof)
            roof.SetActive(true);
        }
        else
        {
            foreach (var roof in _roof)
                roof.SetActive(false);
        }
    }
}
