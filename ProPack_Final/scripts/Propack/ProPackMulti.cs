// #category = ProPack
// #name = ProPack Multi
// #version = 4.38
// #date = 10/5/2001
// #warrior = Neofight
// #email = neofight@tribes2.org
// #web = http://propack.tribes2.org
// #description = FPS, Ping, Speed, Altitude all toggleable within ProPackInterface
// #status = Beta 3
// #Credit = Crunchy, MeBaD

package ProPackMulti {

	// --- PING ---
	function NetBarHud::infoUpdate(%this, %ping, %packetLoss, %sendPackets, %sendBytes, %receivePackets, %receiveBytes) {
		parent::infoUpdate(%this, %ping, %packetLoss, %sendPackets, %sendBytes, %receivePackets, %receiveBytes);
		$ppping = mFloor(%ping);
	}

	function ProPackPingRateCheck() {
		if ($ppping > 999) $ppping = 999;

		if ($ppping < 75) {
			%ProPackPingText = "00FF00";
		} else if ( ($ppping >= 125) && ($ppping < 150) ) {
			%ProPackPingText = "FFFF00";
		} else  if ($ppping >= 150) {
			%ProPackPingText = "FF0000";
		} else {
			%ProPackPingText = "FFFFFF";
		}
		return "<color:" @ %ProPackPingText @ ">" @ $ppping;
	}

	// --- FPS ---
	function ProPackFPSRateCheck() {

		if ($fps::real < 30) {
			%ProPackFPSText = "FF0000";
		} else if (($fps::real >= 60) && ($fps::real < 90)) {
			%ProPackFPSText = "FFFF00";
		} else  if ($fps::real >= 90) {
			%ProPackFPSText = "00FF00";
		} else {
			%ProPackFPSText = "FFFFFF";
		}
		return "<color:" @ %ProPackFPSText @ ">" @ mFloor($fps::real);
	}

	// --- HUD Text ---
	function ProPackMultiUpdate() {
		cancel($ProPackMultiSchedule);

		%speed = mFloor(getControlObjectSpeed() * 3.6);

		if ($ProPackPrefs::FPSHud) {
			$ProMulti[1] = "<just:left><color:FFFF11>FPS <just:right>" @ ProPackFPSRateCheck() @ "\n";
		}

		if ($ProPackPrefs::PingHud) {
			$ProMulti[2] = "<just:left><color:FFFF11>Ping <just:right>" @ ProPackPingRateCheck() @ "\n";
		}

		if ($ProPackPrefs::SpeedHud) {
			$ProMulti[4] = "<just:left><color:FFFF11>Spd <just:right><color:FFFFFF>" @ %speed @ "\n";

		}

		if ($ProPackPrefs::AltHud) {
			$ProMulti[3] = "<just:left><color:FFFF11>Alt <just:right><color:FFFFFF>" @ getControlObjectAltitude() @ "\n";
		}

		if ($ProPackPrefs::PeekSpeedHud) {
			if (%speed == 0) { // Reset if you stop
				$ProPackPeekSpd = 0;
			} else if ((%speed > $ProPackPeekSpd) && ($ProPackPrefs::AddPeek)) { // Added in for Peek Speed
				$ProPackPeekSpd = %speed;
				// Debug - addMessageHudLine( "\c4***\c2Peek Speed: [" @ $ProPackPeekSpd @ "]\c4***");
			}

			if ($ProPackPrefs::SpeedPeekRecord < %speed) {
				$ProPackPrefs::SpeedPeekRecord = %speed;
			}
			$ProMulti[5] = "<just:left><color:FFFF11>Peek <just:right><color:FFFFFF>" @ $ProPackPeekSpd @ "\n";
		}

		ProPackMultiText.setText($ProMulti[1] @ $ProMulti[2] @ $ProMulti[3] @ $ProMulti[4] @ $ProMulti[5]);

		$ProPackMultiSchedule = schedule(500, 0, "ProPackMultiUpdate");
	}

	
	// --- MultiGUI ---
	function ProPackMultiCreate() {
		ProPackMultiDestroy();

		%ProMultiX = getWord($pref::Video::resolution, 0) - 44;
		%ProMultiY = getWord(hudClusterBack.position, 1) + getWord(hudClusterBack.extent, 1) + 2;

		new ShellFieldCtrl(ProPackMulti) {
			profile = "GuiConsoleProfile";
			horizSizing = "left";
			vertSizing = "bottom";
			position = %ProMultiX SPC %ProMultiY;
			extent = "42 80";
			minExtent = "8 8";
			visible = "1";

			new GuiMLTextCtrl(ProPackMultiText) {
				profile = "ProPackTextCtrl";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "0 0";
				extent = "42 80";
				visible = "1";
			};
		};
		playGui.add(ProPackMulti);

		$ProPackMultiSchedule = schedule(1000, 0, "ProPackMultiUpdate");
	}

	function ProPackMultiDestroy() {
		if ($ProPackMultiSchedule !$= "")
			cancel($ProPackMultiSchedule);

		if (isObject(ProPackMulti)) {
			playGui.remove(ProPackMulti);
			ProPackMulti.delete();
		}
	}

	// --- Overrides ---
	function clientCmdTogglePlayHuds(%val) {
		ProPackMulti.setVisible(%val);
		parent::clientCmdTogglePlayHuds(%val);
	}
	
	function PlayGui::onWake(%this) {
		parent::onWake(%this);
		if (!isobject(ProPackMulti)) {
			schedule(3000, 0, "ProPackMultiCreate");
			schedule(3000, 0, "ProPackMultiUpdate");
		}
		if(isObject(HM) && isObject(HudMover)) hudmover::addhud(ProPackMulti, "ProPackMulti");
	}
};
activatePackage(ProPackMulti);