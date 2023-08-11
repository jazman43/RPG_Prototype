using System.Collections.Generic;

namespace RPG.Progression
{
    public interface IModProvider
    {
        IEnumerable<float> GetAdditiveMod(Stats stats);

        IEnumerable<float> GetPercentageMod(Stats stats);
    }
}

