using Source.View;

namespace Source.Model.PortArrivalEvents
{
    public interface IPortArrivalEvent
    {
        void Run(GameState state, IView view);
    }
}