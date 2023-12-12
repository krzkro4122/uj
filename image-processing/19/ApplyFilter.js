// Get the active document and layer
var doc = app.activeDocument;
var layer = doc.activeLayer;

// Apply a Gaussian Blur filter with a radius of 10 pixels
var filterDescriptor = new ActionDescriptor();
var filterReference = new ActionReference();
filterReference.putEnumerated(charIDToTypeID('Lyr '), charIDToTypeID('Ordn'), charIDToTypeID('Trgt'));
filterDescriptor.putReference(charIDToTypeID('null'), filterReference);
var filterSettings = new ActionDescriptor();
filterSettings.putUnitDouble(charIDToTypeID('Rds '), charIDToTypeID('#Pxl'), 10.0);
filterDescriptor.putObject(charIDToTypeID('T   '), charIDToTypeID('GsnB'), filterSettings);
executeAction(charIDToTypeID('Fltr'), filterDescriptor, DialogModes.NO);
