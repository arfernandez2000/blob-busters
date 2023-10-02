using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CmdShoot : ICommand
{
    private IWand _wand;

    public CmdShoot(IWand wand) {
        _wand = wand;
    }

    public void Do() => _wand.Shoot();
}
