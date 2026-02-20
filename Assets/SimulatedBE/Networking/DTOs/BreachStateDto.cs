namespace SimulatedBE.Networking.DTOs
{
    public struct BreachStateDto
    {
        public string BreachState { get; private set; }

        public BreachStateDto(string breachState)
        {
            BreachState = breachState;
        }
        
    }
}