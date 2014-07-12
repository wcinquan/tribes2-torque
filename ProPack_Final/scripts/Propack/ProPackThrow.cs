// #category = ProPack
// #name = ProPack Throw
// #version = 3.01
// #status = Beta 3
// #date = July 4, 2001
// #warrior = Neofight
// #email = neofight@tribes2.org
// #web = http://propack.tribes2.org
// #description = Full-force repair kits, grenades, mines, drop grenades, and an option to beacon your mines!
// #Credit = Happy & Runar

	function ProKit(%val) {
		if (%val)
			throw(RepairKit);
	}

	function DropGrenades(%val) {
		if (%val) {
			throw(Grenade);
			throw(FlareGrenade);
			throw(FlashGrenade);
			throw(CameraGrenade);
			throw(ConcussionGrenade);
		}
	}

	function BeaconMine(%val) {
		if (%val) {
			use(mine);
			use(beacon);
			use(beacon);
		}
	}
			
	function ProGrenade(%val) {
		if(%val)
			commandToServer('endThrowCount');
		else
			commandToServer('startThrowCount');

		$mvTriggerCount4 += $mvTriggerCount4 & 1 == %val ? 2 : 1;
	}

	function ProMine(%val) {
		if (%val)
 			commandToServer( 'endThrowCount' );
		else
 			commandToServer( 'startThrowCount' );
		$mvTriggerCount5++;
	}

package ProPackThrow {
	// When you die the server clears the throw count, so we start it up again when we respawn.
	function updateActionMaps() {
        	parent::updateActionMaps();
		commandToServer( 'startThrowCount' );
	}
};

activatePackage(ProPackThrow);
