using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.RelicPools;
using MegaCrit.Sts2.Core.ValueProps;

namespace JaamagiAncient.Relics.Relics;

[Pool(typeof(EventRelicPool))]
public class IceTemperedIngot : CustomRelicModel
{
    public override RelicRarity Rarity => RelicRarity.Ancient;

    protected override IEnumerable<DynamicVar> CanonicalVars => [new BlockVar(12M, ValueProp.Unpowered)];

    private bool shouldTrigger = false;
    
    public override Task BeforeTurnEndVeryEarly(PlayerChoiceContext choiceContext, CombatSide side)
    {
        if (side != Owner.Creature.Side || Owner.Creature.Block > 0)
            return Task.CompletedTask;
        shouldTrigger = true;
        return Task.CompletedTask;
    }
    
    public override async Task BeforeTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
    {
        if (!shouldTrigger)
            return;
        shouldTrigger = false;
        Flash();
        Decimal num = await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block, null);
    }
}