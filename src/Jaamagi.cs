using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Utils;
using JaamagiAncient.Relics;
using JaamagiAncient.Relics.Relics;
using MegaCrit.Sts2.Core.Models;

namespace JaamagiAncient;

//[Pool(typeof(AncientEventModel))]
public class Jaamagi : CustomAncientModel
{
    protected override OptionPools MakeOptionPools => new OptionPools(MakePool([
        AncientOption<MeltingPermafrost>(),
        AncientOption<ColdSpark>(),
        AncientOption<IceTemperedIngot>(),
        AncientOption<SolidBlizzard>(),
        AncientOption<FreezingFlame>(),
        AncientOption<CrystalScale>(),
        AncientOption<PreservedSoul>(),
        AncientOption<FrostedFlakes>(),
        AncientOption<BrittleIceberg>()
    ]));

    public override bool IsValidForAct(ActModel act)
    {
        return act.ActNumber() == 2;
    }


}