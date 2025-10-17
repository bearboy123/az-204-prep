using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace FileUploader.Models
{
    public class BlobStorage : IStorage
    {
        private readonly AzureStorageConfig storageConfig;
        private CloudBlobContainer blobContainer;
        private readonly ILogger<BlobStorage> logger;

        public BlobStorage(IOptions<AzureStorageConfig> storageConfig, ILogger<BlobStorage> logger)
        {
            this.storageConfig = storageConfig.Value;
            this.logger = logger;
        }

        public async Task Initialize()
        {
            logger.LogInformation("Initializing BlobStorage");
            try
            {
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(storageConfig.ConnectionString);
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                blobContainer = blobClient.GetContainerReference(storageConfig.FileContainerName);
                await blobContainer.CreateIfNotExistsAsync();
                logger.LogInformation("BlobStorage initialized successfully. Container '{ContainerName}' is ready.", storageConfig.FileContainerName);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error initializing BlobStorage. Please check your connection string and container name.");
                throw;
            }
        }

public Task Save(Stream fileStream, string name)
{
    CloudStorageAccount storageAccount = CloudStorageAccount.Parse(storageConfig.ConnectionString);
    CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
    CloudBlobContainer container = blobClient.GetContainerReference(storageConfig.FileContainerName);
    CloudBlockBlob blockBlob = container.GetBlockBlobReference(name);
    return blockBlob.UploadFromStreamAsync(fileStream);
}

public async Task<IEnumerable<string>> GetNames()
{
    List<string> names = new List<string>();

    CloudStorageAccount storageAccount = CloudStorageAccount.Parse(storageConfig.ConnectionString);
    CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
    CloudBlobContainer container = blobClient.GetContainerReference(storageConfig.FileContainerName);

    BlobContinuationToken continuationToken = null;
    BlobResultSegment resultSegment = null;

    do
    {
        resultSegment = await container.ListBlobsSegmentedAsync(continuationToken);

        // Get the name of each blob.
        names.AddRange(resultSegment.Results.OfType<ICloudBlob>().Select(b => b.Name));

        continuationToken = resultSegment.ContinuationToken;
    } while (continuationToken != null);

    return names;
}

public Task<Stream> Load(string name)
{
    CloudStorageAccount storageAccount = CloudStorageAccount.Parse(storageConfig.ConnectionString);
    CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
    CloudBlobContainer container = blobClient.GetContainerReference(storageConfig.FileContainerName);
    return container.GetBlobReference(name).OpenReadAsync();
}
    }
}