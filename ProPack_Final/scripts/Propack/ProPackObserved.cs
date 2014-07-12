// #category = ProPack
// #name = ProPack Observed
// #version = 2.2
// #date = August 13, 2001
// #warrior = Neofight
// #email = neofight@tribes2.org
// #web = http://propack.tribes2.org
// #description = Can't remember who is watching? Now you know.
// #status = Beta 2
// #credit = MeBaD

package ProPackObserved {

	function ProPackCheckObserv(%msgType, %msgString, %a1, %a2, %a3, %a4, %a5, %a6) {
		%messagetype = detag(%msgType);
		%messagestring = detag(%msgString);
		%observername = detag(%a1);

		if ((%messagetype $= "Observer") && (Strstr(%messagestring, "is now observing you.") != -1)) {
			if ($ProPackPrefs::WaveToObserver) commandToServer('CannedChat', 'ChatAnimWave', false);
			$ProPackObCount++;
			$ProPackObserver[$ProPackObCount] = %observername;
		} else if ((%messagetype $= "ObserverEnd") && (Strstr(%messagestring, "is no longer observing you.") != -1) || (%messagetype $= "MsgClientJoinTeam") || (%messagetype $= "MsgClientDrop")) {
			for (%i = 1; %i <= $ProPackObCount; %i++) {
				if ($ProPackObserver[%i] $= %observername) {
					//echo("Client erased : " @ $ProPackObserver[%i]);
					$ProPackObserver[%i] = "";
					$ProPackObCount--;
					//SqueezeObserverLines(%i);
				}
			}
		}

		%count = "1";
		while ($ProPackObserver[%count] !$= "") {
			%ProPackObserverName = %ProPackObserverName @ $ProPackObserver[%count] @ "\n";
			%count++;
		}
		ProPackObserved.setVisible(true);
		ProPackObservedText.setText("<just:right><color:FF00FF>" @ %ProPackObserverName);
	}

	function SqueezeObserverLines(%num) {
		for (%i = %num; %i <= $ProPackObCount; %i++) {
			%x = %i + 1;
			$ProPackObserver[%i] = $ProPackObserver[%x];
		}

		%count = "1";
		while ($ProPackObserver[%count] !$= "") {
			%ProPackObserverName = %ProPackObserverName @ "\n" @ $ProPackObserver[%count];
			%count++;
		}
		ProPackObservedText.setText("<just:right><color:FF00FF>" @ %ProPackObserverName);
	}

	function ProPackObservedCreate() {
		ProPackObservedDestroy();

		%ProObsX = getWord($pref::Video::resolution, 0) - 146;		//- width of the hud + small space
		%ProObsY = getWord(hudClusterBack.position, 1) + getWord(hudClusterBack.extent, 1) + 2 + 64 + 2;

		new ShellFieldCtrl(ProPackObserved) {
			profile = "GuiConsoleProfile";
			horizSizing = "left";
			vertSizing = "bottom";
			position = %ProObsX SPC %ProObsY;
			extent = "144 100";
			minExtent = "8 8";
			visible = "0";

			new GuiMLTextCtrl(ProPackObservedText) {
				profile = "ProPackTextCtrl";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "0 0";
				extent = "144 100";
				visible = "1";
			};
		};
		playGui.add(ProPackObserved);
	}

	function ProPackObservedDestroy() {
		if (isObject(ProPackObserved)) {
			playGui.remove(ProPackObserved);
			ProPackObserved.delete();
		}
	}

	function clientCmdTogglePlayHuds(%val) {
		ProPackObserved.setVisible(%val);
		parent::clientCmdTogglePlayHuds(%val);
	}
	
	function PlayGui::onWake(%this) {
		parent::onWake(%this);
		if (!isObject(ProPackObserved)) schedule(3000, 0, "ProPackObservedCreate"); 			// POS video prefs
		if(isObject(HM) && isObject(HudMover)) hudmover::addhud(ProPackObserved, "ProPackObserved");
	}

	function DispatchLaunchMode() {
		addMessageCallback('Observer', ProPackCheckObserv);
		addMessageCallback('ObserverEnd', ProPackCheckObserv);
		addMessageCallback('MsgClientJoinTeam', ProPackCheckObserv);
		addMessageCallback('MsgClientDrop', ProPackCheckObserv);
		parent::DispatchLaunchmode();
	}
};
activatePackage(ProPackObserved);