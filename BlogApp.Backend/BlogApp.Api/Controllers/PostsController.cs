﻿using BlogApp.Core.Intefaces;
using BlogApp.Models.InputModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BlogApp.Api.Controllers;

[Route("api/v1/[controller]")]
public class PostsController : ApiControllerBase
{
    private readonly ILogger<PostsController> _logger;
    private readonly IPostsService _postService;

    public PostsController(ILogger<PostsController> logger, IPostsService postService)
    {
        _logger = logger;
        _postService = postService;
    }

    [HttpPost]
    public IActionResult Add([FromBody] Post postModel)
    {
        try
        {
            _postService.Add(postModel);

            return Ok();
        }
        catch (Exception ex)
        {
            return ReturnError(HttpStatusCode.InternalServerError, ex, _logger);
        }
    }

    [HttpGet]
    [Route("{id}")]
    [AllowAnonymous]
    public IActionResult Get(int id)
    {
        try
        {
            var post = _postService.Get(id);

            var postJson = SerializeReturn(post);

            return Ok(postJson);
        }
        catch (Exception ex)
        {
            return ReturnError(HttpStatusCode.InternalServerError, ex, _logger);
        }
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult GetFeedPosts()
    {
        try
        {
            var posts = _postService.GetFeedPosts();

            var jsonPosts = SerializeReturn(posts);

            return Ok(jsonPosts);
        }
        catch (Exception ex)
        {
            return ReturnError(HttpStatusCode.InternalServerError, ex, _logger);
        }
    }
}