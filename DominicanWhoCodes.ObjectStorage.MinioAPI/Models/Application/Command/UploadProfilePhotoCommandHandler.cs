using System.IO;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Configuration;
using Minio;

namespace DominicanWhoCodes.ObjectStorage.MinioAPI.Models.Application.Command
{
    public class UploadProfilePhotoCommandHandler : IRequestHandler<UploadProfilePhotoCommand, bool>
    {
        private readonly MinioClient _minioClient;
        private readonly IConfiguration _configuration;

        public UploadProfilePhotoCommandHandler(MinioClient minioClient, IConfiguration configuration)
        {
            this._minioClient = minioClient;
            this._configuration = configuration;
        }
        public async Task<bool> Handle(UploadProfilePhotoCommand request, CancellationToken cancellationToken)
        {
            string bucketName = _configuration["MinioConfiguration:UserProfilePhotosBucketName"];
            bool found = await _minioClient.BucketExistsAsync(bucketName);
            if (!found) await _minioClient.MakeBucketAsync(bucketName, cancellationToken: cancellationToken);

            var fileExtension = Path.GetExtension(request.FileName);
            string realFileName = $"{request.UserId.ToString()}{fileExtension}".ToLower();

            var fileStream = GetStreamFromBytes(request.PhotoContent);
            await _minioClient.PutObjectAsync(
                bucketName: bucketName,
                objectName: realFileName,
                data: fileStream,
                size: fileStream.Length
                );

            return true;
        }

        MemoryStream GetStreamFromBytes(byte[] photoContent)
        {
            return new MemoryStream(photoContent);
        }
    }
}
