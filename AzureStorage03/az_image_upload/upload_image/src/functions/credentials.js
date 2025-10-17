const { app } = require('@azure/functions');
const {
    StorageSharedKeyCredential,
    ContainerSASPermissions,
    generateBlobSASQueryParameters
} = require("@azure/storage-blob");
const { extractConnectionStringParts } = require('./credentials/utils.js');

//     module.exports = async function (context, req) {
//     const permissions = 'c';
//     const container = 'images';
//     context.res = {
//         body: generateSasToken(process.env.AzureWebJobsStorage, container, permissions)
//     };
//     context.done();
// };

function generateSasToken(connectionString, container, permissions) {
    console.log("rrrrrrrr ", connectionString);
    const { accountName, accountKey, url } = extractConnectionStringParts(connectionString);
    console.log("ggggggggg ", accountKey, accountName, url);
    const sharedKeyCredential = new StorageSharedKeyCredential(accountName, accountKey);
    console.log("hhhhhhhhh ", accountKey, accountName, url, sharedKeyCredential);
    var expiryDate = new Date();
    expiryDate.setHours(expiryDate.getHours() + 2);

    const sasKey = generateBlobSASQueryParameters({
        containerName: container,
        permissions: ContainerSASPermissions.parse(permissions),
        expiresOn: expiryDate,
    }, sharedKeyCredential);

    return {
        sasKey: sasKey.toString(),
        url: url
    };
}

app.http('credentials', {
    methods: ['GET', 'POST'],
    authLevel: 'anonymous',
    handler: async (request, context) => {
        // context.log(`Http function processed request for url "${request.url}"`);

        // const name = request.query.get('name') || await request.text() || 'world';

        // return { body: `Hello, ${name}!` };
    const permissions = 'c';
    const container = 'images';
    const token = generateSasToken(process.env.AzureWebJobsStorage, container, permissions);
    return { jsonBody: token };
    }
});
