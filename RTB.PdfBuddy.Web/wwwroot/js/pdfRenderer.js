pdfjsLib.GlobalWorkerOptions.workerSrc = "https://cdnjs.cloudflare.com/ajax/libs/pdf.js/3.11.174/pdf.worker.min.js";
window.pdfRenderer = {
    renderPageThumbnail: async function (arrayBuffer) {
        const pdfData = new Uint8Array(arrayBuffer);
        const pdf = await pdfjsLib.getDocument({ data: pdfData }).promise;
        const page = await pdf.getPage(1); // PDF.js uses 1-based page numbers

        const viewport = page.getViewport({ scale: 1 });

        const canvas = document.createElement("canvas");
        canvas.width = viewport.width;
        canvas.height = viewport.height;

        const context = canvas.getContext("2d");
        await page.render({ canvasContext: context, viewport: viewport }).promise;

        return canvas.toDataURL("image/png");
    }
};