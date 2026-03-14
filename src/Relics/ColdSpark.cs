using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.RelicPools;
using MegaCrit.Sts2.Core.ValueProps;

namespace JaamagiAncient.Relics.Relics;


[Pool(typeof(EventRelicPool))]
public class ColdSpark : CustomRelicModel
{
    
    public override RelicRarity Rarity => RelicRarity.Ancient;
    
    private bool usedThisTurn = false;


    public override Task AfterBlockGained(Creature creature, decimal amount, ValueProp props, CardModel? cardSource)
    {
        if (usedThisTurn || creature.Block < 15) return Task.CompletedTask;
        usedThisTurn = true;
        Flash();
        PlayerCmd.GainEnergy(1, Owner);
        return Task.CompletedTask;
    }

    public override Task AfterPlayerTurnStartEarly(PlayerChoiceContext choiceContext, Player player)
    {
        usedThisTurn = false;
        return Task.CompletedTask;
    }
}