window.luuNhuTapTin = function (tenTapTin, byteBase64) {
    const blob = b64sangBlob(byteBase64, 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet');
    const link = document.createElement('a');
    link.href = window.URL.createObjectURL(blob);
    link.download = tenTapTin;
    link.click();
};

function b64sangBlob(b64Data, contentType = '', kichThuocSlice = 512) {
    const byteCharacters = atob(b64Data);
    const byteArrays = [];

    for (let offset = 0; offset < byteCharacters.length; offset += kichThuocSlice) {
        const slice = byteCharacters.slice(offset, offset + kichThuocSlice);

        const byteNumbers = new Array(slice.length);
        for (let i = 0; i < slice.length; i++) {
            byteNumbers[i] = slice.charCodeAt(i);
        }

        const byteArray = new Uint8Array(byteNumbers);
        byteArrays.push(byteArray);
    }

    const blob = new Blob(byteArrays, { type: contentType });
    return blob;
}