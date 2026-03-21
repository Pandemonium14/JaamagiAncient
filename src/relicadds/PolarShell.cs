using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace JaamagiAncient.relicadds;


[Pool(typeof(EventCardPool))]
public class PolarShell : CustomCardModel
{
    public PolarShell() :
        base(1, CardType.Skill, CardRarity.Event, TargetType.Self, true)
    {
        
    }

    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new BlockVar(20M, ValueProp.Move),
        new PowerVar<BlurPower>(1M)
    ];

    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Innate, CardKeyword.Retain, CardKeyword.Exhaust];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block, cardPlay);
        await PowerCmd.Apply<BlurPower>(Owner.Creature, DynamicVars["BlurPower"].BaseValue, Owner.Creature, this);
    }

    protected override void OnUpgrade() => DynamicVars.Block.UpgradeValueBy(5M);
}