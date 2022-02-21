using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using Infrastructure.Exceptions;
using System.Threading;

namespace API.Helpers
{
    public class ImageHellper : Profile
    {
        private static ReaderWriterLockSlim _readWriteLock = new ReaderWriterLockSlim();

        public static string SHA256CheckSum(string filePath)
        {
            using (SHA256 SHA256 = SHA256Managed.Create())
            {
                using (FileStream fileStream = System.IO.File.OpenRead(filePath))
                    return Convert.ToBase64String(SHA256.ComputeHash(fileStream));
            }
        }
        public static string Base64String(string filePath)
        {
            Byte[] bytes = File.ReadAllBytes(filePath);
            return Convert.ToBase64String(bytes);
        }
        public static bool checkIsDownloaded(string ImageName)
        {
            return System.IO.File.Exists(ImageName);
        }
        public static bool DownloadImage(string URL, string ImageName)
        {
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    byte[] data = webClient.DownloadData(URL);

                    using (MemoryStream mem = new MemoryStream(data))
                    {
                        using (var yourImage = Image.FromStream(mem))
                        {
                            yourImage.Save(ImageName, ImageFormat.Jpeg);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new GeneralException(ex.Message);
            }
            return true;
        }
        public static bool DownloadImageSafe(string URL, string ImageName)
        {
            // Set Status to Locked
            _readWriteLock.EnterWriteLock();
            try
            {
                DownloadImage(URL, ImageName);
            }
            finally
            {
                // Release lock
                _readWriteLock.ExitWriteLock();
            }
            return true;
        }
    }
}