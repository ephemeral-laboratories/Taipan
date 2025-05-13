using Source.View;

namespace Source.Model.PortArrivalEvents
{
    public abstract class BasePortArrivalEvent
    {
        protected abstract bool ShouldRun(GameState state);
        
        protected abstract void Run(GameState state, IView view);

        public void MaybeRun(GameState state, IView view)
        {
            if (!ShouldRun(state)) return;
            Run(state, view);
        }
    }
}