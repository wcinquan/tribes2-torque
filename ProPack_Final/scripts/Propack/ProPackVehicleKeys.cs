// #category = ProPack
// #name = ProPack VehicleKeys
// #version = 2.2
// #date = July 29, 2001
// #warrior = Neofight
// #email = neofight@tribes2.org
// #web = http://propack.tribes2.org
// #description = hard-coded vehicle keys = same on all maps; toggleable teleport option
// #status = Good enough
// #Credit = jsut, Silverspirit

package ProVehicleKeys {
	function VehicleHud::onWake(%this) {
		parent::onWake(%this);

		if (isObject(hudMap)) {
			hudMap.pop();
			hudMap.delete();
		}
		new ActionMap(hudMap);
		hudMap.blockBind(moveMap, toggleInventoryHud);
		hudMap.blockBind(moveMap, toggleScoreScreen);
		hudMap.blockBind(moveMap, toggleCommanderMap);
		hudMap.bindCmd(keyboard, escape, "", "VehicleHud.onCancel();" );
		hudMap.bindCmd(keyboard, "1", "", "VehicleHud.QuickBuy( \"scoutVehicle\" );");
		hudMap.bindCmd(keyboard, "2", "", "VehicleHud.QuickBuy( \"AssaultVehicle\" );");
		hudMap.bindCmd(keyboard, "3", "", "VehicleHud.QuickBuy( \"mobileBaseVehicle\" );");
		hudMap.bindCmd(keyboard, "4", "", "VehicleHud.QuickBuy( \"scoutFlyer\" );");
		hudMap.bindCmd(keyboard, "5", "", "VehicleHud.QuickBuy( \"BomberFlyer\" );");
		hudMap.bindCmd(keyboard, "6", "", "VehicleHud.QuickBuy( \"hapcFlyer\" );");
		hudMap.push();
	}

	function VehicleHud::QuickBuy(%this, %id) {
		parent::QuickBuy(%this,$ProQuickBuy[%id]);
	}

	function VehicleHud::addLine(%this, %tag, %lineNum, %name, %count) {
		$ProQuickBuy[%name] = %lineNum;
		return parent::addLine(%this, %tag, %lineNum, %name, %count);
	}

	function VehicleHud::onSleep(%this) {
		parent::onSleep(%this);
		$ProQuickBuy["scoutVehicle"] = "";
		$ProQuickBuy["AssaultVehicle"] = "";
		$ProQuickBuy["mobileBaseVehicle"] = "";
		$ProQuickBuy["scoutFlyer"] = "";
		$ProQuickBuy["BomberFlyer"] = "";
		$ProQuickBuy["hapcFlyer"] = "";
	}

	function ProPackTeleportToggle(%val) {
		if(%val) {
			if($pref::Vehicle::pilotTeleport $= "1") {
				clientCmdBottomPrint( "Teleport Off", 2, 1 );
				$pref::Vehicle::pilotTeleport = "0";
				toggleVehicleTeleportPref();
			} else {
				clientCmdBottomPrint( "Teleport On", 2, 1 );
				$pref::Vehicle::pilotTeleport = "1";
				toggleVehicleTeleportPref();
			}
		}
	}
};
activatePackage(ProVehicleKeys);