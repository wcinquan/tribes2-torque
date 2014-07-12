// #category = ProPack
// #name = ProPack
// #date = 1/14/2003
// #warrior = MeBaD & Neofight
// #web = http://propack.tribes2.org
// #description = *.ownage
// #status = Till someone gives me somfin good to add... done :X - MeBaD

exec("scripts/Propack/ProPackAutoChat.cs");
exec("scripts/Propack/ProPackFlagTrixy.cs");
exec("scripts/Propack/ProPackDemo.cs");
exec("scripts/Propack/ProPackFixes.cs");
exec("scripts/Propack/ProPackFlag.cs");
exec("scripts/Propack/ProPackInfo.cs");
exec("scripts/Propack/ProPackJoinRetry.cs");
exec("scripts/Propack/ProPackMulti.cs");
exec("scripts/Propack/ProPackObjective.cs");
exec("scripts/Propack/ProPackObserved.cs");
exec("scripts/Propack/ProPackThrow.cs");
exec("scripts/Propack/ProPackTracker.cs");

if(isFile("prefs/ProPackPrefs.cs")) {
	exec("prefs/ProPackPrefs.cs");
	error("Using existing ProPack Prefs...");
} else {
	$ProPackPrefs::AltHud = "0";
	$ProPackPrefs::ammohud = "1";
	$ProPackPrefs::AutoGenBoom = "1";
	$ProPackPrefs::AutoGotFlag = "1";
	$ProPackPrefs::AutoGrabSpeed = "1";
	$ProPackPrefs::AutoTaunt = "1";
	$ProPackPrefs::AutoVPadBoom = "1";
	$ProPackPrefs::bottomprintdlg = "GuiDefaultProfile";
	$ProPackPrefs::bottomprintdlgVar = "0";
	$ProPackPrefs::CarrierHudActive = "0";
	$ProPackPrefs::ChatMenuHud = "GuiDefaultProfile";
	$ProPackPrefs::ChatMenuHudVar = "0";
	$ProPackPrefs::CKillsActive = "1";
	$ProPackPrefs::DeathMessages = "1";
	$ProPackPrefs::FlagHudActive = "1";
	$ProPackPrefs::FPSHud = "1";
	$ProPackPrefs::GrabSpeedText1 = "Crawling Home ";
	$ProPackPrefs::GrabSpeedText2 = "Jogging Home ";
	$ProPackPrefs::GrabSpeedText3 = "Running Home ";
	$ProPackPrefs::GrabSpeedText4 = "Cruisin\' Home ";
	$ProPackPrefs::GrabSpeedText5 = "Haulin\' Ass ";
	$ProPackPrefs::hudclusterback = "0";
	$ProPackprefs::InfoHudActivate = "1";
	$ProPackPrefs::InfoHudActive = "1";
	$ProPackPrefs::mainvotehud = "GuiDefaultProfile";
	$ProPackPrefs::mainVoteHudVar = "0";
	$ProPackPrefs::MuteChatDisplay = "1";
	$ProPackPrefs::MuteDefaultKeys = "1";
	$ProPackPrefs::MuteSpammers = "0";
	$ProPackPrefs::MuteToolsActive = "1";
	$ProPackPrefs::MuteToolsHackzorKitPickup = "Alter/Split-to-InfoHud";
	$ProPackPrefs::MuteToolsHackzorKitPickup_Append = "<color:ff0000>=NEW KIT=";
	$ProPackPrefs::MuteToolsHackzorLoadout = "Append-to-InfoHud";
	$ProPackPrefs::MuteToolsHackzorLoadout_Append = "<color:ff9900>";
	$ProPackPrefs::MuteToolsHackzorPackOff = "BottomPrint";
	$ProPackPrefs::MuteToolsHackzorPackOff_Append = "<color:ffff66>";
	$ProPackPrefs::MuteToolsHackzorPackOn = "BottomPrint";
	$ProPackPrefs::MuteToolsHackzorPackOn_Append = "<color:ffff00>";
	$ProPackPrefs::MuteToolsmsgCarKill = "Mute";
	$ProPackPrefs::MuteToolsmsgCTFEnemyCap = "Mute";
	$ProPackPrefs::MuteToolsmsgCTFEnemyFlagTouch = "Mute";
	$ProPackPrefs::MuteToolsMsgCTFFlagCapped = "Mute";
	$ProPackPrefs::MuteToolsMsgCTFFlagDropped = "Mute";
	$ProPackPrefs::MuteToolsMsgCTFFlagMined = "Mute";
	$ProPackPrefs::MuteToolsMsgCTFFlagReturned = "Mute";
	$ProPackPrefs::MuteToolsMsgCTFFlagTaken = "Mute";
	$ProPackPrefs::MuteToolsmsgCTFFriendCap = "Mute";
	$ProPackPrefs::MuteToolsmsgCTFFriendFlagTouch = "Mute";
	$ProPackPrefs::MuteToolsmsgDepInvDes = "Mute";
	$ProPackPrefs::MuteToolsmsgDepInvRep = "Mute";
	$ProPackPrefs::MuteToolsmsgDepSensorDes = "Mute";
	$ProPackPrefs::MuteToolsmsgDepTurDes = "Mute";
	$ProPackPrefs::MuteToolsmsgDepTurRep = "Mute";
	$ProPackPrefs::MuteToolsmsgDepTurretRep = "Mute";
	$ProPackPrefs::MuteToolsmsgEscAsst = "Mute";
	$ProPackPrefs::MuteToolsmsgFlagDef = "Mute";
	$ProPackPrefs::MuteToolsmsgGenDef = "Mute";
	$ProPackPrefs::MuteToolsmsgGenDes = "Alter/Split-to-InfoHud";
	$ProPackPrefs::MuteToolsmsgGenDes_Append = "<color:1199cc>Enemy Lights OUT!";
	$ProPackPrefs::MuteToolsmsgGenRep = "Mute";
	$ProPackPrefs::MuteToolsmsgHeadshot = "Mute";
	$ProPackPrefs::MuteToolsmsgInvDes = "Mute";
	$ProPackPrefs::MuteToolsmsgInvRep = "Mute";
	$ProPackPrefs::MuteToolsmsgIStationRep = "Mute";
	$ProPackPrefs::MuteToolsmsgItemPickup = "Append-to-InfoHud";
	$ProPackPrefs::MuteToolsMsgItemPickup_Append = "<color:ffff66>";
	$ProPackPrefs::MuteToolsMsgMissionEnd = "Mute";
	$ProPackPrefs::MuteToolsMsgMissionEnding = "Mute";
	$ProPackPrefs::MuteToolsMsgMissionStart = "Mute";
	$ProPackPrefs::MuteToolsmsgMPBTeleDes = "Alter/Split-to-InfoHud";
	$ProPackPrefs::MuteToolsmsgMPBTeleDes_Append = "<color:1199dd>MPB Teleport OUT!";
	$ProPackPrefs::MuteToolsmsgRearshot = "Mute";
	$ProPackPrefs::MuteToolsmsgRepairKitUsed = "Alter/Split-to-InfoHud";
	$ProPackPrefs::MuteToolsmsgRepairKitUsed_Append = "<color:cc0000>-KIT USED-";
	$ProPackPrefs::MuteToolsMsgSatchelChargeDetonate = "BottomPrint";
	$ProPackPrefs::MuteToolsMsgSatchelChargeDetonate_Append = "<color:cc1166>Sachel BOOM!";
	$ProPackPrefs::MuteToolsMsgSatchelChargePlaced = "BottomPrint";
	$ProPackPrefs::MuteToolsMsgSatchelChargePlaced_Append = "<color:cc1199>Sachel deployed";
	$ProPackPrefs::MuteToolsmsgSensorDes = "Mute";
	$ProPackPrefs::MuteToolsmsgSensorRep = "Mute";
	$ProPackPrefs::MuteToolsmsgSentryDes = "Mute";
	$ProPackPrefs::MuteToolsmsgSentryRep = "Mute";
	$ProPackPrefs::MuteToolsmsgSolarDes = "Alter/Split-to-InfoHud";
	$ProPackPrefs::MuteToolsmsgSolarDes_Append = "<color:1199dd>Solar Panel OUT!";
	$ProPackPrefs::MuteToolsmsgsolarRep = "Mute";
	$ProPackPrefs::MuteToolsMsgTeamRabbitFlagDropped = "Mute";
	$ProPackPrefs::MuteToolsMsgTeamRabbitFlagReturned = "Mute";
	$ProPackPrefs::MuteToolsMsgTeamRabbitFlagTaken = "Mute";
	$ProPackPrefs::MuteToolsMsgTR2FlagDropped = "Mute";
	$ProPackPrefs::MuteToolsMsgTR2FlagTaken = "Mute";
	$ProPackPrefs::MuteToolsMsgTR2InstantBonus = "Mute";
	$ProPackPrefs::MuteToolsmsgTurretDes = "Mute";
	$ProPackPrefs::MuteToolsMsgTurretMount = "Append-to-InfoHud";
	$ProPackPrefs::MuteToolsMsgTurretMount_Append = "<color:ffbbee>";
	$ProPackPrefs::MuteToolsmsgTurretRep = "Mute";
	$ProPackPrefs::MuteToolsmsgVehicleDestroy = "Mute";
	$ProPackPrefs::MuteToolsmsgVehicleScore = "Mute";
	$ProPackPrefs::MuteToolsmsgVehicleTeamDestroy = "Mute";
	$ProPackPrefs::MuteToolsmsgVSDes = "Alter/Split-to-InfoHud";
	$ProPackPrefs::MuteToolsmsgVSDes_Append = "<color:1199dd>Enemy V-Pad OUT!";
	$ProPackPrefs::MuteToolsmsgVSRep = "Mute";
	$ProPackPrefs::MuteToolsmsgVStationRep = "Mute";
	$ProPackPrefs::MuteToolsscoreFlaRetMsg = "Mute";
	$ProPackPrefs::MuteToolsscoreStaleRetMsg = "BottomPrint";
	$ProPackPrefs::MuteToolsscoreStaleRetMsg_Append = "<color:cc00cc>Stalemate Broken";
	$ProPackPrefs::OBHudActive = "1";
	$ProPackPrefs::outerChatHud = "GuiChatBackProfile";
	$ProPackPrefs::outerChatHudVar = "1";
	$ProPackPrefs::PingHud = "1";
	$ProPackPrefs::retframehud = "0";
	$ProPackPrefs::ScoreHudActive = "1";
	$ProPackPrefs::SpeedHud = "1";
	$ProPackPrefs::AddPeek = "1";
	$ProPackPrefs::StatusHudActive = "1";
	$ProPackPrefs::TR2BonusHud = "TR2TransHudProfile";
	$ProPackPrefs::TR2BonusHudVar = "0";
	$ProPackPrefs::TR2EventPopup = "TR2EventPopupProfile";
	$ProPackPrefs::TR2EventPopupVar = "1";
	$ProPackPrefs::TrackerActive = "1";
	$ProPackPrefs::TrackerHeadShotSound = "1";
	$ProPackPrefs::TrackerPopup = "1";
	$ProPackPrefs::WaveToObserver = "1";
	$ProPackPrefs::SpeedPeekRecord = 0;
	error("Creating ProPack Prefs...");
}

