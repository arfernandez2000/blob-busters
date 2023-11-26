using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CmdTeleport : ICommand
{
    private IWand _wand;

    public CmdTeleport(IWand wand) {
        _wand = wand;
    }

    public void Do() {
        _wand.Teleport();
    }
}
