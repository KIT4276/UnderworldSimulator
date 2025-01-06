using Zenject;

public class Engineer : InteractableObstacle
{
    [Inject] private WorkbenchSystem _workbenchSystem;

    protected override void Interac()
    {
        _sign.SetActive(false);
        _workbenchSystem.ActivateWorkbench();
    }
}
