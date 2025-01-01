using UnityEngine;
using Zenject;

public class Engineer : MonoBehaviour
{
    [Inject] private DecorationState _decorationState;

    public void EnterToWorkbench()
    {

    }
}
