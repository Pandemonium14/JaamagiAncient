using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Models.RelicPools;
using MegaCrit.Sts2.Core.ValueProps;

namespace JaamagiAncient.Relics.Relics;


[Pool(typeof(EventRelicPool))]
public class ColdSpark : CustomRelicModel
{
    
    public override string PackedIconPath => "res://JaamagiAncient/RelicImages/small/ColdSpark.png";

    protected override string PackedIconOutlinePath => "res://JaamagiAncient/RelicImages/outline/ColdSpark.png";

    protected override string BigIconPath => "res://JaamagiAncient/RelicImages/large/ColdSpark.png";
    
    public override RelicRarity Rarity => RelicRarity.Ancient;
    
    //private bool usedThisTurn = false;

    protected override IEnumerable<DynamicVar> CanonicalVars => [new EnergyVar(1)];

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.ForEnergy(this)];


    public override async Task AfterTurnEndLate(PlayerChoiceContext choiceContext, CombatSide side)
    {
        if (side == CombatSide.Player && Owner.Creature.Block >= 15)
        {
            await PowerCmd.Apply<EnergyNextTurnPower>(Owner.Creature, 1M, Owner.Creature, null);
        }
    }


    //old design
    /*public override Task AfterBlockGained(Creature creature, decimal amount, ValueProp props, CardModel? cardSource)
    {
        if (usedThisTurn || creature.Block < 15) return Task.CompletedTask;
        usedThisTurn = true;
        Flash();
        PlayerCmd.GainEnergy(1, Owner);
        return Task.CompletedTask;
    }*/

    /*public override Task AfterPlayerTurnStartEarly(PlayerChoiceContext choiceContext, Player player)
    {
        usedThisTurn = false;
        return Task.CompletedTask;
    }*/
}