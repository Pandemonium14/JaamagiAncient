using BaseLib.Abstracts;
using BaseLib.Utils;
using JaamagiAncient.relicadds;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Entities.RestSite;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.RelicPools;
using MegaCrit.Sts2.Core.Saves.Runs;
using MegaCrit.Sts2.Core.ValueProps;

namespace JaamagiAncient.Relics;



[Pool(typeof(EventRelicPool))]
public class FreezingFlame : CustomRelicModel
{
    public override RelicRarity Rarity => RelicRarity.Ancient;

    private int _timesFed;

    public override bool ShowCounter => true;

    public override int DisplayAmount => TimesFed;

    [SavedProperty]
    public int TimesFed
    {
        get => _timesFed;
        set
        {
            AssertMutable();
            _timesFed = value;
            DynamicVars.Block.BaseValue = value * 2M + 4M;
            InvokeDisplayAmountChanged();
        }
    }

    protected override IEnumerable<DynamicVar> CanonicalVars => [new BlockVar(3M, ValueProp.Unpowered)];

    public override async Task AfterSideTurnStart(CombatSide side, CombatState combatState)
    {
        if (side != CombatSide.Player)
            return;
        Flash();
        await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block, null);
    }
    
    public override bool TryModifyRestSiteOptions(Player player, ICollection<RestSiteOption> options)
    {
        if (player != Owner)
            return false;
        options.Add(new FeedFlameOption(player));
        return true;
    }
    
    
}