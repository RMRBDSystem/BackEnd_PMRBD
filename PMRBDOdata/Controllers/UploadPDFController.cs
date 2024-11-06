﻿using Firebase.Auth;
using Firebase.Storage;
using System.Net.Sockets;
using System.Reflection.Metadata;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Repository.IRepository;
using BusinessObject.Models;
using System.Threading;

namespace PMRBDOdata.Controllers
{
    [Route("odata/UploadPDF")]
    [ApiController]
    public class UploadPDFController : ODataController
    {

        private readonly IWebHostEnvironment _env;
        private readonly IEbookRepository _ebookRepository;
        private readonly IConfiguration _configuration;
        public UploadPDFController(IWebHostEnvironment env, IEbookRepository ebookRepository, IConfiguration configuration)
        {
            _env = env;
            _ebookRepository = ebookRepository;
            _configuration = configuration;
        }


        [HttpPost]
        public async Task<IActionResult> UploadPDF(IFormFile image, IFormFile document, [FromForm] string ebookName, [FromForm] string description, [FromForm] int price, [FromForm] int createById)
        {
            string ApiKey = _configuration["FirebaseSettings:ApiKey"];
            string Bucket = _configuration["FirebaseSettings:Bucket"];
            string AuthEmail = _configuration["FirebaseSettings:AuthEmail"];
            string AuthPassword = _configuration["FirebaseSettings:AuthPassword"];
            try
            {
                // Kiểm tra null cho các trường
                if (string.IsNullOrWhiteSpace(ebookName) || string.IsNullOrWhiteSpace(description))
                {
                    return BadRequest("Tên sách và mô tả không được trống");
                }

                // Kiểm tra giá trị của createById
                if (createById <= 0)
                {
                    return BadRequest("ID tạo sách không hợp lệ");
                }

                // Kiểm tra file
                if (document == null || document.Length == 0)
                {
                    return BadRequest("Không có file tài liệu");
                }

                if (image == null || image.Length == 0)
                {
                    return BadRequest("Không có file hình ảnh");
                }

                // Kiểm tra loại file
                var allowedFileTypes = new[] { "application/pdf", "application/vnd.openxmlformats-officedocument.presentationml.presentation", "image/jpeg", "image/png" };
                if (!allowedFileTypes.Contains(document.ContentType) || !allowedFileTypes.Contains(image.ContentType))
                {
                    return BadRequest("Loại file không hợp lệ. Chỉ cho phép file PDF, PPTX, JPEG và PNG");
                }

                // Upload file lên Firebase Storage
                var documentFileName = $"{Path.GetFileNameWithoutExtension(document.FileName)}_{DateTime.Now.Ticks}{Path.GetExtension(document.FileName)}";
                var imageFileName = $"{Path.GetFileNameWithoutExtension(image.FileName)}_{DateTime.Now.Ticks}{Path.GetExtension(image.FileName)}";

                var authProvider = new FirebaseAuthProvider(new FirebaseConfig(ApiKey));
                var authLink = await authProvider.SignInWithEmailAndPasswordAsync(AuthEmail, AuthPassword);

                var cancellationToken = new CancellationTokenSource();
                var firebaseStorage = new FirebaseStorage(
                    Bucket,
                    new FirebaseStorageOptions
                    {
                        AuthTokenAsyncFactory = () => Task.FromResult(authLink.FirebaseToken),
                        ThrowOnCancel = true
                    });

                try
                {
                    // Upload file tài liệu
                    await firebaseStorage.Child("EbookPDF").Child(createById.ToString()).Child(documentFileName).PutAsync(document.OpenReadStream(), cancellationToken.Token);

                    // Upload file hình ảnh
                    await firebaseStorage.Child("EbookImages").Child(createById.ToString()).Child(imageFileName).PutAsync(image.OpenReadStream(), cancellationToken.Token);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Lỗi upload file: {ex.Message}");
                    return BadRequest("Lỗi upload file");
                }

                // Lấy URL download file
                var documentUrl = await firebaseStorage.Child("EbookPDF").Child(createById.ToString()).Child(documentFileName).GetDownloadUrlAsync();
                var imageUrl = await firebaseStorage.Child("EbookImages").Child(createById.ToString()).Child(imageFileName).GetDownloadUrlAsync();

                // Tạo sách
                var ebookEntity = new Ebook
                {
                    EbookName = ebookName,
                    Description = description,
                    Price = price,
                    Status = 1,
                    CreateById = createById,
                    ImageUrl = imageUrl,
                    Pdfurl = documentUrl
                };

                await _ebookRepository.AddEbook(ebookEntity);

                return Ok(new { DocumentUrl = documentUrl, ImageUrl = imageUrl });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi: {ex.Message}");
                return BadRequest("Lỗi");
            }
        }





        [HttpGet("{relativePath}")]
        public async Task<IActionResult> GetPDF([FromODataUri] string relativePath)
        {
            try
            {
                string filePath = Path.Combine("wwwroot", relativePath);

                if (!System.IO.File.Exists(filePath))
                {
                    return NotFound();
                }

                var fileStream = new FileStream(filePath, FileMode.Open);
                return File(fileStream, GetContentType(filePath), Path.GetFileName(filePath));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private string GetContentType(string filePath)
        {
            var extension = Path.GetExtension(filePath);
            switch (extension)
            {
                case ".pdf":
                    return "application/pdf";
                case ".pptx":
                    return "application/vnd.openxmlformats-officedocument.presentationml.presentation";
                default:
                    return "application/octet-stream";
            }
        }


        [HttpDelete]
        public IActionResult DeletePDF([FromBody] DeletePDFRequest relativePath)
        {
            try
            {
                string filePath = Path.Combine("wwwroot", relativePath.RelativePath.TrimStart('~', '/'));
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                    return Ok("File deleted successfully");
                }
                else
                {
                    return NotFound("File not found");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public class DeletePDFRequest
        {
            public string RelativePath { get; set; }
        }
    }
}