using Source.View;

namespace Source.Model.PortActions
{
    public interface IPortAction
    {
        void Run(GameState state, IView view);
    }
}