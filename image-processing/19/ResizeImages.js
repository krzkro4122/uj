// Get all open documents
var documents = app.documents;

// Set the new width and height
var newWidth = 800;
var newHeight = 600;

// Loop through each open document and resize
for (var i = 0; i < documents.length; i++) {
    var doc = documents[i];
    doc.resizeImage(newWidth, newHeight);
}