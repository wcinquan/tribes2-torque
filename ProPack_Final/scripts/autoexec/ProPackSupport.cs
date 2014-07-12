// #hide
// #category = ProPack
// #name = ProPackSupport
// #credit = MeBaD, Neofight, PanamaJack

$String::asciiString = " !\"#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_`abcdefghijklmnopqrstuvwxyz{|}~";

// designed for quick access to change a Name -> clientid
function ProGhettoList() {
	cancel($updatingdalist);
	$updatingdalist = schedule(1000, 0, "doUpdateGhettoList");
}

function doUpdateGhettoList () {
	$ProTeamcount[1] = 0;
	$ProTeamcount[2] = 0;

	// get all names to clientID's
	for (%i = 0; %i < PlayerListGroup.getCount(); %i++) { // count starts @ 0
		$MyPlayerID[PlayerListGroup.getObject(%i).name] = PlayerListGroup.getObject(%i);
		$ProTeamCount[PlayerListGroup.getObject(%i).teamId]++;
	}
}

package ProPackSupport {
	function DispatchLaunchMode() {
		addMessageCallback('MsgClient', handleClientJoinTeam);
		addMessageCallback('MsgClientJoin', handleClientJoin);
		addMessageCallback('MsgClientJoinTeam', handleClientJoinTeam);
		addMessageCallback('MsgClientDrop', ProGhettoList);
		addMessageCallback('MsgForceObserver', ProGhettoList);

		parent::DispatchLaunchmode();
	}

	function handleClientJoin(%msgType, %msgString, %clientName, %clientId, %targetId, %isAI, %isAdmin, %isSuperAdmin, %isSmurf, %guid) {
		%clientId = detag(%clientId);
		%clientName = detag(%clientName);

		if (strstr(%msgString, "Welcome to Tribes2") != -1) {
		        $PPClientID = %clientId;
		        $PPName = StripNameColors(%clientName);
		}
		parent::handleClientJoin(%msgType, %msgString, %clientName, %clientId, %targetId, %isAI, %isAdmin, %isSuperAdmin, %isSmurf, %guid);
	}

	function handleClientJoinTeam(%msgType, %msgString, %clientName, %teamName, %clientId, %teamId) {
		%clientId = detag(%clientId);
		%teamId = detag(%teamId);

		if ($PPClientID == %clientId)
			$PPTeam = %teamId;

		ProGhettoList();

		parent::handleClientJoinTeam(%msgType, %msgString, %clientName, %teamName, %clientId, %teamId);
	}

	// From PJ's support
	function String::ascii(%string, %idx) {
		if(Strlen(%string) <= %idx || %idx < 0)
			return -1;
		%char = getSubStr(%string, %idx, 1);
		%idx = Strstr($String::asciiString, %char);
		if(%idx < 0)
			return -1;

		if(Strcmp(%char, getSubStr($String::asciiString, %idx, 1)) == 0)
			return %idx + 32;
		else
			return %idx + 64;
	}

	function String::char(%ascii) {
		if(%ascii < 32 || %ascii > 128)
			return "";
		else
			return getSubStr($String::asciiString, %ascii-32, 1);
	}

	function StripNameColors(%in) {
		%name = "";

		for (%i = 0; %i < Strlen(%in); %i++) {
			%ascii = String::ascii(%in, %i);
			if ((%ascii > 31) && (%ascii < 129))
				%name = %name @ String::char(%ascii);
		}

		%name = %name @ "";
		return %name;
	}

	function StripAll(%in) {
		%name = "";

		for (%i = 0; %i < Strlen(%in); %i++) {
			%ascii = String::ascii(%in, %i);
			if (((%ascii > 47) && (%ascii < 58)) || ((%ascii > 64) && (%ascii < 91)) || ((%ascii > 96) && (%ascii < 123)))
				%name = %name @ String::char(%ascii);
		}

		%name = %name @ "";
		return %name;
	}
	
	// For weapon tracking--maybe one day this will be unnecessary...
	function ProNoSpacedWeaponName(%num) {
		// Edit this if you are a Modder
		%NoSpace[1] = "Blaster";
		%NoSpace[2] = "Plasma";
		%NoSpace[3] = "Chaingun";
		%NoSpace[4] = "Disc";
		%NoSpace[5] = "Grenade";
		%NoSpace[6] = "Laser";
		%NoSpace[7] = "Elf";
		%NoSpace[8] = "Mortar";
		%NoSpace[9] = "Missile";
		%NoSpace[10] = "Shocklance";
		%NoSpace[11] = "Mine";
		%NoSpace[12] = "Explosion";
		%NoSpace[13] = "Impact";
		%NoSpace[14] = "Ground";
		%NoSpace[15] = "Turret";
		%NoSpace[16] = "PlasmaTurret";
		%NoSpace[17] = "AATurret";
		%NoSpace[18] = "ElfTurret";
		%NoSpace[19] = "MortarTurret";
		%NoSpace[20] = "MissileTurret";
		%NoSpace[21] = "ClampTurret";
		%NoSpace[22] = "SpikeTurret";
		%NoSpace[23] = "SentryTurret";
		%NoSpace[24] = "OutOfBounds";
		%NoSpace[25] = "lava";
		%NoSpace[26] = "ShrikeBlaster";
		%NoSpace[27] = "BellyTurret";
		%NoSpace[28] = "Bomberbomb";
		%NoSpace[29] = "TankChaingun";
		%NoSpace[30] = "TankMortar";
		%NoSpace[31] = "SatchelCharge";
		%NoSpace[32] = "MPBMissile";
		%NoSpace[33] = "Lightning";
		%NoSpace[34] = "VehicleSpawn";
		%NoSpace[35] = "ForceField";
		%NoSpace[36] = "Crash";
		%NoSpace[98] = "NexusCamping";
		%NoSpace[99] = "Suicide";
		%NoSpace[201] = "Grid";

		return %NoSpace[%num];
	}
};
activatePackage(ProPackSupport);