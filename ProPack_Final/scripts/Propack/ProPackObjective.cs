 // #category = ProPack
// #name = ProPack Objective
// #version = 6.99
// #description = Simple, small, transparent, information-rich, non-ghey objectives HUD.
// #status = Beta 2
// #date = 11/14/2001
// #warrior = MeBaD
// #credit = Neofight
// #web = http://propack.tribes2.org

// Positions
$ProScorePOS[1280] = "1095 42";
$ProScorePOS[1152] = "963 42";
$ProScorePOS[1024] = "847 42";
$ProScorePOS[800] = "617 42";

// Arena Support information
$ArenaSupport::LocalVersion = 1.0;
$ArenaSupport::RemoteVersion = 0;
$ArenaSupport::TeamCount = 2;

package ProPackObjective {

	//***************************************//
	//     The One and Only ObjectiveHUD     //
	//***************************************//

	function teamScoreIs(%msgType, %msgString, %a1, %a2, %a3, %a4, %a5, %a6) {
		parent::teamScoreIs(%msgType, %msgString, %a1, %a2, %a3, %a4, %a5, %a6);

		%teamNum = detag(%a1);
		%score = detag(%a2);
		if(%score $= "") %score = 0;
		UpdateProScore(%teamNum, %score);
	}

	function YourScoreIs(%msgType, %msgString, %a1, %a2, %a3, %a4, %a5, %a6) {
		parent::YourScoreIs(%msgType, %msgString, %a1, %a2, %a3, %a4, %a5, %a6);

		$ProScoreis = detag(%a1);
		$ProScore[1] = $ProScoreis;
		$ProScore[2] = "";

		UpdateProScore(1);
	}

	function ClientCmdDisplayHuds() {
		switch$ ($HudMode) {
		    case "Pilot":
		      ProHudStatus.setVisible($ProPackPrefs::StatusHudActive);
		      ProHudScore.setVisible($ProPackPrefs::ScoreHudActive);
		    case "Passenger":
		      ProHudStatus.setVisible($ProPackPrefs::StatusHudActive);
		      ProHudScore.setVisible($ProPackPrefs::ScoreHudActive);
		    case "Object":
		      ProHudStatus.setVisible($ProPackPrefs::StatusHudActive);
		      PPTR2CarrierHealth.setVisible($ProPackPrefs::StatusHudActive);
		      ProHudScore.setVisible($ProPackPrefs::ScoreHudActive);
		    case "Observer":
		      ProHudStatus.setVisible(false);
		      PPTR2CarrierHealth.setVisible(false);
		      ProHudScore.setVisible($ProPackPrefs::ScoreHudActive);
		    case "PickTeam":
		      ProHudStatus.setVisible(false);
		      PPTR2CarrierHealth.setVisible(false);
		      ProHudScore.setVisible($ProPackPrefs::ScoreHudActive);
		    case "SiegeHalftime":
		      ProHudStatus.setVisible(false);
		      ProHudScore.setVisible(false);
		    default:
		      ProHudStatus.setVisible(false);
		      PPTR2CarrierHealth.setVisible(false);
		      ProHudScore.setVisible(false);
		  }
		parent::ClientCmdDisplayHuds();
	}

	function setupObjHud(%gameType) {
		for (%i = 1; %i < 3; %i++) {
			$ProTeam[%i] = "";
			$ProScore[%i] = "";
			$ProStatus[%i] = "";
		}
		$ProScoreis = "";
		$ProSingleScore = "";
		$ProDMKills = "";
		$ProDMDeaths = "";
		$ProSiegeTeamCheck = 0;

		switch$ (%gameType) {
			case BountyGame:
				$ProSingleScore = "Score: ";
			case RabbitGame:
				$ProSingleScore = "Score: ";
			case DMGame:
				$ProSingleScore = "Score: ";
			case SiegeGame:
				$ProSingleScore = "->";
				$ProSiegeTeamCheck = 1;
			case HuntersGame:
				$ProSingleScore = "Score: ";
			case TeamRabbitGame:
				// just for grins
			case ArenaGame:
				// just for grins
			case TR2Game:
				// just for grins
		}
		echo("Overriding gametype for ProPackObjectiveHud ... " @ %gameType);
		UpdateProStatus();

		parent::setupObjHud(%gameType);
	}

	//---------------------------------------------------------------
	// Handle all hunters Events
	//---------------------------------------------------------------
	function huntAddTeam(%msgType, %msgString, %a1, %a2, %a3, %a4, %a5, %a6) {
		parent::huntAddTeam(%msgType, %msgString, %a1, %a2, %a3, %a4, %a5, %a6);
		%teamNum = detag(%a1);
		%teamName = detag(%a2);
		%score = detag(%a3);

		$ProTeam[%teamNum] = %teamName;
		$ProScore[%teamNum] = %score;
		UpdateProScore();
	}

	function huntYouHaveFlags(%msgType, %msgString, %a1, %a2, %a3, %a4, %a5, %a6) {
		parent::huntYouHaveFlags(%msgType, %msgString, %a1, %a2, %a3, %a4, %a5, %a6);
		%numFlags = detag(%a1);
		$ProStatus[1] = "<color:FFFF00>Flags: " @ %numFlags;

		%FlagValue = 0;
		for (%i = %numFlags; %i > 0; %i--) {
			%FlagValue = %FlagValue + %i;
		}

		$ProStatus[2] = "<color:00FF00>Value: " @ %FlagValue;
		UpdateProStatus();
	}

	//---------------------------------------------------------------
	// Handle all Siege Events
	//---------------------------------------------------------------
	function siegeAddTeam(%msgType, %msgString, %a1, %a2, %a3, %a4, %a5, %a6) {
		parent::siegeAddTeam(%msgType, %msgString, %a1, %a2, %a3, %a4, %a5, %a6);

		%teamNum = detag(%a1);
		if(detag(%a3))
			%role = "CAPTURE";
		else
			%role = "PROTECT";

		if ($PPTeam == %teamNum) {
			$ProScore[1] = %role;
			$ProScore[2] = "";
			UpdateProScore(1);
		}
	}

	function siegeRolesSwitched(%msgType, %msgString, %a1, %a2, %a3, %a4, %a5, %a6) {
		parent::siegeRolesSwitched(%msgType, %msgString, %a1, %a2, %a3, %a4, %a5, %a6);
		%newOff = detag(%a2);
		%newDef = %newOff == 1 ? 2: 1;

		if ($PPTeam == %newOff) {
			$ProScore[1] = "CAPTURE";
		} else if ($PPTeam == %newDef) {
			$ProScore[1] = "PROTECT";
		} else {
			$ProScore[1] = "";
		}
		$ProScore[2] = "";
		UpdateProScore(1);
	}

	function ProSiegeRoleSwitch(%msgType, %msgString, %a1, %a2, %a3, %a4, %a5, %a6) {
		if ($ProSiegeTeamCheck) {
			if (Strstr($ProScore[1], PROTECT) != -1) {
				$ProScore[1] = "CAPTURE";
			} else {
				$ProScore[1] = "PROTECT";
			}
			$ProScore[2] = "";
			UpdateProScore(1);
		}
	}

	//---------------------------------------------------------------
	// Handle all Wabbit Events
	//---------------------------------------------------------------
	function rabbitFlagTaken(%msgType, %msgString, %a1, %a2, %a3, %a4, %a5, %a6) {
		parent::rabbitFlagTaken(%msgType, %msgString, %a1, %a2, %a3, %a4, %a5, %a6);

		%bunny = StripNameColors(detag(%a1));

		if ( %bunny $= detag($PPName) ) {
			$ProStatus[1] = "<font:Univers bold:16><color:00FF00>RUN!!!";
		} else {
			$ProStatus[1] = %bunny;
		}
		UpdateProStatus();
	}

	function rabbitFlagDropped(%msgType, %msgString, %a1, %a2, %a3, %a4, %a5, %a6) {
		parent::rabbitFlagDropped(%msgType, %msgString, %a1, %a2, %a3, %a4, %a5, %a6);

		$ProStatus[1] = "<color:FFFF00>< Dropped >";
		UpdateProStatus();
	}

	function rabbitFlagReturned(%msgType, %msgString, %a1, %a2, %a3, %a4, %a5, %a6) {
		parent::rabbitFlagReturned(%msgType, %msgString, %a1, %a2, %a3, %a4, %a5, %a6);

		$ProStatus[1] = "<color:00FF00>< Home >";
		UpdateProStatus();
	}

	function rabbitFlagStatus(%msgType, %msgString, %a1, %a2, %a3, %a4, %a5, %a6) {
		parent::rabbitFlagStatus(%msgType, %msgString, %a1, %a2, %a3, %a4, %a5, %a6);

		%flagStatus = StripNameColors(detag(%a1));
		$ProStatus[1] = %flagStatus;
		UpdateProStatus();
	}

	//---------------------------------------------------------------
	// Handle all Team Wabbit Events: Neo's adaptation of the release from the mod author, Juno
	//---------------------------------------------------------------
	function teamRabbitAddTeam(%msgType, %msgString, %a1, %a2, %a3, %a4, %a5, %a6) {
		%teamNum = detag(%a1);
		%teamName = detag(%a2);
		%flagStatus = detag(%a3);
		%score = detag(%a4);

		$ProScore[%teamNum] = "";
		$ProStatus[%teamNum] = "";

		$ProScore[%teamNum] = %score;
		$ProTeam[%teamNum] = %teamName;
		$ProStatus[%teamNum] = %flagStatus;

		UpdateProScore(%teamNum, %score);
		UpdateProStatus();
	}

	function teamRabbitFlagTaken(%msgType, %msgString, %a1, %a2, %a3, %a4, %a5, %a6) {
		%index = detag(%a3);
		%bunny = detag(%a4);
		
		if ( %bunny $= detag($PPName) ) {
			%bunny = "<font:Univers bold:16><color:00FF00>RUN!!!";
		}

		switch (%index) {
			case 1:
			 $ProStatus[1] = "";
			 $ProStatus[2] = %bunny;

			case 2:
			 $ProStatus[1] = %bunny;
			 $ProStatus[2] = "";
		}
		UpdateProStatus();
	}

	function teamRabbitFlagDropped(%msgType, %msgString, %a1, %a2, %a3, %a4, %a5, %a6) {
		%index = detag(%a2);

		switch (%index) {
			case 1:
			 $ProStatus[1] = "";
			 $ProStatus[2] = "<color:FFFF00><Dropped>";

			case 2:
			 $ProStatus[1] = "<color:FFFF00><Dropped>";
			 $ProStatus[2] = "";
		}
		UpdateProStatus();
	}

	function teamRabbitFlagReturned(%msgType, %msgString, %a1, %a2, %a3, %a4, %a5, %a6) {
		$ProStatus[1] = "";
		$ProStatus[2] = "";
		UpdateProStatus();
	}

	function teamRabbitFlagStatus(%msgType, %msgString, %a1, %a2, %a3, %a4, %a5, %a6) {
		%flagStatus = StripNameColors(detag(%a1));
		%team = detag(%a2);

		switch (%team) {
			case 1:
			 $ProStatus[1] = "";
			 $ProStatus[2] = %flagStatus;

			case 2:
			 $ProStatus[1] = %flagStatus;
			 $ProStatus[2] = "";
		}
		UpdateProStatus();
	}

	//---------------------------------------------------------------
	// TR2 Events de Neo
	//---------------------------------------------------------------	
	function TR2HudInit(%msgType, %msgString, %team1, %team2, %score1, %score2, %flagLoc, %carrierHealth, %currentBonus) {
		parent::TR2HudInit(%msgType, %msgString, %team1, %team2, %score1, %score2, %flagLoc, %carrierHealth, %currentBonus);

		%team1Name = detag(%team1);
		%team2Name = detag(%team2);
		%team1Score = detag(%score1);	
		%team2Score = detag(%score2);	
		%flagStatus = detag(%flagLoc);
		
		if(%team1Score $= "") %team1Score = 0;
		if(%team2Score $= "") %team2Score = 0;

		// Set the values
		$ProTeam[1] = %team1Name;
		$ProTeam[2] = %team2Name;
		$ProScore[1] = %team1Score;
		$ProScore[2] = %team2Score;
		$ProStatus[1] = "<color:FFFF00>" @ %flagStatus;

		// Update the huds
		UpdateProScore(1, %team1Score);
		UpdateProScore(2, %team2Score);
		UpdateProStatus();
	}
	
	function TR2FlagTaken (%msgType, %msgString, %client, %team, %flagteam, %clientnamebase) {
		%bunny = StripNameColors(detag(%client));
		
		if ($IHaveFlag) {
			$ProStatus[1] = "<font:Univers bold:16><color:00FF00>RUN!!!";
			PPTR2CarrierHealth.setVisible(false);
		} else if (StrStr(%msgString, "Teammate") != -1) {
			$ProStatus[1] = "<color:33FF33>" @ %bunny;
			PPTR2CarrierHealth.profile.fillColor = "0 255 0";
			PPTR2CarrierHealth.setVisible(true);
		} else {
			$ProStatus[1] = "<color:FF3333>" @ %bunny;
			PPTR2CarrierHealth.profile.fillColor = "255 0 0";
			PPTR2CarrierHealth.setVisible(true);
		}
		UpdateProStatus();
	}
	
	function handleTR2FlagDropped(%msgType, %msgString, %client, %team, %flagteam) {
		parent::handleTR2FlagDropped(%msgType, %msgString, %client, %team, %flagteam);
		
		$ProStatus[1] = "<color:FFAA33><Dropped>";
		PPTR2CarrierHealth.setVisible(false);
		UpdateProStatus();
	}
	
	function handleTR2FlagStatus(%msgType, %msgString, %location) {
		parent::handleTR2FlagStatus(%msgType, %msgString, %location);
		
		%location = detag(%location);
		$ProStatus[1] = "<color:FFFF00>" @ %location;
		PPTR2CarrierHealth.setVisible(false);
		UpdateProStatus();
	}
	
	function handleTR2CarrierHealth(%msgType, %msgString, %amt, %team) {
		parent::handleTR2CarrierHealth(%msgType, %msgString, %amt, %team);
		
		PPTR2CarrierHealth.setValue(%amt);
	}
	
	function handleTR2SetScore(%msgType, %msgString, %team, %score) {
		parent::handleTR2SetScore(%msgType, %msgString, %team, %score);
		
		%team = detag(%team);
		%score = detag(%score);
		$ProScore[%team] = %score;
		UpdateProScore(%team);
	}

	//---------------------------------------------------------------
	// Handle all Death Match Events
	//---------------------------------------------------------------
	function dmKill(%msgType, %msgString, %a1, %a2, %a3, %a4, %a5, %a6) {
		parent::dmKill(%msgType, %msgString, %a1, %a2, %a3, %a4, %a5, %a6);

		$ProDMKills = detag(%a1);
		$ProScore[1] = "<color:00FF00>" @ $ProDMKills @ "<color:FFFF00> / <color:FF0000>" @ $ProDMDeaths @ "<color:FFFF00> " @ $ProScoreis;
		UpdateProScore(1);
	}

	function dmPlayerDies(%msgType, %msgString, %a1, %a2, %a3, %a4, %a5, %a6) {
		parent::dmPlayerDies(%msgType, %msgString, %a1, %a2, %a3, %a4, %a5, %a6);

		$ProDMDeaths = detag(%a1);
		$ProScore[1] = "<color:00FF00>" @ $ProDMKills @ "<color:FFFF00> / <color:FF0000>" @ $ProDMDeaths @ "<color:FFFF00> " @ $ProScoreis;
		UpdateProScore(1);
	}

	//---------------------------------------------------------------
	// Handle All Arena Events: Neo's adaptation of the 1.0 release from the mod author, Teribaen
	//---------------------------------------------------------------
	function arenaVersionMsg( %msgType, %msgString, %a1, %a2, %a3, %a4, %a5, %a6 ) {
		%version = detag(%a1);
		%versionString = detag(%a2);

		$ArenaSupport::RemoteVersion = %version;

		commandToServer( 'ArenaSupportHello', $ArenaSupport::LocalVersion );
	}

	function arenaServerState( %msgType, %msgString, %a1, %a2, %a3, %a4, %a5, %a6 ) {
		%teamCount = detag(%a1);
		$ArenaSupport::TeamCount = %teamCount;
	}

	function arenaAddTeam( %msgType, %msgString, %a1, %a2, %a3, %a4, %a5, %a6 ) {
		%teamNum = detag(%a1);
		if ( %teamNum > 2 ) return;

		%teamName = detag(%a2);
		%score = detag(%a3);
		if(%score $= "") %score = 0;

		%ProaliveCount = detag(%a4);
		%PrototalCount = detag(%a5);
		if(%ProaliveCount $= "") %ProaliveCount = 0;
		if(%PrototalCount $= "") %PrototalCount = 0;

		$ProScore[%teamNum] = "";
		$ProStatus[%teamNum] = "";

		if ( $ArenaSupport::TeamCount == 2 ) {
			$ProTeam[%teamNum] = %teamName;
			$ProScore[%teamNum] = %score;
			// switch the status lines for Arena so it is more intuitive
			if(%teamNum == 1) %teamNum = 2;
			else if (%teamNum == 2) %teamNum = 1;
			$ProStatus[%teamNum] = %ProaliveCount @ "/" @ %PrototalCount;
		}
		UpdateProStatus();
	}

	// Update the alive/total player count for a team in the status hud
	function arenaTeamState(%msgType, %msgString, %a1, %a2, %a3, %a4, %a5, %a6) {
		%teamNum = detag(%a1);

		if ( %teamNum > 2 ) return;

		%ProaliveCount = detag(%a2);
		%PrototalCount = detag(%a3);

		if(%ProaliveCount $= "") %ProaliveCount = 0;
		if(%PrototalCount $= "") %PrototalCount = 0;

		// Switch the status lines for arena so it is more intuitive
		// Display alive/total counts for the teams
		if ( $ArenaSupport::TeamCount == 2 ) {
			if(%teamNum == 1) %teamNum = 2;
			else if (%teamNum == 2) %teamNum = 1;
			$ProStatus[%teamNum] = %ProaliveCount @ "/" @ %PrototalCount;
		}
		UpdateProStatus();
	}

	//---------------------------------------------------------------
	// Handle all Cap and Hold Events
	//---------------------------------------------------------------
	function cnhAddTeam(%msgType, %msgString, %a1, %a2, %a3, %a4, %a5, %a6) {
		parent::cnhAddTeam(%msgType, %msgString, %a1, %a2, %a3, %a4, %a5, %a6);

		%teamNum = detag(%a1);
		%teamName = detag(%a2);
		%score = detag(%a3);
		if(%score $= "") %score = 0;
		%sLimit = detag(%a4);

		$ProStatus[%teamNum] = "Held : " @ detag(%a5);
		$ProTeam[%teamNum] = %teamName;
		$ProScore[%teamNum] = "+" @ (%sLimit - %score);

		UpdateProScore(%teamNum, $ProScore);
		UpdateProStatus();
	}

	function hudFlipFlopsHeld(%msgType, %msgString, %a1, %a2, %a3, %a4, %a5, %a6) {
		parent::hudFlipFlopsHeld(%msgType, %msgString, %a1, %a2, %a3, %a4, %a5, %a6);

		%teamNum = detag(%a1);

		if (%teamNum == "2") %line = "1";
		else %line = "2";

		$ProStatus[%line] = "Held : " @ detag(%a2);
		UpdateProStatus();
	}

	function cnhTeamCap(%msgType, %msgString, %a1, %a2, %a3, %a4, %a5, %a6) {
		parent::cnhTeamCap(%msgType, %msgString, %a1, %a2, %a3, %a4, %a5, %a6);
		%teamNum = detag(%a4);
		%score = detag(%a5);
		%sLimit = detag(%a6);
		%string = %sLimit - %score;
		UpdateProScore(%teamNum, "+" @ %string);
	}

	//---------------------------------------------------------------
	// Handle all Bounty Events
	//---------------------------------------------------------------
	function bountyTargetIs(%msgType, %msgString, %a1, %a2, %a3, %a4, %a5, %a6) {
		parent::bountyTargetIs(%msgType, %msgString, %a1, %a2, %a3, %a4, %a5, %a6);

		%hit = detag(%a1);
		if(%hit $= "")
			%hit = "< Waiting >";

		$ProStatus[1] = StripNameColors(%hit);
		$ProStatus[2] = "";
		UpdateProStatus();
	}

	function bountyTargetDropped(%msgType, %msgString, %a1, %a2, %a3, %a4, %a5, %a6) {
		parent::bountyTargetDropped(%msgType, %msgString, %a1, %a2, %a3, %a4, %a5, %a6);
		$ProStatus[1] = "";
		$ProStatus[2] = "< Waiting >";
		UpdateProStatus();
	}

	//---------------------------------------------------------------
	// Handle all CTF Events
	//---------------------------------------------------------------
	function ctfAddTeam(%msgType, %msgString, %a1, %a2, %a3, %a4, %a5, %a6) {
		parent::ctfAddTeam(%msgType, %msgString, %a1, %a2, %a3, %a4, %a5, %a6);

		%teamNum = detag(%a1);
		%teamName = detag(%a2);
		%flagStatus = detag(%a3);
		%score = detag(%a4);

		if (%flagStatus $= "<At Base>") {
			%flagStatus = "";
		}

		// NULL out before setting
		$ProScore[%teamNum] = "";
		$ProStatus[%teamNum] = "";

		$ProScore[%teamNum] = %score;
		$ProTeam[%teamNum] = %teamName;
		$ProStatus[%teamNum] = %flagStatus;

		UpdateProScore(%teamNum, %score);
		UpdateProStatus();
	}

	function ctfFlagTaken(%msgType, %msgString, %a1, %a2, %a3, %a4, %a5, %a6) {
		parent::ctfFlagTaken(%msgType, %msgString, %a1, %a2, %a3, %a4, %a5, %a6);

		%team = detag(%a3);
		%taker = detag(%a4);
		
		if ( StripNameColors(%taker) $= detag($PPName) ) {
			$ProStatus[%team] = "<font:Univers bold:16><color:00FF00>RUN!!!";
		} else {
			$ProStatus[%team] = %taker;
		}
		
		UpdateProStatus();
	}

	function ctfFlagDropped(%msgType, %msgString, %a1, %a2, %a3, %a4, %a5, %a6) {
		parent::ctfFlagDropped(%msgType, %msgString, %a1, %a2, %a3, %a4, %a5, %a6);

		%team = detag(%a3);
		$ResetTimer = 1;

		if (%team == "1")
			CTFReturnUpdate("1", "46", "1");
		else
			CTFReturnUpdate("2", "46", "1");
	}

	function ctfFlagCapped(%msgType, %msgString, %a1, %a2, %a3, %a4, %a5, %a6) {
		parent::ctfFlagCapped(%msgType, %msgString, %a1, %a2, %a3, %a4, %a5, %a6);

		$ProStatus[1] = "";
		$ProStatus[2] = "";
		UpdateProStatus();
	}

	function ctfFlagReturned(%msgType, %msgString, %a1, %a2, %a3, %a4, %a5, %a6) {
		parent::ctfFlagReturned(%msgType, %msgString, %a1, %a2, %a3, %a4, %a5, %a6);

		%team = detag(%a3);
		$ProStatus[%team] = "";
		UpdateProStatus();
	}

	function CTFReturnUpdate(%line,%time,%new) {

		if ((Strstr($ProStatus[%line], "< ENEMY: ") != -1) || (Strstr($ProStatus[%line], "< YOURS: ") != -1) || (%new == "1")) {

			if ($ResetTimer == "1") {
				%newtime = "45";
				if (ProHudStatus.schedule[%line] !$= "") {
					cancel(ProHudStatus.schedule[%line]);
					ProHudStatus.schedule[%line] = "";
				}
				$ResetTimer = 0;
			} else {
				%newtime = (%time - 1);
			}

			// See if status has changed no need to update
			if (%line == $PPTeam)
				%whom = "YOURS:";
			else
				%whom = "ENEMY:";

			$ProStatus[%line] = "<color:FFFF00>< " @ %whom @ " " @ %newtime @ " >";

			ProHudStatus.schedule[%line] = schedule(1000, 0, "CTFReturnUpdate", %line, %newtime);
		}
		UpdateProStatus();
	}
	//---------------------------------------------------------------
	// Hud Update Functions
	//---------------------------------------------------------------
	function doUpdateGhettoList() {
		parent::doUpdateGhettoList();
		UpdateProScore();
	}

	function UpdateProScore(%team, %amount) {

		if ((%team) && (%amount)) {
			$ProScore[%team] = %amount;
		}
		if ($PPTeam == "1") {
			%color[1] = "00FF00";
			%color[2] = "FFFFFF";
		} else {
			%color[2] = "00FF00";
			%color[1] = "FFFFFF";
		}

		if (($ProPackPrefs::OBHudActive) && ($ProPackPrefs::ScoreHudActive)) {
			ProHudScore.setVisible(true);
		} else {
			ProHudScore.setVisible(false);
		}
		
		if (isobject($hudposProHudScore)) {
			ProHudScore.position = $hudposProHudScore;
		} else {
			ProHudScore.position = $ProScorePOS[getWord($pref::Video::resolution, 0)];
		}
		
		if (StrLen($ProTeam[1]) < 1) {
			ProHudScoreText.setText("<just:left><font:Univers condensed:16><color:" @ %color[1] @ ">" @ $ProSingleScore SPC $ProScore[1]);
		} else {
			ProHudScoreText.setText("<just:left><font:Univers condensed:16><color:" @ %color[1] @ ">" @ $ProTeamCount[1] @ " - " @ getword($ProTeam[1], 0) @ " : " @ $ProScore[1] @ "\n" @
					        "<just:left><font:Univers condensed:16><color:" @ %color[2] @ ">" @ $ProTeamCount[2] @ " - " @ getword($ProTeam[2], 0) @ " : " @ $ProScore[2] );
		}
	}

	function UpdateProStatus() {

		if ((strlen($ProStatus[1]) < 1) && (strlen($ProStatus[2]) < 1)) {
				ProHudStatusText.setText("");
				ProHudStatus.setVisible(false);
		} else {
			if ($PPTeam == 1) 	%otherteam = 2;
			else if ($PPTeam == 2)	%otherteam = 1;
			else 			%otherteam = 0;

			if (($ProPackPrefs::OBHudActive) && ($ProPackPrefs::StatusHudActive)) {
				ProHudStatus.setVisible(true);
			} else {
				ProHudStatus.setVisible(false);
				PPTR2CarrierHealth.setVisible(false);
			}

			ProHudStatusText.setText("<just:center><color:FF0000>" @ $ProStatus[$PPTeam] @
					     "\n<just:center><color:00FF00>" @ $ProStatus[%otherteam]);
		}
	}

	//--------------------------------------
	// GUI
	//--------------------------------------	
	function ProHudCreate() {
		ProHudDestroy();
	
		%ProStatusX = getWord($pref::Video::resolution, 0) * 0.50 - 100;
		%ProStatusY = getWord($pref::Video::resolution, 1) * 0.66;
		
		new ShellFieldCtrl(ProHudStatus) {
			profile = "GuiConsoleProfile";
			horizSizing = "center";
			vertSizing = "bottom";
			position = %ProStatusX SPC %ProStatusY;
			extent = "200 50";
			minExtent = "8 8";
			visible = "0";

			new GuiMLTextCtrl(ProHudStatusText) {
				profile = "ProPackTextCtrl";
				horizSizing = "center";
				vertSizing = "top";
				position = "0 0";
				extent = "200 50";
				minExtent = "8 8";
				visible = "1";
				helpTag = "0";
				lineSpacing = "2";
				allowColorChars = "1";
				maxChars = "-1";
			};
			
			new GuiProgressCtrl(PPTR2CarrierHealth) {
				profile = "TR2CarrierHudProfile";
				horizSizing = "center";
				vertSizing = "bottom";
				position = "50 40";
				extent = "100 10";
				minExtent = "10 10";
				visible = "0";
				hideCursor = "0";
				bypassHideCursor = "0";
				helpTag = "0";
		        };
		};
		playgui.add(ProHudStatus);

		new ShellFieldCtrl(ProHudScore) {
			profile = "GuiConsoleProfile";
			horizSizing = "center";
			vertSizing = "center";
			position = "844 42";
			extent = "104 40";
			minExtent = "8 8";
			visible = "0";

			new GuiMLTextCtrl(ProHudScoreText) {
				profile = "ProPackTextCtrl";
				horizSizing = "center";
				vertSizing = "center";
				position = "0 0";
				extent = "102 38";
				minExtent = "8 8";
				visible = "1";
				helpTag = "0";
				lineSpacing = "2";
				allowColorChars = "1";
				maxChars = "-1";
			};
		};
		playgui.add(ProHudScore);
	}

	function ProHudDestroy() {
		if (isObject(ProHudStatus)) {
			playgui.remove(ProHudStatus);
			ProHudStatus.delete();
		}
		if (isObject(ProHudScore)) {
			playgui.remove(ProHudScore);
			ProHudScore.delete();
		}
	}

	//-------------------
	// Override hud.cs
	//-------------------
	function ClientCmdDisplayHuds() {
		parent::ClientCmdDisplayHuds();
		objectiveHud.setVisible(getProReverse($ProPackPrefs::OBHudActive));
	}

	function updateDemoPlaybackStatus() {		// turns on the default objective hud in demo mode
		parent::updateDemoPlaybackStatus();
		objectiveHud.setVisible(true);
	}

	function restoreAllHuds() {
		parent::restoreAllHuds();
		objectiveHud.setVisible(getProReverse($ProPackPrefs::OBHudActive));
	}

	function clientCmdTogglePlayHuds(%val) {
		parent::clientCmdTogglePlayHuds(%val);
		objectiveHud.setVisible(getProReverse($ProPackPrefs::OBHudActive));

		if ( (%val) && ($ProPackPrefs::ScoreHudActive) ) {
			ProHudScore.setVisible(true);
		} else {
			ProHudScore.setVisible(false);
			PPTR2CarrierHealth.setVisible(false);
		}

		if ( (%val) && ($ProPackPrefs::StatusHudActive) ) {
			ProHudStatus.setVisible(true);
		} else {
			ProHudStatus.setVisible(false);
			PPTR2CarrierHealth.setVisible(false);
		}
	}
	
	function PlayGui::onWake(%this) {
		parent::onWake(%this);
		if (!isObject(ProHudScore)) schedule(3000, 0, "ProHudCreate"); 
		if(isObject(HM) && isObject(HudMover)) hudmover::addhud(ProHudScore, "ProPackScore");
		if(isObject(HM) && isObject(HudMover)) hudmover::addhud(ProHudStatus, "ProPackStatus");
	}

	function DispatchLaunchMode() {
		addMessageCallback('MsgClientJoinTeam', ProSiegeRoleSwitch); // switching Teams
		
		addMessageCallback('MsgTeamRabbitAddTeam', teamRabbitAddTeam);
		addMessageCallback('MsgTeamRabbitFlagTaken', teamRabbitFlagTaken);
		addMessageCallback('MsgTeamRabbitFlagDropped', teamRabbitFlagDropped);
		addMessageCallback('MsgTeamRabbitFlagReturned', teamRabbitFlagReturned);
		addMessageCallback('MsgTeamRabbitFlagStatus', teamRabbitFlagStatus);
		
		addMessageCallback('MsgArenaVersion', arenaVersionMsg );
		addMessageCallback('MsgArenaServerState', arenaServerState);
		addMessageCallback('MsgArenaAddTeam', arenaAddTeam);
		addMessageCallback('MsgArenaTeamState', arenaTeamState);
		
		addMessageCallback('MsgTR2FlagTaken', TR2FlagTaken);

		parent::DispatchLaunchMode();
	}
};
activatePackage(ProPackObjective);