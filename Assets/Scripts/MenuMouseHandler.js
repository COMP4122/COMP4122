/** 
Mouse Down Event Handler
*/
function OnMouseDown()
{ 
// if we clicked the play button
if (this.name == "PlayBT") 
{ 
// load the game 
Application.LoadLevel("Camp");
}
// if we clicked the help button
else if (this.name == "HelpBT")
{ 
// rotate the camera to view the help "page" 
iTween.RotateTo(Camera.main.gameObject, Vector3(10, 60, 0), 1.0);
}
// if we clicked the Back button
else if (this.name == "BackBT" || this.name == "BackBT2")
{ 
// rotate the camera to view the menu "page" 
iTween.RotateTo(Camera.main.gameObject, Vector3(10, -9, 0), 1.0);
}
else if (this.name == "BoardBT")
{ 
// rotate the camera to view the help "page" 
iTween.RotateTo(Camera.main.gameObject, Vector3(60, -9, 0), 1.0);
}
}
