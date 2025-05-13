using Source.View;

namespace Source.Model.PortArrivalEvents
{
    public class LiYuensTrustEvent: BasePortArrivalEvent
    {
        protected override bool ShouldRun(GameState state) => state.Random.Next(20) == 0;

        protected override void Run(GameState state, IView view)
        {
            if (state.LiYuensTrust > 0)
            {
                state.LiYuensTrust++;
            }

            if (state.LiYuensTrust == 4)
            {
                state.LiYuensTrust = 0;
            }
        }
    }
}