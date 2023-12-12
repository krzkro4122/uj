// Set the folder path
var folderPath = "/path/to/input/folder/";

// Set the action name to apply
var actionName = "YourActionNameHere";

// Get a list of files in the folder
var folder = new Folder(folderPath);
var fileList = folder.getFiles();

// Loop through the files and apply the action
for (var i = 0; i < fileList.length; i++) {
    if (fileList[i] instanceof File && fileList[i].name.match(/\.(jpg|jpeg|png|gif|tif|tiff|psd)$/i)) {
        var file = fileList[i];
        app.open(file);
        app.doAction(actionName, actionName);
        var saveFile = new File("/path/to/output/folder/" + file.name);
        app.activeDocument.saveAs(saveFile);
        app.activeDocument.close(SaveOptions.DONOTSAVECHANGES);
    }
}