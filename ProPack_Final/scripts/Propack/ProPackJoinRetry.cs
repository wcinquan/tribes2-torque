// #category = ProPack
// #name = ProPack Join & Retry
// #version = 1.21
// #date = October 13, 2001
// #warrior = Neofight
// #description = Join & Retry
// #status = Good enough ;)
// #credit = PJ

package JoinRetry {
	function onConnectRequestRejected( %msg ) {
		switch$(%msg) {

			case "CR_INVALID_CONNECT_PACKET":
				%error = "Network error - badly formed network packet - this should not happen!";
			case "CR_AUTHENTICATION_FAILED":
				%error = "Failed to authenticate with server.  Please restart TRIBES 2 and try again.";
			case "CR_YOUAREBANNED":
				%error = "You are not allowed to play on this server.";
			case "CR_SERVERFULL":
				%error = "This server is full.";
			default:
				%error = "Connection error.  Please try another server.  Error code: (" @ %msg @ ")";
		}
		DisconnectedCleanup();

		if(%msg !$= "CR_SERVERFULL") {
			MessageBoxOK( "REJECTED", %error);
		} else {
			$RetryTimer = 3;
			AutoConnect();
		}
	}

	function AutoConnect() {
		Canvas.popDialog( MessageBoxOKDlg );

		MessageBoxOK("Retrying Server", "<just:center>\n<color:999999>Server: <color:FFFF00>" @ $JoinGameAddress @ "\n<color:999999>This server is <color:FF0000>full<color:999999>.\n<color:999999>Retrying in <color:00FF00>" @ $RetryTimer @ " <color:999999>seconds.", "ConnectButtonResult();" );

		if($RetryTimer == 0) {
			Canvas.popDialog( MessageBoxOKDlg );
			JoinGame($JoinGameAddress);
		} else {
			$RetryTimer--;
			$RetrySchedule = schedule(1000, 0, "AutoConnect");
		}
	}

	function ConnectButtonResult() {
		cancel($RetrySchedule);
		Canvas.popDialog( MessageBoxOKDlg );
	}

	function OP_LaunchScreenMenu::init( %this ) {
		%this.clear();
		%this.add( "Join Game", 1 );
		%this.add( "Host Game", 2 );
		%this.add( "Warrior Setup", 3 );
		%this.add( "Email", 4 );
		%this.add( "Chat", 5 );
		%this.add( "Browser", 6 );
	}

	function OpenLaunchTabs(%gotoWarriorSetup) {
		switch$($pref::Shell::LaunchGui) {
			case "Join Game" :
				GM_TabView.setSelected(1);
			case "Host Game" :
				GM_TabView.setSelected(2);
		}
		parent::OpenLaunchTabs(%gotoWarriorSetup);
	}
};
activatePackage(JoinRetry);