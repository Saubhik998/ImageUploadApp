using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ImageUploadApp.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;

namespace ImageUploadApp.Services
{
    public class GridFsService
    {
        private readonly IMongoCollection<ImageTextModel> _collection;
        private readonly GridFSBucket _gridFS;

        //Constructor:initializes the GridFS service with the MongoDB database
        public GridFsService(IMongoDatabase database)
        {
            _collection = database.GetCollection<ImageTextModel>("ImageTextCollection");
            _gridFS = new GridFSBucket(database); //GridFS to store image files
        }

        //fetches all uploaded image-text records from the database
        public async Task<List<ImageTextModel>> GetAllUploadsAsync()
        {
            return await _collection.Find(new BsonDocument()).ToListAsync();
        }

        //retrieves a specific upload by its ID
        public async Task<ImageTextModel> GetUploadByIdAsync(string id)
        {
            return await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        //retrieves an image file from GridFS by its stored ImageId
        public async Task<byte[]> GetImageByIdAsync(string imageId)
        {
            if (ObjectId.TryParse(imageId, out ObjectId objectId))
            {
                return await _gridFS.DownloadAsBytesAsync(objectId); 
            }
            return Array.Empty<byte>(); 
        }

        //inserts a new image-text entry into the database
        public async Task<string> InsertUploadAsync(ImageTextModel model, Stream imageStream, string fileName)
        {
            var imageId = await _gridFS.UploadFromStreamAsync(fileName, imageStream); //upload image to GridFS
            model.ImageId = imageId.ToString(); //store the GridFS image ID in the model
            await _collection.InsertOneAsync(model);
            return model.Id;
        }

        //updates an existing upload, allowing text and image updates
        public async Task<bool> UpdateUploadAsync(ImageTextModel model, Stream? newImageStream, string? newFileName)
        {
            var existingRecord = await GetUploadByIdAsync(model.Id);
            if (existingRecord == null)
            {
                return false; 
            }

            existingRecord.Text = model.Text; 

            //if a new image is provided, replace the old one in GridFS
            if (newImageStream != null && !string.IsNullOrEmpty(newFileName))
            {
                if (!string.IsNullOrEmpty(existingRecord.ImageId))
                {
                    await _gridFS.DeleteAsync(new ObjectId(existingRecord.ImageId)); //delete old image from GridFS
                }

                var newImageId = await _gridFS.UploadFromStreamAsync(newFileName, newImageStream); //upload new image
                existingRecord.ImageId = newImageId.ToString();
            }

            await _collection.ReplaceOneAsync(x => x.Id == model.Id, existingRecord); //update record in the database
            return true;
        }
    }
}
