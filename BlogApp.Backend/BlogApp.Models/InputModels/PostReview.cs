﻿using BlogApp.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.Models.InputModels;

public class PostReview
{
    [Required]
    public int IdPost { get; set; }

    [Required]
    public int IdUserReviewer {  get; set; }

    [Required]
    public StatusEnum Status { get; set; }

    public string Feedback {  get; set; }
}