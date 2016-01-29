#Building
After downloading the project, double click on BnSModBackup.csproj and it should open in Visual Studio. (Note, I used Microsoft Visual Studio Community 2015)
It will probably complain that it cannot find the NewtonSoft namespace.

To fix this do the following:
* Save the solution with CTRL+Shift+S (or File > Save All)
* Right click "References" at the top right in the Solution Explorer mini-window.
* From the right-click-menu select "Manage NuGet Packages..."
* It will bring up the NewtonSoft.Json in a window.
* Note: At the top it will ask you to restore the library in yellow, however this has not worked for me.
* So instead of clicking Restore, click on "Update" on the left side of Visual Studio under the "Uninstall" button

You should be able to build the project now.
