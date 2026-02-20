namespace SimulatedBE.Networking.Tags
{
    public class Tag_Sacrifice : ITag
    {
        private IJsonFacade _jsonFacade;

        public Tag_Sacrifice(IJsonFacade jsonFacade)
        {
            _jsonFacade = jsonFacade;
        }

        public void ApplyTag(TagManager tagManager)
        {
            if (CheckCondition())
            {
                var database = _jsonFacade.Database;
                database.DiscardsAmmount += 1;
                _jsonFacade.Database = database;
            }
        }

        private bool CheckCondition()
        {
            var breaches = _jsonFacade.GameBreaches;

            for (int i = breaches.Count - 1; i >= 0; i--)
            {
                if (breaches[i].BreachState == SimulatedBE.Networking.BreachState.Skipped)
                {
                    if (i - 1 >= 0 && breaches[i].BreachState == SimulatedBE.Networking.BreachState.Skipped)
                        return true;

                    break;
                }
            }

            return false;
        }

        public string GetDynamicDescription()
        {
            return "";
        }
    }
}