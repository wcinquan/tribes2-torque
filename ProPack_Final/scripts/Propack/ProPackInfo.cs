// #category = ProPack
// #name = ProPack Info
// #version = 6.52
// #description = Transparent, mesage-filtering, kill-stripping, color-coded, information-rich HUD from hell.
// #status = Beta
// #date = June 3, 2001
// #web = http://propack.tribes2.org
// #warrior = MeBaD
// #credit = Neofight, UberGuy, Ego

package ProPackInfo {

	function ClearProPackInfo() {
		$ProInfoline[1] = "";
		$ProInfoline[2] = "";
		$ProInfoline[3] = "";
		$ProInfoline[4] = "";
		$ProInfoline[5] = "";
		$ProInfoline[6] = "";
		$ProInfoDemo = "";
		$ProInfoTask = "";
		AddProInfoLine(%null);
	}

	function AddProInfoLine(%text, %msgType) {
		$ProInfoline[1] = $ProInfoline[2];
		$ProInfoline[2] = $ProInfoline[3];
		$ProInfoline[3] = $ProInfoline[4];
		$ProInfoline[4] = $ProInfoline[5];
		$ProInfoline[5] = $ProInfoline[6];
		$ProInfoline[6] = %text;

		ProPackInfoText.setText($ProInfoline[1] @ "\n" @
					 $ProInfoline[2] @ "\n" @
					 $ProInfoline[3] @ "\n" @
					 $ProInfoline[4] @ "\n" @
					 $ProInfoline[5] @ "\n" @
					 $ProInfoline[6] @ "\n" @
					 $ProInfoDemo SPC $ProInfoTask);
	}

	function clientTaskCompleted() {
		if((TaskList.currentTask != -1) && isObject(TaskList.currentTask)) {
			%temptask = $ProInfoTask;
			$ProInfoTask = "";
			AddProInfoLine(%temptask @ "<color:0000FF> Completed!");
		}
		parent::clientTaskCompleted();
	}

	function clientCmdAcceptedTask(%desc) {
		$ProInfoTask = "<color:FFFF00>" @ StripNameColors(%desc);
		AddProInfoLine("<color:0000FF>Task:<color:00FF00> " @ $ProInfoTask );
	}

	// This mutes the [V**] keys and fixes the dynamix "error" not allowing us to edit the chat menu :\
	function clientCmdChatMessage(%sender, %voice, %pitch, %msgString, %a1, %a2, %a3, %a4, %a5, %a6, %a7, %a8, %a9, %a10) {

	   %message = detag( %msgString );
	   %voice = detag( %voice );				// Moved the original detag fix to here so all sounds can be used.
	   	
	   	if($ProPackPrefs::MuteSpammers) {
		   	//stfu spammers!!!!!
		   	if($SpammerSTFU[%sender] $= %message) {
		   		return;
		   	}
		   	$SpammerSTFU[%sender] = %message;
		}

		// Mute out [VGX] stuff - Uber Chat with a slight twist
		if ($ProPackPrefs::MuteDefaultKeys) {
			%color = getSubStr(%message,0,1);

			if (%color $= "[") {
				%pos = strpos(%message,"]");
				%color = getSubStr(%message,%pos+2,1); //Skip end ] and color code
				%pos+=3;
			}

			%message = %color @ getSubStr(%message,%pos,strlen(%message)-%pos);
		}

	   if ( ( %message $= "" ) || isClientChatMuted( %sender ) )
	      return;

	   // search for wav tag marker
	   %wavStart = strstr( %message, "~w" );
	   if ( %wavStart == -1 )
	      addMessageHudLine( %message );
	      
	   else
		if(strstr(%message, "~wK") !=-1) {				//THANKS to Ego for the voicepack.cs fix!!!
			%voice = "custom"; 					//Assign our base directory here.
			%wav = getSubStr(%message, %wavStart + 3, 1000); 	//Move the name three spaces for our delim
			%wavFile = "voice/" @ %voice @ "/" @ %wav @ ".wav";
			if (%pitch < 0.5 || %pitch > 2.0)
				%pitch = 1.0;
			%wavLengthMS = alxGetWaveLen(%wavFile) * %pitch;
			if ( $ClientChatHandle[%sender] != 0 )
				alxStop( $ClientChatHandle[%sender] );
			$ClientChatHandle[%sender] = alxCreateSource( AudioChat, %wavFile );
			if (%pitch != 1.0)
				alxSourcef($ClientChatHandle[%sender], "AL_PITCH", %pitch);
			alxPlay( $ClientChatHandle[%sender] );
			%message = getSubStr(%message, 0, %wavStart);
			addMessageHudLine(%message);
		}
	      
	   else
	   {
	      %wav = getSubStr(%message, %wavStart + 2, 1000);
	      if (%voice !$= "")
	         %wavFile = "voice/" @ %voice @ "/" @ %wav @ ".wav";
	      else
	         %wavFile = %wav;
	      //only play voice files that are < 5000ms in length
	      if (%pitch < 0.5 || %pitch > 2.0)
	         %pitch = 1.0;
	      %wavLengthMS = alxGetWaveLen(%wavFile) * %pitch;
	      if (%wavLengthMS < $MaxMessageWavLength )
	      {
	         if ( $ClientChatHandle[%sender] != 0 )
	            alxStop( $ClientChatHandle[%sender] );
	         $ClientChatHandle[%sender] = alxCreateSource( AudioChat, %wavFile );

	         //pitch the handle
	         if (%pitch != 1.0)
	            alxSourcef($ClientChatHandle[%sender], "AL_PITCH", %pitch);
	         alxPlay( $ClientChatHandle[%sender] );
	      }
	      else
	         error( "** WAV file \"" @ %wavFile @ "\" is too long! **" );

	      %message = getSubStr(%message, 0, %wavStart);
	      addMessageHudLine(%message);
	   }
	}

	function addMessageHudLine(%text) {
		if ($ProPackPrefs::MuteToolsActive) {
			// these are the 4 exceptions with either no callback or a shitty one
			if (($ProPackPrefs::MuteToolsHackzorLoadout !$= "") && (Strstr(%text, "Inventory set") != -1 || Strstr(%text, "Inventory updated") != -1)) {
				switch$ ($ProPackPrefs::MuteToolsHackzorLoadout) {
					case "Append-to-InfoHud":
						AddProInfoLine($ProPackPrefs::MuteToolsHackzorLoadout_Append SPC StripNameColors(%text));
					case "Alter/Split-to-InfoHud":
						AddProInfoLine($ProPackPrefs::MuteToolsHackzorLoadout_Append);
					case "BottomPrint":
						clientCmdBottomPrint($ProPackPrefs::MuteToolsHackzorLoadout_Append SPC StripNameColors(%text), 3, 1);
				}
				return;
			}
			if (($ProPackPrefs::MuteToolsHackzorKitPickup !$= "") && (Strstr(%text, "picked up a repair kit.") != -1)) {
				switch$ ($ProPackPrefs::MuteToolsHackzorKitPickup) {
					case "Append-to-InfoHud":
						AddProInfoLine($ProPackPrefs::MuteToolsHackzorKitPickup_Append SPC StripNameColors(%text));
					case "Alter/Split-to-InfoHud":
						AddProInfoLine($ProPackPrefs::MuteToolsHackzorKitPickup_Append);
					case "BottomPrint":
						clientCmdBottomPrint($ProPackPrefs::MuteToolsHackzorKitPickup_Append, 3, 1);
				}
				return;
			}
			if (($ProPackPrefs::MuteToolsHackzorPackOn !$= "") && ((Strstr(%text, "pack on.") != -1) || (Strstr(%text, "pack activated.") != -1))) {
				switch$ ($ProPackPrefs::MuteToolsHackzorPackOn) {
					case "Append-to-InfoHud":
						AddProInfoLine($ProPackPrefs::MuteToolsHackzorPackOn_Append SPC StripNameColors(%text));
					case "Alter/Split-to-InfoHud":
						AddProInfoLine($ProPackPrefs::MuteToolsHackzorPackOn_Append);
					case "BottomPrint":
						clientCmdBottomPrint($ProPackPrefs::MuteToolsHackzorPackOn_Append SPC StripNameColors(%text), 3, 1);
				}
				return;
			}
			if (($ProPackPrefs::MuteToolsHackzorPackOff !$= "") && (Strstr(%text, "pack off.") != -1)) {
				switch$ ($ProPackPrefs::MuteToolsHackzorPackOff) {
					case "Append-to-InfoHud":
						AddProInfoLine($ProPackPrefs::MuteToolsHackzorPackOff_Append SPC StripNameColors(%text));
					case "Alter/Split-to-InfoHud":
						AddProInfoLine($ProPackPrefs::MuteToolsHackzorPackOff_Append);
					case "BottomPrint":
						clientCmdBottomPrint($ProPackPrefs::MuteToolsHackzorPackOff_Append SPC StripNameColors(%text), 3, 1);
				}
				return;
			}
		}
		parent::addMessageHudLine(%text);
	}

	// from Uber's chat with a ProPack twist
	function defaultMessageCallback(%msgType, %msgString, %a1, %a2, %a3, %a4, %a5, %a6, %a7, %a8, %a9, %a10) {

		if ( %msgString $= "" ) return;
		%subType = detag(%msgType);
		%msgs = %msgString;

		if ($ProPackPrefs::MuteToolsActive) { // this IS defaultmessagecallback IF it is muted

			%ProPackCheckMute = $ProPackPrefs::MuteTools[%subType];

			if ((%ProPackCheckMute !$= "") && (%subType !$= "")) { //($ProPackPrefs::InfoHudActive) ?? duh .. it should be anyhow :o
				if (Strstr(%msgString, "picked up a repair kit.") == -1) {
					switch$ (%ProPackCheckMute) {
						case "Append-to-InfoHud":
							%fill = true;
							AddProInfoLine($ProPackPrefs::MuteTools[%subType @ "_Append"] @ StripNameColors(detag(%msgs)));
						case "Alter/Split-to-InfoHud":
							%fill = true;
							AddProInfoLine($ProPackPrefs::MuteTools[%subType @ "_Append"]);
						case "BottomPrint":
							%fill = true;
							clientCmdBottomPrint($ProPackPrefs::MuteTools[%subType @ "_Append"], 3, 1);
						default: // case "Mute":
							%fill = true;
					}
				} else {
					%fill = false; // let it pass to AddMessageHudLine cause it's easier to handle
				}
			}
			if (%fill) {
				%message = detag( %msgString );
				// search for wav tag marker
				%wavStart = strstr( %message, "~w" );
				if ( %wavStart != -1 ) {
					%wav = getSubStr( %message, %wavStart + 2, 1000 );
					%wavLengthMS = alxGetWaveLen( %wav );
					if ( %wavLengthMS <= $MaxMessageWavLength ) {
						alxPlay(alxCreateSource( AudioChat, %wav ));
					} else {
						error( "WAV file \"" @ %wav @ "\" is too long! **" );
					}
				}
				return; // wav only
			}
		}

		if ($ProPackPrefs::CKillsActive) {
			if ( %subType $= "msgSuicide" || %subType $= "msgVehicleCrash" || %subType $= "msgTR2Knockdown" || (strstr(%subType,"Kill") != -1 )) {

				%vict = detag(%a1);
				%kllr = detag(%a4);

				if ($MyPlayerID[%vict].teamId == $PPTeam) {
					if (StripNameColors(%vict) $= $PPName) {
						%Provc = "\c3";
						%vc = "<color:00FFCC>";
					} else {
						%Provc = "\c1";
						%vc = "<color:00FF00>";
					}
				} else {
					%Provc = "\c5";
					%vc = "<color:FF0000>";
				}

				if ($MyPlayerID[%kllr].teamId == $PPTeam) {
					if (StripNameColors(%kllr) $= $PPName) {
						%Prokc = "\c3";
						%kc = "<color:00FFCC>";
					} else {
						%Prokc = "\c1";
						%kc = "<color:00FF00>";
					}
				} else {
					%Prokc = "\c5";
					%kc = "<color:FF0000>";
				}

				if (($ProPackPrefs::DeathMessages) && ($ProPackPrefs::InfoHudActive)) {
					if (%subType $= "msgVehicleCrash")
						%a7 = 36;		// to fix the nexuscamping crap in CTF

					%weapon = ProNoSpacedWeaponName(%a7);  //%weapon = $DamageTypeText[%a7];
					%kllr = StripNameColors(%kllr);
					%vict = StripNameColors(%vict);

					if (%kllr $= %vict) {
						%weapon = "Suicide";
						%vict = "";
					}

					if ((Strlen(%weapon) < 1)) {
						return;
					} else {
						%msg = %kc@%kllr @ " <color:CCFF00>" @ %weapon @ " " @ %vc@%vict;
					}

					AddProInfoLine(%msg);
					return; // mute from normal chat menu
				} else {
					if (%vict !$= "") {
						%msgs = strreplace(%msgs,%vict,%Provc@%vict@"\c0");
					}
					if (%kllr !$= "" && %kllr !$= %vict) {
						%msgs = strreplace(%msgs,%kllr,%Prokc@%kllr@"\c0");
					}
				}
			}
		}
		parent::defaultMessageCallback(%msgType, %msgs, %a1, %a2, %a3, %a4, %a5, %a6, %a7, %a8, %a9, %a10);
	}

	//-------------------------------------
	// Handle ProInfo Chat Menu GUI
	//-------------------------------------
	function ProPackInfoCreate() {
		ProPackInfoDestroy();
		
		%ProInfoX = getWord(outerChatHud.position, 0) + getWord(outerChatHud.extent, 0) + 4;
		%ProInfoY = 0;

		new ShellFieldCtrl(ProPackInfo) {
			profile = "GuiConsoleProfile";
			horizSizing = "center";
			vertSizing = "center";
			position = %ProInfoX SPC %ProInfoY;
			extent = "280 124";
			minExtent = "8 8";
			visible = "1";

			new GuiMLTextCtrl(ProPackInfoText) {
				profile = "ProPackTextCtrl";
				horizSizing = "center";
				vertSizing = "center";
				position = "0 0";
				extent = "280 124";
				minExtent = "4 4";
				visible = "1";
				helpTag = "0";
				lineSpacing = "2";
				allowColorChars = "1";
				maxChars = -1;
			};
		};
		playgui.add(ProPackInfo);
	}

	function ProPackInfoDestroy() {
		if (isObject(ProPackInfo)) {
			playgui.remove(ProPackInfo);
			ProPackInfo.delete();
		}
	}

	// --- Overrides ---
	function clientCmdTogglePlayHuds(%val) {
		if ($ProPackPrefs::InfoHudActive) {
			ProPackInfo.setVisible(%val);
		}
		parent::clientCmdTogglePlayHuds(%val);
	}

	function demoPlaybackComplete() {
		ClearProPackInfo();
		parent::demoPlaybackComplete();
	}

	function Disconnect() {
		ClearProPackInfo();
		parent::Disconnect();
	}
	
	function PlayGui::onWake(%this) {
		parent::onWake(%this);
		if (!isobject(ProPackInfo)) schedule(3000, 0, "ProPackInfoCreate");
		if(isObject(HM) && isObject(HudMover)) hudmover::addhud(ProPackInfo, "ProPackInfo");
	}

	function DispatchLaunchMode() {
		addMessageCallBack('MsgGameOver', ClearProPackInfo);
		addMessageCallBack('MsgArenaRoundEnd', ClearProPackInfo);
		addMessageCallBack('MsgSiegeHalftime', ClearProPackInfo);
		addMessageCallBack('MsgMissionDropInfo', ClearProPackInfo);
		addMessageCallBack('MsgAdminChangeMission', ClearProPackInfo);
		parent::DispatchLaunchMode();
	}
};
activatePackage(ProPackInfo);