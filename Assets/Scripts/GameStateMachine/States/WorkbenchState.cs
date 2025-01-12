public class WorkbenchState : IState
{
    private readonly SpaceDeterminantor _spaceDeterminantor;

    public WorkbenchState(SpaceDeterminantor spaceDeterminantor)
    {
        _spaceDeterminantor = spaceDeterminantor;
    }


    public void Enter()
    {
        _spaceDeterminantor.StartFind();
    }

    public void Exit()
    {
        
    }
}