package ProPackLoad {
	//-------------------------------------------------------------------------------------------
	// Load Gui
	//-------------------------------------------------------------------------------------------
	function toggleProPack(%val) {
		if ( %val )
			toggleCursorHuds('ProPackGui');
	}

	function DispatchLaunchMode() {
		// Needs to exist before everything else :o
		new GuiControlProfile ("ProPackTextCtrl") {
			fontType = "Univers Condensed";
			fontSize = 16;
			fontColor = "255 255 255";
			justify = "left";
		};
		
		new GuiControlProfile ("TR2CarrierHudProfile") {
			opaque = false;
			border = true;
			borderColor = "225 225 225 100";
		};
		
		new GuiControlProfile ("TR2TransHudProfile") {
			bitmap = "scripts/ProPack/gui/trans.png";
			borderColor = "0 0 0 0";
		};
		
		if (isFile("prefs/lifetimekills.cs")) exec("prefs/lifetimekills.cs");
		if (!isObject(ProPackGui)) exec("scripts/ProPack/gui/ProPackInterface.gui");

		schedule(1500, 0, "ProPackGuiLoadout");
		parent::DispatchLaunchMode();

		ProPackGrab1.setText($ProPackPrefs::GrabSpeedText1);  
		ProPackGrab2.setText($ProPackPrefs::GrabSpeedText2);
		ProPackGrab3.setText($ProPackPrefs::GrabSpeedText3);  // Bug fix!
		ProPackGrab4.setText($ProPackPrefs::GrabSpeedText4);
		ProPackGrab5.setText($ProPackPrefs::GrabSpeedText5);
	}

	//-------------------------------------------------------------------------------------------
	// Pro Pack Gui control
	//-------------------------------------------------------------------------------------------
	function ProPackGui::loadHud( %this, %tag ) {
		$Hud[%tag] = ProPackGui;
		$Hud[%tag].childGui = ProPackParent;
		$Hud[%tag].parent = ProPackParent;
	}

	function ProPackGui::onwake(%this) {
		if ( isObject( hudMap ) ) {
			hudMap.pop();
			hudMap.delete();
		}
		new ActionMap( hudMap );
		hudMap.blockBind( moveMap, toggleScoreScreen );
		hudMap.blockBind( moveMap, toggleCommanderMap );
		hudMap.blockBind( moveMap, toggleinventoryScreen );
		hudMap.bindCmd( keyboard, escape, "", "ProPackGui.ondone();" );
		hudMap.push();

		alxPlay(alxCreateSource(AudioChat, "fx/misc/diagnostic_on.wav"));

		ProPackGui.setFrameList("ProPackHudOptions");

		ProPackHudOptionsBtn.setValue(true);
		ProPackHudOptionsNotes.setText(getProPackNotes("ProPackHudOptions"));
		ProPackGui.setupMuteToolsList();
	}

	function ProPackGui::setupHud( %this, %tag ) { }

	function ProPackGui::onsleep(%this) {
		hudMap.pop();
		hudMap.delete();
		updateActionMaps();
		ProPackGui.HideAllRight();

		TR2EventPopup.profile = $ProPackPrefs::TR2EventPopup;
		TR2BonusHud.profile = $ProPackPrefs::TR2BonusHud;
		outerChatHud.profile = $ProPackPrefs::outerChatHud;
		ChatMenuHud.profile = $ProPackPrefs::ChatMenuHud;
		mainvotehud.profile = $ProPackPrefs::mainvotehud;
		hudclusterback.opacity = $ProPackPrefs::hudclusterback;
		BottomPrintDlg.profile = $ProPackPrefs::bottomprintdlg;
		ProPackEditMuteActions.clear();
		$ProPackGui::Command = "";
		clientcmdTogglePlayHuds(true);
	}

	function ProPackGui::ondone(%this) {
		toggleCursorHuds( 'ProPackGui' );
	}

	// HIDE all right sides
	function ProPackGui::HideAllRight(%this) {

		// the Panes
		ProPackHudOptionsFrame.setVisible(false);
		ProPackAutoChatFrame.setVisible(false);
		ProPackTrackerFrame.setVisible(false);
		ProPackInfoHudFrame.setVisible(false);
		ProPackOBHudFrame.setVisible(false);
		ProPackMultiFrame.setVisible(false);
		ProPackMuteFrame.setVisible(false);

		// The Tabs
		ProPackHudOptionsBtn.setValue(false);
		ProPackAutoChatBtn.setValue(false);
		ProPackTrackerBtn.setValue(false);
		ProPackInfoHudBtn.setValue(false);
		ProPackOBHudBtn.setValue(false);
		ProPackMultiBtn.setValue(false);
		ProPackMuteBtn.setValue(false);

		clientcmdTogglePlayHuds(false);

		ProPackGui.Saveus();

		for (%i = 1; %i < 6; %i++) { // moved from multihud for faster processing
			$ProMulti[%i] = "";
		}
	}

	function ProPackGui::Saveus(%this) {

		// Play gui's
		$ProPackPrefs::TR2EventPopup = getProPackTRVALtoGUI(ProPackTR2EventPopup.getValue());
		$ProPackPrefs::TR2BonusHud = getProPackTRVALtoGUI(ProPackTR2BonusHud.getValue());
		$ProPackPrefs::outerChatHud = getProPackVALtoGUI(ProPackOuterChatHud.getValue());
		$ProPackPrefs::ChatMenuHud = getProPackVALtoGUI(ProPackChatMenuHud.getValue());
		$ProPackPrefs::mainvotehud = getProPackVALtoGUI(ProPackmainVoteHud.getValue());
		$ProPackPrefs::bottomprintdlg = getProPackVALtoGUI(ProPackBottomPrintDlg.getValue());
		$ProPackPrefs::ammohud = ProPackammohud.getValue();
		$ProPackPrefs::retframehud = ProPackretframehud.getValue();
		$ProPackPrefs::hudClusterBack = ProPackhudClusterBack.getValue();

		// Auto Chat
		$ProPackPrefs::AutoTaunt = ProPackAutoTaunt.getValue();
		$ProPackPrefs::AutoGotFlag = ProPackAutoGotFlag.getValue();
		$ProPackPrefs::AutoGrabSpeed = ProPackAutoGrabSpeed.getValue();
		$ProPackPrefs::AutoGenBoom = ProPackAutoGenBoom.getValue();
		$ProPackPrefs::AutoVPadBoom = ProPackAutoVPadBoom.getValue();
		$ProPackPrefs::GrabSpeedText1 = ProPackGrab1.getValue();
		$ProPackPrefs::GrabSpeedText2 = ProPackGrab2.getValue();
		$ProPackPrefs::GrabSpeedText3 = ProPackGrab3.getValue();
		$ProPackPrefs::GrabSpeedText4 = ProPackGrab4.getValue();
		$ProPackPrefs::GrabSpeedText5 = ProPackGrab5.getValue();
		$ProPackPrefs::WaveToObserver = ProPackWaveToObserver.getValue();

		// Tracker
		$ProPackPrefs::TrackerActive = ProPackTrackerActivate.getValue();
		$ProPackPrefs::TrackerPopup = ProPackKillPopup.getValue();
		$ProPackPrefs::TrackerHeadShotSound = ProPackHeadShotSounds.getValue();

		// InfoHud
		$ProPackPrefs::InfoHudActive = ProPackInfoHudActivate.getValue();
		$ProPackPrefs::DeathMessages = ProPackSplitDeathMsg.getValue();
		$ProPackPrefs::CKillsActive = ProPackColorKills.getValue();
		$ProPackPrefs::MuteDefaultKeys = ProPackMuteChatDisplay.getValue();
		// New Peek Speed
		$ProPackPrefs::AddPeek = ProPackAddPeekActive.getValue();

		// OBhud & FlagHud
		$ProPackPrefs::OBHudActive = ProPackOBHudActivate.getValue();
		$ProPackPrefs::ScoreHudActive = ProPackScoreHudActivate.getValue();
		$ProPackPrefs::StatusHudActive = ProPackStatusHudActivate.getValue();
		$ProPackPrefs::FlagHudActive = ProPackFlagHudActivate.getValue();

		// Multi
		$ProPackPrefs::FPSHud = ProPackFPSHudActive.getValue();
		$ProPackPrefs::PingHud = ProPackPingHudActive.getValue();
		$ProPackPrefs::AltHud = ProPackAltHudActive.getValue();
		$ProPackPrefs::SpeedHud = ProPackSpeedHudActive.getValue();
		$ProPackPrefs::PeekSpeedHud = ProPackAddPeekActive.getValue();

		//mute
		$ProPackPrefs::MuteToolsActive = ProPackMuteToolsActivate.getValue();

		export("$ProPackPrefs::*", "prefs/ProPackPrefs.cs", false);
		error("ProPack: Saving Prefs...");
	}

	function ProPackGuiLoadout(%this) {		
		TR2EventPopup.profile = $ProPackPrefs::TR2EventPopup;
		TR2BonusHud.profile = $ProPackPrefs::TR2BonusHud;
		outerChatHud.profile = $ProPackPrefs::outerChatHud;
		ChatMenuHud.profile = $ProPackPrefs::ChatMenuHud;
		mainvotehud.profile = $ProPackPrefs::mainvotehud;
		hudclusterback.opacity = $ProPackPrefs::hudclusterback;
		BottomPrintDlg.profile = $ProPackPrefs::bottomprintdlg;
	}

	function ProPackGui::setFrameList(%bs, %this) {

		%frame = %this @ "Frame";
		%button = %this @ "Btn";
		%notes = %this @ "Notes";

		// Play Gui's
		ProPackTR2EventPopup.setValue(getProPackTRGUItoVal($ProPackPrefs::TR2EventPopup));
		ProPackTR2BonusHud.setValue(getProPackTRGUItoVal($ProPackPrefs::TR2BonusHud));	
		ProPackOuterChatHud.setValue(getProPackGUItoVal($ProPackPrefs::outerChatHud));
		ProPackmainVoteHud.setValue(getProPackGUItoVal($ProPackPrefs::mainVoteHud));
		ProPackChatMenuHud.setValue(getProPackGUItoVal($ProPackPrefs::ChatMenuHud));
		ProPackBottomPrintDlg.setValue(getProPackGUItoVal($ProPackPrefs::bottomprintdlg));
		ProPackammohud.setValue($ProPackPrefs::ammohud);
		ProPackretframehud.setValue($ProPackPrefs::retframehud);
		ProPackhudClusterBack.setValue($ProPackPrefs::hudClusterBack);

		// Autochat
		ProPackAutoTaunt.setValue($ProPackPrefs::AutoTaunt);
		ProPackAutoGotFlag.setValue($ProPackPrefs::AutoGotFlag);
		ProPackAutoGrabSpeed.setValue($ProPackPrefs::AutoGrabSpeed);
		ProPackAutoGenBoom.setValue($ProPackPrefs::AutoGenBoom);
		ProPackAutoVPadBoom.setValue($ProPackPrefs::AutoVPadBoom);
		ProPackGrabInfo.setText("<just:right><color:FFFF00>0 -> 99kph\n\n100 -> 174kph\n\n175 -> 249kph\n\n 250 -> 300kph\n\n 300++kph");
		ProPackWaveToObserver.setValue($ProPackPrefs::WaveToObserver);

		// Tracker
		ProPackTrackerActivate.setValue($ProPackPrefs::TrackerActive);
		ProPackKillPopup.setValue($ProPackPrefs::TrackerPopup);
		ProPackHeadShotSounds.setValue($ProPackPrefs::TrackerHeadShotSound);

		// InfoHud
		ProPackInfoHudActivate.setValue($ProPackPrefs::InfoHudActive);
		ProPackSplitDeathMsg.setValue($ProPackPrefs::DeathMessages);
		ProPackColorKills.setValue($ProPackPrefs::CKillsActive);
		ProPackMuteChatDisplay.setValue($ProPackPrefs::MuteDefaultKeys);
		ProPackAddPeekActive.setValue($ProPackPrefs::AddPeek);	// Speed Peek

		// OBhud & FlagHud
		ProPackOBHudActivate.setValue($ProPackPrefs::OBHudActive);
		ProPackScoreHudActivate.setValue($ProPackPrefs::ScoreHudActive);
		ProPackStatusHudActivate.setValue($ProPackPrefs::StatusHudActive);
		ProPackFlagHudActivate.setValue($ProPackPrefs::FlagHudActive);

		// Multi
		ProPackFPSHudActive.setValue($ProPackPrefs::FPSHud);
		ProPackPingHudActive.setValue($ProPackPrefs::PingHud);
		ProPackAltHudActive.setValue($ProPackPrefs::AltHud);
		ProPackSpeedHudActive.setValue($ProPackPrefs::SpeedHud);
		ProPackPeekSpdActive.setValue($ProPackPrefs::PeekSpeedHud);


		// Mute
		ProPackMuteToolsActivate.setValue($ProPackPrefs::MuteToolsActive);

		%notes.setText(getProPackNotes(%this));
		ProPackGui.HideAllRight();
		%button.setValue(true);
		%frame.setVisible(true);
	}
	
	function getProPackTRVALtoGUI(%this) {
		if (%this == 1)
			return "TR2EventPopupProfile";
		else
			return "TR2TransHudProfile";
	}

	function getProPackTRGUItoVal(%this) {
		if ( (%this $= "TR2EventPopupProfile" ) || (%this $= "TR2BonusPopupProfile") )
			return true;
		else
			return false;
	}
		
	function getProPackGUItoVal(%this) {
		if (%this $= "GuiDefaultProfile")
			return false;
		else
			return true;
	}

	function getProPackVALtoGUI(%this) {
		if (%this == 0)
			return "GuiDefaultProfile";
		else
			return "GuiChatBackProfile";
	}

	function getProReverse (%this) {
		if (%this == 1)
			return false;
		else
			return true;
	}
	
	function getProPackNotes(%this) {

		%ProPackVersion = "7 RELEASE";
		%HeaderFont = "<font:Univers bold:12><just:right><color:00FF00>ProPack Version " @
					%ProPackVersion @ "\n\n<just:center><font:Univers bold:16><color:FF9900>";
		%FooterFont = "<color:FFFF00>";

		switch$ (%this) {
			case "ProPackHudOptions":
				return %HeaderFont @ "HudControl\n" @
					  %FooterFont @ "Alter Huds from Transparrent to Opaque!";
			case "ProPackAutoChat":
				return %HeaderFont @ "Auto Chat Controls\n" @
					  %FooterFont @ "Auto-response for events!";
			case "ProPackTracker":
				return %HeaderFont @ "Kill Tracking Events!\n" @
					  %FooterFont @ "Events for legit kills!";
			case "ProPackInfoHud":
				return %HeaderFont @ "Info Hud Controls\n" @
					  %FooterFont @ "AKA Split ChatHud (Mute/Pullout/Bottomprint see Mute tools)";
			case "ProPackOBHud":
				return %HeaderFont @ "THE Objective Hud\n" @
					  %FooterFont @ "OMG, the default one is ghey!";
			case "ProPackMulti":
				return %HeaderFont @ "Multi Hud Control\n" @
					  %FooterFont @ "FPS/Ping/Altitude/Speed/Peek Speed all rolled into one!";
			case "ProPackMute":
				return %HeaderFont @ "Mute/Alter Tools\n" @
					  %FooterFont @ "Tools to change a selected group of messages\nwhile *still* being able to hear the sounds from that message!\n<font:Univers bold:14><color:5599FF>*This list contains the most common message groups to alter*";
		}
	}

	//-------------------------------------------------------------------------------------------
	// Handle Mute Tools list
	//-------------------------------------------------------------------------------------------
	function ProPackGui::setupMuteToolsList(%this) {
		if (!ProPackGui.fillList) {
			%guiRoot = ProPackMuteToolsTree.getFirstRootItem();
			%newGuiId = ProPackMuteToolsTree.insertItem( %guiRoot, "Root Mute/Alter Tools", 0 );
			ProPackGui.filllist((%guiRoot + 1));
			ProPackMuteToolsTree.expandItem( %newGuiId );
			ProPackMuteToolsTree.selectItem( %newGuiId );
			ProPackGui.fillList = true;
		}
	}

	function ProPackGui::fillList(%this, %guiRoot) {
		%listRoot = ProPackMuteToolsTree.insertItem( %guiRoot, "Flag Messages", 0 );
			ProPackMuteToolsTree.insertItem( %listRoot, "(" @ $ProPackPrefs::MuteToolsMsgCTFFlagCapped @ ")- Flag Capped", 1 );
			ProPackMuteToolsTree.insertItem( %listRoot, "(" @ $ProPackPrefs::MuteToolsmsgCTFEnemyCap @ ")- Enemy Capture (score)", 1 );
			ProPackMuteToolsTree.insertItem( %listRoot, "(" @ $ProPackPrefs::MuteToolsmsgCTFFriendCap @ ")- Friendly Capture (score)", 1 );
			ProPackMuteToolsTree.insertItem( %listRoot, "(" @ $ProPackPrefs::MuteToolsmsgCTFEnemyFlagTouch @ ")- Enemy Touch Flag", 1 );
			ProPackMuteToolsTree.insertItem( %listRoot, "(" @ $ProPackPrefs::MuteToolsmsgCTFFriendFlagTouch @ ")- Friendly Touch Flag", 1 );
			ProPackMuteToolsTree.insertItem( %listRoot, "(" @ $ProPackPrefs::MuteToolsscoreFlaRetMsg @ ")- Flag Returned", 1 );
			ProPackMuteToolsTree.insertItem( %listRoot, "(" @ $ProPackPrefs::MuteToolsscoreStaleRetMsg @ ")- StaleMate Broken", 1 );
			ProPackMuteToolsTree.insertItem( %listRoot, "(" @ $ProPackPrefs::MuteToolsmsgCarKill @ ")- Carrier Killed", 1 );
			ProPackMuteToolsTree.insertItem( %listRoot, "(" @ $ProPackPrefs::MuteToolsmsgEscAsst @ ")- Flag Assist", 1 );
			ProPackMuteToolsTree.insertItem( %listRoot, "(" @ $ProPackPrefs::MuteToolsMsgCTFFlagReturned @ ")- Flag Returned", 1 );
			ProPackMuteToolsTree.insertItem( %listRoot, "(" @ $ProPackPrefs::MuteToolsMsgCTFFlagTaken @ ")- Flag Taken", 1 );
			ProPackMuteToolsTree.insertItem( %listRoot, "(" @ $ProPackPrefs::MuteToolsMsgCTFFlagDropped @ ")- Flag Dropped", 1 );
			ProPackMuteToolsTree.insertItem( %listRoot, "(" @ $ProPackPrefs::MuteToolsmsgFlagDef @ ")- Flag Defend", 1 );
			ProPackMuteToolsTree.insertItem( %listRoot, "(" @ $ProPackPrefs::MuteToolsMsgTeamRabbitFlagReturned @ ")- Team Rabbit Flag Returned", 1 );
			ProPackMuteToolsTree.insertItem( %listRoot, "(" @ $ProPackPrefs::MuteToolsMsgTeamRabbitFlagTaken @ ")- Team Rabbit Flag Taken", 1 );
			ProPackMuteToolsTree.insertItem( %listRoot, "(" @ $ProPackPrefs::MuteToolsMsgTeamRabbitFlagDropped @ ")- Team Rabbit Flag Dropped", 1 );
			ProPackMuteToolsTree.insertItem( %listRoot, "(" @ $ProPackPrefs::MuteToolsMsgTR2FlagTaken @ ")- TR2 Flag Taken", 1 );
			ProPackMuteToolsTree.insertItem( %listRoot, "(" @ $ProPackPrefs::MuteToolsMsgTR2FlagDropped @ ")- TR2 Flag Dropped", 1 );

		%listRoot = ProPackMuteToolsTree.insertItem( %guiRoot, "Destroy Point Messages", 0 );
				%VlistRoot = ProPackMuteToolsTree.insertItem( %listRoot, "Vehicles", 0 );
					ProPackMuteToolsTree.insertItem( %VlistRoot, "(" @ $ProPackPrefs::MuteToolsmsgVehicleDestroy @ ")- Vehicle (legit)", 1 );
					ProPackMuteToolsTree.insertItem( %VlistRoot, "(" @ $ProPackPrefs::MuteToolsmsgVehicleTeamDestroy @ ")- Vehicle (TK)", 1 );
					ProPackMuteToolsTree.insertItem( %VlistRoot, "(" @ $ProPackPrefs::MuteToolsmsgVehicleScore @ ")- Vehicle (You Score)", 1 );
			ProPackMuteToolsTree.insertItem( %listRoot, "(" @ $ProPackPrefs::MuteToolsmsgSentryDes @ ")- Sentry Turret Destroy", 1 );
			ProPackMuteToolsTree.insertItem( %listRoot, "(" @ $ProPackPrefs::MuteToolsmsgSensorDes @ ")- Sensor Destroy", 1 );
			ProPackMuteToolsTree.insertItem( %listRoot, "(" @ $ProPackPrefs::MuteToolsmsgTurretDes @ ")- Turret Destroy", 1 );
			ProPackMuteToolsTree.insertItem( %listRoot, "(" @ $ProPackPrefs::MuteToolsmsgSolarDes @ ")- Solar Pannel Destroy", 1 );
			ProPackMuteToolsTree.insertItem( %listRoot, "(" @ $ProPackPrefs::MuteToolsmsgDepSensorDes @ ")- Deployed Sensor Destroy", 1 );
			ProPackMuteToolsTree.insertItem( %listRoot, "(" @ $ProPackPrefs::MuteToolsmsgDepTurDes @ ")- Deployed Turret Destroy", 1 );
			ProPackMuteToolsTree.insertItem( %listRoot, "(" @ $ProPackPrefs::MuteToolsmsgDepInvDes @ ")- Deployed Inventory Station Destroy", 1 );
			ProPackMuteToolsTree.insertItem( %listRoot, "(" @ $ProPackPrefs::MuteToolsmsgInvDes @ ")- Inventory Station Destroy", 1 );
			ProPackMuteToolsTree.insertItem( %listRoot, "(" @ $ProPackPrefs::MuteToolsmsgVSDes @ ")- V-Pad Destroy", 1 );
			ProPackMuteToolsTree.insertItem( %listRoot, "(" @ $ProPackPrefs::MuteToolsmsgMPBTeleDes @ ")- MPB Teleport Destroy", 1 );
			ProPackMuteToolsTree.insertItem( %listRoot, "(" @ $ProPackPrefs::MuteToolsmsgGenDes @ ")- Generator Destroy", 1 );
			
		%listRoot = ProPackMuteToolsTree.insertItem( %guiRoot, "Repair Point Messages", 0 );
			ProPackMuteToolsTree.insertItem( %listRoot, "(" @ $ProPackPrefs::MuteToolsmsgGenRep @ ")- Generator Repair", 1 );
			ProPackMuteToolsTree.insertItem( %listRoot, "(" @ $ProPackPrefs::MuteToolsmsgIStationRep @ ")- Inventory Station Repair", 1 );
			ProPackMuteToolsTree.insertItem( %listRoot, "(" @ $ProPackPrefs::MuteToolsmsgSensorRep @ ")- Sensor Repair", 1 );
			ProPackMuteToolsTree.insertItem( %listRoot, "(" @ $ProPackPrefs::MuteToolsmsgTurretRep @ ")- Turret Repair", 1 );
			ProPackMuteToolsTree.insertItem( %listRoot, "(" @ $ProPackPrefs::MuteToolsmsgVStationRep @ ")- V-Pad Repair", 1 );
			ProPackMuteToolsTree.insertItem( %listRoot, "(" @ $ProPackPrefs::MuteToolsmsgsolarRep @ ")- Solar Pannel Repair", 1 );
			ProPackMuteToolsTree.insertItem( %listRoot, "(" @ $ProPackPrefs::MuteToolsmsgSentryRep @ ")- Sentry Turret Repair", 1 );
			ProPackMuteToolsTree.insertItem( %listRoot, "(" @ $ProPackPrefs::MuteToolsmsgDepTurretRep @ ")- Deployed Turret Repair", 1 );
			ProPackMuteToolsTree.insertItem( %listRoot, "(" @ $ProPackPrefs::MuteToolsmsgDepInvRep @ ")- Deployed Inventory Station Repair", 1 );
			ProPackMuteToolsTree.insertItem( %listRoot, "(" @ $ProPackPrefs::MuteToolsmsgGenDef @ ")- Defending Generator Repair", 1 );

		%listRoot = ProPackMuteToolsTree.insertItem( %guiRoot, "Announcer", 0 );
			ProPackMuteToolsTree.insertItem( %listRoot, "(" @ $ProPackPrefs::MuteToolsMsgMissionEnd @ ")- Mission Ending CountDown", 1 );
			ProPackMuteToolsTree.insertItem( %listRoot, "(" @ $ProPackPrefs::MuteToolsMsgMissionStart @ ")- Mission Start CountDown", 1 );
			ProPackMuteToolsTree.insertItem( %listRoot, "(" @ $ProPackPrefs::MuteToolsMsgCTFFlagMined @ ")- Flag Mined", 1 );

		%listRoot = ProPackMuteToolsTree.insertItem( %guiRoot, "Packs", 0 );
			ProPackMuteToolsTree.insertItem( %listRoot, "(" @ $ProPackPrefs::MuteToolsHackzorPackOn @ ")- (ALL) Pack On", 1 );
			ProPackMuteToolsTree.insertItem( %listRoot, "(" @ $ProPackPrefs::MuteToolsHackzorPackOff @ ")- (ALL) Pack Off", 1 );
			ProPackMuteToolsTree.insertItem( %listRoot, "(" @ $ProPackPrefs::MuteToolsMsgSatchelChargePlaced @ ")- SatchelCharge Placed", 1 );
			ProPackMuteToolsTree.insertItem( %listRoot, "(" @ $ProPackPrefs::MuteToolsMsgSatchelChargeDetonate @ ")- SatchelCharge Detonate", 1 );
			ProPackMuteToolsTree.insertItem( %listRoot, "(" @ $ProPackPrefs::MuteToolsMsgTurretMount @ ")- Turret Mounting", 1 );

		%listRoot = ProPackMuteToolsTree.insertItem( %guiRoot, "Misc", 0 );
			ProPackMuteToolsTree.insertItem( %listRoot, "(" @ $ProPackPrefs::MuteToolsmsgItemPickup @ ")- Item Pickup", 1 );
			ProPackMuteToolsTree.insertItem( %listRoot, "(" @ $ProPackPrefs::MuteToolsmsgRepairKitUsed @ ")- RepairKit Used", 1 );
			ProPackMuteToolsTree.insertItem( %listRoot, "(" @ $ProPackPrefs::MuteToolsHackzorKitPickup @ ")- Repair Kit Pickup", 1 );
			ProPackMuteToolsTree.insertItem( %listRoot, "(" @ $ProPackPrefs::MuteToolsHackzorLoadout @ ")- Loadout", 1 );
			ProPackMuteToolsTree.insertItem( %listRoot, "(" @ $ProPackPrefs::MuteToolsmsgHeadshot @ ")- Headshot Point", 1 );
			ProPackMuteToolsTree.insertItem( %listRoot, "(" @ $ProPackPrefs::MuteToolsmsgRearshot @ ")- RearShock Point", 1 );
			ProPackMuteToolsTree.insertItem( %listRoot, "(" @ $ProPackPrefs::MuteToolsMsgTR2InstantBonus @ ")- TR2 Instant Bonus", 1 );
	}

	function ProPackGui::getMuteListMsgType(%this, %text) {
		// local because this is only used in GUI NOT gameplay!!!
		// DO NOT TOUCH OR I WILL SACRIFICE YOU TO THE UE PIGS!!!
		%newtext = restWords(%text);

		%textToMsgType["Flag Capped"] = "MsgCTFFlagCapped";
		%textToMsgType["Enemy Capture (score)"] = "msgCTFEnemyCap";
		%textToMsgType["Friendly Capture (score)"] = "msgCTFFriendCap";
		%textToMsgType["Friendly Touch Flag"] = "msgCTFFriendFlagTouch";
		%textToMsgType["Enemy Touch Flag"] = "msgCTFEnemyFlagTouch";
		%textToMsgType["Flag Returned"] = "scoreFlaRetMsg";
		%textToMsgType["StaleMate Broken"] = "scoreStaleRetMsg";
		%textToMsgType["Carrier Killed"] = "msgCarKill";
		%textToMsgType["Flag Defended"] = "msgFlagDef";
		%textToMsgType["Flag Assist"] = "msgEscAsst";
		%textToMsgType["Flag Returned"] = "MsgCTFFlagReturned";
		%textToMsgType["Flag Taken"] = "MsgCTFFlagTaken";
		%textToMsgType["Flag Dropped"] = "MsgCTFFlagDropped";
		%textToMsgType["Team Rabbit Flag Returned"] = "MsgTeamRabbitFlagReturned";
		%textToMsgType["Team Rabbit Flag Taken"] = "MsgTeamRabbitFlagTaken";
		%textToMsgType["Team Rabbit Flag Dropped"] = "MsgTeamRabbitFlagDropped";
		%textToMsgType["TR2 Flag Taken"] = "MsgTR2FlagTaken";
		%textToMsgType["TR2 Flag Dropped"] = "MsgTR2FlagDropped";

		%textToMsgType["Sensor Destroy"] = "msgSensorDes";
		%textToMsgType["Turret Destroy"] = "msgTurretDes";
		%textToMsgType["Inventory Station Destroy"] = "msgInvDes";
		%textToMsgType["V-Pad Destroy"] = "msgVSDes";
		%textToMsgType["MPB Teleport Destroy"] = "msgMPBTeleDes";
		%textToMsgType["Solar Pannel Destroy"] = "msgSolarDes";
		%textToMsgType["Sentry Turret Destroy"] = "msgSentryDes";
		%textToMsgType["Deployed Sensor Destroy"] = "msgDepSensorDes";
		%textToMsgType["Deployed Turret Destroy"] = "msgDepTurDes";
		%textToMsgType["Deployed Inventory Station Destroy"] = "msgDepInvDes";
		%textToMsgType["Generator Destroy"] = "msgGenDes";
			%textToMsgType["Vehicle (legit)"] = "msgVehicleDestroy";
			%textToMsgType["Vehicle (TK)"] = "msgVehicleTeamDestroy";
			%textToMsgType["Vehicle (You Score)"] = "msgVehicleScore";
			
		%textToMsgType["Generator Repair"] = "msgGenRep";
		%textToMsgType["Inventory Station Repair"] = "msgIStationRep";	
		%textToMsgType["Sensor Repair"] = "msgSensorRep";
		%textToMsgType["Turret Repair"] = "msgTurretRep";
		%textToMsgType["V-Pad Repair"] = "msgVStationRep";
		%textToMsgType["Solar Pannel Repair"] = "msgsolarRep";
		%textToMsgType["Sentry Turret Repair"] = "msgSentryRep";
		%textToMsgType["Deployed Turret Repair"] = "msgDepTurretRep";
		%textToMsgType["Deployed Inventory Station Repair"] = "msgDepInvRep";
		%textToMsgType["Defending Generator Repair"] = "msgGenDef";

		%textToMsgType["Mission Ending CountDown"] = "MsgMissionEnd";
		%textToMsgType["Mission Start CountDown"] = "MsgMissionStart";
		%textToMsgType["Flag Mined"] = "MsgCTFFlagMined";

		%textToMsgType["(ALL) Pack On"] = "HackzorPackOn";
		%textToMsgType["(ALL) Pack Off"] = "HackzorPackOff";
		%textToMsgType["SatchelCharge Placed"] = "MsgSatchelChargePlaced";
		%textToMsgType["SatchelCharge Detonate"] = "MsgSatchelChargeDetonate";
		%textToMsgType["Turret Mounting"] = "MsgTurretMount";

		%textToMsgType["RepairKit Used"] = "msgRepairKitUsed";
		%textToMsgType["Item Pickup"] = "MsgItemPickup";
		%textToMsgType["Loadout"] = "HackzorLoadout";
		%textToMsgType["Repair Kit Pickup"] = "HackzorKitPickup";
		%textToMsgType["Headshot Point"] = "msgHeadshot";
		%textToMsgType["RearShock Point"] = "msgRearshot";
		%textToMsgType["TR2 Instant Bonus"] = "MsgTR2InstantBonus";
		
		return %textToMsgType[%newtext];
	}

	function ProPackGui::editSelectedMuteListItem(%this) {
		ProPackEditNotes.setText("<color:FFFF00>1) Alter/Split-to-InfoHud:\n     Pulls messages to InfoHud with a replacement of the message\n" @
			"2) Append-to-InfoHud:\n     Pulls messages to InfoHud while appending the text to your altered text\n" @
			"3) BottomPrint:\n     Simular to Alter/Split changing a msg and printing at the bottom of the screen\n\n" @
			"***ALL of these options allow the T2 HTML style code for TextML Controling***");

		%item = ProPackMuteToolsTree.getSelectedItem();
		%temp = ProPackMuteToolsTree.getItemText( %item );
		%command = ProPackMuteToolsTree.getItemValue( %item );
		if (%command) {
			$ProPackGui::Action = ""; // clear anyway
			$ProPackGui::Command = ProPackGui.getMuteListMsgType(%temp);
				ProPackEditMuteActions.add( "Alter/Split-to-InfoHud", 1 );
				%index["Alter/Split-to-InfoHud"] = "1";
				ProPackEditMuteActions.add( "Append-to-InfoHud", 2 );
				%index["Append-to-InfoHud"] = "2";
				ProPackEditMuteActions.add( "BottomPrint", 3 );
				%index["BottomPrint"] = "3";
				ProPackEditMuteActions.add( "Mute", 4 );
				%index["Mute"] = "4";
				ProPackEditMuteActions.add( "Normal (Chat Menu)", "5");
				%index["Normal (Chat Menu)"] = "5";

			ProPackEditMuteActions.setSelected(%index[$ProPackPrefs::MuteTools[ProPackGui.getMuteListMsgType(%temp)]]);
			$ProPackGui::Action = $ProPackPrefs::MuteTools[ProPackGui.getMuteListMsgType(%temp)];
			$ProPackGui::AlterText = $ProPackPrefs::MuteTools[ProPackGui.getMuteListMsgType(%temp) @ "_Append"];
			$ProPackGui::FullName = %temp;

				Canvas.pushDialog( ProPackEditMuteItem );
				ProPackEditTitle.setText("Edit::" @ restWords(%temp));
		}
	}

	function ProPackGui::setMuteItem(%this) {
		Canvas.popDialog(ProPackEditMuteItem);
		if ($ProPackGui::Action $= "Normal (Chat Menu)") {
			$ProPackPrefs::MuteTools[$ProPackGui::Command @ "_Append"] = "";
			$ProPackPrefs::MuteTools[$ProPackGui::Command] = "";
			schedule(250, 0, "MessageBoxOK", "Changes Made", "Item Cleared from Alter List");
		} else if ($ProPackGui::Action $= "Mute") {
			$ProPackPrefs::MuteTools[$ProPackGui::Command] = "Mute";
			$ProPackPrefs::MuteTools[$ProPackGui::Command @ "_Append"] = "";
			schedule(250, 0, "MessageBoxOK", "Changes Made", "Item added to Alter Mute list");
		} else {
			$ProPackPrefs::MuteTools[$ProPackGui::Command] = $ProPackGui::Action;
			if ($ProPackGui::AlterText !$= "") {
				$ProPackPrefs::MuteTools[$ProPackGui::Command @ "_Append"] = $ProPackGui::AlterText;
				schedule(250, 0, "MessageBoxOK", "Changes Made",
									   "\nAction: " @ $ProPackGui::Action @
									   "\nAlter Text: " @ $ProPackGui::AlterText);
			} else {
				schedule(250, 0, "MessageBoxOK", "ERROR", "Text && Action required\nto Alter an Item!");
			}
		}
		ProPackEditMuteActions.clear();
		%dis = ProPackMuteToolsTree.getSelectedItem();
		%temp = ProPackMuteToolsTree.getItemText(%dis);
		ProPackMuteToolsTree.editItem(%dis, "(" @ $ProPackPrefs::MuteTools[ProPackGui.getMuteListMsgType(%temp)] @ ")- " @ restWords($ProPackGui::FullName), 1 );
	}

	function ProPackEditMuteActions::onSelect( %this, %index, %value ) {
		$ProPackGui::Action = %value;
	}
	//-------------------------------------------------------------------------------------------
	// Handle Override Control
	//-------------------------------------------------------------------------------------------
	//key bind setup
	function OptionsDlg::onWake( %this ) {
		if (!$ProBinds) {
			$RemapName[$RemapCount]="\c5ToggleProPack";
			$RemapCmd[$RemapCount]="toggleProPack";
			$RemapCount++;

			$RemapName[$RemapCount]="\c5Last Recall";
			$RemapCmd[$RemapCount]="ActivateReCall";
			$RemapCount++;

			$RemapName[$RemapCount]="\c5LifeTimeKills";
			$RemapCmd[$RemapCount]="ActivateLifeTime";
			$RemapCount++;

			$RemapName[$RemapCount]="\c5Pitch Grenade";
			$RemapCmd[$RemapCount]="ProGrenade";
			$RemapCount++;

			$RemapName[$RemapCount]="\c5Pitch Mine";
			$RemapCmd[$RemapCount]="ProMine";
			$RemapCount++;

			$RemapName[$RemapCount]="\c5Throw RepKit";
			$RemapCmd[$RemapCount]="ProKit";
			$RemapCount++;

			$RemapName[$RemapCount]="\c5Ditch Grenades";
			$RemapCmd[$RemapCount]="DropGrenades";
			$RemapCount++;

			$RemapName[$RemapCount]="\c5Beacon Mine";
			$RemapCmd[$RemapCount]="BeaconMine";
			$RemapCount++;

			$RemapName[$RemapCount]="\c5Teleport Toggle";
			$RemapCmd[$RemapCount]="ProPackTeleportToggle";
			$RemapCount++;

			$RemapName[$RemapCount]="\c5Mute Enemy Team";
			$RemapCmd[$RemapCount]="EnemyMute";
			$RemapCount++;

			$RemapName[$RemapCount]="\c5AutoScreenShot";
			$RemapCmd[$RemapCount]="ActivateAutoSC";
			$RemapCount++;

			$RemapName[$RemapCount]="\c5OneKeyTaunt";
			$RemapCmd[$RemapCount]="TauntLikeHell";
			$RemapCount++;

			$ProBinds = true;
		}
		parent::onWake( %this );
	}
	
	function clientCmdSetPilotVehicleKeys() {
		passengerKeys.copyBind(moveMap, toggleProPack);
		passengerKeys.copyBind(moveMap, ActivateReCall);
		passengerKeys.copyBind(moveMap, ActivateLifeTime);
		passengerKeys.copyBind(moveMap, ProPackTeleportToggle);
		passengerKeys.copyBind(moveMap, EnemyMute);
		passengerKeys.copyBind(moveMap, ActivateAutoSC);
		
		parent::clientCmdSetPilotVehicleKeys();
	}

	function clientCmdSetPassengerVehicleKeys() {
		passengerKeys.copyBind(moveMap, toggleProPack);
		passengerKeys.copyBind(moveMap, ActivateReCall);
		passengerKeys.copyBind(moveMap, ActivateLifeTime);
		passengerKeys.copyBind(moveMap, ProPackTeleportToggle);
		passengerKeys.copyBind(moveMap, EnemyMute);
		passengerKeys.copyBind(moveMap, ActivateAutoSC);
		
		parent::clientCmdSetPassengerVehicleKeys();
	}
	
	function clientCmdSetWeaponsHudActive(%slot, %ret, %vis) {
		parent::clientCmdSetWeaponsHudActive(%slot, %ret, %vis);
		reticleFrameHud.setVisible($ProPackPrefs::retframehud);
		ammoHud.setVisible($ProPackPrefs::ammohud);
	}

	function clientCmdSetFirstPerson(%value) {
		parent::clientCmdSetFirstPerson(%value);
		ammoHud.setVisible($ProPackPrefs::ammohud);
	}

	function ClientCmdDisplayHuds() {
		parent::ClientCmdDisplayHuds();
		ammoHud.setVisible($ProPackPrefs::ammohud);
	}

	function clientcmdTogglePlayHuds(%val) {
		parent::clientcmdTogglePlayHuds(%val);
		ammoHud.setVisible($ProPackPrefs::ammohud);
	}
};
activatePackage(ProPackLoad);