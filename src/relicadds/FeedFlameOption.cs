using JaamagiAncient.Relics;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.RestSite;

namespace JaamagiAncient.relicadds;

public class FeedFlameOption : RestSiteOption
{
    private FreezingFlame relic;
    
    public FeedFlameOption(Player owner) : base(owner)
    {
        relic = owner.GetRelic<FreezingFlame>();
    }

    public override Task<bool> OnSelect()
    {
        if (relic != null)
        {
            relic.TimesFed++;
        }
        return Task.FromResult(true);
    }

    public override string OptionId => "FEEDFLAME";
}