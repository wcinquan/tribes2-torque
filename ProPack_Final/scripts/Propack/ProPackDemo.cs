// #category = ProPack
// #name = ProPack Demo
// #version = 6.82
// #date = September 29, 2001
// #warrior = Neofight
// #email = neofight@tribes2.org
// #description = Static line like T1's old chat in InfoHud displays when you are recording a demo, reminds you to start one when in tourney mode, automatically names your demos for you and auto-stops them at the end of a map.
// #status = beta 6
// #credit = DEDEN, MeBaD, Blotter, FSB-AO

// Match demo reminder set-up
if(!isObject(MatchDemo)) {
	new ScriptObject(MatchDemo) {
		on = 0;
	};
}

package ProPackDemo {
	function beginDemoRecord() {
		
		error("demo started");
		
		if(isDemo()) return;
   		stopDemoRecord();				// make sure that current recording stream is stopped
		$DemoCount = 0;
		%name = getField( $pref::Player[$pref::Player::Current], 0 );

		for(%found = true; %found; $DemoCount++ ) {
			%demonum = $DemoCount;
			while (strlen(%demonum) < 4) %demonum = "0" @ %demonum;
			%file = "recordings/" @ %name @ " - " @ $MissionName @ " - " @ %demonum @ ".rec";
			%found = isFile(%file);
		}

		$DemoFile = %file;
		$ProInfoDemo = "<color:FF33FF>DEMO";
		AddProInfoLine("<color:FF0000>Demo: <color:FFFF00>[<color:00FF00>" @ $DemoFile @ "<color:FFFF00>]");

   		saveDemoSettings();
   		startRecord(%file);

   		if(!isRecordingDemo()) {						// make sure start worked
 			deleteFile($DemoFile);
			$ProInfoDemo = "";
			AddProInfoLine("<color:FF0000>Demo FAILED: <color:00FF00>[<color:FF0000>" @ $DemoFile @ "<color:00FF00>]");
 			$DemoFile = "";
   		}
	}

	function demoRecordComplete() {
		// tell the user
		if($DemoFile !$= "") {
			$ProInfoDemo = "";
			AddProInfoLine("<color:FF0000>Demo End: <color:00FF00>[<color:FFFF00>" @ $DemoFile @ "<color:00FF00>]");
			$DemoFile = "";
		}
	}
	
	function DemoReset(%msgType, %msgString, %a1, %a2, %a3, %a4, %a5, %a6, %a7, %a8, %a9, %a10) {
		if(strstr(%msgString, "Free For All") != -1)
			MatchDemo.on = 0;
	}
	
	function ProPackDemoRemind(%msgType, %msgString, %time) {
		if( !isRecordingDemo() && !isPlayingDemo() && MatchDemo.on && (strstr(%msgString, "Match starts in") != -1) ) {
			if( %time $= "15") AddProInfoLine("<color:00FF00>Start Demo!" @  $ProInfoDemo);
			if( %time $= "10") AddProInfoLine("<color:FFFF00>Start Demo Now!" @ $ProInfoDemo);
			if( %time $= "5" ) AddProInfoLine("<color:FF0000>Start Demo Now Fool!" @ $ProInfoDemo);
		}
	}

	// --- Overrides ---
	function clientCmdPickTeamMenu(%teamA, %teamB) {
		parent::clientCmdPickTeamMenu( %teamA, %teamB );
		MatchDemo.on = 1;
	}
	
	function scheduleStopDemo() {
		schedule(3000, 0, demoRecordComplete);
		schedule(3000, 0, stopDemoRecord);
	}

	function stopDemoRecord() {
		parent::stopDemoRecord();
		if(MatchDemo.on) MatchDemo.on = 0;
	}
	function DispatchLaunchMode() {
		addmessagecallback('MsgGameOver', scheduleStopDemo);
		addMessageCallback('MsgMissionStart', ProPackDemoRemind);
		addMessageCallback('MsgAdminForce', DemoReset);
		addMessageCallback('MsgVotePassed', DemoReset);
		parent::DispatchLaunchMode();
	}
};
activatepackage(ProPackDemo);