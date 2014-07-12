// #name = Waypointer
// #version = 1.2
// #description = Strategic waypoints
// #status = Release
// #credit = Runar

$CommandTask['Player', 3, text]                 = "\c1N\cramed WP";
$CommandTask['Player', 3, tag]                  = 'wpName';
$CommandTask['Player', 3, hotkey]               = "n";

$CommandTask['Player', 4, text]                 = "\c1N\cramed WP";
$CommandTask['Player', 4, tag]                  = 'wpName';
$CommandTask['Player', 4, hotkey]               = "n";
$CommandTask['Player', 4, enemy]                = true;

$CommandTask['Player', 5, text]                 = "\c11\cr Capper";
$CommandTask['Player', 5, tag]                  = 'wpCapper';
$CommandTask['Player', 5, hotkey]               = "1";

$CommandTask['Player', 6, text]                 = "\c11\cr Capper";
$CommandTask['Player', 6, tag]                  = 'wpCapper';
$CommandTask['Player', 6, hotkey]               = "1";
$CommandTask['Player', 6, enemy]                = true;

$CommandTask['Player', 7, text]                 = "\c12\cr Light O";
$CommandTask['Player', 7, tag]                  = 'wpLO';
$CommandTask['Player', 7, hotkey]               = "2";

$CommandTask['Player', 8, text]                 = "\c12\cr Light O";
$CommandTask['Player', 8, tag]                  = 'wpLO';
$CommandTask['Player', 8, hotkey]               = "2";
$CommandTask['Player', 8, enemy]                = true;

$CommandTask['Player', 9, text]                 = "\c13\cr Mid O";
$CommandTask['Player', 9, tag]                  = 'wpMidO';
$CommandTask['Player', 9, hotkey]               = "3";

$CommandTask['Player', 10, text]                 = "\c13\cr Mid O";
$CommandTask['Player', 10, tag]                  = 'wpMidO';
$CommandTask['Player', 10, hotkey]               = "3";
$CommandTask['Player', 10, enemy]                = true;

$CommandTask['Player', 11, text]                 = "\c14\cr Heavy O";
$CommandTask['Player', 11, tag]                  = 'wpHO';
$CommandTask['Player', 11, hotkey]               = "4";

$CommandTask['Player', 12, text]                 = "\c14\cr Heavy O";
$CommandTask['Player', 12, tag]                  = 'wpHO';
$CommandTask['Player', 12, hotkey]               = "4";
$CommandTask['Player', 12, enemy]                = true;

$CommandTask['Player', 13, text]                 = "\c15\cr Light D";
$CommandTask['Player', 13, tag]                  = 'wpLD';
$CommandTask['Player', 13, hotkey]               = "5";

$CommandTask['Player', 14, text]                 = "\c15\cr Light D";
$CommandTask['Player', 14, tag]                  = 'wpLD';
$CommandTask['Player', 14, hotkey]               = "5";
$CommandTask['Player', 14, enemy]                = true;

$CommandTask['Player', 15, text]                 = "\c16\cr Mid D";
$CommandTask['Player', 15, tag]                  = 'wpMidD';
$CommandTask['Player', 15, hotkey]               = "6";

$CommandTask['Player', 16, text]                 = "\c16\cr Mid D";
$CommandTask['Player', 16, tag]                  = 'wpMidD';
$CommandTask['Player', 16, hotkey]               = "6";
$CommandTask['Player', 16, enemy]                = true;

$CommandTask['Player', 17, text]                 = "\c17\cr Heavy D";
$CommandTask['Player', 17, tag]                  = 'wpHD';
$CommandTask['Player', 17, hotkey]               = "7";

$CommandTask['Player', 18, text]                 = "\c17\cr Heavy D";
$CommandTask['Player', 18, tag]                  = 'wpHD';
$CommandTask['Player', 18, hotkey]               = "7";
$CommandTask['Player', 18, enemy]                = true;

$CommandTask['Player', 19, text]                 = "\c18\cr Farmer";
$CommandTask['Player', 19, tag]                  = 'wpFarmer';
$CommandTask['Player', 19, hotkey]               = "8";

$CommandTask['Player', 20, text]                 = "\c18\cr Farmer";
$CommandTask['Player', 20, tag]                  = 'wpFarmer';
$CommandTask['Player', 20, hotkey]               = "8";
$CommandTask['Player', 20, enemy]                = true;

$CommandTask['Player', 21, text]                 = "\c19\cr Heavy on Flag";
$CommandTask['Player', 21, tag]                  = 'wpHoF';
$CommandTask['Player', 21, hotkey]               = "9";

