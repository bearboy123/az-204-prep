function extractConnectionStringParts(connectionString) {
    const parts = connectionString.split(';');
    const map = {};
    parts.forEach(part => {
        const [key, value] = part.split('=');
        map[key] = value;
    });

    const accountName = map['AccountName'];
    const accountKey = Buffer.from(map['AccountKey'], 'base64');
    const endpointSuffix = map['EndpointSuffix'] || 'core.windows.net';
    const protocol = map['DefaultEndpointsProtocol'] || 'https';

    const url = `${protocol}://${accountName}.blob.${endpointSuffix}`;
    return { accountName, accountKey, url };
}

module.exports = { extractConnectionStringParts };