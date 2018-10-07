using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using URIShortener.DTO;
using URIShortener.Models;
using URIShortener.Repositories;
using URIShortener.Util;

namespace URIShortener.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class URIShortenerController : ControllerBase
    {
        private const string FIELD_ORIGINAL = "Original";
        private const string FIELD_ALIAS = "Alias";

        private string _connString;
        private IHostingEnvironment _environment;
        private UriRepository _uriRepository;
        
        public URIShortenerController (IHostingEnvironment environment)
        {
            _environment = environment;
            _connString = "Data Source=" + Path.GetFullPath(_environment.ContentRootPath + "/UriDB.db");
            _uriRepository = new UriRepository(_connString);
        }

        // Get: api/URIShortener
        [HttpGet("{token}")]
        public IActionResult Get(string token)
        {
            if (!String.IsNullOrEmpty(token))
            {
                var model = _uriRepository.FindByField(FIELD_ALIAS, token);
                return Ok(new FormDTO {
                    Uri = model.Original,
                    Alias = model.Alias
                });
            }
            return BadRequest();
        }

        // POST: api/URIShortener
        [HttpPost]
        public IActionResult Post([FromBody] FormDTO formDto)
        {
            try
            {
                Uri uri = new Uri(formDto.Uri);
                formDto.Uri = uri.AbsoluteUri;
            }
            catch (UriFormatException ex)
            {
                var res = new Dictionary<string, string>();
                res.Add("Uri", "Invalid Uri.");
                return BadRequest(res);
            }

            
            var uriModel = _uriRepository.FindByField(FIELD_ORIGINAL, formDto.Uri);
            if (uriModel != null)
            {
                formDto.Alias = GetBaseUrl(Request, uriModel.Alias);
                return Ok(formDto);
            }
            else
            {
                string token = null;
                uriModel = null;
                for (var count = 0; count < 20; count++)
                {
                    token = Token.Generate();
                    uriModel = _uriRepository.FindByField(FIELD_ALIAS, token);
                    if (uriModel == null) break;
                }

                uriModel = new UriModel
                {
                    Original = formDto.Uri,
                    Alias = token
                };

                _uriRepository.Add(uriModel);
                
                formDto.Alias = GetBaseUrl(Request, token);

                return Created("", formDto);
            }
            
        }

        private string GetBaseUrl (HttpRequest req, string aliasToken)
        {
            var s = Request.Scheme;
            var host = Request.Host.ToUriComponent();
            return String.Format("{0}://{1}/{2}", s, host, aliasToken);
        }
        
    }
}
