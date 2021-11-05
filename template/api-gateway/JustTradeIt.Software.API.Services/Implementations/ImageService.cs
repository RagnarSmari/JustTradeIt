using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Amazon;
using Amazon.Internal;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using JustTradeIt.Software.API.Models.Exceptions;
using JustTradeIt.Software.API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Net.Http.Headers;

namespace JustTradeIt.Software.API.Services.Implementations
{
    public class ImageService : IImageService
    {
        private readonly IConfiguration _configuration;
        public ImageService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> UploadImageToBucket(string email, IFormFile image)
        {
            
            var awsconfig = _configuration.GetSection("Aws");
            var bucketName = awsconfig.GetSection("BucketName").Value;
            var KeyId = awsconfig.GetSection("KeyId").Value;
            var keySecret = awsconfig.GetSection("KeySecret").Value;
            string URI = $"https://{bucketName}.s3.eu-west-1.amazonaws.com/";
            IAmazonS3 client = new AmazonS3Client(KeyId,keySecret, RegionEndpoint.EUWest1);


            // Get the file and convert it to the byte[]
            byte[] fileBytes = new Byte[image.Length];
            image.OpenReadStream().Read(fileBytes, 0, Int32.Parse(image.Length.ToString()));
            
            // create unique file name for prevent the mess
            var fileName = Guid.NewGuid() + image.FileName;
            
            var stream = new MemoryStream(fileBytes);
            PutObjectResponse response = null;
            var request = new PutObjectRequest()
            {
                BucketName = bucketName,
                Key = fileName,
                InputStream = stream,
                CannedACL = S3CannedACL.PublicRead,
                ContentType = image.ContentType,
            };
            
            response = await client.PutObjectAsync(request);

            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                return URI + fileName;
            }

            throw new ModelFormatException("Image was not successfully uploaded");

        }
    }
}










