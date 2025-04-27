function exportFile(fileName, byteArray) {
    const blob = new Blob([new Uint8Array(byteArray)], { type: "application/pdf" });
    const url = URL.createObjectURL(blob);
    const a = document.createElement("a");
    a.href = url;
    a.download = fileName;
    a.click();
    URL.revokeObjectURL(url);
}