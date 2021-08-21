using Source.View;

namespace Source.Model.PortArrivalEvents
{
    public class LiYuensTrustEvent: IPortArrivalEvent
    {
        public void Run(GameState state, IView view)
        {
            if (state.Random.Next(20) != 0) return;
            
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