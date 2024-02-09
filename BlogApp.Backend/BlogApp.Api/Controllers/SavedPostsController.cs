﻿using BlogApp.Api.Models;
using BlogApp.Core.Intefaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BlogApp.Api.Controllers;

[Route("api/v1/Posts/Saved")]
public class SavedPostsController : ApiControllerBase
{
    private readonly ILogger<SavedPostsController> _logger;
    private readonly ISavedPostsService _savedPostsService;

    public SavedPostsController(ILogger<SavedPostsController> logger, ISavedPostsService savedPostsService)
    {
        _logger = logger;
        _savedPostsService = savedPostsService;
    }

    [HttpPost]
    public IActionResult Save([FromBody] SavedPostModel savedPost)
    {
        try
        {
            _savedPostsService.Save(savedPost.IdPost, savedPost.IdUser);

            return Ok();
        }
        catch (Exception ex)
        {
            return ReturnError(HttpStatusCode.BadRequest, ex, _logger);
        }
    }

    [HttpDelete]
    [Route("{idSavedPost}")]
    public IActionResult Delete(int idSavedPost)
    {
        try
        {
            _savedPostsService.Delete(idSavedPost);

            return Ok();
        }
        catch (Exception ex)
        {
            return ReturnError(HttpStatusCode.BadRequest, ex, _logger);
        }
    }
}