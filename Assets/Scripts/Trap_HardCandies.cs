using UnityEngine;
using System.Collections;

public class Trap_HardCandies : Trap {

	public override void Spring (Movement target)
	{
        interruptMovement = false;
        changeDir = true;
		if(target == null || rect == null) return;
		base.Spring (target);
		if(target.position == rect.anchoredPosition) {
			target.trap = this;
		}
	}

	public override void HandleTrap (Movement target) {
		base.HandleTrap (target);
	}
}
