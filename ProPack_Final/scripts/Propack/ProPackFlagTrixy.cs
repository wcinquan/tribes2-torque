// #category = ProPack
// #name = ProPack Flag Trixy
// #version = 0.1
// #date = June 30, 2001
// #warrior = MeBaD
// #web = http://propack.tribes2.org
// #description = Tricc Flag Stats
// #credit = (From AutoChat: Neofight, Widow, Blotter)
// #status = Workin

package ProPackFlagTrixy {

	function ProPackAutoGotFlag(%msgType, %msgString, %victimname, %vgen, %vposs, %killername, %kgen, %kposs, %damageType) {
		if ($ProPackPrefs::AutoGotFlag) {
			if (Strstr(%msgString, "You took the") != -1) { // Make sure it's you as the taker!
					$ProPackYellHelp = schedule( 5000, 0, "commandToServer", 'CannedChat', 'ChatHelp', false ); // issue's the command

				if ($ProPackPrefs::AutoGrabSpeed) {

					%speed = mFloor(getControlObjectSpeed() * 3.6);

					// New settings for classic
					if (%speed < 100) {
						%saying = $ProPackPrefs::GrabSpeedText1;
					} else if (%speed < 175) {
						%saying = $ProPackPrefs::GrabSpeedText2;
					} else if (%speed < 250) {
						%saying = $ProPackPrefs::GrabSpeedText3;
					} else if (%speed < 300) {
						%saying = $ProPackPrefs::GrabSpeedText4;
					} else {
						%saying = $ProPackPrefs::GrabSpeedText5;
					}

					if (%speed > $ProPackPrefs::FlagGrabClocked) {
						$ProPackPrefs::FlagGrabClocked = %speed;
						$ProPackPrefs::MapGrabSpeed = $MissionName;
						%words = "You clocked a NEW record of: ";
						export("$ProPackPrefs::*", "prefs/ProPackPrefs.cs", false);
					} else {
						%words = "Current Record: ";
					}

					if ($ProPackPrefs::AddPeek) {
						commandtoserver('TeamMessageSent', "\c2" @ %saying @ " \c4[\c1" @ %speed @ " kph\c4] \c2InBound Peek \c4[\c1" @ $ProPackPeekSpd @ " kph\c4]~wflg.flag");
						ClientCmdbottomprint("<color:FF6666>" @ %words @ "<color:6666FF>" @ $ProPackPrefs::FlagGrabClocked @ "        <color:FF6666> Map : <color:6666FF>" @ $ProPackPrefs::MapGrabSpeed @
							     "\n<color:FF6666>Route Peek Record:<color:6666FF>" @ $ProPackPrefs::SpeedPeekRecord, 5, 2);
						$ProPackPeekSpd = 0; // Reset so the outbound route is on it's own
					} else {
						commandtoserver('TeamMessageSent', "\c2" @ %saying @ " \c4[\c1" @ %speed @ " kph\c4]~wflg.flag");
						ClientCmdbottomprint("<color:FF6666>" @ %words @ "<color:6666FF>" @ $ProPackPrefs::FlagGrabClocked @ "        <color:FF6666> Map : <color:6666FF>" @ $ProPackPrefs::MapGrabSpeed, 5);
					}
				}
			}
		}
	}

	function YouDeadWhileRaggin(%msgType, %msgString) {
		if ((detag(%msgType) $= "MsgCTFFlagCapped") && (Strstr(%msgString, "You captured the") != -1)) {
			if ($ProPackPrefs::AutoGotFlag) {
				if ($ProPackPrefs::AddPeek) {
					commandtoserver('TeamMessageSent', "\c2Capped at->\c4[\c1" @ mFloor(getControlObjectSpeed() * 3.6) @ " kph\c4] \c2Return Peek->\c4[\c1" @ $ProPackPeekSpd @ " kph\c4]~wgbl.woohoo");
					$ProPackPeekSpd = 0;
				} else {
					commandtoserver('TeamMessageSent', "\c2Capped at->\c4[\c1" @ mFloor(getControlObjectSpeed() * 3.6) @ " kph\c4]~wgbl.woohoo");
				}
			}
		}
		cancel($ProPackYellHelp); // so you don't yell help for no reason!
	}

	function DispatchLaunchMode() {
		addMessageCallBack('MsgCTFFlagTaken', ProPackAutoGotFlag);
		addMessageCallBack('MsgCTFFlagDropped', YouDeadWhileRaggin);
		addMessageCallBack('MsgCTFFlagCapped', YouDeadWhileRaggin);
		parent::DispatchLaunchMode();
	}
};
activatePackage(ProPackFlagTrixy);