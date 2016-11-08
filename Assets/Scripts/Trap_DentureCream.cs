using UnityEngine;
using System.Collections;

public class Trap_DentureCream : Trap {

	public override void Spring (Movement target)
	{
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
