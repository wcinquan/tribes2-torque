// #category = ProPack
// #name = ProPack Flag
// #version = 6.98
// #date = October 7, 2001
// #warrior = Neofight
// #email = neofight@tribes2.org
// #web = http://propack.tribes2.org
// #description = Transparent flag event pop-ups
// #status = beta
// #credit = Crunchy

package ProPackFlag {

	function ProPackFlagHandleReturned(%msgType, %msgString, %clientname, %teamName, %team) {
		%team = StripNameColors(detag(%team));
		%name = StripNameColors(detag(%clientname));

	    	ProPackFlagPopup(%name, %team, "returned", "<color:33FF33>", "<color:FF3333>");
	}

	function ProPackFlagHandleTaken(%msgType, %msgString, %clientname, %teamName, %team) {
		%team = StripNameColors(detag(%team));
		%name = StripNameColors(detag(%clientname));
		
 		ProPackFlagPopup(%name, %team, "took", "<color:FF3333>", "<color:33FF33>");
	}
	
	function ProPackFlagTR2Taken(%msgType, %msgString, %clientname, %teamName, %team) {
		%team = StripNameColors(detag(%team));
		%name = StripNameColors(detag(%clientname));
		
		if ($IHaveFlag) {
			ProPackFlagPopup(%name, %team, "took", "<color:33FF33>", "<color:33FF33>");
		} else if (StrStr(%msgString, "Teammate") != -1) {
			ProPackFlagPopup(%name, %team, "took", "<color:33FF33>", "<color:33FF33>");
		} else {	
	    		ProPackFlagPopup(%name, %team, "took", "<color:FF3333>", "<color:FF3333>");
	    	}
	}

	function ProPackFlagHandleDropped(%msgType, %msgString, %clientname, %teamName, %team) {
		%team = StripNameColors(detag(%team));
		%name = StripNameColors(detag(%clientname));
		
    		ProPackFlagPopup(%name, %team, "dropped", "<color:FFAA33>", "<color:FFAA33>");
	}

	function ProPackFlagHandleCapped(%msgType, %msgString, %clientname, %teamName, %team) {
		%team = StripNameColors(detag(%team));
		%name = StripNameColors(detag(%clientname));

	    	ProPackFlagPopup(%name, %team, "captured", "<color:FF0000>", "<color:00FF00>");
	}
	
	function ProPackFlagPopup(%clientname, %team, %action, %team1color, %team2color) {
		%teamName = $clTeamScore[%team, 0];
		%name = StripNameColors(detag(%clientname));
		%color = (%team == $PPTeam) ? %team1color : %team2color;

		if (ProPackFlag.schedule != 0) cancel(ProPackFlag.schedule);

		if ($IHaveFlag) { 		
			%text = %color@"You" SPC %action SPC "the flag!";		
		} else if ( %name $= "0" || %name $= $PPName ) {
			if (%action $= "took") %action = "taken";		
			%text = %color@"The" SPC %teamname SPC "flag was" SPC %action@"!";
		} else {
			%text = %clientname SPC %color@%action SPC "the" SPC %teamName SPC "flag!";
		}

		ProPackFlagText.setText("<just:center>"@%text);

		if ($ProPackPrefs::FlagHudActive) {
			ProPackFlag.setVisible(true);
		} else {
			ProPackFlag.setVisible(false);
		}

		ProPackFlag.schedule = schedule(5000, 0, eval, "ProPackFlag.setVisible(false);");
		ProPackFlag.schedule = schedule(5000, 0, "ProPackFlagClearPopup");
	}

	function ProPackFlagClearPopup() {
		ProPackFlagText.setText("");
	}

	function ProPackFlagCreate() {
		ProPackFlagDestroy();
		
		%ProFlagX = getWord($pref::Video::resolution, 0) * 0.5 - 160;
		%ProFlagY = getWord($pref::Video::resolution, 1) * 0.8;

		new ShellFieldCtrl(ProPackFlag) {
			profile = "GuiConsoleProfile";
			horizSizing = "center";
			vertSizing = "center";
			position = %ProFlagX SPC %ProFlagY;
			extent = "320 20";
			minExtent = "8 8";
			visible = "1";

			new GuiMLTextCtrl(ProPackFlagText) {
				profile = "ProPackTextCtrl";
				horizSizing = "center";
				vertSizing = "center";
				position = "0 0";
				extent = "318 18";
				visible = "1";
				helpTag = "0";
				lineSpacing = "2";
				allowColorChars = "1";
				maxChars = "-1";
			};
		};
		playgui.add(ProPackFlag);
	}

	function ProPackFlagDestroy() {
		if (isObject(ProPackFlag)) {
			playGui.remove(ProPackFlag);
			ProPackFlag.delete();
		}
	}

	// --- Overrides ---
	function clientCmdTogglePlayHuds(%val) {
		if ( (%val) && ($ProPackPrefs::FlagHudActive) ) {
			ProPackFlag.setVisible(true);
		} else {
			ProPackFlag.setVisible(false);
		}
		parent::clientCmdTogglePlayHuds(%val);
	}
	
	function PlayGui::onWake(%this) {
		parent::onWake(%this);
		if (!isObject(ProPackFlag)) schedule(3000, 0, "ProPackFlagCreate");
		if(isObject(HM) && isObject(HudMover)) hudmover::addhud(ProPackFlag, "ProPackFlag");
	}

	function DispatchLaunchMode() {
		addMessageCallback('MsgCTFFlagTaken', ProPackFlagHandleTaken);
		addMessageCallback('MsgCTFFlagDropped', ProPackFlagHandleDropped);
		addMessageCallback('MsgCTFFlagCapped', ProPackFlagHandleCapped);
		addMessageCallback('MsgCTFFlagReturned', ProPackFlagHandleReturned);
		addMessageCallback('MsgTeamRabbitFlagTaken', ProPackFlagHandleTaken);
		addMessageCallback('MsgTeamRabbitFlagDropped', ProPackFlagHandleDropped);
		addMessageCallback('MsgTeamRabbitFlagReturned', ProPackFlagHandleReturned);
		addMessageCallback('MsgTR2FlagTaken', ProPackFlagTR2Taken);
		addMessageCallback('MsgTR2FlagDropped', ProPackFlagHandleDropped);
		
		parent::DispatchLaunchMode();
	}
};
activatePackage(ProPackFlag);