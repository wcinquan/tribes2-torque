// #hide
// #category = ProPack
// #name = ProPack Fixes
// #version = 6.91
// #date = November 21, 2001
// #warrior = Neofight
// #description = Compilation of little T2 fixes from a variety of authors
// #status = Growing as fixes are discovered
// #credit = MeBaD, WegBert, Halide, liq, UberGuy, jsut, Neofight

// If the screenshots path doesn't exist, create it.
$ProShotDirectory = new FileObject();
$ProShotDirectory.openForWrite("screenshots/");
$ProShotDirectory.close();
$ProShotDirectory.delete();
$ProShotDirectory = "";

package Fixes {
	// FixRemap.cs
	// by WegBert
	function redoBrokenMapping( %actionMap, %device, %action, %cmd, %newIndex ) {
		%actionMap.bind( %device, %action, %cmd );
		OP_RemapList.setRowById( %newIndex, buildFullMapString( %newIndex ) );
	}

	function RemapInputCtrl::onInputEvent( %this, %device, %action ) {
		Parent::onInputEvent( %this, %device, %action );

		if (%this.mode !$= "consoleKey") {
			switch$ ( OP_ControlsPane.group ) {
				case "Observer":
					%actionMap = observerMap;
					%cmd  = $ObsRemapCmd[%this.index];

				default:
					%actionMap = moveMap;
					%cmd  = $RemapCmd[%this.index];
			}

			%prevMap = %actionMap.getCommand( %device, %action );
			if (%prevMap !$= %cmd && %prevMap !$= "") {
				%mapName = getMapDisplayName( %device, %action );
				%prevMapIndex = findRemapCmdIndex( %prevMap );
				if (%prevMapIndex == -1)	{
					if (MessageBoxOKDlg.isAwake())
						Canvas.popDialog(MessageBoxOKDlg);

					MessageBoxYesNo( "FIXREMAP WARNING",
					                 "\"" @ %mapName @ "\" is bound to the function \"" @ %prevMap @ "\"! The function may exist in a user script. do you still want to undo this mapping?",
					                 "redoBrokenMapping(" @ %actionMap @ ", " @ %device @ ", \"" @ %action @ "\", \"" @ %cmd @ "\", " @ %this.index @ ");", "" );
				}
			}
		}
	}
	// Shrike reticle fix
	// Halide
	function CommanderMapGui::onSleep(%this) {
		parent::onSleep(%this);
		schedule(200,0,"ClientCmdDisplayHuds");
	}
	// FOV Fix version 1.0
	// liq
	function ClientCmdDisplayHuds() {
		parent::ClientCmdDisplayHuds();
		schedule(150, 0, setFov, $pref::Player::defaultFov);
		schedule(1000, 0, setFov, $pref::Player::defaultFov);   // safety net
	}
	// Eliminate CenterPrint
	// MeBaD
	function clientCmdCenterPrint( %message, %time, %lines ) {
		clientCmdBottomPrint( %message, %time, %lines );
	}
	// T1-esque screenshots
	// UberGuy
	function doScreenShot(%val) {
		%fileName = "";
		if(!%val) {
			for(%found = true; %found; $ProPack::screenCount++ ) {
				%suffix = $ProPack::screenCount @ ".";
				while (strlen(%suffix) < 5) %suffix = "0" @ %suffix;
				%fileName = "screenshots/ScreenShot" @ %suffix @ "png";
				%found = isFile(%fileName);
			}
			screenShot("base/" @ %fileName);
		}
	}	
	// .dso deletion on exit
	// UberGuy
	function quit() {

		%cnt = 0;
		%tmpObj = new ScriptObject() {};
		for(%file = findFirstFile("*.dso"); %file !$= ""; %file = findNextFile("*.dso")) {
			%tmpObj.file[%cnt++] = %file;
		}
		for (%i=0; %i<%cnt; %i++) {
			deleteFile(%tmpObj.file[%i]);
		}
		%tmpObj.delete();
		return parent::quit();
	}
	// TeamChat during Debrief GUI
	// UberGuy
	function DebriefGui::onWake(%this) {
		parent::onWake(%this);
		%bind = moveMap.getBinding(TeamMessageHud);
		debriefMap.bind(getField(%bind, 0), getField(%bind, 1), TeamDebriefChat);
	}
	// Distinguish between global chat...
	function toggleDebriefChat() {
		DB_ChatEntry.teamChat = false;
		parent::toggleDebriefChat();
   	}
	// ... and team chat
	function teamDebriefChat() {
		DB_ChatEntry.teamChat = true;
		Canvas.pushDialog(DB_ChatDlg);
	}
	// Send that chat message
	function DB_ChatEntry::sendChat(%this) {
		%text = %this.getValue();
		if (%text !$= "") {
			if (%this.teamChat)	commandToServer('teamMessageSent', %text);
			else commandToServer('messageSent', %text);
		}
		%this.setValue("");
		MessageHud_Edit.setValue("");
		Canvas.popDialog(DB_ChatDlg);
		// No parent call
	}
	// Chathud Fix
	// Qing
	function resizeChatHud( %val ) {
		if ( %val ) {
			MainChatHud.nextChatHudLen();
			for(%i = 1; %i < 20; %i++)
				schedule(%i * 10 ,0,pageDownMessageHud);
		}
	}
	// StaticWaypointFix
	// jsut
	function CommanderTree::processCommand(%this, %command, %target, %typeTag) {
		parent::processCommand(%this, %command, %target, %typeTag);
		// special case?
		if(%typeTag < 0) {
			switch$(getTaggedString(%command)) {
				// waypoints: tree owns the waypoint targets
				case "CreateWayPoint":
				%target.settext(%this.currentWaypointID);
				return;
			}
		}
	}
	// no ammo weapon switch
	// Neofight
	function clientCmdSetAmmoHudCount(%amount) {
		if(%amount == 0)
			nextWeapon(true);
		parent::clientCmdSetAmmoHudCount(%amount);
	}
	
	// Hudmover calls for the new TR2 huds to be moveable
	// Neofight
	function PlayGui::onWake(%this) {
		parent::onWake(%this);
		if(isObject(HM) && isObject(HudMover)) { 
			hudmover::addhud(TR2EventHud, "TR2 Event Hud"); 
			hudmover::addhud(TR2BonusHud, "TR2 Jackpot Hud");
			hudmover::addhud(TR2_ThrowStrength, "TR2 Throw Indicator");
		}
	}
};
activatePackage(Fixes);