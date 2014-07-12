// #category = ProPack
// #name = ProPack Auto Chat
// #version = 6.81
// #date = June 30, 2001
// #warrior = MeBaD
// #web = http://propack.tribes2.org
// #description = Auto Chat functions for ProPack
// #credit = Neofight, Widow, Blotter
// #status = vgrd

package ProPackAutoChat {

	function ProPackAutoGenBoom(%msgType, %msgString, %victimname, %vgen, %vposs, %killername, %kgen, %kposs, %damageType) {
		if ($ProPackPrefs::AutoGenBoom) {
			if (Strstr(%msgString, "You received a") != -1) { // Make sure it's you
				commandtoserver('TeamMessageSent', "Gens dead!~wene.generator");
			}
		}
	}
	function ProPackAutoVPadBoom(%msgType, %msgString, %victimname, %vgen, %vposs, %killername, %kgen, %kposs, %damageType) {
		if ($ProPackPrefs::AutoVPadBoom) {
			if (Strstr(%msgString, "You received a") != -1) { // Make sure it's you
				commandToServer('CannedChat', 'ChatEnemyVehicleDestroyed', false );
			}
		}
	}

	function ProPackAutoTaunt(%msgType, %msgString, %victimname, %vgen, %vposs, %killername, %kgen, %kposs, %damageType) {

		if ($ProPackPrefs::Autotaunt) {

			$ProPackTauntCount++;

			if ($ProPackTauntCount > 7) {
				$ProPackTauntCount = 0;
			}

			%killer = StripNameColors(detag(%killername));

			if ((Strstr($PPName, %killer) != -1) && (Strlen(%killer) > 0)) {
				issueChatCmd( $ProPackAutoTauntCM, $ProPackTauntCount );
			}
		}
	}

	function TauntLikeHell(%this) {
		if (%this) {
			cancel($YOUSPAMMINGBASTARD);
			$ProPackTauntCount++;

			if ($ProPackTauntCount > 7) {
				$ProPackTauntCount = 0;
			}

			issueChatCmd( $ProPackAutoTauntCM, $ProPackTauntCount );

			$YOUSPAMMINGBASTARD = schedule(500, 0, "TauntLikeHell", %this);
		} else {
			cancel($YOUSPAMMINGBASTARD);
		}
	}

	// Enables Editable chat menu's + ProPack Taunt variable
	function addChat(%keyDesc, %command) {
		if ($ProPackAutoTauntCM $= "") {
			$ProPackAutoTauntCM = $CurrentChatMenu;
		}
		%key = firstWord(%keyDesc);
		%text = restWords(%keyDesc);
		%cm = $CurrentChatMenu;

		if (strstr(%command, "~w") != -1) { // with a ~ it must be custom!
			%wav = firstWord(%command);
			%isteam = restWords(%command);
			%NewText = %text @ %wav;
			%cm.bindCmd(keyboard, %key, "ProPackIssueChat(\"" @ %isteam @ "\",\"" @ %NewText @ "\");", "");
		} else {
			parent::addChat(%keyDesc, %command); // so Custom voice packs can work
			return; // so it dosen't screw up the menu counts
		}
		%cm.option[%cm.optionCount] = %key @ ": " @ %text;
		%cm.command[%cm.optionCount] = %command;
		%cm.isMenu[%cm.optionCount] = 0;
		%cm.optionCount++;
	}

	function ProPackIssueChat(%where, %what) {
		if (%where $= "Team") {
			commandtoserver('TeamMessageSent', %what);
		} else {
			commandtoserver('MessageSent', %what);
		}
		cancelChatMenu();
	}

	function DispatchLaunchMode() {
		addMessageCallBack('MsgLegitKill', ProPackAutoTaunt);
		addMessageCallBack('msgGenDes', ProPackAutoGenBoom);
		addMessageCallBack('msgVSDes', ProPackAutoVPadBoom);
		parent::DispatchLaunchMode();
	}

	function EnemyMute(%val) {
		if (%val) {
			for (%i = 0; %i < PlayerListGroup.getCount(); %i++) {
				%object = PlayerListGroup.getObject(%i);

				if (%object.teamid != $PPTeam) {
					if ((%object.chatMuted) && (!$EnemyTeamMuted)) {
						// fixes the double bug stuff
					} else {
						commandToServer( 'TogglePlayerMute', %object.clientId );	//only mutes your enemy
					}
				} else {
					if (%object.chatMuted) commandToServer( 'TogglePlayerMute', %object.clientId );	//clears any muted players on your team
				}
			}
			schedule(1000, 0, "MuteNotice");
		}
	}
		
	function MuteNotice() {
		if ($EnemyTeamMuted) {
			$EnemyTeamMuted = false;
			addMessageHudLine( "\c4*** \c2Enemy Team Unmuted \c4***");
		} else {
			$EnemyTeamMuted = true;
			addMessageHudLine( "\c4*** \c2Enemy Team Muted \c4***");
		}
	}

};
activatePackage(ProPackAutoChat);