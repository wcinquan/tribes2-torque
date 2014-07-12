// #category = ProPack
// #name = ProPack Tracker
// #version = 5.89
// #description = Death/Kill stat tracking by weapon w/ optional pop-ups
// #status = close to final ;p
// #date = June 3, 2001
// #warrior = MeBaD
// #web = http://propack.tribes2.org
// #credit = Neofight

$isLTon = "1"; 			// make activate LifeTimeStatsFULLStats default to turning on (big screen)
$discscreenshots = "0";  	// disabled by default

$trackerleft[1280] = "415 562";
$trackerright[1280] = "665 562";
$trackerleft[1152] = "351 482";
$trackerright[1152] = "601 482";
$trackerleft[1024] = "287 425";
$trackerright[1024] = "537 425";
$trackerleft[800] = "175 275";
$trackerright[800] = "425 275";

package ProPackTracker {
	// Activate ReCall of last print
	function ActivateReCall(%val) {
		if (%val) {

			TrackerPopup($RecallLeft, $RecallRight, 7);

			if ((Strlen($RecallLeft) < 1) || (Strlen($RecallRight) < 1)) {
				%printthis = "<color:FFFF00>No event in buffer... YET";
			} else {
				%printthis = "<color:FFFFFF><font:Univers condensed:16> Recall of Last Event";
			}
			clientCmdBottomPrint(%printthis, 5, 1);
		}
	}

	// Activate Full display of all kills
	function ActivateLifeTime(%val) {

		if (%val) {
			if ($isLTon == "2") {
				clientcmdTogglePlayHuds(true);
				FullTrackStats.setVisible(false);

				FullTrackStatsText.setText(""); 	// clear that big buffer
				FullTrackStatsRightText.setText(""); 	// clear that big buffer
				FullTrackStatsLeftText.setText(""); 	// clear that big buffer
				clientCmdBottomPrint("Returning to game", 1, 1);
				$isLTon = "1";
			} else {
				clientcmdTogglePlayHuds(false);
				FullTrackStats.BuildFULLprintout();
				$isLTon++;
			}
		}
	}

	// Activate AutoScreenshot
	function ActivateAutoSC(%val) {
		if (%val) {
			if ($discscreenshots == "1") {
				$discscreenshots = "0";
				%word = "DISABLED";
				%color = "FF0000"; // red
			} else {
				$discscreenshots = "1";
				%word = "ENABLED";
				%color = "99FF00"; // green
			}
			clientCmdBottomPrint("<color:FFFFFF><font:Univers condensed:16> Auto Screenshots : <font:Univers bold:16><color:" @ %color @ ">" @ %word, 5, 1);
		}
	}

//---------------------------
// Control the hud events
//---------------------------
	function handleClientJoin(%msgType, %msgString, %clientName, %clientId, %targetId, %isAI, %isAdmin, %isSuperAdmin, %isSmurf, %guid) {
		parent::handleClientJoin(%msgType, %msgString, %clientName, %clientId, %targetId, %isAI, %isAdmin, %isSuperAdmin, %isSmurf, %guid);

		if(StrStr(%msgString, "Welcome to Tribes") != -1) {
			ProClearGameTrack();
		}
	}

	function FullTrackStats::BuildFULLprintout() {

		%weapons = "<font:Univers bold:8>\n<font:Univers bold:16><just:left>";

		for (%i = 1; %i < 31; %i++) {
			%weapon = ProNoSpacedWeaponName(%i);
			if (Strlen(%weapon) > 0) {
				if ((%i != 7) && ((%i > 0) && (%i < 12)) || (%i == 31)) {
					%ThisLTKills = ProhandleLifeTime(%weapon, "2", "kills");
					%ThisLTDeaths = ProhandleLifeTime(%weapon, "2", "deaths");
					%weapons = %weapons @ " " @ %weapon @ "\n";
					%leftstats = %leftstats @ "<font:Univers bold:16><just:center><color:FFFF00>" @ $GameKills[%weapon] @ "<color:FFFFFF> - <color:FF0000>" @ $GameDeaths[%weapon] @ "\n";
					%rightstats = %rightstats @ "<font:Univers bold:16><just:center><color:FFFF00>" @ %ThisLTKills @ "<color:FFFFFF> - <color:FF0000>" @ %ThisLTDeaths @ "\n";
				}
			}
		}

		// adds the rest of them .. heh
		%weapons = %weapons @ " headshots\n rearshocks\n carrierkills\n flagreturns\n";

		%leftstats = %leftstats @ "<font:Univers bold:16><just:center><color:FFFF00>" @ $GameKills[lasertohead] @ "\n";
		%leftstats = %leftstats @ "<font:Univers bold:16><just:center><color:FFFF00>" @ $GameKills[rearshock] @ "\n";
		%leftstats = %leftstats @ "<font:Univers bold:16><just:center><color:FFFF00>" @ $carrierkills @ "\n";
		%leftstats = %leftstats @ "<font:Univers bold:16><just:center><color:FFFF00>" @ $returnsthisgame @ "\n";

		%rightstats = %rightstats @ "<font:Univers bold:16><just:center><color:FFFF00>" @ $LifeTime::Kills_lasertohead @ "\n";
		%rightstats = %rightstats @ "<font:Univers bold:16><just:center><color:FFFF00>" @ $LifeTime::Kills_rearshock @ "\n";
		%rightstats = %rightstats @ "<font:Univers bold:16><just:center><color:FFFF00>" @ $LifeTime::CTFCarrierKills @ "\n";
		%rightstats = %rightstats @ "<font:Univers bold:16><just:center><color:FFFF00>" @ $LifeTime::CTFReturns @ "\n";

		// main big window
		// Game Box
		FullTrackStats.setVisible(true);
		FullTrackStatsText.setText("<just:center><font:Univers bold:18>All your Stats are belong to " @ $PPName @ "\n\n" @
					   "<font:Univers bold:16><color:00FF99>Weapon          Current Kills/Deaths          LifeTime Kills/Deaths\n<color:FFFFFF><font:Univers condensed:16>" @
					   %weapons );
		FullTrackStatsLeftText.setText(%leftstats);
		FullTrackStatsRightText.setText(%rightstats);
	}

	function ProCheckFlagReturn(%msgType, %msgString, %a1, %a2, %a3, %a4, %a5, %a6) {
		if ($ProPackPrefs::TrackerActive) {

			%msg = detag(%msgString);
			%subType = detag(%msgType);
			%printflagloadout = 0;

			if (Strstr(%msg, "You returned your flag") != -1) { 				//MsgCTFFlagReturned
				$returnsthisgame++;
				$LifeTime::CTFReturns++;
					%word = "Flag Returns";

				$FlagPrintGame = $returnsthisgame;
				$FlagPrintLife = $LifeTime::CTFReturns;

				%printflagloadout = 1;
			}

			if ((%subType $= "msgCarKill") && (Strstr(%msg, "You received") != -1)) {  	//Player check added for the 'Classic' Mod
				$carrierkills++;
				$LifeTime::CTFCarrierKills++;
				%word = "FlagCarrier Kills";

				$FlagPrintGame = $carrierkills;
				$FlagPrintLife = $LifeTime::CTFCarrierKills;
				%printflagloadout = 1;
			}

			if (%printflagloadout == "1") {
				%printleft = "Current " @ %word @ "\n";
				%printleft = %printleft @ "<color:FFFF00>" @ $FlagPrintGame;

				%printright = %printright @ "LifeTime " @ %word @ "\n";
				%printright = %printright @ "<color:FFFF00>" @ $FlagPrintLife;

				TrackerPopup(%printleft, %printright, 7);

				$RecallLeft = %printleft;
				$RecallRight = %printright;
			}
		}
	}

	function ProHeadshot (%msgType, %msgString, %a1, %a2, %a3, %a4, %a5, %a6) {
		if ($ProPackPrefs::TrackerHeadshotSound) {
			%msg = detag(%msgString);
			
			if (Strstr(%msg, "You received") != -1)
				alxPlay(alxCreateSource(AudioChat, "headshot.wav"));
		}
	}

	function ProHeadshotKill() {
		if ($ProPackPrefs::TrackerActive) {

			$GameKills[lasertohead]++;
			$LifeTime::Kills_lasertohead++;

			// left box
			%printleft = "Current Headshot kills \n";
			%printleft = %printleft @ "<color:FFFF00>" @ $GameKills[lasertohead];

			// right box
			%printright = %printright @ "LifeTime Headshot kills \n";
			%printright = %printright @ "<color:FFFF00>" @ $LifeTime::Kills_lasertohead;

			TrackerPopup(%printleft, %printright, 7);

			$RecallLeft = %printleft;
			$RecallRight = %printright;

		}

		if ($ProPackPrefs::TrackerHeadshotSound) {
			 %handle[1] = alxCreateSource(AudioChat, "headshot.wav");
			 %handle[2] = alxCreateSource(AudioChat, "wbslap.wav");

			for (%i = 1; %i < 3; %i++) {
				alxPlay(%handle[%i]);
			}
		}
	}
	
	function ProRearshockKill() {
		if ($ProPackPrefs::TrackerActive) {

			$GameKills[rearshock]++;
			$LifeTime::Kills_rearshock++;

			// left box
			%printleft = "Current Rearshock kills \n";
			%printleft = %printleft @ "<color:FFFF00>" @ $GameKills[rearshock];

			// right box
			%printright = %printright @ "LifeTime Rearshock kills \n";
			%printright = %printright @ "<color:FFFF00>" @ $LifeTime::Kills_rearshock;

			TrackerPopup(%printleft, %printright, 7);

			$RecallLeft = %printleft;
			$RecallRight = %printright;

		}
	}

	function ProTrackKill(%msgType, %msgString, %victimName, %victimGender, %victimPoss, %killerName, %killerGender, %killerPoss, %damageType) {
		if ($ProPackPrefs::TrackerActive) {

			%doweprint = 0;
			%CoolshotCheck = detag(%msgType);
			%ProdamageType = detag(%damageType);
			%ProkillerName = StripNameColors(detag(%killerName));
			%ProvictimName = StripNameColors(detag(%victimName));

			if ((%CoolshotCheck $= "MsgHeadshotKill") && (Strstr(%ProkillerName, $PPName) != -1)) {
				ProHeadshotKill();
				return;
			}
			
			if ( ((%CoolshotCheck $= "msgRearshot") || (%CoolshotCheck $= "MsgRearshotKill")) && (Strstr(%ProkillerName, $PPName) != -1) ) {
				ProRearshockKill();
				return;
			}

			%ProWeaponUsed = ProNoSpacedWeaponName(%ProdamageType);		//%ProWeaponUsed = $DamageTypeText[%damageType]

			if (Strlen(%ProWeaponUsed) < 1) {
				return; // default is ghey!
			}

			// Killed Someone
			if (Strstr(%Prokillername, $PPName) != -1) {
				$GameKills[%ProWeaponUsed]++;
				%LifeTimeDeathPrint = ProhandleLifeTime(%ProWeaponUsed, "2", "Deaths");
				%LifeTimeKillPrint = ProhandleLifeTime(%ProWeaponUsed, "1", "Kills");

				if ((Strstr(%ProWeaponUsed, "disc") != -1) && ($discscreenshots == "1")) {
					clientcmdTogglePlayHuds(false);
					schedule(500, 0, "doScreenShot"); // let the disc settle in .. heh
					schedule(900, 0, "clientcmdTogglePlayHuds", true);
				}
				%doweprint = 1;
			}

			// Death by someone
			if (Strstr(%Provictimname, $PPName) != -1) {
				$GameDeaths[%ProWeaponUsed]++;
				%LifeTimeDeathPrint = ProhandleLifeTime(%ProWeaponUsed, "1", "Deaths");
				%LifeTimeKillPrint = ProhandleLifeTime(%ProWeaponUsed, "2", "Kills");

				%doweprint = 1;
			}

			if (%doweprint == 1) {
				%printleft = "Current " @ %ProWeaponUsed @ " Kills \n";
				%printleft = %printleft @ "<color:FFFF00>" @ $GameKills[%ProWeaponUsed] @ "<color:FFFFFF> - <color:FF0000>" @ $GameDeaths[%ProWeaponUsed];

				%printright = %printright @ "LifeTime " @ %ProWeaponUsed @ " Kills \n";
				%printright = %printright @ "<color:FFFF00>" @ %LifeTimeKillPrint @ "<color:FFFFFF> - <color:FF0000>" @ %LifeTimeDeathPrint;

				TrackerPopup(%printleft, %printright, 7);

				$RecallLeft = %printleft;
				$RecallRight = %printright;
			}
		}
	}

	function ProClearGameTrack() {
		for (%i = 0; %i < 36; %i++) {
			%WeaponName = ProNoSpacedWeaponName(%i);
			$GameKills[%WeaponName] = 0;
			$GameDeaths[%WeaponName] = 0;
		}
		$returnsthisgame = 0;
		$carrierkills = 0;
		$gameKills[lasertohead] = 0;
		$gameKills[rearshock] = 0;

		echo("Clearing Game Kills...");
	}

	function Disconnect() {  			// override disconnect just to add export
		ProBackupLTKills();
		ProClearGameTrack();
		parent::Disconnect();
	}

	function ProBackupLTKills() {
		export("$LifeTime::*", "prefs/LifeTimeKills.cs", false);
		echo("Saved LifeTime Kills");
	}

	function ProhandleLifeTime(%weapon,%add,%type) {
		// %weapon, display[2] or add[1], death or kill
		if (%add == "1") { 	// got kill :) || death :(
			if (%type $= "Kills") { $LifeTime::Kills_[%weapon]++; return $LifeTime::Kills_[%weapon]; }
			if (%type $= "Deaths") { $LifeTime::Deaths_[%weapon]++; return $LifeTime::Deaths_[%weapon]; }
		} else { 		// display
			if (%type $= "Kills") { return $LifeTime::Kills_[%weapon]; }
			if (%type $= "Deaths") { return $LifeTime::Deaths_[%weapon]; }
		}
	}

	function DispatchLaunchMode() {
		if (!isPlayingDemo()) {
			addMessageCallback('MsgGameOver', ProBackupLTKills);
			addMessageCallBack('MsgClientReady', ProClearGameTrack); // set everything to 0 when your ready for it
			addMessageCallback('MsgCTFFlagReturned', ProCheckFlagReturn);
			addMessageCallBack('msgCarKill', ProCheckFlagReturn);
			addMessageCallBack('MsgLegitKill', ProTrackKill);
			addMessageCallBack('MsgHeadshotKill', ProTrackKill);
			addMessageCallBack('msgHeadshot', ProHeadshot);
			addMessageCallBack('MsgRearshotKill', ProTrackKill);	// Added for 'Classic' Mod
			addMessageCallBack('msgRearshot', ProTrackKill);	// Added for 'Classic' Mod
			addMessageCallBack('msgTR2Knockdown', ProTrackKill);	// Added for TR2	
		}

		parent::DispatchLaunchMode();
	}

//-----------------
// GUI Stuff now :)
//-----------------
	function TransTrackHudCreate() {
		TransTrackHudDestroy();

		$TransTrackLeft = new ShellFieldCtrl(TransTrackLeft) {
				profile = "GuiConsoleProfile";
				horizSizing = "center";
				vertSizing = "center";
				position = "175 275";
				extent = "200 50";
				minExtent = "8 8";
				visible = "0";

				new GuiMLTextCtrl(TransPrintLeftText) {
					profile = "ProPackTextCtrl";
					horizSizing = "center";
					vertSizing = "center";
					position = "3 5";
					extent = "190 40";
					minExtent = "8 8";
					visible = "1";
					helpTag = "0";
					lineSpacing = "2";
					allowColorChars = "1";
					maxChars = "-1";
				};
			};
			playgui.add($TransTrackLeft);

		$TransTrackRight = new ShellFieldCtrl(TransTrackRight) {
				profile = "GuiConsoleProfile";
				horizSizing = "center";
				vertSizing = "center";
				position = "425 275";
				extent = "200 50";
				minExtent = "8 8";
				visible = "0";

				new GuiMLTextCtrl(TransPrintRightText) {
					profile = "ProPackTextCtrl";
					horizSizing = "center";
					vertSizing = "center";
					position = "3 5";
					extent = "190 40";
					minExtent = "8 8";
					visible = "1";
					helpTag = "0";
					lineSpacing = "2";
					allowColorChars = "1";
					maxChars = "-1";
				};
			};
			playgui.add($TransTrackRight);

	$FullTrackStats = new ShellFieldCtrl(FullTrackStats) {
			profile = "GuiChatBackProfile";
			horizSizing = "center";
			vertSizing = "center";
			position = "0 0";
			extent = "400 324"; 
			minExtent = "8 8";
			visible = "0";

			new GuiMLTextCtrl(FullTrackStatsText) {
				profile = "ProPackTextCtrl";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "3 5";
				extent = "390 290";
				minExtent = "8 8";
				visible = "1";
				helpTag = "0";
				lineSpacing = "3";
				allowColorChars = "1";
				maxChars = "-1";
			};

			new ShellFieldCtrl(FullTrackStatsLeft) {
				profile = "GuiChatBackProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "90 60";
				extent = "150 264";
				minExtent = "8 8";
				visible = "1";

				new GuiMLTextCtrl(FullTrackStatsLeftText) {
					profile = "ProPackTextCtrl";
					horizSizing = "right";
					vertSizing = "bottom";
					position = "2 7";
					extent = "140 264";
					minExtent = "8 8";
					visible = "1";
					helpTag = "0";
					lineSpacing = "2";
					allowColorChars = "1";
					maxChars = "-1";
				};
			};

			new ShellFieldCtrl(FullTrackStatsRight) {
				profile = "GuiChatBackProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "250 60";
				extent = "150 264";
				minExtent = "8 8";
				visible = "1";

				new GuiMLTextCtrl(FullTrackStatsRightText) {
					profile = "ProPackTextCtrl";
					horizSizing = "right";
					vertSizing = "bottom";
					position = "2 7";
					extent = "140 264";
					minExtent = "8 8";
					visible = "1";
					helpTag = "0";
					lineSpacing = "2";
					allowColorChars = "1";
					maxChars = "-1";
				};
			};
		};
		playgui.add($FullTrackStats);
		$TransTrackHudBuild = true;
	}

	function TransTrackHudDestroy() {
		if ($TransTrackHudBuild) {
			playgui.remove($TransTrackRight);
			playgui.remove($TransTrackLeft);
			TransTrackLeft.delete();
			TransTrackRight.delete();
			$TransTrackLeft = "";
			$TransTrackRight = "";

			playgui.remove($FullTrackStats);
			FullTrackStats.delete();
			$FullTrackStats = "";

			$TransTrackHudBuild = false;
		}
	}

	function LoadingGui::onWake(%this) {
		if (!$TransTrackHudBuild) {
			TransTrackHudCreate();
		}
		parent::onWake(%this);
	}

	function TrackerPopup(%leftprint, %rightprint, %time ) {
		if ($ProPackPrefs::TrackerPopup) {

			%sectime = (%time * 1000); // make all time in seconds

			TransTrackLeft.position = $trackerleft[getWord($pref::Video::resolution, 0)];
			TransPrintLeftText.setText("<font:Univers condensed:16><just:center>" @ %leftprint);

			TransTrackRight.position = $trackerright[getWord($pref::Video::resolution, 0)];
			TransPrintRightText.setText("<font:Univers condensed:16><just:center>" @ %rightprint);

			TransTrackLeft.setVisible(true);
			TransTrackRight.setVisible(true);

			schedule(%sectime, 0, eval, "TransTrackLeft.setVisible(false);");
			schedule(%sectime, 0, eval, "TransTrackRight.setVisible(false);");
		}
	}
};
activatePackage(ProPackTracker);