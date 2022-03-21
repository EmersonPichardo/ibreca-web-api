using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System;

namespace ibreca_data_access.Contexts.CloudinaryAPI
{
    public class CloudinaryAPI
    {
        private Cloudinary Cloudinary;

        public CloudinaryAPI()
        {
            Account account = new Account("ibreca", "893652356489654", "IIE5nM-YH_l7lJhga3IlmsFMlz8");
            Cloudinary = new Cloudinary(account);
        }

        public void DeleteAsset(string assetId)
        {
            try
            {
                DeletionParams deletionParams = new DeletionParams(assetId);
                var result = Cloudinary.Destroy(deletionParams);
            }
            catch (Exception) { }
        }
    }
}
