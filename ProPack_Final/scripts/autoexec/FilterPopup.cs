// Filter Popup 1.1 by MexicanSquirrel
// Email: dan-martins@home.com

// Changes the filter text to a nice little popup menu.

// New to this version: Added "ADD" and "DELETE" buttons beside the popup that 
// let you add and delete filters quickly and easily. vge (that's 'duh' for those
// of you who haven't played T1 ;)

// Note: if you run in 640x480 resolution the new buttons will not be available to you. They would be, but they won't fit :o

package FilterPopup {


	function FilterPopupLoad() {

		new ShellPopupMenu(FilterPopup) {
			profile = "ShellPopupProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = "46 -3";
			extent = "195 36";
			minExtent = "49 36";
			visible = "1";
			hideCursor = "0";
			bypassHideCursor = "0";
			helpTag = "0";
			maxLength = "255";
			maxPopupHeight = "200";
			buttonBitmap = "gui/shll_pulldown";
			rolloverBarBitmap = "gui/shll_pulldownbar_rol";
			selectedBarBitmap = "gui/shll_pulldownbar_act";
			noButtonStyle = "0";
		};

		if ($pref::Video::Resolution !$= "640 480 16" && $pref::Video::Resolution !$= "640 480 32") {

		new ShellBitmapButton(FilterPopup_Add) {
			profile = "ShellButtonProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = "223 -3";
			extent = "64 38";
			minExtent = "32 38";
			visible = "1";
			hideCursor = "0";
			bypassHideCursor = "0";
			command = "FilterPopup.addNewFilter();";
			helpTag = "0";
			text = "ADD";
			simpleStyle = "0";
		};
		new ShellBitmapButton(FilterPopup_Delete) {
			profile = "ShellButtonProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = "270 -3";
			extent = "64 38";
			minExtent = "32 38";
			visible = "1";
			hideCursor = "0";
			bypassHideCursor = "0";
			command = "FilterPopup.deleteFilter();";
			helpTag = "0";
			text = "DELETE";
			simpleStyle = "0";
		};

		} // if

		$FilterPopupLoaded = 1;

	} // function FilterPopupLoad()


	function FilterPopupSetup() {

		if (!$FilterPopupLoaded) FilterPopupLoad();

		if (isObject(GMJ_FilterText))
			GMJ_FilterText.delete();
		
		GM_JoinPane.remove(FilterPopup);
		GM_JoinPane.add(FilterPopup);
		
		if ($pref::Video::Resolution !$= "640 480 16" && $pref::Video::Resolution !$= "640 480 32") {

			GM_JoinPane.remove(FilterPopup_Add);
			GM_JoinPane.remove(FilterPopup_Delete);

			GM_JoinPane.add(FilterPopup_Add);
			GM_JoinPane.add(FilterPopup_Delete);
		}
		
		FilterPopup.clear();
		
		FilterPopup.add("All Servers", 0, 1);
		FilterPopup.add("Buddies", 1, 1);
		FilterPopup.add("Favorites", 2, 1);
		
		for (%i = 0; $pref::ServerBrowser::Filter[%i] !$=  ""; %i++)
			FilterPopup.add(getField($pref::ServerBrowser::Filter[%i], 0), %i + 3, 1);
		
		FilterPopup.setSelected($pref::ServerBrowser::activeFilter);
	} // FilterPopupSetup()

	function FilterPopup::onSelect(%this, %id, %text) {
		
		// set the selected item as the current filter
		$pref::ServerBrowser::activeFilter = %id;
		GMJ_Browser.runQuery();
	}

	function FilterPopup::addNewFilter() {
		
		$FilterPopupAddingNew = 1;
		canvas.pushDialog(ChooseFilterDlg);
		ChooseFilterDlg.newFilter();
	}

	function FilterPopup::deleteFilter() {

		if ($pref::ServerBrowser::activeFilter > 2) {
			canvas.pushDialog(ChooseFilterDlg);
			ChooseFilterDlg.deleteFilter();
			ChooseFilterDlg.go();
		}
	}

// -------------------------------------
// Overrides
// -------------------------------------

	function GameGui::onWake(%this) {
		parent::onWake(%this);
		FilterPopupSetup();
	}

	function ChooseFilterDlg::onSleep(%this) {
		parent::onSleep(%this);

		FilterPopupSetup();
	}
	
	function FilterEditDlg::onSleep(%this) {
		parent::onSleep(%this);
		if ($FilterPopupAddingNew)
			ChooseFilterDlg.go();
		$FilterPopupAddingNew = 0;
	}
	
}; //package FilterPopup

$FilterPopupLoaded = 0;
$FilterPopupAddingNew = 0;

activatePackage(FilterPopup);
