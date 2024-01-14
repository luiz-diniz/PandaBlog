﻿namespace BlogApp.Models;

public class SavedPosts
{
    public int Id { get; set; }
    public Post Post { get; set; }
    public User User { get; set; }
    public DateTime SavedDate { get; set; }
}