$CommandTask['Player', 22, text]                 = "\c19\cr Heavy on Flag";
$CommandTask['Player', 22, tag]                  = 'wpHoF';
$CommandTask['Player', 22, hotkey]               = "9";
$CommandTask['Player', 22, enemy]                = true;

$CommandTask['Player', 23, text]                 = "\c10\cr AirForce";
$CommandTask['Player', 23, tag]                  = 'wpAF';
$CommandTask['Player', 23, hotkey]               = "0";

$CommandTask['Player', 24, text]                 = "\c10\cr AirForce";
$CommandTask['Player', 24, tag]                  = 'wpAF';
$CommandTask['Player', 24, hotkey]               = "0";
$CommandTask['Player', 24, enemy]                = true;

package Waypointer
{
	function CommanderTree::processCommand(%this, %command, %target, %typeTag)
	{
		switch$(getTaggedString(%command))
		{
			case "wpName":
        			for(%i = 0; %i < %this.getNumSelectedTargets("Clients"); %i++)
				{
                   			%targetId = %this.getSelectedTarget("Clients", %i);

					if(%target > 0)
						%target = createClientTarget(%targetId, "0 0 0");

					for(%cl = 0; %cl < PlayerListGroup.getCount(); %cl++)
					{
						%obj = PlayerListGroup.getObject(%cl);

						if(%targetid == %obj.targetId)
							%name = %obj.name;
                      			}

					%target.createWaypoint(%name);
         				CMContextPopup.target = -1;
				}
                	return;

			case "wpCapper":
				%target.createWaypoint("Capper " SPC %this.WPcapperID++);
				if(%target.getTargetId() != -1)
				{
					$ClientWaypoints.add(%target);
					CMContextPopup.target = -1;
				}
                	return;

			case "wpLO":
				%target.createWaypoint("Light O " SPC %this.WPloID++);
				if(%target.getTargetId() != -1)
				{
					$ClientWaypoints.add(%target);
					CMContextPopup.target = -1;
				}
                	return;
                	
                	case "wpMidO":
				%target.createWaypoint("Mid O " SPC %this.WPmoID++);
				if(%target.getTargetId() != -1)
				{
					$ClientWaypoints.add(%target);
					CMContextPopup.target = -1;
				}
                	return;

			case "wpHO":
				%target.createWaypoint("Heavy O " SPC %this.WPhoID++);
				if(%target.getTargetId() != -1)
				{
					$ClientWaypoints.add(%target);
					CMContextPopup.target = -1;
				}
                	return;

			case "wpLD":
				%target.createWaypoint("Light D " SPC %this.WPldID++);
				if(%target.getTargetId() != -1)
				{
					$ClientWaypoints.add(%target);
					CMContextPopup.target = -1;
				}
                	return;
                	
                	case "wpMidD":
				%target.createWaypoint("Mid D " SPC %this.WPmdID++);
				if(%target.getTargetId() != -1)
				{
					$ClientWaypoints.add(%target);
					CMContextPopup.target = -1;
				}
                	return;

			case "wpHD":
				%target.createWaypoint("Heavy D " SPC %this.WPhdID++);
				if(%target.getTargetId() != -1)
				{
					$ClientWaypoints.add(%target);
					CMContextPopup.target = -1;
				}
                	return;

			case "wpFarmer":
				%target.createWaypoint("Farmer " SPC %this.WPFarmerID++);
				if(%target.getTargetId() != -1)
				{
					$ClientWaypoints.add(%target);
					CMContextPopup.target = -1;
				}
                	return;

			case "wpHoF":
				%target.createWaypoint("HoF " SPC %this.WPhofID++);
				if(%target.getTargetId() != -1)
				{
					$ClientWaypoints.add(%target);
					CMContextPopup.target = -1;
				}
                	return;

			case "wpAF":
				%target.createWaypoint("AirForce " SPC %this.WPAirForceID++);
				if(%target.getTargetId() != -1)
				{
					$ClientWaypoints.add(%target);
					CMContextPopup.target = -1;
				}
                	return;
		}

		parent::processCommand(%this, %command, %target, %typeTag);
	}


	function CommanderMapGui::reset(%this)
	{
		CommanderTree.WPcapperID = 0;
		CommanderTree.WPloID = 0;
		CommanderTree.WPmoID = 0;
		CommanderTree.WPhoID = 0;
		CommanderTree.WPldID = 0;
		CommanderTree.WPmdID = 0;
		CommanderTree.WPhdID = 0;
		CommanderTree.WPFarmerID = 0;
		CommanderTree.WPhofID = 0;
		CommanderTree.WPAirForceID = 0;

		parent::reset(%this);
	}
};

activatePackage(Waypointer);