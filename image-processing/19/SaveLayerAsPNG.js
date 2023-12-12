// Get the active document
var doc = app.activeDocument;

// Loop through layers and save visible ones as PNG
for (var i = 0; i < doc.layers.length; i++) {
    var layer = doc.layers[i];
    if (layer.visible) {
        var saveOptions = new ExportOptionsSaveForWeb();
        saveOptions.format = SaveDocumentType.PNG;
        saveOptions.PNG8 = false;
        saveOptions.quality = 100;
        var saveFile = new File("/path/to/save/" + layer.name + ".png");
        doc.exportDocument(saveFile, ExportType.SAVEFORWEB, saveOptions);
    }
}