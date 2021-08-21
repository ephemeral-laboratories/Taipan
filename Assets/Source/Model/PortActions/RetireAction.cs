using Source.View;

namespace Source.Model.PortActions
{
    public class RetireAction: IPortAction
    {
        public void Run(GameState state, IView view)
        {
            view.ShowRetirement();
        }
    }
}