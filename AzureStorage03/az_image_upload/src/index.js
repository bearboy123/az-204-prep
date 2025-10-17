const { BlockBlobClient, AnonymousCredential } = require("@azure/storage-blob");

blobUpload = async function(file, url, container, sasKey) {
    var blobName = buildBlobName(file);
    console.log("login2 ", file, url, container, sasKey);
    var login = `${url}/${container}/${blobName}?${sasKey}`;
    console.log("login ", login);
    var blockBlobClient = new BlockBlobClient(login, new AnonymousCredential());
    await blockBlobClient.uploadBrowserData(file);
}

function buildBlobName(file) {
    var filename = file.name.substring(0, file.name.lastIndexOf('.'));
    var ext = file.name.substring(file.name.lastIndexOf('.'));
    return filename + '_' + Math.random().toString(16).slice(2) + ext;
}