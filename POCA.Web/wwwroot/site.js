function downloadFile(baseUrl, fileName, contentType, content) {
    const link = document.createElement('a');
    link.href = `data:${contentType};base64,${content}`;
    link.download = fileName;
    link.click();
}

// Get dropped files
function getDroppedFiles() {
    return window.lastDroppedFiles;
}

// Setup global drop handler
document.addEventListener('dragover', function (e) {
    e.preventDefault();
});

document.addEventListener('drop', function (e) {
    e.preventDefault();
    window.lastDroppedFiles = e.dataTransfer.files;
});
