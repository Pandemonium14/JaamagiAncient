using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Models.RelicPools;
using MegaCrit.Sts2.Core.Models.Relics;
using MegaCrit.Sts2.Core.Saves.Runs;

namespace JaamagiAncient.Relics;



[Pool(typeof(EventRelicPool))]
public class PreservedSoul : CustomRelicModel
{
    public override RelicRarity Rarity => RelicRarity.Ancient;

    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new CardsVar(3),
        new EnergyVar(3)
    ];
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.ForEnergy(this)];
    
    private bool _wasPreservedSoulUsed;
    
    [SavedProperty]
    public bool WasPreservedSoulUsed
    {
        get => _wasPreservedSoulUsed;
        set {
            AssertMutable();
            _wasPreservedSoulUsed = value;
            if (!_wasPreservedSoulUsed)
                return;
            Status = RelicStatus.Disabled;
        }
    }

    public override bool IsUsedUp => WasPreservedSoulUsed;
    
    public override bool ShouldDieLate(Creature creature)
    {
        return creature != Owner.Creature || WasPreservedSoulUsed;
    }

    public override async Task AfterPreventingDeath(Creature creature)
    {
        Flash();
        WasPreservedSoulUsed = true;
        await CreatureCmd.Heal(creature, creature.MaxHp);
        await PowerCmd.Apply<DrawCardsNextTurnPower>(Owner.Creature, DynamicVars.Cards.BaseValue, Owner.Creature, null);
        await PowerCmd.Apply<EnergyNextTurnPower>(Owner.Creature, DynamicVars.Energy.BaseValue, Owner.Creature, null);
    }
}