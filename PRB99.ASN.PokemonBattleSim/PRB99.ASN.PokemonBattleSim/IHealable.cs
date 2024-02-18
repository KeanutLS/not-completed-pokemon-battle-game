using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRB99.ASN.PokemonBattleSim
{
    public interface IHealable
    {
        public string DoHeal(Pokemon user);
    }
}
